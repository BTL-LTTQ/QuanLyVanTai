using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.DAL.Repositories
{
    public class DriverRepository : BaseRepository<Driver>, IDriverRepository
    {
        public DriverRepository(AppDbContext db) : base(db) { }

        public async Task<Driver?> GetByDriverCodeAsync(string code)
            => await _db.Drivers
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DriverCode == code);

        public async Task<List<Driver>> GetActiveDriversAsync()
            => await _db.Drivers
                .AsNoTracking()
                .Where(d => d.Status == "Active")
                .OrderBy(d => d.FullName)
                .ToListAsync();

        public async Task<bool> IsDriverCodeExistsAsync(string code, int? excludeId = null)
            => await _db.Drivers.AnyAsync(d =>
                d.DriverCode == code &&
                (excludeId == null || d.Id != excludeId.Value));

        public async Task<bool> IsLicenseNumberExistsAsync(string licenseNumber, int? excludeId = null)
            => await _db.Drivers.AnyAsync(d =>
                d.LicenseNumber == licenseNumber &&
                (excludeId == null || d.Id != excludeId.Value));
    }
}
