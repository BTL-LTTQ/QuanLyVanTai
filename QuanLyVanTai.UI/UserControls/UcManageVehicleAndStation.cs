using Core.Configs;
using Core.Helpers;
using Core.Security;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Exceptions;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.UI.Forms;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcManageVehicleAndStation : UserControl
    {
        private readonly VehicleManagementService _vehicleService = new();
        private readonly StationManagementService _stationService = new();
        private readonly RouteManagementService _routeService = new();

        private bool _isVehAdding = false;
        private int? _selectedVehId = null;
        private List<RouteListDto> _availableRoutes = [];

        private bool _isStAdding = false;
        private int? _selectedStId = null;

        private static readonly Dictionary<string, int> SupportedVehicleTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Limousine 9 chỗ", 9 },
            { "Giường nằm 24 chỗ", 24 },
            { "Ghế ngồi 28 chỗ", 28 },
            { "Giường nằm 34 chỗ", 34 },
            { "Ghế ngồi 45 chỗ", 45 }
        };

        public UcManageVehicleAndStation()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            SetupRuntimeUI();
            WireEvents();
            ApplySecurity();
            _ = LoadInitialDataAsync();
        }

        private void SetupRuntimeUI()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = ThemeConfig.BackgroundColor;

            // Highlight Search
            DataGridViewHighlightHelper.AttachSearchHighlighter(dgvVehicles, () => txtVehKeyword.Text);
            DataGridViewHighlightHelper.AttachSearchHighlighter(dgvStations, () => txtStKeyword.Text);

            // Style buttons
            ThemeConfig.StyleSecondaryButton(btnVehReset, IconChar.RotateLeft);
            ThemeConfig.StyleSecondaryButton(btnVehExport, IconChar.FileExcel);
            btnVehExport.BackColor = Color.FromArgb(16, 185, 129);

            ThemeConfig.StyleSuccessButton(btnVehAdd, IconChar.Plus);
            ThemeConfig.StylePrimaryButton(btnVehEdit, IconChar.Pen);
            ThemeConfig.StyleDangerButton(btnVehDelete, IconChar.Trash);
            ThemeConfig.StylePrimaryButton(btnVehSave, IconChar.FloppyDisk);
            ThemeConfig.StyleSecondaryButton(btnVehCancel, IconChar.Times);

            ThemeConfig.StyleSecondaryButton(btnStReset, IconChar.RotateLeft);
            ThemeConfig.StyleSecondaryButton(btnStExport, IconChar.FileExcel);
            btnStExport.BackColor = Color.FromArgb(16, 185, 129);

            ThemeConfig.StyleSuccessButton(btnStAdd, IconChar.Plus);
            ThemeConfig.StylePrimaryButton(btnStEdit, IconChar.Pen);
            ThemeConfig.StyleDangerButton(btnStDelete, IconChar.Trash);
            ThemeConfig.StylePrimaryButton(btnStSave, IconChar.FloppyDisk);
            ThemeConfig.StyleSecondaryButton(btnStCancel, IconChar.Times);
        }

        private bool _isVehRefreshing = false;
        private bool _isStRefreshing = false;
        private System.Windows.Forms.Timer? _vehDebounce;
        private System.Windows.Forms.Timer? _stDebounce;

        private void WireEvents()
        {
            // Vehicles: debounce để tránh concurrent DbContext
            txtVehKeyword.TextChanged += (s, e) => ScheduleVehRefresh();
            cboVehType.SelectedIndexChanged += (s, e) => ScheduleVehRefresh();
            cboVehStatus.SelectedIndexChanged += (s, e) => ScheduleVehRefresh();
            cboVehManufacturer.SelectedIndexChanged += (s, e) => ScheduleVehRefresh();
            radVehAnd.CheckedChanged += (s, e) => ScheduleVehRefresh();
            radVehOr.CheckedChanged += (s, e) => ScheduleVehRefresh();

            btnVehReset.Click += (s, e) =>
            {
                txtVehKeyword.Clear();
                cboVehType.SelectedIndex = 0;
                cboVehStatus.SelectedIndex = 0;
                cboVehManufacturer.SelectedIndex = 0;
                radVehAnd.Checked = true;
            };

            btnVehExport.Click += async (s, e) => await ExportVehiclesAsync();

            cboVehEditType.SelectedIndexChanged += (s, e) =>
            {
                var selected = cboVehEditType.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selected) && SupportedVehicleTypes.TryGetValue(selected, out int seats))
                {
                    numVehSeats.Value = seats;
                }
            };

            dgvVehicles.SelectionChanged += dgvVehicles_SelectionChanged;
            btnVehAdd.Click += (s, e) => StartVehAdd();
            btnVehEdit.Click += (s, e) => StartVehEdit();
            btnVehSave.Click += async (s, e) => await SaveVehicleAsync();
            btnVehDelete.Click += async (s, e) => await DeleteVehicleAsync();
            btnVehCancel.Click += (s, e) => CancelVehEdit();

            // Stations: debounce để tránh concurrent DbContext
            txtStKeyword.TextChanged += (s, e) => ScheduleStRefresh();
            cboStCity.SelectedIndexChanged += (s, e) => ScheduleStRefresh();
            cboStStatus.SelectedIndexChanged += (s, e) => ScheduleStRefresh();
            radStAnd.CheckedChanged += (s, e) => ScheduleStRefresh();
            radStOr.CheckedChanged += (s, e) => ScheduleStRefresh();

            btnStReset.Click += (s, e) =>
            {
                txtStKeyword.Clear();
                cboStCity.SelectedIndex = 0;
                cboStStatus.SelectedIndex = 0;
                radStAnd.Checked = true;
            };

            btnStExport.Click += async (s, e) => await ExportStationsAsync();

            dgvStations.SelectionChanged += dgvStations_SelectionChanged;
            btnStAdd.Click += (s, e) => StartStAdd();
            btnStEdit.Click += (s, e) => StartStEdit();
            btnStSave.Click += async (s, e) => await SaveStationAsync();
            btnStDelete.Click += async (s, e) => await DeleteStationAsync();
            btnStCancel.Click += (s, e) => CancelStEdit();
        }

        private void ScheduleVehRefresh()
        {
            _vehDebounce?.Stop();
            _vehDebounce?.Dispose();
            _vehDebounce = new System.Windows.Forms.Timer { Interval = 300 };
            _vehDebounce.Tick += async (s, e) =>
            {
                _vehDebounce?.Stop();
                _vehDebounce?.Dispose();
                _vehDebounce = null;
                await FilterVehiclesAsync();
            };
            _vehDebounce.Start();
        }

        private void ScheduleStRefresh()
        {
            _stDebounce?.Stop();
            _stDebounce?.Dispose();
            _stDebounce = new System.Windows.Forms.Timer { Interval = 300 };
            _stDebounce.Tick += async (s, e) =>
            {
                _stDebounce?.Stop();
                _stDebounce?.Dispose();
                _stDebounce = null;
                await FilterStationsAsync();
            };
            _stDebounce.Start();
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
                _availableRoutes = await _routeService.GetRouteListAsync();
                cboVehRoute.Items.Clear();
                cboVehRoute.Items.Add("-- Chưa phân công --");
                foreach (var r in _availableRoutes)
                    cboVehRoute.Items.Add($"{r.RouteCode} - {r.RouteName}");
                cboVehRoute.SelectedIndex = 0;

                await FilterVehiclesAsync();
                await FilterStationsAsync();
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show($"Lỗi truy cập database: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (_isVehRefreshing) return;
            _isVehRefreshing = true;
            try
            {
                var allVehicles = await _vehicleService.GetVehicleListAsync();

                // Client-side filter
                var keyword  = txtVehKeyword.Text.Trim().ToLower();
                var typeFilter   = cboVehType.SelectedItem?.ToString();
                var statusFilter = cboVehStatus.SelectedItem?.ToString();
                var mfrFilter    = cboVehManufacturer.SelectedItem?.ToString();
                bool useAnd  = radVehAnd.Checked;

                var filtered = allVehicles.Where(v =>
                {
                    bool matchKeyword = string.IsNullOrEmpty(keyword)
                        || v.LicensePlate.ToLower().Contains(keyword)
                        || (v.VehicleType?.ToLower().Contains(keyword) ?? false);
                    bool matchType   = string.IsNullOrEmpty(typeFilter) || typeFilter == "Tất cả" || v.VehicleType == typeFilter;
                    bool matchStatus = string.IsNullOrEmpty(statusFilter) || statusFilter == "Tất cả" || v.Status == statusFilter;
                    bool matchMfr    = string.IsNullOrEmpty(mfrFilter) || mfrFilter == "Tất cả"
                        || (v.Manufacturer?.Contains(mfrFilter, StringComparison.OrdinalIgnoreCase) ?? false);

                    return useAnd
                        ? matchKeyword && matchType && matchStatus && matchMfr
                        : matchKeyword || matchType || matchStatus || matchMfr;
                }).ToList();

                dgvVehicles.Rows.Clear();
                foreach (var v in filtered)
                {
                    string routeName = v.RouteName != null
                        ? $"{v.RouteCode} ({v.RouteName})"
                        : "Chưa phân công";

                    int idx = dgvVehicles.Rows.Add(
                        v.Id,
                        v.LicensePlate,
                        v.VehicleType,
                        v.TotalSeats,
                        v.Manufacturer ?? "Chưa rõ",
                        routeName,
                        v.Status
                    );

                    var statusCell = dgvVehicles.Rows[idx].Cells["colVehStatus"];
                    statusCell.Style.ForeColor = v.Status switch
                    {
                        "Ready"     => Color.FromArgb(16, 185, 129),
                        "InTransit" => Color.FromArgb(59, 130, 246),
                        _           => Color.FromArgb(239, 68, 68)
                    };
                    statusCell.Style.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                }
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu phương tiện: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu phương tiện: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isVehRefreshing = false;
            }
        }

        private void dgvVehicles_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isVehAdding || dgvVehicles.SelectedRows.Count == 0) return;

            var row = dgvVehicles.SelectedRows[0];
            _selectedVehId = Convert.ToInt32(row.Cells["colVehId"].Value);

            txtVehLicense.Text = row.Cells["colVehPlate"].Value?.ToString() ?? "";
            var vType = row.Cells["colVehType"].Value?.ToString() ?? "";
            if (SupportedVehicleTypes.ContainsKey(vType))
            {
                cboVehEditType.SelectedItem = vType;
                numVehSeats.Value = SupportedVehicleTypes[vType];
            }
            else
            {
                // Fallback matching by seat count if legacy type name was stored
                int seats = Convert.ToInt32(row.Cells["colVehSeats"].Value ?? 28);
                var matched = SupportedVehicleTypes.FirstOrDefault(kv => kv.Value == seats);
                if (!string.IsNullOrEmpty(matched.Key))
                {
                    cboVehEditType.SelectedItem = matched.Key;
                    numVehSeats.Value = matched.Value;
                }
                else
                {
                    cboVehEditType.SelectedIndex = 2; // "Ghế ngồi 28 chỗ"
                    numVehSeats.Value = 28;
                }
            }

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
            cboVehEditType.SelectedIndex = 2; // "Ghế ngồi 28 chỗ"
            numVehSeats.Value = 28;
            cboVehEditStatus.SelectedIndex = 0; // "Ready"
            btnVehSave.Enabled = true;
            btnVehCancel.Enabled = true;
            txtVehLicense.Focus();
        }

        private void StartVehEdit()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Vehicle, PermissionAction.Edit)) return;
            if (_selectedVehId == null) return;

            _isVehAdding = false;
            btnVehSave.Enabled = true;
            btnVehCancel.Enabled = true;
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
                assignedRouteId = _availableRoutes[cboVehRoute.SelectedIndex - 1].Id;

            string selectedType = cboVehEditType.SelectedItem?.ToString() ?? "Ghế ngồi 28 chỗ";
            int totalSeats = SupportedVehicleTypes.TryGetValue(selectedType, out int s) ? s : 28;

            var dto = new VehicleFormDto
            {
                Id           = _selectedVehId ?? 0,
                LicensePlate = plate,
                VehicleType  = selectedType,
                TotalSeats   = totalSeats,
                Manufacturer = txtVehManufacturer.Text.Trim(),
                Status       = cboVehEditStatus.SelectedItem?.ToString() ?? "Ready",
                RouteId      = assignedRouteId
            };

            try
            {
                if (_isVehAdding)
                {
                    await _vehicleService.CreateVehicleAsync(dto);
                    MessageBox.Show("Thêm phương tiện thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnVehSave.Enabled = false;
                    _isVehAdding = false;
                    await FilterVehiclesAsync();
                }
                else if (_selectedVehId.HasValue)
                {
                    await _vehicleService.UpdateVehicleAsync(dto);
                    MessageBox.Show("Cập nhật phương tiện thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnVehSave.Enabled = false;
                    await FilterVehiclesAsync();
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

        private async Task DeleteVehicleAsync()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Vehicle, PermissionAction.Delete)) return;
            if (_selectedVehId == null) return;

            using var confirmDlg = new FrmConfirmDelete(txtVehLicense.Text, "Phương tiện vận tải");
            if (confirmDlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                await _vehicleService.DeleteVehicleAsync(_selectedVehId.Value, confirmDlg.IsSoftDelete);
                MessageBox.Show("Xóa phương tiện thành công!", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _selectedVehId = null;
                await FilterVehiclesAsync();
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

        // ==========================================
        // LOGIC TRẠM DỪNG
        // ==========================================
        private async Task FilterStationsAsync()
        {
            if (_isStRefreshing) return;
            _isStRefreshing = true;
            try
            {
                var keyword = txtStKeyword.Text.Trim();
                var allStations = await _stationService.GetStationListAsync(string.IsNullOrWhiteSpace(keyword) ? null : keyword);

                // Client-side filter for city and status combos
                var cityFilter   = cboStCity.SelectedItem?.ToString();
                var statusFilter = cboStStatus.SelectedItem?.ToString();
                bool useAnd      = radStAnd.Checked;

                var filtered = allStations.Where(s =>
                {
                    bool matchCity   = string.IsNullOrEmpty(cityFilter) || cityFilter == "Tất cả" || s.City == cityFilter;
                    bool matchStatus = string.IsNullOrEmpty(statusFilter) || statusFilter == "Tất cả" || s.Status == statusFilter;
                    return useAnd ? matchCity && matchStatus : matchCity || matchStatus;
                }).ToList();

                dgvStations.Rows.Clear();
                foreach (var s in filtered)
                {
                    int idx = dgvStations.Rows.Add(
                        s.Id,
                        s.StationCode,
                        s.StationName,
                        s.Address,
                        s.City,
                        s.RouteCount,
                        s.Status
                    );

                    var statusCell = dgvStations.Rows[idx].Cells["colStStatus"];
                    statusCell.Style.ForeColor = s.Status == "Active"
                        ? Color.FromArgb(16, 185, 129)
                        : Color.FromArgb(239, 68, 68);
                    statusCell.Style.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                }
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu trạm dừng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu trạm dừng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isStRefreshing = false;
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
            btnStCancel.Enabled = true;
            txtStCode.Focus();
        }

        private void StartStEdit()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Station, PermissionAction.Edit)) return;
            if (_selectedStId == null) return;

            _isStAdding = false;
            btnStSave.Enabled = true;
            btnStCancel.Enabled = true;
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

            var dto = new StationFormDto
            {
                Id          = _selectedStId ?? 0,
                StationCode = code,
                StationName = name,
                Address     = txtStAddress.Text.Trim(),
                City        = cboStEditCity.SelectedItem?.ToString() ?? "Hà Nội",
                Status      = cboStEditStatus.SelectedItem?.ToString() ?? "Active"
            };

            try
            {
                if (_isStAdding)
                {
                    await _stationService.CreateStationAsync(dto);
                    MessageBox.Show("Thêm trạm dừng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnStSave.Enabled = false;
                    _isStAdding = false;
                    await FilterStationsAsync();
                }
                else if (_selectedStId.HasValue)
                {
                    await _stationService.UpdateStationAsync(dto);
                    MessageBox.Show("Cập nhật trạm dừng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnStSave.Enabled = false;
                    await FilterStationsAsync();
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

        private async Task DeleteStationAsync()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Station, PermissionAction.Delete)) return;
            if (_selectedStId == null) return;

            using var confirmDlg = new FrmConfirmDelete(txtStName.Text, "Trạm dừng bến xe");
            if (confirmDlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                await _stationService.DeleteStationAsync(_selectedStId.Value, confirmDlg.IsSoftDelete);
                MessageBox.Show("Xóa trạm dừng thành công!", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _selectedStId = null;
                await FilterStationsAsync();
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

        private void CancelVehEdit()
        {
            _isVehAdding = false;
            _selectedVehId = null;
            txtVehLicense.Clear();
            txtVehManufacturer.Clear();
            cboVehRoute.SelectedIndex = 0;
            btnVehSave.Enabled = false;
            btnVehCancel.Enabled = false;
            dgvVehicles.ClearSelection();
        }

        private void CancelStEdit()
        {
            _isStAdding = false;
            _selectedStId = null;
            txtStCode.Clear();
            txtStName.Clear();
            txtStAddress.Clear();
            btnStSave.Enabled = false;
            btnStCancel.Enabled = false;
            dgvStations.ClearSelection();
        }

        private async Task ExportVehiclesAsync()
        {
            btnVehExport.Enabled = false;
            try
            {
                await DataExportHelper.ExportDataGridViewWithDialogAsync(dgvVehicles, "DanhSach_PhuongTien");
            }
            finally
            {
                AuthorizationGuard.ApplyControlSecurity(SystemMenus.Vehicle, btnExport: btnVehExport);
            }
        }

        private async Task ExportStationsAsync()
        {
            btnStExport.Enabled = false;
            try
            {
                await DataExportHelper.ExportDataGridViewWithDialogAsync(dgvStations, "DanhSach_TramDung");
            }
            finally
            {
                AuthorizationGuard.ApplyControlSecurity(SystemMenus.Station, btnExport: btnStExport);
            }
        }
    }
}
