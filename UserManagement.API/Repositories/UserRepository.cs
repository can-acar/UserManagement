using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Data;
using UserManagement.Core.Models;

namespace UserManagement.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManagementData _userManagementData;

    public UserRepository(UserManagementData userManagementData)
    {
        _userManagementData = userManagementData;
    }

  
    public async Task<bool> HasUser(User user)
    {
        return await _userManagementData.Users.AnyAsync(x => x.Username == user.Username || x.Email == user.Email);
    }


    public async Task<User> CreateUser(User user)
    {
        await _userManagementData.Database.BeginTransactionAsync();

        await _userManagementData.Users.AddAsync(user);

        await _userManagementData.SaveChangesAsync();

        await _userManagementData.Database.CommitTransactionAsync();

        return user;
    }
}