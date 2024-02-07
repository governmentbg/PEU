using EAU.ServiceLimits.Server.Manager;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionServerManagerExtensions
    {
        public static IServiceCollection AddServerManager(this IServiceCollection services)
        {
            services.TryAddSingleton<IConfigurationGenerator, ConfigurationGenerator>();

            services.AddServiceLimits();

            return services;
        }
    }
}
