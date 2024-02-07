using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EAU.Nomenclatures.Models;
using EAU.Nomenclatures.Repositories;
using EAU.Web.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EAU.Web.Admin.App.Controllers
{
    public class LanguagesController : BaseNomenclatureApiController
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguagesController(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата за езици.
        /// </summary>
        /// <param name="cancellationToken">Токен по отказване.</param>       
        /// <returns>Номенклатура за език.</returns>
        [HttpGet]
        [Route("Languages")]
        [ProducesResponseType(typeof(IEnumerable<Language>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchLanguages(CancellationToken cancellationToken)
        {
            var res = await _languageRepository.SearchInfoAsync(new LanguageSearchCriteria(), cancellationToken);
            return Ok(res.Data);
        }

        /// <summary>
        /// Операция за активиране/деактивиране на номенклатура на допустимите езици за локализация на системата. 
        /// </summary>
        /// <param name="languageID">Идентификатор на запис за език</param>
        /// <param name="status">Статус</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Languages/{languageID}/Status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> StatusChange(int languageID, Status status, CancellationToken cancellationToken)
        {
            await _languageRepository.UpdateAsync(
                new Language()
                {
                    LanguageID = languageID,
                    IsActive = status == Status.Activate ? true : false
                }, cancellationToken);

            return Ok();
        }
    }
}