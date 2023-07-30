using UserManagement.Infrastructure.Commons;

namespace UserManagement.API.Commands;

public class ActivateUserAccountCommand : IRequest<ServiceResponse>
{
    public string Email { get; }

    public ActivateUserAccountCommand(string email)
    {
        Email = email;
    }
}