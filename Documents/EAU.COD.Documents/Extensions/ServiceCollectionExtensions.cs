using CNSys.Xml.Schema;
using EAU.COD.Documents;
using EAU.COD.Documents.Domain;
using EAU.COD.Documents.Domain.Validations;
using EAU.COD.Documents.Domain.Validations.XSDSchemas;
using EAU.Documents.Domain.Validations.XSDSchemas;
using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCODDocumentServices(this IServiceCollection services)
        {
            services.AddDocumentServiceProvider();

            services.AddCODDocumentServices<RequestForIssuingLicenseForPrivateSecurityServicesService>(DocumentTypeUrisCOD.RequestForIssuingLicenseForPrivateSecurityServices);
            services.AddCODDocumentServices<NotificationForConcludingOrTerminatingEmploymentContractService>(DocumentTypeUrisCOD.NotificationForConcludingOrTerminatingEmploymentContract);
            services.AddCODDocumentServices<NotificationForTakingOrRemovingFromSecurityService>(DocumentTypeUrisCOD.NotificationForTakingOrRemovingFromSecurity);

            services.AddValidatorsFromAssemblyContaining<RequestForIssuingLicenseForPrivateSecurityServicesValidator>(ServiceLifetime.Singleton);

            return services;
        }

        private static IServiceCollection AddCODDocumentServices<TImpl>(this IServiceCollection services, string docTypeURI) where TImpl : class
        {
            services.AddDocumentServices<TImpl>(docTypeURI);
            services.AddDocumentXsdValidationService<IXmlSchemaValidator, EAUCODDocumentsXmlSchemaValidator>(docTypeURI);
            services.AddDocumentXsdValidationService<IWeakenedXmlSchemaValidator, EAUCODDocumentsWeakenedXmlSchemaValidator>(docTypeURI);

            return services;
        }
    }
}
