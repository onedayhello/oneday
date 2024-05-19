using Data.Models;

namespace api.Processes.Users.Interfaces
{
    public interface IAuthenticateUserProcess
    {
        public Task<User?> Authenticate(UserLoginRequest userLoginRequest);
    }
}