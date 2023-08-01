using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using UserManagement.Infrastructure.Extensions;

namespace UserManagement.Infrastructure.Commons;

public class ValidationModelAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid) context.Result = new ValidationFailedResult(context.ModelState);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}

public class ValidationError
{
    public ValidationError(string field, string message)
    {
        Field = field != string.Empty ? field.ToLower() : null;
        Message = message;
    }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Field { get; }

    public string Message { get; }
}

public class ValidationResultModel
{
    public ValidationResultModel(ModelStateDictionary modelState)
    {
        var serializer = new JsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        var errorsObject = new JObject();
        var requiredObject = new JObject();

        Message = "Lütfen giriş bilgilerinizi kontrol ediniz.";
        ErrorType = "validation";
        WasValidated = false;

        modelState.Keys.ForEach(key => modelState[key].Errors.ForEach(error =>
        {
            requiredObject[key.FirstLower()] = true;
            errorsObject[key.FirstLower()] = error.ErrorMessage;
        }));
        Errors = JObject.FromObject(errorsObject, serializer);
        Required = JObject.FromObject(requiredObject, serializer);
    }

    public string Message { get; }
    public string ErrorType { get; }

    public bool WasValidated { get; }
    public JObject Required { get; }
    public JObject Errors { get; }
}

public class ValidationFailedResult : ObjectResult, IActionResult
{
    public ValidationFailedResult(ModelStateDictionary modelState) : base(new ValidationResultModel(modelState))
    {
        StatusCode = StatusCodes.Status422UnprocessableEntity;
    }
}

public class ValidationObject
{
    public bool IsValid { get; set; }
    public string Message { get; set; }
}