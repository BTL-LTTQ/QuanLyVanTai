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
            btnDichVu = new Button();
            btnTrangChu = new Button();
            pnlHeader = new Panel();
            pnlContent = new Panel();
            pnlSidebar.SuspendLayout();
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
            pnlSidebar.Size = new Size(216, 450);
            pnlSidebar.TabIndex = 0;
            // 
            // btnDichVu
            // 
            btnDichVu.Dock = DockStyle.Top;
            btnDichVu.FlatAppearance.BorderSize = 0;
            btnDichVu.FlatStyle = FlatStyle.Flat;
            btnDichVu.Location = new Point(0, 29);
            btnDichVu.Name = "btnDichVu";
            btnDichVu.Size = new Size(216, 29);
            btnDichVu.TabIndex = 1;
            btnDichVu.Text = "Dịch vụ";
            btnDichVu.UseVisualStyleBackColor = true;
            // 
            // btnTrangChu
            // 
            btnTrangChu.Dock = DockStyle.Top;
            btnTrangChu.FlatAppearance.BorderSize = 0;
            btnTrangChu.FlatStyle = FlatStyle.Flat;
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
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(216, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(584, 75);
            pnlHeader.TabIndex = 1;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = SystemColors.ControlDark;
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(216, 75);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(584, 375);
            pnlContent.TabIndex = 2;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlContent);
            Controls.Add(pnlHeader);
            Controls.Add(pnlSidebar);
            Name = "FormMain";
            Text = "Form1";
            pnlSidebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSidebar;
        private Panel pnlHeader;
        private Panel pnlContent;
        private Button btnDichVu;
        private Button btnTrangChu;
    }
}
