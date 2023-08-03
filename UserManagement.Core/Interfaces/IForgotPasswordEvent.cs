namespace UserManagement.Core.Interfaces;

public interface IForgotPasswordEvent
{
    string Email { get; set; }
}