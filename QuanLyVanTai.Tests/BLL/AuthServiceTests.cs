using Core.Helpers;
using Core.Security;
using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.Tests.Helpers;

namespace QuanLyVanTai.Tests.BLL
{
    public class AuthServiceTests
    {
        [Theory]
        [InlineData("", "Password123")]
        [InlineData("admin", "")]
        [InlineData("   ", "   ")]
        public async Task LoginAsync_EmptyInput_ReturnsFailure(string username, string password)
        {
            using var db = TestDbContextFactory.Create();
            var service = new AuthService(db);

            var result = await service.LoginAsync(new LoginRequestDto { Username = username, Password = password });

            Assert.False(result.Success);
            Assert.Contains("đầy đủ", result.Message);
        }

        [Fact]
        public async Task LoginAsync_NonExistentUser_ReturnsFailure()
        {
            using var db = TestDbContextFactory.Create();
            var service = new AuthService(db);

            var result = await service.LoginAsync(new LoginRequestDto
            {
                Username = "non_existent_user",
                Password = "Password123"
            });

            Assert.False(result.Success);
            Assert.Contains("không chính xác", result.Message);
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsSuccess()
        {
            using var db = TestDbContextFactory.Create();
            string salt = SecurityHelper.GenerateSalt();
            string hash = SecurityHelper.HashPassword("Admin@123", salt);

            db.Accounts.Add(new Account
            {
                Username = "admin_test",
                FullName = "Admin Test User",
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = SystemRoles.Admin,
                Status = "Active",
                FailedLoginAttempts = 2
            });
            await db.SaveChangesAsync();

            var service = new AuthService(db);
            var result = await service.LoginAsync(new LoginRequestDto
            {
                Username = "admin_test",
                Password = "Admin@123"
            });

            Assert.True(result.Success);
            Assert.NotNull(result.Account);
            Assert.Equal("admin_test", result.Account.Username);
            Assert.Equal(SystemRoles.Admin, result.Account.Role);

            // Failed login attempts should be reset to 0
            var account = await db.Accounts.FirstAsync(a => a.Username == "admin_test");
            Assert.Equal(0, account.FailedLoginAttempts);
        }

        [Fact]
        public async Task LoginAsync_WrongPassword_IncrementsFailedAttempts()
        {
            using var db = TestDbContextFactory.Create();
            string salt = SecurityHelper.GenerateSalt();
            string hash = SecurityHelper.HashPassword("Admin@123", salt);

            db.Accounts.Add(new Account
            {
                Username = "staff_test",
                FullName = "Staff Test User",
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = SystemRoles.TicketStaff,
                Status = "Active",
                FailedLoginAttempts = 0
            });
            await db.SaveChangesAsync();

            var service = new AuthService(db);
            var result = await service.LoginAsync(new LoginRequestDto
            {
                Username = "staff_test",
                Password = "WrongPassword!"
            });

            Assert.False(result.Success);
            Assert.Equal(4, result.RemainingAttempts);

            var account = await db.Accounts.FirstAsync(a => a.Username == "staff_test");
            Assert.Equal(1, account.FailedLoginAttempts);
        }

        [Fact]
        public async Task LoginAsync_FiveFailedAttempts_LocksAccount()
        {
            using var db = TestDbContextFactory.Create();
            string salt = SecurityHelper.GenerateSalt();
            string hash = SecurityHelper.HashPassword("Admin@123", salt);

            db.Accounts.Add(new Account
            {
                Username = "user_to_lock",
                FullName = "User To Lock",
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = SystemRoles.Customer,
                Status = "Active",
                FailedLoginAttempts = 4 // Đã sai 4 lần
            });
            await db.SaveChangesAsync();

            var service = new AuthService(db);
            // Lần thứ 5 sai -> Khóa tài khoản
            var result = await service.LoginAsync(new LoginRequestDto
            {
                Username = "user_to_lock",
                Password = "WrongPasswordAgain!"
            });

            Assert.False(result.Success);
            Assert.True(result.IsLocked);
            Assert.Equal(5, result.LockoutRemainingMinutes);

            var account = await db.Accounts.FirstAsync(a => a.Username == "user_to_lock");
            Assert.Equal("Locked", account.Status);
            Assert.NotNull(account.LockoutEndTime);
            Assert.True(account.LockoutEndTime > DateTime.UtcNow);
        }

        [Fact]
        public async Task RegisterAsync_PasswordMismatch_ReturnsFailure()
        {
            using var db = TestDbContextFactory.Create();
            var service = new AuthService(db);

            var req = new RegisterRequestDto
            {
                Username = "new_user",
                Password = "Password123@",
                ConfirmPassword = "DifferentPassword123@"
            };

            var result = await service.RegisterAsync(req);

            Assert.False(result.Success);
            Assert.Contains("không trùng khớp", result.Message);
        }

        [Fact]
        public async Task RegisterAsync_DuplicateUsername_ReturnsFailure()
        {
            using var db = TestDbContextFactory.Create();
            db.Accounts.Add(new Account
            {
                Username = "existing_user",
                FullName = "Existing User",
                PasswordHash = "dummy",
                PasswordSalt = "dummy",
                Role = SystemRoles.Customer,
                Status = "Active"
            });
            await db.SaveChangesAsync();

            var service = new AuthService(db);
            var req = new RegisterRequestDto
            {
                Username = "existing_user",
                Password = "Password123@",
                ConfirmPassword = "Password123@"
            };

            var result = await service.RegisterAsync(req);

            Assert.False(result.Success);
            Assert.Contains("đã được sử dụng", result.Message);
        }
    }
}
