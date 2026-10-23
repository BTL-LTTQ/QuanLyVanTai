using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces;

namespace QuanLyVanTai.DAL.Models
{
    [Table("PaymentTransactions")]
    public class PaymentTransaction : IAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TransactionCode { get; set; } = string.Empty; // Mã GD cổng (vnp_TransactionNo / MoMo transId)

        [Required]
        [MaxLength(50)]
        public string OrderCode { get; set; } = string.Empty; // Mã đơn hàng (VD: QLVT_TK20261017001)

        [Required]
        [MaxLength(30)]
        public string Gateway { get; set; } = "VNPay"; // VNPay, MoMo, Cash

        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; } = "VNPay Sandbox"; // VNPay Sandbox, MoMo Sandbox, Tiền mặt

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Pending"; // Pending, Đã thanh toán, Thất bại, Refunded

        [MaxLength(255)]
        public string? OrderInfo { get; set; }

        [MaxLength(20)]
        public string? ResponseCode { get; set; } // "00" (VNPay), "0" (MoMo)

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        public string? RawCallbackData { get; set; } // Lưu vết toàn bộ query callback phục vụ đối soát

        // Foreign key to Ticket
        public int? TicketId { get; set; }

        [ForeignKey(nameof(TicketId))]
        public virtual Ticket? Ticket { get; set; }

        // IAuditable Tracking
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(50)]
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(50)]
        public string? UpdatedBy { get; set; }
    }
}
