using Core.Helpers;
using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.BLL.Services
{
    public class AccountFilterCriteria
    {
        public string? Keyword { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }
    }

    public class AccountService
    {
        // Không lưu context dưới dạng field — mỗi operation tạo mới để tránh concurrent
        private static AppDbContext CreateContext() => new AppDbContext();

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            using var db = CreateContext();
            return await db.Accounts
                .AsNoTracking()
                .OrderByDescending(a => a.Id)
                .ToListAsync();
        }

        public async Task<List<Account>> SearchAndFilterAccountsAsync(AccountFilterCriteria criteria)
        {
            using var db = CreateContext();
            var query = db.Accounts.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(criteria.Keyword))
            {
                string kw = criteria.Keyword.Trim().ToLower();
                query = query.Where(a =>
                    a.Username.ToLower().Contains(kw) ||
                    a.FullName.ToLower().Contains(kw) ||
                    (a.Email != null && a.Email.ToLower().Contains(kw)) ||
                    (a.PhoneNumber != null && a.PhoneNumber.Contains(kw)));
            }

            if (!string.IsNullOrWhiteSpace(criteria.Role) && criteria.Role != "Tất cả")
            {
                query = query.Where(a => a.Role == criteria.Role);
            }

            if (!string.IsNullOrWhiteSpace(criteria.Status) && criteria.Status != "Tất cả")
            {
                query = query.Where(a => a.Status == criteria.Status);
            }

            return await query.OrderByDescending(a => a.Id).ToListAsync();
        }

        public async Task<(bool Success, string Message, Account? Account)> CreateAccountAsync(
            Account account, string plainPassword)
        {
            using var db = CreateContext();

            bool usernameExists = await db.Accounts
                .AnyAsync(a => a.Username.ToLower() == account.Username.ToLower());
            if (usernameExists)
                return (false, $"Tên đăng nhập '{account.Username}' đã tồn tại!", null);

            if (!string.IsNullOrWhiteSpace(account.Email))
            {
                bool emailExists = await db.Accounts
                    .AnyAsync(a => a.Email != null && a.Email.ToLower() == account.Email.ToLower());
                if (emailExists)
                    return (false, $"Email '{account.Email}' đã được sử dụng!", null);
            }

            string salt = SecurityHelper.GenerateSalt();
            account.PasswordSalt = salt;
            account.PasswordHash = SecurityHelper.HashPassword(plainPassword, salt);
            account.Status = "Active";
            account.FailedLoginAttempts = 0;

            db.Accounts.Add(account);
            await db.SaveChangesAsync();
            return (true, $"Thêm tài khoản [{account.Username}] thành công!", account);
        }

        public async Task<(bool Success, string Message)> UpdateAccountAsync(Account account)
        {
            using var db = CreateContext();

            var existing = await db.Accounts.FindAsync(account.Id);
            if (existing == null)
                return (false, "Không tìm thấy tài khoản cần cập nhật!");

            bool usernameConflict = await db.Accounts
                .AnyAsync(a => a.Username.ToLower() == account.Username.ToLower() && a.Id != account.Id);
            if (usernameConflict)
                return (false, $"Tên đăng nhập '{account.Username}' đã tồn tại!");

            existing.Username = account.Username;
            existing.FullName = account.FullName;
            existing.Email = account.Email;
            existing.PhoneNumber = account.PhoneNumber;
            existing.Role = account.Role;
            existing.Status = account.Status;

            await db.SaveChangesAsync();
            return (true, $"Cập nhật tài khoản [{account.Username}] thành công!");
        }

        public async Task<(bool Success, string Message)> ResetPasswordAsync(int accountId, string newPassword)
        {
            using var db = CreateContext();

            var account = await db.Accounts.FindAsync(accountId);
            if (account == null)
                return (false, "Không tìm thấy tài khoản!");

            string salt = SecurityHelper.GenerateSalt();
            account.PasswordSalt = salt;
            account.PasswordHash = SecurityHelper.HashPassword(newPassword, salt);
            account.FailedLoginAttempts = 0;
            account.LockoutEndTime = null;
            account.Status = "Active";

            await db.SaveChangesAsync();
            return (true, "Đặt lại mật khẩu thành công!");
        }

        public async Task<(bool Success, string Message)> DeleteAccountAsync(int accountId)
        {
            using var db = CreateContext();

            var account = await db.Accounts
                .Include(a => a.Tickets)
                .FirstOrDefaultAsync(a => a.Id == accountId);

            if (account == null)
                return (false, "Không tìm thấy tài khoản cần xóa!");

            foreach (var ticket in account.Tickets)
                ticket.AccountId = null;

            db.Accounts.Remove(account);
            await db.SaveChangesAsync();
            return (true, $"Đã xóa tài khoản [{account.Username}] thành công!");
        }
    }
}
