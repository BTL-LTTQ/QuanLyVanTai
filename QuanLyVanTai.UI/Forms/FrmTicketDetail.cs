using System.ComponentModel;
using Core.Configs;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;

namespace QuanLyVanTai.UI.Forms
{
    public partial class FrmTicketDetail : Form
    {
        private readonly int _ticketId;
        private readonly TicketService _ticketService = new();
        private readonly CustomerBookingService _bookingService = new();
        private TicketReceiptDto? _ticketDto;

        public bool WasCancelled { get; private set; }

        public FrmTicketDetail() : this(0)
        {
        }

        public FrmTicketDetail(int ticketId)
        {
            InitializeComponent();
            _ticketId = ticketId;

            // 🛡️ BẢO VỆ DESIGNER: Chặn Designer thực thi logic runtime (SKILL Mục 2.2)
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || this.DesignMode)
            {
                return;
            }

            this.KeyPreview = true;
            SetupUi();
            WireEvents();
        }

        private void SetupUi()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = ThemeConfig.BackgroundColor;

            // Styling buttons theo chuẩn ThemeConfig
            ThemeConfig.StyleSuccessButton(btnInVe, IconChar.Print);
            btnInVe.Size = new Size(160, 42);

            ThemeConfig.StyleDangerButton(btnHuyVe, IconChar.TimesCircle);
            btnHuyVe.Size = new Size(140, 42);

