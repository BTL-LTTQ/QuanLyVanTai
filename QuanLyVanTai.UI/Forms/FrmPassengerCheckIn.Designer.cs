namespace QuanLyVanTai.UI.Forms
{
    partial class FrmPassengerCheckIn
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.GroupBox grpScanner;
        private System.Windows.Forms.TextBox txtQrInput;
        private System.Windows.Forms.Label lblInputHint;
        private System.Windows.Forms.FlowLayoutPanel flpScanActions;
        private FontAwesome.Sharp.IconButton btnCheckIn;
        private FontAwesome.Sharp.IconButton btnScanFile;
        private FontAwesome.Sharp.IconButton btnRevert;
        private System.Windows.Forms.GroupBox grpBoardingCard;
        private System.Windows.Forms.Panel pnlCardStatus;
        private System.Windows.Forms.Label lblCardStatusTitle;
        private System.Windows.Forms.TableLayoutPanel tlpCardDetails;
        private System.Windows.Forms.Label lblTitleCode;
        private System.Windows.Forms.Label lblCardTicketCode;
        private System.Windows.Forms.Label lblTitleName;
        private System.Windows.Forms.Label lblCardCustomerName;
        private System.Windows.Forms.Label lblTitlePhone;
        private System.Windows.Forms.Label lblCardCustomerPhone;
        private System.Windows.Forms.Label lblTitleSeat;
        private System.Windows.Forms.Label lblCardSeat;
        private System.Windows.Forms.Label lblTitleRoute;
        private System.Windows.Forms.Label lblCardRoute;
        private System.Windows.Forms.Label lblTitleVehicle;
        private System.Windows.Forms.Label lblCardVehicle;
        private System.Windows.Forms.Label lblTitleDeparture;
        private System.Windows.Forms.Label lblCardDeparture;
        private System.Windows.Forms.Label lblTitleCheckInTime;
        private System.Windows.Forms.Label lblCardCheckInTime;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlListToolbar;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.Label lblCountBadge;
        private FontAwesome.Sharp.IconButton btnRefreshList;
        private System.Windows.Forms.DataGridView dgvCheckedIn;
        private System.Windows.Forms.Panel pnlFooter;
        private FontAwesome.Sharp.IconButton btnClose;
        private System.Windows.Forms.Label lblFooterStatus;

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
            pnlHeader = new System.Windows.Forms.Panel();
            lblHeaderSubtitle = new System.Windows.Forms.Label();
            lblHeaderTitle = new System.Windows.Forms.Label();
            splitMain = new System.Windows.Forms.SplitContainer();
            pnlLeft = new System.Windows.Forms.Panel();
            grpBoardingCard = new System.Windows.Forms.GroupBox();
            tlpCardDetails = new System.Windows.Forms.TableLayoutPanel();
            lblTitleCode = new System.Windows.Forms.Label();
            lblCardTicketCode = new System.Windows.Forms.Label();
            lblTitleName = new System.Windows.Forms.Label();
            lblCardCustomerName = new System.Windows.Forms.Label();
            lblTitlePhone = new System.Windows.Forms.Label();
            lblCardCustomerPhone = new System.Windows.Forms.Label();
            lblTitleSeat = new System.Windows.Forms.Label();
            lblCardSeat = new System.Windows.Forms.Label();
            lblTitleRoute = new System.Windows.Forms.Label();
            lblCardRoute = new System.Windows.Forms.Label();
            lblTitleVehicle = new System.Windows.Forms.Label();
            lblCardVehicle = new System.Windows.Forms.Label();
            lblTitleDeparture = new System.Windows.Forms.Label();
            lblCardDeparture = new System.Windows.Forms.Label();
            lblTitleCheckInTime = new System.Windows.Forms.Label();
            lblCardCheckInTime = new System.Windows.Forms.Label();
            pnlCardStatus = new System.Windows.Forms.Panel();
            lblCardStatusTitle = new System.Windows.Forms.Label();
            grpScanner = new System.Windows.Forms.GroupBox();
            flpScanActions = new System.Windows.Forms.FlowLayoutPanel();
            btnCheckIn = new FontAwesome.Sharp.IconButton();
            btnScanFile = new FontAwesome.Sharp.IconButton();
            btnRevert = new FontAwesome.Sharp.IconButton();
            txtQrInput = new System.Windows.Forms.TextBox();
            lblInputHint = new System.Windows.Forms.Label();
            pnlRight = new System.Windows.Forms.Panel();
            dgvCheckedIn = new System.Windows.Forms.DataGridView();
            pnlListToolbar = new System.Windows.Forms.Panel();
            btnRefreshList = new FontAwesome.Sharp.IconButton();
            lblCountBadge = new System.Windows.Forms.Label();
            lblListTitle = new System.Windows.Forms.Label();
            pnlFooter = new System.Windows.Forms.Panel();
            btnClose = new FontAwesome.Sharp.IconButton();
            lblFooterStatus = new System.Windows.Forms.Label();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            pnlLeft.SuspendLayout();
            grpBoardingCard.SuspendLayout();
            tlpCardDetails.SuspendLayout();
            pnlCardStatus.SuspendLayout();
            grpScanner.SuspendLayout();
            flpScanActions.SuspendLayout();
            pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCheckedIn).BeginInit();
            pnlListToolbar.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            pnlHeader.Controls.Add(lblHeaderSubtitle);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            pnlHeader.Size = new System.Drawing.Size(1080, 68);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            lblHeaderSubtitle.AutoSize = true;
            lblHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblHeaderSubtitle.Location = new System.Drawing.Point(20, 38);
            lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            lblHeaderSubtitle.Size = new System.Drawing.Size(567, 20);
            lblHeaderSubtitle.TabIndex = 1;
            lblHeaderSubtitle.Text = "Hỗ trợ máy quét Barcode USB, nhập trực tiếp mã vé, hoặc giải mã từ ảnh vé điện tử.";
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            lblHeaderTitle.Location = new System.Drawing.Point(20, 10);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new System.Drawing.Size(434, 28);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "📱 QUÉT MÃ QR CHECK-IN HÀNH KHÁCH";
            // 
            // splitMain
            // 
            splitMain.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            splitMain.Location = new System.Drawing.Point(0, 68);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            splitMain.Panel1.Controls.Add(pnlLeft);
            splitMain.Panel1.Padding = new System.Windows.Forms.Padding(12);
            splitMain.Panel1MinSize = 420;
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.BackColor = System.Drawing.Color.White;
            splitMain.Panel2.Controls.Add(pnlRight);
            splitMain.Panel2.Padding = new System.Windows.Forms.Padding(12);
            splitMain.Panel2MinSize = 480;
            splitMain.Size = new System.Drawing.Size(1080, 574);
            splitMain.SplitterDistance = 460;
            splitMain.TabIndex = 1;
            // 
            // pnlLeft
            // 
            pnlLeft.AutoScroll = true;
            pnlLeft.Controls.Add(grpBoardingCard);
            pnlLeft.Controls.Add(grpScanner);
            pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlLeft.Location = new System.Drawing.Point(12, 12);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new System.Drawing.Size(436, 550);
            pnlLeft.TabIndex = 0;
            // 
            // grpBoardingCard
            // 
            grpBoardingCard.BackColor = System.Drawing.Color.White;
            grpBoardingCard.Controls.Add(tlpCardDetails);
            grpBoardingCard.Controls.Add(pnlCardStatus);
            grpBoardingCard.Dock = System.Windows.Forms.DockStyle.Fill;
            grpBoardingCard.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            grpBoardingCard.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            grpBoardingCard.Location = new System.Drawing.Point(0, 160);
            grpBoardingCard.Name = "grpBoardingCard";
            grpBoardingCard.Padding = new System.Windows.Forms.Padding(10);
            grpBoardingCard.Size = new System.Drawing.Size(436, 390);
            grpBoardingCard.TabIndex = 1;
            grpBoardingCard.TabStop = false;
            grpBoardingCard.Text = " 2. Thẻ Lên Xe (Boarding Pass) ";
            // 
            // tlpCardDetails
            // 
            tlpCardDetails.ColumnCount = 2;
            tlpCardDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            tlpCardDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpCardDetails.Controls.Add(lblTitleCode, 0, 0);
            tlpCardDetails.Controls.Add(lblCardTicketCode, 1, 0);
            tlpCardDetails.Controls.Add(lblTitleName, 0, 1);
            tlpCardDetails.Controls.Add(lblCardCustomerName, 1, 1);
            tlpCardDetails.Controls.Add(lblTitlePhone, 0, 2);
            tlpCardDetails.Controls.Add(lblCardCustomerPhone, 1, 2);
            tlpCardDetails.Controls.Add(lblTitleSeat, 0, 3);
            tlpCardDetails.Controls.Add(lblCardSeat, 1, 3);
            tlpCardDetails.Controls.Add(lblTitleRoute, 0, 4);
            tlpCardDetails.Controls.Add(lblCardRoute, 1, 4);
            tlpCardDetails.Controls.Add(lblTitleVehicle, 0, 5);
            tlpCardDetails.Controls.Add(lblCardVehicle, 1, 5);
            tlpCardDetails.Controls.Add(lblTitleDeparture, 0, 6);
            tlpCardDetails.Controls.Add(lblCardDeparture, 1, 6);
            tlpCardDetails.Controls.Add(lblTitleCheckInTime, 0, 7);
            tlpCardDetails.Controls.Add(lblCardCheckInTime, 1, 7);
            tlpCardDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            tlpCardDetails.Location = new System.Drawing.Point(10, 80);
            tlpCardDetails.Name = "tlpCardDetails";
            tlpCardDetails.RowCount = 8;
            tlpCardDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            tlpCardDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            tlpCardDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            tlpCardDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            tlpCardDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            tlpCardDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            tlpCardDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            tlpCardDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            tlpCardDetails.Size = new System.Drawing.Size(416, 300);
            tlpCardDetails.TabIndex = 1;
            // 
            // lblTitleCode
            // 
            lblTitleCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblTitleCode.AutoSize = true;
            lblTitleCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblTitleCode.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTitleCode.Location = new System.Drawing.Point(3, 6);
            lblTitleCode.Name = "lblTitleCode";
            lblTitleCode.Size = new System.Drawing.Size(53, 20);
            lblTitleCode.TabIndex = 0;
            lblTitleCode.Text = "Mã vé:";
            // 
            // lblCardTicketCode
            // 
            lblCardTicketCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblCardTicketCode.AutoSize = true;
            lblCardTicketCode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblCardTicketCode.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblCardTicketCode.Location = new System.Drawing.Point(113, 4);
            lblCardTicketCode.Name = "lblCardTicketCode";
            lblCardTicketCode.Size = new System.Drawing.Size(27, 23);
            lblCardTicketCode.TabIndex = 1;
            lblCardTicketCode.Text = "—";
            // 
            // lblTitleName
            // 
            lblTitleName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblTitleName.AutoSize = true;
            lblTitleName.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblTitleName.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTitleName.Location = new System.Drawing.Point(3, 38);
            lblTitleName.Name = "lblTitleName";
            lblTitleName.Size = new System.Drawing.Size(89, 20);
            lblTitleName.TabIndex = 2;
            lblTitleName.Text = "Hành khách:";
            // 
            // lblCardCustomerName
            // 
            lblCardCustomerName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblCardCustomerName.AutoSize = true;
            lblCardCustomerName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            lblCardCustomerName.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblCardCustomerName.Location = new System.Drawing.Point(113, 37);
            lblCardCustomerName.Name = "lblCardCustomerName";
            lblCardCustomerName.Size = new System.Drawing.Size(25, 21);
            lblCardCustomerName.TabIndex = 3;
            lblCardCustomerName.Text = "—";
            // 
            // lblTitlePhone
            // 
            lblTitlePhone.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblTitlePhone.AutoSize = true;
            lblTitlePhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblTitlePhone.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTitlePhone.Location = new System.Drawing.Point(3, 70);
            lblTitlePhone.Name = "lblTitlePhone";
            lblTitlePhone.Size = new System.Drawing.Size(100, 20);
            lblTitlePhone.TabIndex = 4;
            lblTitlePhone.Text = "Số điện thoại:";
            // 
            // lblCardCustomerPhone
            // 
            lblCardCustomerPhone.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblCardCustomerPhone.AutoSize = true;
            lblCardCustomerPhone.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lblCardCustomerPhone.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblCardCustomerPhone.Location = new System.Drawing.Point(113, 69);
            lblCardCustomerPhone.Name = "lblCardCustomerPhone";
            lblCardCustomerPhone.Size = new System.Drawing.Size(25, 21);
            lblCardCustomerPhone.TabIndex = 5;
            lblCardCustomerPhone.Text = "—";
            // 
            // lblTitleSeat
            // 
            lblTitleSeat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblTitleSeat.AutoSize = true;
            lblTitleSeat.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblTitleSeat.ForeColor = System.Drawing.Color.FromArgb(14, 165, 233);
            lblTitleSeat.Location = new System.Drawing.Point(3, 110);
            lblTitleSeat.Name = "lblTitleSeat";
            lblTitleSeat.Size = new System.Drawing.Size(68, 21);
            lblTitleSeat.TabIndex = 6;
            lblTitleSeat.Text = "VỊ TRÍ:";
            // 
            // lblCardSeat
            // 
            lblCardSeat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblCardSeat.AutoSize = true;
            lblCardSeat.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            lblCardSeat.ForeColor = System.Drawing.Color.FromArgb(14, 165, 233);
            lblCardSeat.Location = new System.Drawing.Point(113, 98);
            lblCardSeat.Name = "lblCardSeat";
            lblCardSeat.Size = new System.Drawing.Size(53, 46);
            lblCardSeat.TabIndex = 7;
            lblCardSeat.Text = "—";
            // 
            // lblTitleRoute
            // 
            lblTitleRoute.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblTitleRoute.AutoSize = true;
            lblTitleRoute.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblTitleRoute.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTitleRoute.Location = new System.Drawing.Point(3, 152);
            lblTitleRoute.Name = "lblTitleRoute";
            lblTitleRoute.Size = new System.Drawing.Size(70, 20);
            lblTitleRoute.TabIndex = 8;
            lblTitleRoute.Text = "Tuyến xe:";
            // 
            // lblCardRoute
            // 
            lblCardRoute.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblCardRoute.AutoSize = true;
            lblCardRoute.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            lblCardRoute.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblCardRoute.Location = new System.Drawing.Point(113, 152);
            lblCardRoute.Name = "lblCardRoute";
            lblCardRoute.Size = new System.Drawing.Size(25, 20);
            lblCardRoute.TabIndex = 9;
            lblCardRoute.Text = "—";
            // 
            // lblTitleVehicle
            // 
            lblTitleVehicle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblTitleVehicle.AutoSize = true;
            lblTitleVehicle.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblTitleVehicle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTitleVehicle.Location = new System.Drawing.Point(3, 184);
            lblTitleVehicle.Name = "lblTitleVehicle";
            lblTitleVehicle.Size = new System.Drawing.Size(79, 20);
            lblTitleVehicle.TabIndex = 10;
            lblTitleVehicle.Text = "Biển số xe:";
            // 
            // lblCardVehicle
            // 
            lblCardVehicle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblCardVehicle.AutoSize = true;
            lblCardVehicle.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblCardVehicle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblCardVehicle.Location = new System.Drawing.Point(113, 184);
            lblCardVehicle.Name = "lblCardVehicle";
            lblCardVehicle.Size = new System.Drawing.Size(25, 20);
            lblCardVehicle.TabIndex = 11;
            lblCardVehicle.Text = "—";
            // 
            // lblTitleDeparture
            // 
            lblTitleDeparture.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblTitleDeparture.AutoSize = true;
            lblTitleDeparture.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblTitleDeparture.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTitleDeparture.Location = new System.Drawing.Point(3, 216);
            lblTitleDeparture.Name = "lblTitleDeparture";
            lblTitleDeparture.Size = new System.Drawing.Size(83, 20);
            lblTitleDeparture.TabIndex = 12;
            lblTitleDeparture.Text = "Khởi hành:";
            // 
            // lblCardDeparture
            // 
            lblCardDeparture.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblCardDeparture.AutoSize = true;
            lblCardDeparture.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblCardDeparture.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblCardDeparture.Location = new System.Drawing.Point(113, 216);
            lblCardDeparture.Name = "lblCardDeparture";
            lblCardDeparture.Size = new System.Drawing.Size(25, 20);
            lblCardDeparture.TabIndex = 13;
            lblCardDeparture.Text = "—";
            // 
            // lblTitleCheckInTime
            // 
            lblTitleCheckInTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblTitleCheckInTime.AutoSize = true;
            lblTitleCheckInTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblTitleCheckInTime.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblTitleCheckInTime.Location = new System.Drawing.Point(3, 258);
            lblTitleCheckInTime.Name = "lblTitleCheckInTime";
            lblTitleCheckInTime.Size = new System.Drawing.Size(95, 20);
            lblTitleCheckInTime.TabIndex = 14;
            lblTitleCheckInTime.Text = "Giờ check-in:";
            // 
            // lblCardCheckInTime
            // 
            lblCardCheckInTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblCardCheckInTime.AutoSize = true;
            lblCardCheckInTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblCardCheckInTime.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblCardCheckInTime.Location = new System.Drawing.Point(113, 258);
            lblCardCheckInTime.Name = "lblCardCheckInTime";
            lblCardCheckInTime.Size = new System.Drawing.Size(25, 20);
            lblCardCheckInTime.TabIndex = 15;
            lblCardCheckInTime.Text = "—";
            // 
            // pnlCardStatus
            // 
            pnlCardStatus.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            pnlCardStatus.Controls.Add(lblCardStatusTitle);
            pnlCardStatus.Dock = System.Windows.Forms.DockStyle.Top;
            pnlCardStatus.Location = new System.Drawing.Point(10, 32);
            pnlCardStatus.Name = "pnlCardStatus";
            pnlCardStatus.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            pnlCardStatus.Size = new System.Drawing.Size(416, 48);
            pnlCardStatus.TabIndex = 0;
            // 
            // lblCardStatusTitle
            // 
            lblCardStatusTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            lblCardStatusTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblCardStatusTitle.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblCardStatusTitle.Location = new System.Drawing.Point(10, 8);
            lblCardStatusTitle.Name = "lblCardStatusTitle";
            lblCardStatusTitle.Size = new System.Drawing.Size(396, 32);
            lblCardStatusTitle.TabIndex = 0;
            lblCardStatusTitle.Text = "CHỜ QUÉT MÃ QR...";
            lblCardStatusTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpScanner
            // 
            grpScanner.BackColor = System.Drawing.Color.White;
            grpScanner.Controls.Add(flpScanActions);
            grpScanner.Controls.Add(txtQrInput);
            grpScanner.Controls.Add(lblInputHint);
            grpScanner.Dock = System.Windows.Forms.DockStyle.Top;
            grpScanner.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            grpScanner.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            grpScanner.Location = new System.Drawing.Point(0, 0);
            grpScanner.Name = "grpScanner";
            grpScanner.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            grpScanner.Size = new System.Drawing.Size(436, 160);
            grpScanner.TabIndex = 0;
            grpScanner.TabStop = false;
            grpScanner.Text = " 1. Nhập / Quét mã QR vé ";
            // 
            // flpScanActions
            // 
            flpScanActions.Controls.Add(btnCheckIn);
            flpScanActions.Controls.Add(btnScanFile);
            flpScanActions.Controls.Add(btnRevert);
            flpScanActions.Dock = System.Windows.Forms.DockStyle.Fill;
            flpScanActions.Location = new System.Drawing.Point(12, 85);
            flpScanActions.Name = "flpScanActions";
            flpScanActions.Size = new System.Drawing.Size(412, 65);
            flpScanActions.TabIndex = 2;
            // 
            // btnCheckIn
            // 
            btnCheckIn.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            btnCheckIn.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCheckIn.FlatAppearance.BorderSize = 0;
            btnCheckIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCheckIn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnCheckIn.ForeColor = System.Drawing.Color.White;
            btnCheckIn.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnCheckIn.IconColor = System.Drawing.Color.White;
            btnCheckIn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCheckIn.IconSize = 16;
            btnCheckIn.Location = new System.Drawing.Point(3, 3);
            btnCheckIn.Name = "btnCheckIn";
            btnCheckIn.Size = new System.Drawing.Size(130, 36);
            btnCheckIn.TabIndex = 0;
            btnCheckIn.Text = " Check-in";
            btnCheckIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnCheckIn.UseVisualStyleBackColor = false;
            // 
            // btnScanFile
            // 
            btnScanFile.BackColor = System.Drawing.Color.FromArgb(14, 165, 233);
            btnScanFile.Cursor = System.Windows.Forms.Cursors.Hand;
            btnScanFile.FlatAppearance.BorderSize = 0;
            btnScanFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnScanFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnScanFile.ForeColor = System.Drawing.Color.White;
            btnScanFile.IconChar = FontAwesome.Sharp.IconChar.FolderOpen;
            btnScanFile.IconColor = System.Drawing.Color.White;
            btnScanFile.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnScanFile.IconSize = 16;
            btnScanFile.Location = new System.Drawing.Point(139, 3);
            btnScanFile.Name = "btnScanFile";
            btnScanFile.Size = new System.Drawing.Size(155, 36);
            btnScanFile.TabIndex = 1;
            btnScanFile.Text = " Chọn tệp QR...";
            btnScanFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnScanFile.UseVisualStyleBackColor = false;
            // 
            // btnRevert
            // 
            btnRevert.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            btnRevert.Cursor = System.Windows.Forms.Cursors.Hand;
            btnRevert.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnRevert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRevert.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            btnRevert.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnRevert.IconChar = FontAwesome.Sharp.IconChar.RotateLeft;
            btnRevert.IconColor = System.Drawing.Color.FromArgb(100, 116, 139);
            btnRevert.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRevert.IconSize = 15;
            btnRevert.Location = new System.Drawing.Point(300, 3);
            btnRevert.Name = "btnRevert";
            btnRevert.Size = new System.Drawing.Size(100, 36);
            btnRevert.TabIndex = 2;
            btnRevert.Text = " Hoàn tác";
            btnRevert.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnRevert.UseVisualStyleBackColor = false;
            // 
            // txtQrInput
            // 
            txtQrInput.Dock = System.Windows.Forms.DockStyle.Top;
            txtQrInput.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            txtQrInput.Location = new System.Drawing.Point(12, 51);
            txtQrInput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtQrInput.Name = "txtQrInput";
            txtQrInput.PlaceholderText = "Quét mã QR hoặc nhập chuỗi vé rồi nhấn Enter...";
            txtQrInput.Size = new System.Drawing.Size(412, 31);
            txtQrInput.TabIndex = 1;
            // 
            // lblInputHint
            // 
            lblInputHint.AutoSize = true;
            lblInputHint.Dock = System.Windows.Forms.DockStyle.Top;
            lblInputHint.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblInputHint.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblInputHint.Location = new System.Drawing.Point(12, 31);
            lblInputHint.Name = "lblInputHint";
            lblInputHint.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            lblInputHint.Size = new System.Drawing.Size(329, 20);
            lblInputHint.TabIndex = 0;
            lblInputHint.Text = "Nhập mã vé (VD: TK20261001) hoặc quét chuỗi QR:";
            // 
            // pnlRight
            // 
            pnlRight.Controls.Add(dgvCheckedIn);
            pnlRight.Controls.Add(pnlListToolbar);
            pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlRight.Location = new System.Drawing.Point(12, 12);
            pnlRight.Name = "pnlRight";
            pnlRight.Size = new System.Drawing.Size(592, 550);
            pnlRight.TabIndex = 0;
            // 
            // dgvCheckedIn
            // 
            dgvCheckedIn.AllowUserToAddRows = false;
            dgvCheckedIn.AllowUserToDeleteRows = false;
            dgvCheckedIn.BackgroundColor = System.Drawing.Color.White;
            dgvCheckedIn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvCheckedIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCheckedIn.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvCheckedIn.Location = new System.Drawing.Point(0, 50);
            dgvCheckedIn.Name = "dgvCheckedIn";
            dgvCheckedIn.ReadOnly = true;
            dgvCheckedIn.RowHeadersVisible = false;
            dgvCheckedIn.RowHeadersWidth = 51;
            dgvCheckedIn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvCheckedIn.Size = new System.Drawing.Size(592, 500);
            dgvCheckedIn.TabIndex = 1;
            // 
            // pnlListToolbar
            // 
            pnlListToolbar.BackColor = System.Drawing.Color.White;
            pnlListToolbar.Controls.Add(btnRefreshList);
            pnlListToolbar.Controls.Add(lblCountBadge);
            pnlListToolbar.Controls.Add(lblListTitle);
            pnlListToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            pnlListToolbar.Location = new System.Drawing.Point(0, 0);
            pnlListToolbar.Name = "pnlListToolbar";
            pnlListToolbar.Size = new System.Drawing.Size(592, 50);
            pnlListToolbar.TabIndex = 0;
            // 
            // btnRefreshList
            // 
            btnRefreshList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnRefreshList.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            btnRefreshList.Cursor = System.Windows.Forms.Cursors.Hand;
            btnRefreshList.FlatAppearance.BorderSize = 0;
            btnRefreshList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRefreshList.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            btnRefreshList.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnRefreshList.IconChar = FontAwesome.Sharp.IconChar.RotateLeft;
            btnRefreshList.IconColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnRefreshList.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRefreshList.IconSize = 14;
            btnRefreshList.Location = new System.Drawing.Point(472, 8);
            btnRefreshList.Name = "btnRefreshList";
            btnRefreshList.Size = new System.Drawing.Size(110, 32);
            btnRefreshList.TabIndex = 2;
            btnRefreshList.Text = " Làm mới";
            btnRefreshList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnRefreshList.UseVisualStyleBackColor = false;
            // 
            // lblCountBadge
            // 
            lblCountBadge.AutoSize = true;
            lblCountBadge.BackColor = System.Drawing.Color.FromArgb(238, 242, 255);
            lblCountBadge.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            lblCountBadge.ForeColor = System.Drawing.Color.FromArgb(79, 70, 229);
            lblCountBadge.Location = new System.Drawing.Point(230, 14);
            lblCountBadge.Name = "lblCountBadge";
            lblCountBadge.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
            lblCountBadge.Size = new System.Drawing.Size(73, 24);
            lblCountBadge.TabIndex = 1;
            lblCountBadge.Text = "0 khách";
            // 
            // lblListTitle
            // 
            lblListTitle.AutoSize = true;
            lblListTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            lblListTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblListTitle.Location = new System.Drawing.Point(3, 14);
            lblListTitle.Name = "lblListTitle";
            lblListTitle.Size = new System.Drawing.Size(221, 25);
            lblListTitle.TabIndex = 0;
            lblListTitle.Text = "📋 Khách đã lên xe trong ca:";
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = System.Drawing.Color.White;
            pnlFooter.Controls.Add(btnClose);
            pnlFooter.Controls.Add(lblFooterStatus);
            pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlFooter.Location = new System.Drawing.Point(0, 642);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            pnlFooter.Size = new System.Drawing.Size(1080, 52);
            pnlFooter.TabIndex = 2;
            // 
            // btnClose
            // 
            btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClose.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnClose.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            btnClose.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            btnClose.IconColor = System.Drawing.Color.FromArgb(100, 116, 139);
            btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClose.IconSize = 16;
            btnClose.Location = new System.Drawing.Point(964, 8);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(100, 36);
            btnClose.TabIndex = 1;
            btnClose.Text = " Đóng";
            btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // lblFooterStatus
            // 
            lblFooterStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lblFooterStatus.AutoSize = true;
            lblFooterStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblFooterStatus.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblFooterStatus.Location = new System.Drawing.Point(16, 16);
            lblFooterStatus.Name = "lblFooterStatus";
            lblFooterStatus.Size = new System.Drawing.Size(325, 20);
            lblFooterStatus.TabIndex = 0;
            lblFooterStatus.Text = "Hệ thống sẵn sàng tiếp nhận mã QR hành khách.";
            // 
            // FrmPassengerCheckIn
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1080, 694);
            Controls.Add(splitMain);
            Controls.Add(pnlFooter);
            Controls.Add(pnlHeader);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            MinimumSize = new System.Drawing.Size(980, 640);
            Name = "FrmPassengerCheckIn";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Check-in Hành Khách Bằng Quét Mã QR";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            grpBoardingCard.ResumeLayout(false);
            tlpCardDetails.ResumeLayout(false);
            tlpCardDetails.PerformLayout();
            pnlCardStatus.ResumeLayout(false);
            grpScanner.ResumeLayout(false);
            grpScanner.PerformLayout();
            flpScanActions.ResumeLayout(false);
            pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCheckedIn).EndInit();
            pnlListToolbar.ResumeLayout(false);
            pnlListToolbar.PerformLayout();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
