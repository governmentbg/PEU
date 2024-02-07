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
    public class LabelsController : BaseNomenclatureApiController
    {
        private readonly ILabelRepository _labelRepository;
        private readonly ILabelTranslationRepository _translationRepository;
        private readonly ILanguages _languages;

        public LabelsController(
            ILabelRepository labelRepository,
            ILabelTranslationRepository translationRepository,
            ILanguages languages)
        {
            _labelRepository = labelRepository;
            _translationRepository = translationRepository;
            _languages = languages;
        }

        /// <summary>
        /// Операция за изчитане на номенклатурата за етикети на ресурси.
        /// </summary>
        /// <param name="lang">Език за локализация.</param>
        /// <param name="forceTranslated">Флаг, указващ дали да бъдат заредени стойностости от превод, независимо дали има такъв.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>     
        /// <returns>Номенклатура за етикети.</returns>
        [HttpGet]
        [Route("Labels/{lang?}")]
        [ProducesResponseType(typeof(IEnumerable<Label>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchLabels(string lang, bool? forceTranslated, CancellationToken cancellationToken)
        {
            await _languages.EnsureLoadedAsync(cancellationToken);
            int? langID = _languages.GetLanguageOrDefault(lang).LanguageID;

            var res = await _labelRepository.SearchInfoAsync(new LabelSearchCriteria () 
            { 
                LanguageID = langID,
                ForceTranslated = forceTranslated,
                LoadDecsription = true
            }, cancellationToken);
            return Ok(res.Data);
        }

        /// <summary>
        /// Операция за редакция на номенклатура съхраняваща преводите за ресурсите на Български език
        /// </summary>
        /// <param name="labelID">Идентификатор на етикет.</param>
        /// <param name="model">Номенклатура съхраняваща преводите за ресурсите на Български език</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Labels/{labelID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            int labelID, 
            Label model, 
            CancellationToken cancellationToken)
        {
            model.LabelID = labelID;
            await _labelRepository.UpdateAsync(model, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Операция за създаване на номенклатура на превод на ресурс от чужд език.
        /// </summary>
        /// <param name="lang">Код на език.</param>
        /// <param name="labelID">Идентификатор на етикет.</param>
        /// <param name="model">Номенклатура на превод на ресурс от чужд език.</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Labels/{lang}/{labelID}/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTranslation(
            string lang,
            int labelID,
            LabelTranslation model, 
            CancellationToken cancellationToken)
        {
            model.LanguageID = _languages.GetLanguageID(lang);
            model.LabelID = labelID;

            await _translationRepository.CreateAsync(model, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Операция за редакция на номенклатура на превод на ресурс от чужд език.
        /// </summary>
        /// <param name="lang">Код на език.</param>
        /// <param name="labelID">Идентификатор на етикет.</param>
        /// <param name="model">Номенклатура на превод на ресурс от чужд език.</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Labels/{lang}/{labelID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTranslation(
            string lang,
            int labelID, 
            LabelTranslation model, 
            CancellationToken cancellationToken)
        {
            model.LanguageID = _languages.GetLanguageID(lang);
            model.LabelID = labelID;

            await _translationRepository.UpdateAsync(model, cancellationToken);
            return Ok();
        }
    }
}