using AutoMapper;
using CNSys;
using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Models.Forms;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace EAU.Documents.Models.Mapping
{
    public class EAUDocumentsAutoMapperProfile : Profile
    {
        public EAUDocumentsAutoMapperProfile()
        {
            CreateDomainToViewModelMap();
            CreateViewToDomainModelMap();
        }

        private void CreateDomainToViewModelMap()
        {
            CreateMap<IdentificationPhotoAndSignature, IdentificationPhotoAndSignatureVM>()
                .ForMember(s => s.IdentificationPhoto, (config) => config.Ignore())
                .ForMember(s => s.IdentificationSignature, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    if (s.IdentificationPhoto != null && s.IdentificationPhoto.Length > 0)
                        d.IdentificationPhoto = Convert.ToBase64String(s.IdentificationPhoto);

                    if (s.IdentificationSignature != null && s.IdentificationSignature.Length > 0)
                        d.IdentificationSignature = Convert.ToBase64String(s.IdentificationSignature);
                });

            CreateMap<ReceiptAcknowledgedMessageRegisteredBy, ReceiptAcknowledgedMessageRegisteredByVM>()
               .ForMember(d => d.ItemAISURI, v => v.MapFrom(s => s.Item is string ? (string)s.Item : null))
               .ForMember(d => d.ItemOfficer, v => v.MapFrom(s => s.Item is ReceiptAcknowledgedMessageRegisteredByOfficer ? (ReceiptAcknowledgedMessageRegisteredByOfficer)s.Item : null));

            CreateMap<ElectronicServiceProviderBasicData, ElectronicServiceProviderBasicDataVM>();
            CreateMap<AISCaseURI, AISCaseURIVM>()
              .ForMember(d => d.DocumentUri, v => v.MapFrom(s => s.Item is DocumentURI ? (DocumentURI)s.Item : null))
              .ForMember(d => d.TextUri, v => v.MapFrom(s => s.Item is string ? (string)s.Item : null))
              .ForMember(d => d.Choise, v => v.MapFrom(s => s.Item is DocumentURI ? ChoiceType.DocumentUri : ChoiceType.TextUri));

            CreateMap<ElectronicAdministrativeServiceHeader, ElectronicAdministrativeServiceHeaderVM>();
            CreateMap<IdentityDocumentBasicData, IdentityDocumentBasicDataVM>()
                .ForMember(dst => dst.IdentitityIssueDate, opt => opt.MapFrom((s, d) => s.IdentitityIssueDate))
                .ForMember(dst => dst.IdentityDocumentType, opt => opt.MapFrom((s, d) => s.IdentityDocumentType));

            CreateMap<PersonBasicData, PersonBasicDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.IdentityDocument = ctx.Mapper.Map<IdentityDocumentBasicDataVM>(s.IdentityDocument);
                });

            CreateMap<ForeignEntityBasicData, ForeignEntityBasicDataVM>()
                .ForMember(d => d.SelectedChoiceType, v => v.MapFrom(s => s.Items != null && s.Items.Count() > 1 ? ForeignEntityBasicDataVM.ForeignEntityChoiceType.RegisterData : ForeignEntityBasicDataVM.ForeignEntityChoiceType.OtherData));

            CreateMap<DocumentMustServeTo, DocumentMustServeToVM>()
                .AfterMap((s, d) =>
                {
                    if (s.ItemElementName == ItemChoiceType1.AbroadDocumentMustServeTo)
                        d.ItemAbroadDocumentMustServeTo = s.Item;

                    if (s.ItemElementName == ItemChoiceType1.InRepublicOfBulgariaDocumentMustServeTo)
                        d.ItemInRepublicOfBulgariaDocumentMustServeTo = s.Item;

                    d.ItemElementName = s.ItemElementName;
                });

            CreateMap<ElectronicStatementAuthor, ElectronicStatementAuthorVM>();
            CreateMap<ElectronicStatementAuthor, AuthorWithQualityVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s != null)
                    {
                        d.Author = ctx.Mapper.Map<ElectronicStatementAuthorVM>(s);
                        d.SelectedAuthorQuality = s.AuthorQuality is ElectronicServiceAuthorQualityType ? (ElectronicServiceAuthorQualityType)s.AuthorQuality : ElectronicServiceAuthorQualityType.Personal;
                    }
                });

            CreateMap<ElectronicServiceRecipient, ElectronicServiceRecipientVM>()
                .ForMember(d => d.ItemPersonBasicData, v => v.MapFrom(s => s.Item is PersonBasicData ? s.ItemPersonBasicData : null))
                .ForMember(d => d.ItemForeignCitizenBasicData, v => v.MapFrom(s => s.Item is ForeignCitizenBasicData ? s.ItemForeignCitizenBasicData : null))
                .ForMember(d => d.ItemEntityBasicData, v => v.MapFrom(s => s.Item is EntityBasicData ? s.ItemEntityBasicData : null))
                .ForMember(d => d.ItemForeignEntityBasicData, v => v.MapFrom(s => s.Item is ForeignEntityBasicData ? s.ItemForeignEntityBasicData : null));

            CreateMap<RecipientGroup, RecipientGroupVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Authors != null && s.Authors.Count == 1)
                    {
                        d.AuthorWithQuality = ctx.Mapper.Map<AuthorWithQualityVM>(s.Authors[0]);
                    }

                    if (s.Recipients != null && s.Recipients.Count == 1)
                    {
                        d.Recipient = ctx.Mapper.Map<ElectronicServiceRecipientVM>(s.Recipients[0]);
                    }
                });

            CreateMap<ElectronicServiceApplicant, ElectronicServiceApplicantVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.RecipientGroups != null && s.RecipientGroups.Count == 1)
                    {
                        d.RecipientGroup = ctx.Mapper.Map<RecipientGroupVM>(s.RecipientGroups[0]);
                        if (d.RecipientGroup.AuthorWithQuality != null)
                            d.RecipientGroup.AuthorWithQuality.Author.EmailAddress = s.EmailAddress;
                    }
                    else
                    {
                        d.RecipientGroup = null;
                    }
                });

            CreateMap<ServiceApplicantReceiptData, ServiceApplicantReceiptDataVM>()
                .AfterMap((src, dst, ctx) =>
                {
                    dst.ServiceResultReceiptMethod = src.ServiceResultReceiptMethod;
                    switch (src.ServiceResultReceiptMethod)
                    {
                        case ServiceResultReceiptMethods.DeskInAdministration:
                            dst.MunicipalityAdministrationAdress = src.Item != null ? (ServiceApplicantReceiptDataMunicipalityAdministrationAdressVM)ctx.Mapper.Map(src.Item, src.Item.GetType(), dst.MunicipalityAdministrationAdress.GetType()) : null;
                            break;
                        case ServiceResultReceiptMethods.CourierToOtherAddress:
                            dst.ApplicantAdress = src.Item != null ? (ServiceApplicantReceiptDataAddress)src.Item : null;
                            break;
                        case ServiceResultReceiptMethods.CourierToMailBox:
                            dst.PostOfficeBox = src.Item != null ? (string)src.Item : null;
                            break;
                        case ServiceResultReceiptMethods.UnitInAdministration:
                            dst.UnitInAdministration = src.Item != null ? ctx.Mapper.Map<ServiceApplicantReceiptDataUnitInAdministration>(src.Item) : null;
                            break;
                    }
                });

            CreateMap<ServiceApplicantReceiptDataMunicipalityAdministrationAdress, ServiceApplicantReceiptDataMunicipalityAdministrationAdressVM>();
            CreateMap<Citizenship, CitizenshipVM>();
            CreateMap<Domain.Models.Declaration, DeclarationVM>().AfterMap((s, d) =>
            {
                d.Code = s.DeclarationCode;
                d.Content = s.DeclarationName;
            });

            CreateMap<TravelDocument, TravelDocumentVM>()
             .AfterMap((s, d, ctx) =>
             {
                 d.IdentityIssuer = ctx.Mapper.Map<IssuerCountry, IssuerCountryVM>(s.IdentityIssuer);
             });

            #region RemovingIrregularitiesInstructions

            CreateMap<RemovingIrregularitiesInstructionsIrregularities, RemovingIrregularitiesInstructionsIrregularitiesVM>();

            CreateMap<RemovingIrregularitiesInstructions, RemovingIrregularitiesInstructionsVM>()
                .ForMember(s => s.DeadlineCorrectionIrregularities, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicData, ElectronicServiceProviderBasicDataVM>(s.ElectronicServiceProviderBasicData);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicant, ElectronicServiceApplicantVM>(s.ElectronicServiceApplicant);
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURI, AISCaseURIVM>(s.AISCaseURI);

                    d.DeadlineCorrectionIrregularities = MapDeadlineDomainToViewModel(s.DeadlineCorrectionIrregularities);
                    MapperHelper.MapDocumentWithOfficialDomainToViewModel<RemovingIrregularitiesInstructionsOfficial>(s, d);

                    d.Irregularities = ctx.Mapper.Map<List<RemovingIrregularitiesInstructionsIrregularities>, List<RemovingIrregularitiesInstructionsIrregularitiesVM>>(s.Irregularities);
                });

            #endregion

            #region ReceiptAcknowledgedMessage

            CreateMap<ReceiptAcknowledgedMessage, ReceiptAcknowledgedMessageVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.ElectronicServiceProvider = ctx.Mapper.Map<ElectronicServiceProviderBasicData, ElectronicServiceProviderBasicDataVM>(s.ElectronicServiceProvider);
                    d.RegisteredBy = ctx.Mapper.Map<ReceiptAcknowledgedMessageRegisteredBy, ReceiptAcknowledgedMessageRegisteredByVM>(s.RegisteredBy);
                    d.Applicant = ctx.Mapper.Map<ElectronicServiceApplicant, ElectronicServiceApplicantVM>(s.Applicant);
                });

            #endregion

            #region ReceiptNotAcknowledgedMessage

            CreateMap<ReceiptNotAcknowledgedMessage, ReceiptNotAcknowledgedMessageVM>()
              .AfterMap((s, d, ctx) =>
              {
                  d.ElectronicServiceProvider = ctx.Mapper.Map<ElectronicServiceProviderBasicData, ElectronicServiceProviderBasicDataVM>(s.ElectronicServiceProvider);
                  d.Applicant = ctx.Mapper.Map<ElectronicServiceApplicant, ElectronicServiceApplicantVM>(s.Applicant);
              });

            #endregion

            #region PaymentInstructions

            CreateMap<PaymentInstructions, PaymentInstructionsVM>()
                .ForMember(s => s.DeadlineForPayment, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicData, ElectronicServiceProviderBasicDataVM>(s.ElectronicServiceProviderBasicData);
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURI, AISCaseURIVM>(s.AISCaseURI);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicant, ElectronicServiceApplicantVM>(s.ElectronicServiceApplicant);
                    d.DeadlineForPayment = MapDeadlineDomainToViewModel(s.DeadlineForPayment);
                    d.Amount = Convert.ToDecimal(s.Amount);
                });

            #endregion

            #region ReceiptAcknowledgedPaymentForMOI

            CreateMap<ReceiptAcknowledgedPaymentForMOI, ReceiptAcknowledgedPaymentForMOIVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicData, ElectronicServiceProviderBasicDataVM>(s.ElectronicServiceProviderBasicData);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicant, ElectronicServiceApplicantVM>(s.ElectronicServiceApplicant);
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURI, AISCaseURIVM>(s.AISCaseURI);
                    d.Amount = Convert.ToDecimal(s.Amount.ToString());
                });

            #endregion

            #region OutstandingConditionsForStartOfServiceMessage

            CreateMap<OutstandingConditionsForStartOfServiceMessage, OutstandingConditionsForStartOfServiceMessageVM>()
              .AfterMap((s, d, ctx) =>
              {
                  d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicData, ElectronicServiceProviderBasicDataVM>(s.ElectronicServiceProviderBasicData);
                  d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicant, ElectronicServiceApplicantVM>(s.ElectronicServiceApplicant);
                  d.AISCaseURI = ctx.Mapper.Map<AISCaseURI, AISCaseURIVM>(s.AISCaseURI);
              });

            CreateMap<OutstandingConditionsForWithdrawServiceMessage, OutstandingConditionsForWithdrawServiceMessageVM>()
              .AfterMap((s, d, ctx) =>
              {
                  d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicData, ElectronicServiceProviderBasicDataVM>(s.ElectronicServiceProviderBasicData);
                  d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicant, ElectronicServiceApplicantVM>(s.ElectronicServiceApplicant);
                  d.AISCaseURI = ctx.Mapper.Map<AISCaseURI, AISCaseURIVM>(s.AISCaseURI);
              });

            #endregion

            #region Refusal

            CreateMap<Refusal, RefusalVM>()
              .AfterMap((s, d, ctx) =>
              {
                  d.AISCaseURI = ctx.Mapper.Map<AISCaseURI, AISCaseURIVM>(s.AISCaseURI);
                  d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicData, ElectronicServiceProviderBasicDataVM>(s.ElectronicServiceProviderBasicData);
                  d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicant, ElectronicServiceApplicantVM>(s.ElectronicServiceApplicant);
                  MapperHelper.MapDocumentWithOfficialDomainToViewModel<RefusalOfficial>(s, d);
              });

            #endregion

            #region DocumentsWillBeIssuedMessage

            CreateMap<DocumentsWillBeIssuedMessage, DocumentsWillBeIssuedMessageVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURI, AISCaseURIVM>(s.AISCaseURI);
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicData, ElectronicServiceProviderBasicDataVM>(s.ElectronicServiceProviderBasicData);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicant, ElectronicServiceApplicantVM>(s.ElectronicServiceApplicant);
                });

            #endregion

            #region TerminationOfServiceMessage

            CreateMap<TerminationOfServiceMessage, TerminationOfServiceMessageVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicData, ElectronicServiceProviderBasicDataVM>(s.ElectronicServiceProviderBasicData);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicant, ElectronicServiceApplicantVM>(s.ElectronicServiceApplicant);
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURI, AISCaseURIVM>(s.AISCaseURI);
                });

            #endregion

            #region ActionsTakenMessage

            CreateMap<ActionsTakenMessage, ActionsTakenMessageVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicData, ElectronicServiceProviderBasicDataVM>(s.ElectronicServiceProviderBasicData);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicant, ElectronicServiceApplicantVM>(s.ElectronicServiceApplicant);
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURI, AISCaseURIVM>(s.AISCaseURI);
                });

            #endregion

            #region ApplicationForWithdrawService

            CreateMap<ApplicationForWithdrawService, ApplicationForWithdrawServiceVM>()
                   .ForMember(d => d.Declarations, (config) => config.Ignore())
                   .AfterMap((s, d, ctx) =>
                   {
                       MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                       d.Circumstances = ctx.Mapper.Map<ApplicationForWithdrawServiceData, ApplicationForWithdrawServiceDataVM>(s.ApplicationForWithdrawServiceData);
                   });

            CreateMap<ApplicationForWithdrawServiceData, ApplicationForWithdrawServiceDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.PoliceDepartment = s.IssuingPoliceDepartment;
                });

            #endregion
        }

        private void CreateViewToDomainModelMap()
        {
            CreateMap<IdentificationPhotoAndSignatureVM, IdentificationPhotoAndSignature>()
                .ForMember(s => s.IdentificationPhoto, (config) => config.Ignore())
                .ForMember(s => s.IdentificationSignature, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    if (!string.IsNullOrWhiteSpace(s.IdentificationPhoto))
                        d.IdentificationPhoto = Convert.FromBase64String(s.IdentificationPhoto);

                    if (!string.IsNullOrWhiteSpace(s.IdentificationSignature))
                        d.IdentificationSignature = Convert.FromBase64String(s.IdentificationSignature);
                });

            CreateMap<ReceiptAcknowledgedMessageRegisteredByVM, ReceiptAcknowledgedMessageRegisteredBy>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.ItemOfficer != null)
                    {
                        d.Item = s.ItemOfficer;
                    }
                    else if (!string.IsNullOrWhiteSpace(s.ItemAISURI))
                    {
                        d.Item = s.ItemAISURI;
                    }
                });

            CreateMap<ElectronicServiceProviderBasicDataVM, ElectronicServiceProviderBasicData>();

            CreateMap<AISCaseURIVM, AISCaseURI>()
                .AfterMap((s, d) =>
                {
                    switch (s.Choise)
                    {
                        case ChoiceType.DocumentUri:
                            d.Item = s.DocumentUri;
                            break;
                        case ChoiceType.TextUri:
                            d.Item = s.TextUri;
                            break;
                        default:
                            d.Item = null;
                            break;
                    }
                });

            CreateMap<ElectronicAdministrativeServiceHeaderVM, ElectronicAdministrativeServiceHeader>();

            CreateMap<IdentityDocumentBasicDataVM, IdentityDocumentBasicData>()
            .AfterMap((src, dst) =>
            {
                dst.IdentityDocumentType = src.IdentityDocumentType.GetValueOrDefault();
                dst.IdentitityIssueDate = src.IdentitityIssueDate.GetValueOrDefault();
            });

            CreateMap<PersonBasicDataVM, PersonBasicData>()
               .AfterMap((src, dst, ctx) =>
               {
                   dst.IdentityDocument = ctx.Mapper.Map<IdentityDocumentBasicData>(src.IdentityDocument);
               });

            CreateMap<ForeignEntityBasicDataVM, ForeignEntityBasicData>()
                .AfterMap((src, dst) =>
                {
                    switch (src.SelectedChoiceType)
                    {
                        case ForeignEntityBasicDataVM.ForeignEntityChoiceType.RegisterData:
                            dst.ForeignEntityOtherData = null;
                            break;
                        case ForeignEntityBasicDataVM.ForeignEntityChoiceType.OtherData:
                            dst.ForeignEntityRegister = null;
                            dst.ForeignEntityIdentifier = null;
                            break;
                        default:
                            break;
                    }
                });

            CreateMap<DocumentMustServeToVM, DocumentMustServeTo>()
                 .AfterMap((s, d) =>
                 {
                     if (s.ItemElementName == ItemChoiceType1.AbroadDocumentMustServeTo)
                         d.Item = s.ItemAbroadDocumentMustServeTo;

                     if (s.ItemElementName == ItemChoiceType1.InRepublicOfBulgariaDocumentMustServeTo)
                         d.Item = s.ItemInRepublicOfBulgariaDocumentMustServeTo;

                     if (s.ItemElementName.HasValue)
                         d.ItemElementName = s.ItemElementName.Value;
                 });

            CreateMap<ElectronicStatementAuthorVM, ElectronicStatementAuthor>()
                .AfterMap((src, dst, ctx) =>
                {
                    switch (src.SelectedChoiceType)
                    {
                        case PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person:
                            dst.Item = ctx.Mapper.Map<PersonBasicData>(src.ItemPersonBasicData);
                            break;
                        case PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.ForeignPerson:
                            dst.Item = src.ItemForeignCitizenBasicData;
                            break;
                        default:
                            dst.Item = null;
                            break;
                    }
                });


            CreateMap<ElectronicServiceRecipientVM, ElectronicServiceRecipient>()
                .AfterMap((src, dst, ctx) =>
                {
                    switch (src.SelectedChoiceType)
                    {
                        case PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person:
                            dst.Item = ctx.Mapper.Map<PersonBasicData>(src.ItemPersonBasicData);
                            break;
                        case PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.ForeignPerson:
                            dst.Item = src.ItemForeignCitizenBasicData;
                            break;
                        case PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Entity:
                            dst.Item = src.ItemEntityBasicData;
                            break;
                        case PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.ForeignEntity:
                            dst.Item = ctx.Mapper.Map<ForeignEntityBasicData>(src.ItemForeignEntityBasicData);
                            break;
                        default:
                            dst.Item = null;
                            break;
                    }
                });

            CreateMap<RecipientGroupVM, RecipientGroup>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.AuthorWithQuality != null)
                    {
                        d.Authors = new List<ElectronicStatementAuthor>()
                        {
                            ctx.Mapper.Map<ElectronicStatementAuthor>(s.AuthorWithQuality.Author)
                        };

                        d.Authors[0].AuthorQuality = s.AuthorWithQuality.SelectedAuthorQuality;
                    }

                    ElectronicServiceRecipient recipient = ctx.Mapper.Map<ElectronicServiceRecipient>(s.Recipient);

                    d.Recipients = new List<ElectronicServiceRecipient>() { recipient };
                });

            CreateMap<ElectronicServiceApplicantVM, ElectronicServiceApplicant>()
                .AfterMap((src, dst, ctx) =>
            {
                dst.RecipientGroups = new List<RecipientGroup>()
                {
                    ctx.Mapper.Map<RecipientGroup>(src.RecipientGroup)
                };
                dst.EmailAddress = src.RecipientGroup?.AuthorWithQuality?.Author?.EmailAddress;
            });

            CreateMap<ServiceApplicantReceiptDataVM, ServiceApplicantReceiptData>()
                .AfterMap((src, dst, ctx) =>
                {
                    switch (src.ServiceResultReceiptMethod)
                    {
                        case ServiceResultReceiptMethods.DeskInAdministration:
                            dst.Item = ctx.Mapper.Map(src.MunicipalityAdministrationAdress, src.MunicipalityAdministrationAdress.GetType(), typeof(ServiceApplicantReceiptDataMunicipalityAdministrationAdress));
                            break;
                        case ServiceResultReceiptMethods.CourierToOtherAddress:
                            dst.Item = src.ApplicantAdress;
                            break;
                        case ServiceResultReceiptMethods.CourierToMailBox:
                            dst.Item = src.PostOfficeBox;
                            break;
                        case ServiceResultReceiptMethods.UnitInAdministration:
                            if (src.UnitInAdministration != null)
                                dst.Item = src.UnitInAdministration;
                            break;
                    }
                });

            CreateMap<ServiceApplicantReceiptDataMunicipalityAdministrationAdressVM, ServiceApplicantReceiptDataMunicipalityAdministrationAdress>();
            CreateMap<CitizenshipVM, Citizenship>();
            CreateMap<DeclarationVM, Domain.Models.Declaration>().AfterMap((s, d) =>
            {
                d.DeclarationCode = s.Code;
                d.DeclarationName = s.Content;
            });

            CreateMap<TravelDocumentVM, TravelDocument>()
              .AfterMap((s, d, ctx) =>
              {
                  d.IdentityIssuer = ctx.Mapper.Map<IssuerCountryVM, IssuerCountry>(s.IdentityIssuer);
              });

            #region RemovingIrregularitiesInstructions

            CreateMap<RemovingIrregularitiesInstructionsVM, RemovingIrregularitiesInstructions>()
                .ForMember(s => s.DeadlineCorrectionIrregularities, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicDataVM, ElectronicServiceProviderBasicData>(s.ElectronicServiceProviderBasicData);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicantVM, ElectronicServiceApplicant>(s.ElectronicServiceApplicant);
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURIVM, AISCaseURI>(s.AISCaseURI);

                    d.DeadlineCorrectionIrregularities = MapDeadLineViewModelToDomain(s.DeadlineCorrectionIrregularities);
                    MapperHelper.MapDocumentWithOfficialViewModelToDomain<RemovingIrregularitiesInstructionsOfficial>(s, d);

                    if (s.Irregularities != null)
                    {
                        d.Irregularities = ctx.Mapper.Map<List<RemovingIrregularitiesInstructionsIrregularitiesVM>, List<RemovingIrregularitiesInstructionsIrregularities>>(s.Irregularities);
                    }

                    if (d.XMLDigitalSignature == null)
                    {
                        d.XMLDigitalSignature = new XMLDigitalSignature();
                    }
                });

            CreateMap<RemovingIrregularitiesInstructionsIrregularitiesVM, RemovingIrregularitiesInstructionsIrregularities>();

            #endregion

            #region ReceiptAcknowledgedMessage

            CreateMap<ReceiptAcknowledgedMessageVM, ReceiptAcknowledgedMessage>()
              .AfterMap((s, d, ctx) =>
              {
                  d.ElectronicServiceProvider = ctx.Mapper.Map<ElectronicServiceProviderBasicDataVM, ElectronicServiceProviderBasicData>(s.ElectronicServiceProvider);
                  d.RegisteredBy = ctx.Mapper.Map<ReceiptAcknowledgedMessageRegisteredByVM, ReceiptAcknowledgedMessageRegisteredBy>(s.RegisteredBy);
                  d.Applicant = ctx.Mapper.Map<ElectronicServiceApplicantVM, ElectronicServiceApplicant>(s.Applicant);
              });

            #endregion

            #region ReceiptNotAcknowledgedMessage

            CreateMap<ReceiptNotAcknowledgedMessageVM, ReceiptNotAcknowledgedMessage>()
              .AfterMap((s, d, ctx) =>
              {
                  d.ElectronicServiceProvider = ctx.Mapper.Map<ElectronicServiceProviderBasicDataVM, ElectronicServiceProviderBasicData>(s.ElectronicServiceProvider);
                  d.Applicant = ctx.Mapper.Map<ElectronicServiceApplicantVM, ElectronicServiceApplicant>(s.Applicant);
              });

            #endregion

            #region PaymentInstructions

            CreateMap<PaymentInstructionsVM, PaymentInstructions>()
                .ForMember(s => s.DeadlineForPayment, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicDataVM, ElectronicServiceProviderBasicData>(s.ElectronicServiceProviderBasicData);
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURIVM, AISCaseURI>(s.AISCaseURI);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicantVM, ElectronicServiceApplicant>(s.ElectronicServiceApplicant);
                    d.DeadlineForPayment = MapDeadLineViewModelToDomain(s.DeadlineForPayment);
                    d.Amount = Convert.ToDouble(s.Amount);
                });

            #endregion

            #region ReceiptAcknowledgedPaymentForMOI

            CreateMap<ReceiptAcknowledgedPaymentForMOIVM, ReceiptAcknowledgedPaymentForMOI>()
              .AfterMap((s, d, ctx) =>
              {
                  d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicDataVM, ElectronicServiceProviderBasicData>(s.ElectronicServiceProviderBasicData);
                  d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicantVM, ElectronicServiceApplicant>(s.ElectronicServiceApplicant);
                  d.AISCaseURI = ctx.Mapper.Map<AISCaseURIVM, AISCaseURI>(s.AISCaseURI);
                  d.Amount = Convert.ToDouble(s.Amount.ToString());

              });

            #endregion

            #region OutstandingConditionsForStartOfServiceMessage

            CreateMap<OutstandingConditionsForStartOfServiceMessageVM, OutstandingConditionsForStartOfServiceMessage>()
              .AfterMap((s, d, ctx) =>
              {
                  d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicDataVM, ElectronicServiceProviderBasicData>(s.ElectronicServiceProviderBasicData);
                  d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicantVM, ElectronicServiceApplicant>(s.ElectronicServiceApplicant);
                  d.AISCaseURI = ctx.Mapper.Map<AISCaseURIVM, AISCaseURI>(s.AISCaseURI);
              });

            CreateMap<OutstandingConditionsForWithdrawServiceMessageVM, OutstandingConditionsForWithdrawServiceMessage>()
              .AfterMap((s, d, ctx) =>
              {
                  d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicDataVM, ElectronicServiceProviderBasicData>(s.ElectronicServiceProviderBasicData);
                  d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicantVM, ElectronicServiceApplicant>(s.ElectronicServiceApplicant);
                  d.AISCaseURI = ctx.Mapper.Map<AISCaseURIVM, AISCaseURI>(s.AISCaseURI);
              });

            #endregion

            #region Refusal

            CreateMap<RefusalVM, Refusal>()
              .AfterMap((s, d, ctx) =>
              {
                  d.AISCaseURI = ctx.Mapper.Map<AISCaseURIVM, AISCaseURI>(s.AISCaseURI);
                  d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicDataVM, ElectronicServiceProviderBasicData>(s.ElectronicServiceProviderBasicData);
                  d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicantVM, ElectronicServiceApplicant>(s.ElectronicServiceApplicant);
                  MapperHelper.MapDocumentWithOfficialViewModelToDomain<RefusalOfficial>(s, d);
              });

            #endregion

            #region DocumentsWillBeIssuedMessage

            CreateMap<DocumentsWillBeIssuedMessageVM, DocumentsWillBeIssuedMessage>()
                .AfterMap((s, d, ctx) =>
                {
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURIVM, AISCaseURI>(s.AISCaseURI);
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicDataVM, ElectronicServiceProviderBasicData>(s.ElectronicServiceProviderBasicData);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicantVM, ElectronicServiceApplicant>(s.ElectronicServiceApplicant);
                });

            #endregion

            #region TerminationOfServiceMessage

            CreateMap<TerminationOfServiceMessageVM, TerminationOfServiceMessage>()
                .AfterMap((s, d, ctx) =>
                {
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicDataVM, ElectronicServiceProviderBasicData>(s.ElectronicServiceProviderBasicData);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicantVM, ElectronicServiceApplicant>(s.ElectronicServiceApplicant);
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURIVM, AISCaseURI>(s.AISCaseURI);
                });

            #endregion

            #region ActionsTakenMessage

            CreateMap<ActionsTakenMessageVM, ActionsTakenMessage>()
               .AfterMap((s, d, ctx) =>
               {
                   d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicDataVM, ElectronicServiceProviderBasicData>(s.ElectronicServiceProviderBasicData);
                   d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicantVM, ElectronicServiceApplicant>(s.ElectronicServiceApplicant);
                   d.AISCaseURI = ctx.Mapper.Map<AISCaseURIVM, AISCaseURI>(s.AISCaseURI);
               });

            #endregion

            #region ApplicationForWithdrawService

            CreateMap<ApplicationForWithdrawServiceVM, ApplicationForWithdrawService>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                    d.ApplicationForWithdrawServiceData = ctx.Mapper.Map<ApplicationForWithdrawServiceData>(s.Circumstances);
                });

            CreateMap<ApplicationForWithdrawServiceDataVM, ApplicationForWithdrawServiceData>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.PoliceDepartment != null)
                        d.IssuingPoliceDepartment = s.PoliceDepartment;
                });

            #endregion
        }

        #region Helpers

        private static DeadlineVM MapDeadlineDomainToViewModel(string domainModel)
        {
            DeadlineVM res = new DeadlineVM();

            try
            {
                res.OriginalDeadline = domainModel;
                TimeSpan deadline = XmlConvert.ToTimeSpan(domainModel); //System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDuration.Parse(domainModel);

                if (deadline.TotalDays > 0)
                {
                    res.PeriodValue = (int)deadline.TotalDays;
                    res.ExecutionPeriodType = ExecutionPeriodType.Days;
                }
                else if (deadline.TotalHours > 0)
                {
                    res.PeriodValue = (int)deadline.TotalHours;
                    res.ExecutionPeriodType = ExecutionPeriodType.Hours;
                }
                else
                    throw new NotSupportedException();
            }
            catch
            { }

            return res;
        }

        private static string MapDeadLineViewModelToDomain(DeadlineVM viewModel)
        {
            try
            {
                if (viewModel.PeriodValue.HasValue && viewModel.ExecutionPeriodType.HasValue)
                {
                    if (viewModel.ExecutionPeriodType.Value == ExecutionPeriodType.Days)
                        return XmlConvert.ToString(new TimeSpan(viewModel.PeriodValue.Value, 0, 0, 0));
                    else
                        return XmlConvert.ToString(new TimeSpan(viewModel.PeriodValue.Value, 0, 0));
                }
            }
            catch
            { }

            return null;
        }

        #endregion
    }
}
