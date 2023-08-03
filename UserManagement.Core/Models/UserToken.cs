namespace UserManagement.Core.Models;

public partial class UserToken
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Token { get; set; }

    // public string RefreshToken { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiredAt { get; set; }

    public virtual Users User { get; set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiredAt;
}