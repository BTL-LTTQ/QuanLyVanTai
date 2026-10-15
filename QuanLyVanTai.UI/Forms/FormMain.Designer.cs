namespace QuanLyVanTai.UI
{
    partial class FormMain
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
            mnuLamMoi = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            mnuDangXuat = new ToolStripMenuItem();
            mnuThoat = new ToolStripMenuItem();
            mnuDanhMuc = new ToolStripMenuItem();
            mnuTuyenXe = new ToolStripMenuItem();
            mnuPhuongTien = new ToolStripMenuItem();
            mnuGiaVe = new ToolStripMenuItem();
            mnuNghiepVu = new ToolStripMenuItem();
            mnuNhanSu = new ToolStripMenuItem();
            mnuKhuyenMai = new ToolStripMenuItem();
            mnuDichVu = new ToolStripMenuItem();
            mnuBaoCao = new ToolStripMenuItem();
            mnuDashboard = new ToolStripMenuItem();
            mnuNhatKy = new ToolStripMenuItem();
            mnuQuanTri = new ToolStripMenuItem();
            mnuPhanQuyen = new ToolStripMenuItem();
            mnuTroGiup = new ToolStripMenuItem();
            mnuThongTin = new ToolStripMenuItem();
            tsMain = new ToolStrip();
            tsBtnDashboard = new ToolStripButton();
            tsBtnLamMoi = new ToolStripButton();
            tsBtnDatVe = new ToolStripButton();
            tsBtnTuyenXe = new ToolStripButton();
            tsBtnNhatKy = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            tsLblLanguage = new ToolStripLabel();
            tsCboLanguage = new ToolStripComboBox();
            toolStripSeparator3 = new ToolStripSeparator();
            tsBtnDangXuat = new ToolStripButton();
            ssStatus = new StatusStrip();
            lblStatusReady = new ToolStripStatusLabel();
            prgAction = new ToolStripProgressBar();
            lblStatusSpring = new ToolStripStatusLabel();
            lblStatusDb = new ToolStripStatusLabel();
            lblStatusUser = new ToolStripStatusLabel();
            lblStatusVersion = new ToolStripStatusLabel();
            lblClock = new ToolStripStatusLabel();
            pnlContentHost = new Panel();
            pnlCanhBao = new Panel();
            btnDong = new Button();
            lblTieuDe = new Label();
            lblNoiDung = new Label();
            picCanhBao = new PictureBox();
            timerClock = new System.Windows.Forms.Timer(components);

            msMain.SuspendLayout();
            tsMain.SuspendLayout();
            ssStatus.SuspendLayout();
            pnlContentHost.SuspendLayout();
            pnlCanhBao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCanhBao).BeginInit();
            SuspendLayout();

            // 
            // msMain
            // 
            msMain.BackColor = Color.White;
            msMain.Font = new Font("Segoe UI", 9.5F);
            msMain.ImageScalingSize = new Size(20, 20);
            msMain.Items.AddRange(new ToolStripItem[] {
                mnuHeThong,
                mnuDanhMuc,
                mnuNghiepVu,
                mnuBaoCao,
                mnuQuanTri,
                mnuTroGiup
            });
            msMain.Location = new Point(0, 0);
            msMain.Name = "msMain";
            msMain.Padding = new Padding(8, 4, 0, 4);
            msMain.Size = new Size(1280, 33);
            msMain.TabIndex = 0;
            msMain.Text = "msMain";

            // 
            // mnuHeThong
            // 
            mnuHeThong.DropDownItems.AddRange(new ToolStripItem[] {
                mnuLamMoi,
                toolStripSeparator1,
                mnuDangXuat,
                mnuThoat
            });
            mnuHeThong.Name = "mnuHeThong";
            mnuHeThong.Size = new Size(90, 25);
            mnuHeThong.Text = "&Hệ thống";

            // 
            // mnuLamMoi
            // 
            mnuLamMoi.Name = "mnuLamMoi";
            mnuLamMoi.ShortcutKeys = Keys.F5;
            mnuLamMoi.Size = new Size(210, 26);
            mnuLamMoi.Text = "&Làm mới";
            mnuLamMoi.Click += mnuLamMoi_Click;

            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(207, 6);

            // 
            // mnuDangXuat
            // 
            mnuDangXuat.Name = "mnuDangXuat";
            mnuDangXuat.ShortcutKeys = Keys.Control | Keys.L;
            mnuDangXuat.Size = new Size(210, 26);
            mnuDangXuat.Text = "&Đăng xuất";
            mnuDangXuat.Click += mnuDangXuat_Click;

            // 
            // mnuThoat
            // 
            mnuThoat.Name = "mnuThoat";
            mnuThoat.ShortcutKeys = Keys.Alt | Keys.F4;
            mnuThoat.Size = new Size(210, 26);
            mnuThoat.Text = "&Thoát";
            mnuThoat.Click += mnuThoat_Click;

            // 
            // mnuDanhMuc
            // 
            mnuDanhMuc.DropDownItems.AddRange(new ToolStripItem[] {
                mnuTuyenXe,
                mnuPhuongTien,
                mnuGiaVe
            });
            mnuDanhMuc.Name = "mnuDanhMuc";
            mnuDanhMuc.Size = new Size(95, 25);
            mnuDanhMuc.Text = "&Danh mục";

            // 
            // mnuTuyenXe
            // 
            mnuTuyenXe.Name = "mnuTuyenXe";
            mnuTuyenXe.ShortcutKeys = Keys.Control | Keys.D1;
            mnuTuyenXe.Size = new Size(270, 26);
            mnuTuyenXe.Text = "&Tuyến xe && Lộ trình";
            mnuTuyenXe.Click += mnuTuyenXe_Click;

            // 
            // mnuPhuongTien
            // 
            mnuPhuongTien.Name = "mnuPhuongTien";
            mnuPhuongTien.ShortcutKeys = Keys.Control | Keys.D2;
            mnuPhuongTien.Size = new Size(270, 26);
            mnuPhuongTien.Text = "&Phương tiện && Trạm";
            mnuPhuongTien.Click += mnuPhuongTien_Click;

            // 
            // mnuGiaVe
            // 
            mnuGiaVe.Name = "mnuGiaVe";
            mnuGiaVe.ShortcutKeys = Keys.Control | Keys.D3;
            mnuGiaVe.Size = new Size(270, 26);
            mnuGiaVe.Text = "&Bảng giá vé";
            mnuGiaVe.Click += mnuGiaVe_Click;

            // 
            // mnuNghiepVu
            // 
            mnuNghiepVu.DropDownItems.AddRange(new ToolStripItem[] {
                mnuNhanSu,
                mnuKhuyenMai,
                mnuDichVu
            });
            mnuNghiepVu.Name = "mnuNghiepVu";
            mnuNghiepVu.Size = new Size(94, 25);
            mnuNghiepVu.Text = "&Nghiệp vụ";

            // 
            // mnuNhanSu
            // 
            mnuNhanSu.Name = "mnuNhanSu";
            mnuNhanSu.ShortcutKeys = Keys.Control | Keys.D4;
            mnuNhanSu.Size = new Size(280, 26);
            mnuNhanSu.Text = "&Quản lý Nhân sự";
            mnuNhanSu.Click += mnuNhanSu_Click;

            // 
            // mnuKhuyenMai
            // 
            mnuKhuyenMai.Name = "mnuKhuyenMai";
            mnuKhuyenMai.ShortcutKeys = Keys.Control | Keys.D5;
            mnuKhuyenMai.Size = new Size(280, 26);
            mnuKhuyenMai.Text = "&Chương trình Khuyến mãi";
            mnuKhuyenMai.Click += mnuKhuyenMai_Click;

            // 
            // mnuDichVu
            // 
            mnuDichVu.Name = "mnuDichVu";
            mnuDichVu.ShortcutKeys = Keys.Control | Keys.D8;
            mnuDichVu.Size = new Size(280, 26);
            mnuDichVu.Text = "&Dịch vụ Vận tải";
            mnuDichVu.Click += mnuDichVu_Click;

            // 
            // mnuBaoCao
            // 
            mnuBaoCao.DropDownItems.AddRange(new ToolStripItem[] {
                mnuDashboard,
                mnuNhatKy
            });
            mnuBaoCao.Name = "mnuBaoCao";
            mnuBaoCao.Size = new Size(149, 25);
            mnuBaoCao.Text = "&Báo cáo && Thống kê";

            // 
            // mnuDashboard
            // 
            mnuDashboard.Name = "mnuDashboard";
            mnuDashboard.ShortcutKeys = Keys.F2;
            mnuDashboard.Size = new Size(260, 26);
            mnuDashboard.Text = "&Dashboard Tổng quan";
            mnuDashboard.Click += mnuDashboard_Click;

            // 
            // mnuNhatKy
            // 
            mnuNhatKy.Name = "mnuNhatKy";
            mnuNhatKy.ShortcutKeys = Keys.Control | Keys.D7;
            mnuNhatKy.Size = new Size(260, 26);
            mnuNhatKy.Text = "&Nhật ký Hoạt động";
            mnuNhatKy.Click += mnuNhatKy_Click;

            // 
            // mnuQuanTri
            // 
            mnuQuanTri.DropDownItems.AddRange(new ToolStripItem[] {
                mnuPhanQuyen
            });
            mnuQuanTri.Name = "mnuQuanTri";
            mnuQuanTri.Size = new Size(82, 25);
            mnuQuanTri.Text = "&Quản trị";

            // 
            // mnuPhanQuyen
            // 
            mnuPhanQuyen.Name = "mnuPhanQuyen";
            mnuPhanQuyen.ShortcutKeys = Keys.Control | Keys.D6;
            mnuPhanQuyen.Size = new Size(260, 26);
            mnuPhanQuyen.Text = "&Phân quyền Hệ thống";
            mnuPhanQuyen.Click += mnuPhanQuyen_Click;

            // 
            // mnuTroGiup
            // 
            mnuTroGiup.DropDownItems.AddRange(new ToolStripItem[] {
                mnuThongTin
            });
            mnuTroGiup.Name = "mnuTroGiup";
            mnuTroGiup.Size = new Size(81, 25);
            mnuTroGiup.Text = "&Trợ giúp";

            // 
            // mnuThongTin
            // 
            mnuThongTin.Name = "mnuThongTin";
            mnuThongTin.ShortcutKeys = Keys.F1;
            mnuThongTin.Size = new Size(240, 26);
            mnuThongTin.Text = "&Thông tin Phần mềm";
            mnuThongTin.Click += mnuThongTin_Click;

            // 
            // tsMain
            // 
            tsMain.BackColor = Color.FromArgb(248, 250, 252);
            tsMain.Font = new Font("Segoe UI", 9F);
            tsMain.GripStyle = ToolStripGripStyle.Hidden;
            tsMain.ImageScalingSize = new Size(20, 20);
            tsMain.Items.AddRange(new ToolStripItem[] {
                tsBtnDashboard,
                tsBtnLamMoi,
                tsBtnDatVe,
                tsBtnTuyenXe,
                tsBtnNhatKy,
                toolStripSeparator2,
                tsLblLanguage,
                tsCboLanguage,
                toolStripSeparator3,
                tsBtnDangXuat
            });
            tsMain.Location = new Point(0, 33);
            tsMain.Name = "tsMain";
            tsMain.Padding = new Padding(12, 4, 12, 4);
            tsMain.Size = new Size(1280, 40);
            tsMain.TabIndex = 1;
            tsMain.Text = "tsMain";

            // 
            // tsBtnDashboard
            // 
            tsBtnDashboard.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            tsBtnDashboard.ForeColor = Color.FromArgb(15, 23, 42);
            tsBtnDashboard.Name = "tsBtnDashboard";
            tsBtnDashboard.Padding = new Padding(6, 2, 6, 2);
            tsBtnDashboard.Size = new Size(116, 29);
            tsBtnDashboard.Text = "📊 Tổng quan";
            tsBtnDashboard.ToolTipText = "Mở màn hình Dashboard Tổng quan (F2)";
            tsBtnDashboard.Click += mnuDashboard_Click;

            // 
            // tsBtnLamMoi
            // 
            tsBtnLamMoi.ForeColor = Color.FromArgb(30, 41, 59);
            tsBtnLamMoi.Name = "tsBtnLamMoi";
            tsBtnLamMoi.Padding = new Padding(6, 2, 6, 2);
            tsBtnLamMoi.Size = new Size(106, 29);
            tsBtnLamMoi.Text = "🔄 Làm mới";
            tsBtnLamMoi.ToolTipText = "Làm mới dữ liệu toàn hệ thống (F5)";
            tsBtnLamMoi.Click += mnuLamMoi_Click;

            // 
            // tsBtnDatVe
            // 
            tsBtnDatVe.ForeColor = Color.FromArgb(30, 41, 59);
            tsBtnDatVe.Name = "tsBtnDatVe";
            tsBtnDatVe.Padding = new Padding(6, 2, 6, 2);
            tsBtnDatVe.Size = new Size(130, 29);
            tsBtnDatVe.Text = "🎫 Bán vé & Giá";
            tsBtnDatVe.ToolTipText = "Mở nhanh Bảng quản lý giá vé & Đặt vé (Ctrl+3)";
            tsBtnDatVe.Click += mnuGiaVe_Click;

            // 
            // tsBtnTuyenXe
            // 
            tsBtnTuyenXe.ForeColor = Color.FromArgb(30, 41, 59);
            tsBtnTuyenXe.Name = "tsBtnTuyenXe";
            tsBtnTuyenXe.Padding = new Padding(6, 2, 6, 2);
            tsBtnTuyenXe.Size = new Size(106, 29);
            tsBtnTuyenXe.Text = "🚌 Tuyến xe";
            tsBtnTuyenXe.ToolTipText = "Mở nhanh Quản lý Tuyến xe & Lộ trình (Ctrl+1)";
            tsBtnTuyenXe.Click += mnuTuyenXe_Click;

            // 
            // tsBtnNhatKy
            // 
            tsBtnNhatKy.ForeColor = Color.FromArgb(30, 41, 59);
            tsBtnNhatKy.Name = "tsBtnNhatKy";
            tsBtnNhatKy.Padding = new Padding(6, 2, 6, 2);
            tsBtnNhatKy.Size = new Size(150, 29);
            tsBtnNhatKy.Text = "🛡️ Nhật ký an ninh";
            tsBtnNhatKy.ToolTipText = "Kiểm toán hoạt động & Nhật ký an ninh (Ctrl+7)";
            tsBtnNhatKy.Click += mnuNhatKy_Click;

            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 32);

            // 
            // tsLblLanguage
            // 
            tsLblLanguage.ForeColor = Color.FromArgb(71, 85, 105);
            tsLblLanguage.Name = "tsLblLanguage";
            tsLblLanguage.Size = new Size(100, 29);
            tsLblLanguage.Text = "🌐 Ngôn ngữ:";

            // 
            // tsCboLanguage
            // 
            tsCboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            tsCboLanguage.Font = new Font("Segoe UI", 9F);
            tsCboLanguage.Name = "tsCboLanguage";
            tsCboLanguage.Size = new Size(140, 32);

            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 32);

            // 
            // tsBtnDangXuat
            // 
            tsBtnDangXuat.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            tsBtnDangXuat.ForeColor = Color.FromArgb(220, 38, 38);
            tsBtnDangXuat.Name = "tsBtnDangXuat";
            tsBtnDangXuat.Padding = new Padding(6, 2, 6, 2);
            tsBtnDangXuat.Size = new Size(111, 29);
            tsBtnDangXuat.Text = "🚪 Đăng xuất";
            tsBtnDangXuat.ToolTipText = "Đăng xuất khỏi hệ thống (Ctrl+L)";
            tsBtnDangXuat.Click += mnuDangXuat_Click;

            // 
            // ssStatus
            // 
            ssStatus.BackColor = Color.FromArgb(241, 245, 249);
            ssStatus.Font = new Font("Segoe UI", 9F);
            ssStatus.ImageScalingSize = new Size(20, 20);
            ssStatus.Items.AddRange(new ToolStripItem[] {
                lblStatusReady,
                prgAction,
                lblStatusSpring,
                lblStatusDb,
                lblStatusUser,
                lblStatusVersion,
                lblClock
            });
            ssStatus.Location = new Point(0, 692);
            ssStatus.Name = "ssStatus";
            ssStatus.Padding = new Padding(1, 0, 14, 0);
            ssStatus.Size = new Size(1280, 28);
            ssStatus.TabIndex = 2;
            ssStatus.Text = "ssStatus";

            // 
            // lblStatusReady
            // 
            lblStatusReady.Font = new Font("Segoe UI", 9F);
            lblStatusReady.ForeColor = Color.FromArgb(16, 185, 129);
            lblStatusReady.Margin = new Padding(4, 3, 8, 3);
            lblStatusReady.Name = "lblStatusReady";
            lblStatusReady.Size = new Size(142, 22);
            lblStatusReady.Text = "🟢 Hệ thống sẵn sàng";
            lblStatusReady.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // prgAction
            // 
            prgAction.Name = "prgAction";
            prgAction.Size = new Size(120, 20);
            prgAction.Style = ProgressBarStyle.Continuous;
            prgAction.Visible = false;

            // 
            // lblStatusSpring
            // 
            lblStatusSpring.Name = "lblStatusSpring";
            lblStatusSpring.Size = new Size(464, 22);
            lblStatusSpring.Spring = true;

            // 
            // lblStatusDb
            // 
            lblStatusDb.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblStatusDb.BorderStyle = Border3DStyle.Etched;
            lblStatusDb.Font = new Font("Segoe UI", 8.5F);
            lblStatusDb.ForeColor = Color.FromArgb(5, 150, 105);
            lblStatusDb.Margin = new Padding(4, 3, 4, 3);
            lblStatusDb.Name = "lblStatusDb";
            lblStatusDb.Padding = new Padding(4, 0, 4, 0);
            lblStatusDb.Size = new Size(224, 22);
            lblStatusDb.Text = "🗄️ CSDL: SQL Server (Đã kết nối)";

            // 
            // lblStatusUser
            // 
            lblStatusUser.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblStatusUser.BorderStyle = Border3DStyle.Etched;
            lblStatusUser.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblStatusUser.ForeColor = Color.FromArgb(15, 23, 42);
            lblStatusUser.Margin = new Padding(4, 3, 4, 3);
            lblStatusUser.Name = "lblStatusUser";
            lblStatusUser.Padding = new Padding(4, 0, 4, 0);
            lblStatusUser.Size = new Size(174, 22);
            lblStatusUser.Text = "👤 admin (Quản trị viên)";

            // 
            // lblStatusVersion
            // 
            lblStatusVersion.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblStatusVersion.BorderStyle = Border3DStyle.Etched;
            lblStatusVersion.Font = new Font("Segoe UI", 8.5F);
            lblStatusVersion.ForeColor = Color.FromArgb(100, 116, 139);
            lblStatusVersion.Margin = new Padding(4, 3, 4, 3);
            lblStatusVersion.Name = "lblStatusVersion";
            lblStatusVersion.Padding = new Padding(4, 0, 4, 0);
            lblStatusVersion.Size = new Size(59, 22);
            lblStatusVersion.Text = "🏷️ v1.0";

            // 
            // lblClock
            // 
            lblClock.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblClock.BorderStyle = Border3DStyle.Etched;
            lblClock.Font = new Font("Segoe UI", 8.5F);
            lblClock.ForeColor = Color.FromArgb(71, 85, 105);
            lblClock.Margin = new Padding(4, 3, 4, 3);
            lblClock.Name = "lblClock";
            lblClock.Padding = new Padding(4, 0, 4, 0);
            lblClock.Size = new Size(160, 22);
            lblClock.Text = "🕒 14:00:00 15/10/2026";

            // 
            // pnlContentHost
            // 
            pnlContentHost.AutoScroll = true;
            pnlContentHost.BackColor = Color.FromArgb(241, 245, 249);
            pnlContentHost.Controls.Add(pnlCanhBao);
            pnlContentHost.Dock = DockStyle.Fill;
            pnlContentHost.Location = new Point(0, 73);
            pnlContentHost.Name = "pnlContentHost";
            pnlContentHost.Size = new Size(1280, 619);
            pnlContentHost.TabIndex = 3;

            // 
            // pnlCanhBao
            // 
            pnlCanhBao.Anchor = AnchorStyles.None;
            pnlCanhBao.BackColor = Color.White;
            pnlCanhBao.BorderStyle = BorderStyle.FixedSingle;
            pnlCanhBao.Controls.Add(btnDong);
            pnlCanhBao.Controls.Add(lblTieuDe);
            pnlCanhBao.Controls.Add(lblNoiDung);
            pnlCanhBao.Controls.Add(picCanhBao);
            pnlCanhBao.Location = new Point(390, 160);
            pnlCanhBao.Name = "pnlCanhBao";
            pnlCanhBao.Size = new Size(500, 300);
            pnlCanhBao.TabIndex = 0;
            pnlCanhBao.Visible = false;

            // 
            // btnDong
            // 
            btnDong.Location = new Point(175, 235);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(150, 40);
            btnDong.TabIndex = 3;
            btnDong.Text = " Đóng";
            btnDong.UseVisualStyleBackColor = true;

            // 
            // lblTieuDe
            // 
            lblTieuDe.Location = new Point(20, 95);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(460, 32);
            lblTieuDe.TabIndex = 1;
            lblTieuDe.Text = "TRUY CẬP BỊ TỪ CHỐI";
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblNoiDung
            // 
            lblNoiDung.Location = new Point(20, 135);
            lblNoiDung.Name = "lblNoiDung";
            lblNoiDung.Size = new Size(460, 85);
            lblNoiDung.TabIndex = 2;
            lblNoiDung.Text = "Bạn không có quyền truy cập chức năng này.";
            lblNoiDung.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // picCanhBao
            // 
            picCanhBao.Location = new Point(215, 20);
            picCanhBao.Name = "picCanhBao";
            picCanhBao.Size = new Size(70, 70);
            picCanhBao.SizeMode = PictureBoxSizeMode.CenterImage;
            picCanhBao.TabIndex = 0;
            picCanhBao.TabStop = false;

            // 
            // timerClock
            // 
            timerClock.Enabled = true;
            timerClock.Interval = 1000;

            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(pnlContentHost);
            Controls.Add(ssStatus);
            Controls.Add(tsMain);
            Controls.Add(msMain);
            MainMenuStrip = msMain;
            MinimumSize = new Size(1024, 650);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ Thống Quản Lý Vận Tải Hành Khách (BTL LTTQ)";

            msMain.ResumeLayout(false);
            msMain.PerformLayout();
            tsMain.ResumeLayout(false);
            tsMain.PerformLayout();
            ssStatus.ResumeLayout(false);
            ssStatus.PerformLayout();
            pnlContentHost.ResumeLayout(false);
            pnlCanhBao.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picCanhBao).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip msMain;
        private ToolStripMenuItem mnuHeThong;
        private ToolStripMenuItem mnuLamMoi;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem mnuDangXuat;
        private ToolStripMenuItem mnuThoat;
        private ToolStripMenuItem mnuDanhMuc;
        private ToolStripMenuItem mnuTuyenXe;
        private ToolStripMenuItem mnuPhuongTien;
        private ToolStripMenuItem mnuGiaVe;
        private ToolStripMenuItem mnuNghiepVu;
        private ToolStripMenuItem mnuNhanSu;
        private ToolStripMenuItem mnuKhuyenMai;
        private ToolStripMenuItem mnuDichVu;
        private ToolStripMenuItem mnuBaoCao;
        private ToolStripMenuItem mnuDashboard;
        private ToolStripMenuItem mnuNhatKy;
        private ToolStripMenuItem mnuQuanTri;
        private ToolStripMenuItem mnuPhanQuyen;
        private ToolStripMenuItem mnuTroGiup;
        private ToolStripMenuItem mnuThongTin;

        private ToolStrip tsMain;
        private ToolStripButton tsBtnDashboard;
        private ToolStripButton tsBtnLamMoi;
        private ToolStripButton tsBtnDatVe;
        private ToolStripButton tsBtnTuyenXe;
        private ToolStripButton tsBtnNhatKy;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripLabel tsLblLanguage;
        private ToolStripComboBox tsCboLanguage;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton tsBtnDangXuat;

        private StatusStrip ssStatus;
        private ToolStripStatusLabel lblStatusReady;
        private ToolStripProgressBar prgAction;
        private ToolStripStatusLabel lblStatusSpring;
        private ToolStripStatusLabel lblStatusDb;
        private ToolStripStatusLabel lblStatusUser;
        private ToolStripStatusLabel lblStatusVersion;
        private ToolStripStatusLabel lblClock;

        private Panel pnlContentHost;
        private Panel pnlCanhBao;
        private Label lblTieuDe;
        private Label lblNoiDung;
        private PictureBox picCanhBao;
        private Button btnDong;
        private System.Windows.Forms.Timer timerClock;
    }
}
