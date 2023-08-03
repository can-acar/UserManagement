namespace UserManagement.Core.Services;

public interface IMailProvider
{
    Task SendMail(string to, string mail, string subject, string body);
}