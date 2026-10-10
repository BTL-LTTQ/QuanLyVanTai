using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.DAL.Repositories
{
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        // Các status được coi là "đang chiếm dụng" xe / tài xế
        // Delayed vẫn phải tính vì xe/tài xế vẫn đang trên đường, chưa về bến.
        private static readonly string[] ActiveStatuses = ["Scheduled", "InProgress", "Delayed"];

        public ScheduleRepository(AppDbContext db) : base(db) { }

        public async Task<Schedule?> GetByIdWithDetailsAsync(int id)
            => await _db.Schedules
                .Include(s => s.Route)
                .Include(s => s.Vehicle)
                .Include(s => s.DriverAssignments)
                    .ThenInclude(a => a.Driver)
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<List<Schedule>> GetSchedulesAsync(
            DateTime? from,
            DateTime? to,
            string? status,
            int? routeId,
            int? vehicleId)
        {
            var query = _db.Schedules
                .Include(s => s.Route)
                .Include(s => s.Vehicle)
                .Include(s => s.DriverAssignments)
                    .ThenInclude(a => a.Driver)
                .AsNoTracking()
                .AsQueryable();

            if (from.HasValue)
                query = query.Where(s => s.DepartureTime >= from.Value);
            if (to.HasValue)
                query = query.Where(s => s.DepartureTime <= to.Value);
            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(s => s.Status == status);
            if (routeId.HasValue)
                query = query.Where(s => s.RouteId == routeId.Value);
            if (vehicleId.HasValue)
                query = query.Where(s => s.VehicleId == vehicleId.Value);

            return await query.OrderBy(s => s.DepartureTime).ToListAsync();
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Detect overlap: hai khoảng [A.Departure, A.Arrival) và [B.Departure, B.Arrival)
        /// chồng nhau khi A.Departure &lt; B.Arrival AND A.Arrival &gt; B.Departure.
        /// </remarks>
        public async Task<List<Schedule>> GetActiveSchedulesForVehicleAsync(
            int vehicleId,
            DateTime windowStart,
            DateTime windowEnd,
            int? excludeScheduleId = null)
            => await _db.Schedules
                .Include(s => s.Route)
                .Where(s =>
                    s.VehicleId == vehicleId &&
                    ActiveStatuses.Contains(s.Status) &&
                    s.DepartureTime < windowEnd &&
                    s.EstimatedArrivalTime > windowStart &&
                    (excludeScheduleId == null || s.Id != excludeScheduleId.Value))
                .AsNoTracking()
                .ToListAsync();

        /// <inheritdoc/>
        public async Task<List<Schedule>> GetActiveSchedulesForDriverAsync(
            int driverId,
            DateTime windowStart,
            DateTime windowEnd,
            int? excludeScheduleId = null)
            => await _db.Schedules
                .Include(s => s.Route)
                .Include(s => s.Vehicle)
                .Where(s =>
                    ActiveStatuses.Contains(s.Status) &&
                    s.DepartureTime < windowEnd &&
                    s.EstimatedArrivalTime > windowStart &&
                    (excludeScheduleId == null || s.Id != excludeScheduleId.Value) &&
                    s.DriverAssignments.Any(a =>
                        a.DriverId == driverId &&
                        a.AssignmentStatus != "Cancelled" &&
                        a.AssignmentStatus != "Replaced"))
                .AsNoTracking()
                .ToListAsync();

        public async Task<bool> IsScheduleCodeExistsAsync(string code, int? excludeId = null)
            => await _db.Schedules.AnyAsync(s =>
                s.ScheduleCode == code &&
                (excludeId == null || s.Id != excludeId.Value));
    }
}
