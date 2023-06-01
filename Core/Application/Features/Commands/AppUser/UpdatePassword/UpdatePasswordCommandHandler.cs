using Application.Abstractions;
using Application.Constants;
using Application.Exceptions;
using MediatR;

namespace Application.Features.Commands.AppUser.UpdatePassword;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
{
    private readonly IUserService _userService;

    public UpdatePasswordCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        if (!request.NewPassword.Equals(request.NewPasswordConfirm))
            throw new PasswordChangeFailedException(Messages.PasswordAndPasswordConfirmNotEqual);

        await _userService.UpdatePasswordAsync(request.UserId, request.PasswordResetToken, request.NewPassword);

        return new();
    }
}