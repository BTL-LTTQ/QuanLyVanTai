using Core.Security;
using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.BLL.Services
{
    public class PermissionService
    {
        private readonly AppDbContext _context;

        public PermissionService(AppDbContext context)
        {
            _context = context;
        }

        public PermissionService() : this(new AppDbContext())
        {
        }

        /// <summary>
        /// Lấy danh sách quyền của một vai trò từ Database
        /// </summary>
        public async Task<List<RolePermission>> GetPermissionsByRoleAsync(string roleName)
        {
            var list = await _context.RolePermissions
                .Where(p => p.RoleName == roleName)
                .OrderBy(p => p.Id)
                .AsNoTracking()
                .ToListAsync();

            // Nếu DB chưa có bản ghi quyền cho vai trò này, khởi tạo mặc định theo SystemMenus
            if (list.Count == 0)
            {
                list = InitializeDefaultPermissionsForRole(roleName);
                _context.RolePermissions.AddRange(list);
                await _context.SaveChangesAsync();
            }

            return list;
        }

        /// <summary>
        /// Lưu cấu hình phân quyền từ Admin Panel xuống CSDL
        /// </summary>
        public async Task<(bool Success, string Message)> SavePermissionsAsync(string roleName, List<RolePermission> permissions)
        {
            try
            {
                var existing = await _context.RolePermissions
                    .Where(p => p.RoleName == roleName)
                    .ToListAsync();

                foreach (var perm in permissions)
                {
                    var found = existing.FirstOrDefault(e => e.MenuCode == perm.MenuCode);
                    if (found != null)
                    {
                        found.CanView = perm.CanView;
                        found.CanAdd = perm.CanAdd;
                        found.CanEdit = perm.CanEdit;
                        found.CanDelete = perm.CanDelete;
                        found.CanExport = perm.CanExport;
                        found.UpdatedAt = DateTime.UtcNow;
                    }
                    else
                    {
                        _context.RolePermissions.Add(new RolePermission
                        {
                            RoleName = roleName,
                            MenuCode = perm.MenuCode,
                            MenuName = perm.MenuName,
                            CanView = perm.CanView,
                            CanAdd = perm.CanAdd,
                            CanEdit = perm.CanEdit,
                            CanDelete = perm.CanDelete,
                            CanExport = perm.CanExport,
                            UpdatedAt = DateTime.UtcNow
                        });
                    }
                }

                await _context.SaveChangesAsync();

                // Nếu vai trò vừa cập nhật trùng với người dùng hiện tại -> cập nhật UserSession ngay lập tức!
                if (UserSession.CurrentRole == roleName)
                {
                    await LoadPermissionsToUserSessionAsync(roleName);
                }

                return (true, $"Đã lưu và áp dụng phân quyền động cho vai trò [{roleName}] thành công!");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi lưu phân quyền: {ex.Message}");
            }
        }

        /// <summary>
        /// Tải quyền từ CSDL nạp vào UserSession khi người dùng đăng nhập hoặc đổi vai trò
        /// </summary>
        public async Task LoadPermissionsToUserSessionAsync(string roleName)
        {
            if (roleName == SystemRoles.Admin)
            {
                UserSession.SetAdminPermissions();
                return;
            }

            var perms = await GetPermissionsByRoleAsync(roleName);
            var items = perms.Select(p => new UserPermissionItem
            {
                MenuCode = p.MenuCode,
                CanView = p.CanView,
                CanAdd = p.CanAdd,
                CanEdit = p.CanEdit,
                CanDelete = p.CanDelete,
                CanExport = p.CanExport
            });

            UserSession.SetPermissions(items);
        }

        private List<RolePermission> InitializeDefaultPermissionsForRole(string roleName)
        {
            var result = new List<RolePermission>();
            bool isAdmin = roleName == SystemRoles.Admin;
            bool isManager = roleName == SystemRoles.Manager;

            foreach (var menu in SystemMenus.AllMenus)
            {
                bool isTicketRelated = menu.Code == SystemMenus.Ticket || menu.Code == SystemMenus.Route || menu.Code == SystemMenus.Promotion;
                bool isSystemMenu = menu.Code == SystemMenus.Permission || menu.Code == SystemMenus.AuditLog;

                result.Add(new RolePermission
                {
                    RoleName = roleName,
                    MenuCode = menu.Code,
                    MenuName = menu.Name,
                    CanView = isAdmin || (isManager) || (isTicketRelated),
                    CanAdd = isAdmin || (isManager && !isSystemMenu) || (menu.Code == SystemMenus.Ticket),
                    CanEdit = isAdmin || (isManager && !isSystemMenu),
                    CanDelete = isAdmin,
                    CanExport = isAdmin || isManager,
                    UpdatedAt = DateTime.UtcNow
                });
            }

            return result;
        }
    }
}
