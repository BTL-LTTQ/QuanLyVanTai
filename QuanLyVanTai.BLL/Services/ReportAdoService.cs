using Core.DTOs;
using QuanLyVanTai.DAL.Repositories;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// Triển khai <see cref="IReportAdoService"/> — ủy quyền xuống
    /// <see cref="IReportAdoRepository"/> (ADO.NET thuần) và bổ sung
    /// validation đầu vào ở tầng BLL.
    /// </summary>
    public sealed class ReportAdoService : IReportAdoService
    {
        private readonly IReportAdoRepository _repo;

        public ReportAdoService(IReportAdoRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        /// <inheritdoc/>
        public async Task<List<MonthlyRevenueDto>> GetMonthlyRevenueAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken ct = default)
        {
            ValidateDateRange(fromDate, toDate);
            return await _repo.GetMonthlyRevenueAsync(fromDate, toDate, ct);
        }

        /// <inheritdoc/>
        public async Task<List<RouteRevenueAdoDto>> GetRevenueByRouteAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken ct = default)
        {
            ValidateDateRange(fromDate, toDate);
            return await _repo.GetRevenueByRouteAsync(fromDate, toDate, ct);
        }

        /// <inheritdoc/>
        public async Task<decimal> GetTotalRevenueAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken ct = default)
        {
            ValidateDateRange(fromDate, toDate);
            return await _repo.GetTotalRevenueAsync(fromDate, toDate, ct);
        }

        // ─────────────────────────────────────────────
        private static void ValidateDateRange(DateTime from, DateTime to)
        {
            if (from > to)
                throw new ArgumentException(
                    $"Ngày bắt đầu ({from:dd/MM/yyyy}) phải nhỏ hơn hoặc bằng ngày kết thúc ({to:dd/MM/yyyy}).");
        }
    }
}
