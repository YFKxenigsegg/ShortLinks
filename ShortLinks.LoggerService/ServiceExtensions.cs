using Microsoft.Extensions.DependencyInjection;
using ShortLinks.Contracts;

namespace ShortLinks.LoggerService
{
    public static class ServiceExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
