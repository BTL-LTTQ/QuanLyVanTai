using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.DAL.Repositories
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        /// <summary>Lấy chuyến kèm Route, Vehicle và danh sách phân công tài xế.</summary>
        Task<Schedule?> GetByIdWithDetailsAsync(int id);

        /// <summary>Lấy danh sách chuyến có lọc theo khoảng ngày và trạng thái.</summary>
        Task<List<Schedule>> GetSchedulesAsync(
            DateTime? from = null,
            DateTime? to = null,
            string? status = null,
            int? routeId = null,
            int? vehicleId = null);

        /// <summary>
        /// Lấy các chuyến của xe trong khoảng [windowStart, windowEnd) có status ACTIVE
        /// (Scheduled | InProgress) — dùng để detect vehicle conflict.
        /// Loại trừ <paramref name="excludeScheduleId"/> khi update.
        /// </summary>
        Task<List<Schedule>> GetActiveSchedulesForVehicleAsync(
            int vehicleId,
            DateTime windowStart,
            DateTime windowEnd,
            int? excludeScheduleId = null);

        /// <summary>
        /// Lấy các chuyến mà tài xế được phân công trong khoảng thời gian,
        /// bao gồm thông tin Route để tạo message lỗi chi tiết.
        /// Loại trừ <paramref name="excludeScheduleId"/> khi update.
        /// </summary>
        Task<List<Schedule>> GetActiveSchedulesForDriverAsync(
            int driverId,
            DateTime windowStart,
            DateTime windowEnd,
            int? excludeScheduleId = null);

        Task<bool> IsScheduleCodeExistsAsync(string code, int? excludeId = null);
    }
}
