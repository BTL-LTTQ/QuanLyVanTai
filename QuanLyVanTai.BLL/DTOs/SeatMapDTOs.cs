namespace QuanLyVanTai.BLL.DTOs
{
    /// <summary>
    /// Trạng thái của một chỗ ngồi/giường trên sơ đồ
    /// </summary>
    public enum SeatState
    {
        Available,  // Còn trống (cho phép chọn)
        Selected,   // Đang được khách click chọn
        Booked,     // Đã có khách đặt/mua vé
        Holding     // Đang giữ chỗ chờ thanh toán
    }

    /// <summary>
    /// Các dạng layout phương tiện chuẩn
    /// </summary>
    public enum VehicleLayoutType
    {
        Seater9,       // Ghế ngồi 9 chỗ (Limousine)
        Seater28,      // Ghế ngồi 28 chỗ (sửa lại như ảnh)
        Seater45,      // Ghế ngồi 45 chỗ (4 hàng dọc + 1 ghế cuối)
        Sleeper24,     // Giường nằm 24 chỗ (ổn)
        Sleeper34      // Giường nằm 34 (hàng giữa lùi xuống 1)
    }

    /// <summary>
    /// DTO đại diện cho một chỗ ngồi trên sơ đồ ghế
    /// </summary>
    public class SeatVisualItem
    {
        public string SeatCode { get; set; } = string.Empty; // Mã chỗ: A01, B02, L01, 01, P01
        public int Floor { get; set; } = 1;                  // 1: Tầng 1 / Tầng dưới, 2: Tầng 2 / Tầng trên
        public int Row { get; set; }                         // Chỉ số hàng (1-indexed)
        public int Column { get; set; }                      // Chỉ số cột (1-indexed)
        public SeatState State { get; set; } = SeatState.Available;
        public decimal PriceModifier { get; set; } = 0;      // Phụ phí ghế VIP (nếu có)
        public decimal FinalPrice { get; set; }              // Giá sau khi tính
        public string Description { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO tổng hợp sơ đồ chỗ ngồi của một chuyến/phương tiện
    /// </summary>
    public class VehicleSeatMapDto
    {
        public int VehicleId { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
        public VehicleLayoutType LayoutType { get; set; }
        public int TotalFloors { get; set; } = 1;
        public int RowsPerFloor { get; set; }
        public int ColumnsPerFloor { get; set; }
        public decimal BasePrice { get; set; }
        public List<SeatVisualItem> Seats { get; set; } = new();
    }
}
