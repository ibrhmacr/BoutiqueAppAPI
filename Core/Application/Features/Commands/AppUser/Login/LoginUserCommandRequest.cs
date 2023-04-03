using MediatR;

namespace Application.Features.Commands.AppUser.Login;

public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
{
    public string UserNameOrPassword { get; set; }
    
    public string Password { get; set; }
}