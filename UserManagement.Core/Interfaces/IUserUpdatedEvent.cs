namespace UserManagement.Core.Interfaces;

public interface IUserUpdatedEvent
{
    Guid UserId { get; set; }
    string Username { get; set; }
    string Email { get; set; }
}