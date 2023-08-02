using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Core.Commands;
using UserManagement.Core.Events;
using UserManagement.Core.Interfaces;
using UserManagement.Core.Models;
using UserManagement.Core.Services;

namespace UserManagement.API.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, ServiceResponse>
    {
       
        private readonly ILogger<CreateUserHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBusControl _busControl;
        private readonly IUserService _userService;


        public CreateUserHandler(ILogger<CreateUserHandler> logger, IPublishEndpoint publishEndpoint, IBusControl busControl, IUserService userService)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _busControl = busControl;
            _userService = userService;
        }


        public async Task<ServiceResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            // try
            // {
            _logger.LogInformation("[EXECUTING]CreateUserHandler.Handle: {Username},Detail:{@command}", command.Username, command);

            var result = await _userService.CreateUser(command);

            await _publishEndpoint.Publish<IUserRegisteredEvent>(new UserRegisteredEvent
            {
                UserId = Guid.NewGuid(),
                Username = command.Username,
                Password = command.Password,
                Email = command.Email
            }, cancellationToken);


            _logger.LogInformation("[EXECUTED:SUCCESS]CreateUserHandler.Handle: {Username},Detail:{@command}", command.Username, command);


            return result;
        }
    }
}