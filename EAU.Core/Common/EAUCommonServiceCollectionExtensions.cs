using EAU.Common;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EAUCommonServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureEAU<TOptions>(this IServiceCollection services, IConfiguration config) where TOptions : class
        {
            return services.Configure<TOptions>(config.GetEAUSection());
        }

        /// <summary>
        /// Конфигурира GlobalOptions от раздел с име 'EPZEU'.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureEAUGlobalOptions(this IServiceCollection services, IConfiguration config)
        {
            return services.ConfigureEAU<GlobalOptions>(config);
        }
    }
}
