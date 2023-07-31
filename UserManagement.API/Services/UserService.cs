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

    public Task UpdateProfile(Guid userId, string username, string email)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePassword(Guid userId, string newPassword)
    {
        throw new NotImplementedException();
    }
}