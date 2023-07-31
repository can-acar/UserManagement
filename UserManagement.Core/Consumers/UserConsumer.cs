using UserManagement.Core.Commands;

namespace UserManagement.Core.Consumers;

public class UserConsumer
{
}

public class UserActivationMailConsumer : IConsumer<SendUserActivitionMailCommand>
{
    private readonly ILogger<UserActivationMailConsumer> _logger;

    public UserActivationMailConsumer(ILogger<UserActivationMailConsumer> logger)
    {
        _logger = logger;
    }


    public Task Consume(ConsumeContext<SendUserActivitionMailCommand> context)
    {
        _logger.LogInformation("Sending activation mail to {Email}", context.Message.Email);

        return Task.CompletedTask;
    }
}