using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using QuanLyVanTai.BLL.DTOs;

namespace QuanLyVanTai.BLL.Services
{
    // =========================================================================
    // RevenueReportPdfExportService — Xuất báo cáo doanh thu Dashboard ra PDF (A4 ngang)
    // =========================================================================
    // Cách hoạt động:
    //   1. Dựng từng trang báo cáo bằng GDI+ trên Bitmap độ phân giải cao
    //      (1684×1190 px ≈ A4 ngang @ 2x) để chữ tiếng Việt sắc nét.
    //   2. Bảng doanh thu theo ngày / theo tuyến tự động ngắt trang, lặp lại
    //      tiêu đề bảng ở mỗi trang nối tiếp.
    //   3. Đóng gói các trang JPEG thành PDF 1.4 qua PdfImageDocumentBuilder,
    //      không phụ thuộc driver máy in hay thư viện PDF bên thứ ba.
    // =========================================================================

    public class RevenueReportPdfExportService
    {
        // ── Khổ trang: A4 ngang (842×595 pt) render ở độ phân giải gấp đôi ──
        private const int ImageWidth = 1684;
        private const int ImageHeight = 1190;
        private const double PageWidthPoints = 841.89;
        private const double PageHeightPoints = 595.28;

        private const int Margin = 70;
        private const int HeaderHeight = 186;
        private const int ContentBottom = ImageHeight - 86;
        private const int TableWidth = ImageWidth - Margin * 2;
        private const int TableHeaderHeight = 44;
        private const int RowHeight = 38;

        // ── Bảng màu đồng bộ với Dashboard ──
        private static readonly Color Primary = Color.FromArgb(37, 99, 235);
        private static readonly Color Accent = Color.FromArgb(16, 185, 129);
        private static readonly Color Warm = Color.FromArgb(245, 158, 11);
        private static readonly Color Purple = Color.FromArgb(139, 92, 246);
        private static readonly Color Dark = Color.FromArgb(15, 23, 42);
        private static readonly Color Muted = Color.FromArgb(100, 116, 139);
        private static readonly Color RowAlt = Color.FromArgb(248, 250, 252);
        private static readonly Color TotalsRow = Color.FromArgb(239, 246, 255);
        private static readonly Color Line = Color.FromArgb(226, 232, 240);

        private static readonly CultureInfo Vi = CultureInfo.GetCultureInfo("vi-VN");

        private static readonly string[] DailyTitles = { "Ngày", "Số vé", "Doanh thu (VNĐ)", "Tỷ trọng" };
        private static readonly (int X, int W, bool Right)[] DailyColumns =
        {
            (Margin + 16, 490, false),
            (Margin + 526, 380, true),
            (Margin + 936, 380, true),
            (Margin + 1346, 182, true)
        };

        private static readonly string[] RouteTitles = { "Mã tuyến", "Tên tuyến", "Số vé", "Doanh thu (VNĐ)", "Tỷ trọng" };
        private static readonly (int X, int W, bool Right)[] RouteColumns =
        {
            (Margin + 16, 190, false),
            (Margin + 216, 690, false),
            (Margin + 926, 160, true),
            (Margin + 1106, 250, true),
            (Margin + 1366, 162, true)
        };

        // ── PUBLIC API ────────────────────────────────────────────────────────

