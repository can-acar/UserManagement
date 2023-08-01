using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using UserManagement.MailService.Helpers;

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
        IndexFormat = "usermanagement-mailservice-log-{0:yyyy.MM.dd}",
        CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
        MinimumLogEventLevel = LogEventLevel.Debug
    })
    .CreateLogger();

host.UseSerilog();

services.AddOptions();
services.AddHealthChecks();
services.UseServicesConfiguration(configuration);
services.UseMassTransitConfiguration(configuration);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();