using UserManagement.Core.Consumers;
using UserManagement.Infrastructure.Extensions;

namespace UserManagement.Mail.Service.Helpers
{
    public static class MassTransitConfigurationExtension
    {
        public static void UseMassTransitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.SetConsumer<UserActivationMailConsumer>("user-saga");

                x.UseRabbitMq();
            });

            //services.AddHostedService<Worker>();
            //services.AddMassTransitHostedService();
        }
    }
}