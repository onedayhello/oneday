using api.Models;
using api.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class RefreshTokensController : ControllerBase
{
    private IMongoCollection<RefreshToken> refreshTokenCollection;
    private readonly IConfiguration _config;

    public RefreshTokensController(IConfiguration config, MongoDbService mongoDbService)
    {
        refreshTokenCollection = mongoDbService.db.GetCollection<RefreshToken>("refresh-tokens");
        _config = config;
    }

    private static bool TokenExpired(RefreshToken refreshToken)
    {
        if (refreshToken.ExpiresAt <= DateTime.Now) {
            return true;
        }

        return false;
    }

    private static bool TokensDontMatch(string token1, string token2)
    {
        if (token1 != token2) {
            return true;
        }

        return false;
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshRequest)
    {
        RefreshToken refreshToken = await refreshTokenCollection.Find(x => x.UserId == refreshRequest.UserId).FirstOrDefaultAsync();

        bool isExpired = false;
        bool dontMatch = false;

        if (refreshToken != null )
        {
            isExpired = TokenExpired(refreshToken);
            dontMatch = TokensDontMatch(refreshToken.Token, refreshRequest.Token);
        }

        if (refreshToken == null || isExpired || dontMatch)
        {
            return Unauthorized();
        }

        var newAccessToken = refreshToken.UserId.GenerateJwt(_config["JWT:Key"]);

        return Ok(new JwtSecurityTokenHandler().WriteToken(newAccessToken));
    }

    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken(RevokeTokenRequest revokeTokenRequest)
    {
        await refreshTokenCollection.DeleteManyAsync(x => x.UserId == revokeTokenRequest.UserId);

        return Ok();
    }
}