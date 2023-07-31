namespace UserManagement.Core.Interfaces;

public interface IDeleteUserEvent
{
    public Guid UserId { get; set; }
}