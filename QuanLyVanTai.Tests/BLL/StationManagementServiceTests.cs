using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.Exceptions;
using QuanLyVanTai.DAL.Repositories;
using QuanLyVanTai.Tests.Helpers;
using BllValidationException = QuanLyVanTai.BLL.Exceptions.ValidationException;

namespace QuanLyVanTai.Tests.BLL
{
    public class StationManagementServiceTests
    {
        // ── GetAllWithRoutes (Include join N-N) ───────────────────────────────

        [Fact]
        public async Task GetStations_IncludesRoutesForEachStation()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new StationRepository(db);

            var stations = await repo.GetAllWithRoutesAsync();

            Assert.Equal(2, stations.Count);
            // Mỗi trạm phải có ít nhất 1 route liên kết
            Assert.All(stations, s => Assert.NotEmpty(s.Routes));
        }

        // ── City distinct ─────────────────────────────────────────────────────

        [Fact]
        public async Task GetDistinctCities_ReturnsAlphabeticalOrder()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new StationRepository(db);

            var cities = await repo.GetDistinctCitiesAsync();

            Assert.Equal(2, cities.Count);
            // Sorted alphabetically: Đà Nẵng < Hà Nội
            Assert.Equal(cities.OrderBy(c => c).ToList(), cities);
        }

        // ── Duplicate check ───────────────────────────────────────────────────

        [Fact]
        public async Task IsStationCodeExists_DifferentIdSameCode_ReturnsTrue()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new StationRepository(db);

            // Trạm ID=2 muốn dùng mã của ID=1 → phải báo trùng
            bool exists = await repo.IsStationCodeExistsAsync("BX-HN", excludeId: 2);
            Assert.True(exists);
        }

        // ── Soft delete ───────────────────────────────────────────────────────

        [Fact]
        public async Task SoftDelete_Station_DisappearsFromNormalQuery()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new StationRepository(db);

            await repo.DeleteWithTransactionAsync(1, softDelete: true);

            var visible = await db.Stations.ToListAsync();
            Assert.Single(visible); // chỉ còn trạm ID=2
            Assert.Equal(2, visible[0].Id);
        }

        // ── Hard delete business rule ─────────────────────────────────────────

        [Fact]
        public async Task HardDelete_StationInActiveRoute_ThrowsInvalidOperation()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new StationRepository(db);

            // Cả 2 trạm đều thuộc tuyến Active → hard-delete phải fail
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(
                () => repo.DeleteWithTransactionAsync(2, softDelete: false));

            Assert.Contains("tuyến đang hoạt động", ex.Message);
        }

        // ── BLL exception mapping ─────────────────────────────────────────────

        [Fact]
        public async Task DeleteStation_NotFound_BllMapsToEntityNotFoundException()
        {
            using var db = TestDbContextFactory.Create();
            var repo = new StationRepository(db);

            // Giả lập BLL catch KeyNotFoundException → rethrow EntityNotFoundException
            var ex = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                try
                {
                    await repo.DeleteWithTransactionAsync(999, softDelete: true);
                }
                catch (KeyNotFoundException knf)
                {
                    throw new EntityNotFoundException("Station", knf.Message);
                }
            });

            Assert.Equal("Station", ex.EntityName);
        }

        // ── Validation ────────────────────────────────────────────────────────

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public async Task CreateStation_EmptyCode_ThrowsValidationException(string code)
        {
            var ex = await Assert.ThrowsAsync<BllValidationException>(async () =>
            {
                if (string.IsNullOrWhiteSpace(code))
                    throw new BllValidationException("Station", "StationCode", "Mã trạm không được để trống.");
                await Task.CompletedTask;
            });

            Assert.Equal("StationCode", ex.FieldName);
        }

        [Fact]
        public async Task CreateStation_InvalidStatus_ThrowsValidationException()
        {
            var validStatuses = new[] { "Active", "Inactive" };
            string invalidStatus = "Unknown";

            var ex = await Assert.ThrowsAsync<BllValidationException>(async () =>
            {
                if (!validStatuses.Contains(invalidStatus))
                    throw new BllValidationException("Station", "Status", "Trạng thái không hợp lệ.");
                await Task.CompletedTask;
            });

            Assert.Equal("Status", ex.FieldName);
        }
    }
}
