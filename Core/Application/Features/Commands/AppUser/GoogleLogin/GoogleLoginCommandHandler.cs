using Application.Abstractions;
using Application.DTOs.Token;
using MediatR;


namespace Application.Features.Commands.AppUser.GoogleLogin;

public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
{
    private readonly IAuthService _authService;

    public GoogleLoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }


    public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
    {
        TokenDTO token = await _authService.GoogleLoginUserAsync(request.IdToken);
        return new() { Token = token.AccessToken };
    } 
}