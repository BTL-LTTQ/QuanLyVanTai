using System.Security.Cryptography;
using System.Text;

namespace Core.Helpers
{
    /// <summary>
    /// Cung cấp các phương thức mã hóa mật khẩu Hash + Salt an toàn cao (PBKDF2 SHA-256)
    /// và tạo mã bảo mật xác thực OTP.
    /// </summary>
    public static class SecurityHelper
    {
        private const int SaltSize = 16; // 128 bit
        private const int KeySize = 32;  // 256 bit
        private const int Iterations = 100_000; // Khuyến nghị của NIST và OWASP

        /// <summary>
        /// Sinh chuỗi Salt ngẫu nhiên bảo mật bằng RandomNumberGenerator
        /// </summary>
        public static string GenerateSalt(int size = SaltSize)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(size);
            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Băm mật khẩu kết hợp với Salt sử dụng thuật toán PBKDF2 với HMAC-SHA256
        /// </summary>
        public static string HashPassword(string password, string salt)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrEmpty(salt))
                throw new ArgumentNullException(nameof(salt));

            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password: Encoding.UTF8.GetBytes(password),
                salt: saltBytes,
                iterations: Iterations,
                hashAlgorithm: HashAlgorithmName.SHA256,
                outputLength: KeySize
            );

            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Xác thực mật khẩu với chuỗi Hash và Salt đã lưu trong cơ sở dữ liệu.
        /// Hỗ trợ cả cơ chế kiểm tra tương thích ngược với tài khoản seed mặc định.
        /// </summary>
        public static bool VerifyPassword(string inputPassword, string storedHash, string? storedSalt, out bool needsUpgrade)
        {
            needsUpgrade = false;

            if (string.IsNullOrEmpty(inputPassword) || string.IsNullOrEmpty(storedHash))
                return false;

            // Nếu tài khoản có Salt -> xác thực bằng PBKDF2
            if (!string.IsNullOrEmpty(storedSalt))
            {
                try
                {
                    string computedHash = HashPassword(inputPassword, storedSalt);
                    byte[] computedBytes = Convert.FromBase64String(computedHash);
                    byte[] storedBytes = Convert.FromBase64String(storedHash);

                    return CryptographicOperations.FixedTimeEquals(computedBytes, storedBytes);
                }
                catch
                {
                    return false;
                }
            }

            // Fallback: Nếu tài khoản chưa có Salt (như tài khoản seed "admin123"), kiểm tra plain-text
            if (inputPassword == storedHash)
            {
                needsUpgrade = true; // Báo hiệu cần nâng cấp mật khẩu sang Hash + Salt
                return true;
            }

            return false;
        }

        /// <summary>
        /// Băm câu trả lời bí mật (sử dụng salt độc lập gắn với username để không bị mất hiệu lực khi đổi mật khẩu)
        /// </summary>
        public static string HashSecurityAnswer(string answer, string usernameOrKey)
        {
            string normalized = answer.Trim().ToLowerInvariant();
            byte[] saltBytes = SHA256.HashData(Encoding.UTF8.GetBytes(usernameOrKey.Trim().ToLowerInvariant() + "_security_salt_v1"));
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password: Encoding.UTF8.GetBytes(normalized),
                salt: saltBytes,
                iterations: 50_000,
                hashAlgorithm: HashAlgorithmName.SHA256,
                outputLength: KeySize
            );
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Xác thực câu trả lời bí mật
        /// </summary>
        public static bool VerifySecurityAnswer(string inputAnswer, string storedAnswerHash, string usernameOrKey)
        {
            if (string.IsNullOrWhiteSpace(inputAnswer) || string.IsNullOrEmpty(storedAnswerHash))
                return false;

            try
            {
                string computed = HashSecurityAnswer(inputAnswer, usernameOrKey);
                byte[] computedBytes = Convert.FromBase64String(computed);
                byte[] storedBytes = Convert.FromBase64String(storedAnswerHash);

                return CryptographicOperations.FixedTimeEquals(computedBytes, storedBytes);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Tạo mã số xác thực OTP gồm các chữ số ngẫu nhiên an toàn
        /// </summary>
        public static string GenerateNumericOtp(int length = 6)
        {
            int min = (int)Math.Pow(10, length - 1);
            int max = (int)Math.Pow(10, length) - 1;
            int code = RandomNumberGenerator.GetInt32(min, max + 1);
            return code.ToString();
        }
    }
}
