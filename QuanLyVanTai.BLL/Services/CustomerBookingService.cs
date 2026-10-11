using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// Service xử lý nghiệp vụ đặt vé khách hàng, áp dụng khuyến mãi, giao dịch CSDL và chuẩn bị dữ liệu xuất vé
    /// </summary>
    public class CustomerBookingService
    {
        private static AppDbContext CreateContext() => new AppDbContext();

        /// <summary>
        /// Xác thực và tính toán giá trị chiết khấu của mã khuyến mãi
        /// </summary>
        public async Task<PromotionVerifyResultDto> VerifyPromotionAsync(string? code, decimal subTotal)
        {
            await Task.Yield(); // Cho phép chạy bất đồng bộ trơn tru

            if (string.IsNullOrWhiteSpace(code))
            {
                return new PromotionVerifyResultDto
                {
                    IsValid = false,
                    Message = "Chưa nhập mã khuyến mãi.",
                    DiscountAmount = 0,
                    FinalTotal = subTotal
                };
            }

            string cleanCode = code.Trim().ToUpperInvariant();

            // Danh sách khuyến mãi tiêu chuẩn
            switch (cleanCode)
            {
                case "TET2026":
                    {
                        decimal discount = Math.Round(subTotal * 0.20m, 0);
                        if (discount > 300000m) discount = 300000m; // Tối đa 300,000 VNĐ
                        return new PromotionVerifyResultDto
                        {
                            IsValid = true,
                            PromotionCode = cleanCode,
                            Description = "Khuyến mãi Tết 2026 - Giảm 20% (Tối đa 300k)",
                            DiscountPercent = 20,
                            DiscountAmount = discount,
                            FinalTotal = Math.Max(0, subTotal - discount),
                            Message = "Áp dụng thành công mã Tết 2026 (-20%)!"
                        };
                    }

                case "VIP10":
                    {
                        decimal discount = Math.Round(subTotal * 0.10m, 0);
                        return new PromotionVerifyResultDto
                        {
                            IsValid = true,
                            PromotionCode = cleanCode,
                            Description = "Khách hàng thân thiết - Giảm 10%",
                            DiscountPercent = 10,
                            DiscountAmount = discount,
                            FinalTotal = Math.Max(0, subTotal - discount),
                            Message = "Áp dụng thành công mã VIP10 (-10%)!"
                        };
                    }

                case "GIAM50K":
                    {
                        decimal discount = (subTotal >= 150000m) ? 50000m : 0m;
                        if (discount == 0)
                        {
                            return new PromotionVerifyResultDto
                            {
                                IsValid = false,
                                PromotionCode = cleanCode,
                                Message = "Mã GIAM50K chỉ áp dụng cho đơn từ 150.000 VNĐ trở lên!",
                                FinalTotal = subTotal
                            };
                        }
                        return new PromotionVerifyResultDto
                        {
                            IsValid = true,
                            PromotionCode = cleanCode,
                            Description = "Giảm trực tiếp 50.000 VNĐ",
                            DiscountPercent = 0,
                            DiscountAmount = discount,
                            FinalTotal = Math.Max(0, subTotal - discount),
                            Message = "Đã giảm 50.000 VNĐ vào tổng tiền!"
                        };
                    }

                case "CHAOBANMOI":
                    {
                        decimal discount = Math.Round(subTotal * 0.15m, 0);
                        return new PromotionVerifyResultDto
                        {
                            IsValid = true,
                            PromotionCode = cleanCode,
                            Description = "Chào bạn mới - Giảm 15%",
                            DiscountPercent = 15,
                            DiscountAmount = discount,
                            FinalTotal = Math.Max(0, subTotal - discount),
                            Message = "Áp dụng thành công mã Chào Bạn Mới (-15%)!"
                        };
                    }

                default:
                    return new PromotionVerifyResultDto
                    {
                        IsValid = false,
                        PromotionCode = cleanCode,
                        Message = $"Mã khuyến mãi '{cleanCode}' không tồn tại hoặc đã hết hạn!",
                        DiscountAmount = 0,
                        FinalTotal = subTotal
                    };
            }
        }

        /// <summary>
        /// Tạo và đặt đồng thời N vé trong Transaction an toàn của EF Core
        /// </summary>
        public async Task<BookingResultDto> BookTicketsAsync(BookingRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.CustomerName))
                return new BookingResultDto { Success = false, Message = "Họ và tên khách hàng không được để trống!" };

            if (string.IsNullOrWhiteSpace(request.CustomerPhone))
                return new BookingResultDto { Success = false, Message = "Số điện thoại không được để trống!" };

            if (request.SelectedSeats == null || request.SelectedSeats.Count == 0)
                return new BookingResultDto { Success = false, Message = "Vui lòng chọn ít nhất 1 chỗ ngồi trên sơ đồ!" };

            using var db = CreateContext();
            using var transaction = await db.Database.BeginTransactionAsync();

            try
            {
                var vehicle = await db.Vehicles
                    .Include(v => v.Route)
                    .FirstOrDefaultAsync(v => v.Id == request.VehicleId);

                if (vehicle == null)
                    return new BookingResultDto { Success = false, Message = "Phương tiện không tồn tại!" };

                int routeId = request.RouteId > 0 ? request.RouteId : (vehicle.RouteId ?? 0);
                var route = await db.Routes.FindAsync(routeId);
                if (route == null)
                    return new BookingResultDto { Success = false, Message = "Không tìm thấy thông tin tuyến xe!" };

                decimal unitPrice = route.BasePrice > 0 ? route.BasePrice : 200000m;

                // 1. Kiểm tra tranh chấp ghế đồng thời (Concurrency check)
                var bookedSeats = await db.Tickets
                    .Where(t => t.VehicleId == request.VehicleId &&
                                t.DepartureTime.Date == request.DepartureTime.Date &&
                                t.DepartureTime.Hour == request.DepartureTime.Hour &&
                                t.TicketStatus != "Cancelled" &&
                                request.SelectedSeats.Contains(t.SeatNumber))
                    .Select(t => t.SeatNumber)
                    .ToListAsync();

                if (bookedSeats.Any())
                {
                    await transaction.RollbackAsync();
                    return new BookingResultDto
                    {
                        Success = false,
                        Message = $"Rất tiếc! Ghế [{string.Join(", ", bookedSeats)}] vừa được người khác đặt trước. Vui lòng chọn ghế khác!"
                    };
                }

                // 2. Tính tiền và khuyến mãi
                decimal subTotal = unitPrice * request.SelectedSeats.Count;
                var promoResult = await VerifyPromotionAsync(request.PromotionCode, subTotal);
                decimal discountPerSeat = promoResult.DiscountAmount / request.SelectedSeats.Count;

                string datePrefix = "TK" + DateTime.Now.ToString("yyyyMMdd");
                int currentCount = await db.Tickets.CountAsync(t => t.TicketCode.StartsWith(datePrefix));

                var createdTickets = new List<Ticket>();
                var createdTicketCodes = new List<string>();

                for (int i = 0; i < request.SelectedSeats.Count; i++)
                {
                    string seat = request.SelectedSeats[i];
                    currentCount++;
                    string ticketCode = $"{datePrefix}{currentCount:D3}";

                    decimal seatFinalPrice = Math.Max(0, unitPrice - discountPerSeat);

                    var newTicket = new Ticket
                    {
                        TicketCode = ticketCode,
                        CustomerName = request.CustomerName.Trim(),
                        CustomerPhone = request.CustomerPhone.Trim(),
                        SeatNumber = seat,
                        Price = seatFinalPrice,
                        DepartureTime = request.DepartureTime,
                        PaymentStatus = "Paid",
                        TicketStatus = "Booked",
                        RouteId = route.Id,
                        VehicleId = vehicle.Id,
                        AccountId = request.AccountId,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = request.CustomerName.Trim()
                    };

                    db.Tickets.Add(newTicket);
                    createdTickets.Add(newTicket);
                    createdTicketCodes.Add(ticketCode);
                }

                // Lưu các vé
                await db.SaveChangesAsync();

                // 3. Ghi vết Audit Log
                try
                {
                    var auditLog = new AuditLog
                    {
                        Action = "Thêm",
                        EntityName = "Vé xe",
                        RecordId = createdTickets.First().Id.ToString(),
                        UserId = request.AccountId,
                        Username = request.CustomerName.Trim(),
                        Role = "Khách hàng",
                        OldValues = null,
                        NewValues = $"Đặt {createdTickets.Count} vé ({string.Join(", ", request.SelectedSeats)}) cho khách {request.CustomerName} - SĐT: {request.CustomerPhone}. Tổng: {promoResult.FinalTotal:N0} VNĐ",
                        Timestamp = DateTime.UtcNow,
                        Details = $"Mã vé: {string.Join(", ", createdTicketCodes)}"
                    };
                    db.AuditLogs.Add(auditLog);
                    await db.SaveChangesAsync();
                }
                catch
                {
                    // Audit log không làm đổ transaction chính nếu có lỗi phụ
                }

                await transaction.CommitAsync();

                return new BookingResultDto
                {
                    Success = true,
                    Message = $"Đặt {createdTickets.Count} vé thành công! Mã: {string.Join(", ", createdTicketCodes)}",
                    CreatedTicketIds = createdTickets.Select(t => t.Id).ToList(),
                    CreatedTicketCodes = createdTicketCodes,
                    TotalAmount = subTotal,
                    DiscountAmount = promoResult.DiscountAmount,
                    FinalAmount = promoResult.FinalTotal
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new BookingResultDto
                {
                    Success = false,
                    Message = $"Lỗi hệ thống khi đặt vé: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Lấy chi tiết vé phục vụ in ấn và xuất PDF nhúng mã QR
        /// </summary>
        public async Task<List<TicketReceiptDto>> GetTicketReceiptsAsync(List<int> ticketIds)
        {
            using var db = CreateContext();

            var tickets = await db.Tickets
                .Include(t => t.Route)
                .ThenInclude(r => r!.Stations)
                .Include(t => t.Vehicle)
                .Include(t => t.Account)
                .Where(t => ticketIds.Contains(t.Id))
                .ToListAsync();

            var result = new List<TicketReceiptDto>();

            foreach (var t in tickets)
            {
                string routeName = t.Route?.RouteName ?? "Tuyến liên tỉnh";
                string routeCode = t.Route?.RouteCode ?? "HN-DN-01";
                string plate = t.Vehicle?.LicensePlate ?? "29B-123.45";
                string vType = t.Vehicle?.VehicleType ?? "Giường nằm 40 chỗ";
                string depStation = t.Route?.Stations?.FirstOrDefault()?.StationName ?? "Bến xe xuất phát";
                string arrStation = t.Route?.Stations?.LastOrDefault()?.StationName ?? "Bến xe đích";
                string staff = t.Account?.FullName ?? "Nhân viên bán vé";

                int floor = 1;
                if (t.SeatNumber.StartsWith("B", StringComparison.OrdinalIgnoreCase)) floor = 2;
                else if (t.SeatNumber.StartsWith("P", StringComparison.OrdinalIgnoreCase) && int.TryParse(t.SeatNumber.Substring(1), out int pNum) && pNum > 16) floor = 2;

                // Chuỗi mã QR tiêu chuẩn: QLVT|MAVE:...|TUYEN:...|GHE:...|GIA:...|NGAY:...
                string qrData = $"QLVT|MAVE:{t.TicketCode}|TUYEN:{routeCode}|GHE:{t.SeatNumber}|GIA:{t.Price:0}|NGAY:{t.DepartureTime:yyyy-MM-dd HH:mm}";

                result.Add(new TicketReceiptDto
                {
                    TicketId = t.Id,
                    TicketCode = t.TicketCode,
                    CustomerName = t.CustomerName,
                    CustomerPhone = t.CustomerPhone,
                    RouteName = routeName,
                    RouteCode = routeCode,
                    LicensePlate = plate,
                    VehicleType = vType,
                    SeatNumber = t.SeatNumber,
                    Floor = floor,
                    DepartureTime = t.DepartureTime,
                    DepartureStation = depStation,
                    ArrivalStation = arrStation,
                    OriginalPrice = t.Price,
                    DiscountAmount = 0,
                    FinalPrice = t.Price,
                    PaymentStatus = t.PaymentStatus,
                    TicketStatus = t.TicketStatus,
                    BookingTime = t.CreatedAt.ToLocalTime(),
                    StaffName = staff,
                    QrCodeContent = qrData
                });
            }

            return result;
        }
    }
}
