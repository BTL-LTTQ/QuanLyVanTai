using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;
using Core.Helpers;
using Core.Security;

namespace QuanLyVanTai.BLL.Services
{
    public static class DatabaseInitializer
    {
        public static async Task SeedSampleDataAsync(AppDbContext context)
        {
            // Không gọi EnsureCreatedAsync tại đây: việc tạo Database/bảng chỉ do Migration
            // tại Program.cs đảm nhiệm, tránh trộn 2 cơ chế khởi tạo schema của EF Core.

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

            // 4. Kiểm tra và thêm tài khoản Khách hàng mẫu nếu chưa có
            if (!await context.Accounts.AnyAsync(a => a.Username == "khachhang"))
            {
                string salt = SecurityHelper.GenerateSalt();
                string passwordHash = SecurityHelper.HashPassword("khachhang123", salt);

                var customerAccount = new Account
                {
                    Username = "khachhang",
                    PasswordHash = passwordHash,
                    PasswordSalt = salt,
                    FullName = "Khách Hàng Mẫu",
                    Email = "khachhang@vantai.com",
                    PhoneNumber = "0912345678",
                    Role = SystemRoles.Customer,
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                };
                context.Accounts.Add(customerAccount);
                await context.SaveChangesAsync();
            }

            // 5. Khởi tạo dữ liệu vé giao dịch thực tế phục vụ phân tích & biểu đồ Dashboard
            if (await context.Tickets.CountAsync() < 10)
            {
                var routes = await context.Routes.ToListAsync();
                var vehicles = await context.Vehicles.ToListAsync();
                var r1 = routes.FirstOrDefault(r => r.RouteCode == "HN-HP-01") ?? routes.FirstOrDefault();
                var r2 = routes.FirstOrDefault(r => r.RouteCode == "HN-DN-01") ?? routes.Skip(1).FirstOrDefault() ?? r1;
                var r3 = routes.FirstOrDefault(r => r.RouteCode == "HN-SG-01") ?? routes.Skip(2).FirstOrDefault() ?? r2;

                var v1 = vehicles.FirstOrDefault() ?? new Vehicle { LicensePlate = "29B-123.45" };
                var v2 = vehicles.Skip(1).FirstOrDefault() ?? v1;
                var v3 = vehicles.Skip(2).FirstOrDefault() ?? v1;

                if (r1 != null && r2 != null && r3 != null)
                {
                    var now = DateTime.Today;
                    var sampleTickets = new List<Ticket>
                    {
                        // 6 ngày trước
                        new Ticket { TicketCode = "TK20261011001", CustomerName = "Nguyễn Văn An", CustomerPhone = "0912345678", SeatNumber = "A01", Price = r1.BasePrice, DepartureTime = now.AddDays(-6).AddHours(8), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = r1.Id, VehicleId = v1.Id, AccountId = 1 },
                        new Ticket { TicketCode = "TK20261011002", CustomerName = "Trần Thị Mai", CustomerPhone = "0987654321", SeatNumber = "A02", Price = r1.BasePrice, DepartureTime = now.AddDays(-6).AddHours(8), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = r1.Id, VehicleId = v1.Id, AccountId = 1 },
                        new Ticket { TicketCode = "TK20261011003", CustomerName = "Lê Hoàng Long", CustomerPhone = "0901234567", SeatNumber = "B01", Price = r2.BasePrice, DepartureTime = now.AddDays(-6).AddHours(19), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = r2.Id, VehicleId = v2.Id, AccountId = 1 },

                        // 5 ngày trước
                        new Ticket { TicketCode = "TK20261012001", CustomerName = "Phạm Thị Hương", CustomerPhone = "0934567890", SeatNumber = "A05", Price = r1.BasePrice, DepartureTime = now.AddDays(-5).AddHours(9), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = r1.Id, VehicleId = v1.Id, AccountId = 1 },
                        new Ticket { TicketCode = "TK20261012002", CustomerName = "Đặng Minh Quân", CustomerPhone = "0945678901", SeatNumber = "VIP01", Price = r3.BasePrice, DepartureTime = now.AddDays(-5).AddHours(14), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = r3.Id, VehicleId = v3.Id, AccountId = 1 },

                        // 4 ngày trước
                        new Ticket { TicketCode = "TK20261013001", CustomerName = "Vũ Hải Đăng", CustomerPhone = "0967890123", SeatNumber = "B03", Price = r2.BasePrice, DepartureTime = now.AddDays(-4).AddHours(7), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = r2.Id, VehicleId = v2.Id, AccountId = 1 },
                        new Ticket { TicketCode = "TK20261013002", CustomerName = "Bùi Thu Thảo", CustomerPhone = "0978901234", SeatNumber = "B04", Price = r2.BasePrice, DepartureTime = now.AddDays(-4).AddHours(7), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = r2.Id, VehicleId = v2.Id, AccountId = 1 },
                        new Ticket { TicketCode = "TK20261013003", CustomerName = "Ngô Đức Trọng", CustomerPhone = "0989012345", SeatNumber = "A10", Price = r1.BasePrice, DepartureTime = now.AddDays(-4).AddHours(16), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = r1.Id, VehicleId = v1.Id, AccountId = 1 },

                        // 3 ngày trước
                        new Ticket { TicketCode = "TK20261014001", CustomerName = "Hoàng Kim Ngân", CustomerPhone = "0911223344", SeatNumber = "VIP02", Price = r3.BasePrice, DepartureTime = now.AddDays(-3).AddHours(10), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = r3.Id, VehicleId = v3.Id, AccountId = 1 },
                        new Ticket { TicketCode = "TK20261014002", CustomerName = "Đỗ Quốc Bảo", CustomerPhone = "0922334455", SeatNumber = "VIP03", Price = r3.BasePrice, DepartureTime = now.AddDays(-3).AddHours(10), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = r3.Id, VehicleId = v3.Id, AccountId = 1 },
                        new Ticket { TicketCode = "TK20261014003", CustomerName = "Trịnh Phương Ly", CustomerPhone = "0933445566", SeatNumber = "A06", Price = r1.BasePrice, DepartureTime = now.AddDays(-3).AddHours(13), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = r1.Id, VehicleId = v1.Id, AccountId = 1 },

                        // 2 ngày trước
                        new Ticket { TicketCode = "TK20261015001", CustomerName = "Mai Tiến Dũng", CustomerPhone = "0944556677", SeatNumber = "B07", Price = r2.BasePrice, DepartureTime = now.AddDays(-2).AddHours(8), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = r2.Id, VehicleId = v2.Id, AccountId = 1 },
                        new Ticket { TicketCode = "TK20261015002", CustomerName = "Dương Ngọc Ánh", CustomerPhone = "0955667788", SeatNumber = "B08", Price = r2.BasePrice, DepartureTime = now.AddDays(-2).AddHours(8), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = r2.Id, VehicleId = v2.Id, AccountId = 1 },
                        new Ticket { TicketCode = "TK20261015003", CustomerName = "Lý Tuấn Kiệt", CustomerPhone = "0966778899", SeatNumber = "A08", Price = r1.BasePrice, DepartureTime = now.AddDays(-2).AddHours(15), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = r1.Id, VehicleId = v1.Id, AccountId = 1 },

                        // Hôm qua
                        new Ticket { TicketCode = "TK20261016001", CustomerName = "Phan Văn Hậu", CustomerPhone = "0977889900", SeatNumber = "VIP05", Price = r3.BasePrice, DepartureTime = now.AddDays(-1).AddHours(9), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = r3.Id, VehicleId = v3.Id, AccountId = 1 },
                        new Ticket { TicketCode = "TK20261016002", CustomerName = "Tạ Bích Loan", CustomerPhone = "0988990011", SeatNumber = "A12", Price = r1.BasePrice, DepartureTime = now.AddDays(-1).AddHours(11), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = r1.Id, VehicleId = v1.Id, AccountId = 1 },
                        new Ticket { TicketCode = "TK20261016003", CustomerName = "Vương Đình Huệ", CustomerPhone = "0999001122", SeatNumber = "B10", Price = r2.BasePrice, DepartureTime = now.AddDays(-1).AddHours(17), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = r2.Id, VehicleId = v2.Id, AccountId = 1 },

                        // Hôm nay
                        new Ticket { TicketCode = "TK20261017001", CustomerName = "Cao Bá Quát", CustomerPhone = "0911335577", SeatNumber = "A01", Price = r1.BasePrice, DepartureTime = now.AddHours(7), PaymentStatus = "Paid", TicketStatus = "Booked", RouteId = r1.Id, VehicleId = v1.Id, AccountId = 1 },
                        new Ticket { TicketCode = "TK20261017002", CustomerName = "Hà Thu Trang", CustomerPhone = "0922446688", SeatNumber = "VIP06", Price = r3.BasePrice, DepartureTime = now.AddHours(10), PaymentStatus = "Paid", TicketStatus = "Booked", RouteId = r3.Id, VehicleId = v3.Id, AccountId = 1 },
                        new Ticket { TicketCode = "TK20261017003", CustomerName = "Trương Mỹ Linh", CustomerPhone = "0933557799", SeatNumber = "B02", Price = r2.BasePrice, DepartureTime = now.AddHours(15), PaymentStatus = "Paid", TicketStatus = "Booked", RouteId = r2.Id, VehicleId = v2.Id, AccountId = 1 },
                    };

                    context.Tickets.AddRange(sampleTickets);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
