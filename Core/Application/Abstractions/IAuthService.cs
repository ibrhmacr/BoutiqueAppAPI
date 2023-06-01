using Application.DTOs.Token;
using Application.DTOs.User;

namespace Application.Abstractions;

public interface IAuthService
{
    Task<TokenDTO> LoginUserAsync(LoginUserDTO loginUserDto);
    
    Task<TokenDTO> GoogleLoginUserAsync(string idToken);
    
}