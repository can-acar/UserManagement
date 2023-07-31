using Microsoft.AspNetCore.Http;
using UserManagement.Infrastructure.Exceptions;
using AuthenticationException = System.Security.Authentication.AuthenticationException;

namespace UserManagement.Infrastructure.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case AppException:
                    // custom application error
                    _logger.LogError("AppException:{message}, Details: {Error}", error.Message, error);
                    response.StatusCode = (int) HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException:
                    // not found error
                    _logger.LogError("KeyNotFoundException:{message}, Details: {Error}", error.Message, error);
                    response.StatusCode = (int) HttpStatusCode.NotFound;
                    break;
                case AuthenticationException:
                    // pattern ErrorType: {message}, Details: {details}
                    _logger.LogError("AuthenticationException:{message}, Details: {Error}", error.Message, error);


                    response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    break;
                default:
                    // unhandled error
                    _logger.LogError(error, error.Message);
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonConvert.SerializeObject(new {error = true, isSuccess = false, message = error.Message});

            await response.WriteAsync(result);
        }
    }
}