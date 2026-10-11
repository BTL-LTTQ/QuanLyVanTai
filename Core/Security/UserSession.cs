using System.Net;
using System.Net.Sockets;

namespace Core.Security
{
    public class UserPermissionItem
    {
        public string MenuCode { get; set; } = string.Empty;
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanExport { get; set; } = true;
    }

    public static class UserSession
    {
        public static int CurrentUserId { get; private set; } = 1;
        public static string CurrentUsername { get; private set; } = "admin";
        public static string CurrentFullName { get; private set; } = "Quản trị hệ thống";
        public static string CurrentRole { get; private set; } = SystemRoles.Admin;
        public static string CurrentIpAddress { get; private set; } = GetLocalIpAddress();

        private static readonly Dictionary<string, UserPermissionItem> _permissions = new(StringComparer.OrdinalIgnoreCase);

        public static event Action? SessionChanged;

        static UserSession()
        {
            // Default initialization for Admin: all permissions
            SetAdminPermissions();
        }

        public static void SetAdminPermissions()
        {
            _permissions.Clear();
            foreach (var menu in SystemMenus.AllMenus)
            {
                _permissions[menu.Code] = new UserPermissionItem
                {
                    MenuCode = menu.Code,
                    CanView = true,
                    CanAdd = true,
                    CanEdit = true,
                    CanDelete = true,
                    CanExport = true
                };
            }
        }

        public static void SetPermissions(IEnumerable<UserPermissionItem> permissions)
        {
            _permissions.Clear();
            foreach (var p in permissions)
            {
                _permissions[p.MenuCode] = p;
            }
        }

        public static void Login(int userId, string username, string fullName, string role, IEnumerable<UserPermissionItem>? permissions = null)
        {
            CurrentUserId = userId;
            CurrentUsername = username;
            CurrentFullName = fullName;
            CurrentRole = role;

            if (permissions != null)
            {
                SetPermissions(permissions);
            }
            else if (role == SystemRoles.Admin)
            {
                SetAdminPermissions();
            }
            else
            {
                _permissions.Clear();
            }

            SessionChanged?.Invoke();
        }

        public static bool HasPermission(string menuCode, PermissionAction action)
        {
            // Admin role has full access
            if (CurrentRole == SystemRoles.Admin)
                return true;

            if (!_permissions.TryGetValue(menuCode, out var item))
                return false;

            return action switch
            {
                PermissionAction.View => item.CanView,
                PermissionAction.Add => item.CanAdd,
                PermissionAction.Edit => item.CanEdit,
                PermissionAction.Delete => item.CanDelete,
                PermissionAction.Export => item.CanExport,
                _ => false
            };
        }

        public static UserPermissionItem? GetPermission(string menuCode)
        {
            if (CurrentRole == SystemRoles.Admin)
            {
                return new UserPermissionItem
                {
                    MenuCode = menuCode,
                    CanView = true,
                    CanAdd = true,
                    CanEdit = true,
                    CanDelete = true,
                    CanExport = true
                };
            }

            _permissions.TryGetValue(menuCode, out var item);
            return item;
        }

        private static string GetLocalIpAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
            }
            catch
            {
                // Fallback
            }
            return "127.0.0.1";
        }
    }
}
