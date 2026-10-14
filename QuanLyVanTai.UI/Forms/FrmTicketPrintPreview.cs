using System.ComponentModel;
using System.Drawing.Printing;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;

namespace QuanLyVanTai.UI.Forms
{
    public partial class FrmTicketPrintPreview : Form
    {
        private readonly List<TicketReceiptDto> _tickets = new();
        private PrintDocument? _printDocument;
        private int _currentPage = 1;
        private int _totalPages = 1;

        public FrmTicketPrintPreview()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || this.DesignMode)
            {
                return;
            }
        }

        public FrmTicketPrintPreview(List<TicketReceiptDto> tickets) : this()
        {
            _tickets = tickets ?? new List<TicketReceiptDto>();
            _totalPages = Math.Max(1, _tickets.Count);

            this.Load += FrmTicketPrintPreview_Load;
            WireToolbarEvents();
        }

        private void FrmTicketPrintPreview_Load(object? sender, EventArgs e)
        {
            try
            {
                _printDocument = TicketPdfExportService.CreatePrintDocument(_tickets);
                printPreviewControl.Document = _printDocument;
                printPreviewControl.Zoom = 1.0;
                printPreviewControl.StartPage = 0;

                lblTicketSummary.Text = $"Tổng cộng: {_tickets.Count} vé điện tử";
                UpdatePaginationControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo xem trước vé: " + ex.Message, "Lỗi In Ấn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WireToolbarEvents()
        {
            // In trực tiếp
            btnPrint.Click += (s, e) => ExecutePrint();

            // Xuất file PDF / Ảnh
            btnExportPdf.Click += (s, e) => ExecuteExportPdf();

            // Phóng to / Thu nhỏ / Vừa trang
            btnZoomIn.Click += (s, e) =>
            {
                printPreviewControl.AutoZoom = false;
                printPreviewControl.Zoom = Math.Min(3.0, printPreviewControl.Zoom + 0.2);
            };

            btnZoomOut.Click += (s, e) =>
            {
                printPreviewControl.AutoZoom = false;
                printPreviewControl.Zoom = Math.Max(0.4, printPreviewControl.Zoom - 0.2);
            };

            btnFit.Click += (s, e) =>
            {
                printPreviewControl.AutoZoom = true;
            };

            // Chuyển trang
            btnPrevPage.Click += (s, e) =>
            {
                if (_currentPage > 1)
                {
                    _currentPage--;
                    printPreviewControl.StartPage = _currentPage - 1;
                    UpdatePaginationControls();
                }
            };

            btnNextPage.Click += (s, e) =>
            {
                if (_currentPage < _totalPages)
                {
                    _currentPage++;
                    printPreviewControl.StartPage = _currentPage - 1;
                    UpdatePaginationControls();
                }
            };

            // Đóng
            btnClose.Click += (s, e) => this.Close();
        }

        private void UpdatePaginationControls()
        {
            lblPageInfo.Text = $"Trang {_currentPage} / {_totalPages}";
            btnPrevPage.Enabled = (_currentPage > 1);
            btnNextPage.Enabled = (_currentPage < _totalPages);
        }

        private void ExecutePrint()
        {
            if (_printDocument == null || _tickets.Count == 0) return;

            using var printDlg = new PrintDialog();
            printDlg.Document = _printDocument;
            printDlg.AllowSomePages = true;
            printDlg.UseEXDialog = true;

            if (printDlg.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    _printDocument.Print();
                    MessageBox.Show("Đã gửi lệnh in vé tới máy in thành công!", "In Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi trong quá trình gửi lệnh in: " + ex.Message, "Lỗi In Ấn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExecuteExportPdf()
        {
            if (_tickets.Count == 0) return;

            using var saveDlg = new SaveFileDialog();
            saveDlg.Title = "Chọn nơi lưu tệp Vé Điện Tử PDF";
            saveDlg.Filter = "Tài liệu PDF (*.pdf)|*.pdf|Ảnh PNG Vé Xe (*.png)|*.png";
            saveDlg.FileName = $"VeDienTu_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

            if (saveDlg.ShowDialog(this) != DialogResult.OK) return;

            string filePath = saveDlg.FileName;
            string extension = Path.GetExtension(filePath).ToLowerInvariant();

            if (extension == ".pdf")
            {
                ExportViaMicrosoftPrintToPdf(filePath);
            }
            else
            {
                ExportAsImages(filePath);
            }
        }

        private void ExportViaMicrosoftPrintToPdf(string pdfPath)
        {
            try
            {
                // Kiểm tra xem hệ thống có máy in "Microsoft Print to PDF" không
                string pdfPrinter = "Microsoft Print to PDF";
                bool hasPdfPrinter = PrinterSettings.InstalledPrinters.Cast<string>()
                    .Any(p => p.Equals(pdfPrinter, StringComparison.OrdinalIgnoreCase));

                if (!hasPdfPrinter)
                {
                    // Fallback nếu máy Windows không cài Microsoft Print to PDF -> lưu dạng ảnh vé sắc nét
                    string imgPath = Path.ChangeExtension(pdfPath, ".png");
                    ExportAsImages(imgPath);
                    MessageBox.Show($"Máy tính không có sẵn máy in ảo 'Microsoft Print to PDF'.\nHệ thống đã tự động xuất vé dưới dạng ảnh chất lượng cao sắc nét tại:\n{imgPath}",
                        "Xuất Vé Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var pdfDoc = TicketPdfExportService.CreatePrintDocument(_tickets);
                pdfDoc.PrinterSettings.PrinterName = pdfPrinter;
                pdfDoc.PrinterSettings.PrintToFile = true;
                pdfDoc.PrinterSettings.PrintFileName = pdfPath;

                pdfDoc.Print();

                MessageBox.Show($"Xuất vé xe điện tử PDF thành công!\nĐường dẫn: {pdfPath}",
                    "Xuất PDF Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Fallback nếu có lỗi quyền ghi tệp PDF của driver máy in
                string imgPath = Path.ChangeExtension(pdfPath, ".png");
                ExportAsImages(imgPath);
                MessageBox.Show($"Lỗi driver PDF ({ex.Message}). Đã lưu file ảnh vé thay thế tại:\n{imgPath}",
                    "Thông Báo Lưu File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ExportAsImages(string basePath)
        {
            string dir = Path.GetDirectoryName(basePath) ?? string.Empty;
            string nameWithoutExt = Path.GetFileNameWithoutExtension(basePath);

            for (int i = 0; i < _tickets.Count; i++)
            {
                var ticket = _tickets[i];
                string outPath = (_tickets.Count == 1)
                    ? basePath
                    : Path.Combine(dir, $"{nameWithoutExt}_Ve_{ticket.SeatNumber}.png");

                // Tạo bitmap kích thước phiếu vé đứng chuẩn sắc nét
                using var bmp = new Bitmap(600, 880);
                using var g = Graphics.FromImage(bmp);
                g.Clear(Color.White);

                TicketPdfExportService.DrawTicket(g, ticket, new Rectangle(0, 0, bmp.Width, bmp.Height));
                bmp.Save(outPath, System.Drawing.Imaging.ImageFormat.Png);
            }

            MessageBox.Show($"Đã xuất {_tickets.Count} vé điện tử thành công ra file ảnh sắc nét!",
                "Xuất Vé Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
