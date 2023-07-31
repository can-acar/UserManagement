using UserManagement.Infrastructure.Commons;
using UserManagement.Infrastructure.Exceptions;

namespace UserManagement.API.Handlers;

public class LoginUserHandler : IRequestHandler<LoginUserQuery, ServiceResponse>
{
    private readonly ILogger<LoginUserHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public LoginUserHandler(ILogger<LoginUserHandler> logger, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public Task<ServiceResponse> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        throw new AuthenticationException("Invalid username or password.");
    }
}