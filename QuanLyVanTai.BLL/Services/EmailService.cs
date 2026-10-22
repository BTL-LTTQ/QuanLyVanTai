using System.Text;
using System.Text.Json;
using QuanLyVanTai.BLL.DTOs;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// Service trung tâm điều phối dịch vụ Email (SMTP / SendGrid), quản lý cấu hình và sinh HTML template vé điện tử
    /// </summary>
    public class EmailService : IEmailService
    {
        private static EmailService? _instance;
        public static EmailService Instance => _instance ??= new EmailService();

        private readonly EmailSettingsDto _settings;
        private readonly IEmailService _providerService;

        public EmailSettingsDto Settings => _settings;

        public EmailService(EmailSettingsDto? settings = null)
        {
            _settings = settings ?? LoadSettings();

            if (string.Equals(_settings.Provider, "SendGrid", StringComparison.OrdinalIgnoreCase))
            {
                _providerService = new SendGridEmailService(_settings.SendGrid, _settings);
            }
            else
            {
                _providerService = new SmtpEmailService(_settings.Smtp, _settings);
            }
        }

        public static EmailSettingsDto LoadSettings()
        {
            try
            {
                string configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
                if (File.Exists(configPath))
                {
                    string json = File.ReadAllText(configPath);
                    using var doc = JsonDocument.Parse(json);
                    if (doc.RootElement.TryGetProperty("EmailSettings", out var section))
                    {
                        var settings = JsonSerializer.Deserialize<EmailSettingsDto>(
                            section.GetRawText(),
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        if (settings != null) return settings;
                    }
                }
            }
            catch { }

            return new EmailSettingsDto();
        }

        public async Task<EmailSendResultDto> SendEmailAsync(EmailMessageDto message, CancellationToken cancellationToken = default)
        {
            return await _providerService.SendEmailAsync(message, cancellationToken);
        }

        public async Task<EmailSendResultDto> SendTicketEmailAsync(
            string toEmail,
            string customerName,
            List<TicketReceiptDto> tickets,
            byte[]? pdfAttachment = null,
            CancellationToken cancellationToken = default)
        {
            var message = BuildTicketEmailMessage(toEmail, customerName, tickets, pdfAttachment);
            return await _providerService.SendEmailAsync(message, cancellationToken);
        }

        /// <summary>
        /// Xây dựng đối tượng EmailMessageDto chứa nội dung HTML và tệp PDF vé điện tử đính kèm
        /// </summary>
        public static EmailMessageDto BuildTicketEmailMessage(
            string toEmail,
            string customerName,
            List<TicketReceiptDto> tickets,
            byte[]? pdfAttachment = null)
        {
            if (tickets == null || tickets.Count == 0)
                throw new ArgumentException("Danh sách vé không được rỗng.", nameof(tickets));

            var first = tickets[0];
            string primaryCode = first.TicketCode;
            string subject = tickets.Count == 1
                ? $"[QLVT Bus Lines] Xác nhận đặt vé & Vé điện tử #{primaryCode}"
                : $"[QLVT Bus Lines] Xác nhận đặt vé & Vé điện tử #{primaryCode} (và {tickets.Count - 1} vé khác)";

            string html = BuildTicketEmailHtml(customerName, tickets);

            // Sinh PDF nếu chưa được truyền vào
            if (pdfAttachment == null || pdfAttachment.Length == 0)
            {
                pdfAttachment = TicketPdfExportService.GenerateTicketPdf(tickets);
            }

            string pdfFileName = tickets.Count == 1
                ? $"VeDienTu_{primaryCode}.pdf"
                : $"VeDienTu_{primaryCode}_{tickets.Count}ve.pdf";

            var msg = new EmailMessageDto
            {
                ToEmail = toEmail,
                ToName = string.IsNullOrWhiteSpace(customerName) ? first.CustomerName : customerName,
                Subject = subject,
                BodyHtml = html,
                BodyText = $"Kính gửi {customerName},\nVé điện tử {primaryCode} của Quý khách đã thanh toán thành công.\nVui lòng xem file PDF đính kèm để lên xe.\nTổng đài: 1900 6868\nQLVT Bus Lines."
            };

            if (pdfAttachment != null && pdfAttachment.Length > 0)
            {
                msg.Attachments.Add(new EmailAttachmentDto(pdfFileName, pdfAttachment, "application/pdf"));
            }

            return msg;
        }

        /// <summary>
        /// Tạo nội dung HTML chuyên nghiệp, chuẩn responsive email client cho vé xe điện tử
        /// </summary>
        public static string BuildTicketEmailHtml(string customerName, List<TicketReceiptDto> tickets)
        {
            var first = tickets[0];
            string displayName = string.IsNullOrWhiteSpace(customerName) ? first.CustomerName : customerName;
            decimal totalAmount = tickets.Sum(t => t.FinalPrice);

            var sbTickets = new StringBuilder();
            foreach (var t in tickets)
            {
                sbTickets.Append($@"
                    <tr style='border-bottom: 1px solid #e2e8f0;'>
                        <td style='padding: 12px 14px; font-weight: bold; color: #dc2626;'>
                            <span style='background: #fee2e2; padding: 4px 8px; border-radius: 4px; font-family: monospace; font-size: 13px;'>{t.TicketCode}</span>
                        </td>
                        <td style='padding: 12px 14px; color: #1e293b;'>
                            <strong>{t.RouteName}</strong><br/>
                            <span style='font-size: 12px; color: #64748b;'>{t.DepartureStation} &rarr; {t.ArrivalStation}</span>
                        </td>
                        <td style='padding: 12px 14px; color: #1e293b;'>
                            {t.DepartureTime:HH:mm dd/MM/yyyy}<br/>
                            <span style='font-size: 12px; color: #64748b;'>BS: {t.LicensePlate}</span>
                        </td>
                        <td style='padding: 12px 14px; text-align: center; font-weight: bold; color: #0284c7;'>
                            Ghế {t.SeatNumber}<br/>
                            <span style='font-size: 11px; color: #64748b; font-weight: normal;'>Tầng {t.Floor}</span>
                        </td>
                        <td style='padding: 12px 14px; text-align: right; font-weight: bold; color: #107c41;'>
                            {t.FinalPrice:N0} đ
                        </td>
                    </tr>");
            }

            string html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'/>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'/>
    <title>Vé Điện Tử - QLVT Bus Lines</title>
</head>
<body style='margin: 0; padding: 0; background-color: #f1f5f9; font-family: ""Segoe UI"", Tahoma, Geneva, Verdana, sans-serif; color: #0f172a;'>
    <table role='presentation' width='100%' cellspacing='0' cellpadding='0' style='background-color: #f1f5f9; padding: 30px 10px;'>
        <tr>
            <td align='center'>
                <!-- Main Card -->
                <table role='presentation' width='100%' style='max-width: 650px; background-color: #ffffff; border-radius: 12px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.07); overflow: hidden; border: 1px solid #e2e8f0;'>
                    <!-- Header -->
                    <tr>
                        <td style='background: linear-gradient(135deg, #107c41 0%, #064e3b 100%); padding: 28px 30px; text-align: center; color: #ffffff;'>
                            <h1 style='margin: 0; font-size: 24px; font-weight: 800; letter-spacing: 0.5px;'>QLVT BUS LINES</h1>
                            <p style='margin: 6px 0 0; font-size: 13px; opacity: 0.9; text-transform: uppercase; letter-spacing: 1px;'>Hệ Thống Vận Tải Đường Bộ Chất Lượng Cao</p>
                        </td>
                    </tr>

                    <!-- Notification Banner -->
                    <tr>
                        <td style='background-color: #ecfdf5; border-bottom: 2px solid #a7f3d0; padding: 16px 30px;'>
                            <table width='100%'>
                                <tr>
                                    <td width='32' style='vertical-align: middle; font-size: 24px;'>&#9989;</td>
                                    <td style='vertical-align: middle;'>
                                        <div style='color: #065f46; font-weight: bold; font-size: 15px;'>XÁC NHẬN THANH TOÁN THÀNH CÔNG!</div>
                                        <div style='color: #047857; font-size: 13px;'>Vé điện tử của Quý khách đã có hiệu lực trên hệ thống.</div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <!-- Greeting & Summary -->
                    <tr>
                        <td style='padding: 26px 30px 10px;'>
                            <p style='margin: 0 0 14px; font-size: 15px; color: #334155;'>
                                Kính gửi <strong>{displayName}</strong>,
                            </p>
                            <p style='margin: 0 0 18px; font-size: 14px; line-height: 1.6; color: #475569;'>
                                Cảm ơn Quý khách đã tin tưởng lựa chọn dịch vụ vận tải hành khách của <strong>QLVT Bus Lines</strong>. 
                                Chúng tôi xin gửi thông tin chi tiết vé xe điện tử cùng <strong>tệp PDF đính kèm</strong> cho chuyến đi sắp tới của Quý khách:
                            </p>
                        </td>
                    </tr>

                    <!-- Ticket Table -->
                    <tr>
                        <td style='padding: 0 30px 20px;'>
                            <table width='100%' cellspacing='0' cellpadding='0' style='border: 1px solid #e2e8f0; border-radius: 8px; border-collapse: separate; overflow: hidden; font-size: 13.5px;'>
                                <thead>
                                    <tr style='background-color: #f8fafc; color: #475569; text-align: left; font-size: 12px; text-transform: uppercase;'>
                                        <th style='padding: 10px 14px; border-bottom: 2px solid #cbd5e1;'>Mã Vé</th>
                                        <th style='padding: 10px 14px; border-bottom: 2px solid #cbd5e1;'>Tuyến Xe</th>
                                        <th style='padding: 10px 14px; border-bottom: 2px solid #cbd5e1;'>Khởi Hành</th>
                                        <th style='padding: 10px 14px; border-bottom: 2px solid #cbd5e1; text-align: center;'>Ghế / Tầng</th>
                                        <th style='padding: 10px 14px; border-bottom: 2px solid #cbd5e1; text-align: right;'>Giá Vé</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {sbTickets}
                                </tbody>
                                <tfoot>
                                    <tr style='background-color: #f8fafc; font-weight: bold;'>
                                        <td colspan='4' style='padding: 14px; text-align: right; color: #334155;'>Tổng Tiền Đã Thanh Toán:</td>
                                        <td style='padding: 14px; text-align: right; color: #dc2626; font-size: 16px;'>{totalAmount:N0} đ</td>
                                    </tr>
                                </tfoot>
                            </table>
                        </td>
                    </tr>

                    <!-- Instructions / Notice Box -->
                    <tr>
                        <td style='padding: 0 30px 26px;'>
                            <div style='background-color: #fefce8; border-left: 4px solid #eab308; padding: 14px 18px; border-radius: 4px;'>
                                <div style='font-weight: bold; color: #854d0e; font-size: 13.5px; margin-bottom: 6px;'>
                                    &#128204; HƯỚNG DẪN KHI ĐI XE:
                                </div>
                                <ul style='margin: 0; padding-left: 20px; font-size: 13px; color: #713f12; line-height: 1.6;'>
                                    <li>Quý khách vui lòng có mặt tại điểm đón trước giờ khởi hành ít nhất <strong>20 - 30 phút</strong> để làm thủ tục check-in.</li>
                                    <li>Xuất trình <strong>tệp PDF vé điện tử đính kèm</strong> email này hoặc mã QR cho tài xế/phụ xe khi lên xe.</li>
                                    <li>Mỗi hành khách được miễn cước tối đa 20kg hành lý xách tay.</li>
                                </ul>
                            </div>
                        </td>
                    </tr>

                    <!-- Attached File Notice -->
                    <tr>
                        <td style='padding: 0 30px 26px;'>
                            <div style='background-color: #f0fdf4; border: 1px dashed #22c55e; border-radius: 8px; padding: 14px 18px; text-align: center;'>
                                <div style='font-size: 20px; margin-bottom: 4px;'>&#128196;</div>
                                <div style='font-weight: bold; color: #15803d; font-size: 14px;'>Tệp PDF Vé Điện Tử Đã Được Đính Kèm Trong Email Này</div>
                                <div style='font-size: 12px; color: #166534; margin-top: 3px;'>
                                    Quý khách có thể tải về điện thoại hoặc in ra giấy trực tiếp để sử dụng khi lên xe.
                                </div>
                            </div>
                        </td>
                    </tr>

                    <!-- Footer -->
                    <tr>
                        <td style='background-color: #0f172a; padding: 24px 30px; text-align: center; color: #94a3b8; font-size: 12.5px; line-height: 1.6;'>
                            <div style='color: #f8fafc; font-weight: bold; font-size: 14px; margin-bottom: 6px;'>
                                CÔNG TY CỔ PHẦN VẬN TẢI HÀNH KHÁCH ĐƯỜNG BỘ QLVT
                            </div>
                            <div>&#128222; Tổng đài CSKH 24/7: <strong style='color: #38bdf8;'>1900 6868</strong> | &#9993; Email: <a href='mailto:hotro@qlvt.vn' style='color: #38bdf8; text-decoration: none;'>hotro@qlvt.vn</a></div>
                            <div>&#127760; Website: <a href='https://qlvt.vn' style='color: #38bdf8; text-decoration: none;'>https://qlvt.vn</a> | Trụ sở: Hà Nội &bull; Đà Nẵng &bull; TP. Hồ Chí Minh</div>
                            <div style='margin-top: 10px; font-size: 11px; color: #64748b; border-top: 1px solid #334155; padding-top: 10px;'>
                                Email này được gửi tự động từ hệ thống Quản Lý Vận Tải. Vui lòng không trả lời trực tiếp email này.
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";

            return html;
        }
    }
}
