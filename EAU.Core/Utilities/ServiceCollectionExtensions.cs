using EAU.Utilities.Caching;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbCacheInvalidationDispatcher(this IServiceCollection services, Action<DbCacheInvalidationDispatcherOptions, IServiceProvider> configureOptions)
        {
            services.AddOptions<DbCacheInvalidationDispatcherOptions>().Configure<IServiceProvider>((options, sp) =>
            {
                configureOptions?.Invoke(options, sp);
            });

            services.TryAddSingleton<IDbCacheInvalidationDispatcher, DbCacheInvalidationDispatcher>();

            return services;
        }
    }
}
