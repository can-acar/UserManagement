using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Events;

public class ForgotPasswordEvent : IUserForgotPasswordEvent
{
    public string Email { get; set; }
}