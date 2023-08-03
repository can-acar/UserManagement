namespace UserManagement.Core.Interfaces;

public interface IUserForgotPasswordMailSendEvent
{
    string Email { get; set; }
}