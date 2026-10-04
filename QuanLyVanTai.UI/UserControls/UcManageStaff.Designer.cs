namespace QuanLyVanTai.UI.UserControls
{
    partial class UcManageStaff
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcManageStaff));
            imageList1 = new ImageList(components);
            txtTimKiem = new TextBox();
            pnlHeader = new Panel();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            btnTimKiem = new Button();
            btnThemNhanSu = new Button();
            dgvNhanSu = new DataGridView();
            colSTT = new DataGridViewTextBoxColumn();
            colHoTen = new DataGridViewTextBoxColumn();
            colChucVu = new DataGridViewTextBoxColumn();
            colPhongBan = new DataGridViewTextBoxColumn();
            colThaoTac = new DataGridViewTextBoxColumn();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNhanSu).BeginInit();
            SuspendLayout();
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "bus.png");
            imageList1.Images.SetKeyName(1, "gear.png");
            imageList1.Images.SetKeyName(2, "home.png");
            imageList1.Images.SetKeyName(3, "promotion.png");
            imageList1.Images.SetKeyName(4, "ticket.png");
            imageList1.Images.SetKeyName(5, "users.png");
            imageList1.Images.SetKeyName(6, "search.png");
            // 
            // txtTimKiem
            // 
            txtTimKiem.ImeMode = ImeMode.NoControl;
            txtTimKiem.Location = new Point(20, 35);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(300, 27);
            txtTimKiem.TabIndex = 4;
            txtTimKiem.Text = "Tìm kiếm nhân sự..";
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.BorderStyle = BorderStyle.Fixed3D;
            pnlHeader.Controls.Add(comboBox2);
            pnlHeader.Controls.Add(comboBox1);
            pnlHeader.Controls.Add(label2);
            pnlHeader.Controls.Add(label1);
            pnlHeader.Controls.Add(txtTimKiem);
            pnlHeader.Controls.Add(btnTimKiem);
            pnlHeader.Controls.Add(btnThemNhanSu);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(800, 115);
            pnlHeader.TabIndex = 3;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Đang làm việc", "Đã nghĩ", "Nghỉ phép" });
            comboBox2.Location = new Point(500, 35);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(130, 28);
            comboBox2.TabIndex = 10;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Tài xế", "Lơ xe", "Trưởng phòng" });
            comboBox1.Location = new Point(350, 35);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(148, 28);
            comboBox1.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(500, 15);
            label2.Name = "label2";
            label2.Size = new Size(78, 20);
            label2.TabIndex = 8;
            label2.Text = "Trạng thái:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(350, 15);
            label1.Name = "label1";
            label1.Size = new Size(64, 20);
            label1.TabIndex = 7;
            label1.Text = "Chức vụ:";
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(650, 30);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(110, 41);
            btnTimKiem.TabIndex = 5;
            btnTimKiem.Text = "Tìm Kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            // 
            // btnThemNhanSu
            // 
            btnThemNhanSu.BackColor = Color.Blue;
            btnThemNhanSu.ForeColor = Color.White;
            btnThemNhanSu.Location = new Point(780, 27);
            btnThemNhanSu.Name = "btnThemNhanSu";
            btnThemNhanSu.Size = new Size(153, 43);
            btnThemNhanSu.TabIndex = 6;
            btnThemNhanSu.Text = "+ Thêm nhân sự";
            btnThemNhanSu.UseVisualStyleBackColor = false;
            // 
            // dgvNhanSu
            // 
            dgvNhanSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanSu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNhanSu.Columns.AddRange(new DataGridViewColumn[] { colSTT, colHoTen, colChucVu, colPhongBan, colThaoTac });
            dgvNhanSu.Dock = DockStyle.Fill;
            dgvNhanSu.Location = new Point(0, 115);
            dgvNhanSu.Name = "dgvNhanSu";
            dgvNhanSu.RowHeadersVisible = false;
            dgvNhanSu.RowHeadersWidth = 51;
            dgvNhanSu.Size = new Size(800, 335);
            dgvNhanSu.TabIndex = 7;
            dgvNhanSu.CellContentClick += dgvNhanSu_CellContentClick;
            dgvNhanSu.CellMouseClick += dgvNhanSu_CellMouseClick;
            dgvNhanSu.CellPainting += dgvNhanSu_CellPainting;
            // 
            // colSTT
            // 
            colSTT.HeaderText = "STT";
            colSTT.MinimumWidth = 6;
            colSTT.Name = "colSTT";
            // 
            // colHoTen
            // 
            colHoTen.HeaderText = "Họ Tên";
            colHoTen.MinimumWidth = 6;
            colHoTen.Name = "colHoTen";
            // 
            // colChucVu
            // 
            colChucVu.HeaderText = "Chức Vụ";
            colChucVu.MinimumWidth = 6;
            colChucVu.Name = "colChucVu";
            // 
            // colPhongBan
            // 
            colPhongBan.HeaderText = "Phong Ban";
            colPhongBan.MinimumWidth = 6;
            colPhongBan.Name = "colPhongBan";
            // 
            // colThaoTac
            // 
            colThaoTac.HeaderText = "Thao Tác";
            colThaoTac.MinimumWidth = 6;
            colThaoTac.Name = "colThaoTac";
            // 
            // UcManageStaff
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvNhanSu);
            Controls.Add(pnlHeader);
            Name = "UcManageStaff";
            Size = new Size(800, 450);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNhanSu).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private ImageList imageList1;
        private TextBox txtTimKiem;
        private Panel pnlHeader;
        private Button btnTimKiem;
        private Button btnThemNhanSu;
        private DataGridView dgvNhanSu;
        private DataGridViewTextBoxColumn colSTT;
        private DataGridViewTextBoxColumn colHoTen;
        private DataGridViewTextBoxColumn colChucVu;
        private DataGridViewTextBoxColumn colPhongBan;
        private DataGridViewTextBoxColumn colThaoTac;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Label label2;
        private Label label1;
    }
}