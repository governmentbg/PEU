using CNSys;
using EAU.Documents;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Migr.Documents.Domain;
using EAU.Migr.Documents.Domain.Models.Forms;
using EAU.Migr.Documents.Models.Forms;
using EAU.Migr.Documents.XSLT;
using EAU.Nomenclatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.BDS.NRBLD.Models;

namespace EAU.Migr.Documents
{
    internal class ApplicationForIssuingDocumentForForeignersService : ApplicationFormServiceBase<ApplicationForIssuingDocumentForForeigners, ApplicationForIssuingDocumentForForeignersVM>
    {
        public ApplicationForIssuingDocumentForForeignersService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisMigr.ApplicationForIssuingDocumentForForeigners;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3115_ApplicationForIssuingDocumentForForeigners.xslt",
                    Resolver = new MIGREmbeddedXmlResourceResolver()
                };
            }
        }

        protected override List<ElectronicServiceAuthorQualityType> GetPossibleAuthorQualities(ApplicationFormInitializationRequest reques)
        {
            return new List<ElectronicServiceAuthorQualityType>() { ElectronicServiceAuthorQualityType.Personal, ElectronicServiceAuthorQualityType.Representative };
        }

        protected override List<IdentityDocumentType> GetPossibleRecipientIdentityDocumentTypes(ApplicationFormInitializationRequest request)
        {
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

            if (service.AdditionalConfiguration.ContainsKey("identityDocumentType"))
                return service.AdditionalConfiguration["identityDocumentType"].Split(',').Select(t => (IdentityDocumentType)Convert.ToInt32(t)).ToList();

            return new List<IdentityDocumentType>() { IdentityDocumentType.PersonalCard };
        }

        protected override List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType> GetPossibleRecipientTypes(ApplicationFormInitializationRequest reques)
        {
            return new List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType>() { PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person };
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            var app = (ApplicationForIssuingDocumentForForeignersVM)request.Form;

            if (app != null)
            {
                if (app.Circumstances == null)
                    app.Circumstances = new Models.ApplicationForIssuingDocumentForForeignersDataVM();

                //При подаване на услугата в лично качество адреса се изчита от НАИФ НРБЛД, за това си го персистваме в "PersistedAddress"
                app.Circumstances.PersistedAddress = this.getPersonAdress(request.ApplicantInfo, new AddresType[] { AddresType.PermanentАddress, AddresType.CurrentАddress });
            }

            return result;
        }

        public override string SignatureXpath
        {
            get
            {
                return "aipsgap:ApplicationForIssuingDocumentForForeigners/aipsgap:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"aipsgap", "http://ereg.egov.bg/segment/R-3115" },
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}