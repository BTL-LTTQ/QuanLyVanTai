using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Exceptions;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.DAL.Repositories;

namespace QuanLyVanTai.BLL.Services
{
    // =========================================================================
    // RouteManagementService — Business Logic tầng BLL cho Tuyến xe
    // =========================================================================
    // Trách nhiệm:
    //   1. Nhận dữ liệu từ UI (dưới dạng DTO hoặc primitive).
    //   2. Kiểm tra nghiệp vụ (validation, duplicate check, rule check).
    //   3. Gọi Repository tương ứng.
    //   4. Map Entity sang DTO trước khi trả về UI.
    //   5. Bắt mọi exception và throw lại custom exception — UI KHÔNG thấy
    //      DbUpdateException, SqlException hay bất kỳ exception DAL nào.
    //
    // Lưu ý: class này KHÔNG chứa bất kỳ logic hiển thị (MessageBox, DataGridView,
    // Control.Text,...) — đó là trách nhiệm thuần tuý của tầng UI.
    // =========================================================================

    public class RouteManagementService
    {
        // ── Truy vấn ──────────────────────────────────────────────────────────

        /// <summary>
        /// Lấy danh sách tuyến xe kèm trạm dừng và phương tiện (Include join).
        /// Map kết quả sang RouteListDto để UI không phụ thuộc vào EF Entity.
        /// </summary>
        /// <param name="keyword">Từ khoá tìm kiếm theo mã, tên, trạng thái hoặc tên trạm.</param>
        /// <returns>Danh sách RouteListDto đã được sắp xếp theo ID giảm dần.</returns>
        /// <exception cref="DataAccessException">Ném ra khi có lỗi truy vấn database.</exception>
        public async Task<List<RouteListDto>> GetRouteListAsync(string? keyword = null)
        {
            try
            {
                using var db = new AppDbContext();
                var repo = new RouteRepository(db);
                var routes = await repo.GetAllWithStationsAndVehiclesAsync(keyword);

                return routes.Select(r => new RouteListDto
                {
                    Id            = r.Id,
                    RouteCode     = r.RouteCode,
                    RouteName     = r.RouteName,
                    DistanceKm    = r.DistanceKm,
                    EstimatedHours = r.EstimatedHours,
                    BasePrice     = r.BasePrice,
                    Status        = r.Status,
                    StationCount  = r.Stations.Count,
                    StationNames  = string.Join(", ", r.Stations.Select(s => s.StationName)),
                    VehicleCount  = r.Vehicles.Count,
                    CreatedAt     = r.CreatedAt,
                    CreatedBy     = r.CreatedBy
                }).ToList();
            }
            catch (Exception ex) when (ex is not BusStationException)
            {
                throw new DataAccessException("Route", "GetRouteList", ex);
            }
        }

        /// <summary>
        /// Lấy chi tiết tuyến xe theo ID, kèm danh sách trạm dừng và phương tiện.
        /// </summary>
        /// <exception cref="EntityNotFoundException">Nếu không tìm thấy tuyến xe.</exception>
        /// <exception cref="DataAccessException">Nếu có lỗi database.</exception>
        public async Task<RouteDetailDto> GetRouteDetailAsync(int id)
        {
            try
            {
                using var db = new AppDbContext();
                var repo = new RouteRepository(db);
                var route = await repo.GetByIdWithDetailsAsync(id)
                    ?? throw new EntityNotFoundException("Route", id);

                return new RouteDetailDto
                {
                    Id             = route.Id,
                    RouteCode      = route.RouteCode,
                    RouteName      = route.RouteName,
                    DistanceKm     = route.DistanceKm,
                    EstimatedHours = route.EstimatedHours,
                    BasePrice      = route.BasePrice,
                    Status         = route.Status,
                    CreatedAt      = route.CreatedAt,
                    CreatedBy      = route.CreatedBy,
                    UpdatedAt      = route.UpdatedAt,
                    UpdatedBy      = route.UpdatedBy,
                    Stations = route.Stations.Select(s => new StationSummaryDto
                    {
                        Id          = s.Id,
                        StationCode = s.StationCode,
                        StationName = s.StationName,
                        City        = s.City,
                        Status      = s.Status
                    }).ToList(),
                    Vehicles = route.Vehicles.Select(v => new VehicleSummaryDto
                    {
                        Id           = v.Id,
                        LicensePlate = v.LicensePlate,
                        VehicleType  = v.VehicleType,
                        TotalSeats   = v.TotalSeats,
                        Status       = v.Status
                    }).ToList()
                };
            }
            catch (BusStationException)
            {
                throw; // Không bọc lại custom exception của chính mình
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Route", "GetRouteDetail", ex);
            }
        }

