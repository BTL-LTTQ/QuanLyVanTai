using Core.Configs;
using Core.Helpers;
using Core.Security;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcAuditLog : UserControl
    {
        private readonly AuditLogService _auditLogService = new();

        public UcAuditLog()
        {
            InitializeComponent();
            SetupGiaoDien();
            SetupFilterControls();

            btnTimKiem.Click += async (s, e) => await LoadLogsAsync();
            btnXuat.Click += (s, e) => DataExportHelper.ExportDataGridViewWithDialog(dgvAuditLog, "NhatKyHoatDong");

            _ = LoadLogsAsync();
        }

        private void SetupFilterControls()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = ThemeConfig.BackgroundColor;

            dateTimePicker1.Value = DateTime.Today.AddDays(-7);
            dateTimePicker2.Value = DateTime.Today;

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(["Tất cả", "admin", "quanly", "nhanvien"]);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;

            btnTimKiem.BackColor = ThemeConfig.PrimaryColor;
            btnTimKiem.ForeColor = Color.White;
            btnTimKiem.FlatStyle = FlatStyle.Flat;
            btnTimKiem.FlatAppearance.BorderSize = 0;
            btnTimKiem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            btnXuat.BackColor = ThemeConfig.SuccessColor;
            btnXuat.ForeColor = Color.White;
            btnXuat.FlatStyle = FlatStyle.Flat;
            btnXuat.FlatAppearance.BorderSize = 0;
            btnXuat.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        }

        private void SetupGiaoDien()
        {
            dgvAuditLog.Dock = DockStyle.None;
            dgvAuditLog.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            dgvAuditLog.Location = new Point(30, 200);
            dgvAuditLog.Size = new Size(this.ClientSize.Width - 60, this.ClientSize.Height - 220);

            dgvAuditLog.AllowUserToAddRows = false;
            dgvAuditLog.AllowUserToDeleteRows = false;
            dgvAuditLog.AllowUserToResizeRows = false;
            dgvAuditLog.AllowUserToResizeColumns = false;
            dgvAuditLog.RowHeadersVisible = false;
            dgvAuditLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAuditLog.ColumnHeadersHeight = 44;
            dgvAuditLog.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvAuditLog.RowTemplate.Height = 40;
            dgvAuditLog.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvAuditLog.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvAuditLog.GridColor = Color.FromArgb(226, 232, 240);
            dgvAuditLog.BorderStyle = BorderStyle.FixedSingle;

            dgvAuditLog.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvAuditLog.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgvAuditLog.ColumnHeadersDefaultCellStyle.ForeColor = ThemeConfig.TextMain;
            dgvAuditLog.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAuditLog.EnableHeadersVisualStyles = false;

            dgvAuditLog.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvAuditLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAuditLog.MultiSelect = false;
            dgvAuditLog.ReadOnly = true;

            dgvAuditLog.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvAuditLog.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 235, 255);
            dgvAuditLog.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Highlight search
            DataGridViewHighlightHelper.AttachSearchHighlighter(dgvAuditLog, () => comboBox1.SelectedItem?.ToString() == "Tất cả" ? "" : comboBox1.SelectedItem?.ToString() ?? "");
        }

        private async Task LoadLogsAsync()
        {
            var criteria = new AuditLogFilterCriteria
            {
                FromDate = dateTimePicker1.Value.Date,
                ToDate = dateTimePicker2.Value.Date,
                Username = comboBox1.SelectedItem?.ToString()
            };

            var logs = await _auditLogService.SearchLogsAsync(criteria);
            dgvAuditLog.Rows.Clear();

            if (logs.Count == 0)
            {
                // Thêm demo logs nếu mới khởi tạo
                ThemLog(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), UserSession.CurrentUsername, UserSession.CurrentRole, "Khởi chạy", "Hệ thống", UserSession.CurrentIpAddress);
            }
            else
            {
                foreach (var log in logs)
                {
                    ThemLog(
                        log.Timestamp.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss"),
                        log.Username,
                        log.Role,
                        log.Action,
                        $"{log.EntityName} (#{log.RecordId ?? "N/A"}) - {log.Details}",
                        log.IpAddress
                    );
                }
            }
        }

        private void ThemLog(
            string thoiGian,
            string nguoiDung,
            string vaiTro,
            string hanhDong,
            string doiTuong,
            string diaChiIP)
        {
            dgvAuditLog.Rows.Add(
                thoiGian,
                nguoiDung,
                vaiTro,
                hanhDong,
                doiTuong,
                diaChiIP
            );
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }
    }
}
