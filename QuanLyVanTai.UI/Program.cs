using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuanLyVanTai.DAL;

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

            // Đọc file appsettings.json
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

            // Mở Form chính
            Application.Run(new FormMain());
        }
    }
}