using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces;

namespace QuanLyVanTai.DAL.Models
{
    [Table("Routes")]
    public class Route : ISoftDelete, IAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string RouteCode { get; set; } = string.Empty; // Mã tuyến (VD: HN-DN-01)

        [Required]
        [MaxLength(150)]
        public string RouteName { get; set; } = string.Empty; // Tên lộ trình (VD: Hà Nội - Đà Nẵng)

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal DistanceKm { get; set; } // Quãng đường (km)

        [Required]
        [Column(TypeName = "decimal(4,2)")]
        public decimal EstimatedHours { get; set; } // Thời gian chạy dự kiến (giờ)

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BasePrice { get; set; } // Giá cước cơ bản

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Active"; // Active, Suspended

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

        // Navigation Properties:
        // 1. Quan hệ N-N với Station (1 Tuyến đi qua nhiều Trạm dừng)
        public virtual ICollection<Station> Stations { get; set; } = new List<Station>();

        // 2. Quan hệ 1-N với Ticket (1 Tuyến có nhiều Vé bán ra)
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        // 3. Quan hệ 1-N với Vehicle (Các phương tiện được phân công chạy tuyến này)
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
