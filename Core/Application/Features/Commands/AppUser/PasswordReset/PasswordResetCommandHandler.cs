using Application.Abstractions;
using MediatR;

namespace Application.Features.Commands.AppUser.PasswordReset;

public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommandRequest, PasswordResetCommandResponse>
{
    private readonly IUserService _userService;

    public PasswordResetCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<PasswordResetCommandResponse> Handle(PasswordResetCommandRequest request, CancellationToken cancellationToken)
    {
        await _userService.ResetPasswordAsync(request.Email);
        return new();
    }
}