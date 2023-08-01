namespace UserManagement.Core.Interfaces
{
    public interface ISendUserActivationMailEvent
    {
        public Guid UserId { get; set; }
        string Email { get; set; }
    }
}