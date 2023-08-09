using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class RefreshTokensController : ControllerBase
{
    private IMongoCollection<RefreshToken> refreshTokenCollection;
    private readonly IConfiguration _config;

    public RefreshTokensController(IConfiguration config, MongoDbService mongoDbService)
    {
        refreshTokenCollection = mongoDbService.db.GetCollection<RefreshToken>("refresh_token");
        _config = config;
    }

    private bool TokenExpired(RefreshToken token)
    {
        //if expired delete from collection
        return true;
    }

    private static bool TokensDontMatch(string token1, string token2)
    {
        return true;
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

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var newToken = new JwtSecurityToken(
            claims: new[] {
            new Claim("id", refreshToken.UserId)
            },
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return Ok(newToken);
    }
}