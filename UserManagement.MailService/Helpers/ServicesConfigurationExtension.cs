using UserManagement.Core.Services;

namespace UserManagement.MailService.Helpers
{
    public static class ServicesConfigurationExtension
    {
        public static void UseServicesConfiguration(this IServiceCollection services, IConfiguration configurationManager)
        {
            services.AddSingleton<RabbitMqOptions>();
            services.AddSingleton<IEmailRenderService, EmailRenderService>();
            services.AddSingleton<IMailProvider, MailProvider>();
            // services.AddSingleton<IIdentityService, IdentityService>();
            // services.AddScoped<IUserRepository, UserRepository>();
            // services.AddScoped<IUserService, UserService>();
            //
            // services.AddAuthentication(options =>
            // {
            //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            // }).AddJwtBearer("Bearer", options =>
            // {
            //     options.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateIssuer = true,
            //         ValidateAudience = true,
            //         ValidateLifetime = true,
            //         ValidateIssuerSigningKey = true,
            //         ValidIssuer = configurationManager["Jwt:Issuer"],
            //         ValidAudience = configurationManager["Jwt:Audience"],
            //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationManager["Jwt:Key"]))
            //     };
            // });
        }
    }
}