using MediatR;

namespace UserManagement.API.Queries;

public class LoginUserQuery : IRequest<string>
{
    public string Username { get; }
    public string Password { get; }

    public LoginUserQuery(string requestUsername, string requestPassword)
    {
        Username = requestUsername;
        Password = requestPassword;
    }
}