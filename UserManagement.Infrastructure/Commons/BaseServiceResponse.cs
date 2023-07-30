namespace UserManagement.Infrastructure.Commons;

public abstract class BaseServiceResponse<T>
{
    public bool Success { get; protected set; }
    public string Error { get; protected set; }
    public string Message { get; protected set; }
    public T Data { get; protected set; }

    public static BaseServiceResponse<T> SuccessResponse(T data)
    {
        return new SuccessResponse<T>(data);
    }

    public static BaseServiceResponse<T> ErrorResponse(string error, string message = null)
    {
        return new ErrorResponse<T>(error, message);
    }
}