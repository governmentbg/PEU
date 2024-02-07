using CNSys;
using EAU.Documents;
using EAU.Documents.Common;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.KAT.Documents.Domain;
using EAU.KAT.Documents.Domain.Models;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.KAT.Documents.Models.Forms;
using EAU.KAT.Documents.XSLT;
using EAU.Nomenclatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.KAT.AND;

namespace EAU.KAT.Documents
{
    internal class ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsService :
       ApplicationFormServiceBase<ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchants, ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsVM>
    {
        private readonly IANDServicesClientFactory _iANDServicesClientFactory;

        public ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsService(IServiceProvider serviceProvider
            , IANDServicesClientFactory iANDServicesClientFactory): base(serviceProvider)
        {
            _iANDServicesClientFactory = iANDServicesClientFactory;
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchants;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3324_ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchants.xslt",
                    Resolver = new KATEmbeddedXmlResourceResolver()
                };
            }
        }

        protected override List<ElectronicServiceAuthorQualityType> GetPossibleAuthorQualities(ApplicationFormInitializationRequest request)
        {
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

            if (service.AdditionalConfiguration != null
                && (service.AdditionalConfiguration.ContainsKey("possibleAuthorQualitiesBG") || service.AdditionalConfiguration.ContainsKey("possibleAuthorQualitiesF")))
            {
                return request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationBG != null
                    ? service.AdditionalConfiguration["possibleAuthorQualitiesBG"].Split(',').Select(t => (ElectronicServiceAuthorQualityType)Convert.ToInt32(t)).ToList()
                    : service.AdditionalConfiguration["possibleAuthorQualitiesF"].Split(',').Select(t => (ElectronicServiceAuthorQualityType)Convert.ToInt32(t)).ToList();
            }

            return new List<ElectronicServiceAuthorQualityType>()
            {
                ElectronicServiceAuthorQualityType.LegalRepresentative
            };
        }

        protected override List<IdentityDocumentType> GetPossibleRecipientIdentityDocumentTypes(ApplicationFormInitializationRequest request)
        {
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

            if (service.AdditionalConfiguration != null && service.AdditionalConfiguration.ContainsKey("identityDocumentType"))
                return service.AdditionalConfiguration["identityDocumentType"].Split(',').Select(t => (IdentityDocumentType)Convert.ToInt32(t)).ToList();

            return new List<IdentityDocumentType>() { IdentityDocumentType.PersonalCard };
        }

        protected override List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType> GetPossibleRecipientTypes(ApplicationFormInitializationRequest reques)
        {
            return new List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType>()
            {
                PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Entity
            };
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            var app = (ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsVM)request.Form;

            if (app != null)
            {
                
                if (app.MerchantData == null)
                {
                    app.MerchantData = new MerchantData()
                    {
                        CompanyCase = new MerchantDataCompanyCase(),
                        EntityManagementAddress = new EntityAddress(),
                        CorrespondingAddress = new EntityAddress()

                    };
                }

                if (app.Circumstances == null)
                {
                    app.Circumstances = new Models.ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataVM()
                    { 
                        AuthorizedPersons = new Models.AuthorizedPersonCollectionVM()
                        { 
                            Items = new List<ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataAuthorizedPersons>()
                            {
                                new ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataAuthorizedPersons()
                                {
                                    Identifier = new PersonIdentifier() { ItemElementName = PersonIdentifierChoiceType.EGN }
                                }
                            }
                        },
                        VehicleDealershipAddress = new EntityAddress(),
                    };
                }

                CheckForNonHandedAndNonPaidSlip(request, app);
            }

            return result;
        }

        protected override async Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var form = (ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsVM)request.FormData.Form;

            var res = await base.ValidateApplicationFormInternalAsync(
                request
                , cancellationToken
                , false
                , form.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality == ElectronicServiceAuthorQualityType.Personal);

            var isFirstReg = form.ElectronicAdministrativeServiceHeader.ApplicationType == ApplicationType.AppForFirstReg;

            if (isFirstReg)
            {
                if (form.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality == ElectronicServiceAuthorQualityType.Representative
                && (request.FormData.AttachedDocuments == null
                    || request.FormData.AttachedDocuments.Count == 0
                    || !request.FormData.AttachedDocuments.Any(d => d.DocumenTypeID == DocumentTypesStatic.NotarizedPowerOfAttorney)))
                {
                    //При заявяване на услугата в качеството на пълномощник е необходимо да приложите документ от вид "Нотариално заверено изрично пълномощно".
                    res.Add(new TextError("GL_MISSING_DOC_FOR_NOTARIZED_POWER_OF_ATTORNEY_E", "GL_MISSING_DOC_FOR_NOTARIZED_POWER_OF_ATTORNEY_E"));
                }
            }

            return res;
        }

        public override string SignatureXpath
        {
            get
            {
                return "afiotrptm:ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchants/afiotrptm:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"afiotrptm", "http://ereg.egov.bg/segment/R-3324"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}
