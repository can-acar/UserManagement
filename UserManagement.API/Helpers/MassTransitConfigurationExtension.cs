using UserManagement.Core.Consumers;


namespace UserManagement.API.Helpers
{
    public static class MassTransitConfigurationExtension
    {
        public static void UseMassTransitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumer<UserCreateConsumer>();
                cfg.AddConsumer<UserUpdateConsumer>();
                cfg.AddConsumer<UserDeactivateConsumer>();
                cfg.AddConsumer<ForgotPasswordConsumer>();
                //
                cfg.UsingRabbitMq((ctx, ec) =>
                {
                    ec.Host(configuration["RabbitMQ:Host"], h =>
                    {
                        h.Username(configuration["RabbitMQ:Username"]);
                        h.Password(configuration["RabbitMQ:Password"]);
                    });


                    ec.PrefetchCount = 16;

                    ec.UseRetry(r =>
                    {
                        r.Handle<ArgumentNullException>(); // handle null argument exception olunca kuyrukta tekrar gönder
                        r.Incremental(5, TimeSpan.FromMinutes(10),
                            TimeSpan.FromMinutes(10)); // 5 defa tekrar gönder
                        r.Ignore(typeof(InvalidOperationException),
                            typeof(InvalidCastException)); // ignore exception
                    });

                    ec.UseMessageRetry(r => r.Immediate(5));

                    ec.UseCircuitBreaker(c =>
                    {
                        c.TripThreshold = 15;
                        c.ActiveThreshold = 10;
                        c.ResetInterval = TimeSpan.FromMinutes(5);
                        c.TrackingPeriod = TimeSpan.FromMinutes(1);
                    });

                    ec.UseRateLimit(1000, TimeSpan.FromMinutes(1));


                    ec.ReceiveEndpoint("user-register-queue", ep => { ep.ConfigureConsumer<UserCreateConsumer>(ctx); });

                    ec.ReceiveEndpoint("user-update-queue", ep => { ep.ConfigureConsumer<UserUpdateConsumer>(ctx); });

                    ec.ReceiveEndpoint("user-deactivate-queue", ep => { ep.ConfigureConsumer<UserDeactivateConsumer>(ctx); });

                    ec.ReceiveEndpoint("forgot-password-queue", ep => { ep.ConfigureConsumer<ForgotPasswordConsumer>(ctx); });
                });
            });
        }
    }
}