using api.Processes.Tokens;
using api.Processes.Tokens.Interfaces;
using api.Processes.Users;
using api.Processes.Users.Interfaces;

namespace api.Processes
{
    public class ProcessesDI
    {
        public static void RegisterProcesses(IServiceCollection services)
        {
            services.AddScoped<IUsernameInUseProcess, UsernameInUseProcess>();
            services.AddScoped<ICreateUserProcess, CreateUserProcess>();
            services.AddScoped<IAuthenticateUserProcess, AuthenticateUserProcess>();

            services.AddScoped<IGenerateTokenProcess, GenerateTokenProcess>();
            services.AddScoped<IDeleteRefreshTokenProcess, DeleteRefreshTokenProcess>();
            services.AddScoped<IGenerateRefreshTokenProcess, GenerateRefreshTokenProcess>();
        }
    }
}
