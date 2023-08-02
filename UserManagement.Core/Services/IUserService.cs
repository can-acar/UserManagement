using UserManagement.Core.Commands;

namespace UserManagement.Core.Services
{
    public interface IUserService
    {
        Task<(string, ServiceResponse)> CreateUser(CreateUserCommand user);
        Task UpdateProfile(Guid userId, string username, string email);

        Task UpdatePassword(Guid userId, string newPassword);
        Task<ServiceResponse> ActivateUser(string activationCode);
    }
}