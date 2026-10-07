using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.BLL.Services
{
    public class RouteDetailDto
    {
        public Route Route { get; set; } = null!;
        public List<Station> SelectedStations { get; set; } = [];
        public List<Vehicle> AssignedVehicles { get; set; } = [];
    }

    public class RouteService
    {
        private readonly AppDbContext _context;

        public RouteService(AppDbContext context)
        {
            _context = context;
        }

        public RouteService() : this(new AppDbContext())
        {
        }

        /// <summary>
        /// Lấy tất cả tuyến xe kèm danh sách trạm dừng và xe phụ trách
        /// </summary>
        public async Task<List<Route>> GetAllRoutesAsync(string? keyword = null)
        {
            var query = _context.Routes
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

        /// <summary>
        /// Lấy chi tiết tuyến xe theo ID bao gồm trạm dừng và phương tiện
        /// </summary>
        public async Task<Route?> GetRouteByIdAsync(int id)
        {
            return await _context.Routes
                .Include(r => r.Stations)
                .Include(r => r.Vehicles)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        /// <summary>
        /// Tạo mới tuyến xe liên kết đa bảng với Trạm dừng và Phương tiện (dùng Transaction)
        /// </summary>
        public async Task<(bool Success, string Message, Route? Route)> CreateRouteAsync(
            Route route,
            List<int> stationIds,
            List<int> vehicleIds)
        {
            // Kiểm tra trùng mã tuyến
            bool exists = await _context.Routes.AnyAsync(r => r.RouteCode == route.RouteCode);
            if (exists)
            {
                return (false, $"Mã tuyến '{route.RouteCode}' đã tồn tại trong hệ thống!", null);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Thêm tuyến xe
                _context.Routes.Add(route);
                await _context.SaveChangesAsync();

                // 2. Liên kết các Trạm dừng (N-N)
                if (stationIds.Count > 0)
                {
                    var stations = await _context.Stations
                        .Where(s => stationIds.Contains(s.Id))
                        .ToListAsync();

                    foreach (var st in stations)
                    {
                        route.Stations.Add(st);
                    }
                }

                // 3. Phân công các Phương tiện (1-N)
                if (vehicleIds.Count > 0)
                {
                    var vehicles = await _context.Vehicles
                        .Where(v => vehicleIds.Contains(v.Id))
                        .ToListAsync();

                    foreach (var v in vehicles)
                    {
                        v.RouteId = route.Id;
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return (true, "Thêm mới tuyến xe và liên kết dữ liệu thành công!", route);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Lỗi khi thêm tuyến xe (Đã rollback an toàn): {ex.Message}", null);
            }
        }

        /// <summary>
        /// Cập nhật tuyến xe liên kết đa bảng (dùng Transaction)
        /// </summary>
        public async Task<(bool Success, string Message)> UpdateRouteAsync(
            Route updatedRoute,
            List<int> stationIds,
            List<int> vehicleIds)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingRoute = await _context.Routes
                    .Include(r => r.Stations)
                    .Include(r => r.Vehicles)
                    .FirstOrDefaultAsync(r => r.Id == updatedRoute.Id);

                if (existingRoute == null)
                {
                    return (false, "Không tìm thấy tuyến xe cần cập nhật!");
                }

                // Cập nhật thông tin cơ bản
                existingRoute.RouteCode = updatedRoute.RouteCode;
                existingRoute.RouteName = updatedRoute.RouteName;
                existingRoute.DistanceKm = updatedRoute.DistanceKm;
                existingRoute.EstimatedHours = updatedRoute.EstimatedHours;
                existingRoute.BasePrice = updatedRoute.BasePrice;
                existingRoute.Status = updatedRoute.Status;

                // Cập nhật quan hệ N-N với Station
                existingRoute.Stations.Clear();
                if (stationIds.Count > 0)
                {
                    var stations = await _context.Stations
                        .Where(s => stationIds.Contains(s.Id))
                        .ToListAsync();
                    foreach (var st in stations)
                    {
                        existingRoute.Stations.Add(st);
                    }
                }

                // Cập nhật phân công Phương tiện
                // Gỡ các xe cũ không còn được chọn
                var currentVehicles = await _context.Vehicles
                    .Where(v => v.RouteId == existingRoute.Id)
                    .ToListAsync();

                foreach (var v in currentVehicles)
                {
                    if (!vehicleIds.Contains(v.Id))
                    {
                        v.RouteId = null;
                    }
                }

                // Gán xe mới
                if (vehicleIds.Count > 0)
                {
                    var selectedVehicles = await _context.Vehicles
                        .Where(v => vehicleIds.Contains(v.Id))
                        .ToListAsync();

                    foreach (var v in selectedVehicles)
                    {
                        v.RouteId = existingRoute.Id;
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return (true, "Cập nhật thông tin tuyến xe thành công!");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Lỗi khi cập nhật tuyến xe (Đã rollback an toàn): {ex.Message}");
            }
        }

        /// <summary>
        /// Xóa an toàn tuyến xe: Bắt buộc xác nhận, hỗ trợ Soft-delete hoặc Cascade dùng Transaction rollback khi lỗi
        /// </summary>
        public async Task<(bool Success, string Message)> DeleteRouteAsync(int routeId, bool softDelete = true)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var route = await _context.Routes
                    .Include(r => r.Stations)
                    .Include(r => r.Vehicles)
                    .Include(r => r.Tickets)
                    .FirstOrDefaultAsync(r => r.Id == routeId);

                if (route == null)
                {
                    return (false, "Không tìm thấy tuyến xe cần xóa!");
                }

                // Kiểm tra ràng buộc vé nếu có
                int ticketCount = route.Tickets.Count;
                if (ticketCount > 0 && !softDelete)
                {
                    return (false, $"Không thể xóa vĩnh viễn: Tuyến xe đang có {ticketCount} vé liên quan! Vui lòng chọn Xóa mềm (Soft-delete).");
                }

                if (softDelete)
                {
                    // Xóa mềm: Gán IsDeleted = true, ngắt phân công xe
                    foreach (var v in route.Vehicles)
                    {
                        v.RouteId = null;
                    }

                    _context.Routes.Remove(route); // DbContext sẽ tự chuyển thành IsDeleted = true
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return (true, $"Đã xóa mềm Tuyến xe [{route.RouteCode}] thành công. Dữ liệu lịch sử và vé vẫn được bảo toàn!");
                }
                else
                {
                    // Xóa cứng cascade có Transaction
                    foreach (var v in route.Vehicles)
                    {
                        v.RouteId = null;
                    }
                    route.Stations.Clear();

                    _context.Entry(route).State = EntityState.Deleted;
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return (true, $"Đã xóa vĩnh viễn Tuyến xe [{route.RouteCode}] khỏi hệ thống.");
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Lỗi khi xóa tuyến xe (Hệ thống đã Rollback để bảo vệ dữ liệu): {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy lịch sử chỉnh sửa dữ liệu cho Tuyến xe cụ thể (ai đã sửa, sửa khi nào, giá trị cũ/mới)
        /// </summary>
        public async Task<List<AuditLog>> GetRouteAuditHistoryAsync(int routeId)
        {
            string idStr = routeId.ToString();
            return await _context.AuditLogs
                .Where(a => a.EntityName == "Route" && a.RecordId == idStr)
                .OrderByDescending(a => a.Timestamp)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
