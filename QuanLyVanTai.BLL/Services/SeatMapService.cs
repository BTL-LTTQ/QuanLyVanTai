using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// Service sinh cấu trúc sơ đồ ghế & truy vấn tình trạng đặt chỗ theo loại xe và chuyến
    /// </summary>
    public class SeatMapService
    {
        private static AppDbContext CreateContext() => new AppDbContext();

        /// <summary>
        /// Xác định loại layout dựa trên số chỗ
        /// </summary>
        public static VehicleLayoutType DetermineLayoutType(string? vehicleType, int totalSeats)
        {
            if (totalSeats == 9) return VehicleLayoutType.Seater9;
            if (totalSeats == 28) return VehicleLayoutType.Seater28;
            if (totalSeats == 45) return VehicleLayoutType.Seater45;
            if (totalSeats == 24) return VehicleLayoutType.Sleeper24;
            if (totalSeats == 34) return VehicleLayoutType.Sleeper34;
            
            return VehicleLayoutType.Seater28; // Default fallback
        }

        /// <summary>
        /// Tạo danh sách ghế theo loại layout phương tiện
        /// </summary>
        public static List<SeatVisualItem> GenerateSeatTemplate(VehicleLayoutType layoutType, int totalSeats, decimal basePrice)
        {
            var seats = new List<SeatVisualItem>();

            switch (layoutType)
            {
                case VehicleLayoutType.Seater9:
                    // Ghế ngồi 9 chỗ: Row 1(C3,4), Row 2,3(C1,4), Row 4(C1,3,4)
                    seats.Add(new SeatVisualItem { SeatCode = "01", Row = 1, Column = 3, FinalPrice = basePrice * 1.1m, Description = "Ghế cạnh tài xế" });
                    seats.Add(new SeatVisualItem { SeatCode = "02", Row = 1, Column = 4, FinalPrice = basePrice * 1.1m, Description = "Ghế cạnh tài xế" });
                    
                    seats.Add(new SeatVisualItem { SeatCode = "03", Row = 2, Column = 1, FinalPrice = basePrice, Description = "Ghế Limousine đơn" });
                    seats.Add(new SeatVisualItem { SeatCode = "04", Row = 2, Column = 4, FinalPrice = basePrice, Description = "Ghế Limousine đơn" });
                    
                    seats.Add(new SeatVisualItem { SeatCode = "05", Row = 3, Column = 1, FinalPrice = basePrice, Description = "Ghế Limousine đơn" });
                    seats.Add(new SeatVisualItem { SeatCode = "06", Row = 3, Column = 4, FinalPrice = basePrice, Description = "Ghế Limousine đơn" });
                    
                    seats.Add(new SeatVisualItem { SeatCode = "07", Row = 4, Column = 1, FinalPrice = basePrice, Description = "Ghế Limousine băng cuối" });
                    seats.Add(new SeatVisualItem { SeatCode = "08", Row = 4, Column = 3, FinalPrice = basePrice, Description = "Ghế Limousine băng cuối" });
                    seats.Add(new SeatVisualItem { SeatCode = "09", Row = 4, Column = 4, FinalPrice = basePrice, Description = "Ghế Limousine băng cuối" });
                    break;

                case VehicleLayoutType.Seater28:
                    // Ghế ngồi 28 chỗ: 8 hàng đầu cột 1,2, 4. Hàng cuối cột 1,2,3,4.
                    int s28 = 1;
                    for (int r = 1; r <= 8; r++)
                    {
                        seats.Add(new SeatVisualItem { SeatCode = $"{s28:D2}", Row = r, Column = 1, FinalPrice = basePrice, Description = "Ghế cửa sổ trái" }); s28++;
                        seats.Add(new SeatVisualItem { SeatCode = $"{s28:D2}", Row = r, Column = 2, FinalPrice = basePrice, Description = "Ghế lối đi trái" }); s28++;
                        seats.Add(new SeatVisualItem { SeatCode = $"{s28:D2}", Row = r, Column = 4, FinalPrice = basePrice, Description = "Ghế phải" }); s28++;
                    }
                    // Hàng 9
                    seats.Add(new SeatVisualItem { SeatCode = $"{s28:D2}", Row = 9, Column = 1, FinalPrice = basePrice, Description = "Ghế hàng cuối" }); s28++;
                    seats.Add(new SeatVisualItem { SeatCode = $"{s28:D2}", Row = 9, Column = 2, FinalPrice = basePrice, Description = "Ghế hàng cuối" }); s28++;
                    seats.Add(new SeatVisualItem { SeatCode = $"{s28:D2}", Row = 9, Column = 3, FinalPrice = basePrice, Description = "Ghế hàng cuối" }); s28++;
                    seats.Add(new SeatVisualItem { SeatCode = $"{s28:D2}", Row = 9, Column = 4, FinalPrice = basePrice, Description = "Ghế hàng cuối" });
                    break;

                case VehicleLayoutType.Seater45:
                    // Ghế ngồi 45 chỗ: 10 hàng đầu cột 1,2,4,5. Hàng cuối cột 1,2,3,4,5.
                    int s45 = 1;
                    for (int r = 1; r <= 10; r++)
                    {
                        seats.Add(new SeatVisualItem { SeatCode = $"{s45:D2}", Row = r, Column = 1, FinalPrice = basePrice, Description = "Ghế cửa sổ trái" }); s45++;
                        seats.Add(new SeatVisualItem { SeatCode = $"{s45:D2}", Row = r, Column = 2, FinalPrice = basePrice, Description = "Ghế lối đi trái" }); s45++;
                        seats.Add(new SeatVisualItem { SeatCode = $"{s45:D2}", Row = r, Column = 4, FinalPrice = basePrice, Description = "Ghế lối đi phải" }); s45++;
                        seats.Add(new SeatVisualItem { SeatCode = $"{s45:D2}", Row = r, Column = 5, FinalPrice = basePrice, Description = "Ghế cửa sổ phải" }); s45++;
                    }
                    // Hàng 11
                    for (int c = 1; c <= 5; c++)
                    {
                        seats.Add(new SeatVisualItem { SeatCode = $"{s45:D2}", Row = 11, Column = c, FinalPrice = basePrice, Description = "Ghế hàng cuối" });
                        s45++;
                    }
                    break;

                case VehicleLayoutType.Sleeper24:
                    // Giường nằm 24 chỗ: 6 hàng, 2 dãy (Cột 1, 4), 2 tầng
                    for (int floor = 1; floor <= 2; floor++)
                    {
                        int startNum = (floor == 1) ? 1 : 13;
                        decimal cabinPrice = (floor == 1) ? (basePrice * 1.3m) : (basePrice * 1.25m);

                        for (int r = 1; r <= 6; r++)
                        {
                            int pLeft = startNum + (r - 1) * 2;
                            int pRight = pLeft + 1;

                            seats.Add(new SeatVisualItem { SeatCode = $"P{pLeft:D2}", Floor = floor, Row = r, Column = 1, FinalPrice = cabinPrice, Description = $"Cabin VIP Tầng {floor} Trái" });
                            seats.Add(new SeatVisualItem { SeatCode = $"P{pRight:D2}", Floor = floor, Row = r, Column = 4, FinalPrice = cabinPrice, Description = $"Cabin VIP Tầng {floor} Phải" });
                        }
                    }
                    break;

                case VehicleLayoutType.Sleeper34:
                    // Giường nằm 34 chỗ (hàng giữa lùi xuống 1)
                    // Hàng 1 (Cột 1, 5). Hàng 2->6 (Cột 1,3,5). Tổng 17 chỗ mỗi tầng.
                    for (int floor = 1; floor <= 2; floor++)
                    {
                        string prefix = (floor == 1) ? "A" : "B";
                        decimal floorPrice = (floor == 1) ? basePrice : (basePrice * 0.95m);
                        int gCount = 1;

                        for (int r = 1; r <= 6; r++)
                        {
                            // Trái
                            seats.Add(new SeatVisualItem { SeatCode = $"{prefix}{gCount:D2}", Floor = floor, Row = r, Column = 1, FinalPrice = floorPrice, Description = $"Giường tầng {floor} trái" }); gCount++;
                            
                            // Giữa (Lùi 1 hàng, tức là không có ở hàng 1)
                            if (r > 1)
                            {
                                seats.Add(new SeatVisualItem { SeatCode = $"{prefix}{gCount:D2}", Floor = floor, Row = r, Column = 3, FinalPrice = floorPrice, Description = $"Giường tầng {floor} giữa" }); gCount++;
                            }
                            
                            // Phải
                            seats.Add(new SeatVisualItem { SeatCode = $"{prefix}{gCount:D2}", Floor = floor, Row = r, Column = 5, FinalPrice = floorPrice, Description = $"Giường tầng {floor} phải" }); gCount++;
                        }
                    }
                    break;
            }

            return seats;
        }

        /// <summary>
        /// Lấy sơ đồ ghế hoàn chỉnh cho chuyến xe, đồng thời đánh dấu các ghế đã bán từ CSDL
        /// </summary>
        public async Task<VehicleSeatMapDto> GetVehicleSeatMapAsync(int vehicleId, DateTime departureTime, decimal basePrice = 250000)
        {
            using var db = CreateContext();

            var vehicle = await db.Vehicles
                .AsNoTracking()
                .Include(v => v.Route)
                .FirstOrDefaultAsync(v => v.Id == vehicleId);

            string vehicleType = vehicle?.VehicleType ?? "Giường nằm 40 chỗ";
            string licensePlate = vehicle?.LicensePlate ?? "Chưa gán xe";
            int totalSeats = vehicle?.TotalSeats ?? 40;

            if (vehicle?.Route != null && vehicle.Route.BasePrice > 0)
            {
                basePrice = vehicle.Route.BasePrice;
            }

            var layoutType = DetermineLayoutType(vehicleType, totalSeats);
            var seatTemplate = GenerateSeatTemplate(layoutType, totalSeats, basePrice);

            // Tìm các vé đã đặt trên chuyến này (cùng VehicleId, cùng DepartureTime, trạng thái khác Cancelled)
            var bookedTickets = await db.Tickets
                .AsNoTracking()
                .Where(t => t.VehicleId == vehicleId &&
                            t.DepartureTime.Date == departureTime.Date &&
                            t.DepartureTime.Hour == departureTime.Hour &&
                            t.TicketStatus != "Cancelled")
                .Select(t => t.SeatNumber.Trim().ToUpper())
                .ToListAsync();

            var bookedSet = new HashSet<string>(bookedTickets, StringComparer.OrdinalIgnoreCase);

            // Đánh dấu trạng thái ghế
            foreach (var seat in seatTemplate)
            {
                if (bookedSet.Contains(seat.SeatCode.Trim().ToUpper()))
                {
                    seat.State = SeatState.Booked;
                }
                else
                {
                    seat.State = SeatState.Available;
                }
            }

            int floors = (layoutType == VehicleLayoutType.Sleeper24 || layoutType == VehicleLayoutType.Sleeper34) ? 2 : 1;
            int maxRow = seatTemplate.Any() ? seatTemplate.Max(s => s.Row) : 6;
            int maxCol = seatTemplate.Any() ? seatTemplate.Max(s => s.Column) : 5;

            return new VehicleSeatMapDto
            {
                VehicleId = vehicleId,
                LicensePlate = licensePlate,
                VehicleType = vehicleType,
                TotalSeats = totalSeats,
                LayoutType = layoutType,
                TotalFloors = floors,
                RowsPerFloor = maxRow,
                ColumnsPerFloor = maxCol,
                BasePrice = basePrice,
                Seats = seatTemplate
            };
        }
    }
}
