using UserManagement.API.Sagas;


namespace UserManagement.API.Helpers;

public static class MassTransitConfigurationExtension
{
    public static void ConfigureMasstransit(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddSagaStateMachine<ProfileSagaStateMachine, ProfileSagaState>()
                .InMemoryRepository();

            x.AddSagaStateMachine<UserSagaStateMachine, UserSagaState>()
                .InMemoryRepository();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq://localhost");
                cfg.ConfigureEndpoints(context);
            });
        });

        //services.AddMassTransitHostedService();

        services.AddMediatR(typeof(Program).Assembly);
    }
}