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
            btnDichVu = new FontAwesome.Sharp.IconButton();
            btnTrangChu = new FontAwesome.Sharp.IconButton();
            pnlHeader = new Panel();
            lblTitle = new Label();
            pnlContent = new Panel();
            pnlSidebar.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = SystemColors.ActiveBorder;
            pnlSidebar.Controls.Add(btnDichVu);
            pnlSidebar.Controls.Add(btnTrangChu);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(216, 538);
            pnlSidebar.TabIndex = 0;
            // 
            // btnDichVu
            // 
            btnDichVu.Dock = DockStyle.Top;
            btnDichVu.FlatAppearance.BorderSize = 0;
            btnDichVu.FlatStyle = FlatStyle.Flat;
            btnDichVu.IconChar = FontAwesome.Sharp.IconChar.None;
            btnDichVu.IconColor = Color.Black;
            btnDichVu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDichVu.Location = new Point(0, 29);
            btnDichVu.Name = "btnDichVu";
            btnDichVu.Size = new Size(216, 29);
            btnDichVu.TabIndex = 1;
            btnDichVu.Text = "Dịch vụ";
            btnDichVu.UseVisualStyleBackColor = true;
            btnDichVu.Click += btnDichVu_Click;
            // 
            // btnTrangChu
            // 
            btnTrangChu.Dock = DockStyle.Top;
            btnTrangChu.FlatAppearance.BorderSize = 0;
            btnTrangChu.FlatStyle = FlatStyle.Flat;
            btnTrangChu.IconChar = FontAwesome.Sharp.IconChar.None;
            btnTrangChu.IconColor = Color.Black;
            btnTrangChu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnTrangChu.Location = new Point(0, 0);
            btnTrangChu.Name = "btnTrangChu";
            btnTrangChu.Size = new Size(216, 29);
            btnTrangChu.TabIndex = 0;
            btnTrangChu.Text = "Trang Chủ";
            btnTrangChu.UseVisualStyleBackColor = true;
            btnTrangChu.Click += btnTrangChu_Click;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = SystemColors.AppWorkspace;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(216, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(939, 75);
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
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(216, 75);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(939, 463);
            pnlContent.TabIndex = 2;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1155, 538);
            Controls.Add(pnlContent);
            Controls.Add(pnlHeader);
            Controls.Add(pnlSidebar);
            Name = "FormMain";
            Text = "Form1";
            pnlSidebar.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSidebar;
        private Panel pnlHeader;
        private Panel pnlContent;
        private FontAwesome.Sharp.IconButton btnDichVu;
        private FontAwesome.Sharp.IconButton btnTrangChu;
        private Label lblTitle;
    }
}
