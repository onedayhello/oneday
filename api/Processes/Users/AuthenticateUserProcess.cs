using Data.Models;
using Data.Repositories.Interfaces;
using api.Processes.Users.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Cryptography;

namespace api.Processes.Users
{
    public class AuthenticateUserProcess : IAuthenticateUserProcess
    {
        private readonly IUserRepository _userRepository;

        public AuthenticateUserProcess(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        private bool CheckPassword(string password, string hashedPassword)
        {
            /* Fetch the stored value */
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA1);
            byte[] hash = pbkdf2.GetBytes(20);
            if (Enumerable.Range(0, 20).Any(i => hashBytes[i + 16] != hash[i]))
            {
                return false;
            }
            return true;
        }

        public async Task<User?> Authenticate(UserLoginRequest userLoginRequest)
        {
            var user = await _userRepository.GetUserByUsernameAsync(userLoginRequest.Username);

            var passwordIsValid = CheckPassword(userLoginRequest.Password, user.Password);

            if (user == null || !passwordIsValid)
            {
                return null;
            }

            return user;
        }
    }
}
