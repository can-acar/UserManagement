using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using UserManagement.API.Data;
using UserManagement.Core.Sagas;
using UserManagement.Infrastructure.Extensions;


namespace UserManagement.API.Helpers;

public static class MassTransitConfigurationExtension
{
    public static void UseMassTransitConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddSagaStateMachine<UserSagaStateMachine, UserSagaState>()
                .EntityFrameworkRepository(o => o.AddDbContext<DbContext, UserManagementData>((provider, builder) =>
                {
                    builder.UseSqlServer(configuration.GetConnectionString("DB"), options =>
                    {
                        options.UseRelationalNulls();
                        options.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                    }).ConfigureWarnings(c => c.Log((RelationalEventId.CommandExecuting, LogLevel.Debug)));
                }));

           

            x.UseRabbitMq(cfg =>
            {
                cfg.ConfigureEndpoints("user_registration_queue",ers =>
                {
                    ers.ConfigureConsumer<UserRegistrationConsumer>(x);
                }
            });
        });

        //services.AddMassTransitHostedService();
    }
}