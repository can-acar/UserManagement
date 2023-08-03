using UserManagement.Core.Commands;
using UserManagement.Core.Events;
using UserManagement.Core.Interfaces;
using UserManagement.Core.Services;

namespace UserManagement.API.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ServiceResponse>
    {
        private readonly ILogger<UpdateUserHandler> _logger;
        private readonly IUserService _userService;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateUserHandler(ILogger<UpdateUserHandler> logger, IPublishEndpoint publishEndpoint, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<ServiceResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[EXECUTING]UpdateUser Detail:{@request}", request);

            var result = await _userService.UpdateUser(request.UserId, request.Username, request.Email);

            _logger.LogInformation("[EXECUTED:SUCCESS]UpdateUser Detail:{@request}", request);


            _logger.LogInformation("[PUBLISHING]UserUpdatedEvent Detail:{@request}", request);

            await _publishEndpoint.Publish<IUserUpdatedEvent>(new
            {
                request.UserId,
                request.Username,
                request.Email
            }, cancellationToken);

            return result;
        }
    }
}