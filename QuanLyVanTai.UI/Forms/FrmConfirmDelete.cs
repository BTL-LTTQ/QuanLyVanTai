using System.ComponentModel;
using Core.Configs;
using FontAwesome.Sharp;

namespace QuanLyVanTai.UI.Forms
{
    public partial class FrmConfirmDelete : Form
    {
        public bool IsSoftDelete => radSoftDelete.Checked;

        public FrmConfirmDelete() : this("Dữ liệu mẫu", "")
        {
        }

        public FrmConfirmDelete(string itemName, string itemDetails = "")
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || this.DesignMode)
            {
                return;
            }

            picWarning.Image = SystemIcons.Warning.ToBitmap();
            lblItemInfo.Text = $"Đối tượng xóa: {itemName}\n{itemDetails}";

            ThemeConfig.StyleDangerButton(btnConfirm, IconChar.Trash);
            ThemeConfig.StyleSecondaryButton(btnCancel, IconChar.Xmark);
        }
    }
}
