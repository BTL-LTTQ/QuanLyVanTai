using Core.Security;
using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.BLL.Services
{
    public class AuditLogFilterCriteria
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? Username { get; set; }
        public string? Action { get; set; }
        public string? EntityName { get; set; }
        public string? Keyword { get; set; }
    }

    public class AuditLogService
    {
        private readonly AppDbContext _context;

        public AuditLogService(AppDbContext context)
        {
            _context = context;
        }

        public AuditLogService() : this(new AppDbContext())
        {
        }

        /// <summary>
        /// Ghi nhận log thủ công cho các sự kiện nghiệp vụ (Đăng nhập, Đăng xuất, Xuất dữ liệu, Duyệt giao dịch, Truy cập trái phép)
        /// </summary>
        public async Task LogActionAsync(
            string action,
            string entityName,
            string? recordId = null,
            string? details = null,
            string? oldValues = null,
            string? newValues = null)
        {
            try
            {
                var log = new AuditLog
                {
                    UserId = UserSession.CurrentUserId,
                    Username = UserSession.CurrentUsername,
                    Role = UserSession.CurrentRole,
                    Action = action,
                    EntityName = entityName,
                    RecordId = recordId,
                    OldValues = oldValues,
                    NewValues = newValues,
                    IpAddress = UserSession.CurrentIpAddress,
                    Timestamp = DateTime.UtcNow,
                    Details = details ?? $"Tài khoản [{UserSession.CurrentUsername}] thực hiện hành động [{action}] trên [{entityName}]."
                };

                _context.AuditLogs.Add(log);
                await _context.SaveChangesAsync();
            }
            catch
            {
                // Không để lỗi ghi log làm crash luồng chính của ứng dụng
            }
        }

        /// <summary>
        /// Tra cứu và lọc lịch sử Audit Trail theo nhiều tiêu chí
        /// </summary>
        public async Task<List<AuditLog>> SearchLogsAsync(AuditLogFilterCriteria criteria)
        {
            var query = _context.AuditLogs.AsNoTracking().AsQueryable();

            if (criteria.FromDate.HasValue)
            {
                var fromUtc = criteria.FromDate.Value.Date;
                query = query.Where(a => a.Timestamp >= fromUtc);
            }

            if (criteria.ToDate.HasValue)
            {
                var toUtc = criteria.ToDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(a => a.Timestamp <= toUtc);
            }

            if (!string.IsNullOrWhiteSpace(criteria.Username) && criteria.Username != "Tất cả")
            {
                query = query.Where(a => a.Username == criteria.Username);
            }

            if (!string.IsNullOrWhiteSpace(criteria.Action) && criteria.Action != "Tất cả")
            {
                query = query.Where(a => a.Action == criteria.Action);
            }

            if (!string.IsNullOrWhiteSpace(criteria.EntityName) && criteria.EntityName != "Tất cả")
            {
                query = query.Where(a => a.EntityName == criteria.EntityName);
            }

            if (!string.IsNullOrWhiteSpace(criteria.Keyword))
            {
                string kw = criteria.Keyword.Trim().ToLower();
                query = query.Where(a =>
                    a.Username.ToLower().Contains(kw) ||
                    a.Action.ToLower().Contains(kw) ||
                    a.EntityName.ToLower().Contains(kw) ||
                    (a.Details != null && a.Details.ToLower().Contains(kw)) ||
                    (a.RecordId != null && a.RecordId.ToLower().Contains(kw)));
            }

            return await query.OrderByDescending(a => a.Timestamp).Take(500).ToListAsync();
        }
    }
}
