using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text;

namespace QuanLyVanTai.BLL.Services
{
    // =========================================================================
    // PdfImageDocumentBuilder — Dựng file PDF 1.4 tối giản từ các trang ảnh JPEG
    // =========================================================================
    // Dùng chung cho mọi báo cáo dạng "ảnh render" (vé điện tử, báo cáo doanh thu):
    //   • Mỗi trang là một ảnh JPEG được nhúng nguyên bản qua bộ lọc DCTDecode,
    //     nên file mở được bằng mọi trình đọc PDF mà không cần driver máy in
    //     hay thư viện PDF bên thứ ba (đã kiểm chứng với Adobe/Chrome/Edge).
    //   • Không phụ thuộc font hệ thống vì toàn bộ chữ đã được vẽ sẵn trên bitmap.
    // =========================================================================

    public static class PdfImageDocumentBuilder
    {
        /// <summary>Số byte mặc định của header PDF — dùng cho buffer ước lượng.</summary>
        private const int PdfHeaderLength = 15;

        /// <summary>
        /// Mã hóa một Bitmap thành JPEG trong bộ nhớ với chất lượng cho trước.
        /// </summary>
        /// <param name="bitmap">Ảnh nguồn (không bị Dispose bởi hàm này).</param>
        /// <param name="quality">Chất lượng JPEG 0..100, mặc định 95.</param>
        public static byte[] EncodeJpeg(Bitmap bitmap, long quality = 95L)
        {
            ArgumentNullException.ThrowIfNull(bitmap);
            if (quality < 0) quality = 0;
            if (quality > 100) quality = 100;

            var jpegCodec = ImageCodecInfo.GetImageEncoders()
                .FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

            using var ms = new MemoryStream();
            if (jpegCodec == null)
            {
                bitmap.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }

            using var encParams = new EncoderParameters(1);
            encParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            bitmap.Save(ms, jpegCodec, encParams);
            return ms.ToArray();
        }

        /// <summary>
        /// Đóng gói danh sách trang JPEG thành tài liệu PDF 1.4 — khổ trang trùng
        /// kích thước điểm ảnh (1 px = 1 pt). Dùng cho vé in nhiệt.
        /// </summary>
        public static byte[] BuildPdf(List<byte[]> pageJpegList, int width, int height)
            => BuildPdf(pageJpegList, width, height, width, height);

        /// <summary>
        /// Đóng gói danh sách trang JPEG thành tài liệu PDF 1.4 với khổ giấy tách
        /// biệt khỏi độ phân giải ảnh (ví dụ ảnh 1684×1190 px vẽ trên khổ A4 ngang
        /// 841.89×595.28 pt). Ảnh được phóng to phủ kín trang.
        /// </summary>
        /// <param name="pageJpegList">Bytes JPEG của từng trang, theo đúng thứ tự hiển thị.</param>
        /// <param name="imageWidth">Chiều rộng điểm ảnh của ảnh JPEG.</param>
        /// <param name="imageHeight">Chiều cao điểm ảnh của ảnh JPEG.</param>
        /// <param name="pageWidth">Chiều rộng khổ giấy tính bằng point (1/72 inch).</param>
        /// <param name="pageHeight">Chiều cao khổ giấy tính bằng point.</param>
        /// <returns>Nội dung file PDF hoàn chỉnh; mảng rỗng nếu không có trang nào.</returns>
        public static byte[] BuildPdf(
            List<byte[]> pageJpegList,
            int imageWidth,
            int imageHeight,
            double pageWidth,
            double pageHeight)
        {
            ArgumentNullException.ThrowIfNull(pageJpegList);
            if (pageJpegList.Count == 0) return Array.Empty<byte>();
            if (imageWidth <= 0) throw new ArgumentOutOfRangeException(nameof(imageWidth));
            if (imageHeight <= 0) throw new ArgumentOutOfRangeException(nameof(imageHeight));
            if (pageWidth <= 0) throw new ArgumentOutOfRangeException(nameof(pageWidth));
            if (pageHeight <= 0) throw new ArgumentOutOfRangeException(nameof(pageHeight));

            string mediaBox = string.Format(CultureInfo.InvariantCulture, "0 0 {0:0.###} {1:0.###}", pageWidth, pageHeight);
            string contentStream = string.Format(CultureInfo.InvariantCulture,
                "q\r\n{0:0.###} 0 0 {1:0.###} 0 0 cm\r\n/Im1 Do\r\nQ\r\n", pageWidth, pageHeight);

            int pageCount = pageJpegList.Count;

            // Ước lượng dung lượng vừa đủ để tránh MemoryStream phải cấp phát lại
            long estimatedSize = PdfHeaderLength + 1024 + pageJpegList.Sum(p => (long)p.Length + 512);
            using var ms = new MemoryStream((int)Math.Min(estimatedSize, int.MaxValue));
            using var writer = new StreamWriter(ms, Encoding.ASCII, leaveOpen: true);

            // 1. Header chuẩn PDF 1.4
            writer.Write("%PDF-1.4\r\n");
            writer.Write("%\xE2\xE3\xCF\xD3\r\n");
            writer.Flush();

            // 3 object / trang (Page + Content + Image) + Catalog + Pages
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
                writer.Write($"{pageObjId} 0 obj\r\n<< /Type /Page /Parent 2 0 R /MediaBox [ {mediaBox} ] /Resources << /XObject << /Im1 {imageObjId} 0 R >> >> /Contents {contentObjId} 0 R >>\r\nendobj\r\n");
                writer.Flush();

                // Content Stream Object — phóng ảnh phủ kín trang
                byte[] contentBytes = Encoding.ASCII.GetBytes(contentStream);
                offsets[contentObjId] = ms.Position;
                writer.Write($"{contentObjId} 0 obj\r\n<< /Length {contentBytes.Length} >>\r\nstream\r\n");
                writer.Flush();
                ms.Write(contentBytes, 0, contentBytes.Length);
                writer.Write("\r\nendstream\r\nendobj\r\n");
                writer.Flush();

                // Image XObject Object — nhúng nguyên bản JPEG
                offsets[imageObjId] = ms.Position;
                writer.Write($"{imageObjId} 0 obj\r\n<< /Type /XObject /Subtype /Image /Width {imageWidth} /Height {imageHeight} /ColorSpace /DeviceRGB /BitsPerComponent 8 /Filter /DCTDecode /Length {jpegData.Length} >>\r\nstream\r\n");
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
