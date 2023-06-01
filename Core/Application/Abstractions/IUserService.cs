using Application.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace Application.Abstractions;

public interface IUserService
{
    Task<bool> RegisterUserAsync(RegisterUserDTO registerUser);
    
    Task<bool> VerifyEmailAsync(int userId,string verificationCode);

    Task ResetPasswordAsync(string email);
    
    Task<bool> VerifyPasswordResetTokenAsync(int userId, string passwordResetToken);

    Task UpdatePasswordAsync(int userId, string passwordResetToken, string newPassword);
}