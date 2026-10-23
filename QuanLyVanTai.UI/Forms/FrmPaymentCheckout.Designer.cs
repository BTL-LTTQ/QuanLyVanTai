namespace QuanLyVanTai.UI.Forms
{
    partial class FrmPaymentCheckout
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
            pnlHeader = new Panel();
            lblHeaderSub = new Label();
            lblHeaderTitle = new Label();
            splitBody = new SplitContainer();
            pnlLeftContent = new Panel();
            flpActions = new FlowLayoutPanel();
            btnMoTrinhDuyet = new FontAwesome.Sharp.IconButton();
            btnGiaLapThanhCong = new FontAwesome.Sharp.IconButton();
            btnGiaLapThatBai = new FontAwesome.Sharp.IconButton();
            btnHuyGiaoDich = new FontAwesome.Sharp.IconButton();
            grpDonHang = new GroupBox();
            tlpDonHang = new TableLayoutPanel();
            lblMaDonTitle = new Label();
            lblMaDon = new Label();
            lblHanhKhachTitle = new Label();
            lblHanhKhach = new Label();
            lblTuyenTitle = new Label();
            lblTuyen = new Label();
            lblGheTitle = new Label();
            lblGhe = new Label();
            lblCongTitle = new Label();
            cboChonCong = new ComboBox();
            lblTongTienTitle = new Label();
            lblTongTien = new Label();
            lblDemNguocTitle = new Label();
            lblDemNguoc = new Label();
            pnlRightContent = new Panel();
            grpQrCode = new GroupBox();
            pnlQrWrapper = new Panel();
            picQrCode = new PictureBox();
            lblHuongDanQuet = new Label();
            lblTrangThai = new Label();
            prgTienTrinh = new ProgressBar();
            timerCountdown = new System.Windows.Forms.Timer(components);
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitBody).BeginInit();
            splitBody.Panel1.SuspendLayout();
            splitBody.Panel2.SuspendLayout();
            splitBody.SuspendLayout();
            pnlLeftContent.SuspendLayout();
            flpActions.SuspendLayout();
            grpDonHang.SuspendLayout();
            tlpDonHang.SuspendLayout();
            pnlRightContent.SuspendLayout();
            grpQrCode.SuspendLayout();
            pnlQrWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picQrCode).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(37, 99, 235);
            pnlHeader.Controls.Add(lblHeaderSub);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(20, 14, 20, 14);
            pnlHeader.Size = new Size(884, 76);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeaderSub
            // 
            lblHeaderSub.AutoSize = true;
            lblHeaderSub.Font = new Font("Segoe UI", 9F);
            lblHeaderSub.ForeColor = Color.FromArgb(219, 234, 254);
            lblHeaderSub.Location = new Point(20, 42);
            lblHeaderSub.Name = "lblHeaderSub";
            lblHeaderSub.Size = new Size(477, 20);
            lblHeaderSub.TabIndex = 1;
            lblHeaderSub.Text = "Hỗ trợ quét mã QR hoặc chuyển hướng sang cổng thanh toán Sandbox";
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(18, 11);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(499, 31);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "CỔNG THANH TOÁN TRỰC TUYẾN SANDBOX";
            // 
            // splitBody
            // 
            splitBody.Dock = DockStyle.Fill;
            splitBody.FixedPanel = FixedPanel.Panel2;
            splitBody.Location = new Point(0, 76);
            splitBody.Name = "splitBody";
            // 
            // splitBody.Panel1
            // 
            splitBody.Panel1.BackColor = Color.FromArgb(248, 250, 252);
            splitBody.Panel1.Controls.Add(pnlLeftContent);
            splitBody.Panel1.Padding = new Padding(16);
            splitBody.Panel1MinSize = 460;
            // 
            // splitBody.Panel2
            // 
            splitBody.Panel2.BackColor = Color.FromArgb(241, 245, 249);
            splitBody.Panel2.Controls.Add(pnlRightContent);
            splitBody.Panel2.Padding = new Padding(16);
            splitBody.Panel2MinSize = 360;
            splitBody.Size = new Size(884, 535);
            splitBody.SplitterDistance = 472;
            splitBody.SplitterWidth = 6;
            splitBody.TabIndex = 1;
            // 
            // pnlLeftContent
            // 
            pnlLeftContent.AutoScroll = true;
            pnlLeftContent.Controls.Add(flpActions);
            pnlLeftContent.Controls.Add(grpDonHang);
            pnlLeftContent.Dock = DockStyle.Fill;
            pnlLeftContent.Location = new Point(16, 16);
            pnlLeftContent.Name = "pnlLeftContent";
            pnlLeftContent.Size = new Size(440, 503);
            pnlLeftContent.TabIndex = 0;
            // 
            // flpActions
            // 
            flpActions.Controls.Add(btnMoTrinhDuyet);
            flpActions.Controls.Add(btnGiaLapThanhCong);
            flpActions.Controls.Add(btnGiaLapThatBai);
            flpActions.Controls.Add(btnHuyGiaoDich);
            flpActions.Dock = DockStyle.Top;
            flpActions.FlowDirection = FlowDirection.TopDown;
            flpActions.Location = new Point(0, 282);
            flpActions.Name = "flpActions";
            flpActions.Padding = new Padding(0, 10, 0, 0);
            flpActions.Size = new Size(440, 210);
            flpActions.TabIndex = 1;
            // 
            // btnMoTrinhDuyet
            // 
            btnMoTrinhDuyet.BackColor = Color.FromArgb(37, 99, 235);
            btnMoTrinhDuyet.Cursor = Cursors.Hand;
            btnMoTrinhDuyet.FlatAppearance.BorderSize = 0;
            btnMoTrinhDuyet.FlatStyle = FlatStyle.Flat;
            btnMoTrinhDuyet.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnMoTrinhDuyet.ForeColor = Color.White;
            btnMoTrinhDuyet.IconChar = FontAwesome.Sharp.IconChar.Globe;
            btnMoTrinhDuyet.IconColor = Color.White;
            btnMoTrinhDuyet.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnMoTrinhDuyet.IconSize = 20;
            btnMoTrinhDuyet.ImageAlign = ContentAlignment.MiddleLeft;
            btnMoTrinhDuyet.Location = new Point(3, 14);
            btnMoTrinhDuyet.Margin = new Padding(3, 4, 3, 8);
            btnMoTrinhDuyet.Name = "btnMoTrinhDuyet";
            btnMoTrinhDuyet.Padding = new Padding(12, 0, 0, 0);
            btnMoTrinhDuyet.Size = new Size(436, 42);
            btnMoTrinhDuyet.TabIndex = 0;
            btnMoTrinhDuyet.Text = "  Mở Cổng Thanh Toán Trong Trình Duyệt";
            btnMoTrinhDuyet.TextAlign = ContentAlignment.MiddleLeft;
            btnMoTrinhDuyet.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnMoTrinhDuyet.UseVisualStyleBackColor = false;
            // 
            // btnGiaLapThanhCong
            // 
            btnGiaLapThanhCong.BackColor = Color.FromArgb(16, 185, 129);
            btnGiaLapThanhCong.Cursor = Cursors.Hand;
            btnGiaLapThanhCong.FlatAppearance.BorderSize = 0;
            btnGiaLapThanhCong.FlatStyle = FlatStyle.Flat;
            btnGiaLapThanhCong.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnGiaLapThanhCong.ForeColor = Color.White;
            btnGiaLapThanhCong.IconChar = FontAwesome.Sharp.IconChar.CheckCircle;
            btnGiaLapThanhCong.IconColor = Color.White;
            btnGiaLapThanhCong.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGiaLapThanhCong.IconSize = 18;
            btnGiaLapThanhCong.ImageAlign = ContentAlignment.MiddleLeft;
            btnGiaLapThanhCong.Location = new Point(3, 68);
            btnGiaLapThanhCong.Margin = new Padding(3, 4, 3, 6);
            btnGiaLapThanhCong.Name = "btnGiaLapThanhCong";
            btnGiaLapThanhCong.Padding = new Padding(12, 0, 0, 0);
            btnGiaLapThanhCong.Size = new Size(436, 38);
            btnGiaLapThanhCong.TabIndex = 1;
            btnGiaLapThanhCong.Text = "  ⚡ Giả Lập Quét Mã / Thanh Toán Thành Công (Mã 00)";
            btnGiaLapThanhCong.TextAlign = ContentAlignment.MiddleLeft;
            btnGiaLapThanhCong.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGiaLapThanhCong.UseVisualStyleBackColor = false;
            // 
            // btnGiaLapThatBai
            // 
            btnGiaLapThatBai.BackColor = Color.FromArgb(245, 158, 11);
            btnGiaLapThatBai.Cursor = Cursors.Hand;
            btnGiaLapThatBai.FlatAppearance.BorderSize = 0;
            btnGiaLapThatBai.FlatStyle = FlatStyle.Flat;
            btnGiaLapThatBai.Font = new Font("Segoe UI", 9F);
            btnGiaLapThatBai.ForeColor = Color.White;
            btnGiaLapThatBai.IconChar = FontAwesome.Sharp.IconChar.Warning;
            btnGiaLapThatBai.IconColor = Color.White;
            btnGiaLapThatBai.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGiaLapThatBai.IconSize = 18;
            btnGiaLapThatBai.ImageAlign = ContentAlignment.MiddleLeft;
            btnGiaLapThatBai.Location = new Point(3, 116);
            btnGiaLapThatBai.Margin = new Padding(3, 4, 3, 6);
            btnGiaLapThatBai.Name = "btnGiaLapThatBai";
            btnGiaLapThatBai.Padding = new Padding(12, 0, 0, 0);
            btnGiaLapThatBai.Size = new Size(436, 36);
            btnGiaLapThatBai.TabIndex = 2;
            btnGiaLapThatBai.Text = "  ⚠️ Giả Lập Khách Hủy / Thanh Toán Thất Bại";
            btnGiaLapThatBai.TextAlign = ContentAlignment.MiddleLeft;
            btnGiaLapThatBai.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGiaLapThatBai.UseVisualStyleBackColor = false;
            // 
            // btnHuyGiaoDich
            // 
            btnHuyGiaoDich.BackColor = Color.FromArgb(241, 245, 249);
            btnHuyGiaoDich.Cursor = Cursors.Hand;
            btnHuyGiaoDich.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnHuyGiaoDich.FlatStyle = FlatStyle.Flat;
            btnHuyGiaoDich.Font = new Font("Segoe UI", 9F);
            btnHuyGiaoDich.ForeColor = Color.FromArgb(71, 85, 105);
            btnHuyGiaoDich.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnHuyGiaoDich.IconColor = Color.FromArgb(71, 85, 105);
            btnHuyGiaoDich.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnHuyGiaoDich.IconSize = 16;
            btnHuyGiaoDich.ImageAlign = ContentAlignment.MiddleLeft;
            btnHuyGiaoDich.Location = new Point(3, 162);
            btnHuyGiaoDich.Margin = new Padding(3, 4, 3, 4);
            btnHuyGiaoDich.Name = "btnHuyGiaoDich";
            btnHuyGiaoDich.Padding = new Padding(12, 0, 0, 0);
            btnHuyGiaoDich.Size = new Size(440, 36);
            btnHuyGiaoDich.TabIndex = 3;
            btnHuyGiaoDich.Text = "  Hủy Bỏ / Quay Lại Sơ Đồ Đặt Vé";
            btnHuyGiaoDich.TextAlign = ContentAlignment.MiddleLeft;
            btnHuyGiaoDich.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnHuyGiaoDich.UseVisualStyleBackColor = false;
            // 
            // grpDonHang
            // 
            grpDonHang.BackColor = Color.White;
            grpDonHang.Controls.Add(tlpDonHang);
            grpDonHang.Dock = DockStyle.Top;
            grpDonHang.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            grpDonHang.ForeColor = Color.FromArgb(15, 23, 42);
            grpDonHang.Location = new Point(0, 0);
            grpDonHang.Name = "grpDonHang";
            grpDonHang.Padding = new Padding(12);
            grpDonHang.Size = new Size(440, 282);
            grpDonHang.TabIndex = 0;
            grpDonHang.TabStop = false;
            grpDonHang.Text = " Thông Tin Đơn Đặt Vé ";
            // 
            // tlpDonHang
            // 
            tlpDonHang.ColumnCount = 2;
            tlpDonHang.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tlpDonHang.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpDonHang.Controls.Add(lblMaDonTitle, 0, 0);
            tlpDonHang.Controls.Add(lblMaDon, 1, 0);
            tlpDonHang.Controls.Add(lblHanhKhachTitle, 0, 1);
            tlpDonHang.Controls.Add(lblHanhKhach, 1, 1);
            tlpDonHang.Controls.Add(lblTuyenTitle, 0, 2);
            tlpDonHang.Controls.Add(lblTuyen, 1, 2);
            tlpDonHang.Controls.Add(lblGheTitle, 0, 3);
            tlpDonHang.Controls.Add(lblGhe, 1, 3);
            tlpDonHang.Controls.Add(lblCongTitle, 0, 4);
            tlpDonHang.Controls.Add(cboChonCong, 1, 4);
            tlpDonHang.Controls.Add(lblTongTienTitle, 0, 5);
            tlpDonHang.Controls.Add(lblTongTien, 1, 5);
            tlpDonHang.Controls.Add(lblDemNguocTitle, 0, 6);
            tlpDonHang.Controls.Add(lblDemNguoc, 1, 6);
            tlpDonHang.Dock = DockStyle.Fill;
            tlpDonHang.Location = new Point(12, 34);
            tlpDonHang.Name = "tlpDonHang";
            tlpDonHang.RowCount = 7;
            tlpDonHang.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tlpDonHang.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tlpDonHang.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tlpDonHang.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tlpDonHang.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpDonHang.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tlpDonHang.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tlpDonHang.Size = new Size(416, 236);
            tlpDonHang.TabIndex = 0;
            // 
            // lblMaDonTitle
            // 
            lblMaDonTitle.Anchor = AnchorStyles.Left;
            lblMaDonTitle.AutoSize = true;
            lblMaDonTitle.Font = new Font("Segoe UI", 9F);
            lblMaDonTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblMaDonTitle.Location = new Point(3, 6);
            lblMaDonTitle.Name = "lblMaDonTitle";
            lblMaDonTitle.Size = new Size(63, 20);
            lblMaDonTitle.TabIndex = 0;
            lblMaDonTitle.Text = "Mã đơn:";
            // 
            // lblMaDon
            // 
            lblMaDon.Anchor = AnchorStyles.Left;
            lblMaDon.AutoSize = true;
            lblMaDon.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblMaDon.ForeColor = Color.FromArgb(15, 23, 42);
            lblMaDon.Location = new Point(133, 5);
            lblMaDon.Name = "lblMaDon";
            lblMaDon.Size = new Size(119, 21);
            lblMaDon.TabIndex = 1;
            lblMaDon.Text = "TK20261017001";
            // 
            // lblHanhKhachTitle
            // 
            lblHanhKhachTitle.Anchor = AnchorStyles.Left;
            lblHanhKhachTitle.AutoSize = true;
            lblHanhKhachTitle.Font = new Font("Segoe UI", 9F);
            lblHanhKhachTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblHanhKhachTitle.Location = new Point(3, 38);
            lblHanhKhachTitle.Name = "lblHanhKhachTitle";
            lblHanhKhachTitle.Size = new Size(89, 20);
            lblHanhKhachTitle.TabIndex = 2;
            lblHanhKhachTitle.Text = "Khách hàng:";
            // 
            // lblHanhKhach
            // 
            lblHanhKhach.Anchor = AnchorStyles.Left;
            lblHanhKhach.AutoSize = true;
            lblHanhKhach.Font = new Font("Segoe UI", 9.5F);
            lblHanhKhach.ForeColor = Color.FromArgb(30, 41, 59);
            lblHanhKhach.Location = new Point(133, 37);
            lblHanhKhach.Name = "lblHanhKhach";
            lblHanhKhach.Size = new Size(222, 21);
            lblHanhKhach.TabIndex = 3;
            lblHanhKhach.Text = "Nguyễn Văn An - 0912345678";
            // 
            // lblTuyenTitle
            // 
            lblTuyenTitle.Anchor = AnchorStyles.Left;
            lblTuyenTitle.AutoSize = true;
            lblTuyenTitle.Font = new Font("Segoe UI", 9F);
            lblTuyenTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblTuyenTitle.Location = new Point(3, 70);
            lblTuyenTitle.Name = "lblTuyenTitle";
            lblTuyenTitle.Size = new Size(70, 20);
            lblTuyenTitle.TabIndex = 4;
            lblTuyenTitle.Text = "Tuyến xe:";
            // 
            // lblTuyen
            // 
            lblTuyen.Anchor = AnchorStyles.Left;
            lblTuyen.AutoSize = true;
            lblTuyen.Font = new Font("Segoe UI", 9.5F);
            lblTuyen.ForeColor = Color.FromArgb(30, 41, 59);
            lblTuyen.Location = new Point(133, 69);
            lblTuyen.Name = "lblTuyen";
            lblTuyen.Size = new Size(133, 21);
            lblTuyen.TabIndex = 5;
            lblTuyen.Text = "Hà Nội - Đà Nẵng";
            // 
            // lblGheTitle
            // 
            lblGheTitle.Anchor = AnchorStyles.Left;
            lblGheTitle.AutoSize = true;
            lblGheTitle.Font = new Font("Segoe UI", 9F);
            lblGheTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblGheTitle.Location = new Point(3, 102);
            lblGheTitle.Name = "lblGheTitle";
            lblGheTitle.Size = new Size(74, 20);
            lblGheTitle.TabIndex = 6;
            lblGheTitle.Text = "Ghế chọn:";
            // 
            // lblGhe
            // 
            lblGhe.Anchor = AnchorStyles.Left;
            lblGhe.AutoSize = true;
            lblGhe.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblGhe.ForeColor = Color.FromArgb(37, 99, 235);
            lblGhe.Location = new Point(133, 101);
            lblGhe.Name = "lblGhe";
            lblGhe.Size = new Size(73, 21);
            lblGhe.TabIndex = 7;
            lblGhe.Text = "A01, A02";
            // 
            // lblCongTitle
            // 
            lblCongTitle.Anchor = AnchorStyles.Left;
            lblCongTitle.AutoSize = true;
            lblCongTitle.Font = new Font("Segoe UI", 9F);
            lblCongTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblCongTitle.Location = new Point(3, 136);
            lblCongTitle.Name = "lblCongTitle";
            lblCongTitle.Size = new Size(72, 20);
            lblCongTitle.TabIndex = 8;
            lblCongTitle.Text = "Cổng GD:";
            // 
            // cboChonCong
            // 
            cboChonCong.Dock = DockStyle.Fill;
            cboChonCong.DropDownStyle = ComboBoxStyle.DropDownList;
            cboChonCong.Font = new Font("Segoe UI", 9F);
            cboChonCong.FormattingEnabled = true;
            cboChonCong.Location = new Point(133, 131);
            cboChonCong.Name = "cboChonCong";
            cboChonCong.Size = new Size(280, 28);
            cboChonCong.TabIndex = 9;
            // 
            // lblTongTienTitle
            // 
            lblTongTienTitle.Anchor = AnchorStyles.Left;
            lblTongTienTitle.AutoSize = true;
            lblTongTienTitle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblTongTienTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTongTienTitle.Location = new Point(3, 170);
            lblTongTienTitle.Name = "lblTongTienTitle";
            lblTongTienTitle.Size = new Size(109, 25);
            lblTongTienTitle.TabIndex = 10;
            lblTongTienTitle.Text = "Thanh toán:";
            // 
            // lblTongTien
            // 
            lblTongTien.Anchor = AnchorStyles.Left;
            lblTongTien.AutoSize = true;
            lblTongTien.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.FromArgb(16, 185, 129);
            lblTongTien.Location = new Point(133, 167);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(154, 31);
            lblTongTien.TabIndex = 11;
            lblTongTien.Text = "500,000 VNĐ";
            // 
            // lblDemNguocTitle
            // 
            lblDemNguocTitle.Anchor = AnchorStyles.Left;
            lblDemNguocTitle.AutoSize = true;
            lblDemNguocTitle.Font = new Font("Segoe UI", 9F);
            lblDemNguocTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblDemNguocTitle.Location = new Point(3, 209);
            lblDemNguocTitle.Name = "lblDemNguocTitle";
            lblDemNguocTitle.Size = new Size(119, 20);
            lblDemNguocTitle.TabIndex = 12;
            lblDemNguocTitle.Text = "Thời gian giữ vé:";
            // 
            // lblDemNguoc
            // 
            lblDemNguoc.Anchor = AnchorStyles.Left;
            lblDemNguoc.AutoSize = true;
            lblDemNguoc.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblDemNguoc.ForeColor = Color.FromArgb(220, 38, 38);
            lblDemNguoc.Location = new Point(133, 207);
            lblDemNguoc.Name = "lblDemNguoc";
            lblDemNguoc.Size = new Size(48, 23);
            lblDemNguoc.TabIndex = 13;
            lblDemNguoc.Text = "15:00";
            // 
            // pnlRightContent
            // 
            pnlRightContent.Controls.Add(grpQrCode);
            pnlRightContent.Dock = DockStyle.Fill;
            pnlRightContent.Location = new Point(16, 16);
            pnlRightContent.Name = "pnlRightContent";
            pnlRightContent.Size = new Size(374, 503);
            pnlRightContent.TabIndex = 0;
            // 
            // grpQrCode
            // 
            grpQrCode.BackColor = Color.White;
            grpQrCode.Controls.Add(pnlQrWrapper);
            grpQrCode.Controls.Add(lblHuongDanQuet);
            grpQrCode.Controls.Add(lblTrangThai);
            grpQrCode.Controls.Add(prgTienTrinh);
            grpQrCode.Dock = DockStyle.Fill;
            grpQrCode.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            grpQrCode.ForeColor = Color.FromArgb(15, 23, 42);
            grpQrCode.Location = new Point(0, 0);
            grpQrCode.Name = "grpQrCode";
            grpQrCode.Padding = new Padding(14);
            grpQrCode.Size = new Size(374, 503);
            grpQrCode.TabIndex = 0;
            grpQrCode.TabStop = false;
            grpQrCode.Text = " Mã QR Thanh Toán Trực Tuyến ";
            // 
            // pnlQrWrapper
            // 
            pnlQrWrapper.Anchor = AnchorStyles.Top;
            pnlQrWrapper.BackColor = Color.FromArgb(248, 250, 252);
            pnlQrWrapper.BorderStyle = BorderStyle.FixedSingle;
            pnlQrWrapper.Controls.Add(picQrCode);
            pnlQrWrapper.Location = new Point(72, 34);
            pnlQrWrapper.Name = "pnlQrWrapper";
            pnlQrWrapper.Padding = new Padding(8);
            pnlQrWrapper.Size = new Size(230, 230);
            pnlQrWrapper.TabIndex = 0;
            // 
            // picQrCode
            // 
            picQrCode.BackColor = Color.White;
            picQrCode.Dock = DockStyle.Fill;
            picQrCode.Location = new Point(8, 8);
            picQrCode.Name = "picQrCode";
            picQrCode.Size = new Size(212, 212);
            picQrCode.SizeMode = PictureBoxSizeMode.Zoom;
            picQrCode.TabIndex = 0;
            picQrCode.TabStop = false;
            // 
            // lblHuongDanQuet
            // 
            lblHuongDanQuet.Dock = DockStyle.Bottom;
            lblHuongDanQuet.Font = new Font("Segoe UI", 9F);
            lblHuongDanQuet.ForeColor = Color.FromArgb(71, 85, 105);
            lblHuongDanQuet.Location = new Point(14, 275);
            lblHuongDanQuet.Name = "lblHuongDanQuet";
            lblHuongDanQuet.Size = new Size(346, 92);
            lblHuongDanQuet.TabIndex = 1;
            lblHuongDanQuet.Text = "Quét mã QR bằng App Ngân hàng (VNPAY-QR) hoặc Ví MoMo để tiến hành thanh toán.\r\n\r\nSau khi thanh toán thành công, hệ thống sẽ tự động cập nhật ngay lập tức.";
            lblHuongDanQuet.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTrangThai
            // 
            lblTrangThai.Dock = DockStyle.Bottom;
            lblTrangThai.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblTrangThai.ForeColor = Color.FromArgb(37, 99, 235);
            lblTrangThai.Location = new Point(14, 367);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(346, 96);
            lblTrangThai.TabIndex = 2;
            lblTrangThai.Text = "⏳ Đang kết nối cổng và chờ giao dịch...";
            lblTrangThai.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // prgTienTrinh
            // 
            prgTienTrinh.Dock = DockStyle.Bottom;
            prgTienTrinh.Location = new Point(14, 463);
            prgTienTrinh.MarqueeAnimationSpeed = 30;
            prgTienTrinh.Name = "prgTienTrinh";
            prgTienTrinh.Size = new Size(346, 26);
            prgTienTrinh.Style = ProgressBarStyle.Marquee;
            prgTienTrinh.TabIndex = 3;
            // 
            // FrmPaymentCheckout
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(884, 611);
            Controls.Add(splitBody);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(880, 620);
            Name = "FrmPaymentCheckout";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thanh Toán Đặt Vé Trực Tuyến - VNPay / MoMo Sandbox";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            splitBody.Panel1.ResumeLayout(false);
            splitBody.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitBody).EndInit();
            splitBody.ResumeLayout(false);
            pnlLeftContent.ResumeLayout(false);
            flpActions.ResumeLayout(false);
            grpDonHang.ResumeLayout(false);
            tlpDonHang.ResumeLayout(false);
            tlpDonHang.PerformLayout();
            pnlRightContent.ResumeLayout(false);
            grpQrCode.ResumeLayout(false);
            pnlQrWrapper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picQrCode).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSub;
        private SplitContainer splitBody;
        private Panel pnlLeftContent;
        private GroupBox grpDonHang;
        private TableLayoutPanel tlpDonHang;
        private Label lblMaDonTitle;
        private Label lblMaDon;
        private Label lblHanhKhachTitle;
        private Label lblHanhKhach;
        private Label lblTuyenTitle;
        private Label lblTuyen;
        private Label lblGheTitle;
        private Label lblGhe;
        private Label lblCongTitle;
        private ComboBox cboChonCong;
        private Label lblTongTienTitle;
        private Label lblTongTien;
        private Label lblDemNguocTitle;
        private Label lblDemNguoc;
        private FlowLayoutPanel flpActions;
        private FontAwesome.Sharp.IconButton btnMoTrinhDuyet;
        private FontAwesome.Sharp.IconButton btnGiaLapThanhCong;
        private FontAwesome.Sharp.IconButton btnGiaLapThatBai;
        private FontAwesome.Sharp.IconButton btnHuyGiaoDich;
        private Panel pnlRightContent;
        private GroupBox grpQrCode;
        private Panel pnlQrWrapper;
        private PictureBox picQrCode;
        private Label lblHuongDanQuet;
        private Label lblTrangThai;
        private ProgressBar prgTienTrinh;
        private System.Windows.Forms.Timer timerCountdown;
    }
}
