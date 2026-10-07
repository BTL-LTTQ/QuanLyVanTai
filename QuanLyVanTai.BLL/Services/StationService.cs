using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.BLL.Services
{
    public class StationFilterCriteria
    {
        public string? Keyword { get; set; }
        public string? City { get; set; }
        public string? Status { get; set; }
        public bool UseAndLogic { get; set; } = true;
    }

    public class StationService
    {
        // Mỗi operation tạo context riêng → tránh concurrent DbContext
        private static AppDbContext CreateContext() => new AppDbContext();

        public async Task<List<Station>> GetAllStationsAsync()
        {
            using var db = CreateContext();
            return await db.Stations
                .Include(s => s.Routes)
                .AsNoTracking()
                .OrderByDescending(s => s.Id)
                .ToListAsync();
        }

        public async Task<List<Station>> SearchAndFilterStationsAsync(StationFilterCriteria criteria)
        {
            using var db = CreateContext();
            var query = db.Stations
                .Include(s => s.Routes)
                .AsNoTracking()
                .AsQueryable();

            bool hasKeyword = !string.IsNullOrWhiteSpace(criteria.Keyword);
            bool hasCity = !string.IsNullOrWhiteSpace(criteria.City) && criteria.City != "Tất cả";
            bool hasStatus = !string.IsNullOrWhiteSpace(criteria.Status) && criteria.Status != "Tất cả";

            if (!hasKeyword && !hasCity && !hasStatus)
                return await query.OrderByDescending(s => s.Id).ToListAsync();

            string kw = criteria.Keyword?.Trim().ToLower() ?? "";
            string ct = criteria.City?.Trim().ToLower() ?? "";
            string st = criteria.Status?.Trim().ToLower() ?? "";

            if (criteria.UseAndLogic)
            {
                if (hasKeyword)
                    query = query.Where(s =>
                        s.StationCode.ToLower().Contains(kw) ||
                        s.StationName.ToLower().Contains(kw) ||
                        s.Address.ToLower().Contains(kw) ||
                        s.City.ToLower().Contains(kw));

                if (hasCity)
                    query = query.Where(s => s.City.ToLower() == ct);

                if (hasStatus)
                    query = query.Where(s => s.Status.ToLower() == st);
            }
            else
            {
                query = query.Where(s =>
                    (hasKeyword && (
                        s.StationCode.ToLower().Contains(kw) ||
                        s.StationName.ToLower().Contains(kw) ||
                        s.Address.ToLower().Contains(kw) ||
                        s.City.ToLower().Contains(kw))) ||
                    (hasCity && s.City.ToLower() == ct) ||
                    (hasStatus && s.Status.ToLower() == st));
            }

            return await query.OrderByDescending(s => s.Id).ToListAsync();
        }

        public async Task<(bool Success, string Message, Station? Station)> CreateStationAsync(Station station)
        {
            using var db = CreateContext();
            bool exists = await db.Stations.AnyAsync(s => s.StationCode == station.StationCode);
            if (exists)
                return (false, $"Mã trạm '{station.StationCode}' đã tồn tại!", null);

            db.Stations.Add(station);
            await db.SaveChangesAsync();
            return (true, "Thêm trạm dừng thành công!", station);
        }

        public async Task<(bool Success, string Message)> UpdateStationAsync(Station station)
        {
            using var db = CreateContext();
            var existing = await db.Stations.FindAsync(station.Id);
            if (existing == null)
                return (false, "Không tìm thấy trạm dừng cần cập nhật!");

            existing.StationCode = station.StationCode;
            existing.StationName = station.StationName;
            existing.Address = station.Address;
            existing.City = station.City;
            existing.Status = station.Status;

            await db.SaveChangesAsync();
            return (true, "Cập nhật thông tin trạm dừng thành công!");
        }

        public async Task<(bool Success, string Message)> DeleteStationAsync(int stationId, bool softDelete = true)
        {
            using var db = CreateContext();
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                var station = await db.Stations
                    .Include(s => s.Routes)
                    .FirstOrDefaultAsync(s => s.Id == stationId);

                if (station == null)
                    return (false, "Không tìm thấy trạm dừng cần xóa!");

                if (station.Routes.Count > 0 && !softDelete)
                    return (false, $"Trạm đang thuộc {station.Routes.Count} tuyến xe! Vui lòng chọn Xóa mềm.");

                if (softDelete)
                    db.Stations.Remove(station);
                else
                {
                    station.Routes.Clear();
                    db.Entry(station).State = EntityState.Deleted;
                }

                await db.SaveChangesAsync();
                await transaction.CommitAsync();
                return (true, $"Đã xóa trạm dừng [{station.StationName}] thành công!");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Lỗi khi xóa trạm dừng: {ex.Message}");
            }
        }
    }
}
