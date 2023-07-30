namespace UserManagement.API.Requests;

public class UpdateUserRequest
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}