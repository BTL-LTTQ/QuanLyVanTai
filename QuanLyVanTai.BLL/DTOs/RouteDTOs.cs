namespace QuanLyVanTai.BLL.DTOs
{
    // =========================================================================
    // DTOs cho Tuyến xe (Route)
    // =========================================================================
    // DTOs tách biệt dữ liệu trả về BLL khỏi Entity EF, tránh vô tình
    // serialize navigation properties gây circular reference ở tầng UI.
    // =========================================================================

    /// <summary>
    /// DTO dùng để hiển thị danh sách tuyến xe trong DataGridView,
    /// kết hợp thông tin từ Route và các Stations liên kết (N-N).
    /// </summary>
    public class RouteListDto
    {
        public int Id { get; set; }
        public string RouteCode { get; set; } = string.Empty;
        public string RouteName { get; set; } = string.Empty;
        public decimal DistanceKm { get; set; }
        public decimal EstimatedHours { get; set; }
        public decimal BasePrice { get; set; }
        public string Status { get; set; } = string.Empty;

        /// <summary>Số lượng trạm dừng trên tuyến.</summary>
        public int StationCount { get; set; }

        /// <summary>Danh sách tên trạm dừng (đã join), dùng để hiển thị tooltip.</summary>
        public string StationNames { get; set; } = string.Empty;

        /// <summary>Số phương tiện đang phân công chạy tuyến này.</summary>
        public int VehicleCount { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }

    /// <summary>
    /// DTO chi tiết tuyến xe — dùng cho màn hình xem/sửa.
    /// Chứa danh sách Stations đầy đủ để hiển thị trong ListBox/CheckedListBox.
    /// </summary>
    public class RouteDetailDto
    {
        public int Id { get; set; }
        public string RouteCode { get; set; } = string.Empty;
        public string RouteName { get; set; } = string.Empty;
        public decimal DistanceKm { get; set; }
        public decimal EstimatedHours { get; set; }
        public decimal BasePrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public List<StationSummaryDto> Stations { get; set; } = [];
        public List<VehicleSummaryDto> Vehicles { get; set; } = [];
    }

    /// <summary>
    /// DTO tạo mới / cập nhật tuyến xe từ form nhập liệu.
    /// </summary>
    public class RouteFormDto
    {
        public int Id { get; set; } // 0 = thêm mới, > 0 = cập nhật
        public string RouteCode { get; set; } = string.Empty;
        public string RouteName { get; set; } = string.Empty;
        public decimal DistanceKm { get; set; }
        public decimal EstimatedHours { get; set; }
        public decimal BasePrice { get; set; }
        public string Status { get; set; } = "Active";

        /// <summary>Danh sách ID các trạm dừng được chọn từ CheckedListBox.</summary>
        public List<int> SelectedStationIds { get; set; } = [];

        /// <summary>Danh sách ID phương tiện được phân công chạy tuyến này.</summary>
        public List<int> AssignedVehicleIds { get; set; } = [];
    }

    // ── Sub-DTOs ──────────────────────────────────────────────────────────────

    /// <summary>DTO tóm tắt trạm dừng — dùng trong RouteDetailDto.</summary>
    public class StationSummaryDto
    {
        public int Id { get; set; }
        public string StationCode { get; set; } = string.Empty;
        public string StationName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }

    /// <summary>DTO tóm tắt phương tiện — dùng trong RouteDetailDto.</summary>
    public class VehicleSummaryDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
