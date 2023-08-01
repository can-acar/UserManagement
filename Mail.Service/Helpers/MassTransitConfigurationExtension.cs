using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UserManagement.Core.Consumers;
using UserManagement.Core.Data;
using UserManagement.Core.Sagas;
using UserManagement.Infrastructure.Extensions;

namespace Mail.Service.Helpers;

public static class MassTransitConfigurationExtension
{
    public static void UseMassTransitConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddSagaStateMachine<UserSagaStateMachine, UserSagaState>()
                .EntityFrameworkRepository(o => o.AddDbContext<DbContext, UserManagementData>((provider, builder) =>
                {
                    builder.UseSqlServer(configuration.GetConnectionString("SagaMachine"), options =>
                    {
                        options.UseRelationalNulls();
                        options.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                    }).ConfigureWarnings(c => c.Log((RelationalEventId.CommandExecuting, LogLevel.Debug)));
                }));

            // x.AddConsumer<UserCreateConsumer>().Endpoint(ep => ep.Name = "user_create_queue");
            // x.AddConsumer<UserActivationMailConsumer>().Endpoint(ep => ep.Name = "user_activation_mail_queue");
            x.SetConsumer<UserCreateConsumer>("user_create_queue", cfg =>
            {
                cfg.UseRateLimit(10, TimeSpan.FromSeconds(10));
            });
            x.SetConsumer<UserActivationMailConsumer>("user_activation_mail_queue", cfg =>
            {
                cfg.UseRateLimit(10, TimeSpan.FromSeconds(10));
            });


            // x.AddEndpoint("user_registration_queue", cfg =>
            // {
            //     cfg.ConfigureConsumer<UserRegistrationConsumer>(services);
            // });
            //
            //
            x.UseRabbitMq();
        });

        services.AddHostedService<UserManagement.API.Worker>();
        //services.AddMassTransitHostedService();
    }
}