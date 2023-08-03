using MediatR;

namespace UserManagement.Core.Commands;

public class DeactivateUserCommand : IRequest<ServiceResponse>
{
    public Guid UserId { get; }

    public string Username { get; }
    public string Email { get; }

    public DeactivateUserCommand(Guid userId, string username, string email)
    {
        UserId = userId;
        Username = username;
        Email = email;
    }
}