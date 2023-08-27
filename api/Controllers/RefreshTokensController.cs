using api.Models;
using api.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using api.Interfaces;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class RefreshTokensController : ControllerBase
{
    private readonly IRefreshTokenRepository _refreshTokenRepository ;
    private readonly IConfiguration _config;

    public RefreshTokensController(IConfiguration config, IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
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

    [HttpPost("refresh-access-token")]
    public async Task<IActionResult> RefreshAccessToken(RefreshTokenRequest refreshRequest)
    {

        if (Request.Cookies.TryGetValue("refreshToken", out string? val)) 
        {
            string requestRefreshToken = val ?? "";
            
            RefreshToken refreshToken = await _refreshTokenRepository.GetRefreshTokenByUserIdAsync(refreshRequest.UserId);

            bool isExpired = false;
            bool dontMatch = false;

            if (refreshToken != null )
            {
                isExpired = TokenExpired(refreshToken);
                dontMatch = TokensDontMatch(refreshToken.Token, requestRefreshToken);
            }

            if (refreshToken == null || isExpired || dontMatch)
            {
                return Unauthorized();
            }

            var newAccessToken = refreshToken.UserId.GenerateJwt(_config["JWT:Key"]);

            return Ok(new JwtSecurityTokenHandler().WriteToken(newAccessToken));
        }

        return Unauthorized();
    }

    [HttpPost("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken(RevokeTokenRequest revokeTokenRequest)
    {
        await _refreshTokenRepository.DeleteRefreshTokenAsync(revokeTokenRequest.UserId);

        return Ok();
    }
}