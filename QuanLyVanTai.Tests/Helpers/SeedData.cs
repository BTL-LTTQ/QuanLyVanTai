using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.Tests.Helpers
{
    /// <summary>
    /// Seed data mẫu cho unit tests — dùng chung để tránh lặp code trong từng test class.
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Seed 2 trạm, 1 tuyến (liên kết với 2 trạm), 1 phương tiện gán tuyến, 1 phương tiện tự do.
        /// </summary>
        public static async Task SeedBasicAsync(AppDbContext db)
        {
            var stationA = new Station
            {
                Id = 1, StationCode = "BX-HN", StationName = "Bến xe Mỹ Đình",
                Address = "Phạm Hùng, Nam Từ Liêm", City = "Hà Nội", Status = "Active"
            };
            var stationB = new Station
            {
                Id = 2, StationCode = "BX-DN", StationName = "Bến xe Đà Nẵng",
                Address = "Điện Biên Phủ, Thanh Khê", City = "Đà Nẵng", Status = "Active"
            };

            var route = new Route
            {
                Id = 1, RouteCode = "HN-DN-01", RouteName = "Hà Nội - Đà Nẵng",
                DistanceKm = 764, EstimatedHours = 14, BasePrice = 350000, Status = "Active"
            };
            route.Stations.Add(stationA);
            route.Stations.Add(stationB);

            var vehicleAssigned = new Vehicle
            {
                Id = 1, LicensePlate = "29B-12345", VehicleType = "Giường nằm",
                TotalSeats = 40, Manufacturer = "Thaco", Status = "Ready", RouteId = 1
            };
            var vehicleFree = new Vehicle
            {
                Id = 2, LicensePlate = "51A-99999", VehicleType = "Ghế ngồi",
                TotalSeats = 45, Manufacturer = "Hyundai", Status = "Ready", RouteId = null
            };

            db.Stations.AddRange(stationA, stationB);
            db.Routes.Add(route);
            db.Vehicles.AddRange(vehicleAssigned, vehicleFree);
            await db.SaveChangesAsync();
        }

        /// <summary>Seed thêm 1 xe đang chạy (InTransit) để test delete rule.</summary>
        public static async Task SeedInTransitVehicleAsync(AppDbContext db)
        {
            db.Vehicles.Add(new Vehicle
            {
                Id = 3, LicensePlate = "30A-77777", VehicleType = "Limousine",
                TotalSeats = 20, Status = "InTransit", RouteId = 1
            });
            await db.SaveChangesAsync();
        }
    }
}
