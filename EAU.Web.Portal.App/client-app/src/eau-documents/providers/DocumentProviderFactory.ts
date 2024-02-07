import { IDocumentProvider } from './DocumentProvider';
import { ModuleManager } from 'eau-core';
import { RemovingIrregularitiesProvider } from './RemovingIrregularitiesProvider';
import { ReceiptAcknowledgedMessageProvider, OutstandingConditionsForStartOfServiceMessageProvider, ReceiptAcknowledgedPaymentForMOIProvider, TerminationOfServiceMessageProvider, ActionsTakenMessageProvider, OutstandingConditionsForWithdrawServiceMessageProvider } from '.';
import { ReceiptNotAcknowledgedMessageProvider } from './ReceiptNotAcknowledgedMessageProvider';
import { PaymentInstructionsProvider } from './PaymentInstructionsProvider';
import { RefusalProvider } from './RefusalProvider';
import { DocumentsWillBeIssuedMessageProvider } from './DocumentsWillBeIssuedMessageProvider';

export namespace DocumentProviderFactory {
    export function getDocumentProvider(docTypeUri: string): Promise<IDocumentProvider> {
        switch (docTypeUri) {
            case '0010-003104': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.applicationForIssuingDocumentofVehicleOwnershipProvider);
            }));
            case '0010-003117': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.applicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverProvider);
            }));
            case '0010-003305': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.requestForDecisionForDealProvider);
            }));
            case '0010-003045': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.requestForApplicationForIssuingDuplicateDrivingLicenseProvider);
            }));
            case '0010-003031': return Promise.resolve(import(/* webpackChunkName: "eau-documents-bds" */ 'eau-documents-bds').then(bds => {
                return ModuleManager.registerModule(bds.eauDocumentsBDSModule).then(() => bds.applicationForIssuingDocumentProvider);
            }));
            case '0010-003021': return Promise.resolve(import(/* webpackChunkName: "eau-documents-bds" */ 'eau-documents-bds').then(bds => {
                return ModuleManager.registerModule(bds.eauDocumentsBDSModule).then(() => bds.applicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaProvider);
            }));
            case '0010-003002': return Promise.resolve(import(/* webpackChunkName: "eau-documents-bds" */ 'eau-documents-bds').then(bds => {
                return ModuleManager.registerModule(bds.eauDocumentsBDSModule).then(() => bds.applicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensProvider);
            }));
            case '0010-003030': return Promise.resolve(import(/* webpackChunkName: "eau-documents-bds" */ 'eau-documents-bds').then(bds => {
                return ModuleManager.registerModule(bds.eauDocumentsBDSModule).then(() => bds.applicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensProvider);
            }));
            case '0010-003251': return Promise.resolve(import(/* webpackChunkName: "eau-documents-bds" */ 'eau-documents-bds').then(bds => {
                return ModuleManager.registerModule(bds.eauDocumentsBDSModule).then(() => bds.declarationUnderArticle17Provider);
            }));
            case '0010-003254': return Promise.resolve(import(/* webpackChunkName: "eau-documents-bds" */ 'eau-documents-bds').then(bds => {
                return ModuleManager.registerModule(bds.eauDocumentsBDSModule).then(() => bds.InvitationToDrawUpAUANProvider);
            }));
            case '0010-003115': return Promise.resolve(import(/* webpackChunkName: "eau-documents-migr" */ 'eau-documents-migr').then(migr => {
                return ModuleManager.registerModule(migr.eauDocumentsMIGRModule).then(() => migr.applicationForIssuingDocumentForForeignersProvider);
            }));
            case '0010-003016': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kos" */ 'eau-documents-kos').then(kos => {
                return ModuleManager.registerModule(kos.eauDocumentsKOSModule).then(() => kos.applicationByFormAnnex9Provider);
            }));
            case '0010-003113': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kos" */ 'eau-documents-kos').then(kos => {
                return ModuleManager.registerModule(kos.eauDocumentsKOSModule).then(() => kos.applicationByFormAnnex10Provider);
            }));
            case '0010-003138': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kos" */ 'eau-documents-kos').then(kos => {
                return ModuleManager.registerModule(kos.eauDocumentsKOSModule).then(() => kos.NotificationForNonFirearmProvider);
            }));
            case '0010-003139': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kos" */ 'eau-documents-kos').then(kos => {
                return ModuleManager.registerModule(kos.eauDocumentsKOSModule).then(() => kos.NotificationForFirearmProvider);
            }));
            case '0010-003260': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kos" */ 'eau-documents-kos').then(kos => {
                return ModuleManager.registerModule(kos.eauDocumentsKOSModule).then(() => kos.NotificationForControlCouponProvider);
            }));
            case '0010-003108': return Promise.resolve(import(/* webpackChunkName: "eau-documents-cod" */ 'eau-documents-cod').then(cod => {
                return ModuleManager.registerModule(cod.eauDocumentsCODModule).then(() => cod.requestForIssuingLicenseForPrivateSecurityServicesProvider);
            }));
            case '0010-003151': return Promise.resolve(import(/* webpackChunkName: "eau-documents-cod" */ 'eau-documents-cod').then(cod => {
                return ModuleManager.registerModule(cod.eauDocumentsCODModule).then(() => cod.NotificationForConcludingOrTerminatingEmploymentContractProvider);
            }));
            case '0010-003152': return Promise.resolve(import(/* webpackChunkName: "eau-documents-cod" */ 'eau-documents-cod').then(cod => {
                return ModuleManager.registerModule(cod.eauDocumentsCODModule).then(() => cod.NotificationForTakingOrRemovingFromSecurityProvider);
            }));

            case '0010-003107': return Promise.resolve(import(/* webpackChunkName: "eau-documents-PBZN" */ 'eau-documents-pbzn').then(pbzn => {
                return ModuleManager.registerModule(pbzn.eauDocumentsPBZNModule).then(() => pbzn.applicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesProvider);
            }));
            case '0010-003123': return Promise.resolve(import(/* webpackChunkName: "eau-documents-PBZN" */ 'eau-documents-pbzn').then(pbzn => {
                return ModuleManager.registerModule(pbzn.eauDocumentsPBZNModule).then(() => pbzn.appForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutProvider);
            }));
            case '0010-003010': return Promise.resolve(RemovingIrregularitiesProvider);
            case '0010-000002': return Promise.resolve(ReceiptAcknowledgedMessageProvider);
            case '0010-000001': return Promise.resolve(ReceiptNotAcknowledgedMessageProvider);
            case '0010-003103': return Promise.resolve(PaymentInstructionsProvider);
            case '0010-003100': return Promise.resolve(OutstandingConditionsForStartOfServiceMessageProvider);
            case '0010-003119': return Promise.resolve(OutstandingConditionsForWithdrawServiceMessageProvider);
            case '0010-003122': return Promise.resolve(ReceiptAcknowledgedPaymentForMOIProvider);
            case '0010-003133': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.CertificateOfVehicleOwnershipProvider);
            }));
            case '0010-003137': return Promise.resolve(RefusalProvider);
            case '0010-003102': return Promise.resolve(DocumentsWillBeIssuedMessageProvider);
            case '0010-003101': return Promise.resolve(TerminationOfServiceMessageProvider);
            case '0010-003149': return Promise.resolve(ActionsTakenMessageProvider);
            case '0010-003306': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.reportForChangingOwnershipProvider);
            }));
            case '0010-003165': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.ReportForChangingOwnershipV2Provider);
            }));
            case '0010-003046': return Promise.resolve(import(/* webpackChunkName: "eau-documents-bds" */ 'eau-documents-bds').then(bds => {
                return ModuleManager.registerModule(bds.eauDocumentsBDSModule).then(() => bds.certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDProvider);
            }));
            case '0010-003125': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.certificateAttestingToTheRightsAndPenaltiesOfVehicleDriverProvider);
            }));
            case '0010-003308': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.dataForPrintSRMPSProvider);
            }));

            case '0010-003141': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.applicationForIssuingOfDuplicateOfDrivingLicenseControlCouponProvider);
            }));
            case '0010-003142': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.applicationForIssuingOfControlCouponForDriverWithNoPenaltiesProvider);
            }));
            case '0010-003153': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.ApplicationForTerminationOfVehicleRegistrationProvider);
            }));
            case '0010-003155': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateProvider);
            }));
            case '0010-003157': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.ApplicationForIssuingTempraryTraficPermitForVehicleProvider);
            }));
            case '0010-003158': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.ApplicationForTemporaryTakingOffRoadOfVehicleProvider);
            }));
            case '0010-003159': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.ApplicationForCommissioningTemporarilySuspendedVehicleProvider);
            }));
            case '0010-003154': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.ReportForVehicleProvider);
            }));
            case '0010-003156': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.declarationForLostSRPPProvider);
            }));
            case '0010-003160': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesProvider);
            }));
            case '0010-003161': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsProvider);
            }));
            case '0010-003162': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.ApplicationForChangeRegistrationOfVehiclesProvider);
            }));
            case '0010-003163': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.ApplicationForInitialVehicleRegistrationProvider);
            }));
            case '0010-003164': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsProvider);
            }));
            case '0010-003166': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.NotificationForTemporaryRegistrationPlatesProvider);
            }));
            case '0010-003055': return Promise.resolve(import(/* webpackChunkName: "eau-documents-kat" */ 'eau-documents-kat').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsKATModule).then(() => kat.ApplicationForIssuingDriverLicenseProvider);
            }));
            case '0010-003143': return Promise.resolve(import(/* webpackChunkName: "eau-documents-PBZN" */ 'eau-documents-pbzn').then(pbzn => {
                return ModuleManager.registerModule(pbzn.eauDocumentsPBZNModule).then(() => pbzn.certificateForAccidentProvider);
            }));
            case '0010-003145': return Promise.resolve(import(/* webpackChunkName: "eau-documents-PBZN" */ 'eau-documents-pbzn').then(pbzn => {
                return ModuleManager.registerModule(pbzn.eauDocumentsPBZNModule).then(() => pbzn.certificateToWorkWithFluorinatedGreenhouseGassesProvider);
            }));
            case '0010-003001': return Promise.resolve(import(/* webpackChunkName: "eau-documents-PBZN" */ 'eau-documents-bds').then(bds => {
                return ModuleManager.registerModule(bds.eauDocumentsBDSModule).then(() => bds.requestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassportProvider);
            }));
            case '0010-003059': return Promise.resolve(import(/* webpackChunkName: "eau-documents" */ 'eau-documents').then(kat => {
                return ModuleManager.registerModule(kat.eauDocumentsModule).then(() => kat.ApplicationForWithdrawServiceProvider);
            }));
            default: return null;
        }
    }
}