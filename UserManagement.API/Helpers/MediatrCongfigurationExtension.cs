namespace UserManagement.API.Helpers;

public static class MediatrCongfigurationExtension
{
    public static void UseMediatrConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        var assemblies = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(assemblies); });
    }
}