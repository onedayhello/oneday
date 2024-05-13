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
        public UsersService(IUsernameInUseProcess usernameInUseProcess, ICreateUserProcess createUserProcess)
        {
            _usernameInUseProcess = usernameInUseProcess;
            _createUserProcess = createUserProcess;
        }
        public async Task<IActionResult> CreateUser(UserCreateRequest userRequest)
        {
            bool usernameInUse = await _usernameInUseProcess.Check(userRequest.Username);

            if (usernameInUse)
            {
                return new ConflictObjectResult(new { message = "username unavailable" });
            }

            var user = await _createUserProcess.Create(userRequest);

            return new OkObjectResult(user);
        }

        public Task<IActionResult> LoginUser(UserLoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }
    }
}
