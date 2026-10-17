using System;

namespace QuanLyVanTai.BLL.DTOs
{
    public enum CheckInStatus
    {
        Success,
        AlreadyBoarded,
        Cancelled,
        Unpaid,
        NotFound,
        InvalidQr
    }

    /// <summary>
    /// Kết quả xử lý Check-in hành khách bằng mã QR
    /// </summary>
    public class CheckInResultDto
    {
        public bool Success { get; set; }
        public CheckInStatus Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public int TicketId { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string RouteName { get; set; } = string.Empty;
        public string VehiclePlate { get; set; } = string.Empty;
        public string SeatNumber { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime CheckInTime { get; set; } = DateTime.Now;
        public string CheckedInBy { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO hiển thị danh sách hành khách đã check-in lên xe trong ca làm việc
    /// </summary>
    public class CheckedInPassengerDto
    {
        public int TicketId { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string SeatNumber { get; set; } = string.Empty;
        public string RouteName { get; set; } = string.Empty;
        public string VehiclePlate { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime CheckInTime { get; set; }
        public string CheckedInBy { get; set; } = string.Empty;
    }
}
