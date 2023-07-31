using UserManagement.API.Data;
using UserManagement.API.Models;

namespace UserManagement.API.Repositories;

public interface IUserRepository
{
}

public class UserRepository : IUserRepository
{
    private readonly UserManagementData _userManagementData;

    public UserRepository(UserManagementData userManagementData)
    {
        _userManagementData = userManagementData;
    }

    public async Task<User> GetUserById(Guid userId)
    {
        return await Task.FromResult(new User());
    }
}