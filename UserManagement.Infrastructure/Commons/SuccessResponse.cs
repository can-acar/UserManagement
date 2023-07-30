namespace UserManagement.Infrastructure.Commons;

public class SuccessResponse<T> : BaseServiceResponse<T>
{
    public SuccessResponse(T data)
    {
        Success = true;
        Data = data;
    }
}