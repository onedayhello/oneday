using api.Models;

namespace api.interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetRefreshTokenByUserIdAsync(string userId);
        Task  DeleteRefreshTokenAsync(string Id);
    }
}