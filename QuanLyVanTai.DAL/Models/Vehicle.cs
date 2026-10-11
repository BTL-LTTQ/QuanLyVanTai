using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces;

namespace QuanLyVanTai.DAL.Models
{
    [Table("Vehicles")]
    public class Vehicle : ISoftDelete, IAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string LicensePlate { get; set; } = string.Empty; // Biển kiểm soát (VD: 29B-123.45)

        [Required]
        [MaxLength(50)]
        public string VehicleType { get; set; } = string.Empty; // Giường nằm, Ghế ngồi, Limousine...

        [Required]
        public int TotalSeats { get; set; } // Tổng số ghế/giường

        [MaxLength(50)]
        public string? Manufacturer { get; set; } // Hãng sản xuất (Thaco, Hyundai, Universe...)

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Ready"; // Ready (Sẵn sàng), InTransit (Đang chạy), Maintenance (Bảo dưỡng)

        // Phân công tuyến xe phụ trách (Multi-table relationship Tuyến xe - Phương tiện)
        public int? RouteId { get; set; }
        [ForeignKey(nameof(RouteId))]
        public virtual Route? Route { get; set; }

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

        // Navigation Properties: 1 Phương tiện phục vụ nhiều vé/chuyến xe
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
