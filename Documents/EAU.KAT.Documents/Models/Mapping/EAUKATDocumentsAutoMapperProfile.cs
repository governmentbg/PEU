using AutoMapper;
using CNSys;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Documents.Models.Mapping;
using EAU.KAT.Documents.Domain.Models;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.KAT.Documents.Models.Forms;
using EAU.KAT.Documents.Models.Forms.DocumentForms;
using System.Collections.Generic;
using System.Linq;

namespace EAU.KAT.Documents.Models.Mapping
{
    public class EAUKATDocumentsAutoMapperProfile : Profile
    {
        public EAUKATDocumentsAutoMapperProfile()
        {
            CreateDomainToViewModelMap();
            CreateViewToDomainModelMap();
        }

        private void CreateDomainToViewModelMap()
        {
            CreateMap<VehicleOwnerInformationItem, VehicleOwnerInformationItemVM>()
               .AfterMap((s, d, ctx) =>
               {
                   d.Owner = new PersonAndEntityBasicDataVM();

                   if (s.Item is EntityBasicData)
                       d.Owner.ItemEntityBasicData = s.Item as EntityBasicData;

                   if (s.Item is PersonBasicData)
                       d.Owner.ItemPersonBasicData = (ctx.Mapper.Map<PersonBasicDataVM>(s.Item as PersonBasicData));
               });

            CreateMap<VehicleData, VehicleDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Item is VehicleDataItem)
                    {
                        d.VehicleData = ctx.Mapper.Map<VehicleDataItemVM>(s.Item);
                        d.VehicleDataCollection = null;
                    }

