using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mail.Service.Helpers;

public static class MediatrConfigurationExtension
{
    public static void UseMediatrConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        var assemblies = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(assemblies); });
    }
}