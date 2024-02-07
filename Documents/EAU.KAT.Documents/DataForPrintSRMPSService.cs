using CNSys;
using EAU.Documents;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.KAT.Documents.Domain;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.KAT.Documents.Models.Forms;
using EAU.Nomenclatures;
using EAU.Users;
using EAU.Utilities;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.KAT.Documents
{
    public class DataForPrintSRMPSService : ApplicationFormServiceBase<DataForPrintSRMPS, DataForPrintSRMPSVM>
    {
        public DataForPrintSRMPSService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.DataForPrintSRMPS;

        protected override PrintPreviewData PrintPreviewData
        {
            get { return null; }
        }

        public override string SignatureXpath
        {
            get
            {
                return "dfpsrmps:DataForPrintSRMPS/dfpsrmps:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"dfpsrmps", "http://ereg.egov.bg/segment/R-3308"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }

        protected override List<ElectronicServiceAuthorQualityType> GetPossibleAuthorQualities(ApplicationFormInitializationRequest reques)
        {
            return new List<ElectronicServiceAuthorQualityType>() { ElectronicServiceAuthorQualityType.Official };
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

        protected async override Task<OperationResult> InitializeDocumentFormInternalAsync(DocumentFormInitializationRequest request, CancellationToken cancellationToken)
        {
            request.AdditionalData["labelForOfficial"] = "DOC_GL_ElectronicServiceAuthorQualityType_OfficialNotarius_L";
            request.AdditionalData["hideRecipient"] = "true";

            var result = await base.InitializeDocumentFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
            {
                return result;
            }

            var doc = (DataForPrintSRMPSVM)request.Form;

            if (request.Mode == DocumentModes.ViewDocument && doc.ElectronicServiceApplicant.RecipientGroup.Recipient == null)
            {
                doc.ElectronicServiceApplicant.RecipientGroup.Recipient = new ElectronicServiceRecipientVM()
                {
                    ItemPersonBasicData = doc.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.Author.ItemPersonBasicData
                };
            }

            return result;
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(
           ApplicationFormInitializationRequest request,
           CancellationToken cancellationToken)
        {
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            #region NotaryValidation

            var notaryInfo = await GetService<IUserNotaryService>().GetUserNotaryInfoAsync(cancellationToken);

            if (notaryInfo.Errors != null || !notaryInfo.HasNotaryRights)
            {
                var localizer = GetService<IStringLocalizer>();
                var errors = new ErrorCollection { new TextError("GL_NOT_NOTARY_E", localizer["GL_NOT_NOTARY_E"].Value) };

                return new OperationResult(new ErrorCollection(errors));
            }

            #endregion

            var doc = (DataForPrintSRMPSVM)request.Form;

            if (doc.Circumstances.HolderData == null && doc.Circumstances?.NewOwners?.Count > 0)
            {
                doc.Circumstances.HolderData = doc.Circumstances.NewOwners[0];
            }

            if (request.AdditionalData == null)
            {
                request.AdditionalData = new AdditionalData();
            }
           
            request.AdditionalData["skipValidateRecipients"] = "true";
            request.AdditionalData["mainPoliceDepartmentCode"] = doc.Circumstances.IssuingPoliceDepartment.PoliceDepartmentCode;

            return result;
        }
    }
}