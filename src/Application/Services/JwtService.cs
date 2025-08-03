using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Exceptions;
using Application.Interfaces;
using Domain.AggregateRoots;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class JwtService(IConfiguration configuration) : IJwtService
{
    public string GenerateJwtToken(User user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var secret = configuration.GetSection("JwtConfig:Secret").Value;
        var issuer = configuration.GetSection("JwtConfig:Issuer").Value;
        var audience = configuration.GetSection("JwtConfig:Audience").Value;

        if (secret == null || issuer == null || audience == null)
            throw new UnhandledException("Something was wrong with AddJwtBearer JwtConfig");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Audience = audience,
            Subject = new ClaimsIdentity(
                [
                    new Claim("Id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.FullName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(
                        JwtRegisteredClaimNames.Iat,
                        DateTime.Now.ToUniversalTime().ToString()
                    ),
                ]
            ),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
                SecurityAlgorithms.HmacSha256
            ),
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);

        return jwtToken;
    }
}
