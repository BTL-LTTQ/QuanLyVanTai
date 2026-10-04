using System.Text.Json;
using Core.Interfaces;
using Core.Security;
using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Khai báo các DbSet
        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
        public virtual DbSet<Station> Stations { get; set; } = null!;
        public virtual DbSet<Route> Routes { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<AuditLog> AuditLogs { get; set; } = null!;
        public virtual DbSet<RolePermission> RolePermissions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=QuanLyVanTaiDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // 1. Cấu hình bảng Account & Global Query Filter
            // ==========================================
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique().HasFilter("[Email] IS NOT NULL");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.HasQueryFilter(e => !e.IsDeleted);

                // Seed data tài khoản Admin và Nhân viên mặc định
                entity.HasData(
                    new Account
                    {
                        Id = 1,
                        Username = "admin",
                        PasswordHash = "admin123",
                        FullName = "Quản trị hệ thống",
                        Email = "admin@vantai.com",
                        PhoneNumber = "0988888888",
                        Role = SystemRoles.Admin,
                        Status = "Active",
                        CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                        CreatedBy = "System"
                    },
                    new Account
                    {
                        Id = 2,
                        Username = "quanly",
                        PasswordHash = "quanly123",
                        FullName = "Nguyễn Văn Quản Lý",
                        Email = "quanly@vantai.com",
                        PhoneNumber = "0977777777",
                        Role = SystemRoles.Manager,
                        Status = "Active",
                        CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                        CreatedBy = "System"
                    },
                    new Account
                    {
                        Id = 3,
                        Username = "nhanvien",
                        PasswordHash = "nhanvien123",
                        FullName = "Trần Thị Bán Vé",
                        Email = "banve@vantai.com",
                        PhoneNumber = "0966666666",
                        Role = SystemRoles.TicketStaff,
                        Status = "Active",
                        CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                        CreatedBy = "System"
                    }
                );
            });

            // ==========================================
            // 2. Cấu hình bảng Vehicle & Quan hệ với Route
            // ==========================================
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.LicensePlate).IsUnique();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.HasQueryFilter(e => !e.IsDeleted);

                entity.HasOne(v => v.Route)
                      .WithMany(r => r.Vehicles)
                      .HasForeignKey(v => v.RouteId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // ==========================================
            // 3. Cấu hình bảng Station
            // ==========================================
            modelBuilder.Entity<Station>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.StationCode).IsUnique();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // ==========================================
            // 4. Cấu hình bảng Route & Quan hệ N-N với Station
            // ==========================================
            modelBuilder.Entity<Route>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RouteCode).IsUnique();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.HasQueryFilter(e => !e.IsDeleted);

                // Quan hệ N - N giữa Tuyến xe và Trạm dừng
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
                entity.HasQueryFilter(e => !e.IsDeleted);

                entity.HasOne(t => t.Route)
                      .WithMany(r => r.Tickets)
                      .HasForeignKey(t => t.RouteId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Vehicle)
                      .WithMany(v => v.Tickets)
                      .HasForeignKey(t => t.VehicleId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Account)
                      .WithMany(a => a.Tickets)
                      .HasForeignKey(t => t.AccountId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // ==========================================
            // 6. Cấu hình bảng RolePermission & Seed Data
            // ==========================================
            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.RoleName, e.MenuCode }).IsUnique();

                // Seed permissions
                var seedData = new List<RolePermission>();
                int permId = 1;

                // 1. Quản trị viên (Toàn quyền mọi chức năng)
                foreach (var menu in SystemMenus.AllMenus)
                {
                    seedData.Add(new RolePermission
                    {
                        Id = permId++,
                        RoleName = SystemRoles.Admin,
                        MenuCode = menu.Code,
                        MenuName = menu.Name,
                        CanView = true,
                        CanAdd = true,
                        CanEdit = true,
                        CanDelete = true,
                        CanExport = true,
                        UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    });
                }

                // 2. Quản lý
                foreach (var menu in SystemMenus.AllMenus)
                {
                    bool isSystemMenu = menu.Code == SystemMenus.Permission || menu.Code == SystemMenus.AuditLog;
                    seedData.Add(new RolePermission
                    {
                        Id = permId++,
                        RoleName = SystemRoles.Manager,
                        MenuCode = menu.Code,
                        MenuName = menu.Name,
                        CanView = true,
                        CanAdd = !isSystemMenu,
                        CanEdit = !isSystemMenu,
                        CanDelete = false, // Quản lý không được xóa dữ liệu
                        CanExport = true,
                        UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    });
                }

                // 3. Nhân viên bán vé
                foreach (var menu in SystemMenus.AllMenus)
                {
                    bool isTicketRelated = menu.Code == SystemMenus.Ticket || menu.Code == SystemMenus.Route || menu.Code == SystemMenus.Promotion;
                    seedData.Add(new RolePermission
                    {
                        Id = permId++,
                        RoleName = SystemRoles.TicketStaff,
                        MenuCode = menu.Code,
                        MenuName = menu.Name,
                        CanView = isTicketRelated,
                        CanAdd = menu.Code == SystemMenus.Ticket,
                        CanEdit = false,
                        CanDelete = false,
                        CanExport = false,
                        UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    });
                }

                entity.HasData(seedData);
            });

            // ==========================================
            // 7. Cấu hình bảng AuditLog
            // ==========================================
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Timestamp);
                entity.HasIndex(e => e.Username);
                entity.HasIndex(e => e.EntityName);
            });
        }

        // ========================================================
        // Tự động xử lý IAuditable, ISoftDelete và Ghi Audit Trail
        // ========================================================
        public override int SaveChanges()
        {
            ApplyAuditAndSoftDelete();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditAndSoftDelete();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditAndSoftDelete()
        {
            string currentUser = UserSession.CurrentUsername;
            string currentRole = UserSession.CurrentRole;
            string currentIp = UserSession.CurrentIpAddress;
            DateTime now = DateTime.UtcNow;

            var entries = ChangeTracker.Entries().ToList();
            var auditEntries = new List<AuditLog>();

            foreach (var entry in entries)
            {
                // Bỏ qua bảng AuditLog để tránh lặp vô tận
                if (entry.Entity is AuditLog)
                    continue;

                // 1. Xử lý IAuditable
                if (entry.Entity is IAuditable auditable)
                {
                    if (entry.State == EntityState.Added)
                    {
                        auditable.CreatedAt = now;
                        auditable.CreatedBy = currentUser;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        auditable.UpdatedAt = now;
                        auditable.UpdatedBy = currentUser;
                    }
                }

                // 2. Xử lý ISoftDelete khi xóa
                if (entry.Entity is ISoftDelete softDelete && entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    softDelete.IsDeleted = true;
                    softDelete.DeletedAt = now;
                    softDelete.DeletedBy = currentUser;
                }

                // 3. Tự động ghi nhận lịch sử chỉnh sửa dữ liệu vào AuditLog
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                {
                    string entityName = entry.Entity.GetType().Name;
                    string action = entry.State switch
                    {
                        EntityState.Added => "Thêm",
                        EntityState.Modified => (entry.Entity is ISoftDelete sd && sd.IsDeleted) ? "Xóa mềm" : "Sửa",
                        EntityState.Deleted => "Xóa cứng",
                        _ => entry.State.ToString()
                    };

                    string? recordId = null;
                    var keyProperty = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey());
                    if (keyProperty != null)
                    {
                        recordId = keyProperty.CurrentValue?.ToString();
                    }

                    var oldDict = new Dictionary<string, object?>();
                    var newDict = new Dictionary<string, object?>();

                    foreach (var prop in entry.Properties)
                    {
                        if (prop.Metadata.IsPrimaryKey() || prop.Metadata.Name == "PasswordHash")
                            continue;

                        if (entry.State == EntityState.Modified && prop.IsModified)
                        {
                            oldDict[prop.Metadata.Name] = prop.OriginalValue;
                            newDict[prop.Metadata.Name] = prop.CurrentValue;
                        }
                        else if (entry.State == EntityState.Added)
                        {
                            newDict[prop.Metadata.Name] = prop.CurrentValue;
                        }
                        else if (entry.State == EntityState.Deleted)
                        {
                            oldDict[prop.Metadata.Name] = prop.OriginalValue;
                        }
                    }

                    if (oldDict.Count > 0 || newDict.Count > 0)
                    {
                        auditEntries.Add(new AuditLog
                        {
                            UserId = UserSession.CurrentUserId,
                            Username = currentUser,
                            Role = currentRole,
                            Action = action,
                            EntityName = entityName,
                            RecordId = recordId,
                            OldValues = oldDict.Count > 0 ? JsonSerializer.Serialize(oldDict) : null,
                            NewValues = newDict.Count > 0 ? JsonSerializer.Serialize(newDict) : null,
                            IpAddress = currentIp,
                            Timestamp = now,
                            Details = $"Người dùng [{currentUser}] thực hiện [{action}] trên [{entityName}] ID #{recordId ?? "Mới"}"
                        });
                    }
                }
            }

            if (auditEntries.Count > 0)
            {
                AuditLogs.AddRange(auditEntries);
            }
        }
    }
}