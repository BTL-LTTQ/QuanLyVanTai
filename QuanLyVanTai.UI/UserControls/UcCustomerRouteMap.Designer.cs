namespace QuanLyVanTai.UI.UserControls
{
    partial class UcCustomerRouteMap
    {
        private System.ComponentModel.IContainer components = null;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            splitMain = new SplitContainer();
            pnlLeftContent = new Panel();
            grpStations = new GroupBox();
            dgvStations = new DataGridView();
            colOrder = new DataGridViewTextBoxColumn();
            colStationName = new DataGridViewTextBoxColumn();
            colCity = new DataGridViewTextBoxColumn();
            colRole = new DataGridViewTextBoxColumn();
            flpLeftActions = new FlowLayoutPanel();
            btnDatVeTuyenNay = new FontAwesome.Sharp.IconButton();
            btnFitBounds = new FontAwesome.Sharp.IconButton();
            btnMoGoogleMaps = new FontAwesome.Sharp.IconButton();
            grpRouteInfo = new GroupBox();
            tlpRouteInfo = new TableLayoutPanel();
            lblRouteTitle = new Label();
            lblRouteNameVal = new Label();
            lblDistanceTitle = new Label();
            lblDistanceTimeVal = new Label();
            lblPriceTitle = new Label();
            lblBasePriceVal = new Label();
            lblStationCountTitle = new Label();
            lblStationCountVal = new Label();
            grpSelectRoute = new GroupBox();
            tlpSelect = new TableLayoutPanel();
            lblTuyenXe = new Label();
            cboTuyenXe = new ComboBox();
            btnLamMoi = new FontAwesome.Sharp.IconButton();
            pnlRightMap = new Panel();
            pnlWebContainer = new Panel();
            webViewMap = new Microsoft.Web.WebView2.WinForms.WebView2();
            lblLoading = new Label();
            pnlRightToolbar = new Panel();
            flpMapControls = new FlowLayoutPanel();
            btnLayerStreet = new FontAwesome.Sharp.IconButton();
            btnLayerSatellite = new FontAwesome.Sharp.IconButton();
            btnLayerVoyager = new FontAwesome.Sharp.IconButton();
            lblMapStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            pnlLeftContent.SuspendLayout();
            grpStations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStations).BeginInit();
            flpLeftActions.SuspendLayout();
            grpRouteInfo.SuspendLayout();
            tlpRouteInfo.SuspendLayout();
            grpSelectRoute.SuspendLayout();
            tlpSelect.SuspendLayout();
            pnlRightMap.SuspendLayout();
            pnlWebContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webViewMap).BeginInit();
            pnlRightToolbar.SuspendLayout();
            flpMapControls.SuspendLayout();
            SuspendLayout();
            // 
            // splitMain
            // 
            splitMain.Dock = DockStyle.Fill;
            splitMain.FixedPanel = FixedPanel.Panel1;
            splitMain.Location = new Point(0, 0);
            splitMain.Margin = new Padding(3, 4, 3, 4);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.BackColor = Color.FromArgb(248, 250, 252);
            splitMain.Panel1.Controls.Add(pnlLeftContent);
            splitMain.Panel1MinSize = 400;
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.BackColor = Color.FromArgb(241, 245, 249);
            splitMain.Panel2.Controls.Add(pnlRightMap);
            splitMain.Panel2MinSize = 520;
            splitMain.Size = new Size(1300, 850);
            splitMain.SplitterDistance = 470;
            splitMain.SplitterWidth = 6;
            splitMain.TabIndex = 0;
            // 
            // pnlLeftContent
            // 
            pnlLeftContent.Controls.Add(grpStations);
            pnlLeftContent.Controls.Add(flpLeftActions);
            pnlLeftContent.Controls.Add(grpRouteInfo);
            pnlLeftContent.Controls.Add(grpSelectRoute);
            pnlLeftContent.Dock = DockStyle.Fill;
            pnlLeftContent.Location = new Point(0, 0);
            pnlLeftContent.Margin = new Padding(3, 4, 3, 4);
            pnlLeftContent.Name = "pnlLeftContent";
            pnlLeftContent.Padding = new Padding(12);
            pnlLeftContent.Size = new Size(470, 850);
            pnlLeftContent.TabIndex = 0;
            // 
            // grpStations
            // 
            grpStations.BackColor = Color.White;
            grpStations.Controls.Add(dgvStations);
            grpStations.Dock = DockStyle.Fill;
            grpStations.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            grpStations.ForeColor = Color.FromArgb(15, 23, 42);
            grpStations.Location = new Point(12, 280);
            grpStations.Margin = new Padding(3, 8, 3, 8);
            grpStations.Name = "grpStations";
            grpStations.Padding = new Padding(10);
            grpStations.Size = new Size(446, 448);
            grpStations.TabIndex = 2;
            grpStations.TabStop = false;
            grpStations.Text = " 3. Danh Sách Trạm Dừng Trên Tuyến (Lộ Trình) ";
            // 
            // dgvStations
            // 
            dgvStations.AllowUserToAddRows = false;
            dgvStations.AllowUserToDeleteRows = false;
            dgvStations.AllowUserToResizeRows = false;
            dgvStations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStations.BackgroundColor = Color.FromArgb(248, 250, 252);
            dgvStations.BorderStyle = BorderStyle.None;
            dgvStations.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvStations.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(241, 245, 249);
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(241, 245, 249);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvStations.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvStations.ColumnHeadersHeight = 36;
            dgvStations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvStations.Columns.AddRange(new DataGridViewColumn[] { colOrder, colStationName, colCity, colRole });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(30, 27, 75);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvStations.DefaultCellStyle = dataGridViewCellStyle2;
            dgvStations.Dock = DockStyle.Fill;
            dgvStations.EnableHeadersVisualStyles = false;
            dgvStations.GridColor = Color.FromArgb(241, 245, 249);
            dgvStations.Location = new Point(10, 32);
            dgvStations.MultiSelect = false;
            dgvStations.Name = "dgvStations";
            dgvStations.ReadOnly = true;
            dgvStations.RowHeadersVisible = false;
            dgvStations.RowHeadersWidth = 30;
            dgvStations.RowTemplate.Height = 36;
            dgvStations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStations.Size = new Size(426, 406);
            dgvStations.TabIndex = 0;
            // 
            // colOrder
            // 
            colOrder.FillWeight = 15F;
            colOrder.MinimumWidth = 45;
            colOrder.Name = "colOrder";
            colOrder.ReadOnly = true;
            colOrder.HeaderText = "STT";
            // 
            // colStationName
            // 
            colStationName.FillWeight = 45F;
            colStationName.MinimumWidth = 120;
            colStationName.Name = "colStationName";
            colStationName.ReadOnly = true;
            colStationName.HeaderText = "Tên Trạm Dừng";
            // 
            // colCity
            // 
            colCity.FillWeight = 22F;
            colCity.MinimumWidth = 80;
            colCity.Name = "colCity";
            colCity.ReadOnly = true;
            colCity.HeaderText = "Tỉnh / TP";
            // 
            // colRole
            // 
            colRole.FillWeight = 18F;
            colRole.MinimumWidth = 70;
            colRole.Name = "colRole";
            colRole.ReadOnly = true;
            colRole.HeaderText = "Vai Trò";
            // 
            // flpLeftActions
            // 
            flpLeftActions.Controls.Add(btnDatVeTuyenNay);
            flpLeftActions.Controls.Add(btnFitBounds);
            flpLeftActions.Controls.Add(btnMoGoogleMaps);
            flpLeftActions.Dock = DockStyle.Bottom;
            flpLeftActions.Location = new Point(12, 728);
            flpLeftActions.Name = "flpLeftActions";
            flpLeftActions.Padding = new Padding(0, 10, 0, 0);
            flpLeftActions.Size = new Size(446, 110);
            flpLeftActions.TabIndex = 3;
            // 
            // btnDatVeTuyenNay
            // 
            btnDatVeTuyenNay.BackColor = Color.FromArgb(16, 185, 129);
            btnDatVeTuyenNay.Cursor = Cursors.Hand;
            btnDatVeTuyenNay.FlatAppearance.BorderSize = 0;
            btnDatVeTuyenNay.FlatStyle = FlatStyle.Flat;
            btnDatVeTuyenNay.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnDatVeTuyenNay.ForeColor = Color.White;
            btnDatVeTuyenNay.IconChar = FontAwesome.Sharp.IconChar.Ticket;
            btnDatVeTuyenNay.IconColor = Color.White;
            btnDatVeTuyenNay.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDatVeTuyenNay.IconSize = 20;
            btnDatVeTuyenNay.ImageAlign = ContentAlignment.MiddleLeft;
            btnDatVeTuyenNay.Location = new Point(3, 14);
            btnDatVeTuyenNay.Margin = new Padding(3, 4, 8, 6);
            btnDatVeTuyenNay.Name = "btnDatVeTuyenNay";
            btnDatVeTuyenNay.Padding = new Padding(10, 0, 12, 0);
            btnDatVeTuyenNay.Size = new Size(200, 42);
            btnDatVeTuyenNay.TabIndex = 0;
            btnDatVeTuyenNay.Text = "  Đặt Vé Tuyến Này";
            btnDatVeTuyenNay.TextAlign = ContentAlignment.MiddleRight;
            btnDatVeTuyenNay.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDatVeTuyenNay.UseVisualStyleBackColor = false;
            // 
            // btnFitBounds
            // 
            btnFitBounds.BackColor = Color.FromArgb(241, 245, 249);
            btnFitBounds.Cursor = Cursors.Hand;
            btnFitBounds.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnFitBounds.FlatStyle = FlatStyle.Flat;
            btnFitBounds.Font = new Font("Segoe UI", 9F);
            btnFitBounds.ForeColor = Color.FromArgb(51, 65, 85);
            btnFitBounds.IconChar = FontAwesome.Sharp.IconChar.Expand;
            btnFitBounds.IconColor = Color.FromArgb(71, 85, 105);
            btnFitBounds.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnFitBounds.IconSize = 18;
            btnFitBounds.ImageAlign = ContentAlignment.MiddleLeft;
            btnFitBounds.Location = new Point(214, 14);
            btnFitBounds.Margin = new Padding(3, 4, 8, 6);
            btnFitBounds.Name = "btnFitBounds";
            btnFitBounds.Padding = new Padding(8, 0, 10, 0);
            btnFitBounds.Size = new Size(130, 42);
            btnFitBounds.TabIndex = 1;
            btnFitBounds.Text = "  Phóng To";
            btnFitBounds.TextAlign = ContentAlignment.MiddleRight;
            btnFitBounds.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnFitBounds.UseVisualStyleBackColor = false;
            // 
            // btnMoGoogleMaps
            // 
            btnMoGoogleMaps.BackColor = Color.FromArgb(238, 242, 255);
            btnMoGoogleMaps.Cursor = Cursors.Hand;
            btnMoGoogleMaps.FlatAppearance.BorderColor = Color.FromArgb(199, 210, 254);
            btnMoGoogleMaps.FlatStyle = FlatStyle.Flat;
            btnMoGoogleMaps.Font = new Font("Segoe UI", 8.5F);
            btnMoGoogleMaps.ForeColor = Color.FromArgb(67, 56, 202);
            btnMoGoogleMaps.IconChar = FontAwesome.Sharp.IconChar.ArrowUpRightFromSquare;
            btnMoGoogleMaps.IconColor = Color.FromArgb(67, 56, 202);
            btnMoGoogleMaps.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnMoGoogleMaps.IconSize = 16;
            btnMoGoogleMaps.ImageAlign = ContentAlignment.MiddleLeft;
            btnMoGoogleMaps.Location = new Point(3, 66);
            btnMoGoogleMaps.Margin = new Padding(3, 4, 8, 4);
            btnMoGoogleMaps.Name = "btnMoGoogleMaps";
            btnMoGoogleMaps.Padding = new Padding(8, 0, 10, 0);
            btnMoGoogleMaps.Size = new Size(180, 36);
            btnMoGoogleMaps.TabIndex = 2;
            btnMoGoogleMaps.Text = "  Mở Google Maps";
            btnMoGoogleMaps.TextAlign = ContentAlignment.MiddleRight;
            btnMoGoogleMaps.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnMoGoogleMaps.UseVisualStyleBackColor = false;
            // 
            // grpRouteInfo
            // 
            grpRouteInfo.BackColor = Color.White;
            grpRouteInfo.Controls.Add(tlpRouteInfo);
            grpRouteInfo.Dock = DockStyle.Top;
            grpRouteInfo.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            grpRouteInfo.ForeColor = Color.FromArgb(15, 23, 42);
            grpRouteInfo.Location = new Point(12, 102);
            grpRouteInfo.Margin = new Padding(3, 8, 3, 8);
            grpRouteInfo.Name = "grpRouteInfo";
            grpRouteInfo.Padding = new Padding(10);
            grpRouteInfo.Size = new Size(446, 178);
            grpRouteInfo.TabIndex = 1;
            grpRouteInfo.TabStop = false;
            grpRouteInfo.Text = " 2. Thông Số Tuyến Đường ";
            // 
            // tlpRouteInfo
            // 
            tlpRouteInfo.ColumnCount = 2;
            tlpRouteInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tlpRouteInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRouteInfo.Controls.Add(lblRouteTitle, 0, 0);
            tlpRouteInfo.Controls.Add(lblRouteNameVal, 1, 0);
            tlpRouteInfo.Controls.Add(lblDistanceTitle, 0, 1);
            tlpRouteInfo.Controls.Add(lblDistanceTimeVal, 1, 1);
            tlpRouteInfo.Controls.Add(lblPriceTitle, 0, 2);
            tlpRouteInfo.Controls.Add(lblBasePriceVal, 1, 2);
            tlpRouteInfo.Controls.Add(lblStationCountTitle, 0, 3);
            tlpRouteInfo.Controls.Add(lblStationCountVal, 1, 3);
            tlpRouteInfo.Dock = DockStyle.Fill;
            tlpRouteInfo.Location = new Point(10, 32);
            tlpRouteInfo.Name = "tlpRouteInfo";
            tlpRouteInfo.RowCount = 4;
            tlpRouteInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpRouteInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tlpRouteInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tlpRouteInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tlpRouteInfo.Size = new Size(426, 136);
            tlpRouteInfo.TabIndex = 0;
            // 
            // lblRouteTitle
            // 
            lblRouteTitle.Anchor = AnchorStyles.Left;
            lblRouteTitle.AutoSize = true;
            lblRouteTitle.Font = new Font("Segoe UI", 9F);
            lblRouteTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblRouteTitle.Location = new Point(3, 8);
            lblRouteTitle.Name = "lblRouteTitle";
            lblRouteTitle.Size = new Size(67, 20);
            lblRouteTitle.TabIndex = 0;
            lblRouteTitle.Text = "Lộ trình:";
            // 
            // lblRouteNameVal
            // 
            lblRouteNameVal.Anchor = AnchorStyles.Left;
            lblRouteNameVal.AutoSize = true;
            lblRouteNameVal.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblRouteNameVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblRouteNameVal.Location = new Point(123, 7);
            lblRouteNameVal.Name = "lblRouteNameVal";
            lblRouteNameVal.Size = new Size(23, 21);
            lblRouteNameVal.TabIndex = 1;
            lblRouteNameVal.Text = "...";
            // 
            // lblDistanceTitle
            // 
            lblDistanceTitle.Anchor = AnchorStyles.Left;
            lblDistanceTitle.AutoSize = true;
            lblDistanceTitle.Font = new Font("Segoe UI", 9F);
            lblDistanceTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblDistanceTitle.Location = new Point(3, 43);
            lblDistanceTitle.Name = "lblDistanceTitle";
            lblDistanceTitle.Size = new Size(106, 20);
            lblDistanceTitle.TabIndex = 2;
            lblDistanceTitle.Text = "Cự ly & Thời gian:";
            // 
            // lblDistanceTimeVal
            // 
            lblDistanceTimeVal.Anchor = AnchorStyles.Left;
            lblDistanceTimeVal.AutoSize = true;
            lblDistanceTimeVal.Font = new Font("Segoe UI", 9F);
            lblDistanceTimeVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblDistanceTimeVal.Location = new Point(123, 43);
            lblDistanceTimeVal.Name = "lblDistanceTimeVal";
            lblDistanceTimeVal.Size = new Size(21, 20);
            lblDistanceTimeVal.TabIndex = 3;
            lblDistanceTimeVal.Text = "...";
            // 
            // lblPriceTitle
            // 
            lblPriceTitle.Anchor = AnchorStyles.Left;
            lblPriceTitle.AutoSize = true;
            lblPriceTitle.Font = new Font("Segoe UI", 9F);
            lblPriceTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblPriceTitle.Location = new Point(3, 77);
            lblPriceTitle.Name = "lblPriceTitle";
            lblPriceTitle.Size = new Size(93, 20);
            lblPriceTitle.TabIndex = 4;
            lblPriceTitle.Text = "Giá cước gốc:";
            // 
            // lblBasePriceVal
            // 
            lblBasePriceVal.Anchor = AnchorStyles.Left;
            lblBasePriceVal.AutoSize = true;
            lblBasePriceVal.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblBasePriceVal.ForeColor = Color.FromArgb(16, 185, 129);
            lblBasePriceVal.Location = new Point(123, 76);
            lblBasePriceVal.Name = "lblBasePriceVal";
            lblBasePriceVal.Size = new Size(23, 21);
            lblBasePriceVal.TabIndex = 5;
            lblBasePriceVal.Text = "...";
            // 
            // lblStationCountTitle
            // 
            lblStationCountTitle.Anchor = AnchorStyles.Left;
            lblStationCountTitle.AutoSize = true;
            lblStationCountTitle.Font = new Font("Segoe UI", 9F);
            lblStationCountTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblStationCountTitle.Location = new Point(3, 110);
            lblStationCountTitle.Name = "lblStationCountTitle";
            lblStationCountTitle.Size = new Size(100, 20);
            lblStationCountTitle.TabIndex = 6;
            lblStationCountTitle.Text = "Số trạm dừng:";
            // 
            // lblStationCountVal
            // 
            lblStationCountVal.Anchor = AnchorStyles.Left;
            lblStationCountVal.AutoSize = true;
            lblStationCountVal.Font = new Font("Segoe UI", 9F);
            lblStationCountVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblStationCountVal.Location = new Point(123, 110);
            lblStationCountVal.Name = "lblStationCountVal";
            lblStationCountVal.Size = new Size(21, 20);
            lblStationCountVal.TabIndex = 7;
            lblStationCountVal.Text = "...";
            // 
            // grpSelectRoute
            // 
            grpSelectRoute.BackColor = Color.White;
            grpSelectRoute.Controls.Add(tlpSelect);
            grpSelectRoute.Dock = DockStyle.Top;
            grpSelectRoute.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            grpSelectRoute.ForeColor = Color.FromArgb(15, 23, 42);
            grpSelectRoute.Location = new Point(12, 12);
            grpSelectRoute.Margin = new Padding(3, 4, 3, 8);
            grpSelectRoute.Name = "grpSelectRoute";
            grpSelectRoute.Padding = new Padding(10);
            grpSelectRoute.Size = new Size(446, 90);
            grpSelectRoute.TabIndex = 0;
            grpSelectRoute.TabStop = false;
            grpSelectRoute.Text = " 1. Chọn Tuyến Xe Cần Xem Bản Đồ ";
            // 
            // tlpSelect
            // 
            tlpSelect.ColumnCount = 3;
            tlpSelect.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tlpSelect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpSelect.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45F));
            tlpSelect.Controls.Add(lblTuyenXe, 0, 0);
            tlpSelect.Controls.Add(cboTuyenXe, 1, 0);
            tlpSelect.Controls.Add(btnLamMoi, 2, 0);
            tlpSelect.Dock = DockStyle.Fill;
            tlpSelect.Location = new Point(10, 32);
            tlpSelect.Name = "tlpSelect";
            tlpSelect.RowCount = 1;
            tlpSelect.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpSelect.Size = new Size(426, 48);
            tlpSelect.TabIndex = 0;
            // 
            // lblTuyenXe
            // 
            lblTuyenXe.Anchor = AnchorStyles.Left;
            lblTuyenXe.AutoSize = true;
            lblTuyenXe.Font = new Font("Segoe UI", 9F);
            lblTuyenXe.ForeColor = Color.FromArgb(71, 85, 105);
            lblTuyenXe.Location = new Point(3, 14);
            lblTuyenXe.Name = "lblTuyenXe";
            lblTuyenXe.Size = new Size(70, 20);
            lblTuyenXe.TabIndex = 0;
            lblTuyenXe.Text = "Tuyến xe:";
            // 
            // cboTuyenXe
            // 
            cboTuyenXe.Dock = DockStyle.Fill;
            cboTuyenXe.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTuyenXe.Font = new Font("Segoe UI", 9F);
            cboTuyenXe.FormattingEnabled = true;
            cboTuyenXe.Location = new Point(83, 10);
            cboTuyenXe.Margin = new Padding(3, 10, 3, 3);
            cboTuyenXe.Name = "cboTuyenXe";
            cboTuyenXe.Size = new Size(295, 28);
            cboTuyenXe.TabIndex = 1;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.FromArgb(241, 245, 249);
            btnLamMoi.Cursor = Cursors.Hand;
            btnLamMoi.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.IconChar = FontAwesome.Sharp.IconChar.Rotate;
            btnLamMoi.IconColor = Color.FromArgb(71, 85, 105);
            btnLamMoi.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnLamMoi.IconSize = 16;
            btnLamMoi.Location = new Point(384, 8);
            btnLamMoi.Margin = new Padding(3, 8, 3, 3);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(39, 32);
            btnLamMoi.TabIndex = 2;
            btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // pnlRightMap
            // 
            pnlRightMap.Controls.Add(pnlWebContainer);
            pnlRightMap.Controls.Add(pnlRightToolbar);
            pnlRightMap.Dock = DockStyle.Fill;
            pnlRightMap.Location = new Point(0, 0);
            pnlRightMap.Margin = new Padding(3, 4, 3, 4);
            pnlRightMap.Name = "pnlRightMap";
            pnlRightMap.Size = new Size(824, 850);
            pnlRightMap.TabIndex = 0;
            // 
            // pnlWebContainer
            // 
            pnlWebContainer.Controls.Add(webViewMap);
            pnlWebContainer.Controls.Add(lblLoading);
            pnlWebContainer.Dock = DockStyle.Fill;
            pnlWebContainer.Location = new Point(0, 48);
            pnlWebContainer.Name = "pnlWebContainer";
            pnlWebContainer.Size = new Size(824, 802);
            pnlWebContainer.TabIndex = 1;
            // 
            // webViewMap
            // 
            webViewMap.AllowExternalDrop = true;
            webViewMap.CreationProperties = null;
            webViewMap.DefaultBackgroundColor = Color.White;
            webViewMap.Dock = DockStyle.Fill;
            webViewMap.Location = new Point(0, 0);
            webViewMap.Name = "webViewMap";
            webViewMap.Size = new Size(824, 802);
            webViewMap.TabIndex = 0;
            webViewMap.ZoomFactor = 1D;
            // 
            // lblLoading
            // 
            lblLoading.BackColor = Color.FromArgb(248, 250, 252);
            lblLoading.Dock = DockStyle.Fill;
            lblLoading.Font = new Font("Segoe UI", 11F);
            lblLoading.ForeColor = Color.FromArgb(71, 85, 105);
            lblLoading.Location = new Point(0, 0);
            lblLoading.Name = "lblLoading";
            lblLoading.Size = new Size(824, 802);
            lblLoading.TabIndex = 1;
            lblLoading.Text = "⏳ Đang tải dữ liệu bản đồ lộ trình trực tuyến...";
            lblLoading.TextAlign = ContentAlignment.MiddleCenter;
            lblLoading.Visible = false;
            // 
            // pnlRightToolbar
            // 
            pnlRightToolbar.BackColor = Color.White;
            pnlRightToolbar.Controls.Add(flpMapControls);
            pnlRightToolbar.Controls.Add(lblMapStatus);
            pnlRightToolbar.Dock = DockStyle.Top;
            pnlRightToolbar.Location = new Point(0, 0);
            pnlRightToolbar.Name = "pnlRightToolbar";
            pnlRightToolbar.Padding = new Padding(8, 6, 12, 6);
            pnlRightToolbar.Size = new Size(824, 48);
            pnlRightToolbar.TabIndex = 0;
            // 
            // flpMapControls
            // 
            flpMapControls.Controls.Add(btnLayerStreet);
            flpMapControls.Controls.Add(btnLayerSatellite);
            flpMapControls.Controls.Add(btnLayerVoyager);
            flpMapControls.Dock = DockStyle.Left;
            flpMapControls.Location = new Point(8, 6);
            flpMapControls.Name = "flpMapControls";
            flpMapControls.Size = new Size(360, 36);
            flpMapControls.TabIndex = 0;
            // 
            // btnLayerStreet
            // 
            btnLayerStreet.BackColor = Color.FromArgb(238, 242, 255);
            btnLayerStreet.Cursor = Cursors.Hand;
            btnLayerStreet.FlatAppearance.BorderColor = Color.FromArgb(199, 210, 254);
            btnLayerStreet.FlatStyle = FlatStyle.Flat;
            btnLayerStreet.Font = new Font("Segoe UI", 8.5F);
            btnLayerStreet.ForeColor = Color.FromArgb(67, 56, 202);
            btnLayerStreet.IconChar = FontAwesome.Sharp.IconChar.Map;
            btnLayerStreet.IconColor = Color.FromArgb(67, 56, 202);
            btnLayerStreet.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnLayerStreet.IconSize = 16;
            btnLayerStreet.ImageAlign = ContentAlignment.MiddleLeft;
            btnLayerStreet.Location = new Point(3, 2);
            btnLayerStreet.Margin = new Padding(3, 2, 6, 2);
            btnLayerStreet.Name = "btnLayerStreet";
            btnLayerStreet.Padding = new Padding(6, 0, 8, 0);
            btnLayerStreet.Size = new Size(110, 32);
            btnLayerStreet.TabIndex = 0;
            btnLayerStreet.Text = " Đường phố";
            btnLayerStreet.TextAlign = ContentAlignment.MiddleRight;
            btnLayerStreet.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLayerStreet.UseVisualStyleBackColor = false;
            // 
            // btnLayerSatellite
            // 
            btnLayerSatellite.BackColor = Color.FromArgb(241, 245, 249);
            btnLayerSatellite.Cursor = Cursors.Hand;
            btnLayerSatellite.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnLayerSatellite.FlatStyle = FlatStyle.Flat;
            btnLayerSatellite.Font = new Font("Segoe UI", 8.5F);
            btnLayerSatellite.ForeColor = Color.FromArgb(71, 85, 105);
            btnLayerSatellite.IconChar = FontAwesome.Sharp.IconChar.EarthAmericas;
            btnLayerSatellite.IconColor = Color.FromArgb(71, 85, 105);
            btnLayerSatellite.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnLayerSatellite.IconSize = 16;
            btnLayerSatellite.ImageAlign = ContentAlignment.MiddleLeft;
            btnLayerSatellite.Location = new Point(122, 2);
            btnLayerSatellite.Margin = new Padding(3, 2, 6, 2);
            btnLayerSatellite.Name = "btnLayerSatellite";
            btnLayerSatellite.Padding = new Padding(6, 0, 8, 0);
            btnLayerSatellite.Size = new Size(95, 32);
            btnLayerSatellite.TabIndex = 1;
            btnLayerSatellite.Text = " Vệ tinh";
            btnLayerSatellite.TextAlign = ContentAlignment.MiddleRight;
            btnLayerSatellite.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLayerSatellite.UseVisualStyleBackColor = false;
            // 
            // btnLayerVoyager
            // 
            btnLayerVoyager.BackColor = Color.FromArgb(241, 245, 249);
            btnLayerVoyager.Cursor = Cursors.Hand;
            btnLayerVoyager.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnLayerVoyager.FlatStyle = FlatStyle.Flat;
            btnLayerVoyager.Font = new Font("Segoe UI", 8.5F);
            btnLayerVoyager.ForeColor = Color.FromArgb(71, 85, 105);
            btnLayerVoyager.IconChar = FontAwesome.Sharp.IconChar.Compass;
            btnLayerVoyager.IconColor = Color.FromArgb(71, 85, 105);
            btnLayerVoyager.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnLayerVoyager.IconSize = 16;
            btnLayerVoyager.ImageAlign = ContentAlignment.MiddleLeft;
            btnLayerVoyager.Location = new Point(226, 2);
            btnLayerVoyager.Margin = new Padding(3, 2, 6, 2);
            btnLayerVoyager.Name = "btnLayerVoyager";
            btnLayerVoyager.Padding = new Padding(6, 0, 8, 0);
            btnLayerVoyager.Size = new Size(100, 32);
            btnLayerVoyager.TabIndex = 2;
            btnLayerVoyager.Text = " Hiện đại";
            btnLayerVoyager.TextAlign = ContentAlignment.MiddleRight;
            btnLayerVoyager.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLayerVoyager.UseVisualStyleBackColor = false;
            // 
            // lblMapStatus
            // 
            lblMapStatus.Anchor = AnchorStyles.Right;
            lblMapStatus.AutoSize = true;
            lblMapStatus.Font = new Font("Segoe UI", 8.5F);
            lblMapStatus.ForeColor = Color.FromArgb(100, 116, 139);
            lblMapStatus.Location = new Point(410, 14);
            lblMapStatus.Name = "lblMapStatus";
            lblMapStatus.Size = new Size(400, 20);
            lblMapStatus.TabIndex = 1;
            lblMapStatus.Text = "💡 Click vào một trạm trên bảng hoặc marker để phóng to";
            lblMapStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // UcCustomerRouteMap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitMain);
            Margin = new Padding(3, 4, 3, 4);
            Name = "UcCustomerRouteMap";
            Size = new Size(1300, 850);
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            pnlLeftContent.ResumeLayout(false);
            grpStations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvStations).EndInit();
            flpLeftActions.ResumeLayout(false);
            grpRouteInfo.ResumeLayout(false);
            tlpRouteInfo.ResumeLayout(false);
            tlpRouteInfo.PerformLayout();
            grpSelectRoute.ResumeLayout(false);
            tlpSelect.ResumeLayout(false);
            tlpSelect.PerformLayout();
            pnlRightMap.ResumeLayout(false);
            pnlWebContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webViewMap).EndInit();
            pnlRightToolbar.ResumeLayout(false);
            pnlRightToolbar.PerformLayout();
            flpMapControls.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitMain;
        private Panel pnlLeftContent;
        private GroupBox grpSelectRoute;
        private TableLayoutPanel tlpSelect;
        private Label lblTuyenXe;
        private ComboBox cboTuyenXe;
        private FontAwesome.Sharp.IconButton btnLamMoi;
        private GroupBox grpRouteInfo;
        private TableLayoutPanel tlpRouteInfo;
        private Label lblRouteTitle;
        private Label lblRouteNameVal;
        private Label lblDistanceTitle;
        private Label lblDistanceTimeVal;
        private Label lblPriceTitle;
        private Label lblBasePriceVal;
        private Label lblStationCountTitle;
        private Label lblStationCountVal;
        private GroupBox grpStations;
        private DataGridView dgvStations;
        private DataGridViewTextBoxColumn colOrder;
        private DataGridViewTextBoxColumn colStationName;
        private DataGridViewTextBoxColumn colCity;
        private DataGridViewTextBoxColumn colRole;
        private FlowLayoutPanel flpLeftActions;
        private FontAwesome.Sharp.IconButton btnDatVeTuyenNay;
        private FontAwesome.Sharp.IconButton btnFitBounds;
        private FontAwesome.Sharp.IconButton btnMoGoogleMaps;
        private Panel pnlRightMap;
        private Panel pnlRightToolbar;
        private FlowLayoutPanel flpMapControls;
        private FontAwesome.Sharp.IconButton btnLayerStreet;
        private FontAwesome.Sharp.IconButton btnLayerSatellite;
        private FontAwesome.Sharp.IconButton btnLayerVoyager;
        private Label lblMapStatus;
        private Panel pnlWebContainer;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewMap;
        private Label lblLoading;
    }
}
