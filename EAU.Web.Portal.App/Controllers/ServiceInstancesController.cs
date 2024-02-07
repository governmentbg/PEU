using EAU.Audit;
using EAU.Audit.Models;
using EAU.Common;
using EAU.Security;
using EAU.Services.ServiceInstances;
using EAU.Web.Mvc;
using EAU.Web.Portal.App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.EPortal.Clients;
using WAIS.Integration.EPortal.Models;

namespace EAU.Web.Portal.App.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с инстанции на услуги.
    /// </summary>    
    [Route("api/Services/Instances")]
    [Produces("application/json")]
    [Authorize]
    public class ServiceInstancesController : BaseApiController
    {
        private readonly IServiceInstanceService _serviceInstanceService;
        private readonly IEAUUserAccessor _userAccessor;

        public ServiceInstancesController(IServiceInstanceService serviceInstanceService, IEAUUserAccessor userAccessor)
        {
            _serviceInstanceService = serviceInstanceService;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Операция за изчитане на инстанция на услуги.
        /// </summary>
        /// <param name="criteria">Критерии за търсене.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Инстанци на услуги.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ServiceInstance>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] EAU.Services.ServiceInstances.Models.ServiceInstanceSearchCriteria criteria, CancellationToken cancellationToken)
        {
            criteria.ApplicantID = _userAccessor.User.LocalClientID;
            var result = await _serviceInstanceService.SearchAsync(criteria, cancellationToken);

            var mappedRes = Mapper.Map<List<ServiceInstance>>(result);

            return PagedResult(mappedRes, criteria.ExtractState());
        }

        /// <summary>
        /// Операция за изчитане на подробна информавия за инстанция на услуги.
        /// </summary>
        /// <param name="serviceInstanceID">Идентификатор на инстанция на услуга.</param>
        /// <param name="documentService">Сервиз за достъп до сокументи по услигата</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Подробна информация за инстанци на услуги.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ServiceInstanceInfo), StatusCodes.Status200OK)]
        [Route("{serviceInstanceID}/Info")]
        public async Task<IActionResult> GetInfo(long serviceInstanceID,
            [FromServices] IDocumentServiceClient documentService, CancellationToken cancellationToken)
        {
            var serviceInstance = (await _serviceInstanceService.SearchAsync(new Services.ServiceInstances.Models.ServiceInstanceSearchCriteria()
            {
                ServiceInstanceIDs = new List<long>() { serviceInstanceID }
            }, cancellationToken)).SingleOrDefault();

            EnsureExistingServiceInstanceAndCheckAccess(serviceInstanceID, serviceInstance);

            var serviceInstanceInfo = await documentService.GetServiceInstanceAsync(new URI(serviceInstance.CaseFileURI), cancellationToken);

            return Ok(serviceInstanceInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceInstanceID">Идентификатор на инстанция на услуга.</param>
        /// <param name="documentService">Сервиз за достъп до сокументи по услигата</param>
        /// <param name="auditService">Сервиз за одит.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Данни за преписка.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(CaseFileInfo), StatusCodes.Status200OK)]
        [Route("{serviceInstanceID}/CaseFile")]
        public async Task<IActionResult> GetCaseFile(long serviceInstanceID,
            [FromServices] IDocumentServiceClient documentService, [FromServices] IAuditService auditService, CancellationToken cancellationToken)
        {
            var serviceInstance = (await _serviceInstanceService.SearchAsync(new Services.ServiceInstances.Models.ServiceInstanceSearchCriteria()
            {
                ServiceInstanceIDs = new List<long>() { serviceInstanceID }
            }, cancellationToken)).SingleOrDefault();

            EnsureExistingServiceInstanceAndCheckAccess(serviceInstanceID, serviceInstance);

            var caseFileInfo = await documentService.GetCaseFileAsync(new URI(serviceInstance.CaseFileURI), cancellationToken);

            var logAction = new LogAction()
            {
                ObjectType = ObjectTypes.ServiceCaseFile,
                ActionType = ActionTypes.Preview,
                Functionality = Common.Models.Functionalities.Portal,
                UserID = _userAccessor.User.LocalClientID,
                Key = caseFileInfo.CaseFileURI.ToString(),
                LoginSessionID = _userAccessor.User.LoginSessionID,
                IpAddress = _userAccessor.RemoteIpAddress.GetAddressBytes(),
                AdditionalData = new Utilities.AdditionalData()
            };
            logAction.AdditionalData.Add("NomService.Name", serviceInstance.NomService.Name);

            await auditService.CreateLogActionAsync(logAction, cancellationToken);

            return Ok(caseFileInfo);
        }

        [HttpGet]
        [Route("{serviceInstanceID}/Documents/{documentURI}")]
        public async Task<IActionResult> GetDocument(long serviceInstanceID, string documentURI, CancellationToken cancellationToken)
        {
            var document = await _serviceInstanceService.DownloadDocumentContentAsync(serviceInstanceID, documentURI, cancellationToken);

            return new FileStreamResult(document.Result, new MediaTypeHeaderValue("application/xml"))
            {
                FileDownloadName = documentURI + ".xml"
            };
        }

        private void EnsureExistingServiceInstanceAndCheckAccess(long serviceInstanceID, Services.ServiceInstances.Models.ServiceInstance serviceInstance)
        {
            if (serviceInstance == null)
            {
                throw new NoDataFoundException(serviceInstanceID.ToString(), "ServiceInstance");
            }

            if (serviceInstance.ApplicantID != _userAccessor.User.LocalClientID)
            {
                throw new AccessDeniedException(serviceInstanceID.ToString(), "ServiceInstance", _userAccessor.User.LocalClientID);
            }
        }
    }
}
