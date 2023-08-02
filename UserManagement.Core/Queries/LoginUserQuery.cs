using MediatR;

namespace UserManagement.Core.Queries
{
    public class LoginUserQuery : IRequest<ServiceResponse>
    {
        public string Username { get; }
        public string Password { get; }

        public LoginUserQuery(string requestUsername, string requestPassword)
        {
            Username = requestUsername;
            Password = requestPassword;
        }
    }
}