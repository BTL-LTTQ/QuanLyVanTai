namespace QuanLyVanTai.UI.UserControls
{
    partial class UcManageRoute
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
            pnlDashboard = new Panel();
            pnlStats = new Panel();
            cardRevenue = new Panel();
            lblCard4Val = new Label();
            lblCard4Title = new Label();
            barCard4 = new Panel();
            cardTotalDistance = new Panel();
            lblCard3Val = new Label();
            lblCard3Title = new Label();
            barCard3 = new Panel();
            cardActiveRoutes = new Panel();
            lblCard2Val = new Label();
            lblCard2Title = new Label();
            barCard2 = new Panel();
            cardTotalRoutes = new Panel();
            lblCard1Val = new Label();
            lblCard1Title = new Label();
            barCard1 = new Panel();
            lblPageTitle = new Label();
            pnlToolbar = new Panel();
            txtSearch = new TextBox();
            btnImport = new FontAwesome.Sharp.IconButton();
            btnExport = new FontAwesome.Sharp.IconButton();
            btnHistory = new FontAwesome.Sharp.IconButton();
            btnCancel = new FontAwesome.Sharp.IconButton();
            btnSave = new FontAwesome.Sharp.IconButton();
            btnDelete = new FontAwesome.Sharp.IconButton();
            btnEdit = new FontAwesome.Sharp.IconButton();
            btnAdd = new FontAwesome.Sharp.IconButton();
            pnlProgress = new Panel();
            pbExcelProgress = new ProgressBar();
            lblExcelProgress = new Label();
            splitMain = new SplitContainer();
            dgvRoutes = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colCode = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colDistance = new DataGridViewTextBoxColumn();
            colHours = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colStations = new DataGridViewTextBoxColumn();
            colVehicles = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colUpdatedBy = new DataGridViewTextBoxColumn();
            pnlDetail = new Panel();
            clbVehicles = new CheckedListBox();
            cboFkVehicle = new ComboBox();
            lblVehiclesTitle = new Label();
            clbStations = new CheckedListBox();
            cboFkStation = new ComboBox();
            lblStationsTitle = new Label();
            numBasePrice = new NumericUpDown();
            lblBasePrice = new Label();
            numEstimatedHours = new NumericUpDown();
            lblEstimatedHours = new Label();
            numDistance = new NumericUpDown();
            lblDistance = new Label();
            txtRouteName = new TextBox();
            lblRouteName = new Label();
            cboStatus = new ComboBox();
            lblRouteStatus = new Label();
            txtRouteCode = new TextBox();
            lblRouteCode = new Label();
            lblDetailTitle = new Label();
            pnlDashboard.SuspendLayout();
            pnlStats.SuspendLayout();
            cardRevenue.SuspendLayout();
            cardTotalDistance.SuspendLayout();
            cardActiveRoutes.SuspendLayout();
            cardTotalRoutes.SuspendLayout();
            pnlToolbar.SuspendLayout();
            pnlProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoutes).BeginInit();
            pnlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numBasePrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numEstimatedHours).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDistance).BeginInit();
            SuspendLayout();
            // 
            // pnlDashboard
            // 
            pnlDashboard.BackColor = Color.FromArgb(248, 250, 252);
            pnlDashboard.Controls.Add(pnlStats);
            pnlDashboard.Controls.Add(lblPageTitle);
            pnlDashboard.Dock = DockStyle.Top;
            pnlDashboard.Location = new Point(0, 0);
            pnlDashboard.Name = "pnlDashboard";
            pnlDashboard.Padding = new Padding(20, 15, 20, 15);
            pnlDashboard.Size = new Size(1250, 160);
            pnlDashboard.TabIndex = 0;
            // 
            // pnlStats
            // 
            pnlStats.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlStats.BackColor = Color.Transparent;
            pnlStats.Controls.Add(cardRevenue);
            pnlStats.Controls.Add(cardTotalDistance);
            pnlStats.Controls.Add(cardActiveRoutes);
            pnlStats.Controls.Add(cardTotalRoutes);
            pnlStats.Location = new Point(20, 50);
            pnlStats.Name = "pnlStats";
            pnlStats.Size = new Size(1210, 95);
            pnlStats.TabIndex = 1;
            // 
            // cardRevenue
            // 
            cardRevenue.BackColor = Color.White;
            cardRevenue.Controls.Add(lblCard4Val);
            cardRevenue.Controls.Add(lblCard4Title);
            cardRevenue.Controls.Add(barCard4);
            cardRevenue.Location = new Point(915, 0);
            cardRevenue.Name = "cardRevenue";
            cardRevenue.Padding = new Padding(15, 12, 15, 12);
            cardRevenue.Size = new Size(290, 95);
            cardRevenue.TabIndex = 3;
            // 
            // lblCard4Val
            // 
            lblCard4Val.AutoSize = true;
            lblCard4Val.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblCard4Val.ForeColor = Color.FromArgb(239, 68, 68);
            lblCard4Val.Location = new Point(25, 38);
            lblCard4Val.Name = "lblCard4Val";
            lblCard4Val.Size = new Size(109, 41);
            lblCard4Val.TabIndex = 2;
            lblCard4Val.Text = "0 VNĐ";
            // 
            // lblCard4Title
            // 
            lblCard4Title.AutoSize = true;
            lblCard4Title.Font = new Font("Segoe UI", 9.5F);
            lblCard4Title.ForeColor = Color.FromArgb(100, 116, 139);
            lblCard4Title.Location = new Point(25, 12);
            lblCard4Title.Name = "lblCard4Title";
            lblCard4Title.Size = new Size(128, 21);
            lblCard4Title.TabIndex = 1;
            lblCard4Title.Text = "💰 Giá TB/Tuyến";
            // 
            // barCard4
            // 
            barCard4.BackColor = Color.FromArgb(239, 68, 68);
            barCard4.Dock = DockStyle.Left;
            barCard4.Location = new Point(15, 12);
            barCard4.Name = "barCard4";
            barCard4.Size = new Size(4, 71);
            barCard4.TabIndex = 0;
            // 
            // cardTotalDistance
            // 
            cardTotalDistance.BackColor = Color.White;
            cardTotalDistance.Controls.Add(lblCard3Val);
            cardTotalDistance.Controls.Add(lblCard3Title);
            cardTotalDistance.Controls.Add(barCard3);
            cardTotalDistance.Location = new Point(610, 0);
            cardTotalDistance.Name = "cardTotalDistance";
            cardTotalDistance.Padding = new Padding(15, 12, 15, 12);
            cardTotalDistance.Size = new Size(290, 95);
            cardTotalDistance.TabIndex = 2;
            // 
            // lblCard3Val
            // 
            lblCard3Val.AutoSize = true;
            lblCard3Val.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblCard3Val.ForeColor = Color.FromArgb(245, 158, 11);
            lblCard3Val.Location = new Point(25, 38);
            lblCard3Val.Name = "lblCard3Val";
            lblCard3Val.Size = new Size(87, 41);
            lblCard3Val.TabIndex = 2;
            lblCard3Val.Text = "0 km";
            // 
            // lblCard3Title
            // 
            lblCard3Title.AutoSize = true;
            lblCard3Title.Font = new Font("Segoe UI", 9.5F);
            lblCard3Title.ForeColor = Color.FromArgb(100, 116, 139);
            lblCard3Title.Location = new Point(25, 12);
            lblCard3Title.Name = "lblCard3Title";
            lblCard3Title.Size = new Size(174, 21);
            lblCard3Title.TabIndex = 1;
            lblCard3Title.Text = "📏 Tổng Quãng Đường";
            // 
            // barCard3
            // 
            barCard3.BackColor = Color.FromArgb(245, 158, 11);
            barCard3.Dock = DockStyle.Left;
            barCard3.Location = new Point(15, 12);
            barCard3.Name = "barCard3";
            barCard3.Size = new Size(4, 71);
            barCard3.TabIndex = 0;
            // 
            // cardActiveRoutes
            // 
            cardActiveRoutes.BackColor = Color.White;
            cardActiveRoutes.Controls.Add(lblCard2Val);
            cardActiveRoutes.Controls.Add(lblCard2Title);
            cardActiveRoutes.Controls.Add(barCard2);
            cardActiveRoutes.Location = new Point(305, 0);
            cardActiveRoutes.Name = "cardActiveRoutes";
            cardActiveRoutes.Padding = new Padding(15, 12, 15, 12);
            cardActiveRoutes.Size = new Size(290, 95);
            cardActiveRoutes.TabIndex = 1;
            // 
            // lblCard2Val
            // 
            lblCard2Val.AutoSize = true;
            lblCard2Val.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblCard2Val.ForeColor = Color.FromArgb(16, 185, 129);
            lblCard2Val.Location = new Point(25, 38);
            lblCard2Val.Name = "lblCard2Val";
            lblCard2Val.Size = new Size(35, 41);
            lblCard2Val.TabIndex = 2;
            lblCard2Val.Text = "0";
            // 
            // lblCard2Title
            // 
            lblCard2Title.AutoSize = true;
            lblCard2Title.Font = new Font("Segoe UI", 9.5F);
            lblCard2Title.ForeColor = Color.FromArgb(100, 116, 139);
            lblCard2Title.Location = new Point(25, 12);
            lblCard2Title.Name = "lblCard2Title";
            lblCard2Title.Size = new Size(152, 21);
            lblCard2Title.TabIndex = 1;
            lblCard2Title.Text = "✅ Đang Hoạt Động";
            // 
            // barCard2
            // 
            barCard2.BackColor = Color.FromArgb(16, 185, 129);
            barCard2.Dock = DockStyle.Left;
            barCard2.Location = new Point(15, 12);
            barCard2.Name = "barCard2";
            barCard2.Size = new Size(4, 71);
            barCard2.TabIndex = 0;
            // 
            // cardTotalRoutes
            // 
            cardTotalRoutes.BackColor = Color.White;
            cardTotalRoutes.Controls.Add(lblCard1Val);
            cardTotalRoutes.Controls.Add(lblCard1Title);
            cardTotalRoutes.Controls.Add(barCard1);
            cardTotalRoutes.Location = new Point(0, 0);
            cardTotalRoutes.Name = "cardTotalRoutes";
            cardTotalRoutes.Padding = new Padding(15, 12, 15, 12);
            cardTotalRoutes.Size = new Size(290, 95);
            cardTotalRoutes.TabIndex = 0;
            // 
            // lblCard1Val
            // 
            lblCard1Val.AutoSize = true;
            lblCard1Val.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblCard1Val.ForeColor = Color.FromArgb(59, 130, 246);
            lblCard1Val.Location = new Point(25, 38);
            lblCard1Val.Name = "lblCard1Val";
            lblCard1Val.Size = new Size(35, 41);
            lblCard1Val.TabIndex = 2;
            lblCard1Val.Text = "0";
            // 
            // lblCard1Title
            // 
            lblCard1Title.AutoSize = true;
            lblCard1Title.Font = new Font("Segoe UI", 9.5F);
            lblCard1Title.ForeColor = Color.FromArgb(100, 116, 139);
            lblCard1Title.Location = new Point(25, 12);
            lblCard1Title.Name = "lblCard1Title";
            lblCard1Title.Size = new Size(138, 21);
            lblCard1Title.TabIndex = 1;
            lblCard1Title.Text = "🚌 Tổng Tuyến Xe";
            // 
            // barCard1
            // 
            barCard1.BackColor = Color.FromArgb(59, 130, 246);
            barCard1.Dock = DockStyle.Left;
            barCard1.Location = new Point(15, 12);
            barCard1.Name = "barCard1";
            barCard1.Size = new Size(4, 71);
            barCard1.TabIndex = 0;
            // 
            // lblPageTitle
            // 
            lblPageTitle.AutoSize = true;
            lblPageTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblPageTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblPageTitle.Location = new Point(20, 15);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new Size(544, 30);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "📊 TRANG CHỦ - DASHBOARD QUẢN LÝ TUYẾN XE";
            // 
            // pnlToolbar
            // 
            pnlToolbar.BackColor = Color.White;
            pnlToolbar.Controls.Add(txtSearch);
            pnlToolbar.Controls.Add(btnImport);
            pnlToolbar.Controls.Add(btnExport);
            pnlToolbar.Controls.Add(btnHistory);
            pnlToolbar.Controls.Add(btnCancel);
            pnlToolbar.Controls.Add(btnSave);
            pnlToolbar.Controls.Add(btnDelete);
            pnlToolbar.Controls.Add(btnEdit);
            pnlToolbar.Controls.Add(btnAdd);
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Location = new Point(0, 160);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Padding = new Padding(20, 15, 20, 15);
            pnlToolbar.Size = new Size(1250, 75);
            pnlToolbar.TabIndex = 1;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.Location = new Point(1056, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "🔍 Tìm kiếm tuyến xe, lộ trình...";
            txtSearch.Size = new Size(174, 32);
            txtSearch.TabIndex = 8;
            // 
            // btnImport
            // 
            btnImport.BackColor = Color.FromArgb(245, 158, 11);
            btnImport.FlatAppearance.BorderSize = 0;
            btnImport.FlatStyle = FlatStyle.Flat;
            btnImport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnImport.ForeColor = Color.White;
            btnImport.IconChar = FontAwesome.Sharp.IconChar.ArrowRightToFile;
            btnImport.IconColor = Color.White;
            btnImport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnImport.IconSize = 22;
            btnImport.ImageAlign = ContentAlignment.MiddleLeft;
            btnImport.Location = new Point(920, 15);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(130, 45);
            btnImport.TabIndex = 7;
            btnImport.Text = " Nhập Excel";
            btnImport.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnImport.UseVisualStyleBackColor = false;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.FromArgb(16, 185, 129);
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExport.ForeColor = Color.White;
            btnExport.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            btnExport.IconColor = Color.White;
            btnExport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnExport.IconSize = 22;
            btnExport.ImageAlign = ContentAlignment.MiddleLeft;
            btnExport.Location = new Point(780, 15);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(130, 45);
            btnExport.TabIndex = 6;
            btnExport.Text = " Xuất Excel";
            btnExport.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnExport.UseVisualStyleBackColor = false;
            // 
            // btnHistory
            // 
            btnHistory.BackColor = Color.FromArgb(100, 116, 139);
            btnHistory.FlatAppearance.BorderSize = 0;
            btnHistory.FlatStyle = FlatStyle.Flat;
            btnHistory.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnHistory.ForeColor = Color.White;
            btnHistory.IconChar = FontAwesome.Sharp.IconChar.ClockRotateLeft;
            btnHistory.IconColor = Color.White;
            btnHistory.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnHistory.IconSize = 22;
            btnHistory.ImageAlign = ContentAlignment.MiddleLeft;
            btnHistory.Location = new Point(650, 15);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(120, 45);
            btnHistory.TabIndex = 5;
            btnHistory.Text = " Lịch Sử";
            btnHistory.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnHistory.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(100, 116, 139);
            btnCancel.Enabled = false;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnCancel.IconColor = Color.White;
            btnCancel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancel.IconSize = 22;
            btnCancel.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancel.Location = new Point(540, 15);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 45);
            btnCancel.TabIndex = 4;
            btnCancel.Text = " Hủy";
            btnCancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(59, 130, 246);
            btnSave.Enabled = false;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnSave.IconColor = Color.White;
            btnSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSave.IconSize = 22;
            btnSave.ImageAlign = ContentAlignment.MiddleLeft;
            btnSave.Location = new Point(420, 15);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(110, 45);
            btnSave.TabIndex = 3;
            btnSave.Text = " Lưu";
            btnSave.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(239, 68, 68);
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            btnDelete.IconColor = Color.White;
            btnDelete.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDelete.IconSize = 22;
            btnDelete.ImageAlign = ContentAlignment.MiddleLeft;
            btnDelete.Location = new Point(300, 15);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(110, 45);
            btnDelete.TabIndex = 2;
            btnDelete.Text = " Xóa";
            btnDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.FromArgb(59, 130, 246);
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEdit.ForeColor = Color.White;
            btnEdit.IconChar = FontAwesome.Sharp.IconChar.Edit;
            btnEdit.IconColor = Color.White;
            btnEdit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEdit.IconSize = 22;
            btnEdit.ImageAlign = ContentAlignment.MiddleLeft;
            btnEdit.Location = new Point(160, 15);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(130, 45);
            btnEdit.TabIndex = 1;
            btnEdit.Text = " Chỉnh Sửa";
            btnEdit.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(16, 185, 129);
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            btnAdd.IconColor = Color.White;
            btnAdd.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAdd.IconSize = 22;
            btnAdd.ImageAlign = ContentAlignment.MiddleLeft;
            btnAdd.Location = new Point(20, 15);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(130, 45);
            btnAdd.TabIndex = 0;
            btnAdd.Text = " Thêm Tuyến";
            btnAdd.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // pnlProgress
            // 
            pnlProgress.BackColor = Color.White;
            pnlProgress.Controls.Add(pbExcelProgress);
            pnlProgress.Controls.Add(lblExcelProgress);
            pnlProgress.Dock = DockStyle.Bottom;
            pnlProgress.Location = new Point(0, 714);
            pnlProgress.Name = "pnlProgress";
            pnlProgress.Padding = new Padding(20, 8, 20, 8);
            pnlProgress.Size = new Size(1250, 46);
            pnlProgress.TabIndex = 2;
            // 
            // pbExcelProgress
            // 
            pbExcelProgress.Dock = DockStyle.Fill;
            pbExcelProgress.Location = new Point(20, 8);
            pbExcelProgress.Name = "pbExcelProgress";
            pbExcelProgress.Size = new Size(1020, 30);
            pbExcelProgress.TabIndex = 0;
            pbExcelProgress.Visible = false;
            // 
            // lblExcelProgress
            // 
            lblExcelProgress.Dock = DockStyle.Right;
            lblExcelProgress.Font = new Font("Segoe UI", 9F);
            lblExcelProgress.ForeColor = Color.FromArgb(71, 85, 105);
            lblExcelProgress.Location = new Point(1040, 8);
            lblExcelProgress.Name = "lblExcelProgress";
            lblExcelProgress.Size = new Size(190, 30);
            lblExcelProgress.TabIndex = 1;
            lblExcelProgress.Text = "Tiến độ: 0%";
            lblExcelProgress.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // splitMain
            // 
            splitMain.BackColor = Color.FromArgb(226, 232, 240);
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(0, 235);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(dgvRoutes);
            splitMain.Panel1MinSize = 400;
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(pnlDetail);
            splitMain.Panel2MinSize = 380;
            splitMain.Size = new Size(1250, 479);
            splitMain.SplitterDistance = 750;
            splitMain.SplitterWidth = 6;
            splitMain.TabIndex = 3;
            // 
            // dgvRoutes
            // 
            dgvRoutes.AllowUserToAddRows = false;
            dgvRoutes.AllowUserToDeleteRows = false;
            dgvRoutes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoutes.BackgroundColor = Color.White;
            dgvRoutes.BorderStyle = BorderStyle.None;
            dgvRoutes.ColumnHeadersHeight = 44;
            dgvRoutes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvRoutes.Columns.AddRange(new DataGridViewColumn[] { colId, colCode, colName, colDistance, colHours, colPrice, colStations, colVehicles, colStatus, colUpdatedBy });
            dgvRoutes.Dock = DockStyle.Fill;
            dgvRoutes.EnableHeadersVisualStyles = false;
            dgvRoutes.Location = new Point(0, 0);
            dgvRoutes.MultiSelect = false;
            dgvRoutes.Name = "dgvRoutes";
            dgvRoutes.ReadOnly = true;
            dgvRoutes.RowHeadersVisible = false;
            dgvRoutes.RowHeadersWidth = 51;
            dgvRoutes.RowTemplate.Height = 44;
            dgvRoutes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoutes.Size = new Size(750, 479);
            dgvRoutes.TabIndex = 0;
            // 
            // colId
            // 
            colId.HeaderText = "ID";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Visible = false;
            // 
            // colCode
            // 
            colCode.HeaderText = "Mã Tuyến";
            colCode.MinimumWidth = 6;
            colCode.Name = "colCode";
            colCode.ReadOnly = true;
            // 
            // colName
            // 
            colName.HeaderText = "Tên Lộ Trình";
            colName.MinimumWidth = 6;
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colDistance
            // 
            colDistance.HeaderText = "Cự Ly (km)";
            colDistance.MinimumWidth = 6;
            colDistance.Name = "colDistance";
            colDistance.ReadOnly = true;
            // 
            // colHours
            // 
            colHours.HeaderText = "Thời Gian (h)";
            colHours.MinimumWidth = 6;
            colHours.Name = "colHours";
            colHours.ReadOnly = true;
            // 
            // colPrice
            // 
            colPrice.HeaderText = "Giá Cước (VNĐ)";
            colPrice.MinimumWidth = 6;
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            // 
            // colStations
            // 
            colStations.HeaderText = "Số Trạm";
            colStations.MinimumWidth = 6;
            colStations.Name = "colStations";
            colStations.ReadOnly = true;
            // 
            // colVehicles
            // 
            colVehicles.HeaderText = "Số Xe";
            colVehicles.MinimumWidth = 6;
            colVehicles.Name = "colVehicles";
            colVehicles.ReadOnly = true;
            // 
            // colStatus
            // 
            colStatus.HeaderText = "Trạng Thái";
            colStatus.MinimumWidth = 6;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            // 
            // colUpdatedBy
            // 
            colUpdatedBy.HeaderText = "Người Cập Nhật";
            colUpdatedBy.MinimumWidth = 6;
            colUpdatedBy.Name = "colUpdatedBy";
            colUpdatedBy.ReadOnly = true;
            // 
            // pnlDetail
            // 
            pnlDetail.AutoScroll = true;
            pnlDetail.BackColor = Color.White;
            pnlDetail.Controls.Add(clbVehicles);
            pnlDetail.Controls.Add(cboFkVehicle);
            pnlDetail.Controls.Add(lblVehiclesTitle);
            pnlDetail.Controls.Add(clbStations);
            pnlDetail.Controls.Add(cboFkStation);
            pnlDetail.Controls.Add(lblStationsTitle);
            pnlDetail.Controls.Add(numBasePrice);
            pnlDetail.Controls.Add(lblBasePrice);
            pnlDetail.Controls.Add(numEstimatedHours);
            pnlDetail.Controls.Add(lblEstimatedHours);
            pnlDetail.Controls.Add(numDistance);
            pnlDetail.Controls.Add(lblDistance);
            pnlDetail.Controls.Add(txtRouteName);
            pnlDetail.Controls.Add(lblRouteName);
            pnlDetail.Controls.Add(cboStatus);
            pnlDetail.Controls.Add(lblRouteStatus);
            pnlDetail.Controls.Add(txtRouteCode);
            pnlDetail.Controls.Add(lblRouteCode);
            pnlDetail.Controls.Add(lblDetailTitle);
            pnlDetail.Dock = DockStyle.Fill;
            pnlDetail.Location = new Point(0, 0);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Padding = new Padding(18);
            pnlDetail.Size = new Size(494, 479);
            pnlDetail.TabIndex = 0;
            // 
            // clbVehicles
            // 
            clbVehicles.CheckOnClick = true;
            clbVehicles.Font = new Font("Segoe UI", 9F);
            clbVehicles.FormattingEnabled = true;
            clbVehicles.Location = new Point(18, 458);
            clbVehicles.Name = "clbVehicles";
            clbVehicles.Size = new Size(382, 92);
            clbVehicles.TabIndex = 18;
            // 
            // cboFkVehicle
            // 
            cboFkVehicle.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFkVehicle.Font = new Font("Segoe UI", 9F);
            cboFkVehicle.FormattingEnabled = true;
            cboFkVehicle.Location = new Point(18, 424);
            cboFkVehicle.Name = "cboFkVehicle";
            cboFkVehicle.Size = new Size(382, 28);
            cboFkVehicle.TabIndex = 17;
            // 
            // lblVehiclesTitle
            // 
            lblVehiclesTitle.AutoSize = true;
            lblVehiclesTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblVehiclesTitle.ForeColor = Color.FromArgb(59, 130, 246);
            lblVehiclesTitle.Location = new Point(18, 400);
            lblVehiclesTitle.Name = "lblVehiclesTitle";
            lblVehiclesTitle.Size = new Size(334, 21);
            lblVehiclesTitle.TabIndex = 16;
            lblVehiclesTitle.Text = "🚌 Phương Tiện Phân Công (Liên kết 1-N):";
            // 
            // clbStations
            // 
            clbStations.CheckOnClick = true;
            clbStations.Font = new Font("Segoe UI", 9F);
            clbStations.FormattingEnabled = true;
            clbStations.Location = new Point(18, 303);
            clbStations.Name = "clbStations";
            clbStations.Size = new Size(382, 92);
            clbStations.TabIndex = 15;
            // 
            // cboFkStation
            // 
            cboFkStation.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFkStation.Font = new Font("Segoe UI", 9F);
            cboFkStation.FormattingEnabled = true;
            cboFkStation.Location = new Point(18, 269);
            cboFkStation.Name = "cboFkStation";
            cboFkStation.Size = new Size(382, 28);
            cboFkStation.TabIndex = 14;
            // 
            // lblStationsTitle
            // 
            lblStationsTitle.AutoSize = true;
            lblStationsTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStationsTitle.ForeColor = Color.FromArgb(59, 130, 246);
            lblStationsTitle.Location = new Point(18, 245);
            lblStationsTitle.Name = "lblStationsTitle";
            lblStationsTitle.Size = new Size(354, 21);
            lblStationsTitle.TabIndex = 13;
            lblStationsTitle.Text = "📍 Trạm Dừng Thuộc Lộ Trình (Liên kết N-N):";
            // 
            // numBasePrice
            // 
            numBasePrice.Font = new Font("Segoe UI", 9.5F);
            numBasePrice.Increment = new decimal(new int[] { 10000, 0, 0, 0 });
            numBasePrice.Location = new Point(270, 204);
            numBasePrice.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            numBasePrice.Name = "numBasePrice";
            numBasePrice.Size = new Size(130, 29);
            numBasePrice.TabIndex = 12;
            numBasePrice.ThousandsSeparator = true;
            // 
            // lblBasePrice
            // 
            lblBasePrice.AutoSize = true;
            lblBasePrice.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBasePrice.ForeColor = Color.FromArgb(71, 85, 105);
            lblBasePrice.Location = new Point(270, 180);
            lblBasePrice.Name = "lblBasePrice";
            lblBasePrice.Size = new Size(106, 20);
            lblBasePrice.TabIndex = 11;
            lblBasePrice.Text = "Giá Vé (VNĐ):";
            // 
            // numEstimatedHours
            // 
            numEstimatedHours.DecimalPlaces = 1;
            numEstimatedHours.Font = new Font("Segoe UI", 9.5F);
            numEstimatedHours.Location = new Point(150, 204);
            numEstimatedHours.Name = "numEstimatedHours";
            numEstimatedHours.Size = new Size(110, 29);
            numEstimatedHours.TabIndex = 10;
            // 
            // lblEstimatedHours
            // 
            lblEstimatedHours.AutoSize = true;
            lblEstimatedHours.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEstimatedHours.ForeColor = Color.FromArgb(71, 85, 105);
            lblEstimatedHours.Location = new Point(150, 180);
            lblEstimatedHours.Name = "lblEstimatedHours";
            lblEstimatedHours.Size = new Size(118, 20);
            lblEstimatedHours.TabIndex = 9;
            lblEstimatedHours.Text = "Thời Gian (giờ):";
            // 
            // numDistance
            // 
            numDistance.DecimalPlaces = 1;
            numDistance.Font = new Font("Segoe UI", 9.5F);
            numDistance.Location = new Point(18, 204);
            numDistance.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            numDistance.Name = "numDistance";
            numDistance.Size = new Size(120, 29);
            numDistance.TabIndex = 8;
            // 
            // lblDistance
            // 
            lblDistance.AutoSize = true;
            lblDistance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDistance.ForeColor = Color.FromArgb(71, 85, 105);
            lblDistance.Location = new Point(18, 180);
            lblDistance.Name = "lblDistance";
            lblDistance.Size = new Size(89, 20);
            lblDistance.TabIndex = 7;
            lblDistance.Text = "Cự Ly (km):";
            // 
            // txtRouteName
            // 
            txtRouteName.Font = new Font("Segoe UI", 9.5F);
            txtRouteName.Location = new Point(18, 139);
            txtRouteName.Name = "txtRouteName";
            txtRouteName.Size = new Size(382, 29);
            txtRouteName.TabIndex = 6;
            // 
            // lblRouteName
            // 
            lblRouteName.AutoSize = true;
            lblRouteName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRouteName.ForeColor = Color.FromArgb(71, 85, 105);
            lblRouteName.Location = new Point(18, 115);
            lblRouteName.Name = "lblRouteName";
            lblRouteName.Size = new Size(167, 20);
            lblRouteName.TabIndex = 5;
            lblRouteName.Text = "Tên Tuyến / Lộ Trình *:";
            // 
            // cboStatus
            // 
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.Font = new Font("Segoe UI", 9.5F);
            cboStatus.FormattingEnabled = true;
            cboStatus.Items.AddRange(new object[] { "Active", "Suspended" });
            cboStatus.Location = new Point(215, 74);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(185, 29);
            cboStatus.TabIndex = 4;
            // 
            // lblRouteStatus
            // 
            lblRouteStatus.AutoSize = true;
            lblRouteStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRouteStatus.ForeColor = Color.FromArgb(71, 85, 105);
            lblRouteStatus.Location = new Point(215, 50);
            lblRouteStatus.Name = "lblRouteStatus";
            lblRouteStatus.Size = new Size(87, 20);
            lblRouteStatus.TabIndex = 3;
            lblRouteStatus.Text = "Trạng Thái:";
            // 
            // txtRouteCode
            // 
            txtRouteCode.Font = new Font("Segoe UI", 9.5F);
            txtRouteCode.Location = new Point(18, 74);
            txtRouteCode.Name = "txtRouteCode";
            txtRouteCode.Size = new Size(180, 29);
            txtRouteCode.TabIndex = 2;
            // 
            // lblRouteCode
            // 
            lblRouteCode.AutoSize = true;
            lblRouteCode.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRouteCode.ForeColor = Color.FromArgb(71, 85, 105);
            lblRouteCode.Location = new Point(18, 50);
            lblRouteCode.Name = "lblRouteCode";
            lblRouteCode.Size = new Size(114, 20);
            lblRouteCode.TabIndex = 1;
            lblRouteCode.Text = "Mã Tuyến Xe *:";
            // 
            // lblDetailTitle
            // 
            lblDetailTitle.AutoSize = true;
            lblDetailTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDetailTitle.ForeColor = Color.FromArgb(59, 130, 246);
            lblDetailTitle.Location = new Point(18, 15);
            lblDetailTitle.Name = "lblDetailTitle";
            lblDetailTitle.Size = new Size(362, 25);
            lblDetailTitle.TabIndex = 0;
            lblDetailTitle.Text = "CHI TIẾT TUYẾN XE & LIÊN KẾT ĐA BẢNG";
            // 
            // UcManageRoute
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            Controls.Add(splitMain);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlDashboard);
            Controls.Add(pnlProgress);
            Name = "UcManageRoute";
            Size = new Size(1250, 760);
            pnlDashboard.ResumeLayout(false);
            pnlDashboard.PerformLayout();
            pnlStats.ResumeLayout(false);
            cardRevenue.ResumeLayout(false);
            cardRevenue.PerformLayout();
            cardTotalDistance.ResumeLayout(false);
            cardTotalDistance.PerformLayout();
            cardActiveRoutes.ResumeLayout(false);
            cardActiveRoutes.PerformLayout();
            cardTotalRoutes.ResumeLayout(false);
            cardTotalRoutes.PerformLayout();
            pnlToolbar.ResumeLayout(false);
            pnlToolbar.PerformLayout();
            pnlProgress.ResumeLayout(false);
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRoutes).EndInit();
            pnlDetail.ResumeLayout(false);
            pnlDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numBasePrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)numEstimatedHours).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDistance).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDashboard;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.Panel cardTotalRoutes;
        private System.Windows.Forms.Label lblCard1Title;
        private System.Windows.Forms.Label lblCard1Val;
        private System.Windows.Forms.Panel barCard1;
        private System.Windows.Forms.Panel cardActiveRoutes;
        private System.Windows.Forms.Label lblCard2Title;
        private System.Windows.Forms.Label lblCard2Val;
        private System.Windows.Forms.Panel barCard2;
        private System.Windows.Forms.Panel cardTotalDistance;
        private System.Windows.Forms.Label lblCard3Title;
        private System.Windows.Forms.Label lblCard3Val;
        private System.Windows.Forms.Panel barCard3;
        private System.Windows.Forms.Panel cardRevenue;
        private System.Windows.Forms.Label lblCard4Title;
        private System.Windows.Forms.Label lblCard4Val;
        private System.Windows.Forms.Panel barCard4;
        private System.Windows.Forms.Panel pnlToolbar;
        private FontAwesome.Sharp.IconButton btnAdd;
        private FontAwesome.Sharp.IconButton btnEdit;
        private FontAwesome.Sharp.IconButton btnDelete;
        private FontAwesome.Sharp.IconButton btnSave;
        private FontAwesome.Sharp.IconButton btnCancel;
        private FontAwesome.Sharp.IconButton btnHistory;
        private FontAwesome.Sharp.IconButton btnExport;
        private FontAwesome.Sharp.IconButton btnImport;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel pnlProgress;
        private System.Windows.Forms.ProgressBar pbExcelProgress;
        private System.Windows.Forms.Label lblExcelProgress;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.DataGridView dgvRoutes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStations;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVehicles;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUpdatedBy;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.Label lblRouteCode;
        private System.Windows.Forms.TextBox txtRouteCode;
        private System.Windows.Forms.Label lblRouteStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblRouteName;
        private System.Windows.Forms.TextBox txtRouteName;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.NumericUpDown numDistance;
        private System.Windows.Forms.Label lblEstimatedHours;
        private System.Windows.Forms.NumericUpDown numEstimatedHours;
        private System.Windows.Forms.Label lblBasePrice;
        private System.Windows.Forms.NumericUpDown numBasePrice;
        private System.Windows.Forms.Label lblStationsTitle;
        private System.Windows.Forms.ComboBox cboFkStation;
        private System.Windows.Forms.CheckedListBox clbStations;
        private System.Windows.Forms.Label lblVehiclesTitle;
        private System.Windows.Forms.ComboBox cboFkVehicle;
        private System.Windows.Forms.CheckedListBox clbVehicles;
    }
}
