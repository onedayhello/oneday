using System.IdentityModel.Tokens.Jwt;

namespace api.Processes.Tokens.Interfaces
{
    public interface IRefreshAccessTokenProcess
    {
        Task<JwtSecurityToken?> Refresh(string userId, string? refreshTokenValue);
    }
}