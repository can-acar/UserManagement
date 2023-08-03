using UserManagement.Core.Commands;

namespace UserManagement.Core.Services
{
    public interface IUserService
    {
        Task<(string, ServiceResponse)> CreateUser(CreateUserCommand user);
        Task<ServiceResponse> UpdateUser(Guid userId, string userName, string email);
        Task<ServiceResponse> UpdatePassword(Guid userId, string password);
        Task<ServiceResponse> ActivateUser(string activationCode);

        Task<ServiceResponse> DeactivateUser(Guid userId);
    }
}