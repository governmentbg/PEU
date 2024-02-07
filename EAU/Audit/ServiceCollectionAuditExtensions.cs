using EAU.Audit;
using EAU.Audit.Cache;
using EAU.Audit.Repositories;
using EAU.Nomenclatures;
using EAU.Nomenclatures.Cache;
using EAU.Nomenclatures.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionAuditExtensions
    {
        /// <summary>
        /// Регистрира услуги за достъп за одит към базата данни.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEAUAudit(this IServiceCollection services)
        {
            services.TryAddSingleton(typeof(ILogActionRepository), typeof(LogActionRepository));
            services.TryAddSingleton(typeof(IActionTypeRepository), typeof(ActionTypeRepository));
            services.TryAddSingleton(typeof(IObjectTypeRepository), typeof(ObjectTypeRepository));
            services.TryAddSingleton(typeof(IAuditService), typeof(AuditService));
            services.TryAddSingleton<IActionTypesCache, ActionTypesDbCache>();
            services.TryAddSingleton<IActionTypes, ActionTypes>();
            services.TryAddSingleton<IObjectTypesCache, ObjectTypesDbCache>();
            services.TryAddSingleton<IObjectTypes, ObjectTypes>();

            return services;
        }
    }
}
