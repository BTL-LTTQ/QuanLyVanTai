namespace QuanLyVanTai.UI.UserControls
{
    partial class UcPermission
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            cboVaiTro = new ComboBox();
            btnLuu = new Button();
            dgvPhanQuyen = new DataGridView();
            colMenu = new DataGridViewTextBoxColumn();
            colXem = new DataGridViewTextBoxColumn();
            colThem = new DataGridViewTextBoxColumn();
            colSua = new DataGridViewTextBoxColumn();
            colXoa = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvPhanQuyen).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.Location = new Point(36, 34);
            label1.Name = "label1";
            label1.Size = new Size(236, 24);
            label1.TabIndex = 0;
            label1.Text = "Phân quyền theo vai trò";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 163);
            label2.Location = new Point(36, 76);
            label2.Name = "label2";
            label2.Size = new Size(67, 23);
            label2.TabIndex = 1;
            label2.Text = "Vai trò";
            // 
            // cboVaiTro
            // 
            cboVaiTro.FormattingEnabled = true;
            cboVaiTro.Items.AddRange(new object[] { "Quản trị viên", "Quản lý", "Nhân viên bán vé" });
            cboVaiTro.Location = new Point(36, 123);
            cboVaiTro.Name = "cboVaiTro";
            cboVaiTro.Size = new Size(516, 28);
            cboVaiTro.TabIndex = 2;
            cboVaiTro.SelectedIndexChanged += cboVaiTro_SelectedIndexChanged;
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.Blue;
            btnLuu.ForeColor = Color.White;
            btnLuu.Location = new Point(637, 117);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(173, 39);
            btnLuu.TabIndex = 3;
            btnLuu.Text = "Lưu Quyền";
            btnLuu.UseVisualStyleBackColor = false;
            // 
            // dgvPhanQuyen
            // 
            dgvPhanQuyen.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPhanQuyen.Columns.AddRange(new DataGridViewColumn[] { colMenu, colXem, colThem, colSua, colXoa });
            dgvPhanQuyen.Location = new Point(36, 195);
            dgvPhanQuyen.Name = "dgvPhanQuyen";
            dgvPhanQuyen.RowHeadersVisible = false;
            dgvPhanQuyen.RowHeadersWidth = 51;
            dgvPhanQuyen.Size = new Size(774, 452);
            dgvPhanQuyen.TabIndex = 4;
            // 
            // colMenu
            // 
            colMenu.HeaderText = "Menu/ Chức Năng";
            colMenu.MinimumWidth = 6;
            colMenu.Name = "colMenu";
            colMenu.Width = 125;
            // 
            // colXem
            // 
            colXem.HeaderText = "Xem";
            colXem.MinimumWidth = 6;
            colXem.Name = "colXem";
            colXem.Width = 125;
            // 
            // colThem
            // 
            colThem.HeaderText = "Thêm";
            colThem.MinimumWidth = 6;
            colThem.Name = "colThem";
            colThem.Width = 125;
            // 
            // colSua
            // 
            colSua.HeaderText = "Sửa";
            colSua.MinimumWidth = 6;
            colSua.Name = "colSua";
            colSua.Width = 125;
            // 
            // colXoa
            // 
            colXoa.HeaderText = "Xóa";
            colXoa.MinimumWidth = 6;
            colXoa.Name = "colXoa";
            colXoa.Width = 125;
            // 
            // UcPermission
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvPhanQuyen);
            Controls.Add(btnLuu);
            Controls.Add(cboVaiTro);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "UcPermission";
            Size = new Size(847, 681);
            ((System.ComponentModel.ISupportInitialize)dgvPhanQuyen).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox cboVaiTro;
        private Button btnLuu;
        private DataGridView dgvPhanQuyen;
        private DataGridViewTextBoxColumn colMenu;
        private DataGridViewTextBoxColumn colXem;
        private DataGridViewTextBoxColumn colThem;
        private DataGridViewTextBoxColumn colSua;
        private DataGridViewTextBoxColumn colXoa;
    }
}
