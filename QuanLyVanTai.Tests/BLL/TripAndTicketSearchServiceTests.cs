using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.Tests.Helpers;
using Xunit;

namespace QuanLyVanTai.Tests.BLL
{
    public class TripAndTicketSearchServiceTests
    {
        [Fact]
        public async Task SearchAvailableTripsAsync_KeywordSearch_MatchesRouteAndVehicle()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);

            var service = new CustomerBookingService(db);

            // 1. Tìm theo từ khóa tên tuyến
            var resultRoute = await service.SearchAvailableTripsAsync(new TripSearchCriteriaDto
            {
                Keyword = "Đà Nẵng"
            });
            Assert.NotEmpty(resultRoute);
            Assert.Contains(resultRoute, r => r.RouteName.Contains("Đà Nẵng"));

            // 2. Tìm theo biển số xe
            var resultPlate = await service.SearchAvailableTripsAsync(new TripSearchCriteriaDto
            {
                Keyword = "29B-11111"
            });
            Assert.NotEmpty(resultPlate);
            Assert.Contains(resultPlate, r => r.LicensePlate == "29B-11111");

            // 3. Từ khóa không khớp bất kỳ chuyến nào
            var resultNotFound = await service.SearchAvailableTripsAsync(new TripSearchCriteriaDto
            {
                Keyword = "CanThoVungTauKhongCo"
            });
            Assert.Empty(resultNotFound);
        }

        [Fact]
        public async Task SearchAvailableTripsAsync_DateFilter_FiltersCorrectDay()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);

            var service = new CustomerBookingService(db);

            // Chuyến mẫu ScheduleSeedData.Trip1Departure có ngày là 2026-10-11
            var matchDate = new DateTime(2026, 10, 11);
            var wrongDate = new DateTime(2026, 10, 20);

            var matches = await service.SearchAvailableTripsAsync(new TripSearchCriteriaDto
            {
                DepartureDate = matchDate
            });
            Assert.NotEmpty(matches);

            var nonMatches = await service.SearchAvailableTripsAsync(new TripSearchCriteriaDto
            {
                DepartureDate = wrongDate
            });
            Assert.Empty(nonMatches);
        }

        [Fact]
        public async Task SearchAvailableTripsAsync_PriceRange_FiltersCorrectly()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);

            var service = new CustomerBookingService(db);
            // Route 10 trong ScheduleSeedData có BasePrice = 350,000

            // Khoảng giá bao trùm: 200,000 -> 400,000
            var withinRange = await service.SearchAvailableTripsAsync(new TripSearchCriteriaDto
            {
                MinPrice = 200000,
                MaxPrice = 400000
            });
            Assert.NotEmpty(withinRange);

            // Khoảng giá cao hơn giá vé: 500,000 -> 1,000,000
            var aboveRange = await service.SearchAvailableTripsAsync(new TripSearchCriteriaDto
            {
                MinPrice = 500000,
                MaxPrice = 1000000
            });
            Assert.Empty(aboveRange);

            // Khoảng giá thấp hơn giá vé: 100,000 -> 200,000
            var belowRange = await service.SearchAvailableTripsAsync(new TripSearchCriteriaDto
            {
                MinPrice = 100000,
                MaxPrice = 200000
            });
            Assert.Empty(belowRange);
        }

        [Fact]
        public async Task SearchAvailableTripsAsync_SeatCalculation_SubtractsBookedTickets()
        {
            using var db = TestDbContextFactory.Create();
            await ScheduleSeedData.SeedAsync(db);

            // Chuyến Id 100 có xe 10 (TotalSeats = 40), khởi hành ScheduleSeedData.Trip1Departure
            // Thêm 2 vé đã đặt và 1 vé đã hủy
            db.Tickets.AddRange(
                new Ticket
                {
                    TicketCode = "TK_TEST_01",
                    RouteId = 10,
                    VehicleId = 10,
                    DepartureTime = ScheduleSeedData.Trip1Departure,
                    SeatNumber = "A01",
                    TicketStatus = "Booked",
                    PaymentStatus = "Paid"
                },
                new Ticket
                {
                    TicketCode = "TK_TEST_02",
                    RouteId = 10,
                    VehicleId = 10,
                    DepartureTime = ScheduleSeedData.Trip1Departure,
                    SeatNumber = "A02",
                    TicketStatus = "Boarded",
                    PaymentStatus = "Paid"
                },
                new Ticket
                {
                    TicketCode = "TK_TEST_03_CANCELLED",
                    RouteId = 10,
                    VehicleId = 10,
                    DepartureTime = ScheduleSeedData.Trip1Departure,
                    SeatNumber = "A03",
                    TicketStatus = "Cancelled",
                    PaymentStatus = "Refunded"
                }
            );
            await db.SaveChangesAsync();

            var service = new CustomerBookingService(db);
            var trips = await service.SearchAvailableTripsAsync(new TripSearchCriteriaDto());

            var trip = Assert.Single(trips);
            Assert.Equal(40, trip.TotalSeats);
            Assert.Equal(2, trip.BookedSeats); // 2 vé (Booked + Boarded), không tính vé Cancelled
            Assert.Equal(38, trip.AvailableSeats); // 40 - 2 = 38
        }

        [Fact]
        public async Task TicketService_SearchAndFilterTicketsAsync_FiltersByKeywordAndStatusAndPrice()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);

            db.Tickets.AddRange(
                new Ticket
                {
                    TicketCode = "TK2026101701",
                    CustomerName = "Trần Hoàng Nam",
                    CustomerPhone = "0911223344",
                    RouteId = 1,
                    VehicleId = 1,
                    SeatNumber = "A10",
                    Price = 350000,
                    PaymentStatus = "Paid",
                    TicketStatus = "Booked",
                    DepartureTime = DateTime.UtcNow.AddDays(1)
                },
                new Ticket
                {
                    TicketCode = "TK2026101702",
                    CustomerName = "Lê Thị Mai",
                    CustomerPhone = "0988776655",
                    RouteId = 1,
                    VehicleId = 1,
                    SeatNumber = "A11",
                    Price = 500000,
                    PaymentStatus = "Paid",
                    TicketStatus = "Boarded",
                    DepartureTime = DateTime.UtcNow.AddDays(2)
                },
                new Ticket
                {
                    TicketCode = "TK2026101703",
                    CustomerName = "Vũ Đình Tuấn",
                    CustomerPhone = "0933445566",
                    RouteId = 1,
                    VehicleId = 1,
                    SeatNumber = "B01",
                    Price = 300000,
                    PaymentStatus = "Pending",
                    TicketStatus = "Booked",
                    DepartureTime = DateTime.UtcNow.AddDays(3)
                }
            );
            await db.SaveChangesAsync();

            var ticketService = new TicketService(db);

            // 1. Lọc theo từ khóa số điện thoại
            var phoneResult = await ticketService.SearchAndFilterTicketsAsync(new TicketFilterCriteria
            {
                Keyword = "0911223344"
            });
            Assert.Single(phoneResult);
            Assert.Equal("TK2026101701", phoneResult[0].TicketCode);

            // 2. Lọc theo trạng thái Boarded (đã lên xe)
            var boardedResult = await ticketService.SearchAndFilterTicketsAsync(new TicketFilterCriteria
            {
                TicketStatus = "Boarded"
            });
            Assert.Single(boardedResult);
            Assert.Equal("TK2026101702", boardedResult[0].TicketCode);

            // 3. Lọc theo khoảng giá (MinPrice = 400,000)
            var priceResult = await ticketService.SearchAndFilterTicketsAsync(new TicketFilterCriteria
            {
                MinPrice = 400000
            });
            Assert.Single(priceResult);
            Assert.Equal(500000, priceResult[0].Price);

            // 4. Lọc theo PaymentStatus = "Pending"
            var pendingResult = await ticketService.SearchAndFilterTicketsAsync(new TicketFilterCriteria
            {
                PaymentStatus = "Pending"
            });
            Assert.Single(pendingResult);
            Assert.Equal("Vũ Đình Tuấn", pendingResult[0].CustomerName);
        }
    }
}
