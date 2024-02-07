using CNSys;
using EAU.Documents;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.KOS.Documents.Domain;
using EAU.KOS.Documents.Domain.Models.Forms;
using EAU.KOS.Documents.Models.Forms;
using EAU.KOS.Documents.XSLT;
using EAU.Nomenclatures;
using EAU.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.KOS;

namespace EAU.KOS.Documents
{
    //TODO: RES
    internal class NotificationForControlCouponService : ApplicationFormServiceBase<NotificationForControlCoupon, NotificationForControlCouponVM>
    {
        private readonly IKOSServicesClientFactory _kOSServicesClientFactory;
        private readonly IEAUUserAccessor _EAUUserAccessor;

        public NotificationForControlCouponService(IServiceProvider serviceProvider, IKOSServicesClientFactory kOSServicesClientFactory, IEAUUserAccessor eAUUserAccessor) : base(serviceProvider)
        {
            _kOSServicesClientFactory = kOSServicesClientFactory;
            _EAUUserAccessor = eAUUserAccessor;
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            if(string.IsNullOrEmpty(_EAUUserAccessor.User.UIC))
            {
                return new OperationResult("DOC_KOS_ENTITY_KEP_REQUIRED_E", "DOC_KOS_ENTITY_KEP_REQUIRED_E");
            }

            var merchantRes = await _kOSServicesClientFactory.GetKOSServicesClient().MerchantAsync(_EAUUserAccessor.User.UIC, cancellationToken);

            if(!merchantRes.IsSuccessfullyCompleted)
            {
                var errCode = merchantRes.Errors[0].Code;

                switch(errCode)
                {
                    case "400":
                        //Грешка при валидация на заявката. Невалиден ЕИК.
                        return new OperationResult("DOC_KOS_InvalidEIK_E.", "DOC_KOS_InvalidEIK_E");
                    case "404":
                        //Няма информация за валиден търговец с това ЕИК.
                        return new OperationResult("DOC_KOS_NOINFO_4LicensedWeaponTrader_E", "DOC_KOS_NOINFO_4LicensedWeaponTrader_E");
                   default:
                        return new OperationResult("GL_SYSTEM_UNAVAILABLE_E", "GL_SYSTEM_UNAVAILABLE_E");

                }
            }

            var form = (NotificationForControlCouponVM)request.Form;

            if (form.Circumstances == null)
            {
                form.Circumstances = new Models.NotificationForControlCouponDataVM() 
                { 
                    LicenseInfo = new Domain.Models.LicenseInfo(),
                    ControlCouponData = new List<Models.ControlCouponDataItemVM>() 
                };
            }

            return result;
        }

        protected async override Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.ValidateApplicationFormInternalAsync(request, cancellationToken);

            var application = (NotificationForControlCouponVM)request.FormData.Form;
            var services = GetService<IServices>();
            var service = services.Search().Single(s => s.SunauServiceUri == application.ElectronicAdministrativeServiceHeader.SUNAUServiceURI);

            var merchantRes = await _kOSServicesClientFactory.GetKOSServicesClient().MerchantAsync(_EAUUserAccessor.User.UIC, cancellationToken);

            if (!merchantRes.IsSuccessfullyCompleted)
            {
                result.Add(new TextError(merchantRes.Errors[0].Code, merchantRes.Errors[0].Message));
                return result;
            }

            var licenseRes = await _kOSServicesClientFactory.GetKOSServicesClient().LicenseAsync(application.Circumstances.LicenseInfo.PermitNumber, Media.Electronic, true, merchantRes.Response.MerchantValidityToken, cancellationToken);
            
            if (!licenseRes.IsSuccessfullyCompleted)
            {
                result.Add(new TextError(licenseRes.Errors[0].Code, licenseRes.Errors[0].Message));
                return result;
            }

            if (licenseRes.Response.ValidityPeriodEnd.HasValue && licenseRes.Response.ValidityPeriodEnd.Value.DateTime < DateTime.Now)
            {
                //Срокът на валидност на разрешението с въведения номер е изтекъл.
                result.Add(new TextError("DOC_KOS_ValidWeaponAcquisitionPerimit_Expired_E", "DOC_KOS_ValidWeaponAcquisitionPerimit_Expired_E"));
            }

            if (service.AdditionalConfiguration.ContainsKey("controlCouponItemCategory"))
            {
                List<ControlCouponItemCategory> categoryConfig = System.Text.Json.EAUJsonSerializer.Deserialize<List<ControlCouponItemCategory>>(service.AdditionalConfiguration["controlCouponItemCategory"]);

                if(application.Circumstances.ControlCouponData
                    .Any(el => !categoryConfig.Single(c => c.Value == el.CategoryCode).LicenseTypes.Contains(application.Circumstances.LicenseInfo.PermitType)))
                {
                    result.Add(new TextError("DOC_KOS_Document_Incorrect_ControlCard_data_E", "DOC_KOS_Document_Incorrect_ControlCard_data_E"));
                }
            }
            else
                result.Add(new TextError("DOC_KOS_Document_Incorrect_Service_Categories_E", "DOC_KOS_Document_Incorrect_Service_Categories_E"));

            return result;
        }

        protected override List<ElectronicServiceAuthorQualityType> GetPossibleAuthorQualities(ApplicationFormInitializationRequest request)
        {
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

            return request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationBG != null
                ? service.AdditionalConfiguration["possibleAuthorQualitiesBG"].Split(',').Select(t => (ElectronicServiceAuthorQualityType)Convert.ToInt32(t)).ToList()
                : service.AdditionalConfiguration["possibleAuthorQualitiesF"].Split(',').Select(t => (ElectronicServiceAuthorQualityType)Convert.ToInt32(t)).ToList();
        }

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

        protected override string DocumentTypeUri => DocumentTypeUrisKOS.NotificationForControlCoupon;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3260_NotificationForControlCoupon.xslt",
                    Resolver = new KOSEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "nfcc:NotificationForControlCoupon/nfcc:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"nfcc", "http://ereg.egov.bg/segment/R-3260" },
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }

    internal class ControlCouponItemCategory
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "licenseTypes")]
        public string[] LicenseTypes { get; set; }
    }
}
