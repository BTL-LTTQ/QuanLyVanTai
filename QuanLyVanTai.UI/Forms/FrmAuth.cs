using Core.Configs;
using Core.Helpers;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;
using System.Text.RegularExpressions;

namespace QuanLyVanTai.UI.Forms
{
    public partial class FrmAuth : Form
    {
        private readonly IAuthService _authService;
        private bool _isLoaded = false;

        public FrmAuth()
        {
            InitializeComponent();
            _authService = new AuthService();

            ApplyThemeStyles();
            SetupLanguageSelector();
            SetupTooltips();
            RegisterRealtimeValidation();
            LoadUserPreferences();

            CultureHelper.CultureChanged += OnCultureChanged;
            _isLoaded = true;

            ShowView(AuthView.Login);
            UpdateLocalizedTexts();
        }

        private enum AuthView
        {
            Login,
            Register,
            Forgot
        }

        private AuthView _currentView = AuthView.Login;

        // ==========================================
        // 1. ÁP DỤNG THEME & FONT CHUẨN HUẤN CẤU HÌNH
        // ==========================================
        private void ApplyThemeStyles()
        {
            Font = ThemeConfig.MainFont;
            BackColor = ThemeConfig.BackgroundColor;

            // Brand Panel bên trái
            pnlBrand.BackColor = ThemeConfig.PrimaryColor;
            lblBrandTitle.Font = ThemeConfig.TitleFont;
            lblBrandSubtitle.Font = ThemeConfig.MainFont;
            lblFeature1.Font = ThemeConfig.MainFont;
            lblFeature2.Font = ThemeConfig.MainFont;
            lblFeature3.Font = ThemeConfig.MainFont;
            lblLanguageTitle.Font = ThemeConfig.ButtonFont;

            // Right container & tabs
            pnlRight.BackColor = ThemeConfig.BackgroundColor;
            pnlContentContainer.BackColor = ThemeConfig.PanelBackgroundColor;
            pnlViewLogin.BackColor = ThemeConfig.PanelBackgroundColor;
            pnlViewRegister.BackColor = ThemeConfig.PanelBackgroundColor;
            pnlViewForgot.BackColor = ThemeConfig.PanelBackgroundColor;

            // Headings
            lblLoginHeading.Font = ThemeConfig.TitleFont;
            lblLoginHeading.ForeColor = ThemeConfig.TextMain;
            lblLoginSubheading.Font = ThemeConfig.MainFont;
            lblLoginSubheading.ForeColor = ThemeConfig.SecondaryColor;

            lblRegHeading.Font = ThemeConfig.TitleFont;
            lblRegHeading.ForeColor = ThemeConfig.TextMain;
            lblRegSubheading.Font = ThemeConfig.MainFont;
            lblRegSubheading.ForeColor = ThemeConfig.SecondaryColor;

            lblForgotHeading.Font = ThemeConfig.TitleFont;
            lblForgotHeading.ForeColor = ThemeConfig.TextMain;
            lblForgotSubheading.Font = ThemeConfig.MainFont;
            lblForgotSubheading.ForeColor = ThemeConfig.SecondaryColor;

            // Buttons styling theo ThemeConfig
            ThemeConfig.StylePrimaryButton(btnSubmitLogin, FontAwesome.Sharp.IconChar.RightToBracket);
            btnSubmitLogin.Size = new Size(540, 48);

            ThemeConfig.StyleSuccessButton(btnSubmitRegister, FontAwesome.Sharp.IconChar.UserPlus);
            btnSubmitRegister.Size = new Size(540, 46);

            ThemeConfig.StylePrimaryButton(btnSubmitResetPassword, FontAwesome.Sharp.IconChar.Key);
            btnSubmitResetPassword.Size = new Size(540, 44);

            ThemeConfig.StyleSecondaryButton(btnSendOtp, FontAwesome.Sharp.IconChar.PaperPlane);
            btnSendOtp.Size = new Size(180, 40);

            ThemeConfig.StyleSecondaryButton(btnCheckQuestion, FontAwesome.Sharp.IconChar.Question);
            btnCheckQuestion.Size = new Size(180, 36);

            // Wire Tab clicks
            btnNavLogin.Click += (s, e) => ShowView(AuthView.Login);
            btnNavRegister.Click += (s, e) => ShowView(AuthView.Register);
            btnNavForgot.Click += (s, e) => ShowView(AuthView.Forgot);

            lnkGoToRegister.Click += (s, e) => ShowView(AuthView.Register);
            lnkGoToLogin.Click += (s, e) => ShowView(AuthView.Login);
            lnkForgotPassword.Click += (s, e) => ShowView(AuthView.Forgot);
            lnkBackToLogin.Click += (s, e) => ShowView(AuthView.Login);

            // Eye password toggles
            btnToggleLoginPass.Click += (s, e) => {
                txtLoginPassword.UseSystemPasswordChar = !txtLoginPassword.UseSystemPasswordChar;
                btnToggleLoginPass.Text = txtLoginPassword.UseSystemPasswordChar ? "👁" : "🔒";
            };

            btnToggleRegPass.Click += (s, e) => {
                txtRegPassword.UseSystemPasswordChar = !txtRegPassword.UseSystemPasswordChar;
                btnToggleRegPass.Text = txtRegPassword.UseSystemPasswordChar ? "👁" : "🔒";
            };

            // Radio recovery method changed
            radMethodEmail.CheckedChanged += (s, e) => {
                pnlMethodEmail.Visible = radMethodEmail.Checked;
                pnlMethodQuestion.Visible = !radMethodEmail.Checked;
            };

            // Submit Buttons wire
            btnSubmitLogin.Click += BtnSubmitLogin_Click;
            btnSubmitRegister.Click += BtnSubmitRegister_Click;
            btnSendOtp.Click += BtnSendOtp_Click;
            btnCheckQuestion.Click += BtnCheckQuestion_Click;
            btnSubmitResetPassword.Click += BtnSubmitResetPassword_Click;
        }

