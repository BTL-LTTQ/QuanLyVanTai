using Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuanLyVanTai.DAL;
using QuanLyVanTai.UI.Forms;

namespace QuanLyVanTai.UI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 1. Nạp tùy chọn ngôn ngữ đã lưu của người dùng (Part 3)
            var prefs = SessionManager.LoadPreferences();
            if (!string.IsNullOrEmpty(prefs.Language))
            {
                CultureHelper.SetCulture(prefs.Language, savePreference: false);
            }

            // 2. Đọc file appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Lấy Connection String
            var connectionString =
                configuration.GetConnectionString("DefaultConnection");

            // Cấu hình Entity Framework Core sử dụng SQL Server
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            // Tự động kiểm tra và áp dụng Migration sinh Database/bảng nếu chưa có
            try
            {
                using (var db = new AppDbContext(options))
                {
                    db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể khởi tạo hoặc kết nối CSDL SQL Server:\n{ex.Message}\n\nVui lòng kiểm tra lại chuỗi kết nối trong appsettings.json và đảm bảo SQL Server đang chạy.",
                    "Lỗi Cơ Sở Dữ Liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // 3. Vòng lặp xác thực người dùng (Login -> Main -> Logout -> Login)
            while (true)
            {
                using (var authForm = new FrmAuth())
                {
                    var authResult = authForm.ShowDialog();
                    if (authResult != DialogResult.OK)
                    {
                        // Người dùng đóng form đăng nhập mà không xác thực thành công -> Thoát ứng dụng
                        break;
                    }
                }

                // Nếu đăng nhập thành công -> Mở FormMain
                if (SessionManager.IsLoggedIn)
                {
                    Application.Run(new FormMain());

                    // Khi FormMain đóng:
                    // Nếu session đã bị xóa (do người dùng bấm Đăng xuất) -> quay lại mở FrmAuth
                    // Nếu session vẫn còn (người dùng bấm nút X tắt ứng dụng) -> thoát vòng lặp
                    if (SessionManager.IsLoggedIn)
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}