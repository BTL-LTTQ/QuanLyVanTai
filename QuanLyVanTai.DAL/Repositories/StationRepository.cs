using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.DAL.Repositories
{
    /// <summary>
    /// Repository xử lý nghiệp vụ Trạm dừng (Station).
    /// </summary>
    public class StationRepository : BaseRepository<Station>, IStationRepository
    {
        public StationRepository(AppDbContext db) : base(db) { }

        // ── Truy vấn kết hợp ─────────────────────────────────────────────────

        /// <inheritdoc/>
        /// <remarks>
        /// Include(s => s.Routes) thực hiện JOIN qua bảng RouteStations,
        /// trả về Station kèm danh sách Route đi qua trạm đó.
        /// </remarks>
        public async Task<List<Station>> GetAllWithRoutesAsync(string? keyword = null)
        {
            var query = _db.Stations
                .Include(s => s.Routes) // JOIN RouteStations → Routes
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string kw = keyword.Trim().ToLower();
                query = query.Where(s =>
                    s.StationCode.ToLower().Contains(kw) ||
                    s.StationName.ToLower().Contains(kw) ||
                    s.City.ToLower().Contains(kw) ||
                    s.Address.ToLower().Contains(kw));
            }

            return await query.OrderByDescending(s => s.Id).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Station?> GetByIdWithRoutesAsync(int id)
            => await _db.Stations
                .Include(s => s.Routes)
                .FirstOrDefaultAsync(s => s.Id == id);

        /// <inheritdoc/>
        public async Task<Station?> GetByStationCodeAsync(string stationCode)
            => await _db.Stations
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StationCode == stationCode);

        /// <inheritdoc/>
        public async Task<bool> IsStationCodeExistsAsync(string stationCode, int? excludeId = null)
            => await _db.Stations.AnyAsync(s =>
                s.StationCode == stationCode &&
                (excludeId == null || s.Id != excludeId.Value));

        /// <inheritdoc/>
        public async Task<List<string>> GetDistinctCitiesAsync()
            => await _db.Stations
                .AsNoTracking()
                .Select(s => s.City)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

        // ── Xóa an toàn với Transaction ───────────────────────────────────────

        /// <inheritdoc/>
        /// <remarks>
        /// Soft-delete: IsDeleted = true (bảo toàn dữ liệu lịch sử), liên kết
        /// RouteStations vẫn còn nhưng Global Query Filter ẩn Station khỏi truy vấn.
        /// Hard-delete: xóa quan hệ N-N trong RouteStations trước, rồi xóa thẳng bằng SQL.
        /// Chỉ cho phép khi trạm KHÔNG thuộc bất kỳ tuyến nào đang Active.
        /// </remarks>
        public async Task DeleteWithTransactionAsync(int stationId, bool softDelete = true)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var station = await _db.Stations
                    .Include(s => s.Routes)
                    .FirstOrDefaultAsync(s => s.Id == stationId)
                    ?? throw new KeyNotFoundException($"Không tìm thấy trạm dừng ID = {stationId}.");

                if (!softDelete && station.Routes.Any(r => r.Status == "Active"))
                    throw new InvalidOperationException(
                        $"Trạm [{station.StationName}] vẫn thuộc tuyến đang hoạt động. " +
                        "Không thể xóa vĩnh viễn, hãy chọn xóa mềm.");

                if (softDelete)
                {
                    // Remove() → interceptor ISoftDelete → IsDeleted = true
                    _db.Stations.Remove(station);
                }
                else
                {
                    // Xóa liên kết N-N trước (RouteStations)
                    station.Routes.Clear();
                    await _db.SaveChangesAsync();

                    // Hard-delete bỏ qua Global Query Filter
                    await _db.Database.ExecuteSqlRawAsync(
                        "DELETE FROM Stations WHERE Id = {0}", station.Id);

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
