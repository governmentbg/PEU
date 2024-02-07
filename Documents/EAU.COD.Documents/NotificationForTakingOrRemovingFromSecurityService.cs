using CNSys;
using EAU.COD.Documents.Domain;
using EAU.COD.Documents.Domain.Models;
using EAU.COD.Documents.Domain.Models.Forms;
using EAU.COD.Documents.Models.Forms;
using EAU.COD.Documents.XSLT;
using EAU.Documents;
using EAU.Documents.Common;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Nomenclatures;
using EAU.Security;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.CHOD;
using WAIS.Integration.RegiX;

namespace EAU.COD.Documents
{
    internal class NotificationForTakingOrRemovingFromSecurityService
        : ApplicationFormServiceBase<NotificationForTakingOrRemovingFromSecurity, NotificationForTakingOrRemovingFromSecurityVM>
    {
        private readonly ICHODServicesClientFactory _cHODServicesClientFactory;
        private readonly IEAUUserAccessor _userAccessor;

        public NotificationForTakingOrRemovingFromSecurityService(IServiceProvider serviceProvider, ICHODServicesClientFactory cHODServicesClientFactory, IEAUUserAccessor userAccessor) : base(serviceProvider)
        {
            _cHODServicesClientFactory = cHODServicesClientFactory;
            _userAccessor = userAccessor;
        }

        protected override string DocumentTypeUri => DocumentTypeUrisCOD.NotificationForTakingOrRemovingFromSecurity;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3160_NotificationForTakingOrRemovingFromSecurity.xslt",
                    Resolver = new CODEmbeddedXmlResourceResolver()
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

            return new List<ElectronicServiceAuthorQualityType>() { ElectronicServiceAuthorQualityType.LegalRepresentative, ElectronicServiceAuthorQualityType.Representative };
        }

        protected override List<IdentityDocumentType> GetPossibleRecipientIdentityDocumentTypes(ApplicationFormInitializationRequest reques)
        {
            return new List<IdentityDocumentType>() { IdentityDocumentType.PersonalCard };
        }

        protected override List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType> GetPossibleRecipientTypes(ApplicationFormInitializationRequest request)
        {
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

            if (service.AdditionalConfiguration != null && service.AdditionalConfiguration.ContainsKey("possibleRecipientTypes"))
                return service.AdditionalConfiguration["possibleRecipientTypes"].Split(',').Select(t => (PersonAndEntityBasicDataVM.PersonAndEntityChoiceType)Convert.ToInt32(t)).ToList();

            return new List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType>() { PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Entity };
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            var form = (NotificationForTakingOrRemovingFromSecurityVM)request.Form;
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);
            var isValidEntityWithActiveLicence = await ValidateEntityUICAndActiveLicence(cancellationToken);

            if (!isValidEntityWithActiveLicence.IsSuccessfullyCompleted)
                return isValidEntityWithActiveLicence;

            if (form?.ElectronicServiceApplicant?.RecipientGroup?.Recipient?.SelectedChoiceType == PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Entity)
            {
                var regiXEntityData = await GetService<IEntityDataServicesClientFactory>().GetEntityDataServicesClient().GetEntityDataAsync(_userAccessor.User.UIC, CancellationToken.None);

                if (regiXEntityData.IsSuccessfullyCompleted)
                {
                    form.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemEntityBasicData.Identifier = regiXEntityData.Response.Identifier;
                    form.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemEntityBasicData.Name = regiXEntityData.Response.Name;
                    request.AdditionalData["disabledEntityIdentifier"] = "true";
                }
            }

            if (form.Circumstances == null)
                form.Circumstances = new NotificationForTakingOrRemovingFromSecurityDataVM()
                {
                    ContractAssignor = new ContractAssignor() { PersonAssignorData = new PersonAssignorData(), AssignorPersonEntityType = AssignorPersonEntityType.Person},
                    IssuingPoliceDepartment = new PoliceDepartment(),
                    SecurityContractData = new SecurityContractData(),
                    NotificationType = NotificationType.NewSecurityContr235789
                };

