namespace QuanLyVanTai.UI.Forms
{
    partial class FrmSearchTrips
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ComboBox cboPrice;
        private FontAwesome.Sharp.IconButton btnReset;
        private System.Windows.Forms.DataGridView dgvTrips;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblResultCount;
        private FontAwesome.Sharp.IconButton btnSelect;
        private FontAwesome.Sharp.IconButton btnClose;

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
            lblHeaderSubtitle = new System.Windows.Forms.Label();
            lblHeaderTitle = new System.Windows.Forms.Label();
            pnlToolbar = new System.Windows.Forms.Panel();
            btnReset = new FontAwesome.Sharp.IconButton();
            cboPrice = new System.Windows.Forms.ComboBox();
            dtpDate = new System.Windows.Forms.DateTimePicker();
            txtKeyword = new System.Windows.Forms.TextBox();
            dgvTrips = new System.Windows.Forms.DataGridView();
            pnlFooter = new System.Windows.Forms.Panel();
            btnClose = new FontAwesome.Sharp.IconButton();
            btnSelect = new FontAwesome.Sharp.IconButton();
            lblResultCount = new System.Windows.Forms.Label();
            pnlHeader.SuspendLayout();
            pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTrips).BeginInit();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            pnlHeader.Controls.Add(lblHeaderSubtitle);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            pnlHeader.Size = new System.Drawing.Size(920, 68);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            lblHeaderSubtitle.AutoSize = true;
            lblHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblHeaderSubtitle.Location = new System.Drawing.Point(20, 38);
            lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            lblHeaderSubtitle.Size = new System.Drawing.Size(564, 20);
            lblHeaderSubtitle.TabIndex = 1;
            lblHeaderSubtitle.Text = "Lọc real-time gõ đến đâu lọc đến đó (debounce 300ms), kết hợp lọc ngày, giá & highlight.";
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            lblHeaderTitle.Location = new System.Drawing.Point(20, 10);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new System.Drawing.Size(374, 28);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "🔍 TÌM KIẾM CHUYẾN XE KHẢ DỤNG";
            // 
            // pnlToolbar
            // 
            pnlToolbar.BackColor = System.Drawing.Color.White;
            pnlToolbar.Controls.Add(btnReset);
            pnlToolbar.Controls.Add(cboPrice);
            pnlToolbar.Controls.Add(dtpDate);
            pnlToolbar.Controls.Add(txtKeyword);
            pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            pnlToolbar.Location = new System.Drawing.Point(0, 68);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            pnlToolbar.Size = new System.Drawing.Size(920, 56);
            pnlToolbar.TabIndex = 1;
            // 
            // btnReset
            // 
            btnReset.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnReset.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            btnReset.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnReset.IconChar = FontAwesome.Sharp.IconChar.RotateLeft;
            btnReset.IconColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnReset.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnReset.IconSize = 14;
            btnReset.Location = new System.Drawing.Point(740, 11);
            btnReset.Name = "btnReset";
            btnReset.Size = new System.Drawing.Size(100, 31);
            btnReset.TabIndex = 3;
            btnReset.Text = " Đặt lại";
            btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnReset.UseVisualStyleBackColor = false;
            // 
            // cboPrice
            // 
            cboPrice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboPrice.FormattingEnabled = true;
            cboPrice.Location = new System.Drawing.Point(535, 12);
            cboPrice.Name = "cboPrice";
            cboPrice.Size = new System.Drawing.Size(190, 28);
            cboPrice.TabIndex = 2;
            // 
            // dtpDate
            // 
            dtpDate.CustomFormat = "dd/MM/yyyy";
            dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtpDate.Location = new System.Drawing.Point(365, 13);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new System.Drawing.Size(155, 27);
            dtpDate.TabIndex = 1;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new System.Drawing.Point(16, 13);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "🔍 Nhập tuyến đường, bến xe, biển số...";
            txtKeyword.Size = new System.Drawing.Size(335, 27);
            txtKeyword.TabIndex = 0;
            // 
            // dgvTrips
            // 
            dgvTrips.AllowUserToAddRows = false;
            dgvTrips.AllowUserToDeleteRows = false;
            dgvTrips.BackgroundColor = System.Drawing.Color.White;
            dgvTrips.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvTrips.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTrips.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvTrips.Location = new System.Drawing.Point(0, 124);
            dgvTrips.Name = "dgvTrips";
            dgvTrips.ReadOnly = true;
            dgvTrips.RowHeadersVisible = false;
            dgvTrips.RowHeadersWidth = 51;
            dgvTrips.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvTrips.Size = new System.Drawing.Size(920, 412);
            dgvTrips.TabIndex = 2;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = System.Drawing.Color.White;
            pnlFooter.Controls.Add(btnClose);
            pnlFooter.Controls.Add(btnSelect);
            pnlFooter.Controls.Add(lblResultCount);
            pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlFooter.Location = new System.Drawing.Point(0, 536);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            pnlFooter.Size = new System.Drawing.Size(920, 54);
            pnlFooter.TabIndex = 3;
            // 
            // btnClose
            // 
            btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClose.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnClose.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnClose.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            btnClose.IconColor = System.Drawing.Color.FromArgb(100, 116, 139);
            btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClose.IconSize = 16;
            btnClose.Location = new System.Drawing.Point(804, 9);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(100, 36);
            btnClose.TabIndex = 2;
            btnClose.Text = " Đóng";
            btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // btnSelect
            // 
            btnSelect.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnSelect.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            btnSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            btnSelect.FlatAppearance.BorderSize = 0;
            btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSelect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnSelect.ForeColor = System.Drawing.Color.White;
            btnSelect.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnSelect.IconColor = System.Drawing.Color.White;
            btnSelect.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSelect.IconSize = 16;
            btnSelect.Location = new System.Drawing.Point(628, 9);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new System.Drawing.Size(170, 36);
            btnSelect.TabIndex = 1;
            btnSelect.Text = " Chọn chuyến này";
            btnSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnSelect.UseVisualStyleBackColor = false;
            // 
            // lblResultCount
            // 
            lblResultCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblResultCount.AutoSize = true;
            lblResultCount.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            lblResultCount.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblResultCount.Location = new System.Drawing.Point(16, 16);
            lblResultCount.Name = "lblResultCount";
            lblResultCount.Size = new System.Drawing.Size(200, 21);
            lblResultCount.TabIndex = 0;
            lblResultCount.Text = "Tìm thấy: 0 chuyến khả dụng";
            // 
            // FrmSearchTrips
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(920, 590);
            Controls.Add(dgvTrips);
            Controls.Add(pnlFooter);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeader);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            MinimumSize = new System.Drawing.Size(860, 500);
            Name = "FrmSearchTrips";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Tìm Kiếm Chuyến Xe";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlToolbar.ResumeLayout(false);
            pnlToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTrips).EndInit();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
