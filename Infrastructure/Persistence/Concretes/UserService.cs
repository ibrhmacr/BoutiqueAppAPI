using Application.Abstractions;
using Application.Constants;
using Application.DTOs.User;
using Application.Exceptions;
using Application.Helpers;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Concretes;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMailService _mailService;

    public UserService(UserManager<AppUser> userManager, IMailService mailService)
    {
        _userManager = userManager;
        _mailService = mailService;
    }


    public async Task<bool> RegisterUserAsync(RegisterUserDTO registerUser)
    {
        var user = new AppUser()
        {
            UserName = registerUser.UserName,
            Email = registerUser.Email,
            PhoneNumber = registerUser.PhoneNumber,
            EmailConfirmed = false
        };
        IdentityResult result = await _userManager.CreateAsync(user, registerUser.Password);

        if (result.Succeeded)
        {
            var emailVerificationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            emailVerificationCode = emailVerificationCode.UrlEncode();
            _mailService.SendEmailConfirmationMail(user.Email, user.Id, emailVerificationCode);
        }

        return result.Succeeded;

    }
    
    public async Task<bool> VerifyEmailAsync(int userId, string verificationCode)
    {
        if (userId == null || verificationCode == null)
            return false;

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user != null)
        {
            verificationCode = verificationCode.UrlDecode();
            var result = await _userManager.ConfirmEmailAsync(user, verificationCode);
            return result.Succeeded;
        }

        throw new NotFoundUserException(Messages.NotFoundUser);
    }

    public async Task ResetPasswordAsync(string email)//Email Front-End den gelicek
    {
        AppUser user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            passwordResetToken = passwordResetToken.UrlEncode();

            _mailService.SendPasswordResetMailAsync(email, user.Id, passwordResetToken);
            //Front-End de Ilgili mailin icerisinde userId ve token
            //gelicegini belirtmemiz gerekiyor daha sonra dogrulama islemleri gerceklestiriilecek.

        }
    }

    public async Task<bool> VerifyPasswordResetTokenAsync(int userId, string passwordResetToken)
    {
        AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user != null)
        {
            passwordResetToken = passwordResetToken.UrlDecode();
            return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider,
                "ResetPassword", passwordResetToken);
        }

        return false;
    }

    public async Task UpdatePasswordAsync(int userId, string passwordResetToken, string newPassword)
    {
        AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user != null)
        {
            passwordResetToken = passwordResetToken.UrlDecode();
            IdentityResult result = await _userManager.ResetPasswordAsync(user, passwordResetToken, newPassword);
            if (result.Succeeded)
                await _userManager.UpdateSecurityStampAsync(user);
            else
                throw new PasswordChangeFailedException(Messages.PasswordChangeFailed);
        }
    }
}