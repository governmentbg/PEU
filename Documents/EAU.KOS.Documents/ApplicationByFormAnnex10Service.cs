using AutoMapper;
using CNSys;
using EAU.Documents;
using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Models;
using EAU.Documents.Models.Forms;
using EAU.KOS.Documents.Domain;
using EAU.KOS.Documents.Domain.Models.Forms;
using EAU.KOS.Documents.Models;
using EAU.KOS.Documents.Models.Forms;
using EAU.KOS.Documents.XSLT;
using EAU.Nomenclatures;
using EAU.Services.Nomenclatures;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.BDS.NRBLD.Models;

namespace EAU.KOS.Documents
{
    internal class ApplicationByFormAnnex10Service : ApplicationFormServiceBase<ApplicationByFormAnnex10, ApplicationByFormAnnex10VM>
    {
        public ApplicationByFormAnnex10Service(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKOS.ApplicationByFormAnnex10;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3113_ApplicationByFormAnnex10.xslt",
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

            var form = (ApplicationByFormAnnex10VM)request.Form;
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);
            var servingUnitsInfoService = GetService<IServingUnitsInfo>();
            var issuingPoliceDepartment = service.AdditionalConfiguration.ContainsKey("issuingPoliceDepartment") ? service.AdditionalConfiguration["issuingPoliceDepartment"] : null;

            if (form.Circumstances == null)
                form.Circumstances = new ApplicationByFormAnnex10DataVM();

            //При подаване на услугата в лично качество адреса се изчита от НАИФ НРБЛД, за това си го персистваме в "PersistedPersonAddress"
            form.Circumstances.PersistedPersonAddress = this.getPersonAdress(request.ApplicantInfo, new AddresType[] { AddresType.PermanentАddress, AddresType.CurrentАddress });
            form.Circumstances.IssuingDocument = service.ResultDocumentName;
            form.Circumstances.ServicesWithOuterDocumentForThirdPerson = service.AdditionalConfiguration.ContainsKey("servicesWithOuterDocumentForThirdPerson") ? bool.Parse(service.AdditionalConfiguration["servicesWithOuterDocumentForThirdPerson"]) : false;
            form.Circumstances.IsSpecificDataForIssuingDocumentsForKOSRequired = service.AdditionalConfiguration.ContainsKey("IsSpecificDataForIssuingDocumentsForKOSRequired") ? bool.Parse(service.AdditionalConfiguration["IsSpecificDataForIssuingDocumentsForKOSRequired"]) : false;
            form.Circumstances.OnlyGDNPPoliceDepartment = !string.IsNullOrWhiteSpace(issuingPoliceDepartment);

            if (!string.IsNullOrWhiteSpace(issuingPoliceDepartment) && form.Circumstances.IssuingPoliceDepartment == null)
            {
                await servingUnitsInfoService.EnsureLoadedAsync(request.ServiceID.Value, cancellationToken);
                var servingUnits = servingUnitsInfoService.Search(request.ServiceID.Value, out DateTime? lastModifiedDate);
                var unitInfo = servingUnits.FirstOrDefault(s => s.UnitID == int.Parse(issuingPoliceDepartment));

                if (unitInfo != null )
                {
                    form.Circumstances.IssuingPoliceDepartment = new PoliceDepartment();
                    form.Circumstances.IssuingPoliceDepartment.PoliceDepartmentCode = unitInfo.UnitID.ToString();
                    form.Circumstances.IssuingPoliceDepartment.PoliceDepartmentName = unitInfo.Name;
                }
            }

            return result;
        }

        protected async override Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var errCollection = await base.ValidateApplicationFormInternalAsync(request, cancellationToken);
            var validator = new CnsysValidatorBase();
            var form = (ApplicationByFormAnnex10VM)request.FormData.Form;

