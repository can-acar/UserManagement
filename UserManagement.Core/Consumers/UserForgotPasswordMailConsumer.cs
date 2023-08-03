using MediatR;
using UserManagement.Core.Commands;
using UserManagement.Core.Interfaces;

public class UserForgotPasswordMailConsumer : IConsumer<IUserForgotPasswordMailSendEvent>
{
    private readonly ILogger<UserForgotPasswordMailConsumer> _logger;
    private readonly IMediator _mediator;

    public UserForgotPasswordMailConsumer(ILogger<UserForgotPasswordMailConsumer> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IUserForgotPasswordMailSendEvent> context)
    {
        _logger.LogInformation("Sending forgot password mail to {Email}", context.Message.Email);

        await _mediator.Send(new ForgotPasswordCommand(context.Message.Email));

        _logger.LogInformation("Forgot password mail sent to {Email}", context.Message.Email);

        await Task.CompletedTask;
    }
}