using CNSys.Xml;
using EAU.Documents;
using EAU.Services.DocumentProcesses;
using EAU.Web.Api.Private.Models;
using EAU.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EAU.Web.Api.Private.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за валидиране на документи.
    /// </summary>
    [Route("DocumentValidation")]
    [Authorize(Policy = "BackOfficeIntegrationApiPolicy")]
    public class DocumentValidationController : BaseApiController
    {
        /// <summary>
        /// Валидира документ по xml-а му.
        /// </summary>
        /// <param name="xmlDocument">XML string</param>
        /// <param name="documentProcessFormService">IDocumentProcessFormService</param>
        /// <param name="documentServicesProvider">IDocumentFormServiceProvider</param>
        /// <returns>DocumentValidationResult</returns>
        [Route("Validate")]
        [HttpPost]        
        [ProducesResponseType(typeof(DocumentValidationResult), StatusCodes.Status200OK)]
        public IActionResult Validate(
            [FromBody] string xmlDocument
            , [FromServices] IDocumentProcessFormService documentProcessFormService
            , [FromServices] IDocumentFormServiceProvider documentServicesProvider)
        {
            var xml = XmlHelpers.CreateXmlDocument(xmlDocument);
            var documentTypeUri = documentProcessFormService.GetDocumentTypeURI(xml);

            IDocumentFormService docService = documentServicesProvider.GetDocumentFormService(documentTypeUri);
            object docForm = docService.DeserializeDomainForm(xml);

            IDocumentFormValidationService documentFormValidationService = documentServicesProvider.GetDocumentFormValidationService(documentTypeUri);
            CNSys.IErrorCollection valResult = documentFormValidationService.ValidateDomainForm(docForm);

            DocumentValidationResult result = new DocumentValidationResult()
            {
                IsValid = valResult.HasErrors ? false : true,
                Errors = new List<DocumentValidationResult.Error>()
            };

            if (valResult.HasErrors)
            {
                foreach (CNSys.IError item in valResult)
                {
                    result.Errors.Add(new DocumentValidationResult.Error() { Code = item.Code, Message = item.Message });
                }
            }

            return Ok(result);
        }
    }
}
