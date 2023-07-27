namespace UserManagement.API.Handlers;

public class UpdateProfileRequest
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}