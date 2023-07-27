namespace UserManagement.API.Handlers;

public class UpdatePasswordRequest
{
    public Guid UserId { get; set; }
    public string NewPassword { get; set; }
}