using System.ComponentModel;
using System.Diagnostics;
using System.Media;
using Core.Configs;
using Core.Helpers;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;

namespace QuanLyVanTai.UI.Forms
{
    public partial class FrmPaymentCheckout : Form
    {
        private readonly PaymentService _paymentService = new();
        private PaymentCallbackServer? _callbackServer;

        private string _gateway = "VNPay";
        private string _orderCode = string.Empty;
        private decimal _amount;
        private List<int> _ticketIds = new();
        private List<string> _ticketCodes = new();
        private string _customerName = string.Empty;
        private string _customerPhone = string.Empty;
        private string _routeName = string.Empty;
        private string _seatNumbers = string.Empty;

        private PaymentCreateResponseDto? _currentPaymentResponse;
        private int _remainingSeconds = 15 * 60; // 15 phút
        private bool _isCompleted = false;

        public bool IsPaymentSuccessful { get; private set; }
        public PaymentCallbackResultDto? FinalCallbackResult { get; private set; }

        public FrmPaymentCheckout() : this("VNPay", "ORDER-DEMO", 250000m, new List<int>(), new List<string>(), "Khách Hàng Mẫu", "0912345678", "Hà Nội - Đà Nẵng", "A01")
        {
        }

        public FrmPaymentCheckout(
            string gateway,
            string orderCode,
            decimal amount,
            List<int> ticketIds,
            List<string> ticketCodes,
            string customerName,
            string customerPhone,
            string routeName,
            string seatNumbers)
        {
            InitializeComponent();

            _gateway = gateway;
            _orderCode = orderCode;
            _amount = amount;
            _ticketIds = ticketIds;
            _ticketCodes = ticketCodes;
            _customerName = customerName;
            _customerPhone = customerPhone;
            _routeName = routeName;
            _seatNumbers = seatNumbers;

            // 🛡️ BẢO VỆ DESIGNER (SKILL Mục 2.2)
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || this.DesignMode)
            {
                return;
            }

            SetupUi();
            WireEvents();
        }

        private void SetupUi()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = ThemeConfig.BackgroundColor;

            // Điền thông tin đơn đặt vé
            lblMaDon.Text = _orderCode;
            lblHanhKhach.Text = $"{_customerName} - {_customerPhone}";
            lblTuyen.Text = _routeName;
            lblGhe.Text = _seatNumbers;
            lblTongTien.Text = $"{_amount:N0} VNĐ";

            // Khởi tạo ComboBox chọn cổng
            cboChonCong.Items.Clear();
            cboChonCong.Items.Add("VNPay Sandbox (VNPAY-QR / Thẻ ATM)");
            cboChonCong.Items.Add("MoMo Sandbox (Ví MoMo QR / App)");

            if (string.Equals(_gateway, "MoMo", StringComparison.OrdinalIgnoreCase))
            {
                cboChonCong.SelectedIndex = 1;
            }
            else
            {
                cboChonCong.SelectedIndex = 0;
            }

