using api.Data.Models;
using api.Interfaces;
using MongoDB.Driver;

namespace api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly IMongoCollection<RefreshToken> _refreshTokenCollection;

        public UserRepository(MongoDbService mongoDbService)
        {

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

        public async Task CreateUserAsync(User user)
        {
            await _usersCollection.InsertOneAsync(user);
        }

        public async Task DeleteRefreshTokenAsync(string userId)
        {
            await _refreshTokenCollection.DeleteManyAsync(x => x.UserId == userId);
        }

        public async Task<User> GetUserByIdAsync(string Id)
        {
            User user = await _usersCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            User user = await _usersCollection.Find(x => x.Username == username).FirstOrDefaultAsync();
            return user;
        }
    }
}