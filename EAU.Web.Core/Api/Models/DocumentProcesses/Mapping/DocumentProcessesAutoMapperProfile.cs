using AutoMapper;
using System.Linq;
using System.Text.Json;
using DomModels = EAU.Services.DocumentProcesses.Models;

namespace EAU.Web.Api.Models.DocumentProcesses.Mapping
{
    public class DocumentProcessesAutoMapperProfile : Profile
    {
        public DocumentProcessesAutoMapperProfile()
        {
            CreateDomainToViewModelMap();
        }

        private void CreateDomainToViewModelMap()
        {
            CreateMap<DomModels.DocumentProcess, DocumentProcess>()
                .AfterMap((src, dst) =>
                {
                    var textContent = src.ProcessContents?.SingleOrDefault(pc => pc.Type == DomModels.DocumentProcessContentTypes.FormJSON)?.TextContent;
                    if (textContent != null)
                    {
                        using (var jDocument = JsonDocument.Parse(textContent))
                        {
                            dst.Form = jDocument.RootElement.Clone();
                        }
                    }
                });
        }
    }
}
