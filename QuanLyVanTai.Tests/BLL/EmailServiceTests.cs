using System.Text;
using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;
using Xunit;

namespace QuanLyVanTai.Tests.BLL
{
    public class EmailServiceTests
    {
        private TicketReceiptDto CreateSampleReceipt(string code = "TK20261018001")
        {
            return new TicketReceiptDto
            {
                TicketId = 1,
                TicketCode = code,
                CustomerName = "Trần Hoàng Quân",
                CustomerPhone = "0987654321",
                CustomerEmail = "quan.th@example.com",
                RouteName = "Hà Nội - Đà Nẵng",
                RouteCode = "HN-DN-01",
                LicensePlate = "29B-999.88",
                VehicleType = "Giường nằm 40 chỗ",
                SeatNumber = "A05",
                Floor = 1,
                DepartureTime = DateTime.Now.AddDays(2),
                DepartureStation = "Bến xe Giáp Bát",
                ArrivalStation = "Bến xe Trung tâm Đà Nẵng",
                OriginalPrice = 350000m,
                DiscountAmount = 0m,
                FinalPrice = 350000m,
                PaymentStatus = "Đã thanh toán",
                TicketStatus = "Booked",
                BookingTime = DateTime.Now,
                StaffName = "Hệ thống Online",
                QrCodeContent = "QLVT|MAVE:TK20261018001|TUYEN:HN-DN-01|GHE:A05|GIA:350000"
            };
        }

        [Fact]
        public void TicketPdfExportService_GenerateTicketPdf_ReturnsValidPdfBytes()
        {
            // Arrange
            var sampleReceipt = CreateSampleReceipt();
            var tickets = new List<TicketReceiptDto> { sampleReceipt };

            // Act
            byte[] pdfBytes = TicketPdfExportService.GenerateTicketPdf(tickets);

            // Assert
            Assert.NotNull(pdfBytes);
            Assert.True(pdfBytes.Length > 1000, "Tệp PDF vé phải có dung lượng lớn hơn 1KB");

            string pdfHeader = Encoding.ASCII.GetString(pdfBytes, 0, 8);
            Assert.StartsWith("%PDF-1.4", pdfHeader);

            string pdfContent = Encoding.ASCII.GetString(pdfBytes);
            Assert.Contains("%%EOF", pdfContent);
            Assert.Contains("/Type /Catalog", pdfContent);
            Assert.Contains("/Type /Pages", pdfContent);
            Assert.Contains("/Subtype /Image", pdfContent);
        }

        [Fact]
        public void TicketPdfExportService_GenerateMultiPagePdf_ReturnsValidPdfWithMultiplePages()
        {
            // Arrange
            var ticket1 = CreateSampleReceipt("TK20261018001");
            var ticket2 = CreateSampleReceipt("TK20261018002");
            ticket2.SeatNumber = "A06";
            var tickets = new List<TicketReceiptDto> { ticket1, ticket2 };

            // Act
            byte[] pdfBytes = TicketPdfExportService.GenerateTicketPdf(tickets);

            // Assert
            Assert.NotNull(pdfBytes);
            string pdfContent = Encoding.ASCII.GetString(pdfBytes);
            Assert.Contains("/Count 2", pdfContent); // 2 trang
        }

        [Fact]
        public void EmailService_BuildTicketEmailHtml_ContainsCustomerAndTicketInfo()
        {
            // Arrange
            var ticket = CreateSampleReceipt();
            var tickets = new List<TicketReceiptDto> { ticket };

            // Act
            string html = EmailService.BuildTicketEmailHtml("Trần Hoàng Quân", tickets);

            // Assert
            Assert.NotNull(html);
            Assert.Contains("Trần Hoàng Quân", html);
            Assert.Contains(ticket.TicketCode, html);
            Assert.Contains(ticket.RouteName, html);
            Assert.Contains(ticket.SeatNumber, html);
            Assert.Contains(ticket.FinalPrice.ToString("N0"), html); // Giá tiền
            Assert.Contains("QLVT BUS LINES", html);
            Assert.Contains("1900 6868", html); // Hotline
        }

        [Fact]
        public void EmailService_BuildTicketEmailMessage_CreatesValidAttachment()
        {
            // Arrange
            var ticket = CreateSampleReceipt();
            var tickets = new List<TicketReceiptDto> { ticket };

            // Act
            var msg = EmailService.BuildTicketEmailMessage("test@example.com", "Trần Hoàng Quân", tickets);

            // Assert
            Assert.Equal("test@example.com", msg.ToEmail);
            Assert.Equal("Trần Hoàng Quân", msg.ToName);
            Assert.Contains(ticket.TicketCode, msg.Subject);
            Assert.Single(msg.Attachments);

            var att = msg.Attachments[0];
            Assert.EndsWith(".pdf", att.FileName);
            Assert.Equal("application/pdf", att.ContentType);
            Assert.True(att.Data.Length > 0);
        }

