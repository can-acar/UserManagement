using UserManagement.Core.Models;

namespace UserManagement.API.Repositories;

public interface IUserRepository
{
    Task<bool> HasUser(User user);
    Task<User> CreateUser(User user);
}