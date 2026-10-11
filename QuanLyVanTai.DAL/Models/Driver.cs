using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces;

namespace QuanLyVanTai.DAL.Models
{
    /// <summary>
    /// Tài xế — entity riêng, tách khỏi Account để quản lý thông tin nghề nghiệp
    /// (GPLX, ngày hết hạn, trạng thái hoạt động) độc lập với tài khoản hệ thống.
    /// </summary>
    [Table("Drivers")]
    public class Driver : ISoftDelete, IAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>Mã tài xế nội bộ — UNIQUE (VD: TX-001).</summary>
        [Required]
        [MaxLength(20)]
        public string DriverCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(15)]
        public string? PhoneNumber { get; set; }

        /// <summary>Số giấy phép lái xe — UNIQUE.</summary>
        [Required]
        [MaxLength(20)]
        public string LicenseNumber { get; set; } = string.Empty;

        /// <summary>Hạng GPLX (B2, C, D, E, F...).</summary>
        [Required]
        [MaxLength(5)]
        public string LicenseClass { get; set; } = string.Empty;

        /// <summary>Ngày hết hạn GPLX — dùng để cảnh báo khi validate.</summary>
        public DateTime LicenseExpiryDate { get; set; }

        /// <summary>Active = đang làm việc | OnLeave = nghỉ phép | Inactive = nghỉ việc.</summary>
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Active";

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

        // Navigation: 1 tài xế có nhiều lần phân công
        public virtual ICollection<DriverAssignment> Assignments { get; set; } = new List<DriverAssignment>();
    }
}
