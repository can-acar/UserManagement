using MediatR;

namespace UserManagement.Core.Commands
{
    public class CreateUserCommand : IRequest<ServiceResponse>
    {
        public string Username { get; }
        public string Password { get; }

        public string Email { get; }

        public CreateUserCommand(string requestUsername, string requestPassword, string email)
        {
            Username = requestUsername;
            Password = requestPassword;
            Email = email;
        }
    }
}