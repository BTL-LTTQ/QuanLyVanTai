using Core.Configs;
using Core.Security;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL;
using Core.Helpers;
using FontAwesome.Sharp;
using QuanLyVanTai.UI.UserControls;
using UserSession = Core.Security.UserSession;

namespace QuanLyVanTai.UI
{
    public partial class FormMain : Form
    {
        private readonly PermissionService _permissionService = new();
        private readonly AuditLogService _auditLogService = new();
        private Label lblUserGreeting = null!;
        private ComboBox cboMainLanguage = null!;
        private IconButton btnLogout = null!;
        private bool _isChangingLanguage = false;

        public FormMain()
        {
            InitializeComponent();
            this.Font = ThemeConfig.MainFont;
            this.Text = "Hệ Thống Quản Lý Vận Tải Hành Khách (BTL LTTQ)";
            this.WindowState = FormWindowState.Maximized;

            ApplyTheme();
            SetupCanhBao();
            UserSession.SessionChanged += OnSessionChanged;
            btnDong.Click += btnDong_Click;

            // Đăng ký nghe vi phạm an ninh từ AuthorizationGuard để ghi Audit Log
            AuthorizationGuard.UnauthorizedAttemptDetected += (menu, action, msg) =>
            {
                _ = _auditLogService.LogActionAsync("Truy cập trái phép", menu, null, msg);
            };

            this.Load += FormMain_Load;

            SetupHeaderAuthControls();
            CultureHelper.CultureChanged += OnCultureChanged;
            UpdateLocalizedTexts();
        }

        // --- CÁC HÀM CỦA NHÁNH FEATURE ---
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
                // Thông báo nhẹ, không làm gián đoạn
                System.Diagnostics.Debug.WriteLine($"DB Init: {ex.Message}");
            }

