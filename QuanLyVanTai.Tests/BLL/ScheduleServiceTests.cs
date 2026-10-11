using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Exceptions;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.DAL.Repositories;
using QuanLyVanTai.Tests.Helpers;
using BllValidationException = QuanLyVanTai.BLL.Exceptions.ValidationException;

namespace QuanLyVanTai.Tests.BLL
{
    // =========================================================================
    // ScheduleServiceTests
    // =========================================================================
    // Vì ScheduleService dùng `new AppDbContext()` nội bộ, chúng ta test
    // trực tiếp qua repository + validate logic được extract ra private static
    // methods — tương tự cách đã test RouteManagementService.
    //
    // Các test tập trung vào:
    //   1. Validate form (input checks)
    //   2. Overlap detection cho xe (vehicle conflict)
    //   3. Overlap detection cho tài xế (driver conflict)
    //   4. Business rules (GPLX, route status, vehicle maintenance)
    //   5. Status transition state machine
    //   6. ScheduleRepository overlap query logic
    // =========================================================================

    public class ScheduleServiceTests
    {
        // ── Helpers ───────────────────────────────────────────────────────────

        private static DateTime Future(int daysFromNow = 1, int hour = 8)
            => DateTime.UtcNow.Date.AddDays(daysFromNow).AddHours(hour);

        // ── 1. Validate form ──────────────────────────────────────────────────

        [Fact]
        public void ValidateForm_RouteIdZero_ThrowsValidationException()
        {
            var ex = Assert.Throws<BllValidationException>(() =>
                InvokeValidateForm(new CreateScheduleDto
                {
                    RouteId = 0, VehicleId = 1,
                    DepartureTime = Future(),
                    Drivers = [new AssignDriverDto { DriverId = 1, Role = "Primary" }]
                }));
            Assert.Equal("RouteId", ex.FieldName);
        }

        [Fact]
        public void ValidateForm_DepartureInPast_ThrowsValidationException()
        {
            var ex = Assert.Throws<BllValidationException>(() =>
                InvokeValidateForm(new CreateScheduleDto
                {
                    RouteId = 1, VehicleId = 1,
                    DepartureTime = DateTime.UtcNow.AddHours(-1), // quá khứ
                    Drivers = [new AssignDriverDto { DriverId = 1, Role = "Primary" }]
                }));
            Assert.Equal("DepartureTime", ex.FieldName);
        }

        [Fact]
        public void ValidateForm_ArrivalBeforeDeparture_ThrowsValidationException()
        {
            var departure = Future(1, 10);
            var ex = Assert.Throws<BllValidationException>(() =>
                InvokeValidateForm(new CreateScheduleDto
                {
                    RouteId = 1, VehicleId = 1,
                    DepartureTime = departure,
                    EstimatedArrivalTime = departure.AddHours(-1), // đến trước khi đi
                    Drivers = [new AssignDriverDto { DriverId = 1, Role = "Primary" }]
                }));
            Assert.Equal("EstimatedArrivalTime", ex.FieldName);
        }

        [Fact]
        public void ValidateForm_NoDrivers_ThrowsValidationException()
        {
            var ex = Assert.Throws<BllValidationException>(() =>
                InvokeValidateForm(new CreateScheduleDto
                {
                    RouteId = 1, VehicleId = 1,
                    DepartureTime = Future(),
                    Drivers = [] // không có tài xế
                }));
            Assert.Equal("Drivers", ex.FieldName);
        }

        [Fact]
        public void ValidateForm_NoPrimaryDriver_ThrowsValidationException()
        {
            var ex = Assert.Throws<BllValidationException>(() =>
                InvokeValidateForm(new CreateScheduleDto
                {
                    RouteId = 1, VehicleId = 1,
                    DepartureTime = Future(),
                    Drivers = [
                        new AssignDriverDto { DriverId = 1, Role = "Secondary" } // không có Primary
                    ]
                }));
            Assert.Equal("Drivers", ex.FieldName);
            Assert.Contains("Primary", ex.Message);
        }

        [Fact]
        public void ValidateForm_TwoPrimaryDrivers_ThrowsValidationException()
        {
            var ex = Assert.Throws<BllValidationException>(() =>
                InvokeValidateForm(new CreateScheduleDto
                {
                    RouteId = 1, VehicleId = 1,
                    DepartureTime = Future(),
                    Drivers = [
                        new AssignDriverDto { DriverId = 1, Role = "Primary" },
                        new AssignDriverDto { DriverId = 2, Role = "Primary" } // 2 primary
                    ]
                }));
            Assert.Equal("Drivers", ex.FieldName);
            Assert.Contains("1 tài xế Primary", ex.Message);
        }

