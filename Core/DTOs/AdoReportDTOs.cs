namespace Core.DTOs
{
    /// <summary>
    /// DTO thống kê doanh thu theo tháng — dùng kiểu <c>record</c> (C# 9+)
    /// để minh họa tính năng C# hiện đại (immutable, value equality, deconstruct).
    /// Đặt trong <b>Core</b> để cả DAL lẫn BLL đều dùng được mà không tạo dependency vòng.
    /// </summary>
    public record MonthlyRevenueDto(
        int     Year,
        int     Month,
        decimal Revenue,
        int     TicketCount)
    {
        /// <summary>Nhãn hiển thị: "Tháng MM/YYYY"</summary>
        public string MonthLabel => $"Tháng {Month:D2}/{Year}";
    }

    /// <summary>
    /// DTO tổng hợp thống kê theo tuyến xe — dùng <c>record</c>.
    /// </summary>
    public record RouteRevenueAdoDto(
        string  RouteCode,
        string  RouteName,
        decimal Revenue,
        int     TicketCount);
}
