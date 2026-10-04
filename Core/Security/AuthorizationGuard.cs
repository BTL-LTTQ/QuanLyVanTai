namespace Core.Security
{
    public class UnauthorizedFormAccessException : Exception
    {
        public string MenuCode { get; }
        public PermissionAction Action { get; }

        public UnauthorizedFormAccessException(string menuCode, PermissionAction action, string message)
            : base(message)
        {
            MenuCode = menuCode;
            Action = action;
        }
    }

    public static class AuthorizationGuard
    {
        public static event Action<string, PermissionAction, string>? UnauthorizedAttemptDetected;

        /// <summary>
        /// Chặn hoàn toàn việc mở Form/UserControl hoặc thực thi action trái phép qua code.
        /// Không chỉ kiểm tra giao diện, nếu không có quyền sẽ ghi nhận hành vi và ném Exception hoặc cảnh báo.
        /// </summary>
        public static bool CheckAccess(string menuCode, PermissionAction action = PermissionAction.View, bool showWarning = true)
        {
            if (UserSession.HasPermission(menuCode, action))
            {
                return true;
            }

            string actionText = action switch
            {
                PermissionAction.View => "truy cập",
                PermissionAction.Add => "thêm mới",
                PermissionAction.Edit => "chỉnh sửa",
                PermissionAction.Delete => "xóa",
                PermissionAction.Export => "xuất dữ liệu",
                PermissionAction.Approve => "phê duyệt",
                _ => "thao tác"
            };

            string message = $"Tài khoản '{UserSession.CurrentUsername}' (Vai trò: {UserSession.CurrentRole}) không có quyền {actionText} chức năng '{menuCode}'.";

            // Trigger audit log for security breach attempt
            UnauthorizedAttemptDetected?.Invoke(menuCode, action, message);

            if (showWarning)
            {
                MessageBox.Show(
                    $"BẢO MẬT HỆ THỐNG:\n\n{message}\n\nHành động này đã được ghi lại trong Nhật ký an ninh (Audit Trail).",
                    "Truy Cập Bị Từ Chối (403 Forbidden)",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return false;
        }

        /// <summary>
        /// Ném lỗi chặn luồng thực thi hoàn toàn nếu phát hiện mở Form hoặc gọi hàm trái phép qua code
        /// </summary>
        public static void EnsureAccess(string menuCode, PermissionAction action = PermissionAction.View)
        {
            if (!CheckAccess(menuCode, action, showWarning: true))
            {
                throw new UnauthorizedFormAccessException(menuCode, action,
                    $"Cảnh báo an ninh: Chặn truy cập trái phép vào [{menuCode}] - Hành động: [{action}]!");
            }
        }

        /// <summary>
        /// Khóa hoặc mở các nút thao tác UI dựa trên quyền cụ thể của người dùng
        /// </summary>
        public static void ApplyControlSecurity(
            string menuCode,
            Control? btnAdd = null,
            Control? btnEdit = null,
            Control? btnDelete = null,
            Control? btnExport = null)
        {
            if (btnAdd != null)
                btnAdd.Enabled = UserSession.HasPermission(menuCode, PermissionAction.Add);

            if (btnEdit != null)
                btnEdit.Enabled = UserSession.HasPermission(menuCode, PermissionAction.Edit);

            if (btnDelete != null)
                btnDelete.Enabled = UserSession.HasPermission(menuCode, PermissionAction.Delete);

            if (btnExport != null)
                btnExport.Enabled = UserSession.HasPermission(menuCode, PermissionAction.Export);
        }
    }
}
