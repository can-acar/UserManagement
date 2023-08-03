using UserManagement.Core.Commands;
using UserManagement.Core.Interfaces;
using UserManagement.Core.Services;

namespace UserManagement.API.Handlers
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, ServiceResponse>
    {
        private readonly ILogger<ForgotPasswordHandler> _logger;
        private readonly IIdentityService _identityService;
        private readonly IPublishEndpoint _publishEndpoint;

        public ForgotPasswordHandler(ILogger<ForgotPasswordHandler> logger, IIdentityService identityService, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _identityService = identityService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<ServiceResponse> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Forgot password request received with email: {Email}", request.Email);

            await _identityService.ForgotPassword(request.Email);

            _logger.LogInformation("Forgot password request completed successfully with email: {Email}", request.Email);

            await _publishEndpoint.Publish<IUserRegisteredEvent>(new
            {
                Username = request.Email,
                Password = request.Email,
                Email = request.Email
            }, cancellationToken);

            return await Task.FromResult(new ServiceResponse());
        }
    }
}