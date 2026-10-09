using System.Linq.Expressions;

namespace QuanLyVanTai.DAL.Repositories
{
    /// <summary>
    /// Generic repository contract — định nghĩa các thao tác CRUD bất đồng bộ
    /// dùng chung cho mọi entity trong hệ thống.
    /// </summary>
    /// <typeparam name="T">Entity type phải là class.</typeparam>
    public interface IRepository<T> where T : class
    {
        // ── Truy vấn ──────────────────────────────────────────────────────────

        /// <summary>Lấy toàn bộ danh sách (có áp dụng Global Query Filter).</summary>
        Task<List<T>> GetAllAsync();

        /// <summary>Lấy theo điều kiện lọc tuỳ ý.</summary>
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>Lấy 1 bản ghi theo khoá chính.</summary>
        Task<T?> GetByIdAsync(int id);

        // ── Thêm / Sửa / Xóa ─────────────────────────────────────────────────

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);

        /// <summary>
        /// Xóa an toàn: nếu entity implement ISoftDelete thì EF interceptor trong
        /// AppDbContext sẽ tự chuyển Remove() → IsDeleted = true.
        /// Nếu cần hard-delete, dùng phương thức riêng ở concrete repository.
        /// </summary>
        Task DeleteAsync(T entity);

        Task<int> SaveChangesAsync();
    }
}
