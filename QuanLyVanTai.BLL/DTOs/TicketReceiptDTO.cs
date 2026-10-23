namespace QuanLyVanTai.BLL.DTOs
{
    /// <summary>
    /// DTO dùng cho in phiếu vé / vé điện tử và xuất PDF nhúng mã QR
    /// </summary>
    public class TicketReceiptDto
    {
        public int TicketId { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string? CustomerEmail { get; set; }
        public string RouteName { get; set; } = string.Empty;
        public string RouteCode { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public string SeatNumber { get; set; } = string.Empty;
        public int Floor { get; set; } = 1;
        public DateTime DepartureTime { get; set; }
        public string DepartureStation { get; set; } = "Bến xe trung tâm";
        public string ArrivalStation { get; set; } = "Bến xe đến";
        public decimal OriginalPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalPrice { get; set; }
        public string PaymentStatus { get; set; } = "Paid";
        public string TicketStatus { get; set; } = "Booked";
        public DateTime BookingTime { get; set; } = DateTime.Now;
        public string StaffName { get; set; } = "Nhân viên bán vé";
        public string QrCodeContent { get; set; } = string.Empty;
    }
}
