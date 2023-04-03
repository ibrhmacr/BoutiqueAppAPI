using Application.Abstractions;
using Application.Constants;
using Application.DTOs.User;
using Application.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Concretes;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMailService _mailService;

    public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> RegisterUser(RegisterUserDTO registerUser)
    {
        var user = new AppUser()
        {
            UserName = registerUser.UserName,
            Email = registerUser.Email,
            PhoneNumber = registerUser.PhoneNumber,
            EmailConfirmed = false
        };
        IdentityResult result = await _userManager.CreateAsync(user, registerUser.Password);

        return result.Succeeded;

    }
}