using UserManagement.Core.Commands;
using UserManagement.Core.Interfaces;
using UserManagement.Core.Services;

namespace UserManagement.API.Handlers;

public class DeactivateUserHandler : IRequestHandler<DeactivateUserAccountCommand, ServiceResponse>
{
    private readonly ILogger<DeactivateUserHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IBusControl _busControl;
    private readonly IUserService _userService;

    public DeactivateUserHandler(ILogger<DeactivateUserHandler> logger, IPublishEndpoint publishEndpoint, IBusControl busControl, IUserService userService)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
        _busControl = busControl;
        _userService = userService;
    }

    public async Task<ServiceResponse> Handle(DeactivateUserAccountCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[EXECUTING]DeactivateUser Detail:{@request}", request);

        var result = await _userService.DeactivateUser(request.UserId);

        _logger.LogInformation("[EXECUTED:SUCCESS]DeactivateUser Detail:{@request}", request);

        _logger.LogInformation("[PUBLISHING]UserDeactivatedEvent Detail:{@request}", request);

        await _publishEndpoint.Publish<IUserDeactivatedEvent>(new
        {
            request.UserId
        }, cancellationToken);

        return result;
    }
}