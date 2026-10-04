using QuanLyVanTai.BLL.DTOs;

namespace QuanLyVanTai.BLL.Services
{
    public interface IAuthService
    {
        Task<LoginResultDto> LoginAsync(LoginRequestDto request);
        Task<RegisterResultDto> RegisterAsync(RegisterRequestDto request);
        Task<PasswordResetResultDto> RequestPasswordResetOtpAsync(string email);
        Task<PasswordResetResultDto> ResetPasswordWithOtpAsync(string email, string otp, string newPassword);
        Task<SecurityQuestionResultDto> GetSecurityQuestionAsync(string usernameOrEmail);
        Task<PasswordResetResultDto> ResetPasswordWithSecurityQuestionAsync(string usernameOrEmail, string answer, string newPassword);
    }
}
