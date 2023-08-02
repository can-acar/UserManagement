using MediatR;

namespace UserManagement.Core.Commands
{
    public class ActivateUserAccountCommand : IRequest<ServiceResponse>
    {
        public string Email { get; }
        public string Username { get; }

        public ActivateUserAccountCommand(string email, string username)
        {
            Email = email;
            Username = username;
        }
    }
}