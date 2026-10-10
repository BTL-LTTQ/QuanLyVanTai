using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.Tests.Helpers
{
    /// <summary>
    /// Seed data cho unit tests liên quan đến Schedule và DriverAssignment.
    /// Sử dụng thời gian tương lai cố định để tests không bị phụ thuộc vào DateTime.Now.
    /// </summary>
    public static class ScheduleSeedData
    {
        // Thời điểm "hiện tại" giả định dùng xuyên suốt tests
        // (ScheduleService validate DepartureTime > UtcNow nên cần future time)
        public static readonly DateTime FakeNow = new(2026, 10, 10, 0, 0, 0, DateTimeKind.Utc);

        // Các chuyến mẫu
        public static readonly DateTime Trip1Departure = new(2026, 10, 11, 8, 0, 0, DateTimeKind.Utc);
        public static readonly DateTime Trip1Arrival   = new(2026, 10, 11, 10, 0, 0, DateTimeKind.Utc);

        public static readonly DateTime Trip2Departure = new(2026, 10, 11, 14, 0, 0, DateTimeKind.Utc);
        public static readonly DateTime Trip2Arrival   = new(2026, 10, 11, 16, 0, 0, DateTimeKind.Utc);

        // Chuyến trùng giờ với Trip1 (overlap)
        public static readonly DateTime ConflictDeparture = new(2026, 10, 11, 9, 0, 0, DateTimeKind.Utc);
        public static readonly DateTime ConflictArrival   = new(2026, 10, 11, 11, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Seed: 1 Route (Active), 2 Vehicles (Ready), 2 Drivers (Active GPLX còn hạn),
        /// 1 Schedule đã tồn tại (xe 1 + driver 1 chạy Trip1).
        /// </summary>
        public static async Task SeedAsync(AppDbContext db)
        {
            // Route
            db.Routes.Add(new Route
            {
                Id = 10, RouteCode = "HN-DN-01", RouteName = "Hà Nội - Đà Nẵng",
                DistanceKm = 764, EstimatedHours = 14, BasePrice = 350_000, Status = "Active"
            });

            // Route bị Suspended
            db.Routes.Add(new Route
            {
                Id = 11, RouteCode = "HN-HCM-01", RouteName = "Hà Nội - Hồ Chí Minh",
                DistanceKm = 1726, EstimatedHours = 30, BasePrice = 600_000, Status = "Suspended"
            });

            // Vehicles
            db.Vehicles.AddRange(
                new Vehicle
                {
                    Id = 10, LicensePlate = "29B-11111", VehicleType = "Giường nằm",
                    TotalSeats = 40, Status = "Ready"
                },
                new Vehicle
                {
                    Id = 11, LicensePlate = "29B-22222", VehicleType = "Ghế ngồi",
                    TotalSeats = 45, Status = "Ready"
                },
                new Vehicle
                {
                    Id = 12, LicensePlate = "51A-33333", VehicleType = "Limousine",
                    TotalSeats = 20, Status = "Maintenance" // xe đang bảo dưỡng
                }
            );

            // Drivers
            db.Drivers.AddRange(
                new Driver
                {
                    Id = 1, DriverCode = "TX-001", FullName = "Nguyễn Văn An",
                    PhoneNumber = "0901111111", LicenseNumber = "GPLX-001",
                    LicenseClass = "D", LicenseExpiryDate = new DateTime(2030, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Status = "Active"
                },
                new Driver
                {
                    Id = 2, DriverCode = "TX-002", FullName = "Trần Thị Bình",
                    PhoneNumber = "0902222222", LicenseNumber = "GPLX-002",
                    LicenseClass = "D", LicenseExpiryDate = new DateTime(2030, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    Status = "Active"
                },
                new Driver
                {
                    Id = 3, DriverCode = "TX-003", FullName = "Lê Văn Cường",
                    PhoneNumber = "0903333333", LicenseNumber = "GPLX-003",
                    LicenseClass = "D",
                    // GPLX hết hạn trước khi chuyến kết thúc
                    LicenseExpiryDate = new DateTime(2026, 10, 10, 12, 0, 0, DateTimeKind.Utc),
                    Status = "Active"
                },
                new Driver
                {
                    Id = 4, DriverCode = "TX-004", FullName = "Phạm Thị Dung",
                    PhoneNumber = "0904444444", LicenseNumber = "GPLX-004",
                    LicenseClass = "D", LicenseExpiryDate = new DateTime(2030, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Status = "OnLeave" // tài xế đang nghỉ phép
                }
            );

            await db.SaveChangesAsync();

            // Schedule đã có: xe 10 + driver 1 chạy Trip1 (08:00-10:00)
            var existingSchedule = new Schedule
            {
                Id = 100, ScheduleCode = "SCH-EXISTING-001",
                RouteId = 10, VehicleId = 10,
                DepartureTime = Trip1Departure, EstimatedArrivalTime = Trip1Arrival,
                Status = "Scheduled"
            };
            db.Schedules.Add(existingSchedule);
            await db.SaveChangesAsync();

            db.DriverAssignments.Add(new DriverAssignment
            {
                ScheduleId = 100, DriverId = 1, Role = "Primary",
                AssignmentStatus = "Assigned"
            });
            await db.SaveChangesAsync();
        }
    }
}
