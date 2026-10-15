using Core.Configs;
using Core.Helpers;
using Core.Security;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;
using UserSession = Core.Security.UserSession;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcManageStaff : UserControl
    {
        // ==========================================
        // SERVICES
        // ==========================================
        private readonly AccountService _accountService = new();
        private readonly AuditLogService _auditLogService = new();

        // ==========================================
        // STATE
        // ==========================================
        private bool _isAdding = false;
        private int? _selectedAccountId = null;

        // Danh sách roles theo model Account
        private static readonly string[] Roles =
        [
            SystemRoles.Admin,
            SystemRoles.Manager,
            SystemRoles.TicketStaff
        ];

        public UcManageStaff()
        {
            InitializeComponent();

            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || this.DesignMode)
            {
                return;
            }

            if (!AuthorizationGuard.GuardFormAccess(this, SystemMenus.Staff))
                return;

            SetupRuntimeUI();
            WireEvents();
            ApplySecurity();
            _ = LoadDataAsync();
        }

        // ==========================================
        // SETUP RUNTIME UI
        // ==========================================
        private void SetupRuntimeUI()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = ThemeConfig.BackgroundColor;

            // Stats Cards
            pnlStats.Controls.Clear();
            var cardTotal = CreateStatCard("👤 Tổng tài khoản", "0", Color.FromArgb(59, 130, 246), 0, out lblStatTotal);
            var cardActive = CreateStatCard("✅ Đang hoạt động", "0", Color.FromArgb(16, 185, 129), 230, out lblStatActive);
            var cardLocked = CreateStatCard("🔒 Bị khóa", "0", Color.FromArgb(239, 68, 68), 460, out lblStatLocked);
            var cardAdmin = CreateStatCard("⚙️ Quản trị viên", "0", Color.FromArgb(139, 92, 246), 690, out lblStatAdmin);
            pnlStats.Controls.AddRange([cardTotal, cardActive, cardLocked, cardAdmin]);

            // Toolbar items
            cboRole.Items.Clear();
            cboRole.Items.Add("Tất cả vai trò");
            foreach (var r in Roles) cboRole.Items.Add(r);
            cboRole.SelectedIndex = 0;

            cboStatus.Items.Clear();
            cboStatus.Items.AddRange(["Tất cả", "Active", "Locked"]);
            cboStatus.SelectedIndex = 0;

            ThemeConfig.StyleSecondaryButton(btnRefresh, IconChar.RotateLeft);
            ThemeConfig.StyleSuccessButton(btnAdd, IconChar.UserPlus);
            ThemeConfig.StyleSecondaryButton(btnExport, IconChar.FileExcel);

            // DataGridView setup
            if (dgvStaff.Columns.Count == 0)
            {
                dgvStaff.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "ID", Visible = false });
                dgvStaff.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSTT", HeaderText = "STT", Width = 50, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
                dgvStaff.Columns.Add(new DataGridViewTextBoxColumn { Name = "colUsername", HeaderText = "Tên Đăng Nhập", Width = 140, DefaultCellStyle = { Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });
                dgvStaff.Columns.Add(new DataGridViewTextBoxColumn { Name = "colFullName", HeaderText = "Họ Và Tên", Width = 170 });
                dgvStaff.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEmail", HeaderText = "Email", Width = 185 });
                dgvStaff.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPhone", HeaderText = "Điện Thoại", Width = 115, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
                dgvStaff.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRole", HeaderText = "Vai Trò", Width = 140, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
                dgvStaff.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStatus", HeaderText = "Trạng Thái", Width = 100, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
                dgvStaff.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCreated", HeaderText = "Ngày Tạo", Width = 110, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            }

            DataGridViewHighlightHelper.AttachSearchHighlighter(dgvStaff, () => txtKeyword.Text);

            // Detail items
            cboDetailStatus.Items.Clear();
            cboDetailStatus.Items.AddRange(["Active", "Locked"]);
            cboDetailStatus.SelectedIndex = 0;

            cboRole2.Items.Clear();
            foreach (var r in Roles) cboRole2.Items.Add(r);
            cboRole2.SelectedIndex = Roles.Length > 0 ? Roles.Length - 1 : 0;

            ThemeConfig.StylePrimaryButton(btnSave, IconChar.FloppyDisk);
            ThemeConfig.StyleSecondaryButton(btnCancel, IconChar.Xmark);
            ThemeConfig.StyleDangerButton(btnDelete, IconChar.Trash);
            ThemeConfig.StyleSecondaryButton(btnResetPwd, IconChar.Key);

            SetDetailMode(false);
        }

        private static void AddLabel(string text, int y, Panel parent, int x = 18)
        {
            parent.Controls.Add(new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(x, y),
                AutoSize = true
            });
        }

        private Panel CreateStatCard(string title, string value, Color accent, int x, out Label valueLabel)
        {
            var card = new Panel
            {
                Size = new Size(215, 64),
                Location = new Point(x, 0),
                BackColor = Color.White
            };
            card.Controls.Add(new Panel { Size = new Size(4, 64), Location = new Point(0, 0), BackColor = accent });
            card.Controls.Add(new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(14, 10),
                AutoSize = true
            });
            valueLabel = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = accent,
                Location = new Point(14, 28),
                AutoSize = true
            };
            card.Controls.Add(valueLabel);
            card.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            };
            return card;
        }

        // ==========================================
        // WIRE EVENTS
        // ==========================================
        private bool _isRefreshing = false;

        private void WireEvents()
        {
            // Dùng debounce pattern để tránh nhiều event fire đồng thời → DbContext concurrent
            txtKeyword.TextChanged += (s, e) => ScheduleRefresh();
            cboRole.SelectedIndexChanged += (s, e) => ScheduleRefresh();
            cboStatus.SelectedIndexChanged += (s, e) => ScheduleRefresh();

            btnRefresh.Click += async (s, e) => await LoadDataAsync();
            btnAdd.Click += (s, e) => StartAdd();
            btnExport.Click += async (s, e) => await ExportAsync();

            dgvStaff.SelectionChanged += DgvStaff_SelectionChanged;

            btnSave.Click += async (s, e) => await SaveAsync();
            btnCancel.Click += (s, e) => CancelEdit();
            btnDelete.Click += async (s, e) => await DeleteAsync();
            btnResetPwd.Click += async (s, e) => await ResetPasswordAsync();
        }

        // Timer debounce: chờ 300ms sau lần thay đổi cuối mới gọi query → tránh concurrent
        private System.Windows.Forms.Timer? _debounceTimer;

        private void ScheduleRefresh()
        {
            _debounceTimer?.Stop();
            _debounceTimer?.Dispose();
            _debounceTimer = new System.Windows.Forms.Timer { Interval = 300 };
            _debounceTimer.Tick += async (s, e) =>
            {
                _debounceTimer?.Stop();
                _debounceTimer?.Dispose();
                _debounceTimer = null;
                await RefreshGridAsync();
            };
            _debounceTimer.Start();
        }

        private void ApplySecurity()
        {
            AuthorizationGuard.ApplyControlSecurity(
                SystemMenus.Staff,
                btnAdd, btnExport,
                hideWhenDenied: true);
        }

        // ==========================================
        // LOAD / REFRESH DATA
        // ==========================================
        private async Task LoadDataAsync()
        {
            try
            {
                await RefreshGridAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task RefreshGridAsync()
        {
            if (_isRefreshing) return;
            _isRefreshing = true;
            try
            {
                var criteria = new AccountFilterCriteria
                {
                    Keyword = txtKeyword.Text,
                    Role = cboRole.SelectedIndex > 0 ? Roles[cboRole.SelectedIndex - 1] : "Tất cả",
                    Status = cboStatus.SelectedItem?.ToString()
                };

                var accounts = await _accountService.SearchAndFilterAccountsAsync(criteria);
                RenderGrid(accounts);
                UpdateStats(accounts);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lọc dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isRefreshing = false;
            }
        }

        private void RenderGrid(List<Account> accounts)
        {
            dgvStaff.Rows.Clear();

            for (int i = 0; i < accounts.Count; i++)
            {
                var a = accounts[i];
                int idx = dgvStaff.Rows.Add(
                    a.Id,
                    i + 1,
                    a.Username,
                    a.FullName,
                    a.Email ?? "—",
                    a.PhoneNumber ?? "—",
                    a.Role,
                    a.Status,
                    a.CreatedAt.ToString("dd/MM/yyyy")
                );

                var row = dgvStaff.Rows[idx];

                var statusCell = row.Cells["colStatus"];
                statusCell.Style.ForeColor = a.Status == "Active"
                    ? Color.FromArgb(16, 185, 129)
                    : Color.FromArgb(239, 68, 68);
                statusCell.Style.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);

                var roleCell = row.Cells["colRole"];
                roleCell.Style.ForeColor = a.Role == SystemRoles.Admin
                    ? Color.FromArgb(139, 92, 246)
                    : a.Role == SystemRoles.Manager
                        ? Color.FromArgb(59, 130, 246)
                        : ThemeConfig.TextMain;
            }
        }

        private void UpdateStats(List<Account> accounts)
        {
            lblStatTotal.Text = accounts.Count.ToString();
            lblStatActive.Text = accounts.Count(a => a.Status == "Active").ToString();
            lblStatLocked.Text = accounts.Count(a => a.Status == "Locked").ToString();
            lblStatAdmin.Text = accounts.Count(a => a.Role == SystemRoles.Admin).ToString();
        }

        // ==========================================
        // DETAIL PANEL LOGIC
        // ==========================================
        private void DgvStaff_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isAdding || dgvStaff.SelectedRows.Count == 0) return;

            var row = dgvStaff.SelectedRows[0];
            _selectedAccountId = Convert.ToInt32(row.Cells["colId"].Value);
            FillDetailFromRow(row);
            SetDetailMode(isEditing: false);
        }

        private void FillDetailFromRow(DataGridViewRow row)
        {
            txtUsername.Text = row.Cells["colUsername"].Value?.ToString() ?? "";
            txtFullName.Text = row.Cells["colFullName"].Value?.ToString() ?? "";
            txtEmail.Text = (row.Cells["colEmail"].Value?.ToString() ?? "").Replace("—", "");
            txtPhone.Text = (row.Cells["colPhone"].Value?.ToString() ?? "").Replace("—", "");
            txtNewPassword.Text = "";

            string role = row.Cells["colRole"].Value?.ToString() ?? "";
            int roleIdx = Array.IndexOf(Roles, role);
            cboRole2.SelectedIndex = roleIdx >= 0 ? roleIdx : 0;

            string status = row.Cells["colStatus"].Value?.ToString() ?? "Active";
            cboDetailStatus.SelectedItem = status;
        }

        private void SetDetailMode(bool isEditing)
        {
            bool canEdit = UserSession.HasPermission(SystemMenus.Staff, PermissionAction.Edit);
            bool canDelete = UserSession.HasPermission(SystemMenus.Staff, PermissionAction.Delete);

            bool fieldsEnabled = isEditing || _isAdding;

            txtUsername.ReadOnly = !fieldsEnabled || !_isAdding; // Username chỉ nhập khi Thêm mới
            txtFullName.ReadOnly = !fieldsEnabled;
            txtEmail.ReadOnly = !fieldsEnabled;
            txtPhone.ReadOnly = !fieldsEnabled;
            cboRole2.Enabled = fieldsEnabled;
            cboDetailStatus.Enabled = fieldsEnabled;
            txtNewPassword.ReadOnly = !fieldsEnabled;
            txtNewPassword.Visible = fieldsEnabled || !_isAdding;
            lblPasswordHint.Visible = !_isAdding && fieldsEnabled;

            btnSave.Enabled = fieldsEnabled;
            btnCancel.Enabled = fieldsEnabled;
            btnDelete.Enabled = !fieldsEnabled && _selectedAccountId.HasValue && canDelete;
            btnResetPwd.Enabled = !fieldsEnabled && _selectedAccountId.HasValue && canEdit;
        }

        private void StartAdd()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Staff, PermissionAction.Add)) return;

            _isAdding = true;
            _selectedAccountId = null;
            dgvStaff.ClearSelection();

            lblDetailTitle.Text = "THÊM TÀI KHOẢN MỚI";
            txtUsername.Text = "";
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtNewPassword.Text = "";
            cboRole2.SelectedIndex = cboRole2.Items.Count - 1;
            cboDetailStatus.SelectedIndex = 0;

            SetDetailMode(isEditing: true);
            txtUsername.Focus();
        }

        private void CancelEdit()
        {
            _isAdding = false;
            lblDetailTitle.Text = "CHI TIẾT TÀI KHOẢN";
            SetDetailMode(isEditing: false);
            dgvStaff.ClearSelection();
            ClearDetail();
        }

        private void ClearDetail()
        {
            txtUsername.Text = "";
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtNewPassword.Text = "";
        }

        // ==========================================
        // CRUD OPERATIONS
        // ==========================================
        private async Task SaveAsync()
        {
            if (!ValidateInput()) return;

            var account = new Account
            {
                Id = _selectedAccountId ?? 0,
                Username = txtUsername.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                PhoneNumber = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim(),
                Role = cboRole2.SelectedItem?.ToString() ?? SystemRoles.TicketStaff,
                Status = cboDetailStatus.SelectedItem?.ToString() ?? "Active"
            };

            if (_isAdding)
            {
                if (!AuthorizationGuard.CheckAccess(SystemMenus.Staff, PermissionAction.Add)) return;

                string password = txtNewPassword.Text;
                if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                {
                    MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPassword.Focus();
                    return;
                }

                var (ok, msg, created) = await _accountService.CreateAccountAsync(account, password);
                if (ok)
                {
                    _ = WriteAuditAsync("Thêm", created!.Id.ToString(), $"Thêm tài khoản [{created.Username}] vai trò [{created.Role}]");
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isAdding = false;
                    lblDetailTitle.Text = "CHI TIẾT TÀI KHOẢN";
                    await RefreshGridAsync();
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (_selectedAccountId.HasValue)
            {
                if (!AuthorizationGuard.CheckAccess(SystemMenus.Staff, PermissionAction.Edit)) return;

                account.Id = _selectedAccountId.Value;
                var (ok, msg) = await _accountService.UpdateAccountAsync(account);
                if (ok)
                {
                    _ = WriteAuditAsync("Sửa", account.Id.ToString(), $"Cập nhật tài khoản [{account.Username}]");
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await RefreshGridAsync();
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            SetDetailMode(isEditing: false);
        }

        private async Task DeleteAsync()
        {
            if (!_selectedAccountId.HasValue) return;
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Staff, PermissionAction.Delete)) return;

            // Không cho xóa chính mình
            if (_selectedAccountId.Value == UserSession.CurrentUserId)
            {
                MessageBox.Show("Không thể xóa tài khoản của chính bạn!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = txtUsername.Text;
            var result = MessageBox.Show(
                $"Xóa tài khoản [{username}]? Các vé liên quan sẽ được giữ lại nhưng ngắt liên kết.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            var (ok, msg) = await _accountService.DeleteAccountAsync(_selectedAccountId.Value);
            if (ok)
            {
                _ = WriteAuditAsync("Xóa", _selectedAccountId.Value.ToString(), $"Xóa tài khoản [{username}]");
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _selectedAccountId = null;
                ClearDetail();
                await RefreshGridAsync();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ResetPasswordAsync()
        {
            if (!_selectedAccountId.HasValue) return;
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Staff, PermissionAction.Edit)) return;

            string newPwd = txtNewPassword.Text.Trim();
            if (string.IsNullOrWhiteSpace(newPwd) || newPwd.Length < 6)
            {
                MessageBox.Show("Nhập mật khẩu mới (ít nhất 6 ký tự) vào ô Mật Khẩu trước khi Reset.",
                    "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNewPassword.Focus();
                return;
            }

            var result = MessageBox.Show(
                $"Đặt lại mật khẩu mới cho tài khoản [{txtUsername.Text}]?",
                "Xác nhận Reset mật khẩu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            var (ok, msg) = await _accountService.ResetPasswordAsync(_selectedAccountId.Value, newPwd);
            if (ok)
            {
                _ = WriteAuditAsync("Reset mật khẩu", _selectedAccountId.Value.ToString(),
                    $"Đặt lại MK cho [{txtUsername.Text}]");
                txtNewPassword.Text = "";
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ExportAsync()
        {
            btnExport.Enabled = false;
            try
            {
                await DataExportHelper.ExportDataGridViewWithDialogAsync(dgvStaff, "DanhSach_NhanVien");
            }
            finally
            {
                AuthorizationGuard.ApplyControlSecurity(SystemMenus.Staff, btnExport: btnExport);
            }
        }

        // ==========================================
        // VALIDATION
        // ==========================================
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            return true;
        }

        // ==========================================
        // AUDIT
        // ==========================================
        private async Task WriteAuditAsync(string action, string recordId, string details)
        {
            await _auditLogService.LogActionAsync(
                action, "Account", recordId,
                $"{details} | Người thực hiện: {UserSession.CurrentUsername} | {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e) { }
        private void dgvNhanSu_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