            //Валидираме тук, понеже нямаме достъп до IsSpecificDataForIssuingDocumentsForKOSRequired и ServicesWithOuterDocumentForThirdPerson в домейн моделите.
            if (form.Circumstances.IsSpecificDataForIssuingDocumentsForKOSRequired && string.IsNullOrWhiteSpace(form.Circumstances.SpecificDataForIssuingDocumentsForKOS))
            {
                //Полето "Данни, необходими за издаване на документа" трябва да бъде попълнено.
                var localizer = GetService<IStringLocalizer>();
                var localizedError = localizer["DOC_KOS_specificDataForIssuingDocumentsForKOS_Required_L"];
                errCollection.Add(new TextError(localizedError, localizedError));
            }

            if (form.Circumstances.ServicesWithOuterDocumentForThirdPerson && form.Circumstances.IsRecipientEntity.HasValue && form.Circumstances.IsRecipientEntity.Value)
            {
                if (form.Circumstances.PersonGrantedFromIssuingDocument == null 
                    || form.Circumstances.PersonGrantedFromIssuingDocument.Names == null
                    || form.Circumstances.PersonGrantedFromIssuingDocument.Identifier == null
                    || string.IsNullOrWhiteSpace(form.Circumstances.PersonGrantedFromIssuingDocument.Names.First)
                    || string.IsNullOrWhiteSpace(form.Circumstances.PersonGrantedFromIssuingDocument.Names.Last)
                    || string.IsNullOrWhiteSpace(form.Circumstances.PersonGrantedFromIssuingDocument.Identifier.Item)
                    || (form.Circumstances.PersonGrantedFromIssuingDocument.Identifier.ItemElementName == PersonIdentifierChoiceType.EGN && !validator.ValidateEGN(form.Circumstances.PersonGrantedFromIssuingDocument.Identifier.Item))
                    || (form.Circumstances.PersonGrantedFromIssuingDocument.Identifier.ItemElementName == PersonIdentifierChoiceType.LNCh && !validator.ValidateLNCH(form.Circumstances.PersonGrantedFromIssuingDocument.Identifier.Item)))
                {
 
                    //Имате невъведени или некоректни данни в секция "Лице, за което се издава документа".
                    var localizer = GetService<IStringLocalizer>();
                    var localizedError = localizer["DOC_KOS_ServicesWithOuterDocumentForThirdPerson_Required_L"];
                    errCollection.Add(new TextError(localizedError, localizedError));
                }
            }

            if (request.RecipientInfo?.PersonData?.Prohibition > 0 && form.ElectronicServiceApplicant.RecipientGroup.Recipient.SelectedChoiceType == PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person)
            {
                var localizer = GetService<IStringLocalizer>();
                var pid = form.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier.Item;
                var localizedError = localizer["GL_00020_E"].Value.Replace("{pid}", pid);

                errCollection.Add(new TextError(localizedError, localizedError));
            }

            return errCollection;
        }

        public override Task<DocumentFormData> BuildWithdrawServiceFormAsync(object domainForm, CancellationToken cancellationToken)
        {
            var applicationForm = domainForm as ApplicationByFormAnnex10;

            if (applicationForm == null)
                throw new InvalidOperationException();

            ApplicationForWithdrawServiceVM ret = new ApplicationForWithdrawServiceVM();
            ret.Circumstances = new ApplicationForWithdrawServiceDataVM();
            ret.Circumstances.PoliceDepartment = applicationForm.IssuingPoliceDepartment;
            ret.Circumstances.IssuingDocument = applicationForm.ApplicationByFormAnnex10Data.IssuingDocument;
            ret.ElectronicAdministrativeServiceHeader = GetService<IMapper>().Map<ElectronicAdministrativeServiceHeaderVM>(applicationForm.ElectronicAdministrativeServiceHeader);
            ret.ElectronicServiceApplicant = GetService<IMapper>().Map<ElectronicServiceApplicantVM>(applicationForm.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant);

            var formData = new DocumentFormData
            {
                Form = ret
            };

            //Зачиства секциите(РИО обектите), които са празни.
            ObjectUtility.ClearEmptyFields(formData.Form);

            return Task.FromResult(formData);

        }

        public override string SignatureXpath
        {
            get
            {
                return "aipsgap:ApplicationByFormAnnex10/aipsgap:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"aipsgap", "http://ereg.egov.bg/segment/R-3113" },
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}