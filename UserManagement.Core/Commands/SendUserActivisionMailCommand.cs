using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Commands
{
    public class SendUserActivisionMailCommand : IUserActiveEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }

        public SendUserActivisionMailCommand(Guid userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }
}