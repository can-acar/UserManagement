using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Events;

public class UserRegisteredEvent : IUserRegisteredEvent
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}