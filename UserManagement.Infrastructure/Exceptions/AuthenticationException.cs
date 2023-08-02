using System.Globalization;

namespace UserManagement.Infrastructure.Exceptions
{
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
}