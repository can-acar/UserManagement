namespace UserManagement.Core.Interfaces;

public interface IUserDeactivatedEvent
{
    Guid UserId { get; set; }
}