using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EAU.Nomenclatures;
using EAU.Nomenclatures.Models;
using EAU.Nomenclatures.Repositories;
using EAU.Web.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EAU.Web.Admin.App.Controllers
{
    public class ServiceGroupsController : BaseNomenclatureApiController
    {
        private readonly IServiceGroupService _serviceGroupService;
        private readonly IServiceGroupRepository _serviceGroupRepository;
        private readonly ILanguages _languages;

        public ServiceGroupsController(
            IServiceGroupService serviceGroupService,
            IServiceGroupRepository serviceGroupRepository,
            ILanguages languages)
        {
            _serviceGroupService = serviceGroupService;
            _serviceGroupRepository = serviceGroupRepository;
            _languages = languages;
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата на групи услуги по направление на дейност в МВР.
        /// </summary>
        /// <param name="lang">Език.</param>
        /// <param name="forceTranslated">Флаг, указващ дали да бъдат заредени стойностости от превод, независимо дали има такъв.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>       
        /// <returns>Номенклатура за групи услуги по направление на дейност в МВР.</returns>
        [HttpGet]
        [Route("ServiceGroups/{lang?}")]
        [ProducesResponseType(typeof(IEnumerable<ServiceGroup>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchServiceGroups(string lang, bool? forceTranslated, CancellationToken cancellationToken)
        {
            await _languages.EnsureLoadedAsync(cancellationToken);
            int? langID = _languages.GetLanguageOrDefault(lang).LanguageID;

            var res = await _serviceGroupRepository.SearchInfoAsync(new ServiceGroupSearchCriteria() 
            { 
                LanguageID = langID,
                ForceTranslated = forceTranslated
            }, cancellationToken);
            return Ok(res.Data);
        }

        /// <summary>
        /// Операция за създаване на номенклатура на групи услуги по направление на дейност в МВР
        /// </summary>
        /// <param name="model">Номенклатура на групи услуги по направление на дейност в МВР</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ServiceGroups")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(
            ServiceGroup model, 
            CancellationToken cancellationToken)
        {
            var result = await _serviceGroupService.CreateAsync(model, cancellationToken);
            return OperationResult(result);
        }

        /// <summary>
        /// Операция за редакция на номенклатура на групи услуги по направление на дейност в МВР
        /// </summary>
        /// <param name="groupID">Идентификатор на групата</param>
        /// <param name="model">Номенклатура на групи услуги по направление на дейност в МВР</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("ServiceGroups/{groupID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            int groupID, 
            ServiceGroup model, 
            CancellationToken cancellationToken)
        {
            model.GroupID = groupID;
            var result = await  _serviceGroupService.UpdateAsync(model, cancellationToken);
            return OperationResult(result);
        }

        /// <summary>
        /// Операция за активиране/деактивиране на номенклатура на групи услуги по направление на дейност в МВР
        /// </summary>
        /// <param name="groupID">Идентификатор на групата</param>
        /// <param name="status">Статус</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("ServiceGroups/{groupID}/Status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> StatusChange(
            int groupID, 
            Status status, 
            CancellationToken cancellationToken)
        {
            var result = await _serviceGroupService.StatuChangeAsync(groupID, status, cancellationToken);
            return OperationResult(result);
        }

        /// <summary>
        /// Операция за създаване на номенклатура на превод на група от чужд език.
        /// </summary>
        /// <param name="lang">Код на език.</param>
        /// <param name="groupID">Идентификатор на групата.</param>
        /// <param name="model">Номенклатура на превод на група от чужд език.</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ServiceGroups/{lang}/{groupID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTranslation(
            string lang,
            int groupID,
            ServiceGroupTranslation model, 
            CancellationToken cancellationToken)
        {
            model.GroupID = groupID;
            var result = await _serviceGroupService.CreateAsync(model, lang, cancellationToken);
            return OperationResult(result);
        }

        /// <summary>
        /// Операция за редакция на номенклатура на превод на група от чужд език.
        /// </summary>
        /// <param name="lang">Код на език.</param>
        /// <param name="groupID">Идентификатор на групата.</param>
        /// <param name="model">Номенклатура на превод на група от чужд език.</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("ServiceGroups/{lang}/{groupID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTranslation(
            string lang,
            int groupID, 
            ServiceGroupTranslation model, 
            CancellationToken cancellationToken)
        {            
            model.GroupID = groupID;
            var result = await _serviceGroupService.UpdateAsync(model, lang, cancellationToken);
            return OperationResult(result);
        }
    }
}