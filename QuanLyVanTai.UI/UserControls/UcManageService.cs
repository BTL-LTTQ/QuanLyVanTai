using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyVanTai.UI.UserControls;

namespace QuanLyVanTai.UI
{
    public partial class UcManageService : BaseManagementForm
    {
        public UcManageService()
        {
            InitializeComponent();
        }
        
        // Override the Add button click
        protected override void OnAdd()
        {
            base.OnAdd();
            MessageBox.Show("Mở form thêm dịch vụ...");
        }
    }
}
