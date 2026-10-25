using Core.DTOs;

namespace QuanLyVanTai.DAL.Repositories
{
    /// <summary>
    /// Repository báo cáo sử dụng ADO.NET thuần (SqlCommand / SqlDataReader / SqlDataAdapter)
    /// để đối chiếu với cách tiếp cận EF Core — theo yêu cầu Chương 7 đề cương.
    /// </summary>
    public interface IReportAdoRepository
    {
        /// <summary>
        /// Thống kê doanh thu theo từng tháng trong khoảng thời gian chỉ định.
        /// Dùng <c>SqlCommand</c> + <c>SqlDataReader</c> thuần.
        /// </summary>
        Task<List<MonthlyRevenueDto>> GetMonthlyRevenueAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken ct = default);

        /// <summary>
        /// Thống kê doanh thu theo tuyến xe trong khoảng thời gian chỉ định.
        /// Dùng <c>SqlDataAdapter</c> + <c>DataTable</c> thuần.
        /// </summary>
        Task<List<RouteRevenueAdoDto>> GetRevenueByRouteAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken ct = default);

        /// <summary>
        /// Tổng doanh thu trong khoảng thời gian — dùng <c>ExecuteScalarAsync</c>.
        /// </summary>
        Task<decimal> GetTotalRevenueAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken ct = default);
    }
}
