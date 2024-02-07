using EAU.Emails;
using EAU.Emails.Cache;
using EAU.Emails.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionEmailExtensions
    {
        public static IServiceCollection AddEmailServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IEmailRepository, EmailRepository>();
            services.TryAddSingleton<IEmailTemplateRepository, EmailTemplateRepository>();
            services.TryAddSingleton<IEmailService, EmailService>();
            services.TryAddSingleton<IEmailNotificationService, EmailNotificationService>();
            services.TryAddSingleton<IEmailsCache, EmailsTemplateDbDataCache>();

            return services;
        }
    }
}
