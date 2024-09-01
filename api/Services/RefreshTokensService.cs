using Amazon.Runtime.Internal;
using api.Processes.Tokens.Interfaces;
using api.Services.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace api.Services
{
    public class RefreshTokensService : IRefreshTokensService
    {
        private readonly IRefreshAccessTokenProcess _refreshAccessTokenProcess;
        private readonly IDeleteRefreshTokenProcess _deleteRefreshTokenProcess;
        public RefreshTokensService(
            IRefreshAccessTokenProcess refreshAccessTokenProcess,
            IDeleteRefreshTokenProcess deleteRefreshTokenProcess
            )
        {
            _refreshAccessTokenProcess = refreshAccessTokenProcess;
            _deleteRefreshTokenProcess = deleteRefreshTokenProcess;
        }

        public async Task<IActionResult> RefreshAccessToken(RefreshTokenRequest refreshRequest, HttpContext httpContext)
        {
            if (!httpContext.Request.Cookies.TryGetValue("refreshToken", out string? refreshTokenValue))
            {
                return new UnauthorizedResult();
            }

            var newAccessToken = await _refreshAccessTokenProcess.Refresh(refreshRequest.UserId, refreshTokenValue);

            if (newAccessToken == null)
            {
                return new UnauthorizedResult();
            }

            return new OkObjectResult(new JwtSecurityTokenHandler().WriteToken(newAccessToken));

        }

        public async Task<IActionResult> RevokeRefreshToken(RevokeTokenRequest revokeTokenRequest)
        {
            await _deleteRefreshTokenProcess.Delete(revokeTokenRequest.UserId);

            return new OkResult();
        }
    }
}

