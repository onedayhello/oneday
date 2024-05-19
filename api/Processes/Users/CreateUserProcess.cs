using Data.Models;
using Data.Repositories.Interfaces;
using api.Processes.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace api.Processes.Users
{
    public class CreateUserProcess : ICreateUserProcess
    {
        private readonly IUserRepository _userRepository;
        public CreateUserProcess(IUserRepository userRepository) 
        {
           _userRepository = userRepository;
        }

        private string HashPassword (string password)
        {
            var salt = new byte[16];
            RandomNumberGenerator.Create().GetBytes(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA1);
            var hash = pbkdf2.GetBytes(20);
            var hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        public async Task<User> Create(UserCreateRequest userCreateRequest)
        {
            var user = new User
            {
                Username = userCreateRequest.Username,
                Password = HashPassword(userCreateRequest.Password),
            };

            await _userRepository.CreateUserAsync(user);

            return user;
        }
    }
}
