using Core.Configs;
using Core.Helpers;
using FontAwesome.Sharp;
using QuanLyVanTai.UI.UserControls;

namespace QuanLyVanTai.UI
{
    public partial class FormMain : Form
    {
        private Label lblUserGreeting = null!;
        private ComboBox cboMainLanguage = null!;
        private IconButton btnLogout = null!;
        private bool _isChangingLanguage = false;

        public FormMain()
        {
            InitializeComponent();
            this.Font = ThemeConfig.MainFont;
            ApplyTheme();
            SetupHeaderAuthControls();
            CultureHelper.CultureChanged += OnCultureChanged;
            UpdateLocalizedTexts();

            // Hiển thị màn hình mặc định
            btnTrangChu_Click(this, EventArgs.Empty);
        }

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
            // Panel styling
            pnlSidebar.BackColor = ThemeConfig.TextMain;
            pnlHeader.BackColor = ThemeConfig.PrimaryColor; // Use PrimaryColor for Header
            lblTitle.Font = ThemeConfig.TitleFont;
            pnlContent.BackColor = ThemeConfig.BackgroundColor;
            
            // Sidebar Button Styling Helper
            void StyleSidebarButton(IconButton btn, IconChar icon)
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
                btn.Height = 50;
            }

            StyleSidebarButton(btnTrangChu, IconChar.Home);
            StyleSidebarButton(btnDichVu, IconChar.Truck);
        }

        // Hàm xóa nội dung cũ và nạp UserControl mới vào vùng giữa
        private void ShowUserControl(UserControl uc)
        {
            try
            {
                pnlContent.Controls.Clear();
                uc.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(uc);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị nội dung: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTrangChu_Click(object? sender, EventArgs e)
        {
            UcManageRoute ucRoute = new UcManageRoute();
            ShowUserControl(ucRoute);
            lblTitle.Text = CultureHelper.CurrentCulture.Name.StartsWith("en") ? "HOME / ROUTE MANAGEMENT" :
                            CultureHelper.CurrentCulture.Name.StartsWith("ja") ? "ホーム / 路線管理" : "TRANG CHỦ - QUẢN LÝ TUYẾN";
        }
        
        private void btnDichVu_Click(object? sender, EventArgs e)
        {
            UcManageService ucService = new UcManageService();
            ShowUserControl(ucService);
            lblTitle.Text = CultureHelper.CurrentCulture.Name.StartsWith("en") ? "SERVICES MANAGEMENT" :
                            CultureHelper.CurrentCulture.Name.StartsWith("ja") ? "サービス管理" : "QUẢN LÝ DỊCH VỤ VẬN TẢI";
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            CultureHelper.CultureChanged -= OnCultureChanged;
            base.OnFormClosed(e);
        }
    }
}
