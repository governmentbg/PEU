using AutoMapper;
using CNSys;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Documents.Models.Mapping;
using EAU.PBZN.Documents.Domain.Models;
using EAU.PBZN.Documents.Domain.Models.Forms;
using EAU.PBZN.Documents.Models;
using EAU.PBZN.Documents.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAU.Migr.Documents.Models.Mapping
{
    public class EAUPBZNDocumentsAutoMapperProfile : Profile
    {
        public EAUPBZNDocumentsAutoMapperProfile()
        {
            CreateDomainToViewModelMap();
            CreateViewToDomainModelMap();
        }

        private void CreateDomainToViewModelMap()
        {

            #region ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesVM

            CreateMap<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData, ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM>()
            .AfterMap((s, d) =>
            {
                d.CertificateType = s.CertificateType;
                d.CertificateNumber = s.CertificateNumber;
            });
            CreateMap<CertifiedPersonel, CertifiedPersonelVM>();
            CreateMap<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData, ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM>()
            .AfterMap((s, d, ctx) =>
            {
                d.AvailableCertifiedPersonnel = ctx.Mapper.Map<List<CertifiedPersonel>, List<CertifiedPersonelVM>>(s.AvailableCertifiedPersonnel);
            });

            CreateMap<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData, ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.WorkPhone = s.WorkPhone;
                    var app = new ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData();
                    if (s.Item.GetType() == app.GetType())
                    {
                        d.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData = ctx.Mapper.Map<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData, ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM>((ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData)s.Item);
                        d.EntityOrPerson = EntityOrPerson.Person;
                    }
                    else
                    {
                        d.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData = ctx.Mapper.Map<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData, ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM>((ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData)s.Item);
                        d.EntityOrPerson = EntityOrPerson.Entity;
                    }
                });

            CreateMap<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGasses, ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesVM>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                    d.Circumstances = ctx.Mapper.Map<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData, ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVM>(s.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData);
                    d.Circumstances.IssuingPoliceDepartment = s.IssuingPoliceDepartment;
                });

            #endregion

            #region ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOut

            CreateMap<ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData, ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataVM>()
                .AfterMap((s, d) =>
                {
                    if (s.EntityManagementAddress != null)
                    {
                        d.EntityManagementAddress = s.EntityManagementAddress;
                    }


                    if (s.CorespondingAddress != null)
                        d.CorespondingAddress = s.CorespondingAddress;

                });


            CreateMap<ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOut, ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutVM>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                    d.Circumstances = ctx.Mapper.Map<ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData, ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataVM>(s.ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData);
                    d.Circumstances.IssuingPoliceDepartment = s.IssuingPoliceDepartment;
                });
            #endregion

            #region CertificateToWorkWithFluorinatedGreenhouseGasses

            CreateMap<CertificateToWorkWithFluorinatedGreenhouseGasses, CertificateToWorkWithFluorinatedGreenhouseGassesVM>()
                .ForMember(s => s.IdentificationPhoto, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    if (s.IdentificationPhoto != null && s.IdentificationPhoto.Length > 0)
                        d.IdentificationPhoto = Convert.ToBase64String(s.IdentificationPhoto);

                    MapperHelper.MapDocumentWithOfficialDomainToViewModel<CertificateToWorkWithFluorinatedGreenhouseGassesOfficial>(s, d);
                    if (s.Item != null)
                    {
                        if (s.Item.GetType() == typeof(ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData))
                        {
                            d.EntityData = ctx.Mapper.Map<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM>(s.Item);
                        }
                        else
                        {
                            d.PersonData = ctx.Mapper.Map<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM>(s.Item);
                        }
                    }
                });

            CreateMap<CertificateToWorkWithFluorinatedGreenhouseGassesOfficial, OfficialVM>()
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapOfficialDomainToViewModel(s, d);

                    if (d.ForeignCitizenNames != null)
                    {
                        d.ForeignCitizenNames = (ForeignCitizenNames)s.Item;
                    }
                    else
                    {
                        d.PersonNames = (PersonNames)s.Item;
                    }
                });

            #endregion

            #region CertificateForAccident

            CreateMap<CertificateForAccident, CertificateForAccidentVM>()
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapDocumentWithOfficialDomainToViewModel<CertificateForAccidentOfficial>(s, d);
                });

            CreateMap<CertificateForAccidentOfficial, OfficialVM>()
                .AfterMap((s, d) =>
                {
                    MapperHelper.MapOfficialDomainToViewModel(s, d);

                    if (d.ForeignCitizenNames != null)
                    {
                        d.ForeignCitizenNames = (ForeignCitizenNames)s.Item;
                    }
                    else
                    {
                        d.PersonNames = (PersonNames)s.Item;
                    }
                });

            #endregion
        }

        private void CreateViewToDomainModelMap()
        {
            #region ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesVM

            CreateMap<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM, ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData>()
            .AfterMap((s, d) =>
            {
                d.CertificateNumber = s.CertificateNumber;
                d.CertificateType = s.CertificateType.GetValueOrDefault();
            });
            CreateMap<CertifiedPersonelVM, CertifiedPersonel>();
            CreateMap<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM, ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData>()
            .AfterMap((s, d, ctx) =>
            {
                d.AvailableCertifiedPersonnel = ctx.Mapper.Map<List<CertifiedPersonelVM>, List<CertifiedPersonel>>(s.AvailableCertifiedPersonnel);
            });

            CreateMap<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVM, ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData>()
                .AfterMap((s, d, ctx) =>
                {
                    s.WorkPhone = d.WorkPhone;
                    if (s.EntityOrPerson == EntityOrPerson.Person)
                    {
                        d.Item = ctx.Mapper.Map<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM, ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData>(s.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData);
                    }
                    else
                    {
                        d.Item = ctx.Mapper.Map<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM, ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData>(s.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData);
                    }

                });

            CreateMap<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesVM, ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGasses>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                    if (s.Circumstances != null)
                    {
                        d.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData = ctx.Mapper.Map<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVM, ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData>(s.Circumstances);
                        d.IssuingPoliceDepartment = s.Circumstances.IssuingPoliceDepartment;
                    }
                });

            #endregion

            #region ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOut

            CreateMap<ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataVM, ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.IsRecipientEntity.HasValue && s.IsRecipientEntity.GetValueOrDefault())
                    {
                        d.EntityManagementAddress = s.EntityManagementAddress;
                    }

                    if (s.CorespondingAddress != null)
                        d.CorespondingAddress = ctx.Mapper.Map<EntityAddress>(s.CorespondingAddress);

                    d.DocumentMustServeTo = ctx.Mapper.Map<DocumentMustServeTo>(s.DocumentMustServeTo);
                });


            CreateMap<ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutVM, ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOut>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                    if (s.Circumstances != null)
                    {
                        d.IssuingPoliceDepartment = s.Circumstances.IssuingPoliceDepartment;
                        d.ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData = ctx.Mapper.Map<ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataVM, ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData>(s.Circumstances);
                    }
                });
            #endregion

            #region CertificateToWorkWithFluorinatedGreenhouseGasses

            CreateMap<CertificateToWorkWithFluorinatedGreenhouseGassesVM, CertificateToWorkWithFluorinatedGreenhouseGasses>()
                .ForMember(s => s.IdentificationPhoto, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    if (!string.IsNullOrWhiteSpace(s.IdentificationPhoto))
                        d.IdentificationPhoto = Convert.FromBase64String(s.IdentificationPhoto);

                    MapperHelper.MapDocumentWithOfficialViewModelToDomain<CertificateToWorkWithFluorinatedGreenhouseGassesOfficial>(s, d);

                    if (s.EntityData == null || ObjectUtility.IsEmpty(s.EntityData))
                    {
                        d.Item = ctx.Mapper.Map<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData>(s.PersonData);
                    }
                    else
                    {
                        d.Item = ctx.Mapper.Map<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData>(s.EntityData);
                    }
                });

            CreateMap<OfficialVM, CertificateToWorkWithFluorinatedGreenhouseGassesOfficial>()
                .AfterMap((s, d) =>
                {
                    MapperHelper.MapOfficialViewModelToDomain(s, d);
                });

            #endregion

            #region CertificateForAccident

            CreateMap<CertificateForAccidentVM, CertificateForAccident>()
                .AfterMap((s, d) =>
                {
                    MapperHelper.MapDocumentWithOfficialViewModelToDomain<CertificateForAccidentOfficial>(s, d);
                });

            CreateMap<OfficialVM, CertificateForAccidentOfficial>()
                .AfterMap((s, d) =>
                {
                    MapperHelper.MapOfficialViewModelToDomain(s, d);
                });

            #endregion
        }
    }
}