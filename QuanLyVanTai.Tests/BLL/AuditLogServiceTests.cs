using Core.Security;
using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.Tests.Helpers;

namespace QuanLyVanTai.Tests.BLL
{
    public class AuditLogServiceTests
    {
        [Fact]
        public async Task LogActionAsync_CreatesAuditLogWithSessionDetails()
        {
            using var db = TestDbContextFactory.Create();
            var service = new AuditLogService(db);

            UserSession.Login(42, "admin_user", "Quản Trị Viên", SystemRoles.Admin);

            await service.LogActionAsync(
                action: "Thêm",
                entityName: "Tuyến xe",
                recordId: "101",
                details: "Thêm mới tuyến xe HN-DN",
                oldValues: null,
                newValues: "RouteCode=HN-DN-02"
            );

            var logs = await db.AuditLogs.ToListAsync();
            Assert.Single(logs);

            var log = logs[0];
            Assert.Equal(42, log.UserId);
            Assert.Equal("admin_user", log.Username);
            Assert.Equal(SystemRoles.Admin, log.Role);
            Assert.Equal("Thêm", log.Action);
            Assert.Equal("Tuyến xe", log.EntityName);
            Assert.Equal("101", log.RecordId);
            Assert.Equal("Thêm mới tuyến xe HN-DN", log.Details);
            Assert.Equal("RouteCode=HN-DN-02", log.NewValues);
        }

        [Fact]
        public async Task SearchLogsAsync_FiltersByCriteriaCorrectly()
        {
            using var db = TestDbContextFactory.Create();
            var now = DateTime.UtcNow;

            db.AuditLogs.AddRange(
                new AuditLog
                {
                    Username = "nhanvien01",
                    Action = "Đăng nhập",
                    EntityName = "Hệ thống",
                    Timestamp = now.AddDays(-2),
                    Details = "Đăng nhập thành công"
                },
                new AuditLog
                {
                    Username = "admin",
                    Action = "Xóa",
                    EntityName = "Phương tiện",
                    RecordId = "5",
                    Timestamp = now.AddHours(-3),
                    Details = "Xóa xe 29B-12345"
                },
                new AuditLog
                {
                    Username = "admin",
                    Action = "Sửa",
                    EntityName = "Tuyến xe",
                    RecordId = "10",
                    Timestamp = now.AddMinutes(-10),
                    Details = "Cập nhật giá cước"
                }
            );
            await db.SaveChangesAsync();

            var service = new AuditLogService(db);

            // 1. Lọc theo Username
            var userLogs = await service.SearchLogsAsync(new AuditLogFilterCriteria { Username = "admin" });
            Assert.Equal(2, userLogs.Count);

            // 2. Lọc theo Action
            var deleteLogs = await service.SearchLogsAsync(new AuditLogFilterCriteria { Action = "Xóa" });
            Assert.Single(deleteLogs);
            Assert.Equal("Xóa", deleteLogs[0].Action);

            // 3. Lọc theo EntityName
            var routeLogs = await service.SearchLogsAsync(new AuditLogFilterCriteria { EntityName = "Tuyến xe" });
            Assert.Single(routeLogs);
            Assert.Equal("10", routeLogs[0].RecordId);

            // 4. Lọc theo Keyword
            var kwLogs = await service.SearchLogsAsync(new AuditLogFilterCriteria { Keyword = "29B-12345" });
            Assert.Single(kwLogs);
            Assert.Equal("Phương tiện", kwLogs[0].EntityName);

            // 5. Bỏ qua giá trị "Tất cả"
            var allLogs = await service.SearchLogsAsync(new AuditLogFilterCriteria
            {
                Username = "Tất cả",
                Action = "Tất cả",
                EntityName = "Tất cả"
            });
            Assert.Equal(3, allLogs.Count);
        }
    }
}
