namespace Profile.Management.Service.Repositories;

public interface IProfileRepository
{
    Task<Models.Profile?> GetByIdAsync(Guid id);
    Task<Models.Profile?> GetByEmailAsync(string email);
    Task AddAsync(Models.Profile user);
    Task UpdateAsync(Models.Profile user);
    Task DeleteAsync(Guid id);
}

public class ProfileRepository : IProfileRepository
{
    public Task<Models.Profile?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Profile?> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Models.Profile user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Models.Profile user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}