using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyVanTai.DAL.Models
{
    [Table("Vehicles")]
    public class Vehicle
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
        public string Status { get; set; } = "Ready"; // Ready, InTransit, Maintenance

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties: 1 Phương tiện phục vụ nhiều vé/chuyến xe
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