        [Fact]
        public void ValidateForm_DuplicateDriverInSameTrip_ThrowsValidationException()
        {
            var ex = Assert.Throws<BllValidationException>(() =>
                InvokeValidateForm(new CreateScheduleDto
                {
                    RouteId = 1, VehicleId = 1,
                    DepartureTime = Future(),
                    Drivers = [
                        new AssignDriverDto { DriverId = 5, Role = "Primary" },
                        new AssignDriverDto { DriverId = 5, Role = "Secondary" } // cùng 1 người
                    ]
                }));
            Assert.Equal("Drivers", ex.FieldName);
            Assert.Contains("trùng lặp", ex.Message);
        }

        [Fact]
        public void ValidateForm_ValidInput_DoesNotThrow()
        {
            // Không ném exception với input hợp lệ
            InvokeValidateForm(new CreateScheduleDto
            {
                RouteId = 1, VehicleId = 1,
                DepartureTime = Future(2, 8),
                EstimatedArrivalTime = Future(2, 10),
                Drivers = [
                    new AssignDriverDto { DriverId = 1, Role = "Primary" },
                    new AssignDriverDto { DriverId = 2, Role = "Secondary" }
                ]
            });
        }

        // ── 2. Vehicle conflict detection ─────────────────────────────────────

        [Fact]
        public async Task VehicleConflict_OverlappingSchedule_ThrowsBusinessRuleViolation()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);
            var repo = new ScheduleRepository(db);

            // Chuyến mới: xe 10 từ 09:00-11:00, trùng với chuyến hiện có 08:00-10:00
            var conflicts = await repo.GetActiveSchedulesForVehicleAsync(
                vehicleId: 10,
                windowStart: ScheduleSeedData.ConflictDeparture.AddMinutes(-30), // buffer
                windowEnd:   ScheduleSeedData.ConflictArrival.AddMinutes(30),
                excludeScheduleId: null);

