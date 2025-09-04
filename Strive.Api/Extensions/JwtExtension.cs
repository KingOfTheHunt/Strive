using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Strive.Application.UseCases.Users.Authenticate;
using Strive.Core;

namespace Strive.Api.Extensions;

public static class JwtExtension
{
    public static string Generate(ResponseData userData)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = GenerateClaims(userData),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = credentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(ResponseData userData)
    {
        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, userData.Id.ToString()));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, userData.Email));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, userData.Name));

        return claimsIdentity;
    }
}