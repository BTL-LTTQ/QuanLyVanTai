using Core.Configs;
using FontAwesome.Sharp;

namespace QuanLyVanTai.UI.Forms
{
    public class FrmConfirmDelete : Form
    {
        public bool IsSoftDelete => radSoftDelete.Checked;

        private readonly PictureBox picWarning = new();
        private readonly Label lblPrompt = new();
        private readonly Label lblItemInfo = new();
        private readonly RadioButton radSoftDelete = new();
        private readonly RadioButton radHardDelete = new();
        private readonly IconButton btnConfirm = new();
        private readonly IconButton btnCancel = new();

        public FrmConfirmDelete(string itemName, string itemDetails = "")
        {
            InitializeComponent(itemName, itemDetails);
        }

        private void InitializeComponent(string itemName, string itemDetails)
        {
            this.Text = "Xác nhận xóa an toàn dữ liệu";
            this.Size = new Size(540, 360);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.Font = ThemeConfig.MainFont;

            // Warning Icon
            picWarning.Size = new Size(50, 50);
            picWarning.Location = new Point(25, 25);
            picWarning.Image = SystemIcons.Warning.ToBitmap();
            picWarning.SizeMode = PictureBoxSizeMode.CenterImage;
            this.Controls.Add(picWarning);

            // Title
            lblPrompt.Text = "BẠN CÓ CHẮC CHẮN MUỐN XÓA BẢN GHI NÀY?";
            lblPrompt.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblPrompt.ForeColor = ThemeConfig.DangerColor;
            lblPrompt.AutoSize = true;
            lblPrompt.Location = new Point(85, 25);
            this.Controls.Add(lblPrompt);

            // Item Info
            lblItemInfo.Text = $"Đối tượng xóa: {itemName}\n{itemDetails}";
            lblItemInfo.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            lblItemInfo.ForeColor = ThemeConfig.TextMain;
            lblItemInfo.Location = new Point(85, 58);
            lblItemInfo.Size = new Size(420, 50);
            this.Controls.Add(lblItemInfo);

            // Group Options Panel
            Panel pnlOptions = new Panel
            {
                Location = new Point(35, 120),
                Size = new Size(460, 110),
                BackColor = Color.FromArgb(248, 250, 252),
                BorderStyle = BorderStyle.FixedSingle
            };

            radSoftDelete.Text = " Xóa mềm (Soft Delete - Khuyên dùng)\n   Ẩn bản ghi khỏi giao diện, bảo toàn lịch sử và vé liên quan.";
            radSoftDelete.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            radSoftDelete.ForeColor = Color.FromArgb(16, 185, 129); // Green
            radSoftDelete.Checked = true;
            radSoftDelete.Location = new Point(15, 12);
            radSoftDelete.Size = new Size(430, 42);
            pnlOptions.Controls.Add(radSoftDelete);

            radHardDelete.Text = " Xóa vĩnh viễn (Cascade Transaction)\n   Xóa triệt để khỏi CSDL. Sẽ tự động Rollback nếu xảy ra lỗi.";
            radHardDelete.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            radHardDelete.ForeColor = ThemeConfig.DangerColor;
            radHardDelete.Location = new Point(15, 58);
            radHardDelete.Size = new Size(430, 42);
            pnlOptions.Controls.Add(radHardDelete);

            this.Controls.Add(pnlOptions);

            // Buttons
            ThemeConfig.StyleDangerButton(btnConfirm, IconChar.Trash);
            btnConfirm.Text = " Xác nhận xóa";
            btnConfirm.Size = new Size(150, 40);
            btnConfirm.Location = new Point(205, 255);
            btnConfirm.DialogResult = DialogResult.OK;
            this.Controls.Add(btnConfirm);

            ThemeConfig.StyleSecondaryButton(btnCancel, IconChar.Xmark);
            btnCancel.Text = " Hủy bỏ";
            btnCancel.Size = new Size(110, 40);
            btnCancel.Location = new Point(365, 255);
            btnCancel.DialogResult = DialogResult.Cancel;
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnConfirm;
            this.CancelButton = btnCancel;
        }
    }
}
