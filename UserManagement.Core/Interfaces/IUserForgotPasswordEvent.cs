namespace UserManagement.Core.Interfaces;

public interface IUserForgotPasswordEvent
{
    string Email { get; set; }
}