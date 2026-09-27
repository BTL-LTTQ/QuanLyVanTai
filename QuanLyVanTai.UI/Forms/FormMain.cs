using Core.Configs;

namespace QuanLyVanTai.UI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.Font = ThemeConfig.MainFont;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            // Panel styling
            pnlSidebar.BackColor = ThemeConfig.TextMain;
            pnlHeader.BackColor = ThemeConfig.PrimaryColor; // Use PrimaryColor for Header
            lblTitle.Font = ThemeConfig.TitleFont;
            pnlContent.BackColor = ThemeConfig.BackgroundColor;
            
            // Sidebar Button Styling Helper
            void StyleSidebarButton(FontAwesome.Sharp.IconButton btn, FontAwesome.Sharp.IconChar icon)
            {
                btn.BackColor = ThemeConfig.TextMain;
                btn.ForeColor = System.Drawing.Color.White;
                btn.IconChar = icon;
                btn.IconColor = System.Drawing.Color.White;
                btn.IconSize = 32;
                btn.Font = ThemeConfig.MainFont;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Cursor = Cursors.Hand;
                btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                btn.ImageAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(10, 0, 0, 0);
                btn.Height = 50;
            }

            StyleSidebarButton(btnTrangChu, FontAwesome.Sharp.IconChar.Home);
            StyleSidebarButton(btnDichVu, FontAwesome.Sharp.IconChar.Truck);
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
            lblTitle.Text = "TRANG CHỦ";
        }
        
        private void btnDichVu_Click(object sender, EventArgs e)
        {
            UcManageService ucService = new UcManageService();
            ShowUserControl(ucService);
            lblTitle.Text = "QUẢN LÝ DỊCH VỤ";
        }
    }
}
