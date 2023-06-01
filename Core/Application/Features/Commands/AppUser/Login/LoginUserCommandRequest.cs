using MediatR;

namespace Application.Features.Commands.AppUser.Login;

public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
{
    public string UserNameOrEmail { get; set; }
    
    public string Password { get; set; }
}