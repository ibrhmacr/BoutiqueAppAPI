using MediatR;

namespace Application.Features.Commands.AppUser.VeriftPasswordResetToken;

public class VerifyPasswordResetTokenCommandRequest : IRequest<VerifyPasswordResetTokenCommandResponse>
{
    public int UserId { get; set; }

    public string PasswordResetToken { get; set; }
}