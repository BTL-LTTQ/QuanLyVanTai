using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyVanTai.DAL.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string TicketCode { get; set; } = string.Empty; // Mã vé điện tử (VD: TK20260926001)

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; } = string.Empty; // Tên hành khách

        [Required]
        [MaxLength(15)]
        public string CustomerPhone { get; set; } = string.Empty; // SĐT hành khách

        [Required]
        [MaxLength(10)]
        public string SeatNumber { get; set; } = string.Empty; // Vị trí ghế/giường (VD: A01, B02)

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // Giá vé thực tế bán ra

        [Required]
        public DateTime DepartureTime { get; set; } // Thời gian xe khởi hành

        [Required]
        [MaxLength(30)]
        public string PaymentStatus { get; set; } = "Pending"; // Pending, Paid, Refunded

        [Required]
        [MaxLength(30)]
        public string TicketStatus { get; set; } = "Booked"; // Booked, Completed, Cancelled

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Keys
        [Required]
        public int RouteId { get; set; }

        [Required]
        public int VehicleId { get; set; }

        public int? AccountId { get; set; } // Nhân viên lập vé (có thể null nếu khách đặt online hoặc nhân viên đã xóa)

        // Navigation Properties
        [ForeignKey(nameof(RouteId))]
        public virtual Route? Route { get; set; }

        [ForeignKey(nameof(VehicleId))]
        public virtual Vehicle? Vehicle { get; set; }

        [ForeignKey(nameof(AccountId))]
        public virtual Account? Account { get; set; }
    }
}
