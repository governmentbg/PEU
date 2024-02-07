using EAU.Common;
using EAU.Payments;
using EAU.Payments.Obligations;
using EAU.Payments.Obligations.AND;
using EAU.Payments.Obligations.Repositories;
using EAU.Payments.Obligations.SerivceInstances;
using EAU.Payments.PaymentRequest;
using EAU.Payments.PaymentRequests;
using EAU.Payments.PaymentRequests.Epay;
using EAU.Payments.PaymentRequests.PepDaeu;
using EAU.Payments.PaymentRequests.Repositories;
using EAU.Payments.RegistrationsData;
using EAU.Payments.RegistrationsData.Cache;
using EAU.Payments.RegistrationsData.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions клас за IServiceCollection по функционалности по потребителите.
    /// </summary>
    public static class PaymentsServiceCollectionExtensions
    {
        /// <summary>
        /// Регистриране на функционалности за плащания на задължения към АНД.
        /// </summary>
        /// <param name="services">Интерфейс за работа с услуги</param>
        /// <returns></returns>
        public static IServiceCollection AddEAUPaymentsServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IPepDaeuPaymentCallbackService, PepDaeuPaymentCallbackService>(); 
            services.TryAddSingleton<IEPayPaymentCallbackService, EPayPaymentCallbackService>();
            services.TryAddSingleton<EpayPaymentChannelService>();
            services.AddHttpClient<PepDaeoPaymentChannelService>(EAUHttpClientNames.PepDaeuApi)
                 .ConfigurePrimaryHttpMessageHandler((sp) =>
                 {
                     var handler = new HttpClientHandler();

                     var env = sp.GetService<IHostEnvironment>();
                     if (env.IsDevelopment() || env.IsDevelopmentLocal())
                     {
                         handler.ServerCertificateCustomValidationCallback = (a, b, c, d) => true;
                     }
                     
                     return handler;
                 });

            services.AddANDServicesClient();

            services
                 .AddHttpClient(EAUHttpClientNames.WaisIntegrationMoiApi)
                 .ConfigureHttpClient((sp, client) =>
                 {
                     client.BaseAddress = new Uri(sp.GetRequiredService<IOptionsMonitor<GlobalOptions>>().CurrentValue.GL_WAIS_INTEGRATION_MOI_API);
                 });

            services.TryAddSingleton<AndObligationChannelService>();
            services.TryAddSingleton<ServiceInstanceObligationChannelService>();

            services.TryAddSingleton<IObligationRepository, ObligationRepository>();
            services.TryAddSingleton<IObligationChannelProvider, ObligationChannelProvider>();
            services.TryAddSingleton<IPaymentChannelProvider, PaymentChannelProvider>();
            services.TryAddSingleton<IPepDaeoAccessCodeHelper, PepDaeoPaymentChannelService>();
            services.TryAddSingleton<IPaymentRequestService, PaymentRequestService>();
            services.TryAddSingleton<IPaymentRequestRepository, PaymentRequestRepository>();
            services.TryAddSingleton<IObligationService, ObligationService>();
            services.TryAddSingleton<IRegistrationDataRepository, RegistrationDataRepository>();
            services.TryAddSingleton<IRegistrationsDataCache, RegistrationsDataDbCache>();
            services.TryAddSingleton<IRegistrationsData, RegistrationsData>();
            services.TryAddSingleton<IRegistrationDataService, RegistrationDataService>();

            return services;
        }

        /// <summary>
        /// Регистриране на функционалности регистрационни данни за плащания на задължения към АНД.
        /// </summary>
        /// <param name="services">Интерфейс за работа с услуги</param>
        /// <returns></returns>
        public static IServiceCollection AddEAUPaymentsRegDataServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IPaymentRequestRepository, PaymentRequestRepository>();
            services.TryAddSingleton<IRegistrationDataRepository, RegistrationDataRepository>();
            services.TryAddSingleton<IRegistrationsDataCache, RegistrationsDataDbCache>();
            services.TryAddSingleton<IRegistrationsData, RegistrationsData>();
            services.TryAddSingleton<IRegistrationDataService, RegistrationDataService>();

            return services;
        }
    }
}
