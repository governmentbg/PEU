using CNSys;
using EAU.BDS.Documents.Domain;
using EAU.BDS.Documents.Domain.Models.Forms;
using EAU.BDS.Documents.Models.Forms;
using EAU.BDS.Documents.XSLT;
using EAU.Documents;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Nomenclatures;
using EAU.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.BDS.NRBLD.Models;
using WAIS.Integration.MOI.Core.BDS.NRBLD;

namespace EAU.BDS.Documents
{
    internal class DeclarationUndurArticle17Service : ApplicationFormServiceBase<DeclarationUndurArticle17, DeclarationUndurArticle17VM>
    {
        private readonly INRBLDServicesClientFactory _nRBLDServicesClientFactory;

        public DeclarationUndurArticle17Service(IServiceProvider serviceProvider, INRBLDServicesClientFactory iNRBLDServicesClientFactory) : base(serviceProvider)
        {
            _nRBLDServicesClientFactory = iNRBLDServicesClientFactory;
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            var form = (DeclarationUndurArticle17VM)request.Form;
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

            if (service.AdditionalConfiguration == null)
                throw new InvalidOperationException("Missing service configuration.");

            if (form.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.ServiceResultReceiptMethod == null
                && service.DeliveryChannels.Any(d => d.DeliveryChannelID == (short?)ServiceResultReceiptMethods.EmailOrWebApplication))
                form.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.ServiceResultReceiptMethod = ServiceResultReceiptMethods.EmailOrWebApplication;

            if (form.ServiceTermTypeAndApplicantReceipt.ServiceTermType == null)
            {
                form.ServiceTermTypeAndApplicantReceipt.ServiceTermType = ServiceTermType.Regular;
            }

            form.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.UsePredifinedUnitInAdministration = false;
            form.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.UseFilteredUnitInAdministration = true;

            if (form.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.UnitInAdministration == null)
                form.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.UnitInAdministration = new ServiceApplicantReceiptDataUnitInAdministration(); 

            #region Init Circumstances

            if (form.Circumstances == null)
                form.Circumstances = new Domain.Models.DeclarationUndurArticle17Data();

            var isServiceForIDCard = service.AdditionalConfiguration.ContainsKey("isServiceForIDCard") && service.AdditionalConfiguration["isServiceForIDCard"].ToLower() == "true";
            var isServiceForPassport = service.AdditionalConfiguration.ContainsKey("isServiceForPassport") && service.AdditionalConfiguration["isServiceForPassport"].ToLower() == "true";
            var isServiceForDrivingLicense = service.AdditionalConfiguration.ContainsKey("isServiceForDrivingLicense") && service.AdditionalConfiguration["isServiceForDrivingLicense"].ToLower() == "true";

            if (isServiceForIDCard)
            {
                //ЕАУ-54
                form.Circumstances.DocType = Domain.Models.BulgarianIdentityDocumentTypes.IDCard;
            }
            else if (isServiceForPassport)
            {
                //ЕАУ-55
                form.Circumstances.DocType = Domain.Models.BulgarianIdentityDocumentTypes.Passport;
            }
            else if (isServiceForDrivingLicense)
            {
                //ЕАУ-56 - КАТ
                form.Circumstances.DocType = Domain.Models.BulgarianIdentityDocumentTypes.DrivingLicense;
            }
            else
                throw new InvalidOperationException("Invalid service requested.");

            var userAccessor = GetService<IEAUUserAccessor>();
            var personInfoResponse = await _nRBLDServicesClientFactory.GetNRBLDServicesClient().GetPersonInfoAsync(userAccessor.User.PersonIdentifier, false, cancellationToken);

            if (!personInfoResponse.IsSuccessfullyCompleted)
            {
                var errors = new ErrorCollection();
                errors.AddRange(personInfoResponse.Errors.Select(e => (IError)(new TextError(e.Code, e.Message))));

                return new OperationResult(errors);
            }    

            if (form.Circumstances.PermanentAddress == null)
            {
                form.Circumstances.PermanentAddress = this.getPersonAdress(personInfoResponse.Response, new AddresType[] { AddresType.PermanentАddress });

                foreach (var adr in personInfoResponse.Response.Address)
                {
                    if (adr.id == AddresType.PermanentАddress)
                    {
                        form.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.PredifinedUnitInAdministration = new ServiceApplicantReceiptDataUnitInAdministration()
                        {
                            AdministrativeDepartmentCode = adr.PoliceDepartment?.Code,
                            AdministrativeDepartmentName = adr.PoliceDepartment?.Value
                        };
                        break;
                    }
                }
            }

            if (form.Circumstances.PresentAddress == null)
            {
                form.Circumstances.PresentAddress = this.getPersonAdress(personInfoResponse.Response, new AddresType[] { AddresType.CurrentАddress });

                if (form.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.PredifinedUnitInAdministration == null)
                {
                    foreach (var adr in personInfoResponse.Response.Address)
                    {
                        if (adr.id == AddresType.CurrentАddress)
                        {
                            form.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.PredifinedUnitInAdministration = new ServiceApplicantReceiptDataUnitInAdministration()
                            {
                                AdministrativeDepartmentCode = adr.PoliceDepartment?.Code,
                                AdministrativeDepartmentName = adr.PoliceDepartment?.Value
                            };
                            break;
                        }
                    }
                }
            }

            #endregion

            var w17Result = await _nRBLDServicesClientFactory.GetNRBLDServicesClient().GetW17CheckInfoAsync(userAccessor.User.PersonIdentifier, service.SunauServiceUri, cancellationToken);

            if (!w17Result.IsSuccessfullyCompleted)
            {
                var errors = new ErrorCollection();
                errors.AddRange(w17Result.Errors.Select(e => (IError)(new TextError(e.Code, e.Message))));

                return new OperationResult(errors);
            }

            if (!w17Result.Response.Approve.Value)
            {
                return new OperationResult(w17Result.Response.Approve.Reason, w17Result.Response.Approve.Reason);
            }

            return result;
        }

