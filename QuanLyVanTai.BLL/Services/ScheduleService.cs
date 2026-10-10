using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Exceptions;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.DAL.Repositories;

namespace QuanLyVanTai.BLL.Services
{
    // =========================================================================
    // ScheduleService — Lập lịch trình & Phân công tài xế
    // =========================================================================
    // Validate pipeline mỗi khi tạo / cập nhật chuyến:
    //
    //  1. Validate form (required fields, thời gian hợp lệ)
    //  2. Kiểm tra Route tồn tại và đang Active
    //  3. Kiểm tra Vehicle tồn tại, không đang Maintenance
    //  4. Kiểm tra GPLX tài xế còn hạn
    //  5. Detect conflict xe — xe đang chạy chuyến khác trùng giờ?
    //  6. Detect conflict tài xế — tài xế đang bận chuyến khác trùng giờ?
    //
    // Bất kỳ bước nào thất bại → throw custom exception với message cụ thể.
    // =========================================================================

    public class ScheduleService
    {
        // Khoảng đệm thời gian tối thiểu giữa 2 chuyến liên tiếp của cùng 1 xe (phút).
        // Dùng để xe có thời gian quay đầu, nạp nhiên liệu.
        private const int VehicleBufferMinutes = 30;

        // Khoảng đệm tài xế — nghỉ giải lao tối thiểu giữa 2 chuyến (phút).
        private const int DriverBufferMinutes = 15;

        // ── Truy vấn ──────────────────────────────────────────────────────────

        /// <summary>Lấy danh sách chuyến xe với bộ lọc tuỳ ý.</summary>
        public async Task<List<ScheduleListDto>> GetSchedulesAsync(
            DateTime? from = null,
            DateTime? to = null,
            string? status = null,
            int? routeId = null,
            int? vehicleId = null)
        {
            try
            {
                using var db = new AppDbContext();
                var repo = new ScheduleRepository(db);
                var schedules = await repo.GetSchedulesAsync(from, to, status, routeId, vehicleId);
                return schedules.Select(MapToListDto).ToList();
            }
            catch (Exception ex) when (ex is not BusStationException)
            {
                throw new DataAccessException("Schedule", "GetSchedules", ex);
            }
        }

        /// <summary>Lấy chi tiết 1 chuyến kèm tài xế phân công.</summary>
        public async Task<ScheduleDetailDto> GetScheduleDetailAsync(int id)
        {
            try
            {
                using var db = new AppDbContext();
                var schedule = await new ScheduleRepository(db).GetByIdWithDetailsAsync(id)
                    ?? throw new EntityNotFoundException("Schedule", id);
                return MapToDetailDto(schedule);
            }
            catch (BusStationException) { throw; }
            catch (Exception ex) { throw new DataAccessException("Schedule", "GetScheduleDetail", ex); }
        }

        // ── Tạo mới chuyến xe ─────────────────────────────────────────────────

