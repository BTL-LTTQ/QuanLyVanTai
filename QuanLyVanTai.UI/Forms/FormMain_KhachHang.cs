using System.ComponentModel;
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

        private List<Route> _routes = new();
        private List<Vehicle> _vehicles = new();
        private VehicleSeatMapDto? _currentSeatMap;

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

            ThemeConfig.StylePrimaryButton(btnXuatVe, IconChar.Print);
            btnXuatVe.Size = new Size(200, 42);
            btnXuatVe.Enabled = false;

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

            // MenuStrip handlers
            mnuDangXuat.Click += (s, e) =>
            {
                SessionManager.ClearSession();
                this.Close();
            };
            mnuThoat.Click += (s, e) => Application.Exit();
            mnuHuongDan.Click += (s, e) => ShowGuideDialog();
            mnuThongTin.Click += (s, e) => ShowAboutDialog();
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

        #region Tương tác chọn ghế trên sơ đồ GDI+

        private void OnSeatSelectionChanged(List<SeatVisualItem> selectedSeats)
        {
            if (selectedSeats == null || selectedSeats.Count == 0)
            {
                lblDanhSachGhe.Text = "Chưa chọn ghế";
                lblDanhSachGhe.ForeColor = Color.FromArgb(71, 85, 105);
                lblTongTien.Text = "0 VNĐ";
                lblTienGiam.Text = "-0 VNĐ";
                return;
            }

            var seatCodes = selectedSeats.Select(s => s.SeatCode).ToList();
            lblDanhSachGhe.Text = $"{string.Join(", ", seatCodes)} ({selectedSeats.Count} chỗ)";
            lblDanhSachGhe.ForeColor = Color.FromArgb(37, 99, 235);

            decimal subTotal = selectedSeats.Sum(s => s.FinalPrice);
            lblTienGiam.Text = "-0 VNĐ";
            lblTongTien.Text = $"{subTotal:N0} VNĐ";
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
        }

        private void ShowGuideDialog()
        {
            string guide = "HƯỚNG DẪN ĐẶT VÉ TRỰC QUAN BẰNG SƠ ĐỒ GDI+:\n\n" +
                           "1. Chọn Tuyến xe và Ngày khởi hành mong muốn.\n" +
                           "2. Chọn khung giờ & Biển số xe cụ thể trong danh sách.\n" +
                           "3. Quan sát sơ đồ chỗ ngồi:\n" +
                           "   - Ghế màu Xanh nhạt: Còn trống (Click để chọn).\n" +
                           "   - Ghế màu Cam: Đang chọn (Click lần nữa để hủy chọn).\n" +
                           "   - Ghế màu Xám: Đã bán (Không thể chọn).\n\n" +
                           "Chúc quý khách có chuyến đi thượng lộ bình an!";

            MessageBox.Show(guide, "Hướng Dẫn Đặt Vé", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowAboutDialog()
        {
            string about = "HỆ THỐNG QUẢN LÝ VẬN TẢI HÀNH KHÁCH\n" +
                           "Phân hệ: Đặt vé trực quan bằng sơ đồ GDI+\n" +
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
