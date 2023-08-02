using UserManagement.Core.Consumers;

namespace UserManagement.Saga.Helpers
{
    public static class MassTransitConfigurationExtension
    {
        public static void UseMassTransitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumer<UserCreateConsumer>();

                cfg.UsingRabbitMq((ctx, x) =>
                {
                    x.Host(configuration["RabbitMQ:Host"], h =>
                    {
                        h.Username(configuration["RabbitMQ:Username"]);
                        h.Password(configuration["RabbitMQ:Password"]);
                    });

                    // Event türlerini ve consumer'ları ilişkilendirme
                    x.ReceiveEndpoint("user", ep =>
                    {
                        ep.Consumer<UserCreateConsumer>(ctx);
                    });
                });
            });
        }
    }
}