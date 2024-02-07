using EAU.Services.DocumentProcesses;
using EAU.Services.DocumentProcesses.Repositories;
using EAU.Services.Nomenclatures;
using EAU.Services.Nomenclatures.Cache;
using EAU.Services.ServiceInstances;
using EAU.Services.ServiceInstances.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDocumentProcessesServices(this IServiceCollection services)
        {
            services.TryAddSingleton(typeof(IServiceInstanceRepository), typeof(ServiceInstanceRepository));
            services.TryAddSingleton(typeof(IServiceInstanceService), typeof(ServiceInstanceService));
            services.TryAddSingleton(typeof(IAttachedDocumentRepository), typeof(AttachedDocumentRepository));
            services.TryAddSingleton(typeof(IDocumentProcessContentRepository), typeof(DocumentProcessContentRepository));
            services.TryAddSingleton(typeof(IDocumentProcessRepository), typeof(DocumentProcessRepository));
            services.TryAddSingleton(typeof(IDocumentProcessFormService), typeof(DocumentProcessFormService));
            services.TryAddSingleton(typeof(IDocumentProcessService), typeof(DocumentProcessService));
            services.TryAddSingleton(typeof(IDocumentProcessSigningService), typeof(DocumentProcessSigningService));
            services.TryAddSingleton(typeof(IDocumentProcessSigningCallbackService), typeof(DocumentProcessSigningCallbackService));
            services.TryAddSingleton(typeof(IDocumentProcessAttachedDocumentService), typeof(DocumentProcessService));
            services.TryAddSingleton(typeof(IDocumentProcessCallBackService), typeof(DocumentProcessCallBackService));
            services.TryAddSingleton(typeof(IDocumentAccessDataLogManager), typeof(DocumentAccessDataLogManager));

            return services;
        }

        public static IServiceCollection AddServiceUnitsInfoPolling(this IServiceCollection services)
        {
            services.TryAddSingleton(typeof(IServingUnitsInfoCache), typeof(ServingUnitsInfoPollingCache));
            services.TryAddSingleton(typeof(IServingUnitsInfo), typeof(ServingUnitsInfo));

            services.TryAddSingleton(typeof(IDeliveryUnitsInfoCache), typeof(DeliveryUnitsInfoPollingCache));
            services.TryAddSingleton(typeof(IDeliveryUnitsInfo), typeof(DeliveryUnitsInfo));

            services.TryAddSingleton(typeof(IDeliveryUnitsRootInfoCache), typeof(DeliveryUnitsRootInfoPollingCache));
            services.TryAddSingleton(typeof(IDeliveryUnitsRootInfo), typeof(DeliveryUnitsRootInfo));

            return services;
        }

        public static IServiceCollection AddMOINomenclaturesPolling(this IServiceCollection services)
        {
            services.TryAddSingleton(typeof(ITerminationOfRegistrationOfVehicleReasonsCache), typeof(TerminationOfRegistrationOfVehicleReasonsPollingCache));
            services.TryAddSingleton(typeof(ITerminationOfRegistrationOfVehicleReasons), typeof(TerminationOfRegistrationOfVehicleReasons));

            services.TryAddSingleton(typeof(IReasonsForIssuingDuplicateOfSRMPSCache), typeof(ReasonsForIssuingDuplicateOfSRMPSPollingCache));
            services.TryAddSingleton(typeof(IReasonsForIssuingDuplicateOfSRMPS), typeof(ReasonsForIssuingDuplicateOfSRMPS));

            services.TryAddSingleton(typeof(ICountriesCache), typeof(CountriesPollingCache));
            services.TryAddSingleton(typeof(ICountries), typeof(Countries));

            services.TryAddSingleton(typeof(IVehicleBaseColorsCache), typeof(VehicleBaseColorsPollingCache));
            services.TryAddSingleton(typeof(IVehicleBaseColors), typeof(VehicleBaseColors));

            services.TryAddSingleton(typeof(IRegistrationNumberProvinceCodesCache), typeof(RegistrationNumberProvinceCodesPollingCache));
            services.TryAddSingleton(typeof(IRegistrationNumberProvinceCodes), typeof(RegistrationNumberProvinceCodes));

            return services;
        }
    }
}
