namespace api.Processes.Tokens.Interfaces
{
    public interface IDeleteRefreshTokenProcess
    {
        public Task Delete(string userId);
    }
}