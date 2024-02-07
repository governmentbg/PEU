using CNSys;
using EAU.Documents;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.KOS.Documents.Domain;
using EAU.KOS.Documents.Domain.Models;
using EAU.KOS.Documents.Domain.Models.Forms;
using EAU.KOS.Documents.Models;
using EAU.KOS.Documents.Models.Forms;
using EAU.KOS.Documents.XSLT;
using EAU.Nomenclatures;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.BDS.NRBLD.Models;

namespace EAU.KOS.Documents
{
    internal class NotificationForNonFirearmService
        : ApplicationFormServiceBase<NotificationForNonFirearm, NotificationForNonFirearmVM>
    {
        public NotificationForNonFirearmService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKOS.NotificationForNonFirearm;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3050_NotificationForNonFirearm.xslt",
                    Resolver = new KOSEmbeddedXmlResourceResolver()
                };
            }
        }

        /// <summary>
        /// Данни за заявител
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override List<ElectronicServiceAuthorQualityType> GetPossibleAuthorQualities(ApplicationFormInitializationRequest request)
        {
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

            return request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationBG != null
                ? service.AdditionalConfiguration["possibleAuthorQualitiesBG"].Split(',').Select(t => (ElectronicServiceAuthorQualityType)Convert.ToInt32(t)).ToList()
                : service.AdditionalConfiguration["possibleAuthorQualitiesF"].Split(',').Select(t => (ElectronicServiceAuthorQualityType)Convert.ToInt32(t)).ToList();
        }

        /// <summary>
        /// Данни за получател
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType> GetPossibleRecipientTypes(ApplicationFormInitializationRequest request)
        {
            var services = GetService<IServices>();
            var service = services.Search().Single(s => s.ServiceID == request.ServiceID);

            return service.AdditionalConfiguration["possibleRecipientTypes"].Split(',').Select(t => (PersonAndEntityBasicDataVM.PersonAndEntityChoiceType)Convert.ToInt32(t)).ToList();
        }

        protected override List<IdentityDocumentType> GetPossibleRecipientIdentityDocumentTypes(ApplicationFormInitializationRequest request)
        {
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

            if (service.AdditionalConfiguration.ContainsKey("identityDocumentType"))
                return service.AdditionalConfiguration["identityDocumentType"].Split(',').Select(t => (IdentityDocumentType)Convert.ToInt32(t)).ToList();

            return new List<IdentityDocumentType>();
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            var form = (NotificationForNonFirearmVM)request.Form;
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

            if (service.AdditionalConfiguration.ContainsKey("weaponPurposes"))
                request.AdditionalData["weaponPurposes"] = service.AdditionalConfiguration["weaponPurposes"];

            if (service.AdditionalConfiguration.ContainsKey("weaponTypes"))
                request.AdditionalData["weaponTypes"] = service.AdditionalConfiguration["weaponTypes"];

            if (form.Circumstances == null)
                form.Circumstances = new NotificationForNonFirearmDataVM();

            if (form.Circumstances.TechnicalSpecificationsOfWeapons == null || form.Circumstances.TechnicalSpecificationsOfWeapons.Count == 0)
            {
                var techSpecOfWeapon = new TechnicalSpecificationOfWeapon();
                form.Circumstances.TechnicalSpecificationsOfWeapons = new List<TechnicalSpecificationOfWeapon>() { techSpecOfWeapon };
            }

            //При подаване на услугата в лично качество адреса се изчита от НАИФ НРБЛД, за това си го персистваме в "PersistedPersonAddress"
            form.Circumstances.PersistedPersonAddress = this.getPersonAdress(request.ApplicantInfo, new AddresType[] { AddresType.PermanentАddress, AddresType.CurrentАddress });

            return result;
        }

        protected async override Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.ValidateApplicationFormInternalAsync(request, cancellationToken);
            var form = (NotificationForNonFirearmVM)request.FormData.Form;

            if (request.RecipientInfo?.PersonData?.Prohibition > 0 && form.ElectronicServiceApplicant.RecipientGroup.Recipient.SelectedChoiceType == PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person)
            {
                var localizer = GetService<IStringLocalizer>();
                var pid = form.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier.Item;
                var localizedError = localizer["GL_00020_E"].Value.Replace("{pid}", pid);

                result.Add(new TextError(localizedError, localizedError));
            }

            return result;
        }

        public override string SignatureXpath
        {
            get
            {
                return "nfnf:NotificationForNonFirearm/nfnf:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                   {"nfnf", "http://ereg.egov.bg/segment/R-3050" },
                   {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}