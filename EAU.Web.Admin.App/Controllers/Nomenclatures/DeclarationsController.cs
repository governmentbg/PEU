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
    public class DeclarationsController : BaseNomenclatureApiController
    {
        private readonly IDeclarationService _declarationService;
        private readonly IDeclarationRepository _declarationRepository;

        public DeclarationsController(IDeclarationService declarationService, IDeclarationRepository declarationRepository)
        {
            _declarationService = declarationService;
            _declarationRepository = declarationRepository;
        }


        /// <summary>
        /// Операция за изчитане на номенклатурата на декларативни обстоятелства и политики.
        /// </summary>
        /// <param name="cancellationToken">Токен по отказване</param>       
        /// <returns>Номенклатура за декларативни обстоятелства и политики.</returns>
        [HttpGet]
        [Route("Declarations")]
        [ProducesResponseType(typeof(IEnumerable<Declaration>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchDeclarations(CancellationToken cancellationToken)
        {
            var res = await _declarationRepository.SearchInfoAsync(new DeclarationSearchCriteria(), cancellationToken);
            return Ok(res.Data.ToList());
        }

        /// <summary>
        /// Операция за създаване на номенклатура на декларативно обстоятелство/ политика.
        /// </summary>
        /// <param name="model">Номенклатура на декларативни обстоятелства и политики</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Declarations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(
            Declaration model, 
            CancellationToken cancellationToken)
        {
            var result = await _declarationService.CreateAsync(model, cancellationToken);
            return OperationResult(result);
        }

        /// <summary>
        /// Операция за редакция на номенклатура на декларативно обстоятелство/ политика.
        /// </summary>
        /// <param name="declarationID">Идентификатор на декларативно обстоятелство/ политика</param>
        /// <param name="model">Номенклатура на декларативни обстоятелства и политики</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Declarations/{declarationID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            int declarationID, 
            Declaration model, 
            CancellationToken cancellationToken)
        {
            model.DeclarationID = declarationID;
            var result = await _declarationService.UpdateAsync(model, cancellationToken);
            return OperationResult(result);
        }

        /// <summary>
        /// Операция за изтриване на номенклатура на декларативно обстоятелство/ политика.
        /// </summary>
        /// <param name="declarationID">Идентификатор на декларативно обстоятелство/ политика</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Declarations/{declarationID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int declarationID, CancellationToken cancellationToken)
        {
            var result = await _declarationService.DeleteAsync(declarationID, cancellationToken);
            return OperationResult(result);
        }
    }
}