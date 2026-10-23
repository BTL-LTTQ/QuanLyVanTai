using System.ComponentModel;
using System.Text.RegularExpressions;
using Core.Configs;
using Core.Helpers;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.UI.Forms;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcCustomerBooking : UserControl
    {
        private readonly TicketService _ticketService = new();
        private readonly SeatMapService _seatMapService = new();
        private readonly CustomerBookingService _bookingService = new();
        private readonly ScheduleService _scheduleService = new();
        private readonly EmailService _emailService = new();

        private List<Route> _routes = new();
        private VehicleSeatMapDto? _currentSeatMap;
        private PromotionVerifyResultDto? _currentPromotion;
        private int? _initialRouteId;

        public event Action<string, bool>? OnStatusChanged;
        public event Action? OnNavigateToHistory;
        public event Action<int>? OnNavigateToMap;

        public UcCustomerBooking() : this(null)
        {
        }

        public UcCustomerBooking(int? initialRouteId)
        {
            InitializeComponent();
            _initialRouteId = initialRouteId;

            // Chống crash Visual Studio Designer
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

            // Nút tác vụ chuẩn theme
            ThemeConfig.StyleSuccessButton(btnDatVe, IconChar.Ticket);
            ThemeConfig.StyleSecondaryButton(btnHuyChon, IconChar.RotateLeft);
            ThemeConfig.StyleSecondaryButton(btnTaiSoDo, IconChar.RotateLeft);
            ThemeConfig.StylePrimaryButton(btnTimChuyen, IconChar.MagnifyingGlass);
            ThemeConfig.StyleSecondaryButton(btnXemBanDo, IconChar.MapLocationDot);

            // Cấu hình danh sách phương thức thanh toán
            cboPhuongThucThanhToan.Items.Clear();
            cboPhuongThucThanhToan.Items.AddRange(new object[]
            {
                "VNPay Sandbox (QR / Thẻ ATM)",
                "MoMo Sandbox (Ví MoMo QR / App)",
                "Tiền mặt tại quầy vé"
            });
            cboPhuongThucThanhToan.SelectedIndex = 0;

            // Điền thông tin người dùng từ phiên đăng nhập nếu có
            if (SessionManager.IsLoggedIn && SessionManager.CurrentUser != null)
            {
                var user = SessionManager.CurrentUser;
                if (!string.IsNullOrWhiteSpace(user.FullName) && string.IsNullOrWhiteSpace(txtTenKhachHang.Text))
                {
                    txtTenKhachHang.Text = user.FullName;
                }
                if (!string.IsNullOrWhiteSpace(user.PhoneNumber) && string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
                {
                    txtSoDienThoai.Text = user.PhoneNumber;
                }
                if (!string.IsNullOrWhiteSpace(user.Email) && string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    txtEmail.Text = user.Email;
                }
            }
        }

        private void WireEvents()
        {
            this.Load += async (s, e) => await LoadRoutesAsync();

            // Tìm kiếm chuyến xe gõ đến đâu lọc đến đó (debounce 300ms)
            txtTimChuyen.TextChanged += (s, e) => ScheduleTripFilterDebounce();
            btnTimChuyen.Click += async (s, e) => await OpenSearchTripsModalAsync();

            // Sự kiện chọn tuyến và ngày đi
            cboTuyenXe.SelectedIndexChanged += async (s, e) => await OnRouteChangedAsync();
            dtpNgayDi.ValueChanged += async (s, e) => await OnRouteChangedAsync();
            cboChuyenXe.SelectedIndexChanged += async (s, e) => await LoadSeatMapForSelectedTripAsync();
            btnTaiSoDo.Click += async (s, e) => await LoadSeatMapForSelectedTripAsync();
            btnXemBanDo.Click += (s, e) =>
            {
                int rId = cboTuyenXe.SelectedValue is int id ? id : 0;
                OnNavigateToMap?.Invoke(rId);
            };

            // Sơ đồ ghế tương tác
            seatMapControl.SelectionChanged += (s, selectedSeats) => OnSeatSelectionChanged(selectedSeats);
            btnHuyChon.Click += (s, e) => seatMapControl.ClearSelection();

            // Mã khuyến mãi
            btnApDungKhuyenMai.Click += async (s, e) => await ApplyPromotionCodeAsync();
            txtMaKhuyenMai.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    await ApplyPromotionCodeAsync();
                }
            };

            // Nút bấm
            btnDatVe.Click += async (s, e) => await ExecuteBookingAsync();
            btnXemLichSu.Click += (s, e) => OnNavigateToHistory?.Invoke();

            // Validation qua ErrorProvider (Chương 4)
            txtTenKhachHang.Validating += TxtTenKhachHang_Validating;
            txtSoDienThoai.Validating += TxtSoDienThoai_Validating;
            txtEmail.Validating += TxtEmail_Validating;

            txtTenKhachHang.KeyDown += OnInputEnterNextControl;
            txtSoDienThoai.KeyDown += OnInputEnterNextControl;
            txtEmail.KeyDown += OnInputEnterNextControl;
            txtGhiChu.KeyDown += OnInputEnterNextControl;
        }

        #region Nạp dữ liệu Tuyến & Chuyến

        public async Task LoadRoutesAsync()
        {
            try
            {
                SetStatus("Đang tải danh sách tuyến xe...", true);
                _routes = await _ticketService.GetActiveRoutesAsync();

                cboTuyenXe.DataSource = null;
                cboTuyenXe.DisplayMember = "RouteName";
                cboTuyenXe.ValueMember = "Id";
                cboTuyenXe.DataSource = _routes;

                if (_routes.Any())
                {
                    if (_initialRouteId.HasValue && _routes.Any(r => r.Id == _initialRouteId.Value))
                    {
                        cboTuyenXe.SelectedValue = _initialRouteId.Value;
                    }
                    else
                    {
                        cboTuyenXe.SelectedIndex = 0;
                    }
                    await OnRouteChangedAsync();
                }
            }
            catch (Exception ex)
            {
                SetStatus("Lỗi nạp tuyến xe: " + ex.Message, false);
            }
            finally
            {
                SetStatus("Sẵn sàng", false);
            }
        }

        private System.Windows.Forms.Timer? _tripDebounceTimer;

        private void ScheduleTripFilterDebounce()
        {
            _tripDebounceTimer?.Stop();
            _tripDebounceTimer?.Dispose();
            _tripDebounceTimer = new System.Windows.Forms.Timer { Interval = 300 };
            _tripDebounceTimer.Tick += async (s, e) =>
            {
                _tripDebounceTimer?.Stop();
                _tripDebounceTimer?.Dispose();
                _tripDebounceTimer = null;
                await FilterRoutesByKeywordAsync();
            };
            _tripDebounceTimer.Start();
        }

        private async Task FilterRoutesByKeywordAsync()
        {
            string kw = txtTimChuyen.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(kw))
            {
                cboTuyenXe.DataSource = null;
                cboTuyenXe.DisplayMember = "RouteName";
                cboTuyenXe.ValueMember = "Id";
                cboTuyenXe.DataSource = _routes;
            }
            else
            {
                var filtered = _routes.Where(r =>
                    r.RouteName.ToLower().Contains(kw) ||
                    r.RouteCode.ToLower().Contains(kw)).ToList();

                cboTuyenXe.DataSource = null;
                cboTuyenXe.DisplayMember = "RouteName";
                cboTuyenXe.ValueMember = "Id";
                cboTuyenXe.DataSource = filtered;
            }

            if (cboTuyenXe.Items.Count > 0)
            {
                cboTuyenXe.SelectedIndex = 0;
                await OnRouteChangedAsync();
            }
            else
            {
                cboChuyenXe.DataSource = null;
                seatMapControl.LoadSeatMap(new VehicleSeatMapDto());
                lblDonGia.Text = "0 VNĐ";
                SetStatus("Không tìm thấy tuyến xe nào phù hợp với từ khóa.", false);
            }
        }

        private async Task OpenSearchTripsModalAsync()
        {
            try
            {
                using var frm = new Forms.FrmSearchTrips(dtpNgayDi.Value, txtTimChuyen.Text);
                if (frm.ShowDialog(this.FindForm()) == DialogResult.OK && frm.SelectedTrip != null)
                {
                    var trip = frm.SelectedTrip;
                    txtTimChuyen.Text = trip.RouteName;

                    await FilterRoutesByKeywordAsync();

                    for (int i = 0; i < cboTuyenXe.Items.Count; i++)
                    {
                        if (cboTuyenXe.Items[i] is Route r && r.Id == trip.RouteId)
                        {
                            cboTuyenXe.SelectedIndex = i;
                            break;
                        }
                    }

                    dtpNgayDi.Value = trip.DepartureTime.Date;
                    await OnRouteChangedAsync();

                    for (int i = 0; i < cboChuyenXe.Items.Count; i++)
                    {
                        if (cboChuyenXe.Items[i] is TripDisplayItem t && t.ScheduleId == trip.ScheduleId)
                        {
                            cboChuyenXe.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm chuyến xe: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task OnRouteChangedAsync()
        {
            if (cboTuyenXe.SelectedValue is not int routeId || routeId <= 0) return;

            try
            {
                SetStatus("Đang tìm chuyến xe phục vụ tuyến...", true);

                // Chuyến xe thực tế được điều độ ở màn hình Lịch trình (bảng Schedules).
                // Chỉ lấy các chuyến đang ở trạng thái 'Scheduled' trong ngày khách chọn.
                DateTime from = dtpNgayDi.Value.Date.ToUniversalTime();
                DateTime to = dtpNgayDi.Value.Date.AddDays(1).ToUniversalTime();

                var schedules = await _scheduleService.GetSchedulesAsync(
                    from, to, status: "Scheduled", routeId: routeId);

                var tripItems = schedules.Select(s => new TripDisplayItem
                {
                    ScheduleId = s.Id,
                    VehicleId = s.VehicleId,
                    LicensePlate = s.LicensePlate,
                    VehicleType = s.VehicleType,
                    TotalSeats = s.TotalSeats,
                    // DB lưu UTC → hiển thị & đặt vé theo giờ local của máy khách.
                    DepartureTime = s.DepartureTime.ToLocalTime(),
                    DisplayText = $"{s.DepartureTime.ToLocalTime():HH:mm} | {s.LicensePlate} " +
                                  $"({s.VehicleType} - {s.TotalSeats} chỗ)"
                }).ToList();

                cboChuyenXe.DataSource = null;
                cboChuyenXe.DisplayMember = "DisplayText";
                cboChuyenXe.ValueMember = "ScheduleId";
                cboChuyenXe.DataSource = tripItems;

                if (tripItems.Any())
                {
                    cboChuyenXe.SelectedIndex = 0;
                    await LoadSeatMapForSelectedTripAsync();
                }
                else
                {
                    seatMapControl.LoadSeatMap(new VehicleSeatMapDto());
                    lblDonGia.Text = "0 VNĐ";
                    SetStatus("Tuyến này chưa có chuyến xe nào được điều độ trong ngày đã chọn.", false);
                }
            }
            catch (Exception ex)
            {
                SetStatus("Lỗi tìm chuyến xe: " + ex.Message, false);
            }
            finally
            {
                SetStatus("Sẵn sàng", false);
            }
        }

        private async Task LoadSeatMapForSelectedTripAsync()
        {
            if (cboChuyenXe.SelectedItem is not TripDisplayItem trip) return;

            try
            {
                SetStatus($"Đang tải sơ đồ ghế xe {trip.LicensePlate}...", true);

                var selectedRoute = _routes.FirstOrDefault(r => r.Id == (int)(cboTuyenXe.SelectedValue ?? 0));
                decimal basePrice = selectedRoute?.BasePrice ?? 250000m;

                _currentSeatMap = await _seatMapService.GetVehicleSeatMapAsync(trip.VehicleId, trip.DepartureTime, basePrice);
                seatMapControl.LoadSeatMap(_currentSeatMap);

                lblDonGia.Text = $"{_currentSeatMap.BasePrice:N0} VNĐ / vé";
                OnSeatSelectionChanged(new List<SeatVisualItem>());
                SetStatus($"Đã tải sơ đồ xe {trip.LicensePlate} ({trip.VehicleType}). Vui lòng chọn ghế!", false);
            }
            catch (Exception ex)
            {
                SetStatus("Lỗi tải sơ đồ ghế: " + ex.Message, false);
            }
            finally
            {
                SetStatus("Sẵn sàng", false);
            }
        }

        #endregion

        #region Tính tiền & Khuyến mãi

        private void OnSeatSelectionChanged(List<SeatVisualItem> selectedSeats)
        {
            if (selectedSeats == null || selectedSeats.Count == 0)
            {
                lblDanhSachGhe.Text = "Chưa chọn ghế";
                lblDanhSachGhe.ForeColor = Color.FromArgb(71, 85, 105);
                lblTongTien.Text = "0 VNĐ";
                lblTienGiam.Text = "-0 VNĐ";
                _currentPromotion = null;
                return;
            }

            var seatCodes = selectedSeats.Select(s => s.SeatCode).ToList();
            lblDanhSachGhe.Text = $"{string.Join(", ", seatCodes)} ({selectedSeats.Count} chỗ)";
            lblDanhSachGhe.ForeColor = Color.FromArgb(37, 99, 235);

            decimal subTotal = selectedSeats.Sum(s => s.FinalPrice);

            if (_currentPromotion != null && _currentPromotion.IsValid)
            {
                decimal discount = _currentPromotion.DiscountPercent > 0
                    ? Math.Round(subTotal * (_currentPromotion.DiscountPercent / 100m), 0)
                    : _currentPromotion.DiscountAmount;

                _currentPromotion.DiscountAmount = discount;
                _currentPromotion.FinalTotal = Math.Max(0, subTotal - discount);

                lblTienGiam.Text = $"-{discount:N0} VNĐ";
                lblTongTien.Text = $"{_currentPromotion.FinalTotal:N0} VNĐ";
            }
            else
            {
                lblTienGiam.Text = "-0 VNĐ";
                lblTongTien.Text = $"{subTotal:N0} VNĐ";
            }

            errorProviderMain.SetError(seatMapControl, string.Empty);
        }

        private async Task ApplyPromotionCodeAsync()
        {
            var selectedSeats = seatMapControl.GetSelectedSeats();
            if (selectedSeats.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 vị trí ghế trước khi áp dụng mã khuyến mãi!",
                    "Chưa chọn ghế", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string code = txtMaKhuyenMai.Text.Trim();
            if (string.IsNullOrEmpty(code))
            {
                _currentPromotion = null;
                OnSeatSelectionChanged(selectedSeats);
                return;
            }

            decimal subTotal = selectedSeats.Sum(s => s.FinalPrice);
            SetStatus("Đang kiểm tra mã khuyến mãi...", true);

            try
            {
                _currentPromotion = await _bookingService.VerifyPromotionAsync(code, subTotal);

                if (_currentPromotion.IsValid)
                {
                    lblTienGiam.Text = $"-{_currentPromotion.DiscountAmount:N0} VNĐ";
                    lblTongTien.Text = $"{_currentPromotion.FinalTotal:N0} VNĐ";
                    MessageBox.Show($"{_currentPromotion.Message}\n{_currentPromotion.Description}",
                        "Áp dụng thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _currentPromotion = null;
                    lblTienGiam.Text = "-0 VNĐ";
                    lblTongTien.Text = $"{subTotal:N0} VNĐ";
                    MessageBox.Show(_currentPromotion?.Message ?? "Mã không hợp lệ!",
                        "Không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
                SetStatus("Sẵn sàng", false);
            }
        }

        #endregion

        #region Validation

        private void TxtTenKhachHang_Validating(object? sender, CancelEventArgs e)
        {
            string name = txtTenKhachHang.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                errorProviderMain.SetError(txtTenKhachHang, "Họ và tên khách hàng không được để trống!");
            }
            else if (name.Length < 2)
            {
                errorProviderMain.SetError(txtTenKhachHang, "Họ và tên phải có tối thiểu 2 ký tự!");
            }
            else
            {
                errorProviderMain.SetError(txtTenKhachHang, string.Empty);
            }
        }

        private void TxtSoDienThoai_Validating(object? sender, CancelEventArgs e)
        {
            string phone = txtSoDienThoai.Text.Trim();
            if (string.IsNullOrEmpty(phone))
            {
                errorProviderMain.SetError(txtSoDienThoai, "Số điện thoại không được để trống!");
            }
            else if (!Regex.IsMatch(phone, @"^0\d{9}$"))
            {
                errorProviderMain.SetError(txtSoDienThoai, "Số điện thoại phải gồm 10 chữ số và bắt đầu bằng số 0 (VD: 0912345678)!");
            }
            else
            {
                errorProviderMain.SetError(txtSoDienThoai, string.Empty);
            }
        }

        private void TxtEmail_Validating(object? sender, CancelEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(email))
            {
                if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    errorProviderMain.SetError(txtEmail, "Địa chỉ email không đúng định dạng (Ví dụ: khachhang@gmail.com)!");
                }
                else
                {
                    errorProviderMain.SetError(txtEmail, string.Empty);
                }
            }
            else
            {
                errorProviderMain.SetError(txtEmail, string.Empty);
            }
        }

        private bool ValidateAllInputs()
        {
            TxtTenKhachHang_Validating(txtTenKhachHang, new CancelEventArgs());
            TxtSoDienThoai_Validating(txtSoDienThoai, new CancelEventArgs());
            TxtEmail_Validating(txtEmail, new CancelEventArgs());

            if (!string.IsNullOrEmpty(errorProviderMain.GetError(txtTenKhachHang)) ||
                !string.IsNullOrEmpty(errorProviderMain.GetError(txtSoDienThoai)) ||
                !string.IsNullOrEmpty(errorProviderMain.GetError(txtEmail)))
            {
                return false;
            }

            if (cboChuyenXe.SelectedItem is not TripDisplayItem)
            {
                MessageBox.Show("Vui lòng chọn chuyến xe khởi hành!", "Chưa chọn chuyến xe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var selectedSeats = seatMapControl.GetSelectedSeats();
            if (selectedSeats.Count == 0)
            {
                errorProviderMain.SetError(seatMapControl, "Vui lòng chọn ít nhất 1 chỗ ngồi trên sơ đồ xe!");
                MessageBox.Show("Vui lòng chọn ít nhất 1 vị trí ghế trên sơ đồ xe!", "Chưa chọn ghế", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void OnInputEnterNextControl(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender!, true, true, true, true);
            }
        }

        #endregion

        #region Đặt vé

        private async Task ExecuteBookingAsync()
        {
            if (!ValidateAllInputs()) return;

            var trip = (TripDisplayItem)cboChuyenXe.SelectedItem!;
            var selectedSeats = seatMapControl.GetSelectedSeats();
            var seatCodes = selectedSeats.Select(s => s.SeatCode).ToList();

            int selectedMethodIndex = cboPhuongThucThanhToan.SelectedIndex;
            bool isCash = selectedMethodIndex == 2;
            string chosenGateway = selectedMethodIndex == 1 ? "MoMo" : "VNPay";
            string paymentMethodName = isCash ? "Tiền mặt" : $"{chosenGateway} Sandbox";

            string confirmMsg = $"XÁC NHẬN ĐẶT VÉ:\n\n" +
                                $"• Hành khách: {txtTenKhachHang.Text.Trim()}\n" +
                                $"• Điện thoại: {txtSoDienThoai.Text.Trim()}\n" +
                                $"• Chuyến: {trip.LicensePlate} ({trip.DepartureTime:dd/MM/yyyy HH:mm})\n" +
                                $"• Vị trí ghế: {string.Join(", ", seatCodes)}\n" +
                                $"• Phương thức: {paymentMethodName}\n" +
                                $"• Tổng thanh toán: {lblTongTien.Text}\n\n" +
                                $"Quý khách có chắc chắn muốn tiến hành đặt vé không?";

            if (MessageBox.Show(confirmMsg, "Xác nhận đặt vé", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            SetStatus("Đang xử lý đặt vé & ghi nhận hệ thống...", true);

            try
            {
                int routeId = (int)(cboTuyenXe.SelectedValue ?? 0);
                int? accountId = SessionManager.CurrentUser?.Id;

                var bookingRequest = new BookingRequestDto
                {
                    ScheduleId = trip.ScheduleId,
                    RouteId = routeId,
                    VehicleId = trip.VehicleId,
                    DepartureTime = trip.DepartureTime,
                    CustomerName = txtTenKhachHang.Text.Trim(),
                    CustomerPhone = txtSoDienThoai.Text.Trim(),
                    CustomerEmail = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                    CustomerNotes = txtGhiChu.Text.Trim(),
                    SelectedSeats = seatCodes,
                    PromotionCode = txtMaKhuyenMai.Text.Trim(),
                    AccountId = accountId,
                    PaymentMethod = paymentMethodName,
                    PaymentStatus = isCash ? "Paid" : "Pending"
                };

                var result = await _bookingService.BookTicketsAsync(bookingRequest);

                if (!result.Success)
                {
                    MessageBox.Show(result.Message, "Không thể đặt vé", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 1. Trường hợp Khách chọn Tiền mặt: Hoàn tất ngay
                if (isCash)
                {
                    System.Media.SystemSounds.Asterisk.Play();
                    SetStatus(result.Message, false);
                    await LoadSeatMapForSelectedTripAsync();

                    string emailNotice = string.Empty;
                    string customerEmail = txtEmail.Text.Trim();
                    if (!string.IsNullOrWhiteSpace(customerEmail) && _emailService.Settings.EnableAutoSendOnPayment)
                    {
                        _ = Task.Run(async () =>
                        {
                            try
                            {
                                var receipts = await _bookingService.GetTicketReceiptsAsync(result.CreatedTicketIds);
                                await _emailService.SendTicketEmailAsync(customerEmail, txtTenKhachHang.Text.Trim(), receipts);
                            }
                            catch (Exception ex)
                            {
                                Serilog.Log.Error(ex, "[UcCustomerBooking] Lỗi tự động gửi email vé tiền mặt");
                            }
                        });
                        emailNotice = $"\n\n📧 Hệ thống đã tự động gửi vé điện tử (kèm tệp PDF) tới email:\n{customerEmail}";
                    }

                    var ask = MessageBox.Show(
                        $"{result.Message}\n\nVé của quý khách đã được lưu vào hệ thống thành công!{emailNotice}\n\nQuý khách có muốn chuyển ngay sang màn hình 'Vé của tôi' để xem và In vé không?",
                        "Đặt Vé Thành Công",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (ask == DialogResult.Yes)
                    {
                        OnNavigateToHistory?.Invoke();
                    }
                    return;
                }

                // 2. Trường hợp Khách chọn Thanh toán trực tuyến (VNPay / MoMo Sandbox):
                // Mở giao diện FrmPaymentCheckout với HttpClient / QR Code
                string orderCode = $"ORDER-{result.CreatedTicketCodes.First()}";
                using var paymentForm = new FrmPaymentCheckout(
                    gateway: chosenGateway,
                    orderCode: orderCode,
                    amount: result.FinalAmount,
                    ticketIds: result.CreatedTicketIds,
                    ticketCodes: result.CreatedTicketCodes,
                    customerName: txtTenKhachHang.Text.Trim(),
                    customerPhone: txtSoDienThoai.Text.Trim(),
                    routeName: $"{cboTuyenXe.Text} ({trip.DepartureTime:dd/MM/yyyy HH:mm})",
                    seatNumbers: string.Join(", ", seatCodes),
                    customerEmail: string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim()
                );

                var payDialogResult = paymentForm.ShowDialog(this.FindForm());

                await LoadSeatMapForSelectedTripAsync();

                if (payDialogResult == DialogResult.OK && paymentForm.IsPaymentSuccessful)
                {
                    SetStatus($"Thanh toán thành công qua {chosenGateway} Sandbox!", false);

                    var ask = MessageBox.Show(
                        $"🎉 THANH TOÁN TRỰC TUYẾN THÀNH CÔNG!\n\n" +
                        $"• Cổng thanh toán: {chosenGateway} Sandbox\n" +
                        $"• Mã vé: {string.Join(", ", result.CreatedTicketCodes)}\n" +
                        $"• Số tiền: {result.FinalAmount:N0} VNĐ\n\n" +
                        $"Vé của quý khách đã được kích hoạt thành công. Quý khách có muốn chuyển sang mục 'Vé của tôi' để in thẻ lên xe ngay không?",
                        "Thanh Toán Hoàn Tất",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (ask == DialogResult.Yes)
                    {
                        OnNavigateToHistory?.Invoke();
                    }
                }
                else
                {
                    SetStatus("Giao dịch thanh toán chưa hoàn tất hoặc đã bị hủy.", false);
                    MessageBox.Show(
                        "Giao dịch thanh toán trực tuyến chưa hoàn tất hoặc đã bị hủy.\nGhế đã chọn đã được giải phóng trên sơ đồ.",
                        "Giao Dịch Chưa Hoàn Tất",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình đặt vé: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetStatus("Sẵn sàng", false);
            }
        }

        private void SetStatus(string msg, bool isLoading)
        {
            btnDatVe.Enabled = !isLoading;
            btnTaiSoDo.Enabled = !isLoading;
            OnStatusChanged?.Invoke(msg, isLoading);
        }

        #endregion

        private class TripDisplayItem
        {
            /// <summary>ID chuyến xe trong bảng Schedules — dùng để liên kết vé với lịch trình.</summary>
            public int ScheduleId { get; set; }
            public int VehicleId { get; set; }
            public string LicensePlate { get; set; } = string.Empty;
            public string VehicleType { get; set; } = string.Empty;
            public int TotalSeats { get; set; }
            public DateTime DepartureTime { get; set; }
            public string DisplayText { get; set; } = string.Empty;
        }
    }
}
