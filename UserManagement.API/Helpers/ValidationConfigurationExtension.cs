using FluentValidation.AspNetCore;
using MediatR.Extensions.FluentValidation.AspNetCore;
using UserManagement.Infrastructure.Commons;

namespace UserManagement.API.Helpers;

public static class ValidationConfigurationExtension
{
    public static void UseValidationConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddFluentValidationAutoValidation();
    }
}