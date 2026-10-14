using Core.Helpers;
using Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuanLyVanTai.DAL;
using QuanLyVanTai.UI.Forms;
using QuanLyVanTai.UI.Utils;

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
            // 1. Đọc appsettings.json để lấy đường dẫn thư mục Log
            var bootstrapConfig = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .Build();

            string logDir = bootstrapConfig["Logging:LogDirectory"] ?? "Logs";
            if (!Path.IsPathRooted(logDir))
                logDir = Path.Combine(AppContext.BaseDirectory, logDir);

            // 2. Khởi tạo Serilog ghi log hệ thống tự động vào file và console
            LoggerService.Initialize(logDir);

            try
            {
                // 2. Thiết lập Windows Forms và Cơ chế bắt lỗi toàn cục (Global Exception Handler)
                ApplicationConfiguration.Initialize();
                GlobalExceptionHandler.Initialize();

                LoggerService.Info("Đang khởi động ứng dụng Quản Lý Vận Tải...");

                // 3. Nạp tùy chọn ngôn ngữ đã lưu của người dùng (Part 3)
                var prefs = SessionManager.LoadPreferences();
                if (!string.IsNullOrEmpty(prefs.Language))
                {
                    CultureHelper.SetCulture(prefs.Language, savePreference: false);
                }

                // 4. Đọc file appsettings.json
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                // Lấy Connection String
                var connectionString = configuration.GetConnectionString("DefaultConnection");

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
                        DatabaseSeeder.SeedSampleData(db);
                    }
                    LoggerService.Info("Kết nối và cập nhật cơ sở dữ liệu thành công.");
                }
                catch (Exception ex)
                {
                    LoggerService.Error(ex, "Lỗi khi khởi tạo hoặc kết nối CSDL SQL Server.");
                    MessageBox.Show(
                        $"Không thể khởi tạo hoặc kết nối CSDL SQL Server:\n{ex.Message}\n\nVui lòng kiểm tra lại chuỗi kết nối trong appsettings.json và đảm bảo SQL Server đang chạy.",
                        "Lỗi Cơ Sở Dữ Liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                // Tự động kiểm tra và áp dụng Migration sinh Database/bảng nếu chưa có
                try
                {
                    using (var db = new AppDbContext(options))
                    {
                        try
                        {
                            db.Database.Migrate();
                        }
                        catch
                        {
                            db.Database.EnsureCreated();
                        }

                        DatabaseSeeder.SeedSampleData(db);
                    }

                    LoggerService.Info("Kết nối và cập nhật cơ sở dữ liệu thành công.");
                }
                catch (Exception ex)
                {
                    LoggerService.Error(ex, "Lỗi khi khởi tạo hoặc kết nối CSDL SQL Server.");

                    MessageBox.Show(
                        $"Không thể khởi tạo hoặc kết nối CSDL SQL Server:\n{ex.Message}\n\nVui lòng kiểm tra lại chuỗi kết nối trong appsettings.json và đảm bảo SQL Server đang chạy.",
                        "Lỗi Cơ Sở Dữ Liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

// 5. Vòng lặp xác thực người dùng (Login -> Main -> Logout -> Login)
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

                    // Nếu đăng nhập thành công -> Điều hướng theo vai trò người dùng
                    if (SessionManager.IsLoggedIn)
                    {
                        LoggerService.Info("Người dùng [{Username}] ({Role}) đã đăng nhập vào hệ thống.", 
                            SessionManager.CurrentUser?.Username ?? "Guest", 
                            SessionManager.CurrentUser?.Role ?? "Unknown");

                        string role = SessionManager.CurrentUser?.Role ?? string.Empty;

                        // Vai trò 1 (Nhân viên bán vé / Khách hàng): Mở FormMain_KhachHang
                        if (role.Contains("bán vé", StringComparison.OrdinalIgnoreCase) ||
                            role.Contains("khách hàng", StringComparison.OrdinalIgnoreCase))
                        {
                            Application.Run(new FormMain_KhachHang());
                        }
                        else
                        {
                            // Vai trò 2 (Quản trị viên / Quản lý): Mở FormMain
                            Application.Run(new FormMain());
                        }

                        // Khi Form đóng:
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
                    if (SessionManager.IsLoggedIn)
                    {
                        LoggerService.Info("Người dùng [{Username}] ({Role}) đã đăng nhập vào hệ thống.", 
                            SessionManager.CurrentUser?.Username ?? "Guest", 
                            SessionManager.CurrentUser?.Role ?? "Unknown");

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
            catch (Exception ex)
            {
                LoggerService.Fatal(ex, "Ngoại lệ nghiêm trọng chưa được xử lý tại điểm vào Main()");
                MessageBox.Show(
                    $"Ứng dụng gặp sự cố nghiêm trọng:\n{ex.Message}\nChi tiết đã được ghi vào file log.",
                    "Lỗi Nghiêm Trọng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // Đảm bảo đẩy toàn bộ log xuống đĩa trước khi tiến trình tắt hoàn toàn
                LoggerService.CloseAndFlush();
            }
        }
    }
}