using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.Exceptions;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.Tests.Helpers;

namespace QuanLyVanTai.Tests.BLL
{
    /// <summary>
    /// Test các truy vấn phục vụ giai đoạn 1 của màn hình Tuyến xe:
    /// số liệu KPI (GetDashboardStatsAsync) và lịch sử thao tác (GetRouteAuditHistoryAsync).
    /// </summary>
    public class RouteDashboardAuditTests
    {
        private static Route NewRoute(int id, string code, decimal distance, decimal price, string status) => new()
        {
            Id = id,
            RouteCode = code,
            RouteName = $"Tuyến {code}",
            DistanceKm = distance,
            EstimatedHours = 2,
            BasePrice = price,
            Status = status
        };

        // ── GetDashboardStatsAsync ────────────────────────────────────────────

        [Fact]
        public async Task GetDashboardStatsAsync_AggregatesRoutes()
        {
            using var db = TestDbContextFactory.Create();
            db.Routes.AddRange(
                NewRoute(1, "A-01", 100m, 200000m, "Active"),
                NewRoute(2, "B-02", 250m, 400000m, "Suspended"),
                NewRoute(3, "C-03", 150m, 300000m, "Active"));
            await db.SaveChangesAsync();

            var service = new RouteManagementService(db);
            var stats = await service.GetDashboardStatsAsync();

            Assert.Equal(3, stats.TotalRoutes);
            Assert.Equal(2, stats.ActiveRoutes);
            Assert.Equal(500m, stats.TotalDistanceKm);
            Assert.Equal(300000m, stats.AverageBasePrice);
        }

        [Fact]
        public async Task GetDashboardStatsAsync_EmptyDatabase_ReturnsZeros()
        {
            using var db = TestDbContextFactory.Create();

            var service = new RouteManagementService(db);
            var stats = await service.GetDashboardStatsAsync();

            Assert.Equal(0, stats.TotalRoutes);
            Assert.Equal(0, stats.ActiveRoutes);
            Assert.Equal(0m, stats.TotalDistanceKm);
            Assert.Equal(0m, stats.AverageBasePrice);
        }

        [Fact]
        public async Task GetDashboardStatsAsync_ExcludesSoftDeletedRoutes()
        {
            using var db = TestDbContextFactory.Create();
            var deleted = NewRoute(2, "B-02", 999m, 999000m, "Active");
            deleted.IsDeleted = true;

            db.Routes.AddRange(NewRoute(1, "A-01", 100m, 200000m, "Active"), deleted);
            await db.SaveChangesAsync();

            var service = new RouteManagementService(db);
            var stats = await service.GetDashboardStatsAsync();

            Assert.Equal(1, stats.TotalRoutes);
            Assert.Equal(100m, stats.TotalDistanceKm);
        }

        // ── GetRouteAuditHistoryAsync ─────────────────────────────────────────

        [Fact]
        public async Task GetRouteAuditHistoryAsync_ReturnsOnlyLogsOfSelectedRoute()
        {
            using var db = TestDbContextFactory.Create();
            db.Routes.AddRange(
                NewRoute(1, "HN-DN-01", 764m, 350000m, "Active"),
                NewRoute(2, "HN-HP-02", 120m, 150000m, "Active"));
            await db.SaveChangesAsync();

            // Xóa log do interceptor tự sinh khi seed để kiểm soát dữ liệu test
            db.AuditLogs.RemoveRange(await db.AuditLogs.ToListAsync());
            await db.SaveChangesAsync();

            var now = DateTime.UtcNow;
            db.AuditLogs.AddRange(
                // Log "Thêm" có RecordId tạm (khóa chính chưa được EF gán) → khớp qua RouteCode
                new AuditLog
                {
                    Username = "admin", Role = "Quản Trị Viên", Action = "Thêm",
                    EntityName = "Route", RecordId = "-2147482647",
                    NewValues = "{\"RouteCode\":\"HN-DN-01\",\"RouteName\":\"Tuyến HN-DN-01\"}",
                    Timestamp = now.AddHours(-6), Details = "Thêm tuyến"
                },
                new AuditLog
                {
                    Username = "quanly", Role = "Quản Lý", Action = "Sửa",
                    EntityName = "Route", RecordId = "1",
                    OldValues = "{\"BasePrice\":300000}",
                    NewValues = "{\"BasePrice\":350000}",
                    Timestamp = now.AddHours(-2), Details = "Sửa giá vé"
                },
                // Log của tuyến khác — phải bị loại
                new AuditLog
                {
                    Username = "quanly", Role = "Quản Lý", Action = "Sửa",
                    EntityName = "Route", RecordId = "2",
                    NewValues = "{\"BasePrice\":160000}",
                    Timestamp = now.AddHours(-1), Details = "Sửa tuyến khác"
                },
                // Log của phân hệ khác — phải bị loại
                new AuditLog
                {
                    Username = "admin", Role = "Quản Trị Viên", Action = "Sửa",
                    EntityName = "Vehicle", RecordId = "1",
                    NewValues = "{\"Status\":\"Ready\"}",
                    Timestamp = now, Details = "Sửa phương tiện"
                });
            await db.SaveChangesAsync();

            var service = new RouteManagementService(db);
            var history = await service.GetRouteAuditHistoryAsync(1);

            Assert.Equal(2, history.Count);
            Assert.Equal("Sửa", history[0].Action);        // mới nhất lên đầu
            Assert.Equal("quanly", history[0].Username);
            Assert.Contains("BasePrice", history[0].OldValues);
            Assert.Equal("Thêm", history[1].Action);
            Assert.Equal("admin", history[1].Username);
        }

        [Fact]
        public async Task GetRouteAuditHistoryAsync_UnknownRoute_ThrowsEntityNotFoundException()
        {
            using var db = TestDbContextFactory.Create();
            var service = new RouteManagementService(db);

            var ex = await Assert.ThrowsAsync<EntityNotFoundException>(
                () => service.GetRouteAuditHistoryAsync(999));

            Assert.Equal("Route", ex.EntityName);
            Assert.Equal(999, ex.EntityId);
        }
    }
}
