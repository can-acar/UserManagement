namespace UserManagement.Core.Events;

public class UserActivatedEvent
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
}