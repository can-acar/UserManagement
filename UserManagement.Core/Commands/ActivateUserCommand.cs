using MediatR;

namespace UserManagement.Core.Commands;

public class ActivateUserCommand : IRequest<ServiceResponse>
{
    public string ActivationCode { get; }

    public ActivateUserCommand(string activationCode)
    {
        ActivationCode = activationCode;
    }
}