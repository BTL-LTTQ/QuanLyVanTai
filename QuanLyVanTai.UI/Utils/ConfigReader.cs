using Microsoft.Extensions.Configuration;
using QuanLyVanTai.BLL.DTOs;

namespace QuanLyVanTai.UI.Utils
{
    /// <summary>
    /// Tiện ích đọc cấu hình ứng dụng từ appsettings.json.
    /// Caching cấu hình để tránh đọc file nhiều lần.
    /// </summary>
    public static class ConfigReader
    {
        private static IConfiguration? _config;
        private static readonly object _lock = new object();

        public static IConfiguration GetConfiguration()
        {
            if (_config != null) return _config;
            lock (_lock)
            {
                if (_config != null) return _config;
                _config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                    .Build();
                return _config;
            }
        }

        /// <summary>
        /// Đọc cấu hình AI Chatbot từ appsettings.json.
        /// Tự động chọn provider và API key theo cấu hình.
        /// </summary>
        public static AiChatbotConfig GetAiChatbotConfig()
        {
            var cfg = GetConfiguration();
            var section = cfg.GetSection("AiChatbot");

            string provider = section["Provider"] ?? "Gemini";
            string apiKey;
            string model;

            if (provider.Equals("OpenAI", StringComparison.OrdinalIgnoreCase))
            {
                apiKey = section["OpenAI:ApiKey"] ?? section["ApiKey"] ?? string.Empty;
                model = section["OpenAI:Model"] ?? "gpt-4o-mini";
            }
            else
            {
                apiKey = section["ApiKey"] ?? string.Empty;
                model = section["Model"] ?? "gemini-1.5-flash";
            }

            if (double.TryParse(section["Temperature"], out double temp) == false)
                temp = 0.7;

            if (int.TryParse(section["MaxTokens"], out int maxTokens) == false)
                maxTokens = 1024;

            return new AiChatbotConfig
            {
                Provider = provider,
                ApiKey = apiKey,
                Model = model,
                Temperature = temp,
                MaxTokens = maxTokens
            };
        }
    }
}
