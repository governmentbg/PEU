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
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.Core.BDS.NRBLD;
using WAIS.Integration.RegiX;

namespace EAU.KAT.Documents
{
    internal class ApplicationForInitialVehicleRegistrationService : ApplicationFormServiceBase<ApplicationForInitialVehicleRegistration, ApplicationForInitialVehicleRegistrationVM>
    {
        private readonly INRBLDServicesClientFactory _nRBLDServicesClientFactory;

        public ApplicationForInitialVehicleRegistrationService(IServiceProvider serviceProvider, INRBLDServicesClientFactory nRBLDServicesClientFactory)
            : base(serviceProvider)
        {
            _nRBLDServicesClientFactory = nRBLDServicesClientFactory;
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.ApplicationForInitialVehicleRegistration;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3330_ApplicationForInitialVehicleRegistration.xslt",
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

            var app = (ApplicationForInitialVehicleRegistrationVM)request.Form;
            var isForRemovingIregularity = request.AdditionalData.ContainsKey("removingIrregularitiesInstructionURI");

            if (isForRemovingIregularity)
            {
                request.AdditionalData["disabledAuthorQuality"] = "true";
            }

            if (app != null)
            {
                if (app.Circumstances == null)
                {
                    app.Circumstances = new ApplicationForInitialVehicleRegistrationDataVM();

                    app.Circumstances.VehicleIdentificationData = new ApplicationForInitialVehicleRegistrationDataVehicleIdentificationData();

                    var author = app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.Author;
                    var authorQuality = app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality;

                    app.Circumstances.OwnersCollection = new ApplicationForInitialVehicleRegistrationDataOwnersCollectionVM()
                    {
                        Items = new List<InitialVehicleRegistrationOwnerDataVM>() { new InitialVehicleRegistrationOwnerDataVM() }
                    };

                    if (author != null && authorQuality == ElectronicServiceAuthorQualityType.Personal)
                    {
                        var owner = app.Circumstances.OwnersCollection.Items[0];

                        owner.Type = PersonEntityChoiceType.Person;
                        owner.PersonIdentifier = (PersonIdentifier)author.ItemPersonBasicData.Identifier.CloneObject();
                    }

                    if (author != null && authorQuality == ElectronicServiceAuthorQualityType.LegalRepresentative)
                    {
                        var owner = app.Circumstances.OwnersCollection.Items[0];
                        owner.Type = PersonEntityChoiceType.Entity;
                    }

                    app.Circumstances.OwnerOfRegistrationCoupon = new InitialVehicleRegistrationUserOrOwnerOfSRMPSVM()
                    {
                        Type = PersonEntityChoiceType.Person,
                        PersonIdentifier = new PersonIdentifier() { ItemElementName = PersonIdentifierChoiceType.EGN }
                    };
                }

                CheckForNonHandedAndNonPaidSlip(request, app);
            }

            return result;
        }

        protected override async Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var form = (ApplicationForInitialVehicleRegistrationVM)request.FormData.Form;

            var res = await base.ValidateApplicationFormInternalAsync(request
                , cancellationToken
                , false
                , form.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality == ElectronicServiceAuthorQualityType.Personal);

            #region Проверка на първият собственик.

            var firstOwner = form.Circumstances.OwnersCollection.Items.First();

            if (firstOwner.Type == PersonEntityChoiceType.Person)
            {
                var nRBLDData = await _nRBLDServicesClientFactory.GetNRBLDServicesClient().GetPersonInfoAsync(firstOwner.PersonIdentifier.Item, false, cancellationToken);

                if (!nRBLDData.IsSuccessfullyCompleted)
                {
                    nRBLDData.Errors.ForEach(el =>
                    {
                        res.Add(new TextError(el.Code, el.Message));
                    });

                    return res;
                }

                if (nRBLDData.Response.PersonData == null)
                {
                    //няма данни за лицето.
                    var localizer = GetService<IStringLocalizer>();
                    var errMsg = localizer["GL_00016_E"].Value.Replace("{pid}", firstOwner.PersonIdentifier.Item);

                    res.Add(new TextError(errMsg, errMsg));

                    return res;
                }

                if (nRBLDData.Response.PersonData.DeathDate.HasValue)
                {
                    //има данни за дата на смърт за лицето.
                    var localizer = GetService<IStringLocalizer>();
                    var errMsg = localizer["GL_00021_E"].Value.Replace("{pid}", firstOwner.PersonIdentifier.Item);

                    res.Add(new TextError(errMsg, errMsg));

                    return res;
                }

                #region Проверка за адрес

                var hasRequiredAddress = false;

                if (nRBLDData.Response.Address != null)
                {
                    if (nRBLDData.Response.PersonData.PersonIdentification.PersonIdentificationBG != null ||
                        (nRBLDData.Response.PersonData.PersonIdentification.PersonIdentificationF != null && nRBLDData.Response.PersonData.PersonIdentification.PersonIdentificationF.Statut.code == WAIS.Integration.MOI.BDS.NRBLD.Models.StatutCode.ForeignerPermanently))
                    {
                        hasRequiredAddress = nRBLDData.Response.Address.Any(a => a.id == WAIS.Integration.MOI.BDS.NRBLD.Models.AddresType.PermanentАddress);
                    }
                    else
                        hasRequiredAddress = nRBLDData.Response.Address.Any(a => a.id == WAIS.Integration.MOI.BDS.NRBLD.Models.AddresType.CurrentАddress);
                }

                if(!hasRequiredAddress)
                {
                    //няма данни за актуален постоянен/настоящ адрес за лицето.
                    var localizer = GetService<IStringLocalizer>();
                    var errMsg = localizer["GL_00017_E"].Value.Replace("{pid}", firstOwner.PersonIdentifier.Item);
                    res.Add(new TextError(errMsg, errMsg));

                    return res;
                }

                #endregion                
            }
            else
            {
                var regiXEntityData = await GetService<IEntityDataServicesClientFactory>().GetEntityDataServicesClient().GetEntityDataAsync(firstOwner.EntityIdentifier, CancellationToken.None);

                if (!regiXEntityData.IsSuccessfullyCompleted)
                {
                    regiXEntityData.Errors.ForEach(el =>
                    {
                        res.Add(new TextError(el.Code, el.Message));
                    });

                    return res;
                }

                if (string.IsNullOrEmpty(regiXEntityData.Response.Identifier))
                {
                    //няма данни за фирмата.
                    var localizer = GetService<IStringLocalizer>();

                    var errMsg = localizer["GL_00030_E"].Value.Replace("{eik}", firstOwner.EntityIdentifier);

                    res.Add(new TextError(errMsg, errMsg));

                    return res;
                }

                if (regiXEntityData.Response.Address == null
                    || (string.IsNullOrEmpty(regiXEntityData.Response.Address.Settlement)
                        && string.IsNullOrEmpty(regiXEntityData.Response.Address.SettlementEKATTE))
                        )
                {
                    //няма данни за адрес на фирмата.
                    var localizer = GetService<IStringLocalizer>();
                    var errMsg = localizer["GL_00031_E"].Value.Replace("{eik}", firstOwner.EntityIdentifier);

                    res.Add(new TextError(errMsg, errMsg));

                    return res;
                }
            }

            #endregion

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
                return "afovr:ApplicationForInitialVehicleRegistration/afovr:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"afovr", "http://ereg.egov.bg/segment/R-3330"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}
