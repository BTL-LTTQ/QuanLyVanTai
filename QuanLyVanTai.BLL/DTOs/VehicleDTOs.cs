namespace QuanLyVanTai.BLL.DTOs
{
    // =========================================================================
    // DTOs cho Phương tiện (Vehicle)
    // =========================================================================

    /// <summary>
    /// DTO danh sách phương tiện kèm tên tuyến xe phân công (join 1-N).
    /// </summary>
    public class VehicleListDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
        public string? Manufacturer { get; set; }
        public string Status { get; set; } = string.Empty;

        /// <summary>ID tuyến xe phân công (null = chưa gán tuyến).</summary>
        public int? RouteId { get; set; }

        /// <summary>Tên tuyến xe phân công — lấy từ Include(v => v.Route).</summary>
        public string? RouteName { get; set; }

        /// <summary>Mã tuyến xe.</summary>
        public string? RouteCode { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }

    /// <summary>
    /// DTO tạo mới / cập nhật phương tiện.
    /// </summary>
    public class VehicleFormDto
    {
        public int Id { get; set; } // 0 = thêm mới
        public string LicensePlate { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
        public string? Manufacturer { get; set; }
        public string Status { get; set; } = "Ready";

        /// <summary>ID tuyến xe phân công — null nếu chưa gán.</summary>
        public int? RouteId { get; set; }
    }
}
