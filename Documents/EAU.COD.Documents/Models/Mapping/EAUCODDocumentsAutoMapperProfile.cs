using AutoMapper;
using EAU.Documents.Models.Mapping;
using EAU.COD.Documents.Domain.Models;
using EAU.COD.Documents.Domain.Models.Forms;
using EAU.COD.Documents.Models.Forms;
using System.Collections.Generic;
using System.Linq;

namespace EAU.COD.Documents.Models.Mapping
{
    public class EAUCODDocumentsAutoMapperProfile : Profile
    {
        public EAUCODDocumentsAutoMapperProfile()
        {
            CreateDomainToViewModelMap();
            CreateViewToDomainModelMap();
        }

        private void CreateDomainToViewModelMap()
        {
            #region RequestForIssuingLicenseForPrivateSecurityServices

            CreateMap<TerritorialScopeOfServicesDistricts, TerritorialScopeOfServicesDistrictsVM>()
              .AfterMap((s, d) =>
              {
                  d.DistrictGRAOCode = s.DistrictGRAOCode;
                  d.DistrictGRAOName = s.DistrictGRAOName;
              });

            CreateMap<TerritorialScopeOfServices, TerritorialScopeOfServicesVM>();

            CreateMap<RequestForIssuingLicenseForPrivateSecurityServicesData, RequestForIssuingLicenseForPrivateSecurityServicesDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    List<SecurityServiceTypesVM> securityServiceTypes = new List<SecurityServiceTypesVM>()
                    {
                        new SecurityServiceTypesVM() {IsSelected = false, PointOfPrivateSecurityServicesLaw = PointOfPrivateSecurityServicesLaw.PersonalSecurityServicesForPersons},
                        new SecurityServiceTypesVM() {IsSelected = false, PointOfPrivateSecurityServicesLaw = PointOfPrivateSecurityServicesLaw.PropertySecurityServices},
                        new SecurityServiceTypesVM() {IsSelected = false, PointOfPrivateSecurityServicesLaw = PointOfPrivateSecurityServicesLaw.EventsSecurityServices},
                        new SecurityServiceTypesVM() {IsSelected = false, PointOfPrivateSecurityServicesLaw = PointOfPrivateSecurityServicesLaw.ValuablesAndCargoesSecurityServices},
                        new SecurityServiceTypesVM() {IsSelected = false, PointOfPrivateSecurityServicesLaw = PointOfPrivateSecurityServicesLaw.EntityPropertySelfProtection},
                        new SecurityServiceTypesVM() {IsSelected = false, PointOfPrivateSecurityServicesLaw = PointOfPrivateSecurityServicesLaw.AlarmAndSecurityActivity},
                        new SecurityServiceTypesVM() {IsSelected = false, PointOfPrivateSecurityServicesLaw = PointOfPrivateSecurityServicesLaw.RealEstatSecurity},
                        new SecurityServiceTypesVM() {IsSelected = false, PointOfPrivateSecurityServicesLaw = PointOfPrivateSecurityServicesLaw.AgriculturalAndPropertyProtection}
                    };

                    foreach (var item in s.SecurityServiceTypes)
                    {
                        securityServiceTypes.Find(x => x.PointOfPrivateSecurityServicesLaw == item.PointOfPrivateSecurityServicesLaw.Value).TerritorialScopeOfServices = ctx.Mapper.Map<TerritorialScopeOfServices, TerritorialScopeOfServicesVM>(item.TerritorialScopeOfServices);

                        if (securityServiceTypes.Any(x => x.PointOfPrivateSecurityServicesLaw == item.PointOfPrivateSecurityServicesLaw.Value))
                            securityServiceTypes.Find(x => x.PointOfPrivateSecurityServicesLaw == item.PointOfPrivateSecurityServicesLaw.Value).IsSelected = true;
                    }

                    d.SecurityServiceTypes = securityServiceTypes;
                });

