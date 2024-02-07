using System;
using EAU.Web.DataProtection;
using EAU.Web.Protection;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DataProtectionServiceCollectionExtensions
    {
        /// <summary>
        /// Добавя DataProtection към списъка с услугите. Конфигурира го с съхранение на ключовете в базата данни.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="certificateThumbPrint">ThumbPrint на сертифкат.</param>
        /// <param name="environment">environment</param>
        /// <returns></returns>
        public static IDataProtectionBuilder AddEAUDataProtection(this IServiceCollection services, string certificateThumbPrint, IHostEnvironment environment)
        {
            string thumbPrint = certificateThumbPrint.NormalizeThumbprint();

            var ret = services.AddDataProtection();

            if (!string.IsNullOrEmpty(thumbPrint) && environment.IsDevelopmentLocal() == false)
                ret.ProtectKeysWithCertificate(thumbPrint);

            services.TryAddSingleton<IDataProtectionRepository, DataProtectionRepository>();

            services.AddOptions<KeyManagementOptions>().Configure<IServiceProvider>((options, sp) =>
            {
                options.XmlRepository = new DataProtectionKeysXmlRepository(sp.GetRequiredService<IDataProtectionRepository>());
            });

            services.TryAddSingleton(typeof(IDataProtectorService), typeof(GenericDataProtectorService));

            return ret;
        }

        private static string NormalizeThumbprint(this string thumbprint)
        {
            return thumbprint?.Replace(" ", "").Replace("-", "").Replace("_", "");
        }
    }
}
