using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Application.Features.Commands.AppUser.GoogleLogin;

public class GoogleLoginCommandRequest : IRequest<GoogleLoginCommandResponse>
{
    public string IdToken { get; set; }
}