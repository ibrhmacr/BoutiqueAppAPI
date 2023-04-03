using Application.Abstractions;
using Application.Constants;
using Application.Exceptions;
using MediatR;

namespace Application.Features.Commands.AppUser.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
{
    private readonly IUserService _userService;

    public RegisterUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await _userService.RegisterUser(new()
        {
            UserName = request.UserName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Password = request.Password,
            PasswordConfirm = request.PasswordConfirm
        });

        if (result)
        {
            return new()
            {
                Succeded = true,
                Message = Messages.UserRegistered
            };
        }
        else
            throw new UserRegisterFailedException(Messages.UserRegisteredFail);
        
    }
}