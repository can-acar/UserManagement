namespace UserManagement.Core.Interfaces
{
    public interface IUserCreateEvent
    {
        public Guid UserId { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Email { get; set; }
    }
}