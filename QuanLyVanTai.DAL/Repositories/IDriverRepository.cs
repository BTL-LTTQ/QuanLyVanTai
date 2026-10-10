using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.DAL.Repositories
{
    public interface IDriverRepository : IRepository<Driver>
    {
        Task<Driver?> GetByDriverCodeAsync(string code);
        Task<List<Driver>> GetActiveDriversAsync();
        Task<bool> IsDriverCodeExistsAsync(string code, int? excludeId = null);
        Task<bool> IsLicenseNumberExistsAsync(string licenseNumber, int? excludeId = null);
    }
}
