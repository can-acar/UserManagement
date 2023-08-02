using Microsoft.AspNetCore.Identity;

namespace UserManagement.Infrastructure.Commons;

public class PasswordManager
{
    // Şifre hashlemek için kullanılacak PasswordHasher örneği
    // private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
    //
    // // Kullanıcı şifresini hashleyen metod
    // public string HashPassword(string password)
    // {
    //     return _passwordHasher.HashPassword( password);
    // }
    //
    // // Kullanıcının girdiği şifreyi, veritabanında saklanan hash ile karşılaştıran metod
    // public bool VerifyPassword(string hashedPassword, string providedPassword)
    // {
    //     var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
    //     return passwordVerificationResult == PasswordVerificationResult.Success;
    // }
}