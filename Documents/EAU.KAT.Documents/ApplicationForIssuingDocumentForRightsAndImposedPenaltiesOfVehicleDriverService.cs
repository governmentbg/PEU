using CNSys;
using EAU.Documents;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.KAT.Documents.Domain;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.KAT.Documents.Models.Forms.DocumentForms;
using EAU.KAT.Documents.XSLT;
using EAU.Nomenclatures;
using EAU.Security;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.BDS.NRBLD.Models;
using WAIS.Integration.MOI.KAT.AND;
using WAIS.Integration.MOI.KAT.AND.Models;

namespace EAU.KAT.Documents
{
    internal class ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverService :
        ApplicationFormServiceBase<ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriver, ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverVM>
    {
        private readonly IANDServicesClientFactory _iANDServicesClientFactory;

        public ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverService(IServiceProvider serviceProvider, IANDServicesClientFactory iANDServicesClientFactory) : base(serviceProvider)
        {
            _iANDServicesClientFactory = iANDServicesClientFactory;
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriver;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3117_ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriver.xslt",
                    Resolver = new KATEmbeddedXmlResourceResolver()
                };
            }
        }

        protected override List<ElectronicServiceAuthorQualityType> GetPossibleAuthorQualities(ApplicationFormInitializationRequest reques)
        {
            return new List<ElectronicServiceAuthorQualityType>() { ElectronicServiceAuthorQualityType.Personal, ElectronicServiceAuthorQualityType.Representative };
        }

        protected override List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType> GetPossibleRecipientTypes(ApplicationFormInitializationRequest requesD)
        {
            return new List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType>() { PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person };
        }

        protected override List<IdentityDocumentType> GetPossibleRecipientIdentityDocumentTypes(ApplicationFormInitializationRequest request)
        {
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

            if (service.AdditionalConfiguration.ContainsKey("identityDocumentType"))
                return service.AdditionalConfiguration["identityDocumentType"].Split(',').Select(t => (IdentityDocumentType)Convert.ToInt32(t)).ToList();

            return new List<IdentityDocumentType>() { IdentityDocumentType.PersonalCard };
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            var app = (ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverVM)request.Form;

            if (app != null)
            {
                if (app.Circumstances == null)
                {
                    app.Circumstances = new Models.ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverDataVM();
                }

                if (app.Circumstances.PermanentAddress == null)
                {
                    app.Circumstances.PermanentAddress = this.getPersonAdress(request.ApplicantInfo, new AddresType[] { AddresType.PermanentАddress });
                }

                if (app.Circumstances.CurrentAddress == null)
                {
                    app.Circumstances.CurrentAddress = this.getPersonAdress(request.ApplicantInfo, new AddresType[] { AddresType.CurrentАddress });
                }
            }

            CheckForNonHandedAndNonPaidSlip(request, app);

            return result;
        }

        protected async override Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.ValidateApplicationFormInternalAsync(request, cancellationToken);
            var application = (ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverVM)request.FormData.Form;

            if (application != null && request.RecipientInfo != null)
            {
                var ident = application.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier;
                var driverLicense = request.RecipientInfo.Document?.FirstOrDefault(d => d.DocumentType.Type.Code == DocTypeCode.DriverLicense || d.DocumentType.Type.Code == DocTypeCode.DriverLicenseForForeigner);
                
                if (driverLicense != null)
                {
                    //var andResponse = await _iANDServicesClientFactory.GetANDServicesClient().GetObligationDocumentsByLicenceNumAsync(new ObligationDocumentsByLicenceNumRequest()
                    //{
                    //    egn = ident.Item,
                    //    licenceNum = driverLicense.Number
                    //}, cancellationToken);

                    //if (andResponse.IsSuccessfullyCompleted)
                    //{
                    //    ObligationDocumentsByLicenceNumResponse ANDRecipientObligationDocuments = andResponse.Response;
                    //    if (ANDRecipientObligationDocuments.allObligations != null && ANDRecipientObligationDocuments.allObligations.Length > 0)
                    //    {
                    //        if (ANDRecipientObligationDocuments.allObligations.Any(o => o.isServed.HasValue && o.isServed.Value))
                    //        {
                    //            //Има връчени, но неплатени постановления
                    //            var localizer = GetService<IStringLocalizer>();
                    //            var localizedError = localizer["GL_00012_E"].Value.Replace("{pid}", ident.Item);
                    //            result.Add(new TextError(localizedError, localizedError));
                    //        }
                    //    }
                    //}
                }
                else
                {
                    var localizer = GetService<IStringLocalizer>();
                    var errorMsg = localizer["GL_00024_E"].Value.Replace("{pid}", ident.Item);
                    result.Add(new TextError(errorMsg, errorMsg));
                }
            }

            return result;
        }


        public override string SignatureXpath
        {
            get
            {
                return "aipsgap:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriver/aipsgap:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"aipsgap", "http://ereg.egov.bg/segment/R-3117"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}
