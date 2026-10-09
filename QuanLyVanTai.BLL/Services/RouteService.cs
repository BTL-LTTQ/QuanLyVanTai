using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// DTO nội bộ dùng trong RouteService (legacy). Xem QuanLyVanTai.BLL.DTOs.RouteDetailDto
    /// để dùng với RouteManagementService.
    /// </summary>
    public class RouteServiceDetailDto
    {
        public Route Route { get; set; } = null!;
        public List<Station> SelectedStations { get; set; } = [];
        public List<Vehicle> AssignedVehicles { get; set; } = [];
    }

    public class RouteService
    {
        // Mỗi operation tạo context riêng → tránh concurrent DbContext
        private static AppDbContext CreateContext() => new AppDbContext();

        public async Task<List<Route>> GetAllRoutesAsync(string? keyword = null)
        {
            using var db = CreateContext();
            var query = db.Routes
                .Include(r => r.Stations)
                .Include(r => r.Vehicles)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim().ToLower();
                query = query.Where(r =>
                    r.RouteCode.ToLower().Contains(keyword) ||
                    r.RouteName.ToLower().Contains(keyword) ||
                    r.Status.ToLower().Contains(keyword));
            }

            return await query.OrderByDescending(r => r.Id).ToListAsync();
        }

        public async Task<Route?> GetRouteByIdAsync(int id)
        {
            using var db = CreateContext();
            return await db.Routes
                .Include(r => r.Stations)
                .Include(r => r.Vehicles)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<(bool Success, string Message, Route? Route)> CreateRouteAsync(
            Route route,
            List<int> stationIds,
            List<int> vehicleIds)
        {
            using var db = CreateContext();
            bool exists = await db.Routes.AnyAsync(r => r.RouteCode == route.RouteCode);
            if (exists)
                return (false, $"Mã tuyến '{route.RouteCode}' đã tồn tại trong hệ thống!", null);

            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                db.Routes.Add(route);
                await db.SaveChangesAsync();

                if (stationIds.Count > 0)
                {
                    var stations = await db.Stations
                        .Where(s => stationIds.Contains(s.Id))
                        .ToListAsync();
                    foreach (var st in stations)
                        route.Stations.Add(st);
                }

                if (vehicleIds.Count > 0)
                {
                    var vehicles = await db.Vehicles
                        .Where(v => vehicleIds.Contains(v.Id))
                        .ToListAsync();
                    foreach (var v in vehicles)
                        v.RouteId = route.Id;
                }

                await db.SaveChangesAsync();
                await transaction.CommitAsync();
                return (true, "Thêm mới tuyến xe và liên kết dữ liệu thành công!", route);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Lỗi khi thêm tuyến xe (Đã rollback an toàn): {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string Message)> UpdateRouteAsync(
            Route updatedRoute,
            List<int> stationIds,
            List<int> vehicleIds)
        {
            using var db = CreateContext();
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                var existingRoute = await db.Routes
                    .Include(r => r.Stations)
                    .Include(r => r.Vehicles)
                    .FirstOrDefaultAsync(r => r.Id == updatedRoute.Id);

                if (existingRoute == null)
                    return (false, "Không tìm thấy tuyến xe cần cập nhật!");

                existingRoute.RouteCode = updatedRoute.RouteCode;
                existingRoute.RouteName = updatedRoute.RouteName;
                existingRoute.DistanceKm = updatedRoute.DistanceKm;
                existingRoute.EstimatedHours = updatedRoute.EstimatedHours;
                existingRoute.BasePrice = updatedRoute.BasePrice;
                existingRoute.Status = updatedRoute.Status;

                existingRoute.Stations.Clear();
                if (stationIds.Count > 0)
                {
                    var stations = await db.Stations
                        .Where(s => stationIds.Contains(s.Id))
                        .ToListAsync();
                    foreach (var st in stations)
                        existingRoute.Stations.Add(st);
                }

                var currentVehicles = await db.Vehicles
                    .Where(v => v.RouteId == existingRoute.Id)
                    .ToListAsync();
                foreach (var v in currentVehicles)
                    if (!vehicleIds.Contains(v.Id))
                        v.RouteId = null;

                if (vehicleIds.Count > 0)
                {
                    var selectedVehicles = await db.Vehicles
                        .Where(v => vehicleIds.Contains(v.Id))
                        .ToListAsync();
                    foreach (var v in selectedVehicles)
                        v.RouteId = existingRoute.Id;
                }

                await db.SaveChangesAsync();
                await transaction.CommitAsync();
                return (true, "Cập nhật thông tin tuyến xe thành công!");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Lỗi khi cập nhật tuyến xe (Đã rollback an toàn): {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> DeleteRouteAsync(int routeId, bool softDelete = true)
        {
            using var db = CreateContext();
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                var route = await db.Routes
                    .Include(r => r.Stations)
                    .Include(r => r.Vehicles)
                    .Include(r => r.Tickets)
                    .FirstOrDefaultAsync(r => r.Id == routeId);

                if (route == null)
                    return (false, "Không tìm thấy tuyến xe cần xóa!");

                int ticketCount = route.Tickets.Count;
                if (ticketCount > 0 && !softDelete)
                    return (false, $"Không thể xóa vĩnh viễn: Tuyến xe đang có {ticketCount} vé liên quan! Vui lòng chọn Xóa mềm.");

                if (softDelete)
                {
                    // Soft delete: cập nhật vehicle links rồi Remove() — interceptor sẽ chuyển IsDeleted=true
                    foreach (var v in route.Vehicles)
                        v.RouteId = null;

                    db.Routes.Remove(route);
                    await db.SaveChangesAsync();
                }
                else
                {
                    // Hard delete: xóa vĩnh viễn khỏi DB
                    // Bước 1: Flush vehicle RouteId = null (dùng EF update bình thường)
                    foreach (var v in route.Vehicles)
                        v.RouteId = null;

                    // Bước 2: Xóa quan hệ N-N (RouteStations junction table)
                    route.Stations.Clear();
                    await db.SaveChangesAsync(); // flush vehicle + station links

                    // Bước 3: Xóa thẳng bằng SQL trực tiếp (bỏ qua interceptor ISoftDelete)
                    await db.Database.ExecuteSqlRawAsync(
                        "DELETE FROM Routes WHERE Id = {0}", route.Id);
                }

                await transaction.CommitAsync();

                string msg = softDelete
                    ? $"Đã xóa mềm Tuyến xe [{route.RouteCode}] thành công. Dữ liệu lịch sử và vé vẫn được bảo toàn!"
                    : $"Đã xóa vĩnh viễn Tuyến xe [{route.RouteCode}] khỏi hệ thống.";
                return (true, msg);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Lỗi khi xóa tuyến xe (Đã Rollback để bảo vệ dữ liệu): {ex.Message}");
            }
        }

        public async Task<List<AuditLog>> GetRouteAuditHistoryAsync(int routeId)
        {
            using var db = CreateContext();
            string idStr = routeId.ToString();
            return await db.AuditLogs
                .Where(a => a.EntityName == "Route" && a.RecordId == idStr)
                .OrderByDescending(a => a.Timestamp)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
