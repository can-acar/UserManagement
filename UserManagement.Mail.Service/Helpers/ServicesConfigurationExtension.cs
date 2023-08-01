namespace UserManagement.Mail.Service.Helpers
{
    public static class ServicesConfigurationExtension
    {
        public static void UseServicesConfiguration(this IServiceCollection services, IConfiguration configurationManager)
        {
            services.AddSingleton<RabbitMqOptions>();
            // services.AddSingleton<IIdentityService, IdentityService>();
            // services.AddScoped<IUserRepository, UserRepository>();
            // services.AddScoped<IUserService, UserService>();
        }
    }
}