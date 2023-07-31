using UserManagement.Infrastructure.Exceptions;

namespace UserManagement.API.Services;

public class IdentityService : IIdentityService
{
    public Task ForgotPassword(string email)
    {
        throw new AppException("Forgot password request failed.");
    }
}

public interface IIdentityService
{
    Task ForgotPassword(string email);
    
    
}