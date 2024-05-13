using api.Processes.Tokens.Interfaces;
using api.Processes.Users.Interfaces;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace api.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsernameInUseProcess _usernameInUseProcess;
        private readonly ICreateUserProcess _createUserProcess;
        private readonly IAuthenticateUserProcess _authenticateUserProcess;
        private readonly IGenerateTokenProcess _generateTokenProcess;
        private readonly IDeleteRefreshTokenProcess _deleteRefreshTokenProcess;
        private readonly IGenerateRefreshTokenProcess _generateRefreshTokenProcess;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersService(
            IUsernameInUseProcess usernameInUseProcess, 
            ICreateUserProcess createUserProcess,
            IAuthenticateUserProcess authenticateUserProcess,
            IGenerateTokenProcess generateTokenProcess,
            IDeleteRefreshTokenProcess deleteRefreshTokenProcess,
            IGenerateRefreshTokenProcess generateRefreshTokenProcess,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _usernameInUseProcess = usernameInUseProcess;
            _createUserProcess = createUserProcess;
            _authenticateUserProcess = authenticateUserProcess;
            _generateTokenProcess = generateTokenProcess;
            _deleteRefreshTokenProcess = deleteRefreshTokenProcess;
            _generateRefreshTokenProcess = generateRefreshTokenProcess;
            _httpContextAccessor = httpContextAccessor;
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

            var token = _generateTokenProcess.Generate(user.Id);

            await _deleteRefreshTokenProcess.Delete(user.Id);

            var refreshToken = await _generateRefreshTokenProcess.Generate(user.Id);

            var cookieOptions = new CookieOptions
            {
                IsEssential = true,
                Secure = false,
                Path = "/",
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(1)
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            return new OkObjectResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
