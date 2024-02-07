using CNSys;
using EAU.Documents;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.KAT.Documents.Domain;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.KAT.Documents.Models;
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
using WAIS.Integration.MOI.Core.BDS.NRBLD;
using WAIS.Integration.MOI.KAT.AND;

namespace EAU.KAT.Documents
{
    internal class ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesService :
       ApplicationFormServiceBase<ApplicationForIssuingOfControlCouponForDriverWithNoPenalties, ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesVM>
    {
        private readonly INRBLDServicesClientFactory _nRBLDServicesClientFactory;
        private readonly IANDServicesClientFactory _iANDServicesClientFactory;

        public ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesService(IServiceProvider serviceProvider
            , IANDServicesClientFactory iANDServicesClientFactory, INRBLDServicesClientFactory nRBLDServicesClientFactory) : base(serviceProvider)
        {
            _nRBLDServicesClientFactory = nRBLDServicesClientFactory;
            _iANDServicesClientFactory = iANDServicesClientFactory;
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.ApplicationForIssuingOfControlCouponForDriverWithNoPenalties;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3155_ApplicationForIssuingOfControlCouponForDriverWithNoPenalties.xslt",
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
                ElectronicServiceAuthorQualityType.Personal
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
                PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person
            };
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            var app = (ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesVM)request.Form;

            if (app != null)
            {
                var recipientIdentifier = app.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier;
                var nRBLDData = await _nRBLDServicesClientFactory.GetNRBLDServicesClient().GetPersonInfoAsync(recipientIdentifier.Item, false, cancellationToken);

                if (!nRBLDData.IsSuccessfullyCompleted)
                {
                    var errors = new ErrorCollection();
                    errors.AddRange(nRBLDData.Errors.Select(e => (IError)(new TextError(e.Code, e.Message))));

                    return new OperationResult(errors);
                }

                #region Check DrivingLicense

                if (nRBLDData.Response.Document == null
                    || !nRBLDData.Response.Document.Any(d => d.DocumentType.Type.Code == DocTypeCode.DriverLicenseForForeigner
                                                         || d.DocumentType.Type.Code == DocTypeCode.DriverLicense))
                {
                    //Не можете да продължите със заявяване на избраната от Вас услуга. По данни от Националния автоматизиран информационен фонд "Национален регистър на българските лични документи", лицето с ЕГН/ЛНЧ/ЛН {pid} няма издадено свидетелство за управление на МПС.                
                    var localizer = GetService<IStringLocalizer>();
                    var errorMsg = localizer["GL_00024_E"].Value.Replace("{pid}", recipientIdentifier.Item);

                    return new OperationResult(errorMsg, errorMsg);
                }

                #endregion

                if (app.Circumstances == null)
                {
                    app.Circumstances = new ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesDataVM();

                    #region Load PolicDepartment from applicant address

                    app.Circumstances.IssuingPoliceDepartment = new PoliceDepartment();

                    var permanentAddr = nRBLDData?.Response?.Address?.FirstOrDefault(el => el.id == AddresType.PermanentАddress);
                    if (permanentAddr != null)
                    {
                        app.Circumstances.IssuingPoliceDepartment.PoliceDepartmentCode = permanentAddr.PoliceDepartment.Code;
                        app.Circumstances.IssuingPoliceDepartment.PoliceDepartmentName = permanentAddr.PoliceDepartment.Value;
                    }
                    else
                    {
                        var currentAddr = nRBLDData?.Response?.Address?.FirstOrDefault(el => el.id == AddresType.CurrentАddress);

                        app.Circumstances.IssuingPoliceDepartment.PoliceDepartmentCode = currentAddr?.PoliceDepartment?.Code;
                        app.Circumstances.IssuingPoliceDepartment.PoliceDepartmentName = currentAddr?.PoliceDepartment?.Value;
                    }

                    #endregion

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
            }

            return result;
        }

        protected override async Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.ValidateApplicationFormInternalAsync(request, cancellationToken);
            var application = (ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesVM)request.FormData.Form;

            if (application != null && request.RecipientInfo != null)
            {
                var driverLicense = request.RecipientInfo.Document?.FirstOrDefault(d => d.DocumentType.Type.Code == DocTypeCode.DriverLicense || d.DocumentType.Type.Code == DocTypeCode.DriverLicenseForForeigner);
                var ident = application.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier;

                if (driverLicense != null)
                {
                    //REQUIREMENT 2557
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
                    //Не можете да продължите със заявяване на избраната от Вас услуга. По данни от Националния автоматизиран информационен фонд "Национален регистър на българските лични документи", лицето с ЕГН/ЛНЧ/ЛН {pid} няма издадено свидетелство за управление на МПС.
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
                return "afioccfdwnp:ApplicationForIssuingOfControlCouponForDriverWithNoPenalties/afioccfdwnp:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"afioccfdwnp", "http://ereg.egov.bg/segment/R-3155"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}
