using api.Data.Models;

namespace api.Processes.Tokens.Interfaces
{
    public interface IGenerateRefreshTokenProcess
    {
        public Task<RefreshToken> Generate(string userId);
    }
}