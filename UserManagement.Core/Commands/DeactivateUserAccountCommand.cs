using MediatR;

namespace UserManagement.Core.Commands
{
    public class DeactivateUserAccountCommand : IRequest<ServiceResponse>
    {
        public Guid UserId { get; }

        public DeactivateUserAccountCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}