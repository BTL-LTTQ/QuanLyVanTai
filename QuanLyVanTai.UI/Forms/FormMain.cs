using Core.Configs;
using Core.Helpers;
using Core.Logging;
using Core.Security;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL;
using QuanLyVanTai.UI.UserControls;
using UserSession = Core.Security.UserSession;

namespace QuanLyVanTai.UI
{
    public partial class FormMain : Form
    {
        private readonly PermissionService _permissionService = new();
        private readonly AuditLogService _auditLogService = new();
        private readonly List<Image> _navIcons = [];
        private bool _isChangingLanguage = false;
        private UcAiChatbot? _chatbotWidget;
        private UserControl? _currentView;
        private string? _statusPrefix;
        private string? _currentModuleTitle;

        public FormMain()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || this.DesignMode)
            {
                return;
            }

            this.Font = ThemeConfig.MainFont;
            this.Text = "Hệ Thống Quản Lý Vận Tải Hành Khách (BTL LTTQ)";
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(1024, 650);

            ApplyNavigationIcons();
            SetupCanhBao();
            UserSession.SessionChanged += OnSessionChanged;
            btnDong.Click += btnDong_Click;

            // Timer đồng hồ hệ thống trên StatusStrip
            timerClock.Tick += (s, e) => UpdateClock();
            UpdateClock();

            // Đăng ký nghe vi phạm an ninh từ AuthorizationGuard để ghi Audit Log
            AuthorizationGuard.UnauthorizedAttemptDetected += (menu, action, msg) =>
            {
                _ = _auditLogService.LogActionAsync("Truy cập trái phép", menu, null, msg);
            };

            this.Load += FormMain_Load;

