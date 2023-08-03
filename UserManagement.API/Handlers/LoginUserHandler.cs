using UserManagement.Core.Queries;
using UserManagement.Core.Services;
using UserManagement.Infrastructure.Exceptions;

namespace UserManagement.API.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserQuery, ServiceResponse>
    {
        private readonly ILogger<LoginUserHandler> _logger;

        private readonly IIdentityService _identityService;

        public LoginUserHandler(ILogger<LoginUserHandler> logger, IIdentityService identityService)
        {
            _logger = logger;
            _identityService = identityService;
        }

        public async Task<ServiceResponse> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var response = await _identityService.Login(request.Username, request.Password);

            return response;
        }
    }
}