        protected async override Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var errors = await base.ValidateApplicationFormInternalAsync(request, cancellationToken);
            var srvSunauUri = ((DeclarationUndurArticle17VM)request.FormData.Form).ElectronicAdministrativeServiceHeader.SUNAUServiceURI;
            var service = GetService<IServices>().Search().Single(s => string.Compare(s.SunauServiceUri, srvSunauUri, true) == 0);

            if (service.AdditionalConfiguration == null)
                throw new InvalidOperationException("Missing service configuration.");

            var isServiceForIDCard = service.AdditionalConfiguration.ContainsKey("isServiceForIDCard") && service.AdditionalConfiguration["isServiceForIDCard"].ToLower() == "true";
            var isServiceForPassport = service.AdditionalConfiguration.ContainsKey("isServiceForPassport") && service.AdditionalConfiguration["isServiceForPassport"].ToLower() == "true";
            var isServiceForDrivingLicense = service.AdditionalConfiguration.ContainsKey("isServiceForDrivingLicense") && service.AdditionalConfiguration["isServiceForDrivingLicense"].ToLower() == "true";

            var docType = ((DeclarationUndurArticle17VM)request.FormData.Form).Circumstances.DocType;

            if ((isServiceForIDCard && docType != Domain.Models.BulgarianIdentityDocumentTypes.IDCard)
                || (isServiceForPassport && docType != Domain.Models.BulgarianIdentityDocumentTypes.Passport)
                || (isServiceForDrivingLicense && docType != Domain.Models.BulgarianIdentityDocumentTypes.DrivingLicense))
            {
                errors.Add(new TextError("DOC_BDS_Document_IncorrectType_E", "DOC_BDS_Document_IncorrectType_E"));
            }

            return errors;
        }

        protected override List<ElectronicServiceAuthorQualityType> GetPossibleAuthorQualities(ApplicationFormInitializationRequest request)
        {
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

            return request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationBG != null
                ? service.AdditionalConfiguration["possibleAuthorQualitiesBG"].Split(',').Select(t => (ElectronicServiceAuthorQualityType)Convert.ToInt32(t)).ToList()
                : service.AdditionalConfiguration["possibleAuthorQualitiesF"].Split(',').Select(t => (ElectronicServiceAuthorQualityType)Convert.ToInt32(t)).ToList();
        }

        protected override List<IdentityDocumentType> GetPossibleRecipientIdentityDocumentTypes(ApplicationFormInitializationRequest request)
        {
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

            if (service.AdditionalConfiguration.ContainsKey("identityDocumentType"))
                return service.AdditionalConfiguration["identityDocumentType"].Split(',').Select(t => (IdentityDocumentType)Convert.ToInt32(t)).ToList();

            return new List<IdentityDocumentType>();
        }

        protected override List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType> GetPossibleRecipientTypes(ApplicationFormInitializationRequest request)
        {
            var services = GetService<IServices>();
            var service = services.Search().Single(s => s.ServiceID == request.ServiceID);

            return service.AdditionalConfiguration["possibleRecipientTypes"].Split(',').Select(t => (PersonAndEntityBasicDataVM.PersonAndEntityChoiceType)Convert.ToInt32(t)).ToList();
        }

        protected override string DocumentTypeUri => DocumentTypeUrisBDS.DeclarationUndurArticle17;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3251_DeclarationUndurArticle17.xslt",
                    Resolver = new BDSEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "dua:DeclarationUndurArticle17/dua:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"dua", "http://ereg.egov.bg/segment/R-3251" },
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}
