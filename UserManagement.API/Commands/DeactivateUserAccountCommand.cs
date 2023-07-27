namespace UserManagement.API.Commands;

public class DeactivateUserAccountCommand
{
    public Guid UserId { get; }

    public DeactivateUserAccountCommand(Guid requestUserId)
    {
        UserId = requestUserId;
    }
}