using CNSys;
using EAU.COD.Documents.Domain;
using EAU.COD.Documents.Domain.Models.Forms;
using EAU.COD.Documents.Models.Forms;
using EAU.COD.Documents.XSLT;
using EAU.Documents;
using EAU.Documents.Common;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Nomenclatures;
using EAU.Security;
using EAU.Services.Nomenclatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.RegiX;

namespace EAU.COD.Documents
{
    internal class RequestForIssuingLicenseForPrivateSecurityServicesService :
       ApplicationFormServiceBase<RequestForIssuingLicenseForPrivateSecurityServices, RequestForIssuingLicenseForPrivateSecurityServicesVM>
    {
        public RequestForIssuingLicenseForPrivateSecurityServicesService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisCOD.RequestForIssuingLicenseForPrivateSecurityServices;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3108_RequestForIssuingLicenseForPrivateSecurityServices.xslt",
                    Resolver = new CODEmbeddedXmlResourceResolver()
                };
            }
        }

        protected override List<ElectronicServiceAuthorQualityType> GetPossibleAuthorQualities(ApplicationFormInitializationRequest reques)
        {
            return new List<ElectronicServiceAuthorQualityType>() { ElectronicServiceAuthorQualityType.Representative, ElectronicServiceAuthorQualityType.LegalRepresentative };
        }

        protected override List<IdentityDocumentType> GetPossibleRecipientIdentityDocumentTypes(ApplicationFormInitializationRequest reques)
        {
            return new List<IdentityDocumentType>() { IdentityDocumentType.PersonalCard };
        }

        protected override List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType> GetPossibleRecipientTypes(ApplicationFormInitializationRequest reques)
        {
            return new List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType>() { PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Entity };
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            var form = (RequestForIssuingLicenseForPrivateSecurityServicesVM)request.Form;
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);
            var servingUnitsInfoService = GetService<IServingUnitsInfo>();

            if (form.Circumstances == null)
                form.Circumstances = new RequestForIssuingLicenseForPrivateSecurityServicesDataVM();

            form.Circumstances.IssuingPoliceDepartment = new PoliceDepartment();
            var issuingPoliceDepartment = service.AdditionalConfiguration["issuingPoliceDepartment"];

            if (!string.IsNullOrWhiteSpace(issuingPoliceDepartment))
            {
                await servingUnitsInfoService.EnsureLoadedAsync(request.ServiceID.Value, cancellationToken);
                var servingUnits = servingUnitsInfoService.Search(request.ServiceID.Value, out DateTime? lastModifiedDate);
                var unitInfo = servingUnits.FirstOrDefault(s => s.UnitID == int.Parse(issuingPoliceDepartment));

                if (unitInfo != null)
                {
                    form.Circumstances.IssuingPoliceDepartment.PoliceDepartmentCode = unitInfo.UnitID.ToString();
                    form.Circumstances.IssuingPoliceDepartment.PoliceDepartmentName = unitInfo.Name;
                }
            }

            if (form.Circumstances.EntityManagementAddress == null)
            {
                form.Circumstances.EntityManagementAddress = await GetEntityManagementAddressAsync();
            }

            if(form.Circumstances.CorrespondingAddress == null)
                form.Circumstances.CorrespondingAddress = new EntityAddress();

            return result;
        }

        protected async override Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var application = (RequestForIssuingLicenseForPrivateSecurityServicesVM)request.FormData.Form;
            var result = await base.ValidateApplicationFormInternalAsync(request, cancellationToken);

            if (application.ElectronicServiceApplicant?.RecipientGroup?.AuthorWithQuality?.SelectedAuthorQuality == ElectronicServiceAuthorQualityType.Representative
            && (request.FormData.AttachedDocuments == null || request.FormData.AttachedDocuments.Count == 0 || !request.FormData.AttachedDocuments.Any(d => d.DocumenTypeID == DocumentTypesStatic.NotarizedPowerOfAttorney)))
            {
                //При заявяване на услугата в качеството на пълномощник е необходимо да приложените документ от вид "Нотариално заверено изрично пълномощно".
                result.Add(new TextError("GL_MISSING_DOC_FOR_NOTARIZED_POWER_OF_ATTORNEY_E", "GL_MISSING_DOC_FOR_NOTARIZED_POWER_OF_ATTORNEY_E"));
            }

            return result;
        }

        private async Task<EntityAddress> GetEntityManagementAddressAsync()
        {
            var entityManagementAddress = new EntityAddress();

            var userAccessor = GetService<IEAUUserAccessor>();

            if (userAccessor.User != null && !string.IsNullOrWhiteSpace(userAccessor.User.UIC))
            {
                var entityDataServices = GetService<IEntityDataServices>();

                var regiXEntityData = await entityDataServices.GetEntityDataAsync(userAccessor.User.UIC, CancellationToken.None);

                if (regiXEntityData != null && regiXEntityData.IsSuccessfullyCompleted)
                {
                    var regixAddress = regiXEntityData.Response.Address;

                    entityManagementAddress.SettlementName = regixAddress.Settlement;
                    entityManagementAddress.SettlementCode = regixAddress.SettlementEKATTE;

                    entityManagementAddress.MunicipalityName = regixAddress.Municipality;
                    entityManagementAddress.MunicipalityCode = regixAddress.MunicipalityEkatte;

                    entityManagementAddress.DistrictName = regixAddress.District;
                    entityManagementAddress.DistrictCode = regixAddress.DistrictEkatte;

                    entityManagementAddress.AreaName = regixAddress.Area;
                    entityManagementAddress.AreaCode = regixAddress.AreaEkatte;

                    entityManagementAddress.PostCode = regixAddress.PostCode;
                    entityManagementAddress.Street = regixAddress.Street;
                    entityManagementAddress.StreetNumber = regixAddress.StreetNumber;
                    entityManagementAddress.Block = regixAddress.Block;
                    entityManagementAddress.HousingEstate = regixAddress.HousingEstate;
                    entityManagementAddress.Entrance = regixAddress.Entrance;
                    entityManagementAddress.Floor = regixAddress.Floor;
                    entityManagementAddress.Apartment = regixAddress.Apartment;
                }
            }

            return entityManagementAddress;
        }

        public override string SignatureXpath
        {
            get
            {
                return "aipsgap:RequestForIssuingLicenseForPrivateSecurityServices/aipsgap:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"aipsgap", "http://ereg.egov.bg/segment/R-3108"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}
