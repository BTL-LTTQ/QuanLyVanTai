using Core.DTOs;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// Service báo cáo thống kê — sử dụng ADO.NET thuần ở tầng DAL.
    /// Giao diện này giữ cho UI không biết chi tiết triển khai (SqlCommand hay EF Core).
    /// </summary>
    public interface IReportAdoService
    {
        /// <summary>
        /// Lấy danh sách doanh thu theo từng tháng trong khoảng thời gian.
        /// </summary>
        Task<List<MonthlyRevenueDto>> GetMonthlyRevenueAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken ct = default);

        /// <summary>
        /// Lấy danh sách doanh thu theo từng tuyến xe trong khoảng thời gian.
        /// </summary>
        Task<List<RouteRevenueAdoDto>> GetRevenueByRouteAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken ct = default);

        /// <summary>
        /// Tổng doanh thu trong khoảng thời gian.
        /// </summary>
        Task<decimal> GetTotalRevenueAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken ct = default);
    }
}