                    if (s.Item is VehicleDataItemCollection)
                    {
                        d.VehicleDataCollection = new List<VehicleDataItemVM>();

                        foreach (VehicleDataItem item in ((VehicleDataItemCollection)s.Item).Items)
                        {
                            d.VehicleDataCollection.Add(ctx.Mapper.Map<VehicleDataItemVM>(item));
                        }

                        d.VehicleData = null;
                    }
                });

            CreateMap<VehicleDataItem, VehicleDataItemVM>();

            CreateMap<ApplicationForIssuingDocumentofVehicleOwnership, ApplicationForIssuingDocumentofVehicleOwnershipVM>()
                 .ForMember(d => d.Declarations, (config) => config.Ignore())
                 .AfterMap((s, d, ctx) =>
                 {
                     MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                     d.Circumstances = ctx.Mapper.Map<ApplicationForIssuingDocumentofVehicleOwnershipDataVM>(s.ApplicationForIssuingDocumentofVehicleOwnershipData);
                 });

            CreateMap<ApplicationForIssuingDocumentofVehicleOwnershipData, ApplicationForIssuingDocumentofVehicleOwnershipDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Item != null)
                    {
                        d.RegistrationAndMake = new ApplicationForIssuingDocumentofVehicleOwnershipDataRegistrationAndMakeVM()
                        {
                            MakeModel = s.Item.MakeModel,
                            RegistrationNumber = s.Item.RegistrationNumber
                        };
                    }
                });

            CreateMap<ApplicationForIssuingDocumentofVehicleOwnershipDataRegistrationAndMake, ApplicationForIssuingDocumentofVehicleOwnershipDataRegistrationAndMakeVM>();

            CreateMap<VehicleDataRequestOwnersCollectionOwners, OwnerVM>()
            .AfterMap((s, d, ctx) =>
            {
                if (s.Item is PersonIdentifier personIdentifier)
                {
                    d.Type = PersonEntityChoiceType.Person;
                    d.PersonIdentifier = personIdentifier;
                }
                else if (s.Item is string item)
                {
                    d.Type = PersonEntityChoiceType.Entity;
                    d.EntityIdentifier = item;
                }
                else
                {
                    d.Type = null;
                    d.PersonIdentifier = null;
                    d.EntityIdentifier = null;
                }
            });

            CreateMap<VehicleDataRequestOwnersCollection, OwnersCollectionVM>()
               .AfterMap((s, d, ctx) =>
               {
                   if (s.Owners != null && s.Owners.Count > 0)
                       d.Owners = s.Owners.Select(o => ctx.Mapper.Map<OwnerVM>(o)).ToList();
               });

            CreateMap<VehicleDataRequest, VehicleDataRequestVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.OwnersCollection = ctx.Mapper.Map<OwnersCollectionVM>(s.OwnersCollection);
                });

            CreateMap<ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData, ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataVM>()
                .ForMember(d => d.AuthorizedPersons, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    if (s.AuthorizedPersons != null && s.AuthorizedPersons.Count > 0)
                    {
                        d.AuthorizedPersons = new AuthorizedPersonCollectionVM() { Items = s.AuthorizedPersons };
                    }
                });

            #region ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriver

            CreateMap<ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriver, ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverVM>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                    d.Circumstances = ctx.Mapper.Map<ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverDataVM>(s.ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData);
                });

            CreateMap<ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData, ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverDataVM>();

            #endregion

            #region RequestForDecisionForDeal

            CreateMap<RequestForDecisionForDeal, RequestForDecisionForDealVM>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                   d.Circumstances = ctx.Mapper.Map<RequestForDecisionForDealDataVM>(s.RequestForDecisionForDealData);
                   d.Circumstances.NotaryIdentityNumber = s.NotaryIdentityNumber;
               });

            CreateMap<RequestForDecisionForDealData, RequestForDecisionForDealDataVM>()
               .AfterMap((s, d, ctx) =>
               {
                   d.VehicleRegistrationData = ctx.Mapper.Map<VehicleRegistrationData, VehicleRegistrationDataVM>(s.VehicleRegistrationData);
               }); ;

            CreateMap<RequestForDecisionForDealDataOwner, VehicleOwnerDataVM>()
             .AfterMap((s, d, ctx) =>
             {
                 d.PersonEntityData = new PersonEntityDataVM();
                 if (s.Item is Domain.Models.PersonData)
                 {
                     d.PersonEntityData.Person = ctx.Mapper.Map<Domain.Models.PersonData, PersonDataVM>((Domain.Models.PersonData)s.Item);
                     d.PersonEntityData.SelectedChoiceType = PersonEntityChoiceType.Person;
                 }
                 if (s.Item is EntityData)
                 {
                     d.PersonEntityData.Entity = ctx.Mapper.Map<EntityData, EntityDataVM>((EntityData)s.Item);
                     d.PersonEntityData.SelectedChoiceType = PersonEntityChoiceType.Entity;
                 }
             });

            CreateMap<RequestForDecisionForDealDataBuyer, VehicleBuyerDataVM>()
                 .AfterMap((s, d, ctx) =>
                 {
                     d.PersonEntityData = new PersonEntityDataVM();
                     if (s.Item is Domain.Models.PersonData)
                     {
                         d.PersonEntityData.Person = ctx.Mapper.Map<Domain.Models.PersonData, PersonDataVM>((Domain.Models.PersonData)s.Item);
                         d.PersonEntityData.SelectedChoiceType = PersonEntityChoiceType.Person;
                     }
                     if (s.Item is EntityData)
                     {
                         d.PersonEntityData.Entity = ctx.Mapper.Map<EntityData, EntityDataVM>((EntityData)s.Item);
                         d.PersonEntityData.SelectedChoiceType = PersonEntityChoiceType.Entity;
                     }
                 });

            CreateMap<VehicleRegistrationData, VehicleRegistrationDataVM>();

            CreateMap<Domain.Models.PersonData, PersonDataVM>()
              .AfterMap((s, d, ctx) =>
              {
                  if (s.PersonBasicData.Identifier != null)
                  {
                      d.Identifier = new PersonIdentifierVM()
                      {
                          Item = s.PersonBasicData.Identifier.Item,
                          ItemElementName = s.PersonBasicData.Identifier.ItemElementName
                      };
                  }
                  
                  d.IdentityNumber = s.PersonBasicData.IdentityDocument != null ? s.PersonBasicData.IdentityDocument.IdentityNumber : null;
                  d.Names = s.PersonBasicData.Names;
              });

            CreateMap<EntityData, EntityDataVM>();

            #endregion

            #region RequestForApplicationForIssuingDuplicateDrivingLicense

            CreateMap<RequestForApplicationForIssuingDuplicateDrivingLicense, RequestForApplicationForIssuingDuplicateDrivingLicenseVM>()
              .ForMember(d => d.Declarations, (config) => config.Ignore())
              .AfterMap((s, d, ctx) =>
              {
                  MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
              });

            #endregion

            #region CertificateOfVehicleOwnership

            CreateMap<CertificateOfVehicleOwnership, CertificateOfVehicleOwnershipVM>()
                .ForMember(d => d.VehicleOwnerInformationCollection, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURIVM>(s.AISCaseURI);
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicDataVM>(s.ElectronicServiceProviderBasicData);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicantVM>(s.ElectronicServiceApplicant);
                    d.VehicleData = ctx.Mapper.Map<VehicleDataVM>(s.VehicleData);

                    if (s.VehicleOwnerInformationCollection != null &&
                        s.VehicleOwnerInformationCollection.Items != null &&
                        s.VehicleOwnerInformationCollection.Items.Count > 0)
                    {
                        d.VehicleOwnerInformationCollection = new List<VehicleOwnerInformationItemVM>();

                        foreach (var item in s.VehicleOwnerInformationCollection.Items)
                        {
                            d.VehicleOwnerInformationCollection.Add(ctx.Mapper.Map<VehicleOwnerInformationItemVM>(item));
                        }
                    }

                    MapperHelper.MapDocumentWithOfficialDomainToViewModel<CertificateOfVehicleOwnershipOfficial>(s, d);
                });

            #endregion

            #region CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriver

            CreateMap<CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriver, CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURIVM>(s.AISCaseURI);
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicDataVM>(s.ElectronicServiceProviderBasicData);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicantVM>(s.ElectronicServiceApplicant);

                    MapperHelper.MapDocumentWithOfficialDomainToViewModel<CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverOfficial>(s, d);
                });

            #endregion

            #region ApplicationForIssuingDriverLicense

            CreateMap<ApplicationForIssuingDriverLicense, ApplicationForIssuingDriverLicenseVM>()
                   .ForMember(d => d.Declarations, (config) => config.Ignore())
                   .AfterMap((s, d, ctx) =>
                   {
                       MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                       d.Circumstances = ctx.Mapper.Map<ApplicationForIssuingDriverLicenseData, ApplicationForIssuingDriverLicenseDataVM>(s.ApplicationForIssuingDriverLicenseData);
                       d.Circumstances.PoliceDepartment = s.IssuingPoliceDepartment;
                       d.IdentificationPhotoAndSignature = ctx.Mapper.Map<IdentificationPhotoAndSignature, IdentificationPhotoAndSignatureVM>(s.IdentificationPhotoAndSignature);
                   });

            CreateMap<ApplicationForIssuingDriverLicenseData, ApplicationForIssuingDriverLicenseDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.Person = ctx.Mapper.Map<EAU.Documents.Domain.Models.PersonData, PersonDataExtendedVM>(s.Person);
                    d.TravelDocument = ctx.Mapper.Map<TravelDocument, TravelDocumentVM>(s.TravelDocument);
                    d.Citizenship = ctx.Mapper.Map<Citizenship, CitizenshipVM>(s.ForeignCitizenship);
                });

            #endregion

            #region ReportForChangingOwnership

            CreateMap<ReportForChangingOwnership, ReportForChangingOwnershipVM>();

            CreateMap<ReportForChangingOwnershipOldOwnersDataOldOwners, ReportForChangingOwnershipOldOwnersDataOldOwnersVM>()
             .AfterMap((s, d, ctx) =>
             {
                 d.PersonEntityData = new PersonEntityDataVM();
                 if (s.Item is Domain.Models.PersonData)
                 {
                     d.PersonEntityData.Person = ctx.Mapper.Map<Domain.Models.PersonData, PersonDataVM>((Domain.Models.PersonData)s.Item);
                     d.PersonEntityData.SelectedChoiceType = PersonEntityChoiceType.Person;
                 }
                 if (s.Item is EntityData)
                 {
                     d.PersonEntityData.Entity = ctx.Mapper.Map<EntityData, EntityDataVM>((EntityData)s.Item);
                     d.PersonEntityData.SelectedChoiceType = PersonEntityChoiceType.Entity;
                 }
             });

            CreateMap<ReportForChangingOwnershipNewOwnersDataNewOwners, ReportForChangingOwnershipNewOwnersDataNewOwnersVM>()
                 .AfterMap((s, d, ctx) =>
                 {
                     d.PersonEntityData = new PersonEntityDataVM();
                     if (s.Item is Domain.Models.PersonData)
                     {
                         d.PersonEntityData.Person = ctx.Mapper.Map<Domain.Models.PersonData, PersonDataVM>((Domain.Models.PersonData)s.Item);
                         d.PersonEntityData.SelectedChoiceType = PersonEntityChoiceType.Person;
                     }
                     if (s.Item is EntityData)
                     {
                         d.PersonEntityData.Entity = ctx.Mapper.Map<EntityData, EntityDataVM>((EntityData)s.Item);
                         d.PersonEntityData.SelectedChoiceType = PersonEntityChoiceType.Entity;
                     }
                 });

            CreateMap<ReportForChangingOwnershipOldOwnersData, ReportForChangingOwnershipOldOwnersDataVM>();
            CreateMap<ReportForChangingOwnershipNewOwnersData, ReportForChangingOwnershipNewOwnersDataVM>();

            #endregion

            #region DataForPrintSRMPS

            CreateMap<DataForPrintSRMPSDataNewOwner, PersonEntityDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Item is Domain.Models.PersonData)
                    {
                        d.Person = ctx.Mapper.Map<Domain.Models.PersonData, PersonDataVM>((Domain.Models.PersonData)s.Item);
                        d.SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Person;
                    }

                    if (s.Item is EntityData entityData)
                    {
                        d.Entity = ctx.Mapper.Map<EntityData, EntityDataVM>((EntityData)s.Item);

                        if (entityData.Identifier != null && entityData.Identifier.Length == 10)
                            d.SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Farmer;
                        else
                            d.SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Entity;
                    }
                });

            CreateMap<DataForPrintSRMPSDataHolderData, PersonEntityDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Item is Domain.Models.PersonData)
                    {
                        d.Person = ctx.Mapper.Map<Domain.Models.PersonData, PersonDataVM>((Domain.Models.PersonData)s.Item);
                        d.SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Person;
                    }

                    if (s.Item is EntityData entityData)
                    {
                        d.Entity = ctx.Mapper.Map<EntityData, EntityDataVM>((EntityData)s.Item);

                        if (entityData.Identifier != null && entityData.Identifier.Length == 10)
                            d.SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Farmer;
                        else
                            d.SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Entity;
                    }
                });

            CreateMap<DataForPrintSRMPSDataUserData, PersonEntityDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Item is Domain.Models.PersonData)
                    {
                        d.Person = ctx.Mapper.Map<Domain.Models.PersonData, PersonDataVM>((Domain.Models.PersonData)s.Item);
                        d.SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Person;
                    }

                    if (s.Item is EntityData entityData)
                    {
                        d.Entity = ctx.Mapper.Map<EntityData, EntityDataVM>((EntityData)s.Item);

                        if (entityData.Identifier != null && entityData.Identifier.Length == 10)
                            d.SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Farmer;
                        else
                            d.SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Entity;
                    }
                });

            CreateMap<DataForPrintSRMPSData, DataForPrintSRMPSDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.HolderData = ctx.Mapper.Map<DataForPrintSRMPSDataHolderData, PersonEntityDataVM>(s.HolderData);
                    d.UserData = ctx.Mapper.Map<DataForPrintSRMPSDataUserData, PersonEntityDataVM>(s.UserData);

                    if (s.UserData != null)
                        d.HolderNotSameAsUser = true;

                    d.NewOwners = new List<PersonEntityDataVM>();

                    foreach (var newOwner in s.NewOwners)
                    {
                        d.NewOwners.Add(ctx.Mapper.Map<PersonEntityDataVM>(newOwner));
                    }

                    d.NewOwners = ctx.Mapper.Map<List<DataForPrintSRMPSDataNewOwner>, List<PersonEntityDataVM>>(s.NewOwners);
                });

            CreateMap<DataForPrintSRMPS, DataForPrintSRMPSVM>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                    d.Circumstances = ctx.Mapper.Map<DataForPrintSRMPSData, DataForPrintSRMPSDataVM>(s.DataForPrintSRMPSData);
                    d.Circumstances.IssuingPoliceDepartment = s.IssuingPoliceDepartment;
                });

            #endregion

            #region ApplicationForIssuingOfControlCouponForDriverWithNoPenalties

            CreateMap<ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesData, ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesDataVM>();

            CreateMap<ApplicationForIssuingOfControlCouponForDriverWithNoPenalties, ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesVM>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                   d.Circumstances = ctx.Mapper.Map<ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesDataVM>(s.ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesData);
               });

            #endregion

            #region ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCoupon

            CreateMap<ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponData, ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponDataVM>();

            CreateMap<ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCoupon, ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponVM>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                   d.Circumstances = ctx.Mapper.Map<ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponDataVM>(s.ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponData);
               });

            #endregion

            #region ApplicationForTerminationOfVehicleRegistration

            CreateMap<ApplicationForTerminationOfVehicleRegistration, ApplicationForTerminationOfVehicleRegistrationVM>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                   d.Circumstances = ctx.Mapper.Map<VehicleDataRequestVM>(s.VehicleDataRequest);
               });

            #endregion

            #region ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificate

            CreateMap<ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificate, ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateVM>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                   d.Circumstances = ctx.Mapper.Map<VehicleDataRequestVM>(s.VehicleDataRequest);
               });

            #endregion

            #region ApplicationForIssuingTempraryTraficPermitForVehicle

            CreateMap<ApplicationForIssuingTempraryTraficPermitForVehicle, ApplicationForIssuingTempraryTraficPermitForVehicleVM>()
             .ForMember(d => d.Declarations, (config) => config.Ignore())
             .AfterMap((s, d, ctx) =>
             {
                 MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                 d.Circumstances = ctx.Mapper.Map<VehicleDataRequestVM>(s.VehicleDataRequest);
             });

            #endregion

            #region ApplicationForTemporaryTakingOffRoadOfVehicle

            CreateMap<ApplicationForTemporaryTakingOffRoadOfVehicle, ApplicationForTemporaryTakingOffRoadOfVehicleVM>()
             .ForMember(d => d.Declarations, (config) => config.Ignore())
             .AfterMap((s, d, ctx) =>
             {
                 MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                 d.Circumstances = ctx.Mapper.Map<VehicleDataRequestVM>(s.VehicleDataRequest);
             });

            #endregion

            #region ApplicationForCommissioningTemporarilySuspendedVehicle

            CreateMap<ApplicationForCommissioningTemporarilySuspendedVehicle, ApplicationForCommissioningTemporarilySuspendedVehicleVM>()
             .ForMember(d => d.Declarations, (config) => config.Ignore())
             .AfterMap((s, d, ctx) =>
             {
                 MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                 d.Circumstances = ctx.Mapper.Map<VehicleDataRequestVM>(s.VehicleDataRequest);
             });

            #endregion

            #region ReportForVehicle

            CreateMap<ReportForVehicleRPSSVehicleDataOwners, ReportForVehicleRPSSVehicleDataOwnersVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Item is EntityData)
                    {
                        d.EntityData = ctx.Mapper.Map<EntityDataVM>(s.Item);

                    }
                    else if (s.Item is Domain.Models.PersonData)
                    {
                        d.PersonData = ctx.Mapper.Map<PersonDataVM>(s.Item);
                    }
                });

            CreateMap<ReportForVehicleRPSSVehicleData, ReportForVehicleRPSSVehicleDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Owners != null && s.Owners.Count > 0)
                    {
                        d.Owners = s.Owners.Select(el => ctx.Mapper.Map<ReportForVehicleRPSSVehicleDataOwnersVM>(el)).ToList();
                    }
                });

            CreateMap<ReportForVehicle, ReportForVehicleVM>()
                .ForMember(d => d.Owners, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    d.RPSSVehicleData = ctx.Mapper.Map<ReportForVehicleRPSSVehicleDataVM>(s.RPSSVehicleData);

                    if (s.Owners != null && s.Owners.Items != null && s.Owners.Items.Count > 0)
                    {
                        d.Owners = new ReportForVehicleOwnersVM();
                        d.Owners.EntityDataItems = new List<EntityData>();
                        d.Owners.PersonDataItems = new List<Domain.Models.PersonData>();

                        foreach (var owner in s.Owners.Items)
                        {
                            if (owner is Domain.Models.PersonData personOwner)
                            {
                                d.Owners.PersonDataItems.Add(personOwner);
                            }
                            else if (owner is EntityData entityOwner)
                            {
                                d.Owners.EntityDataItems.Add(entityOwner);
                            }
                        }
                    }
                });
            #endregion

            #region DeclarationForLostSRPPS

            CreateMap<DeclarationForLostSRPPS, DeclarationForLostSRPPSVM>()
             .ForMember(d => d.Declarations, (config) => config.Ignore())
             .AfterMap((s, d, ctx) =>
             {
                 MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                 d.Circumstances = s.Data;
             });

            #endregion

            #region ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehicles

            CreateMap<ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehicles, ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesVM>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                    d.Circumstances = ctx.Mapper.Map<ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesDataVM>(s.ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesData);
                });

            CreateMap<ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesData, ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesDataVM>();

            #endregion

            #region ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchants

            CreateMap<ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchants, ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsVM>()
             .ForMember(d => d.Declarations, (config) => config.Ignore())
             .AfterMap((s, d, ctx) =>
             {
                 MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                 d.Circumstances = ctx.Mapper.Map<ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataVM>(s.ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData);
             });

            #endregion

            #region ApplicationForChangeRegistrationOfVehicles

            CreateMap<ApplicationForChangeRegistrationOfVehicles, ApplicationForChangeRegistrationOfVehiclesVM>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                    d.Circumstances = ctx.Mapper.Map<ApplicationForChangeRegistrationOfVehiclesDataVM>(s.ApplicationForChangeRegistrationOfVehiclesData);
                    d.Circumstances.NotaryIdentityNumber = s.NotaryIdentityNumber;
                });

            CreateMap<ApplicationForChangeRegistrationOfVehiclesData, ApplicationForChangeRegistrationOfVehiclesDataVM>()
               .AfterMap((s, d, ctx) =>
               {
                   if (s.VehicleChangeOwnershipData != null)
                   {
                       if (s.VehicleOwnershipChangeType == VehicleOwnershipChangeType.ChangeOwnership)
                       {
                           d.ChangeRegistrationWithPersonOrEntity = new VehicleRegistrationChangeVM();
                           d.ChangeRegistrationWithPersonOrEntity.VehicleRegistrationData = new List<VehicleRegistrationDataVM>();

                           foreach (var ownershipData in s.VehicleChangeOwnershipData)
                           {
                               var vehicleRegistrationData = ctx.Mapper.Map<VehicleRegistrationDataVM>(ownershipData.VehicleRegistrationData);
                               vehicleRegistrationData.AvailableDocumentForPaidAnnualTax = ownershipData.AvailableDocumentForPaidAnnualTax;
                               d.ChangeRegistrationWithPersonOrEntity.VehicleRegistrationData.Add(vehicleRegistrationData);
                               d.ChangeRegistrationWithPersonOrEntity.CurrentOwners = ownershipData.CurrentOwnersCollection.Select(x => ctx.Mapper.Map<VehicleOwnerDataVM>(x)).ToList();
                               d.ChangeRegistrationWithPersonOrEntity.NewOwners = ownershipData.NewOwnersCollection.Select(x => ctx.Mapper.Map<VehicleBuyerDataVM>(x)).ToList();
                           }
                       }
                       else if (s.VehicleOwnershipChangeType == VehicleOwnershipChangeType.Barter && s.VehicleChangeOwnershipData.Count == 2)
                       {
                           d.ChangeRegistrationWithBarterVM = new VehicleRegistrationChangeWithBarterVM();
                           d.ChangeRegistrationWithBarterVM.FirstVehicleData = ctx.Mapper.Map<VehicleRegistrationDataVM>(s.VehicleChangeOwnershipData[0].VehicleRegistrationData);
                           d.ChangeRegistrationWithBarterVM.FirstVehicleData.AvailableDocumentForPaidAnnualTax = s.VehicleChangeOwnershipData[0].AvailableDocumentForPaidAnnualTax;
                           d.ChangeRegistrationWithBarterVM.SecondVehicleData = ctx.Mapper.Map<VehicleRegistrationDataVM>(s.VehicleChangeOwnershipData[1].VehicleRegistrationData);
                           d.ChangeRegistrationWithBarterVM.SecondVehicleData.AvailableDocumentForPaidAnnualTax = s.VehicleChangeOwnershipData[1].AvailableDocumentForPaidAnnualTax;

                           d.ChangeRegistrationWithBarterVM.FirstVehicleOwners = s.VehicleChangeOwnershipData[0].CurrentOwnersCollection.Select(x => ctx.Mapper.Map<VehicleOwnerDataVM>(x)).ToList();
                           d.ChangeRegistrationWithBarterVM.SecondVehicleOwners = s.VehicleChangeOwnershipData[1].CurrentOwnersCollection.Select(x => ctx.Mapper.Map<VehicleOwnerDataVM>(x)).ToList();
                       }
                   }
               });

            CreateMap<VehicleChangeOwnershipDataNewOwner, VehicleBuyerDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.PersonEntityData = new PersonEntityDataVM();

                    if (s.Item is Domain.Models.PersonData person)
                    {
                        d.PersonEntityData.Person = ctx.Mapper.Map<Domain.Models.PersonData, PersonDataVM>(person);
                        d.PersonEntityData.SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Person;
                    }
                    else if (s.Item is EntityData entity)
                    {
                        d.PersonEntityData.Entity = ctx.Mapper.Map<EntityData, EntityDataVM>(entity);

                        if (s.IsFarmer.HasValue && s.IsFarmer.Value)
                            d.PersonEntityData.SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Farmer;
                        else
                            d.PersonEntityData.SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Entity;

                        d.PersonEntityData.IsFarmer = s.IsFarmer;
                    }
                });

            CreateMap<VehicleChangeOwnershipDataCurrentOwner, VehicleOwnerDataVM>()
                .ForMember(d => d.VehicleOwnerAdditionalCircumstances, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   d.PersonEntityData = new PersonEntityDataVM();

                   if (s.Item is Domain.Models.PersonData person)
                   {
                       d.PersonEntityData.Person = ctx.Mapper.Map<Domain.Models.PersonData, PersonDataVM>(person);
                       d.PersonEntityData.SelectedChoiceType = PersonEntityChoiceType.Person;
                   }
                   else if (s.Item is EntityData entity)
                   {
                       d.PersonEntityData.Entity = ctx.Mapper.Map<EntityData, EntityDataVM>(entity);

                       if (s.IsFarmer.HasValue && s.IsFarmer.Value)
                           d.PersonEntityData.SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Farmer;
                       else
                           d.PersonEntityData.SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Entity;

                       d.PersonEntityData.IsFarmer = s.IsFarmer;
                   }

                   if (s.VehicleOwnerAdditionalCircumstances == VehicleOwnerAdditionalCircumstances.SoldBySyndic)
                       d.IsSoldBySyndic = true;
                   else
                       d.VehicleOwnerAdditionalCircumstances = s.VehicleOwnerAdditionalCircumstances;
               });

            CreateMap<VehicleChangeOwnershipDataNewOwner, VehicleOwnerDataVM>()
                .ForMember(d => d.VehicleOwnerAdditionalCircumstances, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    d.PersonEntityData = new PersonEntityDataVM();

                    if (s.Item is Domain.Models.PersonData person)
                    {
                        d.PersonEntityData.Person = ctx.Mapper.Map<Domain.Models.PersonData, PersonDataVM>(person);
                        d.PersonEntityData.SelectedChoiceType = PersonEntityChoiceType.Person;
                    }
                    else if (s.Item is EntityData entity)
                    {
                        d.PersonEntityData.Entity = ctx.Mapper.Map<EntityData, EntityDataVM>(entity);

                        if (s.IsFarmer.HasValue && s.IsFarmer.Value)
                            d.PersonEntityData.SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Farmer;
                        else
                            d.PersonEntityData.SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Entity;

                        d.PersonEntityData.IsFarmer = s.IsFarmer;
                    }

                    if (s.VehicleOwnerAdditionalCircumstances == VehicleOwnerAdditionalCircumstances.SoldBySyndic)
                        d.IsSoldBySyndic = true;
                    else
                        d.VehicleOwnerAdditionalCircumstances = s.VehicleOwnerAdditionalCircumstances;
                });

            #endregion

            #region ApplicationForInitialVehicleRegistration

            CreateMap<ApplicationForInitialVehicleRegistration, ApplicationForInitialVehicleRegistrationVM>()
             .ForMember(d => d.Declarations, (config) => config.Ignore())
             .AfterMap((s, d, ctx) =>
             {
                 MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                 d.Circumstances = ctx.Mapper.Map<ApplicationForInitialVehicleRegistrationDataVM>(s.ApplicationForInitialVehicleRegistrationData);
             });

            CreateMap<InitialVehicleRegistrationOwnerData, InitialVehicleRegistrationUserOrOwnerOfSRMPSVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Item is PersonIdentifier personIdentifier)
                    {
                        d.Type = PersonEntityChoiceType.Person;
                        d.PersonIdentifier = personIdentifier;
                    }
                    else if (s.Item is string item)
                    {
                        d.Type = PersonEntityChoiceType.Entity;
                        d.EntityIdentifier = item;
                    }
                    else
                    {
                        d.Type = null;
                        d.PersonIdentifier = null;
                        d.EntityIdentifier = null;
                    }
                });

            CreateMap<InitialVehicleRegistrationOwnerData, InitialVehicleRegistrationOwnerDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Item is PersonIdentifier personIdentifier)
                    {
                        d.Type = PersonEntityChoiceType.Person;
                        d.PersonIdentifier = personIdentifier;
                    }
                    else if (s.Item is string item)
                    {
                        d.Type = PersonEntityChoiceType.Entity;
                        d.EntityIdentifier = item;
                    }
                    else
                    {
                        d.Type = null;
                        d.PersonIdentifier = null;
                        d.EntityIdentifier = null;
                    }
                });

            CreateMap<ApplicationForInitialVehicleRegistrationDataOwnersCollection, ApplicationForInitialVehicleRegistrationDataOwnersCollectionVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.InitialVehicleRegistrationOwnerData != null && s.InitialVehicleRegistrationOwnerData.Count > 0)
                    {
                        d.Items = s.InitialVehicleRegistrationOwnerData.Select(el => ctx.Mapper.Map<InitialVehicleRegistrationOwnerDataVM>(el)).ToList();
                    }
                });

            CreateMap<ApplicationForInitialVehicleRegistrationData, ApplicationForInitialVehicleRegistrationDataVM>();
            //.AfterMap((s, d, ctx) =>
            //{
            //});

            #endregion

            #region ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigits

            CreateMap<ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData, ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsDataVM>();

            CreateMap<ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigits, ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsVM>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                    d.Circumstances = ctx.Mapper.Map<ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsDataVM>(s.ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData);
                });

            #endregion

            #region NotificationForTemporaryRegistrationPlates

            CreateMap<NotificationForTemporaryRegistrationPlates, NotificationForTemporaryRegistrationPlatesVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURIVM>(s.AISCaseURI);
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicDataVM>(s.ElectronicServiceProviderBasicData);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicantVM>(s.ElectronicServiceApplicant);

                    MapperHelper.MapDocumentWithOfficialDomainToViewModel<NotificationForTemporaryRegistrationPlatesOfficial>(s, d);
                });

            #endregion

            #region ReportForChangingOwnershipV2VM

            CreateMap<ReportForChangingOwnershipV2, ReportForChangingOwnershipV2VM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURIVM>(s.AISCaseURI);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicantVM>(s.ElectronicServiceApplicant);

                    if (s.VehicleData != null && s.VehicleData.Count > 0)
                    {
                        if (IsChangingOwnershipWithBarter(s.VehicleData))
                        {
                            d.VehicleRegistrationChangeWithBarter = new ReportForChangingOwnershipV2ChangeWithBarterVM();

                            d.VehicleRegistrationChangeWithBarter.FirstVehicleData = ctx.Mapper.Map<ReportForChangingOwnershipV2VehicleDataVM>(s.VehicleData[0]);
                            d.VehicleRegistrationChangeWithBarter.SecondVehicleData = ctx.Mapper.Map<ReportForChangingOwnershipV2VehicleDataVM>(s.VehicleData[1]);

                            d.VehicleRegistrationChangeWithBarter.FirstVehicleOwners = s.VehicleData[0].OldOwners.Items.Select(x => ctx.Mapper.Map<PersonEntityDataVM>(x)).ToList();
                            d.VehicleRegistrationChangeWithBarter.SecondVehicleOwners = s.VehicleData[1].OldOwners.Items.Select(x => ctx.Mapper.Map<PersonEntityDataVM>(x)).ToList();
                        }
                        else
                        {
                            d.VehicleRegistrationChange = new List<ReportForChangingOwnershipV2ChangeVM>();

                            foreach (var vehicleData in s.VehicleData)
                            {
                                d.VehicleRegistrationChange.Add(new ReportForChangingOwnershipV2ChangeVM()
                                {
                                    VehicleRegistrationData = ctx.Mapper.Map<ReportForChangingOwnershipV2VehicleDataVM>(vehicleData),
                                    CurrentOwners = vehicleData?.OldOwners?.Items.Select(x => ctx.Mapper.Map<PersonEntityDataVM>(x)).ToList(),
                                    NewOwners = vehicleData?.NewOwners?.Items.Select(x => ctx.Mapper.Map<PersonEntityDataVM>(x)).ToList()
                                });
                            }
                        }
                    }
                });

            CreateMap<ReportForChangingOwnershipV2VehicleData, ReportForChangingOwnershipV2VehicleDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.RegistrationData = s.VehicleRegistrationData;
                });

            CreateMap<object, PersonEntityDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s is Domain.Models.PersonData personData)
                    {
                        d.Person = ctx.Mapper.Map<Domain.Models.PersonData, PersonDataVM>(personData);
                        d.SelectedChoiceType = PersonEntityChoiceType.Person;
                    }
                    else if (s is EntityData entityData)
                    {
                        d.Entity = ctx.Mapper.Map<EntityData, EntityDataVM>(entityData);
                        d.SelectedChoiceType = PersonEntityChoiceType.Entity;
                    }
                });

            #endregion
        }

        private void CreateViewToDomainModelMap()
        {
            CreateMap<VehicleDataVM, VehicleData>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.VehicleData != null)
                        d.Item = ctx.Mapper.Map<VehicleDataItem>(s.VehicleData);

                    if (s.VehicleDataCollection != null)
                    {
                        d.Item = new VehicleDataItemCollection() { Items = new List<VehicleDataItem>() };

                        foreach (VehicleDataItemVM item in s.VehicleDataCollection)
                        {
                            ((VehicleDataItemCollection)d.Item).Items.Add(ctx.Mapper.Map<VehicleDataItem>(item));
                        }
                    }
                });

            CreateMap<VehicleDataItemVM, VehicleDataItem>();

            CreateMap<VehicleOwnerInformationItemVM, VehicleOwnerInformationItem>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Owner != null && s.Owner.SelectedChoiceType == PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Entity)
                        d.Item = s.Owner.ItemEntityBasicData;

                    if (s.Owner != null && s.Owner.SelectedChoiceType == PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person)
                        d.Item = ctx.Mapper.Map<PersonBasicData>(s.Owner.ItemPersonBasicData);

                    if (s.Owner != null && s.Owner.SelectedChoiceType == PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.ForeignPerson)
                        d.Item = s.Owner.ItemForeignCitizenBasicData;
                });

            CreateMap<ApplicationForIssuingDocumentofVehicleOwnershipVM, ApplicationForIssuingDocumentofVehicleOwnership>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                    d.ApplicationForIssuingDocumentofVehicleOwnershipData = ctx.Mapper.Map<ApplicationForIssuingDocumentofVehicleOwnershipData>(s.Circumstances);
                });

            CreateMap<ApplicationForIssuingDocumentofVehicleOwnershipDataVM, ApplicationForIssuingDocumentofVehicleOwnershipData>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.RegistrationAndMake != null)
                    {
                        d.Item = new ApplicationForIssuingDocumentofVehicleOwnershipDataRegistrationAndMake()
                        {
                            MakeModel = s.RegistrationAndMake.MakeModel,
                            RegistrationNumber = s.RegistrationAndMake.RegistrationNumber
                        };
                    }
                });

            CreateMap<ApplicationForIssuingDocumentofVehicleOwnershipDataRegistrationAndMakeVM, ApplicationForIssuingDocumentofVehicleOwnershipDataRegistrationAndMake>();

            CreateMap<OwnerVM, VehicleDataRequestOwnersCollectionOwners>()
            .AfterMap((s, d, ctx) =>
            {
                if (s.Type == null)
                {
                    d.Item = null;
                }
                else if (s.Type == PersonEntityChoiceType.Person)
                {
                    d.Item = s.PersonIdentifier;
                }
                else
                {
                    d.Item = s.EntityIdentifier;
                }
            });

            CreateMap<OwnersCollectionVM, VehicleDataRequestOwnersCollection>()
               .AfterMap((s, d, ctx) =>
               {
                   if (s.Owners != null && s.Owners.Any())
                   {
                       d.Owners = s.Owners.Select(el => ctx.Mapper.Map<VehicleDataRequestOwnersCollectionOwners>(el)).ToList();
                   }
               });

            CreateMap<VehicleDataRequestVM, VehicleDataRequest>()
                .AfterMap((s, d, ctx) =>
                {
                    d.OwnersCollection = ctx.Mapper.Map<VehicleDataRequestOwnersCollection>(s.OwnersCollection);
                });

            CreateMap<ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataVM, ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData>()
                .ForMember(d => d.AuthorizedPersons, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    if (s.AuthorizedPersons != null && s.AuthorizedPersons.Items != null && s.AuthorizedPersons.Items.Count > 0)
                    {
                        d.AuthorizedPersons = s.AuthorizedPersons.Items;
                    }
                });

            #region ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriver

            CreateMap<ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverVM, ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriver>()
       .ForMember(d => d.Declarations, (config) => config.Ignore())
       .AfterMap((s, d, ctx) =>
       {
           MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
           d.ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData = ctx.Mapper.Map<ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData>(s.Circumstances);
       });

            CreateMap<ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverDataVM, ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData>();

            #endregion

            #region RequestForDecisionForDeal

            CreateMap<RequestForDecisionForDealVM, RequestForDecisionForDeal>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                   d.RequestForDecisionForDealData = ctx.Mapper.Map<RequestForDecisionForDealData>(s.Circumstances);
                   d.NotaryIdentityNumber = s.Circumstances.NotaryIdentityNumber;
               });

            CreateMap<RequestForDecisionForDealDataVM, RequestForDecisionForDealData>()
                .AfterMap((s, d, ctx) =>
                {
                    d.VehicleRegistrationData = ctx.Mapper.Map<VehicleRegistrationDataVM, VehicleRegistrationData>(s.VehicleRegistrationData);
                    if (s.BuyersCollection != null && s.BuyersCollection.Count > 0)
                        s.BuyersCollection[0].ValidateEmail = true;

                });

            CreateMap<VehicleOwnerDataVM, RequestForDecisionForDealDataOwner>()
               .AfterMap((s, d, ctx) =>
               {
                   if (s.PersonEntityData.SelectedChoiceType == PersonEntityChoiceType.Person)
                   {
                       d.Item = ctx.Mapper.Map<PersonDataVM, Domain.Models.PersonData>(s.PersonEntityData.Person);
                   }
                   if (s.PersonEntityData.SelectedChoiceType == PersonEntityChoiceType.Entity)
                   {
                       d.Item = ctx.Mapper.Map<EntityDataVM, EntityData>(s.PersonEntityData.Entity);
                   }
               });

            CreateMap<VehicleBuyerDataVM, RequestForDecisionForDealDataBuyer>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.PersonEntityData.SelectedChoiceType == PersonEntityChoiceType.Person)
                    {
                        d.Item = ctx.Mapper.Map<PersonDataVM, Domain.Models.PersonData>(s.PersonEntityData.Person);
                    }
                    if (s.PersonEntityData.SelectedChoiceType == PersonEntityChoiceType.Entity)
                    {
                        d.Item = ctx.Mapper.Map<EntityDataVM, EntityData>(s.PersonEntityData.Entity);
                    }
                });

            CreateMap<VehicleRegistrationDataVM, VehicleRegistrationData>();

            CreateMap<EntityDataVM, EntityData>();

            CreateMap<PersonDataVM, Domain.Models.PersonData>()
                .AfterMap((s, d, ctx) =>
                {
                    d.PersonBasicData = new PersonBasicData()
                    {
                        Identifier = new PersonIdentifier()
                        {
                            Item = s.Identifier.Item,
                            ItemElementName = s.Identifier.ItemElementName ?? PersonIdentifierChoiceType.EGN
                        }
                    };
                    d.PersonBasicData.IdentityDocument = !string.IsNullOrEmpty(s.IdentityNumber)
                        ? new IdentityDocumentBasicData() { IdentityNumber = s.IdentityNumber }
                        : null;
                    d.PersonBasicData.Names = s.Names != null ? s.Names : null;
                });

            #endregion

            #region RequestForApplicationForIssuingDuplicateDrivingLicense

            CreateMap<RequestForApplicationForIssuingDuplicateDrivingLicenseVM, RequestForApplicationForIssuingDuplicateDrivingLicense>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
               });

            #endregion

            #region CertificateOfVehicleOwnership

            CreateMap<CertificateOfVehicleOwnershipVM, CertificateOfVehicleOwnership>()
               .ForMember(d => d.VehicleOwnerInformationCollection, (config) => config.Ignore())

               .AfterMap((s, d, ctx) =>
               {
                   d.AISCaseURI = ctx.Mapper.Map<AISCaseURI>(s.AISCaseURI);
                   d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicData>(s.ElectronicServiceProviderBasicData);
                   d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicant>(s.ElectronicServiceApplicant);

                   if (s.VehicleOwnerInformationCollection != null && s.VehicleOwnerInformationCollection.Count > 0)
                   {
                       d.VehicleOwnerInformationCollection = new VehicleOwnerInformationCollection();
                       d.VehicleOwnerInformationCollection.Items = new List<VehicleOwnerInformationItem>();

                       foreach (VehicleOwnerInformationItemVM item in s.VehicleOwnerInformationCollection)
                       {
                           d.VehicleOwnerInformationCollection.Items.Add(ctx.Mapper.Map<VehicleOwnerInformationItem>(item));
                       }
                   }
                   else
                   {
                       d.VehicleOwnerInformationCollection = null;
                   }

                   if (s.VehicleData != null)
                       d.VehicleData = ctx.Mapper.Map<VehicleData>(s.VehicleData);

                   MapperHelper.MapDocumentWithOfficialViewModelToDomain<CertificateOfVehicleOwnershipOfficial>(s, d);
               });


            #endregion

            #region CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriver

            CreateMap<CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverVM, CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriver>()
                .AfterMap((s, d, ctx) =>
                {
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURI>(s.AISCaseURI);
                    d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicData>(s.ElectronicServiceProviderBasicData);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicant>(s.ElectronicServiceApplicant);

                    MapperHelper.MapDocumentWithOfficialViewModelToDomain<CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverOfficial>(s, d);
                });

            #endregion

            #region ReportForChangingOwnership

            CreateMap<ReportForChangingOwnershipVM, ReportForChangingOwnership>();

            CreateMap<ReportForChangingOwnershipOldOwnersDataOldOwnersVM, ReportForChangingOwnershipOldOwnersDataOldOwners>()
               .AfterMap((s, d, ctx) =>
               {
                   if (s.PersonEntityData.SelectedChoiceType == PersonEntityChoiceType.Person)
                   {
                       d.Item = ctx.Mapper.Map<PersonDataVM, Domain.Models.PersonData>(s.PersonEntityData.Person);
                   }
                   if (s.PersonEntityData.SelectedChoiceType == PersonEntityChoiceType.Entity)
                   {
                       d.Item = ctx.Mapper.Map<EntityDataVM, EntityData>(s.PersonEntityData.Entity);
                   }
               });

            CreateMap<ReportForChangingOwnershipNewOwnersDataNewOwnersVM, ReportForChangingOwnershipNewOwnersDataNewOwners>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.PersonEntityData.SelectedChoiceType == PersonEntityChoiceType.Person)
                    {
                        d.Item = ctx.Mapper.Map<PersonDataVM, Domain.Models.PersonData>(s.PersonEntityData.Person);
                    }
                    if (s.PersonEntityData.SelectedChoiceType == PersonEntityChoiceType.Entity)
                    {
                        d.Item = ctx.Mapper.Map<EntityDataVM, EntityData>(s.PersonEntityData.Entity);
                    }
                });

            CreateMap<ReportForChangingOwnershipOldOwnersDataVM, ReportForChangingOwnershipOldOwnersData>();
            CreateMap<ReportForChangingOwnershipNewOwnersDataVM, ReportForChangingOwnershipNewOwnersData>();

            #endregion

            #region DataForPrintSRMPS

            CreateMap<PersonEntityDataVM, DataForPrintSRMPSDataNewOwner>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.SelectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Person)
                        d.Item = ctx.Mapper.Map<PersonDataVM, Domain.Models.PersonData>(s.Person);

                    if (s.SelectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Entity
                    || s.SelectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Farmer)
                        d.Item = ctx.Mapper.Map<EntityDataVM, EntityData>(s.Entity);
                });

            CreateMap<PersonEntityDataVM, DataForPrintSRMPSDataHolderData>()
              .AfterMap((s, d, ctx) =>
              {
                  if (s.SelectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Person)
                      d.Item = ctx.Mapper.Map<PersonDataVM, Domain.Models.PersonData>(s.Person);

                  if (s.SelectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Entity
                  || s.SelectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Farmer)
                      d.Item = ctx.Mapper.Map<EntityDataVM, EntityData>(s.Entity);
              });

            CreateMap<PersonEntityDataVM, DataForPrintSRMPSDataUserData>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.SelectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Person)
                        d.Item = ctx.Mapper.Map<PersonDataVM, Domain.Models.PersonData>(s.Person);

                    if (s.SelectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Entity 
                    || s.SelectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Farmer)
                        d.Item = ctx.Mapper.Map<EntityDataVM, EntityData>(s.Entity);
                });

            CreateMap<DataForPrintSRMPSDataVM, DataForPrintSRMPSData>()
                .AfterMap((s, d, ctx) =>
                {
                    d.HolderData = ctx.Mapper.Map<PersonEntityDataVM, DataForPrintSRMPSDataHolderData>(s.HolderData);

                    if (s.UserData != null
                        && ((s.UserData.SelectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Person && s.UserData.Person != null && s.UserData.Person.Identifier != null && !string.IsNullOrEmpty(s.UserData.Person.Identifier.Item)) ||
                        (s.UserData.SelectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Entity || s.UserData.SelectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Farmer) && s.UserData.Entity != null && !string.IsNullOrEmpty(s.UserData.Entity.Identifier)))
                    {
                        d.UserData = ctx.Mapper.Map<PersonEntityDataVM, DataForPrintSRMPSDataUserData>(s.UserData);
                    }
                    else
                        d.UserData = null;
                });

            CreateMap<DataForPrintSRMPSVM, DataForPrintSRMPS>()
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                    d.DataForPrintSRMPSData = ctx.Mapper.Map<DataForPrintSRMPSDataVM, DataForPrintSRMPSData>(s.Circumstances);
                    d.IssuingPoliceDepartment = s.Circumstances.IssuingPoliceDepartment;
                });

            #endregion

            #region ApplicationForIssuingOfControlCouponForDriverWithNoPenalties

            CreateMap<ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesDataVM, ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesData>();

            CreateMap<ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesVM, ApplicationForIssuingOfControlCouponForDriverWithNoPenalties>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                   d.ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesData = ctx.Mapper.Map<ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesData>(s.Circumstances);
               });

            #endregion

            #region ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCoupon

            CreateMap<ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponDataVM, ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponData>();

            CreateMap<ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponVM, ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCoupon>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                   d.ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponData = ctx.Mapper.Map<ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponData>(s.Circumstances);
               });

            #endregion

            #region ApplicationForTerminationOfVehicleRegistration

            CreateMap<ApplicationForTerminationOfVehicleRegistrationVM, ApplicationForTerminationOfVehicleRegistration>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                   d.VehicleDataRequest = ctx.Mapper.Map<VehicleDataRequest>(s.Circumstances);
               });

            #endregion

            #region ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificate

            CreateMap<ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateVM, ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificate>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                   d.VehicleDataRequest = ctx.Mapper.Map<VehicleDataRequest>(s.Circumstances);
               });

            #endregion

            #region ApplicationForIssuingTempraryTraficPermitForVehicle

            CreateMap<ApplicationForIssuingTempraryTraficPermitForVehicleVM, ApplicationForIssuingTempraryTraficPermitForVehicle>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                   d.VehicleDataRequest = ctx.Mapper.Map<VehicleDataRequest>(s.Circumstances);
               });

            #endregion

            #region ApplicationForTemporaryTakingOffRoadOfVehicle

            CreateMap<ApplicationForTemporaryTakingOffRoadOfVehicleVM, ApplicationForTemporaryTakingOffRoadOfVehicle>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                   d.VehicleDataRequest = ctx.Mapper.Map<VehicleDataRequest>(s.Circumstances);
               });

            #endregion

            #region ApplicationForCommissioningTemporarilySuspendedVehicle

            CreateMap<ApplicationForCommissioningTemporarilySuspendedVehicleVM, ApplicationForCommissioningTemporarilySuspendedVehicle>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                   d.VehicleDataRequest = ctx.Mapper.Map<VehicleDataRequest>(s.Circumstances);
               });

            #endregion

            #region ReportForVehicle

            CreateMap<ReportForVehicleRPSSVehicleDataOwnersVM, ReportForVehicleRPSSVehicleDataOwners>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.PersonData != null)
                    {
                        d.Item = ctx.Mapper.Map<Domain.Models.PersonData>(s.PersonData);
                    }
                    else if (s.EntityData != null)
                    {
                        d.Item = ctx.Mapper.Map<EntityData>(s.EntityData);
                    }
                });

            CreateMap<ReportForVehicleRPSSVehicleDataVM, ReportForVehicleRPSSVehicleData>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Owners != null && s.Owners.Count > 0)
                    {
                        d.Owners = s.Owners.Select(el => ctx.Mapper.Map<ReportForVehicleRPSSVehicleDataOwners>(el)).ToList();
                    }
                });

            CreateMap<ReportForVehicleVM, ReportForVehicle>()
                .ForMember(d => d.Owners, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    d.RPSSVehicleData = ctx.Mapper.Map<ReportForVehicleRPSSVehicleData>(s.RPSSVehicleData);

                    if (s.Owners != null)
                    {
                        d.Owners = new ReportForVehicleOwners();
                        d.Owners.Items = new List<object>();

                        if (s.Owners.EntityDataItems != null && s.Owners.EntityDataItems.Count > 0)
                            d.Owners.Items.AddRange(s.Owners.EntityDataItems);

                        if (s.Owners.PersonDataItems != null && s.Owners.PersonDataItems.Count > 0)
                            d.Owners.Items.AddRange(s.Owners.PersonDataItems);
                    }
                });

            #endregion

            #region DeclarationForLostSRPPS

            CreateMap<DeclarationForLostSRPPSVM, DeclarationForLostSRPPS>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                   d.Data = s.Circumstances;
               });

            #endregion

            #region ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehicles

            CreateMap<ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesVM, ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehicles>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                    d.ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesData = ctx.Mapper.Map<ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesData>(s.Circumstances);
                });

            CreateMap<ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesDataVM, ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesData>();
            #endregion

            #region ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchants

            CreateMap<ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsVM, ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchants>()
             .ForMember(d => d.Declarations, (config) => config.Ignore())
             .AfterMap((s, d, ctx) =>
             {
                 MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                 d.ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData = ctx.Mapper.Map<ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData>(s.Circumstances);
             });

            #endregion

            #region ApplicationForChangeRegistrationOfVehicles

            CreateMap<ApplicationForChangeRegistrationOfVehiclesVM, ApplicationForChangeRegistrationOfVehicles>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                    d.ApplicationForChangeRegistrationOfVehiclesData = ctx.Mapper.Map<ApplicationForChangeRegistrationOfVehiclesData>(s.Circumstances);
                    d.NotaryIdentityNumber = s.Circumstances.NotaryIdentityNumber;
                });

            CreateMap<ApplicationForChangeRegistrationOfVehiclesDataVM, ApplicationForChangeRegistrationOfVehiclesData>()
               .AfterMap((s, d, ctx) =>
               {
                   d.VehicleChangeOwnershipData = new List<VehicleChangeOwnershipData>();

                   if (s.VehicleOwnershipChangeType == VehicleOwnershipChangeType.ChangeOwnership)
                   {
                       //Промяна на собственост на регистрирани ППС, собственост на едно ЮЛ или ФЛ
                       foreach (var vehicleData in s.ChangeRegistrationWithPersonOrEntity.VehicleRegistrationData)
                       {
                           var vehicleChangeOwnership = new VehicleChangeOwnershipData();
                           vehicleChangeOwnership.VehicleRegistrationData = ctx.Mapper.Map<VehicleRegistrationData>(vehicleData);
                           vehicleChangeOwnership.AvailableDocumentForPaidAnnualTax = vehicleData.AvailableDocumentForPaidAnnualTax;
                           vehicleChangeOwnership.CurrentOwnersCollection = s.ChangeRegistrationWithPersonOrEntity.CurrentOwners.Select(el => ctx.Mapper.Map<VehicleChangeOwnershipDataCurrentOwner>(el)).ToList();
                           vehicleChangeOwnership.NewOwnersCollection = s.ChangeRegistrationWithPersonOrEntity.NewOwners.Select(el => ctx.Mapper.Map<VehicleChangeOwnershipDataNewOwner>(el)).ToList();

                           d.VehicleChangeOwnershipData.Add(vehicleChangeOwnership);
                       }
                   }
                   else if (s.VehicleOwnershipChangeType == VehicleOwnershipChangeType.Barter)
                   {
                       //Замяна на ППС между двама собственици
                       var firstVehicleChangeOwnership = new VehicleChangeOwnershipData();
                       firstVehicleChangeOwnership.VehicleRegistrationData = ctx.Mapper.Map<VehicleRegistrationData>(s.ChangeRegistrationWithBarterVM.FirstVehicleData);
                       firstVehicleChangeOwnership.AvailableDocumentForPaidAnnualTax = s.ChangeRegistrationWithBarterVM.FirstVehicleData.AvailableDocumentForPaidAnnualTax;
                       firstVehicleChangeOwnership.CurrentOwnersCollection = s.ChangeRegistrationWithBarterVM.FirstVehicleOwners.Select(el => ctx.Mapper.Map<VehicleChangeOwnershipDataCurrentOwner>(el)).ToList();
                       firstVehicleChangeOwnership.NewOwnersCollection = s.ChangeRegistrationWithBarterVM.SecondVehicleOwners.Select(el => ctx.Mapper.Map<VehicleChangeOwnershipDataNewOwner>(el)).ToList();

                       d.VehicleChangeOwnershipData.Add(firstVehicleChangeOwnership);

                       var secondVehicleChangeOwnership = new VehicleChangeOwnershipData();
                       secondVehicleChangeOwnership.VehicleRegistrationData = ctx.Mapper.Map<VehicleRegistrationData>(s.ChangeRegistrationWithBarterVM.SecondVehicleData);
                       secondVehicleChangeOwnership.AvailableDocumentForPaidAnnualTax = s.ChangeRegistrationWithBarterVM.SecondVehicleData.AvailableDocumentForPaidAnnualTax;
                       secondVehicleChangeOwnership.CurrentOwnersCollection = s.ChangeRegistrationWithBarterVM.SecondVehicleOwners.Select(el => ctx.Mapper.Map<VehicleChangeOwnershipDataCurrentOwner>(el)).ToList();
                       secondVehicleChangeOwnership.NewOwnersCollection = s.ChangeRegistrationWithBarterVM.FirstVehicleOwners.Select(el => ctx.Mapper.Map<VehicleChangeOwnershipDataNewOwner>(el)).ToList();

                       d.VehicleChangeOwnershipData.Add(secondVehicleChangeOwnership);
                   }
               });

            CreateMap<VehicleOwnerDataVM, VehicleChangeOwnershipDataCurrentOwner>()
                .ForMember(d => d.VehicleOwnerAdditionalCircumstances, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    if (s.PersonEntityData.SelectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Person)
                    {
                        d.Item = ctx.Mapper.Map<PersonDataVM, Domain.Models.PersonData>(s.PersonEntityData.Person);
                    }
                    else
                    {
                        d.Item = ctx.Mapper.Map<EntityDataVM, EntityData>(s.PersonEntityData.Entity);
                    }

                    d.IsFarmer = s.PersonEntityData.IsFarmer;

                    if (s.IsSoldBySyndic.HasValue && s.IsSoldBySyndic.Value)
                        d.VehicleOwnerAdditionalCircumstances = VehicleOwnerAdditionalCircumstances.SoldBySyndic;
                    else
                        d.VehicleOwnerAdditionalCircumstances = s.VehicleOwnerAdditionalCircumstances;

                });

            CreateMap<VehicleOwnerDataVM, VehicleChangeOwnershipDataNewOwner>()
                .ForMember(d => d.VehicleOwnerAdditionalCircumstances, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {

                    if (s.PersonEntityData.SelectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Person)
                    {
                        d.Item = ctx.Mapper.Map<PersonDataVM, Domain.Models.PersonData>(s.PersonEntityData.Person);
                    }
                    else
                    {
                        d.Item = ctx.Mapper.Map<EntityDataVM, EntityData>(s.PersonEntityData.Entity);
                    }

                    d.IsFarmer = s.PersonEntityData.IsFarmer;

                    if (s.IsSoldBySyndic.HasValue && s.IsSoldBySyndic.Value)
                        d.VehicleOwnerAdditionalCircumstances = VehicleOwnerAdditionalCircumstances.SoldBySyndic;
                    else
                        d.VehicleOwnerAdditionalCircumstances = s.VehicleOwnerAdditionalCircumstances;
                });

            CreateMap<VehicleBuyerDataVM, VehicleChangeOwnershipDataNewOwner>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.PersonEntityData.SelectedPersonEntityFarmerChoiceType == PersonEntityFarmerChoiceType.Person)
                    {
                        d.Item = ctx.Mapper.Map<PersonDataVM, Domain.Models.PersonData>(s.PersonEntityData.Person);
                    }
                    else
                    {
                        d.Item = ctx.Mapper.Map<EntityDataVM, EntityData>(s.PersonEntityData.Entity);
                    }
                    d.IsFarmer = s.PersonEntityData.IsFarmer;
                });

            #endregion

            #region ApplicationForInitialVehicleRegistration

            CreateMap<ApplicationForInitialVehicleRegistrationVM, ApplicationForInitialVehicleRegistration>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                    d.ApplicationForInitialVehicleRegistrationData = ctx.Mapper.Map<ApplicationForInitialVehicleRegistrationData>(s.Circumstances);
                });

            CreateMap<InitialVehicleRegistrationUserOrOwnerOfSRMPSVM, InitialVehicleRegistrationOwnerData>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Type == null)
                    {
                        d.Item = null;
                    }
                    else if (s.Type == PersonEntityChoiceType.Person)
                    {
                        d.Item = s.PersonIdentifier;
                    }
                    else
                    {
                        d.Item = s.EntityIdentifier;
                    }
                });

            CreateMap<InitialVehicleRegistrationOwnerDataVM, InitialVehicleRegistrationOwnerData>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Type == null)
                    {
                        d.Item = null;
                    }
                    else if (s.Type == PersonEntityChoiceType.Person)
                    {
                        d.Item = s.PersonIdentifier;
                    }
                    else
                    {
                        d.Item = s.EntityIdentifier;
                    }
                });

            CreateMap<ApplicationForInitialVehicleRegistrationDataOwnersCollectionVM, ApplicationForInitialVehicleRegistrationDataOwnersCollection>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.Items != null && s.Items.Count > 0)
                    {
                        d.InitialVehicleRegistrationOwnerData = s.Items.Select(el => ctx.Mapper.Map<InitialVehicleRegistrationOwnerData>(el)).ToList();
                    }
                });

            CreateMap<ApplicationForInitialVehicleRegistrationDataVM, ApplicationForInitialVehicleRegistrationData>();

            #endregion

            #region ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigits

            CreateMap<ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsDataVM, ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData>();

            CreateMap<ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsVM, ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigits>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                    d.ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData = ctx.Mapper.Map<ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData>(s.Circumstances);
                });

            #endregion

            #region NotificationForTemporaryRegistrationPlates

            CreateMap<NotificationForTemporaryRegistrationPlatesVM, NotificationForTemporaryRegistrationPlates>()
               .AfterMap((s, d, ctx) =>
               {
                   d.AISCaseURI = ctx.Mapper.Map<AISCaseURI>(s.AISCaseURI);
                   d.ElectronicServiceProviderBasicData = ctx.Mapper.Map<ElectronicServiceProviderBasicData>(s.ElectronicServiceProviderBasicData);
                   d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicant>(s.ElectronicServiceApplicant);

                   MapperHelper.MapDocumentWithOfficialViewModelToDomain<NotificationForTemporaryRegistrationPlatesOfficial>(s, d);
               });


            #endregion

            #region ReportForChangingOwnershipV2VM

            CreateMap<ReportForChangingOwnershipV2VM, ReportForChangingOwnershipV2>()
                .AfterMap((s, d, ctx) =>
                {
                    d.AISCaseURI = ctx.Mapper.Map<AISCaseURI>(s.AISCaseURI);
                    d.ElectronicServiceApplicant = ctx.Mapper.Map<ElectronicServiceApplicant>(s.ElectronicServiceApplicant);

                    if (s.VehicleRegistrationChangeWithBarter != null)
                    {
                        d.VehicleData = new List<ReportForChangingOwnershipV2VehicleData>();

                        var firstVehicleData = (ReportForChangingOwnershipV2VehicleData)ctx.Mapper.Map<ReportForChangingOwnershipV2VehicleData>(s.VehicleRegistrationChangeWithBarter.FirstVehicleData);
                        var secondVehicleData = (ReportForChangingOwnershipV2VehicleData)ctx.Mapper.Map<ReportForChangingOwnershipV2VehicleData>(s.VehicleRegistrationChangeWithBarter.SecondVehicleOwners);

                        var firstVehicleOwners = new List<object>();
                        var secondVehicleOwners = new List<object>();

                        foreach (var firstVehicleOwner in s.VehicleRegistrationChangeWithBarter.FirstVehicleOwners)
                        {
                            if (firstVehicleOwner.SelectedChoiceType == PersonEntityChoiceType.Person)
                            {
                                firstVehicleOwners.Add(ctx.Mapper.Map<Domain.Models.PersonData>(firstVehicleOwner.Person));
                            }
                            else if (firstVehicleOwner.SelectedChoiceType == PersonEntityChoiceType.Entity)
                            {
                                firstVehicleOwners.Add(ctx.Mapper.Map<EntityData>(firstVehicleOwner.Entity));
                            }
                        }

                        foreach (var secondVehicleOwner in s.VehicleRegistrationChangeWithBarter.SecondVehicleOwners)
                        {
                            if (secondVehicleOwner.SelectedChoiceType == PersonEntityChoiceType.Person)
                            {
                                secondVehicleOwners.Add(ctx.Mapper.Map<Domain.Models.PersonData>(secondVehicleOwner.Person));
                            }
                            else if (secondVehicleOwner.SelectedChoiceType == PersonEntityChoiceType.Entity)
                            {
                                secondVehicleOwners.Add(ctx.Mapper.Map<EntityData>(secondVehicleOwner.Entity));
                            }
                        }

                        firstVehicleData.OldOwners = new ReportForChangingOwnershipV2VehicleDataOldOwners() { Items = firstVehicleOwners };
                        firstVehicleData.NewOwners = new ReportForChangingOwnershipV2VehicleDataNewOwners() { Items = secondVehicleOwners };
                        d.VehicleData.Add(firstVehicleData);

                        secondVehicleData.OldOwners = new ReportForChangingOwnershipV2VehicleDataOldOwners() { Items = secondVehicleOwners };
                        secondVehicleData.NewOwners = new ReportForChangingOwnershipV2VehicleDataNewOwners() { Items = firstVehicleOwners };
                        d.VehicleData.Add(secondVehicleData);
                    }
                    else if (s.VehicleRegistrationChange != null)
                    {
                        d.VehicleData = new List<ReportForChangingOwnershipV2VehicleData>();

                        foreach (var vehicleRegistrationChange in s.VehicleRegistrationChange)
                        {
                            var vehicleData = (ReportForChangingOwnershipV2VehicleData)ctx.Mapper.Map<ReportForChangingOwnershipV2VehicleData>(vehicleRegistrationChange.VehicleRegistrationData);
                            vehicleData.OldOwners = new ReportForChangingOwnershipV2VehicleDataOldOwners() { Items = new List<object>() };
                            vehicleData.NewOwners = new ReportForChangingOwnershipV2VehicleDataNewOwners() { Items = new List<object>() };

                            foreach (var currentOwner in vehicleRegistrationChange.CurrentOwners)
                            {
                                if (currentOwner.SelectedChoiceType == PersonEntityChoiceType.Person)
                                {
                                    var personData = ctx.Mapper.Map<Domain.Models.PersonData>(currentOwner.Person);
                                    vehicleData.OldOwners.Items.Add(personData);
                                }
                                else if (currentOwner.SelectedChoiceType == PersonEntityChoiceType.Entity)
                                {
                                    var entityData = ctx.Mapper.Map<EntityData>(currentOwner.Entity);
                                    vehicleData.OldOwners.Items.Add(entityData);
                                }
                            }

                            foreach (var newOwner in vehicleRegistrationChange.NewOwners)
                            {
                                if (newOwner.SelectedChoiceType == PersonEntityChoiceType.Person)
                                {
                                    var personData = ctx.Mapper.Map<Domain.Models.PersonData>(newOwner.Person);
                                    vehicleData.NewOwners.Items.Add(personData);
                                }
                                else if (newOwner.SelectedChoiceType == PersonEntityChoiceType.Entity)
                                {
                                    var entityData = ctx.Mapper.Map<EntityData>(newOwner.Entity);
                                    vehicleData.NewOwners.Items.Add(entityData);
                                }
                            }

                            d.VehicleData.Add(vehicleData);
                        }
                    }
                });

            CreateMap<ReportForChangingOwnershipV2VehicleDataVM, ReportForChangingOwnershipV2VehicleData>()
            .AfterMap((s, d, ctx) =>
            {
                d.VehicleRegistrationData = s.RegistrationData;
            });

            #endregion

            #region ApplicationForIssuingDriverLicense

            CreateMap<ApplicationForIssuingDriverLicenseVM, ApplicationForIssuingDriverLicense>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                    d.ApplicationForIssuingDriverLicenseData = ctx.Mapper.Map<ApplicationForIssuingDriverLicenseData>(s.Circumstances);
                    d.IdentificationPhotoAndSignature = ctx.Mapper.Map<IdentificationPhotoAndSignatureVM, IdentificationPhotoAndSignature>(s.IdentificationPhotoAndSignature);

                    if (s.Circumstances.PoliceDepartment != null)
                        d.IssuingPoliceDepartment = s.Circumstances.PoliceDepartment;

                    if (s.Circumstances.ReceivePlace.HasValue)
                        d.ReceivePlace = s.Circumstances.ReceivePlace.Value;
                });

            CreateMap<ApplicationForIssuingDriverLicenseDataVM, ApplicationForIssuingDriverLicenseData>()
                .AfterMap((s, d, ctx) =>
                {
                    d.Person = ctx.Mapper.Map<PersonDataExtendedVM, EAU.Documents.Domain.Models.PersonData>(s.Person);
                    //Ако данните за новия документ за задграничко пътуване са налице ги взимаме
                    if (s.TravelDocument != null && !ObjectUtility.IsEmpty(s.TravelDocument))
                        d.TravelDocument = ctx.Mapper.Map<TravelDocumentVM, TravelDocument>(s.TravelDocument);

                    d.ForeignCitizenship = ctx.Mapper.Map<CitizenshipVM, Citizenship>(s.Citizenship);
                });

            #endregion
        }

        #region Helpers

        private bool IsChangingOwnershipWithBarter(List<ReportForChangingOwnershipV2VehicleData> vehicleData)
        {
            if (vehicleData.Count != 2)
                return false;            

            var firstVehicleOldOwners = vehicleData[0].OldOwners?.Items;
            var secondVehicleNewOwners = vehicleData[1].NewOwners?.Items;

            if (firstVehicleOldOwners == null || secondVehicleNewOwners == null)
                return false;

            if (firstVehicleOldOwners.Count != secondVehicleNewOwners.Count)
                return false;

            for (int i = 0; i < firstVehicleOldOwners.Count; i++)
            {
                var firstVehicleOldOwner = firstVehicleOldOwners[i];
                var secondVehicleNewOwner = secondVehicleNewOwners[i];

                if (firstVehicleOldOwner.GetType() != secondVehicleNewOwner.GetType())
                    return false;
                else if (firstVehicleOldOwner is Domain.Models.PersonData firstVehicleOldOwnerPersonData && secondVehicleNewOwner is Domain.Models.PersonData secondVehicleNewOwnerPersonData
                    && firstVehicleOldOwnerPersonData.PersonBasicData.Identifier.Item != secondVehicleNewOwnerPersonData.PersonBasicData.Identifier.Item)
                    return false;
                else if (firstVehicleOldOwner is EntityData firstVehicleOldOwnerEntityData && secondVehicleNewOwner is EntityData secondVehicleNewOwnerEntityData
                    && firstVehicleOldOwnerEntityData.Identifier != secondVehicleNewOwnerEntityData.Identifier)
                    return false;
            }

            return true;
        }

        #endregion
    }
}
