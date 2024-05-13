using api.Data.Repositories.Interfaces;
using api.Processes.Users.Interfaces;

namespace api.Processes.Users
{
    public class UsernameInUseProcess : IUsernameInUseProcess
    {
        private readonly IUserRepository _userRepository;
        public UsernameInUseProcess(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Check(string username)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(username);

            if (existingUser != null)
            {
                return true;
            }

            return false;
        }
    }
}
