using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.DAL.Repositories
{
    /// <summary>
    /// Repository xử lý nghiệp vụ Phương tiện (Vehicle).
    /// </summary>
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(AppDbContext db) : base(db) { }

        // ── Truy vấn kết hợp ─────────────────────────────────────────────────

        /// <inheritdoc/>
        /// <remarks>
        /// Include(v => v.Route) thực hiện JOIN Vehicle → Route, trả về
        /// thông tin tuyến xe phân công kèm từng phương tiện.
        /// </remarks>
        public async Task<List<Vehicle>> GetAllWithRouteAsync()
            => await _db.Vehicles
                .Include(v => v.Route) // JOIN Routes
                .AsNoTracking()
                .OrderByDescending(v => v.Id)
                .ToListAsync();

        /// <inheritdoc/>
        public async Task<List<Vehicle>> GetAvailableVehiclesAsync()
            => await _db.Vehicles
                .AsNoTracking()
                .Where(v => v.Status == "Ready" && v.RouteId == null)
                .OrderBy(v => v.LicensePlate)
                .ToListAsync();

        /// <inheritdoc/>
        public async Task<List<Vehicle>> GetVehiclesByRouteAsync(int routeId)
            => await _db.Vehicles
                .Include(v => v.Route)
                .AsNoTracking()
                .Where(v => v.RouteId == routeId)
                .OrderBy(v => v.LicensePlate)
                .ToListAsync();

        /// <inheritdoc/>
        public async Task<bool> IsLicensePlateExistsAsync(string licensePlate, int? excludeId = null)
            => await _db.Vehicles.AnyAsync(v =>
                v.LicensePlate == licensePlate &&
                (excludeId == null || v.Id != excludeId));

        // ── Xóa an toàn với Transaction ───────────────────────────────────────

        /// <inheritdoc/>
        /// <remarks>
        /// Hard-delete chỉ được phép khi phương tiện KHÔNG có Ticket liên quan
        /// (ràng buộc FK Restrict trong AppDbContext). Nếu có Ticket, bắt buộc
        /// phải dùng soft-delete để bảo toàn lịch sử vé.
        /// </remarks>
        public async Task DeleteWithTransactionAsync(int vehicleId, bool softDelete = true)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var vehicle = await _db.Vehicles
                    .Include(v => v.Tickets)
                    .FirstOrDefaultAsync(v => v.Id == vehicleId)
                    ?? throw new KeyNotFoundException($"Không tìm thấy phương tiện ID = {vehicleId}.");

                if (!softDelete && vehicle.Tickets.Count > 0)
                    throw new InvalidOperationException(
                        $"Xe [{vehicle.LicensePlate}] đang có {vehicle.Tickets.Count} vé liên quan. " +
                        "Không thể xóa vĩnh viễn, hãy chọn xóa mềm.");

                if (softDelete)
                {
                    // Gỡ khỏi tuyến để tránh khoá FK trước khi soft-delete
                    vehicle.RouteId = null;
                    _db.Vehicles.Remove(vehicle); // → interceptor chuyển IsDeleted = true
                }
                else
                {
                    await _db.Database.ExecuteSqlRawAsync(
                        "DELETE FROM Vehicles WHERE Id = {0}", vehicle.Id);

                    await transaction.CommitAsync();
                    return;
                }

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
