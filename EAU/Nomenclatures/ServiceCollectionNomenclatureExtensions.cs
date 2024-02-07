using EAU.Nomenclatures;
using EAU.Nomenclatures.Cache;
using EAU.Nomenclatures.Repositories;
using EAU.Nomenclatures.Services;
using EAU.Reports;
using EAU.Reports.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionNomenclatureExtensions
    {
        public static IServiceCollection AddEAUNomenclatures(
            this IServiceCollection services)
        {
            AddEAUNomenclatureRepositories(services);
            AddEAUNomenclatureServices(services);

            return services;
        }

        /// <summary>
        /// Регистрира услуги за достъп до номенклатурите към базата данни.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEAUNomenclatureRepositories(
            this IServiceCollection services)
        {
            services.TryAddSingleton<ILabelRepository, LabelRepository>();
            services.TryAddSingleton<ILabelTranslationRepository, LabelTranslationRepository>();
            services.TryAddSingleton<ILanguageRepository, LanguageRepository>();
            services.TryAddSingleton<IDeclarationRepository, DeclarationRepository>();
            services.TryAddSingleton<IDeliveryChannelRepository, DeliveryChannelRepository>();
            services.TryAddSingleton<IDocumentTypeRepository, DocumentTypeRepository>();
            services.TryAddSingleton<IServiceDeclarationRepository, ServiceDeclarationRepository>();
            services.TryAddSingleton<IServiceDeliveryChannelRepository, ServiceDeliveryChannelRepository>();
            services.TryAddSingleton<IServiceDocumentTypeRepository, ServiceDocumentTypeRepository>();
            services.TryAddSingleton<IServiceGroupRepository, ServiceGroupRepository>();
            services.TryAddSingleton<IServiceRepository, ServiceRepository>();
            services.TryAddSingleton<IServiceTermRepository, ServiceTermRepository>();
            services.TryAddSingleton<IEkatteRepository, EkatteRepository>();
            services.TryAddSingleton<IGraoRepository, GraoRepository>();
            services.TryAddSingleton<IServiceTranslationRepository, ServiceTranslationRepository>();
            services.TryAddSingleton<IServiceGroupTranslationRepository, ServiceGroupTranslationRepository>();
            services.TryAddSingleton<IDocumentTemplateRepository, DocumentTemplateRepository>();
            services.TryAddSingleton<IDocumentTemplateFieldRepository, DocumentTemplateFieldRepository>();
            services.TryAddSingleton<ICountryRepository, CountryRepository>();
            services.TryAddSingleton<IReportsRepository, ReportsRepository>();
            services.TryAddSingleton<IReportsService, ReportsService>();

            return services;
        }

        public static IServiceCollection AddEAUNomenclatureServices(
         this IServiceCollection services)
        {
            services.TryAddSingleton<IDeclarationService, DeclarationService>();
            services.TryAddSingleton<IServiceDeclarationService, ServiceDeclarationService>();
            services.TryAddSingleton<IServiceDeliveryChannelService, ServiceDeliveryChannelService>();
            services.TryAddSingleton<IServiceGroupService, ServiceGroupService>();            
            services.TryAddSingleton<IServiceTermService, ServiceTermService>();
            services.TryAddSingleton<IServiceDocumentTypeService, ServiceDocumentTypeService>();
            services.TryAddSingleton<IServiceService, ServiceService>();
            services.TryAddSingleton<IDocumentTemplateService, DocumentTemplateService>();

            return services;
        }


        /// <summary>
        /// Добавя базовите интерфейси с кеширане за работа с номенклатури в EAU с имплементация за достъп през базата данни.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEAUNomenclaturesDBCaching(
            this IServiceCollection services)
        {           
            services.AddEAULabelsDb();
            services.AddEAULabelLocalizationsDb();
            services.AddEAULanguagesDb();
            services.AddEAUDeclarationsDb();
            services.AddEAUDocumentTypesDb();
            services.AddEAUServicesDb();
            services.AddEAUServiceGroupsDb();
            services.AddEAUServiceTermsDb();
            services.AddEAUDeliveryChannelsTermsDb();
            services.AddEAUEkatteDb();
            services.AddEAUGraoDb();
            services.AddEAUDocumentTemplatesDb();
            services.AddEAUDocumentTemplateFielsdDb();
            services.AddEAUCountriesDb();

            return services;
        }

        public static IServiceCollection AddEAULabelLocalizationsDb(
            this IServiceCollection services)
        {
            services.TryAddSingleton<ILabelLocalizationsDataCache, LabelsLocalizationDbCache>();
            services.TryAddSingleton<ILabelLocalizations, LabelLocalizations>();
            return services;
        }

        public static IServiceCollection AddEAULabelsDb(
            this IServiceCollection services)
        {
            services.TryAddSingleton<ILabelsDataCache, LabelsDbCache>();
            services.TryAddSingleton<ILabels, Labels>();
            return services;
        }

        public static IServiceCollection AddEAULanguagesDb(
            this IServiceCollection services)
        {
            services.TryAddSingleton<ILanguagesCache, LanguagesDbCache>();
            services.TryAddSingleton<ILanguages, Languages>();

            return services;
        }

        public static IServiceCollection AddEAUDeclarationsDb(
            this IServiceCollection services)
        {
            services.TryAddSingleton<IDeclarationsCache, DeclarationsDbCache>();
            services.TryAddSingleton<IDeclarations, Declarations>();

            return services;
        }

        public static IServiceCollection AddEAUDocumentTypesDb(
            this IServiceCollection services)
        {
            services.TryAddSingleton<IDocumentTypesCache, DocumentTypesDbCache>();
            services.TryAddSingleton<IDocumentTypes, DocumentTypes>();

            return services;
        }

        public static IServiceCollection AddEAUServicesDb(
            this IServiceCollection services)
        {
            services.TryAddSingleton<IServicesCache, ServicesDbCache>();
            services.TryAddSingleton<IServices, Services>();

            return services;
        }

        public static IServiceCollection AddEAUServiceGroupsDb(
            this IServiceCollection services)
        {
            services.TryAddSingleton<IServiceGroupsCache, ServiceGroupsDbCache>();
            services.TryAddSingleton<IServiceGroups, ServiceGroups>();

            return services;
        }

        public static IServiceCollection AddEAUServiceTermsDb(
            this IServiceCollection services)
        {
            services.TryAddSingleton<IServiceTermsCache, ServiceTermsDbCache>();
            services.TryAddSingleton<IServiceTerms, ServiceTerms>();

            return services;
        }

        public static IServiceCollection AddEAUDeliveryChannelsTermsDb(
            this IServiceCollection services)
        {
            services.TryAddSingleton<IDeliveryChannelsCache, DeliveryChannelsDbCache>();
            services.TryAddSingleton<IDeliveryChannels, DeliveryChannels>();

            return services;
        }

        public static IServiceCollection AddEAUEkatteDb(
            this IServiceCollection services)
        {
            services.TryAddSingleton<IEkatteCache, EkatteDbCache>();
            services.TryAddSingleton<IEkatte, Ekattes>();

            return services;
        }

        public static IServiceCollection AddEAUGraoDb(
            this IServiceCollection services)
        {
            services.TryAddSingleton<IGraoCache, GraoDbCache>();
            services.TryAddSingleton<IGrao, Graos>();

            return services;
        }

        public static IServiceCollection AddEAUDocumentTemplateFielsdDb(
           this IServiceCollection services)
        {
            services.TryAddSingleton<IDocumentTemplateFieldsCache, DocumentTemplateFieldsDbCache>();
            services.TryAddSingleton<IDocumentTemplateFields, DocumentTemplateFields>();

            return services;
        }

        public static IServiceCollection AddEAUDocumentTemplatesDb(
           this IServiceCollection services)
        {
            services.TryAddSingleton<IDocumentTemplatesCache, DocumentTemplatesDbCache>();
            services.TryAddSingleton<IDocumentTemplates, DocumentTemplates>();

            return services;
        }

        public static IServiceCollection AddEAUCountriesDb(
           this IServiceCollection services)
        {
            services.TryAddSingleton<ICountriesCache, CountryChannelsDbCache>();
            services.TryAddSingleton<ICountries, Countries>();

            return services;
        }
    }
}