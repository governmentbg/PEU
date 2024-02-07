using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EAU.Nomenclatures.Models;
using EAU.Nomenclatures.Repositories;
using EAU.Nomenclatures.Services;
using EAU.Web.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EAU.Web.Admin.App.Controllers
{
    public class DocumentTemplatesController : BaseNomenclatureApiController
    {
        private readonly IDocumentTemplateRepository _documentTemplateRepository;
        private readonly IDocumentTemplateService _documentTemplateService;

        public DocumentTemplatesController(
            IDocumentTemplateRepository documentTemplateRepository,
            IDocumentTemplateService documentTemplateService)
        {
            _documentTemplateRepository = documentTemplateRepository;
            _documentTemplateService = documentTemplateService;
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата на шаблони за документи.
        /// </summary>
        /// <param name="cancellationToken">Токен по отказване.</param>       
        /// <returns>Номенклатура на шаблони за документи.</returns>
        [HttpGet]
        [Route("DocumentTemplates")]
        [ProducesResponseType(typeof(IEnumerable<DocumentTemplate>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchDocumentTemplates(CancellationToken cancellationToken)
        {
            var res = await _documentTemplateRepository.SearchInfoAsync(new DocumentTemplateSearchCriteria(), cancellationToken);
            return Ok(res.Data.ToList());
        }

        /// <summary>
        /// Операция за създаване на номенклатура на шаблон за документ.
        /// </summary>
        /// <param name="model">Номенклатура на шаблон за документ</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("DocumentTemplates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(
            DocumentTemplate model, 
            CancellationToken cancellationToken)
        {
            var result = await _documentTemplateService.CreateAsync(model, cancellationToken);
            return OperationResult(result);
        }

        /// <summary>
        /// Операция за редакция на номенклатура на шаблон за документ.
        /// </summary>
        /// <param name="docTemplateID">Идентификатор на запис за шаблон на документ</param>
        /// <param name="model">Номенклатура на шаблон за документ</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("DocumentTemplates/{docTemplateID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            int docTemplateID, 
            DocumentTemplate model, 
            CancellationToken cancellationToken)
        {
            model.DocTemplateID = docTemplateID;
            await _documentTemplateRepository.UpdateAsync(model, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Операция за изтриване на номенклатура на шаблон за документ.
        /// </summary>
        /// <param name="docTemplateID">Идентификатор на запис за шаблон на документ</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DocumentTemplates/{docTemplateID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(
            int docTemplateID, 
            CancellationToken cancellationToken)
        {
            await _documentTemplateRepository.DeleteAsync(docTemplateID, cancellationToken);
            return Ok();
        }
    }
}