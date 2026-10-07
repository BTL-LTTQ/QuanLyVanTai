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
        public bool UseAndLogic { get; set; } = true;
    }

    public class VehicleService
    {
        // Mỗi operation tạo context riêng → tránh concurrent DbContext
        private static AppDbContext CreateContext() => new AppDbContext();

        public async Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            using var db = CreateContext();
            return await db.Vehicles
                .Include(v => v.Route)
                .AsNoTracking()
                .OrderByDescending(v => v.Id)
                .ToListAsync();
        }

        public async Task<List<Vehicle>> SearchAndFilterVehiclesAsync(VehicleFilterCriteria criteria)
        {
            using var db = CreateContext();
            var query = db.Vehicles
                .Include(v => v.Route)
                .AsNoTracking()
                .AsQueryable();

            bool hasKeyword = !string.IsNullOrWhiteSpace(criteria.Keyword);
            bool hasType = !string.IsNullOrWhiteSpace(criteria.VehicleType) && criteria.VehicleType != "Tất cả";
            bool hasStatus = !string.IsNullOrWhiteSpace(criteria.Status) && criteria.Status != "Tất cả";
            bool hasMfr = !string.IsNullOrWhiteSpace(criteria.Manufacturer) && criteria.Manufacturer != "Tất cả";

            if (!hasKeyword && !hasType && !hasStatus && !hasMfr)
                return await query.OrderByDescending(v => v.Id).ToListAsync();

            string kw = criteria.Keyword?.Trim().ToLower() ?? "";
            string vt = criteria.VehicleType?.Trim().ToLower() ?? "";
            string st = criteria.Status?.Trim().ToLower() ?? "";
            string mf = criteria.Manufacturer?.Trim().ToLower() ?? "";

            if (criteria.UseAndLogic)
            {
                if (hasKeyword)
                    query = query.Where(v =>
                        v.LicensePlate.ToLower().Contains(kw) ||
                        v.VehicleType.ToLower().Contains(kw) ||
                        (v.Manufacturer != null && v.Manufacturer.ToLower().Contains(kw)) ||
                        (v.Route != null && v.Route.RouteName.ToLower().Contains(kw)));

                if (hasType)
                    query = query.Where(v => v.VehicleType.ToLower() == vt);

                if (hasStatus)
                    query = query.Where(v => v.Status.ToLower() == st);

                if (hasMfr)
                    query = query.Where(v => v.Manufacturer != null && v.Manufacturer.ToLower() == mf);
            }
            else
            {
                query = query.Where(v =>
                    (hasKeyword && (
                        v.LicensePlate.ToLower().Contains(kw) ||
                        v.VehicleType.ToLower().Contains(kw) ||
                        (v.Manufacturer != null && v.Manufacturer.ToLower().Contains(kw)) ||
                        (v.Route != null && v.Route.RouteName.ToLower().Contains(kw)))) ||
                    (hasType && v.VehicleType.ToLower() == vt) ||
                    (hasStatus && v.Status.ToLower() == st) ||
                    (hasMfr && v.Manufacturer != null && v.Manufacturer.ToLower() == mf));
            }

            return await query.OrderByDescending(v => v.Id).ToListAsync();
        }

        public async Task<(bool Success, string Message, Vehicle? Vehicle)> CreateVehicleAsync(Vehicle vehicle)
        {
            using var db = CreateContext();
            bool exists = await db.Vehicles.AnyAsync(v => v.LicensePlate == vehicle.LicensePlate);
            if (exists)
                return (false, $"Biển số xe '{vehicle.LicensePlate}' đã tồn tại!", null);

            db.Vehicles.Add(vehicle);
            await db.SaveChangesAsync();
            return (true, "Thêm phương tiện thành công!", vehicle);
        }

        public async Task<(bool Success, string Message)> UpdateVehicleAsync(Vehicle vehicle)
        {
            using var db = CreateContext();
            var existing = await db.Vehicles.FindAsync(vehicle.Id);
            if (existing == null)
                return (false, "Không tìm thấy phương tiện cần cập nhật!");

            existing.LicensePlate = vehicle.LicensePlate;
            existing.VehicleType = vehicle.VehicleType;
            existing.TotalSeats = vehicle.TotalSeats;
            existing.Manufacturer = vehicle.Manufacturer;
            existing.Status = vehicle.Status;
            existing.RouteId = vehicle.RouteId;

            await db.SaveChangesAsync();
            return (true, "Cập nhật thông tin phương tiện thành công!");
        }

        public async Task<(bool Success, string Message)> DeleteVehicleAsync(int vehicleId, bool softDelete = true)
        {
            using var db = CreateContext();
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                var vehicle = await db.Vehicles
                    .Include(v => v.Tickets)
                    .FirstOrDefaultAsync(v => v.Id == vehicleId);

                if (vehicle == null)
                    return (false, "Không tìm thấy phương tiện cần xóa!");

                if (vehicle.Tickets.Count > 0 && !softDelete)
                    return (false, $"Xe đang có {vehicle.Tickets.Count} vé liên quan! Vui lòng chọn Xóa mềm.");

                if (softDelete)
                    db.Vehicles.Remove(vehicle);
                else
                    db.Entry(vehicle).State = EntityState.Deleted;

                await db.SaveChangesAsync();
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
