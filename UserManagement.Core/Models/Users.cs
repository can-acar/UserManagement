namespace UserManagement.Core.Models;

public partial class Users
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public bool IsActive { get; set; }

    public bool IsLocked { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<UserActivations> UserActivations { get; set; } = new List<UserActivations>();
}