        /// <summary>
        /// Sinh nội dung file PDF báo cáo doanh thu từ dữ liệu Dashboard.
        /// </summary>
        public byte[] GeneratePdf(RevenueReportDto report)
        {
            ArgumentNullException.ThrowIfNull(report);

            var pages = new List<ReportPage>();
            ReportPage current = StartPage(report, pages, firstPage: true);

            // ── Bảng 1: Doanh thu theo ngày ──
            current.Y = DrawSectionTitle(current.Graphics, current.Y, "1. DOANH THU THEO NGÀY");
            current.Y += 10;
            DrawTableHeader(current.Graphics, current.Y, DailyTitles, DailyColumns);
            current.Y += TableHeaderHeight;

            decimal dailyTotal = report.DailyRevenue.Sum(d => d.Revenue);

            if (report.DailyRevenue.Count == 0)
            {
                DrawEmptyRow(current.Graphics, current.Y, "Không phát sinh doanh thu trong khoảng thời gian đã chọn.");
                current.Y += RowHeight;
            }
            else
            {
                for (int i = 0; i < report.DailyRevenue.Count; i++)
                {
                    if (current.Y + RowHeight > ContentBottom)
                    {
                        current = ContinueSection(report, pages, "1. DOANH THU THEO NGÀY (tiếp theo)", DailyTitles, DailyColumns);
                    }
                    DrawDailyRow(current.Graphics, current.Y, report.DailyRevenue[i], dailyTotal, i);
                    current.Y += RowHeight;
                }

                if (current.Y + RowHeight > ContentBottom)
                {
                    current = ContinueSection(report, pages, "1. DOANH THU THEO NGÀY (tiếp theo)", DailyTitles, DailyColumns);
                }
                DrawDailyTotalRow(current.Graphics, current.Y, report.DailyRevenue, dailyTotal);
                current.Y += RowHeight;
            }

            current.Y += 24;

            // ── Bảng 2: Doanh thu theo tuyến xe ──
            const int SectionTwoHeaderCost = 56; // tiêu đề mục + khoảng đệm
            if (current.Y + SectionTwoHeaderCost + TableHeaderHeight + RowHeight > ContentBottom)
            {
                current = ContinueSection(report, pages, "2. DOANH THU THEO TUYẾN XE", RouteTitles, RouteColumns);
            }
            else
            {
                current.Y = DrawSectionTitle(current.Graphics, current.Y, "2. DOANH THU THEO TUYẾN XE");
                current.Y += 10;
                DrawTableHeader(current.Graphics, current.Y, RouteTitles, RouteColumns);
                current.Y += TableHeaderHeight;
            }

            decimal routeTotal = report.RouteRevenue.Sum(r => r.Revenue);

            if (report.RouteRevenue.Count == 0)
            {
                DrawEmptyRow(current.Graphics, current.Y, "Chưa có tuyến xe nào phát sinh doanh thu trong kỳ.");
                current.Y += RowHeight;
            }
            else
            {
                for (int i = 0; i < report.RouteRevenue.Count; i++)
                {
                    if (current.Y + RowHeight > ContentBottom)
                    {
                        current = ContinueSection(report, pages, "2. DOANH THU THEO TUYẾN XE (tiếp theo)", RouteTitles, RouteColumns);
                    }
                    DrawRouteRow(current.Graphics, current.Y, report.RouteRevenue[i], routeTotal, i);
                    current.Y += RowHeight;
                }

                if (current.Y + RowHeight > ContentBottom)
                {
                    current = ContinueSection(report, pages, "2. DOANH THU THEO TUYẾN XE (tiếp theo)", RouteTitles, RouteColumns);
                }
                DrawRouteTotalRow(current.Graphics, current.Y, report.RouteRevenue, routeTotal);
                current.Y += RowHeight;
            }

            // ── Chân trang + đóng gói PDF ──
            int totalPages = pages.Count;
            for (int i = 0; i < totalPages; i++)
                DrawFooter(pages[i].Graphics, report, i + 1, totalPages);

            var jpegPages = new List<byte[]>(totalPages);
            foreach (var page in pages)
            {
                jpegPages.Add(PdfImageDocumentBuilder.EncodeJpeg(page.Bitmap));
                page.Dispose();
            }

            return PdfImageDocumentBuilder.BuildPdf(
                jpegPages, ImageWidth, ImageHeight, PageWidthPoints, PageHeightPoints);
        }

        /// <summary>
        /// Sinh và lưu báo cáo doanh thu trực tiếp thành file PDF trên đĩa.
        /// </summary>
        public void SaveReportPdf(RevenueReportDto report, string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Đường dẫn file PDF không hợp lệ.", nameof(filePath));

            byte[] bytes = GeneratePdf(report);

            string? dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllBytes(filePath, bytes);
        }

        // ── TRANG ─────────────────────────────────────────────────────────────

        private sealed class ReportPage : IDisposable
        {
            public Bitmap Bitmap { get; }
            public Graphics Graphics { get; }
            public int Y { get; set; }

            public ReportPage()
            {
                Bitmap = new Bitmap(ImageWidth, ImageHeight);
                Graphics = Graphics.FromImage(Bitmap);
                Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Graphics.Clear(Color.White);
            }

            public void Dispose()
            {
                Graphics.Dispose();
                Bitmap.Dispose();
            }
        }

        private static ReportPage StartPage(RevenueReportDto report, List<ReportPage> pages, bool firstPage)
        {
            var page = new ReportPage();
            pages.Add(page);

            DrawPageHeader(page.Graphics, report);
            if (firstPage)
                DrawKpiCards(page.Graphics, report.Summary);

            page.Y = firstPage ? 386 : 232;
            return page;
        }

