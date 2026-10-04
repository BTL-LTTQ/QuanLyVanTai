using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces;

namespace QuanLyVanTai.DAL.Models
{
    [Table("Accounts")]
    public class Account : ISoftDelete, IAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [MaxLength(15)]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [MaxLength(30)]
        public string Role { get; set; } = "Nhân viên bán vé"; // Quản trị viên, Quản lý, Nhân viên bán vé

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Active"; // Active, Locked

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

        // Navigation Properties: 1 Tài khoản nhân viên có thể lập/bán nhiều vé
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
