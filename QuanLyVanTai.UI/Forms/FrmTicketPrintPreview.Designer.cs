namespace QuanLyVanTai.UI.Forms
{
    partial class FrmTicketPrintPreview
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
            tsTool = new ToolStrip();
            btnPrint = new ToolStripButton();
            btnExportPdf = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnZoomIn = new ToolStripButton();
            btnZoomOut = new ToolStripButton();
            btnFit = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnPrevPage = new ToolStripButton();
            lblPageInfo = new ToolStripLabel();
            btnNextPage = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            btnClose = new ToolStripButton();
            ssStatus = new StatusStrip();
            lblTicketSummary = new ToolStripStatusLabel();
            lblPrinterName = new ToolStripStatusLabel();
            printPreviewControl = new PrintPreviewControl();
            tsTool.SuspendLayout();
            ssStatus.SuspendLayout();
            SuspendLayout();
            // 
            // tsTool
            // 
            tsTool.BackColor = Color.White;
            tsTool.Font = new Font("Segoe UI", 9.5F);
            tsTool.GripStyle = ToolStripGripStyle.Hidden;
            tsTool.ImageScalingSize = new Size(20, 20);
            tsTool.Items.AddRange(new ToolStripItem[] { btnPrint, btnExportPdf, toolStripSeparator1, btnZoomIn, btnZoomOut, btnFit, toolStripSeparator2, btnPrevPage, lblPageInfo, btnNextPage, toolStripSeparator3, btnClose });
            tsTool.Location = new Point(0, 0);
            tsTool.Name = "tsTool";
            tsTool.Padding = new Padding(9, 5, 9, 5);
            tsTool.Size = new Size(1120, 38);
            tsTool.TabIndex = 0;
            tsTool.Text = "tsTool";
            // 
            // btnPrint
            // 
            btnPrint.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnPrint.ForeColor = Color.FromArgb(37, 99, 235);
            btnPrint.ImageTransparentColor = Color.Magenta;
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(115, 25);
            btnPrint.Text = "🖨 In vé ngay";
            // 
            // btnExportPdf
            // 
            btnExportPdf.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnExportPdf.ForeColor = Color.FromArgb(220, 38, 38);
            btnExportPdf.ImageTransparentColor = Color.Magenta;
            btnExportPdf.Name = "btnExportPdf";
            btnExportPdf.Size = new Size(124, 25);
            btnExportPdf.Text = "📄 Xuất ra PDF";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 28);
            // 
            // btnZoomIn
            // 
            btnZoomIn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnZoomIn.Name = "btnZoomIn";
            btnZoomIn.Size = new Size(103, 25);
            btnZoomIn.Text = "🔍 Phóng to";
            // 
            // btnZoomOut
            // 
            btnZoomOut.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnZoomOut.Name = "btnZoomOut";
            btnZoomOut.Size = new Size(97, 25);
            btnZoomOut.Text = "🔍 Thu nhỏ";
            // 
            // btnFit
            // 
            btnFit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnFit.Name = "btnFit";
            btnFit.Size = new Size(108, 25);
            btnFit.Text = "📐 Vừa trang";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 28);
            // 
            // btnPrevPage
            // 
            btnPrevPage.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnPrevPage.Name = "btnPrevPage";
            btnPrevPage.Size = new Size(53, 25);
            btnPrevPage.Text = "◀ Lùi";
            // 
            // lblPageInfo
            // 
            lblPageInfo.Font = new Font("Segoe UI Semibold", 9F);
            lblPageInfo.ForeColor = Color.FromArgb(71, 85, 105);
            lblPageInfo.Name = "lblPageInfo";
            lblPageInfo.Size = new Size(78, 25);
            lblPageInfo.Text = "Trang 1 / 1";
            // 
            // btnNextPage
            // 
            btnNextPage.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnNextPage.Name = "btnNextPage";
            btnNextPage.Size = new Size(61, 25);
            btnNextPage.Text = "Tiếp ▶";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 28);
            // 
            // btnClose
            // 
            btnClose.Alignment = ToolStripItemAlignment.Right;
            btnClose.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnClose.ForeColor = Color.FromArgb(100, 116, 139);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(78, 25);
            btnClose.Text = "✖ Đóng";
            // 
            // ssStatus
            // 
            ssStatus.BackColor = Color.White;
            ssStatus.Font = new Font("Segoe UI", 9F);
            ssStatus.ImageScalingSize = new Size(20, 20);
            ssStatus.Items.AddRange(new ToolStripItem[] { lblTicketSummary, lblPrinterName });
            ssStatus.Location = new Point(0, 850);
            ssStatus.Name = "ssStatus";
            ssStatus.Padding = new Padding(1, 0, 16, 0);
            ssStatus.Size = new Size(1120, 30);
            ssStatus.TabIndex = 1;
            ssStatus.Text = "ssStatus";
            // 
            // lblTicketSummary
            // 
            lblTicketSummary.Font = new Font("Segoe UI Semibold", 9F);
            lblTicketSummary.ForeColor = Color.FromArgb(37, 99, 235);
            lblTicketSummary.Name = "lblTicketSummary";
            lblTicketSummary.Size = new Size(142, 24);
            lblTicketSummary.Text = "Tổng cộng: 1 thẻ vé";
            // 
            // lblPrinterName
            // 
            lblPrinterName.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblPrinterName.BorderStyle = Border3DStyle.Etched;
            lblPrinterName.ForeColor = Color.FromArgb(71, 85, 105);
            lblPrinterName.Name = "lblPrinterName";
            lblPrinterName.Size = new Size(961, 24);
            lblPrinterName.Spring = true;
            lblPrinterName.Text = "Khổ giấy: A5 Landscape (Khổ ngang tiêu chuẩn vé xe liên tỉnh)";
            lblPrinterName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // printPreviewControl
            // 
            printPreviewControl.AutoZoom = false;
            printPreviewControl.BackColor = Color.FromArgb(203, 213, 225);
            printPreviewControl.Dock = DockStyle.Fill;
            printPreviewControl.Location = new Point(0, 38);
            printPreviewControl.Margin = new Padding(3, 4, 3, 4);
            printPreviewControl.Name = "printPreviewControl";
            printPreviewControl.Size = new Size(1120, 812);
            printPreviewControl.TabIndex = 2;
            printPreviewControl.Zoom = 1D;
            // 
            // FrmTicketPrintPreview
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(1120, 880);
            Controls.Add(printPreviewControl);
            Controls.Add(ssStatus);
            Controls.Add(tsTool);
            Font = new Font("Segoe UI", 9F);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(935, 731);
            Name = "FrmTicketPrintPreview";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Xem Trước & In Vé Xe Điện Tử Có Mã QR - Boarding Pass Preview";
            tsTool.ResumeLayout(false);
            tsTool.PerformLayout();
            ssStatus.ResumeLayout(false);
            ssStatus.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip tsTool;
        private ToolStripButton btnPrint;
        private ToolStripButton btnExportPdf;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnZoomIn;
        private ToolStripButton btnZoomOut;
        private ToolStripButton btnFit;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnPrevPage;
        private ToolStripLabel lblPageInfo;
        private ToolStripButton btnNextPage;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton btnClose;
        private StatusStrip ssStatus;
        private ToolStripStatusLabel lblTicketSummary;
        private ToolStripStatusLabel lblPrinterName;
        private PrintPreviewControl printPreviewControl;
    }
}
