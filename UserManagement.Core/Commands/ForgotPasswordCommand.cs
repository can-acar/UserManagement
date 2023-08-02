using MediatR;

namespace UserManagement.Core.Commands
{
    public class ForgotPasswordCommand : IRequest<ServiceResponse>
    {
        public string Email { get; }

        public ForgotPasswordCommand(string email)
        {
            Email = email;
        }
    }
}