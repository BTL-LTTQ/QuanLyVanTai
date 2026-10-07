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
        private readonly ComboBox cboCurrentRole = new();
        private readonly Label lblRoleBadge = new();
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

            SetupCanhBao();
            SetupRoleSwitcher();
            ApplyTheme();
// --- GỘP CHUNG VÀO TRONG CONSTRUCTOR ---
            UserSession.SessionChanged += OnSessionChanged;
            btnDong.Click += btnDong_Click;

            // Đăng ký nghe vi phạm an ninh từ AuthorizationGuard để ghi Audit Log
            AuthorizationGuard.UnauthorizedAttemptDetected += (menu, action, msg) =>
            {
                _ = _auditLogService.LogActionAsync("Truy cập trái phép", menu, null, msg);
            };

            this.Load += FormMain_Load;

            // Setup của nhánh main
            SetupHeaderAuthControls();
            CultureHelper.CultureChanged += OnCultureChanged;
            UpdateLocalizedTexts();
        } // Đóng constructor (Đã xóa các dấu ngoặc dư thừa)

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

            // Mở mặc định Form Quản lý Tuyến xe
            OpenMenu(SystemMenus.Route, "QUẢN LÝ TUYẾN XE & LỘ TRÌNH", () => new UcManageRoute());
        }

        private void SetupRoleSwitcher()
        {
            // Thêm thanh chọn Role nhanh trên Header để test Phân Quyền Động & Chặn Mở Form
            lblRoleBadge.Text = "👤 Đang đăng nhập:";
            lblRoleBadge.ForeColor = Color.White;
            lblRoleBadge.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblRoleBadge.AutoSize = true;
            lblRoleBadge.Location = new Point(pnlHeader.Width - 370, 26);
            lblRoleBadge.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            cboCurrentRole.Items.AddRange([
                $"{SystemRoles.Admin} (Toàn quyền)",
                $"{SystemRoles.Manager} (Quản lý)",
                $"{SystemRoles.TicketStaff} (Nhân viên bán vé)"
            ]);
            cboCurrentRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCurrentRole.SelectedIndex = 0;
            cboCurrentRole.Size = new Size(200, 30);
            cboCurrentRole.Location = new Point(pnlHeader.Width - 225, 22);
            cboCurrentRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cboCurrentRole.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);

            cboCurrentRole.SelectedIndexChanged += async (s, e) =>
            {
                string selectedRole = cboCurrentRole.SelectedIndex switch
                {
                    0 => SystemRoles.Admin,
                    1 => SystemRoles.Manager,
                    _ => SystemRoles.TicketStaff
                };

                string username = selectedRole switch
                {
                    SystemRoles.Admin => "admin",
                    SystemRoles.Manager => "quanly",
                    _ => "nhanvien"
                };

                UserSession.Login(
                    userId: cboCurrentRole.SelectedIndex + 1,
                    username: username,
                    fullName: selectedRole,
                    role: selectedRole
                );

                await _permissionService.LoadPermissionsToUserSessionAsync(selectedRole);

                await _auditLogService.LogActionAsync(
                    "Đăng nhập",
                    "Hệ thống",
                    null,
                    $"Người dùng [{username}] chuyển đổi vai trò phiên làm việc sang [{selectedRole}]."
                );
            };

            pnlHeader.Controls.Add(lblRoleBadge);
            pnlHeader.Controls.Add(cboCurrentRole);
        }

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
                Size = new Size(280, 30),
                ForeColor = Color.White,
                Font = ThemeConfig.MainFont,
                TextAlign = ContentAlignment.MiddleRight,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            var user = SessionManager.CurrentUser;
            if (user != null)
            {
                lblUserGreeting.Text = $"👤 {user.FullName} ({user.Role})";
            }
            else
            {
                lblUserGreeting.Text = "👤 Khách (Guest)";
            }

            // 2. Chuyển đổi ngôn ngữ ở header
            cboMainLanguage = new ComboBox
            {
                Size = new Size(130, 30),
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
            {
                cboMainLanguage.SelectedValue = match.Code;
            }

            cboMainLanguage.SelectedIndexChanged += (s, e) => {
                if (_isChangingLanguage || cboMainLanguage.SelectedValue == null) return;
                string selectedLang = cboMainLanguage.SelectedValue.ToString()!;
                CultureHelper.SetCulture(selectedLang, savePreference: true);
            };

            // 3. Nút Đăng xuất
            btnLogout = new IconButton
            {
                Size = new Size(120, 36),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            ThemeConfig.StyleDangerButton(btnLogout, IconChar.RightFromBracket);
            btnLogout.Text = " Đăng xuất";
            btnLogout.Click += BtnLogout_Click;

            // Định vị các control bên phải header
            int top = 18;
            int right = pnlHeader.Width - 140;

            btnLogout.Location = new Point(right, top);
            cboMainLanguage.Location = new Point(right - 145, top + 3);
            lblUserGreeting.Location = new Point(right - 145 - 290, top + 3);

            pnlHeader.Controls.Add(lblUserGreeting);
            pnlHeader.Controls.Add(cboMainLanguage);
            pnlHeader.Controls.Add(btnLogout);

            pnlHeader.Resize += (s, e) => {
                int r = pnlHeader.Width - 140;
                btnLogout.Location = new Point(r, top);
                cboMainLanguage.Location = new Point(r - 145, top + 3);
                lblUserGreeting.Location = new Point(r - 145 - 290, top + 3);
            };
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
            pnlSidebar.BackColor = ThemeConfig.TextMain;
            pnlHeader.BackColor = ThemeConfig.PrimaryColor;
            lblTitle.Font = ThemeConfig.TitleFont;
            pnlContent.BackColor = ThemeConfig.BackgroundColor;

            void StyleSidebarButton(FontAwesome.Sharp.IconButton btn, FontAwesome.Sharp.IconChar icon)
            {
                btn.BackColor = ThemeConfig.TextMain;
                btn.ForeColor = Color.White;
                btn.IconChar = icon;
                btn.IconColor = Color.White;
                btn.IconSize = 28;
                btn.Font = ThemeConfig.MainFont;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Cursor = Cursors.Hand;
                btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                btn.ImageAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(12, 0, 0, 0);
btn.Height = 52;
            }

            // Cấu hình Icon cho menu (Text ngôn ngữ được xử lý ở UpdateLocalizedTexts)
            StyleSidebarButton(btnTrangChu, FontAwesome.Sharp.IconChar.Route);
            StyleSidebarButton(btnDichVu, FontAwesome.Sharp.IconChar.Bus);
            StyleSidebarButton(btnNhanSu, FontAwesome.Sharp.IconChar.Users);
            StyleSidebarButton(btnGiaVe, FontAwesome.Sharp.IconChar.Tag);
            StyleSidebarButton(btnKhuyenMai, FontAwesome.Sharp.IconChar.Percent);
            StyleSidebarButton(btnPhanQuyen, FontAwesome.Sharp.IconChar.ShieldHalved);
            StyleSidebarButton(btnNhatKy, FontAwesome.Sharp.IconChar.ClockRotateLeft);
            StyleSidebarButton(btnCaiDat, FontAwesome.Sharp.IconChar.Gear);
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
            string title = CultureHelper.CurrentCulture.Name.StartsWith("en") ? "HOME / ROUTE MANAGEMENT" :
                           CultureHelper.CurrentCulture.Name.StartsWith("ja") ? "ホーム / 路線管理" : "QUẢN LÝ TUYẾN XE & LỘ TRÌNH";
            OpenMenu(SystemMenus.Route, title, () => new UcManageRoute());
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
            pnlCanhBao.Visible = false;
            pnlCanhBao.BackColor = Color.White;
            pnlCanhBao.BorderStyle = BorderStyle.FixedSingle;
            pnlCanhBao.Size = new Size(500, 300);
            pnlCanhBao.Location = new Point(
                (this.ClientSize.Width - pnlCanhBao.Width) / 2,
                (this.ClientSize.Height - pnlCanhBao.Height) / 2
            );
            pnlCanhBao.Anchor = AnchorStyles.None;

            picCanhBao.Image = SystemIcons.Warning.ToBitmap();
            picCanhBao.SizeMode = PictureBoxSizeMode.CenterImage;
            picCanhBao.Size = new Size(70, 70);
            picCanhBao.Location = new Point(215, 25);

            lblTieuDe.Text = "TRUY CẬP BỊ TỪ CHỐI";
            lblTieuDe.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTieuDe.ForeColor = ThemeConfig.DangerColor;
            lblTieuDe.AutoSize = false;
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            lblTieuDe.Size = new Size(460, 32);
            lblTieuDe.Location = new Point(20, 100);

            lblNoiDung.Font = new Font("Segoe UI", 9.5F);
            lblNoiDung.ForeColor = Color.FromArgb(71, 85, 105);
            lblNoiDung.AutoSize = false;
            lblNoiDung.TextAlign = ContentAlignment.MiddleCenter;
            lblNoiDung.Size = new Size(460, 75);
            lblNoiDung.Location = new Point(20, 138);

            btnDong.Text = " Đóng thông báo";
            btnDong.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDong.ForeColor = Color.White;
            btnDong.BackColor = ThemeConfig.PrimaryColor;
            btnDong.FlatStyle = FlatStyle.Flat;
            btnDong.FlatAppearance.BorderSize = 0;
            btnDong.Size = new Size(150, 40);
            btnDong.Location = new Point(175, 230);

            pnlCanhBao.BringToFront();
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
