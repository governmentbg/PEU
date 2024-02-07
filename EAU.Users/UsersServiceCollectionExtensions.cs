using EAU.Security;
using EAU.Users;
using EAU.Users.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions клас за IServiceCollection по функционалности по потребителите.
    /// </summary>
    public static class UsersServiceCollectionExtensions
    {
        /// <summary>
        /// Регистриране на функционалности по потребителите за автентикация и валидация.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddEAUUsersLogin(this IServiceCollection services)
        {
            services.TryAddSingleton<IUsersLoginService, UsersLoginService>();
            services.TryAddSingleton<IUserPasswordService, UserPasswordService>();

            return services;
        }

        /// <summary>
        /// Добавя услуги за търсене на потребители
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEAUSearchUsers(
            this IServiceCollection services)
        {
            services.TryAddSingleton<IUsersSearchService, UsersSearchService>();
            services.TryAddSingleton<IUsersPermissionRepository, UserPermissionRepository>();
            services.TryAddSingleton<IUsersRepository, UsersRepository>();
            services.TryAddSingleton<IUserLoginSessionRepository, UserLoginSessionRepository>();
            services.TryAddSingleton<ICertificateRepository, CertificateRepository>();

            return services;
        }

        /// <summary>
        /// Добавя услуги за достъп до потребители
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEAUUsers(
            this IServiceCollection services)
        {
            services.AddEAUSearchUsers();

            services.TryAddSingleton<IUsersAuthenticationRepository, UsersAuthenticationRepository>();
            
            services.TryAddSingleton<IUsersProcessesRepository, UserProcessesRepository>();
            services.TryAddSingleton<IUserLoginAttemptRepository, UserLoginAttemptRepository>();
            

            services.TryAddSingleton<IUsersService, UsersService>();
            services.TryAddSingleton<IUserPasswordService, UserPasswordService>();
            services.TryAddSingleton<IUserProcessLinkBuilderService, UserProcessLinkBuilderService>();
            services.TryAddSingleton<IUserInteractionService, UserInteractionService>();
            services.TryAddSingleton<IDataSourceUserMapper, EAUDataSourceUserMapper>();

            return services;
        }
    }
}
