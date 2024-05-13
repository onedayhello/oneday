using System.IdentityModel.Tokens.Jwt;

namespace api.Processes.Tokens.Interfaces
{
    public interface IGenerateTokenProcess
    {
        public JwtSecurityToken Generate(string userId);
    }
}