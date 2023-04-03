using Application.DTOs.Token;
using Application.DTOs.User;

namespace Application.Abstractions;

public interface IAuthService
{
    Task<TokenDTO> LoginUser(LoginUserDTO loginUserDto);
    
    Task<TokenDTO> GoogleLoginUser(string idToken);
    
}