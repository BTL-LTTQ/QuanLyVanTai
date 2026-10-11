using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.DAL.Repositories
{
    /// <summary>
    /// Contract bổ sung nghiệp vụ riêng của Trạm dừng.
    /// </summary>
    public interface IStationRepository : IRepository<Station>
    {
        /// <summary>
        /// Lấy danh sách trạm dừng kèm các tuyến xe đi qua (Join N-N).
        /// </summary>
        Task<List<Station>> GetAllWithRoutesAsync(string? keyword = null);

        /// <summary>
        /// Lấy chi tiết 1 trạm dừng cùng danh sách tuyến xe liên kết.
        /// </summary>
        Task<Station?> GetByIdWithRoutesAsync(int id);

        /// <summary>
        /// Lấy trạm theo mã trạm (StationCode là UNIQUE).
        /// </summary>
        Task<Station?> GetByStationCodeAsync(string stationCode);

        /// <summary>
        /// Kiểm tra mã trạm đã tồn tại chưa.
        /// </summary>
        Task<bool> IsStationCodeExistsAsync(string stationCode, int? excludeId = null);

        /// <summary>
        /// Lấy danh sách tất cả tỉnh/thành phố có trong dữ liệu (dùng cho dropdown lọc).
        /// </summary>
        Task<List<string>> GetDistinctCitiesAsync();

        /// <summary>
        /// Xóa an toàn bằng transaction với rollback tự động.
        /// Soft-delete: chỉ đánh dấu IsDeleted = true, giữ nguyên quan hệ RouteStations.
        /// </summary>
        Task DeleteWithTransactionAsync(int stationId, bool softDelete = true);
    }
}
