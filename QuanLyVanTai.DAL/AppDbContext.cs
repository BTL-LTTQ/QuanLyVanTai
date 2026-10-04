using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        // Hàm khởi tạo nhận cấu hình từ bên ngoài truyền vào (Connection String từ UI hoặc DI)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Khai báo các DbSet đại diện cho các bảng trong CSDL
        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
        public virtual DbSet<Station> Stations { get; set; } = null!;
        public virtual DbSet<Route> Routes { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Fallback connection string hỗ trợ chạy Migration từ PMC hoặc CLI
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=QuanLyVanTaiDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // 1. Cấu hình bảng Account
            // ==========================================
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique().HasFilter("[Email] IS NOT NULL");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

                // Seed data tài khoản Admin mặc định để test đăng nhập
                entity.HasData(new Account
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = "admin123",
                    FullName = "Quản trị hệ thống",
                    Email = "admin@vantai.com",
                    PhoneNumber = "0988888888",
                    Role = "Admin",
                    Status = "Active",
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                });
            });

            // ==========================================
            // 2. Cấu hình bảng Vehicle
            // ==========================================
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.LicensePlate).IsUnique();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            });

            // ==========================================
            // 3. Cấu hình bảng Station
            // ==========================================
            modelBuilder.Entity<Station>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.StationCode).IsUnique();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            });

            // ==========================================
            // 4. Cấu hình bảng Route & Quan hệ N-N với Station
            // ==========================================
            modelBuilder.Entity<Route>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RouteCode).IsUnique();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

                // Thiết lập quan hệ N - N giữa Tuyến xe và Trạm dừng qua bảng trung gian RouteStations
                entity.HasMany(r => r.Stations)
                      .WithMany(s => s.Routes)
                      .UsingEntity(j => j.ToTable("RouteStations"));
            });

            // ==========================================
            // 5. Cấu hình bảng Ticket & Các quan hệ 1-N
            // ==========================================
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.TicketCode).IsUnique();
                entity.HasIndex(e => e.CustomerPhone);
                entity.HasIndex(e => e.DepartureTime);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

                // 1 Tuyến xe có nhiều Vé (Route 1 - N Ticket)
                entity.HasOne(t => t.Route)
                      .WithMany(r => r.Tickets)
                      .HasForeignKey(t => t.RouteId)
                      .OnDelete(DeleteBehavior.Restrict);

                // 1 Xe phục vụ nhiều Vé (Vehicle 1 - N Ticket)
                entity.HasOne(t => t.Vehicle)
                      .WithMany(v => v.Tickets)
                      .HasForeignKey(t => t.VehicleId)
                      .OnDelete(DeleteBehavior.Restrict);

                // 1 Nhân sự lập nhiều Vé (Account 1 - N Ticket)
                entity.HasOne(t => t.Account)
                      .WithMany(a => a.Tickets)
                      .HasForeignKey(t => t.AccountId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}