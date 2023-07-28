using System.Globalization;

namespace UserManagement.Core.Exceptions;

public class AuthenticationException : Exception
{
    public AuthenticationException() : base()
    {
    }

    public AuthenticationException(string message) : base(message)
    {
    }

    public AuthenticationException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}