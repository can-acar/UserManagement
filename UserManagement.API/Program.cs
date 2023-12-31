using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using UserManagement.API.Helpers;
using UserManagement.Infrastructure.Middlewares;

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
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions
    {
        AutoRegisterTemplate = true,
        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
        IndexFormat = "usermanagement-api-log-{0:yyyy.MM.dd}",
        CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
        MinimumLogEventLevel = LogEventLevel.Debug
    })
    .CreateLogger();

host.UseSerilog();

// Add services to the container.

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddCors();
services.AddEndpointsApiExplorer();
services.AddOptions();
services.AddHealthChecks();
services.AddDataProtection();


services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Usermanagement.API", Version = "v1"}); });
// 
services.UseValidationConfiguration(configuration);
services.UseMassTransitConfiguration(configuration);
services.UseMediatrConfiguration(configuration);
services.UseServicesConfiguration(configuration);
services.UseDbConfiguration(configuration);


builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed-window", co =>
    {
        co.Window = TimeSpan.FromSeconds(10);
        co.PermitLimit = 10;
        co.QueueLimit = 10;
        co.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

builder.WebHost.UseKestrel();
builder.WebHost.UseIISIntegration();
builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory());

// var certificate = new X509Certificate2(builder.Configuration["Kestrel:Certificates:Default:Path"]!, builder.Configuration["Kestrel:Certificates:Default:Password"]);
//
// builder.WebHost.ConfigureKestrel(serverOptions =>
// {
//     serverOptions.AddServerHeader = false;
//     serverOptions.ConfigureHttpsDefaults(listenOptions => { listenOptions.ServerCertificate = certificate; });
// });


var app = builder.Build();
app.UseSerilogRequestLogging();
// app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger(opt =>
{
    opt.PreSerializeFilters.Add((swagger, httpReq) =>
    {
        var scheme = httpReq.Host.Host.StartsWith("localhost", StringComparison.OrdinalIgnoreCase) ? "http" : "https";
        swagger.Servers = new List<OpenApiServer>() {new OpenApiServer() {Url = $"{scheme}://{httpReq.Host}"}};
    });
});

app.UseSwaggerUI();
// }

app.UseHealthChecks("/health", new HealthCheckOptions {Predicate = _ => true});
app.UseHealthChecks("/healthz", new HealthCheckOptions {Predicate = _ => true, ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse});
app.MapHealthChecks("/health/live", new HealthCheckOptions() {Predicate = _ => false}); // Exclude all checks and return a 200-Ok
app.MapHealthChecks("/health/ready", new HealthCheckOptions() {Predicate = (check) => check.Tags.Contains("ready")});

app.UseSerilogRequestLogging();

app.UseRouting();
app.UseRateLimiter();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed(_ => true));


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", async context => { await context.Response.WriteAsync("Healtly"); });

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