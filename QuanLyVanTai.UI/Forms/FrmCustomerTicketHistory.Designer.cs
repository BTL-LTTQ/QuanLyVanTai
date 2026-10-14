namespace QuanLyVanTai.UI.Forms
{
    partial class FrmCustomerTicketHistory
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            msMain = new MenuStrip();
            mnuHeThong = new ToolStripMenuItem();
            mnuLamMoi = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            mnuDong = new ToolStripMenuItem();
            mnuTacVu = new ToolStripMenuItem();
            mnuChiTiet = new ToolStripMenuItem();
            mnuInVe = new ToolStripMenuItem();
            mnuHuyVe = new ToolStripMenuItem();
            ssStatus = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            prgAction = new ToolStripProgressBar();
            lblTotalCount = new ToolStripStatusLabel();
            lblClock = new ToolStripStatusLabel();
            pnlMainContainer = new Panel();
            grpDanhSachVe = new GroupBox();
            dgvTickets = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colTicketCode = new DataGridViewTextBoxColumn();
            colRoute = new DataGridViewTextBoxColumn();
            colSeatNumber = new DataGridViewTextBoxColumn();
            colDepartureTime = new DataGridViewTextBoxColumn();
            colTicketStatus = new DataGridViewTextBoxColumn();
            pnlBottomActions = new Panel();
            flpButtons = new FlowLayoutPanel();
            btnChiTiet = new FontAwesome.Sharp.IconButton();
            btnInVe = new FontAwesome.Sharp.IconButton();
            btnHuyVe = new FontAwesome.Sharp.IconButton();
            btnDong = new FontAwesome.Sharp.IconButton();
            lblLuuYQuyDinh = new Label();
            grpTimKiem = new GroupBox();
            tlpTimKiem = new TableLayoutPanel();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            lblLocTrangThai = new Label();
            cboTrangThai = new ComboBox();
            btnTimKiem = new FontAwesome.Sharp.IconButton();
            btnLamMoi = new FontAwesome.Sharp.IconButton();
            timerClock = new System.Windows.Forms.Timer(components);
            msMain.SuspendLayout();
            ssStatus.SuspendLayout();
            pnlMainContainer.SuspendLayout();
            grpDanhSachVe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTickets).BeginInit();
            pnlBottomActions.SuspendLayout();
            flpButtons.SuspendLayout();
            grpTimKiem.SuspendLayout();
            tlpTimKiem.SuspendLayout();
            SuspendLayout();
            // 
            // msMain
            // 
            msMain.BackColor = Color.White;
            msMain.Font = new Font("Segoe UI", 9.5F);
            msMain.ImageScalingSize = new Size(20, 20);
            msMain.Items.AddRange(new ToolStripItem[] { mnuHeThong, mnuTacVu });
            msMain.Location = new Point(0, 0);
            msMain.Name = "msMain";
            msMain.Padding = new Padding(7, 5, 0, 5);
            msMain.Size = new Size(1182, 35);
            msMain.TabIndex = 0;
            msMain.Text = "msMain";
            // 
            // mnuHeThong
            // 
            mnuHeThong.DropDownItems.AddRange(new ToolStripItem[] { mnuLamMoi, toolStripSeparator1, mnuDong });
            mnuHeThong.Name = "mnuHeThong";
            mnuHeThong.Size = new Size(88, 25);
            mnuHeThong.Text = "&Hệ thống";
            // 
            // mnuLamMoi
            // 
            mnuLamMoi.Name = "mnuLamMoi";
            mnuLamMoi.ShortcutKeys = Keys.F5;
            mnuLamMoi.Size = new Size(181, 26);
            mnuLamMoi.Text = "&Làm mới";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(178, 6);
            // 
            // mnuDong
            // 
            mnuDong.Name = "mnuDong";
            mnuDong.ShortcutKeys = Keys.Alt | Keys.F4;
            mnuDong.Size = new Size(181, 26);
            mnuDong.Text = "Đó&ng";
            // 
            // mnuTacVu
            // 
            mnuTacVu.DropDownItems.AddRange(new ToolStripItem[] { mnuChiTiet, mnuInVe, mnuHuyVe });
            mnuTacVu.Name = "mnuTacVu";
            mnuTacVu.Size = new Size(66, 25);
            mnuTacVu.Text = "&Tác vụ";
            // 
            // mnuChiTiet
            // 
            mnuChiTiet.Name = "mnuChiTiet";
            mnuChiTiet.ShortcutKeys = Keys.Control | Keys.D;
            mnuChiTiet.Size = new Size(224, 26);
            mnuChiTiet.Text = "&Xem chi tiết";
            // 
            // mnuInVe
            // 
            mnuInVe.Name = "mnuInVe";
            mnuInVe.ShortcutKeys = Keys.Control | Keys.P;
            mnuInVe.Size = new Size(224, 26);
            mnuInVe.Text = "&In thẻ lên xe";
            // 
            // mnuHuyVe
            // 
            mnuHuyVe.Name = "mnuHuyVe";
            mnuHuyVe.ShortcutKeys = Keys.Delete;
            mnuHuyVe.Size = new Size(224, 26);
            mnuHuyVe.Text = "&Hủy vé xe";
            // 
            // ssStatus
            // 
            ssStatus.BackColor = Color.White;
            ssStatus.ImageScalingSize = new Size(20, 20);
            ssStatus.Items.AddRange(new ToolStripItem[] { lblStatus, prgAction, lblTotalCount, lblClock });
            ssStatus.Location = new Point(0, 723);
            ssStatus.Name = "ssStatus";
            ssStatus.Padding = new Padding(1, 0, 16, 0);
            ssStatus.Size = new Size(1182, 30);
            ssStatus.TabIndex = 2;
            ssStatus.Text = "ssStatus";
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Segoe UI Semibold", 9F);
            lblStatus.ForeColor = Color.FromArgb(37, 99, 235);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(70, 24);
            lblStatus.Text = "Sẵn sàng";
            // 
            // prgAction
            // 
            prgAction.Name = "prgAction";
            prgAction.Size = new Size(130, 22);
            prgAction.Style = ProgressBarStyle.Marquee;
            prgAction.Visible = false;
            // 
            // lblTotalCount
            // 
            lblTotalCount.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblTotalCount.BorderStyle = Border3DStyle.Etched;
            lblTotalCount.ForeColor = Color.FromArgb(71, 85, 105);
            lblTotalCount.Name = "lblTotalCount";
            lblTotalCount.Size = new Size(121, 24);
            lblTotalCount.Text = "Tổng cộng: 0 vé";
            // 
            // lblClock
            // 
            lblClock.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblClock.BorderStyle = Border3DStyle.Etched;
            lblClock.ForeColor = Color.FromArgb(71, 85, 105);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(974, 24);
            lblClock.Spring = true;
            lblClock.Text = "12/10/2026 15:30:00";
            lblClock.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlMainContainer
            // 
            pnlMainContainer.AutoScroll = true;
            pnlMainContainer.BackColor = Color.FromArgb(248, 250, 252);
            pnlMainContainer.Controls.Add(grpDanhSachVe);
            pnlMainContainer.Controls.Add(pnlBottomActions);
            pnlMainContainer.Controls.Add(grpTimKiem);
            pnlMainContainer.Dock = DockStyle.Fill;
            pnlMainContainer.Location = new Point(0, 35);
            pnlMainContainer.Name = "pnlMainContainer";
            pnlMainContainer.Padding = new Padding(15);
            pnlMainContainer.Size = new Size(1182, 688);
            pnlMainContainer.TabIndex = 1;
            // 
            // grpDanhSachVe
            // 
            grpDanhSachVe.BackColor = Color.White;
            grpDanhSachVe.Controls.Add(dgvTickets);
            grpDanhSachVe.Dock = DockStyle.Fill;
            grpDanhSachVe.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            grpDanhSachVe.ForeColor = Color.FromArgb(15, 23, 42);
            grpDanhSachVe.Location = new Point(15, 107);
            grpDanhSachVe.Name = "grpDanhSachVe";
            grpDanhSachVe.Padding = new Padding(12);
            grpDanhSachVe.Size = new Size(1152, 506);
            grpDanhSachVe.TabIndex = 1;
            grpDanhSachVe.TabStop = false;
            grpDanhSachVe.Text = " Danh Sách Vé Của Bạn (Chọn dòng vé rồi ấn Chi Tiết / In Vé / Hủy Vé) ";
            // 
            // dgvTickets
            // 
            dgvTickets.AllowUserToAddRows = false;
            dgvTickets.AllowUserToDeleteRows = false;
            dgvTickets.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 250, 252);
            dgvTickets.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvTickets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTickets.BackgroundColor = Color.FromArgb(241, 245, 249);
            dgvTickets.BorderStyle = BorderStyle.None;
            dgvTickets.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTickets.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.Padding = new Padding(4);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvTickets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvTickets.ColumnHeadersHeight = 38;
            dgvTickets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTickets.Columns.AddRange(new DataGridViewColumn[] { colId, colTicketCode, colRoute, colSeatNumber, colDepartureTime, colTicketStatus });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(224, 242, 254);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvTickets.DefaultCellStyle = dataGridViewCellStyle3;
            dgvTickets.Dock = DockStyle.Fill;
            dgvTickets.EnableHeadersVisualStyles = false;
            dgvTickets.GridColor = Color.FromArgb(226, 232, 240);
            dgvTickets.Location = new Point(12, 35);
            dgvTickets.MultiSelect = false;
            dgvTickets.Name = "dgvTickets";
            dgvTickets.ReadOnly = true;
            dgvTickets.RowHeadersVisible = false;
            dgvTickets.RowHeadersWidth = 51;
            dgvTickets.RowTemplate.Height = 38;
            dgvTickets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTickets.Size = new Size(1128, 459);
            dgvTickets.TabIndex = 0;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.HeaderText = "ID";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Visible = false;
            // 
            // colTicketCode
            // 
            colTicketCode.DataPropertyName = "TicketCode";
            colTicketCode.FillWeight = 110F;
            colTicketCode.HeaderText = "Mã Vé";
            colTicketCode.MinimumWidth = 110;
            colTicketCode.Name = "colTicketCode";
            colTicketCode.ReadOnly = true;
            // 
            // colRoute
            // 
            colRoute.DataPropertyName = "RouteName";
            colRoute.FillWeight = 220F;
            colRoute.HeaderText = "Tuyến Đường";
            colRoute.MinimumWidth = 180;
            colRoute.Name = "colRoute";
            colRoute.ReadOnly = true;
            // 
            // colSeatNumber
            // 
            colSeatNumber.DataPropertyName = "SeatNumber";
            colSeatNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSeatNumber.FillWeight = 70F;
            colSeatNumber.HeaderText = "Ghế";
            colSeatNumber.MinimumWidth = 65;
            colSeatNumber.Name = "colSeatNumber";
            colSeatNumber.ReadOnly = true;
            // 
            // colDepartureTime
            // 
            colDepartureTime.DataPropertyName = "DepartureTimeDisplay";
            colDepartureTime.FillWeight = 140F;
            colDepartureTime.HeaderText = "Giờ Khởi Hành";
            colDepartureTime.MinimumWidth = 135;
            colDepartureTime.Name = "colDepartureTime";
            colDepartureTime.ReadOnly = true;
            // 
            // colTicketStatus
            // 
            colTicketStatus.DataPropertyName = "TicketStatusDisplay";
            colTicketStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTicketStatus.FillWeight = 110F;
            colTicketStatus.HeaderText = "Trạng Thái";
            colTicketStatus.MinimumWidth = 100;
            colTicketStatus.Name = "colTicketStatus";
            colTicketStatus.ReadOnly = true;
            // 
            // pnlBottomActions
            // 
            pnlBottomActions.BackColor = Color.Transparent;
            pnlBottomActions.Controls.Add(flpButtons);
            pnlBottomActions.Controls.Add(lblLuuYQuyDinh);
            pnlBottomActions.Dock = DockStyle.Bottom;
            pnlBottomActions.Location = new Point(15, 613);
            pnlBottomActions.Name = "pnlBottomActions";
            pnlBottomActions.Padding = new Padding(0, 10, 0, 0);
            pnlBottomActions.Size = new Size(1152, 60);
            pnlBottomActions.TabIndex = 2;
            // 
            // flpButtons
            // 
            flpButtons.AutoSize = true;
            flpButtons.Controls.Add(btnChiTiet);
            flpButtons.Controls.Add(btnInVe);
            flpButtons.Controls.Add(btnHuyVe);
            flpButtons.Controls.Add(btnDong);
            flpButtons.Dock = DockStyle.Right;
            flpButtons.Location = new Point(532, 10);
            flpButtons.Name = "flpButtons";
            flpButtons.Size = new Size(620, 50);
            flpButtons.TabIndex = 1;
            flpButtons.WrapContents = false;
            // 
            // btnChiTiet
            // 
            btnChiTiet.BackColor = Color.FromArgb(37, 99, 235);
            btnChiTiet.Cursor = Cursors.Hand;
            btnChiTiet.FlatAppearance.BorderSize = 0;
            btnChiTiet.FlatStyle = FlatStyle.Flat;
            btnChiTiet.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnChiTiet.ForeColor = Color.White;
            btnChiTiet.IconChar = FontAwesome.Sharp.IconChar.CircleInfo;
            btnChiTiet.IconColor = Color.White;
            btnChiTiet.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnChiTiet.IconSize = 20;
            btnChiTiet.ImageAlign = ContentAlignment.MiddleLeft;
            btnChiTiet.Location = new Point(3, 4);
            btnChiTiet.Margin = new Padding(3, 4, 8, 4);
            btnChiTiet.Name = "btnChiTiet";
            btnChiTiet.Size = new Size(150, 42);
            btnChiTiet.TabIndex = 0;
            btnChiTiet.Text = " Xem Chi Tiết";
            btnChiTiet.TextAlign = ContentAlignment.MiddleRight;
            btnChiTiet.UseVisualStyleBackColor = false;
            // 
            // btnInVe
            // 
            btnInVe.BackColor = Color.FromArgb(16, 185, 129);
            btnInVe.Cursor = Cursors.Hand;
            btnInVe.FlatAppearance.BorderSize = 0;
            btnInVe.FlatStyle = FlatStyle.Flat;
            btnInVe.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnInVe.ForeColor = Color.White;
            btnInVe.IconChar = FontAwesome.Sharp.IconChar.Print;
            btnInVe.IconColor = Color.White;
            btnInVe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnInVe.IconSize = 20;
            btnInVe.ImageAlign = ContentAlignment.MiddleLeft;
            btnInVe.Location = new Point(164, 4);
            btnInVe.Margin = new Padding(3, 4, 8, 4);
            btnInVe.Name = "btnInVe";
            btnInVe.Size = new Size(150, 42);
            btnInVe.TabIndex = 1;
            btnInVe.Text = " In Thẻ Lên Xe";
            btnInVe.TextAlign = ContentAlignment.MiddleRight;
            btnInVe.UseVisualStyleBackColor = false;
            // 
            // btnHuyVe
            // 
            btnHuyVe.BackColor = Color.FromArgb(239, 68, 68);
            btnHuyVe.Cursor = Cursors.Hand;
            btnHuyVe.FlatAppearance.BorderSize = 0;
            btnHuyVe.FlatStyle = FlatStyle.Flat;
            btnHuyVe.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnHuyVe.ForeColor = Color.White;
            btnHuyVe.IconChar = FontAwesome.Sharp.IconChar.TimesCircle;
            btnHuyVe.IconColor = Color.White;
            btnHuyVe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnHuyVe.IconSize = 20;
            btnHuyVe.ImageAlign = ContentAlignment.MiddleLeft;
            btnHuyVe.Location = new Point(325, 4);
            btnHuyVe.Margin = new Padding(3, 4, 8, 4);
            btnHuyVe.Name = "btnHuyVe";
            btnHuyVe.Size = new Size(140, 42);
            btnHuyVe.TabIndex = 2;
            btnHuyVe.Text = " Hủy Vé Xe";
            btnHuyVe.TextAlign = ContentAlignment.MiddleRight;
            btnHuyVe.UseVisualStyleBackColor = false;
            // 
            // btnDong
            // 
            btnDong.BackColor = Color.FromArgb(100, 116, 139);
            btnDong.Cursor = Cursors.Hand;
            btnDong.FlatAppearance.BorderSize = 0;
            btnDong.FlatStyle = FlatStyle.Flat;
            btnDong.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnDong.ForeColor = Color.White;
            btnDong.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            btnDong.IconColor = Color.White;
            btnDong.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDong.IconSize = 20;
            btnDong.ImageAlign = ContentAlignment.MiddleLeft;
            btnDong.Location = new Point(476, 4);
            btnDong.Margin = new Padding(3, 4, 3, 4);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(110, 42);
            btnDong.TabIndex = 3;
            btnDong.Text = " Đóng";
            btnDong.TextAlign = ContentAlignment.MiddleRight;
            btnDong.UseVisualStyleBackColor = false;
            // 
            // lblLuuYQuyDinh
            // 
            lblLuuYQuyDinh.AutoSize = true;
            lblLuuYQuyDinh.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblLuuYQuyDinh.ForeColor = Color.FromArgb(220, 38, 38);
            lblLuuYQuyDinh.Location = new Point(0, 18);
            lblLuuYQuyDinh.Name = "lblLuuYQuyDinh";
            lblLuuYQuyDinh.Size = new Size(504, 20);
            lblLuuYQuyDinh.TabIndex = 0;
            lblLuuYQuyDinh.Text = "* Quy định vận tải: Quý khách chỉ được hủy vé trước giờ khởi hành tối thiểu 60 phút.";
            // 
            // grpTimKiem
            // 
            grpTimKiem.BackColor = Color.White;
            grpTimKiem.Controls.Add(tlpTimKiem);
            grpTimKiem.Dock = DockStyle.Top;
            grpTimKiem.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            grpTimKiem.ForeColor = Color.FromArgb(15, 23, 42);
            grpTimKiem.Location = new Point(15, 15);
            grpTimKiem.Name = "grpTimKiem";
            grpTimKiem.Padding = new Padding(12);
            grpTimKiem.Size = new Size(1152, 92);
            grpTimKiem.TabIndex = 0;
            grpTimKiem.TabStop = false;
            grpTimKiem.Text = " Tra Cứu - Lọc Vé ";
            // 
            // tlpTimKiem
            // 
            tlpTimKiem.ColumnCount = 6;
            tlpTimKiem.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            tlpTimKiem.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tlpTimKiem.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tlpTimKiem.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tlpTimKiem.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tlpTimKiem.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
            tlpTimKiem.Controls.Add(lblTimKiem, 0, 0);
            tlpTimKiem.Controls.Add(txtTimKiem, 1, 0);
            tlpTimKiem.Controls.Add(lblLocTrangThai, 2, 0);
            tlpTimKiem.Controls.Add(cboTrangThai, 3, 0);
            tlpTimKiem.Controls.Add(btnTimKiem, 4, 0);
            tlpTimKiem.Controls.Add(btnLamMoi, 5, 0);
            tlpTimKiem.Dock = DockStyle.Fill;
            tlpTimKiem.Location = new Point(12, 35);
            tlpTimKiem.Name = "tlpTimKiem";
            tlpTimKiem.RowCount = 1;
            tlpTimKiem.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpTimKiem.Size = new Size(1128, 45);
            tlpTimKiem.TabIndex = 0;
            // 
            // lblTimKiem
            // 
            lblTimKiem.Anchor = AnchorStyles.Left;
            lblTimKiem.AutoSize = true;
            lblTimKiem.Font = new Font("Segoe UI", 9F);
            lblTimKiem.ForeColor = Color.FromArgb(71, 85, 105);
            lblTimKiem.Location = new Point(3, 12);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(68, 20);
            lblTimKiem.TabIndex = 0;
            lblTimKiem.Text = "Tìm kiếm:";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtTimKiem.Font = new Font("Segoe UI", 9.5F);
            txtTimKiem.Location = new Point(78, 8);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Nhập mã vé, họ tên hoặc số điện thoại...";
            txtTimKiem.Size = new Size(325, 29);
            txtTimKiem.TabIndex = 1;
            // 
            // lblLocTrangThai
            // 
            lblLocTrangThai.Anchor = AnchorStyles.Left;
            lblLocTrangThai.AutoSize = true;
            lblLocTrangThai.Font = new Font("Segoe UI", 9F);
            lblLocTrangThai.ForeColor = Color.FromArgb(71, 85, 105);
            lblLocTrangThai.Location = new Point(409, 12);
            lblLocTrangThai.Name = "lblLocTrangThai";
            lblLocTrangThai.Size = new Size(78, 20);
            lblLocTrangThai.TabIndex = 2;
            lblLocTrangThai.Text = "Trạng thái:";
            // 
            // cboTrangThai
            // 
            cboTrangThai.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Font = new Font("Segoe UI", 9.5F);
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Location = new Point(504, 8);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(265, 29);
            cboTrangThai.TabIndex = 3;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Anchor = AnchorStyles.None;
            btnTimKiem.BackColor = Color.FromArgb(37, 99, 235);
            btnTimKiem.Cursor = Cursors.Hand;
            btnTimKiem.FlatAppearance.BorderSize = 0;
            btnTimKiem.FlatStyle = FlatStyle.Flat;
            btnTimKiem.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnTimKiem.ForeColor = Color.White;
            btnTimKiem.IconChar = FontAwesome.Sharp.IconChar.Search;
            btnTimKiem.IconColor = Color.White;
            btnTimKiem.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnTimKiem.IconSize = 18;
            btnTimKiem.ImageAlign = ContentAlignment.MiddleLeft;
            btnTimKiem.Location = new Point(777, 4);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(120, 36);
            btnTimKiem.TabIndex = 4;
            btnTimKiem.Text = " Tìm Kiếm";
            btnTimKiem.TextAlign = ContentAlignment.MiddleRight;
            btnTimKiem.UseVisualStyleBackColor = false;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Anchor = AnchorStyles.None;
            btnLamMoi.BackColor = Color.FromArgb(241, 245, 249);
            btnLamMoi.Cursor = Cursors.Hand;
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnLamMoi.ForeColor = Color.FromArgb(51, 65, 85);
            btnLamMoi.IconChar = FontAwesome.Sharp.IconChar.Rotate;
            btnLamMoi.IconColor = Color.FromArgb(51, 65, 85);
            btnLamMoi.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnLamMoi.IconSize = 18;
            btnLamMoi.ImageAlign = ContentAlignment.MiddleLeft;
            btnLamMoi.Location = new Point(906, 4);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(118, 36);
            btnLamMoi.TabIndex = 5;
            btnLamMoi.Text = " Làm Mới";
            btnLamMoi.TextAlign = ContentAlignment.MiddleRight;
            btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // FrmCustomerTicketHistory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 753);
            Controls.Add(pnlMainContainer);
            Controls.Add(ssStatus);
            Controls.Add(msMain);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            MainMenuStrip = msMain;
            MinimumSize = new Size(1000, 600);
            Name = "FrmCustomerTicketHistory";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Lịch Sử Đặt Vé & Quản Lý Thẻ Lên Xe - Hệ Thống Quản Lý Vận Tải";
            msMain.ResumeLayout(false);
            msMain.PerformLayout();
            ssStatus.ResumeLayout(false);
            ssStatus.PerformLayout();
            pnlMainContainer.ResumeLayout(false);
            grpDanhSachVe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTickets).EndInit();
            pnlBottomActions.ResumeLayout(false);
            pnlBottomActions.PerformLayout();
            flpButtons.ResumeLayout(false);
            grpTimKiem.ResumeLayout(false);
            tlpTimKiem.ResumeLayout(false);
            tlpTimKiem.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip msMain;
        private ToolStripMenuItem mnuHeThong;
        private ToolStripMenuItem mnuLamMoi;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem mnuDong;
        private ToolStripMenuItem mnuTacVu;
        private ToolStripMenuItem mnuChiTiet;
        private ToolStripMenuItem mnuInVe;
        private ToolStripMenuItem mnuHuyVe;
        private StatusStrip ssStatus;
        private ToolStripStatusLabel lblStatus;
        private ToolStripProgressBar prgAction;
        private ToolStripStatusLabel lblTotalCount;
        private ToolStripStatusLabel lblClock;
        private Panel pnlMainContainer;
        private GroupBox grpTimKiem;
        private TableLayoutPanel tlpTimKiem;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private Label lblLocTrangThai;
        private ComboBox cboTrangThai;
        private FontAwesome.Sharp.IconButton btnTimKiem;
        private FontAwesome.Sharp.IconButton btnLamMoi;
        private GroupBox grpDanhSachVe;
        private DataGridView dgvTickets;
        private Panel pnlBottomActions;
        private Label lblLuuYQuyDinh;
        private FlowLayoutPanel flpButtons;
        private FontAwesome.Sharp.IconButton btnChiTiet;
        private FontAwesome.Sharp.IconButton btnInVe;
        private FontAwesome.Sharp.IconButton btnHuyVe;
        private FontAwesome.Sharp.IconButton btnDong;
        private System.Windows.Forms.Timer timerClock;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colTicketCode;
        private DataGridViewTextBoxColumn colRoute;
        private DataGridViewTextBoxColumn colSeatNumber;
        private DataGridViewTextBoxColumn colDepartureTime;
        private DataGridViewTextBoxColumn colTicketStatus;
    }
}
