using Core.Configs;
using Core.Helpers;
using Core.Security;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;
using UserSession = Core.Security.UserSession;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcManageTicket : UserControl
    {
        // ==========================================
        // SERVICES
        // ==========================================
        private readonly TicketService _ticketService = new();
        private readonly AuditLogService _auditLogService = new();

        // ==========================================
        // STATE
        // ==========================================
        private bool _isAdding = false;
        private bool _isRefreshing = false;
        private int? _selectedTicketId = null;
        private List<Route> _routes = [];
        private List<Vehicle> _vehiclesForRoute = [];

        public UcManageTicket()
        {
            InitializeComponent();

            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || this.DesignMode)
            {
                return;
            }

            if (!AuthorizationGuard.GuardFormAccess(this, SystemMenus.Ticket))
                return;

            SetupRuntimeUI();
            WireEvents();
            ApplySecurity();
            _ = LoadInitialDataAsync();
        }

        // ==========================================
        // SETUP RUNTIME UI
        // ==========================================
        private void SetupRuntimeUI()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = ThemeConfig.BackgroundColor;

            // Stats cards
            pnlStats.Controls.Clear();
            var cardTotal = CreateStatCard("📋 Tổng vé", "0", Color.FromArgb(59, 130, 246), 0, out lblStatTotal);
            var cardBooked = CreateStatCard("🟡 Đã đặt", "0", Color.FromArgb(245, 158, 11), 230, out lblStatBooked);
            var cardPaid = CreateStatCard("✅ Đã thanh toán", "0", Color.FromArgb(16, 185, 129), 460, out lblStatPaid);
            var cardCancelled = CreateStatCard("❌ Đã hủy", "0", Color.FromArgb(239, 68, 68), 690, out lblStatCancelled);
            pnlStats.Controls.AddRange([cardTotal, cardBooked, cardPaid, cardCancelled]);

            // Toolbar items
            cboPayStatus.Items.Clear();
            cboPayStatus.Items.AddRange(["Tất cả", "Pending", "Paid", "Refunded"]);
            cboPayStatus.SelectedIndex = 0;

            cboTicketStatus.Items.Clear();
            cboTicketStatus.Items.AddRange(["Tất cả", "Booked", "Boarded", "Completed", "Cancelled"]);
            cboTicketStatus.SelectedIndex = 0;

            cboPriceRange.Items.Clear();
            cboPriceRange.Items.AddRange(["Tất cả mức giá", "Dưới 200.000 đ", "200.000 đ - 400.000 đ", "400.000 đ - 700.000 đ", "Trên 700.000 đ"]);
            cboPriceRange.SelectedIndex = 0;

            ThemeConfig.StyleSecondaryButton(btnRefresh, IconChar.RotateLeft);
            ThemeConfig.StyleSuccessButton(btnAdd, IconChar.Plus);
            ThemeConfig.StyleSecondaryButton(btnExport, IconChar.FileExcel);
            ThemeConfig.StylePrimaryButton(btnCheckInQr, IconChar.Qrcode);

            // DataGridView setup
            if (dgvTickets.Columns.Count == 0)
            {
                dgvTickets.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "ID", Visible = false });
                dgvTickets.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCode", HeaderText = "Mã Vé", Width = 130, DefaultCellStyle = { Font = new Font("Segoe UI", 9F, FontStyle.Bold) } });
                dgvTickets.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCustomer", HeaderText = "Tên Hành Khách", Width = 160 });
                dgvTickets.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPhone", HeaderText = "Số Điện Thoại", Width = 115, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
                dgvTickets.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRoute", HeaderText = "Tuyến Xe", Width = 180 });
                dgvTickets.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSeat", HeaderText = "Số Ghế", Width = 75, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
                dgvTickets.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDeparture", HeaderText = "Khởi Hành", Width = 140, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
                dgvTickets.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPrice", HeaderText = "Giá Vé (đ)", Width = 105, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
                dgvTickets.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPayStatus", HeaderText = "Thanh Toán", Width = 100, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
                dgvTickets.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTicketStatus", HeaderText = "Trạng Thái", Width = 100, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            }

            DataGridViewHighlightHelper.AttachSearchHighlighter(dgvTickets, () => txtKeyword.Text);

            // Detail items
            cboDetailPayStatus.Items.Clear();
            cboDetailPayStatus.Items.AddRange(["Pending", "Paid", "Refunded"]);
            cboDetailPayStatus.SelectedIndex = 0;

            cboDetailTicketStatus.Items.Clear();
            cboDetailTicketStatus.Items.AddRange(["Booked", "Boarded", "Completed", "Cancelled"]);
            cboDetailTicketStatus.SelectedIndex = 0;

            ThemeConfig.StylePrimaryButton(btnSave, IconChar.FloppyDisk);
            ThemeConfig.StyleSecondaryButton(btnCancel, IconChar.Xmark);
            ThemeConfig.StyleDangerButton(btnDelete, IconChar.Trash);
            ThemeConfig.StyleSecondaryButton(btnCancelTicket, IconChar.Ban);

            SetDetailMode(false);
        }

        private static void AddDetailLabel(string text, int y, Panel parent, int x = 18)
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
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            var bar = new Panel { Size = new Size(4, 64), Location = new Point(0, 0), BackColor = accent };
            card.Controls.Add(bar);

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
        private void WireEvents()
        {
            // Debounce để tránh nhiều event fire đồng thời → DbContext concurrent error
            txtKeyword.TextChanged += (s, e) => ScheduleRefresh();
            cboRoute.SelectedIndexChanged += (s, e) => ScheduleRefresh();
            cboPayStatus.SelectedIndexChanged += (s, e) => ScheduleRefresh();
            cboTicketStatus.SelectedIndexChanged += (s, e) => ScheduleRefresh();
            cboPriceRange.SelectedIndexChanged += (s, e) => ScheduleRefresh();

            chkUseDateFilter.CheckedChanged += (s, e) =>
            {
                dtpFrom.Enabled = chkUseDateFilter.Checked;
                dtpTo.Enabled = chkUseDateFilter.Checked;
            };
            dtpFrom.ValueChanged += (s, e) => ScheduleRefresh();
            dtpTo.ValueChanged += (s, e) => ScheduleRefresh();

            btnRefresh.Click += async (s, e) => await LoadInitialDataAsync();
            btnAdd.Click += (s, e) => StartAdd();
            btnExport.Click += async (s, e) => await ExportAsync();
            btnCheckInQr.Click += async (s, e) => await OpenCheckInDialogAsync();

            dgvTickets.SelectionChanged += DgvTickets_SelectionChanged;
            cboDetailRoute.SelectedIndexChanged += async (s, e) => await LoadVehiclesForSelectedRouteAsync();

            btnSave.Click += async (s, e) => await SaveAsync();
            btnCancel.Click += (s, e) => CancelEdit();
            btnDelete.Click += async (s, e) => await DeleteAsync();
            btnCancelTicket.Click += async (s, e) => await CancelTicketAsync();
        }

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
                SystemMenus.Ticket,
                btnAdd, btnExport,
                hideWhenDenied: true);
        }

        // ==========================================
        // LOAD / REFRESH DATA
        // ==========================================
        private async Task LoadInitialDataAsync()
        {
            try
            {
                // Load routes vào filter và detail combobox
                _routes = await _ticketService.GetActiveRoutesAsync();

                cboRoute.Items.Clear();
                cboRoute.Items.Add("Tất cả");
                cboDetailRoute.Items.Clear();

                foreach (var r in _routes)
                {
                    cboRoute.Items.Add($"{r.RouteCode} - {r.RouteName}");
                    cboDetailRoute.Items.Add($"{r.RouteCode} - {r.RouteName}");
                }

                if (cboRoute.Items.Count > 0) cboRoute.SelectedIndex = 0;

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
                string? routeId = null;
                if (cboRoute.SelectedIndex > 0 && cboRoute.SelectedIndex - 1 < _routes.Count)
                    routeId = _routes[cboRoute.SelectedIndex - 1].Id.ToString();

                decimal? minPrice = null;
                decimal? maxPrice = null;
                switch (cboPriceRange.SelectedIndex)
                {
                    case 1: maxPrice = 200000m; break;
                    case 2: minPrice = 200000m; maxPrice = 400000m; break;
                    case 3: minPrice = 400000m; maxPrice = 700000m; break;
                    case 4: minPrice = 700000m; break;
                }

                var criteria = new TicketFilterCriteria
                {
                    Keyword = txtKeyword.Text,
                    RouteId = routeId,
                    PaymentStatus = cboPayStatus.SelectedItem?.ToString(),
                    TicketStatus = cboTicketStatus.SelectedItem?.ToString(),
                    DepartureFrom = chkUseDateFilter.Checked ? dtpFrom.Value.Date : null,
                    DepartureTo = chkUseDateFilter.Checked ? dtpTo.Value.Date : null,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice
                };

                var tickets = await _ticketService.SearchAndFilterTicketsAsync(criteria);
                RenderGrid(tickets);
                UpdateStats(tickets);
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

        private async Task OpenCheckInDialogAsync()
        {
            try
            {
                using var frm = new Forms.FrmPassengerCheckIn();
                frm.ShowDialog(this.FindForm());
                await RefreshGridAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể mở giao diện Check-in: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderGrid(List<Ticket> tickets)
        {
            dgvTickets.Rows.Clear();

            foreach (var t in tickets)
            {
                int idx = dgvTickets.Rows.Add(
                    t.Id,
                    t.TicketCode,
                    t.CustomerName,
                    t.CustomerPhone,
                    t.Route?.RouteName ?? "—",
                    t.SeatNumber,
                    t.DepartureTime.ToString("dd/MM/yyyy HH:mm"),
                    t.Price.ToString("N0"),
                    t.PaymentStatus,
                    t.TicketStatus
                );

                var row = dgvTickets.Rows[idx];

                // Màu trạng thái thanh toán
                var payCell = row.Cells["colPayStatus"];
                payCell.Style.ForeColor = t.PaymentStatus switch
                {
                    "Paid" => Color.FromArgb(16, 185, 129),
                    "Refunded" => Color.FromArgb(245, 158, 11),
                    _ => Color.FromArgb(100, 116, 139)
                };
                payCell.Style.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);

                // Màu trạng thái vé
                var statusCell = row.Cells["colTicketStatus"];
                statusCell.Style.ForeColor = t.TicketStatus switch
                {
                    "Boarded" => Color.FromArgb(14, 165, 233),
                    "Completed" => Color.FromArgb(16, 185, 129),
                    "Cancelled" => Color.FromArgb(239, 68, 68),
                    _ => Color.FromArgb(245, 158, 11)
                };
                statusCell.Style.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            }
        }

        private void UpdateStats(List<Ticket> tickets)
        {
            lblStatTotal.Text = tickets.Count.ToString();
            lblStatBooked.Text = tickets.Count(t => t.TicketStatus == "Booked").ToString();
            lblStatPaid.Text = tickets.Count(t => t.PaymentStatus == "Paid").ToString();
            lblStatCancelled.Text = tickets.Count(t => t.TicketStatus == "Cancelled").ToString();
        }

        // ==========================================
        // DETAIL PANEL LOGIC
        // ==========================================
        private void DgvTickets_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isAdding || dgvTickets.SelectedRows.Count == 0) return;

            var row = dgvTickets.SelectedRows[0];
            _selectedTicketId = Convert.ToInt32(row.Cells["colId"].Value);

            FillDetailFromRow(row);
            SetDetailMode(isEditing: false);
        }

        private void FillDetailFromRow(DataGridViewRow row)
        {
            txtTicketCode.Text = row.Cells["colCode"].Value?.ToString() ?? "";
            txtCustomerName.Text = row.Cells["colCustomer"].Value?.ToString() ?? "";
            txtCustomerPhone.Text = row.Cells["colPhone"].Value?.ToString() ?? "";
            txtSeatNumber.Text = row.Cells["colSeat"].Value?.ToString() ?? "";

            if (decimal.TryParse(row.Cells["colPrice"].Value?.ToString()?.Replace(",", ""), out decimal price))
                numPrice.Value = price;

            if (DateTime.TryParseExact(row.Cells["colDeparture"].Value?.ToString(), "dd/MM/yyyy HH:mm",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime dep))
                dtpDeparture.Value = dep;

            // Route
            string routeName = row.Cells["colRoute"].Value?.ToString() ?? "";
            for (int i = 0; i < _routes.Count; i++)
            {
                if (_routes[i].RouteName == routeName)
                {
                    cboDetailRoute.SelectedIndex = i;
                    break;
                }
            }

            string ps = row.Cells["colPayStatus"].Value?.ToString() ?? "";
            cboDetailPayStatus.SelectedItem = ps;

            string ts = row.Cells["colTicketStatus"].Value?.ToString() ?? "";
            cboDetailTicketStatus.SelectedItem = ts;
        }

        private void SetDetailMode(bool isEditing)
        {
            bool canEdit = UserSession.HasPermission(SystemMenus.Ticket, PermissionAction.Edit);
            bool canDelete = UserSession.HasPermission(SystemMenus.Ticket, PermissionAction.Delete);

            bool fieldsEnabled = isEditing || _isAdding;

            txtCustomerName.ReadOnly = !fieldsEnabled;
            txtCustomerPhone.ReadOnly = !fieldsEnabled;
            txtSeatNumber.ReadOnly = !fieldsEnabled;
            numPrice.Enabled = fieldsEnabled;
            dtpDeparture.Enabled = fieldsEnabled;
            cboDetailRoute.Enabled = fieldsEnabled;
            cboDetailVehicle.Enabled = fieldsEnabled;
            cboDetailPayStatus.Enabled = fieldsEnabled;
            cboDetailTicketStatus.Enabled = fieldsEnabled && canEdit;

            btnSave.Enabled = fieldsEnabled;
            btnCancel.Enabled = fieldsEnabled;
            btnDelete.Enabled = !fieldsEnabled && _selectedTicketId.HasValue && canDelete;
            btnCancelTicket.Enabled = !fieldsEnabled && _selectedTicketId.HasValue && canEdit;
        }

        private void StartAdd()
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Ticket, PermissionAction.Add)) return;

            _isAdding = true;
            _selectedTicketId = null;
            dgvTickets.ClearSelection();

            lblDetailTitle.Text = "THÊM VÉ MỚI";
            txtTicketCode.Text = "(Tự động sinh)";
            txtCustomerName.Text = "";
            txtCustomerPhone.Text = "";
            txtSeatNumber.Text = "";
            numPrice.Value = 0;
            dtpDeparture.Value = DateTime.Now.AddHours(2);
            if (cboDetailRoute.Items.Count > 0) cboDetailRoute.SelectedIndex = 0;
            cboDetailPayStatus.SelectedIndex = 0;
            cboDetailTicketStatus.SelectedIndex = 0;

            SetDetailMode(isEditing: true);
            txtCustomerName.Focus();
        }

        private void CancelEdit()
        {
            _isAdding = false;
            lblDetailTitle.Text = "CHI TIẾT VÉ XE";
            SetDetailMode(isEditing: false);
            dgvTickets.ClearSelection();
            ClearDetailPanel();
        }

        private void ClearDetailPanel()
        {
            txtTicketCode.Text = "";
            txtCustomerName.Text = "";
            txtCustomerPhone.Text = "";
            txtSeatNumber.Text = "";
            numPrice.Value = 0;
            cboDetailPayStatus.SelectedIndex = 0;
            cboDetailTicketStatus.SelectedIndex = 0;
        }

        private async Task LoadVehiclesForSelectedRouteAsync()
        {
            cboDetailVehicle.Items.Clear();

            if (cboDetailRoute.SelectedIndex < 0 || cboDetailRoute.SelectedIndex >= _routes.Count)
            {
                cboDetailVehicle.Items.Add("-- Chọn tuyến xe trước --");
                cboDetailVehicle.SelectedIndex = 0;
                return;
            }

            int routeId = _routes[cboDetailRoute.SelectedIndex].Id;
            _vehiclesForRoute = await _ticketService.GetVehiclesByRouteAsync(routeId);

            cboDetailVehicle.Items.Clear();
            if (_vehiclesForRoute.Count == 0)
            {
                cboDetailVehicle.Items.Add("-- Không có phương tiện --");
            }
            else
            {
                foreach (var v in _vehiclesForRoute)
                    cboDetailVehicle.Items.Add($"{v.LicensePlate} ({v.VehicleType} - {v.TotalSeats} chỗ)");
            }
            if (cboDetailVehicle.Items.Count > 0)
                cboDetailVehicle.SelectedIndex = 0;
        }

        // ==========================================
        // CRUD OPERATIONS
        // ==========================================
        private async Task SaveAsync()
        {
            if (!ValidateInput()) return;

            int routeIdx = cboDetailRoute.SelectedIndex;
            int vehicleIdx = cboDetailVehicle.SelectedIndex;

            if (routeIdx < 0 || routeIdx >= _routes.Count)
            {
                MessageBox.Show("Vui lòng chọn tuyến xe!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (vehicleIdx < 0 || vehicleIdx >= _vehiclesForRoute.Count)
            {
                MessageBox.Show("Vui lòng chọn phương tiện!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ticket = new Ticket
            {
                Id = _selectedTicketId ?? 0,
                CustomerName = txtCustomerName.Text.Trim(),
                CustomerPhone = txtCustomerPhone.Text.Trim(),
                SeatNumber = txtSeatNumber.Text.Trim(),
                Price = numPrice.Value,
                DepartureTime = dtpDeparture.Value,
                RouteId = _routes[routeIdx].Id,
                VehicleId = _vehiclesForRoute[vehicleIdx].Id,
                PaymentStatus = cboDetailPayStatus.SelectedItem?.ToString() ?? "Pending",
                TicketStatus = cboDetailTicketStatus.SelectedItem?.ToString() ?? "Booked",
                AccountId = UserSession.CurrentUserId > 0 ? UserSession.CurrentUserId : null
            };

            if (_isAdding)
            {
                if (!AuthorizationGuard.CheckAccess(SystemMenus.Ticket, PermissionAction.Add)) return;
                var (ok, msg, created) = await _ticketService.CreateTicketAsync(ticket);
                if (ok)
                {
                    _ = WriteAuditAsync("Thêm", created!.Id.ToString(), $"Thêm vé [{created.TicketCode}] cho [{created.CustomerName}]");
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isAdding = false;
                    lblDetailTitle.Text = "CHI TIẾT VÉ XE";
                    await RefreshGridAsync();
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (_selectedTicketId.HasValue)
            {
                if (!AuthorizationGuard.CheckAccess(SystemMenus.Ticket, PermissionAction.Edit)) return;
                ticket.Id = _selectedTicketId.Value;
                var (ok, msg) = await _ticketService.UpdateTicketAsync(ticket);
                if (ok)
                {
                    _ = WriteAuditAsync("Sửa", ticket.Id.ToString(), $"Cập nhật vé #{ticket.Id}");
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
            if (!_selectedTicketId.HasValue) return;
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Ticket, PermissionAction.Delete)) return;

            var result = MessageBox.Show(
                $"Xóa vé #{_selectedTicketId}? Hành động này không thể hoàn tác.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            var (ok, msg) = await _ticketService.DeleteTicketAsync(_selectedTicketId.Value);
            if (ok)
            {
                _ = WriteAuditAsync("Xóa", _selectedTicketId.Value.ToString(), $"Xóa vé #{_selectedTicketId}");
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _selectedTicketId = null;
                ClearDetailPanel();
                await RefreshGridAsync();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CancelTicketAsync()
        {
            if (!_selectedTicketId.HasValue) return;
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Ticket, PermissionAction.Edit)) return;

            var result = MessageBox.Show(
                "Bạn có chắc muốn hủy vé này? Trạng thái sẽ chuyển sang Cancelled.",
                "Xác nhận hủy vé",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            var (ok, msg) = await _ticketService.CancelTicketAsync(_selectedTicketId.Value);
            if (ok)
            {
                _ = WriteAuditAsync("Hủy vé", _selectedTicketId.Value.ToString(), msg);
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await RefreshGridAsync();
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
                await DataExportHelper.ExportDataGridViewWithDialogAsync(dgvTickets, "DanhSach_Ve");
            }
            finally
            {
                AuthorizationGuard.ApplyControlSecurity(SystemMenus.Ticket, btnExport: btnExport);
            }
        }

        // ==========================================
        // VALIDATION
        // ==========================================
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên hành khách!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCustomerPhone.Text) || txtCustomerPhone.Text.Length < 9)
            {
                MessageBox.Show("Số điện thoại không hợp lệ!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerPhone.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSeatNumber.Text))
            {
                MessageBox.Show("Vui lòng nhập số ghế!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSeatNumber.Focus();
                return false;
            }

            if (numPrice.Value <= 0)
            {
                MessageBox.Show("Giá vé phải lớn hơn 0!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numPrice.Focus();
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
                action, "Ticket", recordId,
                $"{details} | Người thực hiện: {UserSession.CurrentUsername} | {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
        }
    }
}
