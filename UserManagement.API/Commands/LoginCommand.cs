using UserManagement.Infrastructure.Commons;

namespace UserManagement.API.Commands;

public class LoginCommand : IRequest<ServiceResponse>
{
    public string Username { get; }
    public string Password { get; }
}