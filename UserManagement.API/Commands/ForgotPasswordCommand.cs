using UserManagement.Infrastructure.Commons;

namespace UserManagement.API.Commands;

public class ForgotPasswordCommand : IRequest<ServiceResponse>
{
    public string Email { get; }

    public ForgotPasswordCommand(string email)
    {
        Email = email;
    }
}