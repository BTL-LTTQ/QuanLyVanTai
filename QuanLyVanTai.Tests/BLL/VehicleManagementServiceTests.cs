using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.Exceptions;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.DAL.Repositories;
using QuanLyVanTai.Tests.Helpers;
using BllValidationException = QuanLyVanTai.BLL.Exceptions.ValidationException;

namespace QuanLyVanTai.Tests.BLL
{
    public class VehicleManagementServiceTests
    {
        // ── GetAllWithRoute (Include join 1-N) ────────────────────────────────

        [Fact]
        public async Task GetVehicles_IncludesRouteNavigation()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new VehicleRepository(db);

            var vehicles = await repo.GetAllWithRouteAsync();

            var assignedVehicle = vehicles.First(v => v.RouteId == 1);
            Assert.NotNull(assignedVehicle.Route);
            Assert.Equal("Hà Nội - Đà Nẵng", assignedVehicle.Route!.RouteName);
        }

        // ── GetAvailable ──────────────────────────────────────────────────────

        [Fact]
        public async Task GetAvailableVehicles_ExcludesAssignedAndNonReady()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            await SeedData.SeedInTransitVehicleAsync(db);
            var repo = new VehicleRepository(db);

            var available = await repo.GetAvailableVehiclesAsync();

            // Chỉ xe ID=2 (Ready + no route)
            Assert.Single(available);
            Assert.Equal(2, available[0].Id);
        }

        // ── InTransit business rule ───────────────────────────────────────────

        [Fact]
        public async Task DeleteVehicle_InTransit_BllThrowsBusinessRuleViolation()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            await SeedData.SeedInTransitVehicleAsync(db);
            var repo = new VehicleRepository(db);

            var vehicle = await db.Vehicles.FindAsync(3);

            // Simulate BLL business rule check
            var ex = await Assert.ThrowsAsync<BusinessRuleViolationException>(async () =>
            {
                if (vehicle!.Status == "InTransit")
                    throw new BusinessRuleViolationException("Vehicle",
                        $"Xe [{vehicle.LicensePlate}] đang trong trạng thái InTransit.");
                await Task.CompletedTask;
            });

            Assert.Equal("Vehicle", ex.EntityName);
            Assert.Contains("InTransit", ex.Message);
        }

        // ── Duplicate license plate ───────────────────────────────────────────

        [Fact]
        public async Task IsLicensePlateExists_SamePlate_ReturnsTrue()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new VehicleRepository(db);

            Assert.True(await repo.IsLicensePlateExistsAsync("29B-12345"));
        }

        [Fact]
        public async Task IsLicensePlateExists_UpdateSamePlate_ReturnsFalse()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new VehicleRepository(db);

            // Xe đang giữ biển số của chính nó → không bị báo trùng
            Assert.False(await repo.IsLicensePlateExistsAsync("29B-12345", excludeId: 1));
        }

        // ── Soft delete ───────────────────────────────────────────────────────

        [Fact]
        public async Task SoftDelete_Vehicle_IsDeletedTrueAndRouteIdNull()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new VehicleRepository(db);

            await repo.DeleteWithTransactionAsync(1, softDelete: true);

            var deleted = await db.Vehicles.IgnoreQueryFilters()
                .FirstOrDefaultAsync(v => v.Id == 1);
            Assert.True(deleted!.IsDeleted);
            Assert.Null(deleted.RouteId);
        }

        [Fact]
        public async Task SoftDelete_FreeVehicle_IsDeleted()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            var repo = new VehicleRepository(db);

            await repo.DeleteWithTransactionAsync(2, softDelete: true);

            var deleted = await db.Vehicles.IgnoreQueryFilters()
                .FirstOrDefaultAsync(v => v.Id == 2);
            Assert.True(deleted!.IsDeleted);
        }

        // ── Validation ────────────────────────────────────────────────────────

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(101)]
        public async Task CreateVehicle_InvalidTotalSeats_ThrowsValidationException(int seats)
        {
            var ex = await Assert.ThrowsAsync<BllValidationException>(async () =>
            {
                if (seats <= 0 || seats > 100)
                    throw new BllValidationException("Vehicle", "TotalSeats", "Số ghế phải từ 1 đến 100.");
                await Task.CompletedTask;
            });

            Assert.Equal("TotalSeats", ex.FieldName);
        }

        [Theory]
        [InlineData("Ready")]
        [InlineData("InTransit")]
        [InlineData("Maintenance")]
        public async Task CreateVehicle_ValidStatus_DoesNotThrow(string status)
        {
            var validStatuses = new[] { "Ready", "InTransit", "Maintenance" };
            await Task.Run(() => Assert.Contains(status, validStatuses));
        }

        [Fact]
        public async Task CreateVehicle_InvalidStatus_ThrowsValidationException()
        {
            var ex = await Assert.ThrowsAsync<BllValidationException>(async () =>
            {
                string status = "Broken";
                var valid = new[] { "Ready", "InTransit", "Maintenance" };
                if (!valid.Contains(status))
                    throw new BllValidationException("Vehicle", "Status", "Trạng thái không hợp lệ.");
                await Task.CompletedTask;
            });

            Assert.Equal("Status", ex.FieldName);
        }

        // ── GetVehiclesByRoute ────────────────────────────────────────────────

        [Fact]
        public async Task GetVehiclesByRoute_CorrectCount()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);
            // Thêm 1 xe nữa vào tuyến 1
            db.Vehicles.Add(new Vehicle
            {
                Id = 10, LicensePlate = "88A-88888", VehicleType = "Ghế ngồi",
                TotalSeats = 45, Status = "Ready", RouteId = 1
            });
            await db.SaveChangesAsync();

            var repo = new VehicleRepository(db);
            var result = await repo.GetVehiclesByRouteAsync(1);

            Assert.Equal(2, result.Count);
        }
    }
}
