using System.Net;
using System.Net.Mail;
using System.Text;
using QuanLyVanTai.BLL.DTOs;
using Serilog;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// Dịch vụ gửi email bất đồng bộ qua giao thức SMTP (SmtpClient)
    /// Hỗ trợ Gmail, Outlook, máy chủ SMTP doanh nghiệp hoặc Local SMTP Server
    /// </summary>
    public class SmtpEmailService : IEmailService
    {
        private readonly SmtpConfigDto _config;
        private readonly EmailSettingsDto _settings;

        public SmtpEmailService(SmtpConfigDto? config = null, EmailSettingsDto? settings = null)
        {
            _config = config ?? new SmtpConfigDto();
            _settings = settings ?? new EmailSettingsDto { Smtp = _config };
        }

        public async Task<EmailSendResultDto> SendEmailAsync(EmailMessageDto message, CancellationToken cancellationToken = default)
        {
            var result = new EmailSendResultDto
            {
                SentAt = DateTime.UtcNow
            };

            if (string.IsNullOrWhiteSpace(message.ToEmail))
            {
                result.Success = false;
                result.ErrorMessage = "Địa chỉ email người nhận không được để trống.";
                return result;
            }

            // Kiểm tra xem cấu hình có phải là placeholder / chưa cài đặt App Password thực tế hay không
            bool isPlaceholder = string.IsNullOrWhiteSpace(_config.Password) ||
                                 _config.Password.Contains("YOUR_APP_PASSWORD_HERE", StringComparison.OrdinalIgnoreCase) ||
                                 string.IsNullOrWhiteSpace(_config.Host);

            if (isPlaceholder)
            {
                string demoMsg = "[CHẾ ĐỘ MÔ PHỎNG] Chưa cấu hình Mật khẩu ứng dụng Gmail (App Password) trong appsettings.json.";
                Log.Warning("[SmtpEmailService] {Message}. Hệ thống sẽ xuất bản sao email vé ra thư mục để kiểm tra.", demoMsg);

                if (_settings.SaveCopyToFile)
                {
                    string savedPath = SaveEmailCopyToFile(message);
                    result.SavedCopyPath = savedPath;
                    result.Success = true;
                    result.Message = $"Email vé điện tử đã được tạo và lưu thành công tại:\n{savedPath}";
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = demoMsg;
                }
                return result;
            }

            // Gửi email thật qua SMTP Client
            var memoryStreams = new List<MemoryStream>();
            try
            {
                using var mail = new MailMessage();
                mail.From = new MailAddress(_config.FromEmail, _config.FromName, Encoding.UTF8);
                mail.To.Add(new MailAddress(message.ToEmail.Trim(), message.ToName.Trim(), Encoding.UTF8));
                mail.Subject = message.Subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Body = message.BodyHtml;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;

                if (!string.IsNullOrWhiteSpace(message.BodyText))
                {
                    mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(
                        message.BodyText, Encoding.UTF8, "text/plain"));
                }

                // Đính kèm các tệp (PDF vé điện tử...)
                foreach (var att in message.Attachments)
                {
                    if (att.Data != null && att.Data.Length > 0)
                    {
                        var ms = new MemoryStream(att.Data);
                        memoryStreams.Add(ms);
                        var mailAtt = new Attachment(ms, att.FileName, att.ContentType);
                        mail.Attachments.Add(mailAtt);
                    }
                }

                using var client = new SmtpClient(_config.Host, _config.Port)
                {
                    EnableSsl = _config.EnableSsl,
                    Timeout = _config.TimeoutMs > 0 ? _config.TimeoutMs : 15000,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                if (!string.IsNullOrWhiteSpace(_config.UserName))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_config.UserName, _config.Password);
                }

                await client.SendMailAsync(mail, cancellationToken);

                result.Success = true;
                result.Message = $"Đã gửi email vé điện tử thành công tới {message.ToEmail} qua SMTP {_config.Host}.";
                result.MessageId = $"SMTP_{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid():N}";

                Log.Information("[SmtpEmailService] Gửi email thành công tới {ToEmail}. Tiêu đề: {Subject}",
                    message.ToEmail, message.Subject);

                if (_settings.SaveCopyToFile)
                {
                    result.SavedCopyPath = SaveEmailCopyToFile(message);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[SmtpEmailService] Lỗi khi gửi email qua SMTP tới {ToEmail}", message.ToEmail);
                result.Success = false;
                result.ErrorMessage = $"Lỗi kết nối máy chủ gửi mail SMTP ({ex.Message})";

                // Vẫn lưu bản sao dự phòng để khách/giảng viên có thể xem file vé PDF ngay lập tức
                if (_settings.SaveCopyToFile)
                {
                    try
                    {
                        string savedPath = SaveEmailCopyToFile(message);
                        result.SavedCopyPath = savedPath;
                        result.Message = $"Gửi qua mạng gặp sự cố ({ex.Message}). Đã lưu bản sao vé tại:\n{savedPath}";
                    }
                    catch { }
                }
            }
            finally
            {
                foreach (var ms in memoryStreams)
                {
                    ms.Dispose();
                }
            }

            return result;
        }

        public async Task<EmailSendResultDto> SendTicketEmailAsync(
            string toEmail,
            string customerName,
            List<TicketReceiptDto> tickets,
            byte[]? pdfAttachment = null,
            CancellationToken cancellationToken = default)
        {
            var message = EmailService.BuildTicketEmailMessage(toEmail, customerName, tickets, pdfAttachment);
            return await SendEmailAsync(message, cancellationToken);
        }

        /// <summary>
        /// Lưu bản sao email (HTML + tệp PDF vé đính kèm) ra ổ đĩa
        /// </summary>
        public string SaveEmailCopyToFile(EmailMessageDto message)
        {
            string baseDir = Path.Combine(AppContext.BaseDirectory, _settings.SentEmailsDirectory);
            if (!Directory.Exists(baseDir))
            {
                Directory.CreateDirectory(baseDir);
            }

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string safeEmail = string.Concat(message.ToEmail.Split(Path.GetInvalidFileNameChars()));
            string folderName = $"{timestamp}_{safeEmail}";
            string targetFolder = Path.Combine(baseDir, folderName);
            Directory.CreateDirectory(targetFolder);

            // 1. Lưu nội dung HTML email
            string htmlPath = Path.Combine(targetFolder, "Email_NoiDung.html");
            File.WriteAllText(htmlPath, message.BodyHtml, Encoding.UTF8);

            // 2. Lưu các tệp đính kèm (PDF vé điện tử...)
            foreach (var att in message.Attachments)
            {
                if (att.Data != null && att.Data.Length > 0)
                {
                    string attPath = Path.Combine(targetFolder, att.FileName);
                    File.WriteAllBytes(attPath, att.Data);
                }
            }

            // 3. Lưu tệp thông tin tóm tắt
            string metaPath = Path.Combine(targetFolder, "Email_ThongTin.txt");
            string metaInfo = $"NGÀY GỬI: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n" +
                              $"NGƯỜI NHẬN: {message.ToName} <{message.ToEmail}>\n" +
                              $"TIÊU ĐỀ: {message.Subject}\n" +
                              $"SỐ LƯỢNG ĐÍNH KÈM: {message.Attachments.Count}\n" +
                              $"DANH SÁCH FILE: {string.Join(", ", message.Attachments.Select(a => a.FileName))}\n";
            File.WriteAllText(metaPath, metaInfo, Encoding.UTF8);

            return targetFolder;
        }
    }
}
