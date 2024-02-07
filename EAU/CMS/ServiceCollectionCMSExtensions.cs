using EAU.Audit;
using EAU.Audit.Repositories;
using EAU.CMS;
using EAU.CMS.Cache;
using EAU.CMS.Repositories;
using EAU.Common.Repositories;
using EAU.Nomenclatures;
using EAU.Nomenclatures.Cache;
using EAU.Nomenclatures.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionCMSExtensions
    {
        /// <summary>
        /// Регистрира услуги за CMS.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCMS(this IServiceCollection services)
        {
            services.TryAddSingleton<IPageRepository, PageRepository>();
            services.TryAddSingleton<IPageTranslationRepository, PageTranslationRepository>();
            services.TryAddSingleton<IPageService, PageService>();
            services.TryAddSingleton<IPagesCache, PagesDbCache>();
            services.TryAddSingleton<IPages, Pages>();

            return services;
        }
    }
}
