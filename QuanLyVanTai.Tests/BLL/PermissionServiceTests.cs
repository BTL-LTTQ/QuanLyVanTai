using Core.Security;
using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.Tests.Helpers;

namespace QuanLyVanTai.Tests.BLL
{
    public class PermissionServiceTests
    {
        [Fact]
        public async Task GetPermissionsByRole_WhenNoneExist_SeedsDefaultsAndSavesToDb()
        {
            using var db = TestDbContextFactory.Create();
            var service = new PermissionService(db);

            var permissions = await service.GetPermissionsByRoleAsync("NhanVien");

            Assert.NotEmpty(permissions);
            Assert.Equal(SystemMenus.AllMenus.Length, permissions.Count);

            // Kiểm tra đã lưu vào DB
            var dbCount = await db.RolePermissions.CountAsync(p => p.RoleName == "NhanVien");
            Assert.Equal(SystemMenus.AllMenus.Length, dbCount);
        }

        [Fact]
        public async Task GetPermissionsByRole_WhenExist_ReturnsExistingWithoutDuplicating()
        {
            using var db = TestDbContextFactory.Create();
            db.RolePermissions.Add(new RolePermission
            {
                RoleName = "QuanLy",
                MenuCode = SystemMenus.Vehicle,
                MenuName = "Quản lý xe",
                CanView = true,
                CanAdd = true,
                CanEdit = false,
                CanDelete = false,
                CanExport = true
            });
            await db.SaveChangesAsync();

            var service = new PermissionService(db);
            var perms = await service.GetPermissionsByRoleAsync("QuanLy");

            Assert.Single(perms);
            Assert.Equal(SystemMenus.Vehicle, perms[0].MenuCode);
            Assert.True(perms[0].CanView);
            Assert.False(perms[0].CanDelete);
        }

        [Fact]
        public async Task SavePermissionsAsync_UpdatesExistingAndInsertsNew()
        {
            using var db = TestDbContextFactory.Create();
            db.RolePermissions.Add(new RolePermission
            {
                RoleName = "NhanVien",
                MenuCode = SystemMenus.Ticket,
                MenuName = "Bán vé",
                CanView = true,
                CanAdd = false,
                CanEdit = false,
                CanDelete = false,
                CanExport = false
            });
            await db.SaveChangesAsync();

            var service = new PermissionService(db);
            var toSave = new List<RolePermission>
            {
                // Cập nhật Ticket: bật CanAdd, CanExport
                new RolePermission
                {
                    RoleName = "NhanVien",
                    MenuCode = SystemMenus.Ticket,
                    MenuName = "Bán vé",
                    CanView = true,
                    CanAdd = true,
                    CanEdit = false,
                    CanDelete = false,
                    CanExport = true
                },
                // Thêm mới Route
                new RolePermission
                {
                    RoleName = "NhanVien",
                    MenuCode = SystemMenus.Route,
                    MenuName = "Tuyến xe",
                    CanView = true,
                    CanAdd = false,
                    CanEdit = false,
                    CanDelete = false,
                    CanExport = false
                }
            };

            var (success, message) = await service.SavePermissionsAsync("NhanVien", toSave);

            Assert.True(success);
            Assert.Contains("thành công", message);

            var ticketPerm = await db.RolePermissions.FirstOrDefaultAsync(p => p.RoleName == "NhanVien" && p.MenuCode == SystemMenus.Ticket);
            Assert.NotNull(ticketPerm);
            Assert.True(ticketPerm.CanAdd);
            Assert.True(ticketPerm.CanExport);

            var routePerm = await db.RolePermissions.FirstOrDefaultAsync(p => p.RoleName == "NhanVien" && p.MenuCode == SystemMenus.Route);
            Assert.NotNull(routePerm);
            Assert.True(routePerm.CanView);
        }

        [Fact]
        public async Task LoadPermissionsToUserSessionAsync_ForAdmin_GrantsFullPermissions()
        {
            using var db = TestDbContextFactory.Create();
            var service = new PermissionService(db);

            UserSession.Login(1, "admin", "Admin", SystemRoles.Admin);
            await service.LoadPermissionsToUserSessionAsync(SystemRoles.Admin);

            Assert.Equal(SystemRoles.Admin, UserSession.CurrentRole);
            Assert.True(UserSession.HasPermission(SystemMenus.Route, PermissionAction.Delete));
            Assert.True(UserSession.HasPermission(SystemMenus.Permission, PermissionAction.Edit));
        }

        [Fact]
        public async Task LoadPermissionsToUserSessionAsync_ForCustomRole_LoadsFromDb()
        {
            using var db = TestDbContextFactory.Create();
            db.RolePermissions.Add(new RolePermission
            {
                RoleName = "ThuNgan",
                MenuCode = SystemMenus.Ticket,
                MenuName = "Vé xe",
                CanView = true,
                CanAdd = true,
                CanEdit = true,
                CanDelete = false,
                CanExport = false
            });
            await db.SaveChangesAsync();

            var service = new PermissionService(db);
            UserSession.Login(10, "thungan01", "Thu Ngân Test", "ThuNgan");

            await service.LoadPermissionsToUserSessionAsync("ThuNgan");

            Assert.True(UserSession.HasPermission(SystemMenus.Ticket, PermissionAction.View));
            Assert.True(UserSession.HasPermission(SystemMenus.Ticket, PermissionAction.Add));
            Assert.False(UserSession.HasPermission(SystemMenus.Ticket, PermissionAction.Delete));
            Assert.False(UserSession.HasPermission(SystemMenus.Staff, PermissionAction.View));
        }
    }
}
