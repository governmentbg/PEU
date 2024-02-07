using CNSys;
using EAU.Documents;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.KAT.Documents.Domain;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.KAT.Documents.Models.Forms;
using EAU.KAT.Documents.XSLT;
using EAU.Nomenclatures;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.BDS.NRBLD.Models;
using WAIS.Integration.MOI.KAT.AND;

namespace EAU.KAT.Documents
{
    internal class RequestForApplicationForIssuingDuplicateDrivingLicenseService :
        ApplicationFormServiceBase<RequestForApplicationForIssuingDuplicateDrivingLicense, RequestForApplicationForIssuingDuplicateDrivingLicenseVM>
    {
        private readonly IANDServicesClientFactory _iANDServicesClientFactory;

        public RequestForApplicationForIssuingDuplicateDrivingLicenseService(IServiceProvider serviceProvider, IANDServicesClientFactory iANDServicesClientFactory) : base(serviceProvider)
        {
            _iANDServicesClientFactory = iANDServicesClientFactory;
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.RequestForApplicationForIssuingDuplicateDrivingLicense;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3045_RequestForApplicationForIssuingDuplicateDrivingLicense.xslt",
                    Resolver = new KATEmbeddedXmlResourceResolver()
                };
            }
        }

        protected override List<ElectronicServiceAuthorQualityType> GetPossibleAuthorQualities(ApplicationFormInitializationRequest reques)
        {
            return new List<ElectronicServiceAuthorQualityType>() { ElectronicServiceAuthorQualityType.Personal };
        }

        protected override List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType> GetPossibleRecipientTypes(ApplicationFormInitializationRequest reques)
        {
            return new List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType>() { PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person };
        }

        protected override List<IdentityDocumentType> GetPossibleRecipientIdentityDocumentTypes(ApplicationFormInitializationRequest request)
        {
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

            if (service.AdditionalConfiguration.ContainsKey("identityDocumentType"))
                return service.AdditionalConfiguration["identityDocumentType"].Split(',').Select(t => (IdentityDocumentType)Convert.ToInt32(t)).ToList();

            return new List<IdentityDocumentType>() { IdentityDocumentType.PersonalCard };
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            var app = (RequestForApplicationForIssuingDuplicateDrivingLicenseVM)request.Form;

            if (app != null)
            {
                CheckForNonHandedAndNonPaidSlip(request, app);
            }

            return result;
        }

        protected async override Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.ValidateApplicationFormInternalAsync(request, cancellationToken);
            var application = (RequestForApplicationForIssuingDuplicateDrivingLicenseVM)request.FormData.Form;

            if (application != null && request.RecipientInfo != null)
            {
                var driverLicense = request.RecipientInfo.Document?.FirstOrDefault(d => d.DocumentType.Type.Code == DocTypeCode.DriverLicense || d.DocumentType.Type.Code == DocTypeCode.DriverLicenseForForeigner);
                var ident = application.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier;

                if (driverLicense != null)
                {
                    //REQUIREMENT 2557
                    //var andResponse = await _iANDServicesClientFactory.GetANDServicesClient().GetObligationDocumentsByLicenceNumAsync(new ObligationDocumentsByLicenceNumRequest()
                    //{
                    //    egn = ident.Item,
                    //    licenceNum = driverLicense.Number
                    //}, cancellationToken);

                    //if (andResponse.IsSuccessfullyCompleted)
                    //{
                    //    ObligationDocumentsByLicenceNumResponse ANDRecipientObligationDocuments = andResponse.Response;
                    //    if (ANDRecipientObligationDocuments.allObligations != null && ANDRecipientObligationDocuments.allObligations.Length > 0)
                    //    {
                    //        if (ANDRecipientObligationDocuments.allObligations.Any(o => o.isServed.HasValue && o.isServed.Value))
                    //        {
                    //            //Има връчени, но неплатени постановления
                    //            var localizer = GetService<IStringLocalizer>();
                    //            var localizedError = localizer["GL_00012_E"].Value.Replace("{pid}", ident.Item);
                    //            result.Add(new TextError(localizedError, localizedError));
                    //        }
                    //    }
                    //}
                }
                else
                {
                    var localizer = GetService<IStringLocalizer>();
                    var errorMsg = localizer["GL_00024_E"].Value.Replace("{pid}", ident.Item);
                    result.Add(new TextError(errorMsg, errorMsg));
                }
            }

            return result;
        }

        public override string SignatureXpath
        {
            get
            {
                return "aipsgap:RequestForApplicationForIssuingDuplicateDrivingLicense/aipsgap:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"aipsgap", "http://ereg.egov.bg/segment/R-3045"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}
