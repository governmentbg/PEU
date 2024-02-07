using CNSys;
using EAU.Documents.Domain;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Documents.Models.Forms;
using EAU.Documents.XSLT;
using EAU.Nomenclatures;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.Core.BDS.NRBLD;

namespace EAU.Documents
{
    public class ApplicationForWithdrawServiceService
        : ApplicationFormServiceBase<ApplicationForWithdrawService, ApplicationForWithdrawServiceVM>
    {
        private readonly INRBLDServicesClientFactory _nRBLDServicesClientFactory;
        private readonly IConfiguration _configuration;

        public ApplicationForWithdrawServiceService(IServiceProvider serviceProvider, INRBLDServicesClientFactory iNRBLDServicesClientFactory, IConfiguration configuration)
            : base(serviceProvider)
        {
            _nRBLDServicesClientFactory = iNRBLDServicesClientFactory;
            _configuration = configuration;
        }

        public override string SignatureXpath
        {
            get
            {
                return "aipdbd:ApplicationForWithdrawService/aipdbd:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"aipdbd", "http://ereg.egov.bg/segment/R-3059" },
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }

        protected override string DocumentTypeUri => DocumentTypeUris.ApplicationForWithdrawService;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3059_ApplicationForWithdrawService.xslt",
                    Resolver = new CommonEmbeddedXmlResourceResolver()
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
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken, true, true);

            if (!result.IsSuccessfullyCompleted)
                return result;

            #region Model Initialization

            var form = (ApplicationForWithdrawServiceVM)request.Form;

            if (form.Circumstances == null) form.Circumstances = new ApplicationForWithdrawServiceDataVM();

            var orgAppURI = new WAIS.Integration.EPortal.Models.URI(request.AdditionalData["caseFileURI"]);
            form.Circumstances.CaseFileURI = request.AdditionalData["caseFileURI"];

            form.ElectronicAdministrativeServiceHeader.DocumentURI = new Domain.Models.DocumentURI()
            {
                RegisterIndex = orgAppURI.RegisterIndex,
                SequenceNumber = orgAppURI.SequenceNumber,
                ReceiptOrSigningDate = orgAppURI.ReceiptOrSigningDate
            };

            #endregion

            return result;
        }

        protected async override Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var form = (ApplicationForWithdrawServiceVM)request.FormData.Form;
            var service = GetService<IServices>().Search().First(s => s.SunauServiceUri == form.ElectronicAdministrativeServiceHeader.SUNAUServiceURI);

            //TODO Да се провери кога и дали трябва да се прави тази проверка
            var result = await base.ValidateApplicationFormInternalAsync(request, cancellationToken, true);

            return result;
        }

        #region Helpers
        #endregion
    }
}
