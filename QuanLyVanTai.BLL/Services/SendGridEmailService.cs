using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using QuanLyVanTai.BLL.DTOs;
using Serilog;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// Dịch vụ gửi email qua SendGrid REST Web API v3
    /// Hỗ trợ gửi bất đồng bộ, đính kèm file Base64 và không làm phụ thuộc thư viện bên ngoài
    /// </summary>
    public class SendGridEmailService : IEmailService
    {
        private readonly SendGridConfigDto _config;
        private readonly EmailSettingsDto _settings;
        private static readonly HttpClient _httpClient = new();

        public SendGridEmailService(SendGridConfigDto? config = null, EmailSettingsDto? settings = null)
        {
            _config = config ?? new SendGridConfigDto();
            _settings = settings ?? new EmailSettingsDto { SendGrid = _config };
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

            bool isPlaceholder = string.IsNullOrWhiteSpace(_config.ApiKey) ||
                                 _config.ApiKey.Contains("YOUR_SENDGRID_API_KEY_HERE", StringComparison.OrdinalIgnoreCase);

            if (isPlaceholder)
            {
                string demoMsg = "[CHẾ ĐỘ MÔ PHỎNG] Chưa cấu hình SendGrid ApiKey trong appsettings.json.";
                Log.Warning("[SendGridEmailService] {Message}. Hệ thống sẽ xuất bản sao email vé ra thư mục để kiểm tra.", demoMsg);

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

            try
            {
                // Xây dựng payload JSON chuẩn theo SendGrid v3 Mail Send API
                var payload = new
                {
                    personalizations = new[]
                    {
                        new
                        {
                            to = new[]
                            {
                                new
                                {
                                    email = message.ToEmail.Trim(),
                                    name = message.ToName.Trim()
                                }
                            },
                            subject = message.Subject
                        }
                    },
                    from = new
                    {
                        email = _config.FromEmail,
                        name = _config.FromName
                    },
                    content = new[]
                    {
                        new
                        {
                            type = "text/html",
                            value = message.BodyHtml
                        }
                    },
                    attachments = message.Attachments.Select(att => new
                    {
                        content = Convert.ToBase64String(att.Data),
                        filename = att.FileName,
                        type = att.ContentType,
                        disposition = "attachment"
                    }).ToArray()
                };

                string jsonContent = JsonSerializer.Serialize(payload);
                using var request = new HttpRequestMessage(HttpMethod.Post, _config.ApiEndpoint);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _config.ApiKey.Trim());
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    result.Success = true;
                    result.Message = $"Đã gửi email vé điện tử thành công tới {message.ToEmail} qua SendGrid API.";
                    result.MessageId = response.Headers.TryGetValues("X-Message-Id", out var vals) ? vals.FirstOrDefault() : null;

                    Log.Information("[SendGridEmailService] Gửi email thành công tới {ToEmail}. Mã phản hồi: {StatusCode}",
                        message.ToEmail, (int)response.StatusCode);

                    if (_settings.SaveCopyToFile)
                    {
                        result.SavedCopyPath = SaveEmailCopyToFile(message);
                    }
                }
                else
                {
                    string errContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    result.Success = false;
                    result.ErrorMessage = $"SendGrid trả về mã lỗi {(int)response.StatusCode}: {errContent}";
                    Log.Error("[SendGridEmailService] Lỗi gửi email: {Error}", result.ErrorMessage);

                    if (_settings.SaveCopyToFile)
                    {
                        result.SavedCopyPath = SaveEmailCopyToFile(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[SendGridEmailService] Lỗi ngoại lệ khi gửi email qua SendGrid tới {ToEmail}", message.ToEmail);
                result.Success = false;
                result.ErrorMessage = $"Lỗi kết nối SendGrid: {ex.Message}";

                if (_settings.SaveCopyToFile)
                {
                    try
                    {
                        result.SavedCopyPath = SaveEmailCopyToFile(message);
                    }
                    catch { }
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

        private string SaveEmailCopyToFile(EmailMessageDto message)
        {
            string baseDir = Path.Combine(AppContext.BaseDirectory, _settings.SentEmailsDirectory);
            if (!Directory.Exists(baseDir))
            {
                Directory.CreateDirectory(baseDir);
            }

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string safeEmail = string.Concat(message.ToEmail.Split(Path.GetInvalidFileNameChars()));
            string targetFolder = Path.Combine(baseDir, $"{timestamp}_{safeEmail}");
            Directory.CreateDirectory(targetFolder);

            string htmlPath = Path.Combine(targetFolder, "Email_NoiDung.html");
            File.WriteAllText(htmlPath, message.BodyHtml, Encoding.UTF8);

            foreach (var att in message.Attachments)
            {
                if (att.Data != null && att.Data.Length > 0)
                {
                    File.WriteAllBytes(Path.Combine(targetFolder, att.FileName), att.Data);
                }
            }

            return targetFolder;
        }
    }
}