        /// <summary>Tạo trang nối tiếp và vẽ lại tiêu đề mục + dòng tiêu đề bảng.</summary>
        private static ReportPage ContinueSection(
            RevenueReportDto report,
            List<ReportPage> pages,
            string sectionTitle,
            string[] titles,
            (int X, int W, bool Right)[] columns)
        {
            var page = StartPage(report, pages, firstPage: false);
            page.Y = DrawSectionTitle(page.Graphics, page.Y, sectionTitle);
            page.Y += 10;
            DrawTableHeader(page.Graphics, page.Y, titles, columns);
            page.Y += TableHeaderHeight;
            return page;
        }

        // ── HEADER / KPI / FOOTER ────────────────────────────────────────────

        private static void DrawPageHeader(Graphics g, RevenueReportDto report)
        {
            using (var gradient = new LinearGradientBrush(
                new Rectangle(0, 0, ImageWidth, HeaderHeight),
                Color.FromArgb(30, 64, 175), Primary, LinearGradientMode.Horizontal))
            {
                g.FillRectangle(gradient, 0, 0, ImageWidth, HeaderHeight);
            }
            using (var accentBar = new SolidBrush(Accent))
            {
                g.FillRectangle(accentBar, 0, HeaderHeight - 6, ImageWidth, 6);
            }

            using var white = new SolidBrush(Color.White);
            using var softWhite = new SolidBrush(Color.FromArgb(219, 234, 254));
            using var fBrand = FontOf(19, FontStyle.Bold);
            using var fTitle = FontOf(42, FontStyle.Bold);
            using var fPeriod = FontOf(23, FontStyle.Bold);
            using var fMeta = FontOf(17);
            using var rightAlign = new StringFormat { Alignment = StringAlignment.Far };

            g.DrawString("QLVT BUS LINES  •  HỆ THỐNG QUẢN LÝ VẬN TẢI HÀNH KHÁCH",
                fBrand, softWhite, Margin, 30);
            g.DrawString("BÁO CÁO DOANH THU VẬN TẢI", fTitle, white, Margin - 2, 62);

            string period = $"Kỳ báo cáo: {report.FromDate:dd/MM/yyyy} – {report.ToDate:dd/MM/yyyy}";
            g.DrawString(period, fPeriod, white, Margin, 128);

            string createdBy = string.IsNullOrWhiteSpace(report.GeneratedBy) ? "Quản trị viên" : report.GeneratedBy;
            g.DrawString($"Người lập: {createdBy}", fMeta, white,
                new RectangleF(0, 46, ImageWidth - Margin, 26), rightAlign);
            g.DrawString($"Thời điểm xuất: {report.GeneratedAt:dd/MM/yyyy HH:mm}", fMeta, white,
                new RectangleF(0, 76, ImageWidth - Margin, 26), rightAlign);
            g.DrawString($"Số ngày có doanh thu: {report.DailyRevenue.Count(d => d.Revenue > 0)}", fMeta, white,
                new RectangleF(0, 106, ImageWidth - Margin, 26), rightAlign);
        }

        private static void DrawKpiCards(Graphics g, DashboardSummaryDto summary)
        {
            const int y = 216;
            const int height = 142;
            const int gap = 26;
            int cardWidth = (ImageWidth - Margin * 2 - gap * 3) / 4;

            var cards = new (string Title, string Value, string Sub, Color Color)[]
            {
                ("TỔNG DOANH THU", Money(summary.TotalRevenue), $"Hôm nay: {Money(summary.TodayRevenue)}", Primary),
                ("SỐ VÉ ĐÃ BÁN", summary.TotalTickets.ToString("N0", Vi), $"Hôm nay: {summary.TodayTickets.ToString("N0", Vi)} vé", Accent),
                ("TUYẾN ĐANG CHẠY", summary.ActiveRoutes.ToString("N0", Vi), "Tuyến có trạng thái Active", Warm),
                ("XE ĐANG HOẠT ĐỘNG", summary.ActiveVehicles.ToString("N0", Vi), "Phương tiện sẵn sàng khai thác", Purple)
            };

            using var fTitle = FontOf(17, FontStyle.Bold);
            using var fValue = FontOf(33, FontStyle.Bold);
            using var fSub = FontOf(15);
            using var bTitle = new SolidBrush(Muted);
            using var bSub = new SolidBrush(Muted);
            using var borderPen = new Pen(Line);

            for (int i = 0; i < cards.Length; i++)
            {
                int x = Margin + i * (cardWidth + gap);

                using (var background = new SolidBrush(Color.White))
                    g.FillRectangle(background, x, y, cardWidth, height);
                g.DrawRectangle(borderPen, x, y, cardWidth, height);

                using (var bar = new SolidBrush(cards[i].Color))
                    g.FillRectangle(bar, x, y, cardWidth, 7);

                using var bValue = new SolidBrush(cards[i].Color);
                g.DrawString(cards[i].Title, fTitle, bTitle, x + 20, y + 24);
                g.DrawString(cards[i].Value, fValue, bValue, x + 19, y + 50);
                g.DrawString(cards[i].Sub, fSub, bSub, x + 20, y + 108);
            }
        }

