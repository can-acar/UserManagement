using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

namespace UserManagement.API.Helpers;

public static class ValidationConfigurationExtension
{
    public static void AddValidationConfiguration(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });


        services.AddFluentValidationAutoValidation();
    }
}