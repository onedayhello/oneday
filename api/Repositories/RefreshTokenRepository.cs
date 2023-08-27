using api.Interfaces;
using api.Models;

namespace api.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        public Task DeleteRefreshTokenAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken> GetRefreshTokenByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}