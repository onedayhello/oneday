using api.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using api.Data.Models;
using api.Data.Repositories.Interfaces;

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

    [HttpPost("refresh-access-token")]
    public async Task<IActionResult> RefreshAccessToken(RefreshTokenRequest refreshRequest)
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out string? refreshTokenValue))
        {
            return Unauthorized();
        }

        RefreshToken refreshToken = await _refreshTokenRepository.GetRefreshTokenByUserIdAsync(refreshRequest.UserId);

        if (refreshToken == null || refreshToken.ExpiresAt <= DateTime.Now || !String.Equals(refreshToken.Token, refreshTokenValue))
        {
            return Unauthorized();
        }

        var newAccessToken = refreshToken.UserId.GenerateJwt(_config["JWT:Key"]);

        return Ok(new JwtSecurityTokenHandler().WriteToken(newAccessToken));
    }

    [HttpPost("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken(RevokeTokenRequest revokeTokenRequest)
    {
        await _refreshTokenRepository.DeleteRefreshTokenAsync(revokeTokenRequest.UserId);

        return Ok();
    }
}