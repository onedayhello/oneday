using api.Models;
using api.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using api.Interfaces;

namespace api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;

    public UsersController(IConfiguration config, IUserRepository userRepository)
    {
        _config = config;
        _userRepository = userRepository;
    }

    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<IActionResult> CreateUser(UserCreateRequest userRequest)
    {
        string savedPasswordHash = userRequest.Password.HashPassword();

        var user = new User
        {
            Username = userRequest.Username,
            Password = savedPasswordHash
        };

        await _userRepository.CreateUserAsync(user);

        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(UserLoginRequest loginRequest)
    {
        User user = await _userRepository.GetUserByUsernameAsync(loginRequest.Username);

        if ((user != null) && loginRequest.Password.CheckPassword(user.Password))
        {
            var token = user.Id.GenerateJwt(_config["JWT:Key"]);

            await _userRepository.DeleteRefreshTokenAsync(user.Id);

            RefreshToken refreshToken = user.Id.GenerateRefreshToken();

            await _userRepository.CreateRefreshTokenAsync(refreshToken);

            var cookieOptions = new CookieOptions
            {
                Path = "/", HttpOnly = true, Expires = DateTime.Now.AddDays(1)
            };

            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        return Unauthorized();
    }
}
