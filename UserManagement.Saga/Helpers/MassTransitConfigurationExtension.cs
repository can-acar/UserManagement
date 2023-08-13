namespace UserManagement.Saga.Helpers
{
    public static class MassTransitConfigurationExtension
    {
        public static void UseMassTransitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(cfg =>
            {
                var userSaga = new UserSaga();
                var repository = new InMemorySagaRepository<UserSagaState>();

                cfg.UsingRabbitMq((ctx, x) =>
                {
                    x.Host(configuration["RabbitMQ:Host"], h =>
                    {
                        h.Username(configuration["RabbitMQ:Username"]);
                        h.Password(configuration["RabbitMQ:Password"]);
                    });

                    x.ReceiveEndpoint("user-saga", ep =>
                    {
                        ep.PrefetchCount = 8;
                        ep.UseMessageRetry(r => r.Interval(5, 100));
                        ep.StateMachineSaga(userSaga, repository);
                    });

                    x.ReceiveEndpoint("user-forgot-password-saga",
                        ep =>
                        {
                            ep.PrefetchCount = 8;
                            ep.UseMessageRetry(r => r.Interval(5, 100));
                            ep.StateMachineSaga(new UserForgotPasswordSaga(), new InMemorySagaRepository<UserForgotPasswordSagaState>());
                        });
                });
            });
        }
    }
}