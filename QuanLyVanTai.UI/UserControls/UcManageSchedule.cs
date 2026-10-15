using Core.Configs;
using Core.Helpers;
using Core.Security;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Exceptions;
using QuanLyVanTai.BLL.Services;
using UserSession = Core.Security.UserSession;

namespace QuanLyVanTai.UI.UserControls
{
    /// <summary>
    /// Màn hình Điều độ lịch trình &amp; Phân công tài xế.
    /// Toàn bộ nghiệp vụ (kiểm tra chồng lấn xe/tài xế, GPLX, state machine trạng thái)
    /// được xử lý ở <see cref="ScheduleService"/>; view chỉ hiển thị và thu thập dữ liệu.
    /// </summary>
    public partial class UcManageSchedule : UserControl
    {
        private readonly ScheduleService _scheduleService = new();
        private readonly RouteManagementService _routeService = new();
        private readonly VehicleManagementService _vehicleService = new();

        // ── State ─────────────────────────────────────────────────────────────
        private bool _isAddingNew = false;
        private bool _suppressComboEvents = false;
        private int? _selectedScheduleId = null;
        private string _originalStatus = "Scheduled";

        private List<ScheduleListDto> _allSchedules = [];
        private List<RouteListDto> _allRoutes = [];
        private List<VehicleListDto> _routeVehicles = [];
        private List<DriverLookupDto> _allDrivers = [];

        private sealed class LookupItem
        {
            public int Id { get; set; }
            public string Display { get; set; } = string.Empty;
        }

        private sealed class StatusItem
        {
            public string? Value { get; set; }
            public string Display { get; set; } = string.Empty;
            public override string ToString() => Display;
        }

        // Map mã trạng thái (lưu DB) → nhãn tiếng Việt hiển thị.
        private static readonly Dictionary<string, string> StatusLabels = new()
        {
            ["Scheduled"]  = "Đã lên lịch",
            ["InProgress"] = "Đang chạy",
            ["Completed"]  = "Hoàn thành",
            ["Cancelled"]  = "Đã hủy",
            ["Delayed"]    = "Bị trễ"
        };

        public UcManageSchedule()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            SetupRuntimeUI();
            SetupEvents();
            ApplySecurity();
            _ = LoadInitialDataAsync();
        }

        // ── Khởi tạo UI ───────────────────────────────────────────────────────

        private void SetupRuntimeUI()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = Color.FromArgb(248, 250, 252);

            DataGridViewHighlightHelper.AttachSearchHighlighter(dgvSchedules, () => txtSearch.Text);

            // Bộ lọc thời gian mặc định: 7 ngày trước → 30 ngày tới
            dtpTuNgay.Value = DateTime.Today.AddDays(-7);
            dtpDenNgay.Value = DateTime.Today.AddDays(30);

            // Bộ lọc trạng thái
            cboLocTrangThai.Items.Clear();
            cboLocTrangThai.Items.Add(new StatusItem { Value = null, Display = "Tất cả trạng thái" });
            foreach (var pair in StatusLabels)
                cboLocTrangThai.Items.Add(new StatusItem { Value = pair.Key, Display = pair.Value });
            cboLocTrangThai.SelectedIndex = 0;

            // Trạng thái chuyến trong panel chi tiết
            cboTrangThaiChuyen.Items.Clear();
            foreach (var pair in StatusLabels)
                cboTrangThaiChuyen.Items.Add(new StatusItem { Value = pair.Key, Display = pair.Value });

