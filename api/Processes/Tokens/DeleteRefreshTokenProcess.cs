using Data.Repositories.Interfaces;
using api.Processes.Tokens.Interfaces;

namespace api.Processes.Tokens
{
    public class DeleteRefreshTokenProcess : IDeleteRefreshTokenProcess
    {
        private readonly IUserRepository _userRepository;
        public DeleteRefreshTokenProcess(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task Delete(string userId)
        {
            await _userRepository.DeleteRefreshTokenAsync(userId);

            return;
        }
    }
}
