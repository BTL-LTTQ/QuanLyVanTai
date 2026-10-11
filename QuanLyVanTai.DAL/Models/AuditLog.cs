using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyVanTai.DAL.Models
{
    [Table("AuditLogs")]
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Action { get; set; } = string.Empty; // Thêm, Sửa, Xóa, Đăng nhập, Duyệt giao dịch, Xuất file, Truy cập trái phép

        [Required]
        [MaxLength(100)]
        public string EntityName { get; set; } = string.Empty; // Tuyến xe, Trạm dừng, Phương tiện, Nhân sự, Giá vé, Khuyến mãi, Hệ thống

        [MaxLength(50)]
        public string? RecordId { get; set; } // ID bản ghi tác động

        [Column(TypeName = "nvarchar(max)")]
        public string? OldValues { get; set; } // Dữ liệu trước khi sửa/xóa (JSON)

        [Column(TypeName = "nvarchar(max)")]
        public string? NewValues { get; set; } // Dữ liệu sau khi thêm/sửa (JSON)

        [MaxLength(50)]
        public string IpAddress { get; set; } = "127.0.0.1";

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [MaxLength(500)]
        public string? Details { get; set; }
    }
}
