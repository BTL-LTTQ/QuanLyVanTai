using System.Text;
using Serilog;
using Serilog.Events;

namespace Core.Logging
{
    /// <summary>
    /// Dịch vụ quản lý Logging tập trung sử dụng Serilog.
    /// Tự động ghi log vào console và các file log theo ngày (app-yyyyMMdd.log, error-yyyyMMdd.log).
    /// </summary>
    public static class LoggerService
    {
        private static bool _isInitialized = false;
        private static readonly object _lock = new object();
        private static string _logDirectory = string.Empty;

        public static string LogDirectory => _logDirectory;

        /// <summary>
        /// Khởi tạo Serilog cho toàn bộ hệ thống
        /// </summary>
        public static void Initialize(string? customLogPath = null)
        {
            if (_isInitialized) return;

            lock (_lock)
            {
                if (_isInitialized) return;

                try
                {
                    // Xác định đường dẫn thư mục lưu log
                    if (!string.IsNullOrWhiteSpace(customLogPath))
                    {
                        _logDirectory = customLogPath;
                    }
                    else
                    {
                        _logDirectory = Path.Combine(AppContext.BaseDirectory, "Logs");
                    }

                    if (!Directory.Exists(_logDirectory))
                    {
                        Directory.CreateDirectory(_logDirectory);
                    }

                    string logTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

                    // File log tổng hợp (Info, Warning, Error)
                    string generalLogPath = Path.Combine(_logDirectory, "app-.log");

                    // File log riêng biệt chỉ chứa Error & Fatal để tiện tra cứu
                    string errorLogPath = Path.Combine(_logDirectory, "error-.log");

                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.Console(outputTemplate: logTemplate)
                        .WriteTo.File(
                            path: generalLogPath,
                            rollingInterval: RollingInterval.Day,
                            retainedFileCountLimit: 30,
                            encoding: Encoding.UTF8,
                            outputTemplate: logTemplate,
                            restrictedToMinimumLevel: LogEventLevel.Information)
                        .WriteTo.File(
                            path: errorLogPath,
                            rollingInterval: RollingInterval.Day,
                            retainedFileCountLimit: 60,
                            encoding: Encoding.UTF8,
                            outputTemplate: logTemplate,
                            restrictedToMinimumLevel: LogEventLevel.Error)
                        .CreateLogger();

                    _isInitialized = true;
                    Log.Information("=== HỆ THỐNG QUẢN LÝ VẬN TẢI KHỞI ĐỘNG (Serilog Initialized) ===");
                    Log.Information("Thư mục lưu log: {LogDirectory}", _logDirectory);
                }
                catch (Exception ex)
                {
                    // Fallback console nếu lỗi phân quyền thư mục
                    Console.WriteLine($"[CRITICAL] Không thể khởi tạo Serilog: {ex.Message}");
                }
            }
        }

        public static void Info(string message) => Log.Information(message);
        public static void Info(string messageTemplate, params object[] propertyValues) => Log.Information(messageTemplate, propertyValues);

        public static void Warning(string message) => Log.Warning(message);
        public static void Warning(string messageTemplate, params object[] propertyValues) => Log.Warning(messageTemplate, propertyValues);

        public static void Error(Exception? ex, string message) => Log.Error(ex, message);
        public static void Error(Exception? ex, string messageTemplate, params object[] propertyValues) => Log.Error(ex, messageTemplate, propertyValues);

        public static void Fatal(Exception? ex, string message) => Log.Fatal(ex, message);
        public static void Fatal(Exception? ex, string messageTemplate, params object[] propertyValues) => Log.Fatal(ex, messageTemplate, propertyValues);

        /// <summary>
        /// Đẩy toàn bộ log đệm xuống file và giải phóng tài nguyên khi đóng ứng dụng
        /// </summary>
        public static void CloseAndFlush()
        {
            if (!_isInitialized) return;
            try
            {
                Log.Information("=== HỆ THỐNG QUẢN LÝ VẬN TẢI ĐÓNG KẾT THÚC (Serilog Closed) ===");
                Log.CloseAndFlush();
            }
            catch
            {
                // Bỏ qua lỗi khi đóng ứng dụng
            }
        }
    }
}
