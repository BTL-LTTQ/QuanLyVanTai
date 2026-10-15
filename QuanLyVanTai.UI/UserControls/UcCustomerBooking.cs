using System.ComponentModel;
using System.Text.RegularExpressions;
using Core.Configs;
using Core.Helpers;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcCustomerBooking : UserControl
    {
        private readonly TicketService _ticketService = new();
        private readonly SeatMapService _seatMapService = new();
        private readonly CustomerBookingService _bookingService = new();

        private List<Route> _routes = new();
        private List<Vehicle> _vehicles = new();
        private VehicleSeatMapDto? _currentSeatMap;
        private PromotionVerifyResultDto? _currentPromotion;

        public event Action<string, bool>? OnStatusChanged;
        public event Action? OnNavigateToHistory;

        public UcCustomerBooking()
        {
            InitializeComponent();

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
            }
        }

        private void WireEvents()
        {
            this.Load += async (s, e) => await LoadRoutesAsync();

            // Sự kiện chọn tuyến và ngày đi
            cboTuyenXe.SelectedIndexChanged += async (s, e) => await OnRouteChangedAsync();
            dtpNgayDi.ValueChanged += async (s, e) => await OnRouteChangedAsync();
            cboChuyenXe.SelectedIndexChanged += async (s, e) => await LoadSeatMapForSelectedTripAsync();
            btnTaiSoDo.Click += async (s, e) => await LoadSeatMapForSelectedTripAsync();

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

            txtTenKhachHang.KeyDown += OnInputEnterNextControl;
            txtSoDienThoai.KeyDown += OnInputEnterNextControl;
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
                    cboTuyenXe.SelectedIndex = 0;
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

        private async Task OnRouteChangedAsync()
        {
            if (cboTuyenXe.SelectedValue is not int routeId || routeId <= 0) return;

            try
            {
                SetStatus("Đang tìm phương tiện phục vụ tuyến...", true);
                _vehicles = await _ticketService.GetVehiclesByRouteAsync(routeId);

                var tripItems = new List<TripDisplayItem>();
                DateTime chosenDate = dtpNgayDi.Value.Date;

                int[] departureHours = { 6, 8, 12, 14, 18, 20, 22 };

                if (_vehicles.Any())
                {
                    int hourIdx = 0;
                    foreach (var v in _vehicles)
                    {
                        int h = departureHours[hourIdx % departureHours.Length];
                        var depTime = chosenDate.AddHours(h);

                        tripItems.Add(new TripDisplayItem
                        {
                            VehicleId = v.Id,
                            LicensePlate = v.LicensePlate,
                            VehicleType = v.VehicleType,
                            TotalSeats = v.TotalSeats,
                            DepartureTime = depTime,
                            DisplayText = $"{depTime:HH:mm} | {v.LicensePlate} ({v.VehicleType} - {v.TotalSeats} chỗ)"
                        });
                        hourIdx++;
                    }
                }

                cboChuyenXe.DataSource = null;
                cboChuyenXe.DisplayMember = "DisplayText";
                cboChuyenXe.ValueMember = "VehicleId";
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

        private bool ValidateAllInputs()
        {
            TxtTenKhachHang_Validating(txtTenKhachHang, new CancelEventArgs());
            TxtSoDienThoai_Validating(txtSoDienThoai, new CancelEventArgs());

            if (!string.IsNullOrEmpty(errorProviderMain.GetError(txtTenKhachHang)) ||
                !string.IsNullOrEmpty(errorProviderMain.GetError(txtSoDienThoai)))
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

            string confirmMsg = $"XÁC NHẬN ĐẶT VÉ:\n\n" +
                                $"• Hành khách: {txtTenKhachHang.Text.Trim()}\n" +
                                $"• Điện thoại: {txtSoDienThoai.Text.Trim()}\n" +
                                $"• Chuyến: {trip.LicensePlate} ({trip.DepartureTime:dd/MM/yyyy HH:mm})\n" +
                                $"• Vị trí ghế: {string.Join(", ", seatCodes)}\n" +
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
                    RouteId = routeId,
                    VehicleId = trip.VehicleId,
                    DepartureTime = trip.DepartureTime,
                    CustomerName = txtTenKhachHang.Text.Trim(),
                    CustomerPhone = txtSoDienThoai.Text.Trim(),
                    CustomerNotes = txtGhiChu.Text.Trim(),
                    SelectedSeats = seatCodes,
                    PromotionCode = txtMaKhuyenMai.Text.Trim(),
                    AccountId = accountId,
                    PaymentMethod = "Tiền mặt"
                };

                var result = await _bookingService.BookTicketsAsync(bookingRequest);

                if (result.Success)
                {
                    System.Media.SystemSounds.Asterisk.Play();
                    SetStatus(result.Message, false);

                    await LoadSeatMapForSelectedTripAsync();

                    var ask = MessageBox.Show(
                        $"{result.Message}\n\nVé của quý khách đã được lưu vào hệ thống thành công!\nQuý khách có muốn chuyển ngay sang màn hình 'Vé của tôi' để xem và In vé không?",
                        "Đặt Vé Thành Công",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (ask == DialogResult.Yes)
                    {
                        OnNavigateToHistory?.Invoke();
                    }
                }
                else
                {
                    MessageBox.Show(result.Message, "Không thể đặt vé", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            public int VehicleId { get; set; }
            public string LicensePlate { get; set; } = string.Empty;
            public string VehicleType { get; set; } = string.Empty;
            public int TotalSeats { get; set; }
            public DateTime DepartureTime { get; set; }
            public string DisplayText { get; set; } = string.Empty;
        }
    }
}
