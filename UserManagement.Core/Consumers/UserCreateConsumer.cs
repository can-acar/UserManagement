using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Consumers;

public class UserCreateConsumer : IConsumer<ICreateUserEvent>
{
    private readonly ILogger<UserCreateConsumer> _logger;

    public UserCreateConsumer(ILogger<UserCreateConsumer> logger)
    {
        _logger = logger;
    }


    public Task Consume(ConsumeContext<ICreateUserEvent> context)
    {
        _logger.LogInformation("Creating user {Email}", context.Message.Email);

        throw new NotImplementedException();
    }
}