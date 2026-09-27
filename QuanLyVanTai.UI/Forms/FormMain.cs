namespace QuanLyVanTai.UI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }



        // Hàm xóa nội dung cũ và nạp UserControl mới vào vùng giữa
        private void ShowUserControl(UserControl uc)
        {
            // Xóa các control cũ đang hiển thị
            pnlContent.Controls.Clear();

            // Set Dock = Fill để UserControl chiếm toàn bộ vùng Content
            uc.Dock = DockStyle.Fill;

            // Thêm vào Panel và hiển thị
            pnlContent.Controls.Add(uc);
        }
        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            UcManageRoute ucRoute = new UcManageRoute();
            ShowUserControl(ucRoute);
        }
    }
}
