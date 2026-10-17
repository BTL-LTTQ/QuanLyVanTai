using System.ComponentModel;
using Core.Configs;
using Core.Helpers;
using QuanLyVanTai.UI.UserControls;

namespace QuanLyVanTai.UI.Forms
{
    public partial class FormMain_KhachHang : Form
    {
        private UserControl? _currentView;

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

            // Cập nhật thông tin phiên đăng nhập
            if (SessionManager.IsLoggedIn && SessionManager.CurrentUser != null)
            {
                var user = SessionManager.CurrentUser;
                lblCurrentUser.Text = $"👤 {user.FullName} ({user.Role})";
            }
            else
            {
                lblCurrentUser.Text = "👤 Khách Hàng Trực Tuyến";
            }

            lblClock.Text = $"🕒 {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
            timerClock.Interval = 1000;
            timerClock.Start();
        }

        private void WireEvents()
        {
            this.Load += (s, e) => ShowBookingView();
            this.KeyDown += FormMain_KhachHang_KeyDown;

            timerClock.Tick += (s, e) =>
            {
                lblClock.Text = $"🕒 {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
            };

            // Điều hướng chức năng
            mnuDatVe.Click += (s, e) => ShowBookingView();
            tsBtnBooking.Click += (s, e) => ShowBookingView();

            mnuBanDoLoTrinh.Click += (s, e) => ShowRouteMapView();
            tsBtnRouteMap.Click += (s, e) => ShowRouteMapView();

            mnuLichSuDatVe.Click += (s, e) => ShowHistoryView();
            tsBtnHistory.Click += (s, e) => ShowHistoryView();

            // Làm mới
            mnuLamMoi.Click += (s, e) => RefreshCurrentView();
            tsBtnRefresh.Click += (s, e) => RefreshCurrentView();

            // Hướng dẫn & Giới thiệu
            mnuHuongDan.Click += (s, e) => ShowGuideDialog();
            tsBtnGuide.Click += (s, e) => ShowGuideDialog();
            mnuThongTin.Click += (s, e) => ShowAboutDialog();

            // Đăng xuất & Thoát
            mnuDangXuat.Click += (s, e) => PerformLogout();
            tsBtnLogout.Click += (s, e) => PerformLogout();
            mnuThoat.Click += (s, e) => Application.Exit();
        }

        #region Cơ Chế Điều Hướng (View Injection)

        public void SwitchView(UserControl newView)
        {
            if (newView == null) return;

            pnlContentHost.SuspendLayout();
            try
            {
                if (_currentView != null)
                {
                    pnlContentHost.Controls.Remove(_currentView);
                    _currentView.Dispose();
                    _currentView = null;
                }

                newView.Dock = DockStyle.Fill;
                pnlContentHost.Controls.Add(newView);
                newView.BringToFront();
                _currentView = newView;
            }
            finally
            {
                pnlContentHost.ResumeLayout();
            }
        }

        public void ShowBookingView(int? initialRouteId = null)
        {
            var bookingView = new UcCustomerBooking(initialRouteId);
            bookingView.OnStatusChanged += (msg, isLoading) => SetStatus(msg, isLoading);
            bookingView.OnNavigateToHistory += () => ShowHistoryView();
            bookingView.OnNavigateToMap += (routeId) => ShowRouteMapView(routeId);

            SwitchView(bookingView);
            SetStatus("Màn hình: Đặt vé trực quan bằng sơ đồ ghế", false);
            UpdateNavigationButtonState(isBooking: true, isRouteMap: false);
        }

        public void ShowRouteMapView(int? initialRouteId = null)
        {
            var mapView = new UcCustomerRouteMap(initialRouteId);
            mapView.OnStatusChanged += (msg, isLoading) => SetStatus(msg, isLoading);
            mapView.OnNavigateToBooking += (targetRouteId) => ShowBookingView(targetRouteId);

            SwitchView(mapView);
            SetStatus("Màn hình: Bản đồ lộ trình tuyến xe & vị trí trạm dừng (Map API)", false);
            UpdateNavigationButtonState(isBooking: false, isRouteMap: true);
        }

