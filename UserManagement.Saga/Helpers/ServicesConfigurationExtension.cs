namespace UserManagement.Saga.Helpers
{
    public static class ServicesConfigurationExtension
    {
        public static void UseServicesConfiguration(this IServiceCollection services, IConfiguration configurationManager)
        {
            services.AddSingleton<RabbitMqOptions>();
        }
    }
}