        private static void DrawFooter(Graphics g, RevenueReportDto report, int page, int total)
        {
            int y = ImageHeight - 58;

            using (var pen = new Pen(Line))
                g.DrawLine(pen, Margin, y - 10, ImageWidth - Margin, y - 10);

            using var font = FontOf(15);
            using var brush = new SolidBrush(Muted);
            using var center = new StringFormat { Alignment = StringAlignment.Center };
            using var right = new StringFormat { Alignment = StringAlignment.Far };

            g.DrawString("QLVT — Báo cáo doanh thu vận tải hành khách", font, brush, Margin, y);
            g.DrawString($"Trang {page}/{total}", font, brush, new RectangleF(0, y, ImageWidth, 22), center);
            g.DrawString($"Xuất lúc {report.GeneratedAt:dd/MM/yyyy HH:mm}", font, brush,
                new RectangleF(0, y, ImageWidth - Margin, 22), right);
        }

        // ── TIÊU ĐỀ MỤC & BẢNG ───────────────────────────────────────────────

        private static int DrawSectionTitle(Graphics g, int y, string title)
        {
            using (var bar = new SolidBrush(Primary))
                g.FillRectangle(bar, Margin, y + 2, 8, 30);

            using var font = FontOf(27, FontStyle.Bold);
            using var brush = new SolidBrush(Dark);
            g.DrawString(title, font, brush, Margin + 20, y - 3);

            return y + 40;
        }

        private static void DrawTableHeader(
            Graphics g, int y, string[] titles, (int X, int W, bool Right)[] columns)
        {
            using (var background = new SolidBrush(Primary))
                g.FillRectangle(background, Margin, y, TableWidth, TableHeaderHeight);

            using var font = FontOf(20, FontStyle.Bold);
            using var brush = new SolidBrush(Color.White);

            for (int i = 0; i < columns.Length; i++)
            {
                DrawCell(g, y, TableHeaderHeight, columns[i].X, columns[i].W, columns[i].Right, titles[i], font, brush);
            }
        }

        private static void DrawDailyRow(Graphics g, int y, RevenueByDateDto day, decimal total, int rowIndex)
        {
            FillRowBackground(g, y, rowIndex);

            using var font = FontOf(20);
            using var brush = new SolidBrush(Dark);

            string date = day.Date.ToString("dddd, dd/MM/yyyy", Vi);
            string tickets = day.TicketCount.ToString("N0", Vi);
            string revenue = Money(day.Revenue);
            string percentage = FormatPercentage(day.Revenue, total);

            DrawCell(g, y, RowHeight, DailyColumns[0].X, DailyColumns[0].W, false, date, font, brush);
            DrawCell(g, y, RowHeight, DailyColumns[1].X, DailyColumns[1].W, true, tickets, font, brush);
            DrawCell(g, y, RowHeight, DailyColumns[2].X, DailyColumns[2].W, true, revenue, font, brush);
            DrawCell(g, y, RowHeight, DailyColumns[3].X, DailyColumns[3].W, true, percentage, font, brush);
        }

        private static void DrawRouteRow(Graphics g, int y, RevenueByRouteDto route, decimal total, int rowIndex)
        {
            FillRowBackground(g, y, rowIndex);

            using var font = FontOf(20);
            using var brush = new SolidBrush(Dark);

            string percentage = route.Percentage > 0
                ? $"{route.Percentage.ToString("0.0", Vi)}%"
                : FormatPercentage(route.Revenue, total);

            DrawCell(g, y, RowHeight, RouteColumns[0].X, RouteColumns[0].W, false, route.RouteCode, font, brush);
            DrawCell(g, y, RowHeight, RouteColumns[1].X, RouteColumns[1].W, false, route.RouteName, font, brush);
            DrawCell(g, y, RowHeight, RouteColumns[2].X, RouteColumns[2].W, true, route.TicketCount.ToString("N0", Vi), font, brush);
            DrawCell(g, y, RowHeight, RouteColumns[3].X, RouteColumns[3].W, true, Money(route.Revenue), font, brush);
            DrawCell(g, y, RowHeight, RouteColumns[4].X, RouteColumns[4].W, true, percentage, font, brush);
        }

