namespace QuanLyVanTai.UI.UserControls
{
    partial class UcDashboard
    {
        private System.ComponentModel.IContainer components = null;

        // Root & Structure Containers
        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblDashboardTitle;
        private System.Windows.Forms.Label lblDashboardDate;

        // Filter Bar
        private System.Windows.Forms.Panel pnlFilterBar;
        private System.Windows.Forms.Label lblFilterRange;
        private System.Windows.Forms.ComboBox cboDateFilter;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private FontAwesome.Sharp.IconButton btnApplyFilter;
        private FontAwesome.Sharp.IconButton btnRefresh;
        private FontAwesome.Sharp.IconButton btnToggleMode;

        // Stat Cards Flow
        private System.Windows.Forms.FlowLayoutPanel flpStatCards;
        private System.Windows.Forms.Panel pnlCardRevenue;
        private System.Windows.Forms.Panel pnlCardRevenueBar;
        private FontAwesome.Sharp.IconPictureBox picCardRevenue;
        private System.Windows.Forms.Label lblCardRevenueTitle;
        private System.Windows.Forms.Label lblStatRevenue;
        private System.Windows.Forms.Label lblCardRevenueSub;

        private System.Windows.Forms.Panel pnlCardTickets;
        private System.Windows.Forms.Panel pnlCardTicketsBar;
        private FontAwesome.Sharp.IconPictureBox picCardTickets;
        private System.Windows.Forms.Label lblCardTicketsTitle;
        private System.Windows.Forms.Label lblStatTickets;
        private System.Windows.Forms.Label lblCardTicketsSub;

        private System.Windows.Forms.Panel pnlCardRoutes;
        private System.Windows.Forms.Panel pnlCardRoutesBar;
        private FontAwesome.Sharp.IconPictureBox picCardRoutes;
        private System.Windows.Forms.Label lblCardRoutesTitle;
        private System.Windows.Forms.Label lblStatRoutes;
        private System.Windows.Forms.Label lblCardRoutesSub;

        private System.Windows.Forms.Panel pnlCardVehicles;
        private System.Windows.Forms.Panel pnlCardVehiclesBar;
        private FontAwesome.Sharp.IconPictureBox picCardVehicles;
        private System.Windows.Forms.Label lblCardVehiclesTitle;
        private System.Windows.Forms.Label lblStatVehicles;
        private System.Windows.Forms.Label lblCardVehiclesSub;

        // Main Content Area (Charts + Table)
        private System.Windows.Forms.TableLayoutPanel tblContent;
        private System.Windows.Forms.TableLayoutPanel tblCharts;

        // Time Chart (Left)
        private System.Windows.Forms.Panel pnlTimeChartWrap;
        private System.Windows.Forms.Label lblTimeChartTitle;
        private System.Windows.Forms.Label lblTimeChartSub;
        private System.Windows.Forms.Panel pnlTimeChartContainer;
        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart cartesianChartTime;
        private System.Windows.Forms.Panel pnlTimeChartGdi;

        // Route Chart (Right)
        private System.Windows.Forms.Panel pnlRouteChartWrap;
        private System.Windows.Forms.Label lblRouteChartTitle;
        private System.Windows.Forms.Label lblRouteChartSub;
        private System.Windows.Forms.Panel pnlRouteChartContainer;
        private LiveChartsCore.SkiaSharpView.WinForms.PieChart pieChartRoutes;
        private System.Windows.Forms.Panel pnlRouteChartGdi;

        // Recent Transactions Table (Bottom)
        private System.Windows.Forms.Panel pnlTableWrap;
        private System.Windows.Forms.Panel pnlTableHeader;
        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.Label lblTableSub;
        private System.Windows.Forms.DataGridView dgvRecentTransactions;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTicketCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRouteName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVehiclePlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSeatNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDepartureTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentStatus;

        // Footer
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblFooterStatus;
        private System.Windows.Forms.Label lblFooterRecords;
        private System.Windows.Forms.Label lblFooterTime;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dgvHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvRowStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvPriceStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvDateStyle = new System.Windows.Forms.DataGridViewCellStyle();

            tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            pnlHeader = new System.Windows.Forms.Panel();
            lblDashboardTitle = new System.Windows.Forms.Label();
            lblDashboardDate = new System.Windows.Forms.Label();

            pnlFilterBar = new System.Windows.Forms.Panel();
            lblFilterRange = new System.Windows.Forms.Label();
            cboDateFilter = new System.Windows.Forms.ComboBox();
            lblFromDate = new System.Windows.Forms.Label();
            dtpFromDate = new System.Windows.Forms.DateTimePicker();
            lblToDate = new System.Windows.Forms.Label();
            dtpToDate = new System.Windows.Forms.DateTimePicker();
            btnApplyFilter = new FontAwesome.Sharp.IconButton();
            btnRefresh = new FontAwesome.Sharp.IconButton();
            btnToggleMode = new FontAwesome.Sharp.IconButton();

            flpStatCards = new System.Windows.Forms.FlowLayoutPanel();

            // Card 1: Revenue
            pnlCardRevenue = new System.Windows.Forms.Panel();
            pnlCardRevenueBar = new System.Windows.Forms.Panel();
            picCardRevenue = new FontAwesome.Sharp.IconPictureBox();
            lblCardRevenueTitle = new System.Windows.Forms.Label();
            lblStatRevenue = new System.Windows.Forms.Label();
            lblCardRevenueSub = new System.Windows.Forms.Label();

            // Card 2: Tickets
            pnlCardTickets = new System.Windows.Forms.Panel();
            pnlCardTicketsBar = new System.Windows.Forms.Panel();
            picCardTickets = new FontAwesome.Sharp.IconPictureBox();
            lblCardTicketsTitle = new System.Windows.Forms.Label();
            lblStatTickets = new System.Windows.Forms.Label();
            lblCardTicketsSub = new System.Windows.Forms.Label();

            // Card 3: Routes
            pnlCardRoutes = new System.Windows.Forms.Panel();
            pnlCardRoutesBar = new System.Windows.Forms.Panel();
            picCardRoutes = new FontAwesome.Sharp.IconPictureBox();
            lblCardRoutesTitle = new System.Windows.Forms.Label();
            lblStatRoutes = new System.Windows.Forms.Label();
            lblCardRoutesSub = new System.Windows.Forms.Label();

            // Card 4: Vehicles
            pnlCardVehicles = new System.Windows.Forms.Panel();
            pnlCardVehiclesBar = new System.Windows.Forms.Panel();
            picCardVehicles = new FontAwesome.Sharp.IconPictureBox();
            lblCardVehiclesTitle = new System.Windows.Forms.Label();
            lblStatVehicles = new System.Windows.Forms.Label();
            lblCardVehiclesSub = new System.Windows.Forms.Label();

