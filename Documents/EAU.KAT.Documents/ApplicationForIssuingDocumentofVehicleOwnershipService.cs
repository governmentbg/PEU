using CNSys;
using EAU.Documents;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.KAT.Documents.Domain;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.KAT.Documents.Models.Forms;
using EAU.KAT.Documents.XSLT;
using EAU.Nomenclatures;
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
    internal class ApplicationForIssuingDocumentofVehicleOwnershipService :
       ApplicationFormServiceBase<ApplicationForIssuingDocumentofVehicleOwnership, ApplicationForIssuingDocumentofVehicleOwnershipVM>
    {
        private readonly IANDServicesClientFactory _iANDServicesClientFactory;
        public ApplicationForIssuingDocumentofVehicleOwnershipService(IServiceProvider serviceProvider, IANDServicesClientFactory iANDServicesClientFactory) : base(serviceProvider)
        {
            _iANDServicesClientFactory = iANDServicesClientFactory;
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.ApplicationForIssuingDocumentofVehicleOwnership;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3104_ApplicationForIssuingDocumentofVehicleOwnership.xslt",
                    Resolver = new KATEmbeddedXmlResourceResolver()
                };
            }
        }

        protected override List<ElectronicServiceAuthorQualityType> GetPossibleAuthorQualities(ApplicationFormInitializationRequest reques)
        {
            return new List<ElectronicServiceAuthorQualityType>()
            {
                ElectronicServiceAuthorQualityType.Personal,
                ElectronicServiceAuthorQualityType.Representative,
                ElectronicServiceAuthorQualityType.LegalRepresentative,
                ElectronicServiceAuthorQualityType.Official
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

            var app = (ApplicationForIssuingDocumentofVehicleOwnershipVM)request.Form;

            if (app != null)
            {
                if (app.Circumstances == null)
                {
                    app.Circumstances = new Models.ApplicationForIssuingDocumentofVehicleOwnershipDataVM();                  
                }

                if (app.Circumstances.PermanentAddress == null)
                {
                    app.Circumstances.PermanentAddress = this.getPersonAdress(request.ApplicantInfo, new AddresType[] { AddresType.PermanentАddress });
                }

                if (app.Circumstances.CurrentAddress == null)
                {
                    app.Circumstances.CurrentAddress = this.getPersonAdress(request.ApplicantInfo, new AddresType[] { AddresType.CurrentАddress });
                }

                CheckForNonHandedAndNonPaidSlip(request, app);
            }

            return result;
        }

        protected async override Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.ValidateApplicationFormInternalAsync(request, cancellationToken);
            //var application = (ApplicationForIssuingDocumentofVehicleOwnershipVM)request.FormData.Form;

            //if (application != null && request.RecipientInfo != null)
            //{
            //    var driverLicense = request.RecipientInfo.Document?.FirstOrDefault(d => d.DocumentType.Type.Code == DocTypeCode.DriverLicense || d.DocumentType.Type.Code == DocTypeCode.DriverLicenseForForeigner);
            //    var ident = application.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier;

            //    if (driverLicense != null)
            //    {
            //        var andResponse = await _iANDServicesClientFactory.GetANDServicesClient().GetObligationDocumentsByLicenceNumAsync(new ObligationDocumentsByLicenceNumRequest()
            //        {
            //            egn = ident.Item,
            //            licenceNum = driverLicense.Number
            //        }, cancellationToken);

            //        if (andResponse.IsSuccessfullyCompleted)
            //        {
            //            ObligationDocumentsByLicenceNumResponse ANDRecipientObligationDocuments = andResponse.Response;
            //            if (ANDRecipientObligationDocuments.allObligations != null && ANDRecipientObligationDocuments.allObligations.Length > 0)
            //            {
            //                if (ANDRecipientObligationDocuments.allObligations.Any(o => o.isServed.HasValue && o.isServed.Value))
            //                {
            //                    //Има връчени, но неплатени постановления
            //                    var localizer = GetService<IStringLocalizer>();
            //                    var localizedError = localizer["GL_00012_E"].Value.Replace("{pid}", ident.Item);
            //                    result.Add(new TextError(localizedError, localizedError));
            //                }
            //            }
            //        }
            //    }
            //}

            return result;
        }

        protected override Task<OperationResult> InitializeDocumentFormInternalAsync(DocumentFormInitializationRequest request, CancellationToken cancellationToken)
        {
            request.AdditionalData["labelForOfficial"] = "DOC_GL_ElectronicServiceAuthorQualityType_OfficialPerson_L";

            return base.InitializeDocumentFormInternalAsync(request, cancellationToken);
        }

        public override string SignatureXpath
        {
            get
            {
                return "aipsgap:ApplicationForIssuingDocumentofVehicleOwnership/aipsgap:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"aipsgap", "http://ereg.egov.bg/segment/R-3104"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}
