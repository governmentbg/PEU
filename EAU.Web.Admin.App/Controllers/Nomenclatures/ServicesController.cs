using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EAU.Nomenclatures;
using EAU.Nomenclatures.Models;
using EAU.Nomenclatures.Repositories;
using EAU.Nomenclatures.Services;
using EAU.Web.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EAU.Web.Admin.App.Controllers
{
    public class ServicesController : BaseNomenclatureApiController
    {
        private readonly IServiceService _serviceService;
        private readonly ILanguages _languages;
        private readonly IServiceRepository _serviceRepository;

        public ServicesController(
            IServiceService serviceService,
            ILanguages languages,
            IServiceRepository serviceRepository)
        {
            _serviceService = serviceService;
            _languages = languages;
            _serviceRepository = serviceRepository;
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата на услугите.
        /// </summary>
        /// <param name="lang">Език за локализация.</param>
        /// <param name="forceTranslated">Флаг, указващ дали да бъдат заредени стойностости от превод, независимо дали има такъв.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Номенклатура за услугите.</returns>
        [HttpGet]
        [Route("Services/{lang?}")]
        [ProducesResponseType(typeof(IEnumerable<Service>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchServices(string lang, bool? forceTranslated, CancellationToken cancellationToken)
        {
            await _languages.EnsureLoadedAsync(cancellationToken);
            int? langID = _languages.GetLanguageOrDefault(lang).LanguageID;

            var res = await _serviceService.SearchInfoAsync(new ServiceSearchCriteria() 
            { 
                LanguageID = langID,
                ForceTranslated = forceTranslated
            }, cancellationToken);
            return Ok(res.Data);
        }

        /// <summary>
        /// Операция за създаване на номенклатура на административните услуги.
        /// </summary>
        /// <param name="model">Номенклатура на административните услуги.</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Services")]
        public async Task<IActionResult> Create(Service model, CancellationToken cancellationToken)
        {
            var result = await _serviceService.CreateAsync(model, cancellationToken);
            return OperationResult(result);
        }

        /// <summary>
        /// Операция за редакция на номенклатура на административните услуги.
        /// </summary>
        /// <param name="serviceID">Идентификатор на услуга</param>
        /// <param name="model">Номенклатура на административните услуги.</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Services/{serviceID}")]
        public async Task<IActionResult> Update(int serviceID, Service model, CancellationToken cancellationToken)
        {
            model.ServiceID = serviceID;
            var result = await _serviceService.UpdateAsync(model, cancellationToken);
            return OperationResult(result);
        }

        /// <summary>
        /// Операция за активиране/деактивиране на номенклатура на административните услуги.
        /// </summary>
        /// <param name="serviceID">Идентификатор на услуга</param>
        /// <param name="status">Статус</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Services/{serviceID}/Status")]
        public async Task<IActionResult> StatusChange(
            int serviceID,
            Status status,
            CancellationToken cancellationToken)
        {
            var result = await _serviceService.StatuChangeAsync(serviceID, status, cancellationToken);
            return OperationResult(result);
        }

        /// <summary>
        /// Операция за създаване на номенклатура на превод на услуга от чужд език.
        /// </summary>
        /// <param name="lang">Код на език.</param>
        /// <param name="serviceID">Идентификатор на услуга.</param>
        /// <param name="model">Номенклатура на превод на услуга от чужд език.</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Services/{lang}/{serviceID}")]
        public async Task<IActionResult> CreateTranslation(
            string lang,
            int serviceID,
            ServiceTranslation model,
            CancellationToken cancellationToken)
        {
            model.ServiceID = serviceID;
            var result = await _serviceService.CreateAsync(model, lang, cancellationToken);
            return OperationResult(result);
        }

        /// <summary>
        /// Операция за редакция на номенклатура на превод на услуга от чужд език.
        /// </summary>
        /// <param name="lang">Код на език.</param>
        /// <param name="serviceID">Идентификатор на услуга.</param>
        /// <param name="model">Номенклатура на превод на услуга от чужд език.</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Services/{lang}/{serviceID}")]
        public async Task<IActionResult> UpdateTranslation(
            string lang,
            int serviceID,
            ServiceTranslation model,
            CancellationToken cancellationToken)
        {
            model.ServiceID = serviceID;
            var result = await _serviceService.UpdateAsync(model, lang, cancellationToken);
            return OperationResult(result);
        }
    }
}