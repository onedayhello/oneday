using api.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using api.Data.Models;
using api.Data.Repositories.Interfaces;
using api.Services.Interfaces;

namespace api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;
    private readonly IUsersService _usersService;

    public UsersController(IConfiguration config, IUserRepository userRepository, IUsersService usersService)
    {
        _config = config;
        _userRepository = userRepository;
        _usersService = usersService;
    }

    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<IActionResult> CreateUser(UserCreateRequest userRequest)
    {
        return await _usersService.CreateUser(userRequest);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(UserLoginRequest loginRequest)
    {
        User user = await _userRepository.GetUserByUsernameAsync(loginRequest.Username);

        if (user == null || !loginRequest.Password.CheckPassword(user.Password))
        {
            return Unauthorized();
        }

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
}
