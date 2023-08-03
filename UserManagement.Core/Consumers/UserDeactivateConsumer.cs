using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Consumers
{
    public class UserDeactivateConsumer : IConsumer<IUserDeactivatedEvent>
    {
        private readonly ILogger<UserDeactivateConsumer> _logger;


        public UserDeactivateConsumer(ILogger<UserDeactivateConsumer> logger)
        {
            _logger = logger;
        }


        public Task Consume(ConsumeContext<IUserDeactivatedEvent> context)
        {
            _logger.LogInformation("Deactivating user {UserId}", context.Message.UserId);

            return Task.CompletedTask;
        }
    }
}