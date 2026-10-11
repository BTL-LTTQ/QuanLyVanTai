using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces;

namespace QuanLyVanTai.DAL.Models
{
    /// <summary>
    /// Phân công tài xế cho chuyến xe — bảng junction giữa Driver và Schedule.
    /// Lưu thêm vai trò (lái chính / phụ) và trạng thái phân công.
    /// </summary>
    [Table("DriverAssignments")]
    public class DriverAssignment : IAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // ── Quan hệ ───────────────────────────────────────────────────────────
        [Required]
        public int ScheduleId { get; set; }

        [ForeignKey(nameof(ScheduleId))]
        public virtual Schedule? Schedule { get; set; }

        [Required]
        public int DriverId { get; set; }

        [ForeignKey(nameof(DriverId))]
        public virtual Driver? Driver { get; set; }

        // ── Thông tin phân công ───────────────────────────────────────────────

        /// <summary>
        /// Primary = lái chính (chịu trách nhiệm chính)
        /// Secondary = lái phụ / thay thế
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "Primary";

        /// <summary>
        /// Assigned  = đã phân công, chờ chạy
        /// Confirmed = tài xế đã xác nhận
        /// Replaced  = đã thay thế bằng tài xế khác
        /// Cancelled = hủy phân công
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string AssignmentStatus { get; set; } = "Assigned";

        [MaxLength(300)]
        public string? Notes { get; set; }

        // Tracking / Auditing (không soft-delete — dùng AssignmentStatus = Cancelled thay thế)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(50)]
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(50)]
        public string? UpdatedBy { get; set; }
    }
}
