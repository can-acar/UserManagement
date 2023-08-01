using MediatR;

namespace UserManagement.Core.Commands
{
    public class LoginCommand : IRequest<ServiceResponse>
    {
        public string Username { get; }
        public string Password { get; }
    }
}