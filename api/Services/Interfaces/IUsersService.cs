using Microsoft.AspNetCore.Mvc;

namespace api.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<IActionResult> CreateUser(UserCreateRequest userRequest);

        public Task<IActionResult> LoginUser(UserLoginRequest loginRequest);
    }
}