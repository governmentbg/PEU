using CNSys;
using EAU.Common;
using EAU.Security;
using EAU.Services.DocumentProcesses;
using EAU.Web.Api.Models.DocumentProcesses;
using EAU.Web.FileUploadProtection;
using EAU.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.EPortal.Clients;
using WAIS.Integration.EPortal.Models;
using DomModels = EAU.Services.DocumentProcesses.Models;

namespace EAU.Web.Api.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с DocumentProcess.
    /// </summary>    
    [Route("api/Services/DocumentProcesses")]
    [Authorize]
    public class DocumentProcessesController : BaseApiController
    {
        /// <summary>
        /// Операция за изчитане на процес по заявяване.
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>
        /// <param name="loadAllData">Флаг оказващ дали да зареди всички данни за процес по заявяване.</param>
        /// <param name="cancellationToken">Токен за отказване.</param>
        /// <param name="service">Клиент за работа с докуемнтен процес.</param>
        /// <param name="userAccessor">Интерфейс за достъп до EAU потребител.</param>
        /// <returns>Процес по заявяване.</returns>
        [Route("{processID}")]
        [HttpGet]
        [ProducesResponseType(typeof(DocumentProcess), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromRoute] long processID, [FromQuery] bool? loadAllData, CancellationToken cancellationToken, [FromServices] IDocumentProcessService service, [FromServices] IEAUUserAccessor userAccessor)
        {
            if (userAccessor.User.IsUserIdentifiable != true)
            {
                BadRequest("GL_00011_E", "GL_00011_E");
            }

            var process = (await service.SearchAsync(new DomModels.DocumentProcessSearchCriteria()
            {
                DocumentProcessID = processID,
                ApplicantCIN = userAccessor.User.CIN,
                LoadOption = new DomModels.DocumentProcessLoadOption()
                {
                    LoadAttachedDocument = loadAllData,
                    LoadFormJsonContent = loadAllData
                }
            }, cancellationToken)).SingleOrDefault();

            if (process == null)
            {
                throw new NoDataFoundException(processID.ToString(), "DocumentProcess");
            }

            return Ok(Mapper.Map<DocumentProcess>(process));
        }

        /// <summary>
        /// Операция за изчитане на процес по заявяване.
        /// </summary>
        /// <param name="serviceID">Тип на заявление.</param>
        /// <param name="removingIrregularitiesInstructionURI"></param>
        /// <param name="additionalApplicationURI"></param>
        /// <param name="caseFileURI"></param>
        /// <param name="withdrawService"></param>
        /// <param name="cancellationToken">Токен за отказване.</param>
        /// <param name="service">Клиент за работа с докуемнтен процес.</param>
        /// <param name="userAccessor">Интерфейс за достъп до EAU потребител.</param>
        /// <returns>Процес по заявяване.</returns>
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(DocumentProcess), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByServiceIDAsync([FromQuery] int serviceID,
            [FromQuery] string removingIrregularitiesInstructionURI,
            [FromQuery] string additionalApplicationURI,
            [FromQuery] string caseFileURI,
            [FromQuery] bool withdrawService,
            CancellationToken cancellationToken,
            [FromServices] IDocumentProcessService service,
            [FromServices] IEAUUserAccessor userAccessor)
        {
            if (userAccessor.User?.IsUserIdentifiable != true)
            {
                BadRequest("GL_00011_E", "GL_00011_E");
            }

            //TODO- Validate
            //REQ_PEAU_0232 

            var docProcessType = service.GetDocumentProcessType(removingIrregularitiesInstructionURI, additionalApplicationURI, caseFileURI, null, serviceID, withdrawService);
            var process = (await service.SearchAsync(new DomModels.DocumentProcessSearchCriteria()
            {
                ServiceID = serviceID,
                ApplicantCIN = userAccessor.User.CIN,
                DocumentProcessType = docProcessType,
                LoadOption = new DomModels.DocumentProcessLoadOption()
                {
                    LoadAttachedDocument = true,
                    LoadFormJsonContent = true
                }
            }, cancellationToken)).SingleOrDefault();
            if (process != null)
            {
                var processVM = Mapper.Map<DocumentProcess>(process);
                processVM.HasChangesInApplicationsNomenclature = await service.HasChangesInApplicationsNomenclatureAsync(process);
                var applicantPID = processVM.AdditionalData.ContainsKey("applicantEGN") ? processVM.AdditionalData["applicantEGN"] : processVM.AdditionalData["applicantLNCh"];
                var newApplicantPID = userAccessor.User.PersonIdentifier;
                processVM.HasChangedApplicant = applicantPID != newApplicantPID;

                if ((processVM.Status == DomModels.ProcessStatuses.InProcess || processVM.Status == DomModels.ProcessStatuses.Signing) &&
                    (processVM.HasChangedApplicant || processVM.HasChangesInApplicationsNomenclature))
                {
                    processVM.Form = null;
                }

                return Ok(processVM);
            }

            return Ok();
        }

        /// <summary>
        /// Операция за създаване на процес по заявяване.
        /// </summary>
        /// <param name="processRequest">Заявка за създаване на процес по заявяване.</param>
        /// <param name="service">Клиент за работа с докуемнтен процес.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Процес по заявяване.</returns>
        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(DocumentProcess), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] NewProcessRequest processRequest, [FromServices] IDocumentProcessService service, CancellationToken cancellationToken)
        {
            //TODO: certificate Check
            //return BadRequest("GL_NEED_SERTIFICATE_AUTHENTICATION_Е", "GL_NEED_SERTIFICATE_AUTHENTICATION_Е");

            var result = await service.StartAsync(processRequest, cancellationToken);

            return OperationResult<DomModels.DocumentProcess, DocumentProcess>(result);
        }

        /// <summary>
        /// Операция за създаване на процес по преглед на документ.
        /// </summary>
        /// <param name="file">Файлово съдържание, на документ за преглед.</param>
        /// <param name="service">Клиент за работа с докуемнтен процес.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Процес по заявяване.</returns>
        [Route("Preview")]
        [HttpPost]
        [ProducesResponseType(typeof(DocumentProcess), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePreviewAsync(IFormFile file, [FromServices] IDocumentProcessService service, CancellationToken cancellationToken)
        {
            OperationResult<DomModels.DocumentProcess> result = null;

            using (var docXmlStream = file.OpenReadStream())
            {
                result = await service.StartAsync(new NewProcessRequest()
                {
                    DocumentXMLContent = docXmlStream
                }, cancellationToken);
            }

            return OperationResult<DomModels.DocumentProcess, DocumentProcess>(result);
        }

        /// <summary>
        /// Операция за изтриване на процес по заявяване.
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>      
        /// <param name="service">Клиент за работа с докуемнтен процес.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        [Route("{processID}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync([FromRoute] long processID, [FromServices] IDocumentProcessService service, CancellationToken cancellationToken)
        {
            await service.DeleteAsync(processID, cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Операция за обновяване на съдържанието на документа.
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>
        /// <param name="docContent">Съдържание на документа.</param>
        /// <param name="service">Клиент за работа с докуемнтен процес.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        [Route("{processID}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveDocumentFormAsync([FromRoute] long processID, [FromBody] JsonElement docContent, [FromServices] IDocumentProcessService service, CancellationToken cancellationToken)
        {
            var result = await service.UpdateFormAsync(processID, docContent.GetRawText(), cancellationToken);

            return OperationResult(result);
        }

        /// <summary>
        /// Операция за обновяване на съдържанието на документа.
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>
        /// <param name="service">Клиент за работа с докуемнтен процес.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        [Route("ReturnToInProcessStatus/{processID}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ReturnToInProcessStatusAsync([FromRoute] long processID, [FromServices] IDocumentProcessService service, CancellationToken cancellationToken)
        {
            var result = await service.ReturnToInProcessStatusAsync(processID, cancellationToken);

            return OperationResult(result);
        }

        /// <summary>
        /// Операция за изтриване на процес по заявяване.
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>      
        /// <param name="service">Клиент за работа с докуемнтен процес.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        [Route("{processID}/StartSigning")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> StartSigningAsync([FromRoute] long processID, [FromServices] IDocumentProcessSigningService service, CancellationToken cancellationToken)
        {
            var result = await service.StartSigningAsync(processID, cancellationToken);

            return OperationResult(result);
        }

        /// <summary>
        /// Връща Html на документ
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>      
        /// <param name="service">Клиент за работа с докуемнтен процес.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Html на документ</returns>
        [Route("{processID}/Document/Html")]
        [HttpGet]
        public async Task<FileStreamResult> GetHtmlDocument([FromRoute] long processID, [FromServices] IDocumentProcessFormService service, CancellationToken cancellationToken)
        {
            var docStream = await service.GenerateFormHtmlContentAsync(processID, HttpContext.Request.PathBase, cancellationToken);

            return new FileStreamResult(docStream.Result, new MediaTypeHeaderValue("text/html"))
            {
                FileDownloadName = "print.html"
            };
        }

        /// <summary>
        /// Връща Xml на документ
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>      
        /// <param name="service">Клиент за работа с докуемнтен процес.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Html на документ</returns>
        [Route("{processID}/Document/Xml")]
        [HttpGet]
        public async Task<FileStreamResult> GetXmlDocument([FromRoute] long processID,
            [FromServices] IDocumentProcessFormService service, CancellationToken cancellationToken)
        {
            var docStream = await service.DownloadDocumentContent(processID, cancellationToken);

            return new FileStreamResult(docStream.Result, new MediaTypeHeaderValue("application/xml"))
            {
                FileDownloadName = "document.xml"
            };
        }

        /// <summary>
        /// Операция за изчитане на номенклатурите от WAIS
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>
        /// <param name="cancellationToken">Токен за отказване.</param>
        /// <param name="service">Клиент за работа с докуемнтен процес.</param>
        /// <param name="waisIntegrationServiceClientsFactory">Фактори за изчитане на номенклатурите от WAIS</param>
        /// <param name="userAccessor">userAccessor</param>
        /// <returns>Процес по заявяване.</returns>
        [Route("{processID}/Nomenclatures")]
        [HttpGet]
        [ProducesResponseType(typeof(List<NomenclatureItem>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNomenclaturesAsync([FromRoute] long processID, CancellationToken cancellationToken,
            [FromServices] IDocumentProcessService service,
            [FromServices] IWAISIntegrationServiceClientsFactory waisIntegrationServiceClientsFactory, [FromServices] IEAUUserAccessor userAccessor)
        {
            var process = (await service.SearchAsync(new DomModels.DocumentProcessSearchCriteria()
            {
                DocumentProcessID = processID,
                LoadOption = new DomModels.DocumentProcessLoadOption()
                {
                    LoadAttachedDocument = false,
                    LoadFormJsonContent = false
                }
            }, cancellationToken)).SingleOrDefault();

            if (process == null)
            {
                throw new NoDataFoundException(processID.ToString(), "DocumentProcess");
            }

            if (process.ApplicantID != userAccessor.User.LocalClientID)
            {
                throw new AccessDeniedException(processID.ToString(), "DocumentProcess", userAccessor.User.LocalClientID);
            }

            if (process != null && process.AdditionalData.ContainsKey("NomenclatureURL"))
            {
                var nomenclatureUrl = process.AdditionalData["NomenclatureURL"];
                var nomenclatures = await waisIntegrationServiceClientsFactory.GetNomenclaturesClient().GetNomenclaturesAsync(nomenclatureUrl, cancellationToken);

                return Ok(nomenclatures);
            }

            return Ok();
        }

        #region AttachedDocuments

        /// <summary>
        /// Операзия за изтриване на прикачен документ.
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>    
        /// <param name="docID">Идентификатор на документ.</param>
        /// <param name="service">Клиент за работа с прикачени документи.</param>
        /// <param name="cancellationToken">Токен по отказване.</param> 
        [Route("{processID}/AttachedDocuments/{docID}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAttachedDocumentAsync([FromRoute] long processID, [FromRoute] long docID, [FromServices] IDocumentProcessAttachedDocumentService service, CancellationToken cancellationToken)
        {
            await service.DeleteAttachedDocumentAsync(processID, docID, cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Операция за добавяне на прикачен документ.
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>      
        /// <param name="attachedDocument">Документ.</param>
        /// <param name="file">Файлово съдържание.</param>
        /// <param name="service">Клиент за работа с прикачени документи.</param>
        /// <param name="configuration">Конфигурация на приложението.</param>
        /// <param name="documentProcessService">услуга за работа с документни процеси</param>
        /// <param name="stringLocalizer"></param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Документ.</returns>
        [Route("{processID}/AttachedDocuments/Upload")]
        [HttpPost]
        [UploadedFileContentValidationFilter]
        [ProducesResponseType(typeof(DomModels.AttachedDocument), StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadAttachedDocumentAsync(
            [FromRoute] long processID,
            [FromForm] DomModels.AttachedDocument attachedDocument,
            IFormFile file,
            [FromServices] IDocumentProcessAttachedDocumentService service,
            [FromServices] IConfiguration configuration,
            [FromServices] IDocumentProcessService documentProcessService,
            [FromServices] IStringLocalizer stringLocalizer,
            CancellationToken cancellationToken)
        {
            if (file.Length > (configuration.GetEAUSection().GetValue<int>("GL_DOCUMENT_MAX_FILE_SIZE") * 1024))
            {
                return this.StatusCode(413, stringLocalizer["GL_DOCUMENT_MAX_FILE_SIZE_EXCEEDED_E"].Value
                    .Replace("{FILE_SIZE_IN_KB}", (file.Length / 1024).ToString() + " kB")
                    .Replace("{MAX_FILE_SIZE}", (configuration.GetEAUSection().GetValue<int>("GL_DOCUMENT_MAX_FILE_SIZE")).ToString() + " kB"));
            }

            var currentDocumentsTotalSize = (await documentProcessService
                .SearchDocumentProcessContentsAsync(new DomModels.DocumentProcessContentSearchCriteria()
                {
                    DocumentProcessIDs = new List<long>() { processID },
                    LoadOption = new DomModels.DocumentProcessContentLoadOption() { LoadContent = false },
                    Type = DomModels.DocumentProcessContentTypes.AttachedDocument
                }, cancellationToken)).Select(d => d.Length).Sum();

            if ((currentDocumentsTotalSize + file.Length) > (configuration.GetEAUSection().GetValue<int>("GL_APPLICATION_MAX_SIZE") * 1024))
            {
                return BadRequest("APPLICATION_MAX_SIZE_LIMIT_CODE_WITHOUT_LABEL"
                    , stringLocalizer["GL_APPLICATION_MAX_SIZE1_E"].Value
                    .Replace("{FILE_SIZE_IN_KB}", (file.Length / 1024).ToString())
                    .Replace("{MAX_APPLICATION_SIZE}", (configuration.GetEAUSection().GetValue<int>("GL_APPLICATION_MAX_SIZE").ToString())));
            }

            using (var content = file.OpenReadStream())
            {
                attachedDocument.FileName = file.FileName;
                attachedDocument.MimeType = file.ContentType;

                await service.AddAttachedDocumentAsync(processID, attachedDocument, content, cancellationToken);
            }

            return Ok(attachedDocument);
        }

        #region Attached document templates

        /// <summary>
        /// Операция за създаване на прикачена шаблонна декларация към заявление.
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>
        /// <param name="attachedDocument">Прикачен документ.</param>
        /// <param name="service">Клиент за работа с прикачени документи.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        [Route("{processID}/AttachedDocuments/")]
        [HttpPost]
        [ProducesResponseType(typeof(DomModels.AttachedDocument), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAttachedDocumentTemplateAsync(
            [FromRoute] long processID
            , [FromBody] DomModels.AttachedDocument attachedDocument
            , [FromServices] IDocumentProcessAttachedDocumentService service
            , CancellationToken cancellationToken)
        {
            var res = await service.AddAttachedDocumentAsync(processID, attachedDocument, null, cancellationToken);

            return OperationResult(res);
        }

        /// <summary>
        /// Операцията обновява данните за прикачен документ.
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>
        /// <param name="docID">Идентификатор на документ.</param>
        /// <param name="attachedDocument">Прикачен документ.</param>
        /// <param name="service">Клиент за работа с прикачени документи.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        [Route("{processID}/AttachedDocuments/{docID}")]
        [HttpPost]
        [ProducesResponseType(typeof(DomModels.AttachedDocument), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAttachedDocumentTemplateAsync(
            [FromRoute] long processID
            , [FromRoute] long docID
            , [FromBody] DomModels.AttachedDocument attachedDocument
            , [FromServices] IDocumentProcessAttachedDocumentService service
            , CancellationToken cancellationToken)
        {
            await service.UpdateAttachedDocumentAsync(processID, attachedDocument, null, cancellationToken);

            return Ok();
        }

        #region Signing Attached Document Template

        /// <summary>
        /// Операцията стартира процес по подписване на шаблонна декларация приложена към заявление.
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>
        /// <param name="docID">Идентификатор на документ.</param>
        /// <param name="service">Клиент за работа с прикачени документи.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        [Route("{processID}/AttachedDocuments/{docID}/StartSigning")]
        [HttpPost]
        [ProducesResponseType(typeof(DomModels.AttachedDocument), StatusCodes.Status200OK)]
        public async Task<IActionResult> StartSigningAttachedDocumentTemplateAsync(
            [FromRoute] long processID
            , [FromRoute] long docID
            , [FromServices] IDocumentProcessSigningService service
            , CancellationToken cancellationToken)
        {
            var res = await service.StartSigningAttachedDocumentAsync(processID, docID, cancellationToken);

            return OperationResult(res);
        }

        #endregion

        #endregion

        /// <summary>
        /// Операзия за изтегляне на прикачен документ.
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>    
        /// <param name="docID">Идентификатор на документ.</param>
        /// <param name="service">Клиент за работа с прикачени документи.</param>
        /// <param name="cancellationToken">Токен по отказване.</param> 
        [Route("{processID}/AttachedDocuments/{docID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DownloadAttachedDocumentAsync([FromRoute] long processID, [FromRoute] long docID, [FromServices] IDocumentProcessAttachedDocumentService service, CancellationToken cancellationToken)
        {
            var document = (await service.SearchAttachedDocumentsAsync(new DomModels.AttachedDocumentSearchCriteria()
            {
                DocumentProcessID = processID,
                AttachedDocumentID = docID,
                LoadOption = new DomModels.AttachedDocumentLoadOption()
                {
                    LoadContent = true
                }
            }, cancellationToken)).SingleOrDefault();

            if (document != null)
            {
                //TOTO: Add audit log if necessary

                return File(document.Content.Content, document.MimeType, document.FileName);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Oперацията връща прикачените документи за processID.
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>
        /// <param name="service">Клиент за работа с прикачени документи.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        [Route("{processID}/AttachedDocuments")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DomModels.AttachedDocument>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchAttachedDocumentsAsync([FromRoute] long processID, [FromServices] IDocumentProcessAttachedDocumentService service, CancellationToken cancellationToken)
        {
            var documents = (await service.SearchAttachedDocumentsAsync(new DomModels.AttachedDocumentSearchCriteria()
            {
                DocumentProcessID = processID
            }, cancellationToken)).ToList();

            return Ok(documents);
        }

        #endregion
    }
}
