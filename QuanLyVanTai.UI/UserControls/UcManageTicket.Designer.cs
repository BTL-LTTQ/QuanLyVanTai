namespace QuanLyVanTai.UI.UserControls
{
    partial class UcManageTicket
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
            txtTimKiem = new TextBox();
            cboTuyen = new ComboBox();
            cboLoaiVe = new ComboBox();
            btnLoc = new Button();
            btnThemGiaVe = new Button();
            dgvGiaVe = new DataGridView();
            colTuyen = new DataGridViewTextBoxColumn();
            ColLoaiVe = new DataGridViewTextBoxColumn();
            colGiaVe = new DataGridViewTextBoxColumn();
            ColTrangThai = new DataGridViewTextBoxColumn();
            colThaoTac = new DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)dgvGiaVe).BeginInit();
            SuspendLayout();
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(27, 25);
            txtTimKiem.Multiline = true;
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(512, 28);
            txtTimKiem.TabIndex = 0;
            txtTimKiem.Text = "Tìm kiếm tuyến, loại vé..";
            // 
            // cboTuyen
            // 
            cboTuyen.FormattingEnabled = true;
            cboTuyen.Items.AddRange(new object[] { "Tất cả tuyến", "Hà Nội - Đà Nẵng", "Hải Phòng - Hà Nội" });
            cboTuyen.Location = new Point(27, 71);
            cboTuyen.Name = "cboTuyen";
            cboTuyen.Size = new Size(183, 28);
            cboTuyen.TabIndex = 1;
            // 
            // cboLoaiVe
            // 
            cboLoaiVe.FormattingEnabled = true;
            cboLoaiVe.Items.AddRange(new object[] { "Tất cả loại vé", "Người lớn", "Trẻ em" });
            cboLoaiVe.Location = new Point(238, 71);
            cboLoaiVe.Name = "cboLoaiVe";
            cboLoaiVe.Size = new Size(183, 28);
            cboLoaiVe.TabIndex = 2;
            // 
            // btnLoc
            // 
            btnLoc.BackColor = Color.Blue;
            btnLoc.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 163);
            btnLoc.ForeColor = Color.White;
            btnLoc.Location = new Point(445, 59);
            btnLoc.Name = "btnLoc";
            btnLoc.Size = new Size(94, 44);
            btnLoc.TabIndex = 3;
            btnLoc.Text = "Lọc";
            btnLoc.UseVisualStyleBackColor = false;
            // 
            // btnThemGiaVe
            // 
            btnThemGiaVe.BackColor = Color.Blue;
            btnThemGiaVe.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            btnThemGiaVe.ForeColor = Color.White;
            btnThemGiaVe.Location = new Point(27, 123);
            btnThemGiaVe.Name = "btnThemGiaVe";
            btnThemGiaVe.Size = new Size(512, 40);
            btnThemGiaVe.TabIndex = 4;
            btnThemGiaVe.Text = "+ Thêm giá vé";
            btnThemGiaVe.UseVisualStyleBackColor = false;
            // 
            // dgvGiaVe
            // 
            dgvGiaVe.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGiaVe.Columns.AddRange(new DataGridViewColumn[] { colTuyen, ColLoaiVe, colGiaVe, ColTrangThai, colThaoTac });
            dgvGiaVe.Dock = DockStyle.Bottom;
            dgvGiaVe.Location = new Point(0, 169);
            dgvGiaVe.Name = "dgvGiaVe";
            dgvGiaVe.RowHeadersVisible = false;
            dgvGiaVe.RowHeadersWidth = 51;
            dgvGiaVe.Size = new Size(628, 506);
            dgvGiaVe.TabIndex = 5;
            dgvGiaVe.Visible = false;
            dgvGiaVe.CellMouseClick += dgvGiaVe_CellMouseClick;
            dgvGiaVe.CellPainting += dgvGiaVe_CellPainting;
            // 
            // colTuyen
            // 
            colTuyen.HeaderText = "Tuyến";
            colTuyen.MinimumWidth = 6;
            colTuyen.Name = "colTuyen";
            colTuyen.Width = 125;
            // 
            // ColLoaiVe
            // 
            ColLoaiVe.HeaderText = "Loại vé";
            ColLoaiVe.MinimumWidth = 6;
            ColLoaiVe.Name = "ColLoaiVe";
            ColLoaiVe.Width = 125;
            // 
            // colGiaVe
            // 
            colGiaVe.HeaderText = "Giá(VNĐ)";
            colGiaVe.MinimumWidth = 6;
            colGiaVe.Name = "colGiaVe";
            colGiaVe.Width = 125;
            // 
            // ColTrangThai
            // 
            ColTrangThai.HeaderText = "Trạng Thái";
            ColTrangThai.MinimumWidth = 6;
            ColTrangThai.Name = "ColTrangThai";
            ColTrangThai.Width = 125;
            // 
            // colThaoTac
            // 
            colThaoTac.HeaderText = "Thao Tác";
            colThaoTac.MinimumWidth = 6;
            colThaoTac.Name = "colThaoTac";
            colThaoTac.Width = 125;
            // 
            // UcManageTicket
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvGiaVe);
            Controls.Add(btnThemGiaVe);
            Controls.Add(btnLoc);
            Controls.Add(cboLoaiVe);
            Controls.Add(cboTuyen);
            Controls.Add(txtTimKiem);
            Name = "UcManageTicket";
            Size = new Size(628, 675);
            ((System.ComponentModel.ISupportInitialize)dgvGiaVe).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTimKiem;
        private ComboBox cboTuyen;
        private ComboBox cboLoaiVe;
        private Button btnLoc;
        private Button btnThemGiaVe;
        private DataGridView dgvGiaVe;
        private DataGridViewTextBoxColumn colTuyen;
        private DataGridViewTextBoxColumn ColLoaiVe;
        private DataGridViewTextBoxColumn colGiaVe;
        private DataGridViewTextBoxColumn ColTrangThai;
        private DataGridViewButtonColumn colThaoTac;
    }
}
