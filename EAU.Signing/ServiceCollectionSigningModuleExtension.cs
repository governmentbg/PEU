using EAU.Signing;
using EAU.Signing.Configuration;
using EAU.Signing.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionSigningModuleExtension
    {
        public static IServiceCollection AddSigningModuleServices(this IServiceCollection services, IConfiguration config)
        {
            services.ConfigureSIGN<SignModuleGlobalOptions>(config);          
            services.TryAddSingleton<ISigningProcessesRepository, SigningProcessesRepository>();
            services.TryAddSingleton<ISignersRepository, SignersRepository>();

            services.TryAddSingleton<IBSecureDsslClientFactory, SigningServiceClientsFactory>();
            services.TryAddSingleton<IBtrustRemoteClientFactory, SigningServiceClientsFactory>();
            services.TryAddSingleton<IEvrotrustClientFactory, SigningServiceClientsFactory>();
             
            services.TryAddSingleton<IDocumentSigningtUtilityService, DocumentSigningUtilityService>();
            services.TryAddSingleton<ISigningProcessesService, SigningProcessesService>();
            services.TryAddSingleton<ISigningService, SigningService>();

            services.TryAddSingleton<ITestSignService, TestSignService>();
            services.TryAddSingleton<IBTrustProcessorService, BTrustProcessorService>();
            services.TryAddSingleton<IEvrotrustProcessorService, EvrotrustProcessorService>();

            return services;
        }

        public static IServiceCollection ConfigureSIGN<TOptions>(this IServiceCollection services, IConfiguration config) where TOptions : class
        {
            return services.Configure<TOptions>(config.GetSection("EAU"));
        }
    }
}
