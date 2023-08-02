using MediatR;
using UserManagement.Core.Commands;
using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Consumers
{
    public class UserActivationMailConsumer : IConsumer<IUserRegisterActivateMailSendEvent>
    {
        private readonly ILogger<UserActivationMailConsumer> _logger;
        private readonly IMediator _mediator;

        public UserActivationMailConsumer(ILogger<UserActivationMailConsumer> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        public async Task Consume(ConsumeContext<IUserRegisterActivateMailSendEvent> context)
        {
            _logger.LogInformation("Sending activation mail to {Email}", context.Message.Email);

            await _mediator.Send(new ActivateUserAccountCommand(context.Message.Email, context.Message.Username, context.Message.ActivationCode));

            _logger.LogInformation("Activation mail sent to {Email}", context.Message.Email);
            
            await Task.CompletedTask;
        }
    }
}