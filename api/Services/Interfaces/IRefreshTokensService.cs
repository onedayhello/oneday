using Microsoft.AspNetCore.Mvc;

namespace api.Services.Interfaces
{
    public interface IRefreshTokensService
    {
        Task<IActionResult> RefreshAccessToken(RefreshTokenRequest refreshRequest, HttpContext httpContext);
        Task<IActionResult> RevokeRefreshToken(RevokeTokenRequest revokeTokenRequest);
    }
}