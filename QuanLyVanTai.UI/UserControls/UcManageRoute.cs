using Core.Configs;
using Core.Helpers;
using Core.Security;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Exceptions;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.UI.Forms;
using QuanLyVanTai.UI.Models;
using UserSession = Core.Security.UserSession;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcManageRoute : UserControl
    {
        private readonly RouteManagementService _routeService = new();
        private readonly RouteExcelService _excelService = new();
        private readonly RouteJsonExportService _jsonExportService = new();
        private readonly StationManagementService _stationService = new();
        private readonly VehicleManagementService _vehicleService = new();

        // Data binding hai chiều: mọi control nhập liệu dùng chung một nguồn RouteEditModel
        private readonly BindingSource _routeBindingSource = new();

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
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || DesignMode)
                return;
            SetupRuntimeUI();
            SetupEvents();
            ApplySecurity();
            _ = LoadInitialDataAsync();
        }

        private void SetupRuntimeUI()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = Color.FromArgb(248, 250, 252);

            SetupRouteDataBinding();

            // Style Buttons with icons & colors
            ThemeConfig.StyleSuccessButton(btnAdd, IconChar.PlusCircle);
            ThemeConfig.StylePrimaryButton(btnEdit, IconChar.PenToSquare);
            ThemeConfig.StyleDangerButton(btnDelete, IconChar.TrashAlt);
            ThemeConfig.StylePrimaryButton(btnSave, IconChar.FloppyDisk);
            ThemeConfig.StyleSecondaryButton(btnCancel, IconChar.Times);
            ThemeConfig.StyleSecondaryButton(btnHistory, IconChar.History);
            ThemeConfig.StyleSecondaryButton(btnExport, IconChar.FileExcel);
            ThemeConfig.StyleSecondaryButton(btnImport, IconChar.FileImport);

            btnHistory.BackColor = Color.FromArgb(100, 116, 139);
            btnExport.BackColor = Color.FromArgb(16, 185, 129);
            btnImport.BackColor = Color.FromArgb(245, 158, 11);

            // Attach Real-time Highlight Helper
            DataGridViewHighlightHelper.AttachSearchHighlighter(dgvRoutes, () => txtSearch.Text);

            SetFormEditable(false);
        }

        /// <summary>
        /// Thiết lập data binding hai chiều giữa control nhập liệu và <see cref="RouteEditModel"/>.
        /// DataSourceUpdateMode.OnPropertyChanged: mỗi lần người dùng gõ/chọn, model được cập nhật ngay.
        /// </summary>
        private void SetupRouteDataBinding()
        {
            _routeBindingSource.DataSource = typeof(RouteEditModel);

            txtRouteCode.DataBindings.Add(
                nameof(TextBox.Text), _routeBindingSource, nameof(RouteEditModel.RouteCode),
                true, DataSourceUpdateMode.OnPropertyChanged);

            txtRouteName.DataBindings.Add(
                nameof(TextBox.Text), _routeBindingSource, nameof(RouteEditModel.RouteName),
                true, DataSourceUpdateMode.OnPropertyChanged);

            numDistance.DataBindings.Add(
                nameof(NumericUpDown.Value), _routeBindingSource, nameof(RouteEditModel.DistanceKm),
                true, DataSourceUpdateMode.OnPropertyChanged);

            numEstimatedHours.DataBindings.Add(
                nameof(NumericUpDown.Value), _routeBindingSource, nameof(RouteEditModel.EstimatedHours),
                true, DataSourceUpdateMode.OnPropertyChanged);

            numBasePrice.DataBindings.Add(
                nameof(NumericUpDown.Value), _routeBindingSource, nameof(RouteEditModel.BasePrice),
                true, DataSourceUpdateMode.OnPropertyChanged);

            cboStatus.DataBindings.Add(
                nameof(ComboBox.SelectedItem), _routeBindingSource, nameof(RouteEditModel.Status),
                true, DataSourceUpdateMode.OnPropertyChanged);
        }

        /// <summary>Model đang gắn với form nhập liệu (BindingSource có thể đang ở trạng thái thêm mới).</summary>
        private RouteEditModel CurrentRouteModel =>
            _routeBindingSource.Current as RouteEditModel ?? new RouteEditModel();

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
            btnHistory.Click += (s, e) => ViewAuditHistory();
            btnExport.Click += async (s, e) => await ExportRoutesToExcelAsync();
            btnImport.Click += async (s, e) => await ImportRoutesFromExcelAsync();

            // ContextMenuStrip trên DataGridView: xuất Excel / JSON nhanh
            mnuXuatExcel.Click += async (s, e) => await ExportRoutesToExcelAsync();
            mnuXuatJson.Click += async (s, e) => await ExportRoutesToJsonAsync();

            cboFkStation.SelectedIndexChanged += (s, e) => ApplyFkStationSelection();
            cboFkVehicle.SelectedIndexChanged += (s, e) => ApplyFkVehicleSelection();

            // ── Drag & Drop: kéo file .xlsx thả vào DataGridView để import ──
            dgvRoutes.AllowDrop = true;
            dgvRoutes.DragEnter += DgvRoutes_DragEnter;
            dgvRoutes.DragDrop  += DgvRoutes_DragDrop;
        }

        /// <summary>
        /// Kiểm tra file kéo vào có phải .xlsx không.
        /// Nếu đúng: đổi cursor thành Copy để báo hiệu "có thể thả".
        /// Nếu sai : đổi cursor thành None để báo hiệu "không chấp nhận".
        /// </summary>
        private void DgvRoutes_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data is null) return;

            bool isExcelFile =
                e.Data.GetDataPresent(DataFormats.FileDrop) &&
                e.Data.GetData(DataFormats.FileDrop) is string[] files &&
                files.Length == 1 &&
                files[0].EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase);

            e.Effect = isExcelFile ? DragDropEffects.Copy : DragDropEffects.None;
        }

        /// <summary>
        /// Người dùng thả file .xlsx vào DataGridView → gọi import luôn,
        /// không cần mở OpenFileDialog.
        /// </summary>
        private async void DgvRoutes_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetData(DataFormats.FileDrop) is not string[] files || files.Length == 0)
                return;

            string filePath = files[0];

            if (!filePath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Chỉ hỗ trợ file Excel định dạng .xlsx.",
                    "Định dạng không hỗ trợ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!AuthorizationGuard.CheckAccess(SystemMenus.Route, PermissionAction.Add))
                return;

            // Xác nhận trước khi import
            var confirm = MessageBox.Show(
                $"Import tuyến xe từ file:\n{Path.GetFileName(filePath)}\n\nXác nhận tiến hành?",
                "Xác nhận Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            await ImportRoutesFromFileAsync(filePath);
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
                await RefreshDashboardStatsAsync();
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

        /// <summary>
        /// Cập nhật 4 thẻ KPI từ số liệu tổng hợp trong CSDL.
        /// Luôn tính trên toàn bộ tuyến xe (không phụ thuộc từ khóa tìm kiếm đang gõ).
        /// </summary>
        private async Task RefreshDashboardStatsAsync()
        {
            var stats = await _routeService.GetDashboardStatsAsync();

            lblCard1Val.Text = stats.TotalRoutes.ToString("N0");
            lblCard2Val.Text = stats.ActiveRoutes.ToString("N0");
            lblCard3Val.Text = $"{stats.TotalDistanceKm:N0} km";
            lblCard4Val.Text = string.Format(UiTranslator.Translate("{0:N0} VNĐ"), stats.AverageBasePrice);
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
            // Gán model cho BindingSource — toàn bộ text/numeric/combo tự cập nhật nhờ data binding
            _routeBindingSource.DataSource = new RouteEditModel
            {
                RouteCode      = route.RouteCode,
                RouteName      = route.RouteName,
                DistanceKm     = Math.Min(numDistance.Maximum, route.DistanceKm),
                EstimatedHours = Math.Min(numEstimatedHours.Maximum, route.EstimatedHours),
                BasePrice      = Math.Min(numBasePrice.Maximum, route.BasePrice),
                Status         = route.Status
            };

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
            bool canExport = UserSession.HasPermission(SystemMenus.Route, PermissionAction.Export);
            btnExport.Enabled = !editable && canExport;
            btnImport.Enabled = !editable && UserSession.HasPermission(SystemMenus.Route, PermissionAction.Add);

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

            // Reset model rỗng — BindingSource tự đẩy giá trị mặc định xuống các control
            _routeBindingSource.DataSource = new RouteEditModel
            {
                BasePrice = 100000m,
                Status    = "Active"
            };

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
            // Đẩy nốt giá trị control đang chỉnh dở xuống model (binding hai chiều)
            _routeBindingSource.EndEdit();
            RouteEditModel model = CurrentRouteModel;

            string code = model.RouteCode.Trim();
            string name = model.RouteName.Trim();

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
                DistanceKm         = model.DistanceKm,
                EstimatedHours     = model.EstimatedHours,
                BasePrice          = model.BasePrice,
                Status             = model.Status,
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
                    await RefreshDashboardStatsAsync();
                }
                else if (_selectedRouteId.HasValue)
                {
                    await _routeService.UpdateRouteAsync(dto);
                    MessageBox.Show("Cập nhật tuyến xe thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetFormEditable(false);
                    await ReloadRoutesListAsync();
                    await RefreshDashboardStatsAsync();
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
                await RefreshDashboardStatsAsync();
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

        /// <summary>
        /// Mở dialog lịch sử thao tác (Audit Trail) của tuyến xe đang chọn.
        /// </summary>
        private void ViewAuditHistory()
        {
            if (_selectedRouteId is not int routeId)
            {
                MessageBox.Show("Vui lòng chọn một tuyến xe để xem lịch sử thao tác!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dialog = new FrmRouteAuditHistory(routeId, txtRouteCode.Text, txtRouteName.Text);
            dialog.ShowDialog(FindForm());
        }

        // ── Import / Export Excel ─────────────────────────────────────────────

        /// <summary>
        /// Bọc IProgress&lt;int&gt; để cập nhật luôn lên ProgressBar + label.
        /// IProgress.Report chạy trên UI SynchronizationContext nên an toàn thread.
        /// </summary>
        private IProgress<int> CreateProgressReporter()
        {
            return new Progress<int>(pct =>
            {
                int clamped = Math.Clamp(pct, 0, 100);
                pbExcelProgress.Value = clamped;
                lblExcelProgress.Text = string.Format(UiTranslator.Translate("Tiến độ: {0}%"), clamped);
            });
        }

        private void BeginExcelOperation(bool enabled)
        {
            // enabled == true: đang chạy Import/Export → hiện progress, khóa các nút thao tác khác.
            pbExcelProgress.Value = 0;
            lblExcelProgress.Text = string.Format(UiTranslator.Translate("Tiến độ: {0}%"), 0);
            pbExcelProgress.Visible = enabled;
            lblExcelProgress.Visible = enabled;

            btnImport.Enabled = !enabled;
            btnExport.Enabled = !enabled;
            btnAdd.Enabled = !enabled;
            btnEdit.Enabled = !enabled;
            btnDelete.Enabled = !enabled;
            btnHistory.Enabled = !enabled;
            dgvRoutes.Enabled = !enabled;
        }

        /// <summary>
        /// Xuất toàn bộ tuyến xe ra file Excel (.xlsx) với thanh tiến độ.
        /// File dialog chạy trên UI thread; ghi file nặng chạy trong Task.Run ở BLL.
        /// </summary>
        private async Task ExportRoutesToExcelAsync()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Route, PermissionAction.Export))
                return;

            using SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "Xuất danh sách tuyến xe ra Excel",
                FileName = $"DanhSach_TuyenXe_{DateTime.Now:yyyyMMdd_HHmmss}",
                Filter = "File Excel (*.xlsx)|*.xlsx",
                DefaultExt = "xlsx"
            };
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            BeginExcelOperation(true); // Ẩn nút + hiện progress
            try
            {
                var progress = CreateProgressReporter();
                int rowCount = await _excelService.ExportRoutesAsync(sfd.FileName, progress);

                MessageBox.Show(
                    $"Đã xuất {rowCount} tuyến xe ra file:\n{sfd.FileName}",
                    "Xuất Excel thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (BusinessRuleViolationException ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show($"Lỗi database khi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                BeginExcelOperation(false); // Khôi phục UI
            }
        }

        /// <summary>
        /// Xuất danh sách tuyến xe ra file JSON nghiệp vụ (.json) để tích hợp
        /// hệ thống ngoài. Chuỗi UTF-8 giữ nguyên tiếng Việt; bộ lọc từ ô tìm
        /// kiếm hiện tại được áp dụng để chỉ xuất đúng tập dữ liệu đang xem.
        /// </summary>
        private async Task ExportRoutesToJsonAsync()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Route, PermissionAction.Export))
                return;

            using SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "Xuất danh sách tuyến xe ra JSON",
                FileName = $"DanhSach_TuyenXe_{DateTime.Now:yyyyMMdd_HHmmss}",
                Filter = "File JSON (*.json)|*.json",
                DefaultExt = "json"
            };
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                int rowCount = await _jsonExportService.ExportRoutesAsync(sfd.FileName, txtSearch.Text);

                MessageBox.Show(
                    $"Đã xuất {rowCount} tuyến xe ra file JSON:\n{sfd.FileName}",
                    "Xuất JSON thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (BusinessRuleViolationException ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show($"Lỗi database khi xuất JSON: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất JSON: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Import tuyến xe từ file Excel (.xlsx) với thanh tiến độ.
        /// Toàn bộ dữ liệu được bọc trong transaction ở BLL — nếu 1 dòng sai
        /// sẽ Rollback toàn bộ và hiển thị đúng dòng lỗi.
        /// </summary>
        private async Task ImportRoutesFromExcelAsync()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Route, PermissionAction.Add))
                return;

            using OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Chọn file Excel tuyến xe (.xlsx)",
                Filter = "File Excel (*.xlsx)|*.xlsx",
                CheckFileExists = true,
                Multiselect = false
            };
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            await ImportRoutesFromFileAsync(ofd.FileName);
        }

        /// <summary>
        /// Thực hiện import từ đường dẫn file cụ thể — dùng chung cho cả
        /// nút Import (OpenFileDialog) lẫn Drag &amp; Drop.
        /// </summary>
        private async Task ImportRoutesFromFileAsync(string filePath)
        {
            BeginExcelOperation(true); // Ẩn nút + hiện progress
            try
            {
                var progress = CreateProgressReporter();
                var result = await _excelService.ImportRoutesAsync(filePath, progress);

                MessageBox.Show(
                    $"Import thành công!\n\n" +
                    $"Tổng dòng đọc   : {result.TotalRows}\n" +
                    $"Đã thêm mới     : {result.SuccessRows}\n" +
                    $"Bỏ qua (trùng mã): {result.SkippedDuplicateRows}",
                    "Import Excel thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await ReloadRoutesListAsync(txtSearch.Text);
                await RefreshDashboardStatsAsync();
            }
            catch (ExcelImportException ex)
            {
                MessageBox.Show(ex.Message, "Import thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show($"Lỗi database khi import Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi import Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                BeginExcelOperation(false); // Khôi phục UI
            }
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
