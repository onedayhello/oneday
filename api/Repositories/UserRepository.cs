using api.Interfaces;
using api.Models;
using MongoDB.Driver;

namespace api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IMongoCollection<User> _usersCollection;
        private IMongoCollection<RefreshToken> _refreshTokenCollection;
        private readonly IConfiguration _config;
        public UserRepository(IConfiguration config, MongoDbService mongoDbService)
        {
            _config = config;

            _usersCollection = mongoDbService.db.GetCollection<User>("users");
            _refreshTokenCollection = mongoDbService.db
                .GetCollection<RefreshToken>("refresh-tokens");
        }
        public async Task CreateRefreshTokenAsync(RefreshToken refreshToken)
        {
            var ttlIndex = new CreateIndexModel<RefreshToken>(
                Builders<RefreshToken>.IndexKeys.Ascending(x => x.ExpiresAt),
                new CreateIndexOptions { ExpireAfter = TimeSpan.Zero }
            );
            _refreshTokenCollection.Indexes.CreateOne(ttlIndex);

            await _refreshTokenCollection.InsertOneAsync(refreshToken);
        }

        public Task CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteRefreshTokenAsync(string userId)
        {
            await _refreshTokenCollection.DeleteManyAsync(x => x.UserId == userId);
        }

        public Task<User> GetUserByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}