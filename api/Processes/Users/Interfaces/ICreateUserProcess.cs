using Data.Models;

namespace api.Processes.Users.Interfaces
{
    public interface ICreateUserProcess
    {
        public Task<User> Create(UserCreateRequest userCreateRequest);
    }
}