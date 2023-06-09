using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstractions;
using Application.DTOs.Token;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Concretes;

public class TokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenDTO CreateAccessToken(AppUser user)
    {
        TokenDTO token = new();
        
        //SecurityKeyin Simetrigini Aliyoruz
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["TokenOptions:SecurityKey"]));
        
        //Aldiktan sonra sifrelenmis kimligini olusturuyoruz
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        token.Expiration = DateTime.UtcNow.AddHours(5);
        
        //Olusturulacak tokenin degerlerini veriyoruz ve object initiliezer uzerinden degil constructor uzerinden veriyoruz.
        JwtSecurityToken securityToken = new(
            audience: _configuration["TokenOptions:Audience"],
            issuer: _configuration["TokenOptions:Issuer"],
            expires: token.Expiration,
            notBefore:DateTime.UtcNow,
            signingCredentials: signingCredentials,
            claims:new List<Claim>{new(ClaimTypes.Name, user.UserName)}
            );
        //Token ayarlamalarini ozellestirip arttirabilirsin kullanim acisindan sadece bu uc ozelligi tanimladik. Ex TokenExpiration.
        
        //Tokeni olusturma islemi
        //TokenOlusutrucu siniftan nesne olusturup olusturdugumuz tokeni veriyoruz.
        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);

        return token;
    }
}