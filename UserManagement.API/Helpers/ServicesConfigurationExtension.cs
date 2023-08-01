using UserManagement.API.Repositories;
using UserManagement.API.Services;
using UserManagement.Infrastructure.Commons;

namespace UserManagement.API.Helpers;

public static class ServicesConfigurationExtension
{
    public static void UseServicesConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddSingleton<RabbitMqOptions>();
        services.AddSingleton<IIdentityService, IdentityService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
    }
}