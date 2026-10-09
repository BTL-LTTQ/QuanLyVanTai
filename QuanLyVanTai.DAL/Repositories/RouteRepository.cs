using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.DAL.Repositories
{
    /// <summary>
    /// Repository xử lý nghiệp vụ Tuyến xe (Route).
    /// Kế thừa BaseRepository để tái sử dụng CRUD chung, override các phương thức
    /// cần eager loading, đồng thời triển khai các truy vấn nghiệp vụ đặc thù.
    /// </summary>
    public class RouteRepository : BaseRepository<Route>, IRouteRepository
    {
        public RouteRepository(AppDbContext db) : base(db) { }

        // ── Truy vấn kết hợp (Include / Join) ────────────────────────────────

        /// <inheritdoc/>
        /// <remarks>
        /// Sử dụng Include để lấy kèm Stations (quan hệ N-N qua bảng RouteStations)
        /// và Vehicles (quan hệ 1-N) trong một lần truy vấn, tránh N+1 queries.
        /// </remarks>
        public async Task<List<Route>> GetAllWithStationsAndVehiclesAsync(string? keyword = null)
        {
            var query = _db.Routes
                .Include(r => r.Stations)   // JOIN RouteStations → Stations
                .Include(r => r.Vehicles)   // JOIN Vehicles
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string kw = keyword.Trim().ToLower();
                query = query.Where(r =>
                    r.RouteCode.ToLower().Contains(kw) ||
                    r.RouteName.ToLower().Contains(kw) ||
                    r.Status.ToLower().Contains(kw) ||
                    r.Stations.Any(s => s.StationName.ToLower().Contains(kw)));
            }

            return await query.OrderByDescending(r => r.Id).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Route?> GetByIdWithDetailsAsync(int id)
            => await _db.Routes
                .Include(r => r.Stations)
                .Include(r => r.Vehicles)
                .Include(r => r.Tickets)
                .FirstOrDefaultAsync(r => r.Id == id);

        /// <inheritdoc/>
        public async Task<Route?> GetByRouteCodeAsync(string routeCode)
            => await _db.Routes
                .Include(r => r.Stations)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.RouteCode == routeCode);

        /// <inheritdoc/>
        public async Task<bool> IsRouteCodeExistsAsync(string routeCode, int? excludeId = null)
            => await _db.Routes.AnyAsync(r =>
                r.RouteCode == routeCode &&
                (excludeId == null || r.Id != excludeId));

        // ── Xóa an toàn với Transaction ───────────────────────────────────────

        /// <inheritdoc/>
        /// <remarks>
        /// Chiến lược xóa:
        /// - Soft-delete (mặc định): AppDbContext.ApplyAuditAndSoftDelete() chặn Remove()
        ///   và chuyển thành IsDeleted = true. Vehicle.RouteId được giải phóng về null
        ///   trước để không vi phạm ràng buộc FK SetNull.
        /// - Hard-delete: dùng ExecuteSqlRaw để bỏ qua interceptor ISoftDelete, xóa
        ///   bảng RouteStations trước rồi mới xóa Routes để giữ tính toàn vẹn referential.
        ///   Chỉ cho phép khi KHÔNG có Ticket liên quan.
        /// Transaction bao bọc toàn bộ: rollback tự động nếu bất kỳ bước nào thất bại.
        /// </remarks>
        public async Task DeleteWithTransactionAsync(int routeId, bool softDelete = true)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var route = await _db.Routes
                    .Include(r => r.Stations)
                    .Include(r => r.Vehicles)
                    .Include(r => r.Tickets)
                    .FirstOrDefaultAsync(r => r.Id == routeId)
                    ?? throw new KeyNotFoundException($"Không tìm thấy tuyến xe ID = {routeId}.");

                if (!softDelete && route.Tickets.Count > 0)
                    throw new InvalidOperationException(
                        $"Không thể xóa vĩnh viễn: tuyến xe [{route.RouteCode}] đang có " +
                        $"{route.Tickets.Count} vé liên quan. Hãy chọn xóa mềm.");

                // Bước 1: Giải phóng phương tiện khỏi tuyến
                foreach (var vehicle in route.Vehicles)
                    vehicle.RouteId = null;

                if (softDelete)
                {
                    // Bước 2a: Remove() → interceptor ISoftDelete → IsDeleted = true
                    _db.Routes.Remove(route);
                }
                else
                {
                    // Bước 2b: Xóa quan hệ N-N trong bảng junction trước
                    route.Stations.Clear();
                    await _db.SaveChangesAsync(); // flush vehicle + station clear

                    // Bước 3: Hard-delete bỏ qua Global Query Filter + interceptor
                    await _db.Database.ExecuteSqlRawAsync(
                        "DELETE FROM Routes WHERE Id = {0}", route.Id);

                    await transaction.CommitAsync();
                    return; // Thoát sớm, tránh gọi SaveChanges lần 2
                }

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw; // Re-throw để BLL bắt và bọc vào custom exception
            }
        }
    }
}
