using UserManagement.Infrastructure.Commons;

namespace UserManagement.Infrastructure.Extensions;

public static class LoggerMiddlewareConfiguratorExtension
{
    public static void UseExceptionLogger<T>(this IPipeConfigurator<T> configurator) where T : class, PipeContext
    {
        configurator.AddPipeSpecification(new ExceptionLoggerSpecification<T>());
    }
}