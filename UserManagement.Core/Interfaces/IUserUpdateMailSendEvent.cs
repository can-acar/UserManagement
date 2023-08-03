namespace UserManagement.Core.Interfaces;

public interface IUserUpdateMailSendEvent
{
    Guid UserId { get; }
    string Email { get; }
    string Username { get; }
}