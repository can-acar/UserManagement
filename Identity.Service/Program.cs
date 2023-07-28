// See https://aka.ms/new-console-template for more information

using Identity.Service;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json",
            optional: true,
            reloadOnChange: true);
        config.AddEnvironmentVariables();
        if (args != null)
        {
            config.AddCommandLine(args);
        }
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMassTransit(x => { x.UsingRabbitMq((context, cfg) => { cfg.Host("rabbitmq://localhost"); }); });

        services.AddHostedService<Worker>();
    })
    .ConfigureLogging((hostingContext, logging) =>
    {
        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
        logging.AddDebug();
        logging.AddEventSourceLogger();
    })
    .Build()
    .Run();