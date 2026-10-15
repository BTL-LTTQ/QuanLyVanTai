namespace QuanLyVanTai.UI.UserControls
{
    partial class UcManageStaff
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.Label lblStatTotal;
        private System.Windows.Forms.Label lblStatActive;
        private System.Windows.Forms.Label lblStatLocked;
        private System.Windows.Forms.Label lblStatAdmin;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.ComboBox cboRole;
        private System.Windows.Forms.ComboBox cboStatus;
        private FontAwesome.Sharp.IconButton btnRefresh;
        private FontAwesome.Sharp.IconButton btnAdd;
        private FontAwesome.Sharp.IconButton btnExport;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.DataGridView dgvStaff;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.ComboBox cboRole2;
        private System.Windows.Forms.ComboBox cboDetailStatus;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblPasswordHint;
        private FontAwesome.Sharp.IconButton btnSave;
        private FontAwesome.Sharp.IconButton btnCancel;
        private FontAwesome.Sharp.IconButton btnDelete;
        private FontAwesome.Sharp.IconButton btnResetPwd;

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
            pnlHeader = new System.Windows.Forms.Panel();
            lblHeaderTitle = new System.Windows.Forms.Label();
            pnlStats = new System.Windows.Forms.Panel();
            pnlToolbar = new System.Windows.Forms.Panel();
            txtKeyword = new System.Windows.Forms.TextBox();
            cboRole = new System.Windows.Forms.ComboBox();
            cboStatus = new System.Windows.Forms.ComboBox();
            btnRefresh = new FontAwesome.Sharp.IconButton();
            btnAdd = new FontAwesome.Sharp.IconButton();
            btnExport = new FontAwesome.Sharp.IconButton();
            splitMain = new System.Windows.Forms.SplitContainer();
            dgvStaff = new System.Windows.Forms.DataGridView();
            pnlDetail = new System.Windows.Forms.Panel();
            lblDetailTitle = new System.Windows.Forms.Label();
            txtUsername = new System.Windows.Forms.TextBox();
            txtFullName = new System.Windows.Forms.TextBox();
            txtEmail = new System.Windows.Forms.TextBox();
            txtPhone = new System.Windows.Forms.TextBox();
            cboRole2 = new System.Windows.Forms.ComboBox();
            cboDetailStatus = new System.Windows.Forms.ComboBox();
            txtNewPassword = new System.Windows.Forms.TextBox();
            lblPasswordHint = new System.Windows.Forms.Label();
            btnSave = new FontAwesome.Sharp.IconButton();
            btnCancel = new FontAwesome.Sharp.IconButton();
            btnDelete = new FontAwesome.Sharp.IconButton();
            btnResetPwd = new FontAwesome.Sharp.IconButton();
            pnlHeader.SuspendLayout();
            pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStaff).BeginInit();
            pnlDetail.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = System.Drawing.Color.White;
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            pnlHeader.Size = new System.Drawing.Size(1200, 50);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblHeaderTitle.Location = new System.Drawing.Point(20, 12);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new System.Drawing.Size(1160, 26);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "👥 QUẢN LÝ TÀI KHOẢN NHÂN VIÊN";
            lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlStats
            // 
            pnlStats.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            pnlStats.Dock = System.Windows.Forms.DockStyle.Top;
            pnlStats.Location = new System.Drawing.Point(0, 50);
            pnlStats.Name = "pnlStats";
            pnlStats.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            pnlStats.Size = new System.Drawing.Size(1200, 80);
            pnlStats.TabIndex = 1;
            // 
            // pnlToolbar
            // 
            pnlToolbar.BackColor = System.Drawing.Color.White;
            pnlToolbar.Controls.Add(btnExport);
            pnlToolbar.Controls.Add(btnAdd);
            pnlToolbar.Controls.Add(btnRefresh);
            pnlToolbar.Controls.Add(cboStatus);
            pnlToolbar.Controls.Add(cboRole);
            pnlToolbar.Controls.Add(txtKeyword);
            pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            pnlToolbar.Location = new System.Drawing.Point(0, 130);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            pnlToolbar.Size = new System.Drawing.Size(1200, 58);
            pnlToolbar.TabIndex = 2;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new System.Drawing.Point(15, 13);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "🔍 Tên đăng nhập, họ tên, email...";
            txtKeyword.Size = new System.Drawing.Size(220, 27);
            txtKeyword.TabIndex = 0;
            // 
            // cboRole
            // 
            cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboRole.FormattingEnabled = true;
            cboRole.Location = new System.Drawing.Point(245, 13);
            cboRole.Name = "cboRole";
            cboRole.Size = new System.Drawing.Size(160, 28);
            cboRole.TabIndex = 1;
            // 
            // cboStatus
            // 
            cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboStatus.FormattingEnabled = true;
            cboStatus.Location = new System.Drawing.Point(415, 13);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new System.Drawing.Size(110, 28);
            cboStatus.TabIndex = 2;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnRefresh.ForeColor = System.Drawing.Color.White;
            btnRefresh.IconChar = FontAwesome.Sharp.IconChar.RotateLeft;
            btnRefresh.IconColor = System.Drawing.Color.White;
            btnRefresh.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRefresh.IconSize = 16;
            btnRefresh.Location = new System.Drawing.Point(535, 13);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(105, 30);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = " Làm mới";
            btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnAdd.ForeColor = System.Drawing.Color.White;
            btnAdd.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            btnAdd.IconColor = System.Drawing.Color.White;
            btnAdd.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAdd.IconSize = 16;
            btnAdd.Location = new System.Drawing.Point(648, 13);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(95, 30);
            btnAdd.TabIndex = 4;
            btnAdd.Text = " Thêm";
            btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnExport
            // 
            btnExport.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnExport.ForeColor = System.Drawing.Color.White;
            btnExport.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            btnExport.IconColor = System.Drawing.Color.White;
            btnExport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnExport.IconSize = 16;
            btnExport.Location = new System.Drawing.Point(750, 13);
            btnExport.Name = "btnExport";
            btnExport.Size = new System.Drawing.Size(110, 30);
            btnExport.TabIndex = 5;
            btnExport.Text = " Xuất file";
            btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnExport.UseVisualStyleBackColor = false;
            // 
            // splitMain
            // 
            splitMain.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            splitMain.Location = new System.Drawing.Point(0, 188);
            splitMain.Name = "splitMain";
            splitMain.Panel1MinSize = 450;
            splitMain.Panel2MinSize = 350;
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(dgvStaff);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(pnlDetail);
            splitMain.Size = new System.Drawing.Size(1200, 562);
            splitMain.SplitterDistance = 750;
            splitMain.SplitterWidth = 6;
            splitMain.TabIndex = 3;
            // 
            // dgvStaff
            // 
            dgvStaff.AllowUserToAddRows = false;
            dgvStaff.AllowUserToDeleteRows = false;
            dgvStaff.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvStaff.BackgroundColor = System.Drawing.Color.White;
            dgvStaff.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvStaff.ColumnHeadersHeight = 42;
            dgvStaff.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvStaff.EnableHeadersVisualStyles = false;
            dgvStaff.Location = new System.Drawing.Point(0, 0);
            dgvStaff.MultiSelect = false;
            dgvStaff.Name = "dgvStaff";
            dgvStaff.ReadOnly = true;
            dgvStaff.RowHeadersVisible = false;
            dgvStaff.RowTemplate.Height = 42;
            dgvStaff.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvStaff.Size = new System.Drawing.Size(750, 562);
            dgvStaff.TabIndex = 0;
            // 
            // pnlDetail
            // 
            pnlDetail.AutoScroll = true;
            pnlDetail.BackColor = System.Drawing.Color.White;
            pnlDetail.Controls.Add(btnResetPwd);
            pnlDetail.Controls.Add(btnDelete);
            pnlDetail.Controls.Add(btnCancel);
            pnlDetail.Controls.Add(btnSave);
            pnlDetail.Controls.Add(lblPasswordHint);
            pnlDetail.Controls.Add(txtNewPassword);
            pnlDetail.Controls.Add(cboDetailStatus);
            pnlDetail.Controls.Add(cboRole2);
            pnlDetail.Controls.Add(txtPhone);
            pnlDetail.Controls.Add(txtEmail);
            pnlDetail.Controls.Add(txtFullName);
            pnlDetail.Controls.Add(txtUsername);
            pnlDetail.Controls.Add(lblDetailTitle);
            pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlDetail.Location = new System.Drawing.Point(0, 0);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Padding = new System.Windows.Forms.Padding(18);
            pnlDetail.Size = new System.Drawing.Size(444, 562);
            pnlDetail.TabIndex = 0;
            // 
            // lblDetailTitle
            // 
            lblDetailTitle.AutoSize = true;
            lblDetailTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblDetailTitle.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            lblDetailTitle.Location = new System.Drawing.Point(18, 15);
            lblDetailTitle.Name = "lblDetailTitle";
            lblDetailTitle.Size = new System.Drawing.Size(199, 25);
            lblDetailTitle.TabIndex = 0;
            lblDetailTitle.Text = "CHI TIẾT TÀI KHOẢN";
            // 
            // txtUsername
            // 
            txtUsername.Location = new System.Drawing.Point(18, 72);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new System.Drawing.Size(330, 27);
            txtUsername.TabIndex = 1;
            // 
            // txtFullName
            // 
            txtFullName.Location = new System.Drawing.Point(18, 132);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new System.Drawing.Size(330, 27);
            txtFullName.TabIndex = 2;
            // 
            // txtEmail
            // 
            txtEmail.Location = new System.Drawing.Point(18, 192);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new System.Drawing.Size(330, 27);
            txtEmail.TabIndex = 3;
            // 
            // txtPhone
            // 
            txtPhone.Location = new System.Drawing.Point(18, 252);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new System.Drawing.Size(330, 27);
            txtPhone.TabIndex = 4;
            // 
            // cboRole2
            // 
            cboRole2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboRole2.FormattingEnabled = true;
            cboRole2.Location = new System.Drawing.Point(18, 312);
            cboRole2.Name = "cboRole2";
            cboRole2.Size = new System.Drawing.Size(330, 28);
            cboRole2.TabIndex = 5;
            // 
            // cboDetailStatus
            // 
            cboDetailStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboDetailStatus.FormattingEnabled = true;
            cboDetailStatus.Location = new System.Drawing.Point(18, 372);
            cboDetailStatus.Name = "cboDetailStatus";
            cboDetailStatus.Size = new System.Drawing.Size(330, 28);
            cboDetailStatus.TabIndex = 6;
            // 
            // txtNewPassword
            // 
            txtNewPassword.Location = new System.Drawing.Point(18, 432);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.Size = new System.Drawing.Size(330, 27);
            txtNewPassword.TabIndex = 7;
            txtNewPassword.UseSystemPasswordChar = true;
            // 
            // lblPasswordHint
            // 
            lblPasswordHint.AutoSize = true;
            lblPasswordHint.Font = new System.Drawing.Font("Segoe UI", 8F);
            lblPasswordHint.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblPasswordHint.Location = new System.Drawing.Point(18, 462);
            lblPasswordHint.Name = "lblPasswordHint";
            lblPasswordHint.Size = new System.Drawing.Size(262, 19);
            lblPasswordHint.TabIndex = 8;
            lblPasswordHint.Text = "Để trống nếu không muốn đổi mật khẩu";
            // 
            // btnSave
            // 
            btnSave.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            btnSave.IconColor = System.Drawing.Color.White;
            btnSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSave.IconSize = 16;
            btnSave.Location = new System.Drawing.Point(18, 492);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(85, 36);
            btnSave.TabIndex = 9;
            btnSave.Text = " Lưu";
            btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnCancel.ForeColor = System.Drawing.Color.White;
            btnCancel.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            btnCancel.IconColor = System.Drawing.Color.White;
            btnCancel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancel.IconSize = 16;
            btnCancel.Location = new System.Drawing.Point(109, 492);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(85, 36);
            btnCancel.TabIndex = 10;
            btnCancel.Text = " Hủy";
            btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
            btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnDelete.ForeColor = System.Drawing.Color.White;
            btnDelete.IconChar = FontAwesome.Sharp.IconChar.Trash;
            btnDelete.IconColor = System.Drawing.Color.White;
            btnDelete.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDelete.IconSize = 16;
            btnDelete.Location = new System.Drawing.Point(200, 492);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(85, 36);
            btnDelete.TabIndex = 11;
            btnDelete.Text = " Xóa";
            btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnResetPwd
            // 
            btnResetPwd.BackColor = System.Drawing.Color.FromArgb(245, 158, 11);
            btnResetPwd.Cursor = System.Windows.Forms.Cursors.Hand;
            btnResetPwd.FlatAppearance.BorderSize = 0;
            btnResetPwd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnResetPwd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnResetPwd.ForeColor = System.Drawing.Color.White;
            btnResetPwd.IconChar = FontAwesome.Sharp.IconChar.Key;
            btnResetPwd.IconColor = System.Drawing.Color.White;
            btnResetPwd.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnResetPwd.IconSize = 16;
            btnResetPwd.Location = new System.Drawing.Point(291, 492);
            btnResetPwd.Name = "btnResetPwd";
            btnResetPwd.Size = new System.Drawing.Size(110, 36);
            btnResetPwd.TabIndex = 12;
            btnResetPwd.Text = " Đặt lại MK";
            btnResetPwd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnResetPwd.UseVisualStyleBackColor = false;
            // 
            // UcManageStaff
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            Controls.Add(splitMain);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlStats);
            Controls.Add(pnlHeader);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "UcManageStaff";
            Size = new System.Drawing.Size(1200, 750);
            pnlHeader.ResumeLayout(false);
            pnlToolbar.ResumeLayout(false);
            pnlToolbar.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            splitMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvStaff).EndInit();
            pnlDetail.ResumeLayout(false);
            pnlDetail.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
