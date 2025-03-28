using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Services.Interfaces;
using Domain.Models;
using Domain.Shared.Errors;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Implementations;

public class JwtService : IJwtService
{
    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = "4c461d26-0d49-44f1-9a57-697562e7c58a"u8.ToArray();
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }

    public Guid? ValidateJwtToken(string? token)
    {
        if (token == null) return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = "4c461d26-0d49-44f1-9a57-697562e7c58a"u8.ToArray();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            return Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
        }
        catch
        {
            return null;
        }
    }
}