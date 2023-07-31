namespace UserManagement.API.Services;

public interface IUserService
{
    Task<bool> CreateUser(CreateUserCommand user);
    Task UpdateProfile(Guid userId, string username, string email);

    Task UpdatePassword(Guid userId, string newPassword);
}