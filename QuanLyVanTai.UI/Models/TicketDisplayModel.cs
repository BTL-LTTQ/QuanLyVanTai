namespace QuanLyVanTai.UI.Models
{
    public class TicketDisplayModel
    {
        public int Id { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string RouteName { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public string SeatNumber { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public string DepartureTimeDisplay { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string PriceDisplay { get; set; } = string.Empty;
        public string TicketStatus { get; set; } = string.Empty;
        public string TicketStatusDisplay { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string CreatedAtDisplay { get; set; } = string.Empty;
    }
}
