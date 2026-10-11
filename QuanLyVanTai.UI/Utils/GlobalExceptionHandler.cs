using Core.Logging;
using QuanLyVanTai.UI.Forms;

namespace QuanLyVanTai.UI.Utils
{
    /// <summary>
    /// Bộ xử lý ngoại lệ toàn cục (Global Exception Handler) cho ứng dụng Windows Forms.
    /// Bắt toàn bộ lỗi từ UI Thread, Background Thread, Async Task; tự động ghi log vào Serilog
    /// và hiển thị thông báo thân thiện, đảm bảo ứng dụng không bao giờ bị văng (crash) khi demo.
    /// </summary>
    public static class GlobalExceptionHandler
    {
        private static bool _isInitialized = false;

        public static void Initialize()
        {
            if (_isInitialized) return;

            // Thiết lập bắt buộc Windows Forms chuyển tất cả ngoại lệ về ThreadException thay vì ném ra ngoài làm crash app
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // 1. Bắt ngoại lệ trên giao diện người dùng (UI Thread)
            Application.ThreadException += OnThreadException;

            // 2. Bắt ngoại lệ trên các tiến trình nền / non-UI threads
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            // 3. Bắt ngoại lệ trong các tác vụ bất đồng bộ Task không được await
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;

            _isInitialized = true;
            LoggerService.Info("Cơ chế bắt lỗi toàn cục (Global Exception Handler) đã được kích hoạt thành công.");
        }

        /// <summary>
        /// Xử lý ngoại lệ phát sinh trên UI Thread (Application.ThreadException)
        /// </summary>
        private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception, "UI Thread Exception");
        }

        /// <summary>
        /// Xử lý ngoại lệ phát sinh trên Non-UI / Background Thread (AppDomain.UnhandledException)
        /// </summary>
        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                HandleException(ex, $"Non-UI Thread Exception (IsTerminating: {e.IsTerminating})");
            }
            else
            {
                string msg = e.ExceptionObject?.ToString() ?? "Không rõ đối tượng ngoại lệ.";
                LoggerService.Fatal(null, "Phát hiện đối tượng ngoại lệ không xác định: {Message}", msg);
            }
        }

        /// <summary>
        /// Xử lý ngoại lệ chưa quan sát trong các Task bất đồng bộ (TaskScheduler.UnobservedTaskException)
        /// </summary>
        private static void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved(); // Đánh dấu đã xử lý để tránh crash tiến trình .NET
            HandleException(e.Exception, "Unobserved Task Exception");
        }

        /// <summary>
        /// Xử lý tập trung: Ghi log Serilog và hiển thị thông báo ẩn lỗi kỹ thuật
        /// </summary>
        public static void HandleException(Exception ex, string contextSource)
        {
            try
            {
                // Sinh mã lỗi độc nhất để tra cứu log: ERR-yyyyMMdd-XXXX
                string errorId = $"ERR-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";

                // Tự động ghi chi tiết lỗi vào Serilog (lưu file và console)
                LoggerService.Error(ex, "[{ErrorId}] Ngoại lệ được chặn tại [{ContextSource}]: {ErrorMessage}", 
                    errorId, contextSource, ex.Message);

                // Thông báo thân thiện cho người dùng, ẩn lỗi kỹ thuật
                string friendlyMessage = "Đã xảy ra sự cố không mong muốn trong quá trình thực hiện tác vụ.\n" +
                                          "Hệ thống đã tự động bảo vệ để ứng dụng tiếp tục hoạt động bình thường.";

                // Hiển thị hộp thoại giao diện thân thiện
                if (Application.OpenForms.Count > 0)
                {
                    var activeForm = Application.OpenForms[Application.OpenForms.Count - 1];
                    if (activeForm != null && activeForm.InvokeRequired)
                    {
                        activeForm.Invoke(new Action(() => ShowDialog(errorId, friendlyMessage, ex)));
                    }
                    else
                    {
                        ShowDialog(errorId, friendlyMessage, ex);
                    }
                }
                else
                {
                    ShowDialog(errorId, friendlyMessage, ex);
                }
            }
            catch (Exception fallbackEx)
            {
                // Fallback nếu có lỗi ngay trong quá trình xử lý exception
                try
                {
                    LoggerService.Fatal(fallbackEx, "Lỗi xảy ra trong quá trình thực thi GlobalExceptionHandler.");
                }
                catch
                {
                    // Tránh crash tuyệt đối
                }
            }
        }

        private static void ShowDialog(string errorId, string message, Exception ex)
        {
            try
            {
                using var dialog = new FrmErrorDialog(errorId, message, ex);
                dialog.ShowDialog();
            }
            catch
            {
                MessageBox.Show(
                    $"Đã xảy ra sự cố (Mã: {errorId}). Ứng dụng đã được bảo vệ không bị văng.",
                    "Thông Báo Hệ Thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Hàm kích hoạt lỗi giả lập để demo khả năng bắt lỗi toàn cục và ghi log
        /// </summary>
        public static void SimulateDemoException(string testType = "ui")
        {
            if (testType == "async")
            {
                Task.Run(() =>
                {
                    throw new InvalidOperationException("DEMO NGOẠI LỆ BẤT ĐỒNG BỘ: Giả lập lỗi truy vấn CSDL mạng trong Task.");
                });
            }
            else
            {
                throw new ApplicationException("DEMO NGOẠI LỆ GIAO DIỆN: Giả lập lỗi người dùng bấm thao tác bất ngờ khi demo đồ án.");
            }
        }
    }
}
