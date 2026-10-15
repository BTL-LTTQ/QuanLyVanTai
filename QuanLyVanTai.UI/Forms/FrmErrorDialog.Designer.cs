namespace QuanLyVanTai.UI.Forms
{
    partial class FrmErrorDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private FontAwesome.Sharp.IconPictureBox picIcon;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.Label lblFriendly;
        private System.Windows.Forms.Panel pnlButtons;
        private FontAwesome.Sharp.IconButton btnContinue;
        private FontAwesome.Sharp.IconButton btnOpenLogs;
        private FontAwesome.Sharp.IconButton btnToggleDetails;
        private System.Windows.Forms.TextBox txtDetails;

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
            pnlHeader = new System.Windows.Forms.Panel();
            picIcon = new FontAwesome.Sharp.IconPictureBox();
            lblHeaderTitle = new System.Windows.Forms.Label();
            lblHeaderSubtitle = new System.Windows.Forms.Label();
            lblFriendly = new System.Windows.Forms.Label();
            pnlButtons = new System.Windows.Forms.Panel();
            btnContinue = new FontAwesome.Sharp.IconButton();
            btnOpenLogs = new FontAwesome.Sharp.IconButton();
            btnToggleDetails = new FontAwesome.Sharp.IconButton();
            txtDetails = new System.Windows.Forms.TextBox();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picIcon).BeginInit();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = System.Drawing.Color.FromArgb(254, 242, 242);
            pnlHeader.Controls.Add(picIcon);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Controls.Add(lblHeaderSubtitle);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(580, 65);
            pnlHeader.TabIndex = 0;
            // 
            // picIcon
            // 
            picIcon.BackColor = System.Drawing.Color.Transparent;
            picIcon.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            picIcon.IconChar = FontAwesome.Sharp.IconChar.TriangleExclamation;
            picIcon.IconColor = System.Drawing.Color.FromArgb(239, 68, 68);
            picIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            picIcon.IconSize = 40;
            picIcon.Location = new System.Drawing.Point(20, 12);
            picIcon.Name = "picIcon";
            picIcon.Size = new System.Drawing.Size(40, 40);
            picIcon.TabIndex = 0;
            picIcon.TabStop = false;
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            lblHeaderTitle.Location = new System.Drawing.Point(70, 13);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new System.Drawing.Size(437, 25);
            lblHeaderTitle.TabIndex = 1;
            lblHeaderTitle.Text = "ỨNG DỤNG ĐÃ BẢO VỆ VÀ NGĂN CHẶN CRASH";
            // 
            // lblHeaderSubtitle
            // 
            lblHeaderSubtitle.AutoSize = true;
            lblHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblHeaderSubtitle.Location = new System.Drawing.Point(70, 36);
            lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            lblHeaderSubtitle.Size = new System.Drawing.Size(418, 20);
            lblHeaderSubtitle.TabIndex = 2;
            lblHeaderSubtitle.Text = "Cơ chế Global Exception Handler đã bắt được ngoại lệ an toàn.";
            // 
            // lblFriendly
            // 
            lblFriendly.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblFriendly.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblFriendly.Location = new System.Drawing.Point(25, 80);
            lblFriendly.Name = "lblFriendly";
            lblFriendly.Size = new System.Drawing.Size(520, 75);
            lblFriendly.TabIndex = 1;
            lblFriendly.Text = "Thông báo lỗi thân thiện cho người dùng";
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnContinue);
            pnlButtons.Controls.Add(btnOpenLogs);
            pnlButtons.Controls.Add(btnToggleDetails);
            pnlButtons.Location = new System.Drawing.Point(25, 175);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new System.Drawing.Size(520, 45);
            pnlButtons.TabIndex = 2;
            // 
            // btnContinue
            // 
            btnContinue.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnContinue.Cursor = System.Windows.Forms.Cursors.Hand;
            btnContinue.FlatAppearance.BorderSize = 0;
            btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnContinue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnContinue.ForeColor = System.Drawing.Color.White;
            btnContinue.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnContinue.IconColor = System.Drawing.Color.White;
            btnContinue.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnContinue.IconSize = 18;
            btnContinue.Location = new System.Drawing.Point(390, 2);
            btnContinue.Name = "btnContinue";
            btnContinue.Size = new System.Drawing.Size(130, 38);
            btnContinue.TabIndex = 0;
            btnContinue.Text = " Tiếp tục";
            btnContinue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnContinue.UseVisualStyleBackColor = false;
            // 
            // btnOpenLogs
            // 
            btnOpenLogs.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            btnOpenLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            btnOpenLogs.FlatAppearance.BorderSize = 0;
            btnOpenLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnOpenLogs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnOpenLogs.ForeColor = System.Drawing.Color.White;
            btnOpenLogs.IconChar = FontAwesome.Sharp.IconChar.FolderOpen;
            btnOpenLogs.IconColor = System.Drawing.Color.White;
            btnOpenLogs.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnOpenLogs.IconSize = 18;
            btnOpenLogs.Location = new System.Drawing.Point(220, 2);
            btnOpenLogs.Name = "btnOpenLogs";
            btnOpenLogs.Size = new System.Drawing.Size(160, 38);
            btnOpenLogs.TabIndex = 1;
            btnOpenLogs.Text = " Mở thư mục Log";
            btnOpenLogs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnOpenLogs.UseVisualStyleBackColor = false;
            // 
            // btnToggleDetails
            // 
            btnToggleDetails.BackColor = System.Drawing.Color.Transparent;
            btnToggleDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            btnToggleDetails.FlatAppearance.BorderSize = 0;
            btnToggleDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnToggleDetails.Font = new System.Drawing.Font("Segoe UI", 9F);
            btnToggleDetails.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            btnToggleDetails.IconChar = FontAwesome.Sharp.IconChar.None;
            btnToggleDetails.IconColor = System.Drawing.Color.Black;
            btnToggleDetails.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnToggleDetails.Location = new System.Drawing.Point(0, 2);
            btnToggleDetails.Name = "btnToggleDetails";
            btnToggleDetails.Size = new System.Drawing.Size(190, 38);
            btnToggleDetails.TabIndex = 2;
            btnToggleDetails.Text = " Xem chi tiết kỹ thuật ▼";
            btnToggleDetails.UseVisualStyleBackColor = false;
            // 
            // txtDetails
            // 
            txtDetails.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtDetails.Font = new System.Drawing.Font("Consolas", 8.5F);
            txtDetails.Location = new System.Drawing.Point(25, 230);
            txtDetails.Multiline = true;
            txtDetails.Name = "txtDetails";
            txtDetails.ReadOnly = true;
            txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            txtDetails.Size = new System.Drawing.Size(520, 160);
            txtDetails.TabIndex = 3;
            txtDetails.Visible = false;
            // 
            // FrmErrorDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            ClientSize = new System.Drawing.Size(580, 290);
            Controls.Add(txtDetails);
            Controls.Add(pnlButtons);
            Controls.Add(lblFriendly);
            Controls.Add(pnlHeader);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new System.Drawing.Size(580, 290);
            Name = "FrmErrorDialog";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Sự Cố Hệ Thống - Cơ Chế Bắt Lỗi Toàn Cục";
            TopMost = true;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picIcon).EndInit();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
