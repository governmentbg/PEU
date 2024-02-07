using CNSys;
using EAU.Documents;
using EAU.Documents.Common;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.KAT.Documents.Domain;
using EAU.KAT.Documents.Domain.Models;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.KAT.Documents.Models;
using EAU.KAT.Documents.Models.Forms;
using EAU.KAT.Documents.XSLT;
using EAU.Nomenclatures;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.KAT.Documents
{
    internal class ApplicationForCommissioningTemporarilySuspendedVehicleService :
       ApplicationFormServiceBase<ApplicationForCommissioningTemporarilySuspendedVehicle, ApplicationForCommissioningTemporarilySuspendedVehicleVM>
    {
        public ApplicationForCommissioningTemporarilySuspendedVehicleService(IServiceProvider serviceProvider) 
            : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.ApplicationForCommissioningTemporarilySuspendedVehicle;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3319_ApplicationForCommissioningTemporarilySuspendedVehicle.xslt",
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

        protected override Task<OperationResult> InitializeDocumentFormInternalAsync(DocumentFormInitializationRequest request, CancellationToken cancellationToken)
        {
            if (request.AdditionalData == null)
                request.AdditionalData = new Utilities.AdditionalData();
            request.AdditionalData["hideRecipient"] = "true";

            return base.InitializeDocumentFormInternalAsync(request, cancellationToken);
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            //MVREAU2020-1582
            var app = (ApplicationForCommissioningTemporarilySuspendedVehicleVM)request.Form;
            var isForRemovingIregularity = request.AdditionalData.ContainsKey("removingIrregularitiesInstructionURI");
            ElectronicServiceAuthorQualityType? firstAppAuthorQualityType = null;

            if (isForRemovingIregularity)
            {
                firstAppAuthorQualityType = app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality;
                request.AdditionalData["disabledAuthorQuality"] = "true"; //MVREAU2020 - 1609
            }

            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            //MVREAU2020-1582
            if (isForRemovingIregularity && firstAppAuthorQualityType.HasValue)
            {
                app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality = firstAppAuthorQualityType.Value;
            }

            if (app != null)
            {
                if (app.Circumstances == null)
                {
                    app.Circumstances = new VehicleDataRequestVM();

                    var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

                    if (service.AdditionalConfiguration != null && service.AdditionalConfiguration.ContainsKey("serviceCode"))
                        app.Circumstances.ServiceCode = service.AdditionalConfiguration["serviceCode"];
                    else
                        throw new NotSupportedException("Missing key \"serviceCode\" in AdditionalConfiguration.");

                    if (service.AdditionalConfiguration != null && service.AdditionalConfiguration.ContainsKey("serviceName"))
                        app.Circumstances.ServiceName = service.AdditionalConfiguration["serviceName"];
                    else
                        throw new NotSupportedException("Missing key \"serviceName\" in AdditionalConfiguration.");

                    app.Circumstances.Reasons = new List<AISKATReason>() { new AISKATReason() };

                    app.Circumstances.RegistrationData = new VehicleRegistrationData();
                    app.Circumstances.RegistrationData.RegistrationCertificateType = RegistrationCertificateTypeNomenclature.RegistrationDocument;

                    app.Circumstances.OwnersCollection = new OwnersCollectionVM();
                    app.Circumstances.OwnersCollection.Owners = new List<OwnerVM>();

                    var quality = app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality;
                    var author = app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.Author;

                    //Винаги получател на услугата е лицето заявител.
                    app.ElectronicServiceApplicant.RecipientGroup.Recipient = new ElectronicServiceRecipientVM()
                    {
                        SelectedChoiceType = PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person,
                        ItemPersonBasicData = (PersonBasicDataVM)author.ItemPersonBasicData.CloneObject()
                    };
                    
                    if (author != null && quality == ElectronicServiceAuthorQualityType.Personal)
                    {
                        app.Circumstances.OwnersCollection.Owners.Add(new OwnerVM()
                        { 
                            Type = PersonEntityChoiceType.Person,
                            PersonIdentifier = author.ItemPersonBasicData.Identifier
                        });

                    }

                    if(author != null && quality == ElectronicServiceAuthorQualityType.LegalRepresentative)
                    {
                        app.Circumstances.OwnersCollection.Owners.Add(new OwnerVM()
                        {
                            Type = PersonEntityChoiceType.Entity,
                        });
                    }
                }

                CheckForNonHandedAndNonPaidSlip(request, app);
            }

            return result;
        }

        protected override async Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var res = await base.ValidateApplicationFormInternalAsync(request, cancellationToken, false, true);

            var form = (ApplicationForCommissioningTemporarilySuspendedVehicleVM)request.FormData.Form;

            if (form.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality == ElectronicServiceAuthorQualityType.Representative
                && (request.FormData.AttachedDocuments == null
                    || request.FormData.AttachedDocuments.Count == 0
                    || !request.FormData.AttachedDocuments.Any(d => d.DocumenTypeID == DocumentTypesStatic.NotarizedPowerOfAttorney || d.DocumenTypeID == DocumentTypesStatic.CertificateOfInheritance)))
            {
                //Като заявител на услугата в качеството на пълномощник на друго физическо/юридическо лице, трябва да приложите документ от вид "Нотариално заверено изрично пълномощно" и/или "Удостоверение за наследници".
                res.Add(new TextError("DOC_KAT_MissingNotarizedExplicitPowerOfAttorney_E", "DOC_KAT_MissingNotarizedExplicitPowerOfAttorney_E"));
            }

            return res;
        }

        public override string SignatureXpath
        {
            get
            {
                return "apfctsv:ApplicationForCommissioningTemporarilySuspendedVehicle/apfctsv:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"apfctsv", "http://ereg.egov.bg/segment/R-3319"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}
