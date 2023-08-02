namespace UserManagement.Core.Services
{
    public interface IIdentityService
    {
        Task ForgotPassword(string email);
    
    
    }
}