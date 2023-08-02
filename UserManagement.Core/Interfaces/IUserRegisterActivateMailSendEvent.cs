namespace UserManagement.Core.Interfaces;

public interface IUserRegisterActivateMailSendEvent
{
    Guid UserId { get; }
    string Username { get; }
    string Email { get; }

    string ActivationCode { get; }
}