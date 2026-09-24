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

            // Kiểm tra kết nối SQL Server
            using (var db = new AppDbContext(options))
            {
                if (db.Database.CanConnect())
                {
                    MessageBox.Show("Kết nối SQL Server thành công!");
                }
                else
                {
                    MessageBox.Show("Không kết nối được SQL Server!");
                }
            }

            // Mở Form chính
            Application.Run(new Form1());
        }
    }
}