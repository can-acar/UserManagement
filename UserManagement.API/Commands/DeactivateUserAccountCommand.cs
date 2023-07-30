using UserManagement.Infrastructure.Commons;

namespace UserManagement.API.Commands;

public class DeactivateUserAccountCommand : IRequest<ServiceResponse>
{
    public Guid UserId { get; }

    public DeactivateUserAccountCommand(Guid userId)
    {
        UserId = userId;
    }
}