using AutoMapper;
using CNSys;
using EAU.BDS.Documents.Domain.Models;
using EAU.BDS.Documents.Domain.Models.Forms;
using EAU.BDS.Documents.Models.Forms;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Documents.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EAU.BDS.Documents.Models.Mapping
{
    public class EAUBDSDocumentsAutoMapperProfile : Profile
    {
        public EAUBDSDocumentsAutoMapperProfile()
        {
            CreateDomainToViewModelMap();
            CreateViewToDomainModelMap();
        }

        private void CreateDomainToViewModelMap()
        {
            CreateMap<IssuerCountry, IssuerCountryVM>();

            #region ApplicationForIssuingDocument

            CreateMap<ApplicationForIssuingDocument, ApplicationForIssuingDocumentVM>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);

                   d.Circumstances = new ApplicationForIssuingDocumentDataVM();
                   d.Circumstances = ctx.Mapper.Map<ApplicationForIssuingDocumentData, ApplicationForIssuingDocumentDataVM>(s.ApplicationForIssuingDocumentData);
                   d.PersonalInformation = new PersonalInformationVM();

                   if (s.ApplicationForIssuingDocumentData != null)
                       d.PersonalInformation = ctx.Mapper.Map<PersonalInformation, PersonalInformationVM>(s.ApplicationForIssuingDocumentData.PersonalInformation);
               });

            CreateMap<ApplicationForIssuingDocumentData, ApplicationForIssuingDocumentDataVM>()
                 .AfterMap((s, d, ctx) =>
                 {
                     d.DocumentToBeIssuedFor = new DocumentToBeIssuedForVM();
                     d.DocumentToBeIssuedFor = ctx.Mapper.Map<DocumentToBeIssuedFor, DocumentToBeIssuedForVM>(s.DocumentToBeIssuedFor);
                     //d.HasDocumentForDisabilities = s.HasDocumentForDisabilities;
                 });

            CreateMap<DocumentToBeIssuedFor, DocumentToBeIssuedForVM>()
               .AfterMap((s, d, ctx) =>
               {
                   d.DocumentMustServeTo = ctx.Mapper.Map<DocumentMustServeTo, DocumentMustServeToVM>(s.DocumentMustServeTo);
               });

            CreateMap<PersonalInformation, PersonalInformationVM>();
            CreateMap<IssuedBulgarianIdentityDocumentsInPeriod, IssuedBulgarianIdentityDocumentsInPeriodVM>();
            CreateMap<OtherIndormationConnectedWithIssuedBulgarianIdentityDocuments, OtherInformationConnectedWithIssuedBulgarianIdentityDocumentsVM>()
                .ForMember(d => d.DocumentNumbers, (config) => config.Ignore())
                .AfterMap((s, d) =>
            {
                if (s.DocumentNumbers?.Count > 0)
                {
                    d.DocumentNumbers = new List<DocumentNumber>();

                    for (int i = 0; i < s.DocumentNumbers.Count; i++)
                    {
                        d.DocumentNumbers.Add(new DocumentNumber { Number = s.DocumentNumbers[i] });
                    }
                }
            });

            CreateMap<IssuedBulgarianIdentityDocumentsInPeriod, IssuedBulgarianIdentityDocumentsInPeriodVM>()
                .AfterMap((s, d) =>
                {
                    d.IdentitityIssueDate = s.IdentitityIssueDate;
                    d.IdentitityExpireDate = s.IdentitityExpireDate;
                });

            CreateMap<DocumentToBeIssuedFor, DocumentToBeIssuedForVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Item != null)
                    {
                        if (s.Item is IssuedBulgarianIdentityDocumentsInPeriod)
                        {
                            d.ChooseIssuingDocument = IssueDocumentFor.IssuedBulgarianIdentityDocumentsInPeriod;
                            d.IssuedBulgarianIdentityDocumentsInPeriod = s.Item != null ? ctx.Mapper.Map<IssuedBulgarianIdentityDocumentsInPeriodVM>(s.Item) : null;
                        }
                        else
                        {
                            d.ChooseIssuingDocument = IssueDocumentFor.OtherInformationConnectedWithIssuedBulgarianIdentityDocuments;
                            d.OtherInformationConnectedWithIssuedBulgarianIdentityDocuments = s.Item != null ? ctx.Mapper.Map<OtherInformationConnectedWithIssuedBulgarianIdentityDocumentsVM>(s.Item) : null;
                        }
                    }
                    else
                    {
                        d.ChooseIssuingDocument = IssueDocumentFor.IssuedBulgarianIdentityDocumentsInPeriod;
                        d.IssuedBulgarianIdentityDocumentsInPeriod = new IssuedBulgarianIdentityDocumentsInPeriodVM();
                    }
                });

            CreateMap<DocumentMustServeTo, DocumentMustServeToVM>().AfterMap((s, d, ctx) =>
            {
                switch (s.ItemElementName)
                {
                    case ItemChoiceType1.AbroadDocumentMustServeTo:
                        d.ItemAbroadDocumentMustServeTo = s.Item;
                        break;
                    case ItemChoiceType1.InRepublicOfBulgariaDocumentMustServeTo:
                        d.ItemInRepublicOfBulgariaDocumentMustServeTo = s.Item;
                        break;
                    default:
                        break;
                }
            });

            #endregion

            #region ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizens

            CreateMap<ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizens, ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensVM>()
                   .ForMember(d => d.Declarations, (config) => config.Ignore())
                   .AfterMap((s, d, ctx) =>
                   {
                       MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                       d.Circumstances = ctx.Mapper.Map<ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData, ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataVM>(s.ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData);
                       d.IdentificationPhotoAndSignature = ctx.Mapper.Map<IdentificationPhotoAndSignature, IdentificationPhotoAndSignatureVM>(s.IdentificationPhotoAndSignature);
                       d.Circumstances.PoliceDepartment = s.IssuingPoliceDepartment;
                   });

            CreateMap<ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData, ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.Person = ctx.Mapper.Map<PersonData, PersonDataExtendedVM>(s.Person);
                    d.MotherData = ctx.Mapper.Map<ParentData, ParentDataVM>(s.MotherData);
                    d.FatherData = ctx.Mapper.Map<ParentData, ParentDataVM>(s.MotherData);
                    d.SpouseData = ctx.Mapper.Map<CitizenshipRegistrationBasicData, SpouseDataVM>(s.SpouseData);
                });

            CreateMap<PersonData, PersonDataExtendedVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Item.GetType() == typeof(string))
                        d.PlaceOfBirth = (string)s.Item;
                });

            CreateMap<ParentData, ParentDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Item.GetType() == typeof(CitizenshipRegistrationBasicData))
                    {
                        var pData = s.Item != null ? (ParentDataVM)ctx.Mapper.Map(s.Item, s.Item.GetType(), d.GetType()) : null;

                        d.BirthDate = pData.BirthDate;
                        d.Names = pData.Names;
                        d.UnknownParent = pData.UnknownParent;
                    }
                    else if (s.Item.GetType() == typeof(ForeignCitizenData))
                    {
                        var pData = s.Item != null ? (ParentDataVM)ctx.Mapper.Map(s.Item, s.Item.GetType(), d.GetType()) : null;

                        d.BirthDate = pData.BirthDate;
                        d.Names = pData.Names;
                        d.UnknownParent = pData.UnknownParent;
                    }
                    else
                    {
                        d = null;
                    }
                });

            CreateMap<CitizenshipRegistrationBasicData, SpouseDataVM>()
                .AfterMap((s, d) =>
                {
                    if (s.PersonBasicData.Names != null || !string.IsNullOrEmpty(s.PersonBasicData.Identifier.Item))
                    {
                        d.Names = new PersonNames();
                        d.Names = s.PersonBasicData.Names;
                        if (s.PersonBasicData.Identifier != null)
                            d.PID = string.IsNullOrEmpty(s.PersonBasicData.Identifier.Item) ? null : s.PersonBasicData.Identifier.Item;
                    }
                });

            #endregion

            #region ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgaria

            CreateMap<ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgaria, ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaVM>()
                   .ForMember(d => d.Declarations, (config) => config.Ignore())
                   .AfterMap((s, d, ctx) =>
                   {
                       MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                       d.Circumstances = ctx.Mapper.Map<ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData, ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaDataVM>(s.ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData);
                       d.IdentificationPhotoAndSignature = ctx.Mapper.Map<IdentificationPhotoAndSignature, IdentificationPhotoAndSignatureVM>(s.IdentificationPhotoAndSignature);
                       d.Circumstances.HasDocumentForDisabilities = s.HasDocumentForDisabilities;
                       d.Circumstances.IdentityDocumentsType = s.IdentificationDocuments;
                       d.Circumstances.PoliceDepartment = s.IssuingPoliceDepartment;
                   });

            CreateMap<ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData, ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.ForeignIdentityBasicData = ctx.Mapper.Map<ForeignIdentityBasicData, ForeignIdentityBasicDataVM>(s.ForeignIdentityBasicData);
                    d.TravelDocument = ctx.Mapper.Map<TravelDocument, TravelDocumentVM>(s.TravelDocument);
                    d.NewTravelDocument = ctx.Mapper.Map<TravelDocument, TravelDocumentVM>(s.TravelDocument);

                    if (ObjectUtility.IsEmpty(s.MotherData))
                        d.MotherData = ctx.Mapper.Map<ForeignIdentityBasicData, ForeignerParentDataVM>(s.MotherData);
                    else
                    {
                        d.MotherData = new ForeignerParentDataVM();
                        d.MotherData.UnknownParent = true;
                    }

                    if (s.FatherData != null)
                        d.FatherData = ctx.Mapper.Map<ForeignIdentityBasicData, ForeignerParentDataVM>(s.FatherData);
                    else
                    {
                        d.FatherData = new ForeignerParentDataVM();
                        d.FatherData.UnknownParent = true;
                    }

                    d.SpouseData = new ForeignerSpouseDataVM();
                    d.SpouseData = ctx.Mapper.Map<ForeignIdentityBasicData, ForeignerSpouseDataVM>(s.SpouseData);
                });


            CreateMap<ForeignIdentityBasicData, ForeignIdentityBasicDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.ForeignCitizenData = ctx.Mapper.Map<ForeignCitizenData, ForeignCitizenDataVM>(s.ForeignCitizenData);
                });

            CreateMap<ForeignCitizenData, ForeignCitizenDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.Citizenship = ctx.Mapper.Map<Citizenship, CitizenshipVM>(s.Citizenships.FirstOrDefault());

                    if (s.Item is PlaceOfBirthAbroad placeOfBirthAbroad)
                        d.PlaceOfBirthAbroad = placeOfBirthAbroad;

                    d.BirthDate = MapBirthDateFromNaif(s.BirthDate);
                });

            CreateMap<ForeignIdentityBasicData, ForeignerParentDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.ForeignCitizenData.ForeignCitizenNames != null || !string.IsNullOrEmpty(s.EGN) || !string.IsNullOrEmpty(s.LNCh))
                    {
                        d.Names = new ForeignCitizenNames();
                        d.Names = s.ForeignCitizenData.ForeignCitizenNames;
                        d.BirthDate = s.ForeignCitizenData.BirthDate;
                        d.Citizenship = new CitizenshipVM();
                        d.Citizenship = ctx.Mapper.Map<Citizenship, CitizenshipVM>(s.ForeignCitizenData.Citizenships.FirstOrDefault());
                        d.LNCh = s.LNCh;
                        d.EGN = s.EGN;
                    }
                });

            CreateMap<ForeignIdentityBasicData, ForeignerSpouseDataVM>()
             .AfterMap((s, d, ctx) =>
             {
                 if (s.ForeignCitizenData.ForeignCitizenNames != null || !string.IsNullOrEmpty(s.EGN) || !string.IsNullOrEmpty(s.LNCh))
                 {
                     d.Names = new ForeignCitizenNames();
                     d.Names = s.ForeignCitizenData.ForeignCitizenNames;
                     d.BirthDate = s.ForeignCitizenData.BirthDate;
                     d.Citizenship = new CitizenshipVM();
                     d.Citizenship = ctx.Mapper.Map<Citizenship, CitizenshipVM>(s.ForeignCitizenData.Citizenships.FirstOrDefault());
                     d.LNCh = s.LNCh;
                     d.EGN = s.EGN;
                 }
             });

            #endregion

            #region ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizens

            CreateMap<ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizens, ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensVM>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                    d.Circumstances = ctx.Mapper.Map<ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData, ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM>(s.ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData);
                    d.IdentificationPhotoAndSignature = ctx.Mapper.Map<IdentificationPhotoAndSignature, IdentificationPhotoAndSignatureVM>(s.IdentificationPhotoAndSignature);
                    d.Circumstances.HasDocumentForDisabilities = s.HasDocumentForDisabilities;
                    d.Circumstances.IdentityDocumentsType = s.IdentificationDocuments;
                    d.Circumstances.PoliceDepartment = s.IssuingPoliceDepartment;
                });

            CreateMap<ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData, ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.ForeignIdentityBasicData = ctx.Mapper.Map<ForeignIdentityBasicData, ForeignIdentityBasicDataVM>(s.ForeignIdentityBasicData);
                    d.TravelDocument = ctx.Mapper.Map<TravelDocument, TravelDocumentVM>(s.TravelDocument);
                    d.OtherCitizenship = ctx.Mapper.Map<Citizenship, CitizenshipVM>(s.OtherCitizenship);
                });

            #endregion

            #region CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLD

            CreateMap<CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLD, CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURI, AISCaseURIVM>(s.AISCaseURI);
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicData, ElectronicServiceProviderBasicDataVM>(s.ElectronicServiceProviderBasicData);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicant, ElectronicServiceApplicantVM>(s.ElectronicServiceApplicant);
                    d.DocumentMustServeTo = ctx.Mapper.Map<DocumentMustServeTo, DocumentMustServeToVM>(s.DocumentMustServeTo);

                    MapperHelper.MapDocumentWithOfficialDomainToViewModel<CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDOfficial>(s, d);
                });

            #endregion

            #region RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassport

            CreateMap<RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassport, RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassportVM>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                    d.Circumstances = new RequestForApplyingForServiceForIssuingOfBCICardOrPassportDataVM();

                    if (s.IdentificationDocuments != null)
                        d.Circumstances.IdentificationDocuments = s.IdentificationDocuments;
                });

            #endregion

            #region DeclarationUndurArticle17

            CreateMap<DeclarationUndurArticle17, DeclarationUndurArticle17VM>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);

                   d.Circumstances = s.DeclarationUndurArticle17Data;
               });

            #endregion

            #region InvitationToDrawUpAUAN

            CreateMap<InvitationToDrawUpAUAN, InvitationToDrawUpAUANVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURI, AISCaseURIVM>(s.AISCaseURI);
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicData, ElectronicServiceProviderBasicDataVM>(s.ElectronicServiceProviderBasicData);

                    MapperHelper.MapDocumentWithOfficialDomainToViewModel<InvitationToDrawUpAUANOfficial>(s, d);
                });

            #endregion
        }

        private void CreateViewToDomainModelMap()
        {
            CreateMap<IssuerCountryVM, IssuerCountry>();

            CreateMap<PersonDataExtendedVM, PersonData>()
                .AfterMap((s, d, ctx) =>
                {
                    d.Item = s.PlaceOfBirth;
                });

            CreateMap<ParentDataVM, ParentData>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.BirthDate != null || s.Names != null)
                    {
                        d.Item = new CitizenshipRegistrationBasicData()
                        {
                            BirthDate = s.BirthDate,
                            PersonBasicData = s.Names == null ? null : new PersonBasicData()
                            {
                                Names = s.Names
                            }
                        };
                    }
                });

            CreateMap<SpouseDataVM, CitizenshipRegistrationBasicData>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Names != null || !string.IsNullOrEmpty(s.PID))
                    {
                        d.PersonBasicData = new PersonBasicData()
                        {
                            Names = s.Names,
                            Identifier = string.IsNullOrEmpty(s.PID) ? null : new PersonIdentifier()
                            {
                                Item = s.PID,
                                ItemElementName = PersonIdentifierChoiceType.EGN
                            }
                        };
                    }
                });

            CreateMap<ForeignIdentityBasicDataVM, ForeignIdentityBasicData>()
                .AfterMap((s, d, ctx) =>
                {
                    d.ForeignCitizenData = ctx.Mapper.Map<ForeignCitizenDataVM, ForeignCitizenData>(s.ForeignCitizenData);
                });

            CreateMap<ForeignCitizenDataVM, ForeignCitizenData>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Citizenship != null)
                    {
                        if (!string.IsNullOrEmpty(s.Citizenship.CountryGRAOCode) || !string.IsNullOrEmpty(s.Citizenship.CountryName))
                        {
                            d.Citizenships = new List<Citizenship>()
                            {
                                new Citizenship()
                                {
                                    CountryGRAOCode = s.Citizenship.CountryGRAOCode,
                                    CountryName = s.Citizenship.CountryName
                                }
                           };
                        }

                        d.Item = s.PlaceOfBirthAbroad;
                        d.BirthDate = MapBirthDateToNaif(s.BirthDate);
                    }
                });

            CreateMap<IssuerCountryVM, IssuerCountry>()
                .AfterMap((s, d) =>
                {
                    d.CountryGRAOCode = s.CountryGRAOCode;
                    d.CountryName = s.CountryName;
                });

            CreateMap<ForeignerParentDataVM, ForeignIdentityBasicData>()
                .AfterMap((s, d, ctx) =>
                {
                    d.ForeignCitizenData = new ForeignCitizenData()
                    {
                        BirthDate = s.BirthDate,
                        Citizenships = new List<Citizenship>(),
                        ForeignCitizenNames = s.Names
                    };

                    if (s.Citizenship != null)
                        d.ForeignCitizenData.Citizenships.Add(new Citizenship() { CountryGRAOCode = s.Citizenship.CountryGRAOCode, CountryName = s.Citizenship.CountryName });

                    d.EGN = s.EGN;
                    d.LNCh = s.LNCh;
                });

            CreateMap<ForeignerSpouseDataVM, ForeignIdentityBasicData>()
                .AfterMap((s, d) =>
                {
                    d.ForeignCitizenData = new ForeignCitizenData()
                    {
                        BirthDate = s.BirthDate,
                        Citizenships = new List<Citizenship>(),
                        ForeignCitizenNames = s.Names
                    };

                    if (s.Citizenship != null)
                        d.ForeignCitizenData.Citizenships.Add(new Citizenship() { CountryGRAOCode = s.Citizenship.CountryGRAOCode, CountryName = s.Citizenship.CountryName });

                    d.EGN = s.EGN;
                    d.LNCh = s.LNCh;
                });

            #region ApplicationForIssuingDocument

            CreateMap<ApplicationForIssuingDocumentVM, ApplicationForIssuingDocument>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                 {
                     MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                     d.ApplicationForIssuingDocumentData = ctx.Mapper.Map<ApplicationForIssuingDocumentData>(s.Circumstances);
                     d.ApplicationForIssuingDocumentData.PersonalInformation = ctx.Mapper.Map<PersonalInformationVM, PersonalInformation>(s.PersonalInformation);

                     //Всички заявления подадение по електронен път се правят с обикновенна поръчка
                     d.ServiceTermType = ServiceTermType.Regular;
                 });

            CreateMap<PersonalInformationVM, PersonalInformation>();
            CreateMap<IssuedBulgarianIdentityDocumentsInPeriodVM, IssuedBulgarianIdentityDocumentsInPeriod>();
            CreateMap<OtherInformationConnectedWithIssuedBulgarianIdentityDocumentsVM, OtherIndormationConnectedWithIssuedBulgarianIdentityDocuments>()
                  .ForMember(d => d.DocumentNumbers, (config) => config.Ignore())
                  .AfterMap((s, d, ctx) =>
                    {
                        if (s.DocumentNumbers?.Count > 0)
                        {
                            d.DocumentNumbers = new List<string>();

                            for (int i = 0; i < s.DocumentNumbers.Count; i++)
                            {
                                d.DocumentNumbers.Add(s.DocumentNumbers[i].Number);
                            }
                        }
                    });

            CreateMap<ApplicationForIssuingDocumentDataVM, ApplicationForIssuingDocumentData>()
                .AfterMap((s, d, ctx) =>
                {
                    d.DocumentToBeIssuedFor = ctx.Mapper.Map<DocumentToBeIssuedForVM, DocumentToBeIssuedFor>(s.DocumentToBeIssuedFor);
                });

            CreateMap<DocumentToBeIssuedForVM, DocumentToBeIssuedFor>()
                .AfterMap((s, d, ctx) =>
                {
                    d.DocumentMustServeTo = ctx.Mapper.Map<DocumentMustServeToVM, DocumentMustServeTo>(s.DocumentMustServeTo);

                    if (s.ChooseIssuingDocument == IssueDocumentFor.IssuedBulgarianIdentityDocumentsInPeriod)
                        d.Item = ctx.Mapper.Map<IssuedBulgarianIdentityDocumentsInPeriodVM, IssuedBulgarianIdentityDocumentsInPeriod>(s.IssuedBulgarianIdentityDocumentsInPeriod);
                    else if (s.ChooseIssuingDocument == IssueDocumentFor.OtherInformationConnectedWithIssuedBulgarianIdentityDocuments)
                        d.Item = ctx.Mapper.Map<OtherInformationConnectedWithIssuedBulgarianIdentityDocumentsVM, OtherIndormationConnectedWithIssuedBulgarianIdentityDocuments>(s.OtherInformationConnectedWithIssuedBulgarianIdentityDocuments);
                });

            CreateMap<DocumentMustServeToVM, DocumentMustServeTo>().AfterMap((s, d, ctx) =>
            {
                d.Item = s.ItemElementName == ItemChoiceType1.InRepublicOfBulgariaDocumentMustServeTo ? s.ItemInRepublicOfBulgariaDocumentMustServeTo : s.ItemElementName == ItemChoiceType1.AbroadDocumentMustServeTo ? s.ItemAbroadDocumentMustServeTo : null;
            });

            #endregion

            #region ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizens

            CreateMap<ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensVM, ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizens>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                    d.ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData = ctx.Mapper.Map<ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData>(s.Circumstances);
                    d.IdentificationPhotoAndSignature = ctx.Mapper.Map<IdentificationPhotoAndSignatureVM, IdentificationPhotoAndSignature>(s.IdentificationPhotoAndSignature);

                    if (s.Circumstances.PoliceDepartment != null)
                        d.IssuingPoliceDepartment = s.Circumstances.PoliceDepartment;

                    if (s.Circumstances.ReceivePlace.HasValue)
                        d.ReceivePlace = s.Circumstances.ReceivePlace.Value;
                });

            CreateMap<ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataVM, ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData>()
                .AfterMap((s, d, ctx) =>
                {
                    d.Person = ctx.Mapper.Map<PersonDataExtendedVM, PersonData>(s.Person);
                    d.MotherData = ctx.Mapper.Map<ParentDataVM, ParentData>(s.MotherData);
                    d.FatherData = ctx.Mapper.Map<ParentDataVM, ParentData>(s.FatherData);
                    d.SpouseData = ctx.Mapper.Map<SpouseDataVM, CitizenshipRegistrationBasicData>(s.SpouseData);
                    d.AbroadAddress = string.IsNullOrWhiteSpace(s.AbroadAddress) ? null : s.AbroadAddress;
                });

            #endregion

            #region ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgaria

            CreateMap<ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaVM, ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgaria>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                    d.ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData = ctx.Mapper.Map<ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaDataVM, ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData>(s.Circumstances);
                    d.IdentificationPhotoAndSignature = ctx.Mapper.Map<IdentificationPhotoAndSignatureVM, IdentificationPhotoAndSignature>(s.IdentificationPhotoAndSignature);

                    if (s.Circumstances.PoliceDepartment != null)
                        d.IssuingPoliceDepartment = s.Circumstances.PoliceDepartment;

                    d.HasDocumentForDisabilities = s.Circumstances.HasDocumentForDisabilities;
                    d.IdentificationDocuments = s.Circumstances.IdentityDocumentsType;
                });

            CreateMap<ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaDataVM, ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.MotherData != null)
                        d.MotherData = ctx.Mapper.Map<ForeignerParentDataVM, ForeignIdentityBasicData>(s.MotherData);
                    else
                        d.MotherData = null;

                    if (s.FatherData != null)
                        d.FatherData = ctx.Mapper.Map<ForeignerParentDataVM, ForeignIdentityBasicData>(s.FatherData);
                    else
                        d.FatherData = null;

                    if (s.NewTravelDocument != null && !ObjectUtility.IsEmpty(s.NewTravelDocument))
                        d.TravelDocument = ctx.Mapper.Map<TravelDocument>(s.NewTravelDocument);

                    if (s.SpouseData != null)
                        d.SpouseData = ctx.Mapper.Map<ForeignerSpouseDataVM, ForeignIdentityBasicData>(s.SpouseData);
                    else
                        d.SpouseData = null;

                    d.ForeignIdentityBasicData = ctx.Mapper.Map<ForeignIdentityBasicDataVM, ForeignIdentityBasicData>(s.ForeignIdentityBasicData);

                    if (d.PreviousIdentityDocument != null && d.PreviousIdentityDocument.IdentityNumber == null)
                        d.PreviousIdentityDocument.IdentityDocumentType = null;

                    d.AbroadAddress = string.IsNullOrWhiteSpace(s.AbroadAddress) ? null : s.AbroadAddress;
                });

            #endregion

            #region ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizens

            CreateMap<ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensVM, ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizens>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                    d.ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData = ctx.Mapper.Map<ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM, ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData>(s.Circumstances);
                    d.IdentificationPhotoAndSignature = ctx.Mapper.Map<IdentificationPhotoAndSignatureVM, IdentificationPhotoAndSignature>(s.IdentificationPhotoAndSignature);

                    if (s.Circumstances.PoliceDepartment != null)
                        d.IssuingPoliceDepartment = s.Circumstances.PoliceDepartment;

                    d.HasDocumentForDisabilities = s.Circumstances.HasDocumentForDisabilities;
                    d.IdentificationDocuments = s.Circumstances.IdentityDocumentsType;
                });

            CreateMap<ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM, ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData>()
                .AfterMap((s, d, ctx) =>
                {
                    d.ForeignIdentityBasicData = ctx.Mapper.Map<ForeignIdentityBasicDataVM, ForeignIdentityBasicData>(s.ForeignIdentityBasicData);

                    //Ако данните за новия документ за задграничко пътуване са налице ги взимаме
                    if (s.NewTravelDocument != null && !ObjectUtility.IsEmpty(s.NewTravelDocument))
                        d.TravelDocument = ctx.Mapper.Map<TravelDocumentVM, TravelDocument>(s.NewTravelDocument);

                    d.OtherCitizenship = ctx.Mapper.Map<CitizenshipVM, Citizenship>(s.OtherCitizenship);
                });

            #endregion

            #region CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLD

            CreateMap<CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDVM, CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLD>()
                .AfterMap((s, d, ctx) =>
                {
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURIVM, AISCaseURI>(s.AISCaseURI);
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicDataVM, ElectronicServiceProviderBasicData>(s.ElectronicServiceProviderBasicData);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicantVM, ElectronicServiceApplicant>(s.ElectronicServiceApplicant);
                    d.DocumentMustServeTo = ctx.Mapper.Map<DocumentMustServeToVM, DocumentMustServeTo>(s.DocumentMustServeTo);

                    MapperHelper.MapDocumentWithOfficialViewModelToDomain<CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDOfficial>(s, d);
                });

            #endregion

            #region RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassport

            CreateMap<RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassportVM, RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassport>()
                 .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);

                    d.IdentificationDocuments = new List<IdentityDocumentType>();

                    foreach (var id in s.Circumstances.IdentificationDocuments)
                    {
                        d.IdentificationDocuments.Add(id);
                    }
                });

            #endregion

            #region DeclarationUndurArticle17

            CreateMap<DeclarationUndurArticle17VM, DeclarationUndurArticle17>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                    d.DeclarationUndurArticle17Data = s.Circumstances;
                });

            #endregion

            #region InvitationToDrawUpAUAN

            CreateMap<InvitationToDrawUpAUANVM, InvitationToDrawUpAUAN>()
                .AfterMap((s, d, ctx) =>
                {
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURIVM, AISCaseURI>(s.AISCaseURI);
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicDataVM, ElectronicServiceProviderBasicData>(s.ElectronicServiceProviderBasicData);

                    MapperHelper.MapDocumentWithOfficialViewModelToDomain<InvitationToDrawUpAUANOfficial>(s, d);
                });

            #endregion
        }

        #region Helpers

        private static string MapBirthDateFromNaif(string birthdate)
        {
            if (!string.IsNullOrWhiteSpace(birthdate) && birthdate.Length > 7 && birthdate.IndexOf('.') == -1)
                return birthdate.Substring(0, 2) + "." + birthdate.Substring(2, 2) + "." + birthdate.Substring(4);

            return birthdate;
        }

        private static string MapBirthDateToNaif(string birthDate)
        {
            if (!string.IsNullOrWhiteSpace(birthDate) && birthDate.IndexOf('.') > -1)
                return birthDate.Replace(".", string.Empty);

            return birthDate;
        }

        #endregion
    }
}