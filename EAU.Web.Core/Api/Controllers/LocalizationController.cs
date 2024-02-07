using EAU.Nomenclatures;
using EAU.Nomenclatures.Models;
using EAU.Web.Filters;
using EAU.Web.Mvc;
using EAU.Web.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Api.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с локализация.
    /// Методите връщат ObjectResultWithETag, като резултата ТРЯБВА да бъде LINQ заявка, която да не е материализирана,
    /// защото в повечето случаи няма да трябва да се изпълнява. Ще се връща 304!
    /// </summary>    
    [NoopServiceLimiter]
    [ResponseCache(CacheProfileName = "Nomenclatures")]
    [ApiParameter(Name = "If-None-Match", Type = typeof(string), Source = "header")]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    [AllowAnonymous]
    public class LocalizationController : BaseApiController
    {
        private readonly ILabelLocalizations _labelLocalizations;

        public LocalizationController(ILabelLocalizations labelLocalizations)
        {
            _labelLocalizations = labelLocalizations;
        }

        /// <summary>
        /// Операция за изчитане на етикети на ресурси.
        /// </summary>
        /// <param name="lang">Език за локализация.</param>
        /// <param name="prefixes">Префикси на кодове.</param>
        /// <returns>Номенклатура за етикети.</returns>
        [HttpGet]
        [Route("Labels/{lang?}")]
        [ProducesResponseType(typeof(IEnumerable<Label>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchLabelsLocalized(string lang, [FromQuery] string prefixes)
        {
            if (string.IsNullOrEmpty(lang))
                lang = HttpContext.GetLanguage();

            await _labelLocalizations.EnsureLoadedAsync(lang, CancellationToken.None);

            var cachedResources = _labelLocalizations.Search(lang, out DateTime? lastModifiedDate);

            /*резултата ТРЯБВА да бъде LINQ заявка, която да не е материализирана, 
             * защото в повечето случаи няма да трябва да се изпълнява. Ще се връща 304!*/
            return new ObjectResultWithETag(() =>
            {
                if (string.IsNullOrEmpty(prefixes))
                {
                    return cachedResources;
                }
                else
                {
                    var prefixesList = prefixes.Split(',');

                    return cachedResources.Where((item) => { return prefixesList.Any((prefix) => { return item.Key.StartsWith(prefix); }); })
                        .ToDictionary((item) => { return item.Key; }, (item) => { return item.Value; });
                }

            }, lastModifiedDate.Value.FormatForETag());
        }
    }
}