namespace UserManagement.Core.Commons;

public class ExceptionLoggerSpecification<T> : IPipeSpecification<T> where T : class, PipeContext
{
    public IEnumerable<ValidationResult> Validate()
    {
        return Enumerable.Empty<ValidationResult>();
    }

    public void Apply(IPipeBuilder<T> builder)
    {
        builder.AddFilter(new ExceptionLoggerFilter<T>());
    }
}