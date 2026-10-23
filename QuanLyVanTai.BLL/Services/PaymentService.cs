using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// Service trung tâm điều phối các cổng thanh toán trực tuyến và cập nhật trạng thái vào CSDL
    /// </summary>
    public class PaymentService
    {
        private readonly AppDbContext? _injectedContext;
        private readonly PaymentGatewaysSettings _settings;
        private readonly VnPayGatewayService _vnPayService;
        private readonly MoMoGatewayService _moMoService;

        public PaymentGatewaysSettings Settings => _settings;

        public PaymentService(AppDbContext? context = null)
        {
            _injectedContext = context;
            _settings = LoadSettings();
            _vnPayService = new VnPayGatewayService(_settings.VNPay);
            _moMoService = new MoMoGatewayService(_settings.MoMo);
        }

        private AppDbContext CreateContext() => _injectedContext ?? new AppDbContext();
        private bool ShouldDisposeContext => _injectedContext == null;

        private static PaymentGatewaysSettings LoadSettings()
        {
            try
            {
                string configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
                if (File.Exists(configPath))
                {
                    string json = File.ReadAllText(configPath);
                    using var doc = JsonDocument.Parse(json);
                    if (doc.RootElement.TryGetProperty("PaymentGateways", out var pgSection))
                    {
                        var settings = JsonSerializer.Deserialize<PaymentGatewaysSettings>(
                            pgSection.GetRawText(),
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        if (settings != null) return settings;
                    }
                }
            }
            catch { }

            return new PaymentGatewaysSettings();
        }

        public IPaymentGatewayService GetGatewayService(string gatewayName)
        {
            if (string.Equals(gatewayName, "MoMo", StringComparison.OrdinalIgnoreCase))
                return _moMoService;

            return _vnPayService;
        }

        /// <summary>
        /// Khởi tạo phiên thanh toán cho danh sách vé
        /// </summary>
        public async Task<PaymentCreateResponseDto> CreatePaymentSessionAsync(PaymentCreateRequestDto request)
        {
            var gateway = GetGatewayService(request.Gateway);
            return await gateway.CreatePaymentAsync(request);
        }

        /// <summary>
        /// Cập nhật kết quả thanh toán từ callback (Đã thanh toán / Thất bại) vào CSDL
        /// </summary>
        public async Task<bool> UpdatePaymentTransactionResultAsync(
            PaymentCallbackResultDto callbackResult,
            List<int> ticketIds,
            string? username = null,
            int? accountId = null)
        {
            var db = CreateContext();
            try
            {
                using var transaction = await db.Database.BeginTransactionAsync();
                try
                {
                    string targetPaymentStatus = callbackResult.Success ? "Đã thanh toán" : "Thất bại";
                    string targetTicketStatus = callbackResult.Success ? "Booked" : "Cancelled";

                    // 1. Cập nhật các vé trong danh sách
                    var tickets = await db.Tickets
                        .Where(t => ticketIds.Contains(t.Id))
                        .ToListAsync();

                    foreach (var ticket in tickets)
                    {
                        ticket.PaymentStatus = targetPaymentStatus;
                        ticket.TicketStatus = targetTicketStatus;
                        ticket.UpdatedAt = DateTime.UtcNow;
                        ticket.UpdatedBy = username ?? "System_Gateway";
                    }

                    // 2. Lưu giao dịch thanh toán vào bảng PaymentTransactions
                    int? primaryTicketId = tickets.FirstOrDefault()?.Id;
                    var paymentTx = new PaymentTransaction
                    {
                        TransactionCode = callbackResult.TransactionCode,
                        OrderCode = callbackResult.OrderCode,
                        Gateway = callbackResult.Gateway,
                        PaymentMethod = $"{callbackResult.Gateway} Sandbox",
                        Amount = callbackResult.Amount > 0 ? callbackResult.Amount : tickets.Sum(t => t.Price),
                        Status = targetPaymentStatus,
                        OrderInfo = callbackResult.Message,
                        ResponseCode = callbackResult.ResponseCode,
                        TransactionDate = callbackResult.TransactionDate,
                        RawCallbackData = callbackResult.RawData,
                        TicketId = primaryTicketId,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = username ?? "System_Gateway"
                    };

                    db.PaymentTransactions.Add(paymentTx);

                    // 3. Ghi vết Audit Log
                    try
                    {
                        var audit = new AuditLog
                        {
                            Action = callbackResult.Success ? "Thanh toán" : "Hủy thanh toán",
                            EntityName = "PaymentTransaction",
                            RecordId = callbackResult.OrderCode,
                            UserId = accountId,
                            Username = username ?? "Online_Customer",
                            Role = "Khách hàng",
                            OldValues = "PaymentStatus: Pending",
                            NewValues = $"PaymentStatus: {targetPaymentStatus} | Cổng: {callbackResult.Gateway} | Mã GD: {callbackResult.TransactionCode} | Số tiền: {paymentTx.Amount:N0} đ",
                            Timestamp = DateTime.UtcNow,
                            Details = $"Callback kết quả: {callbackResult.Message} (Code: {callbackResult.ResponseCode})"
                        };
                        db.AuditLogs.Add(audit);
                    }
                    catch { }

                    await db.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    System.Diagnostics.Debug.WriteLine($"[PaymentService] Lỗi cập nhật giao dịch CSDL: {ex.Message}");
                    return false;
                }
            }
            finally
            {
                if (ShouldDisposeContext)
                {
                    await db.DisposeAsync();
                }
            }
        }

        /// <summary>
        /// Hủy các vé đang chờ thanh toán khi khách hàng đóng form hoặc hủy bỏ giao dịch
        /// </summary>
        public async Task<bool> CancelPendingPaymentTicketsAsync(List<int> ticketIds, string reason = "Khách hủy thanh toán")
        {
            var db = CreateContext();
            try
            {
                var tickets = await db.Tickets
                    .Where(t => ticketIds.Contains(t.Id) && (t.PaymentStatus == "Pending" || t.PaymentStatus == "Chờ thanh toán"))
                    .ToListAsync();

                if (!tickets.Any()) return true;

                foreach (var t in tickets)
                {
                    t.PaymentStatus = "Thất bại";
                    t.TicketStatus = "Cancelled";
                    t.UpdatedAt = DateTime.UtcNow;
                    t.UpdatedBy = "Customer_Cancel";
                }

                var audit = new AuditLog
                {
                    Action = "Hủy vé",
                    EntityName = "Vé xe",
                    RecordId = string.Join(",", ticketIds),
                    Username = "Customer",
                    Role = "Khách hàng",
                    OldValues = "PaymentStatus: Pending",
                    NewValues = $"PaymentStatus: Thất bại, TicketStatus: Cancelled ({reason})",
                    Timestamp = DateTime.UtcNow
                };
                db.AuditLogs.Add(audit);

                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[PaymentService] Lỗi hủy vé pending: {ex.Message}");
                return false;
            }
            finally
            {
                if (ShouldDisposeContext)
                {
                    await db.DisposeAsync();
                }
            }
        }
    }
}
