namespace QuanLyVanTai.UI.UserControls
{
    partial class UcManageSchedule
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
            this.pnlStats = new System.Windows.Forms.Panel();
            this.cardCancelled = new System.Windows.Forms.Panel();
            this.lblCard4Val = new System.Windows.Forms.Label();
            this.lblCard4Title = new System.Windows.Forms.Label();
            this.barCard4 = new System.Windows.Forms.Panel();
            this.cardCompleted = new System.Windows.Forms.Panel();
            this.lblCard3Val = new System.Windows.Forms.Label();
            this.lblCard3Title = new System.Windows.Forms.Label();
            this.barCard3 = new System.Windows.Forms.Panel();
            this.cardRunning = new System.Windows.Forms.Panel();
            this.lblCard2Val = new System.Windows.Forms.Label();
            this.lblCard2Title = new System.Windows.Forms.Label();
            this.barCard2 = new System.Windows.Forms.Panel();
            this.cardTotal = new System.Windows.Forms.Panel();
            this.lblCard1Val = new System.Windows.Forms.Label();
            this.lblCard1Title = new System.Windows.Forms.Label();
            this.barCard1 = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cboLocTrangThai = new System.Windows.Forms.ComboBox();
            this.lblLocTrangThai = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.btnRefresh = new FontAwesome.Sharp.IconButton();
            this.btnCancel = new FontAwesome.Sharp.IconButton();
            this.btnSave = new FontAwesome.Sharp.IconButton();
            this.btnDelete = new FontAwesome.Sharp.IconButton();
            this.btnEdit = new FontAwesome.Sharp.IconButton();
            this.btnAdd = new FontAwesome.Sharp.IconButton();
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.dgvSchedules = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaChuyen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTuyen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBienSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKhoiHanh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDuKienDen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaiXe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.cboTaiXePhu = new System.Windows.Forms.ComboBox();
            this.lblTaiXePhu = new System.Windows.Forms.Label();
            this.cboTaiXeChinh = new System.Windows.Forms.ComboBox();
            this.lblTaiXeChinh = new System.Windows.Forms.Label();
            this.dtpDuKienDen = new System.Windows.Forms.DateTimePicker();
            this.lblDuKienDen = new System.Windows.Forms.Label();
            this.dtpKhoiHanh = new System.Windows.Forms.DateTimePicker();
            this.lblKhoiHanh = new System.Windows.Forms.Label();
            this.cboXe = new System.Windows.Forms.ComboBox();
            this.lblXe = new System.Windows.Forms.Label();
            this.cboTuyen = new System.Windows.Forms.ComboBox();
            this.lblTuyen = new System.Windows.Forms.Label();
            this.cboTrangThaiChuyen = new System.Windows.Forms.ComboBox();
            this.lblTrangThaiChuyen = new System.Windows.Forms.Label();
            this.txtMaChuyen = new System.Windows.Forms.TextBox();
            this.lblMaChuyen = new System.Windows.Forms.Label();
            this.lblDetailTitle = new System.Windows.Forms.Label();
            this.pnlDashboard.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.cardCancelled.SuspendLayout();
            this.cardCompleted.SuspendLayout();
            this.cardRunning.SuspendLayout();
            this.cardTotal.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedules)).BeginInit();
            this.pnlDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
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
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblPageTitle.Location = new System.Drawing.Point(20, 15);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(580, 30);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "🚌 ĐIỀU ĐỘ LỊCH TRÌNH & PHÂN CÔNG TÀI XẾ";
            // 
            // pnlStats
            // 
            this.pnlStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStats.BackColor = System.Drawing.Color.Transparent;
            this.pnlStats.Controls.Add(this.cardCancelled);
            this.pnlStats.Controls.Add(this.cardCompleted);
            this.pnlStats.Controls.Add(this.cardRunning);
            this.pnlStats.Controls.Add(this.cardTotal);
            this.pnlStats.Location = new System.Drawing.Point(20, 50);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(1210, 95);
            this.pnlStats.TabIndex = 1;
            // 
            // cardTotal
            // 
            this.cardTotal.BackColor = System.Drawing.Color.White;
            this.cardTotal.Controls.Add(this.lblCard1Val);
            this.cardTotal.Controls.Add(this.lblCard1Title);
            this.cardTotal.Controls.Add(this.barCard1);
            this.cardTotal.Location = new System.Drawing.Point(0, 0);
            this.cardTotal.Name = "cardTotal";
            this.cardTotal.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.cardTotal.Size = new System.Drawing.Size(290, 95);
            this.cardTotal.TabIndex = 0;
            // 
            // barCard1
            // 
            this.barCard1.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
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
            this.lblCard1Title.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCard1Title.Location = new System.Drawing.Point(25, 12);
            this.lblCard1Title.Name = "lblCard1Title";
            this.lblCard1Title.Size = new System.Drawing.Size(120, 21);
            this.lblCard1Title.TabIndex = 1;
            this.lblCard1Title.Text = "🚌 Tổng Số Chuyến";
            // 
            // lblCard1Val
            // 
            this.lblCard1Val.AutoSize = true;
            this.lblCard1Val.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblCard1Val.ForeColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.lblCard1Val.Location = new System.Drawing.Point(25, 38);
            this.lblCard1Val.Name = "lblCard1Val";
            this.lblCard1Val.Size = new System.Drawing.Size(35, 41);
            this.lblCard1Val.TabIndex = 2;
            this.lblCard1Val.Text = "0";
            // 
            // cardRunning
            // 
            this.cardRunning.BackColor = System.Drawing.Color.White;
            this.cardRunning.Controls.Add(this.lblCard2Val);
            this.cardRunning.Controls.Add(this.lblCard2Title);
            this.cardRunning.Controls.Add(this.barCard2);
            this.cardRunning.Location = new System.Drawing.Point(305, 0);
            this.cardRunning.Name = "cardRunning";
            this.cardRunning.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.cardRunning.Size = new System.Drawing.Size(290, 95);
            this.cardRunning.TabIndex = 1;
            // 
            // barCard2
            // 
            this.barCard2.BackColor = System.Drawing.Color.FromArgb(245, 158, 11);
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
            this.lblCard2Title.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCard2Title.Location = new System.Drawing.Point(25, 12);
            this.lblCard2Title.Name = "lblCard2Title";
            this.lblCard2Title.Size = new System.Drawing.Size(175, 21);
            this.lblCard2Title.TabIndex = 1;
            this.lblCard2Title.Text = "🚦 Chờ Chạy / Đang Chạy";
            // 
            // lblCard2Val
            // 
            this.lblCard2Val.AutoSize = true;
            this.lblCard2Val.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblCard2Val.ForeColor = System.Drawing.Color.FromArgb(245, 158, 11);
            this.lblCard2Val.Location = new System.Drawing.Point(25, 38);
            this.lblCard2Val.Name = "lblCard2Val";
            this.lblCard2Val.Size = new System.Drawing.Size(35, 41);
            this.lblCard2Val.TabIndex = 2;
            this.lblCard2Val.Text = "0";
            // 
            // cardCompleted
            // 
            this.cardCompleted.BackColor = System.Drawing.Color.White;
            this.cardCompleted.Controls.Add(this.lblCard3Val);
            this.cardCompleted.Controls.Add(this.lblCard3Title);
            this.cardCompleted.Controls.Add(this.barCard3);
            this.cardCompleted.Location = new System.Drawing.Point(610, 0);
            this.cardCompleted.Name = "cardCompleted";
            this.cardCompleted.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.cardCompleted.Size = new System.Drawing.Size(290, 95);
            this.cardCompleted.TabIndex = 2;
            // 
            // barCard3
            // 
            this.barCard3.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
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
            this.lblCard3Title.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCard3Title.Location = new System.Drawing.Point(25, 12);
            this.lblCard3Title.Name = "lblCard3Title";
            this.lblCard3Title.Size = new System.Drawing.Size(141, 21);
            this.lblCard3Title.TabIndex = 1;
            this.lblCard3Title.Text = "✅ Đã Hoàn Thành";
            // 
            // lblCard3Val
            // 
            this.lblCard3Val.AutoSize = true;
            this.lblCard3Val.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblCard3Val.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.lblCard3Val.Location = new System.Drawing.Point(25, 38);
            this.lblCard3Val.Name = "lblCard3Val";
            this.lblCard3Val.Size = new System.Drawing.Size(35, 41);
            this.lblCard3Val.TabIndex = 2;
            this.lblCard3Val.Text = "0";
            // 
            // cardCancelled
            // 
            this.cardCancelled.BackColor = System.Drawing.Color.White;
            this.cardCancelled.Controls.Add(this.lblCard4Val);
            this.cardCancelled.Controls.Add(this.lblCard4Title);
            this.cardCancelled.Controls.Add(this.barCard4);
            this.cardCancelled.Location = new System.Drawing.Point(915, 0);
            this.cardCancelled.Name = "cardCancelled";
            this.cardCancelled.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.cardCancelled.Size = new System.Drawing.Size(290, 95);
            this.cardCancelled.TabIndex = 3;
            // 
            // barCard4
            // 
            this.barCard4.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
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
            this.lblCard4Title.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCard4Title.Location = new System.Drawing.Point(25, 12);
            this.lblCard4Title.Name = "lblCard4Title";
            this.lblCard4Title.Size = new System.Drawing.Size(137, 21);
            this.lblCard4Title.TabIndex = 1;
            this.lblCard4Title.Text = "⚠️ Bị Trễ / Đã Hủy";
            // 
            // lblCard4Val
            // 
            this.lblCard4Val.AutoSize = true;
            this.lblCard4Val.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblCard4Val.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblCard4Val.Location = new System.Drawing.Point(25, 38);
            this.lblCard4Val.Name = "lblCard4Val";
            this.lblCard4Val.Size = new System.Drawing.Size(35, 41);
            this.lblCard4Val.TabIndex = 2;
            this.lblCard4Val.Text = "0";
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Controls.Add(this.txtSearch);
            this.pnlToolbar.Controls.Add(this.cboLocTrangThai);
            this.pnlToolbar.Controls.Add(this.lblLocTrangThai);
            this.pnlToolbar.Controls.Add(this.dtpDenNgay);
            this.pnlToolbar.Controls.Add(this.lblDenNgay);
            this.pnlToolbar.Controls.Add(this.dtpTuNgay);
            this.pnlToolbar.Controls.Add(this.lblTuNgay);
            this.pnlToolbar.Controls.Add(this.btnRefresh);
            this.pnlToolbar.Controls.Add(this.btnCancel);
            this.pnlToolbar.Controls.Add(this.btnSave);
            this.pnlToolbar.Controls.Add(this.btnDelete);
            this.pnlToolbar.Controls.Add(this.btnEdit);
            this.pnlToolbar.Controls.Add(this.btnAdd);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 160);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.pnlToolbar.Size = new System.Drawing.Size(1250, 112);
            this.pnlToolbar.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.btnAdd.IconColor = System.Drawing.Color.White;
            this.btnAdd.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAdd.IconSize = 22;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(20, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(140, 42);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = " Thêm Chuyến";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.IconChar = FontAwesome.Sharp.IconChar.PenToSquare;
            this.btnEdit.IconColor = System.Drawing.Color.White;
            this.btnEdit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEdit.IconSize = 22;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(170, 12);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(130, 42);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = " Chỉnh Sửa";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.IconChar = FontAwesome.Sharp.IconChar.CalendarXmark;
            this.btnDelete.IconColor = System.Drawing.Color.White;
            this.btnDelete.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDelete.IconSize = 22;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(310, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(140, 42);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = " Hủy Chuyến";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
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
            this.btnSave.Location = new System.Drawing.Point(460, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 42);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = " Lưu";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
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
            this.btnCancel.Location = new System.Drawing.Point(580, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 42);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = " Hủy";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.IconChar = FontAwesome.Sharp.IconChar.RotateRight;
            this.btnRefresh.IconColor = System.Drawing.Color.White;
            this.btnRefresh.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRefresh.IconSize = 22;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(690, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(130, 42);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = " Tải Lại";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTuNgay.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblTuNgay.Location = new System.Drawing.Point(20, 74);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(66, 21);
            this.lblTuNgay.TabIndex = 6;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(92, 70);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(140, 29);
            this.dtpTuNgay.TabIndex = 7;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDenNgay.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblDenNgay.Location = new System.Drawing.Point(246, 74);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(77, 21);
            this.lblDenNgay.TabIndex = 8;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(329, 70);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(140, 29);
            this.dtpDenNgay.TabIndex = 9;
            // 
            // lblLocTrangThai
            // 
            this.lblLocTrangThai.AutoSize = true;
            this.lblLocTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblLocTrangThai.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblLocTrangThai.Location = new System.Drawing.Point(483, 74);
            this.lblLocTrangThai.Name = "lblLocTrangThai";
            this.lblLocTrangThai.Size = new System.Drawing.Size(82, 21);
            this.lblLocTrangThai.TabIndex = 10;
            this.lblLocTrangThai.Text = "Trạng thái:";
            // 
            // cboLocTrangThai
            // 
            this.cboLocTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboLocTrangThai.FormattingEnabled = true;
            this.cboLocTrangThai.Location = new System.Drawing.Point(571, 70);
            this.cboLocTrangThai.Name = "cboLocTrangThai";
            this.cboLocTrangThai.Size = new System.Drawing.Size(180, 29);
            this.cboLocTrangThai.TabIndex = 11;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtSearch.Location = new System.Drawing.Point(930, 70);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "🔍 Tìm mã chuyến, tuyến, biển số, tài xế...";
            this.txtSearch.Size = new System.Drawing.Size(300, 31);
            this.txtSearch.TabIndex = 12;
            // 
            // pnlProgress
            // 
            this.pnlProgress.BackColor = System.Drawing.Color.White;
            this.pnlProgress.Controls.Add(this.pbProgress);
            this.pnlProgress.Controls.Add(this.lblProgress);
            this.pnlProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlProgress.Location = new System.Drawing.Point(0, 714);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8);
            this.pnlProgress.Size = new System.Drawing.Size(1250, 46);
            this.pnlProgress.TabIndex = 2;
            // 
            // pbProgress
            // 
            this.pbProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbProgress.Location = new System.Drawing.Point(20, 8);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(1020, 30);
            this.pbProgress.TabIndex = 0;
            this.pbProgress.Visible = false;
            // 
            // lblProgress
            // 
            this.lblProgress.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProgress.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblProgress.Location = new System.Drawing.Point(1040, 8);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(190, 30);
            this.lblProgress.TabIndex = 1;
            this.lblProgress.Text = "Sẵn sàng";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitMain
            // 
            this.splitMain.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 272);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.dgvSchedules);
            this.splitMain.Panel1MinSize = 460;
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlDetail);
            this.splitMain.Panel2MinSize = 420;
            this.splitMain.Size = new System.Drawing.Size(1250, 442);
            this.splitMain.SplitterDistance = 770;
            this.splitMain.SplitterWidth = 6;
            this.splitMain.TabIndex = 3;
            // 
            // dgvSchedules
            // 
            this.dgvSchedules.AllowUserToAddRows = false;
            this.dgvSchedules.AllowUserToDeleteRows = false;
            this.dgvSchedules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSchedules.BackgroundColor = System.Drawing.Color.White;
            this.dgvSchedules.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSchedules.ColumnHeadersHeight = 44;
            this.dgvSchedules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSchedules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colMaChuyen,
            this.colTuyen,
            this.colBienSo,
            this.colKhoiHanh,
            this.colDuKienDen,
            this.colTaiXe,
            this.colTrangThai,
            this.colGhiChu});
            this.dgvSchedules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSchedules.EnableHeadersVisualStyles = false;
            this.dgvSchedules.Location = new System.Drawing.Point(0, 0);
            this.dgvSchedules.MultiSelect = false;
            this.dgvSchedules.Name = "dgvSchedules";
            this.dgvSchedules.ReadOnly = true;
            this.dgvSchedules.RowHeadersVisible = false;
            this.dgvSchedules.RowHeadersWidth = 51;
            this.dgvSchedules.RowTemplate.Height = 44;
            this.dgvSchedules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSchedules.Size = new System.Drawing.Size(770, 442);
            this.dgvSchedules.TabIndex = 0;
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
            // colMaChuyen
            // 
            this.colMaChuyen.HeaderText = "Mã Chuyến";
            this.colMaChuyen.MinimumWidth = 6;
            this.colMaChuyen.Name = "colMaChuyen";
            this.colMaChuyen.ReadOnly = true;
            this.colMaChuyen.Width = 150;
            // 
            // colTuyen
            // 
            this.colTuyen.HeaderText = "Tuyến Xe";
            this.colTuyen.MinimumWidth = 6;
            this.colTuyen.Name = "colTuyen";
            this.colTuyen.ReadOnly = true;
            this.colTuyen.Width = 180;
            // 
            // colBienSo
            // 
            this.colBienSo.HeaderText = "Biển Số";
            this.colBienSo.MinimumWidth = 6;
            this.colBienSo.Name = "colBienSo";
            this.colBienSo.ReadOnly = true;
            this.colBienSo.Width = 110;
            // 
            // colKhoiHanh
            // 
            this.colKhoiHanh.HeaderText = "Khởi Hành";
            this.colKhoiHanh.MinimumWidth = 6;
            this.colKhoiHanh.Name = "colKhoiHanh";
            this.colKhoiHanh.ReadOnly = true;
            this.colKhoiHanh.Width = 140;
            // 
            // colDuKienDen
            // 
            this.colDuKienDen.HeaderText = "Dự Kiến Đến";
            this.colDuKienDen.MinimumWidth = 6;
            this.colDuKienDen.Name = "colDuKienDen";
            this.colDuKienDen.ReadOnly = true;
            this.colDuKienDen.Width = 140;
            // 
            // colTaiXe
            // 
            this.colTaiXe.HeaderText = "Tài Xế Chính";
            this.colTaiXe.MinimumWidth = 6;
            this.colTaiXe.Name = "colTaiXe";
            this.colTaiXe.ReadOnly = true;
            this.colTaiXe.Width = 150;
            // 
            // colTrangThai
            // 
            this.colTrangThai.HeaderText = "Trạng Thái";
            this.colTrangThai.MinimumWidth = 6;
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.ReadOnly = true;
            this.colTrangThai.Width = 130;
            // 
            // colGhiChu
            // 
            this.colGhiChu.HeaderText = "Ghi Chú Điều Phối";
            this.colGhiChu.MinimumWidth = 6;
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.ReadOnly = true;
            this.colGhiChu.Width = 190;
            // 
            // pnlDetail
            // 
            this.pnlDetail.AutoScroll = true;
            this.pnlDetail.BackColor = System.Drawing.Color.White;
            this.pnlDetail.Controls.Add(this.txtGhiChu);
            this.pnlDetail.Controls.Add(this.lblGhiChu);
            this.pnlDetail.Controls.Add(this.cboTaiXePhu);
            this.pnlDetail.Controls.Add(this.lblTaiXePhu);
            this.pnlDetail.Controls.Add(this.cboTaiXeChinh);
            this.pnlDetail.Controls.Add(this.lblTaiXeChinh);
            this.pnlDetail.Controls.Add(this.dtpDuKienDen);
            this.pnlDetail.Controls.Add(this.lblDuKienDen);
            this.pnlDetail.Controls.Add(this.dtpKhoiHanh);
            this.pnlDetail.Controls.Add(this.lblKhoiHanh);
            this.pnlDetail.Controls.Add(this.cboXe);
            this.pnlDetail.Controls.Add(this.lblXe);
            this.pnlDetail.Controls.Add(this.cboTuyen);
            this.pnlDetail.Controls.Add(this.lblTuyen);
            this.pnlDetail.Controls.Add(this.cboTrangThaiChuyen);
            this.pnlDetail.Controls.Add(this.lblTrangThaiChuyen);
            this.pnlDetail.Controls.Add(this.txtMaChuyen);
            this.pnlDetail.Controls.Add(this.lblMaChuyen);
            this.pnlDetail.Controls.Add(this.lblDetailTitle);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetail.Location = new System.Drawing.Point(0, 0);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Padding = new System.Windows.Forms.Padding(18);
            this.pnlDetail.Size = new System.Drawing.Size(474, 442);
            this.pnlDetail.TabIndex = 0;
            // 
            // lblDetailTitle
            // 
            this.lblDetailTitle.AutoSize = true;
            this.lblDetailTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDetailTitle.ForeColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.lblDetailTitle.Location = new System.Drawing.Point(18, 15);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Size = new System.Drawing.Size(372, 25);
            this.lblDetailTitle.TabIndex = 0;
            this.lblDetailTitle.Text = "CHI TIẾT CHUYẾN XE & PHÂN CÔNG TÀI XẾ";
            // 
            // lblMaChuyen
            // 
            this.lblMaChuyen.AutoSize = true;
            this.lblMaChuyen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaChuyen.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblMaChuyen.Location = new System.Drawing.Point(18, 52);
            this.lblMaChuyen.Name = "lblMaChuyen";
            this.lblMaChuyen.Size = new System.Drawing.Size(123, 20);
            this.lblMaChuyen.TabIndex = 1;
            this.lblMaChuyen.Text = "Mã Chuyến Xe:";
            // 
            // txtMaChuyen
            // 
            this.txtMaChuyen.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtMaChuyen.Location = new System.Drawing.Point(18, 76);
            this.txtMaChuyen.Name = "txtMaChuyen";
            this.txtMaChuyen.ReadOnly = true;
            this.txtMaChuyen.Size = new System.Drawing.Size(190, 29);
            this.txtMaChuyen.TabIndex = 2;
            // 
            // lblTrangThaiChuyen
            // 
            this.lblTrangThaiChuyen.AutoSize = true;
            this.lblTrangThaiChuyen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTrangThaiChuyen.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblTrangThaiChuyen.Location = new System.Drawing.Point(224, 52);
            this.lblTrangThaiChuyen.Name = "lblTrangThaiChuyen";
            this.lblTrangThaiChuyen.Size = new System.Drawing.Size(142, 20);
            this.lblTrangThaiChuyen.TabIndex = 3;
            this.lblTrangThaiChuyen.Text = "Trạng Thái Chuyến:";
            // 
            // cboTrangThaiChuyen
            // 
            this.cboTrangThaiChuyen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThaiChuyen.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboTrangThaiChuyen.FormattingEnabled = true;
            this.cboTrangThaiChuyen.Location = new System.Drawing.Point(224, 76);
            this.cboTrangThaiChuyen.Name = "cboTrangThaiChuyen";
            this.cboTrangThaiChuyen.Size = new System.Drawing.Size(200, 29);
            this.cboTrangThaiChuyen.TabIndex = 4;
            // 
            // lblTuyen
            // 
            this.lblTuyen.AutoSize = true;
            this.lblTuyen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTuyen.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblTuyen.Location = new System.Drawing.Point(18, 118);
            this.lblTuyen.Name = "lblTuyen";
            this.lblTuyen.Size = new System.Drawing.Size(138, 20);
            this.lblTuyen.TabIndex = 5;
            this.lblTuyen.Text = "Tuyến Xe (Active) *:";
            // 
            // cboTuyen
            // 
            this.cboTuyen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTuyen.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboTuyen.FormattingEnabled = true;
            this.cboTuyen.Location = new System.Drawing.Point(18, 142);
            this.cboTuyen.Name = "cboTuyen";
            this.cboTuyen.Size = new System.Drawing.Size(406, 29);
            this.cboTuyen.TabIndex = 6;
            // 
            // lblXe
            // 
            this.lblXe.AutoSize = true;
            this.lblXe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblXe.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblXe.Location = new System.Drawing.Point(18, 184);
            this.lblXe.Name = "lblXe";
            this.lblXe.Size = new System.Drawing.Size(122, 20);
            this.lblXe.TabIndex = 7;
            this.lblXe.Text = "Phương Tiện *:";
            // 
            // cboXe
            // 
            this.cboXe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboXe.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboXe.FormattingEnabled = true;
            this.cboXe.Location = new System.Drawing.Point(18, 208);
            this.cboXe.Name = "cboXe";
            this.cboXe.Size = new System.Drawing.Size(406, 29);
            this.cboXe.TabIndex = 8;
            // 
            // lblKhoiHanh
            // 
            this.lblKhoiHanh.AutoSize = true;
            this.lblKhoiHanh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKhoiHanh.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblKhoiHanh.Location = new System.Drawing.Point(18, 250);
            this.lblKhoiHanh.Name = "lblKhoiHanh";
            this.lblKhoiHanh.Size = new System.Drawing.Size(139, 20);
            this.lblKhoiHanh.TabIndex = 9;
            this.lblKhoiHanh.Text = "Khởi Hành (dự kiến):";
            // 
            // dtpKhoiHanh
            // 
            this.dtpKhoiHanh.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpKhoiHanh.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpKhoiHanh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpKhoiHanh.Location = new System.Drawing.Point(18, 274);
            this.dtpKhoiHanh.Name = "dtpKhoiHanh";
            this.dtpKhoiHanh.ShowUpDown = true;
            this.dtpKhoiHanh.Size = new System.Drawing.Size(200, 29);
            this.dtpKhoiHanh.TabIndex = 10;
            // 
            // lblDuKienDen
            // 
            this.lblDuKienDen.AutoSize = true;
            this.lblDuKienDen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDuKienDen.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblDuKienDen.Location = new System.Drawing.Point(228, 250);
            this.lblDuKienDen.Name = "lblDuKienDen";
            this.lblDuKienDen.Size = new System.Drawing.Size(154, 20);
            this.lblDuKienDen.TabIndex = 11;
            this.lblDuKienDen.Text = "Dự Kiến Đến (tự tính):";
            // 
            // dtpDuKienDen
            // 
            this.dtpDuKienDen.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpDuKienDen.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpDuKienDen.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDuKienDen.Location = new System.Drawing.Point(228, 274);
            this.dtpDuKienDen.Name = "dtpDuKienDen";
            this.dtpDuKienDen.ShowUpDown = true;
            this.dtpDuKienDen.Size = new System.Drawing.Size(196, 29);
            this.dtpDuKienDen.TabIndex = 12;
            // 
            // lblTaiXeChinh
            // 
            this.lblTaiXeChinh.AutoSize = true;
            this.lblTaiXeChinh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTaiXeChinh.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblTaiXeChinh.Location = new System.Drawing.Point(18, 316);
            this.lblTaiXeChinh.Name = "lblTaiXeChinh";
            this.lblTaiXeChinh.Size = new System.Drawing.Size(178, 20);
            this.lblTaiXeChinh.TabIndex = 13;
            this.lblTaiXeChinh.Text = "Tài Xế Chính (Primary) *:";
            // 
            // cboTaiXeChinh
            // 
            this.cboTaiXeChinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTaiXeChinh.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboTaiXeChinh.FormattingEnabled = true;
            this.cboTaiXeChinh.Location = new System.Drawing.Point(18, 340);
            this.cboTaiXeChinh.Name = "cboTaiXeChinh";
            this.cboTaiXeChinh.Size = new System.Drawing.Size(406, 29);
            this.cboTaiXeChinh.TabIndex = 14;
            // 
            // lblTaiXePhu
            // 
            this.lblTaiXePhu.AutoSize = true;
            this.lblTaiXePhu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTaiXePhu.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblTaiXePhu.Location = new System.Drawing.Point(18, 382);
            this.lblTaiXePhu.Name = "lblTaiXePhu";
            this.lblTaiXePhu.Size = new System.Drawing.Size(201, 20);
            this.lblTaiXePhu.TabIndex = 15;
            this.lblTaiXePhu.Text = "Tài Xế Phụ (Secondary - tùy chọn):";
            // 
            // cboTaiXePhu
            // 
            this.cboTaiXePhu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTaiXePhu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboTaiXePhu.FormattingEnabled = true;
            this.cboTaiXePhu.Location = new System.Drawing.Point(18, 406);
            this.cboTaiXePhu.Name = "cboTaiXePhu";
            this.cboTaiXePhu.Size = new System.Drawing.Size(406, 29);
            this.cboTaiXePhu.TabIndex = 16;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblGhiChu.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblGhiChu.Location = new System.Drawing.Point(18, 448);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(145, 20);
            this.lblGhiChu.TabIndex = 17;
            this.lblGhiChu.Text = "Ghi Chú Điều Phối:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtGhiChu.Location = new System.Drawing.Point(18, 472);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGhiChu.Size = new System.Drawing.Size(406, 70);
            this.txtGhiChu.TabIndex = 18;
            // 
            // UcManageSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlDashboard);
            this.Controls.Add(this.pnlProgress);
            this.Name = "UcManageSchedule";
            this.Size = new System.Drawing.Size(1250, 760);
            this.pnlDashboard.ResumeLayout(false);
            this.pnlDashboard.PerformLayout();
            this.pnlStats.ResumeLayout(false);
            this.cardCancelled.ResumeLayout(false);
            this.cardCancelled.PerformLayout();
            this.cardCompleted.ResumeLayout(false);
            this.cardCompleted.PerformLayout();
            this.cardRunning.ResumeLayout(false);
            this.cardRunning.PerformLayout();
            this.cardTotal.ResumeLayout(false);
            this.cardTotal.PerformLayout();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlProgress.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedules)).EndInit();
            this.pnlDetail.ResumeLayout(false);
            this.pnlDetail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDashboard;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.Panel cardTotal;
        private System.Windows.Forms.Label lblCard1Title;
        private System.Windows.Forms.Label lblCard1Val;
        private System.Windows.Forms.Panel barCard1;
        private System.Windows.Forms.Panel cardRunning;
        private System.Windows.Forms.Label lblCard2Title;
        private System.Windows.Forms.Label lblCard2Val;
        private System.Windows.Forms.Panel barCard2;
        private System.Windows.Forms.Panel cardCompleted;
        private System.Windows.Forms.Label lblCard3Title;
        private System.Windows.Forms.Label lblCard3Val;
        private System.Windows.Forms.Panel barCard3;
        private System.Windows.Forms.Panel cardCancelled;
        private System.Windows.Forms.Label lblCard4Title;
        private System.Windows.Forms.Label lblCard4Val;
        private System.Windows.Forms.Panel barCard4;
        private System.Windows.Forms.Panel pnlToolbar;
        private FontAwesome.Sharp.IconButton btnAdd;
        private FontAwesome.Sharp.IconButton btnEdit;
        private FontAwesome.Sharp.IconButton btnDelete;
        private FontAwesome.Sharp.IconButton btnSave;
        private FontAwesome.Sharp.IconButton btnCancel;
        private FontAwesome.Sharp.IconButton btnRefresh;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label lblLocTrangThai;
        private System.Windows.Forms.ComboBox cboLocTrangThai;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel pnlProgress;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.DataGridView dgvSchedules;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaChuyen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTuyen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBienSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKhoiHanh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDuKienDen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaiXe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChu;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.Label lblMaChuyen;
        private System.Windows.Forms.TextBox txtMaChuyen;
        private System.Windows.Forms.Label lblTrangThaiChuyen;
        private System.Windows.Forms.ComboBox cboTrangThaiChuyen;
        private System.Windows.Forms.Label lblTuyen;
        private System.Windows.Forms.ComboBox cboTuyen;
        private System.Windows.Forms.Label lblXe;
        private System.Windows.Forms.ComboBox cboXe;
        private System.Windows.Forms.Label lblKhoiHanh;
        private System.Windows.Forms.DateTimePicker dtpKhoiHanh;
        private System.Windows.Forms.Label lblDuKienDen;
        private System.Windows.Forms.DateTimePicker dtpDuKienDen;
        private System.Windows.Forms.Label lblTaiXeChinh;
        private System.Windows.Forms.ComboBox cboTaiXeChinh;
        private System.Windows.Forms.Label lblTaiXePhu;
        private System.Windows.Forms.ComboBox cboTaiXePhu;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
    }
}
