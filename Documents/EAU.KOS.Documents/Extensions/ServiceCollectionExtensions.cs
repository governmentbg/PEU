using CNSys.Xml.Schema;
using EAU.Documents.Domain.Validations.XSDSchemas;
using EAU.KOS.Documents;
using EAU.KOS.Documents.Domain;
using EAU.KOS.Documents.Domain.Validations;
using EAU.KOS.Documents.Domain.Validations.XSDSchemas;
using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKOSDocumentServices(this IServiceCollection services)
        {
            services.AddDocumentServiceProvider();

            services.AddKOSDocumentServices<ApplicationByFormAnnex9Service>(DocumentTypeUrisKOS.ApplicationByFormAnnex9);
            services.AddKOSDocumentServices<ApplicationByFormAnnex10Service>(DocumentTypeUrisKOS.ApplicationByFormAnnex10);
            services.AddKOSDocumentServices<NotificationForNonFirearmService>(DocumentTypeUrisKOS.NotificationForNonFirearm);
            services.AddKOSDocumentServices<NotificationForFirearmService>(DocumentTypeUrisKOS.NotificationForFirearm);
            services.AddKOSDocumentServices<NotificationForControlCouponService>(DocumentTypeUrisKOS.NotificationForControlCoupon);

            services.AddValidatorsFromAssemblyContaining<ApplicationByFormAnnex10Validator>(ServiceLifetime.Singleton);

            return services;
        }

        private static IServiceCollection AddKOSDocumentServices<TImpl>(this IServiceCollection services, string docTypeURI) where TImpl : class
        {
            services.AddDocumentServices<TImpl>(docTypeURI);
            services.AddDocumentXsdValidationService<IXmlSchemaValidator, EAUKOSDocumentsXmlSchemaValidators>(docTypeURI);
            services.AddDocumentXsdValidationService<IWeakenedXmlSchemaValidator, EAUKOSDocumentsWeakenedXmlSchemaValidator>(docTypeURI);

            return services;
        }
    }
}