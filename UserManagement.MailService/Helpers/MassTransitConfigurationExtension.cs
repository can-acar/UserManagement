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
                cfg.AddConsumer<UserUpdateMailConsumer>();
                cfg.AddConsumer<UserDeactiveMailConsumer>();
                cfg.AddConsumer<UserForgotPasswordMailConsumer>();


                cfg.UsingRabbitMq((ctx, x) =>
                {
                    x.Host(configuration["RabbitMQ:Host"], h =>
                    {
                        h.Username(configuration["RabbitMQ:Username"]);
                        h.Password(configuration["RabbitMQ:Password"]);
                    });

                    // Event türlerini ve consumer'ları ilişkilendirme
                    x.ReceiveEndpoint("user-register-activation-queue", ep => { ep.ConfigureConsumer<UserActivationMailConsumer>(ctx); });

                    x.ReceiveEndpoint("user-update-query-queue", ep => { ep.ConfigureConsumer<UserUpdateMailConsumer>(ctx); });
                    //
                    x.ReceiveEndpoint("user-deactivate-query-queue", ep => { ep.ConfigureConsumer<UserDeactiveMailConsumer>(ctx); });
                    //
                    x.ReceiveEndpoint("user-forgot-password-query-queue", ep => { ep.ConfigureConsumer<UserForgotPasswordMailConsumer>(ctx); });
                });
            });
        }
    }
}