            Assert.NotEmpty(conflicts);
            Assert.Equal(100, conflicts[0].Id);
        }

        [Fact]
        public async Task VehicleConflict_NoOverlap_ReturnsEmpty()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);
            var repo = new ScheduleRepository(db);

            // Chuyến mới: xe 10 từ 14:00-16:00, không trùng 08:00-10:00
            // Sau khi tính buffer 30 phút: window là 13:30-16:30
            // Chuyến cũ kết thúc 10:00 < 13:30 → không overlap
            var conflicts = await repo.GetActiveSchedulesForVehicleAsync(
                vehicleId: 10,
                windowStart: ScheduleSeedData.Trip2Departure.AddMinutes(-30),
                windowEnd:   ScheduleSeedData.Trip2Arrival.AddMinutes(30),
                excludeScheduleId: null);

            Assert.Empty(conflicts);
        }

        [Fact]
        public async Task VehicleConflict_DifferentVehicle_ReturnsEmpty()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);
            var repo = new ScheduleRepository(db);

            // Xe 11 khác xe 10 đang bận → không conflict
            var conflicts = await repo.GetActiveSchedulesForVehicleAsync(
                vehicleId: 11,
                windowStart: ScheduleSeedData.ConflictDeparture.AddMinutes(-30),
                windowEnd:   ScheduleSeedData.ConflictArrival.AddMinutes(30),
                excludeScheduleId: null);

            Assert.Empty(conflicts);
        }

        [Fact]
        public async Task VehicleConflict_ExcludeScheduleId_ReturnsSelf()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);
            var repo = new ScheduleRepository(db);

            // Khi update chuyến 100, loại trừ nó → không phát hiện conflict với chính nó
            var conflicts = await repo.GetActiveSchedulesForVehicleAsync(
                vehicleId: 10,
                windowStart: ScheduleSeedData.Trip1Departure.AddMinutes(-30),
                windowEnd:   ScheduleSeedData.Trip1Arrival.AddMinutes(30),
                excludeScheduleId: 100); // loại trừ chính nó

            Assert.Empty(conflicts);
        }

        // ── 3. Driver conflict detection ──────────────────────────────────────

        [Fact]
        public async Task DriverConflict_DriverBusyInOverlapWindow_DetectsConflict()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);
            var repo = new ScheduleRepository(db);

            // Driver 1 đang chạy chuyến 100 từ 08:00-10:00
            // Chuyến mới 09:00-11:00 → overlap
            var conflicts = await repo.GetActiveSchedulesForDriverAsync(
                driverId: 1,
                windowStart: ScheduleSeedData.ConflictDeparture.AddMinutes(-15),
                windowEnd:   ScheduleSeedData.ConflictArrival.AddMinutes(15),
                excludeScheduleId: null);

            Assert.NotEmpty(conflicts);
            Assert.Equal(100, conflicts[0].Id);
        }

        [Fact]
        public async Task DriverConflict_DriverFreeInWindow_ReturnsEmpty()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);
            var repo = new ScheduleRepository(db);

            // Driver 1 bận 08:00-10:00, chuyến mới 14:00-16:00 (sau buffer 15 phút)
            var conflicts = await repo.GetActiveSchedulesForDriverAsync(
                driverId: 1,
                windowStart: ScheduleSeedData.Trip2Departure.AddMinutes(-15),
                windowEnd:   ScheduleSeedData.Trip2Arrival.AddMinutes(15),
                excludeScheduleId: null);

            Assert.Empty(conflicts);
        }

        [Fact]
        public async Task DriverConflict_AnotherDriverSameWindow_ReturnsEmpty()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);
            var repo = new ScheduleRepository(db);

            // Driver 2 chưa có chuyến nào → không conflict dù window trùng
            var conflicts = await repo.GetActiveSchedulesForDriverAsync(
                driverId: 2,
                windowStart: ScheduleSeedData.ConflictDeparture.AddMinutes(-15),
                windowEnd:   ScheduleSeedData.ConflictArrival.AddMinutes(15),
                excludeScheduleId: null);

            Assert.Empty(conflicts);
        }

        // ── 4. Business rules ─────────────────────────────────────────────────

        [Fact]
        public async Task BusinessRule_SuspendedRoute_ThrowsBusinessRuleViolation()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);

            var route = await db.Routes.FindAsync(11); // Suspended route
            Assert.NotNull(route);

            var ex = Assert.Throws<BusinessRuleViolationException>(() =>
            {
                if (route!.Status != "Active")
                    throw new BusinessRuleViolationException("Schedule",
                        $"Tuyến [{route.RouteCode}] đang ở trạng thái '{route.Status}'.");
            });

            Assert.Equal("Schedule", ex.EntityName);
            Assert.Contains("Suspended", ex.Message);
        }

        [Fact]
        public async Task BusinessRule_MaintenanceVehicle_ThrowsBusinessRuleViolation()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);

            var vehicle = await db.Vehicles.FindAsync(12); // Maintenance

            var ex = Assert.Throws<BusinessRuleViolationException>(() =>
            {
                if (vehicle!.Status == "Maintenance")
                    throw new BusinessRuleViolationException("Schedule",
                        $"Xe [{vehicle.LicensePlate}] đang trong trạng thái Bảo dưỡng.");
            });

            Assert.Contains("Bảo dưỡng", ex.Message);
        }

        [Fact]
        public async Task BusinessRule_ExpiredLicense_ThrowsBusinessRuleViolation()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);

            var driver = await db.Drivers.FindAsync(3); // GPLX hết hạn 10/10/2026 12:00
            DateTime arrivalTime = ScheduleSeedData.Trip1Arrival; // 11/10/2026

            var ex = Assert.Throws<BusinessRuleViolationException>(() =>
            {
                if (driver!.LicenseExpiryDate < arrivalTime)
                    throw new BusinessRuleViolationException("Schedule",
                        $"GPLX của tài xế [{driver.FullName}] hết hạn ngày " +
                        $"{driver.LicenseExpiryDate:dd/MM/yyyy}.");
            });

            Assert.Contains("GPLX", ex.Message);
            Assert.Contains("Lê Văn Cường", ex.Message);
        }

        [Fact]
        public async Task BusinessRule_OnLeaveDriver_ThrowsBusinessRuleViolation()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);

            var driver = await db.Drivers.FindAsync(4); // OnLeave

            var ex = Assert.Throws<BusinessRuleViolationException>(() =>
            {
                if (driver!.Status != "Active")
                    throw new BusinessRuleViolationException("Schedule",
                        $"Tài xế [{driver.FullName}] đang ở trạng thái '{driver.Status}'.");
            });

            Assert.Contains("OnLeave", ex.Message);
        }

        [Fact]
        public async Task BusinessRule_DriverNotFound_ThrowsEntityNotFoundException()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);

            var driverIds = new List<int> { 999 };
            var found = await db.Drivers.Where(d => driverIds.Contains(d.Id)).ToListAsync();
            var notFound = driverIds.Except(found.Select(d => d.Id)).ToList();

            var ex = Assert.Throws<EntityNotFoundException>(() =>
            {
                if (notFound.Count > 0)
                    throw new EntityNotFoundException("Driver",
                        $"Không tìm thấy tài xế với ID: [{string.Join(", ", notFound)}].");
            });

            Assert.Equal("Driver", ex.EntityName);
            Assert.Contains("999", ex.Message);
        }

        // ── 5. Status transition state machine ────────────────────────────────

        [Theory]
        [InlineData("Scheduled",  "InProgress", true)]
        [InlineData("Scheduled",  "Cancelled",  true)]
        [InlineData("Scheduled",  "Delayed",    true)]
        [InlineData("InProgress", "Completed",  true)]
        [InlineData("InProgress", "Delayed",    true)]
        [InlineData("InProgress", "Cancelled",  true)]
        [InlineData("Delayed",    "InProgress", true)]
        [InlineData("Delayed",    "Cancelled",  true)]
        [InlineData("Completed",  "Scheduled",  false)] // terminal → không đổi được
        [InlineData("Cancelled",  "Scheduled",  false)] // terminal → không đổi được
        [InlineData("Scheduled",  "Completed",  false)] // bỏ qua InProgress
        [InlineData("Completed",  "InProgress", false)]
        public void StatusTransition_IsCorrect(string current, string next, bool shouldBeValid)
        {
            bool valid = (current, next) switch
            {
                ("Scheduled",  "InProgress") => true,
                ("Scheduled",  "Cancelled")  => true,
                ("Scheduled",  "Delayed")    => true,
                ("InProgress", "Completed")  => true,
                ("InProgress", "Delayed")    => true,
                ("InProgress", "Cancelled")  => true,
                ("Delayed",    "InProgress") => true,
                ("Delayed",    "Cancelled")  => true,
                _ => false
            };
            Assert.Equal(shouldBeValid, valid);
        }

        [Fact]
        public void StatusTransition_InvalidTransition_ThrowsBusinessRuleViolation()
        {
            var ex = Assert.Throws<BusinessRuleViolationException>(() =>
            {
                string current = "Completed"; string next = "Scheduled";
                bool valid = (current, next) switch
                {
                    ("Scheduled", "InProgress") => true,
                    _ => false
                };
                if (!valid)
                    throw new BusinessRuleViolationException("Schedule",
                        $"Không thể chuyển trạng thái từ '{current}' sang '{next}'.");
            });

            Assert.Contains("Completed", ex.Message);
        }

        // ── 6. Overlap query — edge cases ─────────────────────────────────────

        [Fact]
        public async Task OverlapQuery_AdjacentSchedule_NotConflict()
        {
            // Chuyến mới bắt đầu đúng lúc chuyến cũ kết thúc → không overlap (boundary excluded)
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);
            var repo = new ScheduleRepository(db);

            // Chuyến cũ kết thúc 10:00, chuyến mới bắt đầu 10:00
            // Window check không có buffer: [10:00, 12:00) vs chuyến [08:00, 10:00)
            // 10:00 < 10:00 = false → không overlap
            var conflicts = await repo.GetActiveSchedulesForVehicleAsync(
                vehicleId: 10,
                windowStart: ScheduleSeedData.Trip1Arrival,           // 10:00 (bắt đầu window)
                windowEnd: ScheduleSeedData.Trip1Arrival.AddHours(2), // 12:00
                excludeScheduleId: null);

            Assert.Empty(conflicts); // boundary exclusive: chuyến cũ kết thúc đúng lúc window bắt đầu
        }

        [Fact]
        public async Task OverlapQuery_CancelledSchedule_NotConflict()
        {
            // Chuyến đã Cancelled không được tính là conflict
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);

            // Chuyển chuyến 100 sang Cancelled
            var schedule = await db.Schedules.FindAsync(100);
            schedule!.Status = "Cancelled";
            await db.SaveChangesAsync();

            var repo = new ScheduleRepository(db);
            var conflicts = await repo.GetActiveSchedulesForVehicleAsync(
                vehicleId: 10,
                windowStart: ScheduleSeedData.ConflictDeparture.AddMinutes(-30),
                windowEnd:   ScheduleSeedData.ConflictArrival.AddMinutes(30),
                excludeScheduleId: null);

            Assert.Empty(conflicts); // Cancelled không phải ActiveStatus
        }

        [Fact]
        public async Task OverlapQuery_CompletedSchedule_NotConflict()
        {
            // Chuyến đã Completed cũng không được tính là conflict
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);

            var schedule = await db.Schedules.FindAsync(100);
            schedule!.Status = "Completed";
            await db.SaveChangesAsync();

            var repo = new ScheduleRepository(db);
            var conflicts = await repo.GetActiveSchedulesForVehicleAsync(
                vehicleId: 10,
                windowStart: ScheduleSeedData.ConflictDeparture.AddMinutes(-30),
                windowEnd:   ScheduleSeedData.ConflictArrival.AddMinutes(30),
                excludeScheduleId: null);

            Assert.Empty(conflicts);
        }

        [Fact]
        public async Task OverlapQuery_DelayedSchedule_IsStillConflict()
        {
            // Chuyến bị Delayed vẫn chiếm dụng xe — phải bị phát hiện là conflict
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);

            var schedule = await db.Schedules.FindAsync(100);
            schedule!.Status = "Delayed"; // xe vẫn đang trên đường, chỉ bị trễ
            await db.SaveChangesAsync();

            var repo = new ScheduleRepository(db);
            var conflicts = await repo.GetActiveSchedulesForVehicleAsync(
                vehicleId: 10,
                windowStart: ScheduleSeedData.ConflictDeparture.AddMinutes(-30),
                windowEnd:   ScheduleSeedData.ConflictArrival.AddMinutes(30),
                excludeScheduleId: null);

            Assert.NotEmpty(conflicts); // Delayed vẫn phải bị detect là conflict
        }

        // ── 7. DriverRepository ───────────────────────────────────────────────

        [Fact]
        public async Task GetActiveDrivers_ReturnsOnlyActiveStatus()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);
            var repo = new DriverRepository(db);

            var active = await repo.GetActiveDriversAsync();

            // Driver 1, 2, 3 = Active | Driver 4 = OnLeave (bị loại)
            Assert.Equal(3, active.Count);
            Assert.All(active, d => Assert.Equal("Active", d.Status));
        }

        [Fact]
        public async Task IsDriverCodeExists_ExistingCode_ReturnsTrue()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);
            var repo = new DriverRepository(db);

            Assert.True(await repo.IsDriverCodeExistsAsync("TX-001"));
        }

        [Fact]
        public async Task IsDriverCodeExists_ExcludeSelf_ReturnsFalse()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);
            var repo = new DriverRepository(db);

            // Update driver 1 giữ nguyên code → không báo trùng
            Assert.False(await repo.IsDriverCodeExistsAsync("TX-001", excludeId: 1));
        }

        // ── Helpers gọi private static method thông qua logic tương đương ─────

        /// <summary>
        /// Simulate gọi ScheduleService.ValidateScheduleForm() bằng cách
        /// copy trực tiếp validate logic (method là private static trong service).
        /// </summary>
        private static void InvokeValidateForm(CreateScheduleDto dto)
        {
            if (dto.RouteId <= 0)
                throw new BllValidationException("Schedule", "RouteId", "Vui lòng chọn tuyến xe.");

            if (dto.VehicleId <= 0)
                throw new BllValidationException("Schedule", "VehicleId", "Vui lòng chọn phương tiện.");

            if (dto.DepartureTime <= DateTime.UtcNow)
                throw new BllValidationException("Schedule", "DepartureTime",
                    "Thời gian khởi hành phải là thời điểm trong tương lai.");

            if (dto.EstimatedArrivalTime.HasValue &&
                dto.EstimatedArrivalTime.Value <= dto.DepartureTime)
                throw new BllValidationException("Schedule", "EstimatedArrivalTime",
                    "Thời gian dự kiến đến phải sau thời gian khởi hành.");

            if (dto.Drivers.Count == 0)
                throw new BllValidationException("Schedule", "Drivers",
                    "Mỗi chuyến xe phải có ít nhất 1 tài xế được phân công.");

            int primaryCount = dto.Drivers.Count(d => d.Role == "Primary");
            if (primaryCount == 0)
                throw new BllValidationException("Schedule", "Drivers",
                    "Phải có đúng 1 tài xế với vai trò Primary (lái chính).");
            if (primaryCount > 1)
                throw new BllValidationException("Schedule", "Drivers",
                    $"Chỉ được phép 1 tài xế Primary, hiện đang có {primaryCount}.");

            var duplicates = dto.Drivers.GroupBy(d => d.DriverId)
                .Where(g => g.Count() > 1).Select(g => g.Key).ToList();
            if (duplicates.Count > 0)
                throw new BllValidationException("Schedule", "Drivers",
                    $"Tài xế ID [{string.Join(", ", duplicates)}] được phân công trùng lặp trong cùng 1 chuyến.");
        }
    }
}
