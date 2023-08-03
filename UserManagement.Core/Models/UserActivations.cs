namespace UserManagement.Core.Models;

public partial class UserActivations
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string ActivationCode { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime ExpirationDate { get; set; }

    public bool IsExpired => ExpirationDate < DateTime.Now;

    public bool IsUsed => UpdatedAt != null;

    public virtual Users User { get; set; }
}