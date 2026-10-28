using System;

namespace QuanLyVanTai.BLL.DTOs
{
    /// <summary>
    /// DTO tóm tắt các chỉ số hiệu suất chính (KPIs) hiển thị trên Stat Cards
    /// </summary>
    public class DashboardSummaryDto
    {
        public decimal TotalRevenue { get; set; }
        public int TotalTickets { get; set; }
        public int ActiveRoutes { get; set; }
        public int ActiveVehicles { get; set; }
        public decimal TodayRevenue { get; set; }
        public int TodayTickets { get; set; }
        public decimal AverageTicketPrice => TotalTickets > 0 ? TotalRevenue / TotalTickets : 0;
    }

    /// <summary>
    /// DTO thống kê doanh thu và sản lượng vé theo thời gian (ngày)
    /// </summary>
    public class RevenueByDateDto
    {
        public DateTime Date { get; set; }
        public string DateLabel { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
        public int TicketCount { get; set; }
    }

    /// <summary>
    /// DTO phân tích cơ cấu doanh thu theo từng tuyến xe
    /// </summary>
    public class RevenueByRouteDto
    {
        public int RouteId { get; set; }
        public string RouteCode { get; set; } = string.Empty;
        public string RouteName { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
        public int TicketCount { get; set; }
        public double Percentage { get; set; }
    }

    /// <summary>
    /// DTO thông tin giao dịch vé gần đây để hiển thị bảng dữ liệu minh chứng
    /// </summary>
    public class RecentTransactionDto
    {
        public int Id { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string RouteName { get; set; } = string.Empty;
        public string VehiclePlate { get; set; } = string.Empty;
        public string SeatNumber { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime DepartureTime { get; set; }
        public string PaymentStatus { get; set; } = "Paid";
        public string TicketStatus { get; set; } = "Booked";
    }
}
