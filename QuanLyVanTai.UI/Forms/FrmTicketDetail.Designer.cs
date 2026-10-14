namespace QuanLyVanTai.UI.Forms
{
    partial class FrmTicketDetail
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
            pnlHeader = new Panel();
            lblStatusBadge = new Label();
            lblTicketCodeTitle = new Label();
            pnlContent = new Panel();
            grpThongTinXe = new GroupBox();
            tlpThongTinXe = new TableLayoutPanel();
            lblTuyenDuongTitle = new Label();
            lblTuyenDuong = new Label();
            lblBenDiTitle = new Label();
            lblBenDi = new Label();
            lblBenDenTitle = new Label();
            lblBenDen = new Label();
            lblBienSoTitle = new Label();
            lblBienSo = new Label();
            lblLoaiXeTitle = new Label();
            lblLoaiXe = new Label();
            lblKhoiHanhTitle = new Label();
            lblKhoiHanh = new Label();
            lblViTriGheTitle = new Label();
            lblViTriGhe = new Label();
            grpThanhToan = new GroupBox();
            tlpThanhToan = new TableLayoutPanel();
            lblGiaVeTitle = new Label();
            lblGiaVe = new Label();
            lblThanhToanTitle = new Label();
            lblThanhToan = new Label();
            lblNgayDatTitle = new Label();
            lblNgayDat = new Label();
            lblNhanVienTitle = new Label();
            lblNhanVien = new Label();
            grpKhachHang = new GroupBox();
            tlpKhachHang = new TableLayoutPanel();
            lblHoTenTitle = new Label();
            lblHoTen = new Label();
            lblDienThoaiTitle = new Label();
            lblDienThoai = new Label();
            pnlActions = new Panel();
            flpButtons = new FlowLayoutPanel();
            btnInVe = new FontAwesome.Sharp.IconButton();
            btnHuyVe = new FontAwesome.Sharp.IconButton();
            btnDong = new FontAwesome.Sharp.IconButton();
            pnlHeader.SuspendLayout();
            pnlContent.SuspendLayout();
            grpThongTinXe.SuspendLayout();
            tlpThongTinXe.SuspendLayout();
            grpThanhToan.SuspendLayout();
            tlpThanhToan.SuspendLayout();
            grpKhachHang.SuspendLayout();
            tlpKhachHang.SuspendLayout();
            pnlActions.SuspendLayout();
            flpButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblStatusBadge);
            pnlHeader.Controls.Add(lblTicketCodeTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(20, 15, 20, 15);
            pnlHeader.Size = new Size(680, 65);
            pnlHeader.TabIndex = 0;
            // 
            // lblStatusBadge
            // 
            lblStatusBadge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblStatusBadge.BackColor = Color.FromArgb(220, 252, 231);
            lblStatusBadge.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblStatusBadge.ForeColor = Color.FromArgb(16, 124, 65);
            lblStatusBadge.Location = new Point(510, 16);
            lblStatusBadge.Name = "lblStatusBadge";
            lblStatusBadge.Size = new Size(145, 32);
            lblStatusBadge.TabIndex = 1;
            lblStatusBadge.Text = "ĐÃ ĐẶT CHỖ";
            lblStatusBadge.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTicketCodeTitle
            // 
            lblTicketCodeTitle.AutoSize = true;
            lblTicketCodeTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTicketCodeTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTicketCodeTitle.Location = new Point(16, 16);
            lblTicketCodeTitle.Name = "lblTicketCodeTitle";
            lblTicketCodeTitle.Size = new Size(279, 30);
            lblTicketCodeTitle.TabIndex = 0;
            lblTicketCodeTitle.Text = "THÔNG TIN VÉ: TK000000";
            // 
            // pnlContent
            // 
            pnlContent.AutoScroll = true;
            pnlContent.Controls.Add(grpThongTinXe);
            pnlContent.Controls.Add(grpThanhToan);
            pnlContent.Controls.Add(grpKhachHang);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 65);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(16, 12, 16, 12);
            pnlContent.Size = new Size(680, 485);
            pnlContent.TabIndex = 1;
            // 
            // grpThongTinXe
            // 
            grpThongTinXe.BackColor = Color.White;
            grpThongTinXe.Controls.Add(tlpThongTinXe);
            grpThongTinXe.Dock = DockStyle.Top;
            grpThongTinXe.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            grpThongTinXe.ForeColor = Color.FromArgb(15, 23, 42);
            grpThongTinXe.Location = new Point(16, 107);
            grpThongTinXe.Name = "grpThongTinXe";
            grpThongTinXe.Padding = new Padding(12);
            grpThongTinXe.Size = new Size(648, 225);
            grpThongTinXe.TabIndex = 1;
            grpThongTinXe.TabStop = false;
            grpThongTinXe.Text = " Thông Tin Chuyến Xe & Chỗ Ngồi ";
            // 
            // tlpThongTinXe
            // 
            tlpThongTinXe.ColumnCount = 4;
            tlpThongTinXe.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tlpThongTinXe.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpThongTinXe.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tlpThongTinXe.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpThongTinXe.Controls.Add(lblTuyenDuongTitle, 0, 0);
            tlpThongTinXe.Controls.Add(lblTuyenDuong, 1, 0);
            tlpThongTinXe.Controls.Add(lblBenDiTitle, 0, 1);
            tlpThongTinXe.Controls.Add(lblBenDi, 1, 1);
            tlpThongTinXe.Controls.Add(lblBenDenTitle, 2, 1);
            tlpThongTinXe.Controls.Add(lblBenDen, 3, 1);
            tlpThongTinXe.Controls.Add(lblBienSoTitle, 0, 2);
            tlpThongTinXe.Controls.Add(lblBienSo, 1, 2);
            tlpThongTinXe.Controls.Add(lblLoaiXeTitle, 2, 2);
            tlpThongTinXe.Controls.Add(lblLoaiXe, 3, 2);
            tlpThongTinXe.Controls.Add(lblKhoiHanhTitle, 0, 3);
            tlpThongTinXe.Controls.Add(lblKhoiHanh, 1, 3);
            tlpThongTinXe.Controls.Add(lblViTriGheTitle, 2, 3);
            tlpThongTinXe.Controls.Add(lblViTriGhe, 3, 3);
            tlpThongTinXe.Dock = DockStyle.Fill;
            tlpThongTinXe.Location = new Point(12, 34);
            tlpThongTinXe.Name = "tlpThongTinXe";
            tlpThongTinXe.RowCount = 4;
            tlpThongTinXe.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tlpThongTinXe.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tlpThongTinXe.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tlpThongTinXe.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpThongTinXe.Size = new Size(624, 179);
            tlpThongTinXe.TabIndex = 0;
            // 
            // lblTuyenDuongTitle
            // 
            lblTuyenDuongTitle.Anchor = AnchorStyles.Left;
            lblTuyenDuongTitle.AutoSize = true;
            lblTuyenDuongTitle.Font = new Font("Segoe UI", 9F);
            lblTuyenDuongTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblTuyenDuongTitle.Location = new Point(3, 11);
            lblTuyenDuongTitle.Name = "lblTuyenDuongTitle";
            lblTuyenDuongTitle.Size = new Size(99, 20);
            lblTuyenDuongTitle.TabIndex = 0;
            lblTuyenDuongTitle.Text = "Tuyến đường:";
            // 
            // lblTuyenDuong
            // 
            lblTuyenDuong.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblTuyenDuong.AutoSize = true;
            tlpThongTinXe.SetColumnSpan(lblTuyenDuong, 3);
            lblTuyenDuong.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblTuyenDuong.ForeColor = Color.FromArgb(37, 99, 235);
            lblTuyenDuong.Location = new Point(113, 9);
            lblTuyenDuong.Name = "lblTuyenDuong";
            lblTuyenDuong.Size = new Size(508, 23);
            lblTuyenDuong.TabIndex = 1;
            lblTuyenDuong.Text = "Hà Nội - Đà Nẵng";
            // 
            // lblBenDiTitle
            // 
            lblBenDiTitle.Anchor = AnchorStyles.Left;
            lblBenDiTitle.AutoSize = true;
            lblBenDiTitle.Font = new Font("Segoe UI", 9F);
            lblBenDiTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblBenDiTitle.Location = new Point(3, 53);
            lblBenDiTitle.Name = "lblBenDiTitle";
            lblBenDiTitle.Size = new Size(60, 20);
            lblBenDiTitle.TabIndex = 2;
            lblBenDiTitle.Text = "Nơi đi:";
            // 
            // lblBenDi
            // 
            lblBenDi.Anchor = AnchorStyles.Left;
            lblBenDi.AutoSize = true;
            lblBenDi.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblBenDi.ForeColor = Color.FromArgb(15, 23, 42);
            lblBenDi.Location = new Point(113, 52);
            lblBenDi.Name = "lblBenDi";
            lblBenDi.Size = new Size(130, 21);
            lblBenDi.TabIndex = 3;
            lblBenDi.Text = "Bến xe Mỹ Đình";
            // 
            // lblBenDenTitle
            // 
            lblBenDenTitle.Anchor = AnchorStyles.Left;
            lblBenDenTitle.AutoSize = true;
            lblBenDenTitle.Font = new Font("Segoe UI", 9F);
            lblBenDenTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblBenDenTitle.Location = new Point(315, 53);
            lblBenDenTitle.Name = "lblBenDenTitle";
            lblBenDenTitle.Size = new Size(71, 20);
            lblBenDenTitle.TabIndex = 4;
            lblBenDenTitle.Text = "Nơi đến:";
            // 
            // lblBenDen
            // 
            lblBenDen.Anchor = AnchorStyles.Left;
            lblBenDen.AutoSize = true;
            lblBenDen.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblBenDen.ForeColor = Color.FromArgb(15, 23, 42);
            lblBenDen.Location = new Point(425, 52);
            lblBenDen.Name = "lblBenDen";
            lblBenDen.Size = new Size(140, 21);
            lblBenDen.TabIndex = 5;
            lblBenDen.Text = "Bến xe TT Đà Nẵng";
            // 
            // lblBienSoTitle
            // 
            lblBienSoTitle.Anchor = AnchorStyles.Left;
            lblBienSoTitle.AutoSize = true;
            lblBienSoTitle.Font = new Font("Segoe UI", 9F);
            lblBienSoTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblBienSoTitle.Location = new Point(3, 95);
            lblBienSoTitle.Name = "lblBienSoTitle";
            lblBienSoTitle.Size = new Size(81, 20);
            lblBienSoTitle.TabIndex = 6;
            lblBienSoTitle.Text = "Biển số xe:";
            // 
            // lblBienSo
            // 
            lblBienSo.Anchor = AnchorStyles.Left;
            lblBienSo.AutoSize = true;
            lblBienSo.Font = new Font("Segoe UI", 9.5F);
            lblBienSo.ForeColor = Color.FromArgb(15, 23, 42);
            lblBienSo.Location = new Point(113, 94);
            lblBienSo.Name = "lblBienSo";
            lblBienSo.Size = new Size(84, 21);
            lblBienSo.TabIndex = 7;
            lblBienSo.Text = "29B-123.45";
            // 
            // lblLoaiXeTitle
            // 
            lblLoaiXeTitle.Anchor = AnchorStyles.Left;
            lblLoaiXeTitle.AutoSize = true;
            lblLoaiXeTitle.Font = new Font("Segoe UI", 9F);
            lblLoaiXeTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblLoaiXeTitle.Location = new Point(315, 95);
            lblLoaiXeTitle.Name = "lblLoaiXeTitle";
            lblLoaiXeTitle.Size = new Size(61, 20);
            lblLoaiXeTitle.TabIndex = 8;
            lblLoaiXeTitle.Text = "Loại xe:";
            // 
            // lblLoaiXe
            // 
            lblLoaiXe.Anchor = AnchorStyles.Left;
            lblLoaiXe.AutoSize = true;
            lblLoaiXe.Font = new Font("Segoe UI", 9.5F);
            lblLoaiXe.ForeColor = Color.FromArgb(15, 23, 42);
            lblLoaiXe.Location = new Point(425, 94);
            lblLoaiXe.Name = "lblLoaiXe";
            lblLoaiXe.Size = new Size(139, 21);
            lblLoaiXe.TabIndex = 9;
            lblLoaiXe.Text = "Giường nằm 40 chỗ";
            // 
            // lblKhoiHanhTitle
            // 
            lblKhoiHanhTitle.Anchor = AnchorStyles.Left;
            lblKhoiHanhTitle.AutoSize = true;
            lblKhoiHanhTitle.Font = new Font("Segoe UI", 9F);
            lblKhoiHanhTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblKhoiHanhTitle.Location = new Point(3, 139);
            lblKhoiHanhTitle.Name = "lblKhoiHanhTitle";
            lblKhoiHanhTitle.Size = new Size(80, 20);
            lblKhoiHanhTitle.TabIndex = 10;
            lblKhoiHanhTitle.Text = "Khởi hành:";
            // 
            // lblKhoiHanh
            // 
            lblKhoiHanh.Anchor = AnchorStyles.Left;
            lblKhoiHanh.AutoSize = true;
            lblKhoiHanh.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblKhoiHanh.ForeColor = Color.FromArgb(220, 38, 38);
            lblKhoiHanh.Location = new Point(113, 137);
            lblKhoiHanh.Name = "lblKhoiHanh";
            lblKhoiHanh.Size = new Size(147, 23);
            lblKhoiHanh.TabIndex = 11;
            lblKhoiHanh.Text = "14:00 - 20/10/2026";
            // 
            // lblViTriGheTitle
            // 
            lblViTriGheTitle.Anchor = AnchorStyles.Left;
            lblViTriGheTitle.AutoSize = true;
            lblViTriGheTitle.Font = new Font("Segoe UI", 9F);
            lblViTriGheTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblViTriGheTitle.Location = new Point(315, 139);
            lblViTriGheTitle.Name = "lblViTriGheTitle";
            lblViTriGheTitle.Size = new Size(82, 20);
            lblViTriGheTitle.TabIndex = 12;
            lblViTriGheTitle.Text = "Vị trí ghế:";
            // 
            // lblViTriGhe
            // 
            lblViTriGhe.Anchor = AnchorStyles.Left;
            lblViTriGhe.AutoSize = true;
            lblViTriGhe.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblViTriGhe.ForeColor = Color.FromArgb(37, 99, 235);
            lblViTriGhe.Location = new Point(425, 135);
            lblViTriGhe.Name = "lblViTriGhe";
            lblViTriGhe.Size = new Size(119, 28);
            lblViTriGhe.TabIndex = 13;
            lblViTriGhe.Text = "A01 (Tầng 1)";
            // 
            // grpThanhToan
            // 
            grpThanhToan.BackColor = Color.White;
            grpThanhToan.Controls.Add(tlpThanhToan);
            grpThanhToan.Dock = DockStyle.Bottom;
            grpThanhToan.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            grpThanhToan.ForeColor = Color.FromArgb(15, 23, 42);
            grpThanhToan.Location = new Point(16, 338);
            grpThanhToan.Name = "grpThanhToan";
            grpThanhToan.Padding = new Padding(12);
            grpThanhToan.Size = new Size(648, 135);
            grpThanhToan.TabIndex = 2;
            grpThanhToan.TabStop = false;
            grpThanhToan.Text = " Cước Phí & Thanh Toán ";
            // 
            // tlpThanhToan
            // 
            tlpThanhToan.ColumnCount = 4;
            tlpThanhToan.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tlpThanhToan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpThongTinXe.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tlpThanhToan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpThanhToan.Controls.Add(lblGiaVeTitle, 0, 0);
            tlpThanhToan.Controls.Add(lblGiaVe, 1, 0);
            tlpThanhToan.Controls.Add(lblThanhToanTitle, 2, 0);
            tlpThanhToan.Controls.Add(lblThanhToan, 3, 0);
            tlpThanhToan.Controls.Add(lblNgayDatTitle, 0, 1);
            tlpThanhToan.Controls.Add(lblNgayDat, 1, 1);
            tlpThanhToan.Controls.Add(lblNhanVienTitle, 2, 1);
            tlpThanhToan.Controls.Add(lblNhanVien, 3, 1);
            tlpThanhToan.Dock = DockStyle.Fill;
            tlpThanhToan.Location = new Point(12, 34);
            tlpThanhToan.Name = "tlpThanhToan";
            tlpThanhToan.RowCount = 2;
            tlpThanhToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpThanhToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpThanhToan.Size = new Size(624, 89);
            tlpThanhToan.TabIndex = 0;
            // 
            // lblGiaVeTitle
            // 
            lblGiaVeTitle.Anchor = AnchorStyles.Left;
            lblGiaVeTitle.AutoSize = true;
            lblGiaVeTitle.Font = new Font("Segoe UI", 9F);
            lblGiaVeTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblGiaVeTitle.Location = new Point(3, 12);
            lblGiaVeTitle.Name = "lblGiaVeTitle";
            lblGiaVeTitle.Size = new Size(79, 20);
            lblGiaVeTitle.TabIndex = 0;
            lblGiaVeTitle.Text = "Giá cước:";
            // 
            // lblGiaVe
            // 
            lblGiaVe.Anchor = AnchorStyles.Left;
            lblGiaVe.AutoSize = true;
            lblGiaVe.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblGiaVe.ForeColor = Color.FromArgb(15, 23, 42);
            lblGiaVe.Location = new Point(113, 8);
            lblGiaVe.Name = "lblGiaVe";
            lblGiaVe.Size = new Size(130, 28);
            lblGiaVe.TabIndex = 1;
            lblGiaVe.Text = "350.000 VNĐ";
            // 
            // lblThanhToanTitle
            // 
            lblThanhToanTitle.Anchor = AnchorStyles.Left;
            lblThanhToanTitle.AutoSize = true;
            lblThanhToanTitle.Font = new Font("Segoe UI", 9F);
            lblThanhToanTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblThanhToanTitle.Location = new Point(315, 12);
            lblThanhToanTitle.Name = "lblThanhToanTitle";
            lblThanhToanTitle.Size = new Size(81, 20);
            lblThanhToanTitle.TabIndex = 2;
            lblThanhToanTitle.Text = "Trạng thái:";
            // 
            // lblThanhToan
            // 
            lblThanhToan.Anchor = AnchorStyles.Left;
            lblThanhToan.AutoSize = true;
            lblThanhToan.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblThanhToan.ForeColor = Color.FromArgb(16, 124, 65);
            lblThanhToan.Location = new Point(425, 11);
            lblThanhToan.Name = "lblThanhToan";
            lblThanhToan.Size = new Size(132, 23);
            lblThanhToan.TabIndex = 3;
            lblThanhToan.Text = "ĐÃ THANH TOÁN";
            // 
            // lblNgayDatTitle
            // 
            lblNgayDatTitle.Anchor = AnchorStyles.Left;
            lblNgayDatTitle.AutoSize = true;
            lblNgayDatTitle.Font = new Font("Segoe UI", 9F);
            lblNgayDatTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblNgayDatTitle.Location = new Point(3, 57);
            lblNgayDatTitle.Name = "lblNgayDatTitle";
            lblNgayDatTitle.Size = new Size(74, 20);
            lblNgayDatTitle.TabIndex = 4;
            lblNgayDatTitle.Text = "Ngày đặt:";
            // 
            // lblNgayDat
            // 
            lblNgayDat.Anchor = AnchorStyles.Left;
            lblNgayDat.AutoSize = true;
            lblNgayDat.Font = new Font("Segoe UI", 9.5F);
            lblNgayDat.ForeColor = Color.FromArgb(15, 23, 42);
            lblNgayDat.Location = new Point(113, 57);
            lblNgayDat.Name = "lblNgayDat";
            lblNgayDat.Size = new Size(144, 21);
            lblNgayDat.TabIndex = 5;
            lblNgayDat.Text = "12/10/2026 10:20";
            // 
            // lblNhanVienTitle
            // 
            lblNhanVienTitle.Anchor = AnchorStyles.Left;
            lblNhanVienTitle.AutoSize = true;
            lblNhanVienTitle.Font = new Font("Segoe UI", 9F);
            lblNhanVienTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblNhanVienTitle.Location = new Point(315, 57);
            lblNhanVienTitle.Name = "lblNhanVienTitle";
            lblNhanVienTitle.Size = new Size(81, 20);
            lblNhanVienTitle.TabIndex = 6;
            lblNhanVienTitle.Text = "Kênh / NV:";
            // 
            // lblNhanVien
            // 
            lblNhanVien.Anchor = AnchorStyles.Left;
            lblNhanVien.AutoSize = true;
            lblNhanVien.Font = new Font("Segoe UI", 9.5F);
            lblNhanVien.ForeColor = Color.FromArgb(15, 23, 42);
            lblNhanVien.Location = new Point(425, 57);
            lblNhanVien.Name = "lblNhanVien";
            lblNhanVien.Size = new Size(140, 21);
            lblNhanVien.TabIndex = 7;
            lblNhanVien.Text = "Đặt vé trực tuyến";
            // 
            // grpKhachHang
            // 
            grpKhachHang.BackColor = Color.White;
            grpKhachHang.Controls.Add(tlpKhachHang);
            grpKhachHang.Dock = DockStyle.Top;
            grpKhachHang.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            grpKhachHang.ForeColor = Color.FromArgb(15, 23, 42);
            grpKhachHang.Location = new Point(16, 12);
            grpKhachHang.Name = "grpKhachHang";
            grpKhachHang.Padding = new Padding(12);
            grpKhachHang.Size = new Size(648, 95);
            grpKhachHang.TabIndex = 0;
            grpKhachHang.TabStop = false;
            grpKhachHang.Text = " Thông Tin Hành Khách ";
            // 
            // tlpKhachHang
            // 
            tlpKhachHang.ColumnCount = 4;
            tlpKhachHang.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tlpKhachHang.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpKhachHang.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tlpKhachHang.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpKhachHang.Controls.Add(lblHoTenTitle, 0, 0);
            tlpKhachHang.Controls.Add(lblHoTen, 1, 0);
            tlpKhachHang.Controls.Add(lblDienThoaiTitle, 2, 0);
            tlpKhachHang.Controls.Add(lblDienThoai, 3, 0);
            tlpKhachHang.Dock = DockStyle.Fill;
            tlpKhachHang.Location = new Point(12, 34);
            tlpKhachHang.Name = "tlpKhachHang";
            tlpKhachHang.RowCount = 1;
            tlpKhachHang.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpKhachHang.Size = new Size(624, 49);
            tlpKhachHang.TabIndex = 0;
            // 
            // lblHoTenTitle
            // 
            lblHoTenTitle.Anchor = AnchorStyles.Left;
            lblHoTenTitle.AutoSize = true;
            lblHoTenTitle.Font = new Font("Segoe UI", 9F);
            lblHoTenTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblHoTenTitle.Location = new Point(3, 14);
            lblHoTenTitle.Name = "lblHoTenTitle";
            lblHoTenTitle.Size = new Size(76, 20);
            lblHoTenTitle.TabIndex = 0;
            lblHoTenTitle.Text = "Họ và tên:";
            // 
            // lblHoTen
            // 
            lblHoTen.Anchor = AnchorStyles.Left;
            lblHoTen.AutoSize = true;
            lblHoTen.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblHoTen.ForeColor = Color.FromArgb(15, 23, 42);
            lblHoTen.Location = new Point(113, 13);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(130, 23);
            lblHoTen.TabIndex = 1;
            lblHoTen.Text = "Nguyễn Văn An";
            // 
            // lblDienThoaiTitle
            // 
            lblDienThoaiTitle.Anchor = AnchorStyles.Left;
            lblDienThoaiTitle.AutoSize = true;
            lblDienThoaiTitle.Font = new Font("Segoe UI", 9F);
            lblDienThoaiTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblDienThoaiTitle.Location = new Point(315, 14);
            lblDienThoaiTitle.Name = "lblDienThoaiTitle";
            lblDienThoaiTitle.Size = new Size(81, 20);
            lblDienThoaiTitle.TabIndex = 2;
            lblDienThoaiTitle.Text = "Điện thoại:";
            // 
            // lblDienThoai
            // 
            lblDienThoai.Anchor = AnchorStyles.Left;
            lblDienThoai.AutoSize = true;
            lblDienThoai.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblDienThoai.ForeColor = Color.FromArgb(15, 23, 42);
            lblDienThoai.Location = new Point(425, 13);
            lblDienThoai.Name = "lblDienThoai";
            lblDienThoai.Size = new Size(100, 23);
            lblDienThoai.TabIndex = 3;
            lblDienThoai.Text = "0912345678";
            // 
            // pnlActions
            // 
            pnlActions.BackColor = Color.White;
            pnlActions.Controls.Add(flpButtons);
            pnlActions.Dock = DockStyle.Bottom;
            pnlActions.Location = new Point(0, 550);
            pnlActions.Name = "pnlActions";
            pnlActions.Padding = new Padding(16, 10, 16, 10);
            pnlActions.Size = new Size(680, 60);
            pnlActions.TabIndex = 2;
            // 
            // flpButtons
            // 
            flpButtons.Controls.Add(btnInVe);
            flpButtons.Controls.Add(btnHuyVe);
            flpButtons.Controls.Add(btnDong);
            flpButtons.Dock = DockStyle.Right;
            flpButtons.Location = new Point(245, 10);
            flpButtons.Name = "flpButtons";
            flpButtons.Size = new Size(419, 40);
            flpButtons.TabIndex = 0;
            flpButtons.WrapContents = false;
            // 
            // btnInVe
            // 
            btnInVe.BackColor = Color.FromArgb(16, 185, 129);
            btnInVe.Cursor = Cursors.Hand;
            btnInVe.FlatAppearance.BorderSize = 0;
            btnInVe.FlatStyle = FlatStyle.Flat;
            btnInVe.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnInVe.ForeColor = Color.White;
            btnInVe.IconChar = FontAwesome.Sharp.IconChar.Print;
            btnInVe.IconColor = Color.White;
            btnInVe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnInVe.IconSize = 18;
            btnInVe.ImageAlign = ContentAlignment.MiddleLeft;
            btnInVe.Location = new Point(3, 2);
            btnInVe.Margin = new Padding(3, 2, 6, 2);
            btnInVe.Name = "btnInVe";
            btnInVe.Size = new Size(140, 36);
            btnInVe.TabIndex = 0;
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
            btnHuyVe.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnHuyVe.ForeColor = Color.White;
            btnHuyVe.IconChar = FontAwesome.Sharp.IconChar.TimesCircle;
            btnHuyVe.IconColor = Color.White;
            btnHuyVe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnHuyVe.IconSize = 18;
            btnHuyVe.ImageAlign = ContentAlignment.MiddleLeft;
            btnHuyVe.Location = new Point(152, 2);
            btnHuyVe.Margin = new Padding(3, 2, 6, 2);
            btnHuyVe.Name = "btnHuyVe";
            btnHuyVe.Size = new Size(135, 36);
            btnHuyVe.TabIndex = 1;
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
            btnDong.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnDong.ForeColor = Color.White;
            btnDong.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            btnDong.IconColor = Color.White;
            btnDong.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDong.IconSize = 18;
            btnDong.ImageAlign = ContentAlignment.MiddleLeft;
            btnDong.Location = new Point(296, 2);
            btnDong.Margin = new Padding(3, 2, 3, 2);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(110, 36);
            btnDong.TabIndex = 2;
            btnDong.Text = " Đóng";
            btnDong.TextAlign = ContentAlignment.MiddleRight;
            btnDong.UseVisualStyleBackColor = false;
            // 
            // FrmTicketDetail
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(680, 610);
            Controls.Add(pnlContent);
            Controls.Add(pnlActions);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmTicketDetail";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chi Tiết Vé Xe Điện Tử - QLVT Bus Lines";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlContent.ResumeLayout(false);
            grpThongTinXe.ResumeLayout(false);
            tlpThongTinXe.ResumeLayout(false);
            tlpThongTinXe.PerformLayout();
            grpThanhToan.ResumeLayout(false);
            tlpThanhToan.ResumeLayout(false);
            tlpThanhToan.PerformLayout();
            grpKhachHang.ResumeLayout(false);
            tlpKhachHang.ResumeLayout(false);
            tlpKhachHang.PerformLayout();
            pnlActions.ResumeLayout(false);
            flpButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTicketCodeTitle;
        private Label lblStatusBadge;
        private Panel pnlContent;
        private GroupBox grpKhachHang;
        private TableLayoutPanel tlpKhachHang;
        private Label lblHoTenTitle;
        private Label lblHoTen;
        private Label lblDienThoaiTitle;
        private Label lblDienThoai;
        private GroupBox grpThongTinXe;
        private TableLayoutPanel tlpThongTinXe;
        private Label lblTuyenDuongTitle;
        private Label lblTuyenDuong;
        private Label lblBenDiTitle;
        private Label lblBenDi;
        private Label lblBenDenTitle;
        private Label lblBenDen;
        private Label lblBienSoTitle;
        private Label lblBienSo;
        private Label lblLoaiXeTitle;
        private Label lblLoaiXe;
        private Label lblKhoiHanhTitle;
        private Label lblKhoiHanh;
        private Label lblViTriGheTitle;
        private Label lblViTriGhe;
        private GroupBox grpThanhToan;
        private TableLayoutPanel tlpThanhToan;
        private Label lblGiaVeTitle;
        private Label lblGiaVe;
        private Label lblThanhToanTitle;
        private Label lblThanhToan;
        private Label lblNgayDatTitle;
        private Label lblNgayDat;
        private Label lblNhanVienTitle;
        private Label lblNhanVien;
        private Panel pnlActions;
        private FlowLayoutPanel flpButtons;
        private FontAwesome.Sharp.IconButton btnInVe;
        private FontAwesome.Sharp.IconButton btnHuyVe;
        private FontAwesome.Sharp.IconButton btnDong;
    }
}
