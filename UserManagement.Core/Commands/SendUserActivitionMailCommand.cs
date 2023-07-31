using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Commands;

public class SendUserActivitionMailCommand : IActiveUserEvent
{
    public Guid UserId { get; set; }
    public string Email { get; set; }

    public SendUserActivitionMailCommand(Guid userId, string email)
    {
        UserId = userId;
        Email = email;
    }
}