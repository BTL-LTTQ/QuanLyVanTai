using QuanLyVanTai.BLL.Exceptions;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// Lớp cơ sở cho các dịch vụ xuất dữ liệu (Excel, JSON…).
    /// Gom các bước kiểm tra dùng chung để tránh lặp code giữa các service.
    /// </summary>
    public abstract class BaseExportService
    {
        /// <summary>Chặn xuất file khi không có dữ liệu; message giữ nguyên theo từng định dạng.</summary>
        protected static void EnsureHasData(int rowCount, string subject, string message)
        {
            if (rowCount == 0)
                throw new BusinessRuleViolationException(subject, message);
        }

        /// <summary>Tạo thư mục đích nếu người dùng chọn đường dẫn chưa tồn tại.</summary>
        protected static void EnsureOutputDirectory(string filePath)
        {
            string? directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
    }
}
