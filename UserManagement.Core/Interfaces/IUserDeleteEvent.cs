namespace UserManagement.Core.Interfaces
{
    public interface IUserDeleteEvent
    {
        public Guid UserId { get; set; }
    }
}