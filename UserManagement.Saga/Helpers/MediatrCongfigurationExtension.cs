using System.Reflection;

namespace UserManagement.Saga.Helpers
{
    public static class MediatrCongfigurationExtension
    {
        public static void UseMediatrConfiguration(this IServiceCollection services, IConfiguration configurationManager)
        {
            var assemblies = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(assemblies); });
        }
    }
}