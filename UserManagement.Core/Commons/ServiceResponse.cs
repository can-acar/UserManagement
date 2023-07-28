namespace UserManagement.Core.Commons;

public class ServiceResponse
{
    public object Data { get; set; }

    public string Message { get; set; }

    public bool Status { get; set; }

    // public static ServiceResponse<T> Success(T data)
    // {
    //     return new ServiceResponse {Status = true, Data = data};
    // }
    //
    // public static ServiceResponse<T> Error(string errorMessage)
    // {
    //     return new ServiceResponse<T> {Status = false, ErrorMessage = errorMessage};
    // }
    public static async Task<ServiceResponse> Success(string message = null)
    {
        return await Task.FromResult(new ServiceResponse
        {
            Status = true,
            Message = message
        });
    }


    public static async Task<ServiceResponse> Success(string message, bool status, object data)
    {
        return await Task.FromResult(new ServiceResponse
        {
            Status = status,
            Message = message,
            Data = data
        });
    }
}

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

public class SuccessResponse<T> : BaseServiceResponse<T>
{
    public SuccessResponse(T data)
    {
        Success = true;
        Data = data;
    }
}

public class ErrorResponse<T> : BaseServiceResponse<T>
{
    public ErrorResponse(string error, string message = null)
    {
        Success = false;
        Error = error;
        Message = message;
    }
}