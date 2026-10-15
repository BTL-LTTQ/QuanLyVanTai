namespace QuanLyVanTai.UI.UserControls
{
    partial class UcManageVehicleAndStation
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabVehicles = new System.Windows.Forms.TabPage();
            this.splitVehicles = new System.Windows.Forms.SplitContainer();
            this.dgvVehicles = new System.Windows.Forms.DataGridView();
            this.colVehId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVehPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVehType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVehSeats = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVehMfr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVehRoute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVehStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlVehDetail = new System.Windows.Forms.Panel();
            this.btnVehCancel = new FontAwesome.Sharp.IconButton();
            this.btnVehSave = new FontAwesome.Sharp.IconButton();
            this.btnVehDelete = new FontAwesome.Sharp.IconButton();
            this.btnVehEdit = new FontAwesome.Sharp.IconButton();
            this.btnVehAdd = new FontAwesome.Sharp.IconButton();
            this.cboVehRoute = new System.Windows.Forms.ComboBox();
            this.lblVehRoute = new System.Windows.Forms.Label();
            this.txtVehManufacturer = new System.Windows.Forms.TextBox();
            this.lblVehManufacturer = new System.Windows.Forms.Label();
            this.cboVehEditStatus = new System.Windows.Forms.ComboBox();
            this.lblVehStatus = new System.Windows.Forms.Label();
            this.numVehSeats = new System.Windows.Forms.NumericUpDown();
            this.lblVehSeats = new System.Windows.Forms.Label();
            this.cboVehEditType = new System.Windows.Forms.ComboBox();
            this.lblVehType = new System.Windows.Forms.Label();
            this.txtVehLicense = new System.Windows.Forms.TextBox();
            this.lblVehLicense = new System.Windows.Forms.Label();
            this.lblVehTitle = new System.Windows.Forms.Label();
            this.pnlVehiclesFilter = new System.Windows.Forms.Panel();
            this.btnVehExport = new FontAwesome.Sharp.IconButton();
            this.btnVehReset = new FontAwesome.Sharp.IconButton();
            this.radVehOr = new System.Windows.Forms.RadioButton();
            this.radVehAnd = new System.Windows.Forms.RadioButton();
            this.cboVehManufacturer = new System.Windows.Forms.ComboBox();
            this.cboVehStatus = new System.Windows.Forms.ComboBox();
            this.cboVehType = new System.Windows.Forms.ComboBox();
            this.txtVehKeyword = new System.Windows.Forms.TextBox();
            this.tabStations = new System.Windows.Forms.TabPage();
            this.splitStations = new System.Windows.Forms.SplitContainer();
            this.dgvStations = new System.Windows.Forms.DataGridView();
            this.colStId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStRoutes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlStDetail = new System.Windows.Forms.Panel();
            this.btnStCancel = new FontAwesome.Sharp.IconButton();
            this.btnStSave = new FontAwesome.Sharp.IconButton();
            this.btnStDelete = new FontAwesome.Sharp.IconButton();
            this.btnStEdit = new FontAwesome.Sharp.IconButton();
            this.btnStAdd = new FontAwesome.Sharp.IconButton();
            this.cboStEditStatus = new System.Windows.Forms.ComboBox();
            this.lblStStatus = new System.Windows.Forms.Label();
            this.cboStEditCity = new System.Windows.Forms.ComboBox();
            this.lblStCity = new System.Windows.Forms.Label();
            this.txtStAddress = new System.Windows.Forms.TextBox();
            this.lblStAddress = new System.Windows.Forms.Label();
            this.txtStName = new System.Windows.Forms.TextBox();
            this.lblStName = new System.Windows.Forms.Label();
            this.txtStCode = new System.Windows.Forms.TextBox();
            this.lblStCode = new System.Windows.Forms.Label();
            this.lblStTitle = new System.Windows.Forms.Label();
            this.pnlStationsFilter = new System.Windows.Forms.Panel();
            this.btnStExport = new FontAwesome.Sharp.IconButton();
            this.btnStReset = new FontAwesome.Sharp.IconButton();
            this.radStOr = new System.Windows.Forms.RadioButton();
            this.radStAnd = new System.Windows.Forms.RadioButton();
            this.cboStStatus = new System.Windows.Forms.ComboBox();
            this.cboStCity = new System.Windows.Forms.ComboBox();
            this.txtStKeyword = new System.Windows.Forms.TextBox();
            this.tabMain.SuspendLayout();
            this.tabVehicles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitVehicles)).BeginInit();
            this.splitVehicles.Panel1.SuspendLayout();
            this.splitVehicles.Panel2.SuspendLayout();
            this.splitVehicles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehicles)).BeginInit();
            this.pnlVehDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVehSeats)).BeginInit();
            this.pnlVehiclesFilter.SuspendLayout();
            this.tabStations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitStations)).BeginInit();
            this.splitStations.Panel1.SuspendLayout();
            this.splitStations.Panel2.SuspendLayout();
            this.splitStations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStations)).BeginInit();
            this.pnlStDetail.SuspendLayout();
            this.pnlStationsFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabVehicles);
            this.tabMain.Controls.Add(this.tabStations);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Drawing.Point(20, 10);
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1250, 760);
            this.tabMain.TabIndex = 0;
            // 
            // tabVehicles
            // 
            this.tabVehicles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tabVehicles.Controls.Add(this.splitVehicles);
            this.tabVehicles.Controls.Add(this.pnlVehiclesFilter);
            this.tabVehicles.Location = new System.Drawing.Point(4, 46);
            this.tabVehicles.Name = "tabVehicles";
            this.tabVehicles.Size = new System.Drawing.Size(1242, 710);
            this.tabVehicles.TabIndex = 0;
            this.tabVehicles.Text = "🚌 Quản Lý Phương Tiện";
            // 
            // splitVehicles
            // 
            this.splitVehicles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.splitVehicles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitVehicles.Location = new System.Drawing.Point(0, 85);
            this.splitVehicles.Name = "splitVehicles";
            // 
            // splitVehicles.Panel1
            // 
            this.splitVehicles.Panel1.Controls.Add(this.dgvVehicles);
            this.splitVehicles.Panel1MinSize = 400;
            // 
            // splitVehicles.Panel2
            // 
            this.splitVehicles.Panel2.Controls.Add(this.pnlVehDetail);
            this.splitVehicles.Panel2MinSize = 350;
            this.splitVehicles.Size = new System.Drawing.Size(1242, 625);
            this.splitVehicles.SplitterDistance = 750;
            this.splitVehicles.TabIndex = 1;
            // 
            // dgvVehicles
            // 
            this.dgvVehicles.AllowUserToAddRows = false;
            this.dgvVehicles.AllowUserToDeleteRows = false;
            this.dgvVehicles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVehicles.BackgroundColor = System.Drawing.Color.White;
            this.dgvVehicles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvVehicles.ColumnHeadersHeight = 42;
            this.dgvVehicles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvVehicles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colVehId,
            this.colVehPlate,
            this.colVehType,
            this.colVehSeats,
            this.colVehMfr,
            this.colVehRoute,
            this.colVehStatus});
            this.dgvVehicles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVehicles.EnableHeadersVisualStyles = false;
            this.dgvVehicles.Location = new System.Drawing.Point(0, 0);
            this.dgvVehicles.MultiSelect = false;
            this.dgvVehicles.Name = "dgvVehicles";
            this.dgvVehicles.ReadOnly = true;
            this.dgvVehicles.RowHeadersVisible = false;
            this.dgvVehicles.RowHeadersWidth = 51;
            this.dgvVehicles.RowTemplate.Height = 42;
            this.dgvVehicles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVehicles.Size = new System.Drawing.Size(750, 625);
            this.dgvVehicles.TabIndex = 0;
            // 
            // colVehId
            // 
            this.colVehId.HeaderText = "ID";
            this.colVehId.MinimumWidth = 6;
            this.colVehId.Name = "colVehId";
            this.colVehId.ReadOnly = true;
            this.colVehId.Visible = false;
            this.colVehId.Width = 125;
            // 
            // colVehPlate
            // 
            this.colVehPlate.HeaderText = "Biển Số Xe";
            this.colVehPlate.MinimumWidth = 6;
            this.colVehPlate.Name = "colVehPlate";
            this.colVehPlate.ReadOnly = true;
            this.colVehPlate.Width = 120;
            // 
            // colVehType
            // 
            this.colVehType.HeaderText = "Loại Phương Tiện";
            this.colVehType.MinimumWidth = 6;
            this.colVehType.Name = "colVehType";
            this.colVehType.ReadOnly = true;
            this.colVehType.Width = 170;
            // 
            // colVehSeats
            // 
            this.colVehSeats.HeaderText = "Số Ghế";
            this.colVehSeats.MinimumWidth = 6;
            this.colVehSeats.Name = "colVehSeats";
            this.colVehSeats.ReadOnly = true;
            this.colVehSeats.Width = 85;
            // 
            // colVehMfr
            // 
            this.colVehMfr.HeaderText = "Hãng Sản Xuất";
            this.colVehMfr.MinimumWidth = 6;
            this.colVehMfr.Name = "colVehMfr";
            this.colVehMfr.ReadOnly = true;
            this.colVehMfr.Width = 150;
            // 
            // colVehRoute
            // 
            this.colVehRoute.HeaderText = "Tuyến Phụ Trách";
            this.colVehRoute.MinimumWidth = 6;
            this.colVehRoute.Name = "colVehRoute";
            this.colVehRoute.ReadOnly = true;
            this.colVehRoute.Width = 180;
            // 
            // colVehStatus
            // 
            this.colVehStatus.HeaderText = "Trạng Thái";
            this.colVehStatus.MinimumWidth = 6;
            this.colVehStatus.Name = "colVehStatus";
            this.colVehStatus.ReadOnly = true;
            this.colVehStatus.Width = 120;
            // 
            // pnlVehDetail
            // 
            this.pnlVehDetail.AutoScroll = true;
            this.pnlVehDetail.BackColor = System.Drawing.Color.White;
            this.pnlVehDetail.Controls.Add(this.btnVehCancel);
            this.pnlVehDetail.Controls.Add(this.btnVehSave);
            this.pnlVehDetail.Controls.Add(this.btnVehDelete);
            this.pnlVehDetail.Controls.Add(this.btnVehEdit);
            this.pnlVehDetail.Controls.Add(this.btnVehAdd);
            this.pnlVehDetail.Controls.Add(this.cboVehRoute);
            this.pnlVehDetail.Controls.Add(this.lblVehRoute);
            this.pnlVehDetail.Controls.Add(this.txtVehManufacturer);
            this.pnlVehDetail.Controls.Add(this.lblVehManufacturer);
            this.pnlVehDetail.Controls.Add(this.cboVehEditStatus);
            this.pnlVehDetail.Controls.Add(this.lblVehStatus);
            this.pnlVehDetail.Controls.Add(this.numVehSeats);
            this.pnlVehDetail.Controls.Add(this.lblVehSeats);
            this.pnlVehDetail.Controls.Add(this.cboVehEditType);
            this.pnlVehDetail.Controls.Add(this.lblVehType);
            this.pnlVehDetail.Controls.Add(this.txtVehLicense);
            this.pnlVehDetail.Controls.Add(this.lblVehLicense);
            this.pnlVehDetail.Controls.Add(this.lblVehTitle);
            this.pnlVehDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVehDetail.Location = new System.Drawing.Point(0, 0);
            this.pnlVehDetail.Name = "pnlVehDetail";
            this.pnlVehDetail.Padding = new System.Windows.Forms.Padding(18);
            this.pnlVehDetail.Size = new System.Drawing.Size(488, 625);
            this.pnlVehDetail.TabIndex = 0;
            // 
            // btnVehCancel
            // 
            this.btnVehCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnVehCancel.Enabled = false;
            this.btnVehCancel.FlatAppearance.BorderSize = 0;
            this.btnVehCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVehCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVehCancel.ForeColor = System.Drawing.Color.White;
            this.btnVehCancel.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btnVehCancel.IconColor = System.Drawing.Color.White;
            this.btnVehCancel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnVehCancel.IconSize = 18;
            this.btnVehCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVehCancel.Location = new System.Drawing.Point(364, 385);
            this.btnVehCancel.Name = "btnVehCancel";
            this.btnVehCancel.Size = new System.Drawing.Size(82, 36);
            this.btnVehCancel.TabIndex = 17;
            this.btnVehCancel.Text = " Hủy";
            this.btnVehCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVehCancel.UseVisualStyleBackColor = false;
            // 
            // btnVehSave
            // 
            this.btnVehSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnVehSave.Enabled = false;
            this.btnVehSave.FlatAppearance.BorderSize = 0;
            this.btnVehSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVehSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVehSave.ForeColor = System.Drawing.Color.White;
            this.btnVehSave.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnVehSave.IconColor = System.Drawing.Color.White;
            this.btnVehSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnVehSave.IconSize = 18;
            this.btnVehSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVehSave.Location = new System.Drawing.Point(276, 385);
            this.btnVehSave.Name = "btnVehSave";
            this.btnVehSave.Size = new System.Drawing.Size(82, 36);
            this.btnVehSave.TabIndex = 16;
            this.btnVehSave.Text = " Lưu";
            this.btnVehSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVehSave.UseVisualStyleBackColor = false;
            // 
            // btnVehDelete
            // 
            this.btnVehDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnVehDelete.FlatAppearance.BorderSize = 0;
            this.btnVehDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVehDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVehDelete.ForeColor = System.Drawing.Color.White;
            this.btnVehDelete.IconChar = FontAwesome.Sharp.IconChar.Trash;
            this.btnVehDelete.IconColor = System.Drawing.Color.White;
            this.btnVehDelete.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnVehDelete.IconSize = 18;
            this.btnVehDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVehDelete.Location = new System.Drawing.Point(188, 385);
            this.btnVehDelete.Name = "btnVehDelete";
            this.btnVehDelete.Size = new System.Drawing.Size(82, 36);
            this.btnVehDelete.TabIndex = 15;
            this.btnVehDelete.Text = " Xóa";
            this.btnVehDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVehDelete.UseVisualStyleBackColor = false;
            // 
            // btnVehEdit
            // 
            this.btnVehEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnVehEdit.FlatAppearance.BorderSize = 0;
            this.btnVehEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVehEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVehEdit.ForeColor = System.Drawing.Color.White;
            this.btnVehEdit.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.btnVehEdit.IconColor = System.Drawing.Color.White;
            this.btnVehEdit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnVehEdit.IconSize = 18;
            this.btnVehEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVehEdit.Location = new System.Drawing.Point(100, 385);
            this.btnVehEdit.Name = "btnVehEdit";
            this.btnVehEdit.Size = new System.Drawing.Size(82, 36);
            this.btnVehEdit.TabIndex = 14;
            this.btnVehEdit.Text = " Sửa";
            this.btnVehEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVehEdit.UseVisualStyleBackColor = false;
            // 
            // btnVehAdd
            // 
            this.btnVehAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnVehAdd.FlatAppearance.BorderSize = 0;
            this.btnVehAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVehAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVehAdd.ForeColor = System.Drawing.Color.White;
            this.btnVehAdd.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnVehAdd.IconColor = System.Drawing.Color.White;
            this.btnVehAdd.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnVehAdd.IconSize = 18;
            this.btnVehAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVehAdd.Location = new System.Drawing.Point(12, 385);
            this.btnVehAdd.Name = "btnVehAdd";
            this.btnVehAdd.Size = new System.Drawing.Size(82, 36);
            this.btnVehAdd.TabIndex = 13;
            this.btnVehAdd.Text = " Thêm";
            this.btnVehAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVehAdd.UseVisualStyleBackColor = false;
            // 
            // cboVehRoute
            // 
            this.cboVehRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVehRoute.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboVehRoute.FormattingEnabled = true;
            this.cboVehRoute.Location = new System.Drawing.Point(18, 332);
            this.cboVehRoute.Name = "cboVehRoute";
            this.cboVehRoute.Size = new System.Drawing.Size(330, 29);
            this.cboVehRoute.TabIndex = 12;
            // 
            // lblVehRoute
            // 
            this.lblVehRoute.AutoSize = true;
            this.lblVehRoute.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblVehRoute.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblVehRoute.Location = new System.Drawing.Point(18, 310);
            this.lblVehRoute.Name = "lblVehRoute";
            this.lblVehRoute.Size = new System.Drawing.Size(161, 20);
            this.lblVehRoute.TabIndex = 11;
            this.lblVehRoute.Text = "Phân Công Tuyến Xe:";
            // 
            // txtVehManufacturer
            // 
            this.txtVehManufacturer.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtVehManufacturer.Location = new System.Drawing.Point(18, 272);
            this.txtVehManufacturer.Name = "txtVehManufacturer";
            this.txtVehManufacturer.Size = new System.Drawing.Size(330, 29);
            this.txtVehManufacturer.TabIndex = 10;
            // 
            // lblVehManufacturer
            // 
            this.lblVehManufacturer.AutoSize = true;
            this.lblVehManufacturer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblVehManufacturer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblVehManufacturer.Location = new System.Drawing.Point(18, 250);
            this.lblVehManufacturer.Name = "lblVehManufacturer";
            this.lblVehManufacturer.Size = new System.Drawing.Size(117, 20);
            this.lblVehManufacturer.TabIndex = 9;
            this.lblVehManufacturer.Text = "Hãng Sản Xuất:";
            // 
            // cboVehEditStatus
            // 
            this.cboVehEditStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVehEditStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboVehEditStatus.FormattingEnabled = true;
            this.cboVehEditStatus.Items.AddRange(new object[] {
            "Ready",
            "InTransit",
            "Maintenance"});
            this.cboVehEditStatus.Location = new System.Drawing.Point(180, 212);
            this.cboVehEditStatus.Name = "cboVehEditStatus";
            this.cboVehEditStatus.Size = new System.Drawing.Size(168, 29);
            this.cboVehEditStatus.TabIndex = 8;
            // 
            // lblVehStatus
            // 
            this.lblVehStatus.AutoSize = true;
            this.lblVehStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblVehStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblVehStatus.Location = new System.Drawing.Point(180, 190);
            this.lblVehStatus.Name = "lblVehStatus";
            this.lblVehStatus.Size = new System.Drawing.Size(89, 20);
            this.lblVehStatus.TabIndex = 7;
            this.lblVehStatus.Text = "Trạng Thái:";
            // 
            // numVehSeats
            // 
            this.numVehSeats.Enabled = false;
            this.numVehSeats.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.numVehSeats.Location = new System.Drawing.Point(18, 212);
            this.numVehSeats.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numVehSeats.Name = "numVehSeats";
            this.numVehSeats.Size = new System.Drawing.Size(150, 29);
            this.numVehSeats.TabIndex = 6;
            this.numVehSeats.Value = new decimal(new int[] {
            28,
            0,
            0,
            0});
            // 
            // lblVehSeats
            // 
            this.lblVehSeats.AutoSize = true;
            this.lblVehSeats.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblVehSeats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblVehSeats.Location = new System.Drawing.Point(18, 190);
            this.lblVehSeats.Name = "lblVehSeats";
            this.lblVehSeats.Size = new System.Drawing.Size(189, 20);
            this.lblVehSeats.TabIndex = 5;
            this.lblVehSeats.Text = "Tổng Số Chỗ Ghế/Giường:";
            // 
            // cboVehEditType
            // 
            this.cboVehEditType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVehEditType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboVehEditType.FormattingEnabled = true;
            this.cboVehEditType.Items.AddRange(new object[] {
            "Limousine 9 chỗ",
            "Giường nằm 24 chỗ",
            "Ghế ngồi 28 chỗ",
            "Giường nằm 34 chỗ",
            "Ghế ngồi 45 chỗ"});
            this.cboVehEditType.Location = new System.Drawing.Point(18, 152);
            this.cboVehEditType.Name = "cboVehEditType";
            this.cboVehEditType.Size = new System.Drawing.Size(330, 29);
            this.cboVehEditType.TabIndex = 4;
            // 
            // lblVehType
            // 
            this.lblVehType.AutoSize = true;
            this.lblVehType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblVehType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblVehType.Location = new System.Drawing.Point(18, 130);
            this.lblVehType.Name = "lblVehType";
            this.lblVehType.Size = new System.Drawing.Size(69, 20);
            this.lblVehType.TabIndex = 3;
            this.lblVehType.Text = "Loại Xe:";
            // 
            // txtVehLicense
            // 
            this.txtVehLicense.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtVehLicense.Location = new System.Drawing.Point(18, 92);
            this.txtVehLicense.Name = "txtVehLicense";
            this.txtVehLicense.Size = new System.Drawing.Size(330, 29);
            this.txtVehLicense.TabIndex = 2;
            // 
            // lblVehLicense
            // 
            this.lblVehLicense.AutoSize = true;
            this.lblVehLicense.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblVehLicense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblVehLicense.Location = new System.Drawing.Point(18, 70);
            this.lblVehLicense.Name = "lblVehLicense";
            this.lblVehLicense.Size = new System.Drawing.Size(155, 20);
            this.lblVehLicense.TabIndex = 1;
            this.lblVehLicense.Text = "Biển Kiểm Soát *:";
            // 
            // lblVehTitle
            // 
            this.lblVehTitle.AutoSize = true;
            this.lblVehTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblVehTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblVehTitle.Location = new System.Drawing.Point(18, 15);
            this.lblVehTitle.Name = "lblVehTitle";
            this.lblVehTitle.Size = new System.Drawing.Size(227, 25);
            this.lblVehTitle.TabIndex = 0;
            this.lblVehTitle.Text = "CHI TIẾT PHƯƠNG TIỆN";
            // 
            // pnlVehiclesFilter
            // 
            this.pnlVehiclesFilter.BackColor = System.Drawing.Color.White;
            this.pnlVehiclesFilter.Controls.Add(this.btnVehExport);
            this.pnlVehiclesFilter.Controls.Add(this.btnVehReset);
            this.pnlVehiclesFilter.Controls.Add(this.radVehOr);
            this.pnlVehiclesFilter.Controls.Add(this.radVehAnd);
            this.pnlVehiclesFilter.Controls.Add(this.cboVehManufacturer);
            this.pnlVehiclesFilter.Controls.Add(this.cboVehStatus);
            this.pnlVehiclesFilter.Controls.Add(this.cboVehType);
            this.pnlVehiclesFilter.Controls.Add(this.txtVehKeyword);
            this.pnlVehiclesFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlVehiclesFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlVehiclesFilter.Name = "pnlVehiclesFilter";
            this.pnlVehiclesFilter.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlVehiclesFilter.Size = new System.Drawing.Size(1242, 85);
            this.pnlVehiclesFilter.TabIndex = 0;
            // 
            // btnVehExport
            // 
            this.btnVehExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnVehExport.FlatAppearance.BorderSize = 0;
            this.btnVehExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVehExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVehExport.ForeColor = System.Drawing.Color.White;
            this.btnVehExport.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            this.btnVehExport.IconColor = System.Drawing.Color.White;
            this.btnVehExport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnVehExport.IconSize = 18;
            this.btnVehExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVehExport.Location = new System.Drawing.Point(420, 44);
            this.btnVehExport.Name = "btnVehExport";
            this.btnVehExport.Size = new System.Drawing.Size(110, 34);
            this.btnVehExport.TabIndex = 7;
            this.btnVehExport.Text = " Xuất file";
            this.btnVehExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVehExport.UseVisualStyleBackColor = false;
            // 
            // btnVehReset
            // 
            this.btnVehReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnVehReset.FlatAppearance.BorderSize = 0;
            this.btnVehReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVehReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVehReset.ForeColor = System.Drawing.Color.White;
            this.btnVehReset.IconChar = FontAwesome.Sharp.IconChar.RotateLeft;
            this.btnVehReset.IconColor = System.Drawing.Color.White;
            this.btnVehReset.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnVehReset.IconSize = 18;
            this.btnVehReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVehReset.Location = new System.Drawing.Point(310, 44);
            this.btnVehReset.Name = "btnVehReset";
            this.btnVehReset.Size = new System.Drawing.Size(100, 34);
            this.btnVehReset.TabIndex = 6;
            this.btnVehReset.Text = " Đặt lại";
            this.btnVehReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVehReset.UseVisualStyleBackColor = false;
            // 
            // radVehOr
            // 
            this.radVehOr.AutoSize = true;
            this.radVehOr.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.radVehOr.Location = new System.Drawing.Point(160, 48);
            this.radVehOr.Name = "radVehOr";
            this.radVehOr.Size = new System.Drawing.Size(155, 24);
            this.radVehOr.TabIndex = 5;
            this.radVehOr.Text = "OR (Khớp bất kỳ)";
            this.radVehOr.UseVisualStyleBackColor = true;
            // 
            // radVehAnd
            // 
            this.radVehAnd.AutoSize = true;
            this.radVehAnd.Checked = true;
            this.radVehAnd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.radVehAnd.Location = new System.Drawing.Point(15, 48);
            this.radVehAnd.Name = "radVehAnd";
            this.radVehAnd.Size = new System.Drawing.Size(155, 24);
            this.radVehAnd.TabIndex = 4;
            this.radVehAnd.TabStop = true;
            this.radVehAnd.Text = "AND (Khớp tất cả)";
            this.radVehAnd.UseVisualStyleBackColor = true;
            // 
            // cboVehManufacturer
            // 
            this.cboVehManufacturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVehManufacturer.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboVehManufacturer.FormattingEnabled = true;
            this.cboVehManufacturer.Items.AddRange(new object[] {
            "Tất cả",
            "Hyundai Universe",
            "Ford Transit DCar",
            "Thaco Mobihome",
            "Tracomeco",
            "Samco"});
            this.cboVehManufacturer.Location = new System.Drawing.Point(495, 12);
            this.cboVehManufacturer.Name = "cboVehManufacturer";
            this.cboVehManufacturer.Size = new System.Drawing.Size(140, 29);
            this.cboVehManufacturer.TabIndex = 3;
            // 
            // cboVehStatus
            // 
            this.cboVehStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVehStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboVehStatus.FormattingEnabled = true;
            this.cboVehStatus.Items.AddRange(new object[] {
            "Tất cả",
            "Ready",
            "InTransit",
            "Maintenance"});
            this.cboVehStatus.Location = new System.Drawing.Point(355, 12);
            this.cboVehStatus.Name = "cboVehStatus";
            this.cboVehStatus.Size = new System.Drawing.Size(130, 29);
            this.cboVehStatus.TabIndex = 2;
            // 
            // cboVehType
            // 
            this.cboVehType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVehType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboVehType.FormattingEnabled = true;
            this.cboVehType.Items.AddRange(new object[] {
            "Tất cả",
            "Limousine 9 chỗ",
            "Giường nằm 24 chỗ",
            "Ghế ngồi 28 chỗ",
            "Giường nằm 34 chỗ",
            "Ghế ngồi 45 chỗ"});
            this.cboVehType.Location = new System.Drawing.Point(205, 12);
            this.cboVehType.Name = "cboVehType";
            this.cboVehType.Size = new System.Drawing.Size(140, 29);
            this.cboVehType.TabIndex = 1;
            // 
            // txtVehKeyword
            // 
            this.txtVehKeyword.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtVehKeyword.Location = new System.Drawing.Point(15, 12);
            this.txtVehKeyword.Name = "txtVehKeyword";
            this.txtVehKeyword.PlaceholderText = "🔍 Tìm biển số, loại xe...";
            this.txtVehKeyword.Size = new System.Drawing.Size(180, 29);
            this.txtVehKeyword.TabIndex = 0;
            // 
            // tabStations
            // 
            this.tabStations.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tabStations.Controls.Add(this.splitStations);
            this.tabStations.Controls.Add(this.pnlStationsFilter);
            this.tabStations.Location = new System.Drawing.Point(4, 46);
            this.tabStations.Name = "tabStations";
            this.tabStations.Size = new System.Drawing.Size(1242, 710);
            this.tabStations.TabIndex = 1;
            this.tabStations.Text = "📍 Quản Lý Trạm Dừng";
            // 
            // splitStations
            // 
            this.splitStations.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.splitStations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitStations.Location = new System.Drawing.Point(0, 85);
            this.splitStations.Name = "splitStations";
            // 
            // splitStations.Panel1
            // 
            this.splitStations.Panel1.Controls.Add(this.dgvStations);
            this.splitStations.Panel1MinSize = 400;
            // 
            // splitStations.Panel2
            // 
            this.splitStations.Panel2.Controls.Add(this.pnlStDetail);
            this.splitStations.Panel2MinSize = 350;
            this.splitStations.Size = new System.Drawing.Size(1242, 625);
            this.splitStations.SplitterDistance = 750;
            this.splitStations.TabIndex = 1;
            // 
            // dgvStations
            // 
            this.dgvStations.AllowUserToAddRows = false;
            this.dgvStations.AllowUserToDeleteRows = false;
            this.dgvStations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStations.BackgroundColor = System.Drawing.Color.White;
            this.dgvStations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvStations.ColumnHeadersHeight = 42;
            this.dgvStations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvStations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStId,
            this.colStCode,
            this.colStName,
            this.colStAddress,
            this.colStCity,
            this.colStRoutes,
            this.colStStatus});
            this.dgvStations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStations.EnableHeadersVisualStyles = false;
            this.dgvStations.Location = new System.Drawing.Point(0, 0);
            this.dgvStations.MultiSelect = false;
            this.dgvStations.Name = "dgvStations";
            this.dgvStations.ReadOnly = true;
            this.dgvStations.RowHeadersVisible = false;
            this.dgvStations.RowHeadersWidth = 51;
            this.dgvStations.RowTemplate.Height = 42;
            this.dgvStations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStations.Size = new System.Drawing.Size(750, 625);
            this.dgvStations.TabIndex = 0;
            // 
            // colStId
            // 
            this.colStId.HeaderText = "ID";
            this.colStId.MinimumWidth = 6;
            this.colStId.Name = "colStId";
            this.colStId.ReadOnly = true;
            this.colStId.Visible = false;
            this.colStId.Width = 125;
            // 
            // colStCode
            // 
            this.colStCode.HeaderText = "Mã Trạm";
            this.colStCode.MinimumWidth = 6;
            this.colStCode.Name = "colStCode";
            this.colStCode.ReadOnly = true;
            this.colStCode.Width = 130;
            // 
            // colStName
            // 
            this.colStName.HeaderText = "Tên Bến Xe / Trạm";
            this.colStName.MinimumWidth = 6;
            this.colStName.Name = "colStName";
            this.colStName.ReadOnly = true;
            this.colStName.Width = 200;
            // 
            // colStAddress
            // 
            this.colStAddress.HeaderText = "Địa Chỉ";
            this.colStAddress.MinimumWidth = 6;
            this.colStAddress.Name = "colStAddress";
            this.colStAddress.ReadOnly = true;
            this.colStAddress.Width = 230;
            // 
            // colStCity
            // 
            this.colStCity.HeaderText = "Tỉnh / TP";
            this.colStCity.MinimumWidth = 6;
            this.colStCity.Name = "colStCity";
            this.colStCity.ReadOnly = true;
            this.colStCity.Width = 130;
            // 
            // colStRoutes
            // 
            this.colStRoutes.HeaderText = "Số Tuyến";
            this.colStRoutes.MinimumWidth = 6;
            this.colStRoutes.Name = "colStRoutes";
            this.colStRoutes.ReadOnly = true;
            this.colStRoutes.Width = 85;
            // 
            // colStStatus
            // 
            this.colStStatus.HeaderText = "Trạng Thái";
            this.colStStatus.MinimumWidth = 6;
            this.colStStatus.Name = "colStStatus";
            this.colStStatus.ReadOnly = true;
            this.colStStatus.Width = 110;
            // 
            // pnlStDetail
            // 
            this.pnlStDetail.AutoScroll = true;
            this.pnlStDetail.BackColor = System.Drawing.Color.White;
            this.pnlStDetail.Controls.Add(this.btnStCancel);
            this.pnlStDetail.Controls.Add(this.btnStSave);
            this.pnlStDetail.Controls.Add(this.btnStDelete);
            this.pnlStDetail.Controls.Add(this.btnStEdit);
            this.pnlStDetail.Controls.Add(this.btnStAdd);
            this.pnlStDetail.Controls.Add(this.cboStEditStatus);
            this.pnlStDetail.Controls.Add(this.lblStStatus);
            this.pnlStDetail.Controls.Add(this.cboStEditCity);
            this.pnlStDetail.Controls.Add(this.lblStCity);
            this.pnlStDetail.Controls.Add(this.txtStAddress);
            this.pnlStDetail.Controls.Add(this.lblStAddress);
            this.pnlStDetail.Controls.Add(this.txtStName);
            this.pnlStDetail.Controls.Add(this.lblStName);
            this.pnlStDetail.Controls.Add(this.txtStCode);
            this.pnlStDetail.Controls.Add(this.lblStCode);
            this.pnlStDetail.Controls.Add(this.lblStTitle);
            this.pnlStDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStDetail.Location = new System.Drawing.Point(0, 0);
            this.pnlStDetail.Name = "pnlStDetail";
            this.pnlStDetail.Padding = new System.Windows.Forms.Padding(18);
            this.pnlStDetail.Size = new System.Drawing.Size(488, 625);
            this.pnlStDetail.TabIndex = 0;
            // 
            // btnStCancel
            // 
            this.btnStCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnStCancel.Enabled = false;
            this.btnStCancel.FlatAppearance.BorderSize = 0;
            this.btnStCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStCancel.ForeColor = System.Drawing.Color.White;
            this.btnStCancel.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btnStCancel.IconColor = System.Drawing.Color.White;
            this.btnStCancel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnStCancel.IconSize = 18;
            this.btnStCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStCancel.Location = new System.Drawing.Point(364, 320);
            this.btnStCancel.Name = "btnStCancel";
            this.btnStCancel.Size = new System.Drawing.Size(82, 36);
            this.btnStCancel.TabIndex = 15;
            this.btnStCancel.Text = " Hủy";
            this.btnStCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStCancel.UseVisualStyleBackColor = false;
            // 
            // btnStSave
            // 
            this.btnStSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnStSave.Enabled = false;
            this.btnStSave.FlatAppearance.BorderSize = 0;
            this.btnStSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStSave.ForeColor = System.Drawing.Color.White;
            this.btnStSave.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnStSave.IconColor = System.Drawing.Color.White;
            this.btnStSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnStSave.IconSize = 18;
            this.btnStSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStSave.Location = new System.Drawing.Point(276, 320);
            this.btnStSave.Name = "btnStSave";
            this.btnStSave.Size = new System.Drawing.Size(82, 36);
            this.btnStSave.TabIndex = 14;
            this.btnStSave.Text = " Lưu";
            this.btnStSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStSave.UseVisualStyleBackColor = false;
            // 
            // btnStDelete
            // 
            this.btnStDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnStDelete.FlatAppearance.BorderSize = 0;
            this.btnStDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStDelete.ForeColor = System.Drawing.Color.White;
            this.btnStDelete.IconChar = FontAwesome.Sharp.IconChar.Trash;
            this.btnStDelete.IconColor = System.Drawing.Color.White;
            this.btnStDelete.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnStDelete.IconSize = 18;
            this.btnStDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStDelete.Location = new System.Drawing.Point(188, 320);
            this.btnStDelete.Name = "btnStDelete";
            this.btnStDelete.Size = new System.Drawing.Size(82, 36);
            this.btnStDelete.TabIndex = 13;
            this.btnStDelete.Text = " Xóa";
            this.btnStDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStDelete.UseVisualStyleBackColor = false;
            // 
            // btnStEdit
            // 
            this.btnStEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnStEdit.FlatAppearance.BorderSize = 0;
            this.btnStEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStEdit.ForeColor = System.Drawing.Color.White;
            this.btnStEdit.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.btnStEdit.IconColor = System.Drawing.Color.White;
            this.btnStEdit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnStEdit.IconSize = 18;
            this.btnStEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStEdit.Location = new System.Drawing.Point(100, 320);
            this.btnStEdit.Name = "btnStEdit";
            this.btnStEdit.Size = new System.Drawing.Size(82, 36);
            this.btnStEdit.TabIndex = 12;
            this.btnStEdit.Text = " Sửa";
            this.btnStEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStEdit.UseVisualStyleBackColor = false;
            // 
            // btnStAdd
            // 
            this.btnStAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnStAdd.FlatAppearance.BorderSize = 0;
            this.btnStAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStAdd.ForeColor = System.Drawing.Color.White;
            this.btnStAdd.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnStAdd.IconColor = System.Drawing.Color.White;
            this.btnStAdd.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnStAdd.IconSize = 18;
            this.btnStAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStAdd.Location = new System.Drawing.Point(12, 320);
            this.btnStAdd.Name = "btnStAdd";
            this.btnStAdd.Size = new System.Drawing.Size(82, 36);
            this.btnStAdd.TabIndex = 11;
            this.btnStAdd.Text = " Thêm";
            this.btnStAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStAdd.UseVisualStyleBackColor = false;
            // 
            // cboStEditStatus
            // 
            this.cboStEditStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStEditStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboStEditStatus.FormattingEnabled = true;
            this.cboStEditStatus.Items.AddRange(new object[] {
            "Active",
            "Inactive"});
            this.cboStEditStatus.Location = new System.Drawing.Point(185, 252);
            this.cboStEditStatus.Name = "cboStEditStatus";
            this.cboStEditStatus.Size = new System.Drawing.Size(163, 29);
            this.cboStEditStatus.TabIndex = 10;
            // 
            // lblStStatus
            // 
            this.lblStStatus.AutoSize = true;
            this.lblStStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStStatus.Location = new System.Drawing.Point(185, 230);
            this.lblStStatus.Name = "lblStStatus";
            this.lblStStatus.Size = new System.Drawing.Size(89, 20);
            this.lblStStatus.TabIndex = 9;
            this.lblStStatus.Text = "Trạng Thái:";
            // 
            // cboStEditCity
            // 
            this.cboStEditCity.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboStEditCity.FormattingEnabled = true;
            this.cboStEditCity.Items.AddRange(new object[] {
            "Hà Nội",
            "Hải Phòng",
            "Đà Nẵng",
            "TP. Hồ Chí Minh",
            "Nghệ An",
            "Huế",
            "Quảng Ninh",
            "Cần Thơ"});
            this.cboStEditCity.Location = new System.Drawing.Point(18, 252);
            this.cboStEditCity.Name = "cboStEditCity";
            this.cboStEditCity.Size = new System.Drawing.Size(160, 29);
            this.cboStEditCity.TabIndex = 8;
            // 
            // lblStCity
            // 
            this.lblStCity.AutoSize = true;
            this.lblStCity.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStCity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStCity.Location = new System.Drawing.Point(18, 230);
            this.lblStCity.Name = "lblStCity";
            this.lblStCity.Size = new System.Drawing.Size(127, 20);
            this.lblStCity.TabIndex = 7;
            this.lblStCity.Text = "Tỉnh / Thành Phố:";
            // 
            // txtStAddress
            // 
            this.txtStAddress.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtStAddress.Location = new System.Drawing.Point(18, 192);
            this.txtStAddress.Name = "txtStAddress";
            this.txtStAddress.Size = new System.Drawing.Size(330, 29);
            this.txtStAddress.TabIndex = 6;
            // 
            // lblStAddress
            // 
            this.lblStAddress.AutoSize = true;
            this.lblStAddress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStAddress.Location = new System.Drawing.Point(18, 170);
            this.lblStAddress.Name = "lblStAddress";
            this.lblStAddress.Size = new System.Drawing.Size(125, 20);
            this.lblStAddress.TabIndex = 5;
            this.lblStAddress.Text = "Địa Chỉ Chi Tiết:";
            // 
            // txtStName
            // 
            this.txtStName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtStName.Location = new System.Drawing.Point(18, 132);
            this.txtStName.Name = "txtStName";
            this.txtStName.Size = new System.Drawing.Size(330, 29);
            this.txtStName.TabIndex = 4;
            // 
            // lblStName
            // 
            this.lblStName.AutoSize = true;
            this.lblStName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStName.Location = new System.Drawing.Point(18, 110);
            this.lblStName.Name = "lblStName";
            this.lblStName.Size = new System.Drawing.Size(161, 20);
            this.lblStName.TabIndex = 3;
            this.lblStName.Text = "Tên Bến Xe / Trạm *:";
            // 
            // txtStCode
            // 
            this.txtStCode.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtStCode.Location = new System.Drawing.Point(18, 72);
            this.txtStCode.Name = "txtStCode";
            this.txtStCode.Size = new System.Drawing.Size(330, 29);
            this.txtStCode.TabIndex = 2;
            // 
            // lblStCode
            // 
            this.lblStCode.AutoSize = true;
            this.lblStCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStCode.Location = new System.Drawing.Point(18, 50);
            this.lblStCode.Name = "lblStCode";
            this.lblStCode.Size = new System.Drawing.Size(138, 20);
            this.lblStCode.TabIndex = 1;
            this.lblStCode.Text = "Mã Trạm Dừng *:";
            // 
            // lblStTitle
            // 
            this.lblStTitle.AutoSize = true;
            this.lblStTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblStTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblStTitle.Location = new System.Drawing.Point(18, 15);
            this.lblStTitle.Name = "lblStTitle";
            this.lblStTitle.Size = new System.Drawing.Size(206, 25);
            this.lblStTitle.TabIndex = 0;
            this.lblStTitle.Text = "CHI TIẾT TRẠM DỪNG";
            // 
            // pnlStationsFilter
            // 
            this.pnlStationsFilter.BackColor = System.Drawing.Color.White;
            this.pnlStationsFilter.Controls.Add(this.btnStExport);
            this.pnlStationsFilter.Controls.Add(this.btnStReset);
            this.pnlStationsFilter.Controls.Add(this.radStOr);
            this.pnlStationsFilter.Controls.Add(this.radStAnd);
            this.pnlStationsFilter.Controls.Add(this.cboStStatus);
            this.pnlStationsFilter.Controls.Add(this.cboStCity);
            this.pnlStationsFilter.Controls.Add(this.txtStKeyword);
            this.pnlStationsFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStationsFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlStationsFilter.Name = "pnlStationsFilter";
            this.pnlStationsFilter.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlStationsFilter.Size = new System.Drawing.Size(1242, 85);
            this.pnlStationsFilter.TabIndex = 0;
            // 
            // btnStExport
            // 
            this.btnStExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnStExport.FlatAppearance.BorderSize = 0;
            this.btnStExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStExport.ForeColor = System.Drawing.Color.White;
            this.btnStExport.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            this.btnStExport.IconColor = System.Drawing.Color.White;
            this.btnStExport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnStExport.IconSize = 18;
            this.btnStExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStExport.Location = new System.Drawing.Point(420, 44);
            this.btnStExport.Name = "btnStExport";
            this.btnStExport.Size = new System.Drawing.Size(110, 34);
            this.btnStExport.TabIndex = 6;
            this.btnStExport.Text = " Xuất file";
            this.btnStExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStExport.UseVisualStyleBackColor = false;
            // 
            // btnStReset
            // 
            this.btnStReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnStReset.FlatAppearance.BorderSize = 0;
            this.btnStReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStReset.ForeColor = System.Drawing.Color.White;
            this.btnStReset.IconChar = FontAwesome.Sharp.IconChar.RotateLeft;
            this.btnStReset.IconColor = System.Drawing.Color.White;
            this.btnStReset.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnStReset.IconSize = 18;
            this.btnStReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStReset.Location = new System.Drawing.Point(310, 44);
            this.btnStReset.Name = "btnStReset";
            this.btnStReset.Size = new System.Drawing.Size(100, 34);
            this.btnStReset.TabIndex = 5;
            this.btnStReset.Text = " Đặt lại";
            this.btnStReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStReset.UseVisualStyleBackColor = false;
            // 
            // radStOr
            // 
            this.radStOr.AutoSize = true;
            this.radStOr.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.radStOr.Location = new System.Drawing.Point(160, 48);
            this.radStOr.Name = "radStOr";
            this.radStOr.Size = new System.Drawing.Size(155, 24);
            this.radStOr.TabIndex = 4;
            this.radStOr.Text = "OR (Khớp bất kỳ)";
            this.radStOr.UseVisualStyleBackColor = true;
            // 
            // radStAnd
            // 
            this.radStAnd.AutoSize = true;
            this.radStAnd.Checked = true;
            this.radStAnd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.radStAnd.Location = new System.Drawing.Point(15, 48);
            this.radStAnd.Name = "radStAnd";
            this.radStAnd.Size = new System.Drawing.Size(155, 24);
            this.radStAnd.TabIndex = 3;
            this.radStAnd.TabStop = true;
            this.radStAnd.Text = "AND (Khớp tất cả)";
            this.radStAnd.UseVisualStyleBackColor = true;
            // 
            // cboStStatus
            // 
            this.cboStStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboStStatus.FormattingEnabled = true;
            this.cboStStatus.Items.AddRange(new object[] {
            "Tất cả",
            "Active",
            "Inactive"});
            this.cboStStatus.Location = new System.Drawing.Point(415, 12);
            this.cboStStatus.Name = "cboStStatus";
            this.cboStStatus.Size = new System.Drawing.Size(130, 29);
            this.cboStStatus.TabIndex = 2;
            // 
            // cboStCity
            // 
            this.cboStCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStCity.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboStCity.FormattingEnabled = true;
            this.cboStCity.Items.AddRange(new object[] {
            "Tất cả",
            "Hà Nội",
            "Hải Phòng",
            "Đà Nẵng",
            "TP. Hồ Chí Minh",
            "Nghệ An"});
            this.cboStCity.Location = new System.Drawing.Point(245, 12);
            this.cboStCity.Name = "cboStCity";
            this.cboStCity.Size = new System.Drawing.Size(160, 29);
            this.cboStCity.TabIndex = 1;
            // 
            // txtStKeyword
            // 
            this.txtStKeyword.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtStKeyword.Location = new System.Drawing.Point(15, 12);
            this.txtStKeyword.Name = "txtStKeyword";
            this.txtStKeyword.PlaceholderText = "🔍 Tìm mã trạm, tên bến xe...";
            this.txtStKeyword.Size = new System.Drawing.Size(220, 29);
            this.txtStKeyword.TabIndex = 0;
            // 
            // UcManageVehicleAndStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.tabMain);
            this.Name = "UcManageVehicleAndStation";
            this.Size = new System.Drawing.Size(1250, 760);
            this.tabMain.ResumeLayout(false);
            this.tabVehicles.ResumeLayout(false);
            this.splitVehicles.Panel1.ResumeLayout(false);
            this.splitVehicles.Panel2.ResumeLayout(false);
            this.splitVehicles.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitVehicles)).EndInit();
            this.splitVehicles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehicles)).EndInit();
            this.pnlVehDetail.ResumeLayout(false);
            this.pnlVehDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVehSeats)).EndInit();
            this.pnlVehiclesFilter.ResumeLayout(false);
            this.pnlVehiclesFilter.PerformLayout();
            this.tabStations.ResumeLayout(false);
            this.splitStations.Panel1.ResumeLayout(false);
            this.splitStations.Panel2.ResumeLayout(false);
            this.splitStations.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitStations)).EndInit();
            this.splitStations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStations)).EndInit();
            this.pnlStDetail.ResumeLayout(false);
            this.pnlStDetail.PerformLayout();
            this.pnlStationsFilter.ResumeLayout(false);
            this.pnlStationsFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabVehicles;
        private System.Windows.Forms.SplitContainer splitVehicles;
        private System.Windows.Forms.DataGridView dgvVehicles;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVehId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVehPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVehType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVehSeats;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVehMfr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVehRoute;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVehStatus;
        private System.Windows.Forms.Panel pnlVehDetail;
        private FontAwesome.Sharp.IconButton btnVehCancel;
        private FontAwesome.Sharp.IconButton btnVehSave;
        private FontAwesome.Sharp.IconButton btnVehDelete;
        private FontAwesome.Sharp.IconButton btnVehEdit;
        private FontAwesome.Sharp.IconButton btnVehAdd;
        private System.Windows.Forms.ComboBox cboVehRoute;
        private System.Windows.Forms.Label lblVehRoute;
        private System.Windows.Forms.TextBox txtVehManufacturer;
        private System.Windows.Forms.Label lblVehManufacturer;
        private System.Windows.Forms.ComboBox cboVehEditStatus;
        private System.Windows.Forms.Label lblVehStatus;
        private System.Windows.Forms.NumericUpDown numVehSeats;
        private System.Windows.Forms.Label lblVehSeats;
        private System.Windows.Forms.ComboBox cboVehEditType;
        private System.Windows.Forms.Label lblVehType;
        private System.Windows.Forms.TextBox txtVehLicense;
        private System.Windows.Forms.Label lblVehLicense;
        private System.Windows.Forms.Label lblVehTitle;
        private System.Windows.Forms.Panel pnlVehiclesFilter;
        private FontAwesome.Sharp.IconButton btnVehExport;
        private FontAwesome.Sharp.IconButton btnVehReset;
        private System.Windows.Forms.RadioButton radVehOr;
        private System.Windows.Forms.RadioButton radVehAnd;
        private System.Windows.Forms.ComboBox cboVehManufacturer;
        private System.Windows.Forms.ComboBox cboVehStatus;
        private System.Windows.Forms.ComboBox cboVehType;
        private System.Windows.Forms.TextBox txtVehKeyword;
        private System.Windows.Forms.TabPage tabStations;
        private System.Windows.Forms.SplitContainer splitStations;
        private System.Windows.Forms.DataGridView dgvStations;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStCity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStRoutes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStStatus;
        private System.Windows.Forms.Panel pnlStDetail;
        private FontAwesome.Sharp.IconButton btnStCancel;
        private FontAwesome.Sharp.IconButton btnStSave;
        private FontAwesome.Sharp.IconButton btnStDelete;
        private FontAwesome.Sharp.IconButton btnStEdit;
        private FontAwesome.Sharp.IconButton btnStAdd;
        private System.Windows.Forms.ComboBox cboStEditStatus;
        private System.Windows.Forms.Label lblStStatus;
        private System.Windows.Forms.ComboBox cboStEditCity;
        private System.Windows.Forms.Label lblStCity;
        private System.Windows.Forms.TextBox txtStAddress;
        private System.Windows.Forms.Label lblStAddress;
        private System.Windows.Forms.TextBox txtStName;
        private System.Windows.Forms.Label lblStName;
        private System.Windows.Forms.TextBox txtStCode;
        private System.Windows.Forms.Label lblStCode;
        private System.Windows.Forms.Label lblStTitle;
        private System.Windows.Forms.Panel pnlStationsFilter;
        private FontAwesome.Sharp.IconButton btnStExport;
        private FontAwesome.Sharp.IconButton btnStReset;
        private System.Windows.Forms.RadioButton radStOr;
        private System.Windows.Forms.RadioButton radStAnd;
        private System.Windows.Forms.ComboBox cboStStatus;
        private System.Windows.Forms.ComboBox cboStCity;
        private System.Windows.Forms.TextBox txtStKeyword;
    }
}
