using api.Processes.Tokens.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Processes.Tokens
{
    public class GenerateTokenProcess : IGenerateTokenProcess
    {
        private readonly IConfiguration _config;

        public GenerateTokenProcess(IConfiguration config)
        {
            _config = config;
        }

        public JwtSecurityToken Generate(string userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: new[] {
                new Claim("id", userId)
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return token;
        }
    }
}