            if (form.SecurityObjectsData == null)
            {
                form.SecurityObjectsData = new SecurityObjectsDataVM();
                form.SecurityObjectsData.SecurityObjects = new List<SecurityObject>();
            }

            return result;
        }

        protected async override Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.ValidateApplicationFormInternalAsync(request, cancellationToken);
            var application = (NotificationForTakingOrRemovingFromSecurityVM)request.FormData.Form;

            var isValidEntityWithActiveLicence = await ValidateEntityUICAndActiveLicence(cancellationToken);

            if (!isValidEntityWithActiveLicence.IsSuccessfullyCompleted)
                return isValidEntityWithActiveLicence.Errors;

            if (application.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemEntityBasicData == null
                || string.IsNullOrWhiteSpace(application.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemEntityBasicData.Identifier)
                || application.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemEntityBasicData.Identifier != _userAccessor.User.UIC)
            {
                result.Add(new TextError("DOC_COD_RECIPIENT_IDENTIFIER_AND_KEP_DOESNT_MATCH_E", "DOC_COD_RECIPIENT_IDENTIFIER_AND_KEP_DOESNT_MATCH_E"));
            }

            //Валидират се само заявления за първоначално вписване.
            if (application.ElectronicAdministrativeServiceHeader.ApplicationType == ApplicationType.AppForFirstReg)
            {
                if (application.ElectronicServiceApplicant?.RecipientGroup?.AuthorWithQuality?.SelectedAuthorQuality == ElectronicServiceAuthorQualityType.Representative
                    && (request.FormData.AttachedDocuments == null || request.FormData.AttachedDocuments.Count == 0 || !request.FormData.AttachedDocuments.Any(d => d.DocumenTypeID == DocumentTypesStatic.NotarizedPowerOfAttorney)))
                {
                    //Липсва прикачен документ Нотариално заверено изрично пълномощно
                    result.Add(new TextError("GL_MISSING_DOC_FOR_NOTARIZED_POWER_OF_ATTORNEY_2_E", "GL_MISSING_DOC_FOR_NOTARIZED_POWER_OF_ATTORNEY_2_E"));
                }
            }

            return result;
        }

        public override string SignatureXpath
        {
            get
            {
                return "nftorfs:NotificationForTakingOrRemovingFromSecurity/nftorfs:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"nftorfs", "http://ereg.egov.bg/segment/R-3160"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }

        private async Task<OperationResult> ValidateEntityUICAndActiveLicence(CancellationToken cancellationToken)
        {
            var localizer = GetService<IStringLocalizer>();

            if (_userAccessor.User == null || string.IsNullOrWhiteSpace(_userAccessor.User.UIC))
            {
                //Квалифицираният електронен подпис не е издаден на юридическо лице. За подаване на уведомлението е необходимо да се автентикирате с КЕП издаден на лицензирания по ЧОД субект.
                return new OperationResult("DOC_COD_ENTITY_KEP_REQUIRED_E", "DOC_COD_ENTITY_KEP_REQUIRED_E");
            }
            else
            {
                var hasActiveLicencesAsync = await _cHODServicesClientFactory.GetCHODServicesClient().HasActiveLicencesAsync(1, _userAccessor.User.UIC, cancellationToken);

                if (!hasActiveLicencesAsync.IsSuccessfullyCompleted)
                {
                    var errors = new ErrorCollection();
                    errors.AddRange(hasActiveLicencesAsync.Errors.Select(e => (IError)new TextError(e.Code, e.Message)));

                    return new OperationResult(errors);
                }
                else
                {
                    if (!hasActiveLicencesAsync.Response)
                    {
                        //Юридическото лице, притежател на електронния подпис, няма издаден валиден лиценз за извършване на частна охранителна дейност по ЗЧОД.
                        return new OperationResult("DOC_COD_ENTITY_WITHOUT_ACTIVE_LICENCE_E", "DOC_COD_ENTITY_WITHOUT_ACTIVE_LICENCE_E");
                    }
                }
            }

            return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
        }
    }
}