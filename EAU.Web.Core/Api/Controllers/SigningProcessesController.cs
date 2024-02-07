using EAU.Web.Mvc;
using EAU.Signing;
using EAU.Signing.Models;
using EAU.Signing.Models.SearchCriteria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Api.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с процеси за подписване.
    /// </summary>
    [Produces("application/json")]
    [Authorize]
    public partial class SigningProcessesController : BaseApiController
    {
        protected ISigningProcessesService _signingProcessesService = null;

        public SigningProcessesController(ISigningProcessesService signingProcessesService)
        {
            _signingProcessesService = signingProcessesService;
        }

        /// <summary>
        /// Операция за изчитане на процес за подписване.
        /// </summary>
        /// <param name="processID">Идентификатор на процес за подписване.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Процес за подписване.</returns>
        [HttpGet]
        [Route("{processID}")]
        [ProducesResponseType(typeof(IEnumerable<SigningProcessVM>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Search(Guid processID, CancellationToken cancellationToken)
        {
            var result = await _signingProcessesService.SearchAsync(new SigningProcessesSearchCriteria() { ProcessID = processID, LoadSigners = true }, cancellationToken);

            if (result == null)
                return Ok();

            return Ok(result.Select(p => new SigningProcessVM()
            {
                ProcessID = p.ProcessID,
                Status = p.Status,
                Signers = p.Signers.OrderBy(s => s.Order).Select(s => new SignerVM()
                {
                    SignerID = s.SignerID,
                    Name = s.Name,
                    Ident = s.Ident,
                    Order = s.Order,
                    Status = s.Status,
                    SigningChannel = s.SigningChannel,
                    RejectReson = s.RejectReson
                }).ToList()
            }));
        }

        /// <summary>
        /// Операция за отказване на подписване.
        /// </summary>
        /// <param name="processID">Идентификатор на процес.</param>
        /// <param name="signerID">Идентификатор на подписващ</param>
        /// <param name="cancellationToken">Токен за отказване</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{processID}/Signers/{signerID}/RejectSigning")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SignerRejectSigning(Guid processID, long signerID, CancellationToken cancellationToken)
        {
            var res = await _signingProcessesService.SignerRejectRemoteSigningAsync(processID, signerID, null, cancellationToken);

            return OperationResult(res);
        }

        /// <summary>
        /// Операция за отказване на процес за подписване.
        /// </summary>
        /// <param name="processID">Идентификатор на процес за подписване.</param>
        /// <param name="cancellationToken">Токен за отказване.</param>
        /// <returns>Флаг указващ дали е успешна операцията.</returns>
        [HttpPost]
        [Route("{processID}/reject")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> RejectProcess(Guid processID, CancellationToken cancellationToken)
        {
            var result = await _signingProcessesService.StartRejectingProcessAsync(processID, cancellationToken);

            return result.IsSuccessfullyCompleted ? Ok(true) : BadRequest(result);
        }
    }
}
