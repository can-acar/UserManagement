using MediatR;

namespace UserManagement.Core.Commands
{
    public class ActivateUserAccountCommand : IRequest<ServiceResponse>
    {
        public string Email { get; }
        public string Username { get; }
        public string AktivasyonKodu { get; }

        public ActivateUserAccountCommand(string email, string username, string activationCode)
        {
            Email = email;
            Username = username;
            AktivasyonKodu = activationCode;
        }
    }
}