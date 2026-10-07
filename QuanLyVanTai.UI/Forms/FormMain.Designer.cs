namespace QuanLyVanTai.UI
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlSidebar = new Panel();
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
            pnlHeader.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlCanhBao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCanhBao).BeginInit();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.White;
            pnlSidebar.Controls.Add(btnCaiDat);
            pnlSidebar.Controls.Add(btnNhatKy);
            pnlSidebar.Controls.Add(btnPhanQuyen);
            pnlSidebar.Controls.Add(btnKhuyenMai);
            pnlSidebar.Controls.Add(btnGiaVe);
            pnlSidebar.Controls.Add(btnNhanSu);
            pnlSidebar.Controls.Add(btnDichVu);
            pnlSidebar.Controls.Add(btnTrangChu);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(170, 546);
            pnlSidebar.TabIndex = 0;
            // 
            // btnCaiDat
            // 
            btnCaiDat.BackColor = Color.White;
            btnCaiDat.Dock = DockStyle.Top;
            btnCaiDat.FlatAppearance.BorderSize = 0;
            btnCaiDat.FlatStyle = FlatStyle.Flat;
            btnCaiDat.IconChar = FontAwesome.Sharp.IconChar.None;
            btnCaiDat.IconColor = Color.Black;
            btnCaiDat.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCaiDat.Location = new Point(0, 285);
            btnCaiDat.Name = "btnCaiDat";
            btnCaiDat.Size = new Size(170, 40);
            btnCaiDat.TabIndex = 8;
            btnCaiDat.Text = "Cài Đặt";
            btnCaiDat.UseVisualStyleBackColor = false;
            // 
            // btnNhatKy
            // 
            btnNhatKy.BackColor = Color.White;
            btnNhatKy.Dock = DockStyle.Top;
            btnNhatKy.FlatAppearance.BorderSize = 0;
            btnNhatKy.FlatStyle = FlatStyle.Flat;
            btnNhatKy.IconChar = FontAwesome.Sharp.IconChar.None;
            btnNhatKy.IconColor = Color.Black;
            btnNhatKy.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnNhatKy.Location = new Point(0, 245);
            btnNhatKy.Name = "btnNhatKy";
            btnNhatKy.Size = new Size(170, 40);
            btnNhatKy.TabIndex = 7;
            btnNhatKy.Text = "Nhật Ký";
            btnNhatKy.UseVisualStyleBackColor = false;
            btnNhatKy.Click += btnNhatKy_Click;
            // 
            // btnPhanQuyen
            // 
            btnPhanQuyen.BackColor = Color.White;
            btnPhanQuyen.Dock = DockStyle.Top;
            btnPhanQuyen.FlatAppearance.BorderSize = 0;
            btnPhanQuyen.FlatStyle = FlatStyle.Flat;
            btnPhanQuyen.IconChar = FontAwesome.Sharp.IconChar.None;
            btnPhanQuyen.IconColor = Color.Black;
            btnPhanQuyen.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnPhanQuyen.Location = new Point(0, 205);
            btnPhanQuyen.Name = "btnPhanQuyen";
            btnPhanQuyen.Size = new Size(170, 40);
            btnPhanQuyen.TabIndex = 6;
            btnPhanQuyen.Text = "Phân Quyền";
            btnPhanQuyen.UseVisualStyleBackColor = false;
            btnPhanQuyen.Click += btnPhanQuyen_Click;
            // 
            // btnKhuyenMai
            // 
            btnKhuyenMai.BackColor = Color.White;
            btnKhuyenMai.Dock = DockStyle.Top;
            btnKhuyenMai.FlatAppearance.BorderSize = 0;
            btnKhuyenMai.FlatStyle = FlatStyle.Flat;
            btnKhuyenMai.IconChar = FontAwesome.Sharp.IconChar.None;
            btnKhuyenMai.IconColor = Color.Black;
            btnKhuyenMai.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnKhuyenMai.Location = new Point(0, 165);
            btnKhuyenMai.Name = "btnKhuyenMai";
            btnKhuyenMai.Size = new Size(170, 40);
            btnKhuyenMai.TabIndex = 4;
            btnKhuyenMai.Text = "Khuyến Mãi";
            btnKhuyenMai.UseVisualStyleBackColor = false;
            btnKhuyenMai.Click += btnKhuyenMai_Click;
            // 
            // btnGiaVe
            // 
            btnGiaVe.BackColor = Color.White;
            btnGiaVe.Dock = DockStyle.Top;
            btnGiaVe.FlatAppearance.BorderSize = 0;
            btnGiaVe.FlatStyle = FlatStyle.Flat;
            btnGiaVe.IconChar = FontAwesome.Sharp.IconChar.None;
            btnGiaVe.IconColor = Color.Black;
            btnGiaVe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGiaVe.Location = new Point(0, 125);
            btnGiaVe.Name = "btnGiaVe";
            btnGiaVe.Size = new Size(170, 40);
            btnGiaVe.TabIndex = 3;
            btnGiaVe.Text = "Giá Vé";
            btnGiaVe.UseVisualStyleBackColor = false;
            btnGiaVe.Click += btnGiaVe_Click;
            // 
            // btnNhanSu
            // 
            btnNhanSu.BackColor = Color.White;
            btnNhanSu.Dock = DockStyle.Top;
            btnNhanSu.FlatAppearance.BorderSize = 0;
            btnNhanSu.FlatStyle = FlatStyle.Flat;
            btnNhanSu.IconChar = FontAwesome.Sharp.IconChar.None;
            btnNhanSu.IconColor = Color.Black;
            btnNhanSu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnNhanSu.Location = new Point(0, 85);
            btnNhanSu.Name = "btnNhanSu";
            btnNhanSu.Size = new Size(170, 40);
            btnNhanSu.TabIndex = 2;
            btnNhanSu.Text = "Nhân Sự";
            btnNhanSu.UseVisualStyleBackColor = false;
            btnNhanSu.Click += btnNhanSu_Click;
            // 
            // btnDichVu
            // 
            btnDichVu.BackColor = Color.White;
            btnDichVu.Dock = DockStyle.Top;
            btnDichVu.FlatAppearance.BorderSize = 0;
            btnDichVu.FlatStyle = FlatStyle.Flat;
            btnDichVu.IconChar = FontAwesome.Sharp.IconChar.None;
            btnDichVu.IconColor = Color.Black;
            btnDichVu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDichVu.Location = new Point(0, 45);
            btnDichVu.Name = "btnDichVu";
            btnDichVu.Size = new Size(170, 40);
            btnDichVu.TabIndex = 1;
            btnDichVu.Text = "Dịch vụ";
            btnDichVu.UseVisualStyleBackColor = false;
            btnDichVu.Click += btnDichVu_Click;
            // 
            // btnTrangChu
            // 
            btnTrangChu.BackColor = Color.White;
            btnTrangChu.Dock = DockStyle.Top;
            btnTrangChu.FlatAppearance.BorderSize = 0;
            btnTrangChu.FlatStyle = FlatStyle.Flat;
            btnTrangChu.ForeColor = Color.Black;
            btnTrangChu.IconChar = FontAwesome.Sharp.IconChar.None;
            btnTrangChu.IconColor = Color.Black;
            btnTrangChu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnTrangChu.Location = new Point(0, 0);
            btnTrangChu.Name = "btnTrangChu";
            btnTrangChu.Size = new Size(170, 45);
            btnTrangChu.TabIndex = 0;
            btnTrangChu.Text = "Trang Chủ";
            btnTrangChu.UseVisualStyleBackColor = false;
            btnTrangChu.Click += btnTrangChu_Click;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = SystemColors.AppWorkspace;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(170, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(987, 75);
            pnlHeader.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(129, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ VẬN TẢI";
            // 
            // pnlContent
            // 
            pnlContent.BackColor = SystemColors.ControlDark;
            pnlContent.Controls.Add(pnlCanhBao);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(170, 75);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(987, 471);
            pnlContent.TabIndex = 2;
            // 
            // pnlCanhBao
            // 
            pnlCanhBao.Controls.Add(btnDong);
            pnlCanhBao.Controls.Add(lblTieuDe);
            pnlCanhBao.Controls.Add(lblNoiDung);
            pnlCanhBao.Controls.Add(picCanhBao);
            pnlCanhBao.Location = new Point(282, 50);
            pnlCanhBao.Name = "pnlCanhBao";
            pnlCanhBao.Size = new Size(360, 331);
            pnlCanhBao.TabIndex = 0;
            pnlCanhBao.Visible = false;
            // 
            // btnDong
            // 
            btnDong.Location = new Point(141, 248);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(94, 29);
            btnDong.TabIndex = 3;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Location = new Point(141, 140);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(72, 20);
            lblTieuDe.TabIndex = 1;
            lblTieuDe.Text = "Cảnh báo";
            // 
            // lblNoiDung
            // 
            lblNoiDung.AutoSize = true;
            lblNoiDung.Location = new Point(34, 200);
            lblNoiDung.Name = "lblNoiDung";
            lblNoiDung.Size = new Size(302, 20);
            lblNoiDung.TabIndex = 2;
            lblNoiDung.Text = "Bạn không có quyền truy cập chức năng này.";
            // 
            // picCanhBao
            // 
            picCanhBao.Location = new Point(120, 58);
            picCanhBao.Name = "picCanhBao";
            picCanhBao.Size = new Size(125, 62);
            picCanhBao.TabIndex = 0;
            picCanhBao.TabStop = false;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1157, 546);
            Controls.Add(pnlContent);
            Controls.Add(pnlHeader);
            Controls.Add(pnlSidebar);
            Name = "FormMain";
            Text = "Form1";
            pnlSidebar.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlContent.ResumeLayout(false);
            pnlCanhBao.ResumeLayout(false);
            pnlCanhBao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCanhBao).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSidebar;
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
