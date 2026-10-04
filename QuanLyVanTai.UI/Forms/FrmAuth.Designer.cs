namespace QuanLyVanTai.UI.Forms
{
    partial class FrmAuth
    {
        private System.ComponentModel.IContainer components = null;

        // Container panels
        private Panel pnlBrand;
        private Panel pnlRight;
        private Panel pnlTopNav;
        private Panel pnlContentContainer;

        // Brand controls
        private FontAwesome.Sharp.IconPictureBox picLogo;
        private Label lblBrandTitle;
        private Label lblBrandSubtitle;
        private Panel pnlBrandDivider;
        private Label lblFeature1;
        private Label lblFeature2;
        private Label lblFeature3;
        private Panel pnlLocaleCard;
        private Label lblLocaleDemoTitle;
        private Label lblLocaleCurrent;
        private Label lblLocaleDate;
        private Label lblLocalePrice;
        private Label lblLanguageTitle;
        private ComboBox cboLanguage;

        // Nav tabs
        private Button btnNavLogin;
        private Button btnNavRegister;
        private Button btnNavForgot;

        // Login View controls
        private Panel pnlViewLogin;
        private Label lblLoginHeading;
        private Label lblLoginSubheading;
        private Label lblLoginUserTitle;
        private TextBox txtLoginUsername;
        private Label lblLoginPassTitle;
        private TextBox txtLoginPassword;
        private Button btnToggleLoginPass;
        private CheckBox chkRememberMe;
        private LinkLabel lnkForgotPassword;
        private FontAwesome.Sharp.IconButton btnSubmitLogin;
        private Label lblLoginLockoutStatus;
        private Label lblNoAccountPrompt;
        private LinkLabel lnkGoToRegister;

        // Register View controls
        private Panel pnlViewRegister;
        private Label lblRegHeading;
        private Label lblRegSubheading;
        private Label lblRegFullName;
        private TextBox txtRegFullName;
        private Label lblRegUsername;
        private TextBox txtRegUsername;
        private Label lblRegEmail;
        private TextBox txtRegEmail;
        private Label lblRegPhone;
        private TextBox txtRegPhone;
        private Label lblRegPassword;
        private TextBox txtRegPassword;
        private Button btnToggleRegPass;
        private Label lblRegConfirmPassword;
        private TextBox txtRegConfirmPassword;
        private Label lblRegSecurityQuestion;
        private ComboBox cboRegSecurityQuestion;
        private Label lblRegSecurityAnswer;
        private TextBox txtRegSecurityAnswer;
        private FontAwesome.Sharp.IconButton btnSubmitRegister;
        private Label lblHaveAccountPrompt;
        private LinkLabel lnkGoToLogin;

        // Forgot Password View controls
        private Panel pnlViewForgot;
        private Label lblForgotHeading;
        private Label lblForgotSubheading;
        private Label lblForgotUserOrEmail;
        private TextBox txtForgotUserOrEmail;
        private Label lblRecoveryMethodTitle;
        private RadioButton radMethodEmail;
        private RadioButton radMethodQuestion;
        private Panel pnlMethodEmail;
        private FontAwesome.Sharp.IconButton btnSendOtp;
        private Label lblOtpPrompt;
        private TextBox txtForgotOtp;
        private Panel pnlMethodQuestion;
        private FontAwesome.Sharp.IconButton btnCheckQuestion;
        private Label lblDisplayQuestion;
        private TextBox txtForgotSecurityAnswer;
        private Panel pnlNewPasswordSection;
        private Label lblForgotNewPass;
        private TextBox txtForgotNewPass;
        private Label lblForgotConfirmPass;
        private TextBox txtForgotConfirmPass;
        private FontAwesome.Sharp.IconButton btnSubmitResetPassword;
        private LinkLabel lnkBackToLogin;

        // Providers
        private ErrorProvider epErrors;
        private ToolTip toolTipHelp;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            epErrors = new ErrorProvider(components);
            toolTipHelp = new ToolTip(components);

            // Left Brand Panel
            pnlBrand = new Panel();
            picLogo = new FontAwesome.Sharp.IconPictureBox();
            lblBrandTitle = new Label();
            lblBrandSubtitle = new Label();
            pnlBrandDivider = new Panel();
            lblFeature1 = new Label();
            lblFeature2 = new Label();
            lblFeature3 = new Label();
            pnlLocaleCard = new Panel();
            lblLocaleDemoTitle = new Label();
            lblLocaleCurrent = new Label();
            lblLocaleDate = new Label();
            lblLocalePrice = new Label();
            lblLanguageTitle = new Label();
            cboLanguage = new ComboBox();

            // Right Action Panel
            pnlRight = new Panel();
            pnlTopNav = new Panel();
            btnNavLogin = new Button();
            btnNavRegister = new Button();
            btnNavForgot = new Button();
            pnlContentContainer = new Panel();

            // Login View
            pnlViewLogin = new Panel();
            lblLoginHeading = new Label();
            lblLoginSubheading = new Label();
            lblLoginUserTitle = new Label();
            txtLoginUsername = new TextBox();
            lblLoginPassTitle = new Label();
            txtLoginPassword = new TextBox();
            btnToggleLoginPass = new Button();
            chkRememberMe = new CheckBox();
            lnkForgotPassword = new LinkLabel();
            btnSubmitLogin = new FontAwesome.Sharp.IconButton();
            lblLoginLockoutStatus = new Label();
            lblNoAccountPrompt = new Label();
            lnkGoToRegister = new LinkLabel();

            // Register View
            pnlViewRegister = new Panel();
            lblRegHeading = new Label();
            lblRegSubheading = new Label();
            lblRegFullName = new Label();
            txtRegFullName = new TextBox();
            lblRegUsername = new Label();
            txtRegUsername = new TextBox();
            lblRegEmail = new Label();
            txtRegEmail = new TextBox();
            lblRegPhone = new Label();
            txtRegPhone = new TextBox();
            lblRegPassword = new Label();
            txtRegPassword = new TextBox();
            btnToggleRegPass = new Button();
            lblRegConfirmPassword = new Label();
            txtRegConfirmPassword = new TextBox();
            lblRegSecurityQuestion = new Label();
            cboRegSecurityQuestion = new ComboBox();
            lblRegSecurityAnswer = new Label();
            txtRegSecurityAnswer = new TextBox();
            btnSubmitRegister = new FontAwesome.Sharp.IconButton();
            lblHaveAccountPrompt = new Label();
            lnkGoToLogin = new LinkLabel();

            // Forgot Password View
            pnlViewForgot = new Panel();
            lblForgotHeading = new Label();
            lblForgotSubheading = new Label();
            lblForgotUserOrEmail = new Label();
            txtForgotUserOrEmail = new TextBox();
            lblRecoveryMethodTitle = new Label();
            radMethodEmail = new RadioButton();
            radMethodQuestion = new RadioButton();
            pnlMethodEmail = new Panel();
            btnSendOtp = new FontAwesome.Sharp.IconButton();
            lblOtpPrompt = new Label();
            txtForgotOtp = new TextBox();
            pnlMethodQuestion = new Panel();
            btnCheckQuestion = new FontAwesome.Sharp.IconButton();
            lblDisplayQuestion = new Label();
            txtForgotSecurityAnswer = new TextBox();
            pnlNewPasswordSection = new Panel();
            lblForgotNewPass = new Label();
            txtForgotNewPass = new TextBox();
            lblForgotConfirmPass = new Label();
            txtForgotConfirmPass = new TextBox();
            btnSubmitResetPassword = new FontAwesome.Sharp.IconButton();
            lnkBackToLogin = new LinkLabel();

            ((System.ComponentModel.ISupportInitialize)epErrors).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlBrand.SuspendLayout();
            pnlLocaleCard.SuspendLayout();
            pnlRight.SuspendLayout();
            pnlTopNav.SuspendLayout();
            pnlContentContainer.SuspendLayout();
            pnlViewLogin.SuspendLayout();
            pnlViewRegister.SuspendLayout();
            pnlViewForgot.SuspendLayout();
            pnlMethodEmail.SuspendLayout();
            pnlMethodQuestion.SuspendLayout();
            pnlNewPasswordSection.SuspendLayout();
            SuspendLayout();

            // ==========================================
            // Form properties
            // ==========================================
            ClientSize = new Size(1000, 700);
            MinimumSize = new Size(960, 680);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Text = "Hệ Thống Quản Lý Vận Tải - Xác Thực Người Dùng";

            // ErrorProvider setup
            epErrors.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            // ==========================================
            // 1. LEFT BRAND PANEL
            // ==========================================
            pnlBrand.Dock = DockStyle.Left;
            pnlBrand.Width = 330;
            pnlBrand.Padding = new Padding(24);

            // picLogo
            picLogo.IconChar = FontAwesome.Sharp.IconChar.Bus;
            picLogo.IconSize = 64;
            picLogo.Size = new Size(64, 64);
            picLogo.Location = new Point(24, 28);
            picLogo.BackColor = Color.Transparent;
            picLogo.ForeColor = Color.White;

            // lblBrandTitle
            lblBrandTitle.Location = new Point(24, 100);
            lblBrandTitle.Size = new Size(282, 50);
            lblBrandTitle.Text = "HỆ THỐNG QUẢN LÝ VẬN TẢI";
            lblBrandTitle.ForeColor = Color.White;

            // lblBrandSubtitle
            lblBrandSubtitle.Location = new Point(24, 155);
            lblBrandSubtitle.Size = new Size(282, 45);
            lblBrandSubtitle.Text = "Giải pháp vận tải hành khách liên tỉnh tiện lợi, an toàn và hiện đại.";
            lblBrandSubtitle.ForeColor = Color.FromArgb(224, 231, 255);

            // pnlBrandDivider
            pnlBrandDivider.Location = new Point(24, 208);
            pnlBrandDivider.Size = new Size(282, 1);
            pnlBrandDivider.BackColor = Color.FromArgb(80, 255, 255, 255);

            // Features badges
            lblFeature1.Location = new Point(24, 225);
            lblFeature1.Size = new Size(282, 28);
            lblFeature1.Text = "✔ Bảo mật chuẩn Hash + Salt (PBKDF2)";
            lblFeature1.ForeColor = Color.White;

            lblFeature2.Location = new Point(24, 258);
            lblFeature2.Size = new Size(282, 28);
            lblFeature2.Text = "✔ Tự động khóa chống Brute-force";
            lblFeature2.ForeColor = Color.White;

            lblFeature3.Location = new Point(24, 291);
            lblFeature3.Size = new Size(282, 28);
            lblFeature3.Text = "✔ Khôi phục qua Email OTP / Bí mật";
            lblFeature3.ForeColor = Color.White;

            // Locale Demo Card (Part 3 display)
            pnlLocaleCard.Location = new Point(20, 335);
            pnlLocaleCard.Size = new Size(290, 160);
            pnlLocaleCard.BackColor = Color.FromArgb(30, 255, 255, 255);
            pnlLocaleCard.Padding = new Padding(12);

            lblLocaleDemoTitle.Location = new Point(12, 10);
            lblLocaleDemoTitle.Size = new Size(266, 24);
            lblLocaleDemoTitle.Text = "🌐 Định dạng vùng (Locale Info):";
            lblLocaleDemoTitle.ForeColor = Color.White;

            lblLocaleCurrent.Location = new Point(12, 38);
            lblLocaleCurrent.Size = new Size(266, 22);
            lblLocaleCurrent.Text = "Vùng hiện tại: vi-VN (Tiếng Việt)";
            lblLocaleCurrent.ForeColor = Color.FromArgb(240, 244, 255);

            lblLocaleDate.Location = new Point(12, 65);
            lblLocaleDate.Size = new Size(266, 40);
            lblLocaleDate.Text = "Thời gian: ...";
            lblLocaleDate.ForeColor = Color.FromArgb(240, 244, 255);

            lblLocalePrice.Location = new Point(12, 110);
            lblLocalePrice.Size = new Size(266, 40);
            lblLocalePrice.Text = "Giá vé mẫu: 250.000 ₫";
            lblLocalePrice.ForeColor = Color.FromArgb(254, 240, 138);

            pnlLocaleCard.Controls.AddRange(new Control[] {
                lblLocaleDemoTitle, lblLocaleCurrent, lblLocaleDate, lblLocalePrice
            });

            // Language selector
            lblLanguageTitle.Location = new Point(24, 515);
            lblLanguageTitle.Size = new Size(282, 24);
            lblLanguageTitle.Text = "Lựa chọn ngôn ngữ / Language:";
            lblLanguageTitle.ForeColor = Color.White;

            cboLanguage.Location = new Point(24, 545);
            cboLanguage.Size = new Size(282, 32);
            cboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;

            pnlBrand.Controls.AddRange(new Control[] {
                picLogo, lblBrandTitle, lblBrandSubtitle, pnlBrandDivider,
                lblFeature1, lblFeature2, lblFeature3, pnlLocaleCard,
                lblLanguageTitle, cboLanguage
            });

            // ==========================================
            // 2. RIGHT CONTAINER
            // ==========================================
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Padding = new Padding(24, 16, 24, 16);

            // Top Navigation tabs
            pnlTopNav.Dock = DockStyle.Top;
            pnlTopNav.Height = 50;

            btnNavLogin.Location = new Point(0, 5);
            btnNavLogin.Size = new Size(180, 40);
            btnNavLogin.Text = "Đăng nhập";
            btnNavLogin.FlatStyle = FlatStyle.Flat;
            btnNavLogin.FlatAppearance.BorderSize = 0;
            btnNavLogin.Cursor = Cursors.Hand;

            btnNavRegister.Location = new Point(190, 5);
            btnNavRegister.Size = new Size(180, 40);
            btnNavRegister.Text = "Đăng ký tài khoản";
            btnNavRegister.FlatStyle = FlatStyle.Flat;
            btnNavRegister.FlatAppearance.BorderSize = 0;
            btnNavRegister.Cursor = Cursors.Hand;

            btnNavForgot.Location = new Point(380, 5);
            btnNavForgot.Size = new Size(180, 40);
            btnNavForgot.Text = "Quên mật khẩu";
            btnNavForgot.FlatStyle = FlatStyle.Flat;
            btnNavForgot.FlatAppearance.BorderSize = 0;
            btnNavForgot.Cursor = Cursors.Hand;

            pnlTopNav.Controls.AddRange(new Control[] { btnNavLogin, btnNavRegister, btnNavForgot });

            // Content container
            pnlContentContainer.Dock = DockStyle.Fill;
            pnlContentContainer.AutoScroll = true;

            // ==========================================
            // 2A. LOGIN VIEW
            // ==========================================
            pnlViewLogin.Dock = DockStyle.Fill;
            pnlViewLogin.Padding = new Padding(20);

            lblLoginHeading.Location = new Point(20, 20);
            lblLoginHeading.Size = new Size(560, 36);
            lblLoginHeading.Text = "ĐĂNG NHẬP";

            lblLoginSubheading.Location = new Point(20, 58);
            lblLoginSubheading.Size = new Size(560, 24);
            lblLoginSubheading.Text = "Chào mừng bạn quay trở lại. Vui lòng nhập thông tin xác thực.";

            lblLoginUserTitle.Location = new Point(20, 100);
            lblLoginUserTitle.Size = new Size(540, 22);
            lblLoginUserTitle.Text = "Tên đăng nhập hoặc Email:";

            txtLoginUsername.Location = new Point(20, 125);
            txtLoginUsername.Size = new Size(540, 32);

            lblLoginPassTitle.Location = new Point(20, 175);
            lblLoginPassTitle.Size = new Size(540, 22);
            lblLoginPassTitle.Text = "Mật khẩu:";

            txtLoginPassword.Location = new Point(20, 200);
            txtLoginPassword.Size = new Size(490, 32);
            txtLoginPassword.UseSystemPasswordChar = true;

            btnToggleLoginPass.Location = new Point(515, 199);
            btnToggleLoginPass.Size = new Size(45, 33);
            btnToggleLoginPass.Text = "👁";
            btnToggleLoginPass.FlatStyle = FlatStyle.Flat;
            btnToggleLoginPass.Cursor = Cursors.Hand;

            chkRememberMe.Location = new Point(20, 250);
            chkRememberMe.Size = new Size(260, 28);
            chkRememberMe.Text = "Ghi nhớ đăng nhập";
            chkRememberMe.Cursor = Cursors.Hand;

            lnkForgotPassword.Location = new Point(360, 253);
            lnkForgotPassword.Size = new Size(200, 24);
            lnkForgotPassword.Text = "Quên mật khẩu?";
            lnkForgotPassword.TextAlign = ContentAlignment.TopRight;
            lnkForgotPassword.Cursor = Cursors.Hand;

            btnSubmitLogin.Location = new Point(20, 295);
            btnSubmitLogin.Size = new Size(540, 48);
            btnSubmitLogin.Text = " ĐĂNG NHẬP";
            btnSubmitLogin.IconChar = FontAwesome.Sharp.IconChar.RightToBracket;

            lblLoginLockoutStatus.Location = new Point(20, 355);
            lblLoginLockoutStatus.Size = new Size(540, 45);
            lblLoginLockoutStatus.Visible = false;

            lblNoAccountPrompt.Location = new Point(140, 415);
            lblNoAccountPrompt.Size = new Size(180, 24);
            lblNoAccountPrompt.Text = "Chưa có tài khoản?";
            lblNoAccountPrompt.TextAlign = ContentAlignment.MiddleRight;

            lnkGoToRegister.Location = new Point(325, 415);
            lnkGoToRegister.Size = new Size(160, 24);
            lnkGoToRegister.Text = "Đăng ký ngay";
            lnkGoToRegister.TextAlign = ContentAlignment.MiddleLeft;
            lnkGoToRegister.Cursor = Cursors.Hand;

            pnlViewLogin.Controls.AddRange(new Control[] {
                lblLoginHeading, lblLoginSubheading, lblLoginUserTitle, txtLoginUsername,
                lblLoginPassTitle, txtLoginPassword, btnToggleLoginPass,
                chkRememberMe, lnkForgotPassword, btnSubmitLogin,
                lblLoginLockoutStatus, lblNoAccountPrompt, lnkGoToRegister
            });

            // ==========================================
            // 2B. REGISTER VIEW
            // ==========================================
            pnlViewRegister.Dock = DockStyle.Fill;
            pnlViewRegister.Padding = new Padding(20);
            pnlViewRegister.AutoScroll = true;

            lblRegHeading.Location = new Point(20, 10);
            lblRegHeading.Size = new Size(560, 32);
            lblRegHeading.Text = "TẠO TÀI KHOẢN MỚI";

            lblRegSubheading.Location = new Point(20, 44);
            lblRegSubheading.Size = new Size(560, 22);
            lblRegSubheading.Text = "Điền đầy đủ các thông tin bên dưới để đăng ký tài khoản nhân viên.";

            lblRegFullName.Location = new Point(20, 75);
            lblRegFullName.Size = new Size(260, 20);
            lblRegFullName.Text = "Họ và tên đầy đủ *:";

            txtRegFullName.Location = new Point(20, 98);
            txtRegFullName.Size = new Size(260, 30);

            lblRegUsername.Location = new Point(300, 75);
            lblRegUsername.Size = new Size(260, 20);
            lblRegUsername.Text = "Tên đăng nhập *:";

            txtRegUsername.Location = new Point(300, 98);
            txtRegUsername.Size = new Size(260, 30);

            lblRegEmail.Location = new Point(20, 140);
            lblRegEmail.Size = new Size(260, 20);
            lblRegEmail.Text = "Địa chỉ Email *:";

            txtRegEmail.Location = new Point(20, 163);
            txtRegEmail.Size = new Size(260, 30);

            lblRegPhone.Location = new Point(300, 140);
            lblRegPhone.Size = new Size(260, 20);
            lblRegPhone.Text = "Số điện thoại *:";

            txtRegPhone.Location = new Point(300, 163);
            txtRegPhone.Size = new Size(260, 30);

            lblRegPassword.Location = new Point(20, 205);
            lblRegPassword.Size = new Size(260, 20);
            lblRegPassword.Text = "Mật khẩu *:";

            txtRegPassword.Location = new Point(20, 228);
            txtRegPassword.Size = new Size(215, 30);
            txtRegPassword.UseSystemPasswordChar = true;

            btnToggleRegPass.Location = new Point(240, 227);
            btnToggleRegPass.Size = new Size(40, 31);
            btnToggleRegPass.Text = "👁";
            btnToggleRegPass.FlatStyle = FlatStyle.Flat;
            btnToggleRegPass.Cursor = Cursors.Hand;

            lblRegConfirmPassword.Location = new Point(300, 205);
            lblRegConfirmPassword.Size = new Size(260, 20);
            lblRegConfirmPassword.Text = "Xác nhận mật khẩu *:";

            txtRegConfirmPassword.Location = new Point(300, 228);
            txtRegConfirmPassword.Size = new Size(260, 30);
            txtRegConfirmPassword.UseSystemPasswordChar = true;

            lblRegSecurityQuestion.Location = new Point(20, 270);
            lblRegSecurityQuestion.Size = new Size(540, 20);
            lblRegSecurityQuestion.Text = "Câu hỏi bảo mật (dùng để khôi phục tài khoản) *:";

            cboRegSecurityQuestion.Location = new Point(20, 293);
            cboRegSecurityQuestion.Size = new Size(540, 30);
            cboRegSecurityQuestion.DropDownStyle = ComboBoxStyle.DropDownList;

            lblRegSecurityAnswer.Location = new Point(20, 335);
            lblRegSecurityAnswer.Size = new Size(540, 20);
            lblRegSecurityAnswer.Text = "Câu trả lời bí mật *:";

            txtRegSecurityAnswer.Location = new Point(20, 358);
            txtRegSecurityAnswer.Size = new Size(540, 30);

            btnSubmitRegister.Location = new Point(20, 410);
            btnSubmitRegister.Size = new Size(540, 46);
            btnSubmitRegister.Text = " ĐĂNG KÝ TÀI KHOẢN";
            btnSubmitRegister.IconChar = FontAwesome.Sharp.IconChar.UserPlus;

            lblHaveAccountPrompt.Location = new Point(140, 470);
            lblHaveAccountPrompt.Size = new Size(180, 24);
            lblHaveAccountPrompt.Text = "Đã có tài khoản?";
            lblHaveAccountPrompt.TextAlign = ContentAlignment.MiddleRight;

            lnkGoToLogin.Location = new Point(325, 470);
            lnkGoToLogin.Size = new Size(160, 24);
            lnkGoToLogin.Text = "Đăng nhập ngay";
            lnkGoToLogin.TextAlign = ContentAlignment.MiddleLeft;
            lnkGoToLogin.Cursor = Cursors.Hand;

            pnlViewRegister.Controls.AddRange(new Control[] {
                lblRegHeading, lblRegSubheading,
                lblRegFullName, txtRegFullName,
                lblRegUsername, txtRegUsername,
                lblRegEmail, txtRegEmail,
                lblRegPhone, txtRegPhone,
                lblRegPassword, txtRegPassword, btnToggleRegPass,
                lblRegConfirmPassword, txtRegConfirmPassword,
                lblRegSecurityQuestion, cboRegSecurityQuestion,
                lblRegSecurityAnswer, txtRegSecurityAnswer,
                btnSubmitRegister, lblHaveAccountPrompt, lnkGoToLogin
            });

            // ==========================================
            // 2C. FORGOT PASSWORD VIEW
            // ==========================================
            pnlViewForgot.Dock = DockStyle.Fill;
            pnlViewForgot.Padding = new Padding(20);
            pnlViewForgot.AutoScroll = true;

            lblForgotHeading.Location = new Point(20, 15);
            lblForgotHeading.Size = new Size(560, 32);
            lblForgotHeading.Text = "KHÔI PHỤC MẬT KHẨU";

            lblForgotSubheading.Location = new Point(20, 50);
            lblForgotSubheading.Size = new Size(560, 22);
            lblForgotSubheading.Text = "Chọn phương thức khôi phục thuận tiện nhất cho bạn.";

            lblForgotUserOrEmail.Location = new Point(20, 85);
            lblForgotUserOrEmail.Size = new Size(540, 22);
            lblForgotUserOrEmail.Text = "Tên đăng nhập hoặc Email đăng ký *:";

            txtForgotUserOrEmail.Location = new Point(20, 110);
            txtForgotUserOrEmail.Size = new Size(540, 32);

            lblRecoveryMethodTitle.Location = new Point(20, 155);
            lblRecoveryMethodTitle.Size = new Size(540, 22);
            lblRecoveryMethodTitle.Text = "Phương thức khôi phục:";

            radMethodEmail.Location = new Point(20, 180);
            radMethodEmail.Size = new Size(250, 28);
            radMethodEmail.Text = "Qua Email (Mã OTP)";
            radMethodEmail.Checked = true;
            radMethodEmail.Cursor = Cursors.Hand;

            radMethodQuestion.Location = new Point(290, 180);
            radMethodQuestion.Size = new Size(270, 28);
            radMethodQuestion.Text = "Qua Câu hỏi bảo mật";
            radMethodQuestion.Cursor = Cursors.Hand;

            // Panel Email OTP
            pnlMethodEmail.Location = new Point(20, 215);
            pnlMethodEmail.Size = new Size(540, 70);

            btnSendOtp.Location = new Point(0, 15);
            btnSendOtp.Size = new Size(180, 40);
            btnSendOtp.Text = " Gửi mã OTP";
            btnSendOtp.IconChar = FontAwesome.Sharp.IconChar.PaperPlane;

            lblOtpPrompt.Location = new Point(200, 0);
            lblOtpPrompt.Size = new Size(340, 20);
            lblOtpPrompt.Text = "Nhập mã OTP 6 số đã nhận *:";

            txtForgotOtp.Location = new Point(200, 22);
            txtForgotOtp.Size = new Size(340, 32);

            pnlMethodEmail.Controls.AddRange(new Control[] { btnSendOtp, lblOtpPrompt, txtForgotOtp });

            // Panel Security Question
            pnlMethodQuestion.Location = new Point(20, 215);
            pnlMethodQuestion.Size = new Size(540, 85);
            pnlMethodQuestion.Visible = false;

            btnCheckQuestion.Location = new Point(0, 10);
            btnCheckQuestion.Size = new Size(180, 36);
            btnCheckQuestion.Text = " Lấy câu hỏi";
            btnCheckQuestion.IconChar = FontAwesome.Sharp.IconChar.Question;

            lblDisplayQuestion.Location = new Point(190, 10);
            lblDisplayQuestion.Size = new Size(350, 36);
            lblDisplayQuestion.Text = "Nhấn 'Lấy câu hỏi' để hiển thị câu hỏi bảo mật của tài khoản.";

            txtForgotSecurityAnswer.Location = new Point(0, 52);
            txtForgotSecurityAnswer.Size = new Size(540, 30);

            pnlMethodQuestion.Controls.AddRange(new Control[] { btnCheckQuestion, lblDisplayQuestion, txtForgotSecurityAnswer });

            // New password section
            pnlNewPasswordSection.Location = new Point(20, 295);
            pnlNewPasswordSection.Size = new Size(540, 120);

            lblForgotNewPass.Location = new Point(0, 5);
            lblForgotNewPass.Size = new Size(260, 20);
            lblForgotNewPass.Text = "Mật khẩu mới *:";

            txtForgotNewPass.Location = new Point(0, 28);
            txtForgotNewPass.Size = new Size(260, 30);
            txtForgotNewPass.UseSystemPasswordChar = true;

            lblForgotConfirmPass.Location = new Point(280, 5);
            lblForgotConfirmPass.Size = new Size(260, 20);
            lblForgotConfirmPass.Text = "Xác nhận mật khẩu mới *:";

            txtForgotConfirmPass.Location = new Point(280, 28);
            txtForgotConfirmPass.Size = new Size(260, 30);
            txtForgotConfirmPass.UseSystemPasswordChar = true;

            btnSubmitResetPassword.Location = new Point(0, 72);
            btnSubmitResetPassword.Size = new Size(540, 44);
            btnSubmitResetPassword.Text = " ĐẶT LẠI MẬT KHẨU";
            btnSubmitResetPassword.IconChar = FontAwesome.Sharp.IconChar.Key;

            pnlNewPasswordSection.Controls.AddRange(new Control[] {
                lblForgotNewPass, txtForgotNewPass,
                lblForgotConfirmPass, txtForgotConfirmPass,
                btnSubmitResetPassword
            });

            lnkBackToLogin.Location = new Point(20, 425);
            lnkBackToLogin.Size = new Size(540, 26);
            lnkBackToLogin.Text = "← Quay lại Đăng nhập";
            lnkBackToLogin.TextAlign = ContentAlignment.MiddleCenter;
            lnkBackToLogin.Cursor = Cursors.Hand;

            pnlViewForgot.Controls.AddRange(new Control[] {
                lblForgotHeading, lblForgotSubheading,
                lblForgotUserOrEmail, txtForgotUserOrEmail,
                lblRecoveryMethodTitle, radMethodEmail, radMethodQuestion,
                pnlMethodEmail, pnlMethodQuestion,
                pnlNewPasswordSection, lnkBackToLogin
            });

            // Add views to container
            pnlContentContainer.Controls.AddRange(new Control[] {
                pnlViewForgot, pnlViewRegister, pnlViewLogin
            });

            pnlRight.Controls.AddRange(new Control[] {
                pnlContentContainer, pnlTopNav
            });

            Controls.AddRange(new Control[] {
                pnlRight, pnlBrand
            });

            ((System.ComponentModel.ISupportInitialize)epErrors).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlBrand.ResumeLayout(false);
            pnlLocaleCard.ResumeLayout(false);
            pnlRight.ResumeLayout(false);
            pnlTopNav.ResumeLayout(false);
            pnlContentContainer.ResumeLayout(false);
            pnlViewLogin.ResumeLayout(false);
            pnlViewLogin.PerformLayout();
            pnlViewRegister.ResumeLayout(false);
            pnlViewRegister.PerformLayout();
            pnlViewForgot.ResumeLayout(false);
            pnlViewForgot.PerformLayout();
            pnlMethodEmail.ResumeLayout(false);
            pnlMethodEmail.PerformLayout();
            pnlMethodQuestion.ResumeLayout(false);
            pnlMethodQuestion.PerformLayout();
            pnlNewPasswordSection.ResumeLayout(false);
            pnlNewPasswordSection.PerformLayout();
            ResumeLayout(false);
        }
    }
}
