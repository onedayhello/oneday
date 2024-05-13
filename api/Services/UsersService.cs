using api.Data.Models;
using api.Processes.Users.Interfaces;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsernameInUseProcess _usernameInUseProcess;
        private readonly ICreateUserProcess _createUserProcess;
        private readonly IAuthenticateUserProcess _authenticateUserProcess;
        public UsersService(
            IUsernameInUseProcess usernameInUseProcess, 
            ICreateUserProcess createUserProcess, 
            IAuthenticateUserProcess authenticateUserProcess
            )
        {
            _usernameInUseProcess = usernameInUseProcess;
            _createUserProcess = createUserProcess;
            _authenticateUserProcess = authenticateUserProcess;
        }
        public async Task<IActionResult> CreateUser(UserCreateRequest userRequest)
        {
            var usernameInUse = await _usernameInUseProcess.Check(userRequest.Username);

            if (usernameInUse)
            {
                return new ConflictObjectResult(new { message = "username unavailable" });
            }

            var user = await _createUserProcess.Create(userRequest);

            return new OkObjectResult(user);
        }

        public async Task<IActionResult> LoginUser(UserLoginRequest loginRequest)
        {
            var user = await _authenticateUserProcess.Authenticate(loginRequest);

            if (user == null)
            {
                return new UnauthorizedObjectResult("Invalid password");
            }



            throw new NotImplementedException();
        }
    }
}
