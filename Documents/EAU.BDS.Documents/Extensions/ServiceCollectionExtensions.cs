using CNSys.Xml.Schema;
using EAU.BDS.Documents;
using EAU.BDS.Documents.Domain;
using EAU.BDS.Documents.Domain.Validations.XSDSchemas;
using EAU.Documents.Domain.Validations.XSDSchemas;
using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBDSDocumentServices(this IServiceCollection services)
        {
            services.AddDocumentServiceProvider();

            services.AddBDSDocumentServices<ApplicationForIssuingDocumentService>(DocumentTypeUrisBDS.ApplicationForIssuingDocument);

            services.AddBDSDocumentServices<ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaService>(DocumentTypeUrisBDS.ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgaria);
            services.AddBDSDocumentServices<RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassportService>(DocumentTypeUrisBDS.RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassport);
            services.AddBDSDocumentServices<ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensService>(DocumentTypeUrisBDS.ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizens);
            services.AddBDSDocumentServices<ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensService>(DocumentTypeUrisBDS.ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizens);
            services.AddBDSDocumentServices<CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDService>(DocumentTypeUrisBDS.CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLD);
            services.AddBDSDocumentServices<DeclarationUndurArticle17Service>(DocumentTypeUrisBDS.DeclarationUndurArticle17);
            services.AddBDSDocumentServices<InvitationToDrawUpAUANService>(DocumentTypeUrisBDS.InvitationToDrawUpAUAN);

            services.AddValidatorsFromAssemblyContaining<EAU.BDS.Documents.Domain.Validations.ApplicationForIssuingDocumentValidator>(ServiceLifetime.Singleton);

            return services;
        }

        private static IServiceCollection AddBDSDocumentServices<TImpl>(this IServiceCollection services, string docTypeURI) where TImpl : class
        {
            services.AddDocumentServices<TImpl>(docTypeURI);
            services.AddDocumentXsdValidationService<IXmlSchemaValidator, EAUBDSDocumentsXmlSchemaValidators>(docTypeURI);
            services.AddDocumentXsdValidationService<IWeakenedXmlSchemaValidator, EAUBDSDocumentsWeakenedXmlSchemaValidator>(docTypeURI);

            return services;
        }
    }
}