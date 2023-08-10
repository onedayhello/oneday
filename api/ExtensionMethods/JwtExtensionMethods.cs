using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.ExtensionMethods;

public static class StringExtensions
{
    public static JwtSecurityToken GenerateJwt(this string userId, string jwtKey)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: new[] {
                new Claim("id", userId)
            },
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return token;
    }
}