            // Content
            tblContent = new System.Windows.Forms.TableLayoutPanel();
            tblCharts = new System.Windows.Forms.TableLayoutPanel();

            pnlTimeChartWrap = new System.Windows.Forms.Panel();
            lblTimeChartTitle = new System.Windows.Forms.Label();
            lblTimeChartSub = new System.Windows.Forms.Label();
            pnlTimeChartContainer = new System.Windows.Forms.Panel();
            cartesianChartTime = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            pnlTimeChartGdi = new System.Windows.Forms.Panel();

            pnlRouteChartWrap = new System.Windows.Forms.Panel();
            lblRouteChartTitle = new System.Windows.Forms.Label();
            lblRouteChartSub = new System.Windows.Forms.Label();
            pnlRouteChartContainer = new System.Windows.Forms.Panel();
            pieChartRoutes = new LiveChartsCore.SkiaSharpView.WinForms.PieChart();
            pnlRouteChartGdi = new System.Windows.Forms.Panel();

            pnlTableWrap = new System.Windows.Forms.Panel();
            pnlTableHeader = new System.Windows.Forms.Panel();
            lblTableTitle = new System.Windows.Forms.Label();
            lblTableSub = new System.Windows.Forms.Label();
            dgvRecentTransactions = new System.Windows.Forms.DataGridView();
            colTicketCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colCustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colCustomerPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colRouteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colVehiclePlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSeatNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colDepartureTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPaymentStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();

            pnlFooter = new System.Windows.Forms.Panel();
            lblFooterStatus = new System.Windows.Forms.Label();
            lblFooterRecords = new System.Windows.Forms.Label();
            lblFooterTime = new System.Windows.Forms.Label();

            tlpRoot.SuspendLayout();
            pnlHeader.SuspendLayout();
            pnlFilterBar.SuspendLayout();
            flpStatCards.SuspendLayout();

