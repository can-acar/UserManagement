using UserManagement.API.Services;
using UserManagement.Core.Interfaces;
using UserManagement.Infrastructure.Commons;
using UserManagement.Infrastructure.Exceptions;

namespace UserManagement.API.Handlers;

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

        //
        await _publishEndpoint.Publish<IActiveUserEvent>(new
        {
            Username = command.Username,
            Password = command.Password,
            Email = command.Email
        }, cancellationToken);

        // await _busControl.Publish<IActiveUserEvent>(new
        // {
        //     Username = command.Username,
        //     Password = command.Password,
        //     Email = command.Email
        // }, cancellationToken);


        _logger.LogInformation("[EXECUTED:SUCCESS]CreateUserHandler.Handle: {Username},Detail:{@command}", command.Username, command);


        return result;
        // }
        // catch (Exception ex)
        // {
        //     _logger.LogError(ex, "Error creating user with username: {Username}", command.Username);
        //
        //     throw new AppException(ex.Message);
        // }
    }
}