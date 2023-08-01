using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Mail.Service;

public class Worker : IHostedService
{
    private readonly IBusControl _busControl;

    public Worker(IBusControl busControl)
    {
        _busControl = busControl;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _busControl.StartAsync(cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _busControl.StopAsync(cancellationToken);

        return Task.CompletedTask;
    }
}