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
            pnlSidebar = new Panel();
            pnlSidebarTop = new Panel();
            lblAppName = new Label();
            pnlSidebarMenu = new Panel();
            btnCaiDat = new FontAwesome.Sharp.IconButton();
            btnNhatKy = new FontAwesome.Sharp.IconButton();
            btnPhanQuyen = new FontAwesome.Sharp.IconButton();
            btnKhuyenMai = new FontAwesome.Sharp.IconButton();
            btnGiaVe = new FontAwesome.Sharp.IconButton();
            btnNhanSu = new FontAwesome.Sharp.IconButton();
            btnDichVu = new FontAwesome.Sharp.IconButton();
            btnTrangChu = new FontAwesome.Sharp.IconButton();
            pnlHeader = new Panel();
            lblTitle = new Label();
            pnlContent = new Panel();
            pnlCanhBao = new Panel();
            btnDong = new Button();
            lblTieuDe = new Label();
            lblNoiDung = new Label();
            picCanhBao = new PictureBox();

            pnlSidebar.SuspendLayout();
            pnlSidebarTop.SuspendLayout();
            pnlSidebarMenu.SuspendLayout();
            pnlHeader.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlCanhBao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCanhBao).BeginInit();
            SuspendLayout();

            // pnlSidebar
            pnlSidebar.BackColor = Color.FromArgb(30, 41, 59);
            pnlSidebar.Controls.Add(pnlSidebarMenu);
            pnlSidebar.Controls.Add(pnlSidebarTop);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(220, 700);
            pnlSidebar.TabIndex = 0;

            // pnlSidebarTop
            pnlSidebarTop.BackColor = Color.FromArgb(15, 23, 42);
            pnlSidebarTop.Controls.Add(lblAppName);
            pnlSidebarTop.Dock = DockStyle.Top;
            pnlSidebarTop.Height = 72;
            pnlSidebarTop.Name = "pnlSidebarTop";

            lblAppName.Text = "🚌 VẬN TẢI BTL";
            lblAppName.ForeColor = Color.White;
            lblAppName.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblAppName.Dock = DockStyle.Fill;
            lblAppName.TextAlign = ContentAlignment.MiddleCenter;
            lblAppName.Name = "lblAppName";

            // pnlSidebarMenu
            pnlSidebarMenu.Controls.Add(btnCaiDat);
            pnlSidebarMenu.Controls.Add(btnNhatKy);
            pnlSidebarMenu.Controls.Add(btnPhanQuyen);
            pnlSidebarMenu.Controls.Add(btnKhuyenMai);
            pnlSidebarMenu.Controls.Add(btnGiaVe);
            pnlSidebarMenu.Controls.Add(btnNhanSu);
            pnlSidebarMenu.Controls.Add(btnDichVu);
            pnlSidebarMenu.Controls.Add(btnTrangChu);
            pnlSidebarMenu.Dock = DockStyle.Fill;
            pnlSidebarMenu.Name = "pnlSidebarMenu";

            // Sidebar buttons (Dock=Top, added in reverse so TrangChu is first)
            btnCaiDat.BackColor = Color.FromArgb(30, 41, 59);
            btnCaiDat.Dock = DockStyle.Top;
            btnCaiDat.FlatAppearance.BorderSize = 0;
            btnCaiDat.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
            btnCaiDat.FlatStyle = FlatStyle.Flat;
            btnCaiDat.ForeColor = Color.White;
            btnCaiDat.IconChar = FontAwesome.Sharp.IconChar.None;
            btnCaiDat.IconColor = Color.White;
            btnCaiDat.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCaiDat.IconSize = 22;
            btnCaiDat.Name = "btnCaiDat";
            btnCaiDat.Size = new Size(220, 52);
            btnCaiDat.TabIndex = 8;
            btnCaiDat.Text = " Cài Đặt";
            btnCaiDat.TextAlign = ContentAlignment.MiddleLeft;
            btnCaiDat.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCaiDat.ImageAlign = ContentAlignment.MiddleLeft;
            btnCaiDat.Padding = new Padding(16, 0, 0, 0);
            btnCaiDat.UseVisualStyleBackColor = false;
            btnCaiDat.Cursor = Cursors.Hand;

            btnNhatKy.BackColor = Color.FromArgb(30, 41, 59);
            btnNhatKy.Dock = DockStyle.Top;
            btnNhatKy.FlatAppearance.BorderSize = 0;
            btnNhatKy.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
            btnNhatKy.FlatStyle = FlatStyle.Flat;
            btnNhatKy.ForeColor = Color.White;
            btnNhatKy.IconChar = FontAwesome.Sharp.IconChar.None;
            btnNhatKy.IconColor = Color.White;
            btnNhatKy.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnNhatKy.IconSize = 22;
            btnNhatKy.Name = "btnNhatKy";
            btnNhatKy.Size = new Size(220, 52);
            btnNhatKy.TabIndex = 7;
            btnNhatKy.Text = " Nhật Ký";
            btnNhatKy.TextAlign = ContentAlignment.MiddleLeft;
            btnNhatKy.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNhatKy.ImageAlign = ContentAlignment.MiddleLeft;
            btnNhatKy.Padding = new Padding(16, 0, 0, 0);
            btnNhatKy.UseVisualStyleBackColor = false;
            btnNhatKy.Cursor = Cursors.Hand;
            btnNhatKy.Click += btnNhatKy_Click;

            btnPhanQuyen.BackColor = Color.FromArgb(30, 41, 59);
            btnPhanQuyen.Dock = DockStyle.Top;
            btnPhanQuyen.FlatAppearance.BorderSize = 0;
            btnPhanQuyen.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
            btnPhanQuyen.FlatStyle = FlatStyle.Flat;
            btnPhanQuyen.ForeColor = Color.White;
            btnPhanQuyen.IconChar = FontAwesome.Sharp.IconChar.None;
            btnPhanQuyen.IconColor = Color.White;
            btnPhanQuyen.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnPhanQuyen.IconSize = 22;
            btnPhanQuyen.Name = "btnPhanQuyen";
            btnPhanQuyen.Size = new Size(220, 52);
            btnPhanQuyen.TabIndex = 6;
            btnPhanQuyen.Text = " Phân Quyền";
            btnPhanQuyen.TextAlign = ContentAlignment.MiddleLeft;
            btnPhanQuyen.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnPhanQuyen.ImageAlign = ContentAlignment.MiddleLeft;
            btnPhanQuyen.Padding = new Padding(16, 0, 0, 0);
            btnPhanQuyen.UseVisualStyleBackColor = false;
            btnPhanQuyen.Cursor = Cursors.Hand;
            btnPhanQuyen.Click += btnPhanQuyen_Click;

            btnKhuyenMai.BackColor = Color.FromArgb(30, 41, 59);
            btnKhuyenMai.Dock = DockStyle.Top;
            btnKhuyenMai.FlatAppearance.BorderSize = 0;
            btnKhuyenMai.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
            btnKhuyenMai.FlatStyle = FlatStyle.Flat;
            btnKhuyenMai.ForeColor = Color.White;
            btnKhuyenMai.IconChar = FontAwesome.Sharp.IconChar.None;
            btnKhuyenMai.IconColor = Color.White;
            btnKhuyenMai.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnKhuyenMai.IconSize = 22;
            btnKhuyenMai.Name = "btnKhuyenMai";
            btnKhuyenMai.Size = new Size(220, 52);
            btnKhuyenMai.TabIndex = 4;
            btnKhuyenMai.Text = " Khuyến Mãi";
            btnKhuyenMai.TextAlign = ContentAlignment.MiddleLeft;
            btnKhuyenMai.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnKhuyenMai.ImageAlign = ContentAlignment.MiddleLeft;
            btnKhuyenMai.Padding = new Padding(16, 0, 0, 0);
            btnKhuyenMai.UseVisualStyleBackColor = false;
            btnKhuyenMai.Cursor = Cursors.Hand;
            btnKhuyenMai.Click += btnKhuyenMai_Click;

            btnGiaVe.BackColor = Color.FromArgb(30, 41, 59);
            btnGiaVe.Dock = DockStyle.Top;
            btnGiaVe.FlatAppearance.BorderSize = 0;
            btnGiaVe.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
            btnGiaVe.FlatStyle = FlatStyle.Flat;
            btnGiaVe.ForeColor = Color.White;
            btnGiaVe.IconChar = FontAwesome.Sharp.IconChar.None;
            btnGiaVe.IconColor = Color.White;
            btnGiaVe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGiaVe.IconSize = 22;
            btnGiaVe.Name = "btnGiaVe";
            btnGiaVe.Size = new Size(220, 52);
            btnGiaVe.TabIndex = 3;
            btnGiaVe.Text = " Giá Vé";
            btnGiaVe.TextAlign = ContentAlignment.MiddleLeft;
            btnGiaVe.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGiaVe.ImageAlign = ContentAlignment.MiddleLeft;
            btnGiaVe.Padding = new Padding(16, 0, 0, 0);
            btnGiaVe.UseVisualStyleBackColor = false;
            btnGiaVe.Cursor = Cursors.Hand;
            btnGiaVe.Click += btnGiaVe_Click;

            btnNhanSu.BackColor = Color.FromArgb(30, 41, 59);
            btnNhanSu.Dock = DockStyle.Top;
            btnNhanSu.FlatAppearance.BorderSize = 0;
            btnNhanSu.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
            btnNhanSu.FlatStyle = FlatStyle.Flat;
            btnNhanSu.ForeColor = Color.White;
            btnNhanSu.IconChar = FontAwesome.Sharp.IconChar.None;
            btnNhanSu.IconColor = Color.White;
            btnNhanSu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnNhanSu.IconSize = 22;
            btnNhanSu.Name = "btnNhanSu";
            btnNhanSu.Size = new Size(220, 52);
            btnNhanSu.TabIndex = 2;
            btnNhanSu.Text = " Nhân Sự";
            btnNhanSu.TextAlign = ContentAlignment.MiddleLeft;
            btnNhanSu.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNhanSu.ImageAlign = ContentAlignment.MiddleLeft;
            btnNhanSu.Padding = new Padding(16, 0, 0, 0);
            btnNhanSu.UseVisualStyleBackColor = false;
            btnNhanSu.Cursor = Cursors.Hand;
            btnNhanSu.Click += btnNhanSu_Click;

            btnDichVu.BackColor = Color.FromArgb(30, 41, 59);
            btnDichVu.Dock = DockStyle.Top;
            btnDichVu.FlatAppearance.BorderSize = 0;
            btnDichVu.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
            btnDichVu.FlatStyle = FlatStyle.Flat;
            btnDichVu.ForeColor = Color.White;
            btnDichVu.IconChar = FontAwesome.Sharp.IconChar.None;
            btnDichVu.IconColor = Color.White;
            btnDichVu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDichVu.IconSize = 22;
            btnDichVu.Name = "btnDichVu";
            btnDichVu.Size = new Size(220, 52);
            btnDichVu.TabIndex = 1;
            btnDichVu.Text = " Dịch Vụ";
            btnDichVu.TextAlign = ContentAlignment.MiddleLeft;
            btnDichVu.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDichVu.ImageAlign = ContentAlignment.MiddleLeft;
            btnDichVu.Padding = new Padding(16, 0, 0, 0);
            btnDichVu.UseVisualStyleBackColor = false;
            btnDichVu.Cursor = Cursors.Hand;
            btnDichVu.Click += btnDichVu_Click;

            btnTrangChu.BackColor = Color.FromArgb(30, 41, 59);
            btnTrangChu.Dock = DockStyle.Top;
            btnTrangChu.FlatAppearance.BorderSize = 0;
            btnTrangChu.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
            btnTrangChu.FlatStyle = FlatStyle.Flat;
            btnTrangChu.ForeColor = Color.White;
            btnTrangChu.IconChar = FontAwesome.Sharp.IconChar.None;
            btnTrangChu.IconColor = Color.White;
            btnTrangChu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnTrangChu.IconSize = 22;
            btnTrangChu.Name = "btnTrangChu";
            btnTrangChu.Size = new Size(220, 52);
            btnTrangChu.TabIndex = 0;
            btnTrangChu.Text = " Trang Chủ";
            btnTrangChu.TextAlign = ContentAlignment.MiddleLeft;
            btnTrangChu.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnTrangChu.ImageAlign = ContentAlignment.MiddleLeft;
            btnTrangChu.Padding = new Padding(16, 0, 0, 0);
            btnTrangChu.UseVisualStyleBackColor = false;
            btnTrangChu.Cursor = Cursors.Hand;
            btnTrangChu.Click += btnTrangChu_Click;

            // pnlHeader
            pnlHeader.BackColor = Color.FromArgb(59, 130, 246);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(980, 72);
            pnlHeader.TabIndex = 1;

            lblTitle.AutoSize = false;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.Name = "lblTitle";
            lblTitle.Padding = new Padding(20, 0, 0, 0);
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblTitle.Text = "QUẢN LÝ VẬN TẢI";

            // pnlContent
            pnlContent.BackColor = Color.FromArgb(241, 245, 249);
            pnlContent.Controls.Add(pnlCanhBao);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(0);
            pnlContent.TabIndex = 2;

            // pnlCanhBao
            pnlCanhBao.Controls.Add(btnDong);
            pnlCanhBao.Controls.Add(lblTieuDe);
            pnlCanhBao.Controls.Add(lblNoiDung);
            pnlCanhBao.Controls.Add(picCanhBao);
            pnlCanhBao.Anchor = AnchorStyles.None;
            pnlCanhBao.BackColor = Color.White;
            pnlCanhBao.BorderStyle = BorderStyle.FixedSingle;
            pnlCanhBao.Name = "pnlCanhBao";
            pnlCanhBao.Size = new Size(500, 300);
            pnlCanhBao.TabIndex = 0;
            pnlCanhBao.Visible = false;

            btnDong.Location = new Point(175, 235);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(150, 40);
            btnDong.TabIndex = 3;
            btnDong.Text = " Đóng";
            btnDong.UseVisualStyleBackColor = true;

            lblTieuDe.AutoSize = false;
            lblTieuDe.Location = new Point(20, 95);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(460, 32);
            lblTieuDe.TabIndex = 1;
            lblTieuDe.Text = "TRUY CẬP BỊ TỪ CHỐI";
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;

            lblNoiDung.AutoSize = false;
            lblNoiDung.Location = new Point(20, 135);
            lblNoiDung.Name = "lblNoiDung";
            lblNoiDung.Size = new Size(460, 85);
            lblNoiDung.TabIndex = 2;
            lblNoiDung.Text = "Bạn không có quyền truy cập chức năng này.";
            lblNoiDung.TextAlign = ContentAlignment.MiddleCenter;

            picCanhBao.Location = new Point(215, 20);
            picCanhBao.Name = "picCanhBao";
            picCanhBao.Size = new Size(70, 70);
            picCanhBao.TabIndex = 0;
            picCanhBao.TabStop = false;
            picCanhBao.SizeMode = PictureBoxSizeMode.CenterImage;

            // FormMain
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(pnlContent);
            Controls.Add(pnlHeader);
            Controls.Add(pnlSidebar);
            Name = "FormMain";
            Text = "Hệ Thống Quản Lý Vận Tải Hành Khách";
            StartPosition = FormStartPosition.CenterScreen;

            pnlSidebar.ResumeLayout(false);
            pnlSidebarTop.ResumeLayout(false);
            pnlSidebarMenu.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlCanhBao.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picCanhBao).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSidebar;
        private Panel pnlSidebarTop;
        private Panel pnlSidebarMenu;
        private Label lblAppName;
        private Panel pnlHeader;
        private Panel pnlContent;
        private FontAwesome.Sharp.IconButton btnDichVu;
        private FontAwesome.Sharp.IconButton btnTrangChu;
        private Label lblTitle;
        private FontAwesome.Sharp.IconButton btnNhanSu;
        private FontAwesome.Sharp.IconButton btnKhuyenMai;
        private FontAwesome.Sharp.IconButton btnGiaVe;
        private FontAwesome.Sharp.IconButton btnPhanQuyen;
        private FontAwesome.Sharp.IconButton btnCaiDat;
        private FontAwesome.Sharp.IconButton btnNhatKy;
        private Panel pnlCanhBao;
        private Label lblTieuDe;
        private Label lblNoiDung;
        private PictureBox picCanhBao;
        private Button btnDong;
    }
}
