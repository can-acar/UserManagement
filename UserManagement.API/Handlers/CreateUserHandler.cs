using UserManagement.Core.Interfaces;

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

            await _publishEndpoint.Publish<ICreateUserEvent>(new
            {
                Username = command.Username,
                Password = command.Password
            }, cancellationToken);

            // get the response from  Identity.Service 


            return new ServiceResponse
            {
                Status = true,
                Message = "User created successfully"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user with username: {Username}", command.Username);
            throw;
        }
    }
}