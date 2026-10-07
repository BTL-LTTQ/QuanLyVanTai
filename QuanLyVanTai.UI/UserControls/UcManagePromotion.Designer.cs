namespace QuanLyVanTai.UI.UserControls
{
    partial class UcManagePromotion
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
            dgvKhuyenMai = new DataGridView();
            colTen = new DataGridViewTextBoxColumn();
            colLoai = new DataGridViewTextBoxColumn();
            colGiaTri = new DataGridViewTextBoxColumn();
            colThoiGian = new DataGridViewTextBoxColumn();
            ColTrangThai = new DataGridViewTextBoxColumn();
            colThaoTac = new DataGridViewButtonColumn();
            btnThemKhuyenMai = new Button();
            btnLoc = new Button();
            cboLoaiVe = new ComboBox();
            cboLoai = new ComboBox();
            txtTimKiem = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvKhuyenMai).BeginInit();
            SuspendLayout();
            // 
            // dgvKhuyenMai
            // 
            dgvKhuyenMai.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKhuyenMai.Columns.AddRange(new DataGridViewColumn[] { colTen, colLoai, colGiaTri, colThoiGian, ColTrangThai, colThaoTac });
            dgvKhuyenMai.Dock = DockStyle.Bottom;
            dgvKhuyenMai.Location = new Point(0, 156);
            dgvKhuyenMai.Name = "dgvKhuyenMai";
            dgvKhuyenMai.RowHeadersVisible = false;
            dgvKhuyenMai.RowHeadersWidth = 51;
            dgvKhuyenMai.Size = new Size(754, 551);
            dgvKhuyenMai.TabIndex = 11;
            dgvKhuyenMai.Visible = false;
            dgvKhuyenMai.ColumnHeadersVisible = true;
            dgvKhuyenMai.CellMouseClick += dgvKhuyenMai_CellMouseClick;
            dgvKhuyenMai.CellPainting += dgvKhuyenMai_CellPainting;
            // 
            // colTen
            // 
            colTen.HeaderText = "Tên Khuyến Mãi";
            colTen.MinimumWidth = 6;
            colTen.Name = "colTen";
            colTen.Width = 125;
            // 
            // colLoai
            // 
            colLoai.HeaderText = "Loại";
            colLoai.MinimumWidth = 6;
            colLoai.Name = "colLoai";
            colLoai.Width = 125;
            // 
            // colGiaTri
            // 
            colGiaTri.HeaderText = "Giá trị";
            colGiaTri.MinimumWidth = 6;
            colGiaTri.Name = "colGiaTri";
            colGiaTri.Width = 125;
            // 
            // colThoiGian
            // 
            colThoiGian.HeaderText = "Thời Gian";
            colThoiGian.MinimumWidth = 6;
            colThoiGian.Name = "colThoiGian";
            colThoiGian.Width = 125;
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
            // btnThemKhuyenMai
            // 
            btnThemKhuyenMai.BackColor = Color.Blue;
            btnThemKhuyenMai.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            btnThemKhuyenMai.ForeColor = Color.White;
            btnThemKhuyenMai.Location = new Point(27, 110);
            btnThemKhuyenMai.Name = "btnThemKhuyenMai";
            btnThemKhuyenMai.Size = new Size(512, 40);
            btnThemKhuyenMai.TabIndex = 10;
            btnThemKhuyenMai.Text = "+ Thêm khuyến mãi";
            btnThemKhuyenMai.UseVisualStyleBackColor = false;
            // 
            // btnLoc
            // 
            btnLoc.BackColor = Color.Blue;
            btnLoc.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 163);
            btnLoc.ForeColor = Color.White;
            btnLoc.Location = new Point(445, 46);
            btnLoc.Name = "btnLoc";
            btnLoc.Size = new Size(94, 44);
            btnLoc.TabIndex = 9;
            btnLoc.Text = "Lọc";
            btnLoc.UseVisualStyleBackColor = false;
            // 
            // cboLoaiVe
            // 
            cboLoaiVe.FormattingEnabled = true;
            cboLoaiVe.Items.AddRange(new object[] { "Tất cả trạng thái", "03/ 02 - 04/03" });
            cboLoaiVe.Location = new Point(238, 58);
            cboLoaiVe.Name = "cboLoaiVe";
            cboLoaiVe.Size = new Size(183, 28);
            cboLoaiVe.TabIndex = 8;
            // 
            // cboLoai
            // 
            cboLoai.FormattingEnabled = true;
            cboLoai.Items.AddRange(new object[] { "Tất cả loại", "Combo", "Gia Đình" });
            cboLoai.Location = new Point(27, 58);
            cboLoai.Name = "cboLoai";
            cboLoai.Size = new Size(183, 28);
            cboLoai.TabIndex = 7;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(27, 12);
            txtTimKiem.Multiline = true;
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(512, 28);
            txtTimKiem.TabIndex = 6;
            txtTimKiem.Text = "Tìm kiếm khuyến mãi";
            // 
            // UcManagePromotion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvKhuyenMai);
            Controls.Add(btnThemKhuyenMai);
            Controls.Add(btnLoc);
            Controls.Add(cboLoaiVe);
            Controls.Add(cboLoai);
            Controls.Add(txtTimKiem);
            Name = "UcManagePromotion";
            Size = new Size(754, 707);
            ((System.ComponentModel.ISupportInitialize)dgvKhuyenMai).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvKhuyenMai;
        private Button btnThemKhuyenMai;
        private Button btnLoc;
        private ComboBox cboLoaiVe;
        private ComboBox cboLoai;
        private TextBox txtTimKiem;
        private DataGridViewTextBoxColumn colTen;
        private DataGridViewTextBoxColumn colLoai;
        private DataGridViewTextBoxColumn colGiaTri;
        private DataGridViewTextBoxColumn colThoiGian;
        private DataGridViewTextBoxColumn ColTrangThai;
        private DataGridViewButtonColumn colThaoTac;
    }
}
