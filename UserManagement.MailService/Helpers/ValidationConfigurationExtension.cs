using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR.Extensions.FluentValidation.AspNetCore;

namespace UserManagement.MailService.Helpers
{
    public static class ValidationConfigurationExtension
    {
        public static void UseValidationConfiguration(this IServiceCollection services, IConfiguration configurationManager)
        {
            var assemblies = Assembly.GetExecutingAssembly();
            services.AddFluentValidationAutoValidation();
            // services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            //services.AddScoped<IValidator<CreateUserRequest>, RegisterUserRequestValidator>();
            services.AddValidatorsFromAssembly(assemblies);
        }
    }
}