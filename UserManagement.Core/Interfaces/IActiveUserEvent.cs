namespace UserManagement.Core.Interfaces;

public interface IActiveUserEvent
{
    public Guid UserId { get; set; }
    string Email { get; set; }
}