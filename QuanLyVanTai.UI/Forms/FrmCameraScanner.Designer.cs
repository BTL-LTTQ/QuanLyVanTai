namespace QuanLyVanTai.UI.Forms
{
    partial class FrmCameraScanner
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpCamera;
        private System.Windows.Forms.Label lblCamera;
        private System.Windows.Forms.ComboBox cboCamera;
        private FontAwesome.Sharp.IconButton btnStart;
        private FontAwesome.Sharp.IconButton btnStop;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.FlowLayoutPanel flpActions;
        private FontAwesome.Sharp.IconButton btnClose;
        private System.Windows.Forms.Timer previewTimer;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _closing = true;
                ReleaseCamera();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpMain = new System.Windows.Forms.TableLayoutPanel();
            tlpCamera = new System.Windows.Forms.TableLayoutPanel();
            lblCamera = new System.Windows.Forms.Label();
            cboCamera = new System.Windows.Forms.ComboBox();
            btnStart = new FontAwesome.Sharp.IconButton();
            btnStop = new FontAwesome.Sharp.IconButton();
            picPreview = new System.Windows.Forms.PictureBox();
            lblStatus = new System.Windows.Forms.Label();
            flpActions = new System.Windows.Forms.FlowLayoutPanel();
            btnClose = new FontAwesome.Sharp.IconButton();
            previewTimer = new System.Windows.Forms.Timer(components);
            tlpMain.SuspendLayout();
            tlpCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
            flpActions.SuspendLayout();
            SuspendLayout();

            tlpMain.ColumnCount = 1;
            tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpMain.Controls.Add(tlpCamera, 0, 0);
            tlpMain.Controls.Add(picPreview, 0, 1);
            tlpMain.Controls.Add(lblStatus, 0, 2);
            tlpMain.Controls.Add(flpActions, 0, 3);
            tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new System.Windows.Forms.Padding(12);
            tlpMain.RowCount = 4;
            tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            tlpMain.Size = new System.Drawing.Size(820, 640);
            tlpMain.TabIndex = 0;

            tlpCamera.ColumnCount = 4;
            tlpCamera.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            tlpCamera.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpCamera.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            tlpCamera.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            tlpCamera.Controls.Add(lblCamera, 0, 0);
            tlpCamera.Controls.Add(cboCamera, 1, 0);
            tlpCamera.Controls.Add(btnStart, 2, 0);
            tlpCamera.Controls.Add(btnStop, 3, 0);
            tlpCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            tlpCamera.Margin = new System.Windows.Forms.Padding(0);
            tlpCamera.Name = "tlpCamera";
            tlpCamera.RowCount = 1;
            tlpCamera.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpCamera.TabIndex = 0;

            lblCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            lblCamera.Name = "lblCamera";
            lblCamera.Text = "Camera:";
            lblCamera.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblCamera.TabIndex = 0;

            cboCamera.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cboCamera.DisplayMember = "Name";
            cboCamera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboCamera.Enabled = false;
            cboCamera.Name = "cboCamera";
            cboCamera.TabIndex = 1;

            btnStart.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            btnStart.Enabled = false;
            btnStart.IconChar = FontAwesome.Sharp.IconChar.Camera;
            btnStart.IconSize = 18;
            btnStart.Name = "btnStart";
            btnStart.Size = new System.Drawing.Size(144, 36);
            btnStart.TabIndex = 2;
            btnStart.Text = " Bật camera";
            btnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;

            btnStop.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            btnStop.Enabled = false;
            btnStop.IconChar = FontAwesome.Sharp.IconChar.Stop;
            btnStop.IconSize = 18;
            btnStop.Name = "btnStop";
            btnStop.Size = new System.Drawing.Size(104, 36);
            btnStop.TabIndex = 3;
            btnStop.Text = " Dừng";
            btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;

            picPreview.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            picPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            picPreview.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            picPreview.Name = "picPreview";
            picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            picPreview.TabIndex = 1;
            picPreview.TabStop = false;

            lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            lblStatus.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Chọn camera rồi bấm Bật camera. Đưa mã QR trên điện thoại hoặc vé giấy trước camera.\r\nĐọc được mã sẽ tự đóng cửa sổ và xử lý check-in một lần.";

            flpActions.Controls.Add(btnClose);
            flpActions.Dock = System.Windows.Forms.DockStyle.Fill;
            flpActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            flpActions.Margin = new System.Windows.Forms.Padding(0);
            flpActions.Name = "flpActions";
            flpActions.TabIndex = 3;
            flpActions.WrapContents = true;

            btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnClose.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            btnClose.IconSize = 18;
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(110, 36);
            btnClose.TabIndex = 0;
            btnClose.Text = " Đóng";
            btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;

            previewTimer.Interval = 100;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            CancelButton = btnClose;
            ClientSize = new System.Drawing.Size(820, 640);
            Controls.Add(tlpMain);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            MinimumSize = new System.Drawing.Size(720, 540);
            Name = "FrmCameraScanner";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Quét mã QR bằng camera";
            tlpMain.ResumeLayout(false);
            tlpCamera.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
            flpActions.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
