using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL.Repositories;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.Tests.Helpers;

namespace QuanLyVanTai.Tests.DAL
{
    public class RouteRepositoryTests
    {
        // ── GetAllWithStationsAndVehiclesAsync ────────────────────────────────

        [Fact]
        public async Task GetAllWithStations_ReturnsRouteWithStations()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new RouteRepository(db);

            var result = await repo.GetAllWithStationsAndVehiclesAsync();

            Assert.Single(result);
            Assert.Equal(2, result[0].Stations.Count);
            Assert.Single(result[0].Vehicles); // chỉ xe gán tuyến
        }

        [Fact]
        public async Task GetAllWithStations_KeywordFilter_ReturnsMatch()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new RouteRepository(db);

            var result = await repo.GetAllWithStationsAndVehiclesAsync("HN-DN");

            Assert.Single(result);
            Assert.Equal("HN-DN-01", result[0].RouteCode);
        }

        [Fact]
        public async Task GetAllWithStations_KeywordNoMatch_ReturnsEmpty()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new RouteRepository(db);

            var result = await repo.GetAllWithStationsAndVehiclesAsync("KHONGCO");

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllWithStations_KeywordMatchesStationName_ReturnsRoute()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new RouteRepository(db);

            // Tìm bằng tên trạm — test Include join logic
            var result = await repo.GetAllWithStationsAndVehiclesAsync("Mỹ Đình");

            Assert.Single(result);
        }

        // ── GetByIdWithDetailsAsync ───────────────────────────────────────────

        [Fact]
        public async Task GetByIdWithDetails_ExistingId_ReturnsRouteWithDetails()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new RouteRepository(db);

            var route = await repo.GetByIdWithDetailsAsync(1);

            Assert.NotNull(route);
            Assert.Equal("HN-DN-01", route.RouteCode);
            Assert.Equal(2, route.Stations.Count);
        }

        [Fact]
        public async Task GetByIdWithDetails_NonExistentId_ReturnsNull()
        {
            using var db = TestDbContextFactory.Create();
            var repo = new RouteRepository(db);

            var route = await repo.GetByIdWithDetailsAsync(999);

            Assert.Null(route);
        }

        // ── IsRouteCodeExistsAsync ────────────────────────────────────────────

        [Fact]
        public async Task IsRouteCodeExists_ExistingCode_ReturnsTrue()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new RouteRepository(db);

            var exists = await repo.IsRouteCodeExistsAsync("HN-DN-01");

            Assert.True(exists);
        }

        [Fact]
        public async Task IsRouteCodeExists_ExistingCodeExcludeSameId_ReturnsFalse()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new RouteRepository(db);

            // Khi update, exclude chính nó → không được báo trùng
            var exists = await repo.IsRouteCodeExistsAsync("HN-DN-01", excludeId: 1);

            Assert.False(exists);
        }

        [Fact]
        public async Task IsRouteCodeExists_NonExistentCode_ReturnsFalse()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new RouteRepository(db);

            var exists = await repo.IsRouteCodeExistsAsync("SG-CT-99");

            Assert.False(exists);
        }

        // ── DeleteWithTransactionAsync (soft-delete) ──────────────────────────

        [Fact]
        public async Task SoftDelete_ExistingRoute_SetsIsDeletedTrue()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new RouteRepository(db);

            await repo.DeleteWithTransactionAsync(1, softDelete: true);

            // Dùng IgnoreQueryFilters để thấy bản ghi đã soft-deleted
            var deleted = await db.Routes.IgnoreQueryFilters()
                .FirstOrDefaultAsync(r => r.Id == 1);
            Assert.NotNull(deleted);
            Assert.True(deleted.IsDeleted);
        }

        [Fact]
        public async Task SoftDelete_ExistingRoute_NullsVehicleRouteId()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new RouteRepository(db);

            await repo.DeleteWithTransactionAsync(1, softDelete: true);

            var vehicle = await db.Vehicles.FindAsync(1);
            Assert.Null(vehicle!.RouteId);
        }

        [Fact]
        public async Task SoftDelete_NonExistentRoute_ThrowsKeyNotFoundException()
        {
            using var db = TestDbContextFactory.Create();
            var repo = new RouteRepository(db);

            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => repo.DeleteWithTransactionAsync(999, softDelete: true));
        }

        // ── DeleteWithTransactionAsync (hard-delete) ──────────────────────────

        [Fact(Skip = "ExecuteSqlRawAsync là Relational-only API — hard-delete chỉ test được với SQL Server thật, không chạy trên InMemory provider")]
        public async Task HardDelete_RouteWithNoTickets_RemovesFromDb()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new RouteRepository(db);

            await repo.DeleteWithTransactionAsync(1, softDelete: false);

            var deleted = await db.Routes.IgnoreQueryFilters()
                .FirstOrDefaultAsync(r => r.Id == 1);
            Assert.Null(deleted);
        }
    }
}
