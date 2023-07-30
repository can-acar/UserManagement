using UserManagement.Infrastructure.Commons;

namespace UserManagement.Infrastructure.Extensions;

public static class MassTransitExtension
{
    private static readonly HashSet<Action<IBusFactoryConfigurator, IBusRegistrationContext>> BuildBefore = new();

    private static readonly HashSet<Action<IBusRegistrationConfigurator>> BuildBeforeRequestClient = new();


    public static IBusRegistrationConfigurator SetConsumer<TConsumer>(this IBusRegistrationConfigurator configurator, Action<IReceiveEndpointConfigurator> config = null)
        where TConsumer : class, IConsumer
    {
        Action<IBusFactoryConfigurator, IBusRegistrationContext> action = (cfg, context) =>
        {
            var queueName = KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(TConsumer).Name)
                .Replace("consumer", "queue");

            cfg.ReceiveEndpoint(queueName,
                ConfigureEndpoint<TConsumer>(config, context) ?? throw new InvalidOperationException());
        };

        BuildBefore?.Add(action);

        return configurator;
    }


    public static IBusRegistrationConfigurator SetConsumer<TConsumer>(this IBusRegistrationConfigurator configurator, string queueName,
        Action<IReceiveEndpointConfigurator> config = null)
        where TConsumer : class, IConsumer

    {
        Action<IBusFactoryConfigurator, IBusRegistrationContext> action = (cfg, context) => { cfg.ReceiveEndpoint(queueName, ConfigureEndpoint<TConsumer>(config, context)); };

        BuildBefore?.Add(action);

        return configurator;
    }

    public static IBusRegistrationConfigurator SetConsumer<TConsumer>(this IBusRegistrationConfigurator configurator,
        string queueName) where TConsumer : class, IConsumer
    {
        Action<IBusFactoryConfigurator, IBusRegistrationContext> action = (cfg, context) =>
        {
            if (queueName != null)
            {
                cfg.ReceiveEndpoint(queueName,
                    ec =>
                    {
                        ec.PrefetchCount = 16;

                        ec.UseRetry(r =>
                        {
                            r.Handle<ArgumentNullException>(); // handle null argument exception olunca kuyrukta tekrar gönder
                            r.Incremental(5, TimeSpan.FromMinutes(10),
                                TimeSpan.FromMinutes(10)); // 5 defa tekrar gönder
                            r.Ignore(typeof(InvalidOperationException),
                                typeof(InvalidCastException)); // ignore exception
                        });

                        ec.UseMessageRetry(r => r.Immediate(5));

                        ec.UseCircuitBreaker(c =>
                        {
                            c.TripThreshold = 15;
                            c.ActiveThreshold = 10;
                            c.ResetInterval = TimeSpan.FromMinutes(5);
                            c.TrackingPeriod = TimeSpan.FromMinutes(1);
                        });

                        ec.UseRateLimit(1000, TimeSpan.FromMinutes(1));

                        //ec.Consumer<TConsumer>(context);

                        ec.ConfigureConsumer<TConsumer>(context); //, c => { c.UseRetry(r => r.Immediate(5)); });
                    });
            }
            else
            {
                var queueName = KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(TConsumer).Name)
                    .Replace("consumer", "queue");

                cfg.ReceiveEndpoint(queueName,
                    ec =>
                    {
                        ec.PrefetchCount = 16;

                        ec.UseRetry(r =>
                        {
                            r.Handle<ArgumentNullException>(); // handle null argument exception olunca kuyrukta tekrar gönder
                            r.Incremental(5, TimeSpan.FromMinutes(10),
                                TimeSpan.FromMinutes(10)); // 5 defa tekrar gönder
                            r.Ignore(typeof(InvalidOperationException),
                                typeof(InvalidCastException)); // ignore exception
                        });

                        ec.UseMessageRetry(r => r.Immediate(5));

                        ec.UseCircuitBreaker(c =>
                        {
                            c.TripThreshold = 15;
                            c.ActiveThreshold = 10;
                            c.ResetInterval = TimeSpan.FromMinutes(5);
                            c.TrackingPeriod = TimeSpan.FromMinutes(1);
                        });

                        ec.UseRateLimit(1000, TimeSpan.FromMinutes(1));

                        ec.ConfigureConsumer<TConsumer>(context); //, c => { c.UseRetry(r => r.Immediate(5)); });
                    });
            }
        };

        BuildBefore?.Add(action);


        return configurator;
    }


    public static IReceiveEndpointConfigurator ConfigConsumer<TConsumer>(this IReceiveEndpointConfigurator configurator,
        Action<IReceiveEndpointConfigurator> config = null) where TConsumer : class, IConsumer
    {
        Action<IBusFactoryConfigurator, IBusRegistrationContext> action = (cfg, context) =>
        {
            var queueName = KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(TConsumer).Name)
                .Replace("consumer", "queue");

            cfg.ReceiveEndpoint(queueName, ConfigureEndpoint<TConsumer>(config, context));
        };

        BuildBefore?.Add(action);

        return configurator;
    }


    public static IReceiveEndpointConfigurator ConfigConsumer<TConsumer>(this IReceiveEndpointConfigurator configurator, string queueName,
        Action<IReceiveEndpointConfigurator> config = null) where TConsumer : class, IConsumer
    {
        Action<IBusFactoryConfigurator, IBusRegistrationContext> action = (cfg, context) =>
        {
            // var queueName = KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(TConsumer).Name)
            //                                               .Replace("consumer", "queue");

            cfg.ReceiveEndpoint(queueName, ConfigureEndpoint<TConsumer>(config, context));
        };

        BuildBefore?.Add(action);

        return configurator;
    }

    public static IReceiveEndpointConfigurator ConfigConsumer<TConsumer>(this IReceiveEndpointConfigurator configurator, string queueName) where TConsumer : class, IConsumer
    {
        Action<IBusFactoryConfigurator, IBusRegistrationContext> action = (cfg, context) =>
        {
            if (queueName != null)
            {
                cfg.ReceiveEndpoint(queueName,
                    ec =>
                    {
                        ec.PrefetchCount = 16;

                        ec.UseRetry(r =>
                        {
                            r.Handle<ArgumentNullException>(); // handle null argument exception olunca kuyrukta tekrar gönder
                            r.Incremental(5, TimeSpan.FromMinutes(10),
                                TimeSpan.FromMinutes(10)); // 5 defa tekrar gönder
                            r.Ignore(typeof(InvalidOperationException),
                                typeof(InvalidCastException)); // ignore exception
                        });

                        ec.UseMessageRetry(r => r.Immediate(5));

                        ec.UseCircuitBreaker(c =>
                        {
                            c.TripThreshold = 15;
                            c.ActiveThreshold = 10;
                            c.ResetInterval = TimeSpan.FromMinutes(5);
                            c.TrackingPeriod = TimeSpan.FromMinutes(1);
                        });

                        ec.UseRateLimit(1000, TimeSpan.FromMinutes(1));

                        ec.Consumer<TConsumer>(context, c => { c.UseRetry(r => r.Immediate(5)); });

                        ec.ConfigureConsumer<TConsumer>(context); //, c => { c.UseRetry(r => r.Immediate(5)); });
                    });
            }
            else
            {
                var queueName = KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(TConsumer).Name)
                    .Replace("consumer", "queue");

                cfg.ReceiveEndpoint(queueName,
                    ec =>
                    {
                        ec.PrefetchCount = 16;

                        ec.UseRetry(r =>
                        {
                            r.Handle<ArgumentNullException>(); // handle null argument exception olunca kuyrukta tekrar gönder
                            r.Incremental(5, TimeSpan.FromMinutes(10),
                                TimeSpan.FromMinutes(10)); // 5 defa tekrar gönder
                            r.Ignore(typeof(InvalidOperationException),
                                typeof(InvalidCastException)); // ignore exception
                        });

                        ec.UseMessageRetry(r => r.Immediate(5));

                        ec.UseCircuitBreaker(c =>
                        {
                            c.TripThreshold = 15;
                            c.ActiveThreshold = 10;
                            c.ResetInterval = TimeSpan.FromMinutes(5);
                            c.TrackingPeriod = TimeSpan.FromMinutes(1);
                        });

                        ec.UseRateLimit(1000, TimeSpan.FromMinutes(1));

                        ec.ConfigureConsumer<TConsumer>(context); //, c => { c.UseRetry(r => r.Immediate(5)); });
                    });
            }
        };

        BuildBefore?.Add(action);


        return configurator;
    }

    /// <summary>
    ///   RabbitMq kullanmak için
    ///     services.AddMassTransit(x =>
    ///         {
    ///             
    ///             x.AddConsumer<CreateUserConsumer>();
    ///             x.AddConsumer<UpdateUserConsumer>();
    ///             x.AddConsumer<DeleteUserConsumer>();
    ///             x.UseRabbitMq();
    ///         });
    ///     
    /// </summary>
    /// <param name="configurator"></param>
    public static void UseRabbitMq(this IBusRegistrationConfigurator configurator)
    {
        configurator.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            var configuration = context.GetService<IConfiguration>();
            var endPointConfiguration = context.GetService<IBusRegistrationConfigurator>();
            var mqConfig = new RabbitMqOptions(configuration);
            cfg.UseExceptionLogger();


            cfg.Host(mqConfig?.RabbitMqUri, h =>
            {
                h.Username(mqConfig?.RabbitMqUserName);
                h.Password(mqConfig?.RabbitMqPassword);
                h.Heartbeat(TimeSpan.FromMinutes(1));
                h.UseCluster(configurator =>
                {
                    //configurator.Node()
                });
            });

            foreach (var action in BuildBefore) action?.Invoke(cfg, context);


            foreach (var action in BuildBeforeRequestClient) action?.Invoke(endPointConfiguration);
        }));
    }

    public static void UseRabbitMq(this IBusRegistrationConfigurator configurator, Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator> configure = null)
    {
        configurator.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            var configuration = context.GetService<IConfiguration>();
            var endPointConfiguration = context.GetService<IBusRegistrationConfigurator>();
            var mqConfig = new RabbitMqOptions(configuration);
            cfg.UseExceptionLogger();

            cfg.Host(mqConfig.RabbitMqUri,
                h =>
                {
                    h.Username(mqConfig.RabbitMqUserName);
                    h.Password(mqConfig.RabbitMqPassword);
                    h.Heartbeat(TimeSpan.FromMinutes(1));
                    // h.UseCluster(configurator =>
                    // {
                    //     //configurator.Node()
                    // });
                });


            configure?.Invoke(context, cfg);


            foreach (var action in BuildBefore) action?.Invoke(cfg, context);


            foreach (var action in BuildBeforeRequestClient) action?.Invoke(endPointConfiguration);
        }));
    }


    public static void UseMemory(this IBusRegistrationConfigurator configurator,
        Action<IBusRegistrationContext,
            IInMemoryBusFactoryConfigurator> configure = null)
    {
        configurator.AddBus(context => Bus.Factory.CreateUsingInMemory(cfg =>
        {
            var configuration = context.GetService<IConfiguration>();
            //var endPointConfiguration = context.GetService<IContainerBuilderBusConfigurator>();
            //var mqConfig = new RabbitMqOptions(configuration);
            cfg.UseExceptionLogger();


            configure?.Invoke(context, cfg);


            foreach (var action in BuildBefore) action?.Invoke(cfg, context);


            foreach (var action in BuildBeforeRequestClient) action?.Invoke(configurator);
        }));
    }

    private static Action<IReceiveEndpointConfigurator> ConfigureEndpoint<TConsumer>(Action<IReceiveEndpointConfigurator> config, IRegistrationContext context)
        where TConsumer : class, IConsumer
    {
        config = (ec) =>
        {
            ec.Consumer<TConsumer>(context, c => { c.UseRetry(r => r.Immediate(5)); });

            // ec.ConfigureConsumer<TConsumer>(context);
        };

        return config;
    }
}