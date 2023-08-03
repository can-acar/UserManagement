namespace UserManagement.Core.Interfaces;

public interface IUserDeactiveMailSendEvent
{
    string Email { get; set; }
    Guid UserId { get; set; }
    string Username { get; set; }
}