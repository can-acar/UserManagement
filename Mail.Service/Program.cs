// See https://aka.ms/new-console-template for more information

using System.Reflection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using UserManagement.API.Helpers;


var assemblies = Assembly.GetExecutingAssembly();
var builder = WebApplication.CreateBuilder(args);
var host = builder.Host;
var services = builder.Services;
var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


services.AddOptions();
services.AddHealthChecks();
//
services.UseMassTransitConfiguration(configuration);
services.UseMediatrConfiguration(configuration);
services.UseServicesConfiguration(configuration);
services.UseDbConfiguration(configuration);
//
builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory());

var app = builder.Build();
app.UseSerilogRequestLogging();

app.UseHealthChecks("/health", new HealthCheckOptions {Predicate = _ => true});
app.UseHealthChecks("/healthz", new HealthCheckOptions {Predicate = _ => true, ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse});
app.MapHealthChecks("/health/live", new HealthCheckOptions() {Predicate = _ => false}); // Exclude all checks and return a 200-Ok
app.MapHealthChecks("/health/ready", new HealthCheckOptions() {Predicate = (check) => check.Tags.Contains("ready")});