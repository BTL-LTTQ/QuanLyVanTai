using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces;

namespace QuanLyVanTai.DAL.Models
{
    /// <summary>
    /// Lịch trình chuyến xe — một chuyến cụ thể gồm: tuyến, xe, thời gian,
    /// trạng thái vận hành. Mỗi chuyến có thể có nhiều tài xế (lái chính + phụ).
    /// </summary>
    [Table("Schedules")]
    public class Schedule : ISoftDelete, IAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>Mã chuyến xe — UNIQUE (VD: SCH-20261010-001).</summary>
        [Required]
        [MaxLength(30)]
        public string ScheduleCode { get; set; } = string.Empty;

        // ── Quan hệ với Route ──────────────────────────────────────────────────
        [Required]
        public int RouteId { get; set; }

        [ForeignKey(nameof(RouteId))]
        public virtual Route? Route { get; set; }

        // ── Quan hệ với Vehicle ────────────────────────────────────────────────
        [Required]
        public int VehicleId { get; set; }

        [ForeignKey(nameof(VehicleId))]
        public virtual Vehicle? Vehicle { get; set; }

        // ── Thời gian vận hành ─────────────────────────────────────────────────

        /// <summary>Thời điểm xe khởi hành (UTC).</summary>
        [Required]
        public DateTime DepartureTime { get; set; }

        /// <summary>Thời điểm dự kiến đến (UTC).</summary>
        [Required]
        public DateTime EstimatedArrivalTime { get; set; }

        /// <summary>Thời điểm đến thực tế — cập nhật khi chuyến kết thúc.</summary>
        public DateTime? ActualArrivalTime { get; set; }

        // ── Trạng thái vận hành ────────────────────────────────────────────────

        /// <summary>
        /// Scheduled  = đã lên lịch, chờ khởi hành
        /// InProgress = đang chạy
        /// Completed  = hoàn thành
        /// Cancelled  = đã hủy
        /// Delayed    = bị trễ
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Scheduled";

        /// <summary>Ghi chú điều phối (lý do trễ, thay đổi lộ trình...).</summary>
        [MaxLength(500)]
        public string? Notes { get; set; }

        // Tracking / Auditing
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(50)]
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(50)]
        public string? UpdatedBy { get; set; }

        // Soft Delete
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        [MaxLength(50)]
        public string? DeletedBy { get; set; }

        // Navigation: 1 chuyến có thể có nhiều tài xế (lái chính + phụ)
        public virtual ICollection<DriverAssignment> DriverAssignments { get; set; }
            = new List<DriverAssignment>();
    }
}
