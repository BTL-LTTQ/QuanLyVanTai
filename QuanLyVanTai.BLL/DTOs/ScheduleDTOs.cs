namespace QuanLyVanTai.BLL.DTOs
{
    // =========================================================================
    // DTOs cho Lịch trình (Schedule) & Phân công tài xế (DriverAssignment)
    // =========================================================================

    /// <summary>DTO tạo mới / cập nhật một chuyến xe từ form điều phối.</summary>
    public class CreateScheduleDto
    {
        /// <summary>ID tuyến xe sẽ chạy.</summary>
        public int RouteId { get; set; }

        /// <summary>ID phương tiện thực hiện chuyến.</summary>
        public int VehicleId { get; set; }

        /// <summary>Thời điểm khởi hành (UTC).</summary>
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// Thời điểm dự kiến đến (UTC).
        /// Nếu null, BLL sẽ tự tính từ Route.EstimatedHours.
        /// </summary>
        public DateTime? EstimatedArrivalTime { get; set; }

        /// <summary>Danh sách tài xế được phân công kèm vai trò.</summary>
        public List<AssignDriverDto> Drivers { get; set; } = [];

        public string? Notes { get; set; }
    }

    /// <summary>DTO phân công 1 tài xế vào chuyến xe.</summary>
    public class AssignDriverDto
    {
        public int DriverId { get; set; }

        /// <summary>Primary (mặc định) hoặc Secondary.</summary>
        public string Role { get; set; } = "Primary";
        public string? Notes { get; set; }
    }

    /// <summary>DTO hiển thị danh sách chuyến xe (dùng cho DataGridView).</summary>
    public class ScheduleListDto
    {
        public int Id { get; set; }
        public string ScheduleCode { get; set; } = string.Empty;
        public string RouteName { get; set; } = string.Empty;
        public string RouteCode { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public DateTime EstimatedArrivalTime { get; set; }
        public DateTime? ActualArrivalTime { get; set; }
        public string Status { get; set; } = string.Empty;

        /// <summary>Tên tài xế chính (Primary driver).</summary>
        public string PrimaryDriverName { get; set; } = string.Empty;

        /// <summary>Tên tài xế phụ (nếu có).</summary>
        public string? SecondaryDriverName { get; set; }

        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>DTO chi tiết chuyến xe đầy đủ — dùng cho màn hình xem/sửa.</summary>
    public class ScheduleDetailDto
    {
        public int Id { get; set; }
        public string ScheduleCode { get; set; } = string.Empty;

        public int RouteId { get; set; }
        public string RouteCode { get; set; } = string.Empty;
        public string RouteName { get; set; } = string.Empty;
        public decimal DistanceKm { get; set; }

        public int VehicleId { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public int TotalSeats { get; set; }

        public DateTime DepartureTime { get; set; }
        public DateTime EstimatedArrivalTime { get; set; }
        public DateTime? ActualArrivalTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Notes { get; set; }

        public List<DriverAssignmentDto> Assignments { get; set; } = [];

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>DTO thông tin phân công tài xế trong 1 chuyến.</summary>
    public class DriverAssignmentDto
    {
        public int AssignmentId { get; set; }
        public int DriverId { get; set; }
        public string DriverCode { get; set; } = string.Empty;
        public string DriverName { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string AssignmentStatus { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}
