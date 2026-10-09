using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Exceptions;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.DAL.Repositories;

namespace QuanLyVanTai.BLL.Services
{
    // =========================================================================
    // StationManagementService — Business Logic cho Trạm dừng
    // =========================================================================

    public class StationManagementService
    {
        // ── Truy vấn ──────────────────────────────────────────────────────────

        /// <summary>
        /// Lấy danh sách trạm dừng kèm tuyến xe đi qua (Include join N-N).
        /// Map sang StationListDto để UI không phụ thuộc vào EF navigation property.
        /// </summary>
        /// <exception cref="DataAccessException">Khi có lỗi database.</exception>
        public async Task<List<StationListDto>> GetStationListAsync(string? keyword = null)
        {
            try
            {
                using var db = new AppDbContext();
                var repo = new StationRepository(db);
                var stations = await repo.GetAllWithRoutesAsync(keyword);

                return stations.Select(s => new StationListDto
                {
                    Id          = s.Id,
                    StationCode = s.StationCode,
                    StationName = s.StationName,
                    Address     = s.Address,
                    City        = s.City,
                    Status      = s.Status,
                    RouteCount  = s.Routes.Count,
                    RouteCodes  = string.Join(", ", s.Routes.Select(r => r.RouteCode)),
                    CreatedAt   = s.CreatedAt,
                    CreatedBy   = s.CreatedBy
                }).ToList();
            }
            catch (Exception ex) when (ex is not BusStationException)
            {
                throw new DataAccessException("Station", "GetStationList", ex);
            }
        }

        /// <summary>
        /// Lấy danh sách tỉnh/thành phố duy nhất — dùng populate ComboBox lọc.
        /// </summary>
        /// <exception cref="DataAccessException">Khi có lỗi database.</exception>
        public async Task<List<string>> GetDistinctCitiesAsync()
        {
            try
            {
                using var db = new AppDbContext();
                var repo = new StationRepository(db);
                return await repo.GetDistinctCitiesAsync();
            }
            catch (Exception ex) when (ex is not BusStationException)
            {
                throw new DataAccessException("Station", "GetDistinctCities", ex);
            }
        }

        // ── Thêm mới ──────────────────────────────────────────────────────────

        /// <summary>
        /// Thêm mới trạm dừng sau khi kiểm tra validation và trùng mã.
        /// </summary>
        /// <exception cref="Exceptions.ValidationException">Dữ liệu đầu vào không hợp lệ.</exception>
        /// <exception cref="DuplicateEntityException">Mã trạm đã tồn tại.</exception>
        /// <exception cref="DataAccessException">Lỗi lưu database.</exception>
        public async Task<StationListDto> CreateStationAsync(StationFormDto dto)
        {
            ValidateStationForm(dto);

            try
            {
                using var db = new AppDbContext();
                var repo = new StationRepository(db);

                if (await repo.IsStationCodeExistsAsync(dto.StationCode))
                    throw new DuplicateEntityException("Station", "StationCode", dto.StationCode);

                var station = new Station
                {
                    StationCode = dto.StationCode.Trim().ToUpper(),
                    StationName = dto.StationName.Trim(),
                    Address     = dto.Address.Trim(),
                    City        = dto.City.Trim(),
                    Status      = dto.Status
                };

                await repo.AddAsync(station);
                await repo.SaveChangesAsync();

                // Trả về DTO (chưa có Routes vì mới tạo)
                return new StationListDto
                {
                    Id          = station.Id,
                    StationCode = station.StationCode,
                    StationName = station.StationName,
                    Address     = station.Address,
                    City        = station.City,
                    Status      = station.Status,
                    RouteCount  = 0,
                    RouteCodes  = string.Empty,
                    CreatedAt   = station.CreatedAt,
                    CreatedBy   = station.CreatedBy
                };
            }
            catch (BusStationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Station", "CreateStation", ex);
            }
        }

