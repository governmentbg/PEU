using Microsoft.AspNetCore.Authorization;

namespace EAU.Web.Admin.App.Controllers
{
    /// <summary>
    /// Базов контролер за реализация на уеб услуги.
    /// </summary>
    [Authorize]
    public class BaseApiController : EAU.Web.Mvc.BaseApiController
    {
    }
}