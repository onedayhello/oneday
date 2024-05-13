using api.Data.Models;
using api.Data.Repositories.Interfaces;
using api.Processes.Tokens.Interfaces;
using System.Security.Cryptography;

namespace api.Processes.Tokens
{
    public class GenerateRefreshTokenProcess : IGenerateRefreshTokenProcess
    {
        private readonly IUserRepository _userRepository;
        public GenerateRefreshTokenProcess(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public async Task<RefreshToken> Generate(string userId)
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            string token = Convert.ToBase64String(randomNumber);

            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };

            await _userRepository.CreateRefreshTokenAsync(refreshToken);

            return refreshToken;
        }
    }
}
