using AutoMapper;
using EAU.Utilities;
using System.Net;
using DomUserModels = EAU.Users.Models;


namespace EAU.Web.Admin.App.Models.Mapping
{
    public class EAUAdminAutoMapperProfile : Profile
    {
        public EAUAdminAutoMapperProfile()
        {
            CreateDomainToViewModelMap();
            CreateViewToDomainModelMap();
        }

        private void CreateDomainToViewModelMap()
        {
            CreateMap<Audit.Models.LogAction, LogAction>()
                .AfterMap((src, dst) =>
                {
                    if (src.IpAddress != null)
                    {
                        dst.IpAddress = (new IPAddress(src.IpAddress)).ToString();
                    }
                });

            CreateMap<DomUserModels.UserLoginSession, UserLoginSessionVM>()
                .AfterMap((src, dst) =>
                {
                    if (src.IpAddress != null)
                    {
                        dst.IpAddress = (new IPAddress(src.IpAddress)).ToString();
                    }

                    dst.LoginDateTime = DateTimeOffsetHelper.ConvertOffsetToDateTime(src.LoginDate);
                    dst.LogoutDateTime = DateTimeOffsetHelper.ConvertOffsetToDateTime(src.LogoutDate);
                });

            CreateMap<DomUserModels.Certificate, CertificateVM>()
                .AfterMap((src, dst) =>
                {
                    dst.ValidNotBefore = DateTimeOffsetHelper.ConvertOffsetToDateTime(src.NotBefore);
                    dst.ValidNotAfter = DateTimeOffsetHelper.ConvertOffsetToDateTime(src.NotAfter);
                });

            CreateMap<DomUserModels.User, UserVM>();
        }

        private void CreateViewToDomainModelMap()
        {
            CreateMap<LogActionSearchCriteria, EAU.Audit.Repositories.LogActionSearchCriteria>()
                .ForMember(s => s.IpAddress, (config) => config.Ignore())
                .AfterMap((src, dst) =>
                {
                    IPAddress ipAddress = null;
                    if (!string.IsNullOrEmpty(src.IpAddress) && IPAddress.TryParse(src.IpAddress, out ipAddress))
                    {
                        dst.IpAddress = ipAddress;
                    }
                    else
                    {
                        dst.IpAddress = null;
                    }
                });
        }
    }
}