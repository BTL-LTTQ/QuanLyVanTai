using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace QuanLyVanTai.DAL.Repositories
{
    /// <summary>
    /// Triển khai generic — chứa logic CRUD chung, các concrete repository
    /// kế thừa và bổ sung truy vấn nghiệp vụ riêng.
    /// </summary>
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _db;

        protected BaseRepository(AppDbContext db)
        {
            _db = db;
        }

        // ── IRepository<T> ────────────────────────────────────────────────────

        public virtual async Task<List<T>> GetAllAsync()
            => await _db.Set<T>().AsNoTracking().ToListAsync();

        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
            => await _db.Set<T>().AsNoTracking().Where(predicate).ToListAsync();

        public virtual async Task<T?> GetByIdAsync(int id)
            => await _db.Set<T>().FindAsync(id);

        public virtual async Task AddAsync(T entity)
            => await _db.Set<T>().AddAsync(entity);

        public virtual Task UpdateAsync(T entity)
        {
            // EF sẽ tự track nếu entity đã được attach;
            // nếu không, Attach và mark Modified.
            var entry = _db.Entry(entity);
            if (entry.State == EntityState.Detached)
                _db.Set<T>().Attach(entity);
            entry.State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(T entity)
        {
            // Với entity implement ISoftDelete, AppDbContext.ApplyAuditAndSoftDelete()
            // sẽ chặn Deleted → chuyển sang Modified với IsDeleted = true.
            _db.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
            => await _db.SaveChangesAsync();
    }
}
