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
            mnuDoiMatKhau = new ToolStripMenuItem();
            mnuDangXuat = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            mnuThoat = new ToolStripMenuItem();
            mnuChucNang = new ToolStripMenuItem();
            mnuDatVe = new ToolStripMenuItem();
            mnuLichSuDatVe = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            mnuLamMoi = new ToolStripMenuItem();
            mnuTroGiup = new ToolStripMenuItem();
            mnuHuongDan = new ToolStripMenuItem();
            mnuThongTin = new ToolStripMenuItem();
            tsMain = new ToolStrip();
            tsBtnBooking = new ToolStripButton();
            tsBtnHistory = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            tsBtnRefresh = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            tsBtnGuide = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            tsBtnLogout = new ToolStripButton();
            pnlContentHost = new Panel();
            ssStatus = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            lblCurrentUser = new ToolStripStatusLabel();
            prgAction = new ToolStripProgressBar();
            lblClock = new ToolStripStatusLabel();
            timerClock = new System.Windows.Forms.Timer(components);
            msMain.SuspendLayout();
            tsMain.SuspendLayout();
            ssStatus.SuspendLayout();
            SuspendLayout();
            // 
            // msMain
            // 
            msMain.BackColor = Color.White;
            msMain.Font = new Font("Segoe UI", 9.5F);
            msMain.ImageScalingSize = new Size(20, 20);
            msMain.Items.AddRange(new ToolStripItem[] { mnuHeThong, mnuChucNang, mnuTroGiup });
            msMain.Location = new Point(0, 0);
            msMain.Name = "msMain";
            msMain.Padding = new Padding(7, 5, 0, 5);
            msMain.Size = new Size(1382, 35);
            msMain.TabIndex = 0;
            msMain.Text = "msMain";
            // 
            // mnuHeThong
            // 
            mnuHeThong.DropDownItems.AddRange(new ToolStripItem[] { mnuDoiMatKhau, mnuDangXuat, toolStripSeparator1, mnuThoat });
            mnuHeThong.Name = "mnuHeThong";
            mnuHeThong.Size = new Size(88, 25);
            mnuHeThong.Text = "&Hệ thống";
            // 
            // mnuDoiMatKhau
            // 
            mnuDoiMatKhau.Name = "mnuDoiMatKhau";
            mnuDoiMatKhau.Size = new Size(224, 26);
            mnuDoiMatKhau.Text = "Đổi &mật khẩu...";
            // 
            // mnuDangXuat
            // 
            mnuDangXuat.Name = "mnuDangXuat";
            mnuDangXuat.ShortcutKeys = Keys.Control | Keys.Q;
            mnuDangXuat.Size = new Size(224, 26);
            mnuDangXuat.Text = "Đăng &xuất";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(221, 6);
            // 
            // mnuThoat
            // 
            mnuThoat.Name = "mnuThoat";
            mnuThoat.ShortcutKeys = Keys.Alt | Keys.F4;
            mnuThoat.Size = new Size(224, 26);
            mnuThoat.Text = "&Thoát";
            // 
            // mnuChucNang
            // 
            mnuChucNang.DropDownItems.AddRange(new ToolStripItem[] { mnuDatVe, mnuLichSuDatVe, toolStripSeparator2, mnuLamMoi });
            mnuChucNang.Name = "mnuChucNang";
            mnuChucNang.Size = new Size(95, 25);
            mnuChucNang.Text = "&Chức năng";
            // 
            // mnuDatVe
            // 
            mnuDatVe.Name = "mnuDatVe";
            mnuDatVe.ShortcutKeys = Keys.Control | Keys.B;
            mnuDatVe.Size = new Size(260, 26);
            mnuDatVe.Text = "&Đặt vé trực tuyến";
            // 
            // mnuLichSuDatVe
            // 
            mnuLichSuDatVe.Name = "mnuLichSuDatVe";
            mnuLichSuDatVe.ShortcutKeys = Keys.Control | Keys.H;
            mnuLichSuDatVe.Size = new Size(260, 26);
            mnuLichSuDatVe.Text = "&Vé của tôi (Lịch sử)";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(257, 6);
            // 
            // mnuLamMoi
            // 
            mnuLamMoi.Name = "mnuLamMoi";
            mnuLamMoi.ShortcutKeys = Keys.F5;
            mnuLamMoi.Size = new Size(260, 26);
            mnuLamMoi.Text = "&Làm mới dữ liệu";
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
            mnuHuongDan.Size = new Size(244, 26);
            mnuHuongDan.Text = "&Hướng dẫn đặt vé";
            // 
            // mnuThongTin
            // 
            mnuThongTin.Name = "mnuThongTin";
            mnuThongTin.Size = new Size(244, 26);
            mnuThongTin.Text = "Thông &tin phần mềm";
            // 
            // tsMain
            // 
            tsMain.BackColor = Color.White;
            tsMain.Font = new Font("Segoe UI", 9.5F);
            tsMain.GripStyle = ToolStripGripStyle.Hidden;
            tsMain.ImageScalingSize = new Size(22, 22);
            tsMain.Items.AddRange(new ToolStripItem[] { tsBtnBooking, tsBtnHistory, toolStripSeparator3, tsBtnRefresh, toolStripSeparator4, tsBtnGuide, toolStripSeparator5, tsBtnLogout });
            tsMain.Location = new Point(0, 35);
            tsMain.Name = "tsMain";
            tsMain.Padding = new Padding(12, 4, 12, 4);
            tsMain.Size = new Size(1382, 42);
            tsMain.TabIndex = 1;
            tsMain.Text = "tsMain";
            // 
            // tsBtnBooking
            // 
            tsBtnBooking.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            tsBtnBooking.ForeColor = Color.FromArgb(16, 185, 129);
            tsBtnBooking.ImageTransparentColor = Color.Magenta;
            tsBtnBooking.Margin = new Padding(0, 1, 8, 2);
            tsBtnBooking.Name = "tsBtnBooking";
            tsBtnBooking.Size = new Size(137, 31);
            tsBtnBooking.Text = "🎫 Đặt Vé Xe";
            tsBtnBooking.ToolTipText = "Đặt vé xe trực quan qua sơ đồ ghế (Ctrl+B)";
            // 
            // tsBtnHistory
            // 
            tsBtnHistory.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            tsBtnHistory.ForeColor = Color.FromArgb(37, 99, 235);
            tsBtnHistory.ImageTransparentColor = Color.Magenta;
            tsBtnHistory.Margin = new Padding(0, 1, 8, 2);
            tsBtnHistory.Name = "tsBtnHistory";
            tsBtnHistory.Size = new Size(138, 31);
            tsBtnHistory.Text = "📋 Vé Của Tôi";
            tsBtnHistory.ToolTipText = "Tra cứu, xem lại và in vé điện tử (Ctrl+H)";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 34);
            // 
            // tsBtnRefresh
            // 
            tsBtnRefresh.ForeColor = Color.FromArgb(51, 65, 85);
            tsBtnRefresh.ImageTransparentColor = Color.Magenta;
            tsBtnRefresh.Margin = new Padding(8, 1, 8, 2);
            tsBtnRefresh.Name = "tsBtnRefresh";
            tsBtnRefresh.Size = new Size(122, 31);
            tsBtnRefresh.Text = "🔄 Làm Mới (F5)";
            tsBtnRefresh.ToolTipText = "Tải lại dữ liệu (F5)";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 34);
            // 
            // tsBtnGuide
            // 
            tsBtnGuide.ForeColor = Color.FromArgb(51, 65, 85);
            tsBtnGuide.ImageTransparentColor = Color.Magenta;
            tsBtnGuide.Margin = new Padding(8, 1, 8, 2);
            tsBtnGuide.Name = "tsBtnGuide";
            tsBtnGuide.Size = new Size(134, 31);
            tsBtnGuide.Text = "❓ Hướng Dẫn (F1)";
            tsBtnGuide.ToolTipText = "Xem hướng dẫn đặt vé (F1)";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 34);
            // 
            // tsBtnLogout
            // 
            tsBtnLogout.ForeColor = Color.FromArgb(220, 38, 38);
            tsBtnLogout.ImageTransparentColor = Color.Magenta;
            tsBtnLogout.Margin = new Padding(8, 1, 0, 2);
            tsBtnLogout.Name = "tsBtnLogout";
            tsBtnLogout.Size = new Size(103, 31);
            tsBtnLogout.Text = "🚪 Đăng Xuất";
            tsBtnLogout.ToolTipText = "Đăng xuất khỏi hệ thống (Ctrl+Q)";
            // 
            // pnlContentHost
            // 
            pnlContentHost.BackColor = Color.FromArgb(248, 250, 252);
            pnlContentHost.Dock = DockStyle.Fill;
            pnlContentHost.Location = new Point(0, 77);
            pnlContentHost.Name = "pnlContentHost";
            pnlContentHost.Size = new Size(1382, 753);
            pnlContentHost.TabIndex = 2;
            // 
            // ssStatus
            // 
            ssStatus.BackColor = Color.White;
            ssStatus.Font = new Font("Segoe UI", 9F);
            ssStatus.ImageScalingSize = new Size(20, 20);
            ssStatus.Items.AddRange(new ToolStripItem[] { lblStatus, lblCurrentUser, prgAction, lblClock });
            ssStatus.Location = new Point(0, 830);
            ssStatus.Name = "ssStatus";
            ssStatus.Padding = new Padding(1, 0, 16, 0);
            ssStatus.Size = new Size(1382, 30);
            ssStatus.TabIndex = 3;
            ssStatus.Text = "ssStatus";
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Segoe UI Semibold", 9F);
            lblStatus.ForeColor = Color.FromArgb(37, 99, 235);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(68, 24);
            lblStatus.Text = "Sẵn sàng";
            // 
            // lblCurrentUser
            // 
            lblCurrentUser.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblCurrentUser.BorderStyle = Border3DStyle.Etched;
            lblCurrentUser.ForeColor = Color.FromArgb(71, 85, 105);
            lblCurrentUser.Name = "lblCurrentUser";
            lblCurrentUser.Size = new Size(241, 24);
            lblCurrentUser.Text = "Khách hàng: Đang tải...";
            // 
            // prgAction
            // 
            prgAction.Name = "prgAction";
            prgAction.Size = new Size(130, 23);
            prgAction.Style = ProgressBarStyle.Marquee;
            prgAction.Visible = false;
            // 
            // lblClock
            // 
            lblClock.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblClock.BorderStyle = Border3DStyle.Etched;
            lblClock.ForeColor = Color.FromArgb(71, 85, 105);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(926, 24);
            lblClock.Spring = true;
            lblClock.Text = "🕒 10/10/2026 20:30:00";
            lblClock.TextAlign = ContentAlignment.MiddleRight;
            // 
            // FormMain_KhachHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(1382, 860);
            Controls.Add(pnlContentHost);
            Controls.Add(ssStatus);
            Controls.Add(tsMain);
            Controls.Add(msMain);
            Font = new Font("Segoe UI", 9F);
            MainMenuStrip = msMain;
            MinimumSize = new Size(1000, 700);
            Name = "FormMain_KhachHang";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ Thống Đặt Vé Xe Khách Trực Tuyến";
            WindowState = FormWindowState.Maximized;
            msMain.ResumeLayout(false);
            msMain.PerformLayout();
            tsMain.ResumeLayout(false);
            tsMain.PerformLayout();
            ssStatus.ResumeLayout(false);
            ssStatus.PerformLayout();
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
        private ToolStripMenuItem mnuChucNang;
        private ToolStripMenuItem mnuDatVe;
        private ToolStripMenuItem mnuLichSuDatVe;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem mnuLamMoi;
        private ToolStripMenuItem mnuTroGiup;
        private ToolStripMenuItem mnuHuongDan;
        private ToolStripMenuItem mnuThongTin;
        private ToolStrip tsMain;
        private ToolStripButton tsBtnBooking;
        private ToolStripButton tsBtnHistory;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton tsBtnRefresh;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton tsBtnGuide;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton tsBtnLogout;
        private Panel pnlContentHost;
        private StatusStrip ssStatus;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblCurrentUser;
        private ToolStripProgressBar prgAction;
        private ToolStripStatusLabel lblClock;
        private System.Windows.Forms.Timer timerClock;
    }
}
