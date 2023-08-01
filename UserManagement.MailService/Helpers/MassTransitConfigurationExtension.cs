using UserManagement.Core.Consumers;

namespace UserManagement.MailService.Helpers
{
    public static class MassTransitConfigurationExtension
    {
        public static void UseMassTransitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumer<UserActivationMailConsumer>();

                cfg.UsingRabbitMq((ctx, x) =>
                {
                    x.Host(configuration["RabbitMQ:Host"], h =>
                    {
                        h.Username(configuration["RabbitMQ:Username"]);
                        h.Password(configuration["RabbitMQ:Password"]);
                    });

                    // Event türlerini ve consumer'ları ilişkilendirme
                    x.ReceiveEndpoint("user_action_mail", ep => { ep.ConfigureConsumer<UserActivationMailConsumer>(ctx); });
                });
            });
        }
    }
}