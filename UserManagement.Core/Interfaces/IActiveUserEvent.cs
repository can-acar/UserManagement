namespace UserManagement.Core.Interfaces
{
    public interface IUserActiveEvent
    {
        public Guid UserId { get; set; }
        string Email { get; set; }
    }
}