        private static void DrawDailyTotalRow(Graphics g, int y, List<RevenueByDateDto> days, decimal total)
        {
            FillRowBackground(g, y, 0, TotalsRow);

            using var font = FontOf(20, FontStyle.Bold);
            using var brush = new SolidBrush(Dark);

            int ticketSum = days.Sum(d => d.TicketCount);

            DrawCell(g, y, RowHeight, DailyColumns[0].X, DailyColumns[0].W, false, "TỔNG CỘNG", font, brush);
            DrawCell(g, y, RowHeight, DailyColumns[1].X, DailyColumns[1].W, true, ticketSum.ToString("N0", Vi), font, brush);
            DrawCell(g, y, RowHeight, DailyColumns[2].X, DailyColumns[2].W, true, Money(total), font, brush);
            DrawCell(g, y, RowHeight, DailyColumns[3].X, DailyColumns[3].W, true, total > 0 ? "100%" : "0%", font, brush);
        }

        private static void DrawRouteTotalRow(Graphics g, int y, List<RevenueByRouteDto> routes, decimal total)
        {
            FillRowBackground(g, y, 0, TotalsRow);

            using var font = FontOf(20, FontStyle.Bold);
            using var brush = new SolidBrush(Dark);

            int ticketSum = routes.Sum(r => r.TicketCount);

            DrawCell(g, y, RowHeight, RouteColumns[0].X, RouteColumns[0].W, false, "TỔNG CỘNG", font, brush);
            DrawCell(g, y, RowHeight, RouteColumns[1].X, RouteColumns[1].W, false, $"{routes.Count} tuyến", font, brush);
            DrawCell(g, y, RowHeight, RouteColumns[2].X, RouteColumns[2].W, true, ticketSum.ToString("N0", Vi), font, brush);
            DrawCell(g, y, RowHeight, RouteColumns[3].X, RouteColumns[3].W, true, Money(total), font, brush);
            DrawCell(g, y, RowHeight, RouteColumns[4].X, RouteColumns[4].W, true, total > 0 ? "100%" : "0%", font, brush);
        }

        private static void DrawEmptyRow(Graphics g, int y, string message)
        {
            FillRowBackground(g, y, 0);

            using var font = FontOf(19, FontStyle.Italic);
            using var brush = new SolidBrush(Muted);
            DrawCell(g, y, RowHeight, Margin + 16, TableWidth - 32, false, message, font, brush);
        }

        private static void FillRowBackground(Graphics g, int y, int rowIndex, Color? fillOverride = null)
        {
            Color fill = fillOverride ?? (rowIndex % 2 == 1 ? RowAlt : Color.White);
            using (var background = new SolidBrush(fill))
                g.FillRectangle(background, Margin, y, TableWidth, RowHeight);

            using var pen = new Pen(Line);
            g.DrawLine(pen, Margin, y + RowHeight, Margin + TableWidth, y + RowHeight);
        }

        private static void DrawCell(
            Graphics g, int rowY, int rowHeight, int x, int width, bool rightAlign,
            string text, Font font, Brush brush)
        {
            using var format = new StringFormat
            {
                Alignment = rightAlign ? StringAlignment.Far : StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter,
                FormatFlags = StringFormatFlags.NoWrap
            };

            var rect = new RectangleF(x, rowY, width, rowHeight);
            g.DrawString(text ?? string.Empty, font, brush, rect, format);
        }

        // ── TIỆN ÍCH ──────────────────────────────────────────────────────────

        private static Font FontOf(float pixelSize, FontStyle style = FontStyle.Regular)
            => new("Segoe UI", pixelSize, style, GraphicsUnit.Pixel);

        private static string Money(decimal value) => value.ToString("N0", Vi) + " đ";

        private static string FormatPercentage(decimal value, decimal total)
        {
            if (total <= 0) return "0.0%";
            double percent = (double)value / (double)total * 100;
            return percent.ToString("0.0", Vi) + "%";
        }
    }
}
