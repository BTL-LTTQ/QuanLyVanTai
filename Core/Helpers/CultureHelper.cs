using System.Globalization;
using System.Resources;

namespace Core.Helpers
{
    public static class CultureHelper
    {
        public static readonly (string Code, string DisplayName, string Flag)[] SupportedCultures =
        [
            ("vi-VN", "Tiếng Việt", "🇻🇳"),
            ("en-US", "English", "🇺🇸"),
            ("ja-JP", "日本語", "🇯🇵")
        ];

        private static ResourceManager _resourceManager = new ResourceManager("Core.Resources.AppResources", typeof(CultureHelper).Assembly);

        public static event Action? CultureChanged;

        public static CultureInfo CurrentCulture => CultureInfo.CurrentUICulture;

        static CultureHelper()
        {
            try
            {
                // Nạp ngôn ngữ người dùng đã lưu
                var prefs = SessionManager.LoadPreferences();
                string savedLang = prefs.Language;
                if (string.IsNullOrEmpty(savedLang) || !SupportedCultures.Any(c => c.Code == savedLang))
                {
                    savedLang = "vi-VN";
                }

                SetCulture(savedLang, savePreference: false);
            }
            catch
            {
                // Fallback mặc định
            }
        }

        public static void Initialize(ResourceManager? resourceManager = null)
        {
            if (resourceManager != null)
            {
                _resourceManager = resourceManager;
            }
        }

        public static void SetCulture(string cultureCode, bool savePreference = true)
        {
            try
            {
                var culture = new CultureInfo(cultureCode);

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;

                if (savePreference)
                {
                    SessionManager.SaveLanguagePreference(cultureCode);
                }

                CultureChanged?.Invoke();
            }
            catch (Exception)
            {
                // Fallback nếu mã locale không hợp lệ
            }
        }

        public static string GetString(string name)
        {
            if (_resourceManager == null)
                return name;

            try
            {
                string? value = _resourceManager.GetString(name, CultureInfo.CurrentUICulture);
                return value ?? name;
            }
            catch
            {
                return name;
            }
        }

        // ==========================================
        // CÁC HÀM ĐỊNH DẠNG TỰ ĐỘNG CHUẨN LOCALE
        // ==========================================

        /// <summary>
        /// Định dạng tiền tệ tự động theo chuẩn locale đang chọn (VNĐ, USD, JPY...)
        /// </summary>
        public static string FormatCurrency(decimal amount)
        {
            var culture = CultureInfo.CurrentCulture;
            if (culture.Name.StartsWith("vi", StringComparison.OrdinalIgnoreCase))
            {
                return amount.ToString("#,##0 ₫", culture);
            }
            if (culture.Name.StartsWith("ja", StringComparison.OrdinalIgnoreCase))
            {
                return amount.ToString("C0", culture);
            }
            return amount.ToString("C", culture);
        }

        /// <summary>
        /// Định dạng ngày tháng tự động theo chuẩn locale đang chọn
        /// </summary>
        public static string FormatDate(DateTime date, bool includeTime = true)
        {
            var culture = CultureInfo.CurrentCulture;
            if (includeTime)
            {
                return date.ToString("G", culture);
            }
            return date.ToString("d", culture);
        }

        /// <summary>
        /// Định dạng số lượng / khoảng cách (km) tự động phân cách hàng nghìn theo locale
        /// </summary>
        public static string FormatNumber(decimal number, int decimalPlaces = 0)
        {
            var culture = CultureInfo.CurrentCulture;
            string format = decimalPlaces > 0 ? $"N{decimalPlaces}" : "N0";
            return number.ToString(format, culture);
        }
    }
}