            // Mở mặc định Dashboard
            OpenMenu(SystemMenus.Route, "DASHBOARD - TRANG CHỦ", () => new UcDashboard());
        }

        // Role switcher removed from header to avoid visual clutter.
        // Session switching is now done through the proper login flow.

        private void OnSessionChanged()
        {
            UpdateSidebarPermissions();
        }

        private void UpdateSidebarPermissions()
        {
            // Cập nhật trạng thái hiển thị động của menu sidebar từ quyền trong CSDL
            void SetMenuVisual(FontAwesome.Sharp.IconButton btn, string menuCode)
            {
                bool canView = UserSession.HasPermission(menuCode, PermissionAction.View);
                btn.Enabled = true;
                if (!canView)
                {
                    btn.ForeColor = Color.FromArgb(148, 163, 184); // Xám mờ biểu thị không có quyền
                    btn.IconColor = Color.FromArgb(148, 163, 184);
                }
                else
                {
                    btn.ForeColor = Color.White;
                    btn.IconColor = Color.White;
                }
            }

            SetMenuVisual(btnTrangChu, SystemMenus.Route);
            SetMenuVisual(btnDichVu, SystemMenus.Vehicle);
            SetMenuVisual(btnNhanSu, SystemMenus.Staff);
            SetMenuVisual(btnGiaVe, SystemMenus.Ticket);
            SetMenuVisual(btnKhuyenMai, SystemMenus.Promotion);
            SetMenuVisual(btnPhanQuyen, SystemMenus.Permission);
            SetMenuVisual(btnNhatKy, SystemMenus.AuditLog);
        }

        // --- CÁC HÀM CỦA NHÁNH MAIN ---
        private void SetupHeaderAuthControls()
        {
            // 1. Nhãn chào người dùng
            lblUserGreeting = new Label
            {
                AutoSize = false,
                Size = new Size(260, 36),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F),
                TextAlign = ContentAlignment.MiddleRight,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            var user = SessionManager.CurrentUser;
            lblUserGreeting.Text = user != null
                ? $"👤 {user.FullName} ({user.Role})"
                : "👤 Khách (Guest)";

            // 2. Combobox ngôn ngữ
            cboMainLanguage = new ComboBox
            {
                Size = new Size(135, 36),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            cboMainLanguage.DisplayMember = "DisplayName";
            cboMainLanguage.ValueMember = "Code";
            cboMainLanguage.DataSource = CultureHelper.SupportedCultures
                .Select(c => new { Code = c.Code, DisplayName = $"{c.Flag} {c.DisplayName}" })
                .ToList();

            string current = CultureHelper.CurrentCulture.Name;
            var match = CultureHelper.SupportedCultures.FirstOrDefault(c => c.Code.Equals(current, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(match.Code))
                cboMainLanguage.SelectedValue = match.Code;

            cboMainLanguage.SelectedIndexChanged += (s, e) =>
            {
                if (_isChangingLanguage || cboMainLanguage.SelectedValue == null) return;
                CultureHelper.SetCulture(cboMainLanguage.SelectedValue.ToString()!, savePreference: true);
            };

            // 3. Nút Đăng xuất
            btnLogout = new IconButton
            {
                Size = new Size(130, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            ThemeConfig.StyleDangerButton(btnLogout, IconChar.RightFromBracket);
            btnLogout.Text = " Đăng xuất";
            btnLogout.Click += BtnLogout_Click;

            // Hàm định vị lại khi resize
            void RepositionHeaderControls()
            {
                int vCenter = (pnlHeader.Height - 36) / 2;
                int rightEdge = pnlHeader.Width - 16;

                btnLogout.Location = new Point(rightEdge - btnLogout.Width, vCenter);
                cboMainLanguage.Location = new Point(btnLogout.Left - cboMainLanguage.Width - 10, vCenter + 2);
                lblUserGreeting.Location = new Point(cboMainLanguage.Left - lblUserGreeting.Width - 6, vCenter);
            }

            pnlHeader.Controls.Add(lblUserGreeting);
            pnlHeader.Controls.Add(cboMainLanguage);
            pnlHeader.Controls.Add(btnLogout);

            RepositionHeaderControls();
            pnlHeader.Resize += (s, e) => RepositionHeaderControls();
        }

        private void BtnLogout_Click(object? sender, EventArgs e)
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
                    cboMainLanguage.SelectedValue = match.Code;
                }
                UpdateLocalizedTexts();
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
                lblTitle.Text = CultureHelper.GetString("AppTitle");
                btnTrangChu.Text = CultureHelper.CurrentCulture.Name.StartsWith("en") ? "Home" :
                                   CultureHelper.CurrentCulture.Name.StartsWith("ja") ? "ホーム" : "Trang Chủ";
                btnDichVu.Text = CultureHelper.CurrentCulture.Name.StartsWith("en") ? "Services" :
                                 CultureHelper.CurrentCulture.Name.StartsWith("ja") ? "サービス" : "Dịch vụ";
                btnLogout.Text = CultureHelper.CurrentCulture.Name.StartsWith("en") ? " Logout" :
                                 CultureHelper.CurrentCulture.Name.StartsWith("ja") ? " ログアウト" : " Đăng xuất";
            }
            catch
            {
                // Tránh lỗi khi cập nhật ngôn ngữ
            }
        }

        private void ApplyTheme()
        {
            // Content area
            pnlContent.BackColor = ThemeConfig.BackgroundColor;

            // Apply FontAwesome icons to sidebar buttons (Dock/size already set in Designer)
            void SetIcon(FontAwesome.Sharp.IconButton btn, FontAwesome.Sharp.IconChar icon)
            {
                btn.IconChar = icon;
                btn.IconColor = Color.FromArgb(148, 163, 184); // slate-400
                btn.IconSize = 20;
                btn.Font = new Font("Segoe UI", 9.5F);
            }

            SetIcon(btnTrangChu, FontAwesome.Sharp.IconChar.Route);
            SetIcon(btnDichVu,   FontAwesome.Sharp.IconChar.Bus);
            SetIcon(btnNhanSu,   FontAwesome.Sharp.IconChar.Users);
            SetIcon(btnGiaVe,    FontAwesome.Sharp.IconChar.Tag);
            SetIcon(btnKhuyenMai,FontAwesome.Sharp.IconChar.Percent);
            SetIcon(btnPhanQuyen,FontAwesome.Sharp.IconChar.ShieldHalved);
            SetIcon(btnNhatKy,   FontAwesome.Sharp.IconChar.ClockRotateLeft);
            SetIcon(btnCaiDat,   FontAwesome.Sharp.IconChar.Gear);

            // Setup cảnh báo security panel
            SetupCanhBao();
        }

        // ==========================================
        // CƠ CHẾ ĐIỀU HƯỚNG & HIỂN THỊ 
        // (Gộp bảo mật của feature + Try Catch của main)
        // ==========================================
        private void OpenMenu(string menuCode, string title, Func<UserControl> createControl)
        {
            // 1. Kiểm tra quyền trước khi nạp Form
            if (!AuthorizationGuard.CheckAccess(menuCode, PermissionAction.View, showWarning: false))
            {
                AnNoiDungCu();
                HienCanhBao($"BẢO MẬT HỆ THỐNG:\n\nTài khoản '{UserSession.CurrentUsername}' (Vai trò: {UserSession.CurrentRole}) bị CHẶN truy cập vào chức năng '{title}'.\n\nHành động mở Form trái phép qua code đã được ghi lại trong Nhật ký an ninh.");
                return;
            }

            // 2. Nếu được phép -> Ẩn cảnh báo, nạp UserControl
            AnCanhBao();
            lblTitle.Text = title;

            var control = createControl();
            ShowUserControl(control);
        }

        private void ShowUserControl(UserControl uc)
        {
            try
            {
                pnlContent.Controls.Clear();
                pnlContent.Controls.Add(pnlCanhBao); // Giữ panel cảnh báo bảo mật
                uc.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(uc);
                uc.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị nội dung: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AnNoiDungCu()
        {
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(pnlCanhBao);
        }

        // ==========================================
        // SIDEBAR BUTTON CLICKS
        // (Gộp OpenMenu của feature + Tiêu đề đa ngôn ngữ của main)
        // ==========================================
        private void btnTrangChu_Click(object? sender, EventArgs e)
        {
            string title = CultureHelper.CurrentCulture.Name.StartsWith("en") ? "OPERATIONS DASHBOARD" :
                           CultureHelper.CurrentCulture.Name.StartsWith("ja") ? "ダッシュボード" : "DASHBOARD - TRANG CHỦ";
            OpenMenu(SystemMenus.Route, title, () => new UcDashboard());
        }

        private void btnDichVu_Click(object? sender, EventArgs e)
        {
            string title = CultureHelper.CurrentCulture.Name.StartsWith("en") ? "SERVICES MANAGEMENT" :
                           CultureHelper.CurrentCulture.Name.StartsWith("ja") ? "サービス管理" : "QUẢN LÝ PHƯƠNG TIỆN & TRẠM DỪNG";
            OpenMenu(SystemMenus.Vehicle, title, () => new UcManageVehicleAndStation());
        }

        private void btnNhanSu_Click(object sender, EventArgs e)
        {
            OpenMenu(SystemMenus.Staff, "QUẢN LÝ NHÂN SỰ", () => new UcManageStaff());
        }

        private void btnGiaVe_Click(object sender, EventArgs e)
        {
            OpenMenu(SystemMenus.Ticket, "QUẢN LÝ GIÁ VÉ", () => new UcManageTicket());
        }

        private void btnKhuyenMai_Click(object sender, EventArgs e)
        {
            OpenMenu(SystemMenus.Promotion, "QUẢN LÝ KHUYẾN MÃI", () => new UcManagePromotion());
        }

        private void btnPhanQuyen_Click(object sender, EventArgs e)
        {
            OpenMenu(SystemMenus.Permission, "QUẢN LÝ PHÂN QUYỀN ĐỘNG", () => new UcPermission());
        }

        private void btnNhatKy_Click(object sender, EventArgs e)
        {
            OpenMenu(SystemMenus.AuditLog, "NHẬT KÝ HOẠT ĐỘNG (AUDIT TRAIL)", () => new UcAuditLog());
        }

        // ==========================================
        // CẢNH BÁO AN NINH (Từ nhánh feature)
        // ==========================================
        private void SetupCanhBao()
        {
            // Style for warning panel
            picCanhBao.Image = SystemIcons.Warning.ToBitmap();

            lblTieuDe.Text = "TRUY CẬP BỊ TỪ CHỐI";
            lblTieuDe.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTieuDe.ForeColor = ThemeConfig.DangerColor;

            lblNoiDung.Font = new Font("Segoe UI", 9.5F);
            lblNoiDung.ForeColor = Color.FromArgb(71, 85, 105);

            btnDong.Text = " Đóng";
            btnDong.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDong.ForeColor = Color.White;
            btnDong.BackColor = ThemeConfig.PrimaryColor;
            btnDong.FlatStyle = FlatStyle.Flat;
            btnDong.FlatAppearance.BorderSize = 0;

            // Center pnlCanhBao whenever pnlContent resizes
            pnlContent.Resize += (s, e) =>
            {
                pnlCanhBao.Location = new Point(
                    (pnlContent.Width - pnlCanhBao.Width) / 2,
                    (pnlContent.Height - pnlCanhBao.Height) / 2
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

        // ==========================================
        // DỌN DẸP BỘ NHỚ (Từ nhánh main)
        // ==========================================
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            CultureHelper.CultureChanged -= OnCultureChanged;
            base.OnFormClosed(e);
        }
    }
}
