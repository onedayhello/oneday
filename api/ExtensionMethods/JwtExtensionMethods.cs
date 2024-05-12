using api.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace api.ExtensionMethods;

public static class JwtExtensions
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

    public static RefreshToken GenerateRefreshToken(this string userId)
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        string token = Convert.ToBase64String(randomNumber);

        var refreshToken = new RefreshToken{
            UserId = userId,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };

        return refreshToken;
    }
}