namespace UserManagement.Domain.Services;

public interface IRegistrationService
{
}

public class RegistrationService : IRegistrationService
{
    public async Task RegisterUserAsync(string username, string email, string password)
    {
    }
}