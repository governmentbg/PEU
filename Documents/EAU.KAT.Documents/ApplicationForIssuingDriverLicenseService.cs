using CNSys;
using EAU.Documents;
using EAU.Documents.Common;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.KAT.Documents.Domain;
using EAU.KAT.Documents.Domain.Models;
using EAU.KAT.Documents.Models.Forms;
using EAU.KAT.Documents.XSLT;
using EAU.Nomenclatures;
using EAU.Security;
using EAU.Utilities;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.Models;
using WAIS.Integration.MOI.BDS.NRBLD.Models;
using WAIS.Integration.MOI.Core.BDS.NRBLD;

namespace EAU.KAT.Documents
{
    public class ApplicationForIssuingDriverLicenseService
        : ApplicationFormServiceBase<ApplicationForIssuingDriverLicense, ApplicationForIssuingDriverLicenseVM>
    {
        private readonly INRBLDServicesClientFactory _nRBLDServicesClientFactory;

        public ApplicationForIssuingDriverLicenseService(IServiceProvider serviceProvider, INRBLDServicesClientFactory iNRBLDServicesClientFactory)
            : base(serviceProvider)
        {
            _nRBLDServicesClientFactory = iNRBLDServicesClientFactory;
        }

        public override string SignatureXpath
        {
            get
            {
                return "aipdbd:ApplicationForIssuingDriverLicense/aipdbd:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"aipdbd", "http://ereg.egov.bg/segment/R-3055" },
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.ApplicationForIssuingDriverLicense;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3055_ApplicationForIssuingDriverLicense.xslt",
                    Resolver = new KATEmbeddedXmlResourceResolver()
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

            var isServiceForIssuingDrivingLicense = service.AdditionalConfiguration != null 
                && service.AdditionalConfiguration.ContainsKey("isServiceForIssuingDrivingLicense") 
                && service.AdditionalConfiguration["isServiceForIssuingDrivingLicense"].ToLower() == "true";

            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken, !isServiceForIssuingDrivingLicense, true);

            if (!result.IsSuccessfullyCompleted)
                return result;

            var localizer = GetService<IStringLocalizer>();
            var nrbldResult = await GetNRBLDData(request.ServiceID, cancellationToken);

            if (!nrbldResult.IsSuccessfullyCompleted)
            {
                var errors = new ErrorCollection();
                errors.AddRange(nrbldResult.Errors.Select(e => (IError)(new TextError(e.Code, e.Message))));

                return new OperationResult(errors);
            }

            #region Model Initialization

            var form = (ApplicationForIssuingDriverLicenseVM)request.Form;
            var documentTypeIssued = service.AdditionalConfiguration.ContainsKey("documentTypeIssued")
                ? (IdentityDocumentType?)Convert.ToInt32(service.AdditionalConfiguration["documentTypeIssued"])
                : null;

            InitApplicationForIssuingDriverLicenseData(form, nrbldResult.Response, documentTypeIssued, service.AdditionalConfiguration["serviceCode"]);

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

            if (request.AdditionalData.ContainsKey("IsBulgarianCitizen"))
            {
                form.Circumstances.IsBulgarianCitizen = Convert.ToBoolean(request.AdditionalData["IsBulgarianCitizen"]);
            }

            if (request.AdditionalData.ContainsKey("PersonIdentificationForeignStatut"))
            {
                form.Circumstances.ForeignStatut = (PersonIdentificationForeignStatut)Enum.Parse(typeof(PersonIdentificationForeignStatut), request.AdditionalData["PersonIdentificationForeignStatut"]);
            }

            return result;
        }

        protected async override Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var form = (ApplicationForIssuingDriverLicenseVM)request.FormData.Form;
            var service = GetService<IServices>().Search().First(s => s.SunauServiceUri == form.ElectronicAdministrativeServiceHeader.SUNAUServiceURI);
            var result = await base.ValidateApplicationFormInternalAsync(request, cancellationToken, true);
            var nrbldResult = await GetNRBLDData(service.ServiceID, cancellationToken);

            if (!nrbldResult.IsSuccessfullyCompleted)
            {
                ((ErrorCollection)result).AddRange(nrbldResult.Errors.Select(e => (IError)new TextError(e.Code, e.Message)));

                return result;
            }

            if ((form.Declarations?.Declarations?.Any(d => d.Code == "DECL_LostStolenDrivingLicense" && d.IsDeclarationFilled)).GetValueOrDefault() 
                && (request.FormData.AttachedDocuments == null
                    || request.FormData.AttachedDocuments.Count == 0
                    || !request.FormData.AttachedDocuments.Any(d => d.DocumenTypeID == DocumentTypesStatic.DeclarationUnderArticle160)))
            {
                //При отбелязано декларативно обстоятелство за приложена декларация за загубено/откраднато/повредено/унищожено свидетелство за управление на МПС е необходимо към заявлението да приложите Декларация по чл. 16-0, ал. 1 от ЗДвП и по чл. 17, ал.1 от ПИБЛД.
                result.Add(new TextError("GL_MISSING_DOC_FOR_ZDVP160_PIBLD17_E", "GL_MISSING_DOC_FOR_ZDVP160_PIBLD17_E"));
            }

            if (!IsValidatePersonNames(nrbldResult, form.Circumstances))
            {
                //Въведените данни за фамилия и други имена не съответстват на поддържаните за Вас актуални данни за имена на кирилица/латиница в Националния автоматизиран информационен фонд "Национален регистър на българските лични документи". Моля, коригирайте данните.
                result.Add(new TextError("GL_PERSON_FAMILY_OTHER_NAMES_NOT_EQUAL_E", "GL_PERSON_FAMILY_OTHER_NAMES_NOT_EQUAL_E"));
            }

            if ((form.Declarations?.Declarations?.Any(d => d.Code == "DECL_LIVING_OUTSIDE_EU" && !d.IsDeclarationFilled)).GetValueOrDefault() 
                || form.Circumstances.IsBulgarianCitizen == false)
            {
                if (request.FormData.AttachedDocuments == null || !request.FormData.AttachedDocuments.Any(d => d.DocumenTypeID == DocumentTypesStatic.DeclarationOfHabitualResidence))
                {
                    //Необходимо е към заявлението да приложите документ "Декларация за обичайно пребиваване".
                    result.Add(new TextError("GL_MISSING_DOC_FOR_HABITUAL_RESIDENCE_E", "GL_MISSING_DOC_FOR_HABITUAL_RESIDENCE_E"));
                }
            }

            if (form.Circumstances.HasDocumentForDisabilities.GetValueOrDefault()
                && (request.FormData.AttachedDocuments == null
                || !request.FormData.AttachedDocuments.Any(d => d.DocumenTypeID == DocumentTypesStatic.DocumentForDisabilitiesID)))
            {
                //При отбелязано декларативно обстоятелство за наличие на удостоверителен документ от ТЕЛК е необходимо към заявлението да приложите решение от експертна лекарска комисия (ТЕЛК)
                result.Add(new TextError("GL_MISSING_DOC_FOR_DISABILITIES_E", "GL_MISSING_DOC_FOR_DISABILITIES_E"));
            }

            return result;
        }

