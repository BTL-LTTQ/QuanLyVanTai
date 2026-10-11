namespace QuanLyVanTai.BLL.DTOs
{
    // =========================================================================
    // DTOs cho Trạm dừng (Station)
    // =========================================================================

    /// <summary>
    /// DTO danh sách trạm dừng kèm tóm tắt tuyến xe đi qua (join N-N).
    /// </summary>
    public class StationListDto
    {
        public int Id { get; set; }
        public string StationCode { get; set; } = string.Empty;
        public string StationName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        /// <summary>Số tuyến xe đi qua trạm này.</summary>
        public int RouteCount { get; set; }

        /// <summary>Danh sách mã tuyến — dùng để hiển thị tooltip.</summary>
        public string RouteCodes { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }

    /// <summary>
    /// DTO tạo mới / cập nhật trạm dừng.
    /// </summary>
    public class StationFormDto
    {
        public int Id { get; set; } // 0 = thêm mới
        public string StationCode { get; set; } = string.Empty;
        public string StationName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
    }
}
