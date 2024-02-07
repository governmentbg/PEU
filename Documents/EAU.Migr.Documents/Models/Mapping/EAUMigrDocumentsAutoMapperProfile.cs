using AutoMapper;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Documents.Models.Mapping;
using EAU.Migr.Documents.Domain.Models;
using EAU.Migr.Documents.Domain.Models.Forms;
using EAU.Migr.Documents.Models.Forms;

namespace EAU.Migr.Documents.Models.Mapping
{
    public class EAUMigrDocumentsAutoMapperProfile : Profile
    {
        public EAUMigrDocumentsAutoMapperProfile()
        {
            CreateDomainToViewModelMap();
            CreateViewToDomainModelMap();
        }

        private void CreateDomainToViewModelMap()
        {
            #region ApplicationForIssuingDocumentForForeigners

            CreateMap<ApplicationForIssuingDocumentForForeigners, ApplicationForIssuingDocumentForForeignersVM>()
                 .ForMember(d => d.Declarations, (config) => config.Ignore())
                 .AfterMap((s, d, ctx) =>
                 {
                     MapperHelper.MapApplicationDomainToViewModel(ctx.Mapper, s, d);
                     d.Circumstances = ctx.Mapper.Map<ApplicationForIssuingDocumentForForeignersDataVM>(s.ApplicationForIssuingDocumentForForeignersData);
                 });

            CreateMap<ApplicationForIssuingDocumentForForeignersData, ApplicationForIssuingDocumentForForeignersDataVM>()
                .AfterMap((s, d, ctx) =>
                {
                    d.Citizenship = ctx.Mapper.Map<Citizenship, CitizenshipVM>(s.Citizenship);
                });

            #endregion
        }

        private void CreateViewToDomainModelMap()
        {
            #region ApplicationForIssuingDocumentForForeigners

            CreateMap<ApplicationForIssuingDocumentForForeignersVM, ApplicationForIssuingDocumentForForeigners>()
                .ForMember(d => d.Declarations, (config) => config.Ignore())
                .AfterMap((s, d, ctx) =>
                 {
                     MapperHelper.MapApplicationViewModelToDomain(ctx.Mapper, s, d);
                     d.ApplicationForIssuingDocumentForForeignersData = ctx.Mapper.Map<ApplicationForIssuingDocumentForForeignersData>(s.Circumstances);
                 });

            CreateMap<ApplicationForIssuingDocumentForForeignersDataVM, ApplicationForIssuingDocumentForForeignersData>()
                .AfterMap((s, d, ctx) =>
                {
                    d.Citizenship = ctx.Mapper.Map<CitizenshipVM, Citizenship>(s.Citizenship);
                });

            #endregion
        }
    }
}