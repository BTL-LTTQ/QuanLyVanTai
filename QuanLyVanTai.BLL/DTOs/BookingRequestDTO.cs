namespace QuanLyVanTai.BLL.DTOs
{
    /// <summary>
    /// DTO gửi yêu cầu đặt vé từ giao diện bán vé / khách hàng
    /// </summary>
    public class BookingRequestDto
    {
        public int ScheduleId { get; set; }
        public int RouteId { get; set; }
        public int VehicleId { get; set; }
        public DateTime DepartureTime { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string? CustomerNotes { get; set; }
        public List<string> SelectedSeats { get; set; } = new();
        public string? PromotionCode { get; set; }
        public int? AccountId { get; set; }
        public string PaymentMethod { get; set; } = "Tiền mặt"; // Tiền mặt, Chuyển khoản QR
    }

    /// <summary>
    /// Kết quả xác thực mã khuyến mãi
    /// </summary>
    public class PromotionVerifyResultDto
    {
        public bool IsValid { get; set; }
        public string PromotionCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalTotal { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// Kết quả đặt vé trả về cho UI
    /// </summary>
    public class BookingResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<int> CreatedTicketIds { get; set; } = new();
        public List<string> CreatedTicketCodes { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }
    }

    /// <summary>
    /// Tiêu chuẩn tìm kiếm chuyến xe đa tiêu chí cho Khách hàng
    /// </summary>
    public class TripSearchCriteriaDto
    {
        public string? Keyword { get; set; }
        public DateTime? DepartureDate { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? RouteId { get; set; }
    }

    /// <summary>
    /// DTO chuyến xe khả dụng hiển thị trên DataGridView tìm kiếm chuyến xe
    /// </summary>
    public class AvailableTripDto
    {
        public int ScheduleId { get; set; }
        public string ScheduleCode { get; set; } = string.Empty;
        public int RouteId { get; set; }
        public string RouteCode { get; set; } = string.Empty;
        public string RouteName { get; set; } = string.Empty;
        public int VehicleId { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
        public int BookedSeats { get; set; }
        public int AvailableSeats { get; set; }
        public decimal BasePrice { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime EstimatedArrivalTime { get; set; }
        public string DisplayText => $"{DepartureTime:HH:mm} | {RouteName} | {LicensePlate} ({AvailableSeats}/{TotalSeats} chỗ trống) - {BasePrice:N0} đ";
    }
}

