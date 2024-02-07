using CNSys;
using EAU.Documents;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.KAT.Documents.Domain;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.KAT.Documents.Models.Forms;
using EAU.KAT.Documents.XSLT;
using EAU.Nomenclatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.KAT.Documents
{
    public class DeclarationForLostSRPPSService : ApplicationFormServiceBase<DeclarationForLostSRPPS, DeclarationForLostSRPPSVM>
    {
        public DeclarationForLostSRPPSService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.DeclarationForLostSRPPS;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3320_DeclarationForLostSRPPS.xslt",
                    Resolver = new KATEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "dflsrmps:DeclarationForLostSRPPS/dflsrmps:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"dflsrmps", "http://ereg.egov.bg/segment/R-3320"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
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

        protected override Task<OperationResult> InitializeDocumentFormInternalAsync(DocumentFormInitializationRequest request, CancellationToken cancellationToken)
        {
            if (request.AdditionalData == null)
                request.AdditionalData = new Utilities.AdditionalData();
            request.AdditionalData["hideRecipient"] = "true";

            return base.InitializeDocumentFormInternalAsync(request, cancellationToken);
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var app = (DeclarationForLostSRPPSVM)request.Form;
            ElectronicServiceAuthorQualityType? firstAppAuthorQualityType = app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality;
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            request.AdditionalData["disabledAuthorQuality"] = "true"; //MVREAU2020-1609
            
            //MVREAU2020-1621
            if (firstAppAuthorQualityType.HasValue)
                app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality = firstAppAuthorQualityType.Value;

            CheckForNonHandedAndNonPaidSlip(request, app);

            return result;
        }
    }
}