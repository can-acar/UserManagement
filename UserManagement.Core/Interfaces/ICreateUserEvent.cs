namespace UserManagement.Core.Interfaces;

public interface ICreateUserEvent
{
    string Username { get; set; }
    string Password { get; set; }
}