using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using UserManagement.Infrastructure.Extensions;

namespace UserManagement.Infrastructure.Commons;

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