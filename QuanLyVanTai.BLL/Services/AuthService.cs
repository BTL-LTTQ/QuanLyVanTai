using Core.Helpers;
using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.BLL.Services
{
    public class AuthService : IAuthService
    {
        private const int MaxFailedAttempts = 5;
        private const int LockoutMinutes = 5;

        private readonly AppDbContext _db;

        public AuthService(AppDbContext? db = null)
        {
            _db = db ?? new AppDbContext();
        }

        public async Task<LoginResultDto> LoginAsync(LoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return new LoginResultDto
                {
                    Success = false,
                    Message = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu."
                };
            }

            try
            {
                string cleanUsername = request.Username.Trim();
                var account = await _db.Accounts.FirstOrDefaultAsync(a =>
                    a.Username.ToLower() == cleanUsername.ToLower() ||
                    (a.Email != null && a.Email.ToLower() == cleanUsername.ToLower())
                );

                if (account == null)
                {
                    return new LoginResultDto
                    {
                        Success = false,
                        Message = "Tên đăng nhập hoặc mật khẩu không chính xác."
                    };
                }

                // Kiểm tra trạng thái khóa tài khoản
                if (account.Status == "Locked" || (account.LockoutEndTime.HasValue && account.LockoutEndTime.Value > DateTime.UtcNow))
                {
                    if (account.LockoutEndTime.HasValue && account.LockoutEndTime.Value > DateTime.UtcNow)
                    {
                        int remainingMinutes = (int)Math.Ceiling((account.LockoutEndTime.Value - DateTime.UtcNow).TotalMinutes);
                        return new LoginResultDto
                        {
                            Success = false,
                            IsLocked = true,
                            LockoutRemainingMinutes = Math.Max(1, remainingMinutes),
                            Message = $"Tài khoản đang bị tạm khóa do nhập sai mật khẩu quá {MaxFailedAttempts} lần. Vui lòng thử lại sau {remainingMinutes} phút."
                        };
                    }
                    else if (account.LockoutEndTime.HasValue && account.LockoutEndTime.Value <= DateTime.UtcNow)
                    {
                        // Hết thời gian khóa -> tự động mở khóa
                        account.LockoutEndTime = null;
                        account.FailedLoginAttempts = 0;
                        account.Status = "Active";
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        return new LoginResultDto
                        {
                            Success = false,
                            IsLocked = true,
                            Message = "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ quản trị viên."
                        };
                    }
                }

                // Xác thực mật khẩu với Hash + Salt
                bool isPasswordValid = SecurityHelper.VerifyPassword(
                    request.Password,
                    account.PasswordHash,
                    account.PasswordSalt,
                    out bool needsUpgrade
                );

                if (!isPasswordValid)
                {
                    account.FailedLoginAttempts++;

                    if (account.FailedLoginAttempts >= MaxFailedAttempts)
                    {
                        account.LockoutEndTime = DateTime.UtcNow.AddMinutes(LockoutMinutes);
                        account.Status = "Locked";
                        await _db.SaveChangesAsync();

                        return new LoginResultDto
                        {
                            Success = false,
                            IsLocked = true,
                            LockoutRemainingMinutes = LockoutMinutes,
                            Message = $"Bạn đã nhập sai mật khẩu {MaxFailedAttempts} lần liên tiếp. Tài khoản đã bị tạm khóa trong {LockoutMinutes} phút."
                        };
                    }
                    else
                    {
                        await _db.SaveChangesAsync();
                        int remaining = MaxFailedAttempts - account.FailedLoginAttempts;
                        return new LoginResultDto
                        {
                            Success = false,
                            RemainingAttempts = remaining,
                            Message = $"Mật khẩu không chính xác! Bạn còn {remaining} lần thử trước khi tài khoản bị khóa."
                        };
                    }
                }

                // Đăng nhập thành công -> Reset số lần nhập sai
                account.FailedLoginAttempts = 0;
                account.LockoutEndTime = null;

                // Tự động nâng cấp mật khẩu nếu tài khoản cũ chưa có Salt (Seed ban đầu)
                if (needsUpgrade || string.IsNullOrEmpty(account.PasswordSalt))
                {
                    string newSalt = SecurityHelper.GenerateSalt();
                    account.PasswordSalt = newSalt;
                    account.PasswordHash = SecurityHelper.HashPassword(request.Password, newSalt);
                }

                await _db.SaveChangesAsync();

                // Lưu session người dùng
                SessionManager.SetCurrentUser(
                    account.Id,
                    account.Username,
                    account.FullName,
                    account.Email,
                    account.PhoneNumber,
                    account.Role
                );

                // Lưu cấu hình Remember Me
                SessionManager.SaveRememberMe(request.RememberMe, request.Username);

                return new LoginResultDto
                {
                    Success = true,
                    Message = "Đăng nhập thành công!",
                    Account = account
                };
            }
            catch (Exception ex)
            {
                return new LoginResultDto
                {
                    Success = false,
                    Message = $"Lỗi kết nối hệ thống trong quá trình đăng nhập: {ex.Message}"
                };
            }
        }

        public async Task<RegisterResultDto> RegisterAsync(RegisterRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return new RegisterResultDto { Success = false, Message = "Tên đăng nhập và mật khẩu không được để trống." };
            }

            if (request.Password != request.ConfirmPassword)
            {
                return new RegisterResultDto { Success = false, Message = "Mật khẩu xác nhận không trùng khớp." };
            }

            try
            {
                string cleanUsername = request.Username.Trim().ToLower();

                // Kiểm tra tên đăng nhập đã tồn tại chưa
                bool userExists = await _db.Accounts.AnyAsync(a => a.Username.ToLower() == cleanUsername);
                if (userExists)
                {
                    return new RegisterResultDto { Success = false, Message = "Tên đăng nhập đã được sử dụng. Vui lòng chọn tên khác." };
                }

                // Kiểm tra email nếu có
                if (!string.IsNullOrWhiteSpace(request.Email))
                {
                    string cleanEmail = request.Email.Trim().ToLower();
                    bool emailExists = await _db.Accounts.AnyAsync(a => a.Email != null && a.Email.ToLower() == cleanEmail);
                    if (emailExists)
                    {
                        return new RegisterResultDto { Success = false, Message = "Email này đã được đăng ký trong hệ thống." };
                    }
                }

                // Sinh Salt và băm mật khẩu
                string salt = SecurityHelper.GenerateSalt();
                string passwordHash = SecurityHelper.HashPassword(request.Password, salt);

                string? securityAnswerHash = null;
                if (!string.IsNullOrWhiteSpace(request.SecurityAnswer))
                {
                    securityAnswerHash = SecurityHelper.HashSecurityAnswer(request.SecurityAnswer, request.Username.Trim());
                }

                var newAccount = new Account
                {
                    Username = request.Username.Trim(),
                    PasswordHash = passwordHash,
                    PasswordSalt = salt,
                    FullName = string.IsNullOrWhiteSpace(request.FullName) ? request.Username.Trim() : request.FullName.Trim(),
                    Email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim(),
                    PhoneNumber = string.IsNullOrWhiteSpace(request.PhoneNumber) ? null : request.PhoneNumber.Trim(),
                    Role = "Staff", // Mặc định tài khoản đăng ký mới là Nhân viên
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow,
                    SecurityQuestion = string.IsNullOrWhiteSpace(request.SecurityQuestion) ? null : request.SecurityQuestion.Trim(),
                    SecurityAnswerHash = securityAnswerHash,
                    FailedLoginAttempts = 0
                };

                _db.Accounts.Add(newAccount);
                await _db.SaveChangesAsync();

                return new RegisterResultDto
                {
                    Success = true,
                    Message = "Đăng ký tài khoản thành công! Bạn có thể đăng nhập ngay.",
                    CreatedAccountId = newAccount.Id
                };
            }
            catch (Exception ex)
            {
                return new RegisterResultDto
                {
                    Success = false,
                    Message = $"Đã xảy ra lỗi khi tạo tài khoản: {ex.Message}"
                };
            }
        }

        public async Task<PasswordResetResultDto> RequestPasswordResetOtpAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return new PasswordResetResultDto { Success = false, Message = "Vui lòng nhập địa chỉ Email." };
            }

            try
            {
                string cleanEmail = email.Trim().ToLower();
                var account = await _db.Accounts.FirstOrDefaultAsync(a => a.Email != null && a.Email.ToLower() == cleanEmail);
                if (account == null)
                {
                    return new PasswordResetResultDto { Success = false, Message = "Không tìm thấy tài khoản nào khớp với Email này." };
                }

                string otp = SecurityHelper.GenerateNumericOtp(6);
                account.ResetToken = otp;
                account.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(15); // Hiệu lực 15 phút

                await _db.SaveChangesAsync();

                return new PasswordResetResultDto
                {
                    Success = true,
                    Message = "Mã xác thực OTP đã được gửi đến email của bạn (hiệu lực trong 15 phút).",
                    GeneratedOtp = otp
                };
            }
            catch (Exception ex)
            {
                return new PasswordResetResultDto { Success = false, Message = $"Lỗi khi gửi mã xác thực: {ex.Message}" };
            }
        }

        public async Task<PasswordResetResultDto> ResetPasswordWithOtpAsync(string email, string otp, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(otp) || string.IsNullOrWhiteSpace(newPassword))
            {
                return new PasswordResetResultDto { Success = false, Message = "Vui lòng nhập đầy đủ email, mã OTP và mật khẩu mới." };
            }

            try
            {
                string cleanEmail = email.Trim().ToLower();
                var account = await _db.Accounts.FirstOrDefaultAsync(a => a.Email != null && a.Email.ToLower() == cleanEmail);
                if (account == null)
                {
                    return new PasswordResetResultDto { Success = false, Message = "Không tìm thấy tài khoản." };
                }

                if (string.IsNullOrEmpty(account.ResetToken) || account.ResetToken != otp.Trim())
                {
                    return new PasswordResetResultDto { Success = false, Message = "Mã xác thực OTP không chính xác." };
                }

                if (!account.ResetTokenExpiry.HasValue || account.ResetTokenExpiry.Value < DateTime.UtcNow)
                {
                    return new PasswordResetResultDto { Success = false, Message = "Mã xác thực OTP đã hết hạn. Vui lòng yêu cầu mã mới." };
                }

                // Cập nhật mật khẩu mới bằng Salt + PBKDF2
                string newSalt = SecurityHelper.GenerateSalt();
                account.PasswordSalt = newSalt;
                account.PasswordHash = SecurityHelper.HashPassword(newPassword, newSalt);

                // Hủy mã OTP và mở khóa tài khoản nếu đang bị khóa
                account.ResetToken = null;
                account.ResetTokenExpiry = null;
                account.FailedLoginAttempts = 0;
                account.LockoutEndTime = null;
                account.Status = "Active";

                await _db.SaveChangesAsync();

                return new PasswordResetResultDto
                {
                    Success = true,
                    Message = "Đặt lại mật khẩu thành công! Bạn có thể đăng nhập bằng mật khẩu mới."
                };
            }
            catch (Exception ex)
            {
                return new PasswordResetResultDto { Success = false, Message = $"Lỗi khi đặt lại mật khẩu: {ex.Message}" };
            }
        }

        public async Task<SecurityQuestionResultDto> GetSecurityQuestionAsync(string usernameOrEmail)
        {
            if (string.IsNullOrWhiteSpace(usernameOrEmail))
            {
                return new SecurityQuestionResultDto { Success = false, Message = "Vui lòng nhập tên đăng nhập hoặc email." };
            }

            try
            {
                string clean = usernameOrEmail.Trim().ToLower();
                var account = await _db.Accounts.FirstOrDefaultAsync(a =>
                    a.Username.ToLower() == clean || (a.Email != null && a.Email.ToLower() == clean));

                if (account == null)
                {
                    return new SecurityQuestionResultDto { Success = false, Message = "Không tìm thấy tài khoản." };
                }

                if (string.IsNullOrWhiteSpace(account.SecurityQuestion))
                {
                    return new SecurityQuestionResultDto
                    {
                        Success = false,
                        Message = "Tài khoản này chưa thiết lập câu hỏi bảo mật. Vui lòng khôi phục qua Email."
                    };
                }

                return new SecurityQuestionResultDto
                {
                    Success = true,
                    SecurityQuestion = account.SecurityQuestion
                };
            }
            catch (Exception ex)
            {
                return new SecurityQuestionResultDto { Success = false, Message = $"Lỗi hệ thống: {ex.Message}" };
            }
        }

        public async Task<PasswordResetResultDto> ResetPasswordWithSecurityQuestionAsync(string usernameOrEmail, string answer, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(usernameOrEmail) || string.IsNullOrWhiteSpace(answer) || string.IsNullOrWhiteSpace(newPassword))
            {
                return new PasswordResetResultDto { Success = false, Message = "Vui lòng nhập đầy đủ thông tin." };
            }

            try
            {
                string clean = usernameOrEmail.Trim().ToLower();
                var account = await _db.Accounts.FirstOrDefaultAsync(a =>
                    a.Username.ToLower() == clean || (a.Email != null && a.Email.ToLower() == clean));

                if (account == null)
                {
                    return new PasswordResetResultDto { Success = false, Message = "Không tìm thấy tài khoản." };
                }

                if (string.IsNullOrWhiteSpace(account.SecurityAnswerHash))
                {
                    return new PasswordResetResultDto { Success = false, Message = "Tài khoản chưa thiết lập câu trả lời bảo mật." };
                }

                bool isAnswerValid = SecurityHelper.VerifySecurityAnswer(answer, account.SecurityAnswerHash, account.Username);
                if (!isAnswerValid)
                {
                    return new PasswordResetResultDto { Success = false, Message = "Câu trả lời bảo mật không chính xác." };
                }

                // Cập nhật mật khẩu mới
                string newSalt = SecurityHelper.GenerateSalt();
                account.PasswordSalt = newSalt;
                account.PasswordHash = SecurityHelper.HashPassword(newPassword, newSalt);

                account.FailedLoginAttempts = 0;
                account.LockoutEndTime = null;
                account.Status = "Active";

                await _db.SaveChangesAsync();

                return new PasswordResetResultDto
                {
                    Success = true,
                    Message = "Đặt lại mật khẩu thành công! Bạn có thể đăng nhập bằng mật khẩu mới."
                };
            }
            catch (Exception ex)
            {
                return new PasswordResetResultDto { Success = false, Message = $"Lỗi khi đặt lại mật khẩu: {ex.Message}" };
            }
        }
    }
}
