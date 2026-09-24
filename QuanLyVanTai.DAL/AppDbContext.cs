using Microsoft.EntityFrameworkCore;

namespace QuanLyVanTai.DAL
{
    public class AppDbContext : DbContext
    {
        // Hàm khởi tạo nhận cấu hình từ bên ngoài truyền vào (Connection String)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Sau này khi tạo các bảng (Entity), bạn sẽ khai báo các DbSet ở đây. 
        // Ví dụ: public DbSet<Xe> Xes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Nơi cấu hình ràng buộc, khóa chính, khóa ngoại sau này
        }
    }
}