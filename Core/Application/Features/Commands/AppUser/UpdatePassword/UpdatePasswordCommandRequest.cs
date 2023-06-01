using MediatR;

namespace Application.Features.Commands.AppUser.UpdatePassword;

public class UpdatePasswordCommandRequest : IRequest<UpdatePasswordCommandResponse>
{
    public int UserId { get; set; }

    public string PasswordResetToken { get; set; }

    public string NewPassword { get; set; }

    public string NewPasswordConfirm { get; set; }
}