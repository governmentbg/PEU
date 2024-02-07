using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EAU.CMS;
using EAU.CMS.Models;
using EAU.CMS.Repositories;
using EAU.Nomenclatures;
using EAU.Nomenclatures.Models;
using EAU.Nomenclatures.Repositories;
using EAU.Web.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EAU.Web.Admin.App.Controllers
{
    /// <summary>
    /// Контролер за работа със страници с html съдържание.
    /// </summary>
    public class PagesController : BaseApiController
    {
        private readonly IPageService _pageService;
        private readonly IPageRepository _pageRepository;
        private readonly IPageTranslationRepository _translationRepository;
        private readonly ILanguages _languages;

        public PagesController(
            IPageService pageService,
            IPageRepository pageRepository,
            IPageTranslationRepository translationRepository,
            ILanguages languages)
        {
            _pageService = pageService;
            _pageRepository = pageRepository;
            _translationRepository = translationRepository;
            _languages = languages;
        }

        /// <summary>
        /// Операция за изчитане на страници с html съдържание.
        /// </summary>
        /// <param name="lang">Език.</param>
        /// <param name="forceTranslated">Флаг, указващ дали да бъдат заредени стойностости от превод, независимо дали има такъв.</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>       
        /// <returns>Страници с html съдържание.</returns>
        [HttpGet]
        [Route("{lang?}")]
        [ProducesResponseType(typeof(IEnumerable<Page>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPages(string lang, bool? forceTranslated, CancellationToken cancellationToken)
        {
            await _languages.EnsureLoadedAsync(cancellationToken);
            int? langID = _languages.GetLanguageOrDefault(lang).LanguageID;

            var res = await _pageRepository.SearchInfoAsync(new PageSearchCriteria() { LanguageID = langID, ForceTranslated = forceTranslated }, cancellationToken);
            return Ok(res.Data);
        }

        /// <summary>
        /// Операцип за редакция на страница с html съдържание.
        /// </summary>
        /// <param name="pageID">Идентификатор на страница с html съдържание</param>
        /// <param name="model">Страница с html съдържание.</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        [HttpPut]
        [Route("Pages/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            int pageID, 
            Page model, 
            CancellationToken cancellationToken)
        {
            model.PageID = pageID;
            var result = await  _pageService.UpdateAsync(pageID , model.Title, model.Content, cancellationToken);
            return OperationResult(result);
        }

        /// <summary>
        /// Операция за създаване на номенклатура на превод на страница от чужд език.
        /// </summary>
        /// <param name="lang">Код на език.</param>
        /// <param name="pageID">Идентификатор на страницата.</param>
        /// <param name="model">Номенклатура на превод на страница от чужд език.</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        [HttpPost]
        [Route("Pages/{lang}/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTranslation(
            string lang,
            int pageID,
            PageTranslation model, 
            CancellationToken cancellationToken)
        {
            model.LanguageID = _languages.GetLanguageID(lang);
            model.PageID = pageID;

            await _translationRepository.CreateAsync(model, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Операция за редакция на номенклатура на превод на страница от чужд език.
        /// </summary>
        /// <param name="lang">Код на език.</param>
        /// <param name="pageID">Идентификатор на страницата.</param>
        /// <param name="model">Номенклатура на превод на страница от чужд език.</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        [HttpPut]
        [Route("Pages/{lang}/{pageID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTranslation(
            string lang,
            int pageID, 
            PageTranslation model, 
            CancellationToken cancellationToken)
        {
            model.LanguageID = _languages.GetLanguageID(lang);
            model.PageID = pageID;

            await _translationRepository.UpdateAsync(model, cancellationToken);
            return Ok();
        }
    }
}