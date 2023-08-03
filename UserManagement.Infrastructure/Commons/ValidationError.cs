namespace UserManagement.Infrastructure.Commons;

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