namespace QuanLyVanTai.BLL.DTOs
{
    // =========================================================================
    // DTO cho Báo cáo doanh thu (Dashboard → PDF)
    // =========================================================================
    // Gom toàn bộ số liệu cần in vào một đối tượng duy nhất để service xuất PDF
    // không phải truy vấn CSDL lần hai — đảm bảo số liệu in ra khớp đúng những
    // gì người dùng đang thấy trên Dashboard tại thời điểm bấm xuất.
    // =========================================================================

    /// <summary>
    /// Dữ liệu đầy đủ của một báo cáo doanh thu theo khoảng thời gian.
    /// </summary>
    public class RevenueReportDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        /// <summary>Tên người dùng thực hiện xuất báo cáo (hiển thị trên header).</summary>
        public string GeneratedBy { get; set; } = string.Empty;

        /// <summary>Thời điểm xuất báo cáo.</summary>
        public DateTime GeneratedAt { get; set; } = DateTime.Now;

        /// <summary>Các chỉ số tổng hợp (doanh thu, số vé, tuyến, xe...).</summary>
        public DashboardSummaryDto Summary { get; set; } = new();

        /// <summary>Doanh thu theo từng ngày trong khoảng lọc.</summary>
        public List<RevenueByDateDto> DailyRevenue { get; set; } = [];

        /// <summary>Doanh thu theo từng tuyến xe trong khoảng lọc.</summary>
        public List<RevenueByRouteDto> RouteRevenue { get; set; } = [];
    }
}
