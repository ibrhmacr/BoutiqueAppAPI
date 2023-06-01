using MediatR;

namespace Application.Features.Commands.AppUser.VerifyEmail;

public class VerifyEmailCommandRequest : IRequest<VerifyEmailCommandResponse>
{
    public int UserId { get; set; }

    public string VerificationCode { get; set; }
}