        // ==========================================
        // 2. CHUYỂN ĐỔI GIAO DIỆN (LOGIN / REGISTER / FORGOT)
        // ==========================================
        private void ShowView(AuthView view)
        {
            _currentView = view;

            pnlViewLogin.Visible = (view == AuthView.Login);
            pnlViewRegister.Visible = (view == AuthView.Register);
            pnlViewForgot.Visible = (view == AuthView.Forgot);

            // Cập nhật tab active
            void StyleTabButton(Button btn, bool isActive)
            {
                if (isActive)
                {
                    btn.BackColor = ThemeConfig.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.Font = ThemeConfig.ButtonFont;
                }
                else
                {
                    btn.BackColor = Color.FromArgb(241, 245, 249);
                    btn.ForeColor = ThemeConfig.SecondaryColor;
                    btn.Font = ThemeConfig.MainFont;
                }
            }

            StyleTabButton(btnNavLogin, view == AuthView.Login);
            StyleTabButton(btnNavRegister, view == AuthView.Register);
            StyleTabButton(btnNavForgot, view == AuthView.Forgot);

            // Xóa lỗi đang hiển thị khi chuyển tab
            epErrors.Clear();
        }

        // ==========================================
        // 3. THIẾT LẬP ĐA NGÔN NGỮ & USER PREFERENCE (PART 3)
        // ==========================================
        private void SetupLanguageSelector()
        {
            cboLanguage.DisplayMember = "DisplayName";
            cboLanguage.ValueMember = "Code";
            cboLanguage.DataSource = CultureHelper.SupportedCultures
                .Select(c => new { Code = c.Code, DisplayName = $"{c.Flag} {c.DisplayName}" })
                .ToList();

            string current = CultureHelper.CurrentCulture.Name;
            var match = CultureHelper.SupportedCultures.FirstOrDefault(c => c.Code.Equals(current, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(match.Code))
            {
                cboLanguage.SelectedValue = match.Code;
            }

            cboLanguage.SelectedIndexChanged += (s, e) => {
                if (!_isLoaded || cboLanguage.SelectedValue == null) return;
                string selectedLang = cboLanguage.SelectedValue.ToString()!;
                CultureHelper.SetCulture(selectedLang, savePreference: true);
            };
        }

        private void OnCultureChanged()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(OnCultureChanged));
                return;
            }
            UpdateLocalizedTexts();
        }

        private void UpdateLocalizedTexts()
        {
            try
            {
                // Tiêu đề & Brand Panel
                lblBrandTitle.Text = CultureHelper.GetString("AppTitle");
                lblBrandSubtitle.Text = CultureHelper.GetString("AppSubtitle");
                lblLanguageTitle.Text = CultureHelper.GetString("Language") + ":";

                // Tabs
                btnNavLogin.Text = CultureHelper.GetString("TabLogin");
                btnNavRegister.Text = CultureHelper.GetString("TabRegister");
                btnNavForgot.Text = CultureHelper.GetString("TabForgotPassword");

                // Login
                lblLoginHeading.Text = CultureHelper.GetString("TabLogin").ToUpper();
                lblLoginUserTitle.Text = CultureHelper.GetString("UsernameOrEmail") + " *:";
                lblLoginPassTitle.Text = CultureHelper.GetString("Password") + " *:";
                chkRememberMe.Text = CultureHelper.GetString("RememberMe");
                lnkForgotPassword.Text = CultureHelper.GetString("ForgotPasswordLink");
                btnSubmitLogin.Text = " " + CultureHelper.GetString("BtnLogin");
                lblNoAccountPrompt.Text = CultureHelper.GetString("NoAccountPrompt");
                lnkGoToRegister.Text = CultureHelper.GetString("RegisterNow");

                // Register
                lblRegHeading.Text = CultureHelper.GetString("BtnRegister");
                lblRegFullName.Text = CultureHelper.GetString("FullName") + " *:";
                lblRegUsername.Text = CultureHelper.GetString("Username") + " *:";
                lblRegEmail.Text = CultureHelper.GetString("Email") + " *:";
                lblRegPhone.Text = CultureHelper.GetString("PhoneNumber") + " *:";
                lblRegPassword.Text = CultureHelper.GetString("Password") + " *:";
                lblRegConfirmPassword.Text = CultureHelper.GetString("ConfirmPassword") + " *:";
                lblRegSecurityQuestion.Text = CultureHelper.GetString("SecurityQuestion") + " *:";
                lblRegSecurityAnswer.Text = CultureHelper.GetString("SecurityAnswer") + " *:";
                btnSubmitRegister.Text = " " + CultureHelper.GetString("BtnRegister");
                lblHaveAccountPrompt.Text = CultureHelper.GetString("HaveAccountPrompt");
                lnkGoToLogin.Text = CultureHelper.GetString("LoginNow");

                // Load Security Questions combobox
                int currentQIdx = cboRegSecurityQuestion.SelectedIndex;
                cboRegSecurityQuestion.Items.Clear();
                cboRegSecurityQuestion.Items.AddRange(new object[] {
                    CultureHelper.GetString("Question1"),
                    CultureHelper.GetString("Question2"),
                    CultureHelper.GetString("Question3"),
                    CultureHelper.GetString("Question4")
                });
                if (currentQIdx >= 0 && currentQIdx < cboRegSecurityQuestion.Items.Count)
                    cboRegSecurityQuestion.SelectedIndex = currentQIdx;
                else
                    cboRegSecurityQuestion.SelectedIndex = 0;

                // Forgot
                lblForgotHeading.Text = CultureHelper.GetString("TabForgotPassword").ToUpper();
                lblForgotUserOrEmail.Text = CultureHelper.GetString("UsernameOrEmail") + " *:";
                lblRecoveryMethodTitle.Text = CultureHelper.GetString("RecoveryMethod");
                radMethodEmail.Text = CultureHelper.GetString("MethodEmail");
                radMethodQuestion.Text = CultureHelper.GetString("MethodQuestion");
                btnSendOtp.Text = " " + CultureHelper.GetString("SendOtp");
                lblOtpPrompt.Text = CultureHelper.GetString("OtpCode") + " *:";
                btnCheckQuestion.Text = " " + CultureHelper.GetString("CheckQuestion");
                lblForgotNewPass.Text = CultureHelper.GetString("NewPassword") + " *:";
                lblForgotConfirmPass.Text = CultureHelper.GetString("ConfirmNewPassword") + " *:";
                btnSubmitResetPassword.Text = " " + CultureHelper.GetString("BtnResetPassword");
                lnkBackToLogin.Text = "← " + CultureHelper.GetString("BackToLogin");

                // Locale Card Demo (Part 3)
                lblLocaleCurrent.Text = $"Locale: {CultureHelper.CurrentCulture.Name} ({CultureHelper.CurrentCulture.DisplayName})";
                lblLocaleDate.Text = $"Thời gian: {CultureHelper.FormatDate(DateTime.Now, true)}";
                lblLocalePrice.Text = $"Giá vé mẫu: {CultureHelper.FormatCurrency(250000m)}";
            }
            catch (Exception ex)
            {
                // Tránh crash khi cập nhật ngôn ngữ
                System.Diagnostics.Debug.WriteLine($"Lỗi UpdateLocalizedTexts: {ex.Message}");
            }
        }

        // ==========================================
        // 4. TOOLTIP HƯỚNG DẪN CÁCH NHẬP ĐÚNG
        // ==========================================
        private void SetupTooltips()
        {
            toolTipHelp.ToolTipTitle = "Hướng dẫn nhập";
            toolTipHelp.IsBalloon = true;
            toolTipHelp.ToolTipIcon = ToolTipIcon.Info;

            toolTipHelp.SetToolTip(txtLoginUsername, CultureHelper.GetString("TipUsername"));
            toolTipHelp.SetToolTip(txtLoginPassword, CultureHelper.GetString("TipPassword"));
            toolTipHelp.SetToolTip(txtRegFullName, CultureHelper.GetString("TipFullName"));
            toolTipHelp.SetToolTip(txtRegUsername, CultureHelper.GetString("TipUsername"));
            toolTipHelp.SetToolTip(txtRegEmail, CultureHelper.GetString("TipEmail"));
            toolTipHelp.SetToolTip(txtRegPhone, CultureHelper.GetString("TipPhone"));
            toolTipHelp.SetToolTip(txtRegPassword, CultureHelper.GetString("TipPassword"));
            toolTipHelp.SetToolTip(txtRegConfirmPassword, CultureHelper.GetString("TipConfirmPassword"));
            toolTipHelp.SetToolTip(txtRegSecurityAnswer, CultureHelper.GetString("TipSecurityAnswer"));
            toolTipHelp.SetToolTip(txtForgotOtp, CultureHelper.GetString("TipOtp"));
        }

        // ==========================================
        // 5. VALIDATE REAL-TIME VỚI ERRORPROVIDER (PART 2)
        // ==========================================
        private void RegisterRealtimeValidation()
        {
            // --- LOGIN VALIDATION ---
            txtLoginUsername.TextChanged += (s, e) => ValidateLoginUsername(false);
            txtLoginPassword.TextChanged += (s, e) => ValidateLoginPassword(false);

            // --- REGISTER VALIDATION ---
            txtRegFullName.TextChanged += (s, e) => ValidateRegFullName(false);
            txtRegUsername.TextChanged += (s, e) => ValidateRegUsername(false);
            txtRegEmail.TextChanged += (s, e) => ValidateRegEmail(false);
            txtRegPhone.TextChanged += (s, e) => ValidateRegPhone(false);
            txtRegPassword.TextChanged += (s, e) => {
                ValidateRegPassword(false);
                if (!string.IsNullOrEmpty(txtRegConfirmPassword.Text))
                    ValidateRegConfirmPassword(false);
            };
            txtRegConfirmPassword.TextChanged += (s, e) => ValidateRegConfirmPassword(false);
            cboRegSecurityQuestion.SelectedIndexChanged += (s, e) => ValidateRegSecurityQuestion(false);
            txtRegSecurityAnswer.TextChanged += (s, e) => ValidateRegSecurityAnswer(false);

            // --- FORGOT PASSWORD VALIDATION ---
            txtForgotUserOrEmail.TextChanged += (s, e) => ValidateForgotUserOrEmail(false);
            txtForgotOtp.TextChanged += (s, e) => ValidateForgotOtp(false);
            txtForgotSecurityAnswer.TextChanged += (s, e) => ValidateForgotSecurityAnswer(false);
            txtForgotNewPass.TextChanged += (s, e) => {
                ValidateForgotNewPass(false);
                if (!string.IsNullOrEmpty(txtForgotConfirmPass.Text))
                    ValidateForgotConfirmPass(false);
            };
            txtForgotConfirmPass.TextChanged += (s, e) => ValidateForgotConfirmPass(false);
        }

        // ==================== CÁC HÀM VALIDATE CHI TIẾT ====================
        private bool ValidateLoginUsername(bool isSubmitting)
        {
            string val = txtLoginUsername.Text.Trim();
            if (string.IsNullOrEmpty(val))
            {
                if (isSubmitting)
                    epErrors.SetError(txtLoginUsername, CultureHelper.GetString("ValRequired"));
                return false;
            }
            if (val.Length < 3)
            {
                epErrors.SetError(txtLoginUsername, CultureHelper.GetString("ValUsernameLength"));
                return false;
            }
            epErrors.SetError(txtLoginUsername, string.Empty);
            return true;
        }

        private bool ValidateLoginPassword(bool isSubmitting)
        {
            string val = txtLoginPassword.Text;
            if (string.IsNullOrEmpty(val))
            {
                if (isSubmitting)
                    epErrors.SetError(txtLoginPassword, CultureHelper.GetString("ValRequired"));
                return false;
            }
            if (val.Length < 6)
            {
                epErrors.SetError(txtLoginPassword, CultureHelper.GetString("ValPasswordLength"));
                return false;
            }
            epErrors.SetError(txtLoginPassword, string.Empty);
            return true;
        }

        private bool ValidateRegFullName(bool isSubmitting)
        {
            string val = txtRegFullName.Text.Trim();
            if (string.IsNullOrEmpty(val))
            {
                if (isSubmitting)
                    epErrors.SetError(txtRegFullName, CultureHelper.GetString("ValRequired"));
                return false;
            }
            if (val.Length < 2)
            {
                epErrors.SetError(txtRegFullName, "Họ và tên tối thiểu 2 ký tự.");
                return false;
            }
            epErrors.SetError(txtRegFullName, string.Empty);
            return true;
        }

        private bool ValidateRegUsername(bool isSubmitting)
        {
            string val = txtRegUsername.Text.Trim();
            if (string.IsNullOrEmpty(val))
            {
                if (isSubmitting)
                    epErrors.SetError(txtRegUsername, CultureHelper.GetString("ValRequired"));
                return false;
            }
            if (val.Length < 4 || val.Length > 50)
            {
                epErrors.SetError(txtRegUsername, CultureHelper.GetString("ValUsernameLength"));
                return false;
            }
            if (!Regex.IsMatch(val, @"^[a-zA-Z0-9_]+$"))
            {
                epErrors.SetError(txtRegUsername, CultureHelper.GetString("ValUsernameFormat"));
                return false;
            }
            epErrors.SetError(txtRegUsername, string.Empty);
            return true;
        }

        private bool ValidateRegEmail(bool isSubmitting)
        {
            string val = txtRegEmail.Text.Trim();
            if (string.IsNullOrEmpty(val))
            {
                if (isSubmitting)
                    epErrors.SetError(txtRegEmail, CultureHelper.GetString("ValRequired"));
                return false;
            }
            if (!Regex.IsMatch(val, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                epErrors.SetError(txtRegEmail, CultureHelper.GetString("ValEmailFormat"));
                return false;
            }
            epErrors.SetError(txtRegEmail, string.Empty);
            return true;
        }

        private bool ValidateRegPhone(bool isSubmitting)
        {
            string val = txtRegPhone.Text.Trim();
            if (string.IsNullOrEmpty(val))
            {
                if (isSubmitting)
                    epErrors.SetError(txtRegPhone, CultureHelper.GetString("ValRequired"));
                return false;
            }
            if (!Regex.IsMatch(val, @"^0\d{9}$"))
            {
                epErrors.SetError(txtRegPhone, CultureHelper.GetString("ValPhoneFormat"));
                return false;
            }
            epErrors.SetError(txtRegPhone, string.Empty);
            return true;
        }

        private bool ValidateRegPassword(bool isSubmitting)
        {
            string val = txtRegPassword.Text;
            if (string.IsNullOrEmpty(val))
            {
                if (isSubmitting)
                    epErrors.SetError(txtRegPassword, CultureHelper.GetString("ValRequired"));
                return false;
            }
            if (val.Length < 6)
            {
                epErrors.SetError(txtRegPassword, CultureHelper.GetString("ValPasswordLength"));
                return false;
            }
            epErrors.SetError(txtRegPassword, string.Empty);
            return true;
        }

        private bool ValidateRegConfirmPassword(bool isSubmitting)
        {
            string val = txtRegConfirmPassword.Text;
            if (string.IsNullOrEmpty(val))
            {
                if (isSubmitting)
                    epErrors.SetError(txtRegConfirmPassword, CultureHelper.GetString("ValRequired"));
                return false;
            }
            if (val != txtRegPassword.Text)
            {
                epErrors.SetError(txtRegConfirmPassword, CultureHelper.GetString("ValPasswordMatch"));
                return false;
            }
            epErrors.SetError(txtRegConfirmPassword, string.Empty);
            return true;
        }

        private bool ValidateRegSecurityQuestion(bool isSubmitting)
        {
            if (cboRegSecurityQuestion.SelectedIndex < 0)
            {
                if (isSubmitting)
                    epErrors.SetError(cboRegSecurityQuestion, CultureHelper.GetString("ValSecurityQuestion"));
                return false;
            }
            epErrors.SetError(cboRegSecurityQuestion, string.Empty);
            return true;
        }

        private bool ValidateRegSecurityAnswer(bool isSubmitting)
        {
            string val = txtRegSecurityAnswer.Text.Trim();
            if (string.IsNullOrEmpty(val))
            {
                if (isSubmitting)
                    epErrors.SetError(txtRegSecurityAnswer, CultureHelper.GetString("ValSecurityAnswer"));
                return false;
            }
            epErrors.SetError(txtRegSecurityAnswer, string.Empty);
            return true;
        }

        private bool ValidateForgotUserOrEmail(bool isSubmitting)
        {
            string val = txtForgotUserOrEmail.Text.Trim();
            if (string.IsNullOrEmpty(val))
            {
                if (isSubmitting)
                    epErrors.SetError(txtForgotUserOrEmail, CultureHelper.GetString("ValRequired"));
                return false;
            }
            epErrors.SetError(txtForgotUserOrEmail, string.Empty);
            return true;
        }

        private bool ValidateForgotOtp(bool isSubmitting)
        {
            string val = txtForgotOtp.Text.Trim();
            if (string.IsNullOrEmpty(val))
            {
                if (isSubmitting)
                    epErrors.SetError(txtForgotOtp, CultureHelper.GetString("ValRequired"));
                return false;
            }
            if (!Regex.IsMatch(val, @"^\d{6}$"))
            {
                epErrors.SetError(txtForgotOtp, CultureHelper.GetString("ValOtpFormat"));
                return false;
            }
            epErrors.SetError(txtForgotOtp, string.Empty);
            return true;
        }

        private bool ValidateForgotSecurityAnswer(bool isSubmitting)
        {
            string val = txtForgotSecurityAnswer.Text.Trim();
            if (string.IsNullOrEmpty(val))
            {
                if (isSubmitting)
                    epErrors.SetError(txtForgotSecurityAnswer, CultureHelper.GetString("ValSecurityAnswer"));
                return false;
            }
            epErrors.SetError(txtForgotSecurityAnswer, string.Empty);
            return true;
        }

        private bool ValidateForgotNewPass(bool isSubmitting)
        {
            string val = txtForgotNewPass.Text;
            if (string.IsNullOrEmpty(val))
            {
                if (isSubmitting)
                    epErrors.SetError(txtForgotNewPass, CultureHelper.GetString("ValRequired"));
                return false;
            }
            if (val.Length < 6)
            {
                epErrors.SetError(txtForgotNewPass, CultureHelper.GetString("ValPasswordLength"));
                return false;
            }
            epErrors.SetError(txtForgotNewPass, string.Empty);
            return true;
        }

        private bool ValidateForgotConfirmPass(bool isSubmitting)
        {
            string val = txtForgotConfirmPass.Text;
            if (string.IsNullOrEmpty(val))
            {
                if (isSubmitting)
                    epErrors.SetError(txtForgotConfirmPass, CultureHelper.GetString("ValRequired"));
                return false;
            }
            if (val != txtForgotNewPass.Text)
            {
                epErrors.SetError(txtForgotConfirmPass, CultureHelper.GetString("ValPasswordMatch"));
                return false;
            }
            epErrors.SetError(txtForgotConfirmPass, string.Empty);
            return true;
        }

        // ==========================================
        // 6. XỬ LÝ NGHIỆP VỤ BỌC TRY-CATCH CHẶT CHẼ
        // ==========================================

        private void LoadUserPreferences()
        {
            try
            {
                var prefs = SessionManager.LoadPreferences();
                if (prefs.RememberMe && !string.IsNullOrEmpty(prefs.SavedUsername))
                {
                    txtLoginUsername.Text = prefs.SavedUsername;
                    chkRememberMe.Checked = true;
                    txtLoginPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi nạp preferences: {ex.Message}");
            }
        }

        private async void BtnSubmitLogin_Click(object? sender, EventArgs e)
        {
            try
            {
                bool uValid = ValidateLoginUsername(true);
                bool pValid = ValidateLoginPassword(true);

                if (!uValid || !pValid)
                {
                    MessageBox.Show(
                        "Vui lòng kiểm tra lại thông tin đăng nhập còn thiếu hoặc sai định dạng.",
                        "Thông Báo Nhập Liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                btnSubmitLogin.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var req = new LoginRequestDto
                {
                    Username = txtLoginUsername.Text.Trim(),
                    Password = txtLoginPassword.Text,
                    RememberMe = chkRememberMe.Checked
                };

                var result = await _authService.LoginAsync(req);

                if (result.Success)
                {
                    lblLoginLockoutStatus.Visible = false;
                    MessageBox.Show(
                        $"{result.Message}\nXin chào: {result.Account?.FullName} ({result.Account?.Role})",
                        "Đăng Nhập Thành Công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    if (result.IsLocked)
                    {
                        lblLoginLockoutStatus.Text = $"⚠ TÀI KHOẢN BỊ KHÓA\n{result.Message}";
                        lblLoginLockoutStatus.ForeColor = ThemeConfig.DangerColor;
                        lblLoginLockoutStatus.Visible = true;
                    }
                    else if (result.RemainingAttempts > 0 && result.RemainingAttempts < 5)
                    {
                        lblLoginLockoutStatus.Text = $"⚠ Cảnh báo: Bạn còn {result.RemainingAttempts} lần thử trước khi bị khóa tài khoản.";
                        lblLoginLockoutStatus.ForeColor = ThemeConfig.WarningColor;
                        lblLoginLockoutStatus.Visible = true;
                    }

                    MessageBox.Show(
                        result.Message,
                        "Đăng Nhập Thất Bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Đã xảy ra sự cố không mong muốn trong quá trình xử lý đăng nhập:\n{ex.Message}",
                    "Lỗi Hệ Thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                btnSubmitLogin.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async void BtnSubmitRegister_Click(object? sender, EventArgs e)
        {
            try
            {
                bool vName = ValidateRegFullName(true);
                bool vUser = ValidateRegUsername(true);
                bool vEmail = ValidateRegEmail(true);
                bool vPhone = ValidateRegPhone(true);
                bool vPass = ValidateRegPassword(true);
                bool vConf = ValidateRegConfirmPassword(true);
                bool vQuest = ValidateRegSecurityQuestion(true);
                bool vAns = ValidateRegSecurityAnswer(true);

                if (!vName || !vUser || !vEmail || !vPhone || !vPass || !vConf || !vQuest || !vAns)
                {
                    MessageBox.Show(
                        "Vui lòng điền đúng và đủ tất cả các trường thông tin có dấu (*).",
                        "Thông Báo Nhập Liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                btnSubmitRegister.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var req = new RegisterRequestDto
                {
                    FullName = txtRegFullName.Text.Trim(),
                    Username = txtRegUsername.Text.Trim(),
                    Email = txtRegEmail.Text.Trim(),
                    PhoneNumber = txtRegPhone.Text.Trim(),
                    Password = txtRegPassword.Text,
                    ConfirmPassword = txtRegConfirmPassword.Text,
                    SecurityQuestion = cboRegSecurityQuestion.SelectedItem?.ToString(),
                    SecurityAnswer = txtRegSecurityAnswer.Text.Trim()
                };

                var result = await _authService.RegisterAsync(req);

                if (result.Success)
                {
                    MessageBox.Show(
                        result.Message,
                        "Đăng Ký Thành Công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Điền sẵn username sang form login và chuyển sang tab login
                    txtLoginUsername.Text = req.Username;
                    txtLoginPassword.Clear();
                    ShowView(AuthView.Login);
                    txtLoginPassword.Focus();
                }
                else
                {
                    MessageBox.Show(
                        result.Message,
                        "Đăng Ký Không Thành Công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi đăng ký tài khoản:\n{ex.Message}",
                    "Lỗi Hệ Thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                btnSubmitRegister.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async void BtnSendOtp_Click(object? sender, EventArgs e)
        {
            try
            {
                string target = txtForgotUserOrEmail.Text.Trim();
                if (string.IsNullOrEmpty(target))
                {
                    epErrors.SetError(txtForgotUserOrEmail, CultureHelper.GetString("ValRequired"));
                    MessageBox.Show("Vui lòng nhập Email hoặc Tên đăng nhập để nhận mã OTP.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnSendOtp.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var result = await _authService.RequestPasswordResetOtpAsync(target);

                if (result.Success)
                {
                    // Tự động điền mã OTP để hỗ trợ kiểm thử và thuận tiện cho người dùng
                    if (!string.IsNullOrEmpty(result.GeneratedOtp))
                    {
                        txtForgotOtp.Text = result.GeneratedOtp;
                    }

                    MessageBox.Show(
                        $"{result.Message}\n\n[MÃ OTP MÔ PHỎNG KIỂM THỬ: {result.GeneratedOtp}]",
                        "Gửi Mã OTP Thành Công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    MessageBox.Show(result.Message, "Không Thể Gửi OTP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi gửi mã OTP: {ex.Message}", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSendOtp.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async void BtnCheckQuestion_Click(object? sender, EventArgs e)
        {
            try
            {
                string target = txtForgotUserOrEmail.Text.Trim();
                if (string.IsNullOrEmpty(target))
                {
                    epErrors.SetError(txtForgotUserOrEmail, CultureHelper.GetString("ValRequired"));
                    MessageBox.Show("Vui lòng nhập Tên đăng nhập hoặc Email để kiểm tra câu hỏi bảo mật.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnCheckQuestion.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var result = await _authService.GetSecurityQuestionAsync(target);

                if (result.Success && !string.IsNullOrEmpty(result.SecurityQuestion))
                {
                    lblDisplayQuestion.Text = $"❓ Câu hỏi: {result.SecurityQuestion}";
                    lblDisplayQuestion.ForeColor = ThemeConfig.PrimaryColor;
                    txtForgotSecurityAnswer.Focus();
                }
                else
                {
                    lblDisplayQuestion.Text = $"⚠ {result.Message}";
                    lblDisplayQuestion.ForeColor = ThemeConfig.DangerColor;
                    MessageBox.Show(result.Message, "Không Tìm Thấy Câu Hỏi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kiểm tra câu hỏi: {ex.Message}", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCheckQuestion.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async void BtnSubmitResetPassword_Click(object? sender, EventArgs e)
        {
            try
            {
                bool vTarget = ValidateForgotUserOrEmail(true);
                bool vNewPass = ValidateForgotNewPass(true);
                bool vConfPass = ValidateForgotConfirmPass(true);

                if (!vTarget || !vNewPass || !vConfPass)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin mật khẩu mới hợp lệ.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnSubmitResetPassword.Enabled = false;
                Cursor = Cursors.WaitCursor;

                PasswordResetResultDto result;
                string target = txtForgotUserOrEmail.Text.Trim();
                string newPass = txtForgotNewPass.Text;

                if (radMethodEmail.Checked)
                {
                    if (!ValidateForgotOtp(true))
                    {
                        MessageBox.Show("Vui lòng nhập mã OTP 6 số hợp lệ.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    result = await _authService.ResetPasswordWithOtpAsync(target, txtForgotOtp.Text.Trim(), newPass);
                }
                else
                {
                    if (!ValidateForgotSecurityAnswer(true))
                    {
                        MessageBox.Show("Vui lòng nhập câu trả lời bảo mật.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    result = await _authService.ResetPasswordWithSecurityQuestionAsync(target, txtForgotSecurityAnswer.Text.Trim(), newPass);
                }

                if (result.Success)
                {
                    MessageBox.Show(
                        result.Message,
                        "Đặt Lại Mật Khẩu Thành Công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    txtLoginUsername.Text = target;
                    txtLoginPassword.Clear();
                    txtForgotOtp.Clear();
                    txtForgotSecurityAnswer.Clear();
                    txtForgotNewPass.Clear();
                    txtForgotConfirmPass.Clear();

                    ShowView(AuthView.Login);
                    txtLoginPassword.Focus();
                }
                else
                {
                    MessageBox.Show(result.Message, "Đặt Lại Mật Khẩu Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đặt lại mật khẩu: {ex.Message}", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSubmitResetPassword.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            CultureHelper.CultureChanged -= OnCultureChanged;
            base.OnFormClosed(e);
        }
    }
}