        // ── Thêm mới ──────────────────────────────────────────────────────────

        /// <summary>
        /// Thêm mới tuyến xe, liên kết trạm dừng và phân công phương tiện.
        /// Toàn bộ thao tác được bao bọc trong transaction tại repository.
        /// </summary>
        /// <param name="dto">Dữ liệu form nhập từ UI.</param>
        /// <exception cref="Exceptions.ValidationException">Nếu dữ liệu đầu vào không hợp lệ.</exception>
        /// <exception cref="DuplicateEntityException">Nếu mã tuyến đã tồn tại.</exception>
        /// <exception cref="DataAccessException">Nếu có lỗi lưu database.</exception>
        public async Task<RouteDetailDto> CreateRouteAsync(RouteFormDto dto)
        {
            // --- Validation ---
            ValidateRouteForm(dto);

            try
            {
                using var db = new AppDbContext();
                var repo = new RouteRepository(db);

                // Kiểm tra trùng mã tuyến
                if (await repo.IsRouteCodeExistsAsync(dto.RouteCode))
                    throw new DuplicateEntityException("Route", "RouteCode", dto.RouteCode);

                var route = new Route
                {
                    RouteCode      = dto.RouteCode.Trim().ToUpper(),
                    RouteName      = dto.RouteName.Trim(),
                    DistanceKm     = dto.DistanceKm,
                    EstimatedHours = dto.EstimatedHours,
                    BasePrice      = dto.BasePrice,
                    Status         = dto.Status
                };

                // Thêm entity trước để lấy ID
                await repo.AddAsync(route);
                await repo.SaveChangesAsync();

                // Liên kết trạm dừng (N-N)
                if (dto.SelectedStationIds.Count > 0)
                {
                    var stations = await new StationRepository(db)
                        .FindAsync(s => dto.SelectedStationIds.Contains(s.Id));
                    foreach (var station in stations)
                        route.Stations.Add(station);
                }

                // Phân công phương tiện (1-N: cập nhật RouteId)
                if (dto.AssignedVehicleIds.Count > 0)
                {
                    var vehicles = await new VehicleRepository(db)
                        .FindAsync(v => dto.AssignedVehicleIds.Contains(v.Id));
                    foreach (var vehicle in vehicles)
                        vehicle.RouteId = route.Id;
                }

                await repo.SaveChangesAsync();

                // Trả về detail DTO của bản ghi vừa tạo
                return await GetRouteDetailAsync(route.Id);
            }
            catch (BusStationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Route", "CreateRoute", ex);
            }
        }

        // ── Cập nhật ──────────────────────────────────────────────────────────

