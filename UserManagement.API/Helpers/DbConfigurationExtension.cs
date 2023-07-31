using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using UserManagement.Core.Data;

namespace UserManagement.API.Helpers;

public static class DbConfigurationExtension
{
    public static void UseDbConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserManagementData>(options =>
        {
            options.EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .EnableServiceProviderCaching()
                .UseSqlServer(configuration.GetConnectionString("Db"), options =>
                {
                    options.UseRelationalNulls();
                    options.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                }).ConfigureWarnings(c => c.Log((RelationalEventId.CommandExecuting, LogLevel.Debug)));
        });
    }
}