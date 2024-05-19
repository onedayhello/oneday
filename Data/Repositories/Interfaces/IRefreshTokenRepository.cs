using api.Data.Models;

namespace api.Data.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetRefreshTokenByUserIdAsync(string userId);
        Task DeleteRefreshTokenAsync(string Id);
    }
}