        /// <summary>
        /// Cập nhật thông tin tuyến xe, đồng bộ lại danh sách trạm và phương tiện.
        /// </summary>
        /// <exception cref="Exceptions.ValidationException">Nếu dữ liệu đầu vào không hợp lệ.</exception>
        /// <exception cref="EntityNotFoundException">Nếu tuyến xe không tồn tại.</exception>
        /// <exception cref="DuplicateEntityException">Nếu mã tuyến mới bị trùng với tuyến khác.</exception>
        /// <exception cref="DataAccessException">Nếu có lỗi lưu database.</exception>
        public async Task UpdateRouteAsync(RouteFormDto dto)
        {
            if (dto.Id <= 0)
                throw new Exceptions.ValidationException("Route", "Id", "ID tuyến xe không hợp lệ.");

            ValidateRouteForm(dto);

            try
            {
                using var db = new AppDbContext();
                var repo = new RouteRepository(db);

                var route = await repo.GetByIdWithDetailsAsync(dto.Id)
                    ?? throw new EntityNotFoundException("Route", dto.Id);

                // Kiểm tra trùng mã tuyến (loại trừ bản ghi hiện tại)
                if (await repo.IsRouteCodeExistsAsync(dto.RouteCode, excludeId: dto.Id))
                    throw new DuplicateEntityException("Route", "RouteCode", dto.RouteCode);

                // Cập nhật scalar fields
                route.RouteCode      = dto.RouteCode.Trim().ToUpper();
                route.RouteName      = dto.RouteName.Trim();
                route.DistanceKm     = dto.DistanceKm;
                route.EstimatedHours = dto.EstimatedHours;
                route.BasePrice      = dto.BasePrice;
                route.Status         = dto.Status;

                // Đồng bộ Stations: xoá cũ, gán mới
                route.Stations.Clear();
                if (dto.SelectedStationIds.Count > 0)
                {
                    var newStations = await new StationRepository(db)
                        .FindAsync(s => dto.SelectedStationIds.Contains(s.Id));
                    foreach (var s in newStations)
                        route.Stations.Add(s);
                }

                // Đồng bộ Vehicles
                var oldVehicles = await new VehicleRepository(db)
                    .FindAsync(v => v.RouteId == route.Id);
                foreach (var v in oldVehicles)
                    if (!dto.AssignedVehicleIds.Contains(v.Id))
                        v.RouteId = null;

                if (dto.AssignedVehicleIds.Count > 0)
                {
                    var newVehicles = await new VehicleRepository(db)
                        .FindAsync(v => dto.AssignedVehicleIds.Contains(v.Id));
                    foreach (var v in newVehicles)
                        v.RouteId = route.Id;
                }

                await repo.SaveChangesAsync();
            }
            catch (BusStationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Route", "UpdateRoute", ex);
            }
        }

        // ── Xóa ───────────────────────────────────────────────────────────────

        /// <summary>
        /// Xóa tuyến xe an toàn bằng IDbContextTransaction.
        /// Mặc định là soft-delete (IsDeleted = true).
        /// Hard-delete chỉ khi không có vé liên quan và được UI xác nhận rõ ràng.
        /// </summary>
        /// <param name="routeId">ID tuyến cần xóa.</param>
        /// <param name="softDelete">true = soft-delete (khuyến nghị), false = xóa vật lý.</param>
        /// <exception cref="EntityNotFoundException">Nếu tuyến xe không tồn tại.</exception>
        /// <exception cref="BusinessRuleViolationException">Nếu vi phạm ràng buộc nghiệp vụ.</exception>
        /// <exception cref="DataAccessException">Nếu có lỗi transaction/database.</exception>
        public async Task DeleteRouteAsync(int routeId, bool softDelete = true)
        {
            try
            {
                using var db = new AppDbContext();
                var repo = new RouteRepository(db);
                await repo.DeleteWithTransactionAsync(routeId, softDelete);
            }
            catch (KeyNotFoundException ex)
            {
                throw new EntityNotFoundException("Route", ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessRuleViolationException("Route", ex.Message);
            }
            catch (BusStationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Route", "DeleteRoute", ex);
            }
        }

        // ── Validation nội bộ ─────────────────────────────────────────────────

        private static void ValidateRouteForm(RouteFormDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.RouteCode))
                throw new Exceptions.ValidationException("Route", "RouteCode", "Mã tuyến không được để trống.");

            if (dto.RouteCode.Trim().Length > 20)
                throw new Exceptions.ValidationException("Route", "RouteCode", "Mã tuyến không được vượt quá 20 ký tự.");

            if (string.IsNullOrWhiteSpace(dto.RouteName))
                throw new Exceptions.ValidationException("Route", "RouteName", "Tên tuyến không được để trống.");

            if (dto.DistanceKm <= 0)
                throw new Exceptions.ValidationException("Route", "DistanceKm", "Quãng đường phải lớn hơn 0 km.");

            if (dto.EstimatedHours <= 0)
                throw new Exceptions.ValidationException("Route", "EstimatedHours", "Thời gian chạy dự kiến phải lớn hơn 0 giờ.");

            if (dto.BasePrice < 0)
                throw new Exceptions.ValidationException("Route", "BasePrice", "Giá cước không được là số âm.");

            var validStatuses = new[] { "Active", "Suspended" };
            if (!validStatuses.Contains(dto.Status))
                throw new Exceptions.ValidationException("Route", "Status", $"Trạng thái không hợp lệ. Chỉ chấp nhận: {string.Join(", ", validStatuses)}.");
        }
    }
}
