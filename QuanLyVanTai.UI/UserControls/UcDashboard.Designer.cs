namespace QuanLyVanTai.UI.UserControls
{
    partial class UcDashboard
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblDashboardTitle;
        private System.Windows.Forms.Label lblDashboardDate;
        private System.Windows.Forms.FlowLayoutPanel flpStatCards;
        private System.Windows.Forms.TableLayoutPanel tblBody;
        private System.Windows.Forms.Panel pnlChartWrap;
        private System.Windows.Forms.Label lblChartTitle;
        private System.Windows.Forms.Label lblChartUnit;
        private System.Windows.Forms.Panel pnlChartCanvas;
        private System.Windows.Forms.Panel pnlMapWrap;
        private System.Windows.Forms.Label lblMapTitle;
        private System.Windows.Forms.Label lblLiveStatus;
        private System.Windows.Forms.Panel pnlMapCanvas;
        private System.Windows.Forms.Panel pnlBusScroll;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblFooterStatus;
        private System.Windows.Forms.Label lblFooterVehicles;
        private System.Windows.Forms.Label lblFooterTime;

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
            tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            pnlHeader = new System.Windows.Forms.Panel();
            lblDashboardTitle = new System.Windows.Forms.Label();
            lblDashboardDate = new System.Windows.Forms.Label();
            flpStatCards = new System.Windows.Forms.FlowLayoutPanel();
            tblBody = new System.Windows.Forms.TableLayoutPanel();
            pnlChartWrap = new System.Windows.Forms.Panel();
            lblChartTitle = new System.Windows.Forms.Label();
            lblChartUnit = new System.Windows.Forms.Label();
            pnlChartCanvas = new System.Windows.Forms.Panel();
            pnlMapWrap = new System.Windows.Forms.Panel();
            lblMapTitle = new System.Windows.Forms.Label();
            lblLiveStatus = new System.Windows.Forms.Label();
            pnlMapCanvas = new System.Windows.Forms.Panel();
            pnlBusScroll = new System.Windows.Forms.Panel();
            pnlFooter = new System.Windows.Forms.Panel();
            lblFooterStatus = new System.Windows.Forms.Label();
            lblFooterVehicles = new System.Windows.Forms.Label();
            lblFooterTime = new System.Windows.Forms.Label();
            tlpRoot.SuspendLayout();
            pnlHeader.SuspendLayout();
            tblBody.SuspendLayout();
            pnlChartWrap.SuspendLayout();
            pnlMapWrap.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // tlpRoot
            // 
            tlpRoot.BackColor = System.Drawing.Color.Transparent;
            tlpRoot.ColumnCount = 1;
            tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpRoot.Controls.Add(pnlHeader, 0, 0);
            tlpRoot.Controls.Add(flpStatCards, 0, 1);
            tlpRoot.Controls.Add(tblBody, 0, 2);
            tlpRoot.Controls.Add(pnlFooter, 0, 3);
            tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            tlpRoot.Location = new System.Drawing.Point(0, 0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 4;
            tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            tlpRoot.Size = new System.Drawing.Size(1200, 800);
            tlpRoot.TabIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = System.Drawing.Color.White;
            pnlHeader.Controls.Add(lblDashboardTitle);
            pnlHeader.Controls.Add(lblDashboardDate);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Margin = new System.Windows.Forms.Padding(0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            pnlHeader.Size = new System.Drawing.Size(1200, 70);
            pnlHeader.TabIndex = 0;
            // 
            // lblDashboardTitle
            // 
            lblDashboardTitle.AutoSize = true;
            lblDashboardTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            lblDashboardTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblDashboardTitle.Location = new System.Drawing.Point(20, 12);
            lblDashboardTitle.Name = "lblDashboardTitle";
            lblDashboardTitle.Size = new System.Drawing.Size(281, 35);
            lblDashboardTitle.TabIndex = 0;
            lblDashboardTitle.Text = "Operations Dashboard";
            // 
            // lblDashboardDate
            // 
            lblDashboardDate.AutoSize = true;
            lblDashboardDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblDashboardDate.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblDashboardDate.Location = new System.Drawing.Point(22, 44);
            lblDashboardDate.Name = "lblDashboardDate";
            lblDashboardDate.Size = new System.Drawing.Size(260, 20);
            lblDashboardDate.TabIndex = 1;
            lblDashboardDate.Text = "Hệ Thống Giám Sát & Điều Hành Vận Tải";
            // 
            // flpStatCards
            // 
            flpStatCards.AutoScroll = true;
            flpStatCards.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            flpStatCards.Dock = System.Windows.Forms.DockStyle.Fill;
            flpStatCards.Location = new System.Drawing.Point(0, 70);
            flpStatCards.Margin = new System.Windows.Forms.Padding(0);
            flpStatCards.Name = "flpStatCards";
            flpStatCards.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            flpStatCards.Size = new System.Drawing.Size(1200, 115);
            flpStatCards.TabIndex = 1;
            flpStatCards.WrapContents = false;
            // 
            // tblBody
            // 
            tblBody.BackColor = System.Drawing.Color.Transparent;
            tblBody.ColumnCount = 2;
            tblBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            tblBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            tblBody.Controls.Add(pnlChartWrap, 0, 0);
            tblBody.Controls.Add(pnlMapWrap, 1, 0);
            tblBody.Dock = System.Windows.Forms.DockStyle.Fill;
            tblBody.Location = new System.Drawing.Point(0, 185);
            tblBody.Margin = new System.Windows.Forms.Padding(0);
            tblBody.Name = "tblBody";
            tblBody.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            tblBody.RowCount = 1;
            tblBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tblBody.Size = new System.Drawing.Size(1200, 579);
            tblBody.TabIndex = 2;
            // 
            // pnlChartWrap
            // 
            pnlChartWrap.BackColor = System.Drawing.Color.White;
            pnlChartWrap.Controls.Add(lblChartTitle);
            pnlChartWrap.Controls.Add(lblChartUnit);
            pnlChartWrap.Controls.Add(pnlChartCanvas);
            pnlChartWrap.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlChartWrap.Location = new System.Drawing.Point(20, 10);
            pnlChartWrap.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            pnlChartWrap.Name = "pnlChartWrap";
            pnlChartWrap.Size = new System.Drawing.Size(664, 559);
            pnlChartWrap.TabIndex = 0;
            // 
            // lblChartTitle
            // 
            lblChartTitle.AutoSize = true;
            lblChartTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            lblChartTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblChartTitle.Location = new System.Drawing.Point(16, 14);
            lblChartTitle.Name = "lblChartTitle";
            lblChartTitle.Size = new System.Drawing.Size(422, 25);
            lblChartTitle.TabIndex = 0;
            lblChartTitle.Text = "Doanh Thu & Sản Lượng Vé Bán — 10 Ngày Gần Nhất";
            // 
            // lblChartUnit
            // 
            lblChartUnit.AutoSize = true;
            lblChartUnit.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblChartUnit.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblChartUnit.Location = new System.Drawing.Point(16, 36);
            lblChartUnit.Name = "lblChartUnit";
            lblChartUnit.Size = new System.Drawing.Size(149, 20);
            lblChartUnit.TabIndex = 1;
            lblChartUnit.Text = "Đơn vị tính: Triệu VNĐ";
            // 
            // pnlChartCanvas
            // 
            pnlChartCanvas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlChartCanvas.BackColor = System.Drawing.Color.White;
            pnlChartCanvas.Location = new System.Drawing.Point(10, 60);
            pnlChartCanvas.Name = "pnlChartCanvas";
            pnlChartCanvas.Size = new System.Drawing.Size(644, 485);
            pnlChartCanvas.TabIndex = 2;
            // 
            // pnlMapWrap
            // 
            pnlMapWrap.BackColor = System.Drawing.Color.White;
            pnlMapWrap.Controls.Add(lblMapTitle);
            pnlMapWrap.Controls.Add(lblLiveStatus);
            pnlMapWrap.Controls.Add(pnlMapCanvas);
            pnlMapWrap.Controls.Add(pnlBusScroll);
            pnlMapWrap.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlMapWrap.Location = new System.Drawing.Point(700, 10);
            pnlMapWrap.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            pnlMapWrap.Name = "pnlMapWrap";
            pnlMapWrap.Size = new System.Drawing.Size(480, 559);
            pnlMapWrap.TabIndex = 1;
            // 
            // lblMapTitle
            // 
            lblMapTitle.AutoSize = true;
            lblMapTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            lblMapTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblMapTitle.Location = new System.Drawing.Point(16, 14);
            lblMapTitle.Name = "lblMapTitle";
            lblMapTitle.Size = new System.Drawing.Size(280, 25);
            lblMapTitle.TabIndex = 0;
            lblMapTitle.Text = "Giám Sát GPS Vận Hành Xe Tuyến";
            // 
            // lblLiveStatus
            // 
            lblLiveStatus.AutoSize = true;
            lblLiveStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            lblLiveStatus.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            lblLiveStatus.Location = new System.Drawing.Point(16, 36);
            lblLiveStatus.Name = "lblLiveStatus";
            lblLiveStatus.Size = new System.Drawing.Size(188, 20);
            lblLiveStatus.TabIndex = 1;
            lblLiveStatus.Text = "● TRỰC TIẾP (REAL-TIME)";
            // 
            // pnlMapCanvas
            // 
            pnlMapCanvas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlMapCanvas.BackColor = System.Drawing.Color.FromArgb(235, 242, 248);
            pnlMapCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlMapCanvas.Location = new System.Drawing.Point(10, 58);
            pnlMapCanvas.Name = "pnlMapCanvas";
            pnlMapCanvas.Size = new System.Drawing.Size(460, 230);
            pnlMapCanvas.TabIndex = 2;
            // 
            // pnlBusScroll
            // 
            pnlBusScroll.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlBusScroll.AutoScroll = true;
            pnlBusScroll.BackColor = System.Drawing.Color.Transparent;
            pnlBusScroll.Location = new System.Drawing.Point(10, 296);
            pnlBusScroll.Name = "pnlBusScroll";
            pnlBusScroll.Size = new System.Drawing.Size(460, 250);
            pnlBusScroll.TabIndex = 3;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = System.Drawing.Color.White;
            pnlFooter.Controls.Add(lblFooterStatus);
            pnlFooter.Controls.Add(lblFooterVehicles);
            pnlFooter.Controls.Add(lblFooterTime);
            pnlFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlFooter.Location = new System.Drawing.Point(0, 764);
            pnlFooter.Margin = new System.Windows.Forms.Padding(0);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new System.Drawing.Size(1200, 36);
            pnlFooter.TabIndex = 3;
            // 
            // lblFooterStatus
            // 
            lblFooterStatus.AutoSize = true;
            lblFooterStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblFooterStatus.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblFooterStatus.Location = new System.Drawing.Point(20, 8);
            lblFooterStatus.Name = "lblFooterStatus";
            lblFooterStatus.Size = new System.Drawing.Size(236, 20);
            lblFooterStatus.TabIndex = 0;
            lblFooterStatus.Text = "Hệ thống sẵn sàng · Đã đồng bộ CSDL";
            // 
            // lblFooterVehicles
            // 
            lblFooterVehicles.Anchor = System.Windows.Forms.AnchorStyles.Top;
            lblFooterVehicles.AutoSize = true;
            lblFooterVehicles.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblFooterVehicles.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblFooterVehicles.Location = new System.Drawing.Point(480, 8);
            lblFooterVehicles.Name = "lblFooterVehicles";
            lblFooterVehicles.Size = new System.Drawing.Size(225, 20);
            lblFooterVehicles.TabIndex = 1;
            lblFooterVehicles.Text = "42 phương tiện đang chạy · 3 cảnh báo";
            // 
            // lblFooterTime
            // 
            lblFooterTime.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblFooterTime.AutoSize = true;
            lblFooterTime.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblFooterTime.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblFooterTime.Location = new System.Drawing.Point(1000, 8);
            lblFooterTime.Name = "lblFooterTime";
            lblFooterTime.Size = new System.Drawing.Size(140, 20);
            lblFooterTime.TabIndex = 2;
            lblFooterTime.Text = "14/10/2026 12:00:00";
            // 
            // UcDashboard
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            Controls.Add(tlpRoot);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "UcDashboard";
            Size = new System.Drawing.Size(1200, 800);
            tlpRoot.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            tblBody.ResumeLayout(false);
            pnlChartWrap.ResumeLayout(false);
            pnlChartWrap.PerformLayout();
            pnlMapWrap.ResumeLayout(false);
            pnlMapWrap.PerformLayout();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
