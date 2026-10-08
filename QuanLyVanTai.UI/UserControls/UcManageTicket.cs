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
        // UI CONTROLS - HEADER
        // ==========================================
        private readonly Panel pnlHeader = new();

        // ==========================================
        // UI CONTROLS - STATS
        // ==========================================
        private readonly Panel pnlStats = new();
        private Label lblStatTotal = new();
        private Label lblStatBooked = new();
        private Label lblStatPaid = new();
        private Label lblStatCancelled = new();

        // ==========================================
        // UI CONTROLS - TOOLBAR / FILTER
        // ==========================================
        private readonly Panel pnlToolbar = new();
        private readonly TextBox txtKeyword = new();
        private readonly ComboBox cboRoute = new();
        private readonly ComboBox cboPayStatus = new();
        private readonly ComboBox cboTicketStatus = new();
        private readonly DateTimePicker dtpFrom = new();
        private readonly DateTimePicker dtpTo = new();
        private readonly CheckBox chkUseDateFilter = new();
        private readonly IconButton btnRefresh = new();
        private readonly IconButton btnAdd = new();
        private readonly IconButton btnExport = new();

        // ==========================================
        // UI CONTROLS - DATAGRIDVIEW
        // ==========================================
        private readonly SplitContainer splitMain = new();
        private readonly DataGridView dgvTickets = new();

        // ==========================================
        // UI CONTROLS - DETAIL PANEL
        // ==========================================
        private readonly Panel pnlDetail = new();
        private readonly Label lblDetailTitle = new();
        private readonly TextBox txtTicketCode = new();
        private readonly TextBox txtCustomerName = new();
        private readonly TextBox txtCustomerPhone = new();
        private readonly TextBox txtSeatNumber = new();
        private readonly NumericUpDown numPrice = new();
        private readonly DateTimePicker dtpDeparture = new();
        private readonly ComboBox cboDetailRoute = new();
        private readonly ComboBox cboDetailVehicle = new();
        private readonly ComboBox cboDetailPayStatus = new();
        private readonly ComboBox cboDetailTicketStatus = new();
        private readonly IconButton btnSave = new();
        private readonly IconButton btnCancel = new();
        private readonly IconButton btnDelete = new();
        private readonly IconButton btnCancelTicket = new();

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
            if (!AuthorizationGuard.GuardFormAccess(this, SystemMenus.Ticket))
                return;

            BuildUI();
            WireEvents();
            ApplySecurity();
            _ = LoadInitialDataAsync();
        }

        // ==========================================
        // BUILD UI
        // ==========================================
        private void BuildUI()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = ThemeConfig.BackgroundColor;

            // --- HEADER ---
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 50;
            pnlHeader.BackColor = Color.White;
            pnlHeader.Padding = new Padding(20, 12, 20, 12);

            var lblTitle = new Label
            {
                Text = "🎫 QUẢN LÝ VÉ XE",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = ThemeConfig.TextMain,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);

            // --- STATS CARDS ---
            pnlStats.Dock = DockStyle.Top;
            pnlStats.Height = 80;
            pnlStats.BackColor = ThemeConfig.BackgroundColor;
            pnlStats.Padding = new Padding(15, 8, 15, 8);

            var cardTotal = CreateStatCard("📋 Tổng vé", "0", Color.FromArgb(59, 130, 246), 0, out lblStatTotal);
            var cardBooked = CreateStatCard("🟡 Đã đặt", "0", Color.FromArgb(245, 158, 11), 230, out lblStatBooked);
            var cardPaid = CreateStatCard("✅ Đã thanh toán", "0", Color.FromArgb(16, 185, 129), 460, out lblStatPaid);
            var cardCancelled = CreateStatCard("❌ Đã hủy", "0", Color.FromArgb(239, 68, 68), 690, out lblStatCancelled);
            pnlStats.Controls.AddRange([cardTotal, cardBooked, cardPaid, cardCancelled]);
            this.Controls.Add(pnlStats);

            // --- TOOLBAR ---
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Height = 90;
            pnlToolbar.BackColor = Color.White;
            pnlToolbar.Padding = new Padding(15, 8, 15, 8);

            // ROW 1: Search + Filters
            txtKeyword.Location = new Point(15, 8);
            txtKeyword.Size = new Size(200, 30);
            txtKeyword.PlaceholderText = "🔍 Mã vé, tên KH, SĐT...";

            cboRoute.Location = new Point(225, 8);
            cboRoute.Size = new Size(160, 30);
            cboRoute.DropDownStyle = ComboBoxStyle.DropDownList;

            cboPayStatus.Location = new Point(395, 8);
            cboPayStatus.Size = new Size(120, 30);
            cboPayStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPayStatus.Items.AddRange(["Tất cả", "Pending", "Paid", "Refunded"]);
            cboPayStatus.SelectedIndex = 0;

            cboTicketStatus.Location = new Point(525, 8);
            cboTicketStatus.Size = new Size(120, 30);
            cboTicketStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTicketStatus.Items.AddRange(["Tất cả", "Booked", "Completed", "Cancelled"]);
            cboTicketStatus.SelectedIndex = 0;

            // ROW 2: Date filter + Buttons
            chkUseDateFilter.Text = "Ngày khởi hành:";
            chkUseDateFilter.Location = new Point(15, 46);
            chkUseDateFilter.Size = new Size(140, 26);
            chkUseDateFilter.Font = new Font("Segoe UI", 9F);

            dtpFrom.Location = new Point(160, 44);
            dtpFrom.Size = new Size(140, 28);
            dtpFrom.Format = DateTimePickerFormat.Short;
            dtpFrom.Enabled = false;

            var lblTo = new Label { Text = "→", Location = new Point(308, 48), AutoSize = true };
            pnlToolbar.Controls.Add(lblTo);

            dtpTo.Location = new Point(325, 44);
            dtpTo.Size = new Size(140, 28);
            dtpTo.Format = DateTimePickerFormat.Short;
            dtpTo.Enabled = false;

            ThemeConfig.StyleSecondaryButton(btnRefresh, IconChar.RotateLeft);
            btnRefresh.Text = " Làm mới";
            btnRefresh.Size = new Size(105, 32);
            btnRefresh.Location = new Point(480, 42);

            ThemeConfig.StyleSuccessButton(btnAdd, IconChar.Plus);
            btnAdd.Text = " Thêm vé";
            btnAdd.Size = new Size(110, 32);
            btnAdd.Location = new Point(595, 42);

            ThemeConfig.StyleSecondaryButton(btnExport, IconChar.FileExcel);
            btnExport.Text = " Xuất file";
            btnExport.BackColor = Color.FromArgb(16, 185, 129);
            btnExport.Size = new Size(110, 32);
            btnExport.Location = new Point(715, 42);

            pnlToolbar.Controls.AddRange([
                txtKeyword, cboRoute, cboPayStatus, cboTicketStatus,
                chkUseDateFilter, dtpFrom, dtpTo, btnRefresh, btnAdd, btnExport
            ]);
            this.Controls.Add(pnlToolbar);

            // --- MAIN SPLIT (DataGrid + Detail) ---
            splitMain.Dock = DockStyle.Fill;
            splitMain.SplitterDistance = 720;
            splitMain.BackColor = Color.FromArgb(226, 232, 240);

            // DataGridView
            dgvTickets.Dock = DockStyle.Fill;
            dgvTickets.BackgroundColor = Color.White;
            dgvTickets.BorderStyle = BorderStyle.None;
            dgvTickets.AllowUserToAddRows = false;
            dgvTickets.AllowUserToDeleteRows = false;
            dgvTickets.ReadOnly = true;
            dgvTickets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTickets.MultiSelect = false;
            dgvTickets.RowHeadersVisible = false;
            dgvTickets.RowTemplate.Height = 42;
            dgvTickets.ColumnHeadersHeight = 42;
            dgvTickets.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvTickets.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgvTickets.ColumnHeadersDefaultCellStyle.ForeColor = ThemeConfig.TextMain;
            dgvTickets.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvTickets.EnableHeadersVisualStyles = false;
            dgvTickets.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvTickets.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvTickets.DefaultCellStyle.SelectionForeColor = Color.Black;

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

            DataGridViewHighlightHelper.AttachSearchHighlighter(dgvTickets, () => txtKeyword.Text);
            splitMain.Panel1.Controls.Add(dgvTickets);

            // Detail Panel
            BuildDetailPanel();
            splitMain.Panel2.Controls.Add(pnlDetail);

            this.Controls.Add(splitMain);
        }

        private void BuildDetailPanel()
        {
            pnlDetail.Dock = DockStyle.Fill;
            pnlDetail.BackColor = Color.White;
            pnlDetail.Padding = new Padding(18);
            pnlDetail.AutoScroll = true;

            lblDetailTitle.Text = "CHI TIẾT VÉ XE";
            lblDetailTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDetailTitle.ForeColor = ThemeConfig.PrimaryColor;
            lblDetailTitle.Location = new Point(18, 15);
            lblDetailTitle.AutoSize = true;
            pnlDetail.Controls.Add(lblDetailTitle);

            int y = 50;

            // Mã vé (readonly)
            AddDetailLabel("Mã Vé:", y, pnlDetail);
            txtTicketCode.Location = new Point(18, y + 22);
            txtTicketCode.Size = new Size(330, 30);
            txtTicketCode.ReadOnly = true;
            txtTicketCode.BackColor = Color.FromArgb(241, 245, 249);
            pnlDetail.Controls.Add(txtTicketCode);
            y += 60;

            // Tên hành khách
            AddDetailLabel("Tên Hành Khách *:", y, pnlDetail);
            txtCustomerName.Location = new Point(18, y + 22);
            txtCustomerName.Size = new Size(330, 30);
            pnlDetail.Controls.Add(txtCustomerName);
            y += 60;

            // SĐT
            AddDetailLabel("Số Điện Thoại *:", y, pnlDetail);
            txtCustomerPhone.Location = new Point(18, y + 22);
            txtCustomerPhone.Size = new Size(160, 30);
            pnlDetail.Controls.Add(txtCustomerPhone);

            AddDetailLabel("Số Ghế *:", y, pnlDetail, 195);
            txtSeatNumber.Location = new Point(195, y + 22);
            txtSeatNumber.Size = new Size(153, 30);
            txtSeatNumber.PlaceholderText = "VD: A01";
            pnlDetail.Controls.Add(txtSeatNumber);
            y += 60;

            // Tuyến xe
            AddDetailLabel("Tuyến Xe *:", y, pnlDetail);
            cboDetailRoute.Location = new Point(18, y + 22);
            cboDetailRoute.Size = new Size(330, 30);
            cboDetailRoute.DropDownStyle = ComboBoxStyle.DropDownList;
            pnlDetail.Controls.Add(cboDetailRoute);
            y += 60;

            // Phương tiện
            AddDetailLabel("Phương Tiện *:", y, pnlDetail);
            cboDetailVehicle.Location = new Point(18, y + 22);
            cboDetailVehicle.Size = new Size(330, 30);
            cboDetailVehicle.DropDownStyle = ComboBoxStyle.DropDownList;
            pnlDetail.Controls.Add(cboDetailVehicle);
            y += 60;

            // Giá vé
            AddDetailLabel("Giá Vé (VNĐ) *:", y, pnlDetail);
            numPrice.Location = new Point(18, y + 22);
            numPrice.Size = new Size(160, 30);
            numPrice.Maximum = 10_000_000;
            numPrice.Minimum = 0;
            numPrice.DecimalPlaces = 0;
            numPrice.ThousandsSeparator = true;
            numPrice.Increment = 10000;
            pnlDetail.Controls.Add(numPrice);
            y += 60;

            // Ngày khởi hành
            AddDetailLabel("Ngày & Giờ Khởi Hành *:", y, pnlDetail);
            dtpDeparture.Location = new Point(18, y + 22);
            dtpDeparture.Size = new Size(240, 30);
            dtpDeparture.Format = DateTimePickerFormat.Custom;
            dtpDeparture.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpDeparture.Value = DateTime.Now.AddHours(1);
            pnlDetail.Controls.Add(dtpDeparture);
            y += 60;

            // Trạng thái thanh toán
            AddDetailLabel("Thanh Toán:", y, pnlDetail);
            cboDetailPayStatus.Location = new Point(18, y + 22);
            cboDetailPayStatus.Size = new Size(155, 30);
            cboDetailPayStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDetailPayStatus.Items.AddRange(["Pending", "Paid", "Refunded"]);
            cboDetailPayStatus.SelectedIndex = 0;
            pnlDetail.Controls.Add(cboDetailPayStatus);

            AddDetailLabel("Trạng Thái Vé:", y, pnlDetail, 185);
            cboDetailTicketStatus.Location = new Point(185, y + 22);
            cboDetailTicketStatus.Size = new Size(163, 30);
            cboDetailTicketStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDetailTicketStatus.Items.AddRange(["Booked", "Completed", "Cancelled"]);
            cboDetailTicketStatus.SelectedIndex = 0;
            pnlDetail.Controls.Add(cboDetailTicketStatus);
            y += 68;

            // Action Buttons
            ThemeConfig.StylePrimaryButton(btnSave, IconChar.FloppyDisk);
            btnSave.Text = " Lưu";
            btnSave.Size = new Size(82, 36);
            btnSave.Location = new Point(12, y);
            btnSave.Enabled = false;

            ThemeConfig.StyleSecondaryButton(btnCancel, IconChar.Xmark);
            btnCancel.Text = " Hủy";
            btnCancel.Size = new Size(82, 36);
            btnCancel.Location = new Point(100, y);
            btnCancel.Enabled = false;

            ThemeConfig.StyleDangerButton(btnDelete, IconChar.Trash);
            btnDelete.Text = " Xóa";
            btnDelete.Size = new Size(82, 36);
            btnDelete.Location = new Point(188, y);
            btnDelete.Enabled = false;

            ThemeConfig.StyleSecondaryButton(btnCancelTicket, IconChar.Ban);
            btnCancelTicket.Text = " Hủy Vé";
            btnCancelTicket.BackColor = Color.FromArgb(245, 158, 11);
            btnCancelTicket.Size = new Size(95, 36);
            btnCancelTicket.Location = new Point(276, y);
            btnCancelTicket.Enabled = false;

            pnlDetail.Controls.AddRange([btnSave, btnCancel, btnDelete, btnCancelTicket]);
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

                var criteria = new TicketFilterCriteria
                {
                    Keyword = txtKeyword.Text,
                    RouteId = routeId,
                    PaymentStatus = cboPayStatus.SelectedItem?.ToString(),
                    TicketStatus = cboTicketStatus.SelectedItem?.ToString(),
                    DepartureFrom = chkUseDateFilter.Checked ? dtpFrom.Value.Date : null,
                    DepartureTo = chkUseDateFilter.Checked ? dtpTo.Value.Date : null
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
