using EAU.DirectoryServices;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions клас за IServiceCollection по функционалности за потребителите от активна директория.
    /// </summary>
    public static class UsersServiceCollectionExtensions
    {
        /// <summary>
        /// Добавя услуги за достъп до потребители от активна директория
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddLDAPUsers(this IServiceCollection services)
        {
            services.TryAddSingleton<ILDAPUserService, LDAPUserService>();

            return services;
        }
    }
}
