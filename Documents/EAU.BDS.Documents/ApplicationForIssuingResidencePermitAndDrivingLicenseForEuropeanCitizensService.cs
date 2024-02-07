using CNSys;
using EAU.BDS.Documents.Domain;
using EAU.BDS.Documents.Domain.Models;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.Models;
using WAIS.Integration.MOI.BDS.NRBLD.Models;
using WAIS.Integration.MOI.Core.BDS.NRBLD;
using WAIS.Integration.MOI.KAT.AND;

namespace EAU.BDS.Documents
{
    internal class ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensService
        : ApplicationFormServiceBase<ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizens, ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensVM>
    {
        private readonly INRBLDServicesClientFactory _nRBLDServicesClientFactory;
        private readonly IANDServicesClientFactory _iANDServicesClientFactory;

        public ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensService(IServiceProvider serviceProvider, INRBLDServicesClientFactory iNRBLDServicesClientFactory, IANDServicesClientFactory iANDServicesClientFactory)
            : base(serviceProvider)
        {
            _nRBLDServicesClientFactory = iNRBLDServicesClientFactory;
            _iANDServicesClientFactory = iANDServicesClientFactory;
        }

        protected override string DocumentTypeUri => DocumentTypeUrisBDS.ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizens;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3030_ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizens.xslt",
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
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken, false, true);

