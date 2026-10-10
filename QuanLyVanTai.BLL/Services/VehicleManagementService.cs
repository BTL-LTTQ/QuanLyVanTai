using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Exceptions;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.DAL.Repositories;

namespace QuanLyVanTai.BLL.Services
{
    // =========================================================================
    // VehicleManagementService — Business Logic cho Phương tiện
    // =========================================================================

    public class VehicleManagementService
    {
        // ── Truy vấn ──────────────────────────────────────────────────────────

        /// <summary>
        /// Lấy danh sách phương tiện kèm tuyến xe phân công (Include join 1-N).
        /// </summary>
        /// <exception cref="DataAccessException">Khi có lỗi database.</exception>
        public async Task<List<VehicleListDto>> GetVehicleListAsync()
        {
            try
            {
                using var db = new AppDbContext();
                var repo = new VehicleRepository(db);
                var vehicles = await repo.GetAllWithRouteAsync();
                return vehicles.Select(MapToListDto).ToList();
            }
            catch (Exception ex) when (ex is not BusStationException)
            {
                throw new DataAccessException("Vehicle", "GetVehicleList", ex);
            }
        }

        /// <summary>
        /// Lấy danh sách phương tiện theo tuyến xe — dùng cho ComboBox chọn xe
        /// khi tạo Ticket hoặc phân công vào tuyến mới.
        /// </summary>
        /// <param name="routeId">null = lấy xe chưa gán tuyến (sẵn sàng), &gt;0 = lấy xe của tuyến đó.</param>
        /// <exception cref="DataAccessException">Khi có lỗi database.</exception>
        public async Task<List<VehicleListDto>> GetVehiclesByRouteAsync(int? routeId)
        {
            try
            {
                using var db = new AppDbContext();
                var repo = new VehicleRepository(db);

                List<Vehicle> vehicles = routeId == null
                    ? await repo.GetAvailableVehiclesAsync()
                    : await repo.GetVehiclesByRouteAsync(routeId.Value);

                return vehicles.Select(MapToListDto).ToList();
            }
            catch (Exception ex) when (ex is not BusStationException)
            {
                throw new DataAccessException("Vehicle", "GetVehiclesByRoute", ex);
            }
        }

        // ── Thêm mới ──────────────────────────────────────────────────────────

        /// <summary>
        /// Thêm mới phương tiện sau khi kiểm tra validation và trùng biển số.
        /// </summary>
        /// <exception cref="Exceptions.ValidationException">Dữ liệu không hợp lệ.</exception>
        /// <exception cref="DuplicateEntityException">Biển số đã tồn tại.</exception>
        /// <exception cref="BusinessRuleViolationException">Tuyến xe phân công không tồn tại.</exception>
        /// <exception cref="DataAccessException">Lỗi lưu database.</exception>
        public async Task<VehicleListDto> CreateVehicleAsync(VehicleFormDto dto)
        {
            ValidateVehicleForm(dto);

            try
            {
                using var db = new AppDbContext();
                var repo = new VehicleRepository(db);

                if (await repo.IsLicensePlateExistsAsync(dto.LicensePlate))
                    throw new DuplicateEntityException("Vehicle", "LicensePlate", dto.LicensePlate);

                // Kiểm tra tuyến xe phân công có tồn tại không
                if (dto.RouteId.HasValue)
                {
                    var routeRepo = new RouteRepository(db);
                    var routeExists = await routeRepo.GetByIdAsync(dto.RouteId.Value);
                    if (routeExists == null)
                        throw new BusinessRuleViolationException("Vehicle",
                            $"Tuyến xe ID = {dto.RouteId} không tồn tại. Không thể phân công.");
                }

                var vehicle = new Vehicle
                {
                    LicensePlate = dto.LicensePlate.Trim().ToUpper(),
                    VehicleType  = dto.VehicleType.Trim(),
                    TotalSeats   = dto.TotalSeats,
                    Manufacturer = dto.Manufacturer?.Trim(),
                    Status       = dto.Status,
                    RouteId      = dto.RouteId
                };

                await repo.AddAsync(vehicle);
                await repo.SaveChangesAsync();

                return MapToListDto(vehicle);
            }
            catch (BusStationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Vehicle", "CreateVehicle", ex);
            }
        }

        // ── Cập nhật ──────────────────────────────────────────────────────────

