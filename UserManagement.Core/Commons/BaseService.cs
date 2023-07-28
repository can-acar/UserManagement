namespace UserManagement.Core.Commons;

public class BaseService
{
    public Task<ServiceResponse> Success(string message = null)
    {
        throw new NotImplementedException();
    }

    public static ServiceResponse Error(string error, string message = null)
    {
        throw new NotImplementedException();
    }
}