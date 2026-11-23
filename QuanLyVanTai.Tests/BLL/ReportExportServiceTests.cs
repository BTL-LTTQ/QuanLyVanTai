using System.Drawing;
using System.Text;
using System.Text.Json;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;

namespace QuanLyVanTai.Tests.BLL
{
    /// <summary>
    /// Kiểm thử các service xuất báo cáo mới:
    ///   • RouteJsonExportService  — JSON nghiệp vụ giữ nguyên tiếng Việt
    ///   • PdfImageDocumentBuilder — đóng gói PDF 1.4 từ trang JPEG
    ///   • RevenueReportPdfExportService — báo cáo doanh thu A4 ngang nhiều trang
    /// </summary>
    public class ReportExportServiceTests
    {
        // ── JSON ─────────────────────────────────────────────────────────────

        [Fact]
        public void Serialize_Routes_KeepsVietnameseTextUnescaped()
        {
            var routes = new List<RouteListDto>
            {
                new()
                {
                    Id = 1,
                    RouteCode = "T01",
                    RouteName = "Hà Nội - Hải Phòng",
                    DistanceKm = 120,
                    EstimatedHours = 2.5m,
                    BasePrice = 150_000m,
                    Status = "Active",
                    StationCount = 3,
                    StationNames = "Bến xe Mỹ Đình; Bến xe Hải Phòng"
                }
            };

            string json = RouteJsonExportService.Serialize(routes);

            Assert.Contains("Hà Nội - Hải Phòng", json);
            Assert.DoesNotContain("\\u", json);

            using var document = JsonDocument.Parse(json);
            var first = document.RootElement[0];
            Assert.Equal("T01", first.GetProperty("RouteCode").GetString());
            Assert.Equal("Hà Nội - Hải Phòng", first.GetProperty("RouteName").GetString());
            Assert.Equal(150_000m, first.GetProperty("BasePrice").GetDecimal());
        }

        [Fact]
        public void Serialize_EmptyList_ReturnsEmptyJsonArray()
        {
            string json = RouteJsonExportService.Serialize([]);
            Assert.Equal("[]", json.Trim());
        }

        // ── PDF Builder ───────────────────────────────────────────────────────

        [Fact]
        public void PdfImageDocumentBuilder_ProducesValidPdfStructure()
        {
            using var bitmap = CreateSolidBitmap(240, 160);
            byte[] jpeg = PdfImageDocumentBuilder.EncodeJpeg(bitmap, 80);

            // JPEG chuẩn bắt đầu bằng FF D8
            Assert.True(jpeg.Length > 100);
            Assert.Equal(0xFF, jpeg[0]);
            Assert.Equal(0xD8, jpeg[1]);

            byte[] pdf = PdfImageDocumentBuilder.BuildPdf([jpeg], 240, 160);

            Assert.StartsWith("%PDF-1.4", Encoding.ASCII.GetString(pdf, 0, 8));
            Assert.Contains("%%EOF", Encoding.ASCII.GetString(pdf));
            Assert.Contains("/MediaBox [ 0 0 240 160 ]", Encoding.ASCII.GetString(pdf));
        }

        [Fact]
        public void PdfImageDocumentBuilder_SupportsSeparatePageSize()
        {
            using var bitmap = CreateSolidBitmap(1684, 1190);
            byte[] jpeg = PdfImageDocumentBuilder.EncodeJpeg(bitmap, 60);

            byte[] pdf = PdfImageDocumentBuilder.BuildPdf([jpeg], 1684, 1190, 841.89, 595.28);
            string content = Encoding.ASCII.GetString(pdf);

            // Khổ A4 ngang tính bằng point, ảnh vẫn giữ độ phân giải pixel gốc
            Assert.Contains("/MediaBox [ 0 0 841.89 595.28 ]", content);
            Assert.Contains("/Width 1684 /Height 1190", content);
        }

        [Fact]
        public void PdfImageDocumentBuilder_EmptyPages_ReturnsEmptyArray()
        {
            byte[] pdf = PdfImageDocumentBuilder.BuildPdf([], 100, 100);
            Assert.Empty(pdf);
        }

        // ── Báo cáo doanh thu ─────────────────────────────────────────────────

