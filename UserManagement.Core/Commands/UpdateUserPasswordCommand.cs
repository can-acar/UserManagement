using MediatR;

namespace UserManagement.Core.Commands
{
    public class UpdateUserPasswordCommand : IRequest<ServiceResponse>
    {
        public Guid UserId { get; }
        public string Password { get; }

        public UpdateUserPasswordCommand(Guid userId, string password)
        {
            UserId = userId;
            Password = password;
        }
    }
}