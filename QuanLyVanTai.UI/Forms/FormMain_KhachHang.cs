using System.ComponentModel;
using System.Text.RegularExpressions;
using Core.Configs;
using Core.Helpers;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.UI.Forms
{
    public partial class FormMain_KhachHang : Form
    {
        private readonly TicketService _ticketService = new();
        private readonly SeatMapService _seatMapService = new();
        private readonly CustomerBookingService _bookingService = new();

        private List<Route> _routes = new();
        private List<Vehicle> _vehicles = new();
        private VehicleSeatMapDto? _currentSeatMap;
        private List<int> _lastBookedTicketIds = new();
        private PromotionVerifyResultDto? _currentPromotion;

        public FormMain_KhachHang()
        {
            InitializeComponent();

            // Chống crash Visual Studio Designer
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || this.DesignMode)
            {
                return;
            }

            this.KeyPreview = true;
            SetupFormUi();
            WireEvents();
        }

        private void SetupFormUi()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = ThemeConfig.BackgroundColor;

            // Áp dụng theme button theo chuẩn
            ThemeConfig.StyleSuccessButton(btnDatVe, IconChar.Ticket);
            btnDatVe.Size = new Size(200, 42);

            btnXuatVe.Visible = false;

            ThemeConfig.StyleSecondaryButton(btnTaiSoDo, IconChar.RotateLeft);
            btnTaiSoDo.Size = new Size(160, 32);

            // Cập nhật thông tin phiên đăng nhập
            if (SessionManager.IsLoggedIn && SessionManager.CurrentUser != null)
            {
                var user = SessionManager.CurrentUser;
                lblCurrentUser.Text = $"Người dùng: {user.FullName} ({user.Role})";

                if (!string.IsNullOrWhiteSpace(user.FullName) && string.IsNullOrWhiteSpace(txtTenKhachHang.Text))
                {
                    txtTenKhachHang.Text = user.FullName;
                }
                if (!string.IsNullOrWhiteSpace(user.PhoneNumber) && string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
                {
                    txtSoDienThoai.Text = user.PhoneNumber;
                }
            }
            else
            {
                lblCurrentUser.Text = "Khách Hàng Trực Tuyến";
            }

            lblClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void WireEvents()
        {
            this.Load += FormMain_KhachHang_Load;
            this.KeyDown += FormMain_KhachHang_KeyDown;

            // Timer đồng hồ
            timerClock.Tick += (s, e) =>
            {
                lblClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            };

            // Sự kiện chọn tuyến và xe
            cboTuyenXe.SelectedIndexChanged += async (s, e) => await OnRouteChangedAsync();
            dtpNgayDi.ValueChanged += async (s, e) => await OnRouteChangedAsync();
            cboChuyenXe.SelectedIndexChanged += async (s, e) => await LoadSeatMapForSelectedTripAsync();
            btnTaiSoDo.Click += async (s, e) => await LoadSeatMapForSelectedTripAsync();

            // Sự kiện chọn ghế trên sơ đồ GDI+
            seatMapControl.SelectionChanged += (s, selectedSeats) => OnSeatSelectionChanged(selectedSeats);
            btnHuyChon.Click += (s, e) => seatMapControl.ClearSelection();

            // Áp dụng khuyến mãi
            btnApDungKhuyenMai.Click += async (s, e) => await ApplyPromotionCodeAsync();
            txtMaKhuyenMai.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    await ApplyPromotionCodeAsync();
                }
            };

            // Nút đặt vé
            btnDatVe.Click += async (s, e) => await ExecuteBookingAsync();

            // Validation qua ErrorProvider (Chương 4)
            txtTenKhachHang.Validating += TxtTenKhachHang_Validating;
            txtSoDienThoai.Validating += TxtSoDienThoai_Validating;

            // Chuyển focus bằng phím Enter
            txtTenKhachHang.KeyDown += OnInputEnterNextControl;
            txtSoDienThoai.KeyDown += OnInputEnterNextControl;
            txtGhiChu.KeyDown += OnInputEnterNextControl;

            // MenuStrip handlers
            mnuDangXuat.Click += (s, e) =>
            {
                SessionManager.ClearSession();
                this.Close();
            };
            mnuThoat.Click += (s, e) => Application.Exit();
            mnuHuongDan.Click += (s, e) => ShowGuideDialog();
            mnuThongTin.Click += (s, e) => ShowAboutDialog();
            mnuTraCuuVe.Click += (s, e) => OpenBookingHistory();
            mnuLichSuDatVe.Click += (s, e) => OpenBookingHistory();
        }

        private async void FormMain_KhachHang_Load(object? sender, EventArgs e)
        {
            await LoadRoutesAsync();
        }

        #region Xử lý nạp dữ liệu tuyến và chuyến

        private async Task LoadRoutesAsync()
        {
            try
            {
                SetLoading(true, "Đang tải danh sách tuyến xe...");
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
                lblStatus.Text = "Lỗi nạp tuyến xe: " + ex.Message;
            }
            finally
            {
                SetLoading(false, "Sẵn sàng");
            }
        }

        private async Task OnRouteChangedAsync()
        {
            if (cboTuyenXe.SelectedValue is not int routeId || routeId <= 0) return;

            try
            {
                SetLoading(true, "Đang tìm phương tiện phục vụ tuyến...");
                _vehicles = await _ticketService.GetVehiclesByRouteAsync(routeId);

                var tripItems = new List<TripDisplayItem>();
                DateTime chosenDate = dtpNgayDi.Value.Date;

                // Các khung giờ chạy chuẩn trong ngày
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
                lblStatus.Text = "Lỗi tìm chuyến xe: " + ex.Message;
            }
            finally
            {
                SetLoading(false, "Sẵn sàng");
            }
        }

        private async Task LoadSeatMapForSelectedTripAsync()
        {
            if (cboChuyenXe.SelectedItem is not TripDisplayItem trip) return;

            try
            {
                SetLoading(true, $"Đang tải sơ đồ ghế xe {trip.LicensePlate}...");

                var selectedRoute = _routes.FirstOrDefault(r => r.Id == (int)(cboTuyenXe.SelectedValue ?? 0));
                decimal basePrice = selectedRoute?.BasePrice ?? 250000m;

                _currentSeatMap = await _seatMapService.GetVehicleSeatMapAsync(trip.VehicleId, trip.DepartureTime, basePrice);
                seatMapControl.LoadSeatMap(_currentSeatMap);

                lblDonGia.Text = $"{_currentSeatMap.BasePrice:N0} VNĐ / vé";
                OnSeatSelectionChanged(new List<SeatVisualItem>());
                lblStatus.Text = $"Đã tải sơ đồ xe {trip.LicensePlate} ({trip.VehicleType}). Vui lòng chọn ghế!";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi tải sơ đồ ghế: " + ex.Message;
            }
            finally
            {
                SetLoading(false);
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

            // Tính lại khuyến mãi nếu có
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
            SetLoading(true, "Đang kiểm tra mã khuyến mãi...");

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
                SetLoading(false, "Sẵn sàng");
            }
        }

        #endregion

        #region Validation bằng ErrorProvider (Chương 4)

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

        #region Đặt vé & Xuất vé PDF

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

            SetLoading(true, "Đang xử lý đặt vé & ghi nhận hệ thống...");

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
                    _lastBookedTicketIds = result.CreatedTicketIds;

                    lblStatus.Text = result.Message;
                    System.Media.SystemSounds.Asterisk.Play();

                    // Tải lại sơ đồ ghế để ghế vừa đặt chuyển sang màu xám Đã bán
                    await LoadSeatMapForSelectedTripAsync();

                    MessageBox.Show(
                        $"{result.Message}\n\nVé của quý khách đã được lưu vào hệ thống thành công!\nQuý khách có thể xem lại, IN VÉ hoặc HỦY VÉ bất kỳ lúc nào tại menu 'Vé của tôi > Lịch sử đặt vé' (phím tắt Ctrl+H).",
                        "Đặt Vé Thành Công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
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
                SetLoading(false, "Sẵn sàng");
            }
        }

        private async Task OpenTicketPrintPreviewAsync()
        {
            if (_lastBookedTicketIds == null || _lastBookedTicketIds.Count == 0)
            {
                MessageBox.Show("Chưa có vé nào được đặt trong lượt này để xuất!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SetLoading(true, "Đang chuẩn bị mẫu vé điện tử...");
            try
            {
                var receipts = await _bookingService.GetTicketReceiptsAsync(_lastBookedTicketIds);
                if (receipts.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin vé cần in!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var previewForm = new FrmTicketPrintPreview(receipts);
                previewForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị bản in vé: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoading(false, "Sẵn sàng");
            }
        }

        #endregion

        #region Tra cứu vé & Xem lịch sử (FrmCustomerTicketHistory)

        private void OpenBookingHistory(string? searchKeyword = null)
        {
            string? phone = searchKeyword;
            if (string.IsNullOrWhiteSpace(phone))
            {
                phone = SessionManager.CurrentUser?.PhoneNumber;
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                phone = txtSoDienThoai.Text.Trim();
            }

            using var frm = new FrmCustomerTicketHistory(phone);
            frm.ShowDialog(this);
        }

        #endregion

        #region Phím tắt & Trợ giúp

        private void FormMain_KhachHang_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                e.Handled = true;
                btnTaiSoDo.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                e.Handled = true;
                btnDatVe.PerformClick();
            }
            else if ((e.Control && e.KeyCode == Keys.H) || (e.Control && e.KeyCode == Keys.P))
            {
                e.Handled = true;
                OpenBookingHistory();
            }
        }

        private void ShowGuideDialog()
        {
            string guide = "HƯỚNG DẪN ĐẶT VÉ TRỰC QUAN BẰNG SƠ ĐỒ GDI+:\n\n" +
                           "1. Chọn Tuyến xe và Ngày khởi hành mong muốn.\n" +
                           "2. Chọn khung giờ & Biển số xe cụ thể trong danh sách.\n" +
                           "3. Quan sát sơ đồ chỗ ngồi:\n" +
                           "   - Ghế màu Xanh nhạt: Còn trống (Click để chọn).\n" +
                           "   - Ghế màu Cam: Đang chọn (Click lần nữa để hủy chọn).\n" +
                           "   - Ghế màu Xám: Đã bán (Không thể chọn).\n" +
                           "4. Điền họ tên và SĐT (10 số, bắt đầu bằng 0).\n" +
                           "5. Nhập mã khuyến mãi (VD: TET2026, VIP10, GIAM50K) nếu có.\n" +
                           "6. Nhấn ĐẶT VÉ (F5 / Ctrl+S) -> Vào 'Vé của tôi > Lịch sử đặt vé' (Ctrl+H) để In vé hoặc Hủy vé (trước 1 giờ xuất bến).\n\n" +
                           "Chúc quý khách có chuyến đi thượng lộ bình an!";

            MessageBox.Show(guide, "Hướng Dẫn Đặt Vé", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowAboutDialog()
        {
            string about = "HỆ THỐNG QUẢN LÝ VẬN TẢI HÀNH KHÁCH\n" +
                           "Phân hệ: Đặt vé trực quan & Xuất vé điện tử PDF nhúng mã QR\n" +
                           "Nền tảng: .NET 10 / C# 13+ (Lập trình trực quan LTTQ)\n" +
                           "Tác giả: Nhóm phát triển đồ án Quản Lý Vận Tải";

            MessageBox.Show(about, "Thông Tin Ứng Dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetLoading(bool isLoading, string statusText = "Đang xử lý...")
        {
            prgAction.Visible = isLoading;
            lblStatus.Text = statusText;
            btnDatVe.Enabled = !isLoading;
            btnTaiSoDo.Enabled = !isLoading;
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
