namespace api.Processes.Users.Interfaces
{
    public interface IUsernameInUseProcess
    {
        public Task<bool> Check(string username);
    }
}