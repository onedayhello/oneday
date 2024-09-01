using api.Processes.Tokens.Interfaces;
using Data.Models;
using Data.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace api.Processes.Tokens
{
    public class RefreshAccessTokenProcess : IRefreshAccessTokenProcess
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IGenerateTokenProcess _generateTokenProcess;

        public RefreshAccessTokenProcess(
            IRefreshTokenRepository refreshTokenRepository,
            IGenerateTokenProcess generateTokenProcess
            )
        {
            _refreshTokenRepository = refreshTokenRepository;
            _generateTokenProcess = generateTokenProcess;
        }

        public async Task<JwtSecurityToken?> Refresh(string userId, string? refreshTokenValue)
        {
            RefreshToken refreshToken = await _refreshTokenRepository.GetRefreshTokenByUserIdAsync(userId);

            if (refreshToken == null || refreshToken.ExpiresAt <= DateTime.Now || !String.Equals(refreshToken.Token, refreshTokenValue))
            {
                return null;
            }

            var newAccessToken = _generateTokenProcess.Generate(userId);

            return newAccessToken;

        }
    }
}
