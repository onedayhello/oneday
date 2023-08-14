using api.Models;
using api.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{

    private IMongoCollection<User> usersCollection;
    private IMongoCollection<RefreshToken> refreshTokenCollection; //only for creation of refresh tokens
    private readonly IConfiguration _config;

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
            throw new UnauthorizedAccessException();
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

    public UsersController(IConfiguration config, MongoDbService mongoDbService)
    {
        usersCollection = mongoDbService.db.GetCollection<User>("users");

        refreshTokenCollection = mongoDbService.db.GetCollection<RefreshToken>("refresh-tokens");
        var ttlIndex = new CreateIndexModel<RefreshToken>(
            Builders<RefreshToken>.IndexKeys.Ascending(x => x.ExpiresAt),
            new CreateIndexOptions { ExpireAfter = TimeSpan.Zero }
        );
        refreshTokenCollection.Indexes.CreateOne(ttlIndex);
        _config = config;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserAsync(string Id)
    {
        var user = await usersCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();
        return Ok(user);
    }

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

        await usersCollection.InsertOneAsync(user);

        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(UserLoginRequest loginRequest)
    {
        var user = await usersCollection.Find(x => x.Username == loginRequest.Username).FirstOrDefaultAsync();

        if (user == null)
        {
            return Unauthorized();
        }

        if (checkPassword(loginRequest.Password, user.Password))
        {
            var token = user.Id.GenerateJwt(_config["JWT:Key"]);

            await refreshTokenCollection.DeleteManyAsync(x => x.UserId == user.Id);
            
            var refreshToken = generateRefreshToken(user.Id);

            await refreshTokenCollection.InsertOneAsync(refreshToken);

            var cookieOptions = new CookieOptions
            {
                Path = "/", HttpOnly = true, Expires = DateTime.Now.AddDays(1)
            };

            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        throw new Exception();
    }
}
