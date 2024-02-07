using CNSys;
using EAU.Documents;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Nomenclatures;
using EAU.PBZN.Documents.Domain;
using EAU.PBZN.Documents.Domain.Models.Forms;
using EAU.PBZN.Documents.Models.Forms;
using EAU.PBZN.Documents.XSLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.PBZN.Documents
{
    internal class ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutService :
     ApplicationFormServiceBase<ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOut, ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutVM>
    {
        public ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisPBZN.ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOut;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3123_ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOut.xslt",
                    Resolver = new PBZNEmbeddedXmlResourceResolver()
                };
            }
        }

        protected override List<ElectronicServiceAuthorQualityType> GetPossibleAuthorQualities(ApplicationFormInitializationRequest reques)
        {
            return new List<ElectronicServiceAuthorQualityType>() { ElectronicServiceAuthorQualityType.Personal, ElectronicServiceAuthorQualityType.Representative, ElectronicServiceAuthorQualityType.LegalRepresentative };
        }

        protected override List<IdentityDocumentType> GetPossibleRecipientIdentityDocumentTypes(ApplicationFormInitializationRequest reques)
        {
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == reques.ServiceID);

            if (service.AdditionalConfiguration.ContainsKey("identityDocumentType"))
                return service.AdditionalConfiguration["identityDocumentType"].Split(',').Select(t => (IdentityDocumentType)Convert.ToInt32(t)).ToList();

            return new List<IdentityDocumentType>();
        }

        protected override List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType> GetPossibleRecipientTypes(ApplicationFormInitializationRequest reques)
        {
            return new List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType>() { PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person, PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Entity };
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            var app = (ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutVM)request.Form;

            if (app != null)
            {
                if (app.Circumstances == null)
                    app.Circumstances = new Models.ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataVM();
            }

            return result;
        }


        public override string SignatureXpath
        {
            get
            {
                return "aipsgap:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOut/aipsgap:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"aipsgap", "http://ereg.egov.bg/segment/R-3123"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}
