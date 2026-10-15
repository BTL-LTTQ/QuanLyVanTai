namespace QuanLyVanTai.UI.Forms
{
    partial class FrmConfirmDelete
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox picWarning;
        private System.Windows.Forms.Label lblPrompt;
        private System.Windows.Forms.Label lblItemInfo;
        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.RadioButton radSoftDelete;
        private System.Windows.Forms.RadioButton radHardDelete;
        private FontAwesome.Sharp.IconButton btnConfirm;
        private FontAwesome.Sharp.IconButton btnCancel;

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
            picWarning = new System.Windows.Forms.PictureBox();
            lblPrompt = new System.Windows.Forms.Label();
            lblItemInfo = new System.Windows.Forms.Label();
            pnlOptions = new System.Windows.Forms.Panel();
            radSoftDelete = new System.Windows.Forms.RadioButton();
            radHardDelete = new System.Windows.Forms.RadioButton();
            btnConfirm = new FontAwesome.Sharp.IconButton();
            btnCancel = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)picWarning).BeginInit();
            pnlOptions.SuspendLayout();
            SuspendLayout();
            // 
            // picWarning
            // 
            picWarning.Location = new System.Drawing.Point(25, 25);
            picWarning.Name = "picWarning";
            picWarning.Size = new System.Drawing.Size(50, 50);
            picWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            picWarning.TabIndex = 0;
            picWarning.TabStop = false;
            // 
            // lblPrompt
            // 
            lblPrompt.AutoSize = true;
            lblPrompt.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            lblPrompt.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            lblPrompt.Location = new System.Drawing.Point(85, 25);
            lblPrompt.Name = "lblPrompt";
            lblPrompt.Size = new System.Drawing.Size(434, 28);
            lblPrompt.TabIndex = 1;
            lblPrompt.Text = "BẠN CÓ CHẮC CHẮN MUỐN XÓA BẢN GHI NÀY?";
            // 
            // lblItemInfo
            // 
            lblItemInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lblItemInfo.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblItemInfo.Location = new System.Drawing.Point(85, 58);
            lblItemInfo.Name = "lblItemInfo";
            lblItemInfo.Size = new System.Drawing.Size(420, 50);
            lblItemInfo.TabIndex = 2;
            lblItemInfo.Text = "Đối tượng xóa: ...";
            // 
            // pnlOptions
            // 
            pnlOptions.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            pnlOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlOptions.Controls.Add(radSoftDelete);
            pnlOptions.Controls.Add(radHardDelete);
            pnlOptions.Location = new System.Drawing.Point(35, 120);
            pnlOptions.Name = "pnlOptions";
            pnlOptions.Size = new System.Drawing.Size(460, 110);
            pnlOptions.TabIndex = 3;
            // 
            // radSoftDelete
            // 
            radSoftDelete.Checked = true;
            radSoftDelete.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            radSoftDelete.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            radSoftDelete.Location = new System.Drawing.Point(15, 12);
            radSoftDelete.Name = "radSoftDelete";
            radSoftDelete.Size = new System.Drawing.Size(430, 42);
            radSoftDelete.TabIndex = 0;
            radSoftDelete.TabStop = true;
            radSoftDelete.Text = " Xóa mềm (Soft Delete - Khuyên dùng)\n   Ẩn bản ghi khỏi giao diện, bảo toàn lịch sử và vé liên quan.";
            radSoftDelete.UseVisualStyleBackColor = true;
            // 
            // radHardDelete
            // 
            radHardDelete.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            radHardDelete.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            radHardDelete.Location = new System.Drawing.Point(15, 58);
            radHardDelete.Name = "radHardDelete";
            radHardDelete.Size = new System.Drawing.Size(430, 42);
            radHardDelete.TabIndex = 1;
            radHardDelete.Text = " Xóa vĩnh viễn (Cascade Transaction)\n   Xóa triệt để khỏi CSDL. Sẽ tự động Rollback nếu xảy ra lỗi.";
            radHardDelete.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
            btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnConfirm.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnConfirm.ForeColor = System.Drawing.Color.White;
            btnConfirm.IconChar = FontAwesome.Sharp.IconChar.Trash;
            btnConfirm.IconColor = System.Drawing.Color.White;
            btnConfirm.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnConfirm.IconSize = 18;
            btnConfirm.Location = new System.Drawing.Point(205, 255);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new System.Drawing.Size(150, 40);
            btnConfirm.TabIndex = 4;
            btnConfirm.Text = " Xác nhận xóa";
            btnConfirm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnConfirm.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnCancel.ForeColor = System.Drawing.Color.White;
            btnCancel.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            btnCancel.IconColor = System.Drawing.Color.White;
            btnCancel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancel.IconSize = 18;
            btnCancel.Location = new System.Drawing.Point(365, 255);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(110, 40);
            btnCancel.TabIndex = 5;
            btnCancel.Text = " Hủy bỏ";
            btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // FrmConfirmDelete
            // 
            AcceptButton = btnConfirm;
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            CancelButton = btnCancel;
            ClientSize = new System.Drawing.Size(540, 320);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Controls.Add(pnlOptions);
            Controls.Add(lblItemInfo);
            Controls.Add(lblPrompt);
            Controls.Add(picWarning);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new System.Drawing.Size(540, 320);
            Name = "FrmConfirmDelete";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Xác nhận xóa an toàn dữ liệu";
            ((System.ComponentModel.ISupportInitialize)picWarning).EndInit();
            pnlOptions.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
