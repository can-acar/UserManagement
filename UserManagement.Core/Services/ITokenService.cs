namespace UserManagement.Core.Services;

public interface ITokenService
{
    string GenerateToken(string username);
    string GenerateToken(object value);
    bool ValidateToken(string token);
    string GetUsernameFromToken(string token);
}