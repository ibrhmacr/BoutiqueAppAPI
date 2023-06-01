using Application.Abstractions;
using MediatR;

namespace Application.Features.Commands.AppUser.VeriftPasswordResetToken;

public class VerifyPasswordResetTokenCommandHandler : IRequestHandler<VerifyPasswordResetTokenCommandRequest, VerifyPasswordResetTokenCommandResponse>
{
    private readonly IUserService _userService;

    public VerifyPasswordResetTokenCommandHandler(IUserService userService)
    {
        _userService = userService;
    }


    public async Task<VerifyPasswordResetTokenCommandResponse> Handle(VerifyPasswordResetTokenCommandRequest request, CancellationToken cancellationToken)
    {
        bool state = await _userService.VerifyPasswordResetTokenAsync(request.UserId, request.PasswordResetToken);
        return new()
        {
            State = state
        };
    }
}