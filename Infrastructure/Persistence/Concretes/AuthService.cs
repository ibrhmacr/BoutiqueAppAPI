using Application.Abstractions;
using Application.Constants;
using Application.DTOs.Token;
using Application.DTOs.User;
using Application.Exceptions;
using Domain.Entities;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Concretes;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
    }
    public async Task<TokenDTO> LoginUserAsync(LoginUserDTO loginUserDto)
    {
        AppUser? user = await _userManager.FindByNameAsync(loginUserDto.UserNameOrEmail);
        if (user == null)
            user = await _userManager.FindByEmailAsync(loginUserDto.UserNameOrEmail);
        
        if (user == null)
            throw new NotFoundUserException(Messages.NotFoundUser);

        if (!user.EmailConfirmed)
            throw new EmailConfirmedFailException(Messages.EmailConfirmedFail);
        
        
        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginUserDto.Password, false);
        //todo lockout durumunu kontrol edebilirsin.

        if (result.Succeeded)
        {
            TokenDTO token = _tokenHandler.CreateAccessToken(user);
            return token;
        }
        else
            throw new Exception(Messages.LoginFail);
        
    }

    public async Task<TokenDTO> GoogleLoginUserAsync(string idToken)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string>
                { "158572277152-i7hrsceh6n6mh5carjvo6sf1gejjppi2.apps.googleusercontent.com" } //ClientId
        };
        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

        var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

        Domain.Entities.AppUser? user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        bool result = user != null;

        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new()
                {
                    Email = payload.Email,
                    UserName = payload.Name
                };
                IdentityResult identityResult = await _userManager.CreateAsync(user);
                result = identityResult.Succeeded;
            }
        }

        if (result)
            await _userManager.AddLoginAsync(user, info);
        else
            throw new InvalidExternalAuthenticationException(Messages.InvalidExternalAuthentication);

        TokenDTO token = _tokenHandler.CreateAccessToken(user);

        return token;

    }

    
    
}