using UserManagement.Core.Commons;

namespace UserManagement.Core.Extensions;

public static class LoggerMiddlewareConfiguratorExtension
{
    public static void UseExceptionLogger<T>(this IPipeConfigurator<T> configurator) where T : class, PipeContext
    {
        configurator.AddPipeSpecification(new ExceptionLoggerSpecification<T>());
    }
}