        /// <summary>
        /// Cập nhật thông tin phương tiện.
        /// Kiểm tra nghiệp vụ: xe đang InTransit không được đổi biển số.
        /// </summary>
        /// <exception cref="Exceptions.ValidationException">Dữ liệu không hợp lệ.</exception>
        /// <exception cref="EntityNotFoundException">Phương tiện không tồn tại.</exception>
        /// <exception cref="DuplicateEntityException">Biển số mới bị trùng.</exception>
        /// <exception cref="BusinessRuleViolationException">Vi phạm quy tắc nghiệp vụ.</exception>
        /// <exception cref="DataAccessException">Lỗi database.</exception>
        public async Task UpdateVehicleAsync(VehicleFormDto dto)
        {
            if (dto.Id <= 0)
                throw new Exceptions.ValidationException("Vehicle", "Id", "ID phương tiện không hợp lệ.");

            ValidateVehicleForm(dto);

            try
            {
                using var db = new AppDbContext();
                var repo = new VehicleRepository(db);

                var vehicle = await repo.GetByIdAsync(dto.Id)
                    ?? throw new EntityNotFoundException("Vehicle", dto.Id);

                // Quy tắc nghiệp vụ: xe đang chạy không được đổi biển số
                string newPlate = dto.LicensePlate.Trim().ToUpper();
                if (vehicle.Status == "InTransit" && vehicle.LicensePlate != newPlate)
                    throw new BusinessRuleViolationException("Vehicle",
                        $"Xe [{vehicle.LicensePlate}] đang trong trạng thái InTransit. Không thể đổi biển số.");

                if (await repo.IsLicensePlateExistsAsync(dto.LicensePlate, excludeId: dto.Id))
                    throw new DuplicateEntityException("Vehicle", "LicensePlate", dto.LicensePlate);

                // Kiểm tra tuyến xe mới có tồn tại không
                if (dto.RouteId.HasValue)
                {
                    var routeRepo = new RouteRepository(db);
                    var routeExists = await routeRepo.GetByIdAsync(dto.RouteId.Value);
                    if (routeExists == null)
                        throw new BusinessRuleViolationException("Vehicle",
                            $"Tuyến xe ID = {dto.RouteId} không tồn tại. Không thể phân công.");
                }

                vehicle.LicensePlate = newPlate;
                vehicle.VehicleType  = dto.VehicleType.Trim();
                vehicle.TotalSeats   = dto.TotalSeats;
                vehicle.Manufacturer = dto.Manufacturer?.Trim();
                vehicle.Status       = dto.Status;
                vehicle.RouteId      = dto.RouteId;

                await repo.UpdateAsync(vehicle);
                await repo.SaveChangesAsync();
            }
            catch (BusStationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Vehicle", "UpdateVehicle", ex);
            }
        }

        // ── Xóa ───────────────────────────────────────────────────────────────

        /// <summary>
        /// Xóa phương tiện an toàn bằng IDbContextTransaction.
        /// Soft-delete là mặc định. Xe đang InTransit không được xóa dưới mọi hình thức.
        /// </summary>
        /// <param name="vehicleId">ID phương tiện cần xóa.</param>
        /// <param name="softDelete">true = soft-delete, false = xóa vật lý (chỉ khi không có Ticket).</param>
        /// <exception cref="EntityNotFoundException">Phương tiện không tồn tại.</exception>
        /// <exception cref="BusinessRuleViolationException">Vi phạm ràng buộc nghiệp vụ.</exception>
        /// <exception cref="DataAccessException">Lỗi transaction/database.</exception>
        public async Task DeleteVehicleAsync(int vehicleId, bool softDelete = true)
        {
            try
            {
                // Dùng một DbContext duy nhất cho toàn bộ operation
                using var db = new AppDbContext();
                var repo = new VehicleRepository(db);

                // Load kèm Tickets để kiểm tra cả rule InTransit lẫn ràng buộc hard-delete
                var vehicle = await db.Vehicles
                    .Include(v => v.Tickets)
                    .FirstOrDefaultAsync(v => v.Id == vehicleId)
                    ?? throw new EntityNotFoundException("Vehicle", vehicleId);

                if (vehicle.Status == "InTransit")
                    throw new BusinessRuleViolationException("Vehicle",
                        $"Xe [{vehicle.LicensePlate}] đang trong trạng thái InTransit. " +
                        "Không thể xóa xe đang hoạt động trên đường.");

                if (!softDelete && vehicle.Tickets.Count > 0)
                    throw new BusinessRuleViolationException("Vehicle",
                        $"Xe [{vehicle.LicensePlate}] đang có {vehicle.Tickets.Count} vé liên quan. " +
                        "Không thể xóa vĩnh viễn, hãy chọn xóa mềm.");

                // Gọi transaction trong cùng db context — tránh load lại lần 2
                await repo.DeleteWithTransactionAsync(vehicleId, softDelete);
            }
            catch (KeyNotFoundException ex)
            {
                throw new EntityNotFoundException("Vehicle", ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessRuleViolationException("Vehicle", ex.Message);
            }
            catch (BusStationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Vehicle", "DeleteVehicle", ex);
            }
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        /// <summary>Map Vehicle entity → VehicleListDto.</summary>
        private static VehicleListDto MapToListDto(Vehicle v) => new()
        {
            Id           = v.Id,
            LicensePlate = v.LicensePlate,
            VehicleType  = v.VehicleType,
            TotalSeats   = v.TotalSeats,
            Manufacturer = v.Manufacturer,
            Status       = v.Status,
            RouteId      = v.RouteId,
            RouteName    = v.Route?.RouteName,
            RouteCode    = v.Route?.RouteCode,
            CreatedAt    = v.CreatedAt,
            CreatedBy    = v.CreatedBy
        };

        private static void ValidateVehicleForm(VehicleFormDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.LicensePlate))
                throw new Exceptions.ValidationException("Vehicle", "LicensePlate", "Biển số xe không được để trống.");

            if (dto.LicensePlate.Trim().Length > 20)
                throw new Exceptions.ValidationException("Vehicle", "LicensePlate", "Biển số xe không vượt quá 20 ký tự.");

            if (string.IsNullOrWhiteSpace(dto.VehicleType))
                throw new Exceptions.ValidationException("Vehicle", "VehicleType", "Loại xe không được để trống.");

            if (dto.TotalSeats <= 0 || dto.TotalSeats > 100)
                throw new Exceptions.ValidationException("Vehicle", "TotalSeats", "Số ghế/giường phải từ 1 đến 100.");

            var validStatuses = new[] { "Ready", "InTransit", "Maintenance" };
            if (!validStatuses.Contains(dto.Status))
                throw new Exceptions.ValidationException("Vehicle", "Status",
                    $"Trạng thái không hợp lệ. Chỉ chấp nhận: {string.Join(", ", validStatuses)}.");
        }
    }
}
