using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.BLL.Services
{
    public class TicketFilterCriteria
    {
        public string? Keyword { get; set; }
        public string? RouteId { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TicketStatus { get; set; }
        public DateTime? DepartureFrom { get; set; }
        public DateTime? DepartureTo { get; set; }
    }

    public class TicketService
    {
        // Mỗi operation dùng context riêng → tránh concurrent DbContext
        private static AppDbContext CreateContext() => new AppDbContext();

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            using var db = CreateContext();
            return await db.Tickets
                .Include(t => t.Route)
                .Include(t => t.Vehicle)
                .Include(t => t.Account)
                .AsNoTracking()
                .OrderByDescending(t => t.Id)
                .ToListAsync();
        }

        public async Task<List<Ticket>> SearchAndFilterTicketsAsync(TicketFilterCriteria criteria)
        {
            using var db = CreateContext();
            var query = db.Tickets
                .Include(t => t.Route)
                .Include(t => t.Vehicle)
                .Include(t => t.Account)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(criteria.Keyword))
            {
                string kw = criteria.Keyword.Trim().ToLower();
                query = query.Where(t =>
                    t.TicketCode.ToLower().Contains(kw) ||
                    t.CustomerName.ToLower().Contains(kw) ||
                    t.CustomerPhone.Contains(kw) ||
                    t.SeatNumber.ToLower().Contains(kw));
            }

            if (!string.IsNullOrWhiteSpace(criteria.RouteId) && criteria.RouteId != "Tất cả"
                && int.TryParse(criteria.RouteId, out int routeId))
            {
                query = query.Where(t => t.RouteId == routeId);
            }

            if (!string.IsNullOrWhiteSpace(criteria.PaymentStatus) && criteria.PaymentStatus != "Tất cả")
                query = query.Where(t => t.PaymentStatus == criteria.PaymentStatus);

            if (!string.IsNullOrWhiteSpace(criteria.TicketStatus) && criteria.TicketStatus != "Tất cả")
                query = query.Where(t => t.TicketStatus == criteria.TicketStatus);

            if (criteria.DepartureFrom.HasValue)
                query = query.Where(t => t.DepartureTime >= criteria.DepartureFrom.Value);

            if (criteria.DepartureTo.HasValue)
                query = query.Where(t => t.DepartureTime <= criteria.DepartureTo.Value.AddDays(1).AddTicks(-1));

            return await query.OrderByDescending(t => t.Id).Take(500).ToListAsync();
        }

        public async Task<string> GenerateTicketCodeAsync()
        {
            using var db = CreateContext();
            string prefix = "TK" + DateTime.Now.ToString("yyyyMMdd");
            int count = await db.Tickets.CountAsync(t => t.TicketCode.StartsWith(prefix));
            return $"{prefix}{(count + 1):D3}";
        }

        public async Task<(bool Success, string Message, Ticket? Ticket)> CreateTicketAsync(Ticket ticket)
        {
            using var db = CreateContext();
            var vehicle = await db.Vehicles.FindAsync(ticket.VehicleId);
            if (vehicle == null)
                return (false, "Không tìm thấy phương tiện!", null);

            ticket.TicketCode = await GenerateTicketCodeAsync();
            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();
            return (true, $"Đặt vé thành công! Mã vé: {ticket.TicketCode}", ticket);
        }

        public async Task<(bool Success, string Message)> UpdateTicketAsync(Ticket ticket)
        {
            using var db = CreateContext();
            var existing = await db.Tickets.FindAsync(ticket.Id);
            if (existing == null)
                return (false, "Không tìm thấy vé cần cập nhật!");

            existing.CustomerName = ticket.CustomerName;
            existing.CustomerPhone = ticket.CustomerPhone;
            existing.SeatNumber = ticket.SeatNumber;
            existing.Price = ticket.Price;
            existing.DepartureTime = ticket.DepartureTime;
            existing.PaymentStatus = ticket.PaymentStatus;
            existing.TicketStatus = ticket.TicketStatus;
            existing.RouteId = ticket.RouteId;
            existing.VehicleId = ticket.VehicleId;

            await db.SaveChangesAsync();
            return (true, "Cập nhật vé thành công!");
        }

        public async Task<(bool Success, string Message)> CancelTicketAsync(int ticketId)
        {
            using var db = CreateContext();
            var ticket = await db.Tickets.FindAsync(ticketId);
            if (ticket == null)
                return (false, "Không tìm thấy vé cần hủy!");

            if (ticket.TicketStatus == "Cancelled")
                return (false, "Vé đã được hủy trước đó!");

            ticket.TicketStatus = "Cancelled";
            ticket.PaymentStatus = ticket.PaymentStatus == "Paid" ? "Refunded" : ticket.PaymentStatus;

            await db.SaveChangesAsync();
            return (true, $"Đã hủy vé [{ticket.TicketCode}] thành công!");
        }

        public async Task<(bool Success, string Message)> DeleteTicketAsync(int ticketId)
        {
            using var db = CreateContext();
            var ticket = await db.Tickets.FindAsync(ticketId);
            if (ticket == null)
                return (false, "Không tìm thấy vé cần xóa!");

            db.Tickets.Remove(ticket);
            await db.SaveChangesAsync();
            return (true, $"Đã xóa vé [{ticket.TicketCode}] khỏi hệ thống!");
        }

        public async Task<List<Route>> GetActiveRoutesAsync()
        {
            using var db = CreateContext();
            return await db.Routes
                .AsNoTracking()
                .Where(r => r.Status == "Active")
                .OrderBy(r => r.RouteName)
                .ToListAsync();
        }

        public async Task<List<Vehicle>> GetVehiclesByRouteAsync(int routeId)
        {
            using var db = CreateContext();
            return await db.Vehicles
                .AsNoTracking()
                .Where(v => v.RouteId == routeId && v.Status != "Maintenance")
                .OrderBy(v => v.LicensePlate)
                .ToListAsync();
        }
    }
}
