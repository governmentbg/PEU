using EAU.Nomenclatures;
using EAU.Nomenclatures.Models;
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
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с номенклатури.
    /// Методите връщат ObjectResultWithETag, като резултата ТРЯБВА да бъде LINQ заявка, която да не е материализирана,
    /// защото в повечето случаи няма да трябва да се изпълнява. Ще се връща 304!
    /// </summary>    
    [NoopServiceLimiter]
    [ResponseCache(CacheProfileName = "Nomenclatures")]
    [ApiParameter(Name = "If-None-Match", Type = typeof(string), Source = "header")]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    public class NomenclaturesCacheController : BaseNomenclatureApiController
    {
        private readonly ILabels _labels;
        private readonly IServices _services;
        private readonly ILanguages _languages;
        private readonly IDeclarations _declarations;
        private readonly IServiceGroups _serviceGroups;
        private readonly IDocumentTemplates _documentTemplates;

        public NomenclaturesCacheController(
            ILabels labels,
            IServices services,
            ILanguages languages,
            IDeclarations declarations,
            IServiceGroups serviceGroups,
            IDocumentTemplates documentTemplates)
        {
            _labels = labels;
            _services = services;
            _languages = languages;
            _declarations = declarations;
            _serviceGroups = serviceGroups;
            _documentTemplates = documentTemplates;
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата за етикети на ресурси.
        /// </summary>
        /// <param name="lang">Език за локализация.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Номенклатура за етикети.</returns>
        [HttpGet]
        [Route("Labels/{lang?}")]
        [ProducesResponseType(typeof(IEnumerable<Label>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchLabels(string lang, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(lang))
                lang = HttpContext.GetLanguage();

            await _labels.EnsureLoadedAsync(lang, CancellationToken.None);
            var cachedResources = _labels.Search(lang, out DateTime? lastModifiedDate);

            /*резултата ТРЯБВА да бъде LINQ заявка, която да не е материализирана, 
            * защото в повечето случаи няма да трябва да се изпълнява. Ще се връща 304!*/
            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата на услугите.
        /// </summary>
        /// <param name="lang">Език за локализация.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Номенклатура за услугите.</returns>
        [HttpGet]
        [Route("Services/{lang?}")]
        [ProducesResponseType(typeof(IEnumerable<Service>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchServices(string lang, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(lang))
                lang = HttpContext.GetLanguage();

            await _services.EnsureLoadedAsync(lang, cancellationToken);
            var cachedResources = _services.Search(lang, out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата на групи услуги по направление на дейност в МВР.
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="cancellationToken"></param>       
        /// <returns>Номенклатура за групи услуги по направление на дейност в МВР.</returns>
        [HttpGet]
        [Route("ServiceGroups/{lang?}")]
        [ProducesResponseType(typeof(IEnumerable<ServiceGroup>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchServiceGroups(string lang, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(lang))
                lang = HttpContext.GetLanguage();

            await _serviceGroups.EnsureLoadedAsync(lang, cancellationToken);
            var cachedResources = _serviceGroups.Search(lang, out DateTime ? lastModifiedDate);

            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата за езици.
        /// </summary>
        /// <param name="cancellationToken"></param>       
        /// <returns>Номенклатура за език.</returns>
        [HttpGet]
        [Route("Languages")]
        [ProducesResponseType(typeof(IEnumerable<Language>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchLanguages(CancellationToken cancellationToken)
        {
            await _languages.EnsureLoadedAsync(cancellationToken);
            var cachedResources = _languages.Search(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата на декларативни обстоятелства и политики.
        /// </summary>
        /// <param name="cancellationToken"></param>       
        /// <returns>Номенклатура за декларативни обстоятелства и политики.</returns>
        [HttpGet]
        [Route("Declarations")]
        [ProducesResponseType(typeof(IEnumerable<Declaration>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchDeclarations(CancellationToken cancellationToken)
        {
            await _declarations.EnsureLoadedAsync(cancellationToken);
            var cachedResources = _declarations.Search(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата на шаблони за документи.
        /// </summary>
        /// <param name="cancellationToken"></param>       
        /// <returns>Номенклатура на шаблони за документи.</returns>
        [HttpGet]
        [Route("DocumentTemplates")]
        [ProducesResponseType(typeof(IEnumerable<DocumentTemplate>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchDocumentTemplates(CancellationToken cancellationToken)
        {
            await _documentTemplates.EnsureLoadedAsync(cancellationToken);
            var cachedResources = _documentTemplates.Search(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }
    }
}
