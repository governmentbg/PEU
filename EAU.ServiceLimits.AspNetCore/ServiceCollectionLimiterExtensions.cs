using EAU.ServiceLimits.AspNetCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionLimiterExtensions
    {
        public static IServiceCollection AddServiceLimiterService(this IServiceCollection services)
        {
            services.TryAddSingleton<IServiceLimiter, LyftServieRateLimiter>();

            return services;
        }
    }
}
