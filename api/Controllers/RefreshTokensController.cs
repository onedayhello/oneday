using Microsoft.AspNetCore.Mvc;
using api.Services.Interfaces;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class RefreshTokensController : ControllerBase
{
    private readonly IRefreshTokensService _refreshTokensService;
    private readonly IConfiguration _config;

    public RefreshTokensController(
        IConfiguration config,
        IRefreshTokensService refreshTokenService
        )
    {
        _config = config;
        _refreshTokensService = refreshTokenService;
    }

    [HttpPost("refresh-access-token")]
    public async Task<IActionResult> RefreshAccessToken(RefreshTokenRequest refreshRequest)
    {
        return await _refreshTokensService.RefreshAccessToken(refreshRequest, HttpContext);
    }

    [HttpPost("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken(RevokeTokenRequest revokeTokenRequest)
    {
        return await _refreshTokensService.RevokeRefreshToken(revokeTokenRequest);
    }
}