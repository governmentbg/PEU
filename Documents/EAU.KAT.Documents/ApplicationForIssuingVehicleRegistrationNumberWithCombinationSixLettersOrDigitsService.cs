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
using EAU.Services.Nomenclatures;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.BDS.NRBLD.Models;
using WAIS.Integration.MOI.Core.BDS.NRBLD;
using WAIS.Integration.MOI.Nomenclatures;
using WAIS.Integration.RegiX;

namespace EAU.KAT.Documents
{
    internal class ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsService :
       ApplicationFormServiceBase<ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigits, ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsVM>
    {
        private INomenclatureServices mNomenclatureServices;
        private IEntityDataServices mEntityService;

        public ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsService(
            IServiceProvider serviceProvider
            , INomenclatureServices nomenclatureServices
            , IEntityDataServices entityDataServices)
            : base(serviceProvider)
        {
            mNomenclatureServices = nomenclatureServices;
            mEntityService = entityDataServices;
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigits;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3333_ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigits.xslt",
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

            var app = (ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsVM)request.Form;

            if (app != null)
            {
                if (app.Circumstances == null)
                {
                    app.Circumstances = new ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsDataVM();

                    #region PoliceDepartment

                    var servingUnitsInfoService = GetService<IServingUnitsInfo>();
                    await servingUnitsInfoService.EnsureLoadedAsync(request.ServiceID.Value, cancellationToken);
                    var servingUnits = servingUnitsInfoService.Search(request.ServiceID.Value, out DateTime? lastModifiedDate);

                    Address permanentAdr = null;
                    Address currAdr = null;

                    if (request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationBG != null ||
                        (request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationF != null && request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationF.Statut.code == WAIS.Integration.MOI.BDS.NRBLD.Models.StatutCode.ForeignerPermanently))
                    {
                        permanentAdr = request.ApplicantInfo.Address.SingleOrDefault(a => a.id == WAIS.Integration.MOI.BDS.NRBLD.Models.AddresType.PermanentАddress);
                    }
                    else
                        currAdr = request.ApplicantInfo.Address.SingleOrDefault(a => a.id == WAIS.Integration.MOI.BDS.NRBLD.Models.AddresType.CurrentАddress);

                    if (permanentAdr != null)
                    {
                        var unitInfo = servingUnits.Where(u => u.ParentUnitID == int.Parse(permanentAdr.PoliceDepartment.Code)).Single();
                        app.Circumstances.AuthorPoliceDepartment = new PoliceDepartment()
                        {
                            PoliceDepartmentCode = unitInfo.UnitID.ToString(),
                            PoliceDepartmentName = unitInfo.Name
                        };
                    }
                    else
                    {
                        if (currAdr != null)
                        {
                            var unitInfo = servingUnits.Where(u => u.ParentUnitID == int.Parse(currAdr.PoliceDepartment.Code)).Single();

                            app.Circumstances.AuthorPoliceDepartment = new PoliceDepartment()
                            {
                                PoliceDepartmentCode = unitInfo.UnitID.ToString(),
                                PoliceDepartmentName = unitInfo.Name
                            };
                        }
                        else
                        {
                            if (request.ApplicantInfo.Address == null)
                            {
                                var localizer = GetService<IStringLocalizer>();
                                var errMsg = localizer["GL_00017_E"].Value.Replace("{pid}", app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.Author.ItemPersonBasicData.Identifier.Item);

                                return new OperationResult(errMsg, errMsg);
                            }
                        }
                    }

                    app.Circumstances.IssuingPoliceDepartment = new PoliceDepartment()
                    {
                        PoliceDepartmentCode = app.Circumstances.AuthorPoliceDepartment?.PoliceDepartmentCode,
                        PoliceDepartmentName = app.Circumstances.AuthorPoliceDepartment?.PoliceDepartmentName
                    };

                    #endregion

                    //автоматично се попълват с тези стоиности от номенклатурата на МВР, без право на редакция.
                    app.Circumstances.PlatesTypeCode = "9739";
                    app.Circumstances.PlatesTypeName = "НАЦИОНАЛНА";

                    app.Circumstances.PlatesContentType = PlatesContentTypes.FourDigits;
                }

                CheckForNonHandedAndNonPaidSlip(request, app);
            }

            return result;
        }

        protected override async Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var form = (ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsVM)request.FormData.Form;

            var res = await base.ValidateApplicationFormInternalAsync(
                request
                , cancellationToken
                , false
                , form.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality == ElectronicServiceAuthorQualityType.Personal);

            if (res.Count > 0)
                return res;

            #region Проверка на заявителя.

            var recipient = form.ElectronicServiceApplicant.RecipientGroup.Recipient;
            var service = GetService<IServices>().Search().Single(s => s.SunauServiceUri == form.ElectronicAdministrativeServiceHeader.SUNAUServiceURI);
            var appPoliceDepartment = form.Circumstances.IssuingPoliceDepartment.PoliceDepartmentCode;
            var localizer = GetService<IStringLocalizer>();

            if (recipient.SelectedChoiceType == PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person)
            {
                var nrlbldService = GetService<INRBLDServicesClientFactory>().GetNRBLDServicesClient();
                var recipientResult = await nrlbldService.GetPersonInfoAsync(recipient.ItemPersonBasicData.Identifier.Item, false, cancellationToken);

                if (!recipientResult.IsSuccessfullyCompleted)
                {
                    recipientResult.Errors.ForEach(el =>
                    {
                        res.Add(new TextError(el.Code, el.Message));
                    });

                    return res;
                }

                //if(recipientResult.Response.PersonData.DeathDate != null)
                //{
                //    //лицето е починало. - Тази грешка се локализира на сървара за това ключа и не трябва да отговаря на съществуващ, зада не се промени съобщението
                //    var localizedError = localizer["GL_00021_E"].Value.Replace("{pid}", recipient.ItemPersonBasicData.Identifier.Item);
                //    res.Add(new TextError(localizedError, localizedError));

                //    return res;
                //}

                #region Проверка за адрес

                var hasRequiredAddress = false;

                if (recipientResult.Response.Address != null)
                {
                    if (recipientResult.Response.PersonData.PersonIdentification.PersonIdentificationBG != null ||
                        (recipientResult.Response.PersonData.PersonIdentification.PersonIdentificationF != null && recipientResult.Response.PersonData.PersonIdentification.PersonIdentificationF.Statut.code == WAIS.Integration.MOI.BDS.NRBLD.Models.StatutCode.ForeignerPermanently))
                    {
                        hasRequiredAddress = recipientResult.Response.Address.Any(a => a.id == WAIS.Integration.MOI.BDS.NRBLD.Models.AddresType.PermanentАddress);
                    }
                    else
                        hasRequiredAddress = recipientResult.Response.Address.Any(a => a.id == WAIS.Integration.MOI.BDS.NRBLD.Models.AddresType.CurrentАddress);
                }

                if (!hasRequiredAddress)
                {
                    //няма данни за актуален постоянен/настоящ адрес за лицето.
                    var errMsg = localizer["GL_00017_E"].Value.Replace("{pid}", recipient.ItemPersonBasicData.Identifier.Item);
                    res.Add(new TextError(errMsg, errMsg));

                    return res;
                }

                #endregion                

                var permanentOrCurrAdr = recipientResult.Response.Address.FirstOrDefault(a => a.id == AddresType.PermanentАddress);

                if (permanentOrCurrAdr == null)
                {
                    permanentOrCurrAdr = recipientResult.Response.Address.Single(a => a.id == AddresType.CurrentАddress);
                }

                var recipientPoliceDepartmentCode = permanentOrCurrAdr.PoliceDepartment.Code;

                IServingUnitsInfo servingUnitsSrv = GetService<IServingUnitsInfo>();
                await servingUnitsSrv.EnsureLoadedAsync(service.ServiceID.Value, cancellationToken);
                var units = servingUnitsSrv.Search(service.ServiceID.Value, out DateTime? lastDate);

                var recipientPoliceDepartment = units.Single(u => u.ParentUnitID == Convert.ToInt32(recipientPoliceDepartmentCode));

                if (string.Compare(appPoliceDepartment, recipientPoliceDepartment.UnitID.ToString(), true) != 0)
                {
                    //Посочената в заявлението структура на МВР по месторегистрация на ППС не съответства на областта от адреса в Национален регистър на българските лични документи за лицето-получател на услугата.
                    res.Add(new TextError("GL_00033_E", "GL_00033_E"));
                    return res;
                }
            }

            if (recipient.SelectedChoiceType == PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Entity)
            {
                var recipientResult = await mEntityService.GetEntityDataAsync(recipient.ItemEntityBasicData.Identifier, cancellationToken);

                if (!recipientResult.IsSuccessfullyCompleted)
                {
                    recipientResult.Errors.ForEach(el =>
                    {
                        res.Add(new TextError(el.Code, el.Message));
                    });

                    return res;
                }

                if (recipientResult.Response == null || string.IsNullOrEmpty(recipientResult.Response.Identifier))
                {
                    //няма данни за фирмата.
                    var errMsg = localizer["GL_00030_E"].Value.Replace("{eik}", recipient.ItemEntityBasicData.Identifier);

                    res.Add(new TextError(errMsg, errMsg));

                    return res;
                }

                if (recipientResult.Response.Address == null
                    || (string.IsNullOrEmpty(recipientResult.Response.Address.Settlement)
                        && string.IsNullOrEmpty(recipientResult.Response.Address.SettlementEKATTE))
                        )
                {
                    //няма данни за адрес на фирмата.
                    var errMsg = localizer["GL_00034_E"].Value.Replace("{eik}", recipient.ItemEntityBasicData.Identifier);

                    res.Add(new TextError(errMsg, errMsg));

                    return res;
                }

                var entityPoliceDepartment = await mNomenclatureServices.GetPoliceDepartment(recipientResult.Response.Address.SettlementEKATTE, cancellationToken);

                IServingUnitsInfo servingUnitsSrv = GetService<IServingUnitsInfo>();
                await servingUnitsSrv.EnsureLoadedAsync(service.ServiceID.Value, cancellationToken);
                var units = servingUnitsSrv.Search(service.ServiceID.Value, out DateTime? lastDate);

                var recipientPoliceDepartment = units.Single(u => u.ParentUnitID == entityPoliceDepartment.Code);

                if (string.Compare(appPoliceDepartment, recipientPoliceDepartment.UnitID.ToString(), true) != 0)
                {
                    //Посочената в заявлението структура на МВР по месторегистрация на ППС не съответства на областта от адреса на управление в ТРРЮЛНЦ/Регистър БУЛСТАТ за лицето-получател на услугата.
                    res.Add(new TextError("GL_00032_E", "GL_00032_E"));
                    return res;
                }
            }

            #endregion

            if (form.ElectronicAdministrativeServiceHeader.ApplicationType == ApplicationType.AppForFirstReg)
            {
                if (form.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality == ElectronicServiceAuthorQualityType.Personal
                    && request.FormData.AttachedDocuments != null
                    && request.FormData.AttachedDocuments.Any(d => d.DocumenTypeID == DocumentTypesStatic.NotarizedPowerOfAttorney))
                {
                    //При заявяване на услугата в лично качество не се допуска прилагане на документ от вид "Нотариално заверено изрично пълномощно".
                    res.Add(new TextError("GL_NEEDLESS_DOC_FOR_NOTARIZED_POWER_OF_ATTORNEY_E", "GL_NEEDLESS_DOC_FOR_NOTARIZED_POWER_OF_ATTORNEY_E"));
                }

                if (form.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality == ElectronicServiceAuthorQualityType.Representative
                && (request.FormData.AttachedDocuments == null
                    || request.FormData.AttachedDocuments.Count == 0
                    || !request.FormData.AttachedDocuments.Any(d => d.DocumenTypeID == DocumentTypesStatic.NotarizedPowerOfAttorney)))
                {
                    //При заявяване на услугата в качеството на пълномощник е необходимо да приложите документ от вид "Нотариално заверено изрично пълномощно".
                    res.Add(new TextError("GL_MISSING_DOC_FOR_NOTARIZED_POWER_OF_ATTORNEY_E", "GL_MISSING_DOC_FOR_NOTARIZED_POWER_OF_ATTORNEY_E"));
                }

                if (request.FormData.AttachedDocuments == null
                    || request.FormData.AttachedDocuments.Count == 0
                    || !request.FormData.AttachedDocuments.Any(d => d.DocumenTypeID == DocumentTypesStatic.DocumentForAcquisition))
                {
                    //Необходимо да приложите документ от вид "Документ за придобиване на ППС".
                    res.Add(new TextError("GL_MISSING_DOC_FOR_ACQUISITION_E", "GL_MISSING_DOC_FOR_ACQUISITION_E"));
                }
            }

            return res;
        }

        public override string SignatureXpath
        {
            get
            {
                return "afttorov:ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigits/afttorov:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"afttorov", "http://ereg.egov.bg/segment/R-3333"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}
