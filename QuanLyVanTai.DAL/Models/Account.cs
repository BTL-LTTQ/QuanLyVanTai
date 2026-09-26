using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyVanTai.DAL.Models
{
    [Table("Accounts")]
    public class Account
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
        public string Role { get; set; } = "Guest";

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Active"; // Active, Locked

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties: 1 Tài khoản nhân viên có thể lập/bán nhiều vé
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
