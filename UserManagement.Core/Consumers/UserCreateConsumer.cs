using UserManagement.Core.Events;
using UserManagement.Core.Repositories;

namespace UserManagement.Core.Consumers
{
    public class UserCreateConsumer : IConsumer<UserCreateEvent>
    {
        private readonly ILogger<UserCreateConsumer> _logger;
        private readonly IUserRepository _userRepository;

        public UserCreateConsumer(ILogger<UserCreateConsumer> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }


        public Task Consume(ConsumeContext<UserCreateEvent> context)
        {
            _logger.LogInformation("Creating user {Email}", context.Message.Email);

            return Task.CompletedTask;
        }
    }
}