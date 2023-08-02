namespace UserManagement.API.Requests
{
    public class UpdatePasswordRequest
    {
        public Guid UserId { get; set; }
        public string NewPassword { get; set; }
    }
}