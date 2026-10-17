using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyVanTai.BLL.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly Func<AppDbContext> _contextFactory;

        public DashboardService() : this(() => new AppDbContext()) { }

        public DashboardService(Func<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        private AppDbContext CreateContext() => _contextFactory();

        public async Task<DashboardSummaryDto> GetSummaryAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            using var db = CreateContext();

            var query = db.Tickets
                .AsNoTracking()
                .Where(t => !t.IsDeleted && t.TicketStatus != "Cancelled");

            if (fromDate.HasValue)
            {
                var start = fromDate.Value.Date;
                query = query.Where(t => t.DepartureTime >= start);
            }

            if (toDate.HasValue)
            {
                var end = toDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(t => t.DepartureTime <= end);
            }

            var paidQuery = query.Where(t => t.PaymentStatus == "Paid");

            decimal totalRevenue = await paidQuery.SumAsync(t => (decimal?)t.Price) ?? 0m;
            int totalTickets = await paidQuery.CountAsync();

            int activeRoutes = await db.Routes
                .AsNoTracking()
                .CountAsync(r => !r.IsDeleted && r.Status == "Active");

            int activeVehicles = await db.Vehicles
                .AsNoTracking()
                .CountAsync(v => !v.IsDeleted && v.Status != "Maintenance");

            var todayStart = DateTime.Today;
            var todayEnd = todayStart.AddDays(1).AddTicks(-1);

            var todayQuery = db.Tickets
                .AsNoTracking()
                .Where(t => !t.IsDeleted && t.TicketStatus != "Cancelled" && t.PaymentStatus == "Paid" &&
                            t.DepartureTime >= todayStart && t.DepartureTime <= todayEnd);

            decimal todayRevenue = await todayQuery.SumAsync(t => (decimal?)t.Price) ?? 0m;
            int todayTickets = await todayQuery.CountAsync();

            return new DashboardSummaryDto
            {
                TotalRevenue = totalRevenue,
                TotalTickets = totalTickets,
                ActiveRoutes = activeRoutes,
                ActiveVehicles = activeVehicles,
                TodayRevenue = todayRevenue,
                TodayTickets = todayTickets
            };
        }

        public async Task<List<RevenueByDateDto>> GetRevenueByDateAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            using var db = CreateContext();

            var endDate = toDate?.Date ?? DateTime.Today;
            var startDate = fromDate?.Date ?? endDate.AddDays(-6);

            if (startDate > endDate)
            {
                (startDate, endDate) = (endDate, startDate);
            }

            var filterEnd = endDate.AddDays(1).AddTicks(-1);

            var tickets = await db.Tickets
                .AsNoTracking()
                .Where(t => !t.IsDeleted && t.TicketStatus != "Cancelled" && t.PaymentStatus == "Paid" &&
                            t.DepartureTime >= startDate && t.DepartureTime <= filterEnd)
                .Select(t => new { t.DepartureTime, t.Price })
                .ToListAsync();

            var ticketGroups = tickets
                .GroupBy(t => t.DepartureTime.Date)
                .ToDictionary(
                    g => g.Key,
                    g => new { Revenue = g.Sum(x => x.Price), Count = g.Count() }
                );

            var result = new List<RevenueByDateDto>();
            for (var cur = startDate; cur <= endDate; cur = cur.AddDays(1))
            {
                ticketGroups.TryGetValue(cur, out var data);
                result.Add(new RevenueByDateDto
                {
                    Date = cur,
                    DateLabel = cur.ToString("dd/MM"),
                    Revenue = data?.Revenue ?? 0m,
                    TicketCount = data?.Count ?? 0
                });
            }

            return result;
        }

        public async Task<List<RevenueByRouteDto>> GetRevenueByRouteAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            using var db = CreateContext();

            var query = db.Tickets
                .Include(t => t.Route)
                .AsNoTracking()
                .Where(t => !t.IsDeleted && t.TicketStatus != "Cancelled" && t.PaymentStatus == "Paid");

            if (fromDate.HasValue)
            {
                var start = fromDate.Value.Date;
                query = query.Where(t => t.DepartureTime >= start);
            }

            if (toDate.HasValue)
            {
                var end = toDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(t => t.DepartureTime <= end);
            }

            var tickets = await query
                .Select(t => new
                {
                    t.RouteId,
                    RouteCode = t.Route != null ? t.Route.RouteCode : "N/A",
                    RouteName = t.Route != null ? t.Route.RouteName : "Tuyến không xác định",
                    t.Price
                })
                .ToListAsync();

            decimal totalRevenue = tickets.Sum(t => t.Price);

            var grouped = tickets
                .GroupBy(t => new { t.RouteId, t.RouteCode, t.RouteName })
                .Select(g =>
                {
                    decimal rev = g.Sum(x => x.Price);
                    return new RevenueByRouteDto
                    {
                        RouteId = g.Key.RouteId,
                        RouteCode = g.Key.RouteCode,
                        RouteName = g.Key.RouteName,
                        Revenue = rev,
                        TicketCount = g.Count(),
                        Percentage = totalRevenue > 0 ? (double)Math.Round((rev / totalRevenue) * 100m, 1) : 0
                    };
                })
                .OrderByDescending(r => r.Revenue)
                .ToList();

            // Nếu không có vé nào theo tuyến trong khoảng chọn, lấy danh sách tuyến xe đang hoạt động với doanh thu 0
            if (grouped.Count == 0)
            {
                var activeRoutes = await db.Routes
                    .AsNoTracking()
                    .Where(r => !r.IsDeleted && r.Status == "Active")
                    .Take(5)
                    .ToListAsync();

                foreach (var r in activeRoutes)
                {
                    grouped.Add(new RevenueByRouteDto
                    {
                        RouteId = r.Id,
                        RouteCode = r.RouteCode,
                        RouteName = r.RouteName,
                        Revenue = 0,
                        TicketCount = 0,
                        Percentage = 0
                    });
                }
            }

            return grouped;
        }

        public async Task<List<RecentTransactionDto>> GetRecentTransactionsAsync(int limit = 10)
        {
            using var db = CreateContext();

            return await db.Tickets
                .Include(t => t.Route)
                .Include(t => t.Vehicle)
                .AsNoTracking()
                .Where(t => !t.IsDeleted)
                .OrderByDescending(t => t.Id)
                .Take(limit)
                .Select(t => new RecentTransactionDto
                {
                    Id = t.Id,
                    TicketCode = t.TicketCode,
                    CustomerName = t.CustomerName,
                    CustomerPhone = t.CustomerPhone,
                    RouteName = t.Route != null ? t.Route.RouteName : "N/A",
                    VehiclePlate = t.Vehicle != null ? t.Vehicle.LicensePlate : "N/A",
                    SeatNumber = t.SeatNumber,
                    Price = t.Price,
                    DepartureTime = t.DepartureTime,
                    PaymentStatus = t.PaymentStatus,
                    TicketStatus = t.TicketStatus
                })
                .ToListAsync();
        }
    }
}
