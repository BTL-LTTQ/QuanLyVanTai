using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Text;
using QRCoder;
using QuanLyVanTai.BLL.DTOs;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// Service sinh mã QR, tạo đồ họa vé điện tử GDI+ và in / xuất PDF
    /// </summary>
    public class TicketPdfExportService
    {
        /// <summary>
        /// Sinh ảnh QR Code từ chuỗi dữ liệu bằng thư viện QRCoder thuần C#
        /// </summary>
        public static Bitmap GenerateQrCode(string content, int pixelsPerModule = 6)
        {
            if (string.IsNullOrWhiteSpace(content))
                content = "QLVT|EMPTY";

            using var qrGenerator = new QRCodeGenerator();
            // ECCLevel.M (15% sửa lỗi) là chuẩn công nghiệp tối ưu cho vé điện tử:
            // Ô module to, rõ ràng, ma trận thông thoáng, giúp camera điện thoại (Zalo/iOS/Android) bắt nét cực nhạy
            using var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.M);
            
            // QRCoder v1.6+: QRCode.GetGraphic(pixelsPerModule)
            using var qrCode = new QRCode(qrCodeData);
            return qrCode.GetGraphic(pixelsPerModule, Color.Black, Color.White, true);
        }

        /// <summary>
        /// Vẽ toàn bộ mẫu vé điện tử chuẩn theo mẫu vé in nhiệt của hệ thống Quản Lý Vận Tải (QLVT)
        /// </summary>
        public static void DrawTicket(Graphics g, TicketReceiptDto ticket, Rectangle bounds)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Kích thước chuẩn cho phiếu vé in nhiệt dạng đứng (Thermal Receipt)
            int ticketWidth = Math.Min(bounds.Width, 580);
            int ticketHeight = Math.Min(bounds.Height, 840);
            if (ticketWidth < 350) ticketWidth = bounds.Width;
            if (ticketHeight < 500) ticketHeight = bounds.Height;

            int left = bounds.Left + Math.Max(0, (bounds.Width - ticketWidth) / 2);
            int top = bounds.Top + Math.Max(0, (bounds.Height - ticketHeight) / 2);
            int right = left + ticketWidth;

            // 1. Nền giấy in vé trắng tinh
            using (var bgBrush = new SolidBrush(Color.White))
            {
                g.FillRectangle(bgBrush, left, top, ticketWidth, ticketHeight);
            }
            using (var borderPen = new Pen(Color.FromArgb(226, 232, 240), 1f))
            {
                g.DrawRectangle(borderPen, left, top, ticketWidth, ticketHeight);
            }

            // Bảng màu nhận diện chuẩn dự án Quản Lý Vận Tải (QLVT)
            Color themePrimary = Color.FromArgb(16, 124, 65);  // Xanh lá chuẩn vận tải hành khách
            Color themeAccent = Color.FromArgb(220, 38, 38);   // Đỏ cam điểm nhấn
            Color labelGray = Color.FromArgb(100, 116, 139);
            Color textDark = Color.FromArgb(15, 23, 42);

            // 2. Dải hoa văn trang trí ở mép trên
            DrawZebraStripe(g, left + 12, top + 8, ticketWidth - 24, themePrimary);

            // 3. Header: Thương hiệu dự án QLVT (Trái) & Tên công ty (Phải)
            using (var fontBrand = new Font("Segoe UI", 13.5f, FontStyle.Bold))
            using (var fontSlogan = new Font("Segoe UI", 7.5f, FontStyle.Bold))
            using (var fontCompany1 = new Font("Segoe UI", 8f, FontStyle.Bold))
            using (var fontCompany2 = new Font("Segoe UI", 8.5f, FontStyle.Bold))
            using (var brushPrimary = new SolidBrush(themePrimary))
            using (var brushAccent = new SolidBrush(themeAccent))
            {
                // Bên trái: Thương hiệu dự án
                g.DrawString("QLVT BUS LINES", fontBrand, brushAccent, left + 15, top + 18);
                g.DrawString("CHẤT LƯỢNG - AN TOÀN - TẬN TÂM", fontSlogan, brushPrimary, left + 17, top + 40);

                // Bên phải: Tên pháp nhân theo đồ án
                string compLine1 = "CÔNG TY CỔ PHẦN VẬN TẢI HÀNH KHÁCH";
                string compLine2 = "HỆ THỐNG VẬN TẢI ĐƯỜNG BỘ QLVT";
                var sizeLine1 = g.MeasureString(compLine1, fontCompany1);
                var sizeLine2 = g.MeasureString(compLine2, fontCompany2);

                g.DrawString(compLine1, fontCompany1, brushPrimary, right - sizeLine1.Width - 15, top + 18);
                g.DrawString(compLine2, fontCompany2, brushPrimary, right - sizeLine2.Width - 15, top + 34);
            }

            // 4. Tiêu đề vé & Thông tin tuyến + Mã QR bên phải
            int headerSectionY = top + 64;

            // Xử lý thông tin nơi đi, nơi đến lấy chuẩn xác từ dữ liệu Database
            string fromStation = !string.IsNullOrWhiteSpace(ticket.DepartureStation) && !ticket.DepartureStation.Contains("xuất phát") && !ticket.DepartureStation.Contains("trung tâm")
                ? ticket.DepartureStation
                : (ticket.RouteName.Contains('-') ? ticket.RouteName.Split('-')[0].Trim() : "BẾN XE XUẤT PHÁT");

            string toStation = !string.IsNullOrWhiteSpace(ticket.ArrivalStation) && !ticket.ArrivalStation.Contains("đích") && !ticket.ArrivalStation.Contains("đến")
                ? ticket.ArrivalStation
                : (ticket.RouteName.Contains('-') ? ticket.RouteName.Split('-')[1].Split('(')[0].Trim() : "BẾN XE ĐẾN");

            string routeDisplay = !string.IsNullOrWhiteSpace(ticket.RouteName)
                ? ticket.RouteName
                : $"{fromStation} - {toStation}";

            string plateDisplay = !string.IsNullOrWhiteSpace(ticket.LicensePlate)
                ? ticket.LicensePlate
                : "Đang điều xe";

            // Mã QR và Mã vé đặt ở góc phải (kích thước lớn, viền Quiet Zone chuẩn)
            int qrSize = 110;
            int qrX = right - qrSize - 18;
            int qrY = headerSectionY;

            try
            {
                // Hộp nền trắng tinh + viền nhẹ tạo Quiet Zone rõ ràng cho camera điện thoại nhận dạng
                using (var whiteBrush = new SolidBrush(Color.White))
                using (var borderPen = new Pen(Color.FromArgb(226, 232, 240), 1f))
                {
                    g.FillRectangle(whiteBrush, qrX - 4, qrY - 4, qrSize + 8, qrSize + 8);
                    g.DrawRectangle(borderPen, qrX - 4, qrY - 4, qrSize + 8, qrSize + 8);
                }

                using var qrBmp = GenerateQrCode(ticket.QrCodeContent, 6);

                // Quan trọng: Sử dụng NearestNeighbor để các ô vuông mã QR sắc nét 100%, không bị nhòe mờ do Bicubic
                var prevInterpolation = g.InterpolationMode;
                var prevSmoothing = g.SmoothingMode;
                var prevPixelOffset = g.PixelOffsetMode;

                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.SmoothingMode = SmoothingMode.None;
                g.PixelOffsetMode = PixelOffsetMode.Half;

                g.DrawImage(qrBmp, qrX, qrY, qrSize, qrSize);

                g.InterpolationMode = prevInterpolation;
                g.SmoothingMode = prevSmoothing;
                g.PixelOffsetMode = prevPixelOffset;
            }
            catch
            {
                g.DrawRectangle(Pens.Gray, qrX, qrY, qrSize, qrSize);
            }

            // Chữ "MÃ VÉ/ CODE" và số mã vé in đậm dưới QR
            using (var fontCodeLabel = new Font("Segoe UI", 7.5f, FontStyle.Regular))
            using (var fontCodeVal = new Font("Segoe UI", 12f, FontStyle.Bold))
            using (var brushLabel = new SolidBrush(labelGray))
            using (var brushDark = new SolidBrush(textDark))
            {
                string codeLabel = "MÃ VÉ/ CODE:";
                var sizeLbl = g.MeasureString(codeLabel, fontCodeLabel);
                g.DrawString(codeLabel, fontCodeLabel, brushLabel, qrX + (qrSize - sizeLbl.Width) / 2, qrY + qrSize + 2);

                string codeVal = string.IsNullOrWhiteSpace(ticket.TicketCode) ? "TK" + ticket.TicketId.ToString("D8") : ticket.TicketCode;
                var sizeVal = g.MeasureString(codeVal, fontCodeVal);
                g.DrawString(codeVal, fontCodeVal, brushDark, qrX + (qrSize - sizeVal.Width) / 2, qrY + qrSize + 16);
            }

            // Bên trái của QR: Tiêu đề THẺ LÊN XE, Tuyến đường, Xe
            using (var fontBoardingPass = new Font("Segoe UI", 12f, FontStyle.Bold))
            using (var fontRoute = new Font("Segoe UI", 13.5f, FontStyle.Bold))
            using (var fontBus = new Font("Segoe UI", 11.5f, FontStyle.Bold))
            using (var brushAccent = new SolidBrush(themeAccent))
            using (var brushDark = new SolidBrush(textDark))
            {
                g.DrawString("THẺ LÊN XE/ BOARDING PASS", fontBoardingPass, brushAccent, left + 15, headerSectionY);
                g.DrawString(routeDisplay, fontRoute, brushDark, left + 15, headerSectionY + 24);
                g.DrawString($"XE/BUS: {plateDisplay}", fontBus, brushDark, left + 15, headerSectionY + 50);
            }

            // 5. Khối chi tiết hành trình (Labels cột trái, Giá trị cột phải)
            int curY = headerSectionY + 115;
            int labelX = left + 15;
            int valueX = left + 185;

            using (var fontLbl = new Font("Segoe UI", 8.5f, FontStyle.Regular))
            using (var fontVal = new Font("Segoe UI", 11.5f, FontStyle.Bold))
            using (var fontBigTime = new Font("Segoe UI", 14f, FontStyle.Bold))
            using (var fontSeat = new Font("Segoe UI", 14f, FontStyle.Bold))
            using (var fontPrice = new Font("Segoe UI", 12.5f, FontStyle.Bold))
            using (var fontSubNote = new Font("Segoe UI", 8f, FontStyle.Italic))
            using (var fontSmallBold = new Font("Segoe UI", 9.5f, FontStyle.Bold))
            using (var brushLbl = new SolidBrush(labelGray))
            using (var brushDark = new SolidBrush(textDark))
            using (var brushAccent = new SolidBrush(themeAccent))
            {
                // Dòng 1: NƠI ĐI/ FROM (Lấy từ dữ liệu thực tế)
                g.DrawString("NƠI ĐI/ FROM:", fontLbl, brushLbl, labelX, curY);
                g.DrawString(fromStation.ToUpper(), fontVal, brushDark, valueX, curY - 2);
                curY += 32;

                // Dòng 2: NƠI ĐẾN/ TO (Lấy từ dữ liệu thực tế)
                g.DrawString("NƠI ĐẾN/ TO:", fontLbl, brushLbl, labelX, curY);
                g.DrawString(toStation.ToUpper(), fontVal, brushDark, valueX, curY - 2);
                curY += 34;

                // Dòng 3: KHỞI HÀNH/ DEPARTURE TIME: Giờ in to phía trên, gạch ngang, ngày ở dưới
                g.DrawString("KHỞI HÀNH/ DEPARTURE TIME:", fontLbl, brushLbl, labelX, curY + 6);
                string timeStr = ticket.DepartureTime.ToString("HH:mm");
                string dateStr = ticket.DepartureTime.ToString("dd-MM-yyyy");

                int timeValX = valueX + 40;
                g.DrawString(timeStr, fontBigTime, brushDark, timeValX, curY - 4);
                using (var penLine = new Pen(Color.FromArgb(71, 85, 105), 1.2f))
                {
                    g.DrawLine(penLine, timeValX - 25, curY + 22, timeValX + 110, curY + 22);
                }
                g.DrawString(dateStr, fontVal, brushDark, timeValX - 10, curY + 25);
                curY += 56;

                // Dòng 4: GHẾ/ SEAT & GIÁ VÉ/ PRICE
                g.DrawString("GHẾ/ SEAT:", fontLbl, brushLbl, labelX, curY + 2);
                string seatDisplay = ticket.SeatNumber;
                if (ticket.Floor > 1) seatDisplay += $" (T{ticket.Floor})";
                g.DrawString(seatDisplay, fontSeat, brushDark, valueX, curY - 2);

                int priceLabelX = left + 270;
                g.DrawString("GIÁ VÉ/ PRICE:", fontLbl, brushLbl, priceLabelX, curY + 2);
                g.DrawString($"{ticket.FinalPrice:N0} VNĐ", fontPrice, brushDark, priceLabelX + 90, curY - 1);
                curY += 36;

                // Dòng 5: HỌ TÊN/ NAME
                g.DrawString("HỌ TÊN/ NAME:", fontLbl, brushLbl, labelX, curY);
                string custName = string.IsNullOrWhiteSpace(ticket.CustomerName) ? "HÀNH KHÁCH" : ticket.CustomerName.ToUpper();
                g.DrawString(custName, fontVal, brushDark, valueX, curY - 2);
                curY += 34;

                // Dòng 6: LÊN XE TẠI/ PICK UP POINT
                g.DrawString("LÊN XE TẠI/ PICK UP POINT:", fontLbl, brushLbl, labelX, curY);
                g.DrawString($"Lên xe: {fromStation}", fontVal, brushDark, valueX + 35, curY - 2);
                curY += 24;

                // Dặn dò có mặt tại văn phòng
                g.DrawString("Hành khách có mặt tại văn phòng trước giờ khởi hành", fontSubNote, brushAccent, labelX + 10, curY);
                curY += 16;
                g.DrawString("To be present at this office before departure time", fontSubNote, brushAccent, labelX + 10, curY);
                curY += 32;

                // 6. Thông tin nhân viên & ngày in
                g.DrawString("NHÂN VIÊN/ EMPLOYEE:", fontLbl, brushLbl, labelX, curY);
                string staff = string.IsNullOrWhiteSpace(ticket.StaffName) ? "NHÂN VIÊN PHÒNG VÉ" : ticket.StaffName.ToUpper();
                g.DrawString(staff, fontSmallBold, brushDark, valueX, curY - 1);
                curY += 26;

                g.DrawString("NGÀY IN/ PRINTED DATE:", fontLbl, brushLbl, labelX, curY);
                string printedDate = $"{ticket.BookingTime:HH:mm dd/MM/yyyy} / {fromStation}";
                g.DrawString(printedDate, fontSmallBold, brushDark, valueX, curY - 1);
                curY += 36;

                // 7. Ghi chú hóa đơn điện tử
                using (var fontNote = new Font("Segoe UI", 7.5f, FontStyle.Italic))
                {
                    string noteVn = "Ghi chú: Để tra cứu thông tin vé và xuất hóa đơn điện tử, quý khách vui lòng";
                    string noteVn2 = "truy cập website hệ thống Quản Lý Vận Tải hoặc liên hệ phòng vé bến xe.";
                    string noteEn = "To look up ticket information and receive an e-invoice, please access our website";
                    string noteEn2 = "or contact the ticket office before departure.";

                    g.DrawString(noteVn, fontNote, brushAccent, labelX, curY);
                    curY += 15;
                    g.DrawString(noteVn2, fontNote, brushAccent, labelX, curY);
                    curY += 15;
                    g.DrawString(noteEn, fontNote, brushAccent, labelX, curY);
                    curY += 15;
                    g.DrawString(noteEn2, fontNote, brushAccent, labelX, curY);
                    curY += 25;
                }

                // 8. Chân vé: Website, hotline, kênh hỗ trợ của dự án QLVT
                using (var fontFooter = new Font("Segoe UI", 8f, FontStyle.Regular))
                using (var fontHotlineLbl = new Font("Segoe UI", 7.5f, FontStyle.Bold))
                using (var fontHotline = new Font("Segoe UI", 14f, FontStyle.Bold))
                using (var brushPrimary = new SolidBrush(themePrimary))
                {
                    // Trái: Các kênh liên hệ
                    g.DrawString("🌐 qlvt.vn   ✉ hotro@qlvt.vn   ⓕ /quanlyvantai", fontFooter, brushPrimary, labelX, curY + 12);

                    // Phải: Tổng đài Contact Center
                    string hotlineLbl = "TỔNG ĐÀI/ CONTACT CENTER:";
                    var sizeHlLbl = g.MeasureString(hotlineLbl, fontHotlineLbl);
                    g.DrawString(hotlineLbl, fontHotlineLbl, brushPrimary, right - sizeHlLbl.Width - 15, curY);

                    string hotlineNum = "1900 6868";
                    var sizeHlNum = g.MeasureString(hotlineNum, fontHotline);
                    g.DrawString(hotlineNum, fontHotline, brushAccent, right - sizeHlNum.Width - 15, curY + 14);
                }

                // Viền ziczac trang trí ở mép dưới
                DrawZebraStripe(g, left + 12, top + ticketHeight - 16, ticketWidth - 24, themePrimary);

                // Nếu vé đã hủy, đóng dấu đỏ cảnh báo rõ ràng trên toàn bộ mặt vé
                if (ticket.TicketStatus == "Cancelled")
                {
                    using var cancelBrush = new SolidBrush(Color.FromArgb(170, 220, 38, 38));
                    using var cancelPen = new Pen(Color.FromArgb(190, 220, 38, 38), 4f);
                    using var fontCancelled = new Font("Segoe UI", 24f, FontStyle.Bold);

                    var state = g.Save();
                    g.TranslateTransform(left + ticketWidth / 2f, top + ticketHeight / 2f);
                    g.RotateTransform(-28f);
                    string stampText = "VÉ ĐÃ HỦY / VOID";
                    var sz = g.MeasureString(stampText, fontCancelled);
                    var rectStamp = new RectangleF(-sz.Width / 2f - 18, -sz.Height / 2f - 8, sz.Width + 36, sz.Height + 16);
                    g.DrawRectangle(cancelPen, rectStamp.X, rectStamp.Y, rectStamp.Width, rectStamp.Height);
                    g.DrawString(stampText, fontCancelled, cancelBrush, -sz.Width / 2f, -sz.Height / 2f);
                    g.Restore(state);
                }
            }
        }

        /// <summary>
        /// Vẽ hoa văn trang trí mép vé in nhiệt chuẩn phong cách vé xe đường bộ
        /// </summary>
        private static void DrawZebraStripe(Graphics g, int x, int y, int width, Color color)
        {
            using var brush = new SolidBrush(color);
            int blockWidth = 7;
            int blockHeight = 4;
            int gap = 5;

            for (int curX = x; curX + blockWidth <= x + width; curX += blockWidth + gap)
            {
                g.FillRectangle(brush, curX, y, blockWidth, blockHeight);
            }
        }

        /// <summary>
        /// Khởi tạo PrintDocument hỗ trợ in đa trang cho danh sách vé (chuẩn khổ đứng nhiệt)
        /// </summary>
        public static PrintDocument CreatePrintDocument(List<TicketReceiptDto> tickets)
        {
            var doc = new PrintDocument();
            // Khổ đứng chuẩn giấy in nhiệt / vé xe (rộng 600 x cao 860)
            doc.DefaultPageSettings.PaperSize = new PaperSize("QLVT_BoardingPass", 600, 860);
            doc.DefaultPageSettings.Landscape = false;
            doc.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);

            int currentTicketIndex = 0;

            doc.PrintPage += (sender, e) =>
            {
                if (e.Graphics == null || tickets.Count == 0 || currentTicketIndex >= tickets.Count)
                {
                    e.HasMorePages = false;
                    return;
                }

                var currentTicket = tickets[currentTicketIndex];
                Rectangle pageBounds = e.MarginBounds;

                DrawTicket(e.Graphics, currentTicket, pageBounds);

                currentTicketIndex++;
                e.HasMorePages = (currentTicketIndex < tickets.Count);
            };

            doc.BeginPrint += (s, e) =>
            {
                currentTicketIndex = 0;
            };

            return doc;
        }

        /// <summary>
        /// Sinh mảng byte file PDF vé điện tử chuẩn (PDF 1.4) độc lập 100% với driver máy in
        /// Có thể mở ngay bằng bất kỳ phần mềm đọc PDF nào (Adobe Acrobat, Chrome, Edge...)
        /// </summary>
        public static byte[] GenerateTicketPdf(List<TicketReceiptDto> tickets)
        {
            if (tickets == null || tickets.Count == 0)
                return Array.Empty<byte>();

            int width = 580;
            int height = 840;

            var pageJpegBytes = new List<byte[]>();

            var jpegCodec = ImageCodecInfo.GetImageEncoders()
                .FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

            using var encParams = new EncoderParameters(1);
            encParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 95L);

            foreach (var ticket in tickets)
            {
                using var bmp = new Bitmap(width, height);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    DrawTicket(g, ticket, new Rectangle(0, 0, width, height));
                }

                using var msJpeg = new MemoryStream();
                if (jpegCodec != null)
                {
                    bmp.Save(msJpeg, jpegCodec, encParams);
                }
                else
                {
                    bmp.Save(msJpeg, ImageFormat.Jpeg);
                }
                pageJpegBytes.Add(msJpeg.ToArray());
            }

            return BuildPdfFromJpegPages(pageJpegBytes, width, height);
        }

        /// <summary>
        /// Sinh file PDF cho một vé duy nhất
        /// </summary>
        public static byte[] GenerateSingleTicketPdf(TicketReceiptDto ticket)
        {
            return GenerateTicketPdf(new List<TicketReceiptDto> { ticket });
        }

        /// <summary>
        /// Lưu vé điện tử trực tiếp thành file PDF trên đĩa cứng
        /// </summary>
        public static void SaveTicketPdf(List<TicketReceiptDto> tickets, string filePath)
        {
            var bytes = GenerateTicketPdf(tickets);
            string? dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllBytes(filePath, bytes);
        }

        /// <summary>
        /// Đóng gói các trang ảnh JPEG thành tài liệu chuẩn PDF 1.4
        /// </summary>
        private static byte[] BuildPdfFromJpegPages(List<byte[]> pageJpegList, int width, int height)
        {
            int pageCount = pageJpegList.Count;
            if (pageCount == 0) return Array.Empty<byte>();

            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms, Encoding.ASCII, leaveOpen: true);

            // 1. Header chuẩn PDF 1.4
            writer.Write("%PDF-1.4\r\n");
            writer.Write("%\xE2\xE3\xCF\xD3\r\n");
            writer.Flush();

            int totalObjects = 2 + (pageCount * 3);
            long[] offsets = new long[totalObjects + 1];

            // Object 1: Catalog
            offsets[1] = ms.Position;
            writer.Write("1 0 obj\r\n<< /Type /Catalog /Pages 2 0 R >>\r\nendobj\r\n");
            writer.Flush();

            // Object 2: Pages
            offsets[2] = ms.Position;
            var sbKids = new StringBuilder();
            for (int i = 0; i < pageCount; i++)
            {
                int pId = 3 + (i * 3);
                sbKids.Append($"{pId} 0 R ");
            }
            writer.Write($"2 0 obj\r\n<< /Type /Pages /Kids [ {sbKids} ] /Count {pageCount} >>\r\nendobj\r\n");
            writer.Flush();

            // Cho từng trang: Page, Content Stream, Image XObject
            for (int i = 0; i < pageCount; i++)
            {
                int pageObjId = 3 + (i * 3);
                int contentObjId = pageObjId + 1;
                int imageObjId = pageObjId + 2;
                byte[] jpegData = pageJpegList[i];

                // Page Object
                offsets[pageObjId] = ms.Position;
                writer.Write($"{pageObjId} 0 obj\r\n<< /Type /Page /Parent 2 0 R /MediaBox [ 0 0 {width} {height} ] /Resources << /XObject << /Im1 {imageObjId} 0 R >> >> /Contents {contentObjId} 0 R >>\r\nendobj\r\n");
                writer.Flush();

                // Content Stream Object
                string contentStream = $"q\r\n{width} 0 0 {height} 0 0 cm\r\n/Im1 Do\r\nQ\r\n";
                byte[] contentBytes = Encoding.ASCII.GetBytes(contentStream);
                offsets[contentObjId] = ms.Position;
                writer.Write($"{contentObjId} 0 obj\r\n<< /Length {contentBytes.Length} >>\r\nstream\r\n");
                writer.Flush();
                ms.Write(contentBytes, 0, contentBytes.Length);
                writer.Write("\r\nendstream\r\nendobj\r\n");
                writer.Flush();

                // Image XObject Object
                offsets[imageObjId] = ms.Position;
                writer.Write($"{imageObjId} 0 obj\r\n<< /Type /XObject /Subtype /Image /Width {width} /Height {height} /ColorSpace /DeviceRGB /BitsPerComponent 8 /Filter /DCTDecode /Length {jpegData.Length} >>\r\nstream\r\n");
                writer.Flush();
                ms.Write(jpegData, 0, jpegData.Length);
                writer.Write("\r\nendstream\r\nendobj\r\n");
                writer.Flush();
            }

            // XRef Table
            long xrefPos = ms.Position;
            writer.Write($"xref\r\n0 {totalObjects + 1}\r\n");
            writer.Write("0000000000 65535 f \r\n");
            for (int objId = 1; objId <= totalObjects; objId++)
            {
                writer.Write($"{offsets[objId]:D10} 00000 n \r\n");
            }

            // Trailer
            writer.Write($"trailer\r\n<< /Size {totalObjects + 1} /Root 1 0 R >>\r\nstartxref\r\n{xrefPos}\r\n%%EOF\r\n");
            writer.Flush();

            return ms.ToArray();
        }
    }
}
