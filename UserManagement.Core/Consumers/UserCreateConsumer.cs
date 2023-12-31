﻿using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Consumers
{
    public class UserCreateConsumer : IConsumer<IUserRegisteredEvent>
    {
        private readonly ILogger<UserCreateConsumer> _logger;


        public UserCreateConsumer(ILogger<UserCreateConsumer> logger)
        {
            _logger = logger;
        }


        public Task Consume(ConsumeContext<IUserRegisteredEvent> context)
        {
            _logger.LogInformation("Creating user {Email}", context.Message.Email);

            return Task.CompletedTask;
        }
    }
}