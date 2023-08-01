using UserManagement.Infrastructure.Exceptions;

namespace UserManagement.Core.Services
{
    public class IdentityService : IIdentityService
    {
        public Task ForgotPassword(string email)
        {
            throw new AppException("Forgot password request failed.");
        }
    }
}