using Application.DTOs.Token;

namespace Application.Abstractions;

public interface ITokenHandler
{
    TokenDTO CreateAccessToken();
}