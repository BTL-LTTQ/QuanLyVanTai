using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyVanTai.DAL.Models
{
    [Table("RolePermissions")]
    public class RolePermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; } = string.Empty; // Quản trị viên, Quản lý, Nhân viên bán vé

        [Required]
        [MaxLength(50)]
        public string MenuCode { get; set; } = string.Empty; // TuyenXe, TramDung, PhuongTien, NhanSu, GiaVe, KhuyenMai, PhanQuyen, NhatKyHoatDong

        [Required]
        [MaxLength(100)]
        public string MenuName { get; set; } = string.Empty;

        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanExport { get; set; } = true;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
