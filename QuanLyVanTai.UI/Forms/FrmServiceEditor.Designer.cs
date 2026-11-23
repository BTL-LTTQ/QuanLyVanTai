namespace QuanLyVanTai.UI.Forms
{
    partial class FrmServiceEditor
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.CheckBox chkActive;
        private FontAwesome.Sharp.IconButton btnSave;
        private FontAwesome.Sharp.IconButton btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;

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
            lblName = new System.Windows.Forms.Label();
            txtName = new System.Windows.Forms.TextBox();
            lblCategory = new System.Windows.Forms.Label();
            cboCategory = new System.Windows.Forms.ComboBox();
            lblPrice = new System.Windows.Forms.Label();
            numPrice = new System.Windows.Forms.NumericUpDown();
            lblDescription = new System.Windows.Forms.Label();
            txtDescription = new System.Windows.Forms.TextBox();
            chkActive = new System.Windows.Forms.CheckBox();
            btnSave = new FontAwesome.Sharp.IconButton();
            btnCancel = new FontAwesome.Sharp.IconButton();
            errorProvider = new System.Windows.Forms.ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblName.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblName.Location = new System.Drawing.Point(24, 22);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(102, 21);
            lblName.TabIndex = 0;
            lblName.Text = "Tên dịch vụ (*)";
            // 
            // txtName
            // 
            txtName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            txtName.Location = new System.Drawing.Point(24, 46);
            txtName.MaxLength = 150;
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(460, 29);
            txtName.TabIndex = 1;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblCategory.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblCategory.Location = new System.Drawing.Point(24, 90);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new System.Drawing.Size(82, 21);
            lblCategory.TabIndex = 2;
            lblCategory.Text = "Danh mục";
            // 
            // cboCategory
            // 
            cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboCategory.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new System.Drawing.Point(24, 114);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new System.Drawing.Size(220, 29);
            cboCategory.TabIndex = 3;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblPrice.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblPrice.Location = new System.Drawing.Point(264, 90);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new System.Drawing.Size(90, 21);
            lblPrice.TabIndex = 4;
            lblPrice.Text = "Giá (VNĐ)";
            // 
            // numPrice
            // 
            numPrice.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            numPrice.Increment = new decimal(new int[] { 10000, 0, 0, 0 });
            numPrice.Location = new System.Drawing.Point(264, 114);
            numPrice.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numPrice.Name = "numPrice";
            numPrice.Size = new System.Drawing.Size(220, 29);
            numPrice.TabIndex = 5;
            numPrice.ThousandsSeparator = true;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblDescription.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblDescription.Location = new System.Drawing.Point(24, 158);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new System.Drawing.Size(62, 21);
            lblDescription.TabIndex = 6;
            lblDescription.Text = "Mô tả";
            // 
            // txtDescription
            // 
            txtDescription.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            txtDescription.Location = new System.Drawing.Point(24, 182);
            txtDescription.MaxLength = 300;
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtDescription.Size = new System.Drawing.Size(460, 96);
            txtDescription.TabIndex = 7;
            // 
            // chkActive
            // 
            chkActive.AutoSize = true;
            chkActive.Checked = true;
            chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            chkActive.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            chkActive.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            chkActive.Location = new System.Drawing.Point(24, 292);
            chkActive.Name = "chkActive";
            chkActive.Size = new System.Drawing.Size(160, 25);
            chkActive.TabIndex = 8;
            chkActive.Text = "Đang hoạt động";
            chkActive.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSave.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            btnSave.IconColor = System.Drawing.Color.White;
            btnSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSave.IconSize = 18;
            btnSave.Location = new System.Drawing.Point(244, 336);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(140, 40);
            btnSave.TabIndex = 9;
            btnSave.Text = " Lưu dịch vụ";
            btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnSave.UseVisualStyleBackColor = false;
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
            btnCancel.Location = new System.Drawing.Point(394, 336);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(90, 40);
            btnCancel.TabIndex = 10;
            btnCancel.Text = " Hủy";
            btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // FrmServiceEditor
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            CancelButton = btnCancel;
            ClientSize = new System.Drawing.Size(508, 400);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(chkActive);
            Controls.Add(txtDescription);
            Controls.Add(lblDescription);
            Controls.Add(numPrice);
            Controls.Add(lblPrice);
            Controls.Add(cboCategory);
            Controls.Add(lblCategory);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new System.Drawing.Size(524, 439);
            Name = "FrmServiceEditor";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Thêm dịch vụ vận tải";
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
