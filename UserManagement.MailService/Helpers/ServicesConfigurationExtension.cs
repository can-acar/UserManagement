using UserManagement.Core.Services;

namespace UserManagement.MailService.Helpers
{
    public static class ServicesConfigurationExtension
    {
        public static void UseServicesConfiguration(this IServiceCollection services, IConfiguration configurationManager)
        {
            services.AddSingleton<RabbitMqOptions>();
            services.AddSingleton<IEmailRenderService, EmailRenderService>();
            services.AddSingleton<IMailProvider, MailProvider>();
          
        }
    }
}