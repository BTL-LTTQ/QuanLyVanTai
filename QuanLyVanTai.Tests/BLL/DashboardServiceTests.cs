using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace QuanLyVanTai.Tests.BLL
{
    public class DashboardServiceTests
    {
        [Fact]
        public async Task GetSummaryAsync_ReturnsAccurateTotalsAndTodayStats()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using (var db = TestDbContextFactory.Create(dbName))
            {
                var route1 = new Route { Id = 1, RouteCode = "R1", RouteName = "Hà Nội - Hải Phòng", DistanceKm = 100, EstimatedHours = 2, BasePrice = 150000, Status = "Active" };
                var route2 = new Route { Id = 2, RouteCode = "R2", RouteName = "Hà Nội - Đà Nẵng", DistanceKm = 700, EstimatedHours = 14, BasePrice = 450000, Status = "Active" };
                var vehicle1 = new Vehicle { Id = 1, LicensePlate = "29B-111.11", TotalSeats = 29, Status = "Ready" };

                db.Routes.AddRange(route1, route2);
                db.Vehicles.Add(vehicle1);

                var today = DateTime.Today;
                db.Tickets.AddRange(
                    new Ticket { TicketCode = "TK01", CustomerName = "A", CustomerPhone = "091", SeatNumber = "1", Price = 150000, DepartureTime = today.AddHours(8), PaymentStatus = "Paid", TicketStatus = "Booked", RouteId = 1, VehicleId = 1 },
                    new Ticket { TicketCode = "TK02", CustomerName = "B", CustomerPhone = "092", SeatNumber = "2", Price = 450000, DepartureTime = today.AddHours(9), PaymentStatus = "Paid", TicketStatus = "Booked", RouteId = 2, VehicleId = 1 },
                    new Ticket { TicketCode = "TK03", CustomerName = "C", CustomerPhone = "093", SeatNumber = "3", Price = 150000, DepartureTime = today.AddDays(-2), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = 1, VehicleId = 1 },
                    new Ticket { TicketCode = "TK04", CustomerName = "D", CustomerPhone = "094", SeatNumber = "4", Price = 200000, DepartureTime = today.AddHours(10), PaymentStatus = "Pending", TicketStatus = "Cancelled", RouteId = 1, VehicleId = 1 } // Ignored
                );
                await db.SaveChangesAsync();
            }

            var service = new DashboardService(() => TestDbContextFactory.Create(dbName));

            // Act
            var summary = await service.GetSummaryAsync();

            // Assert
            Assert.Equal(750000m, summary.TotalRevenue); // 150k + 450k + 150k
            Assert.Equal(3, summary.TotalTickets);
            Assert.Equal(2, summary.ActiveRoutes);
            Assert.Equal(1, summary.ActiveVehicles);
            Assert.Equal(600000m, summary.TodayRevenue); // TK01 + TK02
            Assert.Equal(2, summary.TodayTickets);
        }

        [Fact]
        public async Task GetRevenueByDateAsync_ReturnsContinuousDailySeries()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            var today = DateTime.Today;
            using (var db = TestDbContextFactory.Create(dbName))
            {
                var route = new Route { Id = 1, RouteCode = "R1", RouteName = "HN-HP", DistanceKm = 100, EstimatedHours = 2, BasePrice = 100000, Status = "Active" };
                var vehicle = new Vehicle { Id = 1, LicensePlate = "29B-111", TotalSeats = 29, Status = "Ready" };
                db.Routes.Add(route);
                db.Vehicles.Add(vehicle);

                db.Tickets.AddRange(
                    new Ticket { TicketCode = "TK01", CustomerName = "A", CustomerPhone = "091", SeatNumber = "1", Price = 100000, DepartureTime = today.AddDays(-2).AddHours(8), PaymentStatus = "Paid", TicketStatus = "Completed", RouteId = 1, VehicleId = 1 },
                    new Ticket { TicketCode = "TK02", CustomerName = "B", CustomerPhone = "092", SeatNumber = "2", Price = 100000, DepartureTime = today.AddHours(8), PaymentStatus = "Paid", TicketStatus = "Booked", RouteId = 1, VehicleId = 1 }
                );
                await db.SaveChangesAsync();
            }

            var service = new DashboardService(() => TestDbContextFactory.Create(dbName));

            // Act: 3 days range: today-2 to today
            var list = await service.GetRevenueByDateAsync(today.AddDays(-2), today);

            // Assert
            Assert.Equal(3, list.Count);
            Assert.Equal(100000m, list[0].Revenue); // today - 2
            Assert.Equal(0m, list[1].Revenue);      // today - 1
            Assert.Equal(100000m, list[2].Revenue); // today
        }

        [Fact]
        public async Task GetRevenueByRouteAsync_ReturnsGroupedRouteRevenueAndPercentages()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using (var db = TestDbContextFactory.Create(dbName))
            {
                var r1 = new Route { Id = 1, RouteCode = "R1", RouteName = "Tuyến 1", DistanceKm = 100, EstimatedHours = 2, BasePrice = 100000, Status = "Active" };
                var r2 = new Route { Id = 2, RouteCode = "R2", RouteName = "Tuyến 2", DistanceKm = 200, EstimatedHours = 4, BasePrice = 300000, Status = "Active" };
                var v = new Vehicle { Id = 1, LicensePlate = "29B-111", TotalSeats = 29, Status = "Ready" };
                db.Routes.AddRange(r1, r2);
                db.Vehicles.Add(v);

                db.Tickets.AddRange(
                    new Ticket { TicketCode = "TK01", CustomerName = "A", CustomerPhone = "091", SeatNumber = "1", Price = 100000, DepartureTime = DateTime.Today, PaymentStatus = "Paid", TicketStatus = "Booked", RouteId = 1, VehicleId = 1 },
                    new Ticket { TicketCode = "TK02", CustomerName = "B", CustomerPhone = "092", SeatNumber = "2", Price = 300000, DepartureTime = DateTime.Today, PaymentStatus = "Paid", TicketStatus = "Booked", RouteId = 2, VehicleId = 1 }
                );
                await db.SaveChangesAsync();
            }

            var service = new DashboardService(() => TestDbContextFactory.Create(dbName));

            // Act
            var routes = await service.GetRevenueByRouteAsync();

            // Assert
            Assert.Equal(2, routes.Count);
            Assert.Equal("Tuyến 2", routes[0].RouteName); // Sorted desc by revenue (300k > 100k)
            Assert.Equal(300000m, routes[0].Revenue);
            Assert.Equal(75.0, routes[0].Percentage);
            Assert.Equal("Tuyến 1", routes[1].RouteName);
            Assert.Equal(100000m, routes[1].Revenue);
            Assert.Equal(25.0, routes[1].Percentage);
        }

        [Fact]
        public async Task GetRecentTransactionsAsync_ReturnsOrderedRecentTickets()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using (var db = TestDbContextFactory.Create(dbName))
            {
                var r1 = new Route { Id = 1, RouteCode = "R1", RouteName = "Tuyến 1", DistanceKm = 100, EstimatedHours = 2, BasePrice = 100000, Status = "Active" };
                var v = new Vehicle { Id = 1, LicensePlate = "29B-111", TotalSeats = 29, Status = "Ready" };
                db.Routes.Add(r1);
                db.Vehicles.Add(v);

                db.Tickets.AddRange(
                    new Ticket { Id = 1, TicketCode = "TK01", CustomerName = "A", CustomerPhone = "091", SeatNumber = "1", Price = 100000, DepartureTime = DateTime.Today, PaymentStatus = "Paid", TicketStatus = "Booked", RouteId = 1, VehicleId = 1 },
                    new Ticket { Id = 2, TicketCode = "TK02", CustomerName = "B", CustomerPhone = "092", SeatNumber = "2", Price = 100000, DepartureTime = DateTime.Today, PaymentStatus = "Paid", TicketStatus = "Booked", RouteId = 1, VehicleId = 1 }
                );
                await db.SaveChangesAsync();
            }

            var service = new DashboardService(() => TestDbContextFactory.Create(dbName));

            // Act
            var transactions = await service.GetRecentTransactionsAsync(5);

            // Assert
            Assert.Equal(2, transactions.Count);
            Assert.Equal("TK02", transactions[0].TicketCode); // id 2 first
        }
    }
}
