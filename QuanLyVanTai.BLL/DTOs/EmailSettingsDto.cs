namespace QuanLyVanTai.BLL.DTOs
{
    /// <summary>
    /// Cấu hình kết nối máy chủ gửi mail SMTP (SmtpClient)
    /// </summary>
    public class SmtpConfigDto
    {
        public string Host { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 587;
        public bool EnableSsl { get; set; } = true;
        public string UserName { get; set; } = "hotro.quanlyvantai@gmail.com";
        public string Password { get; set; } = string.Empty; // Mật khẩu ứng dụng (App Password)
        public string FromEmail { get; set; } = "hotro.quanlyvantai@gmail.com";
        public string FromName { get; set; } = "QLVT Bus Lines - Vé Điện Tử";
        public int TimeoutMs { get; set; } = 15000;
    }

    /// <summary>
    /// Cấu hình gửi mail qua REST API SendGrid
    /// </summary>
    public class SendGridConfigDto
    {
        public string ApiKey { get; set; } = string.Empty;
        public string FromEmail { get; set; } = "hotro@qlvt.vn";
        public string FromName { get; set; } = "QLVT Bus Lines - Vé Điện Tử";
        public string ApiEndpoint { get; set; } = "https://api.sendgrid.com/v3/mail/send";
    }

    /// <summary>
    /// Cấu hình tổng thể dịch vụ Email trong appsettings.json
    /// </summary>
    public class EmailSettingsDto
    {
        /// <summary>
        /// Nhà cung cấp: "Smtp", "SendGrid", "Mock"
        /// </summary>
        public string Provider { get; set; } = "Smtp";

        public SmtpConfigDto Smtp { get; set; } = new();

        public SendGridConfigDto SendGrid { get; set; } = new();

        /// <summary>
        /// Tự động gửi email đính kèm PDF vé điện tử sau khi thanh toán thành công
        /// </summary>
        public bool EnableAutoSendOnPayment { get; set; } = true;

        /// <summary>
        /// Luôn lưu một bản sao email (HTML + PDF đính kèm) ra thư mục cục bộ phục vụ audit / kiểm tra offline
        /// </summary>
        public bool SaveCopyToFile { get; set; } = true;

        /// <summary>
        /// Thư mục lưu bản sao email
        /// </summary>
        public string SentEmailsDirectory { get; set; } = "SentEmails";
    }

    /// <summary>
    /// Tệp đính kèm gửi kèm email (PDF vé điện tử, ảnh...)
    /// </summary>
    public class EmailAttachmentDto
    {
        public string FileName { get; set; } = "VeDienTu.pdf";
        public string ContentType { get; set; } = "application/pdf";
        public byte[] Data { get; set; } = Array.Empty<byte>();

        public EmailAttachmentDto() { }

        public EmailAttachmentDto(string fileName, byte[] data, string contentType = "application/pdf")
        {
            FileName = fileName;
            Data = data;
            ContentType = contentType;
        }
    }

    /// <summary>
    /// Đối tượng thông điệp Email
    /// </summary>
    public class EmailMessageDto
    {
        public string ToEmail { get; set; } = string.Empty;
        public string ToName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string BodyHtml { get; set; } = string.Empty;
        public string? BodyText { get; set; }
        public List<EmailAttachmentDto> Attachments { get; set; } = new();
    }

    /// <summary>
    /// Kết quả thực thi gửi Email
    /// </summary>
    public class EmailSendResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
        public string? MessageId { get; set; }
        public string? SavedCopyPath { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
