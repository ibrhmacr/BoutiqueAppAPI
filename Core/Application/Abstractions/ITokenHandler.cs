using Application.DTOs.Token;
using Domain.Entities;

namespace Application.Abstractions;

public interface ITokenHandler
{
    TokenDTO CreateAccessToken(AppUser user);
}