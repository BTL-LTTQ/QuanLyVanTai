namespace QuanLyVanTai.UI.UserControls
{
    partial class UcCustomerBooking
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
            splitMain = new SplitContainer();
            pnlLeftScroll = new Panel();
            flpButtons = new FlowLayoutPanel();
            btnDatVe = new FontAwesome.Sharp.IconButton();
            btnHuyChon = new FontAwesome.Sharp.IconButton();
            btnXemLichSu = new FontAwesome.Sharp.IconButton();
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
            lblTienGiam = new Label();
            lblTongTienTitle = new Label();
            lblTongTien = new Label();
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
            errorProviderMain = new ErrorProvider(components);
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
            splitMain.Panel1.Controls.Add(pnlLeftScroll);
            splitMain.Panel1MinSize = 420;
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.BackColor = Color.FromArgb(241, 245, 249);
            splitMain.Panel2.Controls.Add(pnlRight);
            splitMain.Panel2MinSize = 500;
            splitMain.Size = new Size(1300, 850);
            splitMain.SplitterDistance = 480;
            splitMain.SplitterWidth = 6;
            splitMain.TabIndex = 0;
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
            pnlLeftScroll.Size = new Size(480, 850);
            pnlLeftScroll.TabIndex = 0;
            // 
            // flpButtons
            // 
            flpButtons.Controls.Add(btnDatVe);
            flpButtons.Controls.Add(btnHuyChon);
            flpButtons.Controls.Add(btnXemLichSu);
            flpButtons.Dock = DockStyle.Top;
            flpButtons.Location = new Point(14, 690);
            flpButtons.Margin = new Padding(3, 4, 3, 4);
            flpButtons.Name = "flpButtons";
            flpButtons.Padding = new Padding(0, 10, 0, 0);
            flpButtons.Size = new Size(452, 120);
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
            btnDatVe.IconSize = 22;
            btnDatVe.ImageAlign = ContentAlignment.MiddleLeft;
            btnDatVe.Location = new Point(3, 14);
            btnDatVe.Margin = new Padding(3, 4, 8, 4);
            btnDatVe.Name = "btnDatVe";
            btnDatVe.Padding = new Padding(12, 0, 16, 0);
            btnDatVe.Size = new Size(190, 42);
            btnDatVe.TabIndex = 0;
            btnDatVe.Text = "  Xác Nhận Đặt Vé";
            btnDatVe.TextAlign = ContentAlignment.MiddleRight;
            btnDatVe.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDatVe.UseVisualStyleBackColor = false;
            // 
            // btnHuyChon
            // 
            btnHuyChon.BackColor = Color.FromArgb(241, 245, 249);
            btnHuyChon.Cursor = Cursors.Hand;
            btnHuyChon.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnHuyChon.FlatStyle = FlatStyle.Flat;
            btnHuyChon.Font = new Font("Segoe UI", 9F);
            btnHuyChon.ForeColor = Color.FromArgb(71, 85, 105);
            btnHuyChon.IconChar = FontAwesome.Sharp.IconChar.RotateBack;
            btnHuyChon.IconColor = Color.FromArgb(71, 85, 105);
            btnHuyChon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnHuyChon.IconSize = 18;
            btnHuyChon.ImageAlign = ContentAlignment.MiddleLeft;
            btnHuyChon.Location = new Point(204, 14);
            btnHuyChon.Margin = new Padding(3, 4, 8, 4);
            btnHuyChon.Name = "btnHuyChon";
            btnHuyChon.Padding = new Padding(8, 0, 10, 0);
            btnHuyChon.Size = new Size(110, 42);
            btnHuyChon.TabIndex = 1;
            btnHuyChon.Text = " Bỏ chọn";
            btnHuyChon.TextAlign = ContentAlignment.MiddleRight;
            btnHuyChon.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnHuyChon.UseVisualStyleBackColor = false;
            // 
            // btnXemLichSu
            // 
            btnXemLichSu.BackColor = Color.FromArgb(238, 242, 255);
            btnXemLichSu.Cursor = Cursors.Hand;
            btnXemLichSu.FlatAppearance.BorderColor = Color.FromArgb(199, 210, 254);
            btnXemLichSu.FlatStyle = FlatStyle.Flat;
            btnXemLichSu.Font = new Font("Segoe UI", 9F);
            btnXemLichSu.ForeColor = Color.FromArgb(67, 56, 202);
            btnXemLichSu.IconChar = FontAwesome.Sharp.IconChar.ClockRotateLeft;
            btnXemLichSu.IconColor = Color.FromArgb(67, 56, 202);
            btnXemLichSu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnXemLichSu.IconSize = 18;
            btnXemLichSu.ImageAlign = ContentAlignment.MiddleLeft;
            btnXemLichSu.Location = new Point(3, 64);
            btnXemLichSu.Margin = new Padding(3, 4, 8, 4);
            btnXemLichSu.Name = "btnXemLichSu";
            btnXemLichSu.Padding = new Padding(10, 0, 12, 0);
            btnXemLichSu.Size = new Size(190, 38);
            btnXemLichSu.TabIndex = 2;
            btnXemLichSu.Text = "  Lịch sử vé của tôi";
            btnXemLichSu.TextAlign = ContentAlignment.MiddleRight;
            btnXemLichSu.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnXemLichSu.UseVisualStyleBackColor = false;
            // 
            // grpThanhToan
            // 
            grpThanhToan.BackColor = Color.White;
            grpThanhToan.Controls.Add(tlpThanhToan);
            grpThanhToan.Dock = DockStyle.Top;
            grpThanhToan.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            grpThanhToan.ForeColor = Color.FromArgb(15, 23, 42);
            grpThanhToan.Location = new Point(14, 440);
            grpThanhToan.Margin = new Padding(3, 4, 3, 10);
            grpThanhToan.Name = "grpThanhToan";
            grpThanhToan.Padding = new Padding(12);
            grpThanhToan.Size = new Size(452, 250);
            grpThanhToan.TabIndex = 2;
            grpThanhToan.TabStop = false;
            grpThanhToan.Text = " 3. Chi Tiết Thanh Toán ";
            // 
            // tlpThanhToan
            // 
            tlpThanhToan.ColumnCount = 2;
            tlpThanhToan.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tlpThanhToan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpThanhToan.Controls.Add(lblDanhSachGheTitle, 0, 0);
            tlpThanhToan.Controls.Add(lblDanhSachGhe, 1, 0);
            tlpThanhToan.Controls.Add(lblDonGiaTitle, 0, 1);
            tlpThanhToan.Controls.Add(lblDonGia, 1, 1);
            tlpThanhToan.Controls.Add(lblMaKhuyenMai, 0, 2);
            tlpThanhToan.Controls.Add(pnlKhuyenMaiBox, 1, 2);
            tlpThanhToan.Controls.Add(lblTienGiamTitle, 0, 3);
            tlpThanhToan.Controls.Add(lblTienGiam, 1, 3);
            tlpThanhToan.Controls.Add(lblTongTienTitle, 0, 4);
            tlpThanhToan.Controls.Add(lblTongTien, 1, 4);
            tlpThanhToan.Dock = DockStyle.Fill;
            tlpThanhToan.Location = new Point(12, 34);
            tlpThanhToan.Name = "tlpThanhToan";
            tlpThanhToan.RowCount = 5;
            tlpThanhToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpThanhToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpThanhToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tlpThanhToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpThanhToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpThanhToan.Size = new Size(428, 204);
            tlpThanhToan.TabIndex = 0;
            // 
            // lblDanhSachGheTitle
            // 
            lblDanhSachGheTitle.Anchor = AnchorStyles.Left;
            lblDanhSachGheTitle.AutoSize = true;
            lblDanhSachGheTitle.Font = new Font("Segoe UI", 9F);
            lblDanhSachGheTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblDanhSachGheTitle.Location = new Point(3, 8);
            lblDanhSachGheTitle.Name = "lblDanhSachGheTitle";
            lblDanhSachGheTitle.Size = new Size(74, 20);
            lblDanhSachGheTitle.TabIndex = 0;
            lblDanhSachGheTitle.Text = "Ghế chọn:";
            // 
            // lblDanhSachGhe
            // 
            lblDanhSachGhe.Anchor = AnchorStyles.Left;
            lblDanhSachGhe.AutoSize = true;
            lblDanhSachGhe.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblDanhSachGhe.ForeColor = Color.FromArgb(37, 99, 235);
            lblDanhSachGhe.Location = new Point(133, 7);
            lblDanhSachGhe.Name = "lblDanhSachGhe";
            lblDanhSachGhe.Size = new Size(119, 21);
            lblDanhSachGhe.TabIndex = 1;
            lblDanhSachGhe.Text = "Chưa chọn ghế";
            // 
            // lblDonGiaTitle
            // 
            lblDonGiaTitle.Anchor = AnchorStyles.Left;
            lblDonGiaTitle.AutoSize = true;
            lblDonGiaTitle.Font = new Font("Segoe UI", 9F);
            lblDonGiaTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblDonGiaTitle.Location = new Point(3, 44);
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
            lblDonGia.ForeColor = Color.FromArgb(30, 41, 59);
            lblDonGia.Location = new Point(133, 43);
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
            lblMaKhuyenMai.Location = new Point(3, 83);
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
            pnlKhuyenMaiBox.Location = new Point(133, 75);
            pnlKhuyenMaiBox.Name = "pnlKhuyenMaiBox";
            pnlKhuyenMaiBox.Size = new Size(292, 36);
            pnlKhuyenMaiBox.TabIndex = 5;
            // 
            // txtMaKhuyenMai
            // 
            txtMaKhuyenMai.CharacterCasing = CharacterCasing.Upper;
            txtMaKhuyenMai.Dock = DockStyle.Fill;
            txtMaKhuyenMai.Font = new Font("Segoe UI", 9F);
            txtMaKhuyenMai.Location = new Point(0, 0);
            txtMaKhuyenMai.Name = "txtMaKhuyenMai";
            txtMaKhuyenMai.PlaceholderText = "Nhập mã KM...";
            txtMaKhuyenMai.Size = new Size(207, 27);
            txtMaKhuyenMai.TabIndex = 0;
            // 
            // btnApDungKhuyenMai
            // 
            btnApDungKhuyenMai.BackColor = Color.FromArgb(37, 99, 235);
            btnApDungKhuyenMai.Cursor = Cursors.Hand;
            btnApDungKhuyenMai.Dock = DockStyle.Right;
            btnApDungKhuyenMai.FlatAppearance.BorderSize = 0;
            btnApDungKhuyenMai.FlatStyle = FlatStyle.Flat;
            btnApDungKhuyenMai.Font = new Font("Segoe UI", 8.5F);
            btnApDungKhuyenMai.ForeColor = Color.White;
            btnApDungKhuyenMai.Location = new Point(207, 0);
            btnApDungKhuyenMai.Name = "btnApDungKhuyenMai";
            btnApDungKhuyenMai.Size = new Size(85, 36);
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
            lblTienGiamTitle.Location = new Point(3, 122);
            lblTienGiamTitle.Name = "lblTienGiamTitle";
            lblTienGiamTitle.Size = new Size(72, 20);
            lblTienGiamTitle.TabIndex = 6;
            lblTienGiamTitle.Text = "Giảm giá:";
            // 
            // lblTienGiam
            // 
            lblTienGiam.Anchor = AnchorStyles.Left;
            lblTienGiam.AutoSize = true;
            lblTienGiam.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblTienGiam.ForeColor = Color.FromArgb(220, 38, 38);
            lblTienGiam.Location = new Point(133, 121);
            lblTienGiam.Name = "lblTienGiam";
            lblTienGiam.Size = new Size(62, 21);
            lblTienGiam.TabIndex = 7;
            lblTienGiam.Text = "-0 VNĐ";
            // 
            // lblTongTienTitle
            // 
            lblTongTienTitle.Anchor = AnchorStyles.Left;
            lblTongTienTitle.AutoSize = true;
            lblTongTienTitle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblTongTienTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTongTienTitle.Location = new Point(3, 164);
            lblTongTienTitle.Name = "lblTongTienTitle";
            lblTongTienTitle.Size = new Size(97, 25);
            lblTongTienTitle.TabIndex = 8;
            lblTongTienTitle.Text = "Tổng tiền:";
            // 
            // lblTongTien
            // 
            lblTongTien.Anchor = AnchorStyles.Left;
            lblTongTien.AutoSize = true;
            lblTongTien.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.FromArgb(16, 185, 129);
            lblTongTien.Location = new Point(133, 162);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(80, 30);
            lblTongTien.TabIndex = 9;
            lblTongTien.Text = "0 VNĐ";
            // 
            // grpThongTinKhach
            // 
            grpThongTinKhach.BackColor = Color.White;
            grpThongTinKhach.Controls.Add(tlpKhachHang);
            grpThongTinKhach.Dock = DockStyle.Top;
            grpThongTinKhach.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            grpThongTinKhach.ForeColor = Color.FromArgb(15, 23, 42);
            grpThongTinKhach.Location = new Point(14, 236);
            grpThongTinKhach.Margin = new Padding(3, 4, 3, 10);
            grpThongTinKhach.Name = "grpThongTinKhach";
            grpThongTinKhach.Padding = new Padding(12);
            grpThongTinKhach.Size = new Size(452, 204);
            grpThongTinKhach.TabIndex = 1;
            grpThongTinKhach.TabStop = false;
            grpThongTinKhach.Text = " 2. Thông Tin Khách Hàng ";
            // 
            // tlpKhachHang
            // 
            tlpKhachHang.ColumnCount = 2;
            tlpKhachHang.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tlpKhachHang.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpKhachHang.Controls.Add(lblTenKhachHang, 0, 0);
            tlpKhachHang.Controls.Add(txtTenKhachHang, 1, 0);
            tlpKhachHang.Controls.Add(lblSoDienThoai, 0, 1);
            tlpKhachHang.Controls.Add(txtSoDienThoai, 1, 1);
            tlpKhachHang.Controls.Add(lblGhiChu, 0, 2);
            tlpKhachHang.Controls.Add(txtGhiChu, 1, 2);
            tlpKhachHang.Dock = DockStyle.Fill;
            tlpKhachHang.Location = new Point(12, 34);
            tlpKhachHang.Name = "tlpKhachHang";
            tlpKhachHang.RowCount = 3;
            tlpKhachHang.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            tlpKhachHang.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            tlpKhachHang.RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));
            tlpKhachHang.Size = new Size(428, 158);
            tlpKhachHang.TabIndex = 0;
            // 
            // lblTenKhachHang
            // 
            lblTenKhachHang.Anchor = AnchorStyles.Left;
            lblTenKhachHang.AutoSize = true;
            lblTenKhachHang.Font = new Font("Segoe UI", 9F);
            lblTenKhachHang.ForeColor = Color.FromArgb(71, 85, 105);
            lblTenKhachHang.Location = new Point(3, 13);
            lblTenKhachHang.Name = "lblTenKhachHang";
            lblTenKhachHang.Size = new Size(76, 20);
            lblTenKhachHang.TabIndex = 0;
            lblTenKhachHang.Text = "Họ và tên:";
            // 
            // txtTenKhachHang
            // 
            txtTenKhachHang.Dock = DockStyle.Fill;
            txtTenKhachHang.Font = new Font("Segoe UI", 9F);
            txtTenKhachHang.Location = new Point(133, 8);
            txtTenKhachHang.Margin = new Padding(3, 8, 3, 3);
            txtTenKhachHang.Name = "txtTenKhachHang";
            txtTenKhachHang.PlaceholderText = "Nhập họ tên hành khách...";
            txtTenKhachHang.Size = new Size(292, 27);
            txtTenKhachHang.TabIndex = 1;
            // 
            // lblSoDienThoai
            // 
            lblSoDienThoai.Anchor = AnchorStyles.Left;
            lblSoDienThoai.AutoSize = true;
            lblSoDienThoai.Font = new Font("Segoe UI", 9F);
            lblSoDienThoai.ForeColor = Color.FromArgb(71, 85, 105);
            lblSoDienThoai.Location = new Point(3, 59);
            lblSoDienThoai.Name = "lblSoDienThoai";
            lblSoDienThoai.Size = new Size(100, 20);
            lblSoDienThoai.TabIndex = 2;
            lblSoDienThoai.Text = "Số điện thoại:";
            // 
            // txtSoDienThoai
            // 
            txtSoDienThoai.Dock = DockStyle.Fill;
            txtSoDienThoai.Font = new Font("Segoe UI", 9F);
            txtSoDienThoai.Location = new Point(133, 54);
            txtSoDienThoai.Margin = new Padding(3, 8, 3, 3);
            txtSoDienThoai.MaxLength = 10;
            txtSoDienThoai.Name = "txtSoDienThoai";
            txtSoDienThoai.PlaceholderText = "09xxxxxxxx (10 chữ số)";
            txtSoDienThoai.Size = new Size(292, 27);
            txtSoDienThoai.TabIndex = 3;
            // 
            // lblGhiChu
            // 
            lblGhiChu.Anchor = AnchorStyles.Left;
            lblGhiChu.AutoSize = true;
            lblGhiChu.Font = new Font("Segoe UI", 9F);
            lblGhiChu.ForeColor = Color.FromArgb(71, 85, 105);
            lblGhiChu.Location = new Point(3, 115);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(61, 20);
            lblGhiChu.TabIndex = 4;
            lblGhiChu.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            txtGhiChu.Dock = DockStyle.Fill;
            txtGhiChu.Font = new Font("Segoe UI", 9F);
            txtGhiChu.Location = new Point(133, 100);
            txtGhiChu.Margin = new Padding(3, 8, 3, 3);
            txtGhiChu.Multiline = true;
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.PlaceholderText = "Điểm đón, yêu cầu đặc biệt...";
            txtGhiChu.Size = new Size(292, 55);
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
            grpTimChuyen.Margin = new Padding(3, 4, 3, 10);
            grpTimChuyen.Name = "grpTimChuyen";
            grpTimChuyen.Padding = new Padding(12);
            grpTimChuyen.Size = new Size(452, 220);
            grpTimChuyen.TabIndex = 0;
            grpTimChuyen.TabStop = false;
            grpTimChuyen.Text = " 1. Chọn Tuyến & Chuyến Xe ";
            // 
            // tlpTimChuyen
            // 
            tlpTimChuyen.ColumnCount = 2;
            tlpTimChuyen.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tlpTimChuyen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTimChuyen.Controls.Add(lblTuyenXe, 0, 0);
            tlpTimChuyen.Controls.Add(cboTuyenXe, 1, 0);
            tlpTimChuyen.Controls.Add(lblNgayDi, 0, 1);
            tlpTimChuyen.Controls.Add(dtpNgayDi, 1, 1);
            tlpTimChuyen.Controls.Add(lblChuyenXe, 0, 2);
            tlpTimChuyen.Controls.Add(cboChuyenXe, 1, 2);
            tlpTimChuyen.Controls.Add(btnTaiSoDo, 1, 3);
            tlpTimChuyen.Dock = DockStyle.Fill;
            tlpTimChuyen.Location = new Point(12, 34);
            tlpTimChuyen.Name = "tlpTimChuyen";
            tlpTimChuyen.RowCount = 4;
            tlpTimChuyen.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tlpTimChuyen.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tlpTimChuyen.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tlpTimChuyen.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpTimChuyen.Size = new Size(428, 174);
            tlpTimChuyen.TabIndex = 0;
            // 
            // lblTuyenXe
            // 
            lblTuyenXe.Anchor = AnchorStyles.Left;
            lblTuyenXe.AutoSize = true;
            lblTuyenXe.Font = new Font("Segoe UI", 9F);
            lblTuyenXe.ForeColor = Color.FromArgb(71, 85, 105);
            lblTuyenXe.Location = new Point(3, 11);
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
            cboTuyenXe.Location = new Point(133, 6);
            cboTuyenXe.Margin = new Padding(3, 6, 3, 3);
            cboTuyenXe.Name = "cboTuyenXe";
            cboTuyenXe.Size = new Size(292, 28);
            cboTuyenXe.TabIndex = 1;
            // 
            // lblNgayDi
            // 
            lblNgayDi.Anchor = AnchorStyles.Left;
            lblNgayDi.AutoSize = true;
            lblNgayDi.Font = new Font("Segoe UI", 9F);
            lblNgayDi.ForeColor = Color.FromArgb(71, 85, 105);
            lblNgayDi.Location = new Point(3, 53);
            lblNgayDi.Name = "lblNgayDi";
            lblNgayDi.Size = new Size(64, 20);
            lblNgayDi.TabIndex = 2;
            lblNgayDi.Text = "Ngày đi:";
            // 
            // dtpNgayDi
            // 
            dtpNgayDi.CustomFormat = "dd/MM/yyyy";
            dtpNgayDi.Dock = DockStyle.Fill;
            dtpNgayDi.Font = new Font("Segoe UI", 9F);
            dtpNgayDi.Format = DateTimePickerFormat.Custom;
            dtpNgayDi.Location = new Point(133, 48);
            dtpNgayDi.Margin = new Padding(3, 6, 3, 3);
            dtpNgayDi.MinDate = new DateTime(2025, 1, 1, 0, 0, 0, 0);
            dtpNgayDi.Name = "dtpNgayDi";
            dtpNgayDi.Size = new Size(292, 27);
            dtpNgayDi.TabIndex = 3;
            // 
            // lblChuyenXe
            // 
            lblChuyenXe.Anchor = AnchorStyles.Left;
            lblChuyenXe.AutoSize = true;
            lblChuyenXe.Font = new Font("Segoe UI", 9F);
            lblChuyenXe.ForeColor = Color.FromArgb(71, 85, 105);
            lblChuyenXe.Location = new Point(3, 95);
            lblChuyenXe.Name = "lblChuyenXe";
            lblChuyenXe.Size = new Size(79, 20);
            lblChuyenXe.TabIndex = 4;
            lblChuyenXe.Text = "Chuyến xe:";
            // 
            // cboChuyenXe
            // 
            cboChuyenXe.Dock = DockStyle.Fill;
            cboChuyenXe.DropDownStyle = ComboBoxStyle.DropDownList;
            cboChuyenXe.Font = new Font("Segoe UI", 9F);
            cboChuyenXe.FormattingEnabled = true;
            cboChuyenXe.Location = new Point(133, 90);
            cboChuyenXe.Margin = new Padding(3, 6, 3, 3);
            cboChuyenXe.Name = "cboChuyenXe";
            cboChuyenXe.Size = new Size(292, 28);
            cboChuyenXe.TabIndex = 5;
            // 
            // btnTaiSoDo
            // 
            btnTaiSoDo.BackColor = Color.FromArgb(241, 245, 249);
            btnTaiSoDo.Cursor = Cursors.Hand;
            btnTaiSoDo.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnTaiSoDo.FlatStyle = FlatStyle.Flat;
            btnTaiSoDo.Font = new Font("Segoe UI", 8.5F);
            btnTaiSoDo.ForeColor = Color.FromArgb(51, 65, 85);
            btnTaiSoDo.IconChar = FontAwesome.Sharp.IconChar.RotateBack;
            btnTaiSoDo.IconColor = Color.FromArgb(71, 85, 105);
            btnTaiSoDo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnTaiSoDo.IconSize = 16;
            btnTaiSoDo.ImageAlign = ContentAlignment.MiddleLeft;
            btnTaiSoDo.Location = new Point(133, 131);
            btnTaiSoDo.Margin = new Padding(3, 5, 3, 3);
            btnTaiSoDo.Name = "btnTaiSoDo";
            btnTaiSoDo.Padding = new Padding(8, 0, 10, 0);
            btnTaiSoDo.Size = new Size(160, 34);
            btnTaiSoDo.TabIndex = 6;
            btnTaiSoDo.Text = "  Tải lại sơ đồ";
            btnTaiSoDo.TextAlign = ContentAlignment.MiddleRight;
            btnTaiSoDo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnTaiSoDo.UseVisualStyleBackColor = false;
            // 
            // pnlRight
            // 
            pnlRight.BackColor = Color.FromArgb(241, 245, 249);
            pnlRight.Controls.Add(seatMapControl);
            pnlRight.Controls.Add(pnlLegend);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Location = new Point(0, 0);
            pnlRight.Margin = new Padding(3, 4, 3, 4);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(12);
            pnlRight.Size = new Size(814, 850);
            pnlRight.TabIndex = 0;
            // 
            // seatMapControl
            // 
            seatMapControl.AutoScroll = true;
            seatMapControl.BackColor = Color.FromArgb(248, 250, 252);
            seatMapControl.Dock = DockStyle.Fill;
            seatMapControl.Location = new Point(12, 54);
            seatMapControl.Margin = new Padding(3, 4, 3, 4);
            seatMapControl.Name = "seatMapControl";
            seatMapControl.Size = new Size(790, 784);
            seatMapControl.TabIndex = 1;
            // 
            // pnlLegend
            // 
            pnlLegend.BackColor = Color.White;
            pnlLegend.Controls.Add(flpLegend);
            pnlLegend.Dock = DockStyle.Top;
            pnlLegend.Location = new Point(12, 12);
            pnlLegend.Margin = new Padding(3, 4, 3, 4);
            pnlLegend.Name = "pnlLegend";
            pnlLegend.Padding = new Padding(8, 6, 8, 6);
            pnlLegend.Size = new Size(790, 42);
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
            flpLegend.Font = new Font("Segoe UI", 9F);
            flpLegend.Location = new Point(8, 6);
            flpLegend.Margin = new Padding(3, 4, 3, 4);
            flpLegend.Name = "flpLegend";
            flpLegend.Size = new Size(774, 30);
            flpLegend.TabIndex = 0;
            // 
            // pnlLegendAvailColor
            // 
            pnlLegendAvailColor.BackColor = Color.FromArgb(224, 242, 254);
            pnlLegendAvailColor.BorderStyle = BorderStyle.FixedSingle;
            pnlLegendAvailColor.Location = new Point(3, 5);
            pnlLegendAvailColor.Margin = new Padding(3, 5, 3, 3);
            pnlLegendAvailColor.Name = "pnlLegendAvailColor";
            pnlLegendAvailColor.Size = new Size(20, 20);
            pnlLegendAvailColor.TabIndex = 0;
            // 
            // lblLegendAvail
            // 
            lblLegendAvail.AutoSize = true;
            lblLegendAvail.ForeColor = Color.FromArgb(51, 65, 85);
            lblLegendAvail.Location = new Point(29, 4);
            lblLegendAvail.Margin = new Padding(3, 4, 18, 0);
            lblLegendAvail.Name = "lblLegendAvail";
            lblLegendAvail.Size = new Size(75, 20);
            lblLegendAvail.TabIndex = 1;
            lblLegendAvail.Text = "Ghế trống";
            // 
            // pnlLegendSelColor
            // 
            pnlLegendSelColor.BackColor = Color.FromArgb(249, 115, 22);
            pnlLegendSelColor.BorderStyle = BorderStyle.FixedSingle;
            pnlLegendSelColor.Location = new Point(125, 5);
            pnlLegendSelColor.Margin = new Padding(3, 5, 3, 3);
            pnlLegendSelColor.Name = "pnlLegendSelColor";
            pnlLegendSelColor.Size = new Size(20, 20);
            pnlLegendSelColor.TabIndex = 2;
            // 
            // lblLegendSel
            // 
            lblLegendSel.AutoSize = true;
            lblLegendSel.ForeColor = Color.FromArgb(51, 65, 85);
            lblLegendSel.Location = new Point(151, 4);
            lblLegendSel.Margin = new Padding(3, 4, 18, 0);
            lblLegendSel.Name = "lblLegendSel";
            lblLegendSel.Size = new Size(81, 20);
            lblLegendSel.TabIndex = 3;
            lblLegendSel.Text = "Đang chọn";
            // 
            // pnlLegendBookedColor
            // 
            pnlLegendBookedColor.BackColor = Color.FromArgb(148, 163, 184);
            pnlLegendBookedColor.BorderStyle = BorderStyle.FixedSingle;
            pnlLegendBookedColor.Location = new Point(253, 5);
            pnlLegendBookedColor.Margin = new Padding(3, 5, 3, 3);
            pnlLegendBookedColor.Name = "pnlLegendBookedColor";
            pnlLegendBookedColor.Size = new Size(20, 20);
            pnlLegendBookedColor.TabIndex = 4;
            // 
            // lblLegendBooked
            // 
            lblLegendBooked.AutoSize = true;
            lblLegendBooked.ForeColor = Color.FromArgb(51, 65, 85);
            lblLegendBooked.Location = new Point(279, 4);
            lblLegendBooked.Margin = new Padding(3, 4, 18, 0);
            lblLegendBooked.Name = "lblLegendBooked";
            lblLegendBooked.Size = new Size(57, 20);
            lblLegendBooked.TabIndex = 5;
            lblLegendBooked.Text = "Đã bán";
            // 
            // lblLegendSteering
            // 
            lblLegendSteering.AutoSize = true;
            lblLegendSteering.ForeColor = Color.FromArgb(100, 116, 139);
            lblLegendSteering.Location = new Point(357, 4);
            lblLegendSteering.Margin = new Padding(3, 4, 3, 0);
            lblLegendSteering.Name = "lblLegendSteering";
            lblLegendSteering.Size = new Size(80, 20);
            lblLegendSteering.TabIndex = 6;
            lblLegendSteering.Text = "\U0001f6de Đầu xe";
            // 
            // errorProviderMain
            // 
            errorProviderMain.ContainerControl = this;
            // 
            // UcCustomerBooking
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            Controls.Add(splitMain);
            Font = new Font("Segoe UI", 9F);
            Margin = new Padding(3, 4, 3, 4);
            Name = "UcCustomerBooking";
            Size = new Size(1300, 850);
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
        }

        #endregion

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
        private FontAwesome.Sharp.IconButton btnHuyChon;
        private FontAwesome.Sharp.IconButton btnXemLichSu;
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
    }
}