        /// <summary>
        /// Tạo một chuyến xe mới, gán xe, tuyến, thời gian và phân công tài xế.
        /// Toàn bộ validate chạy trước khi bất kỳ thay đổi nào được ghi xuống DB.
        /// </summary>
        /// <exception cref="ValidationException">Dữ liệu form không hợp lệ.</exception>
        /// <exception cref="EntityNotFoundException">Route / Vehicle / Driver không tồn tại.</exception>
        /// <exception cref="BusinessRuleViolationException">
        /// Vi phạm quy tắc nghiệp vụ: xe hoặc tài xế đang bận chuyến khác,
        /// GPLX hết hạn, route bị suspended...
        /// </exception>
        /// <exception cref="DataAccessException">Lỗi lưu database.</exception>
        public async Task<ScheduleDetailDto> CreateScheduleAsync(CreateScheduleDto dto)
        {
            // ── Bước 1: Validate dữ liệu đầu vào ─────────────────────────────
            ValidateScheduleForm(dto);

            try
            {
                using var db = new AppDbContext();

                // ── Bước 2: Kiểm tra Route ─────────────────────────────────────
                var route = await db.Routes.FindAsync(dto.RouteId)
                    ?? throw new EntityNotFoundException("Route", dto.RouteId);

                if (route.Status != "Active")
                    throw new BusinessRuleViolationException("Schedule",
                        $"Tuyến [{route.RouteCode}] đang ở trạng thái '{route.Status}'. " +
                        "Chỉ có thể lập lịch cho tuyến Active.");

                // ── Bước 3: Kiểm tra Vehicle ───────────────────────────────────
                var vehicle = await db.Vehicles.FindAsync(dto.VehicleId)
                    ?? throw new EntityNotFoundException("Vehicle", dto.VehicleId);

                if (vehicle.Status == "Maintenance")
                    throw new BusinessRuleViolationException("Schedule",
                        $"Xe [{vehicle.LicensePlate}] đang trong trạng thái Bảo dưỡng. " +
                        "Không thể lập lịch cho xe này.");

                // ── Tính thời gian kết thúc dự kiến ───────────────────────────
                DateTime arrival = dto.EstimatedArrivalTime
                    ?? dto.DepartureTime.AddHours((double)route.EstimatedHours);

                // ── Bước 4: Load và validate tất cả tài xế trước khi detect conflict ──
                var driverEntities = await LoadAndValidateDriversAsync(db, dto.Drivers, arrival);

                // ── Bước 5: Detect conflict xe ─────────────────────────────────
                await CheckVehicleConflictAsync(db, vehicle, dto.DepartureTime, arrival,
                    excludeScheduleId: null);

                // ── Bước 6: Detect conflict tài xế ────────────────────────────
                await CheckDriverConflictsAsync(db, driverEntities, dto.DepartureTime, arrival,
                    excludeScheduleId: null);

                // ── Bước 7: Tạo Schedule và lưu (trong transaction) ───────────
                await using var transaction = await db.Database.BeginTransactionAsync();
                try
                {
                    var schedule = new Schedule
                    {
                        ScheduleCode         = GenerateScheduleCode(dto.DepartureTime),
                        RouteId              = dto.RouteId,
                        VehicleId            = dto.VehicleId,
                        DepartureTime        = dto.DepartureTime,
                        EstimatedArrivalTime = arrival,
                        Status               = "Scheduled",
                        Notes                = dto.Notes
                    };

                    db.Schedules.Add(schedule);
                    await db.SaveChangesAsync();

                    // Phân công tài xế
                    foreach (var assignDto in dto.Drivers)
                    {
                        db.DriverAssignments.Add(new DriverAssignment
                        {
                            ScheduleId       = schedule.Id,
                            DriverId         = assignDto.DriverId,
                            Role             = assignDto.Role,
                            AssignmentStatus = "Assigned",
                            Notes            = assignDto.Notes
                        });
                    }
                    await db.SaveChangesAsync();
                    await transaction.CommitAsync();

                    // Trả về detail DTO đầy đủ
                    var saved = await new ScheduleRepository(db).GetByIdWithDetailsAsync(schedule.Id);
                    return MapToDetailDto(saved!);
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (BusStationException) { throw; }
            catch (Exception ex) { throw new DataAccessException("Schedule", "CreateSchedule", ex); }
        }

        // ── Cập nhật chuyến xe ────────────────────────────────────────────────

        /// <summary>
        /// Cập nhật thông tin chuyến xe (thời gian, xe, tuyến).
        /// Re-validate toàn bộ conflict sau khi thay đổi, loại trừ chính chuyến đang sửa.
        /// </summary>
        public async Task<ScheduleDetailDto> UpdateScheduleAsync(int scheduleId, CreateScheduleDto dto)
        {
            ValidateScheduleForm(dto);

            try
            {
                using var db = new AppDbContext();
                var scheduleRepo = new ScheduleRepository(db);

                var existing = await scheduleRepo.GetByIdWithDetailsAsync(scheduleId)
                    ?? throw new EntityNotFoundException("Schedule", scheduleId);

                if (existing.Status is "Completed" or "Cancelled")
                    throw new BusinessRuleViolationException("Schedule",
                        $"Chuyến [{existing.ScheduleCode}] đã ở trạng thái '{existing.Status}'. " +
                        "Không thể chỉnh sửa.");

                var route = await db.Routes.FindAsync(dto.RouteId)
                    ?? throw new EntityNotFoundException("Route", dto.RouteId);

                if (route.Status != "Active")
                    throw new BusinessRuleViolationException("Schedule",
                        $"Tuyến [{route.RouteCode}] đang ở trạng thái '{route.Status}'.");

                var vehicle = await db.Vehicles.FindAsync(dto.VehicleId)
                    ?? throw new EntityNotFoundException("Vehicle", dto.VehicleId);

                if (vehicle.Status == "Maintenance")
                    throw new BusinessRuleViolationException("Schedule",
                        $"Xe [{vehicle.LicensePlate}] đang Bảo dưỡng.");

                DateTime arrival = dto.EstimatedArrivalTime
                    ?? dto.DepartureTime.AddHours((double)route.EstimatedHours);

                var driverEntities = await LoadAndValidateDriversAsync(db, dto.Drivers, arrival);

                // Loại trừ chính chuyến này khi kiểm tra conflict
                await CheckVehicleConflictAsync(db, vehicle, dto.DepartureTime, arrival,
                    excludeScheduleId: scheduleId);

                await CheckDriverConflictsAsync(db, driverEntities, dto.DepartureTime, arrival,
                    excludeScheduleId: scheduleId);

                await using var transaction = await db.Database.BeginTransactionAsync();
                try
                {
                    existing.RouteId              = dto.RouteId;
                    existing.VehicleId            = dto.VehicleId;
                    existing.DepartureTime        = dto.DepartureTime;
                    existing.EstimatedArrivalTime = arrival;
                    existing.Notes                = dto.Notes;

                    // Xóa phân công cũ, flush trước rồi mới tạo mới
                    // để tránh vi phạm UNIQUE index (ScheduleId, DriverId, Role)
                    // khi EF xử lý cùng lúc delete + insert cùng key.
                    db.DriverAssignments.RemoveRange(existing.DriverAssignments);
                    await db.SaveChangesAsync(); // flush delete

                    foreach (var assignDto in dto.Drivers)
                    {
                        db.DriverAssignments.Add(new DriverAssignment
                        {
                            ScheduleId       = existing.Id,
                            DriverId         = assignDto.DriverId,
                            Role             = assignDto.Role,
                            AssignmentStatus = "Assigned",
                            Notes            = assignDto.Notes
                        });
                    }

                    await db.SaveChangesAsync();
                    await transaction.CommitAsync();

                    var updated = await scheduleRepo.GetByIdWithDetailsAsync(scheduleId);
                    return MapToDetailDto(updated!);
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (BusStationException) { throw; }
            catch (Exception ex) { throw new DataAccessException("Schedule", "UpdateSchedule", ex); }
        }

        // ── Cập nhật trạng thái ───────────────────────────────────────────────

        /// <summary>
        /// Cập nhật trạng thái chuyến (InProgress, Completed, Cancelled...).
        /// Khi Completed, ghi nhận thời gian đến thực tế.
        /// Khi Delayed, cho phép cập nhật lại thời gian dự kiến đến.
        /// </summary>
        public async Task UpdateScheduleStatusAsync(
            int scheduleId,
            string newStatus,
            DateTime? actualArrival = null,
            DateTime? newEstimatedArrival = null)
        {
            var validStatuses = new[] { "Scheduled", "InProgress", "Completed", "Cancelled", "Delayed" };
            if (!validStatuses.Contains(newStatus))
                throw new ValidationException("Schedule", "Status",
                    $"Trạng thái không hợp lệ. Chỉ chấp nhận: {string.Join(", ", validStatuses)}.");

            if (newStatus == "Delayed" && newEstimatedArrival.HasValue)
            {
                // Thời gian mới phải sau thời điểm hiện tại
                if (newEstimatedArrival.Value <= DateTime.UtcNow)
                    throw new ValidationException("Schedule", "NewEstimatedArrival",
                        "Thời gian đến mới khi trễ phải ở tương lai.");
            }

            try
            {
                using var db = new AppDbContext();
                var schedule = await db.Schedules.FindAsync(scheduleId)
                    ?? throw new EntityNotFoundException("Schedule", scheduleId);

                // Kiểm tra transition hợp lệ
                EnsureValidStatusTransition(schedule.Status, newStatus, schedule.ScheduleCode);

                schedule.Status = newStatus;

                if (newStatus == "Completed")
                    schedule.ActualArrivalTime = actualArrival ?? DateTime.UtcNow;

                if (newStatus == "Delayed" && newEstimatedArrival.HasValue)
                    schedule.EstimatedArrivalTime = newEstimatedArrival.Value;

                await db.SaveChangesAsync();
            }
            catch (BusStationException) { throw; }
            catch (Exception ex) { throw new DataAccessException("Schedule", "UpdateStatus", ex); }
        }

        // ── Validate helpers ──────────────────────────────────────────────────

        private static void ValidateScheduleForm(CreateScheduleDto dto)
        {
            if (dto.RouteId <= 0)
                throw new ValidationException("Schedule", "RouteId", "Vui lòng chọn tuyến xe.");

            if (dto.VehicleId <= 0)
                throw new ValidationException("Schedule", "VehicleId", "Vui lòng chọn phương tiện.");

            if (dto.DepartureTime <= DateTime.UtcNow)
                throw new ValidationException("Schedule", "DepartureTime",
                    "Thời gian khởi hành phải là thời điểm trong tương lai.");

            if (dto.EstimatedArrivalTime.HasValue &&
                dto.EstimatedArrivalTime.Value <= dto.DepartureTime)
                throw new ValidationException("Schedule", "EstimatedArrivalTime",
                    "Thời gian dự kiến đến phải sau thời gian khởi hành.");

            if (dto.Drivers.Count == 0)
                throw new ValidationException("Schedule", "Drivers",
                    "Mỗi chuyến xe phải có ít nhất 1 tài xế được phân công.");

            // Mỗi chuyến chỉ có tối đa 1 lái chính
            int primaryCount = dto.Drivers.Count(d => d.Role == "Primary");
            if (primaryCount == 0)
                throw new ValidationException("Schedule", "Drivers",
                    "Phải có đúng 1 tài xế với vai trò Primary (lái chính).");
            if (primaryCount > 1)
                throw new ValidationException("Schedule", "Drivers",
                    $"Chỉ được phép 1 tài xế Primary, hiện đang có {primaryCount}.");

            // Không phân công cùng 1 tài xế 2 lần trong 1 chuyến
            var duplicateDrivers = dto.Drivers
                .GroupBy(d => d.DriverId)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (duplicateDrivers.Count > 0)
                throw new ValidationException("Schedule", "Drivers",
                    $"Tài xế ID [{string.Join(", ", duplicateDrivers)}] được phân công trùng lặp trong cùng 1 chuyến.");
        }

        /// <summary>
        /// Load thông tin tài xế từ DB và validate từng người:
        /// - Tài xế phải tồn tại và đang Active
        /// - GPLX chưa hết hạn tại thời điểm kết thúc chuyến
        /// </summary>
        private static async Task<List<Driver>> LoadAndValidateDriversAsync(
            AppDbContext db,
            List<AssignDriverDto> assignDtos,
            DateTime arrivalTime)
        {
            var driverIds = assignDtos.Select(d => d.DriverId).Distinct().ToList();
            var drivers = await db.Drivers
                .Where(d => driverIds.Contains(d.Id))
                .ToListAsync();

            // Kiểm tra tài xế không tồn tại
            var notFound = driverIds.Except(drivers.Select(d => d.Id)).ToList();
            if (notFound.Count > 0)
                throw new EntityNotFoundException("Driver",
                    $"Không tìm thấy tài xế với ID: [{string.Join(", ", notFound)}].");

            foreach (var driver in drivers)
            {
                // Tài xế phải đang hoạt động
                if (driver.Status != "Active")
                    throw new BusinessRuleViolationException("Schedule",
                        $"Tài xế [{driver.FullName}] đang ở trạng thái '{driver.Status}'. " +
                        "Không thể phân công tài xế không Active.");

                // GPLX không được hết hạn trước khi chuyến kết thúc
                if (driver.LicenseExpiryDate < arrivalTime)
                    throw new BusinessRuleViolationException("Schedule",
                        $"GPLX của tài xế [{driver.FullName}] (số: {driver.LicenseNumber}) " +
                        $"hết hạn ngày {driver.LicenseExpiryDate:dd/MM/yyyy}. " +
                        "Không thể phân công tài xế có GPLX hết hạn.");
            }

            return drivers;
        }

        /// <summary>
        /// Kiểm tra xe có đang bận chuyến khác trùng thời gian không.
        /// Áp dụng thêm buffer <see cref="VehicleBufferMinutes"/> để đảm bảo xe có thời gian quay đầu.
        /// </summary>
        private static async Task CheckVehicleConflictAsync(
            AppDbContext db,
            Vehicle vehicle,
            DateTime departure,
            DateTime arrival,
            int? excludeScheduleId)
        {
            // Mở rộng cửa sổ kiểm tra bằng buffer
            DateTime checkStart = departure.AddMinutes(-VehicleBufferMinutes);
            DateTime checkEnd   = arrival.AddMinutes(VehicleBufferMinutes);

            var conflicts = await new ScheduleRepository(db)
                .GetActiveSchedulesForVehicleAsync(vehicle.Id, checkStart, checkEnd, excludeScheduleId);

            if (conflicts.Count == 0) return;

            // Lấy conflict đầu tiên để tạo message cụ thể
            var conflict = conflicts[0];
            string timeRange = FormatTimeRange(conflict.DepartureTime, conflict.EstimatedArrivalTime);
            string routeName = conflict.Route?.RouteName ?? $"Tuyến #{conflict.RouteId}";

            throw new BusinessRuleViolationException("Schedule",
                $"Xe [{vehicle.LicensePlate}] đang được phân công chạy {routeName} " +
                $"từ {timeRange}. " +
                $"Cần khoảng cách tối thiểu {VehicleBufferMinutes} phút giữa 2 chuyến liên tiếp.");
        }

        /// <summary>
        /// Kiểm tra từng tài xế có đang bận chuyến khác trùng thời gian không.
        /// Ném exception ngay khi phát hiện conflict đầu tiên với message đầy đủ chi tiết.
        /// </summary>
        private static async Task CheckDriverConflictsAsync(
            AppDbContext db,
            List<Driver> drivers,
            DateTime departure,
            DateTime arrival,
            int? excludeScheduleId)
        {
            // Buffer tài xế nhỏ hơn: chỉ cần nghỉ ngắn giữa 2 chuyến
            DateTime checkStart = departure.AddMinutes(-DriverBufferMinutes);
            DateTime checkEnd   = arrival.AddMinutes(DriverBufferMinutes);

            var scheduleRepo = new ScheduleRepository(db);

            foreach (var driver in drivers)
            {
                var conflicts = await scheduleRepo.GetActiveSchedulesForDriverAsync(
                    driver.Id, checkStart, checkEnd, excludeScheduleId);

                if (conflicts.Count == 0) continue;

                var conflict = conflicts[0];
                string timeRange = FormatTimeRange(conflict.DepartureTime, conflict.EstimatedArrivalTime);
                string routeName = conflict.Route?.RouteName ?? $"Tuyến #{conflict.RouteId}";

                throw new BusinessRuleViolationException("Schedule",
                    $"Tài xế [{driver.FullName}] đang chạy chuyến {routeName} " +
                    $"từ {timeRange}. " +
                    $"Cần khoảng cách tối thiểu {DriverBufferMinutes} phút giữa 2 chuyến.");
            }
        }

        /// <summary>Đảm bảo transition trạng thái theo state machine hợp lệ.</summary>
        private static void EnsureValidStatusTransition(string current, string next, string code)
        {
            // State machine: Scheduled → InProgress | Cancelled | Delayed
            //                InProgress → Completed | Delayed
            //                Delayed → InProgress | Cancelled
            //                Completed / Cancelled → terminal (không đổi được)
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

            if (!valid)
                throw new BusinessRuleViolationException("Schedule",
                    $"Chuyến [{code}]: không thể chuyển trạng thái từ '{current}' sang '{next}'.");
        }

        // ── Mapping & Helpers ─────────────────────────────────────────────────

        private static string GenerateScheduleCode(DateTime departure)
            => $"SCH-{departure:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";

        private static string FormatTimeRange(DateTime start, DateTime end)
        {
            // Nếu cùng ngày: "08:30 - 10:45 ngày 10/10/2026"
            // Khác ngày: "08:30 ngày 10/10 - 06:00 ngày 11/10/2026"
            if (start.Date == end.Date)
                return $"{start:HH:mm} - {end:HH:mm} ngày {start:dd/MM/yyyy}";
            return $"{start:HH:mm dd/MM} - {end:HH:mm dd/MM/yyyy}";
        }

        private static ScheduleListDto MapToListDto(Schedule s)
        {
            var primaryAssignment = s.DriverAssignments
                .FirstOrDefault(a => a.Role == "Primary" && a.AssignmentStatus != "Cancelled");
            var secondaryAssignment = s.DriverAssignments
                .FirstOrDefault(a => a.Role == "Secondary" && a.AssignmentStatus != "Cancelled");

            return new ScheduleListDto
            {
                Id                   = s.Id,
                ScheduleCode         = s.ScheduleCode,
                RouteName            = s.Route?.RouteName ?? string.Empty,
                RouteCode            = s.Route?.RouteCode ?? string.Empty,
                LicensePlate         = s.Vehicle?.LicensePlate ?? string.Empty,
                VehicleType          = s.Vehicle?.VehicleType ?? string.Empty,
                DepartureTime        = s.DepartureTime,
                EstimatedArrivalTime = s.EstimatedArrivalTime,
                ActualArrivalTime    = s.ActualArrivalTime,
                Status               = s.Status,
                PrimaryDriverName    = primaryAssignment?.Driver?.FullName ?? string.Empty,
                SecondaryDriverName  = secondaryAssignment?.Driver?.FullName,
                Notes                = s.Notes,
                CreatedAt            = s.CreatedAt
            };
        }

        private static ScheduleDetailDto MapToDetailDto(Schedule s) => new()
        {
            Id                   = s.Id,
            ScheduleCode         = s.ScheduleCode,
            RouteId              = s.RouteId,
            RouteCode            = s.Route?.RouteCode ?? string.Empty,
            RouteName            = s.Route?.RouteName ?? string.Empty,
            DistanceKm           = s.Route?.DistanceKm ?? 0,
            VehicleId            = s.VehicleId,
            LicensePlate         = s.Vehicle?.LicensePlate ?? string.Empty,
            VehicleType          = s.Vehicle?.VehicleType ?? string.Empty,
            TotalSeats           = s.Vehicle?.TotalSeats ?? 0,
            DepartureTime        = s.DepartureTime,
            EstimatedArrivalTime = s.EstimatedArrivalTime,
            ActualArrivalTime    = s.ActualArrivalTime,
            Status               = s.Status,
            Notes                = s.Notes,
            CreatedAt            = s.CreatedAt,
            CreatedBy            = s.CreatedBy,
            UpdatedAt            = s.UpdatedAt,
            Assignments = s.DriverAssignments.Select(a => new DriverAssignmentDto
            {
                AssignmentId     = a.Id,
                DriverId         = a.DriverId,
                DriverCode       = a.Driver?.DriverCode ?? string.Empty,
                DriverName       = a.Driver?.FullName ?? string.Empty,
                LicenseNumber    = a.Driver?.LicenseNumber ?? string.Empty,
                Role             = a.Role,
                AssignmentStatus = a.AssignmentStatus,
                Notes            = a.Notes
            }).ToList()
        };
    }
}
