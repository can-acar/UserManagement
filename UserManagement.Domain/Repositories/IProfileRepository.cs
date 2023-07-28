using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories;

public interface IProfileRepository
{
    Task<Profile?> GetByIdAsync(Guid id);
    Task<Profile?> GetByEmailAsync(string email);
    Task AddAsync(Profile user);
    Task UpdateAsync(Profile user);
    Task DeleteAsync(Guid id);
}

public class ProfileRepository : IProfileRepository
{
    public Task<Profile?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Profile?> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Profile user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Profile user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}