            SetFormEditable(false);
        }

        private void SetupEvents()
        {
            dgvSchedules.SelectionChanged += dgvSchedules_SelectionChanged;

            btnAdd.Click += (s, e) => StartAddNew();
            btnEdit.Click += (s, e) => StartEdit();
            btnSave.Click += async (s, e) => await SaveScheduleAsync();
            btnCancel.Click += (s, e) => CancelEdit();
            btnDelete.Click += async (s, e) => await CancelSelectedScheduleAsync();
            btnRefresh.Click += async (s, e) => await ReloadSchedulesAsync();

            // Lọc / tìm kiếm chạy trên tập dữ liệu đã tải (client-side)
            txtSearch.TextChanged += (s, e) => ApplyGridFilter();
            cboLocTrangThai.SelectedIndexChanged += async (s, e) => await ReloadSchedulesAsync();
            dtpTuNgay.ValueChanged += async (s, e) => await ReloadSchedulesAsync();
            dtpDenNgay.ValueChanged += async (s, e) => await ReloadSchedulesAsync();

            // Khi đổi tuyến → nạp lại danh sách xe thuộc tuyến đó và tính lại giờ đến
            cboTuyen.SelectedIndexChanged += async (s, e) => await OnRouteChangedAsync();

            // Tính lại giờ dự kiến đến khi đổi giờ khởi hành
            dtpKhoiHanh.ValueChanged += (s, e) => RecalculateArrival();
        }

        private void ApplySecurity()
        {
            AuthorizationGuard.ApplyControlSecurity(SystemMenus.Schedule, btnAdd, btnEdit, btnDelete);
        }

        // ── Nạp dữ liệu ───────────────────────────────────────────────────────

        private async Task LoadInitialDataAsync()
        {
            try
            {
                BeginBusy(true, "Đang tải dữ liệu...");

                _allRoutes = (await _routeService.GetRouteListAsync())
                    .Where(r => r.Status == "Active")
                    .ToList();

                _allDrivers = await _scheduleService.GetActiveDriversAsync();

                BindRouteComboBox();
                BindDriverComboBoxes();

                await ReloadSchedulesAsync();
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show($"Lỗi truy cập database: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu lịch trình: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                BeginBusy(false);
            }
        }

        private void BindRouteComboBox()
        {
            _suppressComboEvents = true;
            try
            {
                var items = new List<LookupItem> { new() { Id = 0, Display = "-- Chọn tuyến xe --" } };
                items.AddRange(_allRoutes.Select(r => new LookupItem
                {
                    Id = r.Id,
                    Display = $"{r.RouteCode} - {r.RouteName}"
                }));

                cboTuyen.DisplayMember = nameof(LookupItem.Display);
                cboTuyen.ValueMember = nameof(LookupItem.Id);
                cboTuyen.DataSource = items;
            }
            finally
            {
                _suppressComboEvents = false;
            }
        }

        private void BindDriverComboBoxes()
        {
            _suppressComboEvents = true;
            try
            {
                var primary = new List<LookupItem> { new() { Id = 0, Display = "-- Chọn tài xế chính --" } };
                primary.AddRange(_allDrivers.Select(d => new LookupItem { Id = d.Id, Display = d.Display }));

                cboTaiXeChinh.DisplayMember = nameof(LookupItem.Display);
                cboTaiXeChinh.ValueMember = nameof(LookupItem.Id);
                cboTaiXeChinh.DataSource = primary;

                var secondary = new List<LookupItem> { new() { Id = 0, Display = "-- Không phân công --" } };
                secondary.AddRange(_allDrivers.Select(d => new LookupItem { Id = d.Id, Display = d.Display }));

                cboTaiXePhu.DisplayMember = nameof(LookupItem.Display);
                cboTaiXePhu.ValueMember = nameof(LookupItem.Id);
                cboTaiXePhu.DataSource = secondary;
            }
            finally
            {
                _suppressComboEvents = false;
            }
        }

        /// <summary>Nạp danh sách xe thuộc tuyến đang chọn vào cboXe (loại xe đang bảo dưỡng).</summary>
        private async Task OnRouteChangedAsync()
        {
            if (_suppressComboEvents)
                return;

            int keepVehicleId = GetSelectedId(cboXe);
            await LoadVehiclesForRouteAsync(GetSelectedId(cboTuyen), keepVehicleId > 0 ? keepVehicleId : null);
            RecalculateArrival();
        }

        /// <summary>Nạp cboXe theo tuyến. keepVehicleId dùng để giữ lại lựa chọn khi chỉ đổi bộ lọc.</summary>
        private async Task LoadVehiclesForRouteAsync(int routeId, int? keepVehicleId)
        {
            try
            {
                if (routeId <= 0)
                    _routeVehicles = [];
                else
                    _routeVehicles = (await _vehicleService.GetVehiclesByRouteAsync(routeId))
                        .Where(v => v.Status != "Maintenance")
                        .ToList();
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show($"Lỗi tải danh sách xe của tuyến: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _routeVehicles = [];
            }

            _suppressComboEvents = true;
            try
            {
                var items = new List<LookupItem> { new() { Id = 0, Display = "-- Chọn phương tiện --" } };
                items.AddRange(_routeVehicles.Select(v => new LookupItem
                {
                    Id = v.Id,
                    Display = $"{v.LicensePlate} ({v.VehicleType} - {v.TotalSeats} chỗ)"
                }));

                cboXe.DataSource = items;
                cboXe.DisplayMember = nameof(LookupItem.Display);
                cboXe.ValueMember = nameof(LookupItem.Id);

                if (keepVehicleId.HasValue && _routeVehicles.Any(v => v.Id == keepVehicleId.Value))
                    SelectComboById(cboXe, keepVehicleId.Value);
            }
            finally
            {
                _suppressComboEvents = false;
            }
        }

        /// <summary>Tự tính giờ đến dự kiến = giờ khởi hành + EstimatedHours của tuyến (tối thiểu 30 phút).</summary>
        private void RecalculateArrival()
        {
            if (_suppressComboEvents)
                return;

            int routeId = GetSelectedId(cboTuyen);
            var route = _allRoutes.FirstOrDefault(r => r.Id == routeId);
            if (route == null)
                return;

            double hours = Math.Max(0.5, (double)route.EstimatedHours);
            dtpDuKienDen.Value = dtpKhoiHanh.Value.AddHours(hours);
        }

        private async Task ReloadSchedulesAsync()
        {
            int? keepId = _selectedScheduleId;
            try
            {
                BeginBusy(true, "Đang tải lịch trình...");

                DateTime from = dtpTuNgay.Value.Date.ToUniversalTime();
                DateTime to = dtpDenNgay.Value.Date.AddDays(1).ToUniversalTime();
                string? status = (cboLocTrangThai.SelectedItem as StatusItem)?.Value;

                _allSchedules = await _scheduleService.GetSchedulesAsync(from, to, status);

                ApplyGridFilter();

                if (keepId.HasValue && dgvSchedules.Rows.Count > 0)
                    SelectRowById(keepId.Value);
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show($"Lỗi truy cập database: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải lịch trình: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                BeginBusy(false);
            }
        }

        // ── Hiển thị ──────────────────────────────────────────────────────────

        private void ApplyGridFilter()
        {
            string keyword = txtSearch.Text.Trim();
            var data = string.IsNullOrWhiteSpace(keyword)
                ? _allSchedules
                : _allSchedules.Where(s =>
                    Contains(s.ScheduleCode, keyword) ||
                    Contains(s.RouteName, keyword) ||
                    Contains(s.RouteCode, keyword) ||
                    Contains(s.LicensePlate, keyword) ||
                    Contains(s.PrimaryDriverName, keyword)).ToList();

            dgvSchedules.Rows.Clear();
            foreach (var s in data)
            {
                int rowIndex = dgvSchedules.Rows.Add(
                    s.Id,
                    s.ScheduleCode,
                    $"{s.RouteCode} - {s.RouteName}",
                    s.LicensePlate,
                    s.DepartureTime.ToLocalTime().ToString("dd/MM/yyyy HH:mm"),
                    s.EstimatedArrivalTime.ToLocalTime().ToString("dd/MM/yyyy HH:mm"),
                    string.IsNullOrWhiteSpace(s.PrimaryDriverName) ? "(chưa phân công)" : s.PrimaryDriverName,
                    StatusLabels.TryGetValue(s.Status, out var label) ? label : s.Status,
                    s.Notes ?? string.Empty);

                dgvSchedules.Rows[rowIndex].Tag = s.Status;
            }

            UpdateStats();
        }

        private void UpdateStats()
        {
            lblCard1Val.Text = _allSchedules.Count.ToString();
            lblCard2Val.Text = _allSchedules.Count(s => s.Status is "Scheduled" or "InProgress").ToString();
            lblCard3Val.Text = _allSchedules.Count(s => s.Status == "Completed").ToString();
            lblCard4Val.Text = _allSchedules.Count(s => s.Status is "Delayed" or "Cancelled").ToString();
        }

        private static bool Contains(string? source, string keyword)
            => !string.IsNullOrEmpty(source) &&
               source.Contains(keyword, StringComparison.OrdinalIgnoreCase);

        // ── Chọn dòng → hiển thị chi tiết ─────────────────────────────────────

        private async void dgvSchedules_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isAddingNew || dgvSchedules.SelectedRows.Count == 0)
                return;

            var row = dgvSchedules.SelectedRows[0];
            int scheduleId = Convert.ToInt32(row.Cells["colId"].Value);
            _selectedScheduleId = scheduleId;

            try
            {
                var detail = await _scheduleService.GetScheduleDetailAsync(scheduleId);
                await DisplayScheduleDetailAsync(detail);
            }
            catch (EntityNotFoundException)
            {
                // Dòng còn trên lưới nhưng đã bị xóa khỏi DB — bỏ qua.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chi tiết chuyến xe: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task DisplayScheduleDetailAsync(ScheduleDetailDto detail)
        {
            // Nạp xe của tuyến trước khi chọn đúng xe của chuyến
            await LoadVehiclesForRouteAsync(detail.RouteId, detail.VehicleId);

            _suppressComboEvents = true;
            try
            {
                txtMaChuyen.Text = detail.ScheduleCode;
                _originalStatus = detail.Status;

                SelectComboById(cboTuyen, detail.RouteId);
                SelectComboById(cboXe, detail.VehicleId);

                dtpKhoiHanh.Value = detail.DepartureTime.ToLocalTime();
                dtpDuKienDen.Value = detail.EstimatedArrivalTime.ToLocalTime();

                var primary = detail.Assignments
                    .FirstOrDefault(a => a.Role == "Primary" && a.AssignmentStatus != "Cancelled");
                var secondary = detail.Assignments
                    .FirstOrDefault(a => a.Role == "Secondary" && a.AssignmentStatus != "Cancelled");

                SelectComboById(cboTaiXeChinh, primary?.DriverId ?? 0);
                SelectComboById(cboTaiXePhu, secondary?.DriverId ?? 0);

                SelectStatusCombo(cboTrangThaiChuyen, detail.Status);
                txtGhiChu.Text = detail.Notes ?? string.Empty;
            }
            finally
            {
                _suppressComboEvents = false;
            }

            SetFormEditable(false);
        }

        // ── Trạng thái form ───────────────────────────────────────────────────

        private void SetFormEditable(bool editable)
        {
            cboTuyen.Enabled = editable;
            cboXe.Enabled = editable;
            dtpKhoiHanh.Enabled = editable;
            dtpDuKienDen.Enabled = editable;
            cboTaiXeChinh.Enabled = editable;
            cboTaiXePhu.Enabled = editable;
            cboTrangThaiChuyen.Enabled = editable && !_isAddingNew;
            txtGhiChu.ReadOnly = !editable;

            bool hasSelection = dgvSchedules.SelectedRows.Count > 0;
            string rowStatus = hasSelection ? dgvSchedules.SelectedRows[0].Tag?.ToString() ?? string.Empty : string.Empty;
            bool isTerminal = rowStatus is "Completed" or "Cancelled";

            btnAdd.Enabled = !editable && UserSession.HasPermission(SystemMenus.Schedule, PermissionAction.Add);
            btnEdit.Enabled = !editable && hasSelection && !isTerminal &&
                              UserSession.HasPermission(SystemMenus.Schedule, PermissionAction.Edit);
            btnDelete.Enabled = !editable && hasSelection && !isTerminal &&
                                UserSession.HasPermission(SystemMenus.Schedule, PermissionAction.Delete);
            btnRefresh.Enabled = !editable;

            btnSave.Enabled = editable;
            btnCancel.Enabled = editable;

            dgvSchedules.Enabled = !editable;
        }

        private void StartAddNew()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Schedule, PermissionAction.Add))
                return;

            _isAddingNew = true;
            _selectedScheduleId = null;

            _suppressComboEvents = true;
            try
            {
                cboTuyen.SelectedIndex = 0;
                cboXe.DataSource = new List<LookupItem> { new() { Id = 0, Display = "-- Chọn phương tiện --" } };
                cboTaiXeChinh.SelectedIndex = 0;
                cboTaiXePhu.SelectedIndex = 0;
                SelectStatusCombo(cboTrangThaiChuyen, "Scheduled");

                dtpKhoiHanh.Value = DateTime.Now.AddHours(2);
                dtpDuKienDen.Value = DateTime.Now.AddHours(3);
                txtMaChuyen.Text = "(Tự sinh khi lưu)";
                txtGhiChu.Clear();
            }
            finally
            {
                _suppressComboEvents = false;
            }

            _routeVehicles = [];
            SetFormEditable(true);
            cboTuyen.Focus();
        }

        private void StartEdit()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Schedule, PermissionAction.Edit))
                return;

            if (_selectedScheduleId == null)
            {
                MessageBox.Show("Vui lòng chọn một chuyến xe cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _isAddingNew = false;
            SetFormEditable(true);
            cboTuyen.Focus();
        }

        private void CancelEdit()
        {
            _isAddingNew = false;
            SetFormEditable(false);
            dgvSchedules_SelectionChanged(null, EventArgs.Empty);
        }

        // ── Lưu ───────────────────────────────────────────────────────────────

        private async Task SaveScheduleAsync()
        {
            int routeId = GetSelectedId(cboTuyen);
            int vehicleId = GetSelectedId(cboXe);
            int primaryDriverId = GetSelectedId(cboTaiXeChinh);
            int secondaryDriverId = GetSelectedId(cboTaiXePhu);

            if (routeId <= 0)
            {
                MessageBox.Show("Vui lòng chọn tuyến xe!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTuyen.Focus();
                return;
            }
            if (vehicleId <= 0)
            {
                MessageBox.Show("Vui lòng chọn phương tiện!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboXe.Focus();
                return;
            }
            if (primaryDriverId <= 0)
            {
                MessageBox.Show("Vui lòng chọn tài xế chính cho chuyến!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTaiXeChinh.Focus();
                return;
            }
            if (secondaryDriverId > 0 && secondaryDriverId == primaryDriverId)
            {
                MessageBox.Show("Tài xế phụ không được trùng với tài xế chính!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTaiXePhu.Focus();
                return;
            }

            var dto = new CreateScheduleDto
            {
                RouteId = routeId,
                VehicleId = vehicleId,
                // DateTimePicker trả giờ local → chuyển sang UTC vì DB lưu UTC.
                DepartureTime = dtpKhoiHanh.Value.ToUniversalTime(),
                EstimatedArrivalTime = dtpDuKienDen.Value.ToUniversalTime(),
                Notes = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim(),
                Drivers =
                [
                    new AssignDriverDto { DriverId = primaryDriverId, Role = "Primary" }
                ]
            };

            if (secondaryDriverId > 0)
                dto.Drivers.Add(new AssignDriverDto { DriverId = secondaryDriverId, Role = "Secondary" });

            try
            {
                BeginBusy(true, "Đang lưu chuyến xe...");

                if (_isAddingNew)
                {
                    var created = await _scheduleService.CreateScheduleAsync(dto);
                    _selectedScheduleId = created.Id;
                    MessageBox.Show($"Thêm chuyến xe [{created.ScheduleCode}] thành công!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_selectedScheduleId.HasValue)
                {
                    await _scheduleService.UpdateScheduleAsync(_selectedScheduleId.Value, dto);

                    string newStatus = (cboTrangThaiChuyen.SelectedItem as StatusItem)?.Value ?? _originalStatus;
                    if (newStatus != _originalStatus)
                        await _scheduleService.UpdateScheduleStatusAsync(_selectedScheduleId.Value, newStatus);

                    MessageBox.Show("Cập nhật chuyến xe thành công!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                _isAddingNew = false;
                SetFormEditable(false);
                await ReloadSchedulesAsync();
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
                MessageBox.Show(ex.Message, "Vi phạm quy tắc nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show($"Lỗi database: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                BeginBusy(false);
            }
        }

        // ── Hủy chuyến ────────────────────────────────────────────────────────

        private async Task CancelSelectedScheduleAsync()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Schedule, PermissionAction.Delete))
                return;

            if (_selectedScheduleId == null)
            {
                MessageBox.Show("Vui lòng chọn chuyến xe cần hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string code = txtMaChuyen.Text;
            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn hủy chuyến [{code}]?\n" +
                "Xe và tài xế của chuyến sẽ được giải phóng khỏi lịch trình này.",
                "Xác nhận hủy chuyến", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
                return;

            try
            {
                BeginBusy(true, "Đang hủy chuyến...");
                await _scheduleService.UpdateScheduleStatusAsync(_selectedScheduleId.Value, "Cancelled");
                MessageBox.Show($"Đã hủy chuyến [{code}]!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await ReloadSchedulesAsync();
            }
            catch (BusinessRuleViolationException ex)
            {
                MessageBox.Show(ex.Message, "Không thể hủy chuyến", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (EntityNotFoundException ex)
            {
                MessageBox.Show($"Không tìm thấy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show($"Lỗi database: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                BeginBusy(false);
            }
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private void BeginBusy(bool busy, string message = "")
        {
            pbProgress.Visible = busy;
            lblProgress.Text = busy ? message : "Sẵn sàng";
            if (busy)
            {
                pbProgress.Style = ProgressBarStyle.Marquee;
                pbProgress.MarqueeAnimationSpeed = 30;
            }
            else
            {
                pbProgress.Style = ProgressBarStyle.Blocks;
                pbProgress.MarqueeAnimationSpeed = 0;
            }
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
        }

        private static int GetSelectedId(ComboBox combo)
            => (combo.SelectedItem as LookupItem)?.Id ?? 0;

        private static void SelectComboById(ComboBox combo, int id)
        {
            if (combo.DataSource is not IEnumerable<LookupItem> items)
                return;

            var target = items.FirstOrDefault(i => i.Id == id);
            if (target != null)
                combo.SelectedItem = target;
        }

        private static void SelectStatusCombo(ComboBox combo, string status)
        {
            foreach (var item in combo.Items)
            {
                if (item is StatusItem si && si.Value == status)
                {
                    combo.SelectedItem = si;
                    return;
                }
            }
        }

        private void SelectRowById(int id)
        {
            foreach (DataGridViewRow row in dgvSchedules.Rows)
            {
                if (Convert.ToInt32(row.Cells["colId"].Value) == id)
                {
                    row.Selected = true;
                    return;
                }
            }
        }
    }
}
