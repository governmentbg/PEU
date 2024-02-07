using CNSys.Xml.Schema;
using EAU.Documents.Domain.Validations.XSDSchemas;
using EAU.KAT.Documents;
using EAU.KAT.Documents.Domain;
using EAU.KAT.Documents.Domain.Validations.XSDSchemas;
using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKATDocumentServices(this IServiceCollection services)
        {
            services.AddDocumentServiceProvider();

            services.AddKATDocumentServices<ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverService>(DocumentTypeUrisKAT.ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriver);
            services.AddKATDocumentServices<ApplicationForIssuingDocumentofVehicleOwnershipService>(DocumentTypeUrisKAT.ApplicationForIssuingDocumentofVehicleOwnership);
            services.AddKATDocumentServices<RequestForDecisionForDealService>(DocumentTypeUrisKAT.RequestForDecisionForDeal);
            services.AddKATDocumentServices<RequestForApplicationForIssuingDuplicateDrivingLicenseService>(DocumentTypeUrisKAT.RequestForApplicationForIssuingDuplicateDrivingLicense);
            services.AddKATDocumentServices<CertificateOfVehicleOwnershipService>(DocumentTypeUrisKAT.CertificateOfVehicleOwnership);
            services.AddKATDocumentServices<CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverService>(DocumentTypeUrisKAT.CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriver);
            services.AddKATDocumentServices<ReportForChangingOwnershipService>(DocumentTypeUrisKAT.ReportForChangingOwnership);
            services.AddKATDocumentServices<ReportForChangingOwnershipV2Service>(DocumentTypeUrisKAT.ReportForChangingOwnershipV2);
            services.AddKATDocumentServices<DataForPrintSRMPSService>(DocumentTypeUrisKAT.DataForPrintSRMPS);
            services.AddKATDocumentServices<ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesService>(DocumentTypeUrisKAT.ApplicationForIssuingOfControlCouponForDriverWithNoPenalties);
            services.AddKATDocumentServices<ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponService>(DocumentTypeUrisKAT.ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCoupon);
            services.AddKATDocumentServices<ApplicationForTerminationOfVehicleRegistrationService>(DocumentTypeUrisKAT.ApplicationForTerminationOfVehicleRegistration);
            services.AddKATDocumentServices<ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateService>(DocumentTypeUrisKAT.ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificate);
            services.AddKATDocumentServices<ApplicationForIssuingTempraryTraficPermitForVehicleService>(DocumentTypeUrisKAT.ApplicationForIssuingTempraryTraficPermitForVehicle);
            services.AddKATDocumentServices<ApplicationForTemporaryTakingOffRoadOfVehicleService>(DocumentTypeUrisKAT.ApplicationForTemporaryTakingOffRoadOfVehicle);
            services.AddKATDocumentServices<ApplicationForCommissioningTemporarilySuspendedVehicleService>(DocumentTypeUrisKAT.ApplicationForCommissioningTemporarilySuspendedVehicle);
            services.AddKATDocumentServices<DeclarationForLostSRPPSService>(DocumentTypeUrisKAT.DeclarationForLostSRPPS);
            services.AddKATDocumentServices<ReportForVehicleService>(DocumentTypeUrisKAT.ReportForVehicle);
            services.AddKATDocumentServices<ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesService>(DocumentTypeUrisKAT.ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehicles);
            services.AddKATDocumentServices<ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsService>(DocumentTypeUrisKAT.ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchants);
            services.AddKATDocumentServices<ApplicationForChangeRegistrationOfVehiclesService>(DocumentTypeUrisKAT.ApplicationForChangeRegistrationOfVehicles);
            services.AddKATDocumentServices<ApplicationForInitialVehicleRegistrationService>(DocumentTypeUrisKAT.ApplicationForInitialVehicleRegistration);
            services.AddKATDocumentServices<ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsService>(DocumentTypeUrisKAT.ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigits);
            services.AddKATDocumentServices<NotificationForTemporaryRegistrationPlatesService>(DocumentTypeUrisKAT.NotificationForTemporaryRegistrationPlates);
            services.AddKATDocumentServices<ApplicationForIssuingDriverLicenseService>(DocumentTypeUrisKAT.ApplicationForIssuingDriverLicense);

            services.AddValidatorsFromAssemblyContaining<EAU.KAT.Documents.Domain.Validations.ApplicationForIssuingDocumentofVehicleOwnershipValidator>(ServiceLifetime.Singleton);

            return services;
        }

        private static IServiceCollection AddKATDocumentServices<TImpl>(this IServiceCollection services, string docTypeURI) where TImpl : class
        {
            services.AddDocumentServices<TImpl>(docTypeURI);
            services.AddDocumentXsdValidationService<IXmlSchemaValidator, EAUKATDocumentsXmlSchemaValidator>(docTypeURI);
            services.AddDocumentXsdValidationService<IWeakenedXmlSchemaValidator, EAUKATDocumentsWeakenedXmlSchemaValidator>(docTypeURI);

            return services;
        }
    }
}
