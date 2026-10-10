using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL.Repositories;
using QuanLyVanTai.Tests.Helpers;

namespace QuanLyVanTai.Tests.DAL
{
    public class StationRepositoryTests
    {
        // ── GetAllWithRoutesAsync ─────────────────────────────────────────────

        [Fact]
        public async Task GetAllWithRoutes_ReturnsStationsWithRoutes()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new StationRepository(db);

            var result = await repo.GetAllWithRoutesAsync();

            Assert.Equal(2, result.Count);
            // Cả 2 trạm đều thuộc tuyến HN-DN-01
            Assert.All(result, s => Assert.Single(s.Routes));
        }

        [Fact]
        public async Task GetAllWithRoutes_KeywordFilter_ReturnsMatch()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new StationRepository(db);

            var result = await repo.GetAllWithRoutesAsync("Đà Nẵng");

            Assert.Single(result);
            Assert.Equal("BX-DN", result[0].StationCode);
        }

        // ── IsStationCodeExistsAsync ──────────────────────────────────────────

        [Fact]
        public async Task IsStationCodeExists_ExistingCode_ReturnsTrue()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new StationRepository(db);

            Assert.True(await repo.IsStationCodeExistsAsync("BX-HN"));
        }

        [Fact]
        public async Task IsStationCodeExists_ExcludeSelf_ReturnsFalse()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new StationRepository(db);

            // Khi update trạm ID=1 với cùng code → không được báo trùng
            Assert.False(await repo.IsStationCodeExistsAsync("BX-HN", excludeId: 1));
        }

        [Fact]
        public async Task IsStationCodeExists_NewCode_ReturnsFalse()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new StationRepository(db);

            Assert.False(await repo.IsStationCodeExistsAsync("BX-NEWCODE"));
        }

        // ── GetDistinctCitiesAsync ────────────────────────────────────────────

        [Fact]
        public async Task GetDistinctCities_ReturnsSortedUniqueList()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new StationRepository(db);

            var cities = await repo.GetDistinctCitiesAsync();

            Assert.Equal(2, cities.Count);
            Assert.Equal("Đà Nẵng", cities[0]); // OrderBy tên
            Assert.Equal("Hà Nội", cities[1]);
        }

        // ── DeleteWithTransactionAsync (soft-delete) ──────────────────────────

        [Fact]
        public async Task SoftDelete_Station_SetsIsDeletedTrue()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new StationRepository(db);

            await repo.DeleteWithTransactionAsync(1, softDelete: true);

            var deleted = await db.Stations.IgnoreQueryFilters()
                .FirstOrDefaultAsync(s => s.Id == 1);
            Assert.NotNull(deleted);
            Assert.True(deleted.IsDeleted);
        }

        [Fact]
        public async Task SoftDelete_NonExistentStation_ThrowsKeyNotFoundException()
        {
            using var db = TestDbContextFactory.Create();
            var repo = new StationRepository(db);

            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => repo.DeleteWithTransactionAsync(999, softDelete: true));
        }

        [Fact]
        public async Task HardDelete_StationWithActiveRoute_ThrowsInvalidOperationException()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new StationRepository(db);

            // Trạm đang thuộc tuyến Active → không được hard-delete
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => repo.DeleteWithTransactionAsync(1, softDelete: false));
        }
    }
}
