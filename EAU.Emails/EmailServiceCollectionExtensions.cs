using EAU.Emails;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Статичен клас за екстеншъни свързани с имейли.
    /// </summary>
    public static class EmailExtensions2
    {
        /// <summary>
        /// Добавя услуги за обработка и изпращане на емайл съобщения.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEmailProcessingServices(this IServiceCollection services)
        {
            services.TryAddScoped(typeof(IEmailsSendingService), typeof(EmailsSendingService));

            services.TryAddSingleton(typeof(IHostedService), typeof(EmailSenderEngine));

            return services;
        }
    }
}