            ThemeConfig.StylePrimaryButton(btnMoTrinhDuyet, FontAwesome.Sharp.IconChar.Globe);
            ThemeConfig.StyleSuccessButton(btnGiaLapThanhCong, FontAwesome.Sharp.IconChar.CheckCircle);
            ThemeConfig.StyleDangerButton(btnGiaLapThatBai, FontAwesome.Sharp.IconChar.TriangleExclamation);
            ThemeConfig.StyleSecondaryButton(btnHuyGiaoDich, FontAwesome.Sharp.IconChar.Xmark);
        }

        private void WireEvents()
        {
            this.Load += async (s, e) =>
            {
                StartCallbackServer();
                await GeneratePaymentSessionAsync();
                StartCountdownTimer();
            };

            this.FormClosing += (s, e) =>
            {
                timerCountdown.Stop();
                StopCallbackServer();
            };

            cboChonCong.SelectedIndexChanged += async (s, e) =>
            {
                if (_isCompleted) return;
                _gateway = cboChonCong.SelectedIndex == 1 ? "MoMo" : "VNPay";
                await GeneratePaymentSessionAsync();
            };

            btnMoTrinhDuyet.Click += (s, e) => OpenPaymentInBrowser();
            btnGiaLapThanhCong.Click += async (s, e) => await SimulatePaymentCallbackAsync(isSuccess: true);
            btnGiaLapThatBai.Click += async (s, e) => await SimulatePaymentCallbackAsync(isSuccess: false);

            btnHuyGiaoDich.Click += async (s, e) =>
            {
                if (_isCompleted)
                {
                    this.Close();
                    return;
                }

                if (MessageBox.Show("Quý khách có chắc chắn muốn hủy giao dịch thanh toán này không?\nGhế đã chọn sẽ được giải phóng trên hệ thống.",
                    "Xác nhận hủy thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    await CancelPaymentAsync("Khách hàng chủ động hủy giao dịch thanh toán");
                }
            };

            timerCountdown.Interval = 1000;
            timerCountdown.Tick += async (s, e) =>
            {
                _remainingSeconds--;
                if (_remainingSeconds >= 0)
                {
                    int mins = _remainingSeconds / 60;
                    int secs = _remainingSeconds % 60;
                    lblDemNguoc.Text = $"{mins:D2}:{secs:D2}";
                }
                else
                {
                    timerCountdown.Stop();
                    lblDemNguoc.Text = "Hết hạn";
                    lblTrangThai.Text = "❌ ĐÃ HẾT HẠN GIỮ VÉ! Giao dịch tự động hủy.";
                    lblTrangThai.ForeColor = Color.FromArgb(239, 68, 68);
                    await CancelPaymentAsync("Hết thời gian giữ chỗ thanh toán trực tuyến (15 phút)");
                }
            };
        }

        private void StartCallbackServer()
        {
            try
            {
                _callbackServer = new PaymentCallbackServer(
                    port: _paymentService.Settings.CallbackPort,
                    vnPayConfig: _paymentService.Settings.VNPay,
                    moMoConfig: _paymentService.Settings.MoMo
                );
                _callbackServer.Start();
                PaymentCallbackServer.OnCallbackReceived += HandleCallbackFromListener;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FrmPaymentCheckout] Lỗi start server: {ex.Message}");
            }
        }

        private void StopCallbackServer()
        {
            PaymentCallbackServer.OnCallbackReceived -= HandleCallbackFromListener;
            _callbackServer?.Stop();
            _callbackServer?.Dispose();
            _callbackServer = null;
        }

        private void HandleCallbackFromListener(PaymentCallbackResultDto result)
        {
            // Đảm bảo chạy trên UI Thread
            if (this.IsDisposed || !this.IsHandleCreated) return;

            this.Invoke(new Action(async () =>
            {
                if (_isCompleted) return;

                // Kiểm tra xem callback có tương ứng với đơn hàng hiện tại không (hoặc nhận mọi callback nếu mã khớp/rỗng)
                if (string.IsNullOrWhiteSpace(result.OrderCode) ||
                    result.OrderCode.Equals(_orderCode, StringComparison.OrdinalIgnoreCase) ||
                    _orderCode.Contains(result.OrderCode, StringComparison.OrdinalIgnoreCase))
                {
                    await ProcessCompletedPaymentAsync(result);
                }
            }));
        }

        private void StartCountdownTimer()
        {
            _remainingSeconds = 15 * 60;
            timerCountdown.Start();
        }

        private async Task GeneratePaymentSessionAsync()
        {
            prgTienTrinh.Visible = true;
            lblTrangThai.ForeColor = Color.FromArgb(37, 99, 235);
            lblTrangThai.Text = $"⏳ Đang gọi API khởi tạo phiên thanh toán {_gateway} Sandbox qua HttpClient...";

            try
            {
                var request = new PaymentCreateRequestDto
                {
                    Gateway = _gateway,
                    OrderCode = _orderCode,
                    Amount = _amount,
                    OrderInfo = $"Thanh toan ve xe {_orderCode} ({_seatNumbers})",
                    CustomerName = _customerName,
                    CustomerPhone = _customerPhone,
                    TicketIds = _ticketIds,
                    IpAddress = "127.0.0.1"
                };

                _currentPaymentResponse = await _paymentService.CreatePaymentSessionAsync(request);

                if (_currentPaymentResponse.Success)
                {
                    // Sinh ảnh QR Code từ URL thanh toán
                    string qrContent = string.IsNullOrEmpty(_currentPaymentResponse.QrCodeContent)
                        ? _currentPaymentResponse.PaymentUrl
                        : _currentPaymentResponse.QrCodeContent;

                    var qrBitmap = TicketPdfExportService.GenerateQrCode(qrContent, pixelsPerModule: 5);
                    picQrCode.Image?.Dispose();
                    picQrCode.Image = qrBitmap;

                    lblTrangThai.Text = $"⏳ Đang chờ khách hàng thanh toán qua {_gateway} Sandbox...\n(Quét mã QR hoặc nhấn 'Mở Cổng Thanh Toán' bên trái)";
                }
                else
                {
                    lblTrangThai.ForeColor = Color.FromArgb(239, 68, 68);
                    lblTrangThai.Text = $"❌ Lỗi khởi tạo cổng: {_currentPaymentResponse.Message}";
                }
            }
            catch (Exception ex)
            {
                lblTrangThai.ForeColor = Color.FromArgb(239, 68, 68);
                lblTrangThai.Text = $"❌ Lỗi kết nối cổng thanh toán: {ex.Message}";
            }
        }

        private void OpenPaymentInBrowser()
        {
            if (_currentPaymentResponse == null || string.IsNullOrWhiteSpace(_currentPaymentResponse.PaymentUrl))
            {
                MessageBox.Show("Chưa có URL thanh toán hợp lệ. Vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = _currentPaymentResponse.PaymentUrl,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tự động mở trình duyệt: " + ex.Message + "\n\nURL: " + _currentPaymentResponse.PaymentUrl,
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Giả lập callback thành công hoặc thất bại từ cổng thanh toán (dành cho chấm điểm & bảo vệ đồ án)
        /// </summary>
        private async Task SimulatePaymentCallbackAsync(bool isSuccess)
        {
            if (_isCompleted) return;

            string txnCode = $"{_gateway.ToUpper()}_TEST_{DateTime.Now:yyyyMMddHHmmss}";
            var simulatedResult = new PaymentCallbackResultDto
            {
                Success = isSuccess,
                Gateway = _gateway,
                OrderCode = _orderCode,
                TransactionCode = txnCode,
                Amount = _amount,
                ResponseCode = isSuccess ? (_gateway == "MoMo" ? "0" : "00") : (_gateway == "MoMo" ? "1000" : "24"),
                Message = isSuccess
                    ? $"[GIẢ LẬP] Giao dịch {_gateway} Sandbox thành công (Code 00)."
                    : $"[GIẢ LẬP] Khách hàng đã hủy giao dịch trên cổng {_gateway}.",
                IsSignatureValid = true,
                RawData = $"simulated=true&gateway={_gateway}&orderId={_orderCode}&status={(isSuccess ? "00" : "24")}",
                TransactionDate = DateTime.UtcNow
            };

            await ProcessCompletedPaymentAsync(simulatedResult);
        }

        private async Task ProcessCompletedPaymentAsync(PaymentCallbackResultDto callbackResult)
        {
            if (_isCompleted) return;
            _isCompleted = true;
            timerCountdown.Stop();
            FinalCallbackResult = callbackResult;

            prgTienTrinh.Style = ProgressBarStyle.Blocks;
            prgTienTrinh.Value = 100;

            btnMoTrinhDuyet.Enabled = false;
            btnGiaLapThanhCong.Enabled = false;
            btnGiaLapThatBai.Enabled = false;
            cboChonCong.Enabled = false;

            lblTrangThai.Text = "⏳ Đang cập nhật trạng thái giao dịch vào CSDL...";

            // 1. Ghi nhận giao dịch và cập nhật trạng thái vé vào CSDL qua BLL
            bool dbSuccess = await _paymentService.UpdatePaymentTransactionResultAsync(
                callbackResult,
                _ticketIds,
                username: _customerName,
                accountId: SessionManager.CurrentUser?.Id
            );

            if (callbackResult.Success && dbSuccess)
            {
                IsPaymentSuccessful = true;
                SystemSounds.Asterisk.Play();

                lblTrangThai.ForeColor = Color.FromArgb(16, 185, 129);
                lblTrangThai.Text = $"✅ THANH TOÁN THÀNH CÔNG!\n" +
                                   $"Cổng: {callbackResult.Gateway} Sandbox | Mã GD: {callbackResult.TransactionCode}\n" +
                                   $"Trạng thái 'Đã thanh toán' đã được lưu vào CSDL.";

                await Task.Delay(1500);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                IsPaymentSuccessful = false;
                SystemSounds.Hand.Play();

                lblTrangThai.ForeColor = Color.FromArgb(239, 68, 68);
                lblTrangThai.Text = $"❌ GIAO DỊCH THẤT BẠI HOẶC BỊ HỦY!\n" +
                                   $"{callbackResult.Message}\n" +
                                   $"Trạng thái 'Thất bại' đã được cập nhật vào CSDL. Ghế đã được giải phóng.";

                await Task.Delay(2000);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private async Task CancelPaymentAsync(string reason)
        {
            _isCompleted = true;
            timerCountdown.Stop();

            await _paymentService.CancelPendingPaymentTicketsAsync(_ticketIds, reason);
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