            pnlCardRevenue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCardRevenue).BeginInit();
            pnlCardTickets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCardTickets).BeginInit();
            pnlCardRoutes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCardRoutes).BeginInit();
            pnlCardVehicles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCardVehicles).BeginInit();

            tblContent.SuspendLayout();
            tblCharts.SuspendLayout();
            pnlTimeChartWrap.SuspendLayout();
            pnlTimeChartContainer.SuspendLayout();
            pnlRouteChartWrap.SuspendLayout();
            pnlRouteChartContainer.SuspendLayout();
            pnlTableWrap.SuspendLayout();
            pnlTableHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentTransactions).BeginInit();
            pnlFooter.SuspendLayout();
            SuspendLayout();

            // 
            // tlpRoot
            // 
            tlpRoot.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            tlpRoot.ColumnCount = 1;
            tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpRoot.Controls.Add(pnlHeader, 0, 0);
            tlpRoot.Controls.Add(pnlFilterBar, 0, 1);
            tlpRoot.Controls.Add(flpStatCards, 0, 2);
            tlpRoot.Controls.Add(tblContent, 0, 3);
            tlpRoot.Controls.Add(pnlFooter, 0, 4);
            tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            tlpRoot.Location = new System.Drawing.Point(0, 0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 5;
            tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            tlpRoot.Size = new System.Drawing.Size(1260, 840);
            tlpRoot.TabIndex = 0;

            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = System.Drawing.Color.White;
            pnlHeader.Controls.Add(lblDashboardTitle);
            pnlHeader.Controls.Add(lblDashboardDate);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Margin = new System.Windows.Forms.Padding(0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            pnlHeader.Size = new System.Drawing.Size(1260, 68);
            pnlHeader.TabIndex = 0;

            // 
            // lblDashboardTitle
            // 
            lblDashboardTitle.AutoSize = true;
            lblDashboardTitle.Font = new System.Drawing.Font("Segoe UI", 14.5F, System.Drawing.FontStyle.Bold);
            lblDashboardTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblDashboardTitle.Location = new System.Drawing.Point(18, 11);
            lblDashboardTitle.Name = "lblDashboardTitle";
            lblDashboardTitle.Size = new System.Drawing.Size(650, 33);
            lblDashboardTitle.TabIndex = 0;
            lblDashboardTitle.Text = "TỔNG QUAN ĐIỀU HÀNH & PHÂN TÍCH DOANH THU (ADMIN)";

            // 
            // lblDashboardDate
            // 
            lblDashboardDate.AutoSize = true;
            lblDashboardDate.Font = new System.Drawing.Font("Segoe UI", 8.8F);
            lblDashboardDate.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblDashboardDate.Location = new System.Drawing.Point(20, 42);
            lblDashboardDate.Name = "lblDashboardDate";
            lblDashboardDate.Size = new System.Drawing.Size(430, 20);
            lblDashboardDate.TabIndex = 1;
            lblDashboardDate.Text = "Thống kê biểu đồ thời gian thực dựa trên dữ liệu giao dịch vé thực tế";

            // 
            // pnlFilterBar
            // 
            pnlFilterBar.BackColor = System.Drawing.Color.White;
            pnlFilterBar.Controls.Add(lblFilterRange);
            pnlFilterBar.Controls.Add(cboDateFilter);
            pnlFilterBar.Controls.Add(lblFromDate);
            pnlFilterBar.Controls.Add(dtpFromDate);
            pnlFilterBar.Controls.Add(lblToDate);
            pnlFilterBar.Controls.Add(dtpToDate);
            pnlFilterBar.Controls.Add(btnApplyFilter);
            pnlFilterBar.Controls.Add(btnRefresh);
            pnlFilterBar.Controls.Add(btnToggleMode);
            pnlFilterBar.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlFilterBar.Location = new System.Drawing.Point(0, 68);
            pnlFilterBar.Margin = new System.Windows.Forms.Padding(0);
            pnlFilterBar.Name = "pnlFilterBar";
            pnlFilterBar.Padding = new System.Windows.Forms.Padding(20, 6, 20, 6);
            pnlFilterBar.Size = new System.Drawing.Size(1260, 46);
            pnlFilterBar.TabIndex = 1;

            // 
            // lblFilterRange
            // 
            lblFilterRange.AutoSize = true;
            lblFilterRange.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblFilterRange.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblFilterRange.Location = new System.Drawing.Point(18, 12);
            lblFilterRange.Name = "lblFilterRange";
            lblFilterRange.Size = new System.Drawing.Size(130, 20);
            lblFilterRange.TabIndex = 0;
            lblFilterRange.Text = "Mốc thời gian lọc:";

            // 
            // cboDateFilter
            // 
            cboDateFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboDateFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            cboDateFilter.FormattingEnabled = true;
            cboDateFilter.Items.AddRange(new object[] {
                "7 ngày gần nhất",
                "30 ngày gần nhất",
                "Tháng này",
                "Tùy chọn ngày"
            });
            cboDateFilter.Location = new System.Drawing.Point(152, 9);
            cboDateFilter.Name = "cboDateFilter";
            cboDateFilter.Size = new System.Drawing.Size(145, 28);
            cboDateFilter.TabIndex = 1;

            // 
            // lblFromDate
            // 
            lblFromDate.AutoSize = true;
            lblFromDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblFromDate.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblFromDate.Location = new System.Drawing.Point(310, 13);
            lblFromDate.Name = "lblFromDate";
            lblFromDate.Size = new System.Drawing.Size(29, 20);
            lblFromDate.TabIndex = 2;
            lblFromDate.Text = "Từ:";

            // 
            // dtpFromDate
            // 
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpFromDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtpFromDate.Location = new System.Drawing.Point(344, 9);
            dtpFromDate.Name = "dtpFromDate";
            dtpFromDate.Size = new System.Drawing.Size(120, 27);
            dtpFromDate.TabIndex = 3;

            // 
            // lblToDate
            // 
            lblToDate.AutoSize = true;
            lblToDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblToDate.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblToDate.Location = new System.Drawing.Point(474, 13);
            lblToDate.Name = "lblToDate";
            lblToDate.Size = new System.Drawing.Size(39, 20);
            lblToDate.TabIndex = 4;
            lblToDate.Text = "Đến:";

            // 
            // dtpToDate
            // 
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtpToDate.Location = new System.Drawing.Point(516, 9);
            dtpToDate.Name = "dtpToDate";
            dtpToDate.Size = new System.Drawing.Size(120, 27);
            dtpToDate.TabIndex = 5;

            // 
            // btnApplyFilter
            // 
            btnApplyFilter.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            btnApplyFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            btnApplyFilter.FlatAppearance.BorderSize = 0;
            btnApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnApplyFilter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnApplyFilter.ForeColor = System.Drawing.Color.White;
            btnApplyFilter.IconChar = FontAwesome.Sharp.IconChar.Filter;
            btnApplyFilter.IconColor = System.Drawing.Color.White;
            btnApplyFilter.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnApplyFilter.IconSize = 16;
            btnApplyFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnApplyFilter.Location = new System.Drawing.Point(648, 8);
            btnApplyFilter.Name = "btnApplyFilter";
            btnApplyFilter.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            btnApplyFilter.Size = new System.Drawing.Size(100, 29);
            btnApplyFilter.TabIndex = 6;
            btnApplyFilter.Text = "  Lọc";
            btnApplyFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            btnApplyFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnApplyFilter.UseVisualStyleBackColor = false;

            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnRefresh.ForeColor = System.Drawing.Color.White;
            btnRefresh.IconChar = FontAwesome.Sharp.IconChar.Rotate;
            btnRefresh.IconColor = System.Drawing.Color.White;
            btnRefresh.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRefresh.IconSize = 16;
            btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnRefresh.Location = new System.Drawing.Point(756, 8);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            btnRefresh.Size = new System.Drawing.Size(115, 29);
            btnRefresh.TabIndex = 7;
            btnRefresh.Text = "  Làm mới";
            btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnRefresh.UseVisualStyleBackColor = false;

            // 
            // btnToggleMode
            // 
            btnToggleMode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnToggleMode.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            btnToggleMode.Cursor = System.Windows.Forms.Cursors.Hand;
            btnToggleMode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnToggleMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnToggleMode.Font = new System.Drawing.Font("Segoe UI", 8.8F, System.Drawing.FontStyle.Bold);
            btnToggleMode.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            btnToggleMode.IconChar = FontAwesome.Sharp.IconChar.ChartLine;
            btnToggleMode.IconColor = System.Drawing.Color.FromArgb(16, 185, 129);
            btnToggleMode.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnToggleMode.IconSize = 18;
            btnToggleMode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnToggleMode.Location = new System.Drawing.Point(970, 7);
            btnToggleMode.Name = "btnToggleMode";
            btnToggleMode.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            btnToggleMode.Size = new System.Drawing.Size(270, 31);
            btnToggleMode.TabIndex = 8;
            btnToggleMode.Text = " Chế độ: LiveCharts2 (Hiện đại)";
            btnToggleMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnToggleMode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnToggleMode.UseVisualStyleBackColor = false;

            // 
            // flpStatCards
            // 
            flpStatCards.AutoScroll = true;
            flpStatCards.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            flpStatCards.Controls.Add(pnlCardRevenue);
            flpStatCards.Controls.Add(pnlCardTickets);
            flpStatCards.Controls.Add(pnlCardRoutes);
            flpStatCards.Controls.Add(pnlCardVehicles);
            flpStatCards.Dock = System.Windows.Forms.DockStyle.Fill;
            flpStatCards.Location = new System.Drawing.Point(0, 114);
            flpStatCards.Margin = new System.Windows.Forms.Padding(0);
            flpStatCards.Name = "flpStatCards";
            flpStatCards.Padding = new System.Windows.Forms.Padding(18, 8, 18, 6);
            flpStatCards.Size = new System.Drawing.Size(1260, 106);
            flpStatCards.TabIndex = 2;
            flpStatCards.WrapContents = false;

            // 
            // pnlCardRevenue
            // 
            pnlCardRevenue.BackColor = System.Drawing.Color.White;
            pnlCardRevenue.Controls.Add(pnlCardRevenueBar);
            pnlCardRevenue.Controls.Add(picCardRevenue);
            pnlCardRevenue.Controls.Add(lblCardRevenueTitle);
            pnlCardRevenue.Controls.Add(lblStatRevenue);
            pnlCardRevenue.Controls.Add(lblCardRevenueSub);
            pnlCardRevenue.Location = new System.Drawing.Point(21, 11);
            pnlCardRevenue.Margin = new System.Windows.Forms.Padding(3, 3, 14, 3);
            pnlCardRevenue.Name = "pnlCardRevenue";
            pnlCardRevenue.Size = new System.Drawing.Size(288, 84);
            pnlCardRevenue.TabIndex = 0;

            // 
            // pnlCardRevenueBar
            // 
            pnlCardRevenueBar.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            pnlCardRevenueBar.Dock = System.Windows.Forms.DockStyle.Left;
            pnlCardRevenueBar.Location = new System.Drawing.Point(0, 0);
            pnlCardRevenueBar.Name = "pnlCardRevenueBar";
            pnlCardRevenueBar.Size = new System.Drawing.Size(5, 84);
            pnlCardRevenueBar.TabIndex = 0;

            // 
            // picCardRevenue
            // 
            picCardRevenue.BackColor = System.Drawing.Color.FromArgb(236, 253, 245);
            picCardRevenue.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            picCardRevenue.IconChar = FontAwesome.Sharp.IconChar.MoneyBillWave;
            picCardRevenue.IconColor = System.Drawing.Color.FromArgb(16, 185, 129);
            picCardRevenue.IconFont = FontAwesome.Sharp.IconFont.Auto;
            picCardRevenue.IconSize = 28;
            picCardRevenue.Location = new System.Drawing.Point(14, 24);
            picCardRevenue.Name = "picCardRevenue";
            picCardRevenue.Padding = new System.Windows.Forms.Padding(4);
            picCardRevenue.Size = new System.Drawing.Size(38, 38);
            picCardRevenue.TabIndex = 1;
            picCardRevenue.TabStop = false;

            // 
            // lblCardRevenueTitle
            // 
            lblCardRevenueTitle.AutoSize = true;
            lblCardRevenueTitle.Font = new System.Drawing.Font("Segoe UI", 8.2F, System.Drawing.FontStyle.Bold);
            lblCardRevenueTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblCardRevenueTitle.Location = new System.Drawing.Point(58, 12);
            lblCardRevenueTitle.Name = "lblCardRevenueTitle";
            lblCardRevenueTitle.Size = new System.Drawing.Size(160, 19);
            lblCardRevenueTitle.TabIndex = 2;
            lblCardRevenueTitle.Text = "DOANH THU THỰC TẾ";

            // 
            // lblStatRevenue
            // 
            lblStatRevenue.AutoSize = true;
            lblStatRevenue.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            lblStatRevenue.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            lblStatRevenue.Location = new System.Drawing.Point(56, 31);
            lblStatRevenue.Name = "lblStatRevenue";
            lblStatRevenue.Size = new System.Drawing.Size(120, 31);
            lblStatRevenue.TabIndex = 3;
            lblStatRevenue.Text = "0 VNĐ";

            // 
            // lblCardRevenueSub
            // 
            lblCardRevenueSub.AutoSize = true;
            lblCardRevenueSub.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            lblCardRevenueSub.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblCardRevenueSub.Location = new System.Drawing.Point(58, 62);
            lblCardRevenueSub.Name = "lblCardRevenueSub";
            lblCardRevenueSub.Size = new System.Drawing.Size(175, 17);
            lblCardRevenueSub.TabIndex = 4;
            lblCardRevenueSub.Text = "Từ vé đã thanh toán (Paid)";

            // 
            // pnlCardTickets
            // 
            pnlCardTickets.BackColor = System.Drawing.Color.White;
            pnlCardTickets.Controls.Add(pnlCardTicketsBar);
            pnlCardTickets.Controls.Add(picCardTickets);
            pnlCardTickets.Controls.Add(lblCardTicketsTitle);
            pnlCardTickets.Controls.Add(lblStatTickets);
            pnlCardTickets.Controls.Add(lblCardTicketsSub);
            pnlCardTickets.Location = new System.Drawing.Point(326, 11);
            pnlCardTickets.Margin = new System.Windows.Forms.Padding(3, 3, 14, 3);
            pnlCardTickets.Name = "pnlCardTickets";
            pnlCardTickets.Size = new System.Drawing.Size(288, 84);
            pnlCardTickets.TabIndex = 1;

            // 
            // pnlCardTicketsBar
            // 
            pnlCardTicketsBar.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            pnlCardTicketsBar.Dock = System.Windows.Forms.DockStyle.Left;
            pnlCardTicketsBar.Location = new System.Drawing.Point(0, 0);
            pnlCardTicketsBar.Name = "pnlCardTicketsBar";
            pnlCardTicketsBar.Size = new System.Drawing.Size(5, 84);
            pnlCardTicketsBar.TabIndex = 0;

            // 
            // picCardTickets
            // 
            picCardTickets.BackColor = System.Drawing.Color.FromArgb(239, 246, 255);
            picCardTickets.ForeColor = System.Drawing.Color.FromArgb(59, 130, 246);
            picCardTickets.IconChar = FontAwesome.Sharp.IconChar.Ticket;
            picCardTickets.IconColor = System.Drawing.Color.FromArgb(59, 130, 246);
            picCardTickets.IconFont = FontAwesome.Sharp.IconFont.Auto;
            picCardTickets.IconSize = 28;
            picCardTickets.Location = new System.Drawing.Point(14, 24);
            picCardTickets.Name = "picCardTickets";
            picCardTickets.Padding = new System.Windows.Forms.Padding(4);
            picCardTickets.Size = new System.Drawing.Size(38, 38);
            picCardTickets.TabIndex = 1;
            picCardTickets.TabStop = false;

            // 
            // lblCardTicketsTitle
            // 
            lblCardTicketsTitle.AutoSize = true;
            lblCardTicketsTitle.Font = new System.Drawing.Font("Segoe UI", 8.2F, System.Drawing.FontStyle.Bold);
            lblCardTicketsTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblCardTicketsTitle.Location = new System.Drawing.Point(58, 12);
            lblCardTicketsTitle.Name = "lblCardTicketsTitle";
            lblCardTicketsTitle.Size = new System.Drawing.Size(140, 19);
            lblCardTicketsTitle.TabIndex = 2;
            lblCardTicketsTitle.Text = "TỔNG VÉ ĐÃ BÁN";

            // 
            // lblStatTickets
            // 
            lblStatTickets.AutoSize = true;
            lblStatTickets.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            lblStatTickets.ForeColor = System.Drawing.Color.FromArgb(59, 130, 246);
            lblStatTickets.Location = new System.Drawing.Point(56, 31);
            lblStatTickets.Name = "lblStatTickets";
            lblStatTickets.Size = new System.Drawing.Size(75, 31);
            lblStatTickets.TabIndex = 3;
            lblStatTickets.Text = "0 vé";

            // 
            // lblCardTicketsSub
            // 
            lblCardTicketsSub.AutoSize = true;
            lblCardTicketsSub.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            lblCardTicketsSub.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblCardTicketsSub.Location = new System.Drawing.Point(58, 62);
            lblCardTicketsSub.Name = "lblCardTicketsSub";
            lblCardTicketsSub.Size = new System.Drawing.Size(160, 17);
            lblCardTicketsSub.TabIndex = 4;
            lblCardTicketsSub.Text = "Giao dịch bán thành công";

            // 
            // pnlCardRoutes
            // 
            pnlCardRoutes.BackColor = System.Drawing.Color.White;
            pnlCardRoutes.Controls.Add(pnlCardRoutesBar);
            pnlCardRoutes.Controls.Add(picCardRoutes);
            pnlCardRoutes.Controls.Add(lblCardRoutesTitle);
            pnlCardRoutes.Controls.Add(lblStatRoutes);
            pnlCardRoutes.Controls.Add(lblCardRoutesSub);
            pnlCardRoutes.Location = new System.Drawing.Point(631, 11);
            pnlCardRoutes.Margin = new System.Windows.Forms.Padding(3, 3, 14, 3);
            pnlCardRoutes.Name = "pnlCardRoutes";
            pnlCardRoutes.Size = new System.Drawing.Size(288, 84);
            pnlCardRoutes.TabIndex = 2;

            // 
            // pnlCardRoutesBar
            // 
            pnlCardRoutesBar.BackColor = System.Drawing.Color.FromArgb(139, 92, 246);
            pnlCardRoutesBar.Dock = System.Windows.Forms.DockStyle.Left;
            pnlCardRoutesBar.Location = new System.Drawing.Point(0, 0);
            pnlCardRoutesBar.Name = "pnlCardRoutesBar";
            pnlCardRoutesBar.Size = new System.Drawing.Size(5, 84);
            pnlCardRoutesBar.TabIndex = 0;

            // 
            // picCardRoutes
            // 
            picCardRoutes.BackColor = System.Drawing.Color.FromArgb(245, 243, 255);
            picCardRoutes.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            picCardRoutes.IconChar = FontAwesome.Sharp.IconChar.Route;
            picCardRoutes.IconColor = System.Drawing.Color.FromArgb(139, 92, 246);
            picCardRoutes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            picCardRoutes.IconSize = 28;
            picCardRoutes.Location = new System.Drawing.Point(14, 24);
            picCardRoutes.Name = "picCardRoutes";
            picCardRoutes.Padding = new System.Windows.Forms.Padding(4);
            picCardRoutes.Size = new System.Drawing.Size(38, 38);
            picCardRoutes.TabIndex = 1;
            picCardRoutes.TabStop = false;

            // 
            // lblCardRoutesTitle
            // 
            lblCardRoutesTitle.AutoSize = true;
            lblCardRoutesTitle.Font = new System.Drawing.Font("Segoe UI", 8.2F, System.Drawing.FontStyle.Bold);
            lblCardRoutesTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblCardRoutesTitle.Location = new System.Drawing.Point(58, 12);
            lblCardRoutesTitle.Name = "lblCardRoutesTitle";
            lblCardRoutesTitle.Size = new System.Drawing.Size(155, 19);
            lblCardRoutesTitle.TabIndex = 2;
            lblCardRoutesTitle.Text = "TUYẾN KHAI THÁC";

            // 
            // lblStatRoutes
            // 
            lblStatRoutes.AutoSize = true;
            lblStatRoutes.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            lblStatRoutes.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246);
            lblStatRoutes.Location = new System.Drawing.Point(56, 31);
            lblStatRoutes.Name = "lblStatRoutes";
            lblStatRoutes.Size = new System.Drawing.Size(100, 31);
            lblStatRoutes.TabIndex = 3;
            lblStatRoutes.Text = "0 tuyến";

            // 
            // lblCardRoutesSub
            // 
            lblCardRoutesSub.AutoSize = true;
            lblCardRoutesSub.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            lblCardRoutesSub.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblCardRoutesSub.Location = new System.Drawing.Point(58, 62);
            lblCardRoutesSub.Name = "lblCardRoutesSub";
            lblCardRoutesSub.Size = new System.Drawing.Size(155, 17);
            lblCardRoutesSub.TabIndex = 4;
            lblCardRoutesSub.Text = "Lộ trình đang hoạt động";

            // 
            // pnlCardVehicles
            // 
            pnlCardVehicles.BackColor = System.Drawing.Color.White;
            pnlCardVehicles.Controls.Add(pnlCardVehiclesBar);
            pnlCardVehicles.Controls.Add(picCardVehicles);
            pnlCardVehicles.Controls.Add(lblCardVehiclesTitle);
            pnlCardVehicles.Controls.Add(lblStatVehicles);
            pnlCardVehicles.Controls.Add(lblCardVehiclesSub);
            pnlCardVehicles.Location = new System.Drawing.Point(936, 11);
            pnlCardVehicles.Margin = new System.Windows.Forms.Padding(3, 3, 14, 3);
            pnlCardVehicles.Name = "pnlCardVehicles";
            pnlCardVehicles.Size = new System.Drawing.Size(288, 84);
            pnlCardVehicles.TabIndex = 3;

            // 
            // pnlCardVehiclesBar
            // 
            pnlCardVehiclesBar.BackColor = System.Drawing.Color.FromArgb(245, 158, 11);
            pnlCardVehiclesBar.Dock = System.Windows.Forms.DockStyle.Left;
            pnlCardVehiclesBar.Location = new System.Drawing.Point(0, 0);
            pnlCardVehiclesBar.Name = "pnlCardVehiclesBar";
            pnlCardVehiclesBar.Size = new System.Drawing.Size(5, 84);
            pnlCardVehiclesBar.TabIndex = 0;

            // 
            // picCardVehicles
            // 
            picCardVehicles.BackColor = System.Drawing.Color.FromArgb(254, 243, 199);
            picCardVehicles.ForeColor = System.Drawing.Color.FromArgb(245, 158, 11);
            picCardVehicles.IconChar = FontAwesome.Sharp.IconChar.Bus;
            picCardVehicles.IconColor = System.Drawing.Color.FromArgb(245, 158, 11);
            picCardVehicles.IconFont = FontAwesome.Sharp.IconFont.Auto;
            picCardVehicles.IconSize = 28;
            picCardVehicles.Location = new System.Drawing.Point(14, 24);
            picCardVehicles.Name = "picCardVehicles";
            picCardVehicles.Padding = new System.Windows.Forms.Padding(4);
            picCardVehicles.Size = new System.Drawing.Size(38, 38);
            picCardVehicles.TabIndex = 1;
            picCardVehicles.TabStop = false;

            // 
            // lblCardVehiclesTitle
            // 
            lblCardVehiclesTitle.AutoSize = true;
            lblCardVehiclesTitle.Font = new System.Drawing.Font("Segoe UI", 8.2F, System.Drawing.FontStyle.Bold);
            lblCardVehiclesTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblCardVehiclesTitle.Location = new System.Drawing.Point(58, 12);
            lblCardVehiclesTitle.Name = "lblCardVehiclesTitle";
            lblCardVehiclesTitle.Size = new System.Drawing.Size(180, 19);
            lblCardVehiclesTitle.TabIndex = 2;
            lblCardVehiclesTitle.Text = "PHƯƠNG TIỆN SẴN SÀNG";

            // 
            // lblStatVehicles
            // 
            lblStatVehicles.AutoSize = true;
            lblStatVehicles.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            lblStatVehicles.ForeColor = System.Drawing.Color.FromArgb(245, 158, 11);
            lblStatVehicles.Location = new System.Drawing.Point(56, 31);
            lblStatVehicles.Name = "lblStatVehicles";
            lblStatVehicles.Size = new System.Drawing.Size(65, 31);
            lblStatVehicles.TabIndex = 3;
            lblStatVehicles.Text = "0 xe";

            // 
            // lblCardVehiclesSub
            // 
            lblCardVehiclesSub.AutoSize = true;
            lblCardVehiclesSub.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            lblCardVehiclesSub.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblCardVehiclesSub.Location = new System.Drawing.Point(58, 62);
            lblCardVehiclesSub.Name = "lblCardVehiclesSub";
            lblCardVehiclesSub.Size = new System.Drawing.Size(185, 17);
            lblCardVehiclesSub.TabIndex = 4;
            lblCardVehiclesSub.Text = "Đang vận hành & trực sẵn sàng";

            // 
            // tblContent
            // 
            tblContent.BackColor = System.Drawing.Color.Transparent;
            tblContent.ColumnCount = 1;
            tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tblContent.Controls.Add(tblCharts, 0, 0);
            tblContent.Controls.Add(pnlTableWrap, 0, 1);
            tblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            tblContent.Location = new System.Drawing.Point(0, 220);
            tblContent.Margin = new System.Windows.Forms.Padding(0);
            tblContent.Name = "tblContent";
            tblContent.Padding = new System.Windows.Forms.Padding(20, 6, 20, 6);
            tblContent.RowCount = 2;
            tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64F));
            tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36F));
            tblContent.Size = new System.Drawing.Size(1260, 586);
            tblContent.TabIndex = 3;

            // 
            // tblCharts
            // 
            tblCharts.BackColor = System.Drawing.Color.Transparent;
            tblCharts.ColumnCount = 2;
            tblCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            tblCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            tblCharts.Controls.Add(pnlTimeChartWrap, 0, 0);
            tblCharts.Controls.Add(pnlRouteChartWrap, 1, 0);
            tblCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            tblCharts.Location = new System.Drawing.Point(20, 6);
            tblCharts.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            tblCharts.Name = "tblCharts";
            tblCharts.RowCount = 1;
            tblCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tblCharts.Size = new System.Drawing.Size(1220, 359);
            tblCharts.TabIndex = 0;

            // 
            // pnlTimeChartWrap
            // 
            pnlTimeChartWrap.BackColor = System.Drawing.Color.White;
            pnlTimeChartWrap.Controls.Add(lblTimeChartTitle);
            pnlTimeChartWrap.Controls.Add(lblTimeChartSub);
            pnlTimeChartWrap.Controls.Add(pnlTimeChartContainer);
            pnlTimeChartWrap.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlTimeChartWrap.Location = new System.Drawing.Point(0, 0);
            pnlTimeChartWrap.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            pnlTimeChartWrap.Name = "pnlTimeChartWrap";
            pnlTimeChartWrap.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            pnlTimeChartWrap.Size = new System.Drawing.Size(724, 359);
            pnlTimeChartWrap.TabIndex = 0;

            // 
            // lblTimeChartTitle
            // 
            lblTimeChartTitle.AutoSize = true;
            lblTimeChartTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            lblTimeChartTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblTimeChartTitle.Location = new System.Drawing.Point(14, 10);
            lblTimeChartTitle.Name = "lblTimeChartTitle";
            lblTimeChartTitle.Size = new System.Drawing.Size(430, 25);
            lblTimeChartTitle.TabIndex = 0;
            lblTimeChartTitle.Text = "DOANH THU & SẢN LƯỢNG VÉ THEO THỜI GIAN";

            // 
            // lblTimeChartSub
            // 
            lblTimeChartSub.AutoSize = true;
            lblTimeChartSub.Font = new System.Drawing.Font("Segoe UI", 8.2F);
            lblTimeChartSub.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTimeChartSub.Location = new System.Drawing.Point(15, 34);
            lblTimeChartSub.Name = "lblTimeChartSub";
            lblTimeChartSub.Size = new System.Drawing.Size(380, 19);
            lblTimeChartSub.TabIndex = 1;
            lblTimeChartSub.Text = "Biểu đồ cột doanh số (VNĐ) kết hợp đường số vé bán theo ngày";

            // 
            // pnlTimeChartContainer
            // 
            pnlTimeChartContainer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlTimeChartContainer.BackColor = System.Drawing.Color.White;
            pnlTimeChartContainer.Controls.Add(cartesianChartTime);
            pnlTimeChartContainer.Controls.Add(pnlTimeChartGdi);
            pnlTimeChartContainer.Location = new System.Drawing.Point(10, 58);
            pnlTimeChartContainer.Name = "pnlTimeChartContainer";
            pnlTimeChartContainer.Size = new System.Drawing.Size(700, 288);
            pnlTimeChartContainer.TabIndex = 2;

            // 
            // cartesianChartTime
            // 
            cartesianChartTime.Dock = System.Windows.Forms.DockStyle.Fill;
            cartesianChartTime.Location = new System.Drawing.Point(0, 0);
            cartesianChartTime.Name = "cartesianChartTime";
            cartesianChartTime.Size = new System.Drawing.Size(700, 288);
            cartesianChartTime.TabIndex = 0;

            // 
            // pnlTimeChartGdi
            // 
            pnlTimeChartGdi.BackColor = System.Drawing.Color.White;
            pnlTimeChartGdi.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlTimeChartGdi.Location = new System.Drawing.Point(0, 0);
            pnlTimeChartGdi.Name = "pnlTimeChartGdi";
            pnlTimeChartGdi.Size = new System.Drawing.Size(700, 288);
            pnlTimeChartGdi.TabIndex = 1;
            pnlTimeChartGdi.Visible = false;

            // 
            // pnlRouteChartWrap
            // 
            pnlRouteChartWrap.BackColor = System.Drawing.Color.White;
            pnlRouteChartWrap.Controls.Add(lblRouteChartTitle);
            pnlRouteChartWrap.Controls.Add(lblRouteChartSub);
            pnlRouteChartWrap.Controls.Add(pnlRouteChartContainer);
            pnlRouteChartWrap.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlRouteChartWrap.Location = new System.Drawing.Point(732, 0);
            pnlRouteChartWrap.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            pnlRouteChartWrap.Name = "pnlRouteChartWrap";
            pnlRouteChartWrap.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            pnlRouteChartWrap.Size = new System.Drawing.Size(488, 359);
            pnlRouteChartWrap.TabIndex = 1;

            // 
            // lblRouteChartTitle
            // 
            lblRouteChartTitle.AutoSize = true;
            lblRouteChartTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            lblRouteChartTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblRouteChartTitle.Location = new System.Drawing.Point(14, 10);
            lblRouteChartTitle.Name = "lblRouteChartTitle";
            lblRouteChartTitle.Size = new System.Drawing.Size(350, 25);
            lblRouteChartTitle.TabIndex = 0;
            lblRouteChartTitle.Text = "CƠ CẤU DOANH THU THEO TUYẾN XE";

            // 
            // lblRouteChartSub
            // 
            lblRouteChartSub.AutoSize = true;
            lblRouteChartSub.Font = new System.Drawing.Font("Segoe UI", 8.2F);
            lblRouteChartSub.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblRouteChartSub.Location = new System.Drawing.Point(15, 34);
            lblRouteChartSub.Name = "lblRouteChartSub";
            lblRouteChartSub.Size = new System.Drawing.Size(320, 19);
            lblRouteChartSub.TabIndex = 1;
            lblRouteChartSub.Text = "Tỷ trọng doanh số (%) giữa các lộ trình khai thác";

            // 
            // pnlRouteChartContainer
            // 
            pnlRouteChartContainer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlRouteChartContainer.BackColor = System.Drawing.Color.White;
            pnlRouteChartContainer.Controls.Add(pieChartRoutes);
            pnlRouteChartContainer.Controls.Add(pnlRouteChartGdi);
            pnlRouteChartContainer.Location = new System.Drawing.Point(10, 58);
            pnlRouteChartContainer.Name = "pnlRouteChartContainer";
            pnlRouteChartContainer.Size = new System.Drawing.Size(464, 288);
            pnlRouteChartContainer.TabIndex = 2;

            // 
            // pieChartRoutes
            // 
            pieChartRoutes.Dock = System.Windows.Forms.DockStyle.Fill;
            pieChartRoutes.Location = new System.Drawing.Point(0, 0);
            pieChartRoutes.Name = "pieChartRoutes";
            pieChartRoutes.Size = new System.Drawing.Size(464, 288);
            pieChartRoutes.TabIndex = 0;

            // 
            // pnlRouteChartGdi
            // 
            pnlRouteChartGdi.BackColor = System.Drawing.Color.White;
            pnlRouteChartGdi.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlRouteChartGdi.Location = new System.Drawing.Point(0, 0);
            pnlRouteChartGdi.Name = "pnlRouteChartGdi";
            pnlRouteChartGdi.Size = new System.Drawing.Size(464, 288);
            pnlRouteChartGdi.TabIndex = 1;
            pnlRouteChartGdi.Visible = false;

            // 
            // pnlTableWrap
            // 
            pnlTableWrap.BackColor = System.Drawing.Color.White;
            pnlTableWrap.Controls.Add(dgvRecentTransactions);
            pnlTableWrap.Controls.Add(pnlTableHeader);
            pnlTableWrap.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlTableWrap.Location = new System.Drawing.Point(20, 373);
            pnlTableWrap.Margin = new System.Windows.Forms.Padding(0);
            pnlTableWrap.Name = "pnlTableWrap";
            pnlTableWrap.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            pnlTableWrap.Size = new System.Drawing.Size(1220, 201);
            pnlTableWrap.TabIndex = 1;

            // 
            // pnlTableHeader
            // 
            pnlTableHeader.Controls.Add(lblTableTitle);
            pnlTableHeader.Controls.Add(lblTableSub);
            pnlTableHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlTableHeader.Location = new System.Drawing.Point(14, 8);
            pnlTableHeader.Name = "pnlTableHeader";
            pnlTableHeader.Size = new System.Drawing.Size(1192, 34);
            pnlTableHeader.TabIndex = 0;

            // 
            // lblTableTitle
            // 
            lblTableTitle.AutoSize = true;
            lblTableTitle.Font = new System.Drawing.Font("Segoe UI", 9.8F, System.Drawing.FontStyle.Bold);
            lblTableTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblTableTitle.Location = new System.Drawing.Point(0, 4);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new System.Drawing.Size(520, 23);
            lblTableTitle.TabIndex = 0;
            lblTableTitle.Text = "GIAO DỊCH VÉ GẦN ĐÂY (DỮ LIỆU THỰC TẾ ĐỒNG BỘ TỪ CSDL)";

            // 
            // lblTableSub
            // 
            lblTableSub.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblTableSub.AutoSize = true;
            lblTableSub.Font = new System.Drawing.Font("Segoe UI", 8.2F);
            lblTableSub.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTableSub.Location = new System.Drawing.Point(750, 8);
            lblTableSub.Name = "lblTableSub";
            lblTableSub.Size = new System.Drawing.Size(438, 19);
            lblTableSub.TabIndex = 1;
            lblTableSub.Text = "Minh chứng số liệu biểu đồ được truy vấn trực tiếp từ bảng Tickets CSDL";

            // 
            // dgvRecentTransactions
            // 
            dgvRecentTransactions.AllowUserToAddRows = false;
            dgvRecentTransactions.AllowUserToDeleteRows = false;
            dgvRecentTransactions.AllowUserToResizeRows = false;
            dgvRecentTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvRecentTransactions.BackgroundColor = System.Drawing.Color.White;
            dgvRecentTransactions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvRecentTransactions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRecentTransactions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;

            dgvHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvHeaderStyle.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            dgvHeaderStyle.Font = new System.Drawing.Font("Segoe UI", 8.8F, System.Drawing.FontStyle.Bold);
            dgvHeaderStyle.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            dgvHeaderStyle.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            dgvHeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            dgvHeaderStyle.SelectionForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            dgvHeaderStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvRecentTransactions.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            dgvRecentTransactions.ColumnHeadersHeight = 32;
            dgvRecentTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvRecentTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                colTicketCode,
                colCustomerName,
                colCustomerPhone,
                colRouteName,
                colVehiclePlate,
                colSeatNumber,
                colPrice,
                colDepartureTime,
                colPaymentStatus
            });

            dgvRowStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvRowStyle.BackColor = System.Drawing.Color.White;
            dgvRowStyle.Font = new System.Drawing.Font("Segoe UI", 8.8F);
            dgvRowStyle.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            dgvRowStyle.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            dgvRowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(224, 242, 254);
            dgvRowStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dgvRecentTransactions.DefaultCellStyle = dgvRowStyle;
            dgvRecentTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvRecentTransactions.EnableHeadersVisualStyles = false;
            dgvRecentTransactions.GridColor = System.Drawing.Color.FromArgb(241, 245, 249);
            dgvRecentTransactions.Location = new System.Drawing.Point(14, 42);
            dgvRecentTransactions.MultiSelect = false;
            dgvRecentTransactions.Name = "dgvRecentTransactions";
            dgvRecentTransactions.ReadOnly = true;
            dgvRecentTransactions.RowHeadersVisible = false;
            dgvRecentTransactions.RowTemplate.Height = 28;
            dgvRecentTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvRecentTransactions.Size = new System.Drawing.Size(1192, 151);
            dgvRecentTransactions.TabIndex = 1;

            // 
            // colTicketCode
            // 
            colTicketCode.DataPropertyName = "TicketCode";
            colTicketCode.FillWeight = 12F;
            colTicketCode.MinimumWidth = 90;
            colTicketCode.HeaderText = "Mã vé";
            colTicketCode.Name = "colTicketCode";
            colTicketCode.ReadOnly = true;

            // 
            // colCustomerName
            // 
            colCustomerName.DataPropertyName = "CustomerName";
            colCustomerName.FillWeight = 16F;
            colCustomerName.MinimumWidth = 110;
            colCustomerName.HeaderText = "Hành khách";
            colCustomerName.Name = "colCustomerName";
            colCustomerName.ReadOnly = true;

            // 
            // colCustomerPhone
            // 
            colCustomerPhone.DataPropertyName = "CustomerPhone";
            colCustomerPhone.FillWeight = 11F;
            colCustomerPhone.MinimumWidth = 85;
            colCustomerPhone.HeaderText = "Số ĐT";
            colCustomerPhone.Name = "colCustomerPhone";
            colCustomerPhone.ReadOnly = true;

            // 
            // colRouteName
            // 
            colRouteName.DataPropertyName = "RouteName";
            colRouteName.FillWeight = 23F;
            colRouteName.MinimumWidth = 140;
            colRouteName.HeaderText = "Lộ trình tuyến xe";
            colRouteName.Name = "colRouteName";
            colRouteName.ReadOnly = true;

            // 
            // colVehiclePlate
            // 
            colVehiclePlate.DataPropertyName = "VehiclePlate";
            colVehiclePlate.FillWeight = 11F;
            colVehiclePlate.MinimumWidth = 85;
            colVehiclePlate.HeaderText = "Biển số xe";
            colVehiclePlate.Name = "colVehiclePlate";
            colVehiclePlate.ReadOnly = true;

            // 
            // colSeatNumber
            // 
            colSeatNumber.DataPropertyName = "SeatNumber";
            colSeatNumber.FillWeight = 7F;
            colSeatNumber.MinimumWidth = 55;
            colSeatNumber.HeaderText = "Số ghế";
            colSeatNumber.Name = "colSeatNumber";
            colSeatNumber.ReadOnly = true;

            // 
            // colPrice
            // 
            colPrice.DataPropertyName = "Price";
            dgvPriceStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgvPriceStyle.Format = "#,##0 VNĐ";
            colPrice.DefaultCellStyle = dgvPriceStyle;
            colPrice.FillWeight = 11F;
            colPrice.MinimumWidth = 90;
            colPrice.HeaderText = "Giá vé";
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;

            // 
            // colDepartureTime
            // 
            colDepartureTime.DataPropertyName = "DepartureTime";
            dgvDateStyle.Format = "dd/MM/yyyy HH:mm";
            colDepartureTime.DefaultCellStyle = dgvDateStyle;
            colDepartureTime.FillWeight = 12F;
            colDepartureTime.MinimumWidth = 105;
            colDepartureTime.HeaderText = "Giờ xuất bến";
            colDepartureTime.Name = "colDepartureTime";
            colDepartureTime.ReadOnly = true;

            // 
            // colPaymentStatus
            // 
            colPaymentStatus.DataPropertyName = "PaymentStatus";
            colPaymentStatus.FillWeight = 9F;
            colPaymentStatus.MinimumWidth = 75;
            colPaymentStatus.HeaderText = "Thanh toán";
            colPaymentStatus.Name = "colPaymentStatus";
            colPaymentStatus.ReadOnly = true;

            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = System.Drawing.Color.White;
            pnlFooter.Controls.Add(lblFooterStatus);
            pnlFooter.Controls.Add(lblFooterRecords);
            pnlFooter.Controls.Add(lblFooterTime);
            pnlFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlFooter.Location = new System.Drawing.Point(0, 806);
            pnlFooter.Margin = new System.Windows.Forms.Padding(0);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            pnlFooter.Size = new System.Drawing.Size(1260, 34);
            pnlFooter.TabIndex = 4;

            // 
            // lblFooterStatus
            // 
            lblFooterStatus.AutoSize = true;
            lblFooterStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblFooterStatus.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            lblFooterStatus.Location = new System.Drawing.Point(18, 7);
            lblFooterStatus.Name = "lblFooterStatus";
            lblFooterStatus.Size = new System.Drawing.Size(270, 20);
            lblFooterStatus.TabIndex = 0;
            lblFooterStatus.Text = "● Hệ thống sẵn sàng · Đã kết nối CSDL";

            // 
            // lblFooterRecords
            // 
            lblFooterRecords.Anchor = System.Windows.Forms.AnchorStyles.Top;
            lblFooterRecords.AutoSize = true;
            lblFooterRecords.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblFooterRecords.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblFooterRecords.Location = new System.Drawing.Point(520, 7);
            lblFooterRecords.Name = "lblFooterRecords";
            lblFooterRecords.Size = new System.Drawing.Size(260, 20);
            lblFooterRecords.TabIndex = 1;
            lblFooterRecords.Text = "Đang hiển thị 10 giao dịch gần nhất";

            // 
            // lblFooterTime
            // 
            lblFooterTime.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblFooterTime.AutoSize = true;
            lblFooterTime.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblFooterTime.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblFooterTime.Location = new System.Drawing.Point(1050, 7);
            lblFooterTime.Name = "lblFooterTime";
            lblFooterTime.Size = new System.Drawing.Size(140, 20);
            lblFooterTime.TabIndex = 2;
            lblFooterTime.Text = "17/10/2026 10:30:00";

            // 
            // UcDashboard
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            Controls.Add(tlpRoot);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "UcDashboard";
            Size = new System.Drawing.Size(1260, 840);

            tlpRoot.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlFilterBar.ResumeLayout(false);
            pnlFilterBar.PerformLayout();
            flpStatCards.ResumeLayout(false);

            pnlCardRevenue.ResumeLayout(false);
            pnlCardRevenue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCardRevenue).EndInit();
            pnlCardTickets.ResumeLayout(false);
            pnlCardTickets.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCardTickets).EndInit();
            pnlCardRoutes.ResumeLayout(false);
            pnlCardRoutes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCardRoutes).EndInit();
            pnlCardVehicles.ResumeLayout(false);
            pnlCardVehicles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCardVehicles).EndInit();

            tblContent.ResumeLayout(false);
            tblCharts.ResumeLayout(false);
            pnlTimeChartWrap.ResumeLayout(false);
            pnlTimeChartWrap.PerformLayout();
            pnlTimeChartContainer.ResumeLayout(false);
            pnlRouteChartWrap.ResumeLayout(false);
            pnlRouteChartWrap.PerformLayout();
            pnlRouteChartContainer.ResumeLayout(false);
            pnlTableWrap.ResumeLayout(false);
            pnlTableHeader.ResumeLayout(false);
            pnlTableHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentTransactions).EndInit();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
