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
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }

    public class TicketService
    {
        private readonly AppDbContext? _injectedContext;

        public TicketService() { }

        public TicketService(AppDbContext context)
        {
            _injectedContext = context;
        }

        private AppDbContext CreateContext() => _injectedContext ?? new AppDbContext();
        private bool ShouldDisposeContext => _injectedContext == null;

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            var db = CreateContext();
            try
            {
                return await db.Tickets
                    .Include(t => t.Route)
                    .Include(t => t.Vehicle)
                    .Include(t => t.Account)
                    .AsNoTracking()
                    .OrderByDescending(t => t.Id)
                    .ToListAsync();
            }
            finally
            {
                if (ShouldDisposeContext)
                {
                    await db.DisposeAsync();
                }
            }
        }

        public async Task<List<Ticket>> SearchAndFilterTicketsAsync(TicketFilterCriteria criteria)
        {
            var db = CreateContext();
            try
            {
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
                    t.SeatNumber.ToLower().Contains(kw) ||
                    (t.Route != null && (t.Route.RouteName.ToLower().Contains(kw) || t.Route.RouteCode.ToLower().Contains(kw))) ||
                    (t.Vehicle != null && t.Vehicle.LicensePlate.ToLower().Contains(kw)));
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

            if (criteria.MinPrice.HasValue)
                query = query.Where(t => t.Price >= criteria.MinPrice.Value);

            if (criteria.MaxPrice.HasValue)
                query = query.Where(t => t.Price <= criteria.MaxPrice.Value);

            return await query.OrderByDescending(t => t.Id).Take(500).ToListAsync();
        }
        finally
        {
            if (ShouldDisposeContext)
            {
                await db.DisposeAsync();
            }
        }
    }

        public async Task<string> GenerateTicketCodeAsync()
        {
            var db = CreateContext();
            try
            {
                string prefix = "TK" + DateTime.Now.ToString("yyyyMMdd");
                int count = await db.Tickets.CountAsync(t => t.TicketCode.StartsWith(prefix));
                return $"{prefix}{(count + 1):D3}";
            }
            finally
            {
                if (ShouldDisposeContext)
                {
                    await db.DisposeAsync();
                }
            }
        }

        public async Task<(bool Success, string Message, Ticket? Ticket)> CreateTicketAsync(Ticket ticket)
        {
            var db = CreateContext();
            try
            {
                var vehicle = await db.Vehicles.FindAsync(ticket.VehicleId);
                if (vehicle == null)
                    return (false, "Không tìm thấy phương tiện!", null);

                // Kiểm tra ghế trùng: cùng xe + cùng số ghế + cùng giờ khởi hành, vé chưa bị hủy
                bool seatTaken = await db.Tickets.AnyAsync(t =>
                    t.VehicleId == ticket.VehicleId &&
                    t.SeatNumber == ticket.SeatNumber &&
                    t.DepartureTime == ticket.DepartureTime &&
                    t.TicketStatus != "Cancelled");

                if (seatTaken)
                    return (false, $"Ghế {ticket.SeatNumber} đã được đặt trên chuyến này! Vui lòng chọn ghế khác.", null);

                ticket.TicketCode = await GenerateTicketCodeAsync();
                db.Tickets.Add(ticket);
                await db.SaveChangesAsync();
                return (true, $"Đặt vé thành công! Mã vé: {ticket.TicketCode}", ticket);
            }
            finally
            {
                if (ShouldDisposeContext)
                {
                    await db.DisposeAsync();
                }
            }
        }

        public async Task<(bool Success, string Message)> UpdateTicketAsync(Ticket ticket)
        {
            var db = CreateContext();
            try
            {
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
            finally
            {
                if (ShouldDisposeContext)
                {
                    await db.DisposeAsync();
                }
            }
        }

        public async Task<(bool Success, string Message)> CancelTicketAsync(int ticketId)
        {
            var db = CreateContext();
            try
            {
                var ticket = await db.Tickets.FindAsync(ticketId);
                if (ticket == null)
                    return (false, "Không tìm thấy vé cần hủy!");

                if (ticket.TicketStatus == "Cancelled")
                    return (false, "Vé đã được hủy trước đó!");

                // Quy định nghiệp vụ: Chỉ cho phép hủy vé trước giờ khởi hành tối thiểu 1 giờ (60 phút)
                if (ticket.DepartureTime <= DateTime.Now)
                    return (false, "Chuyến xe đã khởi hành, không thể hủy vé!");

                if (ticket.DepartureTime - DateTime.Now < TimeSpan.FromHours(1))
                {
                    var remaining = ticket.DepartureTime - DateTime.Now;
                    int remainingMinutes = Math.Max(0, (int)remaining.TotalMinutes);
                    return (false, $"Không thể hủy vé! Chuyến xe sẽ khởi hành sau {remainingMinutes} phút nữa.\nTheo quy định của hệ thống, quý khách chỉ được hủy vé trước giờ xe chạy tối thiểu 60 phút (1 giờ).");
                }

                ticket.TicketStatus = "Cancelled";
                ticket.PaymentStatus = ticket.PaymentStatus == "Paid" ? "Refunded" : ticket.PaymentStatus;

                await db.SaveChangesAsync();
                return (true, $"Đã hủy vé [{ticket.TicketCode}] thành công!");
            }
            finally
            {
                if (ShouldDisposeContext)
                {
                    await db.DisposeAsync();
                }
            }
        }

        public async Task<(bool Success, string Message)> DeleteTicketAsync(int ticketId)
        {
            var db = CreateContext();
            try
            {
                var ticket = await db.Tickets.FindAsync(ticketId);
                if (ticket == null)
                    return (false, "Không tìm thấy vé cần xóa!");

                db.Tickets.Remove(ticket);
                await db.SaveChangesAsync();
                return (true, $"Đã xóa vé [{ticket.TicketCode}] khỏi hệ thống!");
            }
            finally
            {
                if (ShouldDisposeContext)
                {
                    await db.DisposeAsync();
                }
            }
        }

        public async Task<List<Route>> GetActiveRoutesAsync()
        {
            var db = CreateContext();
            try
            {
                return await db.Routes
                    .AsNoTracking()
                    .Where(r => r.Status == "Active")
                    .OrderBy(r => r.RouteName)
                    .ToListAsync();
            }
            finally
            {
                if (ShouldDisposeContext)
                {
                    await db.DisposeAsync();
                }
            }
        }

        public async Task<List<Vehicle>> GetVehiclesByRouteAsync(int routeId)
        {
            var db = CreateContext();
            try
            {
                return await db.Vehicles
                    .AsNoTracking()
                    .Where(v => v.RouteId == routeId && v.Status != "Maintenance")
                    .OrderBy(v => v.LicensePlate)
                    .ToListAsync();
            }
            finally
            {
                if (ShouldDisposeContext)
                {
                    await db.DisposeAsync();
                }
            }
        }
    }
}
