namespace QuanLyVanTai.UI.Forms
{
    partial class FrmRouteAuditHistory
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblRouteInfo = new Label();
            lblTitle = new Label();
            pnlFooter = new Panel();
            lblSummary = new Label();
            btnClose = new FontAwesome.Sharp.IconButton();
            dgvHistory = new DataGridView();
            colTime = new DataGridViewTextBoxColumn();
            colUser = new DataGridViewTextBoxColumn();
            colRole = new DataGridViewTextBoxColumn();
            colAction = new DataGridViewTextBoxColumn();
            colChanges = new DataGridViewTextBoxColumn();
            colIp = new DataGridViewTextBoxColumn();
            pnlHeader.SuspendLayout();
            pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblRouteInfo);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(20, 14, 20, 10);
            pnlHeader.Size = new Size(1020, 86);
            pnlHeader.TabIndex = 0;
            // 
            // lblRouteInfo
            // 
            lblRouteInfo.AutoSize = true;
            lblRouteInfo.Font = new Font("Segoe UI", 9.5F);
            lblRouteInfo.ForeColor = Color.FromArgb(100, 116, 139);
            lblRouteInfo.Location = new Point(20, 48);
            lblRouteInfo.Name = "lblRouteInfo";
            lblRouteInfo.Size = new Size(200, 21);
            lblRouteInfo.TabIndex = 1;
            lblRouteInfo.Text = "Mã tuyến: ...   |   Lộ trình: ...";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTitle.Location = new Point(20, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(400, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📜 LỊCH SỬ THAO TÁC TUYẾN XE";
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.Controls.Add(lblSummary);
            pnlFooter.Controls.Add(btnClose);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 566);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(20, 8, 20, 8);
            pnlFooter.Size = new Size(1020, 54);
            pnlFooter.TabIndex = 2;
            // 
            // lblSummary
            // 
            lblSummary.Dock = DockStyle.Left;
            lblSummary.Font = new Font("Segoe UI", 9.5F);
            lblSummary.ForeColor = Color.FromArgb(71, 85, 105);
            lblSummary.Location = new Point(20, 8);
            lblSummary.Name = "lblSummary";
            lblSummary.Size = new Size(700, 38);
            lblSummary.TabIndex = 0;
            lblSummary.Text = "Đang tải lịch sử thao tác...";
            lblSummary.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BackColor = Color.FromArgb(100, 116, 139);
            btnClose.Cursor = Cursors.Hand;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            btnClose.IconColor = Color.White;
            btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClose.IconSize = 18;
            btnClose.Location = new Point(870, 8);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(130, 38);
            btnClose.TabIndex = 1;
            btnClose.Text = " Đóng";
            btnClose.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // dgvHistory
            // 
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToDeleteRows = false;
            dgvHistory.AllowUserToResizeRows = false;
            dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.BorderStyle = BorderStyle.None;
            dgvHistory.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvHistory.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            dgvHistory.ColumnHeadersHeight = 44;
            dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvHistory.Columns.AddRange(new DataGridViewColumn[] { colTime, colUser, colRole, colAction, colChanges, colIp });
            dgvHistory.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvHistory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 235, 255);
            dgvHistory.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.GridColor = Color.FromArgb(226, 232, 240);
            dgvHistory.Location = new Point(0, 86);
            dgvHistory.MultiSelect = false;
            dgvHistory.Name = "dgvHistory";
            dgvHistory.ReadOnly = true;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.RowHeadersWidth = 51;
            dgvHistory.RowTemplate.Height = 40;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.Size = new Size(1020, 480);
            dgvHistory.TabIndex = 1;
            // 
            // colTime
            // 
            colTime.FillWeight = 14F;
            colTime.HeaderText = "Thời Gian";
            colTime.MinimumWidth = 130;
            colTime.Name = "colTime";
            colTime.ReadOnly = true;
            // 
            // colUser
            // 
            colUser.FillWeight = 12F;
            colUser.HeaderText = "Người Dùng";
            colUser.MinimumWidth = 100;
            colUser.Name = "colUser";
            colUser.ReadOnly = true;
            // 
            // colRole
            // 
            colRole.FillWeight = 10F;
            colRole.HeaderText = "Vai Trò";
            colRole.MinimumWidth = 90;
            colRole.Name = "colRole";
            colRole.ReadOnly = true;
            // 
            // colAction
            // 
            colAction.FillWeight = 9F;
            colAction.HeaderText = "Hành Động";
            colAction.MinimumWidth = 90;
            colAction.Name = "colAction";
            colAction.ReadOnly = true;
            // 
            // colChanges
            // 
            colChanges.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            colChanges.FillWeight = 45F;
            colChanges.HeaderText = "Nội Dung Thay Đổi";
            colChanges.MinimumWidth = 260;
            colChanges.Name = "colChanges";
            colChanges.ReadOnly = true;
            // 
            // colIp
            // 
            colIp.FillWeight = 10F;
            colIp.HeaderText = "Địa Chỉ IP";
            colIp.MinimumWidth = 90;
            colIp.Name = "colIp";
            colIp.ReadOnly = true;
            // 
            // FrmRouteAuditHistory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnClose;
            ClientSize = new Size(1020, 620);
            Controls.Add(dgvHistory);
            Controls.Add(pnlFooter);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9F);
            MinimizeBox = false;
            MinimumSize = new Size(900, 560);
            Name = "FrmRouteAuditHistory";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Lịch sử thao tác tuyến xe";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblRouteInfo;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblSummary;
        private FontAwesome.Sharp.IconButton btnClose;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRole;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChanges;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIp;
    }
}