        [Fact]
        public void RevenueReport_GeneratesPdfWithVietnameseContent()
        {
            var service = new RevenueReportPdfExportService();
            var report = BuildSampleReport(dailyDayCount: 7, routeCount: 4);

            byte[] pdf = service.GeneratePdf(report);

            Assert.True(pdf.Length > 20_000, $"PDF quá nhỏ ({pdf.Length} bytes) — có thể render lỗi.");
            Assert.StartsWith("%PDF-1.4", Encoding.ASCII.GetString(pdf, 0, 8));
            Assert.Contains("%%EOF", Encoding.ASCII.GetString(pdf));

            // 7 ngày + 4 tuyến gọn trong 1 trang
            Assert.Equal(1, CountPages(pdf));
        }

        [Fact]
        public void RevenueReport_PaginatesWhenTablesExceedPageHeight()
        {
            var service = new RevenueReportPdfExportService();
            // 60 ngày + 25 tuyến chắc chắn vượt 1 trang A4 ngang
            var report = BuildSampleReport(dailyDayCount: 60, routeCount: 25);

            byte[] pdf = service.GeneratePdf(report);

            Assert.True(CountPages(pdf) >= 2,
                "Bảng dài phải tự ngắt sang trang mới với tiêu đề lặp lại.");

            // Mỗi trang đều là ảnh JPEG nhúng hợp lệ
            string content = Encoding.ASCII.GetString(pdf);
            Assert.Contains("/Filter /DCTDecode", content);
        }

        [Fact]
        public void RevenueReport_SaveReportPdf_WritesFileToDisk()
        {
            var service = new RevenueReportPdfExportService();
            var report = BuildSampleReport(dailyDayCount: 3, routeCount: 2);

            string dir = Path.Combine(Path.GetTempPath(), "qlvt-tests", Guid.NewGuid().ToString("N"));
            string filePath = Path.Combine(dir, "BaoCao.pdf");

            try
            {
                service.SaveReportPdf(report, filePath);

                Assert.True(File.Exists(filePath));
                var bytes = File.ReadAllBytes(filePath);
                Assert.StartsWith("%PDF-1.4", Encoding.ASCII.GetString(bytes, 0, 8));
            }
            finally
            {
                if (Directory.Exists(dir))
                    Directory.Delete(dir, recursive: true);
            }
        }

        // ── Helpers ──────────────────────────────────────────────────────────

        private static Bitmap CreateSolidBitmap(int width, int height)
        {
            var bitmap = new Bitmap(width, height);
            using var g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            using var brush = new SolidBrush(Color.FromArgb(37, 99, 235));
            g.FillRectangle(brush, 10, 10, width / 2, height / 2);
            return bitmap;
        }

        private static int CountPages(byte[] pdf)
            => CountOccurrences(Encoding.ASCII.GetString(pdf), "/Type /Page /Parent");

        private static int CountOccurrences(string text, string token)
        {
            int count = 0;
            int index = 0;
            while ((index = text.IndexOf(token, index, StringComparison.Ordinal)) >= 0)
            {
                count++;
                index += token.Length;
            }
            return count;
        }

        private static RevenueReportDto BuildSampleReport(int dailyDayCount, int routeCount)
        {
            var start = new DateTime(2026, 10, 1);
            var days = Enumerable.Range(0, dailyDayCount)
                .Select(i => new RevenueByDateDto
                {
                    Date = start.AddDays(i),
                    DateLabel = start.AddDays(i).ToString("dd/MM"),
                    Revenue = 1_000_000m + i * 125_000m,
                    TicketCount = 20 + i
                })
                .ToList();

            var routes = Enumerable.Range(1, routeCount)
                .Select(i => new RevenueByRouteDto
                {
                    RouteId = i,
                    RouteCode = $"T{i:D2}",
                    RouteName = $"Tuyến số {i} - Hà Nội đi tỉnh lẻ",
                    Revenue = 5_000_000m / i,
                    TicketCount = 100 / i,
                    Percentage = Math.Round(100.0 / routeCount, 1)
                })
                .ToList();

            return new RevenueReportDto
            {
                FromDate = days[0].Date,
                ToDate = days[^1].Date,
                GeneratedBy = "Nguyễn Văn Kiểm Thử",
                GeneratedAt = new DateTime(2026, 11, 23, 9, 30, 0),
                Summary = new DashboardSummaryDto
                {
                    TotalRevenue = days.Sum(d => d.Revenue),
                    TotalTickets = days.Sum(d => d.TicketCount),
                    ActiveRoutes = routeCount,
                    ActiveVehicles = routeCount + 3,
                    TodayRevenue = days[^1].Revenue,
                    TodayTickets = days[^1].TicketCount
                },
                DailyRevenue = days,
                RouteRevenue = routes
            };
        }
    }
}