        #region Helpers

        /// <summary>
        /// Иницилизира данните на "Заявление за издаване на свидетелство за управление на МПС"
        /// </summary>
        private void InitApplicationForIssuingDriverLicenseData(ApplicationForIssuingDriverLicenseVM application,
            ResponseNData responseNData,
            IdentityDocumentType? documentTypeIssued,
            string serviceCode)
        {
            var pIdentDataBG = responseNData?.PersonData?.PersonIdentification?.PersonIdentificationBG;
            var pIdentDataF = responseNData?.PersonData?.PersonIdentification?.PersonIdentificationF;

            if (application.Circumstances == null && (pIdentDataBG != null || pIdentDataF != null))
            {
                PersonIdentifier personIdentifier = SetPersonIdentifier(pIdentDataBG, pIdentDataF);

                application.Circumstances = new ApplicationForIssuingDriverLicenseDataVM()
                {
                    Person = new PersonDataExtendedVM()
                    {
                        PlaceOfBirth = responseNData.PersonData?.BirthPlace,
                        EyesColor = GetMOIBIDEyesColorByEyesColorCode(responseNData.PersonData?.EyesColor),
                        MaritalStatus = GetMOIBIDMaritalStatusByMaritalStatusCode(responseNData.PersonData?.MaritalStatus),
                        Height = responseNData.PersonData?.Height,
                        PersonIdentification = SetPersonIdentificationData(pIdentDataBG, pIdentDataF, personIdentifier)
                    },
                    TravelDocument = pIdentDataF?.NationalDocument != null ? new TravelDocumentVM()
                    {
                        IdentityNumber = pIdentDataF.NationalDocument.Number,
                        IdentitityIssueDate = pIdentDataF.NationalDocument.IssueDate,
                        IdentitityExpireDate = pIdentDataF.NationalDocument.ValidDate,
                        IdentityIssuer = new IssuerCountryVM()
                        {
                            CountryName = pIdentDataF.NationalDocument.IssuingCountry
                        }
                    } : null,
                    Citizenship = pIdentDataF?.Nationality != null ? new CitizenshipVM()
                    {
                        CountryName = pIdentDataF.Nationality
                    } : null,
                    PersonFamily = pIdentDataBG != null ? pIdentDataBG.Names.Family.Cyrillic : null,
                    OtherNames = pIdentDataBG != null ? $"{pIdentDataBG.Names.FirstName.Cyrillic} {pIdentDataBG.Names?.Surname?.Cyrillic}".TrimEnd() : null,
                    Address = InitPersonAddress(responseNData.Address),
                    IdentificationDocuments = documentTypeIssued.HasValue ? new List<IdentityDocumentType>() { documentTypeIssued.Value } : new List<IdentityDocumentType>(),
                    PoliceDepartment = new PoliceDepartment()
                    {
                        PoliceDepartmentCode = responseNData.Address?.PoliceDepartment?.Code,
                        PoliceDepartmentName = responseNData.Address?.PoliceDepartment?.Value
                    },
                    ServiceCode = serviceCode
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

            application.IdentificationPhotoAndSignature = new IdentificationPhotoAndSignatureVM()
            {
                IdentificationPhoto = System.Convert.ToBase64String(responseNData?.PersonData?.Picture)
            };
        }

        private PersonIdentificationData SetPersonIdentificationData(PersonIdentificationDataBG personIdentificationBG, PersonIdentificationDataF personIdentificationF, PersonIdentifier personIdentifier)
        {
            PersonIdentificationData personIdentificationData = null;

            if (personIdentificationBG != null)
            {
                personIdentificationData = new PersonIdentificationData()
                {
                    Names = new PersonNames()
                    {
                        First = personIdentificationBG.Names?.FirstName?.Cyrillic,
                        Middle = personIdentificationBG.Names?.Surname?.Cyrillic,
                        Last = personIdentificationBG.Names?.Family?.Cyrillic,
                    },
                    NamesLatin = new PersonNamesLatin()
                    {
                        First = personIdentificationBG.Names?.FirstName?.Latin,
                        Middle = personIdentificationBG.Names?.Surname?.Latin,
                        Last = personIdentificationBG.Names?.Family?.Latin
                    },
                    Gender = new GenderData()
                    {
                        Genders = new GenderDataGender[]
                            {
                                    new GenderDataGender()
                                    {
                                        Code = personIdentificationBG.Sex?.code != null ? personIdentificationBG.Sex?.code.Value.ToString() : null,
                                        Name = personIdentificationBG.Sex?.Value != null ? personIdentificationBG.Sex?.Value.ToString() : null
                                    }
                            }
                    },
                    Identifier = personIdentifier,
                    BirthDate = personIdentificationBG.BirthDate
                };
            }
            else if(personIdentificationF != null)
            {
                personIdentificationData = new PersonIdentificationData()
                {
                    Names = new PersonNames()
                    {
                        First = personIdentificationF.Names?.FirstName?.Cyrillic,
                        Middle = personIdentificationF.Names?.Surname?.Cyrillic,
                        Last = personIdentificationF.Names?.Family?.Cyrillic,
                    },
                    NamesLatin = new PersonNamesLatin()
                    {
                        First = personIdentificationF.Names?.FirstName?.Latin,
                        Middle = personIdentificationF.Names?.Surname?.Latin,
                        Last = personIdentificationF.Names?.Family?.Latin
                    },
                    Gender = new GenderData()
                    {
                        Genders = new GenderDataGender[]
                            {
                                    new GenderDataGender()
                                    {
                                        Code = personIdentificationF.Sex?.code != null ? personIdentificationF.Sex?.code.Value.ToString() : null,
                                        Name = personIdentificationF.Sex?.Value != null ? personIdentificationF.Sex?.Value.ToString() : null
                                    }
                            }
                    },
                    Identifier = personIdentifier,
                    BirthDate = DateTime.ParseExact(MapBirthDateFromNaif(personIdentificationF.BirthDate), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None)
                };
            }

            return personIdentificationData;
        }

        private PersonIdentifier SetPersonIdentifier(PersonIdentificationDataBG personIdentificationBG, PersonIdentificationDataF personIdentificationF)
        {
            PersonIdentifier personIdentifier = null;

            if (personIdentificationBG != null)
            {
                personIdentifier = new PersonIdentifier
                {
                    ItemElementName = PersonIdentifierChoiceType.EGN,
                    Item = personIdentificationBG.PIN
                };
            }
            else if (personIdentificationF != null)
            {
                if (personIdentificationF.Statut?.codeValue == StatutCode.EUCitizen)
                {
                    personIdentifier = new PersonIdentifier
                    {
                        ItemElementName = PersonIdentifierChoiceType.LNCh,
                        Item = personIdentificationF.LNC
                    };
                }
                else if (personIdentificationF.Statut?.codeValue == StatutCode.ForeignerPermanently)
                {
                    personIdentifier = new PersonIdentifier
                    {
                        ItemElementName = PersonIdentifierChoiceType.EGN,
                        Item = personIdentificationF.PIN
                    };
                }
                else if (personIdentificationF.Statut?.codeValue == StatutCode.ForeignerTemporarily)
                {
                    personIdentifier = new PersonIdentifier
                    {
                        ItemElementName = PersonIdentifierChoiceType.LNCh,
                        Item = personIdentificationF.LNC
                    };
                }

            }

            return personIdentifier;
        }

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

        private bool IsValidatePersonNames(ServiceResult<ResponseNData> nrbldResult, ApplicationForIssuingDriverLicenseDataVM circumstances)
        {
            var personBG = nrbldResult.Response.PersonData.PersonIdentification.PersonIdentificationBG;
            var personF = nrbldResult.Response.PersonData.PersonIdentification.PersonIdentificationF;

            if (personBG != null && !string.IsNullOrWhiteSpace(circumstances.PersonFamily))
            {
                if (!string.Equals(circumstances.PersonFamily, personBG.Names.Family.Cyrillic, StringComparison.OrdinalIgnoreCase)
                    && !circumstances.OtherNames.Contains($"{personBG.Names.FirstName.Cyrillic} {personBG.Names.Surname.Cyrillic}"))
                {
                    return false;
                }
            }
            else if(personF != null)
            {
                //Проверка дали поле "Фамилия" и "Други имена" са със символи на латиница или на кирилица
                if(!string.IsNullOrEmpty(circumstances.PersonFamily) && !string.IsNullOrEmpty(circumstances.OtherNames))
                {
                    var isNamesSameAlphabet = IsNamesSameCyrillicOrLatin(circumstances.PersonFamily, circumstances.OtherNames);

                    if (isNamesSameAlphabet == false) return false;
                }

                var nrlbldNames = new List<string>();

                GetAllForeignPersonNames(nrlbldNames, personF.Names.FirstName?.Cyrillic);
                GetAllForeignPersonNames(nrlbldNames, personF.Names.Surname?.Cyrillic);
                GetAllForeignPersonNames(nrlbldNames, personF.Names.Family?.Cyrillic);
                GetAllForeignPersonNames(nrlbldNames, personF.Names.FirstName?.Latin);
                GetAllForeignPersonNames(nrlbldNames, personF.Names.Surname?.Latin);
                GetAllForeignPersonNames(nrlbldNames, personF.Names.Family?.Latin);

                if (!string.IsNullOrWhiteSpace(circumstances?.PersonFamily))
                {
                    var personFamilyNames = circumstances.PersonFamily?.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

                    foreach (var name in personFamilyNames)
                    {
                        var isEqualNames = ComparisonNames(nrlbldNames, name);

                        if (!isEqualNames) return false;
                       
                    }
                }

                if (!string.IsNullOrWhiteSpace(circumstances.OtherNames))
                {
                    var otherNames = circumstances.OtherNames?.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

                    foreach (var name in otherNames)
                    {
                        var isEqualNames = ComparisonNames(nrlbldNames, name);

                        if (!isEqualNames) return false;
                    }
                }
            }

            return true;
        }

        private bool IsNamesSameCyrillicOrLatin(string family, string otherNames)
        {
            var patternCyrillic = "^(([А-Яа-я]+([' -]+[А-Яа-я]+)*)(?![a-zA-Z]))$";
            var patternLatin = "^(([A-Za-z]+([' -][A-Za-z]+)*)(?![а-яА-Я]))$";

            if((Regex.Match(family, patternCyrillic).Success && Regex.Match(otherNames, patternCyrillic).Success)
                || (Regex.Match(family, patternLatin).Success && Regex.Match(otherNames, patternLatin).Success))
            {
                return true;
            }

            return false;
        }

        private bool ComparisonNames(List<string> nrbldNames, string name)
        {
            foreach (var nrbldName in nrbldNames)
            {
                if(name.Equals(nrbldName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private void GetAllForeignPersonNames(List<string> allNames, string currentName)
        {
            var names = currentName?.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (names != null)
            {
                foreach (var name in names) 
                {
                    if (!allNames.Contains(name))
                    {
                        allNames.Add(name);
                    }
                }
            }
        }

        /// <summary>
        /// При формат ddMMyyyy връща стринг във формат dd.MM.yyyy
        /// </summary>
        /// <param name="birthdate"></param>
        /// <returns></returns>
        private string MapBirthDateFromNaif(string birthdate)
        {
            if (!string.IsNullOrWhiteSpace(birthdate) && birthdate.Length > 7 && birthdate.IndexOf('.') == -1)
                return birthdate.Substring(0, 2) + "." + birthdate.Substring(2, 2) + "." + birthdate.Substring(4);

            return birthdate;
        }

        #endregion
    }
}