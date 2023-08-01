using UserManagement.Core.Commands;

namespace UserManagement.API.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ServiceResponse>
    {
        private readonly ILogger<UpdateUserHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateUserHandler(ILogger<UpdateUserHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public Task<ServiceResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}