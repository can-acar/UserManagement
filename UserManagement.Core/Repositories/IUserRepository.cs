using UserManagement.Core.Models;

namespace UserManagement.Core.Repositories
{
    public interface IUserRepository
    {
        Task<bool> HasUser(User user);
        Task<User> CreateUser(User user);
    }
}