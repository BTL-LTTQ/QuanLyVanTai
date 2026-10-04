using System.Text.Json;

namespace Core.Helpers
{
    public class UserSession
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } = "Guest";
        public DateTime LoginTime { get; set; } = DateTime.UtcNow;
    }

    public class AppPreferences
    {
        public bool RememberMe { get; set; } = false;
        public string SavedUsername { get; set; } = string.Empty;
        public string Language { get; set; } = "vi-VN"; // Mặc định là Tiếng Việt
    }

    public static class SessionManager
    {
        private static readonly string AppDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "QuanLyVanTai"
        );

        private static readonly string PreferencesFilePath = Path.Combine(AppDataFolder, "preferences.json");

        public static UserSession? CurrentUser { get; private set; }

        public static bool IsLoggedIn => CurrentUser != null;

        public static void SetCurrentUser(int id, string username, string fullName, string? email, string? phoneNumber, string role)
        {
            CurrentUser = new UserSession
            {
                Id = id,
                Username = username,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                Role = role,
                LoginTime = DateTime.UtcNow
            };
        }

        public static void ClearSession()
        {
            CurrentUser = null;
        }

        public static AppPreferences LoadPreferences()
        {
            try
            {
                if (File.Exists(PreferencesFilePath))
                {
                    string json = File.ReadAllText(PreferencesFilePath);
                    var prefs = JsonSerializer.Deserialize<AppPreferences>(json);
                    if (prefs != null)
                        return prefs;
                }
            }
            catch
            {
                // Fallback nếu có lỗi đọc file
            }

            return new AppPreferences();
        }

        public static void SavePreferences(AppPreferences preferences)
        {
            try
            {
                if (!Directory.Exists(AppDataFolder))
                {
                    Directory.CreateDirectory(AppDataFolder);
                }

                string json = JsonSerializer.Serialize(preferences, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(PreferencesFilePath, json);
            }
            catch
            {
                // Bỏ qua lỗi ghi file để tránh crash
            }
        }

        public static void SaveRememberMe(bool remember, string username)
        {
            var prefs = LoadPreferences();
            prefs.RememberMe = remember;
            prefs.SavedUsername = remember ? username : string.Empty;
            SavePreferences(prefs);
        }

        public static void SaveLanguagePreference(string cultureCode)
        {
            var prefs = LoadPreferences();
            prefs.Language = cultureCode;
            SavePreferences(prefs);
        }
    }
}
