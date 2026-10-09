namespace QuanLyVanTai.BLL.Exceptions
{
    // =========================================================================
    // Custom Exception Hierarchy cho hệ thống Quản lý Bến xe
    // =========================================================================
    // Mục đích: cung cấp exception có ngữ nghĩa rõ ràng để tầng UI (Windows Forms)
    // có thể phân biệt loại lỗi và hiển thị thông báo phù hợp mà không cần
    // parse message string.
    //
    // Cách dùng ở UI:
    //   catch (EntityNotFoundException ex)      → hiển thị MessageBox "Không tìm thấy"
    //   catch (DuplicateEntityException ex)     → tô đỏ field mã trùng
    //   catch (BusinessRuleViolationException ex) → cảnh báo nghiệp vụ
    //   catch (BusStationException ex)          → fallback chung
    //   catch (Exception ex)                   → lỗi hệ thống không mong đợi
    // =========================================================================

    /// <summary>
    /// Base exception cho toàn bộ hệ thống Quản lý Bến xe.
    /// Tất cả custom exceptions kế thừa từ lớp này.
    /// </summary>
    public class BusStationException : Exception
    {
        /// <summary>Tên entity liên quan (Route / Station / Vehicle / ...).</summary>
        public string EntityName { get; }

        public BusStationException(string entityName, string message)
            : base(message)
        {
            EntityName = entityName;
        }

        public BusStationException(string entityName, string message, Exception innerException)
            : base(message, innerException)
        {
            EntityName = entityName;
        }
    }

    // -------------------------------------------------------------------------

    /// <summary>
    /// Ném ra khi không tìm thấy bản ghi theo ID hoặc mã nghiệp vụ.
    /// → UI nên hiển thị thông báo "Không tìm thấy ..." và refresh danh sách.
    /// </summary>
    public sealed class EntityNotFoundException : BusStationException
    {
        public int? EntityId { get; }

        public EntityNotFoundException(string entityName, int id)
            : base(entityName, $"Không tìm thấy {entityName} có ID = {id}.")
        {
            EntityId = id;
        }

        public EntityNotFoundException(string entityName, string message)
            : base(entityName, message)
        {
        }
    }

    // -------------------------------------------------------------------------

    /// <summary>
    /// Ném ra khi vi phạm UNIQUE constraint (mã tuyến, biển số, mã trạm trùng).
    /// → UI nên focus vào field bị trùng và hiển thị gợi ý.
    /// </summary>
    public sealed class DuplicateEntityException : BusStationException
    {
        /// <summary>Tên field bị trùng (ví dụ: "RouteCode", "LicensePlate").</summary>
        public string DuplicateField { get; }

        /// <summary>Giá trị bị trùng.</summary>
        public string DuplicateValue { get; }

        public DuplicateEntityException(string entityName, string fieldName, string value)
            : base(entityName, $"{entityName}: Giá trị '{value}' của trường [{fieldName}] đã tồn tại trong hệ thống.")
        {
            DuplicateField = fieldName;
            DuplicateValue = value;
        }
    }

    // -------------------------------------------------------------------------

    /// <summary>
    /// Ném ra khi vi phạm quy tắc nghiệp vụ (xóa tuyến có vé, xóa cứng khi còn FK, ...).
    /// → UI nên hiển thị cảnh báo và giải thích lý do từ Message.
    /// </summary>
    public sealed class BusinessRuleViolationException : BusStationException
    {
        public BusinessRuleViolationException(string entityName, string message)
            : base(entityName, message)
        {
        }
    }

    // -------------------------------------------------------------------------

    /// <summary>
    /// Ném ra khi dữ liệu đầu vào không hợp lệ (null, rỗng, giá trị âm, ...).
    /// → UI nên highlight field và hiển thị lỗi validation.
    /// </summary>
    public sealed class ValidationException : BusStationException
    {
        /// <summary>Tên field không hợp lệ.</summary>
        public string FieldName { get; }

        public ValidationException(string entityName, string fieldName, string message)
            : base(entityName, message)
        {
            FieldName = fieldName;
        }
    }

    // -------------------------------------------------------------------------

    /// <summary>
    /// Ném ra khi thao tác database thất bại (lỗi transaction, kết nối, ...).
    /// Luôn bao gồm InnerException gốc để log chi tiết.
    /// → UI nên hiển thị thông báo lỗi kỹ thuật và đề nghị thử lại.
    /// </summary>
    public sealed class DataAccessException : BusStationException
    {
        public DataAccessException(string entityName, string operation, Exception innerException)
            : base(entityName,
                  $"Lỗi truy cập dữ liệu khi thực hiện [{operation}] trên [{entityName}]: {innerException.Message}",
                  innerException)
        {
        }
    }
}
