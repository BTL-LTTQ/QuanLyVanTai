using System.Diagnostics;
using Core.Configs;
using Core.Logging;
using FontAwesome.Sharp;

namespace QuanLyVanTai.UI.Forms
{
    /// <summary>
    /// Hộp thoại thông báo lỗi thân thiện với người dùng, ẩn lỗi kĩ thuật,
    /// đồng thời hỗ trợ tra cứu mã lỗi và mở thư mục Log để kiểm tra.
    /// </summary>
    public class FrmErrorDialog : Form
    {
        private readonly string _errorId;
        private readonly string _userMessage;
        private readonly string _technicalDetails;
        private TextBox txtDetails = null!;
        private IconButton btnToggleDetails = null!;
        private bool _detailsVisible = false;

        public FrmErrorDialog(string errorId, string userMessage, Exception? exception)
        {
            _errorId = errorId;
            _userMessage = userMessage;
            _technicalDetails = exception != null
                ? $"Loại lỗi: {exception.GetType().FullName}\n" +
                  $"Thông điệp: {exception.Message}\n" +
                  $"Nguồn phát sinh: {exception.Source}\n\n" +
                  $"Chi tiết StackTrace:\n{exception.StackTrace}\n\n" +
                  (exception.InnerException != null ? $"Inner Exception: {exception.InnerException.Message}\n{exception.InnerException.StackTrace}" : "")
                : "Không có thông tin chi tiết.";

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Sự Cố Hệ Thống - Cơ Chế Bắt Lỗi Toàn Cục";
            this.Size = new Size(580, 290);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = ThemeConfig.BackgroundColor;
            this.Font = ThemeConfig.MainFont;
            this.ShowInTaskbar = false;
            this.TopMost = true;

            // Header Banner
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                BackColor = Color.FromArgb(254, 242, 242) // Màu đỏ phấn cảnh báo nhẹ
            };

            var picIcon = new IconPictureBox
            {
                IconChar = IconChar.TriangleExclamation,
                IconColor = ThemeConfig.DangerColor,
                IconSize = 40,
                Size = new Size(40, 40),
                Location = new Point(20, 12),
                BackColor = Color.Transparent
            };

            var lblHeaderTitle = new Label
            {
                Text = "ỨNG DỤNG ĐÃ BẢO VỆ VÀ NGĂN CHẶN CRASH",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = ThemeConfig.DangerColor,
                AutoSize = true,
                Location = new Point(70, 13)
            };

            var lblHeaderSubtitle = new Label
            {
                Text = "Cơ chế Global Exception Handler đã bắt được ngoại lệ an toàn.",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = ThemeConfig.SecondaryColor,
                AutoSize = true,
                Location = new Point(70, 36)
            };

            pnlHeader.Controls.Add(picIcon);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Controls.Add(lblHeaderSubtitle);

            // Nội dung thân thiện
            var lblFriendly = new Label
            {
                Text = $"{_userMessage}\n\n" +
                       $"• Mã tham chiếu sự cố: {_errorId}\n" +
                       $"• Trạng thái hệ thống: Đã ghi nhận vào file Log, ứng dụng vẫn an toàn tiếp tục.",
                Location = new Point(25, 80),
                Size = new Size(520, 75),
                Font = ThemeConfig.MainFont,
                ForeColor = ThemeConfig.TextMain
            };

            // Vùng nút bấm
            var pnlButtons = new Panel
            {
                Location = new Point(25, 175),
                Size = new Size(520, 45)
            };

            var btnContinue = new IconButton
            {
                Text = " Tiếp tục",
                Size = new Size(130, 38),
                Location = new Point(390, 2)
            };
            ThemeConfig.StylePrimaryButton(btnContinue, IconChar.Check);
            btnContinue.Click += (s, e) => this.Close();

            var btnOpenLogs = new IconButton
            {
                Text = " Mở thư mục Log",
                Size = new Size(160, 38),
                Location = new Point(220, 2)
            };
            ThemeConfig.StyleSecondaryButton(btnOpenLogs, IconChar.FolderOpen);
            btnOpenLogs.Click += (s, e) =>
            {
                try
                {
                    string dir = LoggerService.LogDirectory;
                    if (Directory.Exists(dir))
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = dir,
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        MessageBox.Show("Chưa tạo thư mục Log hoặc file log chưa được ghi.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể mở thư mục: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            btnToggleDetails = new IconButton
            {
                Text = " Xem chi tiết kỹ thuật ▼",
                Size = new Size(190, 38),
                Location = new Point(0, 2),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.Transparent,
                ForeColor = ThemeConfig.SecondaryColor,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnToggleDetails.Click += (s, e) => ToggleDetails();

            pnlButtons.Controls.Add(btnContinue);
            pnlButtons.Controls.Add(btnOpenLogs);
            pnlButtons.Controls.Add(btnToggleDetails);

            // Hộp chi tiết kỹ thuật (ẩn mặc định)
            txtDetails = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Both,
                Text = _technicalDetails,
                Location = new Point(25, 230),
                Size = new Size(520, 160),
                Font = new Font("Consolas", 8.5F, FontStyle.Regular),
                BackColor = Color.FromArgb(248, 250, 252),
                Visible = false
            };

            this.Controls.Add(pnlHeader);
            this.Controls.Add(lblFriendly);
            this.Controls.Add(pnlButtons);
            this.Controls.Add(txtDetails);
        }

        private void ToggleDetails()
        {
            _detailsVisible = !_detailsVisible;
            if (_detailsVisible)
            {
                btnToggleDetails.Text = " Ẩn chi tiết kỹ thuật ▲";
                this.Height = 460;
                txtDetails.Visible = true;
            }
            else
            {
                btnToggleDetails.Text = " Xem chi tiết kỹ thuật ▼";
                txtDetails.Visible = false;
                this.Height = 290;
            }
        }
    }
}
