using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyVanTai.DAL.Models
{
    [Table("Stations")]
    public class Station
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string StationCode { get; set; } = string.Empty; // Mã trạm (VD: BX-MYDINH)

        [Required]
        [MaxLength(100)]
        public string StationName { get; set; } = string.Empty; // Tên trạm / bến xe

        [Required]
        [MaxLength(255)]
        public string Address { get; set; } = string.Empty; // Địa chỉ trạm

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty; // Tỉnh / Thành phố

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Active"; // Active, Inactive

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties: Quan hệ N-N với Tuyến xe (1 Trạm thuộc nhiều Tuyến)
        public virtual ICollection<Route> Routes { get; set; } = new List<Route>();
    }
}
