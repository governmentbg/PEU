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
    [ResponseCache(CacheProfileName = "Nomenclatures")]
    [ApiParameter(Name = "If-None-Match", Type = typeof(string), Source = "header")]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    [NoopServiceLimiter]
    public class NomenclaturesCommonCacheController : BaseNomenclatureApiController
    {
        private readonly IDocumentTypes _documentTypes;
        private readonly IDeliveryChannels _deliveryChannels;
        private readonly IServiceTerms _serviceTerms;
        private readonly IEkatte _ekatte;
        private readonly IGrao _grao;
        private readonly IDocumentTemplateFields _documentTemplateFields;
        private readonly ICountries _countries;

        public NomenclaturesCommonCacheController(
            IDocumentTypes documentTypes,
            IDeliveryChannels deliveryChannels,
            IServiceTerms serviceTerms,
            IEkatte ekatte,
            IGrao grao,
            IDocumentTemplateFields documentTemplateFields,
            ICountries countries)
        {
            _documentTypes = documentTypes;
            _deliveryChannels = deliveryChannels;
            _serviceTerms = serviceTerms;
            _ekatte = ekatte;
            _grao = grao;
            _documentTemplateFields = documentTemplateFields;
            _countries = countries;
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата на видовете документи.
        /// </summary>
        /// <param name="cancellationToken"></param>       
        /// <returns>Номенклатура за видовете документи.</returns>
        [HttpGet]
        [Route("DocumentTypes")]
        [ProducesResponseType(typeof(IEnumerable<DocumentType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchDocumentTypes(CancellationToken cancellationToken)
        {
            await _documentTypes.EnsureLoadedAsync(cancellationToken);
            var cachedResources = _documentTypes.Search(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата на начините на получаване на резултат от услуга.
        /// </summary>
        /// <param name="cancellationToken"></param>       
        /// <returns>Номенклатура за начините на получаване на резултат от услуга.</returns>
        [HttpGet]
        [Route("DeliveryChannels")]
        [ProducesResponseType(typeof(IEnumerable<DeliveryChannel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchDeliveryChannels(CancellationToken cancellationToken)
        {
            await _deliveryChannels.EnsureLoadedAsync(cancellationToken);
            var cachedResources = _deliveryChannels.Search(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата за типове на предаване на административните услуги.
        /// </summary>
        /// <param name="cancellationToken"></param>       
        /// <returns>Номенклатура за типове на предаване на административните услуги.</returns>
        [HttpGet]
        [Route("ServiceTerms")]
        [ProducesResponseType(typeof(IEnumerable<ServiceTerm>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchServiceTerms(CancellationToken cancellationToken)
        {
            await _serviceTerms.EnsureLoadedAsync(cancellationToken);
            var cachedResources = _serviceTerms.Search(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата на EKATTE.
        /// </summary>
        /// <param name="cancellationToken"></param>       
        /// <returns>Номенклатура за EKATTE.</returns>
        [HttpGet]
        [Route("Ekatte")]
        [ProducesResponseType(typeof(IEnumerable<Ekatte>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchEkatte(CancellationToken cancellationToken)
        {
            await _ekatte.EnsureLoadedAsync(cancellationToken);
            var cachedResources = _ekatte.Search(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата на GRAO.
        /// </summary>
        /// <param name="cancellationToken"></param>       
        /// <returns>Номенклатура за GRAO.</returns>
        [HttpGet]
        [Route("Grao")]
        [ProducesResponseType(typeof(IEnumerable<Grao>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchGrao(CancellationToken cancellationToken)
        {
            await _grao.EnsureLoadedAsync(cancellationToken);
            var cachedResources = _grao.Search(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата на полетата в шаблони за документи.
        /// </summary>
        /// <param name="cancellationToken"></param>       
        /// <returns>Номенклатура на полетата в шаблони за документи.</returns>
        [HttpGet]
        [Route("DocumentTemplateFields")]
        [ProducesResponseType(typeof(IEnumerable<DocumentTemplateField>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchDocumentTemplateField(CancellationToken cancellationToken)
        {
            await _documentTemplateFields.EnsureLoadedAsync(cancellationToken);
            var cachedResources = _documentTemplateFields.Search(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата на държавите.
        /// </summary>
        /// <param name="cancellationToken"></param>       
        /// <returns>Номенклатура на държавите.</returns>
        [HttpGet]
        [Route("Countries")]
        [ProducesResponseType(typeof(IEnumerable<Country>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchCountries(CancellationToken cancellationToken)
        {
            await _countries.EnsureLoadedAsync(cancellationToken);
            var cachedResources = _countries.Search(out DateTime? lastModifiedDate);

            return new ObjectResultWithETag(() =>
            {
                return cachedResources;

            }, lastModifiedDate.Value.FormatForETag());
        }
    }
}
