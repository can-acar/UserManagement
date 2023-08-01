using UserManagement.Infrastructure.Commons;

namespace UserManagement.API.Services;

public interface IUserService
{
    Task<ServiceResponse> CreateUser(CreateUserCommand user);
    Task UpdateProfile(Guid userId, string username, string email);

    Task UpdatePassword(Guid userId, string newPassword);
}