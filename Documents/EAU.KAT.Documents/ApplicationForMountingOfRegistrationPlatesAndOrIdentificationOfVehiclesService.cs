using CNSys;
using EAU.Documents;
using EAU.Documents.Common;
using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Models;
using EAU.KAT.Documents.Domain;
using EAU.KAT.Documents.Domain.Models;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.KAT.Documents.Models;
using EAU.KAT.Documents.Models.Forms;
using EAU.KAT.Documents.XSLT;
using EAU.Nomenclatures;
using EAU.Services.Nomenclatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.KAT.Documents
{
    internal class ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesService :
       ApplicationFormServiceBase<ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehicles, ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesVM>
    {
        public ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesService(IServiceProvider serviceProvider) 
            : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehicles;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3322_ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehicles.xslt",
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
                ElectronicServiceAuthorQualityType.Personal,
                ElectronicServiceAuthorQualityType.Representative,
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
                PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person,
                PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Entity
            };
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            var app = (ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesVM)request.Form;
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);
            var servingUnitsInfoService = GetService<IServingUnitsInfo>();

            if (app != null)
            {
                if (app.Circumstances == null)
                {
                    app.Circumstances = new ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesDataVM();
                }

                //Структура на МВР, предоставяща услугата
                app.Circumstances.PoliceDepartment = new PoliceDepartment();
                var issuingPoliceDepartment = service.AdditionalConfiguration["issuingPoliceDepartment"];
                if (!string.IsNullOrWhiteSpace(issuingPoliceDepartment))
                {
                    await servingUnitsInfoService.EnsureLoadedAsync(request.ServiceID.Value, cancellationToken);
                    var servingUnits = servingUnitsInfoService.Search(request.ServiceID.Value, out DateTime? lastModifiedDate);
                    var unitInfo = servingUnits.FirstOrDefault(s => s.UnitID == int.Parse(issuingPoliceDepartment));

                    if (unitInfo != null)
                    {
                        app.Circumstances.PoliceDepartment.PoliceDepartmentCode = unitInfo.UnitID.ToString();
                        app.Circumstances.PoliceDepartment.PoliceDepartmentName = unitInfo.Name;
                    }
                }

                CheckForNonHandedAndNonPaidSlip(request, app);
            }

            return result;
        }

        protected override async Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var res = await base.ValidateApplicationFormInternalAsync(request, cancellationToken, false);

            var form = (ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesVM)request.FormData.Form;

            if (form.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality == ElectronicServiceAuthorQualityType.Representative
            && (request.FormData.AttachedDocuments == null
                || request.FormData.AttachedDocuments.Count == 0
                || !request.FormData.AttachedDocuments.Any(d => d.DocumenTypeID == DocumentTypesStatic.NotarizedPowerOfAttorney)))
            {
                //При заявяване на услугата в качеството на пълномощник е необходимо да приложите документ от вид "Нотариално заверено изрично пълномощно".
                res.Add(new TextError("GL_MISSING_DOC_FOR_NOTARIZED_POWER_OF_ATTORNEY_E", "GL_MISSING_DOC_FOR_NOTARIZED_POWER_OF_ATTORNEY_E"));
            }

            return res;
        }

        public override string SignatureXpath
        {
            get
            {
                return "afmorpaoiov:ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehicles/afmorpaoiov:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"afmorpaoiov", "http://ereg.egov.bg/segment/R-3322"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}
