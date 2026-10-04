using Core.Configs;
using Core.Security;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcPermission : UserControl
    {
        private readonly PermissionService _permissionService = new();
        private readonly AuditLogService _auditLogService = new();

        public UcPermission()
        {
            InitializeComponent();
            SetupGiaoDien();
            SetupDataGridView();
            LoadVaiTro();

            btnLuu.Click += async (s, e) => await LuuQuyenAsync();

            _ = LoadQuyenTheoVaiTroAsync();
        }

        private async void cboVaiTro_SelectedIndexChanged(object? sender, EventArgs e)
        {
            await LoadQuyenTheoVaiTroAsync();
        }

        private void SetupGiaoDien()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = ThemeConfig.BackgroundColor;

            dgvPhanQuyen.Dock = DockStyle.None;
            dgvPhanQuyen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            dgvPhanQuyen.Location = new Point(35, 195);
            dgvPhanQuyen.Size = new Size(this.ClientSize.Width - 70, this.ClientSize.Height - 215);

            dgvPhanQuyen.AllowUserToAddRows = false;
            dgvPhanQuyen.AllowUserToDeleteRows = false;
            dgvPhanQuyen.AllowUserToResizeRows = false;
            dgvPhanQuyen.AllowUserToResizeColumns = false;
            dgvPhanQuyen.RowHeadersVisible = false;
            dgvPhanQuyen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhanQuyen.ColumnHeadersHeight = 45;
            dgvPhanQuyen.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPhanQuyen.RowTemplate.Height = 42;
            dgvPhanQuyen.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvPhanQuyen.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvPhanQuyen.GridColor = Color.FromArgb(226, 232, 240);
            dgvPhanQuyen.BorderStyle = BorderStyle.FixedSingle;

            dgvPhanQuyen.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvPhanQuyen.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgvPhanQuyen.ColumnHeadersDefaultCellStyle.ForeColor = ThemeConfig.TextMain;
            dgvPhanQuyen.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPhanQuyen.EnableHeadersVisualStyles = false;

            dgvPhanQuyen.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvPhanQuyen.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPhanQuyen.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhanQuyen.MultiSelect = false;

            dgvPhanQuyen.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvPhanQuyen.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 235, 255);
            dgvPhanQuyen.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void LoadVaiTro()
        {
            cboVaiTro.Items.Clear();
            foreach (var role in SystemRoles.AllRoles)
            {
                cboVaiTro.Items.Add(role);
            }

            cboVaiTro.DropDownStyle = ComboBoxStyle.DropDownList;
            cboVaiTro.SelectedIndex = 0;
        }

        private void SetupDataGridView()
        {
            dgvPhanQuyen.Columns.Clear();

            // Menu Code (Hidden)
            dgvPhanQuyen.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMenuCode",
                HeaderText = "Mã",
                Visible = false
            });

            // Chức năng
            dgvPhanQuyen.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colChucNang",
                HeaderText = "Menu / Chức Năng",
                FillWeight = 200,
                ReadOnly = true,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            // Quyền Xem
            dgvPhanQuyen.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colXem",
                HeaderText = "Xem",
                FillWeight = 75,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Quyền Thêm
            dgvPhanQuyen.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colThem",
                HeaderText = "Thêm",
                FillWeight = 75,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Quyền Sửa
            dgvPhanQuyen.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colSua",
                HeaderText = "Sửa",
                FillWeight = 75,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Quyền Xóa
            dgvPhanQuyen.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colXoa",
                HeaderText = "Xóa",
                FillWeight = 75,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Quyền Xuất File
            dgvPhanQuyen.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colXuat",
                HeaderText = "Xuất File",
                FillWeight = 85,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
        }

        private async Task LoadQuyenTheoVaiTroAsync()
        {
            string vaiTro = cboVaiTro.SelectedItem?.ToString() ?? SystemRoles.Admin;
            var perms = await _permissionService.GetPermissionsByRoleAsync(vaiTro);

            dgvPhanQuyen.Rows.Clear();

            foreach (var menu in SystemMenus.AllMenus)
            {
                var perm = perms.FirstOrDefault(p => p.MenuCode == menu.Code);
                bool canView = perm?.CanView ?? (vaiTro == SystemRoles.Admin);
                bool canAdd = perm?.CanAdd ?? (vaiTro == SystemRoles.Admin);
                bool canEdit = perm?.CanEdit ?? (vaiTro == SystemRoles.Admin);
                bool canDelete = perm?.CanDelete ?? (vaiTro == SystemRoles.Admin);
                bool canExport = perm?.CanExport ?? (vaiTro == SystemRoles.Admin);

                dgvPhanQuyen.Rows.Add(
                    menu.Code,
                    menu.Name,
                    canView,
                    canAdd,
                    canEdit,
                    canDelete,
                    canExport
                );
            }

            // Quản trị viên luôn có toàn quyền và không cần sửa chính nó
            bool isReadOnly = vaiTro == SystemRoles.Admin;
            foreach (DataGridViewRow row in dgvPhanQuyen.Rows)
            {
                row.Cells["colXem"].ReadOnly = isReadOnly;
                row.Cells["colThem"].ReadOnly = isReadOnly;
                row.Cells["colSua"].ReadOnly = isReadOnly;
                row.Cells["colXoa"].ReadOnly = isReadOnly;
                row.Cells["colXuat"].ReadOnly = isReadOnly;
            }

            btnLuu.Enabled = !isReadOnly && AuthorizationGuard.CheckAccess(SystemMenus.Permission, PermissionAction.Edit, showWarning: false);
        }

        private async Task LuuQuyenAsync()
        {
            string vaiTro = cboVaiTro.SelectedItem?.ToString() ?? "";
            if (string.IsNullOrWhiteSpace(vaiTro)) return;

            if (!AuthorizationGuard.CheckAccess(SystemMenus.Permission, PermissionAction.Edit))
                return;

            dgvPhanQuyen.EndEdit();

            var permissionsToSave = new List<RolePermission>();
            foreach (DataGridViewRow row in dgvPhanQuyen.Rows)
            {
                string menuCode = row.Cells["colMenuCode"].Value?.ToString() ?? "";
                string menuName = row.Cells["colChucNang"].Value?.ToString() ?? "";
                bool canView = Convert.ToBoolean(row.Cells["colXem"].Value);
                bool canAdd = Convert.ToBoolean(row.Cells["colThem"].Value);
                bool canEdit = Convert.ToBoolean(row.Cells["colSua"].Value);
                bool canDelete = Convert.ToBoolean(row.Cells["colXoa"].Value);
                bool canExport = Convert.ToBoolean(row.Cells["colXuat"].Value);

                permissionsToSave.Add(new RolePermission
                {
                    RoleName = vaiTro,
                    MenuCode = menuCode,
                    MenuName = menuName,
                    CanView = canView,
                    CanAdd = canAdd,
                    CanEdit = canEdit,
                    CanDelete = canDelete,
                    CanExport = canExport
                });
            }

            var (success, msg) = await _permissionService.SavePermissionsAsync(vaiTro, permissionsToSave);
            if (success)
            {
                await _auditLogService.LogActionAsync(
                    "Sửa",
                    "Phân quyền",
                    vaiTro,
                    $"Cập nhật phân quyền động cho vai trò [{vaiTro}]."
                );

                MessageBox.Show(
                    $"Đã lưu thành công phân quyền cho vai trò [{vaiTro}].\nCác thay đổi đã được áp dụng ngay lập tức trên hệ thống!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(msg, "Lỗi Lưu Phân Quyền", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}