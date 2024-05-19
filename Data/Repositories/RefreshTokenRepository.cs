using api.Data.Models;
using api.Data.Repositories.Interfaces;
using MongoDB.Driver;

namespace api.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMongoCollection<RefreshToken> _refreshTokenCollection;
        public RefreshTokenRepository(MongoDbService mongoDbService)
        {
            _refreshTokenCollection = mongoDbService.db
                .GetCollection<RefreshToken>("refresh-tokens");
        }
        public async Task DeleteRefreshTokenAsync(string Id)
        {
            await _refreshTokenCollection.DeleteManyAsync(x => x.UserId == Id);
        }

        public async Task<RefreshToken> GetRefreshTokenByUserIdAsync(string userId)
        {
            RefreshToken refreshToken = await _refreshTokenCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            return refreshToken;
        }
    }
}