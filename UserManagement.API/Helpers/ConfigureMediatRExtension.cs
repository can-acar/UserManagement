using FluentValidation;
using UserManagement.API.Repositories;
using UserManagement.API.Services;

namespace UserManagement.API.Helpers;

public static class MediatrCongfigurationExtension
{
    public static void UseMediatRConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        var assemblies = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(assemblies); });
    }
}

public static class ServicesConfigurationExtension
{
    public static void UseServicesConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddSingleton<IIdentityService, IdentityService>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}