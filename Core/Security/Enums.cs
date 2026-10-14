namespace Core.Security
{
    public enum PermissionAction
    {
        View,
        Add,
        Edit,
        Delete,
        Export,
        Approve
    }

    public static class SystemRoles
    {
        public const string Admin = "Quản trị viên";
        public const string Manager = "Quản lý";
        public const string TicketStaff = "Nhân viên bán vé";

        public static readonly string[] AllRoles = [Admin, Manager, TicketStaff];
    }

    public static class SystemMenus
    {
        public const string Route = "TuyenXe";
        public const string Station = "TramDung";
        public const string Vehicle = "PhuongTien";
        public const string Staff = "NhanSu";
        public const string Service = "DichVu";
        public const string Ticket = "GiaVe";
        public const string Promotion = "KhuyenMai";
        public const string Permission = "PhanQuyen";
        public const string AuditLog = "NhatKyHoatDong";

        public static readonly (string Code, string Name)[] AllMenus =
        [
            (Route, "Quản lý Tuyến xe"),
            (Station, "Quản lý Trạm dừng"),
            (Vehicle, "Quản lý Phương tiện"),
            (Staff, "Quản lý Nhân sự"),
            (Service, "Quản lý Dịch vụ"),
            (Ticket, "Quản lý Giá vé"),
            (Promotion, "Quản lý Khuyến mãi"),
            (Permission, "Quản lý Phân quyền"),
            (AuditLog, "Nhật ký hoạt động")
        ];
    }
}
