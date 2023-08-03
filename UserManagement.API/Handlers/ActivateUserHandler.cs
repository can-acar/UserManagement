using UserManagement.Core.Commands;
using UserManagement.Core.Services;

namespace UserManagement.API.Handlers;

public class ActivateUserHandler : IRequestHandler<ActivateUserCommand, ServiceResponse>
{
    private readonly ILogger<ActivateUserHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IBusControl _busControl;
    private readonly IUserService _userService;


    public ActivateUserHandler(ILogger<ActivateUserHandler> logger, IPublishEndpoint publishEndpoint, IBusControl busControl, IUserService userService)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
        _busControl = busControl;
        _userService = userService;
    }

    public async Task<ServiceResponse> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[EXECUTING]ActivateUser Detail:{@request}", request);

        var result = await _userService.ActivateUser(request.ActivationCode);

        _logger.LogInformation("[EXECUTED:SUCCESS]ActivateUser Detail:{@request}", request);

        return result;
    }
}