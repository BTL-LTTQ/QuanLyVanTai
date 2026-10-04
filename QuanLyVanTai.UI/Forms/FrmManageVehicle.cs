using QuanLyVanTai.UI.UserControls;

namespace QuanLyVanTai.UI.Forms
{
    public partial class FrmManageVehicle : BaseManagementForm
    {
        public FrmManageVehicle()
        {
            InitializeComponent();
        }

        protected override void OnSave()
        {
            base.OnSave(); // Giữ lại logic ẩn/hiện nút của form cha (nếu có)

            // Viết code thu thập dữ liệu trên giao diện và gọi BLL để lưu xuống CSDL
            MessageBox.Show("Đã lưu thông tin Xe thành công!");
        }
    }
}
