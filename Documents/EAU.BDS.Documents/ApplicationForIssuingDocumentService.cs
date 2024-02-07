using CNSys;
using EAU.BDS.Documents.Domain;
using EAU.BDS.Documents.Domain.Models.Forms;
using EAU.BDS.Documents.Models;
using EAU.BDS.Documents.Models.Forms;
using EAU.BDS.Documents.XSLT;
using EAU.Documents;
using EAU.Documents.Common;
using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Models;
using EAU.Nomenclatures;
using EAU.Security;
using EAU.Utilities;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.BDS.NRBLD.Models;
using WAIS.Integration.MOI.Core.BDS.NRBLD;

namespace EAU.BDS.Documents
{
    internal class ApplicationForIssuingDocumentService : ApplicationFormServiceBase<ApplicationForIssuingDocument, ApplicationForIssuingDocumentVM>
    {
        private readonly INRBLDServicesClientFactory _nRBLDServicesClientFactory;

        public ApplicationForIssuingDocumentService(IServiceProvider serviceProvider, INRBLDServicesClientFactory iNRBLDServicesClientFactory) : base(serviceProvider)
        {
            _nRBLDServicesClientFactory = iNRBLDServicesClientFactory;
        }

        protected override string DocumentTypeUri => DocumentTypeUrisBDS.ApplicationForIssuingDocument;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3031_ApplicationForIssuingDocument.xslt",
                    Resolver = new BDSEmbeddedXmlResourceResolver()
                };
            }
        }

        protected override List<ElectronicServiceAuthorQualityType> GetPossibleAuthorQualities(ApplicationFormInitializationRequest request)
        {
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

            return request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationBG != null
                ? service.AdditionalConfiguration["possibleAuthorQualitiesBG"].Split(',').Select(t => (ElectronicServiceAuthorQualityType)Convert.ToInt32(t)).ToList()
                : service.AdditionalConfiguration["possibleAuthorQualitiesF"].Split(',').Select(t => (ElectronicServiceAuthorQualityType)Convert.ToInt32(t)).ToList();
        }

        protected override List<IdentityDocumentType> GetPossibleRecipientIdentityDocumentTypes(ApplicationFormInitializationRequest request)
        {
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

            if (service.AdditionalConfiguration.ContainsKey("identityDocumentType"))
                return service.AdditionalConfiguration["identityDocumentType"].Split(',').Select(t => (IdentityDocumentType)Convert.ToInt32(t)).ToList();

            return new List<IdentityDocumentType>();
        }

        protected override List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType> GetPossibleRecipientTypes(ApplicationFormInitializationRequest request)
        {
            var services = GetService<IServices>();
            var service = services.Search().Single(s => s.ServiceID == request.ServiceID);

            return service.AdditionalConfiguration["possibleRecipientTypes"].Split(',').Select(t => (PersonAndEntityBasicDataVM.PersonAndEntityChoiceType)Convert.ToInt32(t)).ToList();
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            var form = (ApplicationForIssuingDocumentVM)request.Form;
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

            #region Init Circumstances

            if (form.Circumstances == null)
                form.Circumstances = new ApplicationForIssuingDocumentDataVM();

            if (form.Circumstances.DocumentToBeIssuedFor == null)
                form.Circumstances.DocumentToBeIssuedFor = new DocumentToBeIssuedForVM();

            if (form.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.UnitInAdministration == null)
                form.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.UnitInAdministration = new ServiceApplicantReceiptDataUnitInAdministration();

            form.Circumstances.DocumentToBeIssuedFor.ChooseIssuingDocument = (IssueDocumentFor)Convert.ToInt32(service.AdditionalConfiguration["issueDocumentFor"]);

            if (form.Circumstances.DocumentToBeIssuedFor.ChooseIssuingDocument == IssueDocumentFor.IssuedBulgarianIdentityDocumentsInPeriod)
            {
                if(form.Circumstances.DocumentToBeIssuedFor.IssuedBulgarianIdentityDocumentsInPeriod == null)
                {
                    form.Circumstances.DocumentToBeIssuedFor.IssuedBulgarianIdentityDocumentsInPeriod = new IssuedBulgarianIdentityDocumentsInPeriodVM();
                    form.Circumstances.DocumentToBeIssuedFor.IssuedBulgarianIdentityDocumentsInPeriod.IdentitityIssueDate = new DateTime(2000, 01, 01, 00, 00, 00);
                    form.Circumstances.DocumentToBeIssuedFor.IssuedBulgarianIdentityDocumentsInPeriod.IdentitityExpireDate = DateTime.Now.RoundToEndOfDay(); //Края на днешние ден
                }
            }
            else
            {
                if(form.Circumstances.DocumentToBeIssuedFor.OtherInformationConnectedWithIssuedBulgarianIdentityDocuments == null)
                {
                    form.Circumstances.DocumentToBeIssuedFor.OtherInformationConnectedWithIssuedBulgarianIdentityDocuments = new OtherInformationConnectedWithIssuedBulgarianIdentityDocumentsVM()
                    {
                        DocumentNumbers = new List<DocumentNumber>() { new DocumentNumber() { Number = "" } },
                        DocumentsInfos = new List<Domain.Models.IssuedBulgarianIdentityDocumentInfo>() { new Domain.Models.IssuedBulgarianIdentityDocumentInfo() },
                        IncludsDataInCertificate = new List<Domain.Models.DataContainsInCertificateNomenclature>()
                    };
                }
            }

            if (form.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.ServiceResultReceiptMethod == null 
                && service.DeliveryChannels.Any(d => d.DeliveryChannelID == (short?)ServiceResultReceiptMethods.EmailOrWebApplication))
                form.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.ServiceResultReceiptMethod = ServiceResultReceiptMethods.EmailOrWebApplication;

            if (form.Circumstances.DocumentToBeIssuedFor.DocumentMustServeTo != null 
                && form.Circumstances.DocumentToBeIssuedFor.DocumentMustServeTo.ItemElementName == ItemChoiceType1.AbroadDocumentMustServeTo)
            {
                form.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.ServiceResultReceiptMethod = ServiceResultReceiptMethods.UnitInAdministration;
            }

            #endregion

            #region init PersonalInformation

            if (form.PersonalInformation == null)
            {
                form.PersonalInformation = new PersonalInformationVM();

                var userAccessor = GetService<IEAUUserAccessor>();
                var personInfoResponse = await _nRBLDServicesClientFactory.GetNRBLDServicesClient().GetPersonInfoAsync(userAccessor.User.PersonIdentifier, false, cancellationToken);
                form.PersonalInformation.PersonAddress = getPersonAdress(personInfoResponse.Response, new AddresType[] { AddresType.PermanentАddress, AddresType.CurrentАddress });
            }

            #endregion

            if (request.AdditionalData == null)
                request.AdditionalData = new AdditionalData();

            request.AdditionalData["hideIdentifierChoice"] = "true";

            return result;
        }

        protected async override Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var errCollection = await base.ValidateApplicationFormInternalAsync(request, cancellationToken);
            var localizer = GetService<IStringLocalizer>();

            if (request.RecipientInfo != null)
            {
                //Ако не е български гражданин
                if (request.RecipientInfo.PersonData.PersonIdentification.PersonIdentificationBG == null)
                {
                    if (request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationF != null)
                    {
                        var localizedError = localizer["GL_00018_E"].Value.Replace("{pid}", request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationF.LNC);
                        errCollection.Add(new TextError(localizedError, localizedError));
                    }
                }
                else
                {
                    //Ако идентификаторът не е ЕГН
                    CnsysValidatorBase v = new CnsysValidatorBase();
                    if (!v.ValidateEGN(request.RecipientInfo.PersonData.PersonIdentification.PersonIdentificationBG.PIN))
                        errCollection.Add(new TextError("DOC_BDS_RECIPIENT_ID_E", "DOC_BDS_RECIPIENT_ID_E"));
                }
            }
        
            var application = (ApplicationForIssuingDocumentVM)request.FormData.Form;

            if (application.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality == ElectronicServiceAuthorQualityType.Representative 
                && (request.FormData.AttachedDocuments == null
                    || request.FormData.AttachedDocuments.Count == 0
                    || !request.FormData.AttachedDocuments.Any(ad => ad.DocumenTypeID == DocumentTypesStatic.NotarizedPowerOfAttorney)))
            {
                //Task 4709 когато заявителят е в качеството на пълномощник и не приложил към заявлението документ от вид "Нотариално заверено изрично пълномощно".
                errCollection.Add(new TextError("GL_MISSING_DOC_FOR_NOTARIZED_POWER_OF_ATTORNEY_E", "GL_MISSING_DOC_FOR_NOTARIZED_POWER_OF_ATTORNEY_E"));
            }

            return errCollection;
        }

        public override string SignatureXpath
        {
            get
            {
                return "aipdbc:ApplicationForIssuingDocument/aipdbc:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"aipdbc", "http://ereg.egov.bg/segment/R-3031" },
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}
