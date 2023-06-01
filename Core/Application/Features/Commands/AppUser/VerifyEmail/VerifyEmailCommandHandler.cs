using Application.Abstractions;
using Application.Constants;
using Application.Exceptions;
using MediatR;

namespace Application.Features.Commands.AppUser.VerifyEmail;

public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommandRequest, VerifyEmailCommandResponse>
{
    private readonly IUserService _userService;

    public VerifyEmailCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<VerifyEmailCommandResponse> Handle(VerifyEmailCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await _userService.VerifyEmailAsync(request.UserId, request.VerificationCode);
        if (result)
            return new();

        throw new UserRegisterFailedException(Messages.UserRegisteredFail);

    }
}