            SetupLanguageControls();
            UpdateUserInfoDisplay();
            CultureHelper.CultureChanged += OnCultureChanged;
            UpdateLocalizedTexts();
        }

        private async void FormMain_Load(object? sender, EventArgs e)
        {
            try
            {
                using var db = new AppDbContext();
                await DatabaseInitializer.SeedSampleDataAsync(db);
                await _permissionService.LoadPermissionsToUserSessionAsync(UserSession.CurrentRole);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DB Init: {ex.Message}");
            }

            UpdateMenuPermissions();
            UpdateUserInfoDisplay();

            // Hiển thị màn hình mặc định là Dashboard tổng quan
            mnuDashboard_Click(this, EventArgs.Empty);

            // Khởi tạo AI Chatbot Widget nổi ở góc dưới phải
            InitializeChatbotWidget();
        }

        private void OnSessionChanged()
        {
            UpdateMenuPermissions();
            UpdateUserInfoDisplay();
        }

        private void UpdateUserInfoDisplay()
        {
            var user = SessionManager.CurrentUser;
            if (user != null)
            {
                lblStatusUser.Text = $"{UiTranslator.Translate(user.FullName)} ({UiTranslator.Translate(user.Role)})";
            }
            else
            {
                string role = UserSession.CurrentRole;
                string username = UserSession.CurrentUsername;
                lblStatusUser.Text = $"{username} ({UiTranslator.Translate(role)})";
            }
        }

        private void UpdateClock()
        {
            lblClock.Text = $"{DateTime.Now:HH:mm:ss  dd/MM/yyyy}";
        }

        private void UpdateMenuPermissions()
        {
            // Ẩn hẳn chức năng không có quyền (thay vì để xám gây rối mắt)
            SetMenuVisible(mnuTuyenXe, SystemMenus.Route);
            SetMenuVisible(mnuPhuongTien, SystemMenus.Vehicle);
            SetMenuVisible(mnuNhanSu, SystemMenus.Staff);
            SetMenuVisible(mnuLichTrinh, SystemMenus.Schedule);
            SetMenuVisible(mnuKhuyenMai, SystemMenus.Promotion);
            SetMenuVisible(mnuDichVu, SystemMenus.Service);
            SetMenuVisible(mnuCheckInQr, SystemMenus.Ticket);
            SetMenuVisible(mnuDashboard, SystemMenus.Route);
            SetMenuVisible(mnuAdoReport, SystemMenus.AuditLog);
            SetMenuVisible(mnuNhatKy, SystemMenus.AuditLog);
            SetMenuVisible(mnuPhanQuyen, SystemMenus.Permission);

            tsBtnDashboard.Visible = UserSession.HasPermission(SystemMenus.Route, PermissionAction.View);
            tsBtnDatVe.Visible = UserSession.HasPermission(SystemMenus.Ticket, PermissionAction.View);
            tsBtnCheckInQr.Visible = UserSession.HasPermission(SystemMenus.Ticket, PermissionAction.View);

            // Nhóm menu cấp 1 chỉ hiện khi còn ít nhất một mục con được phép sử dụng
            foreach (var topMenu in msMain.Items.OfType<ToolStripMenuItem>())
            {
                if (topMenu.DropDownItems.Count == 0)
                    continue;

                topMenu.Visible = topMenu.DropDownItems
                    .OfType<ToolStripMenuItem>()
                    .Any(item => item.Available);
            }
        }

        private static void SetMenuVisible(ToolStripMenuItem menuItem, string menuCode)
            => menuItem.Available = UserSession.HasPermission(menuCode, PermissionAction.View);

        /// <summary>Gán icon FontAwesome thống nhất cho menu, toolbar và nhãn ngôn ngữ.</summary>
        private void ApplyNavigationIcons()
        {
            mnuLamMoi.Image = NavIcon(IconChar.ArrowsRotate);
            mnuDangXuat.Image = NavIcon(IconChar.RightFromBracket, ThemeConfig.DangerColor);
            mnuThoat.Image = NavIcon(IconChar.DoorOpen);

            mnuTuyenXe.Image = NavIcon(IconChar.Route);
            mnuPhuongTien.Image = NavIcon(IconChar.Bus);
            mnuNhanSu.Image = NavIcon(IconChar.Users);
            mnuLichTrinh.Image = NavIcon(IconChar.CalendarDays);
            mnuKhuyenMai.Image = NavIcon(IconChar.Percent);
            mnuDichVu.Image = NavIcon(IconChar.BellConcierge);
            mnuCheckInQr.Image = NavIcon(IconChar.Qrcode);

            mnuDashboard.Image = NavIcon(IconChar.ChartPie);
            mnuAdoReport.Image = NavIcon(IconChar.FileInvoiceDollar);
            mnuNhatKy.Image = NavIcon(IconChar.ShieldHalved);
            mnuPhanQuyen.Image = NavIcon(IconChar.UserShield);
            mnuThongTin.Image = NavIcon(IconChar.CircleInfo);

            tsBtnDashboard.Image = NavIcon(IconChar.ChartPie);
            tsBtnDatVe.Image = NavIcon(IconChar.Tags);
            tsBtnCheckInQr.Image = NavIcon(IconChar.Qrcode);
            tsBtnLamMoi.Image = NavIcon(IconChar.ArrowsRotate);
            tsLblLanguage.Image = NavIcon(IconChar.Globe);
            tsBtnDangXuat.Image = NavIcon(IconChar.RightFromBracket, ThemeConfig.DangerColor);
        }

        private Image NavIcon(IconChar icon, Color? color = null)
        {
            var bitmap = FormsIconHelper.ToBitmap(icon, (Color?)color ?? Color.FromArgb(51, 65, 85), 20, 0, FlipOrientation.Normal);
            _navIcons.Add(bitmap);
            return bitmap;
        }

        private void SetupLanguageControls()
        {
            tsCboLanguage.ComboBox.DisplayMember = "DisplayName";
            tsCboLanguage.ComboBox.ValueMember = "Code";
            tsCboLanguage.ComboBox.DataSource = CultureHelper.SupportedCultures
                .Select(c => new { Code = c.Code, DisplayName = $"{c.Flag} {c.DisplayName}" })
                .ToList();

            string current = CultureHelper.CurrentCulture.Name;
            var match = CultureHelper.SupportedCultures.FirstOrDefault(c => c.Code.Equals(current, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(match.Code))
            {
                tsCboLanguage.ComboBox.SelectedValue = match.Code;
            }

            tsCboLanguage.SelectedIndexChanged += (s, e) =>
            {
                if (_isChangingLanguage || tsCboLanguage.ComboBox.SelectedValue == null) return;
                CultureHelper.SetCulture(tsCboLanguage.ComboBox.SelectedValue.ToString()!, savePreference: true);
            };
        }

        private void OnCultureChanged()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(OnCultureChanged));
                return;
            }

            _isChangingLanguage = true;
            try
            {
                string current = CultureHelper.CurrentCulture.Name;
                var match = CultureHelper.SupportedCultures.FirstOrDefault(c => c.Code.Equals(current, StringComparison.OrdinalIgnoreCase));
                if (!string.IsNullOrEmpty(match.Code))
                {
                    tsCboLanguage.ComboBox.SelectedValue = match.Code;
                }
                UpdateLocalizedTexts();
                UpdateUserInfoDisplay();
            }
            finally
            {
                _isChangingLanguage = false;
            }
        }

        private void UpdateLocalizedTexts()
        {
            try
            {
                Text = CultureHelper.GetString("AppTitle");
            }
            catch
            {
                // Bỏ qua lỗi tài nguyên ngôn ngữ
            }

            // Ghép lại dòng trạng thái theo ngôn ngữ mới (không thể dịch chuỗi ghép bằng quét từ điển)
            if (_currentModuleTitle != null && _statusPrefix != null)
            {
                lblStatusReady.Text = $"{UiTranslator.Translate(_statusPrefix)} {UiTranslator.Translate(_currentModuleTitle)}";
            }
        }

        // ==========================================
        // CƠ CHẾ ĐIỀU HƯỚNG & HIỂN THỊ (VIEW INJECTION)
        // ==========================================
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
                UiTranslator.Apply(newView);
                _currentView = newView;

                // Đảm bảo AI Chatbot Widget luôn nổi trên cùng
                _chatbotWidget?.BringToFront();
            }
            finally
            {
                pnlContentHost.ResumeLayout();
            }
        }

        private void OpenMenu(string menuCode, string title, Func<UserControl> createControl, ToolStripMenuItem? menuItem = null)
        {
            // 1. Kiểm tra quyền trước khi nạp Form
            if (!AuthorizationGuard.CheckAccess(menuCode, PermissionAction.View, showWarning: false))
            {
                AnNoiDungCu();
                HienCanhBao(string.Format(
                    UiTranslator.Translate("BẢO MẬT HỆ THỐNG:\n\nTài khoản '{0}' (Vai trò: {1}) bị CHẶN truy cập vào chức năng '{2}'.\n\nHành động mở Form trái phép qua code đã được ghi lại trong Nhật ký an ninh."),
                    UserSession.CurrentUsername, UiTranslator.Translate(UserSession.CurrentRole), UiTranslator.Translate(title)));
                _statusPrefix = "Truy cập bị từ chối:";
                _currentModuleTitle = title;
                lblStatusReady.Text = $"{UiTranslator.Translate(_statusPrefix)} {UiTranslator.Translate(title)}";
                return;
            }

            // 2. Nếu được phép -> Ẩn cảnh báo, nạp UserControl
            AnCanhBao();
            var control = createControl();
            SwitchView(control);
            SetActiveMenu(menuItem);
            _statusPrefix = "Đang làm việc:";
            _currentModuleTitle = title;
            lblStatusReady.Text = $"{UiTranslator.Translate(_statusPrefix)} {UiTranslator.Translate(title)}";
        }

        /// <summary>Đánh dấu checkmark ở mục menu tương ứng với màn hình đang mở.</summary>
        private void SetActiveMenu(ToolStripMenuItem? activeItem)
        {
            foreach (var topMenu in msMain.Items.OfType<ToolStripMenuItem>())
            {
                foreach (var child in topMenu.DropDownItems.OfType<ToolStripMenuItem>())
                {
                    child.Checked = ReferenceEquals(child, activeItem);
                }
            }
        }

        private void AnNoiDungCu()
        {
            if (_currentView != null)
            {
                pnlContentHost.Controls.Remove(_currentView);
                _currentView.Dispose();
                _currentView = null;
            }
            pnlContentHost.Controls.Add(pnlCanhBao);
        }

        // ==========================================
        // MENU & TOOLSTRIP CLICK HANDLERS
        // ==========================================
        private void mnuDashboard_Click(object? sender, EventArgs e)
        {
            OpenMenu(SystemMenus.Route, "Dashboard tổng quan", () => new UcDashboard(), mnuDashboard);
        }

        private void mnuTuyenXe_Click(object? sender, EventArgs e)
        {
            OpenMenu(SystemMenus.Route, "Quản lý tuyến xe & lộ trình", () => new UcManageRoute(), mnuTuyenXe);
        }

        private void mnuLichTrinh_Click(object? sender, EventArgs e)
        {
            OpenMenu(SystemMenus.Schedule, "Điều độ lịch trình & phân công tài xế", () => new UcManageSchedule(), mnuLichTrinh);
        }

        private void mnuPhuongTien_Click(object? sender, EventArgs e)
        {
            OpenMenu(SystemMenus.Vehicle, "Quản lý phương tiện & trạm dừng", () => new UcManageVehicleAndStation(), mnuPhuongTien);
        }

        private void tsBtnDatVe_Click(object? sender, EventArgs e)
        {
            OpenMenu(SystemMenus.Ticket, "Quản lý bảng giá vé", () => new UcManageTicket());
        }

        private void mnuNhanSu_Click(object? sender, EventArgs e)
        {
            OpenMenu(SystemMenus.Staff, "Quản lý nhân sự", () => new UcManageStaff(), mnuNhanSu);
        }

        private void mnuKhuyenMai_Click(object? sender, EventArgs e)
        {
            OpenMenu(SystemMenus.Promotion, "Quản lý chương trình khuyến mãi", () => new UcManagePromotion(), mnuKhuyenMai);
        }

        private void mnuDichVu_Click(object? sender, EventArgs e)
        {
            OpenMenu(SystemMenus.Service, "Quản lý dịch vụ vận tải", () => new UcManageService(), mnuDichVu);
        }

        private void mnuCheckInQr_Click(object? sender, EventArgs e)
        {
            using var frm = new Forms.FrmPassengerCheckIn();
            frm.ShowDialog(this);
        }

        private void mnuPhanQuyen_Click(object? sender, EventArgs e)
        {
            OpenMenu(SystemMenus.Permission, "Quản lý phân quyền hệ thống", () => new UcPermission(), mnuPhanQuyen);
        }

        private void mnuNhatKy_Click(object? sender, EventArgs e)
        {
            OpenMenu(SystemMenus.AuditLog, "Nhật ký hoạt động (Audit Trail)", () => new UcAuditLog(), mnuNhatKy);
        }

        private void mnuAdoReport_Click(object? sender, EventArgs e)
        {
            OpenMenu(SystemMenus.AuditLog, "Báo cáo doanh thu — ADO.NET thuần", () => new UcAdoReport(), mnuAdoReport);
        }

        private void mnuLamMoi_Click(object? sender, EventArgs e)
        {
            if (_currentView is UcManageRoute)
                mnuTuyenXe_Click(sender, e);
            else if (_currentView is UcManageSchedule)
                mnuLichTrinh_Click(sender, e);
            else if (_currentView is UcManageVehicleAndStation)
                mnuPhuongTien_Click(sender, e);
            else if (_currentView is UcManageTicket)
                tsBtnDatVe_Click(sender, e);
            else if (_currentView is UcManageStaff)
                mnuNhanSu_Click(sender, e);
            else if (_currentView is UcManagePromotion)
                mnuKhuyenMai_Click(sender, e);
            else if (_currentView is UcManageService)
                mnuDichVu_Click(sender, e);
            else if (_currentView is UcPermission)
                mnuPhanQuyen_Click(sender, e);
            else if (_currentView is UcAuditLog)
                mnuNhatKy_Click(sender, e);
            else
                mnuDashboard_Click(sender, e);
        }

        private void mnuDangXuat_Click(object? sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?",
                "Xác Nhận Đăng Xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                SessionManager.ClearSession();
                this.Close();
            }
        }

        private void mnuThoat_Click(object? sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát khỏi ứng dụng?",
                "Xác Nhận Thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void mnuThongTin_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(
                "HỆ THỐNG QUẢN LÝ VẬN TẢI HÀNH KHÁCH (BTL LTTQ)\n\n" +
                "• Nền tảng: .NET 10 (C# 13)\n" +
                "• Kiến trúc: 3 tầng (UI - BLL - DAL)\n" +
                "• CSDL: SQL Server (EF Core Code-First + ADO.NET)\n" +
                "• Thiết kế chuẩn Desktop: MenuStrip, ToolStrip, StatusStrip, UserControls\n" +
                "• Đồ án Lập trình trực quan (LTTQ)\n" +
                "• Trường Đại học Giao thông Vận tải - Phân hiệu Thành phố Hồ Chí Minh\n\n" +
                "Phiên bản: 1.0.0",
                "Thông Tin Phần Mềm",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // ==========================================
        // CẢNH BÁO AN NINH
        // ==========================================
        private void SetupCanhBao()
        {
            picCanhBao.Image = SystemIcons.Warning.ToBitmap();

            lblTieuDe.Text = "TRUY CẬP BỊ TỪ CHỐI";
            lblTieuDe.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTieuDe.ForeColor = ThemeConfig.DangerColor;

            lblNoiDung.Font = new Font("Segoe UI", 9.5F);
            lblNoiDung.ForeColor = Color.FromArgb(71, 85, 105);

            btnDong.Text = "Đóng";
            btnDong.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDong.ForeColor = Color.White;
            btnDong.BackColor = ThemeConfig.PrimaryColor;
            btnDong.FlatStyle = FlatStyle.Flat;
            btnDong.FlatAppearance.BorderSize = 0;

            pnlContentHost.Resize += (s, e) =>
            {
                pnlCanhBao.Location = new Point(
                    (pnlContentHost.Width - pnlCanhBao.Width) / 2,
                    (pnlContentHost.Height - pnlCanhBao.Height) / 2
                );
            };
        }

        private void HienCanhBao(string noiDung)
        {
            lblNoiDung.Text = noiDung;
            pnlCanhBao.Visible = true;
            pnlCanhBao.BringToFront();
        }

        private void AnCanhBao()
        {
            pnlCanhBao.Visible = false;
        }

        private void btnDong_Click(object? sender, EventArgs e)
        {
            AnCanhBao();
        }

        // ====================================================================================
        // AI CHATBOT WIDGET - Nổi góc dưới phải của Form (Floating Action Widget)
        // ====================================================================================
        private void InitializeChatbotWidget()
        {
            try
            {
                _chatbotWidget = new UcAiChatbot();
                
                // Cập nhật vị trí khi người dùng đóng/mở khung chat
                _chatbotWidget.ChatToggled += (s, e) => PositionChatbotWidget();

                // Thêm trực tiếp vào Form để nổi độc lập trên tất cả panel
                this.Controls.Add(_chatbotWidget);
                _chatbotWidget.BringToFront();

                // Đặt vị trí ban đầu và khi resize form
                PositionChatbotWidget();
                this.Resize += (s, e) => PositionChatbotWidget();
                this.Shown += (s, e) => PositionChatbotWidget();

                LoggerService.Info("AI Chatbot Widget đã được khởi tạo trong FormMain.");
            }
            catch (Exception ex)
            {
                LoggerService.Error(ex, "Không thể khởi tạo AI Chatbot Widget.");
            }
        }

        private void PositionChatbotWidget()
        {
            if (_chatbotWidget == null) return;
            // Căn lề góc dưới phải của Form với margin 18px (tránh StatusStrip 26px)
            int margin = 18;
            int x = this.ClientSize.Width - _chatbotWidget.Width - margin;
            int y = this.ClientSize.Height - _chatbotWidget.Height - margin - 26;
            _chatbotWidget.Location = new Point(Math.Max(10, x), Math.Max(10, y));
            _chatbotWidget.BringToFront();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            CultureHelper.CultureChanged -= OnCultureChanged;
            _chatbotWidget?.Dispose();
            _currentView?.Dispose();

            foreach (var icon in _navIcons)
                icon.Dispose();
            _navIcons.Clear();

            base.OnFormClosed(e);
        }
    }
}
