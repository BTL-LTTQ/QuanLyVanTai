using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL.Repositories;
using QuanLyVanTai.Tests.Helpers;

namespace QuanLyVanTai.Tests.DAL
{
    public class VehicleRepositoryTests
    {
        // ── GetAllWithRouteAsync ──────────────────────────────────────────────

        [Fact]
        public async Task GetAllWithRoute_ReturnsVehiclesWithRoute()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new VehicleRepository(db);

            var result = await repo.GetAllWithRouteAsync();

            Assert.Equal(2, result.Count);
            var assigned = result.First(v => v.Id == 1);
            Assert.NotNull(assigned.Route);
            Assert.Equal("HN-DN-01", assigned.Route!.RouteCode);
        }

        [Fact]
        public async Task GetAllWithRoute_FreeVehicle_RouteIsNull()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new VehicleRepository(db);

            var result = await repo.GetAllWithRouteAsync();

            var free = result.First(v => v.Id == 2);
            Assert.Null(free.Route);
        }

        // ── GetAvailableVehiclesAsync ─────────────────────────────────────────

        [Fact]
        public async Task GetAvailableVehicles_ReturnsOnlyReadyAndUnassigned()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new VehicleRepository(db);

            var result = await repo.GetAvailableVehiclesAsync();

            // Chỉ xe ID=2 là Ready + RouteId null
            Assert.Single(result);
            Assert.Equal("51A-99999", result[0].LicensePlate);
        }

        // ── GetVehiclesByRouteAsync ───────────────────────────────────────────

        [Fact]
        public async Task GetVehiclesByRoute_ReturnsVehiclesForRoute()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new VehicleRepository(db);

            var result = await repo.GetVehiclesByRouteAsync(1);

            Assert.Single(result);
            Assert.Equal("29B-12345", result[0].LicensePlate);
        }

        [Fact]
        public async Task GetVehiclesByRoute_NoVehicles_ReturnsEmpty()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new VehicleRepository(db);

            var result = await repo.GetVehiclesByRouteAsync(999);

            Assert.Empty(result);
        }

        // ── IsLicensePlateExistsAsync ─────────────────────────────────────────

        [Fact]
        public async Task IsLicensePlateExists_ExistingPlate_ReturnsTrue()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new VehicleRepository(db);

            Assert.True(await repo.IsLicensePlateExistsAsync("29B-12345"));
        }

        [Fact]
        public async Task IsLicensePlateExists_ExcludeSelf_ReturnsFalse()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new VehicleRepository(db);

            Assert.False(await repo.IsLicensePlateExistsAsync("29B-12345", excludeId: 1));
        }

        [Fact]
        public async Task IsLicensePlateExists_NewPlate_ReturnsFalse()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new VehicleRepository(db);

            Assert.False(await repo.IsLicensePlateExistsAsync("00A-00000"));
        }

        // ── DeleteWithTransactionAsync ────────────────────────────────────────

        [Fact]
        public async Task SoftDelete_Vehicle_SetsIsDeletedAndNullsRouteId()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new VehicleRepository(db);

            await repo.DeleteWithTransactionAsync(1, softDelete: true);

            var deleted = await db.Vehicles.IgnoreQueryFilters()
                .FirstOrDefaultAsync(v => v.Id == 1);
            Assert.NotNull(deleted);
            Assert.True(deleted.IsDeleted);
            Assert.Null(deleted.RouteId);
        }

        [Fact]
        public async Task SoftDelete_NonExistentVehicle_ThrowsKeyNotFoundException()
        {
            using var db = TestDbContextFactory.Create();
            var repo = new VehicleRepository(db);

            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => repo.DeleteWithTransactionAsync(999, softDelete: true));
        }
    }
}
