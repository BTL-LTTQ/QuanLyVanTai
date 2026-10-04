using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.BLL.Services
{
    public static class DatabaseInitializer
    {
        public static async Task SeedSampleDataAsync(AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            // 1. Kiểm tra và thêm Trạm dừng nếu chưa có
            if (!await context.Stations.AnyAsync())
            {
                var stations = new List<Station>
                {
                    new Station { StationCode = "BX-MYDINH", StationName = "Bến xe Mỹ Đình", Address = "20 Phạm Hùng, Mỹ Đình", City = "Hà Nội", Status = "Active" },
                    new Station { StationCode = "BX-GIAPBAT", StationName = "Bến xe Giáp Bát", Address = "Km 6 Giải Phóng, Giáp Bát", City = "Hà Nội", Status = "Active" },
                    new Station { StationCode = "BX-NIEMNGHIA", StationName = "Bến xe Niệm Nghĩa", Address = "275 Trần Nguyên Hãn", City = "Hải Phòng", Status = "Active" },
                    new Station { StationCode = "BX-TRUNGTAM-DN", StationName = "Bến xe Trung tâm Đà Nẵng", Address = "Tôn Đức Thắng, Cẩm Lệ", City = "Đà Nẵng", Status = "Active" },
                    new Station { StationCode = "BX-MIENDONG", StationName = "Bến xe Miền Đông Mới", Address = "Hoàng Hữu Nam, TP Thủ Đức", City = "TP. Hồ Chí Minh", Status = "Active" },
                    new Station { StationCode = "BX-MIENTAY", StationName = "Bến xe Miền Tây", Address = "395 Kinh Dương Vương, An Lạc", City = "TP. Hồ Chí Minh", Status = "Active" },
                    new Station { StationCode = "BX-VINH", StationName = "Bến xe Bắc Vinh", Address = "Xã Nghi Kim", City = "Nghệ An", Status = "Active" }
                };

                context.Stations.AddRange(stations);
                await context.SaveChangesAsync();
            }

            // 2. Kiểm tra và thêm Tuyến xe nếu chưa có
            if (!await context.Routes.AnyAsync())
            {
                var allStations = await context.Stations.ToListAsync();
                var stMyDinh = allStations.First(s => s.StationCode == "BX-MYDINH");
                var stNiemNghia = allStations.First(s => s.StationCode == "BX-NIEMNGHIA");
                var stDaNang = allStations.First(s => s.StationCode == "BX-TRUNGTAM-DN");
                var stMienDong = allStations.First(s => s.StationCode == "BX-MIENDONG");
                var stVinh = allStations.First(s => s.StationCode == "BX-VINH");

                var routes = new List<Route>
                {
                    new Route
                    {
                        RouteCode = "HN-HP-01",
                        RouteName = "Hà Nội - Hải Phòng (Cao tốc 5B)",
                        DistanceKm = 120.5m,
                        EstimatedHours = 1.75m,
                        BasePrice = 150000m,
                        Status = "Active",
                        Stations = new List<Station> { stMyDinh, stNiemNghia }
                    },
                    new Route
                    {
                        RouteCode = "HN-DN-01",
                        RouteName = "Hà Nội - Đà Nẵng (Bắc Nam)",
                        DistanceKm = 760.0m,
                        EstimatedHours = 14.5m,
                        BasePrice = 450000m,
                        Status = "Active",
                        Stations = new List<Station> { stMyDinh, stVinh, stDaNang }
                    },
                    new Route
                    {
                        RouteCode = "HN-SG-01",
                        RouteName = "Hà Nội - TP. Hồ Chí Minh (Xuyên Việt)",
                        DistanceKm = 1720.0m,
                        EstimatedHours = 32.0m,
                        BasePrice = 850000m,
                        Status = "Active",
                        Stations = new List<Station> { stMyDinh, stVinh, stDaNang, stMienDong }
                    }
                };

                context.Routes.AddRange(routes);
                await context.SaveChangesAsync();
            }

            // 3. Kiểm tra và thêm Phương tiện nếu chưa có
            if (!await context.Vehicles.AnyAsync())
            {
                var route1 = await context.Routes.FirstOrDefaultAsync(r => r.RouteCode == "HN-HP-01");
                var route2 = await context.Routes.FirstOrDefaultAsync(r => r.RouteCode == "HN-DN-01");

                var vehicles = new List<Vehicle>
                {
                    new Vehicle { LicensePlate = "29B-123.45", VehicleType = "Ghế ngồi cao cấp", TotalSeats = 29, Manufacturer = "Hyundai Universe", Status = "Ready", RouteId = route1?.Id },
                    new Vehicle { LicensePlate = "29B-888.88", VehicleType = "Limousine VIP", TotalSeats = 16, Manufacturer = "Ford Transit DCar", Status = "InTransit", RouteId = route1?.Id },
                    new Vehicle { LicensePlate = "43B-567.89", VehicleType = "Giường nằm 40 chỗ", TotalSeats = 40, Manufacturer = "Thaco Mobihome", Status = "Ready", RouteId = route2?.Id },
                    new Vehicle { LicensePlate = "51B-999.99", VehicleType = "Giường nằm VIP 32 phòng", TotalSeats = 32, Manufacturer = "Tracomeco", Status = "Ready", RouteId = route2?.Id },
                    new Vehicle { LicensePlate = "30E-333.33", VehicleType = "Ghế ngồi", TotalSeats = 34, Manufacturer = "Samco", Status = "Maintenance", RouteId = null }
                };

                context.Vehicles.AddRange(vehicles);
                await context.SaveChangesAsync();
            }

            // 4. Khởi tạo một số vé mẫu
            if (!await context.Tickets.AnyAsync())
            {
                var r1 = await context.Routes.FirstOrDefaultAsync();
                var v1 = await context.Vehicles.FirstOrDefaultAsync();
                if (r1 != null && v1 != null)
                {
                    context.Tickets.AddRange(
                        new Ticket
                        {
                            TicketCode = "TK20261001",
                            CustomerName = "Nguyễn Văn An",
                            CustomerPhone = "0912345678",
                            SeatNumber = "A01",
                            Price = r1.BasePrice,
                            DepartureTime = DateTime.UtcNow.AddHours(2),
                            PaymentStatus = "Paid",
                            TicketStatus = "Booked",
                            RouteId = r1.Id,
                            VehicleId = v1.Id,
                            AccountId = 1
                        },
                        new Ticket
                        {
                            TicketCode = "TK20261002",
                            CustomerName = "Trần Thị Mai",
                            CustomerPhone = "0987654321",
                            SeatNumber = "A02",
                            Price = r1.BasePrice,
                            DepartureTime = DateTime.UtcNow.AddHours(2),
                            PaymentStatus = "Paid",
                            TicketStatus = "Booked",
                            RouteId = r1.Id,
                            VehicleId = v1.Id,
                            AccountId = 1
                        }
                    );
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