        public void ShowHistoryView(string? phoneOrKeyword = null)
        {
            string? searchPhone = phoneOrKeyword;
            if (string.IsNullOrWhiteSpace(searchPhone))
            {
                searchPhone = SessionManager.CurrentUser?.PhoneNumber;
            }

            var historyView = new UcCustomerTicketHistory(searchPhone);
            historyView.OnStatusChanged += (msg, isLoading) => SetStatus(msg, isLoading);
            historyView.OnNavigateToBooking += () => ShowBookingView();

            SwitchView(historyView);
            SetStatus("Màn hình: Lịch sử đặt vé & In vé điện tử", false);
            UpdateNavigationButtonState(isBooking: false, isRouteMap: false);
        }

        private void RefreshCurrentView()
        {
            if (_currentView is UcCustomerBooking bookingView)
            {
                _ = bookingView.LoadRoutesAsync();
            }
            else if (_currentView is UcCustomerRouteMap mapView)
            {
                _ = mapView.LoadRoutesAsync();
            }
            else if (_currentView is UcCustomerTicketHistory historyView)
            {
                _ = historyView.RefreshDataAsync();
            }
        }

        private void UpdateNavigationButtonState(bool isBooking, bool isRouteMap = false)
        {
            tsBtnBooking.Checked = isBooking;
            tsBtnRouteMap.Checked = isRouteMap;
            tsBtnHistory.Checked = !isBooking && !isRouteMap;
        }

        public void SetStatus(string text, bool isLoading)
        {
            lblStatus.Text = text;
            prgAction.Visible = isLoading;
        }

        #endregion

        #region Phím Tắt & Tác Vụ Phụ

        private void FormMain_KhachHang_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                e.Handled = true;
                ShowGuideDialog();
            }
            else if (e.KeyCode == Keys.F5)
            {
                e.Handled = true;
                RefreshCurrentView();
            }
            else if (e.Control && e.KeyCode == Keys.B)
            {
                e.Handled = true;
                ShowBookingView();
            }
            else if (e.Control && e.KeyCode == Keys.M)
            {
                e.Handled = true;
                ShowRouteMapView();
            }
            else if (e.Control && e.KeyCode == Keys.H)
            {
                e.Handled = true;
                ShowHistoryView();
            }
            else if (e.Control && e.KeyCode == Keys.Q)
            {
                e.Handled = true;
                PerformLogout();
            }
        }

        private void PerformLogout()
        {
            if (MessageBox.Show("Quý khách có chắc chắn muốn đăng xuất tài khoản không?",
                    "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SessionManager.ClearSession();
                this.Close();
            }
        }

        private void ShowGuideDialog()
        {
            string guide = "HƯỚNG DẪN ĐẶT VÉ TRỰC QUAN BẰNG SƠ ĐỒ GDI+:\n\n" +
                           "1. Chọn Tuyến xe và Ngày khởi hành mong muốn.\n" +
                           "2. Chọn khung giờ & Biển số xe cụ thể trong danh sách chuyến.\n" +
                           "3. Quan sát sơ đồ chỗ ngồi:\n" +
                           "   - Ghế màu Xanh nhạt: Còn trống (Click để chọn).\n" +
                           "   - Ghế màu Cam: Đang chọn (Click lần nữa để hủy chọn).\n" +
                           "   - Ghế màu Xám: Đã bán (Không thể chọn).\n" +
                           "4. Điền họ tên và SĐT (10 số, bắt đầu bằng 0).\n" +
                           "5. Nhập mã khuyến mãi (VD: TET2026, VIP10, GIAM50K) nếu có.\n" +
                           "6. Nhấn XÁC NHẬN ĐẶT VÉ -> Vào tab 'Vé Của Tôi' (Ctrl+H) để In vé hoặc Hủy vé (trước 1 giờ xuất bến).\n\n" +
                           "Chúc quý khách có chuyến đi thượng lộ bình an!";

            MessageBox.Show(guide, "Hướng Dẫn Sử Dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowAboutDialog()
        {
            string about = "HỆ THỐNG QUẢN LÝ VẬN TẢI HÀNH KHÁCH\n" +
                           "Phân hệ: Đặt vé trực quan & Xuất vé điện tử PDF nhúng mã QR\n" +
                           "Nền tảng: .NET 10 / C# 13+ (Lập trình trực quan LTTQ)\n" +
                           "Kiến trúc: View Injection với UserControl chuẩn Desktop";

            MessageBox.Show(about, "Thông Tin Ứng Dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}
