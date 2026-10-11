namespace QuanLyVanTai.UI.Forms
{
    partial class FormMain_KhachHang
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
            msMain = new MenuStrip();
            mnuHeThong = new ToolStripMenuItem();
            mnuDangXuat = new ToolStripMenuItem();
            mnuDoiMatKhau = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            mnuThoat = new ToolStripMenuItem();
            mnuVeCuaToi = new ToolStripMenuItem();
            mnuTraCuuVe = new ToolStripMenuItem();
            mnuLichSuDatVe = new ToolStripMenuItem();
            mnuTroGiup = new ToolStripMenuItem();
            mnuHuongDan = new ToolStripMenuItem();
            mnuThongTin = new ToolStripMenuItem();
            ssStatus = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            lblCurrentUser = new ToolStripStatusLabel();
            prgAction = new ToolStripProgressBar();
            lblClock = new ToolStripStatusLabel();
            splitMain = new SplitContainer();
            pnlLeftScroll = new Panel();
            flpButtons = new FlowLayoutPanel();
            btnDatVe = new FontAwesome.Sharp.IconButton();
            btnHuyChon = new FontAwesome.Sharp.IconButton();
            grpThanhToan = new GroupBox();
            tlpThanhToan = new TableLayoutPanel();
            lblDanhSachGheTitle = new Label();
            lblDanhSachGhe = new Label();
            lblDonGiaTitle = new Label();
            lblDonGia = new Label();
            lblMaKhuyenMai = new Label();
            pnlKhuyenMaiBox = new Panel();
            txtMaKhuyenMai = new TextBox();
            btnApDungKhuyenMai = new Button();
            lblTienGiamTitle = new Label();
            lblTongTienTitle = new Label();
            lblTongTien = new Label();
            lblTienGiam = new Label();
            grpThongTinKhach = new GroupBox();
            tlpKhachHang = new TableLayoutPanel();
            lblTenKhachHang = new Label();
            txtTenKhachHang = new TextBox();
            lblSoDienThoai = new Label();
            txtSoDienThoai = new TextBox();
            lblGhiChu = new Label();
            txtGhiChu = new TextBox();
            grpTimChuyen = new GroupBox();
            tlpTimChuyen = new TableLayoutPanel();
            lblTuyenXe = new Label();
            cboTuyenXe = new ComboBox();
            lblNgayDi = new Label();
            dtpNgayDi = new DateTimePicker();
            lblChuyenXe = new Label();
            cboChuyenXe = new ComboBox();
            btnTaiSoDo = new FontAwesome.Sharp.IconButton();
            pnlRight = new Panel();
            seatMapControl = new QuanLyVanTai.UI.Components.SeatMapControl();
            pnlLegend = new Panel();
            flpLegend = new FlowLayoutPanel();
            pnlLegendAvailColor = new Panel();
            lblLegendAvail = new Label();
            pnlLegendSelColor = new Panel();
            lblLegendSel = new Label();
            pnlLegendBookedColor = new Panel();
            lblLegendBooked = new Label();
            lblLegendSteering = new Label();
            btnXuatVe = new FontAwesome.Sharp.IconButton();
            errorProviderMain = new ErrorProvider(components);
            timerClock = new System.Windows.Forms.Timer(components);
            msMain.SuspendLayout();
            ssStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            pnlLeftScroll.SuspendLayout();
            flpButtons.SuspendLayout();
            grpThanhToan.SuspendLayout();
            tlpThanhToan.SuspendLayout();
            pnlKhuyenMaiBox.SuspendLayout();
            grpThongTinKhach.SuspendLayout();
            tlpKhachHang.SuspendLayout();
            grpTimChuyen.SuspendLayout();
            tlpTimChuyen.SuspendLayout();
            pnlRight.SuspendLayout();
            pnlLegend.SuspendLayout();
            flpLegend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProviderMain).BeginInit();
            SuspendLayout();
            // 
            // msMain
            // 
            msMain.BackColor = Color.White;
            msMain.Font = new Font("Segoe UI", 9.5F);
            msMain.ImageScalingSize = new Size(20, 20);
            msMain.Items.AddRange(new ToolStripItem[] { mnuHeThong, mnuVeCuaToi, mnuTroGiup });
            msMain.Location = new Point(0, 0);
            msMain.Name = "msMain";
            msMain.Padding = new Padding(7, 5, 0, 5);
            msMain.Size = new Size(1463, 35);
            msMain.TabIndex = 0;
            msMain.Text = "msMain";
            // 
            // mnuHeThong
            // 
            mnuHeThong.DropDownItems.AddRange(new ToolStripItem[] { mnuDangXuat, mnuDoiMatKhau, toolStripSeparator1, mnuThoat });
            mnuHeThong.Name = "mnuHeThong";
            mnuHeThong.Size = new Size(88, 25);
            mnuHeThong.Text = "&Hệ thống";
            // 
            // mnuDangXuat
            // 
            mnuDangXuat.Name = "mnuDangXuat";
            mnuDangXuat.ShortcutKeys = Keys.Control | Keys.Q;
            mnuDangXuat.Size = new Size(222, 26);
            mnuDangXuat.Text = "Đăng &xuất";
            // 
            // mnuDoiMatKhau
            // 
            mnuDoiMatKhau.Name = "mnuDoiMatKhau";
            mnuDoiMatKhau.Size = new Size(222, 26);
            mnuDoiMatKhau.Text = "Đổi &mật khẩu...";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(219, 6);
            // 
            // mnuThoat
            // 
            mnuThoat.Name = "mnuThoat";
            mnuThoat.ShortcutKeys = Keys.Alt | Keys.F4;
            mnuThoat.Size = new Size(222, 26);
            mnuThoat.Text = "&Thoát";
            // 
            // mnuVeCuaToi
            // 
            mnuVeCuaToi.DropDownItems.AddRange(new ToolStripItem[] { mnuTraCuuVe, mnuLichSuDatVe });
            mnuVeCuaToi.Name = "mnuVeCuaToi";
            mnuVeCuaToi.Size = new Size(91, 25);
            mnuVeCuaToi.Text = "&Vé của tôi";
            // 
            // mnuTraCuuVe
            // 
            mnuTraCuuVe.Name = "mnuTraCuuVe";
            mnuTraCuuVe.ShortcutKeys = Keys.Control | Keys.F;
            mnuTraCuuVe.Size = new Size(253, 26);
            mnuTraCuuVe.Text = "&Tra cứu mã vé...";
            // 
            // mnuLichSuDatVe
            // 
            mnuLichSuDatVe.Name = "mnuLichSuDatVe";
            mnuLichSuDatVe.ShortcutKeys = Keys.Control | Keys.H;
            mnuLichSuDatVe.Size = new Size(253, 26);
            mnuLichSuDatVe.Text = "&Lịch sử đặt vé";
            // 
            // mnuTroGiup
            // 
            mnuTroGiup.DropDownItems.AddRange(new ToolStripItem[] { mnuHuongDan, mnuThongTin });
            mnuTroGiup.Name = "mnuTroGiup";
            mnuTroGiup.Size = new Size(82, 25);
            mnuTroGiup.Text = "&Trợ giúp";
            // 
            // mnuHuongDan
            // 
            mnuHuongDan.Name = "mnuHuongDan";
            mnuHuongDan.ShortcutKeys = Keys.F1;
            mnuHuongDan.Size = new Size(267, 26);
            mnuHuongDan.Text = "&Hướng dẫn chọn ghế";
            // 
            // mnuThongTin
            // 
            mnuThongTin.Name = "mnuThongTin";
            mnuThongTin.Size = new Size(267, 26);
            mnuThongTin.Text = "Thông &tin phần mềm";
            // 
            // ssStatus
            // 
            ssStatus.BackColor = Color.White;
            ssStatus.Font = new Font("Segoe UI", 9F);
            ssStatus.ImageScalingSize = new Size(20, 20);
            ssStatus.Items.AddRange(new ToolStripItem[] { lblStatus, lblCurrentUser, prgAction, lblClock });
            ssStatus.Location = new Point(0, 1037);
            ssStatus.Name = "ssStatus";
            ssStatus.Padding = new Padding(1, 0, 16, 0);
            ssStatus.Size = new Size(1463, 30);
            ssStatus.TabIndex = 1;
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
            // lblCurrentUser
            // 
            lblCurrentUser.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblCurrentUser.BorderStyle = Border3DStyle.Etched;
            lblCurrentUser.ForeColor = Color.FromArgb(71, 85, 105);
            lblCurrentUser.Name = "lblCurrentUser";
            lblCurrentUser.Size = new Size(241, 24);
            lblCurrentUser.Text = "Nhân viên: Khách Hàng Trực Tuyến";
            // 
            // prgAction
            // 
            prgAction.Name = "prgAction";
            prgAction.Size = new Size(137, 23);
            prgAction.Style = ProgressBarStyle.Marquee;
            prgAction.Visible = false;
            // 
            // lblClock
            // 
            lblClock.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblClock.BorderStyle = Border3DStyle.Etched;
            lblClock.ForeColor = Color.FromArgb(71, 85, 105);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(1135, 24);
            lblClock.Spring = true;
            lblClock.Text = "10/10/2026 20:30:00";
            lblClock.TextAlign = ContentAlignment.MiddleRight;
            // 
            // splitMain
            // 
            splitMain.Dock = DockStyle.Fill;
            splitMain.FixedPanel = FixedPanel.Panel1;
            splitMain.Location = new Point(0, 35);
            splitMain.Margin = new Padding(3, 4, 3, 4);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.BackColor = Color.FromArgb(248, 250, 252);
            splitMain.Panel1.Controls.Add(pnlLeftScroll);
            splitMain.Panel1MinSize = 420;
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.BackColor = Color.FromArgb(241, 245, 249);
            splitMain.Panel2.Controls.Add(pnlRight);
            splitMain.Panel2MinSize = 500;
            splitMain.Size = new Size(1463, 1002);
            splitMain.SplitterDistance = 514;
            splitMain.SplitterWidth = 6;
            splitMain.TabIndex = 2;
            // 
            // pnlLeftScroll
            // 
            pnlLeftScroll.AutoScroll = true;
            pnlLeftScroll.Controls.Add(flpButtons);
            pnlLeftScroll.Controls.Add(grpThanhToan);
            pnlLeftScroll.Controls.Add(grpThongTinKhach);
            pnlLeftScroll.Controls.Add(grpTimChuyen);
            pnlLeftScroll.Dock = DockStyle.Fill;
            pnlLeftScroll.Location = new Point(0, 0);
            pnlLeftScroll.Margin = new Padding(3, 4, 3, 4);
            pnlLeftScroll.Name = "pnlLeftScroll";
            pnlLeftScroll.Padding = new Padding(14, 16, 14, 16);
            pnlLeftScroll.Size = new Size(514, 1002);
            pnlLeftScroll.TabIndex = 0;
            // 
            // flpButtons
            // 
            flpButtons.Controls.Add(btnDatVe);
            flpButtons.Controls.Add(btnHuyChon);
            flpButtons.Dock = DockStyle.Top;
            flpButtons.Location = new Point(14, 796);
            flpButtons.Margin = new Padding(3, 4, 3, 4);
            flpButtons.Name = "flpButtons";
            flpButtons.Padding = new Padding(0, 11, 0, 0);
            flpButtons.Size = new Size(486, 127);
            flpButtons.TabIndex = 3;
            // 
            // btnDatVe
            // 
            btnDatVe.BackColor = Color.FromArgb(16, 185, 129);
            btnDatVe.Cursor = Cursors.Hand;
            btnDatVe.FlatAppearance.BorderSize = 0;
            btnDatVe.FlatStyle = FlatStyle.Flat;
            btnDatVe.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnDatVe.ForeColor = Color.White;
            btnDatVe.IconChar = FontAwesome.Sharp.IconChar.Ticket;
            btnDatVe.IconColor = Color.White;
            btnDatVe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDatVe.IconSize = 20;
            btnDatVe.ImageAlign = ContentAlignment.MiddleLeft;
            btnDatVe.Location = new Point(3, 15);
            btnDatVe.Margin = new Padding(3, 4, 3, 4);
            btnDatVe.Name = "btnDatVe";
            btnDatVe.Size = new Size(276, 53);
            btnDatVe.TabIndex = 0;
            btnDatVe.Text = " ĐẶT VÉ (F5/Ctrl+S)";
            btnDatVe.UseVisualStyleBackColor = false;
            // 
            // btnHuyChon
            // 
            btnHuyChon.BackColor = Color.FromArgb(226, 232, 240);
            btnHuyChon.Cursor = Cursors.Hand;
            btnHuyChon.FlatAppearance.BorderSize = 0;
            btnHuyChon.FlatStyle = FlatStyle.Flat;
            btnHuyChon.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnHuyChon.ForeColor = Color.FromArgb(71, 85, 105);
            btnHuyChon.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnHuyChon.IconColor = Color.FromArgb(71, 85, 105);
            btnHuyChon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnHuyChon.IconSize = 16;
            btnHuyChon.ImageAlign = ContentAlignment.MiddleLeft;
            btnHuyChon.Location = new Point(285, 15);
            btnHuyChon.Margin = new Padding(3, 4, 3, 4);
            btnHuyChon.Name = "btnHuyChon";
            btnHuyChon.Size = new Size(187, 53);
            btnHuyChon.TabIndex = 2;
            btnHuyChon.Text = " Bỏ chọn tất cả";
            btnHuyChon.UseVisualStyleBackColor = false;
            // 
            // grpThanhToan
            // 
            grpThanhToan.BackColor = Color.White;
            grpThanhToan.Controls.Add(tlpThanhToan);
            grpThanhToan.Dock = DockStyle.Top;
            grpThanhToan.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            grpThanhToan.ForeColor = Color.FromArgb(15, 23, 42);
            grpThanhToan.Location = new Point(14, 511);
            grpThanhToan.Margin = new Padding(3, 4, 3, 4);
            grpThanhToan.Name = "grpThanhToan";
            grpThanhToan.Padding = new Padding(11);
            grpThanhToan.Size = new Size(486, 285);
            grpThanhToan.TabIndex = 2;
            grpThanhToan.TabStop = false;
            grpThanhToan.Text = "3. THANH TOÁN VÉ";
            // 
            // tlpThanhToan
            // 
            tlpThanhToan.ColumnCount = 2;
            tlpThanhToan.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 137F));
            tlpThanhToan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpThanhToan.Controls.Add(lblDanhSachGheTitle, 0, 0);
            tlpThanhToan.Controls.Add(lblDanhSachGhe, 1, 0);
            tlpThanhToan.Controls.Add(lblDonGiaTitle, 0, 1);
            tlpThanhToan.Controls.Add(lblDonGia, 1, 1);
            tlpThanhToan.Controls.Add(lblMaKhuyenMai, 0, 2);
            tlpThanhToan.Controls.Add(pnlKhuyenMaiBox, 1, 2);
            tlpThanhToan.Controls.Add(lblTienGiamTitle, 0, 3);
            tlpThanhToan.Controls.Add(lblTongTienTitle, 0, 4);
            tlpThanhToan.Controls.Add(lblTongTien, 1, 4);
            tlpThanhToan.Controls.Add(lblTienGiam, 1, 3);
            tlpThanhToan.Dock = DockStyle.Fill;
            tlpThanhToan.Location = new Point(11, 33);
            tlpThanhToan.Margin = new Padding(3, 4, 3, 4);
            tlpThanhToan.Name = "tlpThanhToan";
            tlpThanhToan.RowCount = 5;
            tlpThanhToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            tlpThanhToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            tlpThanhToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            tlpThanhToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            tlpThanhToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            tlpThanhToan.Size = new Size(464, 241);
            tlpThanhToan.TabIndex = 0;
            // 
            // lblDanhSachGheTitle
            // 
            lblDanhSachGheTitle.Anchor = AnchorStyles.Left;
            lblDanhSachGheTitle.AutoSize = true;
            lblDanhSachGheTitle.Font = new Font("Segoe UI", 9F);
            lblDanhSachGheTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblDanhSachGheTitle.Location = new Point(3, 11);
            lblDanhSachGheTitle.Name = "lblDanhSachGheTitle";
            lblDanhSachGheTitle.Size = new Size(95, 20);
            lblDanhSachGheTitle.TabIndex = 0;
            lblDanhSachGheTitle.Text = "Ghế đã chọn:";
            // 
            // lblDanhSachGhe
            // 
            lblDanhSachGhe.Anchor = AnchorStyles.Left;
            lblDanhSachGhe.AutoSize = true;
            lblDanhSachGhe.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblDanhSachGhe.ForeColor = Color.FromArgb(37, 99, 235);
            lblDanhSachGhe.Location = new Point(140, 10);
            lblDanhSachGhe.Name = "lblDanhSachGhe";
            lblDanhSachGhe.Size = new Size(128, 23);
            lblDanhSachGhe.TabIndex = 1;
            lblDanhSachGhe.Text = "Chưa chọn ghế";
            // 
            // lblDonGiaTitle
            // 
            lblDonGiaTitle.Anchor = AnchorStyles.Left;
            lblDonGiaTitle.AutoSize = true;
            lblDonGiaTitle.Font = new Font("Segoe UI", 9F);
            lblDonGiaTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblDonGiaTitle.Location = new Point(3, 54);
            lblDonGiaTitle.Name = "lblDonGiaTitle";
            lblDonGiaTitle.Size = new Size(65, 20);
            lblDonGiaTitle.TabIndex = 2;
            lblDonGiaTitle.Text = "Đơn giá:";
            // 
            // lblDonGia
            // 
            lblDonGia.Anchor = AnchorStyles.Left;
            lblDonGia.AutoSize = true;
            lblDonGia.Font = new Font("Segoe UI", 9.5F);
            lblDonGia.ForeColor = Color.FromArgb(15, 23, 42);
            lblDonGia.Location = new Point(140, 54);
            lblDonGia.Name = "lblDonGia";
            lblDonGia.Size = new Size(56, 21);
            lblDonGia.TabIndex = 3;
            lblDonGia.Text = "0 VNĐ";
            // 
            // lblMaKhuyenMai
            // 
            lblMaKhuyenMai.Anchor = AnchorStyles.Left;
            lblMaKhuyenMai.AutoSize = true;
            lblMaKhuyenMai.Font = new Font("Segoe UI", 9F);
            lblMaKhuyenMai.ForeColor = Color.FromArgb(71, 85, 105);
            lblMaKhuyenMai.Location = new Point(3, 101);
            lblMaKhuyenMai.Name = "lblMaKhuyenMai";
            lblMaKhuyenMai.Size = new Size(89, 20);
            lblMaKhuyenMai.TabIndex = 4;
            lblMaKhuyenMai.Text = "Khuyến mãi:";
            // 
            // pnlKhuyenMaiBox
            // 
            pnlKhuyenMaiBox.Controls.Add(txtMaKhuyenMai);
            pnlKhuyenMaiBox.Controls.Add(btnApDungKhuyenMai);
            pnlKhuyenMaiBox.Dock = DockStyle.Fill;
            pnlKhuyenMaiBox.Location = new Point(140, 90);
            pnlKhuyenMaiBox.Margin = new Padding(3, 4, 3, 4);
            pnlKhuyenMaiBox.Name = "pnlKhuyenMaiBox";
            pnlKhuyenMaiBox.Size = new Size(321, 43);
            pnlKhuyenMaiBox.TabIndex = 5;
            // 
            // txtMaKhuyenMai
            // 
            txtMaKhuyenMai.CharacterCasing = CharacterCasing.Upper;
            txtMaKhuyenMai.Font = new Font("Segoe UI", 9.5F);
            txtMaKhuyenMai.Location = new Point(7, 8);
            txtMaKhuyenMai.Margin = new Padding(3, 4, 3, 4);
            txtMaKhuyenMai.Name = "txtMaKhuyenMai";
            txtMaKhuyenMai.PlaceholderText = "Mã KM (VD: TET2026)";
            txtMaKhuyenMai.Size = new Size(198, 29);
            txtMaKhuyenMai.TabIndex = 0;
            // 
            // btnApDungKhuyenMai
            // 
            btnApDungKhuyenMai.BackColor = Color.FromArgb(241, 245, 249);
            btnApDungKhuyenMai.Cursor = Cursors.Hand;
            btnApDungKhuyenMai.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnApDungKhuyenMai.FlatStyle = FlatStyle.Flat;
            btnApDungKhuyenMai.Font = new Font("Segoe UI", 8.5F);
            btnApDungKhuyenMai.Location = new Point(211, 4);
            btnApDungKhuyenMai.Margin = new Padding(3, 4, 3, 4);
            btnApDungKhuyenMai.Name = "btnApDungKhuyenMai";
            btnApDungKhuyenMai.Size = new Size(103, 35);
            btnApDungKhuyenMai.TabIndex = 1;
            btnApDungKhuyenMai.Text = "Áp dụng";
            btnApDungKhuyenMai.UseVisualStyleBackColor = false;
            // 
            // lblTienGiamTitle
            // 
            lblTienGiamTitle.Anchor = AnchorStyles.Left;
            lblTienGiamTitle.AutoSize = true;
            lblTienGiamTitle.Font = new Font("Segoe UI", 9F);
            lblTienGiamTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblTienGiamTitle.Location = new Point(3, 148);
            lblTienGiamTitle.Name = "lblTienGiamTitle";
            lblTienGiamTitle.Size = new Size(70, 20);
            lblTienGiamTitle.TabIndex = 6;
            lblTienGiamTitle.Text = "Giảm trừ:";
            // 
            // lblTongTienTitle
            // 
            lblTongTienTitle.Anchor = AnchorStyles.Left;
            lblTongTienTitle.AutoSize = true;
            lblTongTienTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTongTienTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTongTienTitle.Location = new Point(3, 199);
            lblTongTienTitle.Name = "lblTongTienTitle";
            lblTongTienTitle.Size = new Size(105, 23);
            lblTongTienTitle.TabIndex = 8;
            lblTongTienTitle.Text = "TỔNG TIỀN:";
            // 
            // lblTongTien
            // 
            lblTongTien.Anchor = AnchorStyles.Left;
            lblTongTien.AutoSize = true;
            lblTongTien.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.FromArgb(220, 38, 38);
            lblTongTien.Location = new Point(140, 195);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(83, 31);
            lblTongTien.TabIndex = 9;
            lblTongTien.Text = "0 VNĐ";
            // 
            // lblTienGiam
            // 
            lblTienGiam.Anchor = AnchorStyles.Left;
            lblTienGiam.AutoSize = true;
            lblTienGiam.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblTienGiam.ForeColor = Color.FromArgb(16, 185, 129);
            lblTienGiam.Location = new Point(140, 148);
            lblTienGiam.Name = "lblTienGiam";
            lblTienGiam.Size = new Size(62, 21);
            lblTienGiam.TabIndex = 7;
            lblTienGiam.Text = "-0 VNĐ";
            // 
            // grpThongTinKhach
            // 
            grpThongTinKhach.BackColor = Color.White;
            grpThongTinKhach.Controls.Add(tlpKhachHang);
            grpThongTinKhach.Dock = DockStyle.Top;
            grpThongTinKhach.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            grpThongTinKhach.ForeColor = Color.FromArgb(15, 23, 42);
            grpThongTinKhach.Location = new Point(14, 291);
            grpThongTinKhach.Margin = new Padding(3, 4, 3, 4);
            grpThongTinKhach.Name = "grpThongTinKhach";
            grpThongTinKhach.Padding = new Padding(11);
            grpThongTinKhach.Size = new Size(486, 220);
            grpThongTinKhach.TabIndex = 1;
            grpThongTinKhach.TabStop = false;
            grpThongTinKhach.Text = "2. THÔNG TIN HÀNH KHÁCH";
            // 
            // tlpKhachHang
            // 
            tlpKhachHang.ColumnCount = 2;
            tlpKhachHang.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tlpKhachHang.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpKhachHang.Controls.Add(lblTenKhachHang, 0, 0);
            tlpKhachHang.Controls.Add(txtTenKhachHang, 1, 0);
            tlpKhachHang.Controls.Add(lblSoDienThoai, 0, 1);
            tlpKhachHang.Controls.Add(txtSoDienThoai, 1, 1);
            tlpKhachHang.Controls.Add(lblGhiChu, 0, 2);
            tlpKhachHang.Controls.Add(txtGhiChu, 1, 2);
            tlpKhachHang.Dock = DockStyle.Fill;
            tlpKhachHang.Location = new Point(11, 33);
            tlpKhachHang.Margin = new Padding(3, 4, 3, 4);
            tlpKhachHang.Name = "tlpKhachHang";
            tlpKhachHang.RowCount = 3;
            tlpKhachHang.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlpKhachHang.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlpKhachHang.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlpKhachHang.Size = new Size(464, 176);
            tlpKhachHang.TabIndex = 0;
            // 
            // lblTenKhachHang
            // 
            lblTenKhachHang.Anchor = AnchorStyles.Left;
            lblTenKhachHang.AutoSize = true;
            lblTenKhachHang.Font = new Font("Segoe UI", 9F);
            lblTenKhachHang.ForeColor = Color.FromArgb(71, 85, 105);
            lblTenKhachHang.Location = new Point(3, 18);
            lblTenKhachHang.Name = "lblTenKhachHang";
            lblTenKhachHang.Size = new Size(96, 20);
            lblTenKhachHang.TabIndex = 0;
            lblTenKhachHang.Text = "Họ và tên (*):";
            // 
            // txtTenKhachHang
            // 
            txtTenKhachHang.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtTenKhachHang.Font = new Font("Segoe UI", 9.5F);
            txtTenKhachHang.Location = new Point(123, 13);
            txtTenKhachHang.Margin = new Padding(3, 4, 3, 4);
            txtTenKhachHang.Name = "txtTenKhachHang";
            txtTenKhachHang.PlaceholderText = "Nhập họ tên hành khách";
            txtTenKhachHang.Size = new Size(338, 29);
            txtTenKhachHang.TabIndex = 1;
            // 
            // lblSoDienThoai
            // 
            lblSoDienThoai.Anchor = AnchorStyles.Left;
            lblSoDienThoai.AutoSize = true;
            lblSoDienThoai.Font = new Font("Segoe UI", 9F);
            lblSoDienThoai.ForeColor = Color.FromArgb(71, 85, 105);
            lblSoDienThoai.Location = new Point(3, 64);
            lblSoDienThoai.Name = "lblSoDienThoai";
            lblSoDienThoai.Size = new Size(101, 40);
            lblSoDienThoai.TabIndex = 2;
            lblSoDienThoai.Text = "Số điện thoại (*):";
            // 
            // txtSoDienThoai
            // 
            txtSoDienThoai.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtSoDienThoai.Font = new Font("Segoe UI", 9.5F);
            txtSoDienThoai.Location = new Point(123, 69);
            txtSoDienThoai.Margin = new Padding(3, 4, 3, 4);
            txtSoDienThoai.MaxLength = 10;
            txtSoDienThoai.Name = "txtSoDienThoai";
            txtSoDienThoai.PlaceholderText = "10 chữ số (VD: 0912345678)";
            txtSoDienThoai.Size = new Size(338, 29);
            txtSoDienThoai.TabIndex = 3;
            // 
            // lblGhiChu
            // 
            lblGhiChu.Anchor = AnchorStyles.Left;
            lblGhiChu.AutoSize = true;
            lblGhiChu.Font = new Font("Segoe UI", 9F);
            lblGhiChu.ForeColor = Color.FromArgb(71, 85, 105);
            lblGhiChu.Location = new Point(3, 134);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(61, 20);
            lblGhiChu.TabIndex = 4;
            lblGhiChu.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            txtGhiChu.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtGhiChu.Font = new Font("Segoe UI", 9.5F);
            txtGhiChu.Location = new Point(123, 129);
            txtGhiChu.Margin = new Padding(3, 4, 3, 4);
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.PlaceholderText = "Điểm đón, yêu cầu đặc biệt...";
            txtGhiChu.Size = new Size(338, 29);
            txtGhiChu.TabIndex = 5;
            // 
            // grpTimChuyen
            // 
            grpTimChuyen.BackColor = Color.White;
            grpTimChuyen.Controls.Add(tlpTimChuyen);
            grpTimChuyen.Dock = DockStyle.Top;
            grpTimChuyen.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            grpTimChuyen.ForeColor = Color.FromArgb(15, 23, 42);
            grpTimChuyen.Location = new Point(14, 16);
            grpTimChuyen.Margin = new Padding(3, 4, 3, 4);
            grpTimChuyen.Name = "grpTimChuyen";
            grpTimChuyen.Padding = new Padding(11);
            grpTimChuyen.Size = new Size(486, 275);
            grpTimChuyen.TabIndex = 0;
            grpTimChuyen.TabStop = false;
            grpTimChuyen.Text = "1. CHỌN CHUYẾN XE";
            // 
            // tlpTimChuyen
            // 
            tlpTimChuyen.ColumnCount = 2;
            tlpTimChuyen.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tlpTimChuyen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTimChuyen.Controls.Add(lblTuyenXe, 0, 0);
            tlpTimChuyen.Controls.Add(cboTuyenXe, 1, 0);
            tlpTimChuyen.Controls.Add(lblNgayDi, 0, 1);
            tlpTimChuyen.Controls.Add(dtpNgayDi, 1, 1);
            tlpTimChuyen.Controls.Add(lblChuyenXe, 0, 2);
            tlpTimChuyen.Controls.Add(cboChuyenXe, 1, 2);
            tlpTimChuyen.Controls.Add(btnTaiSoDo, 1, 3);
            tlpTimChuyen.Dock = DockStyle.Fill;
            tlpTimChuyen.Location = new Point(11, 33);
            tlpTimChuyen.Margin = new Padding(3, 4, 3, 4);
            tlpTimChuyen.Name = "tlpTimChuyen";
            tlpTimChuyen.RowCount = 4;
            tlpTimChuyen.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlpTimChuyen.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlpTimChuyen.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlpTimChuyen.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tlpTimChuyen.Size = new Size(464, 231);
            tlpTimChuyen.TabIndex = 0;
            // 
            // lblTuyenXe
            // 
            lblTuyenXe.Anchor = AnchorStyles.Left;
            lblTuyenXe.AutoSize = true;
            lblTuyenXe.Font = new Font("Segoe UI", 9F);
            lblTuyenXe.ForeColor = Color.FromArgb(71, 85, 105);
            lblTuyenXe.Location = new Point(3, 18);
            lblTuyenXe.Name = "lblTuyenXe";
            lblTuyenXe.Size = new Size(70, 20);
            lblTuyenXe.TabIndex = 0;
            lblTuyenXe.Text = "Tuyến xe:";
            // 
            // cboTuyenXe
            // 
            cboTuyenXe.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cboTuyenXe.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTuyenXe.Font = new Font("Segoe UI", 9.5F);
            cboTuyenXe.FormattingEnabled = true;
            cboTuyenXe.Location = new Point(123, 13);
            cboTuyenXe.Margin = new Padding(3, 4, 3, 4);
            cboTuyenXe.Name = "cboTuyenXe";
            cboTuyenXe.Size = new Size(338, 29);
            cboTuyenXe.TabIndex = 1;
            // 
            // lblNgayDi
            // 
            lblNgayDi.Anchor = AnchorStyles.Left;
            lblNgayDi.AutoSize = true;
            lblNgayDi.Font = new Font("Segoe UI", 9F);
            lblNgayDi.ForeColor = Color.FromArgb(71, 85, 105);
            lblNgayDi.Location = new Point(3, 74);
            lblNgayDi.Name = "lblNgayDi";
            lblNgayDi.Size = new Size(64, 20);
            lblNgayDi.TabIndex = 2;
            lblNgayDi.Text = "Ngày đi:";
            // 
            // dtpNgayDi
            // 
            dtpNgayDi.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpNgayDi.CustomFormat = "dd/MM/yyyy";
            dtpNgayDi.Font = new Font("Segoe UI", 9.5F);
            dtpNgayDi.Format = DateTimePickerFormat.Custom;
            dtpNgayDi.Location = new Point(123, 69);
            dtpNgayDi.Margin = new Padding(3, 4, 3, 4);
            dtpNgayDi.Name = "dtpNgayDi";
            dtpNgayDi.Size = new Size(338, 29);
            dtpNgayDi.TabIndex = 3;
            // 
            // lblChuyenXe
            // 
            lblChuyenXe.Anchor = AnchorStyles.Left;
            lblChuyenXe.AutoSize = true;
            lblChuyenXe.Font = new Font("Segoe UI", 9F);
            lblChuyenXe.ForeColor = Color.FromArgb(71, 85, 105);
            lblChuyenXe.Location = new Point(3, 130);
            lblChuyenXe.Name = "lblChuyenXe";
            lblChuyenXe.Size = new Size(72, 20);
            lblChuyenXe.TabIndex = 4;
            lblChuyenXe.Text = "Xe && Giờ:";
            // 
            // cboChuyenXe
            // 
            cboChuyenXe.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cboChuyenXe.DropDownStyle = ComboBoxStyle.DropDownList;
            cboChuyenXe.Font = new Font("Segoe UI", 9.5F);
            cboChuyenXe.FormattingEnabled = true;
            cboChuyenXe.Location = new Point(123, 125);
            cboChuyenXe.Margin = new Padding(3, 4, 3, 4);
            cboChuyenXe.Name = "cboChuyenXe";
            cboChuyenXe.Size = new Size(338, 29);
            cboChuyenXe.TabIndex = 5;
            // 
            // btnTaiSoDo
            // 
            btnTaiSoDo.BackColor = Color.FromArgb(241, 245, 249);
            btnTaiSoDo.Cursor = Cursors.Hand;
            btnTaiSoDo.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnTaiSoDo.FlatStyle = FlatStyle.Flat;
            btnTaiSoDo.Font = new Font("Segoe UI", 9F);
            btnTaiSoDo.ForeColor = Color.FromArgb(15, 23, 42);
            btnTaiSoDo.IconChar = FontAwesome.Sharp.IconChar.RotateBack;
            btnTaiSoDo.IconColor = Color.FromArgb(37, 99, 235);
            btnTaiSoDo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnTaiSoDo.IconSize = 16;
            btnTaiSoDo.ImageAlign = ContentAlignment.MiddleLeft;
            btnTaiSoDo.Location = new Point(123, 172);
            btnTaiSoDo.Margin = new Padding(3, 4, 3, 4);
            btnTaiSoDo.Name = "btnTaiSoDo";
            btnTaiSoDo.Size = new Size(338, 55);
            btnTaiSoDo.TabIndex = 6;
            btnTaiSoDo.Text = "  Tải lại sơ đồ ghế";
            btnTaiSoDo.UseVisualStyleBackColor = false;
            // 
            // pnlRight
            // 
            pnlRight.BackColor = Color.White;
            pnlRight.Controls.Add(seatMapControl);
            pnlRight.Controls.Add(pnlLegend);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Location = new Point(0, 0);
            pnlRight.Margin = new Padding(3, 4, 3, 4);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(14, 16, 14, 16);
            pnlRight.Size = new Size(943, 1002);
            pnlRight.TabIndex = 0;
            // 
            // seatMapControl
            // 
            seatMapControl.AutoScroll = true;
            seatMapControl.BackColor = Color.FromArgb(248, 250, 252);
            seatMapControl.Dock = DockStyle.Fill;
            seatMapControl.Location = new Point(14, 77);
            seatMapControl.Margin = new Padding(3, 4, 3, 4);
            seatMapControl.Name = "seatMapControl";
            seatMapControl.Size = new Size(915, 909);
            seatMapControl.TabIndex = 1;
            // 
            // pnlLegend
            // 
            pnlLegend.BackColor = Color.White;
            pnlLegend.Controls.Add(flpLegend);
            pnlLegend.Dock = DockStyle.Top;
            pnlLegend.Location = new Point(14, 16);
            pnlLegend.Margin = new Padding(3, 4, 3, 4);
            pnlLegend.Name = "pnlLegend";
            pnlLegend.Padding = new Padding(9, 8, 9, 11);
            pnlLegend.Size = new Size(915, 61);
            pnlLegend.TabIndex = 0;
            // 
            // flpLegend
            // 
            flpLegend.Controls.Add(pnlLegendAvailColor);
            flpLegend.Controls.Add(lblLegendAvail);
            flpLegend.Controls.Add(pnlLegendSelColor);
            flpLegend.Controls.Add(lblLegendSel);
            flpLegend.Controls.Add(pnlLegendBookedColor);
            flpLegend.Controls.Add(lblLegendBooked);
            flpLegend.Controls.Add(lblLegendSteering);
            flpLegend.Dock = DockStyle.Fill;
            flpLegend.Location = new Point(9, 8);
            flpLegend.Margin = new Padding(3, 4, 3, 4);
            flpLegend.Name = "flpLegend";
            flpLegend.Size = new Size(897, 42);
            flpLegend.TabIndex = 0;
            // 
            // pnlLegendAvailColor
            // 
            pnlLegendAvailColor.BackColor = Color.FromArgb(224, 242, 254);
            pnlLegendAvailColor.BorderStyle = BorderStyle.FixedSingle;
            pnlLegendAvailColor.Location = new Point(3, 5);
            pnlLegendAvailColor.Margin = new Padding(3, 5, 3, 4);
            pnlLegendAvailColor.Name = "pnlLegendAvailColor";
            pnlLegendAvailColor.Size = new Size(23, 26);
            pnlLegendAvailColor.TabIndex = 0;
            // 
            // lblLegendAvail
            // 
            lblLegendAvail.AutoSize = true;
            lblLegendAvail.Font = new Font("Segoe UI", 9F);
            lblLegendAvail.ForeColor = Color.FromArgb(15, 23, 42);
            lblLegendAvail.Location = new Point(32, 8);
            lblLegendAvail.Margin = new Padding(3, 8, 21, 0);
            lblLegendAvail.Name = "lblLegendAvail";
            lblLegendAvail.Size = new Size(75, 20);
            lblLegendAvail.TabIndex = 1;
            lblLegendAvail.Text = "Còn trống";
            // 
            // pnlLegendSelColor
            // 
            pnlLegendSelColor.BackColor = Color.FromArgb(254, 243, 199);
            pnlLegendSelColor.BorderStyle = BorderStyle.FixedSingle;
            pnlLegendSelColor.Location = new Point(131, 5);
            pnlLegendSelColor.Margin = new Padding(3, 5, 3, 4);
            pnlLegendSelColor.Name = "pnlLegendSelColor";
            pnlLegendSelColor.Size = new Size(23, 26);
            pnlLegendSelColor.TabIndex = 2;
            // 
            // lblLegendSel
            // 
            lblLegendSel.AutoSize = true;
            lblLegendSel.Font = new Font("Segoe UI", 9F);
            lblLegendSel.ForeColor = Color.FromArgb(15, 23, 42);
            lblLegendSel.Location = new Point(160, 8);
            lblLegendSel.Margin = new Padding(3, 8, 21, 0);
            lblLegendSel.Name = "lblLegendSel";
            lblLegendSel.Size = new Size(81, 20);
            lblLegendSel.TabIndex = 3;
            lblLegendSel.Text = "Đang chọn";
            // 
            // pnlLegendBookedColor
            // 
            pnlLegendBookedColor.BackColor = Color.FromArgb(226, 232, 240);
            pnlLegendBookedColor.BorderStyle = BorderStyle.FixedSingle;
            pnlLegendBookedColor.Location = new Point(265, 5);
            pnlLegendBookedColor.Margin = new Padding(3, 5, 3, 4);
            pnlLegendBookedColor.Name = "pnlLegendBookedColor";
            pnlLegendBookedColor.Size = new Size(23, 26);
            pnlLegendBookedColor.TabIndex = 4;
            // 
            // lblLegendBooked
            // 
            lblLegendBooked.AutoSize = true;
            lblLegendBooked.Font = new Font("Segoe UI", 9F);
            lblLegendBooked.ForeColor = Color.FromArgb(15, 23, 42);
            lblLegendBooked.Location = new Point(294, 8);
            lblLegendBooked.Margin = new Padding(3, 8, 21, 0);
            lblLegendBooked.Name = "lblLegendBooked";
            lblLegendBooked.Size = new Size(57, 20);
            lblLegendBooked.TabIndex = 5;
            lblLegendBooked.Text = "Đã bán";
            // 
            // lblLegendSteering
            // 
            lblLegendSteering.AutoSize = true;
            lblLegendSteering.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblLegendSteering.ForeColor = Color.FromArgb(100, 116, 139);
            lblLegendSteering.Location = new Point(375, 8);
            lblLegendSteering.Margin = new Padding(3, 8, 3, 0);
            lblLegendSteering.Name = "lblLegendSteering";
            lblLegendSteering.Size = new Size(183, 20);
            lblLegendSteering.TabIndex = 6;
            lblLegendSteering.Text = "⎈ Vô lăng = Hướng đầu xe";
            // 
            // btnXuatVe
            // 
            btnXuatVe.BackColor = Color.FromArgb(37, 99, 235);
            btnXuatVe.Cursor = Cursors.Hand;
            btnXuatVe.FlatAppearance.BorderSize = 0;
            btnXuatVe.FlatStyle = FlatStyle.Flat;
            btnXuatVe.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnXuatVe.ForeColor = Color.White;
            btnXuatVe.IconChar = FontAwesome.Sharp.IconChar.Print;
            btnXuatVe.IconColor = Color.White;
            btnXuatVe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnXuatVe.IconSize = 20;
            btnXuatVe.ImageAlign = ContentAlignment.MiddleLeft;
            btnXuatVe.Location = new Point(243, 15);
            btnXuatVe.Margin = new Padding(3, 4, 3, 4);
            btnXuatVe.Name = "btnXuatVe";
            btnXuatVe.Size = new Size(222, 53);
            btnXuatVe.TabIndex = 1;
            btnXuatVe.Text = " IN & XUẤT VÉ (Ctrl+P)";
            btnXuatVe.UseVisualStyleBackColor = false;
            // 
            // errorProviderMain
            // 
            errorProviderMain.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProviderMain.ContainerControl = this;
            // 
            // timerClock
            // 
            timerClock.Enabled = true;
            timerClock.Interval = 1000;
            // 
            // FormMain_KhachHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(1463, 1067);
            Controls.Add(splitMain);
            Controls.Add(ssStatus);
            Controls.Add(msMain);
            Font = new Font("Segoe UI", 9F);
            MainMenuStrip = msMain;
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1168, 891);
            Name = "FormMain_KhachHang";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ Thống Đặt Vé & Bán Vé Xe Khách - Quản Lý Vận Tải";
            msMain.ResumeLayout(false);
            msMain.PerformLayout();
            ssStatus.ResumeLayout(false);
            ssStatus.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            pnlLeftScroll.ResumeLayout(false);
            flpButtons.ResumeLayout(false);
            grpThanhToan.ResumeLayout(false);
            tlpThanhToan.ResumeLayout(false);
            tlpThanhToan.PerformLayout();
            pnlKhuyenMaiBox.ResumeLayout(false);
            pnlKhuyenMaiBox.PerformLayout();
            grpThongTinKhach.ResumeLayout(false);
            tlpKhachHang.ResumeLayout(false);
            tlpKhachHang.PerformLayout();
            grpTimChuyen.ResumeLayout(false);
            tlpTimChuyen.ResumeLayout(false);
            tlpTimChuyen.PerformLayout();
            pnlRight.ResumeLayout(false);
            pnlLegend.ResumeLayout(false);
            flpLegend.ResumeLayout(false);
            flpLegend.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProviderMain).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip msMain;
        private ToolStripMenuItem mnuHeThong;
        private ToolStripMenuItem mnuDangXuat;
        private ToolStripMenuItem mnuDoiMatKhau;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem mnuThoat;
        private ToolStripMenuItem mnuVeCuaToi;
        private ToolStripMenuItem mnuTraCuuVe;
        private ToolStripMenuItem mnuLichSuDatVe;
        private ToolStripMenuItem mnuTroGiup;
        private ToolStripMenuItem mnuHuongDan;
        private ToolStripMenuItem mnuThongTin;
        private StatusStrip ssStatus;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblCurrentUser;
        private ToolStripProgressBar prgAction;
        private ToolStripStatusLabel lblClock;
        private SplitContainer splitMain;
        private Panel pnlLeftScroll;
        private GroupBox grpTimChuyen;
        private TableLayoutPanel tlpTimChuyen;
        private Label lblTuyenXe;
        private ComboBox cboTuyenXe;
        private Label lblNgayDi;
        private DateTimePicker dtpNgayDi;
        private Label lblChuyenXe;
        private ComboBox cboChuyenXe;
        private FontAwesome.Sharp.IconButton btnTaiSoDo;
        private GroupBox grpThongTinKhach;
        private TableLayoutPanel tlpKhachHang;
        private Label lblTenKhachHang;
        private TextBox txtTenKhachHang;
        private Label lblSoDienThoai;
        private TextBox txtSoDienThoai;
        private Label lblGhiChu;
        private TextBox txtGhiChu;
        private GroupBox grpThanhToan;
        private TableLayoutPanel tlpThanhToan;
        private Label lblDanhSachGheTitle;
        private Label lblDanhSachGhe;
        private Label lblDonGiaTitle;
        private Label lblDonGia;
        private Label lblMaKhuyenMai;
        private Panel pnlKhuyenMaiBox;
        private TextBox txtMaKhuyenMai;
        private Button btnApDungKhuyenMai;
        private Label lblTienGiamTitle;
        private Label lblTienGiam;
        private Label lblTongTienTitle;
        private Label lblTongTien;
        private FlowLayoutPanel flpButtons;
        private FontAwesome.Sharp.IconButton btnDatVe;
        private FontAwesome.Sharp.IconButton btnXuatVe;
        private FontAwesome.Sharp.IconButton btnHuyChon;
        private Panel pnlRight;
        private Components.SeatMapControl seatMapControl;
        private Panel pnlLegend;
        private FlowLayoutPanel flpLegend;
        private Panel pnlLegendAvailColor;
        private Label lblLegendAvail;
        private Panel pnlLegendSelColor;
        private Label lblLegendSel;
        private Panel pnlLegendBookedColor;
        private Label lblLegendBooked;
        private Label lblLegendSteering;
        private ErrorProvider errorProviderMain;
        private System.Windows.Forms.Timer timerClock;
    }
}
