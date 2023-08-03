using Microsoft.AspNetCore.Http;
using UserManagement.Infrastructure.Exceptions;


namespace UserManagement.Infrastructure.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

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


                switch (error.GetType().Name)
                {
                    case nameof(AppException):
                        // custom application error
                        var appEx = (AppException) error;
                        _logger.LogError("AppException:{message}, Details: {Error}", appEx.Message, appEx);
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        break;
                    case nameof(KeyNotFoundException):
                        // not found error
                        var keyEx = (KeyNotFoundException) error;
                        _logger.LogError("KeyNotFoundException:{message}, Details: {Error}", keyEx.Message, keyEx);
                        response.StatusCode = (int) HttpStatusCode.NotFound;
                        break;
                    case nameof(AuthenticationException):
                        // pattern ErrorType: {message}, Details: {details}

                        _logger.LogError("AuthenticationException:{message}", error.Message);
                        response.StatusCode = (int) HttpStatusCode.Unauthorized;
                        break;
                    default:
                        // unhandled error
                        _logger.LogError(error, error.Message);
                        response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        break;
                }

                // switch (error)
                // {
                //     case AppException appEx:
                //         // custom application error
                //         _logger.LogError("AppException:{message}, Details: {Error}", appEx.Message, appEx);
                //         response.StatusCode = (int) HttpStatusCode.BadRequest;
                //         break;
                //     case KeyNotFoundException keyEx:
                //         // not found error
                //         _logger.LogError("KeyNotFoundException:{message}, Details: {Error}", keyEx.Message, keyEx);
                //         response.StatusCode = (int) HttpStatusCode.NotFound;
                //         break;
                //     case AuthenticationException authEx:
                //         // pattern ErrorType: {message}, Details: {details}
                //         _logger.LogError("AuthenticationException:{message}", authEx.Message);
                //         response.StatusCode = (int) HttpStatusCode.Unauthorized;
                //         break;
                //     default:
                //         // unhandled error
                //         _logger.LogError(error, error.Message);
                //         response.StatusCode = (int) HttpStatusCode.InternalServerError;
                //         break;
                //
                //     // case AppException:
                //     //     // custom application error
                //     //     _logger.LogError("AppException:{message}, Details: {Error}", error.Message, error);
                //     //     response.StatusCode = (int) HttpStatusCode.BadRequest;
                //     //     break;
                //     // case KeyNotFoundException:
                //     //     // not found error
                //     //     _logger.LogError("KeyNotFoundException:{message}, Details: {Error}", error.Message, error);
                //     //     response.StatusCode = (int) HttpStatusCode.NotFound;
                //     //     break;
                //     // case AuthenticationException:
                //     //     // pattern ErrorType: {message}, Details: {details}
                //     //     _logger.LogError("AuthenticationException:{message}", error.Message);
                //     //
                //     //
                //     //     response.StatusCode = (int) HttpStatusCode.Unauthorized;
                //     //     break;
                //     // default:
                //     //     // unhandled error
                //     //     _logger.LogError(error, error.Message);
                //     //     response.StatusCode = (int) HttpStatusCode.InternalServerError;
                //     //     break;
                // }

                var result = JsonConvert.SerializeObject(new {error = true, isSuccess = false, message = error.Message});

                await response.WriteAsync(result);
            }
        }
    }
}