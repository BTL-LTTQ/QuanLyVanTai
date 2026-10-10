using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.DAL.Repositories
{
    /// <summary>
    /// Contract bổ sung nghiệp vụ riêng của Phương tiện.
    /// </summary>
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        /// <summary>
        /// Lấy danh sách phương tiện kèm thông tin tuyến xe được phân công.
        /// </summary>
        Task<List<Vehicle>> GetAllWithRouteAsync();

        /// <summary>
        /// Lấy danh sách phương tiện đang sẵn sàng (Status = "Ready" và chưa gán tuyến).
        /// </summary>
        Task<List<Vehicle>> GetAvailableVehiclesAsync();

        /// <summary>
        /// Lấy danh sách phương tiện đang chạy trên một tuyến xe cụ thể.
        /// </summary>
        Task<List<Vehicle>> GetVehiclesByRouteAsync(int routeId);

        /// <summary>
        /// Kiểm tra biển số đã tồn tại chưa.
        /// </summary>
        Task<bool> IsLicensePlateExistsAsync(string licensePlate, int? excludeId = null);

        /// <summary>
        /// Xóa an toàn bằng transaction với rollback tự động.
        /// Kiểm tra ràng buộc Ticket trước khi hard-delete.
        /// </summary>
        Task DeleteWithTransactionAsync(int vehicleId, bool softDelete = true);
    }
}
