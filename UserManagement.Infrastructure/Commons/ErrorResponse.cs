namespace UserManagement.Infrastructure.Commons
{
    public class ErrorResponse<T> : BaseServiceResponse<T>
    {
        public ErrorResponse(string error, string message = null)
        {
            Success = false;
            Error = error;
            Message = message;
        }
    }
}