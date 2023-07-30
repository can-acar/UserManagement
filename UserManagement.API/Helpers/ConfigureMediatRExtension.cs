namespace UserManagement.API.Helpers;

public static class ConfigureMediatRExtension
{
    public static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Program).Assembly);
        //services.AddMassTransitHostedService();
    }
}