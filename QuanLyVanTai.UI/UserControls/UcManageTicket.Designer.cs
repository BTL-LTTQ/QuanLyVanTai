namespace QuanLyVanTai.UI.UserControls
{
    partial class UcManageTicket
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.Label lblStatTotal;
        private System.Windows.Forms.Label lblStatBooked;
        private System.Windows.Forms.Label lblStatPaid;
        private System.Windows.Forms.Label lblStatCancelled;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.ComboBox cboRoute;
        private System.Windows.Forms.ComboBox cboPayStatus;
        private System.Windows.Forms.ComboBox cboTicketStatus;
        private System.Windows.Forms.ComboBox cboPriceRange;
        private System.Windows.Forms.CheckBox chkUseDateFilter;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private FontAwesome.Sharp.IconButton btnRefresh;
        private FontAwesome.Sharp.IconButton btnAdd;
        private FontAwesome.Sharp.IconButton btnExport;
        private FontAwesome.Sharp.IconButton btnCheckInQr;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.DataGridView dgvTickets;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.Label lblTicketCode;
        private System.Windows.Forms.TextBox txtTicketCode;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label lblCustomerPhone;
        private System.Windows.Forms.TextBox txtCustomerPhone;
        private System.Windows.Forms.Label lblSeatNumber;
        private System.Windows.Forms.TextBox txtSeatNumber;
        private System.Windows.Forms.Label lblDetailRoute;
        private System.Windows.Forms.ComboBox cboDetailRoute;
        private System.Windows.Forms.Label lblDetailVehicle;
        private System.Windows.Forms.ComboBox cboDetailVehicle;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label lblDeparture;
        private System.Windows.Forms.DateTimePicker dtpDeparture;
        private System.Windows.Forms.Label lblDetailPayStatus;
        private System.Windows.Forms.ComboBox cboDetailPayStatus;
        private System.Windows.Forms.Label lblDetailTicketStatus;
        private System.Windows.Forms.ComboBox cboDetailTicketStatus;
        private FontAwesome.Sharp.IconButton btnSave;
        private FontAwesome.Sharp.IconButton btnCancel;
        private FontAwesome.Sharp.IconButton btnDelete;
        private FontAwesome.Sharp.IconButton btnCancelTicket;
        private System.Windows.Forms.ErrorProvider errorProviderMain;

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
            pnlHeader = new System.Windows.Forms.Panel();
            lblHeaderTitle = new System.Windows.Forms.Label();
            pnlStats = new System.Windows.Forms.Panel();
            pnlToolbar = new System.Windows.Forms.Panel();
            txtKeyword = new System.Windows.Forms.TextBox();
            cboRoute = new System.Windows.Forms.ComboBox();
            cboPayStatus = new System.Windows.Forms.ComboBox();
            cboTicketStatus = new System.Windows.Forms.ComboBox();
            cboPriceRange = new System.Windows.Forms.ComboBox();
            chkUseDateFilter = new System.Windows.Forms.CheckBox();
            dtpFrom = new System.Windows.Forms.DateTimePicker();
            lblTo = new System.Windows.Forms.Label();
            dtpTo = new System.Windows.Forms.DateTimePicker();
            btnRefresh = new FontAwesome.Sharp.IconButton();
            btnAdd = new FontAwesome.Sharp.IconButton();
            btnExport = new FontAwesome.Sharp.IconButton();
            btnCheckInQr = new FontAwesome.Sharp.IconButton();
            splitMain = new System.Windows.Forms.SplitContainer();
            dgvTickets = new System.Windows.Forms.DataGridView();
            pnlDetail = new System.Windows.Forms.Panel();
            lblDetailTitle = new System.Windows.Forms.Label();
            lblTicketCode = new System.Windows.Forms.Label();
            txtTicketCode = new System.Windows.Forms.TextBox();
            lblCustomerName = new System.Windows.Forms.Label();
            txtCustomerName = new System.Windows.Forms.TextBox();
            lblCustomerPhone = new System.Windows.Forms.Label();
            txtCustomerPhone = new System.Windows.Forms.TextBox();
            lblSeatNumber = new System.Windows.Forms.Label();
            txtSeatNumber = new System.Windows.Forms.TextBox();
            lblDetailRoute = new System.Windows.Forms.Label();
            cboDetailRoute = new System.Windows.Forms.ComboBox();
            lblDetailVehicle = new System.Windows.Forms.Label();
            cboDetailVehicle = new System.Windows.Forms.ComboBox();
            lblPrice = new System.Windows.Forms.Label();
            numPrice = new System.Windows.Forms.NumericUpDown();
            lblDeparture = new System.Windows.Forms.Label();
            dtpDeparture = new System.Windows.Forms.DateTimePicker();
            lblDetailPayStatus = new System.Windows.Forms.Label();
            cboDetailPayStatus = new System.Windows.Forms.ComboBox();
            lblDetailTicketStatus = new System.Windows.Forms.Label();
            cboDetailTicketStatus = new System.Windows.Forms.ComboBox();
            btnSave = new FontAwesome.Sharp.IconButton();
            btnCancel = new FontAwesome.Sharp.IconButton();
            btnDelete = new FontAwesome.Sharp.IconButton();
            btnCancelTicket = new FontAwesome.Sharp.IconButton();
            errorProviderMain = new System.Windows.Forms.ErrorProvider(components);
            pnlHeader.SuspendLayout();
            pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTickets).BeginInit();
            pnlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProviderMain).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = System.Drawing.Color.White;
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            pnlHeader.Size = new System.Drawing.Size(1200, 50);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblHeaderTitle.Location = new System.Drawing.Point(20, 12);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new System.Drawing.Size(1160, 26);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "🎫 QUẢN LÝ VÉ XE";
            lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlStats
            // 
            pnlStats.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            pnlStats.Dock = System.Windows.Forms.DockStyle.Top;
            pnlStats.Location = new System.Drawing.Point(0, 50);
            pnlStats.Name = "pnlStats";
            pnlStats.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            pnlStats.Size = new System.Drawing.Size(1200, 80);
            pnlStats.TabIndex = 1;
            // 
            // pnlToolbar
            // 
            pnlToolbar.BackColor = System.Drawing.Color.White;
            pnlToolbar.Controls.Add(btnCheckInQr);
            pnlToolbar.Controls.Add(btnExport);
            pnlToolbar.Controls.Add(btnAdd);
            pnlToolbar.Controls.Add(btnRefresh);
            pnlToolbar.Controls.Add(dtpTo);
            pnlToolbar.Controls.Add(lblTo);
            pnlToolbar.Controls.Add(dtpFrom);
            pnlToolbar.Controls.Add(chkUseDateFilter);
            pnlToolbar.Controls.Add(cboPriceRange);
            pnlToolbar.Controls.Add(cboTicketStatus);
            pnlToolbar.Controls.Add(cboPayStatus);
            pnlToolbar.Controls.Add(cboRoute);
            pnlToolbar.Controls.Add(txtKeyword);
            pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            pnlToolbar.Location = new System.Drawing.Point(0, 130);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            pnlToolbar.Size = new System.Drawing.Size(1200, 90);
            pnlToolbar.TabIndex = 2;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new System.Drawing.Point(15, 8);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "🔍 Mã vé, tên KH, SĐT...";
            txtKeyword.Size = new System.Drawing.Size(200, 27);
            txtKeyword.TabIndex = 0;
            // 
            // cboRoute
            // 
            cboRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboRoute.FormattingEnabled = true;
            cboRoute.Location = new System.Drawing.Point(225, 8);
            cboRoute.Name = "cboRoute";
            cboRoute.Size = new System.Drawing.Size(160, 28);
            cboRoute.TabIndex = 1;
            // 
            // cboPayStatus
            // 
            cboPayStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboPayStatus.FormattingEnabled = true;
            cboPayStatus.Location = new System.Drawing.Point(395, 8);
            cboPayStatus.Name = "cboPayStatus";
            cboPayStatus.Size = new System.Drawing.Size(120, 28);
            cboPayStatus.TabIndex = 2;
            // 
            // cboTicketStatus
            // 
            cboTicketStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboTicketStatus.FormattingEnabled = true;
            cboTicketStatus.Location = new System.Drawing.Point(525, 8);
            cboTicketStatus.Name = "cboTicketStatus";
            cboTicketStatus.Size = new System.Drawing.Size(120, 28);
            cboTicketStatus.TabIndex = 3;
            // 
            // cboPriceRange
            // 
            cboPriceRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboPriceRange.FormattingEnabled = true;
            cboPriceRange.Location = new System.Drawing.Point(655, 8);
            cboPriceRange.Name = "cboPriceRange";
            cboPriceRange.Size = new System.Drawing.Size(170, 28);
            cboPriceRange.TabIndex = 4;
            // 
            // chkUseDateFilter
            // 
            chkUseDateFilter.Location = new System.Drawing.Point(15, 46);
            chkUseDateFilter.Name = "chkUseDateFilter";
            chkUseDateFilter.Size = new System.Drawing.Size(140, 26);
            chkUseDateFilter.TabIndex = 4;
            chkUseDateFilter.Text = "Ngày khởi hành:";
            chkUseDateFilter.UseVisualStyleBackColor = true;
            // 
            // dtpFrom
            // 
            dtpFrom.Enabled = false;
            dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpFrom.Location = new System.Drawing.Point(160, 44);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new System.Drawing.Size(140, 27);
            dtpFrom.TabIndex = 5;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new System.Drawing.Point(306, 48);
            lblTo.Name = "lblTo";
            lblTo.Size = new System.Drawing.Size(21, 20);
            lblTo.TabIndex = 6;
            lblTo.Text = "→";
            // 
            // dtpTo
            // 
            dtpTo.Enabled = false;
            dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpTo.Location = new System.Drawing.Point(330, 44);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new System.Drawing.Size(140, 27);
            dtpTo.TabIndex = 7;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnRefresh.ForeColor = System.Drawing.Color.White;
            btnRefresh.IconChar = FontAwesome.Sharp.IconChar.RotateLeft;
            btnRefresh.IconColor = System.Drawing.Color.White;
            btnRefresh.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRefresh.IconSize = 16;
            btnRefresh.Location = new System.Drawing.Point(480, 42);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(105, 32);
            btnRefresh.TabIndex = 8;
            btnRefresh.Text = " Làm mới";
            btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnAdd.ForeColor = System.Drawing.Color.White;
            btnAdd.IconChar = FontAwesome.Sharp.IconChar.Plus;
            btnAdd.IconColor = System.Drawing.Color.White;
            btnAdd.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAdd.IconSize = 16;
            btnAdd.Location = new System.Drawing.Point(595, 42);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(110, 32);
            btnAdd.TabIndex = 9;
            btnAdd.Text = " Thêm vé";
            btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnExport
            // 
            btnExport.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnExport.ForeColor = System.Drawing.Color.White;
            btnExport.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            btnExport.IconColor = System.Drawing.Color.White;
            btnExport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnExport.IconSize = 16;
            btnExport.Location = new System.Drawing.Point(715, 42);
            btnExport.Name = "btnExport";
            btnExport.Size = new System.Drawing.Size(110, 32);
            btnExport.TabIndex = 10;
            btnExport.Text = " Xuất file";
            btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnExport.UseVisualStyleBackColor = false;
            // 
            // btnCheckInQr
            // 
            btnCheckInQr.BackColor = System.Drawing.Color.FromArgb(14, 165, 233);
            btnCheckInQr.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCheckInQr.FlatAppearance.BorderSize = 0;
            btnCheckInQr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCheckInQr.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnCheckInQr.ForeColor = System.Drawing.Color.White;
            btnCheckInQr.IconChar = FontAwesome.Sharp.IconChar.Qrcode;
            btnCheckInQr.IconColor = System.Drawing.Color.White;
            btnCheckInQr.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCheckInQr.IconSize = 16;
            btnCheckInQr.Location = new System.Drawing.Point(835, 42);
            btnCheckInQr.Name = "btnCheckInQr";
            btnCheckInQr.Size = new System.Drawing.Size(155, 32);
            btnCheckInQr.TabIndex = 11;
            btnCheckInQr.Text = " Quét QR Check-in";
            btnCheckInQr.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnCheckInQr.UseVisualStyleBackColor = false;
            // 
            // splitMain
            // 
            splitMain.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            splitMain.Location = new System.Drawing.Point(0, 220);
            splitMain.Name = "splitMain";
            splitMain.Panel1MinSize = 450;
            splitMain.Panel2MinSize = 350;
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(dgvTickets);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(pnlDetail);
            splitMain.Size = new System.Drawing.Size(1200, 530);
            splitMain.SplitterDistance = 750;
            splitMain.SplitterWidth = 6;
            splitMain.TabIndex = 3;
            // 
            // dgvTickets
            // 
            dgvTickets.AllowUserToAddRows = false;
            dgvTickets.AllowUserToDeleteRows = false;
            dgvTickets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvTickets.BackgroundColor = System.Drawing.Color.White;
            dgvTickets.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvTickets.ColumnHeadersHeight = 42;
            dgvTickets.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvTickets.EnableHeadersVisualStyles = false;
            dgvTickets.Location = new System.Drawing.Point(0, 0);
            dgvTickets.MultiSelect = false;
            dgvTickets.Name = "dgvTickets";
            dgvTickets.ReadOnly = true;
            dgvTickets.RowHeadersVisible = false;
            dgvTickets.RowTemplate.Height = 42;
            dgvTickets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvTickets.Size = new System.Drawing.Size(750, 530);
            dgvTickets.TabIndex = 0;
            // 
            // pnlDetail
            // 
            pnlDetail.AutoScroll = true;
            pnlDetail.BackColor = System.Drawing.Color.White;
            pnlDetail.Controls.Add(btnCancelTicket);
            pnlDetail.Controls.Add(btnDelete);
            pnlDetail.Controls.Add(btnCancel);
            pnlDetail.Controls.Add(btnSave);
            pnlDetail.Controls.Add(cboDetailTicketStatus);
            pnlDetail.Controls.Add(lblDetailTicketStatus);
            pnlDetail.Controls.Add(cboDetailPayStatus);
            pnlDetail.Controls.Add(lblDetailPayStatus);
            pnlDetail.Controls.Add(dtpDeparture);
            pnlDetail.Controls.Add(lblDeparture);
            pnlDetail.Controls.Add(numPrice);
            pnlDetail.Controls.Add(lblPrice);
            pnlDetail.Controls.Add(cboDetailVehicle);
            pnlDetail.Controls.Add(lblDetailVehicle);
            pnlDetail.Controls.Add(cboDetailRoute);
            pnlDetail.Controls.Add(lblDetailRoute);
            pnlDetail.Controls.Add(txtSeatNumber);
            pnlDetail.Controls.Add(lblSeatNumber);
            pnlDetail.Controls.Add(txtCustomerPhone);
            pnlDetail.Controls.Add(lblCustomerPhone);
            pnlDetail.Controls.Add(txtCustomerName);
            pnlDetail.Controls.Add(lblCustomerName);
            pnlDetail.Controls.Add(txtTicketCode);
            pnlDetail.Controls.Add(lblTicketCode);
            pnlDetail.Controls.Add(lblDetailTitle);
            pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlDetail.Location = new System.Drawing.Point(0, 0);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Padding = new System.Windows.Forms.Padding(18);
            pnlDetail.Size = new System.Drawing.Size(444, 530);
            pnlDetail.TabIndex = 0;
            // 
            // lblDetailTitle
            // 
            lblDetailTitle.AutoSize = true;
            lblDetailTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblDetailTitle.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            lblDetailTitle.Location = new System.Drawing.Point(18, 15);
            lblDetailTitle.Name = "lblDetailTitle";
            lblDetailTitle.Size = new System.Drawing.Size(148, 25);
            lblDetailTitle.TabIndex = 0;
            lblDetailTitle.Text = "CHI TIẾT VÉ XE";
            // 
            // lblTicketCode
            // 
            lblTicketCode.AutoSize = true;
            lblTicketCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblTicketCode.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblTicketCode.Location = new System.Drawing.Point(18, 50);
            lblTicketCode.Name = "lblTicketCode";
            lblTicketCode.Size = new System.Drawing.Size(58, 20);
            lblTicketCode.TabIndex = 1;
            lblTicketCode.Text = "Mã Vé:";
            // 
            // txtTicketCode
            // 
            txtTicketCode.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            txtTicketCode.Location = new System.Drawing.Point(18, 72);
            txtTicketCode.Name = "txtTicketCode";
            txtTicketCode.ReadOnly = true;
            txtTicketCode.Size = new System.Drawing.Size(330, 27);
            txtTicketCode.TabIndex = 2;
            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoSize = true;
            lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblCustomerName.Location = new System.Drawing.Point(18, 110);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new System.Drawing.Size(134, 20);
            lblCustomerName.TabIndex = 3;
            lblCustomerName.Text = "Tên Hành Khách *:";
            // 
            // txtCustomerName
            // 
            txtCustomerName.Location = new System.Drawing.Point(18, 132);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new System.Drawing.Size(330, 27);
            txtCustomerName.TabIndex = 4;
            // 
            // lblCustomerPhone
            // 
            lblCustomerPhone.AutoSize = true;
            lblCustomerPhone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblCustomerPhone.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblCustomerPhone.Location = new System.Drawing.Point(18, 170);
            lblCustomerPhone.Name = "lblCustomerPhone";
            lblCustomerPhone.Size = new System.Drawing.Size(123, 20);
            lblCustomerPhone.TabIndex = 5;
            lblCustomerPhone.Text = "Số Điện Thoại *:";
            // 
            // txtCustomerPhone
            // 
            txtCustomerPhone.Location = new System.Drawing.Point(18, 192);
            txtCustomerPhone.Name = "txtCustomerPhone";
            txtCustomerPhone.Size = new System.Drawing.Size(160, 27);
            txtCustomerPhone.TabIndex = 6;
            // 
            // lblSeatNumber
            // 
            lblSeatNumber.AutoSize = true;
            lblSeatNumber.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblSeatNumber.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblSeatNumber.Location = new System.Drawing.Point(195, 170);
            lblSeatNumber.Name = "lblSeatNumber";
            lblSeatNumber.Size = new System.Drawing.Size(72, 20);
            lblSeatNumber.TabIndex = 7;
            lblSeatNumber.Text = "Số Ghế *:";
            // 
            // txtSeatNumber
            // 
            txtSeatNumber.Location = new System.Drawing.Point(195, 192);
            txtSeatNumber.Name = "txtSeatNumber";
            txtSeatNumber.PlaceholderText = "VD: A01";
            txtSeatNumber.Size = new System.Drawing.Size(153, 27);
            txtSeatNumber.TabIndex = 8;
            // 
            // lblDetailRoute
            // 
            lblDetailRoute.AutoSize = true;
            lblDetailRoute.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblDetailRoute.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblDetailRoute.Location = new System.Drawing.Point(18, 230);
            lblDetailRoute.Name = "lblDetailRoute";
            lblDetailRoute.Size = new System.Drawing.Size(87, 20);
            lblDetailRoute.TabIndex = 9;
            lblDetailRoute.Text = "Tuyến Xe *:";
            // 
            // cboDetailRoute
            // 
            cboDetailRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboDetailRoute.FormattingEnabled = true;
            cboDetailRoute.Location = new System.Drawing.Point(18, 252);
            cboDetailRoute.Name = "cboDetailRoute";
            cboDetailRoute.Size = new System.Drawing.Size(330, 28);
            cboDetailRoute.TabIndex = 10;
            // 
            // lblDetailVehicle
            // 
            lblDetailVehicle.AutoSize = true;
            lblDetailVehicle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblDetailVehicle.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblDetailVehicle.Location = new System.Drawing.Point(18, 290);
            lblDetailVehicle.Name = "lblDetailVehicle";
            lblDetailVehicle.Size = new System.Drawing.Size(113, 20);
            lblDetailVehicle.TabIndex = 11;
            lblDetailVehicle.Text = "Phương Tiện *:";
            // 
            // cboDetailVehicle
            // 
            cboDetailVehicle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboDetailVehicle.FormattingEnabled = true;
            cboDetailVehicle.Location = new System.Drawing.Point(18, 312);
            cboDetailVehicle.Name = "cboDetailVehicle";
            cboDetailVehicle.Size = new System.Drawing.Size(330, 28);
            cboDetailVehicle.TabIndex = 12;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblPrice.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblPrice.Location = new System.Drawing.Point(18, 350);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new System.Drawing.Size(112, 20);
            lblPrice.TabIndex = 13;
            lblPrice.Text = "Giá Vé (VNĐ) *:";
            // 
            // numPrice
            // 
            numPrice.Increment = new decimal(new int[] { 10000, 0, 0, 0 });
            numPrice.Location = new System.Drawing.Point(18, 372);
            numPrice.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            numPrice.Name = "numPrice";
            numPrice.Size = new System.Drawing.Size(160, 27);
            numPrice.TabIndex = 14;
            numPrice.ThousandsSeparator = true;
            // 
            // lblDeparture
            // 
            lblDeparture.AutoSize = true;
            lblDeparture.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblDeparture.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblDeparture.Location = new System.Drawing.Point(18, 410);
            lblDeparture.Name = "lblDeparture";
            lblDeparture.Size = new System.Drawing.Size(156, 20);
            lblDeparture.TabIndex = 15;
            lblDeparture.Text = "Thời Gian Khởi Hành:";
            // 
            // dtpDeparture
            // 
            dtpDeparture.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpDeparture.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtpDeparture.Location = new System.Drawing.Point(18, 432);
            dtpDeparture.Name = "dtpDeparture";
            dtpDeparture.Size = new System.Drawing.Size(240, 27);
            dtpDeparture.TabIndex = 16;
            // 
            // lblDetailPayStatus
            // 
            lblDetailPayStatus.AutoSize = true;
            lblDetailPayStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblDetailPayStatus.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblDetailPayStatus.Location = new System.Drawing.Point(18, 470);
            lblDetailPayStatus.Name = "lblDetailPayStatus";
            lblDetailPayStatus.Size = new System.Drawing.Size(96, 20);
            lblDetailPayStatus.TabIndex = 17;
            lblDetailPayStatus.Text = "Thanh Toán:";
            // 
            // cboDetailPayStatus
            // 
            cboDetailPayStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboDetailPayStatus.FormattingEnabled = true;
            cboDetailPayStatus.Location = new System.Drawing.Point(18, 492);
            cboDetailPayStatus.Name = "cboDetailPayStatus";
            cboDetailPayStatus.Size = new System.Drawing.Size(155, 28);
            cboDetailPayStatus.TabIndex = 18;
            // 
            // lblDetailTicketStatus
            // 
            lblDetailTicketStatus.AutoSize = true;
            lblDetailTicketStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblDetailTicketStatus.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblDetailTicketStatus.Location = new System.Drawing.Point(185, 470);
            lblDetailTicketStatus.Name = "lblDetailTicketStatus";
            lblDetailTicketStatus.Size = new System.Drawing.Size(113, 20);
            lblDetailTicketStatus.TabIndex = 19;
            lblDetailTicketStatus.Text = "Trạng Thái Vé:";
            // 
            // cboDetailTicketStatus
            // 
            cboDetailTicketStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboDetailTicketStatus.FormattingEnabled = true;
            cboDetailTicketStatus.Location = new System.Drawing.Point(185, 492);
            cboDetailTicketStatus.Name = "cboDetailTicketStatus";
            cboDetailTicketStatus.Size = new System.Drawing.Size(163, 28);
            cboDetailTicketStatus.TabIndex = 20;
            // 
            // btnSave
            // 
            btnSave.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            btnSave.IconColor = System.Drawing.Color.White;
            btnSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSave.IconSize = 16;
            btnSave.Location = new System.Drawing.Point(12, 540);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(82, 36);
            btnSave.TabIndex = 21;
            btnSave.Text = " Lưu";
            btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            btnCancel.CausesValidation = false;
            btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnCancel.ForeColor = System.Drawing.Color.White;
            btnCancel.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            btnCancel.IconColor = System.Drawing.Color.White;
            btnCancel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancel.IconSize = 16;
            btnCancel.Location = new System.Drawing.Point(100, 540);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(82, 36);
            btnCancel.TabIndex = 22;
            btnCancel.Text = " Hủy";
            btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
            btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnDelete.ForeColor = System.Drawing.Color.White;
            btnDelete.IconChar = FontAwesome.Sharp.IconChar.Trash;
            btnDelete.IconColor = System.Drawing.Color.White;
            btnDelete.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDelete.IconSize = 16;
            btnDelete.Location = new System.Drawing.Point(188, 540);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(82, 36);
            btnDelete.TabIndex = 23;
            btnDelete.Text = " Xóa";
            btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnCancelTicket
            // 
            btnCancelTicket.BackColor = System.Drawing.Color.FromArgb(245, 158, 11);
            btnCancelTicket.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCancelTicket.FlatAppearance.BorderSize = 0;
            btnCancelTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancelTicket.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnCancelTicket.ForeColor = System.Drawing.Color.White;
            btnCancelTicket.IconChar = FontAwesome.Sharp.IconChar.Ban;
            btnCancelTicket.IconColor = System.Drawing.Color.White;
            btnCancelTicket.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelTicket.IconSize = 16;
            btnCancelTicket.Location = new System.Drawing.Point(276, 540);
            btnCancelTicket.Name = "btnCancelTicket";
            btnCancelTicket.Size = new System.Drawing.Size(102, 36);
            btnCancelTicket.TabIndex = 24;
            btnCancelTicket.Text = " Hủy vé";
            btnCancelTicket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnCancelTicket.UseVisualStyleBackColor = false;
            // 
            // errorProviderMain
            // 
            errorProviderMain.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            errorProviderMain.ContainerControl = this;
            // 
            // UcManageTicket
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            Controls.Add(splitMain);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlStats);
            Controls.Add(pnlHeader);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "UcManageTicket";
            Size = new System.Drawing.Size(1200, 750);
            pnlHeader.ResumeLayout(false);
            pnlToolbar.ResumeLayout(false);
            pnlToolbar.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            splitMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTickets).EndInit();
            pnlDetail.ResumeLayout(false);
            pnlDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProviderMain).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}
