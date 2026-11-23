using System.ComponentModel;
using System.Globalization;
using System.Text.Json;
using Core.Configs;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.Exceptions;
using QuanLyVanTai.BLL.Services;

namespace QuanLyVanTai.UI.Forms
{
    /// <summary>
    /// Dialog hiển thị lịch sử thao tác (Audit Trail) của một tuyến xe.
    /// Dữ liệu đọc từ BLL qua <see cref="RouteManagementService.GetRouteAuditHistoryAsync(int)"/>.
    /// </summary>
    public partial class FrmRouteAuditHistory : Form
    {
        private static readonly CultureInfo ViCulture = CultureInfo.GetCultureInfo("vi-VN");

        private readonly RouteManagementService _routeService = new();
        private readonly int _routeId;

        /// <summary>Thứ tự hiển thị các trường nghiệp vụ trong cột "Nội Dung Thay Đổi".</summary>
        private static readonly string[] FieldOrder =
        [
            "RouteCode", "RouteName", "DistanceKm", "EstimatedHours", "BasePrice", "Status", "IsDeleted"
        ];

        private static readonly Dictionary<string, string> FieldLabels = new()
        {
            ["RouteCode"]      = "Mã tuyến",
            ["RouteName"]      = "Tên tuyến",
            ["DistanceKm"]     = "Cự ly (km)",
            ["EstimatedHours"] = "Thời gian (h)",
            ["BasePrice"]      = "Giá vé (VNĐ)",
            ["Status"]         = "Trạng thái",
            ["IsDeleted"]      = "Đã xóa"
        };

        /// <summary>Các cột audit hệ thống — ẩn khỏi nội dung thay đổi để tránh nhiễu.</summary>
        private static readonly HashSet<string> HiddenFields =
        [
            "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy", "DeletedAt", "DeletedBy"
        ];

        public FrmRouteAuditHistory() : this(0, string.Empty, string.Empty)
        {
        }

        public FrmRouteAuditHistory(int routeId, string routeCode, string routeName)
        {
            InitializeComponent();
            _routeId = routeId;

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            lblRouteInfo.Text = $"Mã tuyến: {routeCode}   |   Lộ trình: {routeName}";
            ThemeConfig.StyleSecondaryButton(btnClose, IconChar.Xmark);

            Shown += async (s, e) => await LoadHistoryAsync();
        }

        private async Task LoadHistoryAsync()
        {
            try
            {
                var history = await _routeService.GetRouteAuditHistoryAsync(_routeId);
                if (IsDisposed || Disposing)
                    return;

                dgvHistory.Rows.Clear();
                foreach (var item in history)
                {
                    int index = dgvHistory.Rows.Add(
                        item.Timestamp.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss"),
                        item.Username,
                        item.Role,
                        item.Action,
                        FormatChanges(item.OldValues, item.NewValues),
                        item.IpAddress);

                    dgvHistory.Rows[index].Cells[colAction.Index].Style.ForeColor = GetActionColor(item.Action);
                }

                lblSummary.Text = history.Count == 0
                    ? "Chưa có bản ghi lịch sử nào cho tuyến xe này."
                    : $"Tổng cộng {history.Count} bản ghi thay đổi (mới nhất ở trên cùng).";
            }
            catch (EntityNotFoundException)
            {
                lblSummary.Text = "Không tìm thấy tuyến xe để tra cứu lịch sử.";
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show($"Lỗi truy vấn lịch sử thao tác: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static Color GetActionColor(string action) => action switch
        {
            "Thêm" => Color.FromArgb(16, 185, 129),
            "Sửa" => Color.FromArgb(59, 130, 246),
            _ when action.StartsWith("Xóa", StringComparison.Ordinal) => Color.FromArgb(239, 68, 68),
            _ => Color.FromArgb(71, 85, 105)
        };

        /// <summary>
        /// Chuyển cặp JSON OldValues/NewValues thành chuỗi dễ đọc, ví dụ:
        /// "Giá vé (VNĐ): 300.000 → 350.000; Trạng thái: Active → Suspended".
        /// </summary>
        private static string FormatChanges(string? oldJson, string? newJson)
        {
            var oldValues = ParseValues(oldJson);
            var newValues = ParseValues(newJson);

            var keys = (newValues.Count > 0 ? newValues.Keys : oldValues.Keys)
                .Where(k => !HiddenFields.Contains(k))
                .OrderBy(k =>
                {
                    int index = Array.IndexOf(FieldOrder, k);
                    return index >= 0 ? index : int.MaxValue;
                })
                .ThenBy(k => k)
                .ToList();

            if (keys.Count == 0)
                return "(Không thay đổi dữ liệu tuyến)";

            return string.Join("; ", keys.Select(k =>
            {
                string label = FieldLabels.GetValueOrDefault(k, k);

                if (newValues.Count == 0)
                    return $"{label}: {oldValues[k]}";

                string newText = newValues.TryGetValue(k, out var nv) ? nv : "…";
                if (oldValues.Count == 0)
                    return $"{label}: {newText}";

                string oldText = oldValues.TryGetValue(k, out var ov) ? ov : "…";
                return $"{label}: {oldText} → {newText}";
            }));
        }

        private static Dictionary<string, string> ParseValues(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return [];

            try
            {
                using var document = JsonDocument.Parse(json);
                if (document.RootElement.ValueKind != JsonValueKind.Object)
                    return [];

                return document.RootElement.EnumerateObject().ToDictionary(
                    property => property.Name,
                    property => property.Value.ValueKind switch
                    {
                        JsonValueKind.Null => "(trống)",
                        JsonValueKind.String => property.Value.GetString() ?? string.Empty,
                        JsonValueKind.Number => FormatNumber(property.Value),
                        JsonValueKind.True => "Có",
                        JsonValueKind.False => "Không",
                        _ => property.Value.GetRawText()
                    });
            }
            catch (JsonException)
            {
                return [];
            }
        }

        /// <summary>Định dạng số theo kiểu Việt Nam (150.000, 14,5) thay vì JSON thô (150000.00).</summary>
        private static string FormatNumber(JsonElement value)
            => value.TryGetDecimal(out decimal number)
                ? number.ToString("#,##0.##", ViCulture)
                : value.GetRawText();
    }
}
