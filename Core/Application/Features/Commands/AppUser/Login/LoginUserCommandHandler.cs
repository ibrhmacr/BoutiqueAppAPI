using Application.Abstractions;
using Application.Constants;
using Application.DTOs.Token;
using MediatR;

namespace Application.Features.Commands.AppUser.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    private readonly IAuthService _authService;

    public LoginUserCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }


    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        TokenDTO token = await _authService.LoginUser(new()
        {
            UserNameOrPassword = request.UserNameOrPassword,
            Password = request.Password
        });
        
        return new LoginUserSuccessCommandResponse() { Message = Messages.SuccessLogin, Token = token.AccessToken};
        
    }
}