using MediatR;

namespace UserManagement.Core.Commands
{
    public class UpdateUserCommand : IRequest<ServiceResponse>
    {
        public Guid UserId { get; }

        public string Username { get; }
        public string Email { get; }

        public UpdateUserCommand(Guid userId, string username, string email)
        {
            UserId = userId;
            Username = username;
            Email = email;
        }
    }
}