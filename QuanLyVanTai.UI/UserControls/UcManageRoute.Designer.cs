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
            this.pnlDashboard = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.pnlStats = new System.Windows.Forms.Panel();
            this.cardTotalRoutes = new System.Windows.Forms.Panel();
            this.lblCard1Title = new System.Windows.Forms.Label();
            this.lblCard1Val = new System.Windows.Forms.Label();
            this.barCard1 = new System.Windows.Forms.Panel();
            this.cardActiveRoutes = new System.Windows.Forms.Panel();
            this.lblCard2Title = new System.Windows.Forms.Label();
            this.lblCard2Val = new System.Windows.Forms.Label();
            this.barCard2 = new System.Windows.Forms.Panel();
            this.cardTotalDistance = new System.Windows.Forms.Panel();
            this.lblCard3Title = new System.Windows.Forms.Label();
            this.lblCard3Val = new System.Windows.Forms.Label();
            this.barCard3 = new System.Windows.Forms.Panel();
            this.cardRevenue = new System.Windows.Forms.Panel();
            this.lblCard4Title = new System.Windows.Forms.Label();
            this.lblCard4Val = new System.Windows.Forms.Label();
            this.barCard4 = new System.Windows.Forms.Panel();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.btnAdd = new FontAwesome.Sharp.IconButton();
            this.btnEdit = new FontAwesome.Sharp.IconButton();
            this.btnDelete = new FontAwesome.Sharp.IconButton();
            this.btnSave = new FontAwesome.Sharp.IconButton();
            this.btnCancel = new FontAwesome.Sharp.IconButton();
            this.btnHistory = new FontAwesome.Sharp.IconButton();
            this.btnExport = new FontAwesome.Sharp.IconButton();
            this.btnImport = new FontAwesome.Sharp.IconButton();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.pbExcelProgress = new System.Windows.Forms.ProgressBar();
            this.lblExcelProgress = new System.Windows.Forms.Label();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.dgvRoutes = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStations = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVehicles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUpdatedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.lblDetailTitle = new System.Windows.Forms.Label();
            this.lblRouteCode = new System.Windows.Forms.Label();
            this.txtRouteCode = new System.Windows.Forms.TextBox();
            this.lblRouteStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblRouteName = new System.Windows.Forms.Label();
            this.txtRouteName = new System.Windows.Forms.TextBox();
            this.lblDistance = new System.Windows.Forms.Label();
            this.numDistance = new System.Windows.Forms.NumericUpDown();
            this.lblEstimatedHours = new System.Windows.Forms.Label();
            this.numEstimatedHours = new System.Windows.Forms.NumericUpDown();
            this.lblBasePrice = new System.Windows.Forms.Label();
            this.numBasePrice = new System.Windows.Forms.NumericUpDown();
            this.lblStationsTitle = new System.Windows.Forms.Label();
            this.cboFkStation = new System.Windows.Forms.ComboBox();
            this.clbStations = new System.Windows.Forms.CheckedListBox();
            this.lblVehiclesTitle = new System.Windows.Forms.Label();
            this.cboFkVehicle = new System.Windows.Forms.ComboBox();
            this.clbVehicles = new System.Windows.Forms.CheckedListBox();
            this.pnlDashboard.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.cardTotalRoutes.SuspendLayout();
            this.cardActiveRoutes.SuspendLayout();
            this.cardTotalDistance.SuspendLayout();
            this.cardRevenue.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoutes)).BeginInit();
            this.pnlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEstimatedHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBasePrice)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlDashboard.Controls.Add(this.pnlStats);
            this.pnlDashboard.Controls.Add(this.lblPageTitle);
            this.pnlDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDashboard.Location = new System.Drawing.Point(0, 0);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.pnlDashboard.Size = new System.Drawing.Size(1250, 160);
            this.pnlDashboard.TabIndex = 0;
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblPageTitle.Location = new System.Drawing.Point(20, 15);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(465, 30);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "📊 TRANG CHỦ - DASHBOARD QUẢN LÝ TUYẾN XE";
            // 
            // pnlStats
            // 
            this.pnlStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStats.BackColor = System.Drawing.Color.Transparent;
            this.pnlStats.Controls.Add(this.cardRevenue);
            this.pnlStats.Controls.Add(this.cardTotalDistance);
            this.pnlStats.Controls.Add(this.cardActiveRoutes);
            this.pnlStats.Controls.Add(this.cardTotalRoutes);
            this.pnlStats.Location = new System.Drawing.Point(20, 50);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(1210, 95);
            this.pnlStats.TabIndex = 1;
            // 
            // cardTotalRoutes
            // 
            this.cardTotalRoutes.BackColor = System.Drawing.Color.White;
            this.cardTotalRoutes.Controls.Add(this.lblCard1Val);
            this.cardTotalRoutes.Controls.Add(this.lblCard1Title);
            this.cardTotalRoutes.Controls.Add(this.barCard1);
            this.cardTotalRoutes.Location = new System.Drawing.Point(0, 0);
            this.cardTotalRoutes.Name = "cardTotalRoutes";
            this.cardTotalRoutes.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.cardTotalRoutes.Size = new System.Drawing.Size(290, 95);
            this.cardTotalRoutes.TabIndex = 0;
            // 
            // barCard1
            // 
            this.barCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.barCard1.Dock = System.Windows.Forms.DockStyle.Left;
            this.barCard1.Location = new System.Drawing.Point(15, 12);
            this.barCard1.Name = "barCard1";
            this.barCard1.Size = new System.Drawing.Size(4, 71);
            this.barCard1.TabIndex = 0;
            // 
            // lblCard1Title
            // 
            this.lblCard1Title.AutoSize = true;
            this.lblCard1Title.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCard1Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCard1Title.Location = new System.Drawing.Point(25, 12);
            this.lblCard1Title.Name = "lblCard1Title";
            this.lblCard1Title.Size = new System.Drawing.Size(128, 21);
            this.lblCard1Title.TabIndex = 1;
            this.lblCard1Title.Text = "🚌 Tổng Tuyến Xe";
            // 
            // lblCard1Val
            // 
            this.lblCard1Val.AutoSize = true;
            this.lblCard1Val.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblCard1Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblCard1Val.Location = new System.Drawing.Point(25, 38);
            this.lblCard1Val.Name = "lblCard1Val";
            this.lblCard1Val.Size = new System.Drawing.Size(35, 41);
            this.lblCard1Val.TabIndex = 2;
            this.lblCard1Val.Text = "0";
            // 
            // cardActiveRoutes
            // 
            this.cardActiveRoutes.BackColor = System.Drawing.Color.White;
            this.cardActiveRoutes.Controls.Add(this.lblCard2Val);
            this.cardActiveRoutes.Controls.Add(this.lblCard2Title);
            this.cardActiveRoutes.Controls.Add(this.barCard2);
            this.cardActiveRoutes.Location = new System.Drawing.Point(305, 0);
            this.cardActiveRoutes.Name = "cardActiveRoutes";
            this.cardActiveRoutes.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.cardActiveRoutes.Size = new System.Drawing.Size(290, 95);
            this.cardActiveRoutes.TabIndex = 1;
            // 
            // barCard2
            // 
            this.barCard2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.barCard2.Dock = System.Windows.Forms.DockStyle.Left;
            this.barCard2.Location = new System.Drawing.Point(15, 12);
            this.barCard2.Name = "barCard2";
            this.barCard2.Size = new System.Drawing.Size(4, 71);
            this.barCard2.TabIndex = 0;
            // 
            // lblCard2Title
            // 
            this.lblCard2Title.AutoSize = true;
            this.lblCard2Title.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCard2Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCard2Title.Location = new System.Drawing.Point(25, 12);
            this.lblCard2Title.Name = "lblCard2Title";
            this.lblCard2Title.Size = new System.Drawing.Size(149, 21);
            this.lblCard2Title.TabIndex = 1;
            this.lblCard2Title.Text = "✅ Đang Hoạt Động";
            // 
            // lblCard2Val
            // 
            this.lblCard2Val.AutoSize = true;
            this.lblCard2Val.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblCard2Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblCard2Val.Location = new System.Drawing.Point(25, 38);
            this.lblCard2Val.Name = "lblCard2Val";
            this.lblCard2Val.Size = new System.Drawing.Size(35, 41);
            this.lblCard2Val.TabIndex = 2;
            this.lblCard2Val.Text = "0";
            // 
            // cardTotalDistance
            // 
            this.cardTotalDistance.BackColor = System.Drawing.Color.White;
            this.cardTotalDistance.Controls.Add(this.lblCard3Val);
            this.cardTotalDistance.Controls.Add(this.lblCard3Title);
            this.cardTotalDistance.Controls.Add(this.barCard3);
            this.cardTotalDistance.Location = new System.Drawing.Point(610, 0);
            this.cardTotalDistance.Name = "cardTotalDistance";
            this.cardTotalDistance.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.cardTotalDistance.Size = new System.Drawing.Size(290, 95);
            this.cardTotalDistance.TabIndex = 2;
            // 
            // barCard3
            // 
            this.barCard3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.barCard3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barCard3.Location = new System.Drawing.Point(15, 12);
            this.barCard3.Name = "barCard3";
            this.barCard3.Size = new System.Drawing.Size(4, 71);
            this.barCard3.TabIndex = 0;
            // 
            // lblCard3Title
            // 
            this.lblCard3Title.AutoSize = true;
            this.lblCard3Title.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCard3Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCard3Title.Location = new System.Drawing.Point(25, 12);
            this.lblCard3Title.Name = "lblCard3Title";
            this.lblCard3Title.Size = new System.Drawing.Size(161, 21);
            this.lblCard3Title.TabIndex = 1;
            this.lblCard3Title.Text = "📏 Tổng Quãng Đường";
            // 
            // lblCard3Val
            // 
            this.lblCard3Val.AutoSize = true;
            this.lblCard3Val.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblCard3Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.lblCard3Val.Location = new System.Drawing.Point(25, 38);
            this.lblCard3Val.Name = "lblCard3Val";
            this.lblCard3Val.Size = new System.Drawing.Size(89, 41);
            this.lblCard3Val.TabIndex = 2;
            this.lblCard3Val.Text = "0 km";
            // 
            // cardRevenue
            // 
            this.cardRevenue.BackColor = System.Drawing.Color.White;
            this.cardRevenue.Controls.Add(this.lblCard4Val);
            this.cardRevenue.Controls.Add(this.lblCard4Title);
            this.cardRevenue.Controls.Add(this.barCard4);
            this.cardRevenue.Location = new System.Drawing.Point(915, 0);
            this.cardRevenue.Name = "cardRevenue";
            this.cardRevenue.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.cardRevenue.Size = new System.Drawing.Size(290, 95);
            this.cardRevenue.TabIndex = 3;
            // 
            // barCard4
            // 
            this.barCard4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.barCard4.Dock = System.Windows.Forms.DockStyle.Left;
            this.barCard4.Location = new System.Drawing.Point(15, 12);
            this.barCard4.Name = "barCard4";
            this.barCard4.Size = new System.Drawing.Size(4, 71);
            this.barCard4.TabIndex = 0;
            // 
            // lblCard4Title
            // 
            this.lblCard4Title.AutoSize = true;
            this.lblCard4Title.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCard4Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCard4Title.Location = new System.Drawing.Point(25, 12);
            this.lblCard4Title.Name = "lblCard4Title";
            this.lblCard4Title.Size = new System.Drawing.Size(127, 21);
            this.lblCard4Title.TabIndex = 1;
            this.lblCard4Title.Text = "💰 Giá TB/Tuyến";
            // 
            // lblCard4Val
            // 
            this.lblCard4Val.AutoSize = true;
            this.lblCard4Val.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblCard4Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.lblCard4Val.Location = new System.Drawing.Point(25, 38);
            this.lblCard4Val.Name = "lblCard4Val";
            this.lblCard4Val.Size = new System.Drawing.Size(107, 41);
            this.lblCard4Val.TabIndex = 2;
            this.lblCard4Val.Text = "0 VNĐ";
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Controls.Add(this.txtSearch);
            this.pnlToolbar.Controls.Add(this.btnImport);
            this.pnlToolbar.Controls.Add(this.btnExport);
            this.pnlToolbar.Controls.Add(this.btnHistory);
            this.pnlToolbar.Controls.Add(this.btnCancel);
            this.pnlToolbar.Controls.Add(this.btnSave);
            this.pnlToolbar.Controls.Add(this.btnDelete);
            this.pnlToolbar.Controls.Add(this.btnEdit);
            this.pnlToolbar.Controls.Add(this.btnAdd);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 160);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.pnlToolbar.Size = new System.Drawing.Size(1250, 75);
            this.pnlToolbar.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.btnAdd.IconColor = System.Drawing.Color.White;
            this.btnAdd.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAdd.IconSize = 22;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(20, 15);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(130, 45);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = " Thêm Tuyến";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.IconChar = FontAwesome.Sharp.IconChar.PenToSquare;
            this.btnEdit.IconColor = System.Drawing.Color.White;
            this.btnEdit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEdit.IconSize = 22;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(160, 15);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(130, 45);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = " Chỉnh Sửa";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            this.btnDelete.IconColor = System.Drawing.Color.White;
            this.btnDelete.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDelete.IconSize = 22;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(300, 15);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 45);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = " Xóa";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnSave.Enabled = false;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnSave.IconColor = System.Drawing.Color.White;
            this.btnSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSave.IconSize = 22;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(420, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 45);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = " Lưu";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnCancel.Enabled = false;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btnCancel.IconColor = System.Drawing.Color.White;
            this.btnCancel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCancel.IconSize = 22;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(540, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 45);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = " Hủy";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnHistory
            // 
            this.btnHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnHistory.FlatAppearance.BorderSize = 0;
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHistory.ForeColor = System.Drawing.Color.White;
            this.btnHistory.IconChar = FontAwesome.Sharp.IconChar.History;
            this.btnHistory.IconColor = System.Drawing.Color.White;
            this.btnHistory.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnHistory.IconSize = 22;
            this.btnHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHistory.Location = new System.Drawing.Point(650, 15);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(120, 45);
            this.btnHistory.TabIndex = 5;
            this.btnHistory.Text = " Lịch Sử";
            this.btnHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHistory.UseVisualStyleBackColor = false;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            this.btnExport.IconColor = System.Drawing.Color.White;
            this.btnExport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExport.IconSize = 22;
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(780, 15);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(130, 45);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = " Xuất Excel";
            this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExport.UseVisualStyleBackColor = false;
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnImport.ForeColor = System.Drawing.Color.White;
            this.btnImport.IconChar = FontAwesome.Sharp.IconChar.FileImport;
            this.btnImport.IconColor = System.Drawing.Color.White;
            this.btnImport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnImport.IconSize = 22;
            this.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImport.Location = new System.Drawing.Point(920, 15);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(130, 45);
            this.btnImport.TabIndex = 7;
            this.btnImport.Text = " Nhập Excel";
            this.btnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImport.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.Location = new System.Drawing.Point(950, 20);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "🔍 Tìm kiếm tuyến xe, lộ trình...";
            this.txtSearch.Size = new System.Drawing.Size(280, 32);
            this.txtSearch.TabIndex = 8;
            // 
            // pnlProgress
            // 
            this.pnlProgress.BackColor = System.Drawing.Color.White;
            this.pnlProgress.Controls.Add(this.pbExcelProgress);
            this.pnlProgress.Controls.Add(this.lblExcelProgress);
            this.pnlProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlProgress.Location = new System.Drawing.Point(0, 714);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8);
            this.pnlProgress.Size = new System.Drawing.Size(1250, 46);
            this.pnlProgress.TabIndex = 2;
            // 
            // pbExcelProgress
            // 
            this.pbExcelProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbExcelProgress.Location = new System.Drawing.Point(20, 8);
            this.pbExcelProgress.Name = "pbExcelProgress";
            this.pbExcelProgress.Size = new System.Drawing.Size(1020, 30);
            this.pbExcelProgress.TabIndex = 0;
            this.pbExcelProgress.Visible = false;
            // 
            // lblExcelProgress
            // 
            this.lblExcelProgress.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblExcelProgress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblExcelProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblExcelProgress.Location = new System.Drawing.Point(1040, 8);
            this.lblExcelProgress.Name = "lblExcelProgress";
            this.lblExcelProgress.Size = new System.Drawing.Size(190, 30);
            this.lblExcelProgress.TabIndex = 1;
            this.lblExcelProgress.Text = "Tiến độ: 0%";
            this.lblExcelProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitMain
            // 
            this.splitMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 235);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.dgvRoutes);
            this.splitMain.Panel1MinSize = 400;
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlDetail);
            this.splitMain.Panel2MinSize = 380;
            this.splitMain.Size = new System.Drawing.Size(1250, 479);
            this.splitMain.SplitterDistance = 750;
            this.splitMain.SplitterWidth = 6;
            this.splitMain.TabIndex = 3;
            // 
            // dgvRoutes
            // 
            this.dgvRoutes.AllowUserToAddRows = false;
            this.dgvRoutes.AllowUserToDeleteRows = false;
            this.dgvRoutes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRoutes.BackgroundColor = System.Drawing.Color.White;
            this.dgvRoutes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRoutes.ColumnHeadersHeight = 44;
            this.dgvRoutes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRoutes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colCode,
            this.colName,
            this.colDistance,
            this.colHours,
            this.colPrice,
            this.colStations,
            this.colVehicles,
            this.colStatus,
            this.colUpdatedBy});
            this.dgvRoutes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRoutes.EnableHeadersVisualStyles = false;
            this.dgvRoutes.Location = new System.Drawing.Point(0, 0);
            this.dgvRoutes.MultiSelect = false;
            this.dgvRoutes.Name = "dgvRoutes";
            this.dgvRoutes.ReadOnly = true;
            this.dgvRoutes.RowHeadersVisible = false;
            this.dgvRoutes.RowHeadersWidth = 51;
            this.dgvRoutes.RowTemplate.Height = 44;
            this.dgvRoutes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoutes.Size = new System.Drawing.Size(750, 479);
            this.dgvRoutes.TabIndex = 0;
            // 
            // colId
            // 
            this.colId.HeaderText = "ID";
            this.colId.MinimumWidth = 6;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            this.colId.Width = 125;
            // 
            // colCode
            // 
            this.colCode.HeaderText = "Mã Tuyến";
            this.colCode.MinimumWidth = 6;
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            this.colCode.Width = 110;
            // 
            // colName
            // 
            this.colName.HeaderText = "Tên Lộ Trình";
            this.colName.MinimumWidth = 6;
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 210;
            // 
            // colDistance
            // 
            this.colDistance.HeaderText = "Cự Ly (km)";
            this.colDistance.MinimumWidth = 6;
            this.colDistance.Name = "colDistance";
            this.colDistance.ReadOnly = true;
            this.colDistance.Width = 100;
            // 
            // colHours
            // 
            this.colHours.HeaderText = "Thời Gian (h)";
            this.colHours.MinimumWidth = 6;
            this.colHours.Name = "colHours";
            this.colHours.ReadOnly = true;
            this.colHours.Width = 105;
            // 
            // colPrice
            // 
            this.colPrice.HeaderText = "Giá Cước (VNĐ)";
            this.colPrice.MinimumWidth = 6;
            this.colPrice.Name = "colPrice";
            this.colPrice.ReadOnly = true;
            this.colPrice.Width = 125;
            // 
            // colStations
            // 
            this.colStations.HeaderText = "Số Trạm";
            this.colStations.MinimumWidth = 6;
            this.colStations.Name = "colStations";
            this.colStations.ReadOnly = true;
            this.colStations.Width = 85;
            // 
            // colVehicles
            // 
            this.colVehicles.HeaderText = "Số Xe";
            this.colVehicles.MinimumWidth = 6;
            this.colVehicles.Name = "colVehicles";
            this.colVehicles.ReadOnly = true;
            this.colVehicles.Width = 80;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Trạng Thái";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 110;
            // 
            // colUpdatedBy
            // 
            this.colUpdatedBy.HeaderText = "Người Cập Nhật";
            this.colUpdatedBy.MinimumWidth = 6;
            this.colUpdatedBy.Name = "colUpdatedBy";
            this.colUpdatedBy.ReadOnly = true;
            this.colUpdatedBy.Width = 130;
            // 
            // pnlDetail
            // 
            this.pnlDetail.AutoScroll = true;
            this.pnlDetail.BackColor = System.Drawing.Color.White;
            this.pnlDetail.Controls.Add(this.clbVehicles);
            this.pnlDetail.Controls.Add(this.cboFkVehicle);
            this.pnlDetail.Controls.Add(this.lblVehiclesTitle);
            this.pnlDetail.Controls.Add(this.clbStations);
            this.pnlDetail.Controls.Add(this.cboFkStation);
            this.pnlDetail.Controls.Add(this.lblStationsTitle);
            this.pnlDetail.Controls.Add(this.numBasePrice);
            this.pnlDetail.Controls.Add(this.lblBasePrice);
            this.pnlDetail.Controls.Add(this.numEstimatedHours);
            this.pnlDetail.Controls.Add(this.lblEstimatedHours);
            this.pnlDetail.Controls.Add(this.numDistance);
            this.pnlDetail.Controls.Add(this.lblDistance);
            this.pnlDetail.Controls.Add(this.txtRouteName);
            this.pnlDetail.Controls.Add(this.lblRouteName);
            this.pnlDetail.Controls.Add(this.cboStatus);
            this.pnlDetail.Controls.Add(this.lblRouteStatus);
            this.pnlDetail.Controls.Add(this.txtRouteCode);
            this.pnlDetail.Controls.Add(this.lblRouteCode);
            this.pnlDetail.Controls.Add(this.lblDetailTitle);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetail.Location = new System.Drawing.Point(0, 0);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Padding = new System.Windows.Forms.Padding(18);
            this.pnlDetail.Size = new System.Drawing.Size(494, 479);
            this.pnlDetail.TabIndex = 0;
            // 
            // lblDetailTitle
            // 
            this.lblDetailTitle.AutoSize = true;
            this.lblDetailTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDetailTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblDetailTitle.Location = new System.Drawing.Point(18, 15);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Size = new System.Drawing.Size(370, 25);
            this.lblDetailTitle.TabIndex = 0;
            this.lblDetailTitle.Text = "CHI TIẾT TUYẾN XE & LIÊN KẾT ĐA BẢNG";
            // 
            // lblRouteCode
            // 
            this.lblRouteCode.AutoSize = true;
            this.lblRouteCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRouteCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblRouteCode.Location = new System.Drawing.Point(18, 50);
            this.lblRouteCode.Name = "lblRouteCode";
            this.lblRouteCode.Size = new System.Drawing.Size(123, 20);
            this.lblRouteCode.TabIndex = 1;
            this.lblRouteCode.Text = "Mã Tuyến Xe *:";
            // 
            // txtRouteCode
            // 
            this.txtRouteCode.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtRouteCode.Location = new System.Drawing.Point(18, 74);
            this.txtRouteCode.Name = "txtRouteCode";
            this.txtRouteCode.Size = new System.Drawing.Size(180, 29);
            this.txtRouteCode.TabIndex = 2;
            // 
            // lblRouteStatus
            // 
            this.lblRouteStatus.AutoSize = true;
            this.lblRouteStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRouteStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblRouteStatus.Location = new System.Drawing.Point(215, 50);
            this.lblRouteStatus.Name = "lblRouteStatus";
            this.lblRouteStatus.Size = new System.Drawing.Size(89, 20);
            this.lblRouteStatus.TabIndex = 3;
            this.lblRouteStatus.Text = "Trạng Thái:";
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Items.AddRange(new object[] {
            "Active",
            "Suspended"});
            this.cboStatus.Location = new System.Drawing.Point(215, 74);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(185, 29);
            this.cboStatus.TabIndex = 4;
            // 
            // lblRouteName
            // 
            this.lblRouteName.AutoSize = true;
            this.lblRouteName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRouteName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblRouteName.Location = new System.Drawing.Point(18, 115);
            this.lblRouteName.Name = "lblRouteName";
            this.lblRouteName.Size = new System.Drawing.Size(167, 20);
            this.lblRouteName.TabIndex = 5;
            this.lblRouteName.Text = "Tên Tuyến / Lộ Trình *:";
            // 
            // txtRouteName
            // 
            this.txtRouteName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtRouteName.Location = new System.Drawing.Point(18, 139);
            this.txtRouteName.Name = "txtRouteName";
            this.txtRouteName.Size = new System.Drawing.Size(382, 29);
            this.txtRouteName.TabIndex = 6;
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDistance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDistance.Location = new System.Drawing.Point(18, 180);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(89, 20);
            this.lblDistance.TabIndex = 7;
            this.lblDistance.Text = "Cự Ly (km):";
            // 
            // numDistance
            // 
            this.numDistance.DecimalPlaces = 1;
            this.numDistance.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.numDistance.Location = new System.Drawing.Point(18, 204);
            this.numDistance.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numDistance.Name = "numDistance";
            this.numDistance.Size = new System.Drawing.Size(120, 29);
            this.numDistance.TabIndex = 8;
            // 
            // lblEstimatedHours
            // 
            this.lblEstimatedHours.AutoSize = true;
            this.lblEstimatedHours.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstimatedHours.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblEstimatedHours.Location = new System.Drawing.Point(150, 180);
            this.lblEstimatedHours.Name = "lblEstimatedHours";
            this.lblEstimatedHours.Size = new System.Drawing.Size(117, 20);
            this.lblEstimatedHours.TabIndex = 9;
            this.lblEstimatedHours.Text = "Thời Gian (giờ):";
            // 
            // numEstimatedHours
            // 
            this.numEstimatedHours.DecimalPlaces = 1;
            this.numEstimatedHours.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.numEstimatedHours.Location = new System.Drawing.Point(150, 204);
            this.numEstimatedHours.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numEstimatedHours.Name = "numEstimatedHours";
            this.numEstimatedHours.Size = new System.Drawing.Size(110, 29);
            this.numEstimatedHours.TabIndex = 10;
            // 
            // lblBasePrice
            // 
            this.lblBasePrice.AutoSize = true;
            this.lblBasePrice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBasePrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblBasePrice.Location = new System.Drawing.Point(270, 180);
            this.lblBasePrice.Name = "lblBasePrice";
            this.lblBasePrice.Size = new System.Drawing.Size(104, 20);
            this.lblBasePrice.TabIndex = 11;
            this.lblBasePrice.Text = "Giá Vé (VNĐ):";
            // 
            // numBasePrice
            // 
            this.numBasePrice.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.numBasePrice.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numBasePrice.Location = new System.Drawing.Point(270, 204);
            this.numBasePrice.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numBasePrice.Name = "numBasePrice";
            this.numBasePrice.Size = new System.Drawing.Size(130, 29);
            this.numBasePrice.TabIndex = 12;
            this.numBasePrice.ThousandsSeparator = true;
            // 
            // lblStationsTitle
            // 
            this.lblStationsTitle.AutoSize = true;
            this.lblStationsTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStationsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblStationsTitle.Location = new System.Drawing.Point(18, 245);
            this.lblStationsTitle.Name = "lblStationsTitle";
            this.lblStationsTitle.Size = new System.Drawing.Size(359, 21);
            this.lblStationsTitle.TabIndex = 13;
            this.lblStationsTitle.Text = "📍 Trạm Dừng Thuộc Lộ Trình (Liên kết N-N):";
            // 
            // cboFkStation
            // 
            this.cboFkStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFkStation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboFkStation.FormattingEnabled = true;
            this.cboFkStation.Location = new System.Drawing.Point(18, 269);
            this.cboFkStation.Name = "cboFkStation";
            this.cboFkStation.Size = new System.Drawing.Size(382, 28);
            this.cboFkStation.TabIndex = 14;
            // 
            // clbStations
            // 
            this.clbStations.CheckOnClick = true;
            this.clbStations.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.clbStations.FormattingEnabled = true;
            this.clbStations.Location = new System.Drawing.Point(18, 303);
            this.clbStations.Name = "clbStations";
            this.clbStations.Size = new System.Drawing.Size(382, 92);
            this.clbStations.TabIndex = 15;
            // 
            // lblVehiclesTitle
            // 
            this.lblVehiclesTitle.AutoSize = true;
            this.lblVehiclesTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblVehiclesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblVehiclesTitle.Location = new System.Drawing.Point(18, 400);
            this.lblVehiclesTitle.Name = "lblVehiclesTitle";
            this.lblVehiclesTitle.Size = new System.Drawing.Size(349, 21);
            this.lblVehiclesTitle.TabIndex = 16;
            this.lblVehiclesTitle.Text = "🚌 Phương Tiện Phân Công (Liên kết 1-N):";
            // 
            // cboFkVehicle
            // 
            this.cboFkVehicle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFkVehicle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboFkVehicle.FormattingEnabled = true;
            this.cboFkVehicle.Location = new System.Drawing.Point(18, 424);
            this.cboFkVehicle.Name = "cboFkVehicle";
            this.cboFkVehicle.Size = new System.Drawing.Size(382, 28);
            this.cboFkVehicle.TabIndex = 17;
            // 
            // clbVehicles
            // 
            this.clbVehicles.CheckOnClick = true;
            this.clbVehicles.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.clbVehicles.FormattingEnabled = true;
            this.clbVehicles.Location = new System.Drawing.Point(18, 458);
            this.clbVehicles.Name = "clbVehicles";
            this.clbVehicles.Size = new System.Drawing.Size(382, 92);
            this.clbVehicles.TabIndex = 18;
            // 
            // UcManageRoute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlDashboard);
            this.Controls.Add(this.pnlProgress);
            this.Name = "UcManageRoute";
            this.Size = new System.Drawing.Size(1250, 760);
            this.pnlDashboard.ResumeLayout(false);
            this.pnlDashboard.PerformLayout();
            this.pnlStats.ResumeLayout(false);
            this.cardTotalRoutes.ResumeLayout(false);
            this.cardTotalRoutes.PerformLayout();
            this.cardActiveRoutes.ResumeLayout(false);
            this.cardActiveRoutes.PerformLayout();
            this.cardTotalDistance.ResumeLayout(false);
            this.cardTotalDistance.PerformLayout();
            this.cardRevenue.ResumeLayout(false);
            this.cardRevenue.PerformLayout();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlProgress.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            this.splitMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoutes)).EndInit();
            this.pnlDetail.ResumeLayout(false);
            this.pnlDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEstimatedHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBasePrice)).EndInit();
            this.ResumeLayout(false);

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
