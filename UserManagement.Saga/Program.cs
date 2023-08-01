// See https://aka.ms/new-console-template for more information

// using MassTransit;
// using Serilog;
// using Serilog.Events;
// using Serilog.Formatting.Elasticsearch;
// using Serilog.Sinks.Elasticsearch;
// using Serilog.Sinks.SystemConsole.Themes;
// using UserManagement.Saga;
//
// Log.Logger = new LoggerConfiguration()
//     .MinimumLevel.Debug() // developer mode
//     .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
//     .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
//     .MinimumLevel.Override("Microsoft.AspNetCore.DataProtection", LogEventLevel.Information)
//     .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Internal.WebHost", LogEventLevel.Information)
//     .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Server.WebListener", LogEventLevel.Information)
//     .Enrich.WithThreadName()
//     .Enrich.WithThreadId()
//     //.Enrich.WithExceptionDetails()
//     .Enrich.WithProperty("ApplicationName", "UserManagement.API")
//     .Enrich.FromLogContext()
//     //.WriteTo.File(@"logs/log-.txt", fileSizeLimitBytes: 3000, rollingInterval: RollingInterval.Day)
//     .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
//     .WriteTo.Elasticsearch(new ElasticsearchSinkOptions
//     {
//         AutoRegisterTemplate = true,
//         AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
//         IndexFormat = "usermanagement-api-log-{0:yyyy.MM.dd}",
//         CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
//         MinimumLogEventLevel = LogEventLevel.Debug
//     })
//     .CreateLogger();
//
// var userState = new UserSaga();
// var repository = new InMemorySagaRepository<UserSagaState>();
//
// var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
// {
//     cfg.Host("localhost", "/", h =>
//     {
//         h.Username("guest");
//         h.Password("guest");
//     });
//
//     cfg.ReceiveEndpoint("user-saga", e =>
//     {
//         e.StateMachineSaga(userState, repository);
//     });
// });
//
//
// bus.StartAsync();
//
// Log.Information("Saga started");
// Console.WriteLine("User saga state machine started..");
// Console.ReadLine();


using System.Reflection;
using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using UserManagement.Saga;

var assemblies = Assembly.GetExecutingAssembly();
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5001");
var host = builder.Host;
var services = builder.Services;
var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();


host.UseSerilog();
services.AddOptions();
services.AddHealthChecks();
//
// services.UseMassTransitConfiguration(configuration);
// services.UseMediatrConfiguration(configuration);
// services.UseServicesConfiguration(configuration);
// services.UseDbConfiguration(configuration);
//
builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory());

var app = builder.Build();
app.UseSerilogRequestLogging();
var userState = new UserSaga();
var repository = new InMemorySagaRepository<UserSagaState>();

var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host("localhost", "/", h =>
    {
        h.Username("guest");
        h.Password("guest");
    });

    cfg.ReceiveEndpoint("user-saga", e => { e.StateMachineSaga(userState, repository); });
});

bus.StartAsync();

app.UseHealthChecks("/health", new HealthCheckOptions {Predicate = _ => true});
app.UseHealthChecks("/healthz", new HealthCheckOptions {Predicate = _ => true, ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse});
Log.Information("Saga started");
Console.WriteLine("User saga state machine started..");
app.Run();