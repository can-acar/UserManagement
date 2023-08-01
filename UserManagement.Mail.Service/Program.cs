using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.SystemConsole.Themes;
using UserManagement.Mail.Service.Helpers;

var assemblies = Assembly.GetExecutingAssembly();
var builder = WebApplication.CreateBuilder(args);
var host = builder.Host;
var services = builder.Services;
var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // developer mode
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore.DataProtection", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Internal.WebHost", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Server.WebListener", LogEventLevel.Information)
    .Enrich.WithThreadName()
    .Enrich.WithThreadId()
    //.Enrich.WithExceptionDetails()
    .Enrich.WithProperty("ApplicationName", "UserManagement.API")
    .Enrich.FromLogContext()
    //.WriteTo.File(@"logs/log-.txt", fileSizeLimitBytes: 3000, rollingInterval: RollingInterval.Day)
    .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions
    {
        AutoRegisterTemplate = true,
        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
        IndexFormat = "usermanagement-mail-log-{0:yyyy.MM.dd}",
        CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
        MinimumLogEventLevel = LogEventLevel.Debug
    })
    .CreateLogger();

host.UseSerilog();

// Add services to the container.

services.AddOptions();

services.AddHealthChecks();
services.UseServicesConfiguration(configuration);
services.UseMassTransitConfiguration(configuration);
// services.UseMediatrConfiguration(configuration);

builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory());

var app = builder.Build();

try
{
    app.Run();
    return 0;
}
catch (Exception ex)
{
    var type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, "Unhandled exception");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}