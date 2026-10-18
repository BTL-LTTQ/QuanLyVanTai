using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;
using ZXing;
using ZXing.Windows.Compatibility;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// Service nghiệp vụ Quét mã QR & Check-in hành khách lên xe (dành cho Nhân viên / Lơ xe)
    /// </summary>
    public class PassengerCheckInService
    {
        private readonly AppDbContext? _injectedContext;

        public PassengerCheckInService() { }

        public PassengerCheckInService(AppDbContext context)
        {
            _injectedContext = context;
        }

        private AppDbContext CreateContext() => _injectedContext ?? new AppDbContext();
        private bool ShouldDisposeContext => _injectedContext == null;

        /// <summary>
        /// Giải mã nội dung Barcode / QR Code từ ảnh Bitmap trong bộ nhớ
        /// </summary>
        public string? DecodeQrFromBitmap(Bitmap bitmap)
        {
            if (bitmap == null) return null;

            try
            {
                var reader = new BarcodeReader
                {
                    AutoRotate = true,
                    Options = new ZXing.Common.DecodingOptions
                    {
                        TryHarder = true,
                        PossibleFormats = new List<BarcodeFormat>
                        {
                            BarcodeFormat.QR_CODE,
                            BarcodeFormat.CODE_128,
                            BarcodeFormat.CODE_39
                        }
                    }
                };

                var result = reader.Decode(bitmap);
                return result?.Text?.Trim();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Đọc và giải mã mã QR từ tệp tin hình ảnh trên đĩa (PNG, JPG, BMP)
        /// </summary>
        public string? DecodeQrFromFile(string imageFilePath)
        {
            if (string.IsNullOrWhiteSpace(imageFilePath) || !File.Exists(imageFilePath))
                return null;

            try
            {
                using var bitmap = new Bitmap(imageFilePath);
                return DecodeQrFromBitmap(bitmap);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Trích xuất mã vé điện tử từ chuỗi mã QR chuẩn hoặc chuỗi quét trực tiếp
        /// Chuẩn hệ thống: QLVT|MAVE:{TicketCode}|TUYEN:... hoặc chuỗi mã vé thuần TK...
        /// </summary>
        public string ParseTicketCode(string? rawQrOrCode)
        {
            if (string.IsNullOrWhiteSpace(rawQrOrCode))
                return string.Empty;

            string payload = rawQrOrCode.Trim();

            // Trường hợp 1: Chứa khóa MAVE:...
            var match = Regex.Match(payload, @"MAVE[:=]([A-Za-z0-9_\-]+)", RegexOptions.IgnoreCase);
            if (match.Success && match.Groups.Count > 1)
            {
                return match.Groups[1].Value.Trim().ToUpperInvariant();
            }

            // Trường hợp 2: Định dạng phân tách bởi ký tự '|'
            if (payload.Contains('|'))
            {
                var parts = payload.Split('|', StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in parts)
                {
                    if (part.StartsWith("MAVE:", StringComparison.OrdinalIgnoreCase))
                    {
                        return part.Substring(5).Trim().ToUpperInvariant();
                    }
                }
            }

            // Trường hợp 3: Chuỗi mã vé trực tiếp (VD: TK20261001, TICKET-01, ...)
            return payload.ToUpperInvariant();
        }

        /// <summary>
        /// Xử lý Check-in hành khách: đối chiếu cơ sở dữ liệu, kiểm tra hợp lệ và tự động đổi trạng thái sang "Boarded"
        /// </summary>
        public async Task<CheckInResultDto> ProcessCheckInAsync(string qrPayloadOrCode, string? staffUsername = null)
        {
            string ticketCode = ParseTicketCode(qrPayloadOrCode);

            if (string.IsNullOrWhiteSpace(ticketCode))
            {
                return new CheckInResultDto
                {
                    Success = false,
                    Status = CheckInStatus.InvalidQr,
                    Message = "Mã QR hoặc dữ liệu quét không hợp lệ (không tìm thấy mã vé).",
                    CheckInTime = DateTime.Now
                };
            }

            var db = CreateContext();
            try
            {
                var ticket = await db.Tickets
                    .Include(t => t.Route)
                    .Include(t => t.Vehicle)
                    .FirstOrDefaultAsync(t => !t.IsDeleted && t.TicketCode.ToUpper() == ticketCode);

                if (ticket == null)
                {
                    return new CheckInResultDto
                    {
                        Success = false,
                        Status = CheckInStatus.NotFound,
                        TicketCode = ticketCode,
                        Message = $"Không tìm thấy vé '{ticketCode}' trên hệ thống dữ liệu.",
                        CheckInTime = DateTime.Now
                    };
                }

                string routeName = ticket.Route?.RouteName ?? "Tuyến liên tỉnh";
                string vehiclePlate = ticket.Vehicle?.LicensePlate ?? "—";

                // Kiểm tra 1: Vé đã bị hủy
                if (string.Equals(ticket.TicketStatus, "Cancelled", StringComparison.OrdinalIgnoreCase))
                {
                    return new CheckInResultDto
                    {
                        Success = false,
                        Status = CheckInStatus.Cancelled,
                        TicketId = ticket.Id,
                        TicketCode = ticket.TicketCode,
                        CustomerName = ticket.CustomerName,
                        CustomerPhone = ticket.CustomerPhone,
                        RouteName = routeName,
                        VehiclePlate = vehiclePlate,
                        SeatNumber = ticket.SeatNumber,
                        Price = ticket.Price,
                        DepartureTime = ticket.DepartureTime,
                        Message = $"Vé {ticket.TicketCode} ĐÃ BỊ HỦY trước đó! Không được phép cho hành khách lên xe.",
                        CheckInTime = DateTime.Now
                    };
                }

                // Kiểm tra 2: Chưa thanh toán
                if (!string.Equals(ticket.PaymentStatus, "Paid", StringComparison.OrdinalIgnoreCase))
                {
                    return new CheckInResultDto
                    {
                        Success = false,
                        Status = CheckInStatus.Unpaid,
                        TicketId = ticket.Id,
                        TicketCode = ticket.TicketCode,
                        CustomerName = ticket.CustomerName,
                        CustomerPhone = ticket.CustomerPhone,
                        RouteName = routeName,
                        VehiclePlate = vehiclePlate,
                        SeatNumber = ticket.SeatNumber,
                        Price = ticket.Price,
                        DepartureTime = ticket.DepartureTime,
                        Message = $"Vé {ticket.TicketCode} CHƯA THANH TOÁN (Trạng thái: {ticket.PaymentStatus}). Vui lòng thu tiền trước khi cho lên xe!",
                        CheckInTime = DateTime.Now
                    };
                }

                // Kiểm tra 3: Đã Check-in lên xe trước đó
                if (string.Equals(ticket.TicketStatus, "Boarded", StringComparison.OrdinalIgnoreCase))
                {
                    return new CheckInResultDto
                    {
                        Success = false,
                        Status = CheckInStatus.AlreadyBoarded,
                        TicketId = ticket.Id,
                        TicketCode = ticket.TicketCode,
                        CustomerName = ticket.CustomerName,
                        CustomerPhone = ticket.CustomerPhone,
                        RouteName = routeName,
                        VehiclePlate = vehiclePlate,
                        SeatNumber = ticket.SeatNumber,
                        Price = ticket.Price,
                        DepartureTime = ticket.DepartureTime,
                        CheckInTime = ticket.UpdatedAt?.ToLocalTime() ?? DateTime.Now,
                        CheckedInBy = ticket.UpdatedBy ?? "Nhân viên",
                        Message = $"CẢNH BÁO: Vé {ticket.TicketCode} đã được check-in lên xe lúc {(ticket.UpdatedAt?.ToLocalTime().ToString("HH:mm dd/MM/yyyy") ?? "trước đó")}!"
                    };
                }

                // Hợp lệ: Thực hiện Check-in đổi trạng thái vé
                string staff = !string.IsNullOrWhiteSpace(staffUsername) ? staffUsername : "StaffCheckIn";
                ticket.TicketStatus = "Boarded";
                ticket.UpdatedAt = DateTime.UtcNow;
                ticket.UpdatedBy = staff;

                await db.SaveChangesAsync();

                return new CheckInResultDto
                {
                    Success = true,
                    Status = CheckInStatus.Success,
                    TicketId = ticket.Id,
                    TicketCode = ticket.TicketCode,
                    CustomerName = ticket.CustomerName,
                    CustomerPhone = ticket.CustomerPhone,
                    RouteName = routeName,
                    VehiclePlate = vehiclePlate,
                    SeatNumber = ticket.SeatNumber,
                    Price = ticket.Price,
                    DepartureTime = ticket.DepartureTime,
                    CheckInTime = DateTime.Now,
                    CheckedInBy = staff,
                    Message = $"Check-in THÀNH CÔNG! Ghế: {ticket.SeatNumber} - Mời khách {ticket.CustomerName} lên xe."
                };
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
        /// Hoàn tác Check-in (chuyển trạng thái từ Boarded trở lại Booked khi quét nhầm)
        /// </summary>
        public async Task<bool> RevertCheckInAsync(string ticketCode, string? staffUsername = null)
        {
            if (string.IsNullOrWhiteSpace(ticketCode)) return false;

            var db = CreateContext();
            try
            {
                var ticket = await db.Tickets.FirstOrDefaultAsync(t => !t.IsDeleted && t.TicketCode == ticketCode.Trim());
                if (ticket == null || ticket.TicketStatus != "Boarded") return false;

                ticket.TicketStatus = "Booked";
                ticket.UpdatedAt = DateTime.UtcNow;
                ticket.UpdatedBy = staffUsername ?? "StaffCheckIn";

                await db.SaveChangesAsync();
                return true;
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
        /// Lấy danh sách hành khách đã check-in lên xe trong ngày làm việc
        /// </summary>
        public async Task<List<CheckedInPassengerDto>> GetTodayCheckedInTicketsAsync(int? routeId = null)
        {
            var db = CreateContext();
            try
            {
                var todayUtc = DateTime.UtcNow.Date;

                var query = db.Tickets
                    .Include(t => t.Route)
                    .Include(t => t.Vehicle)
                    .Where(t => !t.IsDeleted && t.TicketStatus == "Boarded");

                if (routeId.HasValue && routeId.Value > 0)
                {
                    query = query.Where(t => t.RouteId == routeId.Value);
                }

                // Lấy các vé check-in trong ngày hôm nay hoặc sắp xếp theo thời gian mới nhất
                var list = await query
                    .OrderByDescending(t => t.UpdatedAt)
                    .Take(50)
                    .ToListAsync();

                return list.Select(t => new CheckedInPassengerDto
                {
                    TicketId = t.Id,
                    TicketCode = t.TicketCode,
                    CustomerName = t.CustomerName,
                    CustomerPhone = t.CustomerPhone,
                    SeatNumber = t.SeatNumber,
                    RouteName = t.Route?.RouteName ?? "—",
                    VehiclePlate = t.Vehicle?.LicensePlate ?? "—",
                    Price = t.Price,
                    DepartureTime = t.DepartureTime,
                    CheckInTime = t.UpdatedAt?.ToLocalTime() ?? t.CreatedAt.ToLocalTime(),
                    CheckedInBy = t.UpdatedBy ?? "—"
                }).ToList();
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
