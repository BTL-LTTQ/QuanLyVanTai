using Core.Configs;
using FontAwesome.Sharp;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.UI.Forms
{
    public class FrmAuditHistory : Form
    {
        private readonly DataGridView dgvHistory = new();
        private readonly Label lblTitle = new();
        private readonly IconButton btnClose = new();
        private readonly Panel pnlHeader = new();
        private readonly Panel pnlBottom = new();

        public FrmAuditHistory(string entityTitle, List<AuditLog> logs)
        {
            InitializeComponent(entityTitle, logs);
        }

        private void InitializeComponent(string entityTitle, List<AuditLog> logs)
        {
            this.Text = $"Lịch sử chỉnh sửa - {entityTitle}";
            this.Size = new Size(880, 520);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = ThemeConfig.BackgroundColor;
            this.Font = ThemeConfig.MainFont;

            // Header Panel
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 60;
            pnlHeader.BackColor = ThemeConfig.PrimaryColor;

            lblTitle.Text = $"LỊCH SỬ CHỈNH SỬA DỮ LIỆU: {entityTitle.ToUpper()}";
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 18);
            pnlHeader.Controls.Add(lblTitle);

            // Bottom Panel
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Height = 60;
            pnlBottom.BackColor = Color.White;

            ThemeConfig.StyleSecondaryButton(btnClose, IconChar.Xmark);
            btnClose.Text = " Đóng";
            btnClose.Size = new Size(110, 38);
            btnClose.Location = new Point(this.ClientSize.Width - 130, 11);
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Click += (s, e) => this.Close();
            pnlBottom.Controls.Add(btnClose);

            // DataGridView Setup
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.BorderStyle = BorderStyle.None;
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToDeleteRows = false;
            dgvHistory.ReadOnly = true;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.MultiSelect = false;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.RowTemplate.Height = 40;
            dgvHistory.ColumnHeadersHeight = 40;
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = ThemeConfig.TextMain;
            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTime", HeaderText = "Thời Gian", FillWeight = 120 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "colUser", HeaderText = "Người Sửa", FillWeight = 100 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRole", HeaderText = "Vai Trò", FillWeight = 90 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "colAction", HeaderText = "Hành Động", FillWeight = 80 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDetails", HeaderText = "Chi Tiết Thay Đổi", FillWeight = 250 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "colIp", HeaderText = "Địa Chỉ IP", FillWeight = 90 });

            // Nạp dữ liệu
            if (logs.Count == 0)
            {
                dgvHistory.Rows.Add(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), "Hệ thống", "System", "Khởi tạo", "Bản ghi ban đầu của hệ thống", "127.0.0.1");
            }
            else
            {
                foreach (var log in logs)
                {
                    string details = log.Details ?? string.Empty;
                    if (!string.IsNullOrEmpty(log.NewValues))
                    {
                        details += $" | Dữ liệu mới: {log.NewValues}";
                    }
                    if (!string.IsNullOrEmpty(log.OldValues))
                    {
                        details += $" | Giá trị cũ: {log.OldValues}";
                    }

                    dgvHistory.Rows.Add(
                        log.Timestamp.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss"),
                        log.Username,
                        log.Role,
                        log.Action,
                        details,
                        log.IpAddress
                    );
                }
            }

            this.Controls.Add(dgvHistory);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(pnlHeader);
        }
    }
}
