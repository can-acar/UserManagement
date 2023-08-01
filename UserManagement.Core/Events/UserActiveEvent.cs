using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Events
{
    public class UserActiveEvent : IUserActiveEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}