namespace QuanLyVanTai.UI.UserControls
{
    partial class UcAuditLog
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
            label3 = new Label();
            dateTimePicker1 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            comboBox1 = new ComboBox();
            btnTimKiem = new Button();
            btnXuat = new Button();
            dgvAuditLog = new DataGridView();
            colThoiGian = new DataGridViewTextBoxColumn();
            colNguoiDung = new DataGridViewTextBoxColumn();
            colVaiTro = new DataGridViewTextBoxColumn();
            colHanhDong = new DataGridViewTextBoxColumn();
            colDoiTuong = new DataGridViewTextBoxColumn();
            colDiaChiIP = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvAuditLog).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 33);
            label1.Name = "label1";
            label1.Size = new Size(62, 20);
            label1.TabIndex = 0;
            label1.Text = "Từ ngày";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(325, 33);
            label2.Name = "label2";
            label2.Size = new Size(72, 20);
            label2.TabIndex = 1;
            label2.Text = "Đến ngày";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(606, 33);
            label3.Name = "label3";
            label3.Size = new Size(89, 20);
            label3.TabIndex = 2;
            label3.Text = "Người dùng";
            label3.Click += label3_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(34, 82);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 3;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Location = new Point(325, 82);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(250, 27);
            dateTimePicker2.TabIndex = 4;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(606, 81);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 5;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(815, 83);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(94, 29);
            btnTimKiem.TabIndex = 6;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            // 
            // btnXuat
            // 
            btnXuat.Location = new Point(815, 139);
            btnXuat.Name = "btnXuat";
            btnXuat.Size = new Size(94, 29);
            btnXuat.TabIndex = 7;
            btnXuat.Text = "Xuất";
            btnXuat.UseVisualStyleBackColor = true;
            // 
            // dgvAuditLog
            // 
            dgvAuditLog.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAuditLog.Columns.AddRange(new DataGridViewColumn[] { colThoiGian, colNguoiDung, colVaiTro, colHanhDong, colDoiTuong, colDiaChiIP });
            dgvAuditLog.Location = new Point(34, 215);
            dgvAuditLog.Name = "dgvAuditLog";
            dgvAuditLog.RowHeadersVisible = false;
            dgvAuditLog.RowHeadersWidth = 51;
            dgvAuditLog.Size = new Size(875, 429);
            dgvAuditLog.TabIndex = 8;
            // 
            // colThoiGian
            // 
            colThoiGian.HeaderText = "Thời Gian";
            colThoiGian.MinimumWidth = 6;
            colThoiGian.Name = "colThoiGian";
            colThoiGian.Width = 125;
            // 
            // colNguoiDung
            // 
            colNguoiDung.HeaderText = "Người Dùng";
            colNguoiDung.MinimumWidth = 6;
            colNguoiDung.Name = "colNguoiDung";
            colNguoiDung.Width = 125;
            // 
            // colVaiTro
            // 
            colVaiTro.HeaderText = "Vai Trò";
            colVaiTro.MinimumWidth = 6;
            colVaiTro.Name = "colVaiTro";
            colVaiTro.Width = 125;
            // 
            // colHanhDong
            // 
            colHanhDong.HeaderText = "Hành Động";
            colHanhDong.MinimumWidth = 6;
            colHanhDong.Name = "colHanhDong";
            colHanhDong.Width = 125;
            // 
            // colDoiTuong
            // 
            colDoiTuong.HeaderText = "Đối Tượng";
            colDoiTuong.MinimumWidth = 6;
            colDoiTuong.Name = "colDoiTuong";
            colDoiTuong.Width = 125;
            // 
            // colDiaChiIP
            // 
            colDiaChiIP.HeaderText = "IP";
            colDiaChiIP.MinimumWidth = 6;
            colDiaChiIP.Name = "colDiaChiIP";
            colDiaChiIP.Width = 125;
            // 
            // UcAuditLog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvAuditLog);
            Controls.Add(btnXuat);
            Controls.Add(btnTimKiem);
            Controls.Add(comboBox1);
            Controls.Add(dateTimePicker2);
            Controls.Add(dateTimePicker1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "UcAuditLog";
            Size = new Size(935, 687);
            ((System.ComponentModel.ISupportInitialize)dgvAuditLog).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private DateTimePicker dateTimePicker2;
        private ComboBox comboBox1;
        private Button btnTimKiem;
        private Button btnXuat;
        private DataGridView dgvAuditLog;
        private DataGridViewTextBoxColumn colThoiGian;
        private DataGridViewTextBoxColumn colNguoiDung;
        private DataGridViewTextBoxColumn colVaiTro;
        private DataGridViewTextBoxColumn colHanhDong;
        private DataGridViewTextBoxColumn colDoiTuong;
        private DataGridViewTextBoxColumn colDiaChiIP;
    }
}
