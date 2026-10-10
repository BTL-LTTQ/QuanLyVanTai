using Core.Configs;
using Core.Helpers;
using Core.Security;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Exceptions;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.UI.Forms;
using UserSession = Core.Security.UserSession;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcManageRoute : UserControl
    {
        private readonly RouteManagementService _routeService = new();
        private readonly StationManagementService _stationService = new();
        private readonly VehicleManagementService _vehicleService = new();

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
        private readonly ComboBox cboFkStation = new();
        private readonly ComboBox cboFkVehicle = new();

        // State tracking
        private bool _isAddingNew = false;
        private bool _suppressFkComboEvents = false;
        private int? _selectedRouteId = null;
        private List<StationSummaryDto> _allStations = [];
        private List<VehicleSummaryDto> _allVehicles = [];

        private sealed class FkLookupItem
        {
            public int Id { get; set; }
            public string Display { get; set; } = string.Empty;
        }

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
            this.BackColor = Color.FromArgb(248, 250, 252);

            // ==========================================
            // 1. DASHBOARD HEADER với Statistics Cards
            // ==========================================
            Panel pnlDashboard = new Panel
            {
                Dock = DockStyle.Top,
                Height = 160,
                BackColor = Color.FromArgb(248, 250, 252),
                Padding = new Padding(20, 15, 20, 15)
            };

            Label lblPageTitle = new Label
            {
                Text = "📊 TRANG CHỦ - DASHBOARD QUẢN LÝ TUYẾN XE",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                AutoSize = true,
                Location = new Point(20, 15)
            };
            pnlDashboard.Controls.Add(lblPageTitle);

            // Statistics Cards Container
            Panel pnlStats = new Panel
            {
                Location = new Point(20, 50),
                Size = new Size(1200, 95),
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            // Card 1: Tổng tuyến xe
            Panel cardTotalRoutes = CreateStatCard("🚌 Tổng Tuyến Xe", "0", Color.FromArgb(59, 130, 246), 0);
            // Card 2: Đang hoạt động
            Panel cardActiveRoutes = CreateStatCard("✅ Đang Hoạt Động", "0", Color.FromArgb(16, 185, 129), 310);
            // Card 3: Tổng quãng đường
            Panel cardTotalDistance = CreateStatCard("📏 Tổng Quãng Đường", "0 km", Color.FromArgb(245, 158, 11), 620);
            // Card 4: Doanh thu ước tính
            Panel cardRevenue = CreateStatCard("💰 Giá TB/Tuyến", "0 VNĐ", Color.FromArgb(239, 68, 68), 930);

            pnlStats.Controls.AddRange(new Control[] { cardTotalRoutes, cardActiveRoutes, cardTotalDistance, cardRevenue });
            pnlDashboard.Controls.Add(pnlStats);

            this.Controls.Add(pnlDashboard);

            // ==========================================
            // 2. Toolbar & Action Buttons
            // ==========================================
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Height = 75;
            pnlToolbar.BackColor = Color.White;
            pnlToolbar.Padding = new Padding(20, 15, 20, 15);

            // Style Buttons với icon đẹp hơn
            ThemeConfig.StyleSuccessButton(btnAdd, IconChar.PlusCircle);
            btnAdd.Text = " Thêm Tuyến";
            btnAdd.Size = new Size(130, 45);
            btnAdd.Location = new Point(20, 15);
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            ThemeConfig.StylePrimaryButton(btnEdit, IconChar.PenToSquare);
            btnEdit.Text = " Chỉnh Sửa";
            btnEdit.Size = new Size(130, 45);
            btnEdit.Location = new Point(160, 15);
            btnEdit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            ThemeConfig.StyleDangerButton(btnDelete, IconChar.TrashAlt);
            btnDelete.Text = " Xóa";
            btnDelete.Size = new Size(110, 45);
            btnDelete.Location = new Point(300, 15);
            btnDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            ThemeConfig.StylePrimaryButton(btnSave, IconChar.FloppyDisk);
            btnSave.Text = " Lưu";
            btnSave.Size = new Size(110, 45);
            btnSave.Location = new Point(420, 15);
            btnSave.Enabled = false;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            ThemeConfig.StyleSecondaryButton(btnCancel, IconChar.Times);
            btnCancel.Text = " Hủy";
            btnCancel.Size = new Size(100, 45);
            btnCancel.Location = new Point(540, 15);
            btnCancel.Enabled = false;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            ThemeConfig.StyleSecondaryButton(btnHistory, IconChar.History);
            btnHistory.Text = " Lịch Sử";
            btnHistory.BackColor = Color.FromArgb(100, 116, 139);
            btnHistory.Size = new Size(120, 45);
            btnHistory.Location = new Point(650, 15);
            btnHistory.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            ThemeConfig.StyleSecondaryButton(btnExport, IconChar.FileExcel);
            btnExport.Text = " Xuất Excel";
            btnExport.BackColor = Color.FromArgb(16, 185, 129);
            btnExport.Size = new Size(130, 45);
            btnExport.Location = new Point(780, 15);
            btnExport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Search box với style đẹp hơn
            txtSearch.Size = new Size(280, 45);
            txtSearch.Location = new Point(920, 15);
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.PlaceholderText = "🔍 Tìm kiếm tuyến xe, lộ trình...";
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;

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

            // Multi-table 1: Trạm dừng liên kết (N - N) + ComboBox khóa ngoại
            Label lblStationsTitle = CreateFieldLabel("📍 Trạm Dừng Thuộc Lộ Trình (Liên kết N-N):", 18, y);
            lblStationsTitle.ForeColor = ThemeConfig.PrimaryColor;
            lblStationsTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            pnlDetail.Controls.Add(lblStationsTitle);

            cboFkStation.Location = new Point(18, y + 22);
            cboFkStation.Size = new Size(382, 28);
            cboFkStation.DropDownStyle = ComboBoxStyle.DropDownList;
            pnlDetail.Controls.Add(cboFkStation);

            clbStations.Location = new Point(18, y + 54);
            clbStations.Size = new Size(382, 90);
            clbStations.CheckOnClick = true;
            pnlDetail.Controls.Add(clbStations);

            y += 155;

            // Multi-table 2: Phương tiện phân công (1 - N) + ComboBox khóa ngoại
            Label lblVehiclesTitle = CreateFieldLabel("🚌 Phương Tiện Phân Công (Liên kết 1-N):", 18, y);
            lblVehiclesTitle.ForeColor = ThemeConfig.PrimaryColor;
            lblVehiclesTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            pnlDetail.Controls.Add(lblVehiclesTitle);

            cboFkVehicle.Location = new Point(18, y + 22);
            cboFkVehicle.Size = new Size(382, 28);
            cboFkVehicle.DropDownStyle = ComboBoxStyle.DropDownList;
            pnlDetail.Controls.Add(cboFkVehicle);

            clbVehicles.Location = new Point(18, y + 54);
            clbVehicles.Size = new Size(382, 90);
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

        private Panel CreateStatCard(string title, string value, Color accentColor, int xPos)
        {
            Panel card = new Panel
            {
                Size = new Size(290, 95),
                Location = new Point(xPos, 0),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Padding = new Padding(15, 12, 15, 12)
            };

            // Accent bar bên trái
            Panel accentBar = new Panel
            {
                Size = new Size(4, 95),
                Location = new Point(0, 0),
                BackColor = accentColor
            };
            card.Controls.Add(accentBar);

            // Title
            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(20, 15),
                AutoSize = true
            };
            card.Controls.Add(lblTitle);

            // Value
            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = accentColor,
                Location = new Point(20, 40),
                AutoSize = true,
                Tag = "statValue" // Tag để update dynamic
            };
            card.Controls.Add(lblValue);

            // Shadow effect (optional - simulated with border)
            card.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(226, 232, 240), 2))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                }
            };

            return card;
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

            cboFkStation.SelectedIndexChanged += (s, e) => ApplyFkStationSelection();
            cboFkVehicle.SelectedIndexChanged += (s, e) => ApplyFkVehicleSelection();
        }

        private void ApplySecurity()
        {
            AuthorizationGuard.ApplyControlSecurity(SystemMenus.Route, btnAdd, btnEdit, btnDelete, btnExport);
        }

        private async Task LoadInitialDataAsync()
        {
            try
            {
                var stationsDto = await _stationService.GetStationListAsync();
                _allStations = stationsDto.Select(dto => new StationSummaryDto
                {
                    Id          = dto.Id,
                    StationCode = dto.StationCode,
                    StationName = dto.StationName,
                    City        = dto.City,
                    Status      = dto.Status
                }).ToList();

                var vehiclesDto = await _vehicleService.GetVehicleListAsync();
                _allVehicles = vehiclesDto.Select(dto => new VehicleSummaryDto
                {
                    Id           = dto.Id,
                    LicensePlate = dto.LicensePlate,
                    VehicleType  = dto.VehicleType,
                    TotalSeats   = dto.TotalSeats,
                    Status       = dto.Status
                }).ToList();

                clbStations.Items.Clear();
                foreach (var st in _allStations)
                    clbStations.Items.Add($"{st.StationCode} - {st.StationName} ({st.City})");

                clbVehicles.Items.Clear();
                foreach (var v in _allVehicles)
                    clbVehicles.Items.Add($"{v.LicensePlate} ({v.VehicleType})");

                BindForeignKeyComboBoxes();
                await ReloadRoutesListAsync();
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show($"Lỗi truy cập database: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu tuyến xe: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ReloadRoutesListAsync(string? keyword = null)
        {
            int? currentId = _selectedRouteId;
            var routes = await _routeService.GetRouteListAsync(keyword);

            dgvRoutes.Rows.Clear();
            foreach (var r in routes)
            {
                string statusText = r.Status == "Active" ? "Đang hoạt động" : "Tạm ngừng";
                string updatedInfo = r.CreatedBy ?? "admin";

                int rowIndex = dgvRoutes.Rows.Add(
                    r.Id,
                    r.RouteCode,
                    r.RouteName,
                    $"{r.DistanceKm:N1}",
                    $"{r.EstimatedHours:N1}",
                    $"{r.BasePrice:N0}",
                    r.StationCount,
                    r.VehicleCount,
                    statusText,
                    updatedInfo
                );

                if (currentId.HasValue && r.Id == currentId.Value)
                    dgvRoutes.Rows[rowIndex].Selected = true;
            }

            if (dgvRoutes.SelectedRows.Count == 0 && dgvRoutes.Rows.Count > 0)
                dgvRoutes.Rows[0].Selected = true;
        }

        private async void dgvRoutes_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isAddingNew || dgvRoutes.SelectedRows.Count == 0)
                return;

            var row = dgvRoutes.SelectedRows[0];
            int routeId = Convert.ToInt32(row.Cells["colId"].Value);
            _selectedRouteId = routeId;

            try
            {
                var route = await _routeService.GetRouteDetailAsync(routeId);
                DisplayRouteDetails(route);
            }
            catch (EntityNotFoundException)
            {
                // Row in grid but deleted from DB — ignore
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chi tiết tuyến xe: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayRouteDetails(RouteDetailDto route)
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
                clbStations.SetItemChecked(i, routeStationIds.Contains(_allStations[i].Id));

            // Check multi-table Vehicles
            var routeVehicleIds = route.Vehicles.Select(v => v.Id).ToHashSet();
            for (int i = 0; i < _allVehicles.Count; i++)
                clbVehicles.SetItemChecked(i, routeVehicleIds.Contains(_allVehicles[i].Id));
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
            cboFkStation.Enabled = editable;
            cboFkVehicle.Enabled = editable;

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

            var selectedStationIds = new List<int>();
            for (int i = 0; i < clbStations.Items.Count; i++)
                if (clbStations.GetItemChecked(i) && i < _allStations.Count)
                    selectedStationIds.Add(_allStations[i].Id);

            var selectedVehicleIds = new List<int>();
            for (int i = 0; i < clbVehicles.Items.Count; i++)
                if (clbVehicles.GetItemChecked(i) && i < _allVehicles.Count)
                    selectedVehicleIds.Add(_allVehicles[i].Id);

            var dto = new RouteFormDto
            {
                Id                 = _selectedRouteId ?? 0,
                RouteCode          = code,
                RouteName          = name,
                DistanceKm         = numDistance.Value,
                EstimatedHours     = numEstimatedHours.Value,
                BasePrice          = numBasePrice.Value,
                Status             = cboStatus.SelectedItem?.ToString() ?? "Active",
                SelectedStationIds = selectedStationIds,
                AssignedVehicleIds = selectedVehicleIds
            };

            try
            {
                if (_isAddingNew)
                {
                    var created = await _routeService.CreateRouteAsync(dto);
                    _selectedRouteId = created.Id;
                    MessageBox.Show("Thêm tuyến xe thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetFormEditable(false);
                    _isAddingNew = false;
                    await ReloadRoutesListAsync();
                }
                else if (_selectedRouteId.HasValue)
                {
                    await _routeService.UpdateRouteAsync(dto);
                    MessageBox.Show("Cập nhật tuyến xe thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetFormEditable(false);
                    await ReloadRoutesListAsync();
                }
            }
            catch (QuanLyVanTai.BLL.Exceptions.ValidationException ex)
            {
                MessageBox.Show($"Dữ liệu không hợp lệ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (DuplicateEntityException ex)
            {
                MessageBox.Show($"Trùng lặp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (EntityNotFoundException ex)
            {
                MessageBox.Show($"Không tìm thấy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (BusinessRuleViolationException ex)
            {
                MessageBox.Show($"Vi phạm quy tắc: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show($"Lỗi database: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            using var confirmDlg = new FrmConfirmDelete(
                $"{code} ({name})",
                $"Đang có {clbStations.CheckedItems.Count} trạm dừng và {clbVehicles.CheckedItems.Count} xe được liên kết."
            );
            if (confirmDlg.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                await _routeService.DeleteRouteAsync(_selectedRouteId.Value, confirmDlg.IsSoftDelete);
                MessageBox.Show("Xóa tuyến xe thành công!", "Kết quả xóa an toàn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _selectedRouteId = null;
                await ReloadRoutesListAsync();
            }
            catch (EntityNotFoundException ex)
            {
                MessageBox.Show($"Không tìm thấy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (BusinessRuleViolationException ex)
            {
                MessageBox.Show($"Vi phạm quy tắc: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show($"Lỗi database: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TODO: GetRouteAuditHistoryAsync chưa được implement trong RouteManagementService.
        // Uncomment và implement sau khi ManagementService hỗ trợ audit history.
        private async Task ViewAuditHistoryAsync()
        {
            await Task.CompletedTask;
            MessageBox.Show("Tính năng xem lịch sử đang được phát triển.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BindForeignKeyComboBoxes()
        {
            _suppressFkComboEvents = true;
            try
            {
                var stationItems = new List<FkLookupItem>
                {
                    new() { Id = 0, Display = "-- Chọn trạm dừng (khóa ngoại) --" }
                };
                stationItems.AddRange(_allStations.Select(st => new FkLookupItem
                {
                    Id = st.Id,
                    Display = $"{st.StationCode} - {st.StationName} ({st.City})"
                }));
                cboFkStation.DisplayMember = nameof(FkLookupItem.Display);
                cboFkStation.ValueMember = nameof(FkLookupItem.Id);
                cboFkStation.DataSource = stationItems;

                var vehicleItems = new List<FkLookupItem>
                {
                    new() { Id = 0, Display = "-- Chọn phương tiện (khóa ngoại) --" }
                };
                vehicleItems.AddRange(_allVehicles.Select(v => new FkLookupItem
                {
                    Id = v.Id,
                    Display = $"{v.LicensePlate} ({v.VehicleType})"
                }));
                cboFkVehicle.DisplayMember = nameof(FkLookupItem.Display);
                cboFkVehicle.ValueMember = nameof(FkLookupItem.Id);
                cboFkVehicle.DataSource = vehicleItems;
            }
            finally
            {
                _suppressFkComboEvents = false;
            }
        }

        private void ApplyFkStationSelection()
        {
            if (_suppressFkComboEvents || cboFkStation.SelectedItem is not FkLookupItem item || item.Id <= 0)
                return;

            int idx = _allStations.FindIndex(st => st.Id == item.Id);
            if (idx >= 0 && idx < clbStations.Items.Count)
                clbStations.SetItemChecked(idx, true);
        }

        private void ApplyFkVehicleSelection()
        {
            if (_suppressFkComboEvents || cboFkVehicle.SelectedItem is not FkLookupItem item || item.Id <= 0)
                return;

            int idx = _allVehicles.FindIndex(v => v.Id == item.Id);
            if (idx >= 0 && idx < clbVehicles.Items.Count)
                clbVehicles.SetItemChecked(idx, true);
        }
    }
}
