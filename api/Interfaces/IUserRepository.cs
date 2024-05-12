using api.Data.Models;

namespace api.Interfaces
{
    public interface IUserRepository 
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByIdAsync(string Id);
        Task CreateUserAsync(User user);

        Task DeleteRefreshTokenAsync(string userId);
        Task CreateRefreshTokenAsync(RefreshToken refreshToken);
    }
}