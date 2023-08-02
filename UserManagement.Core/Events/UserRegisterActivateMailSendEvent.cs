namespace UserManagement.Core.Events;

public class UserRegisterActivateMailSendEvent
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}