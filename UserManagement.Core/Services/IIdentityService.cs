namespace UserManagement.Core.Services
{
    public interface IIdentityService
    {
        Task ForgotPassword(string email);


        Task<ServiceResponse> Login(string username, string password);
    }
}