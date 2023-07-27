namespace UserManagement.API.Commands;

public class UpdateUserPasswordCommand
{
    public Guid UserId { get; }
    public string Password { get; }

    public UpdateUserPasswordCommand(Guid requestUserId, string requestPassword)
    {
        UserId = requestUserId;
        Password = requestPassword;
    }
}

public class UpdateUserProfileCommand
{
    public Guid UserId { get; }
    public string Username { get; }
    public string Email { get; }

    public UpdateUserProfileCommand(Guid requestUserId, string requestUsername, string requestEmail)
    {
        UserId = requestUserId;
        Username = requestUsername;
        Email = requestEmail;
    }
}