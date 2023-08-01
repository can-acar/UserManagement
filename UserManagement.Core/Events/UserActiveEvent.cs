using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Events
{
    public class UserActiveEvent : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}