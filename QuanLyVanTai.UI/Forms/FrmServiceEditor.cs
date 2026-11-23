using System.ComponentModel;
using Core.Configs;
using FontAwesome.Sharp;

namespace QuanLyVanTai.UI.Forms
{
    /// <summary>Dữ liệu khởi tạo cho hộp thoại thêm/sửa dịch vụ.</summary>
    public record ServiceEditorSeed(string Name, string Description, string Category, decimal Price, bool IsActive);

    public partial class FrmServiceEditor : Form
    {
        public string ServiceName => txtName.Text.Trim();
        public string Category => cboCategory.SelectedItem?.ToString() ?? "Vận chuyển";
        public decimal Price => numPrice.Value;
        public string DescriptionText => txtDescription.Text.Trim();
        public bool IsActive => chkActive.Checked;

        public FrmServiceEditor() : this(null)
        {
        }

        public FrmServiceEditor(ServiceEditorSeed? seed)
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || this.DesignMode)
            {
                return;
            }

            ThemeConfig.StyleSuccessButton(btnSave, IconChar.FloppyDisk);
            ThemeConfig.StyleSecondaryButton(btnCancel, IconChar.Xmark);

            cboCategory.Items.Clear();
            cboCategory.Items.AddRange(["Vận chuyển", "Bảo hiểm", "Ưu đãi", "Hỗ trợ"]);
            cboCategory.SelectedIndex = 0;

            btnSave.Click += btnSave_Click;
            txtName.TextChanged += (s, e) => errorProvider.SetError(txtName, string.Empty);

            if (seed != null)
            {
                Text = "Chỉnh sửa dịch vụ";
                btnSave.Text = " Lưu thay đổi";
                txtName.Text = seed.Name;
                txtDescription.Text = seed.Description;
                if (cboCategory.Items.Contains(seed.Category))
                    cboCategory.SelectedItem = seed.Category;
                numPrice.Value = Math.Clamp(seed.Price, numPrice.Minimum, numPrice.Maximum);
                chkActive.Checked = seed.IsActive;
            }
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorProvider.SetError(txtName, "Vui lòng nhập tên dịch vụ!");
                txtName.Focus();
                return;
            }

            errorProvider.SetError(txtName, string.Empty);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
