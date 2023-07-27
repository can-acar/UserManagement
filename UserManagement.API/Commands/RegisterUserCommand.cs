namespace UserManagement.API.Commands;

public class RegisterUserCommand : IRequest<bool>
{
    public string Username { get; }
    public string Email { get; }
    public string Password { get; }

    public RegisterUserCommand(string requestUsername, string requestEmail, string requestPassword)
    {
        Username = requestUsername;
        Email = requestEmail;
        Password = requestPassword;
    }
}