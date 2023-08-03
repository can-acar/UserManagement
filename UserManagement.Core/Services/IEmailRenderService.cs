namespace UserManagement.Core.Services;

public interface IEmailRenderService
{
    Task<string> RenderEmailTemplate(string username, string activationLink, string companyName);
    Task<string> RenderUpdateEmailTemplate(string username, string companyName);
    Task<string> RenderDeactivationEmailTemplate(string username, string companyName);
}