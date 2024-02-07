using EAU.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EAU.Web.EDocViewer.App.Controllers
{
    [Route("api/[controller]")]
    public class TestController : BaseApiController
    {
        [HttpGet, Route("Hello")]
        [AllowAnonymous]
        public async Task<ActionResult> Test_Hello()
        {
            return Ok($"Its up!");
        }
    }
}
