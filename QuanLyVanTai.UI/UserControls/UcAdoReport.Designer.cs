namespace QuanLyVanTai.UI.UserControls
{
    partial class UcAdoReport
    {
        private System.ComponentModel.IContainer components = null;

        // ── Controls ──────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel      pnlTop;
        private System.Windows.Forms.Label      lblTitle;
        private System.Windows.Forms.Label      lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label      lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button     btnLoad;
        private System.Windows.Forms.ProgressBar progressBar;

        private System.Windows.Forms.SplitContainer splitMain;

        private System.Windows.Forms.GroupBox   grpMonthly;
        private System.Windows.Forms.DataGridView dgvMonthly;

        private System.Windows.Forms.GroupBox   grpRoutes;
        private System.Windows.Forms.DataGridView dgvRoutes;

        private System.Windows.Forms.Panel      pnlBottom;
        private System.Windows.Forms.Label      lblTotal;
        private System.Windows.Forms.Label      lblNote;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components  = new System.ComponentModel.Container();
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            // ── pnlTop ──────────────────────────────────────────────────────
            pnlTop = new System.Windows.Forms.Panel
            {
                Dock      = DockStyle.Top,
                Height    = 58,
                BackColor = System.Drawing.Color.FromArgb(15, 23, 42),
                Padding   = new Padding(12, 0, 12, 0)
            };

            lblTitle = new System.Windows.Forms.Label
            {
                Text      = "📊  Báo Cáo Doanh Thu  —  ADO.NET thuần (SqlCommand / SqlDataReader / SqlDataAdapter)",
                ForeColor = System.Drawing.Color.White,
                Font      = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold),
                AutoSize  = true,
                Location  = new System.Drawing.Point(12, 8)
            };

            lblFrom = new System.Windows.Forms.Label
            {
                Text      = "Từ ngày:",
                ForeColor = System.Drawing.Color.FromArgb(148, 163, 184),
                AutoSize  = true,
                Location  = new System.Drawing.Point(12, 34)
            };
            dtpFrom = new System.Windows.Forms.DateTimePicker
            {
                Format   = DateTimePickerFormat.Short,
                Location = new System.Drawing.Point(72, 30),
                Width    = 110
            };

            lblTo = new System.Windows.Forms.Label
            {
                Text      = "Đến:",
                ForeColor = System.Drawing.Color.FromArgb(148, 163, 184),
                AutoSize  = true,
                Location  = new System.Drawing.Point(192, 34)
            };
            dtpTo = new System.Windows.Forms.DateTimePicker
            {
                Format   = DateTimePickerFormat.Short,
                Location = new System.Drawing.Point(226, 30),
                Width    = 110
            };

            btnLoad = new System.Windows.Forms.Button
            {
                Text      = "🔍  Truy Vấn",
                Location  = new System.Drawing.Point(348, 28),
                Width     = 110,
                Height    = 26,
                BackColor = System.Drawing.Color.FromArgb(59, 130, 246),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold),
                Cursor    = Cursors.Hand
            };
            btnLoad.FlatAppearance.BorderSize = 0;
            btnLoad.Click += BtnLoad_Click;

            progressBar = new System.Windows.Forms.ProgressBar
            {
                Style   = ProgressBarStyle.Marquee,
                Height  = 4,
                Dock    = DockStyle.Bottom,
                Visible = false
            };

            pnlTop.Controls.AddRange(new Control[]
                { lblTitle, lblFrom, dtpFrom, lblTo, dtpTo, btnLoad, progressBar });

            // ── SplitContainer (trái = theo tháng, phải = theo tuyến) ────────
            splitMain = new System.Windows.Forms.SplitContainer
            {
                Dock        = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 420,
                Panel1MinSize    = 280,
                Panel2MinSize    = 280
            };

            // GroupBox trái
            grpMonthly = new System.Windows.Forms.GroupBox
            {
                Text    = "Doanh thu theo Tháng  [SqlCommand + SqlDataReader]",
                Dock    = DockStyle.Fill,
                Padding = new Padding(6),
                Font    = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold)
            };

            dgvMonthly = BuildDgv();
            grpMonthly.Controls.Add(dgvMonthly);
            splitMain.Panel1.Controls.Add(grpMonthly);

            // GroupBox phải
            grpRoutes = new System.Windows.Forms.GroupBox
            {
                Text    = "Doanh thu theo Tuyến  [SqlDataAdapter + DataTable]",
                Dock    = DockStyle.Fill,
                Padding = new Padding(6),
                Font    = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold)
            };

            dgvRoutes = BuildDgv();
            grpRoutes.Controls.Add(dgvRoutes);
            splitMain.Panel2.Controls.Add(grpRoutes);

            // ── pnlBottom ────────────────────────────────────────────────────
            pnlBottom = new System.Windows.Forms.Panel
            {
                Dock      = DockStyle.Bottom,
                Height    = 52,
                BackColor = System.Drawing.Color.FromArgb(241, 245, 249),
                Padding   = new Padding(12, 4, 12, 4)
            };

            lblTotal = new System.Windows.Forms.Label
            {
                Text      = "Tổng doanh thu (ADO.NET): —",
                Font      = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(15, 118, 110),
                AutoSize  = true,
                Location  = new System.Drawing.Point(12, 6)
            };

            lblNote = new System.Windows.Forms.Label
            {
                Text      = "Nhấn \"Truy Vấn\" để nạp dữ liệu.",
                ForeColor = System.Drawing.Color.FromArgb(100, 116, 139),
                Font      = new System.Drawing.Font("Segoe UI", 8.5f),
                AutoSize  = false,
                Dock      = DockStyle.Bottom,
                Height    = 22
            };

            pnlBottom.Controls.AddRange(new Control[] { lblTotal, lblNote });

            // ── Đưa vào UserControl ──────────────────────────────────────────
            Controls.AddRange(new Control[] { splitMain, pnlTop, pnlBottom });
            Size      = new System.Drawing.Size(900, 520);
            BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
        }

        /// <summary>Tạo DataGridView chuẩn style dùng lại cho cả hai bảng.</summary>
        private static System.Windows.Forms.DataGridView BuildDgv()
        {
            var dgv = new System.Windows.Forms.DataGridView
            {
                Dock                  = DockStyle.Fill,
                ReadOnly              = true,
                AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows    = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible     = false,
                SelectionMode         = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor       = System.Drawing.Color.White,
                BorderStyle           = BorderStyle.None,
                GridColor             = System.Drawing.Color.FromArgb(226, 232, 240),
                Font                  = new System.Drawing.Font("Segoe UI", 9f)
            };

            dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font      = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            dgv.ColumnHeadersHeight = 32;
            dgv.EnableHeadersVisualStyles = false;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dgv.RowTemplate.Height = 28;

            return dgv;
        }
    }
}
