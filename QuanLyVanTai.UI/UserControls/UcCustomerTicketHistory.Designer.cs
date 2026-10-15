namespace QuanLyVanTai.UI.UserControls
{
    partial class UcCustomerTicketHistory
    {
        private System.ComponentModel.IContainer components = null;

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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            pnlMainContainer = new Panel();
            grpDanhSachVe = new GroupBox();
            dgvTickets = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colTicketCode = new DataGridViewTextBoxColumn();
            colRoute = new DataGridViewTextBoxColumn();
            colSeatNumber = new DataGridViewTextBoxColumn();
            colDepartureTime = new DataGridViewTextBoxColumn();
            colTicketStatus = new DataGridViewTextBoxColumn();
            pnlBottomActions = new Panel();
            flpButtons = new FlowLayoutPanel();
            btnChiTiet = new FontAwesome.Sharp.IconButton();
            btnInVe = new FontAwesome.Sharp.IconButton();
            btnHuyVe = new FontAwesome.Sharp.IconButton();
            btnQuayLai = new FontAwesome.Sharp.IconButton();
            lblLuuYQuyDinh = new Label();
            grpTimKiem = new GroupBox();
            tlpTimKiem = new TableLayoutPanel();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            lblLocTrangThai = new Label();
            cboTrangThai = new ComboBox();
            btnTimKiem = new FontAwesome.Sharp.IconButton();
            btnLamMoi = new FontAwesome.Sharp.IconButton();
            pnlMainContainer.SuspendLayout();
            grpDanhSachVe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTickets).BeginInit();
            pnlBottomActions.SuspendLayout();
            flpButtons.SuspendLayout();
            grpTimKiem.SuspendLayout();
            tlpTimKiem.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMainContainer
            // 
            pnlMainContainer.BackColor = Color.FromArgb(248, 250, 252);
            pnlMainContainer.Controls.Add(grpDanhSachVe);
            pnlMainContainer.Controls.Add(pnlBottomActions);
            pnlMainContainer.Controls.Add(grpTimKiem);
            pnlMainContainer.Dock = DockStyle.Fill;
            pnlMainContainer.Location = new Point(0, 0);
            pnlMainContainer.Name = "pnlMainContainer";
            pnlMainContainer.Padding = new Padding(15);
            pnlMainContainer.Size = new Size(1200, 750);
            pnlMainContainer.TabIndex = 0;
            // 
            // grpDanhSachVe
            // 
            grpDanhSachVe.BackColor = Color.White;
            grpDanhSachVe.Controls.Add(dgvTickets);
            grpDanhSachVe.Dock = DockStyle.Fill;
            grpDanhSachVe.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            grpDanhSachVe.ForeColor = Color.FromArgb(15, 23, 42);
            grpDanhSachVe.Location = new Point(15, 107);
            grpDanhSachVe.Name = "grpDanhSachVe";
            grpDanhSachVe.Padding = new Padding(12);
            grpDanhSachVe.Size = new Size(1170, 568);
            grpDanhSachVe.TabIndex = 1;
            grpDanhSachVe.TabStop = false;
            grpDanhSachVe.Text = " Danh Sách Vé Của Bạn ";
            // 
            // dgvTickets
            // 
            dgvTickets.AllowUserToAddRows = false;
            dgvTickets.AllowUserToDeleteRows = false;
            dgvTickets.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 250, 252);
            dgvTickets.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvTickets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTickets.BackgroundColor = Color.White;
            dgvTickets.BorderStyle = BorderStyle.None;
            dgvTickets.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTickets.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(241, 245, 249);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(71, 85, 105);
            dataGridViewCellStyle2.Padding = new Padding(6, 8, 6, 8);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(241, 245, 249);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(71, 85, 105);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvTickets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvTickets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTickets.Columns.AddRange(new DataGridViewColumn[] { colId, colTicketCode, colRoute, colSeatNumber, colDepartureTime, colTicketStatus });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle3.Padding = new Padding(6, 4, 6, 4);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(238, 242, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(37, 99, 235);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvTickets.DefaultCellStyle = dataGridViewCellStyle3;
            dgvTickets.Dock = DockStyle.Fill;
            dgvTickets.EnableHeadersVisualStyles = false;
            dgvTickets.GridColor = Color.FromArgb(226, 232, 240);
            dgvTickets.Location = new Point(12, 35);
            dgvTickets.MultiSelect = false;
            dgvTickets.Name = "dgvTickets";
            dgvTickets.ReadOnly = true;
            dgvTickets.RowHeadersVisible = false;
            dgvTickets.RowHeadersWidth = 51;
            dgvTickets.RowTemplate.Height = 38;
            dgvTickets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTickets.Size = new Size(1146, 521);
            dgvTickets.TabIndex = 0;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.HeaderText = "ID";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Visible = false;
            // 
            // colTicketCode
            // 
            colTicketCode.DataPropertyName = "TicketCode";
            colTicketCode.FillWeight = 110F;
            colTicketCode.HeaderText = "Mã Vé";
            colTicketCode.MinimumWidth = 110;
            colTicketCode.Name = "colTicketCode";
            colTicketCode.ReadOnly = true;
            // 
            // colRoute
            // 
            colRoute.DataPropertyName = "RouteName";
            colRoute.FillWeight = 220F;
            colRoute.HeaderText = "Tuyến Đường";
            colRoute.MinimumWidth = 180;
            colRoute.Name = "colRoute";
            colRoute.ReadOnly = true;
            // 
            // colSeatNumber
            // 
            colSeatNumber.DataPropertyName = "SeatNumber";
            colSeatNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSeatNumber.FillWeight = 70F;
            colSeatNumber.HeaderText = "Ghế";
            colSeatNumber.MinimumWidth = 65;
            colSeatNumber.Name = "colSeatNumber";
            colSeatNumber.ReadOnly = true;
            // 
            // colDepartureTime
            // 
            colDepartureTime.DataPropertyName = "DepartureTimeDisplay";
            colDepartureTime.FillWeight = 140F;
            colDepartureTime.HeaderText = "Giờ Khởi Hành";
            colDepartureTime.MinimumWidth = 135;
            colDepartureTime.Name = "colDepartureTime";
            colDepartureTime.ReadOnly = true;
            // 
            // colTicketStatus
            // 
            colTicketStatus.DataPropertyName = "TicketStatusDisplay";
            colTicketStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTicketStatus.FillWeight = 110F;
            colTicketStatus.HeaderText = "Trạng Thái";
            colTicketStatus.MinimumWidth = 100;
            colTicketStatus.Name = "colTicketStatus";
            colTicketStatus.ReadOnly = true;
            // 
            // pnlBottomActions
            // 
            pnlBottomActions.BackColor = Color.Transparent;
            pnlBottomActions.Controls.Add(flpButtons);
            pnlBottomActions.Controls.Add(lblLuuYQuyDinh);
            pnlBottomActions.Dock = DockStyle.Bottom;
            pnlBottomActions.Location = new Point(15, 675);
            pnlBottomActions.Name = "pnlBottomActions";
            pnlBottomActions.Padding = new Padding(0, 10, 0, 0);
            pnlBottomActions.Size = new Size(1170, 60);
            pnlBottomActions.TabIndex = 2;
            // 
            // flpButtons
            // 
            flpButtons.AutoSize = true;
            flpButtons.Controls.Add(btnChiTiet);
            flpButtons.Controls.Add(btnInVe);
            flpButtons.Controls.Add(btnHuyVe);
            flpButtons.Controls.Add(btnQuayLai);
            flpButtons.Dock = DockStyle.Right;
            flpButtons.Location = new Point(510, 10);
            flpButtons.Name = "flpButtons";
            flpButtons.Size = new Size(660, 50);
            flpButtons.TabIndex = 1;
            flpButtons.WrapContents = false;
            // 
            // btnChiTiet
            // 
            btnChiTiet.BackColor = Color.FromArgb(37, 99, 235);
            btnChiTiet.Cursor = Cursors.Hand;
            btnChiTiet.FlatAppearance.BorderSize = 0;
            btnChiTiet.FlatStyle = FlatStyle.Flat;
            btnChiTiet.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnChiTiet.ForeColor = Color.White;
            btnChiTiet.IconChar = FontAwesome.Sharp.IconChar.CircleInfo;
            btnChiTiet.IconColor = Color.White;
            btnChiTiet.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnChiTiet.IconSize = 20;
            btnChiTiet.ImageAlign = ContentAlignment.MiddleLeft;
            btnChiTiet.Location = new Point(3, 4);
            btnChiTiet.Margin = new Padding(3, 4, 8, 4);
            btnChiTiet.Name = "btnChiTiet";
            btnChiTiet.Size = new Size(140, 42);
            btnChiTiet.TabIndex = 0;
            btnChiTiet.Text = " Xem Chi Tiết";
            btnChiTiet.TextAlign = ContentAlignment.MiddleRight;
            btnChiTiet.UseVisualStyleBackColor = false;
            // 
            // btnInVe
            // 
            btnInVe.BackColor = Color.FromArgb(16, 185, 129);
            btnInVe.Cursor = Cursors.Hand;
            btnInVe.FlatAppearance.BorderSize = 0;
            btnInVe.FlatStyle = FlatStyle.Flat;
            btnInVe.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnInVe.ForeColor = Color.White;
            btnInVe.IconChar = FontAwesome.Sharp.IconChar.Print;
            btnInVe.IconColor = Color.White;
            btnInVe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnInVe.IconSize = 20;
            btnInVe.ImageAlign = ContentAlignment.MiddleLeft;
            btnInVe.Location = new Point(154, 4);
            btnInVe.Margin = new Padding(3, 4, 8, 4);
            btnInVe.Name = "btnInVe";
            btnInVe.Size = new Size(150, 42);
            btnInVe.TabIndex = 1;
            btnInVe.Text = " In Thẻ Lên Xe";
            btnInVe.TextAlign = ContentAlignment.MiddleRight;
            btnInVe.UseVisualStyleBackColor = false;
            // 
            // btnHuyVe
            // 
            btnHuyVe.BackColor = Color.FromArgb(239, 68, 68);
            btnHuyVe.Cursor = Cursors.Hand;
            btnHuyVe.FlatAppearance.BorderSize = 0;
            btnHuyVe.FlatStyle = FlatStyle.Flat;
            btnHuyVe.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnHuyVe.ForeColor = Color.White;
            btnHuyVe.IconChar = FontAwesome.Sharp.IconChar.TimesCircle;
            btnHuyVe.IconColor = Color.White;
            btnHuyVe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnHuyVe.IconSize = 20;
            btnHuyVe.ImageAlign = ContentAlignment.MiddleLeft;
            btnHuyVe.Location = new Point(315, 4);
            btnHuyVe.Margin = new Padding(3, 4, 8, 4);
            btnHuyVe.Name = "btnHuyVe";
            btnHuyVe.Size = new Size(130, 42);
            btnHuyVe.TabIndex = 2;
            btnHuyVe.Text = " Hủy Vé Xe";
            btnHuyVe.TextAlign = ContentAlignment.MiddleRight;
            btnHuyVe.UseVisualStyleBackColor = false;
            // 
            // btnQuayLai
            // 
            btnQuayLai.BackColor = Color.FromArgb(100, 116, 139);
            btnQuayLai.Cursor = Cursors.Hand;
            btnQuayLai.FlatAppearance.BorderSize = 0;
            btnQuayLai.FlatStyle = FlatStyle.Flat;
            btnQuayLai.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnQuayLai.ForeColor = Color.White;
            btnQuayLai.IconChar = FontAwesome.Sharp.IconChar.ArrowLeft;
            btnQuayLai.IconColor = Color.White;
            btnQuayLai.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnQuayLai.IconSize = 20;
            btnQuayLai.ImageAlign = ContentAlignment.MiddleLeft;
            btnQuayLai.Location = new Point(456, 4);
            btnQuayLai.Margin = new Padding(3, 4, 3, 4);
            btnQuayLai.Name = "btnQuayLai";
            btnQuayLai.Size = new Size(180, 42);
            btnQuayLai.TabIndex = 3;
            btnQuayLai.Text = " Quay Lại Đặt Vé";
            btnQuayLai.TextAlign = ContentAlignment.MiddleRight;
            btnQuayLai.UseVisualStyleBackColor = false;
            // 
            // lblLuuYQuyDinh
            // 
            lblLuuYQuyDinh.AutoSize = true;
            lblLuuYQuyDinh.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblLuuYQuyDinh.ForeColor = Color.FromArgb(220, 38, 38);
            lblLuuYQuyDinh.Location = new Point(0, 18);
            lblLuuYQuyDinh.Name = "lblLuuYQuyDinh";
            lblLuuYQuyDinh.Size = new Size(504, 20);
            lblLuuYQuyDinh.TabIndex = 0;
            lblLuuYQuyDinh.Text = "* Quy định vận tải: Quý khách chỉ được hủy vé trước giờ khởi hành tối thiểu 60 phút.";
            // 
            // grpTimKiem
            // 
            grpTimKiem.BackColor = Color.White;
            grpTimKiem.Controls.Add(tlpTimKiem);
            grpTimKiem.Dock = DockStyle.Top;
            grpTimKiem.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            grpTimKiem.ForeColor = Color.FromArgb(15, 23, 42);
            grpTimKiem.Location = new Point(15, 15);
            grpTimKiem.Name = "grpTimKiem";
            grpTimKiem.Padding = new Padding(12);
            grpTimKiem.Size = new Size(1170, 92);
            grpTimKiem.TabIndex = 0;
            grpTimKiem.TabStop = false;
            grpTimKiem.Text = " Tra Cứu - Lọc Vé ";
            // 
            // tlpTimKiem
            // 
            tlpTimKiem.ColumnCount = 6;
            tlpTimKiem.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            tlpTimKiem.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tlpTimKiem.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tlpTimKiem.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tlpTimKiem.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tlpTimKiem.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
            tlpTimKiem.Controls.Add(lblTimKiem, 0, 0);
            tlpTimKiem.Controls.Add(txtTimKiem, 1, 0);
            tlpTimKiem.Controls.Add(lblLocTrangThai, 2, 0);
            tlpTimKiem.Controls.Add(cboTrangThai, 3, 0);
            tlpTimKiem.Controls.Add(btnTimKiem, 4, 0);
            tlpTimKiem.Controls.Add(btnLamMoi, 5, 0);
            tlpTimKiem.Dock = DockStyle.Fill;
            tlpTimKiem.Location = new Point(12, 35);
            tlpTimKiem.Name = "tlpTimKiem";
            tlpTimKiem.RowCount = 1;
            tlpTimKiem.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpTimKiem.Size = new Size(1146, 45);
            tlpTimKiem.TabIndex = 0;
            // 
            // lblTimKiem
            // 
            lblTimKiem.Anchor = AnchorStyles.Left;
            lblTimKiem.AutoSize = true;
            lblTimKiem.Font = new Font("Segoe UI", 9F);
            lblTimKiem.ForeColor = Color.FromArgb(71, 85, 105);
            lblTimKiem.Location = new Point(3, 12);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(73, 20);
            lblTimKiem.TabIndex = 0;
            lblTimKiem.Text = "Tìm kiếm:";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtTimKiem.Font = new Font("Segoe UI", 9.5F);
            txtTimKiem.Location = new Point(98, 8);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Nhập mã vé, họ tên hoặc số điện thoại...";
            txtTimKiem.Size = new Size(334, 29);
            txtTimKiem.TabIndex = 1;
            // 
            // lblLocTrangThai
            // 
            lblLocTrangThai.Anchor = AnchorStyles.Left;
            lblLocTrangThai.AutoSize = true;
            lblLocTrangThai.Font = new Font("Segoe UI", 9F);
            lblLocTrangThai.ForeColor = Color.FromArgb(71, 85, 105);
            lblLocTrangThai.Location = new Point(438, 12);
            lblLocTrangThai.Name = "lblLocTrangThai";
            lblLocTrangThai.Size = new Size(78, 20);
            lblLocTrangThai.TabIndex = 2;
            lblLocTrangThai.Text = "Trạng thái:";
            // 
            // cboTrangThai
            // 
            cboTrangThai.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Font = new Font("Segoe UI", 9.5F);
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Location = new Point(538, 8);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(273, 29);
            cboTrangThai.TabIndex = 3;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Anchor = AnchorStyles.None;
            btnTimKiem.BackColor = Color.FromArgb(37, 99, 235);
            btnTimKiem.Cursor = Cursors.Hand;
            btnTimKiem.FlatAppearance.BorderSize = 0;
            btnTimKiem.FlatStyle = FlatStyle.Flat;
            btnTimKiem.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnTimKiem.ForeColor = Color.White;
            btnTimKiem.IconChar = FontAwesome.Sharp.IconChar.Search;
            btnTimKiem.IconColor = Color.White;
            btnTimKiem.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnTimKiem.IconSize = 18;
            btnTimKiem.ImageAlign = ContentAlignment.MiddleLeft;
            btnTimKiem.Location = new Point(819, 4);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(120, 36);
            btnTimKiem.TabIndex = 4;
            btnTimKiem.Text = " Tìm Kiếm";
            btnTimKiem.TextAlign = ContentAlignment.MiddleRight;
            btnTimKiem.UseVisualStyleBackColor = false;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Anchor = AnchorStyles.None;
            btnLamMoi.BackColor = Color.FromArgb(241, 245, 249);
            btnLamMoi.Cursor = Cursors.Hand;
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnLamMoi.ForeColor = Color.FromArgb(51, 65, 85);
            btnLamMoi.IconChar = FontAwesome.Sharp.IconChar.Rotate;
            btnLamMoi.IconColor = Color.FromArgb(51, 65, 85);
            btnLamMoi.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnLamMoi.IconSize = 18;
            btnLamMoi.ImageAlign = ContentAlignment.MiddleLeft;
            btnLamMoi.Location = new Point(947, 4);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(118, 36);
            btnLamMoi.TabIndex = 5;
            btnLamMoi.Text = " Làm Mới";
            btnLamMoi.TextAlign = ContentAlignment.MiddleRight;
            btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // UcCustomerTicketHistory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            Controls.Add(pnlMainContainer);
            Font = new Font("Segoe UI", 9F);
            Name = "UcCustomerTicketHistory";
            Size = new Size(1200, 750);
            pnlMainContainer.ResumeLayout(false);
            grpDanhSachVe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTickets).EndInit();
            pnlBottomActions.ResumeLayout(false);
            pnlBottomActions.PerformLayout();
            flpButtons.ResumeLayout(false);
            grpTimKiem.ResumeLayout(false);
            tlpTimKiem.ResumeLayout(false);
            tlpTimKiem.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMainContainer;
        private GroupBox grpDanhSachVe;
        private DataGridView dgvTickets;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colTicketCode;
        private DataGridViewTextBoxColumn colRoute;
        private DataGridViewTextBoxColumn colSeatNumber;
        private DataGridViewTextBoxColumn colDepartureTime;
        private DataGridViewTextBoxColumn colTicketStatus;
        private Panel pnlBottomActions;
        private FlowLayoutPanel flpButtons;
        private FontAwesome.Sharp.IconButton btnChiTiet;
        private FontAwesome.Sharp.IconButton btnInVe;
        private FontAwesome.Sharp.IconButton btnHuyVe;
        private FontAwesome.Sharp.IconButton btnQuayLai;
        private Label lblLuuYQuyDinh;
        private GroupBox grpTimKiem;
        private TableLayoutPanel tlpTimKiem;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private Label lblLocTrangThai;
        private ComboBox cboTrangThai;
        private FontAwesome.Sharp.IconButton btnTimKiem;
        private FontAwesome.Sharp.IconButton btnLamMoi;
    }
}