            ThemeConfig.StyleSecondaryButton(btnDong, IconChar.Xmark);
            btnDong.Size = new Size(110, 42);
        }

        private void WireEvents()
        {
            this.Load += async (s, e) => await LoadTicketDataAsync();
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            };

            btnInVe.Click += BtnInVe_Click;
            btnHuyVe.Click += async (s, e) => await BtnHuyVe_Click();
            btnDong.Click += (s, e) => this.Close();
        }

        private async Task LoadTicketDataAsync()
        {
            if (_ticketId <= 0) return;

            try
            {
                var receipts = await _bookingService.GetTicketReceiptsAsync(new List<int> { _ticketId });
                _ticketDto = receipts.FirstOrDefault();
                if (_ticketDto == null)
                {
                    MessageBox.Show(
                        "Không tìm thấy thông tin chi tiết vé!",
                        "Lỗi Dữ Liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                BindTicketData(_ticketDto);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi tải thông tin chi tiết vé: {ex.Message}",
                    "Lỗi Hệ Thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BindTicketData(TicketReceiptDto t)
        {
            // Mã vé & Trạng thái
            lblTicketCodeTitle.Text = $"THÔNG TIN VÉ: {t.TicketCode}";
            UpdateStatusBadge(t.TicketStatus);

            // Thông tin hành khách
            lblHoTen.Text = string.IsNullOrWhiteSpace(t.CustomerName) ? "Chưa cung cấp" : t.CustomerName;
            lblDienThoai.Text = string.IsNullOrWhiteSpace(t.CustomerPhone) ? "Chưa cung cấp" : t.CustomerPhone;

            // Thông tin chuyến xe & chỗ ngồi
            lblTuyenDuong.Text = string.IsNullOrWhiteSpace(t.RouteName) ? "Không xác định" : t.RouteName;
            lblBenDi.Text = string.IsNullOrWhiteSpace(t.DepartureStation) ? "Bến xuất phát" : t.DepartureStation;
            lblBenDen.Text = string.IsNullOrWhiteSpace(t.ArrivalStation) ? "Bến trả khách" : t.ArrivalStation;
            lblBienSo.Text = string.IsNullOrWhiteSpace(t.LicensePlate) ? "Đang xếp xe" : t.LicensePlate;
            lblLoaiXe.Text = string.IsNullOrWhiteSpace(t.VehicleType) ? "Xe khách chất lượng cao" : t.VehicleType;
            lblKhoiHanh.Text = t.DepartureTime.ToString("HH:mm - dd/MM/yyyy");
            lblViTriGhe.Text = $"Ghế {t.SeatNumber} (Tầng {t.Floor})";

            // Thông tin thanh toán & đặt vé
            lblGiaVe.Text = $"{t.FinalPrice:N0} VNĐ";
            lblThanhToan.Text = t.PaymentStatus switch
            {
                "Paid" => "Đã thanh toán",
                "Unpaid" => "Chưa thanh toán",
                "Refunded" => "Đã hoàn tiền",
                _ => t.PaymentStatus
            };
            lblNgayDat.Text = t.BookingTime.ToString("dd/MM/yyyy HH:mm");
            lblNhanVien.Text = string.IsNullOrWhiteSpace(t.StaffName) ? "Hệ thống trực tuyến" : t.StaffName;

            // Kiểm tra trạng thái vé để bật/tắt nút In vé và Hủy vé
            if (t.TicketStatus == "Cancelled")
            {
                btnInVe.Enabled = false;
                btnInVe.Text = " Vé Đã Hủy";
                btnHuyVe.Enabled = false;
                btnHuyVe.Text = " Đã Hủy Vé";
            }
            else
            {
                btnInVe.Enabled = true;
                btnInVe.Text = " In Thẻ Lên Xe";
                btnHuyVe.Enabled = true;
                btnHuyVe.Text = " Hủy Vé Xe";
            }
        }

        private void UpdateStatusBadge(string status)
        {
            switch (status)
            {
                case "Booked":
                    lblStatusBadge.Text = "ĐÃ ĐẶT CHỖ";
                    lblStatusBadge.BackColor = Color.FromArgb(220, 252, 231);
                    lblStatusBadge.ForeColor = Color.FromArgb(22, 101, 52);
                    break;
                case "Cancelled":
                    lblStatusBadge.Text = "ĐÃ HỦY VÉ";
                    lblStatusBadge.BackColor = Color.FromArgb(254, 226, 226);
                    lblStatusBadge.ForeColor = Color.FromArgb(185, 28, 28);
                    break;
                case "Completed":
                    lblStatusBadge.Text = "HOÀN TẤT";
                    lblStatusBadge.BackColor = Color.FromArgb(219, 234, 254);
                    lblStatusBadge.ForeColor = Color.FromArgb(29, 78, 216);
                    break;
                default:
                    lblStatusBadge.Text = status.ToUpper();
                    lblStatusBadge.BackColor = Color.FromArgb(241, 245, 249);
                    lblStatusBadge.ForeColor = Color.FromArgb(71, 85, 105);
                    break;
            }
        }

        private void BtnInVe_Click(object? sender, EventArgs e)
        {
            if (_ticketDto == null) return;

            if (_ticketDto.TicketStatus == "Cancelled")
            {
                MessageBox.Show(
                    "Vé này đã bị hủy, không thể in thẻ lên xe!",
                    "Vé Không Hợp Lệ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var previewForm = new FrmTicketPrintPreview(new List<TicketReceiptDto> { _ticketDto });
                previewForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể mở xem trước bản in: {ex.Message}",
                    "Lỗi In Vé",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task BtnHuyVe_Click()
        {
            if (_ticketDto == null) return;

            if (_ticketDto.TicketStatus == "Cancelled")
            {
                MessageBox.Show(
                    "Vé này đã được hủy trước đó!",
                    "Thông Báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // Kiểm tra quy định hủy vé trước 1 giờ
            var timeRemaining = _ticketDto.DepartureTime - DateTime.Now;
            if (_ticketDto.DepartureTime <= DateTime.Now)
            {
                MessageBox.Show(
                    "Chuyến xe đã khởi hành, không thể thực hiện hủy vé!",
                    "Không Thể Hủy",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (timeRemaining < TimeSpan.FromHours(1))
            {
                int remainingMinutes = Math.Max(0, (int)timeRemaining.TotalMinutes);
                MessageBox.Show(
                    $"Không thể hủy vé! Chuyến xe sẽ khởi hành sau {remainingMinutes} phút nữa.\n" +
                    "Theo quy định của hệ thống QLVT, quý khách chỉ được hủy vé trước giờ xe xuất bến tối thiểu 60 phút (1 giờ).",
                    "Quy Định Hủy Vé",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var dialog = MessageBox.Show(
                $"Quý khách có chắc chắn muốn HỦY vé [{_ticketDto.TicketCode}]?\n" +
                $"• Tuyến: {_ticketDto.RouteName}\n" +
                $"• Giờ khởi hành: {_ticketDto.DepartureTime:HH:mm dd/MM/yyyy}\n" +
                $"• Ghế: {_ticketDto.SeatNumber}\n\n" +
                "Lưu ý: Sau khi hủy, chỗ ngồi này sẽ được mở lại cho các hành khách khác đặt chỗ.",
                "Xác Nhận Hủy Vé",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (dialog != DialogResult.Yes) return;

            try
            {
                var (success, message) = await _ticketService.CancelTicketAsync(_ticketDto.TicketId);
                if (success)
                {
                    MessageBox.Show(
                        message,
                        "Hủy Vé Thành Công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    WasCancelled = true;
                    _ticketDto.TicketStatus = "Cancelled";
                    UpdateStatusBadge("Cancelled");
                    btnInVe.Enabled = false;
                    btnInVe.Text = " Vé Đã Hủy";
                    btnHuyVe.Enabled = false;
                    btnHuyVe.Text = " Đã Hủy Vé";
                }
                else
                {
                    MessageBox.Show(
                        message,
                        "Không Thể Hủy Vé",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi thực hiện hủy vé: {ex.Message}",
                    "Lỗi Hệ Thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
