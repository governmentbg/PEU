using CNSys;
using EAU.BDS.Documents.Domain;
using EAU.BDS.Documents.Domain.Models.Forms;
using EAU.BDS.Documents.Models;
using EAU.BDS.Documents.Models.Forms;
using EAU.BDS.Documents.XSLT;
using EAU.Documents;
using EAU.Documents.Common;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Nomenclatures;
using EAU.Security;
using EAU.Utilities;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.Models;
using WAIS.Integration.MOI.BDS.NRBLD;
using WAIS.Integration.MOI.BDS.NRBLD.Models;
using WAIS.Integration.MOI.Core.BDS.NRBLD;
using WAIS.Integration.MOI.KAT.AND;

namespace EAU.BDS.Documents
{
    internal class ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensService
        : ApplicationFormServiceBase<ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizens, ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensVM>
    {
        private readonly INRBLDServicesClientFactory _nRBLDServicesClientFactory;
        private readonly IANDServicesClientFactory _iANDServicesClientFactory;

        public ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensService(IServiceProvider serviceProvider, INRBLDServicesClientFactory iNRBLDServicesClientFactory, IANDServicesClientFactory iANDServicesClientFactory)
            : base(serviceProvider)
        {
            _nRBLDServicesClientFactory = iNRBLDServicesClientFactory;
            _iANDServicesClientFactory = iANDServicesClientFactory;
        }

        protected override string DocumentTypeUri => DocumentTypeUrisBDS.ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizens;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3002_ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizens.xslt",
                    Resolver = new BDSEmbeddedXmlResourceResolver()
                };
            }
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

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);
            var isServiceForIssuingDrivingLicense = service.AdditionalConfiguration != null && service.AdditionalConfiguration.ContainsKey("isServiceForIssuingDrivingLicense") &&
           service.AdditionalConfiguration["isServiceForIssuingDrivingLicense"].ToLower() == "true";
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken, !isServiceForIssuingDrivingLicense, true);

            if (!result.IsSuccessfullyCompleted)
                return result;

            var localizer = GetService<IStringLocalizer>();

            //Ако не е български гражданин връщаме грешка
            if (request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationBG == null)
                return new OperationResult("GL_00026_E", localizer["GL_00026_E"].Value);


            var nrbldResult = await GetNRBLDData(request.ServiceID, cancellationToken);

            if (!nrbldResult.IsSuccessfullyCompleted)
            {
                var errors = new ErrorCollection();
                errors.AddRange(nrbldResult.Errors.Select(e => (IError)(new TextError(e.Code, e.Message))));

                return new OperationResult(errors);
            }

            #region Model Initialization

            var form = (ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensVM)request.Form;
            var documentTypeIssued = service.AdditionalConfiguration.ContainsKey("documentTypeIssued")
                ? (IdentityDocumentType?)Convert.ToInt32(service.AdditionalConfiguration["documentTypeIssued"])
                : null;

            InitApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizens(form, nrbldResult.Response, documentTypeIssued, service.AdditionalConfiguration);

            #endregion

            if (isServiceForIssuingDrivingLicense)
            {
                CheckForNonHandedAndNonPaidSlip(request, form);
            }

            if (service.AdditionalConfiguration != null && service.AdditionalConfiguration.ContainsKey("showMessageForNonPaidSlip") &&
           service.AdditionalConfiguration["showMessageForNonPaidSlip"].ToLower() == "true")
            {
                if (request.AdditionalData == null)
                    request.AdditionalData = new AdditionalData();

                request.AdditionalData["showMessageForNonPaidSlip"] = "true";
            }

            return result;
        }

        protected async override Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var application = (ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensVM)request.FormData.Form;
            var service = GetService<IServices>().Search().First(s => s.SunauServiceUri == application.ElectronicAdministrativeServiceHeader.SUNAUServiceURI);
            var isServiceForIssuingDrivingLicense = service.AdditionalConfiguration != null && service.AdditionalConfiguration.ContainsKey("isServiceForIssuingDrivingLicense") &&
             service.AdditionalConfiguration["isServiceForIssuingDrivingLicense"].ToLower() == "true";

            //Ако заявлението е за лична карта или паспорт (ЕАУ-1 или ЕАУ-2) не правим проверка за български личен документ.
            var result = await base.ValidateApplicationFormInternalAsync(request, cancellationToken, !isServiceForIssuingDrivingLicense);
            var nrbldResult = await GetNRBLDData(service.ServiceID, cancellationToken);

            if (!nrbldResult.IsSuccessfullyCompleted)
            {
                ((ErrorCollection)result).AddRange(nrbldResult.Errors.Select(e => (IError)new TextError(e.Code, e.Message)));

                return result;
            }

            var form = (ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensVM)request.FormData.Form;

            if (form.Circumstances.HasDocumentForDisabilities.GetValueOrDefault())
            {
                if (request.FormData.AttachedDocuments == null
                    || request.FormData.AttachedDocuments.Count == 0
                    || !request.FormData.AttachedDocuments.Any(d => d.DocumenTypeID == DocumentTypesStatic.DocumentForDisabilitiesID))
                {
                    //При отбелязано декларативно обстоятелство за наличие на удостоверителен документ от ТЕЛК е необходимо към заявлението да приложите решение от експертна лекарска комисия (ТЕЛК)
                    result.Add(new TextError("GL_MISSING_DOC_FOR_DISABILITIES_E", "GL_MISSING_DOC_FOR_DISABILITIES_E"));
                }
            }

            if ((form.Declarations?.Declarations?.Any(d => d.Code == "DECL_LostStolenDocument" && d.IsDeclarationFilled)).GetValueOrDefault())
            {
                if (request.FormData.AttachedDocuments == null
                    || request.FormData.AttachedDocuments.Count == 0
                    || !request.FormData.AttachedDocuments.Any(d => d.DocumenTypeID == DocumentTypesStatic.DeclarationForLostStolenDocument))
                {
                    //При отбелязано декларативно обстоятелство за приложена декларация за загубен/откраднат/повреден/унищожен български личен документ е необходимо към заявлението да приложите Декларация по чл. 17, ал.1 от ПИБЛД.
                    result.Add(new TextError("GL_MISSING_DOC_FOR_PIBLD17_E", "GL_MISSING_DOC_FOR_PIBLD17_E"));
                }
            }

            if (isServiceForIssuingDrivingLicense)
            {
                if (form.ElectronicAdministrativeServiceHeader.ApplicationType == ApplicationType.AppForFirstReg
                    && (request.FormData.AttachedDocuments == null
                    || request.FormData.AttachedDocuments.Count == 0
                    || !request.FormData.AttachedDocuments.Any(d => d.DocumenTypeID == DocumentTypesStatic.DeclarationOfHabitualResidence)))
                {
                    //Необходимо е към заявлението да приложите документ "Декларация за обичайно пребиваване".
                    result.Add(new TextError("GL_MISSING_DOC_FOR_HABITUAL_RESIDENCE_E", "GL_MISSING_DOC_FOR_HABITUAL_RESIDENCE_E"));
                }

                //REQUIREMENT 2557
                //if (request.RecipientInfo != null)
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
            }
            else
            {
                //Ръста на човек, цвета на очите му и семейното му положение се валидират само за лична карта и паспорт.
                const int possibleHeightDeviation = 10;
                var isValidHeight = true;
                var isValidEyeColor = true;
                var isValidMaritalStatus = true;

                if (nrbldResult.Response.PersonData != null)
                {
                    //Ръста на човека не трябва да е променен с повече от 10 сантиметра 
                    if (form.Circumstances.Person.Height.HasValue)
                    {
                        if (nrbldResult.Response.PersonData.Height != null)
                            isValidHeight = (nrbldResult.Response.PersonData.Height + possibleHeightDeviation >= form.Circumstances.Person.Height)
                                && (nrbldResult.Response.PersonData.Height - possibleHeightDeviation <= form.Circumstances.Person.Height);
                    }
                    else
                        result.Add(new TextError("DOC_BDS_REQUIRED_HEIGHT_E", "DOC_BDS_REQUIRED_HEIGHT_E"));

                    if (nrbldResult.Response.PersonData.EyesColor != null && nrbldResult.Response.PersonData.EyesColor.code != null && nrbldResult.Response.PersonData.EyesColor.code.HasValue)
                        isValidEyeColor = form.Circumstances.Person.EyesColor == GetMOIBIDEyesColorByEyesColorCode(nrbldResult.Response.PersonData.EyesColor);

                    if (nrbldResult.Response.PersonData.MaritalStatus != null && nrbldResult.Response.PersonData.MaritalStatus.code != null && nrbldResult.Response.PersonData.MaritalStatus.code.HasValue)
                        isValidMaritalStatus = form.Circumstances.Person.MaritalStatus == GetMOIBIDMaritalStatusByMaritalStatusCode(nrbldResult.Response.PersonData.MaritalStatus);

                    //Въведените данни за ръст, цвят на очи и семейно положение не съответстват на поддържаните актуални данни за Вас в Националния автоматизиран информационен фонд 
                    if (!isValidHeight || !isValidEyeColor || !isValidMaritalStatus)
                        result.Add(new TextError("DOC_GL_MISMATCH_PERSONAL_DATA_E", "DOC_GL_MISMATCH_PERSONAL_DATA_E"));
                }
            }

            return result;
        }

        public override string SignatureXpath
        {
            get
            {
                return "aipdbc:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizens/aipdbc:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"aipdbc", "http://ereg.egov.bg/segment/R-3002" },
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }

        #region Helpers

        private Task<ServiceResult<ResponseNData>> GetNRBLDData(int? serviceId, CancellationToken cancellationToken)
        {
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == serviceId);
            var userAccessor = GetService<IEAUUserAccessor>();

            var requestData = new RequestDataNW1()
            {
                Citizen = new WAIS.Integration.MOI.Models.Citizen()
                {
                    PID = new WAIS.Integration.MOI.Models.CitizenPID()
                    {
                        code = userAccessor.User.PersonIdentifierType == PersonIdentifierTypes.EGN ? (short)PIDType.EGN : (short)PIDType.LNCh,
                        Value = userAccessor.User.PersonIdentifier
                    },
                    HasPicture = true
                },
                ServiceCode = service.AdditionalConfiguration["serviceCode"]
            };

            return _nRBLDServicesClientFactory.GetNRBLDServicesClient().CallVerifyConditionsMethodAsync(requestData, cancellationToken);
        }

        /// <summary>
        /// Иницилизира данните на "Заявление за издаване на лични документи на български граждани"
        /// </summary>
        private void InitApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizens(ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensVM application,
            ResponseNData responseNData,
            IdentityDocumentType? documentTypeIssued,
            AdditionalData serviceAdditionalData)
        {
            var pIdentData = responseNData?.PersonData?.PersonIdentification?.PersonIdentificationBG;

            if (application.Circumstances == null && pIdentData != null)
            {
                application.Circumstances = new ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataVM()
                {
                    Person = new PersonDataExtendedVM()
                    {
                        PlaceOfBirth = responseNData.PersonData?.BirthPlace,
                        EyesColor = GetMOIBIDEyesColorByEyesColorCode(responseNData.PersonData?.EyesColor),
                        MaritalStatus = GetMOIBIDMaritalStatusByMaritalStatusCode(responseNData.PersonData?.MaritalStatus),
                        Height = responseNData.PersonData?.Height,
                        PersonIdentification = new PersonIdentificationData()
                        {
                            Names = new PersonNames()
                            {
                                First = pIdentData.Names?.FirstName?.Cyrillic,
                                Middle = pIdentData.Names?.Surname?.Cyrillic,
                                Last = pIdentData.Names?.Family?.Cyrillic,
                            },
                            NamesLatin = new PersonNamesLatin()
                            {
                                First = pIdentData.Names?.FirstName?.Latin,
                                Middle = pIdentData.Names?.Surname?.Latin,
                                Last = pIdentData.Names?.Family?.Latin
                            },
                            Gender = new GenderData()
                            {
                                Genders = new GenderDataGender[]
                            {
                                    new GenderDataGender()
                                    {
                                        Code = pIdentData.Sex?.code != null ? pIdentData.Sex?.code.Value.ToString() : null,
                                        Name = pIdentData.Sex?.Value != null ? pIdentData.Sex?.Value.ToString() : null
                                    }
                            }
                            },
                            Identifier = new PersonIdentifier()
                            {
                                ItemElementName = PersonIdentifierChoiceType.EGN,
                                Item = pIdentData.PIN
                            },
                            BirthDate = pIdentData.BirthDate
                        }
                    },
                    PermanentAddress = InitPersonAddress(responseNData.Address),
                    IdentificationDocuments = documentTypeIssued.HasValue ? new List<IdentityDocumentType>() { documentTypeIssued.Value } : new List<IdentityDocumentType>(),
                    PoliceDepartment = new PoliceDepartment()
                    {
                        PoliceDepartmentCode = responseNData.Address?.PoliceDepartment?.Code,
                        PoliceDepartmentName = responseNData.Address?.PoliceDepartment?.Value
                    },
                    ServiceCode = serviceAdditionalData["serviceCode"]
                };
            }

            application.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.PredifinedUnitInAdministration = new ServiceApplicantReceiptDataUnitInAdministration()
            {
                AdministrativeDepartmentCode = responseNData.Address?.PoliceDepartment?.Code,
                AdministrativeDepartmentName = responseNData.Address?.PoliceDepartment?.Value
            };

            if (application.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.UnitInAdministration == null)
                application.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.UnitInAdministration = new ServiceApplicantReceiptDataUnitInAdministration();

            if (application.ServiceTermTypeAndApplicantReceipt.ServiceTermType == ServiceTermType.Fast && application.Circumstances.PoliceDepartment != null)
            {
                application.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.UsePredifinedUnitInAdministration = true;
                application.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.UseFilteredUnitInAdministration = false;
                application.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.UnitInAdministration.AdministrativeDepartmentCode = application.Circumstances.PoliceDepartment.PoliceDepartmentCode;
                application.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.UnitInAdministration.AdministrativeDepartmentName = application.Circumstances.PoliceDepartment.PoliceDepartmentName;
            }
            else if (application.ServiceTermTypeAndApplicantReceipt.ServiceTermType == ServiceTermType.Regular)
            {
                application.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.UsePredifinedUnitInAdministration = false;
                application.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.UseFilteredUnitInAdministration = true;

                if (application.ElectronicAdministrativeServiceHeader.ApplicationType == ApplicationType.AppForFirstReg)
                {
                    application.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.UnitInAdministration.AdministrativeDepartmentCode = null;
                    application.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.UnitInAdministration.AdministrativeDepartmentName = null;
                }
            }

            application.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.RestrictReceiptUnitToPermanentAddress = (serviceAdditionalData["restrictReceiptUnitToPermanentAddress"] == "1");
            
            application.IdentificationPhotoAndSignature = new IdentificationPhotoAndSignatureVM()
            {
                IdentificationPhoto = System.Convert.ToBase64String(responseNData?.PersonData?.Picture)
            };
        }

        private BIDEyesColor? GetMOIBIDEyesColorByEyesColorCode(PersonDataEyesColor color)
        {
            if (color == null)
                return null;

            switch (color.code.Value)
            {
                case EyesColorCode.Brown:
                    return BIDEyesColor.Brown;
                case EyesColorCode.Colorful:
                    return BIDEyesColor.Colorful;
                case EyesColorCode.Blue:
                    return BIDEyesColor.Blue;
                case EyesColorCode.Grey:
                    return BIDEyesColor.Gray;
                case EyesColorCode.Green:
                    return BIDEyesColor.Green;
                case EyesColorCode.Black:
                    return BIDEyesColor.Black;
                case EyesColorCode.None:
                    return null;
                case EyesColorCode.Red:
                    return BIDEyesColor.Red;
                case EyesColorCode.Heterochromia:
                    return BIDEyesColor.Heterochromia;
                default:
                    throw new ArgumentException("There is no MOIBIDEyesColor for this EyesColorCode!");
            }
        }

        private BIDMaritalStatus? GetMOIBIDMaritalStatusByMaritalStatusCode(PersonDataMaritalStatus status)
        {
            if (status == null)
                return null;

            switch (status.code.Value)
            {
                case MaritalStatusCode.Widow:
                    return BIDMaritalStatus.Widowed;
                case MaritalStatusCode.Single:
                    return BIDMaritalStatus.Single;
                case MaritalStatusCode.Married:
                    return BIDMaritalStatus.Maried;
                case MaritalStatusCode.Divorced:
                    return BIDMaritalStatus.Divorsed;
                case MaritalStatusCode.ActuallySeparated:
                    return BIDMaritalStatus.Separated;
                case MaritalStatusCode.Unknown:
                    return BIDMaritalStatus.Unspecified;
                default:
                    throw new ArgumentException("There is no MOIBIDMaritalStatus for this MaritalStatusCode!");
            }
        }

        private PersonAddress InitPersonAddress(Address addressBID)
        {
            PersonAddress address = null;

            if (addressBID != null)
            {
                address = new PersonAddress()
                {
                    #region Init Address

                    DistrictGRAOName = addressBID.District?.Value,
                    DistrictGRAOCode = addressBID.District?.Code?.PadLeft(2, '0'),
                    MunicipalityGRAOName = addressBID.Municipality?.Value,
                    MunicipalityGRAOCode = addressBID.Municipality?.Code?.PadLeft(2, '0'),
                    SettlementGRAOName = addressBID.Settlement.Value,
                    SettlementGRAOCode = addressBID.Settlement?.Code?.PadLeft(5, '0'),
                    StreetText = addressBID.Location?.Value,
                    StreetGRAOCode = addressBID.Location?.Code?.PadLeft(5, '0'),
                    Apartment = addressBID.Apartment,
                    BuildingNumber = addressBID.BuildingNumber,
                    Entrance = addressBID.Entrance,
                    Floor = addressBID.Floor.HasValue ? addressBID.Floor.ToString() : null

                    #endregion
                };
            }

            return address;
        }

        #endregion
    }
}