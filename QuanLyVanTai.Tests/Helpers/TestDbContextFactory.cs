using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL;

namespace QuanLyVanTai.Tests.Helpers
{
    /// <summary>
    /// Tạo AppDbContext dùng InMemory provider — mỗi test nhận DB sạch riêng biệt
    /// thông qua unique database name, tránh cross-test pollution.
    /// </summary>
    public static class TestDbContextFactory
    {
        /// <summary>
        /// Tạo DbContext InMemory với tên DB duy nhất (dùng Guid).
        /// Không cần SQL Server thật — chạy được offline, nhanh, isolated.
        /// </summary>
        public static AppDbContext Create(string? dbName = null)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName ?? Guid.NewGuid().ToString())
                // Disable transaction support warning (InMemory không hỗ trợ real transactions)
                .ConfigureWarnings(w => w.Ignore(
                    Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            return new AppDbContext(options);
        }
    }
}
