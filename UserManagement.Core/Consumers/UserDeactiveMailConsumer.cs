using MediatR;
using UserManagement.Core.Commands;
using UserManagement.Core.Interfaces;

public class UserDeactiveMailConsumer : IConsumer<IUserDeactiveMailSendEvent>
{
    private readonly ILogger<UserDeactiveMailConsumer> _logger;
    private readonly IMediator _mediator;

    public UserDeactiveMailConsumer(ILogger<UserDeactiveMailConsumer> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IUserDeactiveMailSendEvent> context)
    {
        _logger.LogInformation("Sending deactivation mail to {Email}", context.Message.Email);
        
        await _mediator.Send(new DeactivateUserCommand(context.Message.UserId, context.Message.Email, context.Message.Username));

        _logger.LogInformation("Deactivation mail sent to {Email}", context.Message.Email);

        await Task.CompletedTask;
    }
}