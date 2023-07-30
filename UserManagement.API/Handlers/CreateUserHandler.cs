using UserManagement.Core.Interfaces;
using UserManagement.Infrastructure.Commons;
using UserManagement.Infrastructure.Exceptions;

namespace UserManagement.API.Handlers;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, ServiceResponse>
{
    private readonly ILogger<CreateUserHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateUserHandler(ILogger<CreateUserHandler> logger, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }


    public async Task<ServiceResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Creating user with username: {Username}", command.Username);

            await _publishEndpoint.Publish<IActiveUserEvent>(new
            {
                Username = command.Username,
                Password = command.Password,
                Email = command.Email
            }, cancellationToken);


            _logger.LogInformation("User created successfully with username: {Username}", command.Username);


            return await ServiceResponse.SuccessAsync("User created successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user with username: {Username}", command.Username);

            throw new AppException(ex.Message);
        }
    }
}