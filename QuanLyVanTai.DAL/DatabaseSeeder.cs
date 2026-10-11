using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.DAL
{
    public static class DatabaseSeeder
    {
        public static void SeedSampleData(AppDbContext db)
        {
            try
            {
                // 1. Seed Trạm dừng nếu chưa có
                if (!db.Stations.Any())
                {
                    var stations = new List<Station>
                    {
                        new Station { StationCode = "BX-MYDINH", StationName = "Bến xe Mỹ Đình", Address = "Số 20 Phạm Hùng, Nam Từ Liêm", City = "Hà Nội", Status = "Active" },
                        new Station { StationCode = "BX-GIAPBAT", StationName = "Bến xe Giáp Bát", Address = "Km6 Giải Phóng, Hoàng Mai", City = "Hà Nội", Status = "Active" },
                        new Station { StationCode = "BX-DANANG", StationName = "Bến xe Trung tâm Đà Nẵng", Address = "Tôn Đức Thắng, Liên Chiểu", City = "Đà Nẵng", Status = "Active" },
                        new Station { StationCode = "BX-MIENDONG", StationName = "Bến xe Miền Đông mới", Address = "501 Hoàng Hữu Nam, TP. Thủ Đức", City = "TP. Hồ Chí Minh", Status = "Active" },
                        new Station { StationCode = "BX-DALAT", StationName = "Bến xe Liên tỉnh Đà Lạt", Address = "Số 1 Tô Hiến Thành, P.3", City = "Lâm Đồng", Status = "Active" },
                        new Station { StationCode = "BX-NIEMNGHIA", StationName = "Bến xe Niệm Nghĩa", Address = "Trần Nguyên Hãn, Lê Chân", City = "Hải Phòng", Status = "Active" }
                    };
                    db.Stations.AddRange(stations);
                    db.SaveChanges();
                }

                // 2. Seed Xe khách nếu chưa có
                if (!db.Vehicles.Any())
                {
                    var vehicles = new List<Vehicle>
                    {
                        new Vehicle { LicensePlate = "29B-123.45", VehicleType = "Giường nằm 36 chỗ", TotalSeats = 36, Status = "Active" },
                        new Vehicle { LicensePlate = "29B-678.90", VehicleType = "Limousine VIP 22 phòng", TotalSeats = 22, Status = "Active" },
                        new Vehicle { LicensePlate = "43B-555.88", VehicleType = "Giường nằm 40 chỗ", TotalSeats = 40, Status = "Active" },
                        new Vehicle { LicensePlate = "51B-999.66", VehicleType = "Limousine VIP 24 phòng", TotalSeats = 24, Status = "Active" }
                    };
                    db.Vehicles.AddRange(vehicles);
                    db.SaveChanges();
                }

                // 3. Seed Tuyến xe nếu chưa có
                if (!db.Routes.Any())
                {
                    var routes = new List<Route>
                    {
                        new Route
                        {
                            RouteCode = "HN-DN-01",
                            RouteName = "Hà Nội - Đà Nẵng (Cao tốc Bắc Nam)",
                            DistanceKm = 760m,
                            EstimatedHours = 14.5m,
                            BasePrice = 380000m,
                            Status = "Active"
                        },
                        new Route
                        {
                            RouteCode = "HN-SG-01",
                            RouteName = "Hà Nội - TP. Hồ Chí Minh (Xuyên Việt)",
                            DistanceKm = 1720m,
                            EstimatedHours = 32.0m,
                            BasePrice = 750000m,
                            Status = "Active"
                        },
                        new Route
                        {
                            RouteCode = "SG-DL-01",
                            RouteName = "TP. Hồ Chí Minh - Đà Lạt",
                            DistanceKm = 305m,
                            EstimatedHours = 6.5m,
                            BasePrice = 280000m,
                            Status = "Active"
                        },
                        new Route
                        {
                            RouteCode = "HN-HP-01",
                            RouteName = "Hà Nội - Hải Phòng (Cao tốc 5B)",
                            DistanceKm = 120m,
                            EstimatedHours = 1.8m,
                            BasePrice = 120000m,
                            Status = "Active"
                        },
                        new Route
                        {
                            RouteCode = "SG-VT-01",
                            RouteName = "TP. Hồ Chí Minh - Vũng Tàu",
                            DistanceKm = 95m,
                            EstimatedHours = 2.0m,
                            BasePrice = 150000m,
                            Status = "Active"
                        }
                    };
                    db.Routes.AddRange(routes);
                    db.SaveChanges();
                }
            }
            catch
            {
                // Bỏ qua lỗi seed nếu đã tồn tại dữ liệu
            }
        }
    }
}