            CreateMap<RequestForIssuingLicenseForPrivateSecurityServicesDataSecurityServiceTypes, SecurityServiceTypesVM>()
                 .AfterMap((s, d, ctx) =>
                 {
                     d.PointOfPrivateSecurityServicesLaw = s.PointOfPrivateSecurityServicesLaw.Value;
                     d.TerritorialScopeOfServices = ctx.Mapper.Map<TerritorialScopeOfServices, TerritorialScopeOfServicesVM>(s.TerritorialScopeOfServices);
                     d.IsSelected = true;
                 });

            CreateMap<RequestForIssuingLicenseForPrivateSecurityServices, RequestForIssuingLicenseForPrivateSecurityServicesVM>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                    d.Circumstances = ctx.Mapper.Map<RequestForIssuingLicenseForPrivateSecurityServicesData, RequestForIssuingLicenseForPrivateSecurityServicesDataVM>(s.RequestForIssuingLicenseForPrivateSecurityServicesData);

                    if (d.Circumstances != null)
                        d.Circumstances.IssuingPoliceDepartment = s.IssuingPoliceDepartment;
                });

            #endregion

            #region NotificationForConcludingOrTerminatingEmploymentContract

            CreateMap<NotificationForConcludingOrTerminatingEmploymentContractData, NotificationForConcludingOrTerminatingEmploymentContractDataVM>();

            CreateMap<NotificationForConcludingOrTerminatingEmploymentContract, NotificationForConcludingOrTerminatingEmploymentContractVM>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                    d.Circumstances = ctx.Mapper.Map<NotificationForConcludingOrTerminatingEmploymentContractData, NotificationForConcludingOrTerminatingEmploymentContractDataVM>(s.NotificationForConcludingOrTerminatingEmploymentContractData);
                });

            #endregion

            #region NotificationForTakingOrRemovingFromSecurity

            CreateMap<SecurityObjectsData, SecurityObjectsDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.SecurityObjects != null && s.SecurityObjects.Count > 0)
                    {
                        d.SecurityObjects = new List<SecurityObject>();

                        foreach (var securityObject in s.SecurityObjects)
                        {
                            if (securityObject.PointOfPrivateSecurityServicesLaw.HasValue)
                            {
                                d.SecurityObjects.Add(securityObject);
                            }
                        }
                    }
                });

            CreateMap<NotificationForTakingOrRemovingFromSecurityData, NotificationForTakingOrRemovingFromSecurityDataVM>();

            CreateMap<NotificationForTakingOrRemovingFromSecurity, NotificationForTakingOrRemovingFromSecurityVM>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                    d.Circumstances = ctx.Mapper.Map<NotificationForTakingOrRemovingFromSecurityData, NotificationForTakingOrRemovingFromSecurityDataVM>(s.NotificationForTakingOrRemovingFromSecurityData);
                    d.SecurityObjectsData = ctx.Mapper.Map<SecurityObjectsData, SecurityObjectsDataVM>(s.SecurityObjectsData);
                });

            #endregion
        }

        private void CreateViewToDomainModelMap()
        {
            #region RequestForIssuingLicenseForPrivateSecurityServices

            CreateMap<TerritorialScopeOfServicesDistrictsVM, TerritorialScopeOfServicesDistricts>()
               .AfterMap((s, d) =>
               {
                   d.DistrictGRAOCode = s.DistrictGRAOCode;
                   d.DistrictGRAOName = s.DistrictGRAOName;
               });

            CreateMap<TerritorialScopeOfServicesVM, TerritorialScopeOfServices>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.ScopeOfCertification == ScopeOfCertification.SelectedDistricts && s.Districts != null && s.Districts.Count > 0 && !string.IsNullOrEmpty(s.Districts[0].DistrictGRAOCode))
                        d.Districts = ctx.Mapper.Map<List<TerritorialScopeOfServicesDistrictsVM>, List<TerritorialScopeOfServicesDistricts>>(s.Districts);
                    else
                        d.Districts = null;

                    d.ScopeOfCertification = s.ScopeOfCertification;
                });

            CreateMap<SecurityServiceTypesVM, RequestForIssuingLicenseForPrivateSecurityServicesDataSecurityServiceTypes>()
                .AfterMap((s, d, ctx) =>
                {
                    d.PointOfPrivateSecurityServicesLaw = s.PointOfPrivateSecurityServicesLaw;
                    d.TerritorialScopeOfServices = ctx.Mapper.Map<TerritorialScopeOfServicesVM, TerritorialScopeOfServices>(s.TerritorialScopeOfServices);
                });

            CreateMap<RequestForIssuingLicenseForPrivateSecurityServicesDataVM, RequestForIssuingLicenseForPrivateSecurityServicesData>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.SecurityServiceTypes != null)
                    {
                        d.SecurityServiceTypes = new List<RequestForIssuingLicenseForPrivateSecurityServicesDataSecurityServiceTypes>();

                        foreach (var SecurityServiceType in s.SecurityServiceTypes.Where(t => t.IsSelected))
                        {
                            d.SecurityServiceTypes.Add(ctx.Mapper.Map<SecurityServiceTypesVM, RequestForIssuingLicenseForPrivateSecurityServicesDataSecurityServiceTypes>(SecurityServiceType));
                        }
                    }
                });

            CreateMap<RequestForIssuingLicenseForPrivateSecurityServicesVM, RequestForIssuingLicenseForPrivateSecurityServices>()
             .ForMember(d => d.Declarations, (config) => config.Ignore())
             .AfterMap((s, d, ctx) =>
             {
                 MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);

                 if (s.Circumstances != null)
                 {
                     d.IssuingPoliceDepartment = s.Circumstances.IssuingPoliceDepartment;
                     d.RequestForIssuingLicenseForPrivateSecurityServicesData = ctx.Mapper.Map<RequestForIssuingLicenseForPrivateSecurityServicesDataVM, RequestForIssuingLicenseForPrivateSecurityServicesData>(s.Circumstances);
                 }
             });

            #endregion

            #region NotificationForConcludingOrTerminatingEmploymentContract

            CreateMap<NotificationForConcludingOrTerminatingEmploymentContractDataVM, NotificationForConcludingOrTerminatingEmploymentContractData>();

            CreateMap<NotificationForConcludingOrTerminatingEmploymentContractVM, NotificationForConcludingOrTerminatingEmploymentContract>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                {
                    MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);

                    if (s.Circumstances != null)
                        d.NotificationForConcludingOrTerminatingEmploymentContractData = ctx.Mapper.Map<NotificationForConcludingOrTerminatingEmploymentContractDataVM, NotificationForConcludingOrTerminatingEmploymentContractData>(s.Circumstances);
                });

            #endregion

            #region NotificationForTakingOrRemovingFromSecurity

            CreateMap<SecurityObjectsDataVM, SecurityObjectsData>()
                .AfterMap((s, d, ctx) =>
                {
                    if (s.SecurityObjects != null && s.SecurityObjects.Count > 0)
                    {
                        d.SecurityObjects = new List<SecurityObject>();

                        foreach (var securityObject in s.SecurityObjects)
                        {
                            if (securityObject.PointOfPrivateSecurityServicesLaw.HasValue)
                            {
                                d.SecurityObjects.Add(securityObject);
                            }
                        }
                    }
                });

            CreateMap<NotificationForTakingOrRemovingFromSecurityDataVM, NotificationForTakingOrRemovingFromSecurityData>();

            CreateMap<NotificationForTakingOrRemovingFromSecurityVM, NotificationForTakingOrRemovingFromSecurity>()
               .ForMember(d => d.Declarations, (config) => config.Ignore())
               .AfterMap((s, d, ctx) =>
               {
                   MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);

                   if (s.Circumstances != null)
                       d.NotificationForTakingOrRemovingFromSecurityData = ctx.Mapper.Map<NotificationForTakingOrRemovingFromSecurityDataVM, NotificationForTakingOrRemovingFromSecurityData>(s.Circumstances);

                   if (s.SecurityObjectsData != null)
                       d.SecurityObjectsData = ctx.Mapper.Map<SecurityObjectsDataVM, SecurityObjectsData>(s.SecurityObjectsData);
               });

            #endregion
        }
    }
}