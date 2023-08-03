using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Consumers;

public class ForgotPasswordConsumer : IConsumer<IForgotPasswordEvent>
{
    private readonly ILogger<ForgotPasswordConsumer> _logger;

    public ForgotPasswordConsumer(ILogger<ForgotPasswordConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<IForgotPasswordEvent> context)
    {
        _logger.LogInformation("Forgot password for user {Email}", context.Message.Email);

        return Task.CompletedTask;
    }
}