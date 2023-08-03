using UserManagement.Core.Events;
using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Consumers
{
    public class UserUpdateConsumer : IConsumer<IUserUpdatedEvent>
    {
        private readonly ILogger<UserUpdateConsumer> _logger;


        public UserUpdateConsumer(ILogger<UserUpdateConsumer> logger)
        {
            _logger = logger;
        }


        public Task Consume(ConsumeContext<IUserUpdatedEvent> context)
        {
            _logger.LogInformation("Updating user {Email}", context.Message.Email);

            return Task.CompletedTask;
        }
    }
}