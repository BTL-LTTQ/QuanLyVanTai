using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.BLL.Services
{
    public class VehicleFilterCriteria
    {
        public string? Keyword { get; set; }
        public string? VehicleType { get; set; }
        public string? Status { get; set; }
        public string? Manufacturer { get; set; }
        public bool UseAndLogic { get; set; } = true; // true = AND, false = OR
    }

    public class VehicleService
    {
        private readonly AppDbContext _context;

        public VehicleService(AppDbContext context)
        {
            _context = context;
        }

        public VehicleService() : this(new AppDbContext())
        {
        }

        public async Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            return await _context.Vehicles
                .Include(v => v.Route)
                .AsNoTracking()
                .OrderByDescending(v => v.Id)
                .ToListAsync();
        }

        /// <summary>
        /// Bộ lọc đa tiêu chí linh hoạt kết hợp logic AND hoặc OR và tìm kiếm real-time
        /// </summary>
        public async Task<List<Vehicle>> SearchAndFilterVehiclesAsync(VehicleFilterCriteria criteria)
        {
            var query = _context.Vehicles
                .Include(v => v.Route)
                .AsNoTracking()
                .AsQueryable();

            bool hasKeyword = !string.IsNullOrWhiteSpace(criteria.Keyword);
            bool hasType = !string.IsNullOrWhiteSpace(criteria.VehicleType) && criteria.VehicleType != "Tất cả";
            bool hasStatus = !string.IsNullOrWhiteSpace(criteria.Status) && criteria.Status != "Tất cả";
            bool hasManufacturer = !string.IsNullOrWhiteSpace(criteria.Manufacturer) && criteria.Manufacturer != "Tất cả";

            // Nếu không có bất kỳ tiêu chí nào được chọn -> trả về toàn bộ
            if (!hasKeyword && !hasType && !hasStatus && !hasManufacturer)
            {
                return await query.OrderByDescending(v => v.Id).ToListAsync();
            }

            string kw = criteria.Keyword?.Trim().ToLower() ?? string.Empty;
            string vt = criteria.VehicleType?.Trim().ToLower() ?? string.Empty;
            string st = criteria.Status?.Trim().ToLower() ?? string.Empty;
            string mf = criteria.Manufacturer?.Trim().ToLower() ?? string.Empty;

            if (criteria.UseAndLogic)
            {
                // Logic AND: Tất cả các tiêu chí được chọn ĐỀU PHẢI THỎA MÃN
                if (hasKeyword)
                {
                    query = query.Where(v =>
                        v.LicensePlate.ToLower().Contains(kw) ||
                        v.VehicleType.ToLower().Contains(kw) ||
                        (v.Manufacturer != null && v.Manufacturer.ToLower().Contains(kw)) ||
                        (v.Route != null && v.Route.RouteName.ToLower().Contains(kw)));
                }

                if (hasType)
                {
                    query = query.Where(v => v.VehicleType.ToLower() == vt);
                }

                if (hasStatus)
                {
                    query = query.Where(v => v.Status.ToLower() == st);
                }

                if (hasManufacturer)
                {
                    query = query.Where(v => v.Manufacturer != null && v.Manufacturer.ToLower() == mf);
                }
            }
            else
            {
                // Logic OR: Thỏa mãn BẤT KỲ tiêu chí nào trong số các tiêu chí được kích hoạt
                query = query.Where(v =>
                    (hasKeyword && (
                        v.LicensePlate.ToLower().Contains(kw) ||
                        v.VehicleType.ToLower().Contains(kw) ||
                        (v.Manufacturer != null && v.Manufacturer.ToLower().Contains(kw)) ||
                        (v.Route != null && v.Route.RouteName.ToLower().Contains(kw)))) ||
                    (hasType && v.VehicleType.ToLower() == vt) ||
                    (hasStatus && v.Status.ToLower() == st) ||
                    (hasManufacturer && v.Manufacturer != null && v.Manufacturer.ToLower() == mf)
                );
            }

            return await query.OrderByDescending(v => v.Id).ToListAsync();
        }

        public async Task<(bool Success, string Message, Vehicle? Vehicle)> CreateVehicleAsync(Vehicle vehicle)
        {
            bool exists = await _context.Vehicles.AnyAsync(v => v.LicensePlate == vehicle.LicensePlate);
            if (exists)
            {
                return (false, $"Biển số xe '{vehicle.LicensePlate}' đã tồn tại!", null);
            }

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return (true, "Thêm phương tiện thành công!", vehicle);
        }

        public async Task<(bool Success, string Message)> UpdateVehicleAsync(Vehicle vehicle)
        {
            var existing = await _context.Vehicles.FindAsync(vehicle.Id);
            if (existing == null)
            {
                return (false, "Không tìm thấy phương tiện cần cập nhật!");
            }

            existing.LicensePlate = vehicle.LicensePlate;
            existing.VehicleType = vehicle.VehicleType;
            existing.TotalSeats = vehicle.TotalSeats;
            existing.Manufacturer = vehicle.Manufacturer;
            existing.Status = vehicle.Status;
            existing.RouteId = vehicle.RouteId;

            await _context.SaveChangesAsync();
            return (true, "Cập nhật thông tin phương tiện thành công!");
        }

        public async Task<(bool Success, string Message)> DeleteVehicleAsync(int vehicleId, bool softDelete = true)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var vehicle = await _context.Vehicles
                    .Include(v => v.Tickets)
                    .FirstOrDefaultAsync(v => v.Id == vehicleId);

                if (vehicle == null)
                {
                    return (false, "Không tìm thấy phương tiện cần xóa!");
                }

                if (vehicle.Tickets.Count > 0 && !softDelete)
                {
                    return (false, $"Xe đang có {vehicle.Tickets.Count} vé liên quan! Vui lòng chọn Xóa mềm.");
                }

                if (softDelete)
                {
                    _context.Vehicles.Remove(vehicle);
                }
                else
                {
                    _context.Entry(vehicle).State = EntityState.Deleted;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return (true, $"Đã xóa phương tiện [{vehicle.LicensePlate}] thành công!");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Lỗi khi xóa phương tiện: {ex.Message}");
            }
        }
    }
}
