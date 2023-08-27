using api.Models;
using api.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using api.Interfaces;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;

    public UsersController(IConfiguration config, MongoDbService mongoDbService, IUserRepository userRepository)
    {
        _config = config;
        _userRepository = userRepository;
    }

    private bool checkPassword(string password, string savedPasswordHash)
    {
        /* Fetch the stored value */
        /* Extract the bytes */
        byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
        /* Get the salt */
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);
        /* Compute the hash on the password the user entered */
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(20);
        /* Compare the results */
        if (Enumerable.Range(0, 20).Any(i => hashBytes[i + 16] != hash[i]))
        {
            return false;
        }
        return true;
    }

    private RefreshToken generateRefreshToken(string userId)
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        string token = Convert.ToBase64String(randomNumber);

        var refreshToken = new RefreshToken{
            UserId = userId,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };

        return refreshToken;
    }

    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<IActionResult> CreateUser(UserCreateRequest userRequest)
    {
        var salt = new byte[16];
        RandomNumberGenerator.Create().GetBytes(salt);
        var pbkdf2 = new Rfc2898DeriveBytes(userRequest.Password, salt, 100000);
        var hash = pbkdf2.GetBytes(20);
        var hashBytes = new byte[36];

        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        var savedPasswordHash = Convert.ToBase64String(hashBytes);

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

        if ((user != null) && checkPassword(loginRequest.Password, user.Password))
        {
            var token = user.Id.GenerateJwt(_config["JWT:Key"]);

            await _userRepository.DeleteRefreshTokenAsync(user.Id);
            
            var refreshToken = generateRefreshToken(user.Id);

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