            if (!result.IsSuccessfullyCompleted)
                return result;

            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);
            var nrbldResult = await GetNRBLDData(request.ServiceID, cancellationToken);

            if (!nrbldResult.IsSuccessfullyCompleted)
            {
                var errors = new ErrorCollection();
                errors.AddRange(nrbldResult.Errors.Select(e => (IError)(new TextError(e.Code, e.Message))));

                return new OperationResult(errors);
            }

            #region Model Initialization

            var form = (ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensVM)request.Form;
            var documentTypeIssued = service.AdditionalConfiguration.ContainsKey("documentTypeIssued")
            ? (IdentityDocumentType?)Convert.ToInt32(service.AdditionalConfiguration["documentTypeIssued"])
            : null;

            InitApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizens(form, nrbldResult.Response, documentTypeIssued, service.AdditionalConfiguration["serviceCode"]);

            #endregion

            CheckForNonHandedAndNonPaidSlip(request, form);

            return result;
        }

        protected async override Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.ValidateApplicationFormInternalAsync(request, cancellationToken);
            var application = (ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensVM)request.FormData.Form;
            var service = GetService<IServices>().Search().First(s => s.SunauServiceUri == application.ElectronicAdministrativeServiceHeader.SUNAUServiceURI);
            var nrbldResult = await GetNRBLDData(service.ServiceID, cancellationToken);

            if (!nrbldResult.IsSuccessfullyCompleted)
                ((ErrorCollection)result).AddRange(nrbldResult.Errors.Select(e => (IError)new TextError(e.Code, e.Message)));

            //Валидират се само заявления за първоначално вписване.
            if (application.ElectronicAdministrativeServiceHeader.ApplicationType == ApplicationType.AppForFirstReg)
            {
                if (application.Circumstances.HasDocumentForDisabilities.GetValueOrDefault())
                {
                    if (request.FormData.AttachedDocuments == null
                         || request.FormData.AttachedDocuments.Count == 0
                         || !request.FormData.AttachedDocuments.Any(d => d.DocumenTypeID == DocumentTypesStatic.DocumentForDisabilitiesID))
                    {
                        //При отбелязано декларативно обстоятелство за наличие на удостоверителен документ от ТЕЛК е необходимо към заявлението да приложите решение от експертна лекарска комисия (ТЕЛК)
                        result.Add(new TextError("GL_MISSING_DOC_FOR_DISABILITIES_E", "GL_MISSING_DOC_FOR_DISABILITIES_E"));
                    }
                }

                if (request.FormData.AttachedDocuments == null
                   || request.FormData.AttachedDocuments.Count == 0
                   || !request.FormData.AttachedDocuments.Any(d => d.DocumenTypeID == DocumentTypesStatic.DeclarationOfHabitualResidence))
                {
                    //Необходимо е към заявлението да приложите документ "Декларация за обичайно пребиваване".
                    result.Add(new TextError("GL_MISSING_DOC_FOR_HABITUAL_RESIDENCE_E", "GL_MISSING_DOC_FOR_HABITUAL_RESIDENCE_E"));
                }
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

            return result;
        }

        public override string SignatureXpath
        {
            get
            {
                return "aipdbc:ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizens/aipdbc:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"aipdbc", "http://ereg.egov.bg/segment/R-3030"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }

        #region Init Helpers

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

        private void InitApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizens(ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensVM application,
            ResponseNData responseNData,
            IdentityDocumentType? documentTypeIssued,
            string serviceCode)
        {
            var pIdentData = responseNData?.PersonData?.PersonIdentification?.PersonIdentificationF;

            if (application.Circumstances == null && pIdentData != null)
            {
                application.Circumstances = new ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM()
                {
                    ForeignIdentityBasicData = new ForeignIdentityBasicDataVM()
                    {
                        ForeignCitizenData = new ForeignCitizenDataVM()
                        {
                            ForeignCitizenNames = new ForeignCitizenNames()
                            {
                                FirstCyrillic = pIdentData.Names?.FirstName?.Cyrillic,
                                LastCyrillic = pIdentData.Names?.Family?.Cyrillic,
                                OtherCyrillic = pIdentData.Names?.Surname?.Cyrillic,
                                FirstLatin = pIdentData.Names?.FirstName?.Latin,
                                LastLatin = pIdentData.Names?.Family?.Latin,
                                OtherLatin = pIdentData.Names?.Surname?.Latin
                            },
                            BirthDate = MapBirthDateFromNaif(pIdentData.BirthDate),
                            GenderCode = pIdentData.Sex?.code != null ? pIdentData.Sex?.code.Value.ToString() : null,
                            GenderName = pIdentData.Sex?.Value != null ? pIdentData.Sex?.Value.ToString() : null,
                            Citizenship = new CitizenshipVM()
                            {
                                CountryName = pIdentData.Nationality
                            },
                            PlaceOfBirthAbroad = new PlaceOfBirthAbroad()
                            {
                                CountryName = responseNData.PersonData?.BirthPlace
                            }
                        },
                        EyesColor = responseNData.PersonData?.EyesColor == null ? null : GetMOIBIDEyesColorByEyesColorCode(responseNData.PersonData?.EyesColor),
                        MaritalStatus = responseNData.PersonData?.MaritalStatus == null ? null : GetMOIBIDMaritalStatusByMaritalStatusCode(responseNData.PersonData?.MaritalStatus),
                        Height = responseNData.PersonData?.Height,
                        EGN = pIdentData.PIN,
                        LNCh = pIdentData.LNC
                    },
                    TravelDocument = pIdentData.NationalDocument == null ? null: new TravelDocumentVM()
                    {
                        IdentityNumber = pIdentData.NationalDocument.Number,
                        IdentitityIssueDate = pIdentData.NationalDocument.IssueDate,
                        IdentitityExpireDate = pIdentData.NationalDocument.ValidDate,
                        IdentityIssuer = new IssuerCountryVM()
                        {
                            CountryName = pIdentData.NationalDocument.IssuingCountry
                        }
                    },
                    Address = new PersonAddress()
                    {
                        DistrictGRAOName = responseNData.Address?.District?.Value,
                        DistrictGRAOCode = responseNData.Address?.District != null ? responseNData.Address?.District?.Code?.PadLeft(2, '0') : null,
                        MunicipalityGRAOName = responseNData.Address?.Municipality?.Value,
                        MunicipalityGRAOCode = responseNData.Address?.Municipality != null ? responseNData.Address?.Municipality?.Code?.PadLeft(2, '0') : null,
                        SettlementGRAOName = responseNData.Address?.Settlement?.Value,
                        SettlementGRAOCode = responseNData.Address?.Settlement != null ? responseNData.Address?.Settlement?.Code?.PadLeft(5, '0') : null,
                        StreetText = responseNData.Address?.Location != null ? responseNData.Address?.Location?.Value : null,
                        StreetGRAOCode = responseNData.Address?.Location != null ? responseNData.Address?.Location?.Code?.PadLeft(5, '0') : null,
                        Apartment = responseNData.Address?.Apartment,
                        BuildingNumber = responseNData.Address?.BuildingNumber,
                        Entrance = responseNData.Address?.Entrance,
                        Floor = responseNData.Address?.Floor != null ? responseNData.Address?.Floor?.ToString() : null
                    },
                    IdentityDocumentsType = documentTypeIssued.HasValue ? new List<IdentityDocumentType>() { documentTypeIssued.Value } : new List<IdentityDocumentType>(),
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
                IdentificationPhoto = System.Convert.ToBase64String(responseNData.PersonData?.Picture)
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