using UserManagement.Infrastructure.Commons;

namespace UserManagement.Infrastructure;

public interface IBaseService
{
    ServiceResponse Success(string message = null);
    ServiceResponse Error(string error, string message = null);
}