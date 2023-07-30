using Microsoft.AspNetCore.Http;
using UserManagement.Infrastructure.Commons;

namespace UserManagement.Infrastructure.Middlewares;

public class ServiceResponseHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ServiceResponseHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);


        if (context.Response.StatusCode == 200)
        {
            var responseBody = await GetResponseBodyAsync(context.Response);
            var serviceResponse = ProcessResponseBody(responseBody);

            if (serviceResponse.Status)
            {
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    error = false,
                    isSuccess = true,
                    message = serviceResponse.Message,
                    data = serviceResponse.Data
                }));
            }
            else
            {
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    error = true,
                    isSuccess = false,
                    message = serviceResponse.Message
                }));
            }
        }
    }

    private async Task<string> GetResponseBodyAsync(HttpResponse response)
    {
        using var responseStream = new StreamReader(response.Body);
        return await responseStream.ReadToEndAsync();
    }

    private ServiceResponse ProcessResponseBody(string responseBody)
    {
        return new ServiceResponse {Status = true, Message = "Success", Data = responseBody};
    }

    // Eğer yanıtı özelleştirilmiş şekilde geri döndürmek için kullanılırsa bu metot kullanılabilir.
    private async Task WriteResponseAsync(HttpContext context, string result)
    {
        context.Response.ContentLength = result.Length;
        await context.Response.WriteAsync(result);
    }
}