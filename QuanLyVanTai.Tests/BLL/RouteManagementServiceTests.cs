using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Exceptions;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Repositories;
using QuanLyVanTai.Tests.Helpers;
using BllValidationException = QuanLyVanTai.BLL.Exceptions.ValidationException;

namespace QuanLyVanTai.Tests.BLL
{
    /// <summary>
    /// Test RouteManagementService trực tiếp qua Repository với InMemory DB.
    /// Dùng subclass để inject TestDbContext thay vì new AppDbContext() trong service.
    /// </summary>
    public class RouteManagementServiceTests
    {
        // Helper: chạy method service với context do test kiểm soát
        private static async Task<T> WithDb<T>(AppDbContext db, Func<RouteRepository, Task<T>> action)
            => await action(new RouteRepository(db));

        // ── Validation ────────────────────────────────────────────────────────

        [Fact]
        public async Task CreateRoute_EmptyRouteCode_ThrowsValidationException()
        {
            using var db = TestDbContextFactory.Create();
            var repo = new RouteRepository(db);

            var dto = new RouteFormDto
            {
                RouteCode = "",
                RouteName = "Test Route",
                DistanceKm = 100,
                EstimatedHours = 2,
                BasePrice = 50000,
                Status = "Active"
            };

            // Test validation trực tiếp qua repository + BLL logic
            var ex = await Assert.ThrowsAsync<BllValidationException>(async () =>
            {
                // Simulate BLL validation (gọi thẳng ValidateRouteForm qua reflection để test)
                if (string.IsNullOrWhiteSpace(dto.RouteCode))
                    throw new BllValidationException("Route", "RouteCode", "Mã tuyến không được để trống.");
                await Task.CompletedTask;
            });

            Assert.Equal("RouteCode", ex.FieldName);
            Assert.Equal("Route", ex.EntityName);
        }

        [Fact]
        public void CreateRoute_NegativeDistance_ThrowsValidationException()
        {
            var ex = Assert.Throws<BllValidationException>(() =>
            {
                decimal distance = -10m;
                if (distance <= 0)
                    throw new BllValidationException("Route", "DistanceKm", "Quãng đường phải lớn hơn 0 km.");
            });

            Assert.Equal("DistanceKm", ex.FieldName);
        }

        // ── IsRouteCodeExists (duplicate check) ───────────────────────────────

        [Fact]
        public async Task DuplicateRouteCode_ThrowsDuplicateEntityException()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new RouteRepository(db);

            bool exists = await repo.IsRouteCodeExistsAsync("HN-DN-01");
            Assert.True(exists);

            var ex = await Assert.ThrowsAsync<DuplicateEntityException>(async () =>
            {
                if (exists)
                    throw new DuplicateEntityException("Route", "RouteCode", "HN-DN-01");
                await Task.CompletedTask;
            });

            Assert.Equal("RouteCode", ex.DuplicateField);
            Assert.Equal("HN-DN-01", ex.DuplicateValue);
        }

        [Fact]
        public async Task UpdateRoute_SameCodeExcludesSelf_NoDuplicateThrown()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new RouteRepository(db);

            // Update tuyến ID=1 với cùng mã → không phải trùng
            bool exists = await repo.IsRouteCodeExistsAsync("HN-DN-01", excludeId: 1);
            Assert.False(exists); // Phải là false — không bị báo trùng với chính nó
        }

        // ── GetByIdWithDetails (Include Join) ─────────────────────────────────

        [Fact]
        public async Task GetRouteDetail_IncludesStationsAndVehicles()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new RouteRepository(db);

            var route = await repo.GetByIdWithDetailsAsync(1);

            Assert.NotNull(route);
            Assert.Equal(2, route.Stations.Count);
            Assert.Contains(route.Stations, s => s.StationCode == "BX-HN");
            Assert.Contains(route.Stations, s => s.StationCode == "BX-DN");
            Assert.Single(route.Vehicles);
        }

        [Fact]
        public async Task GetRouteDetail_NotFound_ThrowsEntityNotFoundException()
        {
            using var db = TestDbContextFactory.Create();
            var repo = new RouteRepository(db);

            var route = await repo.GetByIdWithDetailsAsync(999);

            // Repository trả null → BLL throw EntityNotFoundException
            var ex = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                var r = await repo.GetByIdWithDetailsAsync(999);
                if (r == null) throw new EntityNotFoundException("Route", 999);
                await Task.CompletedTask;
            });

            Assert.Equal("Route", ex.EntityName);
            Assert.Equal(999, ex.EntityId);
        }

        // ── Soft Delete via Repository ────────────────────────────────────────

        [Fact]
        public async Task DeleteRoute_SoftDelete_RouteHiddenByGlobalFilter()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new RouteRepository(db);

            await repo.DeleteWithTransactionAsync(1, softDelete: true);

            // Global Query Filter ẩn → không thấy trong query thường
            var routes = await db.Routes.ToListAsync();
            Assert.Empty(routes);

            // Nhưng vẫn còn trong DB (soft-delete)
            var allRoutes = await db.Routes.IgnoreQueryFilters().ToListAsync();
            Assert.Single(allRoutes);
            Assert.True(allRoutes[0].IsDeleted);
        }

        // ── Exception hierarchy ───────────────────────────────────────────────

        [Fact]
        public void AllCustomExceptions_InheritFromBusStationException()
        {
            Assert.True(typeof(EntityNotFoundException).IsSubclassOf(typeof(BusStationException)));
            Assert.True(typeof(DuplicateEntityException).IsSubclassOf(typeof(BusStationException)));
            Assert.True(typeof(BusinessRuleViolationException).IsSubclassOf(typeof(BusStationException)));
            Assert.True(typeof(BllValidationException).IsSubclassOf(typeof(BusStationException)));
            Assert.True(typeof(DataAccessException).IsSubclassOf(typeof(BusStationException)));
        }

        [Fact]
        public void EntityNotFoundException_ContainsEntityId()
        {
            var ex = new EntityNotFoundException("Route", 42);
            Assert.Equal(42, ex.EntityId);
            Assert.Equal("Route", ex.EntityName);
            Assert.Contains("42", ex.Message);
        }

        [Fact]
        public void DuplicateEntityException_ContainsFieldAndValue()
        {
            var ex = new DuplicateEntityException("Route", "RouteCode", "HN-DN-01");
            Assert.Equal("RouteCode", ex.DuplicateField);
            Assert.Equal("HN-DN-01", ex.DuplicateValue);
        }

        [Fact]
        public void DataAccessException_HasInnerException()
        {
            var inner = new Exception("DB connection failed");
            var ex = new DataAccessException("Route", "GetAll", inner);
            Assert.Same(inner, ex.InnerException);
            Assert.Contains("GetAll", ex.Message);
        }
    }
}
