using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.DAL.Repositories
{
    /// <summary>
    /// Contract bổ sung nghiệp vụ riêng của Tuyến xe, ngoài CRUD từ IRepository.
    /// </summary>
    public interface IRouteRepository : IRepository<Route>
    {
        /// <summary>
        /// Lấy danh sách tuyến xe kèm toàn bộ trạm dừng và phương tiện phân công.
        /// Áp dụng Include (eager loading) để tránh N+1 queries.
        /// </summary>
        Task<List<Route>> GetAllWithStationsAndVehiclesAsync(string? keyword = null);

        /// <summary>
        /// Lấy chi tiết 1 tuyến xe cùng danh sách trạm dừng và phương tiện.
        /// </summary>
        Task<Route?> GetByIdWithDetailsAsync(int id);

        /// <summary>
        /// Lấy tuyến xe theo mã tuyến (RouteCode là UNIQUE).
        /// </summary>
        Task<Route?> GetByRouteCodeAsync(string routeCode);

        /// <summary>
        /// Kiểm tra mã tuyến đã tồn tại chưa (dùng khi thêm/sửa).
        /// </summary>
        Task<bool> IsRouteCodeExistsAsync(string routeCode, int? excludeId = null);

        /// <summary>
        /// Xóa an toàn bằng transaction: soft-delete tuyến, giải phóng Vehicle.RouteId,
        /// rollback tự động nếu có lỗi phát sinh.
        /// </summary>
        Task DeleteWithTransactionAsync(int routeId, bool softDelete = true);
    }
}
