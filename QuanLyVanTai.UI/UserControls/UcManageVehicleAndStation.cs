using Core.Configs;
using Core.Helpers;
using Core.Security;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.UI.Forms;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcManageVehicleAndStation : UserControl
    {
        private readonly VehicleService _vehicleService = new();
        private readonly StationService _stationService = new();
        private readonly RouteService _routeService = new();

        private readonly TabControl tabMain = new();

        // ==========================================
        // TAB 1: PHƯƠNG TIỆN CONTROLS
        // ==========================================
        private readonly TabPage tabVehicles = new();
        private readonly Panel pnlVehiclesFilter = new();
        private readonly TextBox txtVehKeyword = new();
        private readonly ComboBox cboVehType = new();
        private readonly ComboBox cboVehStatus = new();
        private readonly ComboBox cboVehManufacturer = new();
        private readonly RadioButton radVehAnd = new();
        private readonly RadioButton radVehOr = new();
        private readonly IconButton btnVehReset = new();
        private readonly IconButton btnVehExport = new();

        private readonly SplitContainer splitVehicles = new();
        private readonly DataGridView dgvVehicles = new();

        private readonly TextBox txtVehLicense = new();
        private readonly ComboBox cboVehEditType = new();
        private readonly NumericUpDown numVehSeats = new();
        private readonly TextBox txtVehManufacturer = new();
        private readonly ComboBox cboVehEditStatus = new();
        private readonly ComboBox cboVehRoute = new();

        private readonly IconButton btnVehAdd = new();
        private readonly IconButton btnVehEdit = new();
        private readonly IconButton btnVehDelete = new();
        private readonly IconButton btnVehSave = new();
        private readonly IconButton btnVehCancel = new();

        private bool _isVehAdding = false;
        private int? _selectedVehId = null;
        private List<Route> _availableRoutes = [];

        // ==========================================
        // TAB 2: TRẠM DỪNG CONTROLS
        // ==========================================
        private readonly TabPage tabStations = new();
        private readonly Panel pnlStationsFilter = new();
        private readonly TextBox txtStKeyword = new();
        private readonly ComboBox cboStCity = new();
        private readonly ComboBox cboStStatus = new();
        private readonly RadioButton radStAnd = new();
        private readonly RadioButton radStOr = new();
        private readonly IconButton btnStReset = new();
        private readonly IconButton btnStExport = new();

        private readonly SplitContainer splitStations = new();
        private readonly DataGridView dgvStations = new();

        private readonly TextBox txtStCode = new();
        private readonly TextBox txtStName = new();
        private readonly TextBox txtStAddress = new();
        private readonly ComboBox cboStEditCity = new();
        private readonly ComboBox cboStEditStatus = new();

        private readonly IconButton btnStAdd = new();
        private readonly IconButton btnStEdit = new();
        private readonly IconButton btnStDelete = new();
        private readonly IconButton btnStSave = new();
        private readonly IconButton btnStCancel = new();

        private bool _isStAdding = false;
        private int? _selectedStId = null;

        public UcManageVehicleAndStation()
        {
            InitializeComponent();
            BuildUI();
            WireEvents();
            ApplySecurity();
            _ = LoadInitialDataAsync();
        }

        private void BuildUI()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = ThemeConfig.BackgroundColor;

            tabMain.Dock = DockStyle.Fill;
            tabMain.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            tabMain.Padding = new Point(20, 10);

            // Tab 1
            tabVehicles.Text = "🚌 Quản Lý Phương Tiện";
            tabVehicles.BackColor = ThemeConfig.BackgroundColor;
            BuildVehiclesTab();
            tabMain.TabPages.Add(tabVehicles);

            // Tab 2
            tabStations.Text = "📍 Quản Lý Trạm Dừng";
            tabStations.BackColor = ThemeConfig.BackgroundColor;
            BuildStationsTab();
            tabMain.TabPages.Add(tabStations);

            this.Controls.Add(tabMain);
        }

        // ==========================================
        // BUILD TAB 1: PHƯƠNG TIỆN
        // ==========================================
        private void BuildVehiclesTab()
        {
            // Filter Panel Top
            pnlVehiclesFilter.Dock = DockStyle.Top;
            pnlVehiclesFilter.Height = 85;
            pnlVehiclesFilter.BackColor = Color.White;
            pnlVehiclesFilter.Padding = new Padding(15, 10, 15, 10);

            // Search Keyword
            txtVehKeyword.Location = new Point(15, 12);
            txtVehKeyword.Size = new Size(180, 32);
            txtVehKeyword.PlaceholderText = "🔍 Tìm biển số, loại xe...";

            // Filter Type
            cboVehType.Location = new Point(205, 12);
            cboVehType.Size = new Size(140, 32);
            cboVehType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboVehType.Items.AddRange(["Tất cả", "Ghế ngồi cao cấp", "Limousine VIP", "Giường nằm 40 chỗ", "Giường nằm VIP 32 phòng", "Ghế ngồi"]);
            cboVehType.SelectedIndex = 0;

            // Filter Status
            cboVehStatus.Location = new Point(355, 12);
            cboVehStatus.Size = new Size(130, 32);
            cboVehStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboVehStatus.Items.AddRange(["Tất cả", "Ready", "InTransit", "Maintenance"]);
            cboVehStatus.SelectedIndex = 0;

            // Filter Manufacturer
            cboVehManufacturer.Location = new Point(495, 12);
            cboVehManufacturer.Size = new Size(140, 32);
            cboVehManufacturer.DropDownStyle = ComboBoxStyle.DropDownList;
            cboVehManufacturer.Items.AddRange(["Tất cả", "Hyundai Universe", "Ford Transit DCar", "Thaco Mobihome", "Tracomeco", "Samco"]);
            cboVehManufacturer.SelectedIndex = 0;

            // Logic Radio buttons
            radVehAnd.Text = "AND (Khớp tất cả)";
            radVehAnd.Checked = true;
            radVehAnd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radVehAnd.Location = new Point(15, 48);
            radVehAnd.Size = new Size(140, 25);

            radVehOr.Text = "OR (Khớp bất kỳ)";
            radVehOr.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radVehOr.Location = new Point(160, 48);
            radVehOr.Size = new Size(140, 25);

            // Reset Filter
            ThemeConfig.StyleSecondaryButton(btnVehReset, IconChar.RotateLeft);
            btnVehReset.Text = " Đặt lại";
            btnVehReset.Size = new Size(100, 34);
            btnVehReset.Location = new Point(310, 44);

            // Export Button
            ThemeConfig.StyleSecondaryButton(btnVehExport, IconChar.FileExcel);
            btnVehExport.Text = " Xuất file";
            btnVehExport.BackColor = Color.FromArgb(16, 185, 129);
            btnVehExport.Size = new Size(110, 34);
            btnVehExport.Location = new Point(420, 44);

            pnlVehiclesFilter.Controls.AddRange([txtVehKeyword, cboVehType, cboVehStatus, cboVehManufacturer, radVehAnd, radVehOr, btnVehReset, btnVehExport]);
            tabVehicles.Controls.Add(pnlVehiclesFilter);

            // Split Container
            splitVehicles.Dock = DockStyle.Fill;
            splitVehicles.SplitterDistance = 670;
            splitVehicles.BackColor = Color.FromArgb(226, 232, 240);

            // DataGridView Setup
            dgvVehicles.Dock = DockStyle.Fill;
            dgvVehicles.BackgroundColor = Color.White;
            dgvVehicles.BorderStyle = BorderStyle.None;
            dgvVehicles.AllowUserToAddRows = false;
            dgvVehicles.AllowUserToDeleteRows = false;
            dgvVehicles.ReadOnly = true;
            dgvVehicles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVehicles.MultiSelect = false;
            dgvVehicles.RowHeadersVisible = false;
            dgvVehicles.RowTemplate.Height = 42;
            dgvVehicles.ColumnHeadersHeight = 42;
            dgvVehicles.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvVehicles.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgvVehicles.ColumnHeadersDefaultCellStyle.ForeColor = ThemeConfig.TextMain;
            dgvVehicles.EnableHeadersVisualStyles = false;
            dgvVehicles.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            dgvVehicles.Columns.Add(new DataGridViewTextBoxColumn { Name = "colVehId", HeaderText = "ID", Visible = false });
            dgvVehicles.Columns.Add(new DataGridViewTextBoxColumn { Name = "colVehPlate", HeaderText = "Biển Số Xe", Width = 120, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvVehicles.Columns.Add(new DataGridViewTextBoxColumn { Name = "colVehType", HeaderText = "Loại Phương Tiện", Width = 170 });
            dgvVehicles.Columns.Add(new DataGridViewTextBoxColumn { Name = "colVehSeats", HeaderText = "Số Ghế", Width = 85, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvVehicles.Columns.Add(new DataGridViewTextBoxColumn { Name = "colVehMfr", HeaderText = "Hãng Sản Xuất", Width = 150 });
            dgvVehicles.Columns.Add(new DataGridViewTextBoxColumn { Name = "colVehRoute", HeaderText = "Tuyến Phụ Trách", Width = 180 });
            dgvVehicles.Columns.Add(new DataGridViewTextBoxColumn { Name = "colVehStatus", HeaderText = "Trạng Thái", Width = 120, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            // Highlight Search
            DataGridViewHighlightHelper.AttachSearchHighlighter(dgvVehicles, () => txtVehKeyword.Text);
            splitVehicles.Panel1.Controls.Add(dgvVehicles);

            // Detail Edit Panel
            Panel pnlVehDetail = new()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(18),
                AutoScroll = true
            };

            Label lblVehTitle = new Label
            {
                Text = "CHI TIẾT PHƯƠNG TIỆN",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = ThemeConfig.PrimaryColor,
                Location = new Point(18, 15),
                AutoSize = true
            };
            pnlVehDetail.Controls.Add(lblVehTitle);

            int y = 50;
            pnlVehDetail.Controls.Add(new Label { Text = "Biển Kiểm Soát *:", Location = new Point(18, y), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            txtVehLicense.Location = new Point(18, y + 22);
            txtVehLicense.Size = new Size(330, 30);
            pnlVehDetail.Controls.Add(txtVehLicense);

            y += 60;
            pnlVehDetail.Controls.Add(new Label { Text = "Loại Xe:", Location = new Point(18, y), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            cboVehEditType.Location = new Point(18, y + 22);
            cboVehEditType.Size = new Size(330, 30);
            cboVehEditType.Items.AddRange(["Ghế ngồi cao cấp", "Limousine VIP", "Giường nằm 40 chỗ", "Giường nằm VIP 32 phòng", "Ghế ngồi"]);
            cboVehEditType.SelectedIndex = 0;
            pnlVehDetail.Controls.Add(cboVehEditType);

            y += 60;
            pnlVehDetail.Controls.Add(new Label { Text = "Tổng Số Chỗ Ghế/Giường:", Location = new Point(18, y), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            numVehSeats.Location = new Point(18, y + 22);
            numVehSeats.Size = new Size(150, 30);
            numVehSeats.Maximum = 80;
            numVehSeats.Value = 29;
            pnlVehDetail.Controls.Add(numVehSeats);

            pnlVehDetail.Controls.Add(new Label { Text = "Trạng Thái:", Location = new Point(180, y), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            cboVehEditStatus.Location = new Point(180, y + 22);
            cboVehEditStatus.Size = new Size(168, 30);
            cboVehEditStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboVehEditStatus.Items.AddRange(["Ready", "InTransit", "Maintenance"]);
            cboVehEditStatus.SelectedIndex = 0;
            pnlVehDetail.Controls.Add(cboVehEditStatus);

            y += 60;
            pnlVehDetail.Controls.Add(new Label { Text = "Hãng Sản Xuất:", Location = new Point(18, y), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            txtVehManufacturer.Location = new Point(18, y + 22);
            txtVehManufacturer.Size = new Size(330, 30);
            pnlVehDetail.Controls.Add(txtVehManufacturer);

            y += 60;
            pnlVehDetail.Controls.Add(new Label { Text = "Phân Công Tuyến Xe:", Location = new Point(18, y), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            cboVehRoute.Location = new Point(18, y + 22);
            cboVehRoute.Size = new Size(330, 30);
            cboVehRoute.DropDownStyle = ComboBoxStyle.DropDownList;
            pnlVehDetail.Controls.Add(cboVehRoute);

            // Action Buttons
            y += 75;
            ThemeConfig.StyleSuccessButton(btnVehAdd, IconChar.Plus);
            btnVehAdd.Text = " Thêm";
            btnVehAdd.Size = new Size(80, 36);
            btnVehAdd.Location = new Point(18, y);

            ThemeConfig.StylePrimaryButton(btnVehEdit, IconChar.Pen);
            btnVehEdit.Text = " Sửa";
            btnVehEdit.Size = new Size(75, 36);
            btnVehEdit.Location = new Point(105, y);

            ThemeConfig.StyleDangerButton(btnVehDelete, IconChar.Trash);
            btnVehDelete.Text = " Xóa";
            btnVehDelete.Size = new Size(75, 36);
            btnVehDelete.Location = new Point(188, y);

            ThemeConfig.StylePrimaryButton(btnVehSave, IconChar.FloppyDisk);
            btnVehSave.Text = " Lưu";
            btnVehSave.Size = new Size(75, 36);
            btnVehSave.Location = new Point(270, y);
            btnVehSave.Enabled = false;

            pnlVehDetail.Controls.AddRange([btnVehAdd, btnVehEdit, btnVehDelete, btnVehSave]);
            splitVehicles.Panel2.Controls.Add(pnlVehDetail);

            tabVehicles.Controls.Add(splitVehicles);
        }

        // ==========================================
        // BUILD TAB 2: TRẠM DỪNG
        // ==========================================
        private void BuildStationsTab()
        {
            // Filter Panel Top
            pnlStationsFilter.Dock = DockStyle.Top;
            pnlStationsFilter.Height = 85;
            pnlStationsFilter.BackColor = Color.White;
            pnlStationsFilter.Padding = new Padding(15, 10, 15, 10);

            // Search Keyword
            txtStKeyword.Location = new Point(15, 12);
            txtStKeyword.Size = new Size(220, 32);
            txtStKeyword.PlaceholderText = "🔍 Tìm mã trạm, tên bến xe...";

            // Filter City
            cboStCity.Location = new Point(245, 12);
            cboStCity.Size = new Size(160, 32);
            cboStCity.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStCity.Items.AddRange(["Tất cả", "Hà Nội", "Hải Phòng", "Đà Nẵng", "TP. Hồ Chí Minh", "Nghệ An"]);
            cboStCity.SelectedIndex = 0;

            // Filter Status
            cboStStatus.Location = new Point(415, 12);
            cboStStatus.Size = new Size(130, 32);
            cboStStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStStatus.Items.AddRange(["Tất cả", "Active", "Inactive"]);
            cboStStatus.SelectedIndex = 0;

            // Logic Radio buttons
            radStAnd.Text = "AND (Khớp tất cả)";
            radStAnd.Checked = true;
            radStAnd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radStAnd.Location = new Point(15, 48);
            radStAnd.Size = new Size(140, 25);

            radStOr.Text = "OR (Khớp bất kỳ)";
            radStOr.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radStOr.Location = new Point(160, 48);
            radStOr.Size = new Size(140, 25);

            ThemeConfig.StyleSecondaryButton(btnStReset, IconChar.RotateLeft);
            btnStReset.Text = " Đặt lại";
            btnStReset.Size = new Size(100, 34);
            btnStReset.Location = new Point(310, 44);

            ThemeConfig.StyleSecondaryButton(btnStExport, IconChar.FileExcel);
            btnStExport.Text = " Xuất file";
            btnStExport.BackColor = Color.FromArgb(16, 185, 129);
            btnStExport.Size = new Size(110, 34);
            btnStExport.Location = new Point(420, 44);

            pnlStationsFilter.Controls.AddRange([txtStKeyword, cboStCity, cboStStatus, radStAnd, radStOr, btnStReset, btnStExport]);
            tabStations.Controls.Add(pnlStationsFilter);

            // Split Container
            splitStations.Dock = DockStyle.Fill;
            splitStations.SplitterDistance = 670;
            splitStations.BackColor = Color.FromArgb(226, 232, 240);

            // DataGridView Setup
            dgvStations.Dock = DockStyle.Fill;
            dgvStations.BackgroundColor = Color.White;
            dgvStations.BorderStyle = BorderStyle.None;
            dgvStations.AllowUserToAddRows = false;
            dgvStations.AllowUserToDeleteRows = false;
            dgvStations.ReadOnly = true;
            dgvStations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStations.MultiSelect = false;
            dgvStations.RowHeadersVisible = false;
            dgvStations.RowTemplate.Height = 42;
            dgvStations.ColumnHeadersHeight = 42;
            dgvStations.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvStations.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgvStations.ColumnHeadersDefaultCellStyle.ForeColor = ThemeConfig.TextMain;
            dgvStations.EnableHeadersVisualStyles = false;
            dgvStations.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            dgvStations.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStId", HeaderText = "ID", Visible = false });
            dgvStations.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStCode", HeaderText = "Mã Trạm", Width = 130, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvStations.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStName", HeaderText = "Tên Bến Xe / Trạm", Width = 200 });
            dgvStations.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStAddress", HeaderText = "Địa Chỉ", Width = 230 });
            dgvStations.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStCity", HeaderText = "Tỉnh / TP", Width = 130 });
            dgvStations.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStRoutes", HeaderText = "Số Tuyến", Width = 85, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvStations.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStStatus", HeaderText = "Trạng Thái", Width = 110, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            // Highlight Search
            DataGridViewHighlightHelper.AttachSearchHighlighter(dgvStations, () => txtStKeyword.Text);
            splitStations.Panel1.Controls.Add(dgvStations);

            // Detail Edit Panel
            Panel pnlStDetail = new()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(18),
                AutoScroll = true
            };

            Label lblStTitle = new Label
            {
                Text = "CHI TIẾT TRẠM DỪNG",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = ThemeConfig.PrimaryColor,
                Location = new Point(18, 15),
                AutoSize = true
            };
            pnlStDetail.Controls.Add(lblStTitle);

            int y2 = 50;
            pnlStDetail.Controls.Add(new Label { Text = "Mã Trạm Dừng *:", Location = new Point(18, y2), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            txtStCode.Location = new Point(18, y2 + 22);
            txtStCode.Size = new Size(330, 30);
            pnlStDetail.Controls.Add(txtStCode);

            y2 += 60;
            pnlStDetail.Controls.Add(new Label { Text = "Tên Bến Xe / Trạm *:", Location = new Point(18, y2), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            txtStName.Location = new Point(18, y2 + 22);
            txtStName.Size = new Size(330, 30);
            pnlStDetail.Controls.Add(txtStName);

            y2 += 60;
            pnlStDetail.Controls.Add(new Label { Text = "Địa Chỉ Chi Tiết:", Location = new Point(18, y2), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            txtStAddress.Location = new Point(18, y2 + 22);
            txtStAddress.Size = new Size(330, 30);
            pnlStDetail.Controls.Add(txtStAddress);

            y2 += 60;
            pnlStDetail.Controls.Add(new Label { Text = "Tỉnh / Thành Phố:", Location = new Point(18, y2), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            cboStEditCity.Location = new Point(18, y2 + 22);
            cboStEditCity.Size = new Size(160, 30);
            cboStEditCity.Items.AddRange(["Hà Nội", "Hải Phòng", "Đà Nẵng", "TP. Hồ Chí Minh", "Nghệ An", "Huế", "Quảng Ninh", "Cần Thơ"]);
            cboStEditCity.SelectedIndex = 0;
            pnlStDetail.Controls.Add(cboStEditCity);

            pnlStDetail.Controls.Add(new Label { Text = "Trạng Thái:", Location = new Point(185, y2), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
            cboStEditStatus.Location = new Point(185, y2 + 22);
            cboStEditStatus.Size = new Size(163, 30);
            cboStEditStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStEditStatus.Items.AddRange(["Active", "Inactive"]);
            cboStEditStatus.SelectedIndex = 0;
            pnlStDetail.Controls.Add(cboStEditStatus);

            // Action Buttons
            y2 += 75;
            ThemeConfig.StyleSuccessButton(btnStAdd, IconChar.Plus);
            btnStAdd.Text = " Thêm";
            btnStAdd.Size = new Size(80, 36);
            btnStAdd.Location = new Point(18, y2);

            ThemeConfig.StylePrimaryButton(btnStEdit, IconChar.Pen);
            btnStEdit.Text = " Sửa";
            btnStEdit.Size = new Size(75, 36);
            btnStEdit.Location = new Point(105, y2);

            ThemeConfig.StyleDangerButton(btnStDelete, IconChar.Trash);
            btnStDelete.Text = " Xóa";
            btnStDelete.Size = new Size(75, 36);
            btnStDelete.Location = new Point(188, y2);

            ThemeConfig.StylePrimaryButton(btnStSave, IconChar.FloppyDisk);
            btnStSave.Text = " Lưu";
            btnStSave.Size = new Size(75, 36);
            btnStSave.Location = new Point(270, y2);
            btnStSave.Enabled = false;

            pnlStDetail.Controls.AddRange([btnStAdd, btnStEdit, btnStDelete, btnStSave]);
            splitStations.Panel2.Controls.Add(pnlStDetail);

            tabStations.Controls.Add(splitStations);
        }

        private void WireEvents()
        {
            // Vehicles Events
            EventHandler triggerVehFilter = async (s, e) => await FilterVehiclesAsync();
            txtVehKeyword.TextChanged += triggerVehFilter;
            cboVehType.SelectedIndexChanged += triggerVehFilter;
            cboVehStatus.SelectedIndexChanged += triggerVehFilter;
            cboVehManufacturer.SelectedIndexChanged += triggerVehFilter;
            radVehAnd.CheckedChanged += triggerVehFilter;
            radVehOr.CheckedChanged += triggerVehFilter;

            btnVehReset.Click += (s, e) =>
            {
                txtVehKeyword.Clear();
                cboVehType.SelectedIndex = 0;
                cboVehStatus.SelectedIndex = 0;
                cboVehManufacturer.SelectedIndex = 0;
                radVehAnd.Checked = true;
            };

            btnVehExport.Click += (s, e) => DataExportHelper.ExportDataGridViewWithDialog(dgvVehicles, "DanhSach_PhuongTien");

            dgvVehicles.SelectionChanged += dgvVehicles_SelectionChanged;
            btnVehAdd.Click += (s, e) => StartVehAdd();
            btnVehEdit.Click += (s, e) => StartVehEdit();
            btnVehSave.Click += async (s, e) => await SaveVehicleAsync();
            btnVehDelete.Click += async (s, e) => await DeleteVehicleAsync();

            // Stations Events
            EventHandler triggerStFilter = async (s, e) => await FilterStationsAsync();
            txtStKeyword.TextChanged += triggerStFilter;
            cboStCity.SelectedIndexChanged += triggerStFilter;
            cboStStatus.SelectedIndexChanged += triggerStFilter;
            radStAnd.CheckedChanged += triggerStFilter;
            radStOr.CheckedChanged += triggerStFilter;

            btnStReset.Click += (s, e) =>
            {
                txtStKeyword.Clear();
                cboStCity.SelectedIndex = 0;
                cboStStatus.SelectedIndex = 0;
                radStAnd.Checked = true;
            };

            btnStExport.Click += (s, e) => DataExportHelper.ExportDataGridViewWithDialog(dgvStations, "DanhSach_TramDung");

            dgvStations.SelectionChanged += dgvStations_SelectionChanged;
            btnStAdd.Click += (s, e) => StartStAdd();
            btnStEdit.Click += (s, e) => StartStEdit();
            btnStSave.Click += async (s, e) => await SaveStationAsync();
            btnStDelete.Click += async (s, e) => await DeleteStationAsync();
        }

        private void ApplySecurity()
        {
            AuthorizationGuard.ApplyControlSecurity(SystemMenus.Vehicle, btnVehAdd, btnVehEdit, btnVehDelete, btnVehExport);
            AuthorizationGuard.ApplyControlSecurity(SystemMenus.Station, btnStAdd, btnStEdit, btnStDelete, btnStExport);
        }

        private async Task LoadInitialDataAsync()
        {
            try
            {
                _availableRoutes = await _routeService.GetAllRoutesAsync();
                cboVehRoute.Items.Clear();
                cboVehRoute.Items.Add("-- Chưa phân công --");
                foreach (var r in _availableRoutes)
                {
                    cboVehRoute.Items.Add($"{r.RouteCode} - {r.RouteName}");
                }
                cboVehRoute.SelectedIndex = 0;

                await FilterVehiclesAsync();
                await FilterStationsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu phương tiện/trạm dừng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // LOGIC PHƯƠNG TIỆN
        // ==========================================
        private async Task FilterVehiclesAsync()
        {
            var criteria = new VehicleFilterCriteria
            {
                Keyword = txtVehKeyword.Text,
                VehicleType = cboVehType.SelectedItem?.ToString(),
                Status = cboVehStatus.SelectedItem?.ToString(),
                Manufacturer = cboVehManufacturer.SelectedItem?.ToString(),
                UseAndLogic = radVehAnd.Checked
            };

            var vehicles = await _vehicleService.SearchAndFilterVehiclesAsync(criteria);
            dgvVehicles.Rows.Clear();

            foreach (var v in vehicles)
            {
                string routeName = v.Route != null ? $"{v.Route.RouteCode} ({v.Route.RouteName})" : "Chưa phân công";
                dgvVehicles.Rows.Add(
                    v.Id,
                    v.LicensePlate,
                    v.VehicleType,
                    v.TotalSeats,
                    v.Manufacturer ?? "Chưa rõ",
                    routeName,
                    v.Status
                );
            }
        }

        private void dgvVehicles_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isVehAdding || dgvVehicles.SelectedRows.Count == 0) return;

            var row = dgvVehicles.SelectedRows[0];
            _selectedVehId = Convert.ToInt32(row.Cells["colVehId"].Value);

            txtVehLicense.Text = row.Cells["colVehPlate"].Value?.ToString() ?? "";
            cboVehEditType.SelectedItem = row.Cells["colVehType"].Value?.ToString() ?? "";
            numVehSeats.Value = Convert.ToDecimal(row.Cells["colVehSeats"].Value ?? 29);
            txtVehManufacturer.Text = row.Cells["colVehMfr"].Value?.ToString() ?? "";
            cboVehEditStatus.SelectedItem = row.Cells["colVehStatus"].Value?.ToString() ?? "Ready";
        }

        private void StartVehAdd()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Vehicle, PermissionAction.Add)) return;

            _isVehAdding = true;
            _selectedVehId = null;
            txtVehLicense.Clear();
            txtVehManufacturer.Clear();
            cboVehRoute.SelectedIndex = 0;
            btnVehSave.Enabled = true;
            txtVehLicense.Focus();
        }

        private void StartVehEdit()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Vehicle, PermissionAction.Edit)) return;
            if (_selectedVehId == null) return;

            _isVehAdding = false;
            btnVehSave.Enabled = true;
            txtVehLicense.Focus();
        }

        private async Task SaveVehicleAsync()
        {
            string plate = txtVehLicense.Text.Trim();
            if (string.IsNullOrWhiteSpace(plate))
            {
                MessageBox.Show("Vui lòng nhập Biển kiểm soát xe!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? assignedRouteId = null;
            if (cboVehRoute.SelectedIndex > 0 && cboVehRoute.SelectedIndex - 1 < _availableRoutes.Count)
            {
                assignedRouteId = _availableRoutes[cboVehRoute.SelectedIndex - 1].Id;
            }

            if (_isVehAdding)
            {
                var vehicle = new Vehicle
                {
                    LicensePlate = plate,
                    VehicleType = cboVehEditType.SelectedItem?.ToString() ?? "Ghế ngồi",
                    TotalSeats = (int)numVehSeats.Value,
                    Manufacturer = txtVehManufacturer.Text.Trim(),
                    Status = cboVehEditStatus.SelectedItem?.ToString() ?? "Ready",
                    RouteId = assignedRouteId
                };

                var (success, msg, _) = await _vehicleService.CreateVehicleAsync(vehicle);
                if (success)
                {
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnVehSave.Enabled = false;
                    _isVehAdding = false;
                    await FilterVehiclesAsync();
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (_selectedVehId.HasValue)
            {
                var vehicle = new Vehicle
                {
                    Id = _selectedVehId.Value,
                    LicensePlate = plate,
                    VehicleType = cboVehEditType.SelectedItem?.ToString() ?? "Ghế ngồi",
                    TotalSeats = (int)numVehSeats.Value,
                    Manufacturer = txtVehManufacturer.Text.Trim(),
                    Status = cboVehEditStatus.SelectedItem?.ToString() ?? "Ready",
                    RouteId = assignedRouteId
                };

                var (success, msg) = await _vehicleService.UpdateVehicleAsync(vehicle);
                if (success)
                {
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnVehSave.Enabled = false;
                    await FilterVehiclesAsync();
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task DeleteVehicleAsync()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Vehicle, PermissionAction.Delete)) return;
            if (_selectedVehId == null) return;

            using var confirmDlg = new FrmConfirmDelete(txtVehLicense.Text, "Phương tiện vận tải");
            if (confirmDlg.ShowDialog() != DialogResult.OK) return;

            var (success, msg) = await _vehicleService.DeleteVehicleAsync(_selectedVehId.Value, confirmDlg.IsSoftDelete);
            if (success)
            {
                MessageBox.Show(msg, "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _selectedVehId = null;
                await FilterVehiclesAsync();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // LOGIC TRẠM DỪNG
        // ==========================================
        private async Task FilterStationsAsync()
        {
            var criteria = new StationFilterCriteria
            {
                Keyword = txtStKeyword.Text,
                City = cboStCity.SelectedItem?.ToString(),
                Status = cboStStatus.SelectedItem?.ToString(),
                UseAndLogic = radStAnd.Checked
            };

            var stations = await _stationService.SearchAndFilterStationsAsync(criteria);
            dgvStations.Rows.Clear();

            foreach (var s in stations)
            {
                dgvStations.Rows.Add(
                    s.Id,
                    s.StationCode,
                    s.StationName,
                    s.Address,
                    s.City,
                    s.Routes.Count,
                    s.Status
                );
            }
        }

        private void dgvStations_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isStAdding || dgvStations.SelectedRows.Count == 0) return;

            var row = dgvStations.SelectedRows[0];
            _selectedStId = Convert.ToInt32(row.Cells["colStId"].Value);

            txtStCode.Text = row.Cells["colStCode"].Value?.ToString() ?? "";
            txtStName.Text = row.Cells["colStName"].Value?.ToString() ?? "";
            txtStAddress.Text = row.Cells["colStAddress"].Value?.ToString() ?? "";
            cboStEditCity.SelectedItem = row.Cells["colStCity"].Value?.ToString() ?? "Hà Nội";
            cboStEditStatus.SelectedItem = row.Cells["colStStatus"].Value?.ToString() ?? "Active";
        }

        private void StartStAdd()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Station, PermissionAction.Add)) return;

            _isStAdding = true;
            _selectedStId = null;
            txtStCode.Clear();
            txtStName.Clear();
            txtStAddress.Clear();
            btnStSave.Enabled = true;
            txtStCode.Focus();
        }

        private void StartStEdit()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Station, PermissionAction.Edit)) return;
            if (_selectedStId == null) return;

            _isStAdding = false;
            btnStSave.Enabled = true;
            txtStCode.Focus();
        }

        private async Task SaveStationAsync()
        {
            string code = txtStCode.Text.Trim();
            string name = txtStName.Text.Trim();

            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng điền đủ Mã trạm và Tên bến xe!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_isStAdding)
            {
                var station = new Station
                {
                    StationCode = code,
                    StationName = name,
                    Address = txtStAddress.Text.Trim(),
                    City = cboStEditCity.SelectedItem?.ToString() ?? "Hà Nội",
                    Status = cboStEditStatus.SelectedItem?.ToString() ?? "Active"
                };

                var (success, msg, _) = await _stationService.CreateStationAsync(station);
                if (success)
                {
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnStSave.Enabled = false;
                    _isStAdding = false;
                    await FilterStationsAsync();
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (_selectedStId.HasValue)
            {
                var station = new Station
                {
                    Id = _selectedStId.Value,
                    StationCode = code,
                    StationName = name,
                    Address = txtStAddress.Text.Trim(),
                    City = cboStEditCity.SelectedItem?.ToString() ?? "Hà Nội",
                    Status = cboStEditStatus.SelectedItem?.ToString() ?? "Active"
                };

                var (success, msg) = await _stationService.UpdateStationAsync(station);
                if (success)
                {
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnStSave.Enabled = false;
                    await FilterStationsAsync();
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task DeleteStationAsync()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Station, PermissionAction.Delete)) return;
            if (_selectedStId == null) return;

            using var confirmDlg = new FrmConfirmDelete(txtStName.Text, "Trạm dừng bến xe");
            if (confirmDlg.ShowDialog() != DialogResult.OK) return;

            var (success, msg) = await _stationService.DeleteStationAsync(_selectedStId.Value, confirmDlg.IsSoftDelete);
            if (success)
            {
                MessageBox.Show(msg, "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _selectedStId = null;
                await FilterStationsAsync();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