        // ── Cập nhật ──────────────────────────────────────────────────────────

        /// <summary>
        /// Cập nhật thông tin trạm dừng.
        /// </summary>
        /// <exception cref="Exceptions.ValidationException">Dữ liệu không hợp lệ.</exception>
        /// <exception cref="EntityNotFoundException">Trạm dừng không tồn tại.</exception>
        /// <exception cref="DuplicateEntityException">Mã trạm mới bị trùng.</exception>
        /// <exception cref="DataAccessException">Lỗi database.</exception>
        public async Task UpdateStationAsync(StationFormDto dto)
        {
            if (dto.Id <= 0)
                throw new Exceptions.ValidationException("Station", "Id", "ID trạm dừng không hợp lệ.");

            ValidateStationForm(dto);

            try
            {
                using var db = new AppDbContext();
                var repo = new StationRepository(db);

                var station = await repo.GetByIdAsync(dto.Id)
                    ?? throw new EntityNotFoundException("Station", dto.Id);

                if (await repo.IsStationCodeExistsAsync(dto.StationCode, excludeId: dto.Id))
                    throw new DuplicateEntityException("Station", "StationCode", dto.StationCode);

                station.StationCode = dto.StationCode.Trim().ToUpper();
                station.StationName = dto.StationName.Trim();
                station.Address     = dto.Address.Trim();
                station.City        = dto.City.Trim();
                station.Status      = dto.Status;

                await repo.UpdateAsync(station);
                await repo.SaveChangesAsync();
            }
            catch (BusStationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Station", "UpdateStation", ex);
            }
        }

        // ── Xóa ───────────────────────────────────────────────────────────────

        /// <summary>
        /// Xóa trạm dừng an toàn bằng IDbContextTransaction.
        /// Soft-delete là mặc định và được khuyến nghị.
        /// </summary>
        /// <param name="stationId">ID trạm cần xóa.</param>
        /// <param name="softDelete">true = chỉ đánh dấu IsDeleted, false = xóa vật lý.</param>
        /// <exception cref="EntityNotFoundException">Trạm dừng không tồn tại.</exception>
        /// <exception cref="BusinessRuleViolationException">Vi phạm ràng buộc nghiệp vụ.</exception>
        /// <exception cref="DataAccessException">Lỗi transaction/database.</exception>
        public async Task DeleteStationAsync(int stationId, bool softDelete = true)
        {
            try
            {
                using var db = new AppDbContext();
                var repo = new StationRepository(db);
                await repo.DeleteWithTransactionAsync(stationId, softDelete);
            }
            catch (KeyNotFoundException ex)
            {
                throw new EntityNotFoundException("Station", ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessRuleViolationException("Station", ex.Message);
            }
            catch (BusStationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Station", "DeleteStation", ex);
            }
        }

        // ── Validation nội bộ ─────────────────────────────────────────────────

        private static void ValidateStationForm(StationFormDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.StationCode))
                throw new Exceptions.ValidationException("Station", "StationCode", "Mã trạm không được để trống.");

            if (dto.StationCode.Trim().Length > 20)
                throw new Exceptions.ValidationException("Station", "StationCode", "Mã trạm không vượt quá 20 ký tự.");

            if (string.IsNullOrWhiteSpace(dto.StationName))
                throw new Exceptions.ValidationException("Station", "StationName", "Tên trạm không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.Address))
                throw new Exceptions.ValidationException("Station", "Address", "Địa chỉ không được để trống.");

            if (string.IsNullOrWhiteSpace(dto.City))
                throw new Exceptions.ValidationException("Station", "City", "Tỉnh/Thành phố không được để trống.");

            var validStatuses = new[] { "Active", "Inactive" };
            if (!validStatuses.Contains(dto.Status))
                throw new Exceptions.ValidationException("Station", "Status",
                    $"Trạng thái không hợp lệ. Chỉ chấp nhận: {string.Join(", ", validStatuses)}.");
        }
    }
}
