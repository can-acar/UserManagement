using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<User> GetByUsernameAsync(string username);
    Task<User> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new List<User>();

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        return await Task.FromResult(_users.FirstOrDefault(u => u.Username == username));
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await Task.FromResult(_users.FirstOrDefault(u => u.Email == email));
    }

    public async Task AddAsync(User user)
    {
        _users.Add(user);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(User user)
    {
        // Update user in the list
        await Task.CompletedTask;
    }
}