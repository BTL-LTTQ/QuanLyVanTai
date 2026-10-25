using System.Data;
using Microsoft.Data.SqlClient;
using Core.DTOs;

namespace QuanLyVanTai.DAL.Repositories
{
    /// <summary>
    /// Triển khai báo cáo thống kê bằng <b>ADO.NET thuần</b>:
    /// <list type="bullet">
    ///   <item><c>SqlConnection</c> – quản lý kết nối</item>
    ///   <item><c>SqlCommand</c>    – tạo và thực thi lệnh SQL</item>
    ///   <item><c>SqlDataReader</c> – đọc kết quả dạng stream (forward-only)</item>
    ///   <item><c>SqlDataAdapter</c> + <c>DataTable</c> – đọc kết quả vào bộ nhớ</item>
    ///   <item>Tham số hóa hoàn toàn để chống SQL Injection</item>
    /// </list>
    /// Đây là cách tiếp cận "thấp tầng" để đối chiếu với EF Core theo Chương 7 đề cương.
    /// </summary>
    public sealed class ReportAdoRepository : IReportAdoRepository
    {
        private readonly string _connectionString;

        public ReportAdoRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string không được để trống.", nameof(connectionString));

            _connectionString = connectionString;
        }

        // =====================================================================
        // 1. Thống kê doanh thu theo tháng — SqlCommand + SqlDataReader
        // =====================================================================

        /// <inheritdoc/>
        public async Task<List<MonthlyRevenueDto>> GetMonthlyRevenueAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken ct = default)
        {
            // Câu lệnh SQL tham số hóa (@FromDate, @ToDate) — không nối chuỗi trực tiếp
            const string sql = """
                SELECT
                    YEAR(DepartureTime)           AS [Year],
                    MONTH(DepartureTime)          AS [Month],
                    SUM(Price)                    AS [Revenue],
                    COUNT(*)                      AS [TicketCount]
                FROM Tickets
                WHERE IsDeleted  = 0
                  AND TicketStatus <> 'Cancelled'
                  AND PaymentStatus = 'Paid'
                  AND DepartureTime >= @FromDate
                  AND DepartureTime <  @ToDate
                GROUP BY YEAR(DepartureTime), MONTH(DepartureTime)
                ORDER BY [Year], [Month]
                """;

            var result = new List<MonthlyRevenueDto>();

            // using — đảm bảo đóng kết nối dù có exception
            await using var conn = new SqlConnection(_connectionString);
            await using var cmd  = new SqlCommand(sql, conn);

            // Thêm tham số an toàn — không bao giờ nối chuỗi người dùng vào SQL
            cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate.Date;
            cmd.Parameters.Add("@ToDate",   SqlDbType.DateTime).Value = toDate.Date.AddDays(1);

            await conn.OpenAsync(ct);

            // SqlDataReader — đọc từng dòng theo dạng stream, tiết kiệm bộ nhớ
            await using var reader = await cmd.ExecuteReaderAsync(
                CommandBehavior.CloseConnection, ct);

            while (await reader.ReadAsync(ct))
            {
                // Đọc cột theo tên — rõ ràng hơn đọc theo index
                result.Add(new MonthlyRevenueDto(
                    Year:        reader.GetInt32(reader.GetOrdinal("Year")),
                    Month:       reader.GetInt32(reader.GetOrdinal("Month")),
                    Revenue:     reader.GetDecimal(reader.GetOrdinal("Revenue")),
                    TicketCount: reader.GetInt32(reader.GetOrdinal("TicketCount"))
                ));
            }

            return result;
        }

        // =====================================================================
        // 2. Thống kê doanh thu theo tuyến — SqlDataAdapter + DataTable
        // =====================================================================

        /// <inheritdoc/>
        public async Task<List<RouteRevenueAdoDto>> GetRevenueByRouteAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken ct = default)
        {
            const string sql = """
                SELECT
                    r.RouteCode                   AS [RouteCode],
                    r.RouteName                   AS [RouteName],
                    SUM(t.Price)                  AS [Revenue],
                    COUNT(t.Id)                   AS [TicketCount]
                FROM Tickets  t
                JOIN Routes   r ON r.Id = t.RouteId
                WHERE t.IsDeleted  = 0
                  AND t.TicketStatus <> 'Cancelled'
                  AND t.PaymentStatus = 'Paid'
                  AND r.IsDeleted = 0
                  AND t.DepartureTime >= @FromDate
                  AND t.DepartureTime <  @ToDate
                GROUP BY r.RouteCode, r.RouteName
                ORDER BY [Revenue] DESC
                """;

            // SqlDataAdapter nạp toàn bộ kết quả vào DataTable trong bộ nhớ
            // — hữu ích khi cần binding vào DataGridView trực tiếp
            var table = new DataTable();

            // Task.Run vì SqlDataAdapter.Fill() không có async API
            await Task.Run(() =>
            {
                using var conn    = new SqlConnection(_connectionString);
                using var cmd     = new SqlCommand(sql, conn);
                using var adapter = new SqlDataAdapter(cmd);

                cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate.Date;
                cmd.Parameters.Add("@ToDate",   SqlDbType.DateTime).Value = toDate.Date.AddDays(1);

                adapter.Fill(table); // Fill tự mở + đóng kết nối
            }, ct);

            // Chuyển DataTable → List<DTO>
            var result = new List<RouteRevenueAdoDto>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
            {
                result.Add(new RouteRevenueAdoDto(
                    RouteCode:   row["RouteCode"].ToString() ?? string.Empty,
                    RouteName:   row["RouteName"].ToString() ?? string.Empty,
                    Revenue:     Convert.ToDecimal(row["Revenue"]),
                    TicketCount: Convert.ToInt32(row["TicketCount"])
                ));
            }

            return result;
        }

        // =====================================================================
        // 3. Tổng doanh thu — ExecuteScalarAsync (trả về 1 giá trị duy nhất)
        // =====================================================================

        /// <inheritdoc/>
        public async Task<decimal> GetTotalRevenueAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken ct = default)
        {
            const string sql = """
                SELECT ISNULL(SUM(Price), 0)
                FROM Tickets
                WHERE IsDeleted     = 0
                  AND TicketStatus <> 'Cancelled'
                  AND PaymentStatus = 'Paid'
                  AND DepartureTime >= @FromDate
                  AND DepartureTime <  @ToDate
                """;

            await using var conn = new SqlConnection(_connectionString);
            await using var cmd  = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate.Date;
            cmd.Parameters.Add("@ToDate",   SqlDbType.DateTime).Value = toDate.Date.AddDays(1);

            await conn.OpenAsync(ct);

            // ExecuteScalarAsync — dùng khi kết quả chỉ có 1 ô (SUM, COUNT, ...)
            var scalar = await cmd.ExecuteScalarAsync(ct);
            return Convert.ToDecimal(scalar);
        }
    }
}
