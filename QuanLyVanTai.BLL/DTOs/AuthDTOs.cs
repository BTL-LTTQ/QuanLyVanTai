using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.BLL.DTOs
{
    public class LoginRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; } = false;
    }

    public class LoginResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Account? Account { get; set; }
        public int RemainingAttempts { get; set; }
        public bool IsLocked { get; set; }
        public int LockoutRemainingMinutes { get; set; }
    }

    public class RegisterRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SecurityQuestion { get; set; }
        public string? SecurityAnswer { get; set; }
    }

    public class RegisterResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int CreatedAccountId { get; set; }
    }

    public class PasswordResetResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? GeneratedOtp { get; set; } // Giúp hiển thị popup test môi trường dev
    }

    public class SecurityQuestionResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? SecurityQuestion { get; set; }
    }
}
