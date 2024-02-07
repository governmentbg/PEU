using CNSys.Xml.Schema;
using EAU.Documents.Domain.Validations.XSDSchemas;
using EAU.PBZN.Documents;
using EAU.PBZN.Documents.Domain;
using EAU.PBZN.Documents.Domain.Validations;
using EAU.PBZN.Documents.Domain.Validations.XSDSchemas;
using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPBZNDocumentServices(this IServiceCollection services)
        {
            services.AddDocumentServiceProvider();

            services.AddPBZNDocumentServices<ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutService>
                (DocumentTypeUrisPBZN.ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOut);

            services.AddPBZNDocumentServices<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesService>
                (DocumentTypeUrisPBZN.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGasses);

            services.AddPBZNDocumentServices<CertificateForAccidentService>(DocumentTypeUrisPBZN.CertificateForAccident);
            services.AddPBZNDocumentServices<CertificateToWorkWithFluorinatedGreenhouseGassesService>(DocumentTypeUrisPBZN.CertificateToWorkWithFluorinatedGreenhouseGasses);

            services.AddValidatorsFromAssemblyContaining<ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutValidator>(ServiceLifetime.Singleton);

            return services;
        }

        private static IServiceCollection AddPBZNDocumentServices<TImpl>(this IServiceCollection services, string docTypeURI) where TImpl : class
        {
            services.AddDocumentServices<TImpl>(docTypeURI);
            services.AddDocumentXsdValidationService<IXmlSchemaValidator, EAUPBZNDocumentsXmlSchemaValidators>(docTypeURI);
            services.AddDocumentXsdValidationService<IWeakenedXmlSchemaValidator, EAUPBZNDocumentsWeakenedXmlSchemaValidator>(docTypeURI);

            return services;
        }
    }
}