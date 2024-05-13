using api.Services.Interfaces;

namespace api.Services
{
    public class ServicesDI
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
        }
    }
}
