using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Events
{
    public class UserCreateEvent : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}