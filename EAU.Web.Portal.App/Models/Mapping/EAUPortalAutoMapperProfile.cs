using AutoMapper;
using WAIS.Integration.MOI.KAT.SPRKRTCO.Models;
using DomModels = EAU.Services.ServiceInstances.Models;

namespace EAU.Web.Portal.App.Models.Mapping
{
    public class EAUPortalAutoMapperProfile : Profile
    {
        public EAUPortalAutoMapperProfile()
        {
            CreateDomainToViewModelMap();
            CreateViewToDomainModelMap();
        }

        private void CreateDomainToViewModelMap()
        {
            CreateMap<DomModels.ServiceInstance, ServiceInstance>()
                .AfterMap((src, dst) =>
                {
                    dst.Service = src.NomService?.Name;
                });
        }

        private void CreateViewToDomainModelMap()
        {
            CreateMap<FourDigitSearchCriteriaVM, FourDigitSearchCriteria>()
                .AfterMap((s, d) => 
                {
                    if (s.FourDigitSearchType == FourDigitSearchTypes.ByInterval)
                    {
                        if(!string.IsNullOrEmpty(s.FromRegNumber) && !string.IsNullOrEmpty(s.ToRegNumber))
                        {
                            d.PlateNumberSearch = string.Format("{0}-{1}", s.FromRegNumber, s.ToRegNumber);
                        }
                    }
                    else 
                    {
                        d.PlateNumberSearch = string.Format("{0}-{1}", s.SpecificRegNumber, s.SpecificRegNumber);
                    }
                });
        }
    }
}
