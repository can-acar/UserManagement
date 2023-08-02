namespace UserManagement.MailService
{
    public class Worker : IHostedService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBusControl _busControl;

        public Worker(ILogger<Worker> logger, IBusControl busControl)
        {
            _logger = logger;
            _busControl = busControl;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            _busControl.StartAsync(cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker stopping at: {time}", DateTimeOffset.Now);

            _busControl.StopAsync(cancellationToken);

            return Task.CompletedTask;
        }
    }
}