using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using dafstore.Application.Users.Abstractions.Services;
using dafstore.Application.Users.Commands.AuthenticateUser;
using dafstore.Domain.Shared;
using Microsoft.IdentityModel.Tokens;

namespace dafstore.Infrastructure.Services;

public class AuthenticateService : IAuthenticateService
{
    public string Generate(AuthenticateDTO dto)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(dto),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = credentials,
        };
        
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private ClaimsIdentity GenerateClaims(AuthenticateDTO dto)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim(ClaimTypes.Email, dto.Email));
        
        foreach (var role in dto.Roles)
            ci.AddClaim(new Claim(ClaimTypes.Role, role));

        return ci;
    }
}