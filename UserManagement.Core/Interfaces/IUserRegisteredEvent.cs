namespace UserManagement.Core.Interfaces;

public interface IUserRegisteredEvent
{
    Guid UserId { get; }
    string Username { get; }
    string Email { get; }

    string Password { get; }
}