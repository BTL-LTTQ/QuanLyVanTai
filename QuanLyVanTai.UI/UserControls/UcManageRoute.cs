using Core.Configs;
using Core.Helpers;
using Core.Security;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.UI.Forms;
using UserSession = Core.Security.UserSession;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcManageRoute : UserControl
    {
        private readonly RouteService _routeService = new();
        private readonly StationService _stationService = new();
        private readonly VehicleService _vehicleService = new();

        // UI Controls
        private readonly Panel pnlToolbar = new();
        private readonly SplitContainer splitMain = new();
        private readonly DataGridView dgvRoutes = new();

        // Search & Filter
        private readonly TextBox txtSearch = new();
        private readonly IconButton btnSearch = new();

        // Action Buttons
        private readonly IconButton btnAdd = new();
        private readonly IconButton btnEdit = new();
        private readonly IconButton btnDelete = new();
        private readonly IconButton btnSave = new();
        private readonly IconButton btnCancel = new();
        private readonly IconButton btnHistory = new();
        private readonly IconButton btnExport = new();

        // Detail Edit Controls
        private readonly Panel pnlDetail = new();
        private readonly TextBox txtRouteCode = new();
        private readonly TextBox txtRouteName = new();
        private readonly NumericUpDown numDistance = new();
        private readonly NumericUpDown numEstimatedHours = new();
        private readonly NumericUpDown numBasePrice = new();
        private readonly ComboBox cboStatus = new();
        private readonly CheckedListBox clbStations = new();
        private readonly CheckedListBox clbVehicles = new();

        // State tracking
        private bool _isAddingNew = false;
        private int? _selectedRouteId = null;
        private List<Station> _allStations = [];
        private List<Vehicle> _allVehicles = [];

        public UcManageRoute()
        {
            InitializeComponent();
            BuildCustomLayout();
            SetupEvents();
            ApplySecurity();
            _ = LoadInitialDataAsync();
        }

        private void BuildCustomLayout()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = ThemeConfig.BackgroundColor;

            // ==========================================
            // 1. Toolbar & Header
            // ==========================================
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Height = 70;
            pnlToolbar.BackColor = Color.White;
            pnlToolbar.Padding = new Padding(15, 12, 15, 12);

            // Style Buttons
            ThemeConfig.StyleSuccessButton(btnAdd, IconChar.Plus);
            btnAdd.Text = " Thêm";
            btnAdd.Size = new Size(100, 42);
            btnAdd.Location = new Point(15, 14);

            ThemeConfig.StylePrimaryButton(btnEdit, IconChar.Pen);
            btnEdit.Text = " Sửa";
            btnEdit.Size = new Size(95, 42);
            btnEdit.Location = new Point(125, 14);

            ThemeConfig.StyleDangerButton(btnDelete, IconChar.Trash);
            btnDelete.Text = " Xóa";
            btnDelete.Size = new Size(95, 42);
            btnDelete.Location = new Point(230, 14);

            ThemeConfig.StylePrimaryButton(btnSave, IconChar.FloppyDisk);
            btnSave.Text = " Lưu";
            btnSave.Size = new Size(95, 42);
            btnSave.Location = new Point(335, 14);
            btnSave.Enabled = false;

            ThemeConfig.StyleSecondaryButton(btnCancel, IconChar.Xmark);
            btnCancel.Text = " Hủy";
            btnCancel.Size = new Size(90, 42);
            btnCancel.Location = new Point(440, 14);
            btnCancel.Enabled = false;

            ThemeConfig.StyleSecondaryButton(btnHistory, IconChar.ClockRotateLeft);
            btnHistory.Text = " Lịch sử";
            btnHistory.BackColor = Color.FromArgb(100, 116, 139);
            btnHistory.Size = new Size(105, 42);
            btnHistory.Location = new Point(540, 14);

            ThemeConfig.StyleSecondaryButton(btnExport, IconChar.FileExcel);
            btnExport.Text = " Xuất file";
            btnExport.BackColor = Color.FromArgb(16, 185, 129); // Green
            btnExport.Size = new Size(115, 42);
            btnExport.Location = new Point(655, 14);

            // Search box
            txtSearch.Size = new Size(220, 36);
            txtSearch.Location = new Point(810, 18);
            txtSearch.Font = new Font("Segoe UI", 10.5F);
            txtSearch.PlaceholderText = "🔍 Tìm tuyến, lộ trình...";
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            pnlToolbar.Controls.AddRange([btnAdd, btnEdit, btnDelete, btnSave, btnCancel, btnHistory, btnExport, txtSearch]);
            this.Controls.Add(pnlToolbar);

            // ==========================================
            // 2. Split Container (Master DataGridView / Detail Panel)
            // ==========================================
            splitMain.Dock = DockStyle.Fill;
            splitMain.Orientation = Orientation.Vertical;
            splitMain.SplitterDistance = 640;
            splitMain.SplitterWidth = 6;
            splitMain.BackColor = Color.FromArgb(226, 232, 240);

            // Setup DataGridView
            dgvRoutes.Dock = DockStyle.Fill;
            dgvRoutes.BackgroundColor = Color.White;
            dgvRoutes.BorderStyle = BorderStyle.None;
            dgvRoutes.AllowUserToAddRows = false;
            dgvRoutes.AllowUserToDeleteRows = false;
            dgvRoutes.ReadOnly = true;
            dgvRoutes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoutes.MultiSelect = false;
            dgvRoutes.RowHeadersVisible = false;
            dgvRoutes.RowTemplate.Height = 44;
            dgvRoutes.ColumnHeadersHeight = 44;
            dgvRoutes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvRoutes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvRoutes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgvRoutes.ColumnHeadersDefaultCellStyle.ForeColor = ThemeConfig.TextMain;
            dgvRoutes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRoutes.EnableHeadersVisualStyles = false;
            dgvRoutes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvRoutes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvRoutes.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvRoutes.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "ID", Visible = false });
            dgvRoutes.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCode", HeaderText = "Mã Tuyến", Width = 110, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvRoutes.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Tên Lộ Trình", Width = 210 });
            dgvRoutes.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDistance", HeaderText = "Cự Ly (km)", Width = 100, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvRoutes.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHours", HeaderText = "Thời Gian (h)", Width = 105, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvRoutes.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPrice", HeaderText = "Giá Cước (VNĐ)", Width = 125, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvRoutes.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStations", HeaderText = "Số Trạm", Width = 85, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvRoutes.Columns.Add(new DataGridViewTextBoxColumn { Name = "colVehicles", HeaderText = "Số Xe", Width = 80, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvRoutes.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStatus", HeaderText = "Trạng Thái", Width = 110, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvRoutes.Columns.Add(new DataGridViewTextBoxColumn { Name = "colUpdatedBy", HeaderText = "Người Cập Nhật", Width = 130 });

            // Attach Real-time Highlight Helper
            DataGridViewHighlightHelper.AttachSearchHighlighter(dgvRoutes, () => txtSearch.Text);

            splitMain.Panel1.Controls.Add(dgvRoutes);

            // ==========================================
            // 3. Detail & Multi-table Panel (Panel 2)
            // ==========================================
            pnlDetail.Dock = DockStyle.Fill;
            pnlDetail.BackColor = Color.White;
            pnlDetail.AutoScroll = true;
            pnlDetail.Padding = new Padding(18);

            Label lblDetailTitle = new Label
            {
                Text = "CHI TIẾT TUYẾN XE & LIÊN KẾT ĐA BẢNG",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = ThemeConfig.PrimaryColor,
                AutoSize = true,
                Location = new Point(18, 15)
            };
            pnlDetail.Controls.Add(lblDetailTitle);

            int y = 50;

            // Mã Tuyến
            pnlDetail.Controls.Add(CreateFieldLabel("Mã Tuyến Xe *:", 18, y));
            txtRouteCode.Location = new Point(18, y + 24);
            txtRouteCode.Size = new Size(180, 32);
            pnlDetail.Controls.Add(txtRouteCode);

            // Trạng Thái
            pnlDetail.Controls.Add(CreateFieldLabel("Trạng Thái:", 215, y));
            cboStatus.Location = new Point(215, y + 24);
            cboStatus.Size = new Size(185, 32);
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.Items.AddRange(["Active", "Suspended"]);
            cboStatus.SelectedIndex = 0;
            pnlDetail.Controls.Add(cboStatus);

            y += 65;

            // Tên Lộ Trình
            pnlDetail.Controls.Add(CreateFieldLabel("Tên Tuyến / Lộ Trình *:", 18, y));
            txtRouteName.Location = new Point(18, y + 24);
            txtRouteName.Size = new Size(382, 32);
            pnlDetail.Controls.Add(txtRouteName);

            y += 65;

            // Cự ly (km)
            pnlDetail.Controls.Add(CreateFieldLabel("Cự Ly (km):", 18, y));
            numDistance.Location = new Point(18, y + 24);
            numDistance.Size = new Size(120, 32);
            numDistance.DecimalPlaces = 1;
            numDistance.Maximum = 5000;
            pnlDetail.Controls.Add(numDistance);

            // Thời gian (giờ)
            pnlDetail.Controls.Add(CreateFieldLabel("Thời Gian (giờ):", 150, y));
            numEstimatedHours.Location = new Point(150, y + 24);
            numEstimatedHours.Size = new Size(110, 32);
            numEstimatedHours.DecimalPlaces = 1;
            numEstimatedHours.Maximum = 100;
            pnlDetail.Controls.Add(numEstimatedHours);

            // Giá cước cơ bản
            pnlDetail.Controls.Add(CreateFieldLabel("Giá Vé (VNĐ):", 270, y));
            numBasePrice.Location = new Point(270, y + 24);
            numBasePrice.Size = new Size(130, 32);
            numBasePrice.ThousandsSeparator = true;
            numBasePrice.Maximum = 10000000;
            numBasePrice.Increment = 10000;
            pnlDetail.Controls.Add(numBasePrice);

            y += 65;

            // Multi-table 1: Trạm dừng liên kết (N - N)
            Label lblStationsTitle = CreateFieldLabel("📍 Trạm Dừng Thuộc Lộ Trình (Liên kết N-N):", 18, y);
            lblStationsTitle.ForeColor = ThemeConfig.PrimaryColor;
            lblStationsTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            pnlDetail.Controls.Add(lblStationsTitle);

            clbStations.Location = new Point(18, y + 24);
            clbStations.Size = new Size(382, 110);
            clbStations.CheckOnClick = true;
            pnlDetail.Controls.Add(clbStations);

            y += 145;

            // Multi-table 2: Phương tiện phân công (1 - N)
            Label lblVehiclesTitle = CreateFieldLabel("🚌 Phương Tiện Phân Công (Liên kết 1-N):", 18, y);
            lblVehiclesTitle.ForeColor = ThemeConfig.PrimaryColor;
            lblVehiclesTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            pnlDetail.Controls.Add(lblVehiclesTitle);

            clbVehicles.Location = new Point(18, y + 24);
            clbVehicles.Size = new Size(382, 110);
            clbVehicles.CheckOnClick = true;
            pnlDetail.Controls.Add(clbVehicles);

            splitMain.Panel2.Controls.Add(pnlDetail);
            this.Controls.Add(splitMain);

            SetFormEditable(false);
        }

        private Label CreateFieldLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105)
            };
        }

        private void SetupEvents()
        {
            dgvRoutes.SelectionChanged += dgvRoutes_SelectionChanged;

            // Real-time search debounce / event
            txtSearch.TextChanged += async (s, e) =>
            {
                await ReloadRoutesListAsync(txtSearch.Text);
            };

            btnAdd.Click += (s, e) => StartAddNew();
            btnEdit.Click += (s, e) => StartEdit();
            btnSave.Click += async (s, e) => await SaveRouteAsync();
            btnCancel.Click += (s, e) => CancelEdit();
            btnDelete.Click += async (s, e) => await DeleteSelectedRouteAsync();
            btnHistory.Click += async (s, e) => await ViewAuditHistoryAsync();
            btnExport.Click += (s, e) => DataExportHelper.ExportDataGridViewWithDialog(dgvRoutes, "DanhSach_TuyenXe");
        }

        private void ApplySecurity()
        {
            AuthorizationGuard.ApplyControlSecurity(SystemMenus.Route, btnAdd, btnEdit, btnDelete, btnExport);
        }

        private async Task LoadInitialDataAsync()
        {
            try
            {
                // Tải danh sách Trạm dừng và Phương tiện cho multi-table selection
                _allStations = await _stationService.GetAllStationsAsync();
                _allVehicles = await _vehicleService.GetAllVehiclesAsync();

                clbStations.Items.Clear();
                foreach (var st in _allStations)
                {
                    clbStations.Items.Add($"{st.StationCode} - {st.StationName} ({st.City})");
                }

                clbVehicles.Items.Clear();
                foreach (var v in _allVehicles)
                {
                    clbVehicles.Items.Add($"{v.LicensePlate} ({v.VehicleType})");
                }

                await ReloadRoutesListAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu tuyến xe: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ReloadRoutesListAsync(string? keyword = null)
        {
            int? currentId = _selectedRouteId;
            var routes = await _routeService.GetAllRoutesAsync(keyword);

            dgvRoutes.Rows.Clear();
            foreach (var r in routes)
            {
                string statusText = r.Status == "Active" ? "Đang hoạt động" : "Tạm ngừng";
                string updatedInfo = r.UpdatedBy ?? r.CreatedBy ?? "admin";
                if (r.UpdatedAt.HasValue)
                {
                    updatedInfo += $" ({r.UpdatedAt.Value.ToLocalTime():dd/MM/yyyy})";
                }

                int rowIndex = dgvRoutes.Rows.Add(
                    r.Id,
                    r.RouteCode,
                    r.RouteName,
                    $"{r.DistanceKm:N1}",
                    $"{r.EstimatedHours:N1}",
                    $"{r.BasePrice:N0}",
                    r.Stations.Count,
                    r.Vehicles.Count,
                    statusText,
                    updatedInfo
                );

                if (currentId.HasValue && r.Id == currentId.Value)
                {
                    dgvRoutes.Rows[rowIndex].Selected = true;
                }
            }

            if (dgvRoutes.SelectedRows.Count == 0 && dgvRoutes.Rows.Count > 0)
            {
                dgvRoutes.Rows[0].Selected = true;
            }
        }

        private async void dgvRoutes_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isAddingNew || dgvRoutes.SelectedRows.Count == 0)
                return;

            var row = dgvRoutes.SelectedRows[0];
            int routeId = Convert.ToInt32(row.Cells["colId"].Value);
            _selectedRouteId = routeId;

            var route = await _routeService.GetRouteByIdAsync(routeId);
            if (route != null)
            {
                DisplayRouteDetails(route);
            }
        }

        private void DisplayRouteDetails(Route route)
        {
            txtRouteCode.Text = route.RouteCode;
            txtRouteName.Text = route.RouteName;
            numDistance.Value = Math.Min(numDistance.Maximum, route.DistanceKm);
            numEstimatedHours.Value = Math.Min(numEstimatedHours.Maximum, route.EstimatedHours);
            numBasePrice.Value = Math.Min(numBasePrice.Maximum, route.BasePrice);
            cboStatus.SelectedItem = route.Status;

            // Check multi-table Stations
            var routeStationIds = route.Stations.Select(s => s.Id).ToHashSet();
            for (int i = 0; i < _allStations.Count; i++)
            {
                clbStations.SetItemChecked(i, routeStationIds.Contains(_allStations[i].Id));
            }

            // Check multi-table Vehicles
            var routeVehicleIds = route.Vehicles.Select(v => v.Id).ToHashSet();
            for (int i = 0; i < _allVehicles.Count; i++)
            {
                clbVehicles.SetItemChecked(i, routeVehicleIds.Contains(_allVehicles[i].Id));
            }
        }

        private void SetFormEditable(bool editable)
        {
            txtRouteCode.ReadOnly = !editable;
            txtRouteName.ReadOnly = !editable;
            numDistance.Enabled = editable;
            numEstimatedHours.Enabled = editable;
            numBasePrice.Enabled = editable;
            cboStatus.Enabled = editable;
            clbStations.Enabled = editable;
            clbVehicles.Enabled = editable;

            btnAdd.Enabled = !editable && UserSession.HasPermission(SystemMenus.Route, PermissionAction.Add);
            btnEdit.Enabled = !editable && dgvRoutes.SelectedRows.Count > 0 && UserSession.HasPermission(SystemMenus.Route, PermissionAction.Edit);
            btnDelete.Enabled = !editable && dgvRoutes.SelectedRows.Count > 0 && UserSession.HasPermission(SystemMenus.Route, PermissionAction.Delete);
            btnHistory.Enabled = !editable && dgvRoutes.SelectedRows.Count > 0;
            btnExport.Enabled = !editable;

            btnSave.Enabled = editable;
            btnCancel.Enabled = editable;
            dgvRoutes.Enabled = !editable;
        }

        private void StartAddNew()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Route, PermissionAction.Add))
                return;

            _isAddingNew = true;
            _selectedRouteId = null;

            txtRouteCode.Clear();
            txtRouteName.Clear();
            numDistance.Value = 0;
            numEstimatedHours.Value = 0;
            numBasePrice.Value = 100000;
            cboStatus.SelectedIndex = 0;

            for (int i = 0; i < clbStations.Items.Count; i++)
                clbStations.SetItemChecked(i, false);

            for (int i = 0; i < clbVehicles.Items.Count; i++)
                clbVehicles.SetItemChecked(i, false);

            SetFormEditable(true);
            txtRouteCode.Focus();
        }

        private void StartEdit()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Route, PermissionAction.Edit))
                return;

            if (_selectedRouteId == null)
            {
                MessageBox.Show("Vui lòng chọn một tuyến xe cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _isAddingNew = false;
            SetFormEditable(true);
            txtRouteCode.Focus();
        }

        private void CancelEdit()
        {
            _isAddingNew = false;
            SetFormEditable(false);
            dgvRoutes_SelectionChanged(null, EventArgs.Empty);
        }

        private async Task SaveRouteAsync()
        {
            string code = txtRouteCode.Text.Trim();
            string name = txtRouteName.Text.Trim();

            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Vui lòng nhập Mã tuyến xe!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRouteCode.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng nhập Tên lộ trình tuyến xe!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRouteName.Focus();
                return;
            }

            // Thu thập trạm dừng được chọn (Multi-table)
            var selectedStationIds = new List<int>();
            for (int i = 0; i < clbStations.Items.Count; i++)
            {
                if (clbStations.GetItemChecked(i) && i < _allStations.Count)
                {
                    selectedStationIds.Add(_allStations[i].Id);
                }
            }

            // Thu thập xe phân công (Multi-table)
            var selectedVehicleIds = new List<int>();
            for (int i = 0; i < clbVehicles.Items.Count; i++)
            {
                if (clbVehicles.GetItemChecked(i) && i < _allVehicles.Count)
                {
                    selectedVehicleIds.Add(_allVehicles[i].Id);
                }
            }

            if (_isAddingNew)
            {
                var newRoute = new Route
                {
                    RouteCode = code,
                    RouteName = name,
                    DistanceKm = numDistance.Value,
                    EstimatedHours = numEstimatedHours.Value,
                    BasePrice = numBasePrice.Value,
                    Status = cboStatus.SelectedItem?.ToString() ?? "Active"
                };

                var (success, msg, created) = await _routeService.CreateRouteAsync(newRoute, selectedStationIds, selectedVehicleIds);
                if (success && created != null)
                {
                    _selectedRouteId = created.Id;
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetFormEditable(false);
                    _isAddingNew = false;
                    await ReloadRoutesListAsync();
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi thêm tuyến xe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (_selectedRouteId.HasValue)
            {
                var updateRoute = new Route
                {
                    Id = _selectedRouteId.Value,
                    RouteCode = code,
                    RouteName = name,
                    DistanceKm = numDistance.Value,
                    EstimatedHours = numEstimatedHours.Value,
                    BasePrice = numBasePrice.Value,
                    Status = cboStatus.SelectedItem?.ToString() ?? "Active"
                };

                var (success, msg) = await _routeService.UpdateRouteAsync(updateRoute, selectedStationIds, selectedVehicleIds);
                if (success)
                {
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetFormEditable(false);
                    await ReloadRoutesListAsync();
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task DeleteSelectedRouteAsync()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Route, PermissionAction.Delete))
                return;

            if (_selectedRouteId == null)
            {
                MessageBox.Show("Vui lòng chọn tuyến xe cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string code = txtRouteCode.Text;
            string name = txtRouteName.Text;

            // Mở Dialog Xác nhận xóa an toàn
            using var confirmDlg = new FrmConfirmDelete(
                $"{code} ({name})",
                $"Đang có {clbStations.CheckedItems.Count} trạm dừng và {clbVehicles.CheckedItems.Count} xe được liên kết."
            );

            if (confirmDlg.ShowDialog() != DialogResult.OK)
                return;

            bool isSoftDelete = confirmDlg.IsSoftDelete;
            var (success, msg) = await _routeService.DeleteRouteAsync(_selectedRouteId.Value, isSoftDelete);

            if (success)
            {
                MessageBox.Show(msg, "Kết quả xóa an toàn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _selectedRouteId = null;
                await ReloadRoutesListAsync();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi xóa dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ViewAuditHistoryAsync()
        {
            if (_selectedRouteId == null)
            {
                MessageBox.Show("Vui lòng chọn tuyến xe để xem lịch sử!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var logs = await _routeService.GetRouteAuditHistoryAsync(_selectedRouteId.Value);
            using var histDlg = new FrmAuditHistory($"Tuyến xe {txtRouteCode.Text}", logs);
            histDlg.ShowDialog();
        }
    }
}
