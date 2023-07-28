namespace UserManagement.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }

    public string Password { get; set; }


    public virtual Profile Profile { get; set; }

    public Guid? ProfileId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsLocked { get; set; }
    public bool IsActive { get; set; }
    public bool IsPasswordChangeRequired { get; set; }
}