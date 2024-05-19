using api.Services.Interfaces;

namespace api.Services
{
    public static class ServicesDepInj
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
            return services;
        }
    }
}
