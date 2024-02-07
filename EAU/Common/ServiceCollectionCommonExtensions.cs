using EAU.Common;
using EAU.Common.Cache;
using EAU.Common.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionCommonExtensions
    {
        /// <summary>
        /// Добавяне на IAppParameters с достъп през базата данни.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEAUAppParametersDb(
            this IServiceCollection services)
        {
            services.TryAddSingleton<IAppParameterRepository, AppParameterRepository>();
            services.TryAddSingleton<IAppParametersCache, AppParametersDbCache>();
            services.TryAddSingleton<IAppParameters, AppParameters>();
            services.TryAddSingleton<IAppParameterService, AppParameterService>(); 

            return services;
        }

        /// <summary>
        /// Добавяне на Functionalities с достъп през базата данни.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEAUFunctionalitiesDb(
            this IServiceCollection services)
        {
            services.TryAddSingleton<IFunctionalityRepository, FunctionalityRepository>();
            services.TryAddSingleton<IFunctionalitiesCache, FunctionalitiesDbCache>();
            services.TryAddSingleton<IFunctionalities, Functionalities>();

            return services;
        }
    }
}
