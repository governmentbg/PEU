using EAU.Web.Mvc;
using EAU.Web.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAU.Web.Api.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с номенклатури.
    /// </summary>    
    [Produces("application/json")]    
    [Route("api/Nomenclatures")]
    public abstract class BaseNomenclatureApiController : BaseApiController
    {
    }
}
