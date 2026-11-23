using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Exceptions;

namespace QuanLyVanTai.BLL.Services
{
    // =========================================================================
    // RouteJsonExportService — Xuất danh sách tuyến xe ra file JSON
    // =========================================================================
    // Trách nhiệm:
    //   1. Lấy dữ liệu nghiệp vụ qua RouteManagementService (đã map sang DTO).
    //   2. Serialize JSON UTF-8 (giữ nguyên tiếng Việt, không escape thành \uXXXX).
    //   3. Ghi file bất đồng bộ (File.WriteAllTextAsync) để UI không bị đơ.
    // Minh họa yêu cầu Import/Export dữ liệu của Chương 5.
    // =========================================================================

    public class RouteJsonExportService
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        private readonly RouteManagementService _routeService;

        public RouteJsonExportService(RouteManagementService? routeService = null)
        {
            _routeService = routeService ?? new RouteManagementService();
        }

        /// <summary>
        /// Xuất danh sách tuyến xe ra file JSON.
        /// </summary>
        /// <param name="filePath">Đường dẫn file đích (.json).</param>
        /// <param name="keyword">Từ khoá lọc — null/rỗng nghĩa là xuất toàn bộ.</param>
        /// <returns>Số bản ghi đã xuất.</returns>
        public async Task<int> ExportRoutesAsync(
            string filePath,
            string? keyword = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

            var routes = await _routeService.GetRouteListAsync(keyword);
            if (routes.Count == 0)
                throw new BusinessRuleViolationException("Route", "Không có tuyến xe nào để xuất JSON.");

            string json = Serialize(routes);

            string? directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            await File.WriteAllTextAsync(
                filePath,
                json,
                new UTF8Encoding(encoderShouldEmitUTF8Identifier: false),
                cancellationToken);

            return routes.Count;
        }

        /// <summary>Serialize danh sách tuyến xe sang JSON (tách riêng để unit test không cần DB).</summary>
        public static string Serialize(IEnumerable<RouteListDto> routes)
        {
            ArgumentNullException.ThrowIfNull(routes);
            return JsonSerializer.Serialize(routes, JsonOptions);
        }
    }
}
