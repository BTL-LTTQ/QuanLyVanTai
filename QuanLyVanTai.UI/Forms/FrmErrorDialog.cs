using System.ComponentModel;
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
    public partial class FrmErrorDialog : Form
    {
        private readonly string _errorId;
        private readonly string _userMessage;
        private readonly string _technicalDetails;
        private bool _detailsVisible = false;

        public FrmErrorDialog() : this("ERR-0000", "Lỗi mẫu hiển thị giao diện thiết kế", null)
        {
        }

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

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || this.DesignMode)
            {
                return;
            }

            lblFriendly.Text = $"{_userMessage}\n\n" +
                               $"• Mã tham chiếu sự cố: {_errorId}\n" +
                               $"• Trạng thái hệ thống: Đã ghi nhận vào file Log, ứng dụng vẫn an toàn tiếp tục.";

            txtDetails.Text = _technicalDetails;

            ThemeConfig.StylePrimaryButton(btnContinue, IconChar.Check);
            btnContinue.Click += (s, e) => this.Close();

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

            btnToggleDetails.Click += (s, e) => ToggleDetails();
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
