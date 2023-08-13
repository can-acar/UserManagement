using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Events;

public class UserForgotPasswordMailSendEvent : IUserForgotPasswordMailSendEvent
{
    public string Email { get; set; }
}