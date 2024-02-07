using EAU.CMS.Cache;
using EAU.CMS.Models;
using EAU.Web.Filters;
using EAU.Web.Mvc;
using EAU.Web.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Api.Controllers
{
    [Route("api/Pages")]
    [ResponseCache(CacheProfileName = "Nomenclatures")]
    [ApiParameter(Name = "If-None-Match", Type = typeof(string), Source = "header")]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    [NoopServiceLimiter]
    public class PagesCacheController : BaseApiController
    {
        private readonly IPages _pages;

        public PagesCacheController(
            IPages pages)
        {
            _pages = pages;
        }

        /// <summary>
        /// Операция за изчитане на страници с html съдържание.
        /// </summary>
        /// <param name="lang">Език.</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>       
        /// <returns>Страници с html съдържание.</returns>
        [HttpGet]
        [Route("{lang?}")]
        [ProducesResponseType(typeof(IEnumerable<Page>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPages(string lang, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(lang))
                lang = HttpContext.GetLanguage();

            await _pages.EnsureLoadedAsync(lang, cancellationToken);
            var cachedResources = _pages.Search(lang, out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }
    }
}
