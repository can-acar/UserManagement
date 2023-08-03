using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Events;

public class UserUpdatedEvent : IUserUpdatedEvent
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}