        [Fact]
        public async Task SmtpEmailService_WhenNoAppPassword_SavesLocalCopyGracefully()
        {
            // Arrange
            var settings = new EmailSettingsDto
            {
                Provider = "Smtp",
                SaveCopyToFile = true,
                SentEmailsDirectory = "TestSentEmails",
                Smtp = new SmtpConfigDto
                {
                    Host = "smtp.gmail.com",
                    UserName = "test@gmail.com",
                    Password = "YOUR_APP_PASSWORD_HERE" // Placeholder -> Chế độ mô phỏng
                }
            };
            var service = new SmtpEmailService(settings.Smtp, settings);
            var sampleReceipt = CreateSampleReceipt();

            // Act
            var result = await service.SendTicketEmailAsync("khachhang@example.com", "Trần Hoàng Quân", new List<TicketReceiptDto> { sampleReceipt });

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.SavedCopyPath);
            Assert.True(Directory.Exists(result.SavedCopyPath));

            string htmlFile = Path.Combine(result.SavedCopyPath, "Email_NoiDung.html");
            Assert.True(File.Exists(htmlFile));
            string htmlContent = await File.ReadAllTextAsync(htmlFile);
            Assert.Contains(sampleReceipt.TicketCode, htmlContent);

            // Cleanup test directory
            try
            {
                Directory.Delete(Path.Combine(AppContext.BaseDirectory, "TestSentEmails"), true);
            }
            catch { }
        }

        [Fact]
        public async Task SendGridEmailService_WhenNoApiKey_SavesLocalCopyGracefully()
        {
            // Arrange
            var settings = new EmailSettingsDto
            {
                Provider = "SendGrid",
                SaveCopyToFile = true,
                SentEmailsDirectory = "TestSentEmailsSendGrid",
                SendGrid = new SendGridConfigDto
                {
                    ApiKey = "YOUR_SENDGRID_API_KEY_HERE" // Placeholder -> Chế độ mô phỏng
                }
            };
            var service = new SendGridEmailService(settings.SendGrid, settings);
            var sampleReceipt = CreateSampleReceipt();

            // Act
            var result = await service.SendTicketEmailAsync("khachhang@example.com", "Trần Hoàng Quân", new List<TicketReceiptDto> { sampleReceipt });

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.SavedCopyPath);
            Assert.True(Directory.Exists(result.SavedCopyPath));

            // Cleanup
            try
            {
                Directory.Delete(Path.Combine(AppContext.BaseDirectory, "TestSentEmailsSendGrid"), true);
            }
            catch { }
        }

        [Fact]
        public async Task CustomerBookingService_WithCustomerEmail_SavesAndRetrievesEmail()
        {
            // Arrange
            using var db = QuanLyVanTai.Tests.Helpers.TestDbContextFactory.Create();

            var route = new Route
            {
                RouteCode = "HN-HP-01",
                RouteName = "Hà Nội - Hải Phòng",
                DistanceKm = 100,
                EstimatedHours = 2,
                BasePrice = 120000m,
                Status = "Active"
            };
            var vehicle = new Vehicle
            {
                LicensePlate = "29B-111.22",
                VehicleType = "Ghế ngồi 29 chỗ",
                TotalSeats = 29,
                Status = "Active"
            };
            var schedule = new Schedule
            {
                ScheduleCode = "SCH-TEST-001",
                Route = route,
                Vehicle = vehicle,
                DepartureTime = DateTime.UtcNow.AddDays(1),
                EstimatedArrivalTime = DateTime.UtcNow.AddDays(1).AddHours(2),
                Status = "Scheduled"
            };

            db.Routes.Add(route);
            db.Vehicles.Add(vehicle);
            db.Schedules.Add(schedule);
            await db.SaveChangesAsync();

            var bookingService = new CustomerBookingService(db);
            var bookingRequest = new BookingRequestDto
            {
                ScheduleId = schedule.Id,
                RouteId = route.Id,
                VehicleId = vehicle.Id,
                DepartureTime = schedule.DepartureTime,
                CustomerName = "Nguyễn Văn Test",
                CustomerPhone = "0912345678",
                CustomerEmail = "nguyenvan.test@example.com",
                SelectedSeats = new List<string> { "A01" },
                PaymentMethod = "Tiền mặt",
                PaymentStatus = "Paid"
            };

            // Act
            var bookingResult = await bookingService.BookTicketsAsync(bookingRequest);

            // Assert
            Assert.True(bookingResult.Success);
            Assert.Single(bookingResult.CreatedTicketIds);

            var receipts = await bookingService.GetTicketReceiptsAsync(bookingResult.CreatedTicketIds);
            Assert.Single(receipts);
            Assert.Equal("nguyenvan.test@example.com", receipts[0].CustomerEmail);
            Assert.Equal("Nguyễn Văn Test", receipts[0].CustomerName);
        }
    }
}
