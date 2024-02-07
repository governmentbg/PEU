using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAU.Web.Mvc.Filters
{
    public interface IApiDescriptionFilter : IFilterMetadata
    {
        void Process(ApiDescription apiDescription);
    }
}
