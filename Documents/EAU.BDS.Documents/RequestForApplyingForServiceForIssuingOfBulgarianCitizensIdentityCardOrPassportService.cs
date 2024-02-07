using CNSys;
using EAU.BDS.Documents.Domain;
using EAU.BDS.Documents.Domain.Models.Forms;
using EAU.BDS.Documents.Models;
using EAU.BDS.Documents.Models.Forms;
using EAU.Documents;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Nomenclatures;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.BDS.Documents
{
    internal class RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassportService
        : ApplicationFormServiceBase<RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassport, RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassportVM>
    {
        public RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassportService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisBDS.RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassport;

        protected override PrintPreviewData PrintPreviewData { get { return null; } }

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

            return result;
        }

        protected async override Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var errCollection = await base.ValidateApplicationFormInternalAsync(request, cancellationToken);
            var localizer = GetService<IStringLocalizer>();

            //Ако не е български гражданин
            if (request.RecipientInfo.PersonData.PersonIdentification.PersonIdentificationBG == null)
            {
                var localizedError = localizer["GL_00018_E"].Value.Replace("{pid}", request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationF.LNC);
                errCollection.Add(new TextError(localizedError, localizedError));
            }

            return errCollection;
        }

        public override string SignatureXpath
        {
            get
            {
                return "rasibic:RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassport/rasibic:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"rasibic", "http://ereg.egov.bg/segment/R-3001"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}