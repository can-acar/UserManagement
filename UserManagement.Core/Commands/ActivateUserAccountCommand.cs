using MediatR;

namespace UserManagement.Core.Commands
{
    public class ActivateUserAccountCommand : IRequest<ServiceResponse>
    {
        public string Email { get; }

        public ActivateUserAccountCommand(string email)
        {
            Email = email;
        }
    }
}