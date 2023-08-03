using MediatR;

namespace UserManagement.Core.Commands;

public class UpdateUserAccountCommand : IRequest<ServiceResponse>
{
    public Guid UserId { get; }

    public string Username { get; }
    public string Email { get; }

    public UpdateUserAccountCommand(Guid userId, string username, string email)
    {
        UserId = userId;
        Username = username;
        Email = email;
    }
}