using QuanLyVanTai.BLL.DTOs;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// Giao diện dịch vụ gửi email bất đồng bộ (SMTP / SendGrid)
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Gửi thông điệp email tổng quát bất đồng bộ
        /// </summary>
        Task<EmailSendResultDto> SendEmailAsync(EmailMessageDto message, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gửi vé điện tử kèm file PDF đính kèm tự động cho khách hàng sau thanh toán
        /// </summary>
        Task<EmailSendResultDto> SendTicketEmailAsync(
            string toEmail,
            string customerName,
            List<TicketReceiptDto> tickets,
            byte[]? pdfAttachment = null,
            CancellationToken cancellationToken = default);
    }
}
