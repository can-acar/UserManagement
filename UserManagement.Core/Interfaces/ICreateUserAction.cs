namespace UserManagement.Core.Interfaces;

public interface ICreateUserAction
{
    string Username { get; set; }
    string Password { get; set; }

    string Email { get; set; }
}