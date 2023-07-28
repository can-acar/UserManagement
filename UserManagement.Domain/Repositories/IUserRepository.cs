using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByUsernameAsync(string username);
    Task<User> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}

public class UserRepository : IUserRepository
{
    public Task<User?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }
}