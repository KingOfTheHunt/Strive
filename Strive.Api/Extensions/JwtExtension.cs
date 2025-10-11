using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Strive.Application.Users.UseCases.Authenticate;
using Strive.Core;

namespace Strive.Api.Extensions;

public static class JwtExtension
{
    public static string Generate(ResponseData data)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = GenerateClaims(data),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = credentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(ResponseData data)
    {
        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, data.Id.ToString()));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, data.Name));

        return claimsIdentity;
    }
}