using UserManagement.Core.Commons;

namespace UserManagement.Core.Interfaces;

public interface IBaseService
{
    ServiceResponse Success(string message = null);
    ServiceResponse Error(string error, string message = null);
}