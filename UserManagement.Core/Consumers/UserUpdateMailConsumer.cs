using MediatR;
using UserManagement.Core.Commands;
using UserManagement.Core.Interfaces;

public class UserUpdateMailConsumer : IConsumer<IUserUpdateMailSendEvent>
{
    private readonly ILogger<UserUpdateMailConsumer> _logger;
    private readonly IMediator _mediator;

    public UserUpdateMailConsumer(ILogger<UserUpdateMailConsumer> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IUserUpdateMailSendEvent> context)
    {
        _logger.LogInformation("Sending update mail to {Email}", context.Message.Email);

        await _mediator.Send(new UpdateUserAccountCommand(context.Message.UserId, context.Message.Email, context.Message.Username));

        _logger.LogInformation("Update mail sent to {Email}", context.Message.Email);

        await Task.CompletedTask;
    }
}