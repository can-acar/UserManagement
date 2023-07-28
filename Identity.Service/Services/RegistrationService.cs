using UserManagement.Infrastructure.Commons;
using UserManagement.Infrastructure.Interfaces;

namespace Identity.Service.Services;

public interface IRegistrationService
{
    Task<ServiceResponse> RegisterUserAsync(string username, string email, string password);
}

public class RegistrationService : IRegistrationService
{
    public async Task<ServiceResponse> RegisterUserAsync(string username, string email, string password)
    {
        return await ServiceResponse.Success(
            message: "User registered successfully",
            status: true,
            data: new
            {
                Username = username,
                Email = email,
                Password = password
            }
        );
    }
}