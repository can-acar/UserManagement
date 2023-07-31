namespace UserManagement.API.Services;

public interface IIdentityService
{
    Task ForgotPassword(string email);
    
    
}