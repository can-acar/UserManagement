namespace UserManagement.API.Services;

public class UserService : IUserService
{
    public ILogger<UserService> Logger { get; }

    public UserService(ILogger<UserService> logger)
    {
        Logger = logger;
    }

    public Task<bool> CreateUser(CreateUserCommand user)
    {
        throw new NotImplementedException();
    }
}

public interface IUserService
{
    Task<bool> CreateUser(CreateUserCommand user);
}