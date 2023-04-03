using MediatR;

namespace Application.Features.Commands.AppUser.Register;

public class RegisterUserCommandRequest : IRequest<RegisterUserCommandResponse>
{
    public string UserName { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string PasswordConfirm { get; set; }
}