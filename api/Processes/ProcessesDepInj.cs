using api.Processes.Tokens.Interfaces;
using api.Processes.Tokens;
using api.Processes.Users.Interfaces;
using api.Processes.Users;

namespace api.Processes
{
    public static class ProcessesDepInj
    {
        public static IServiceCollection AddProcesses(this IServiceCollection services)
        {
            services.AddScoped<IUsernameInUseProcess, UsernameInUseProcess>();
            services.AddScoped<ICreateUserProcess, CreateUserProcess>();
            services.AddScoped<IAuthenticateUserProcess, AuthenticateUserProcess>();

            services.AddScoped<IGenerateTokenProcess, GenerateTokenProcess>();
            services.AddScoped<IDeleteRefreshTokenProcess, DeleteRefreshTokenProcess>();
            services.AddScoped<IGenerateRefreshTokenProcess, GenerateRefreshTokenProcess>();
            return services;
        }
    }
}
