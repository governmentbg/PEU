using EAU.Web.Mvc;
using EAU.Signing;
using EAU.Signing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Api.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class BTrustProcessorController : BaseApiController
    {
        #region Private members

        private IBTrustProcessorService _btrustProcessorService = null;

        #endregion

        #region Constructor

        public BTrustProcessorController(IBTrustProcessorService bTrustProcessorService)
        {
            _btrustProcessorService = bTrustProcessorService;
        }

        #endregion

        /// <summary>
        /// Операция за създаване на заявка към BISS за подписване.
        /// </summary>
        /// <param name="createRequest">заявка за създаване</param>
        /// <param name="cancellationToken">токен за отказване</param>
        /// <returns>JSON обект на заявка към BISS.</returns>
        [HttpPost]
        [Route("CreateBissSignRequest")]
        [ProducesResponseType(typeof(BissSignRequestExtended), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateBissSignRequest([FromBody]CreateBissSignRequest createRequest, CancellationToken cancellationToken)
        {
            var res = await _btrustProcessorService.CreateBissSignRequestAsync(createRequest.ProcessID.Value, createRequest.UserCertBase64, cancellationToken);

            return OperationResult(res);
        }

        /// <summary>
        /// Операция за приключване на локално подписване.
        /// </summary>
        /// <param name="assembleSignatureWithDocumentRequest">Данни за сглобяване на подписа от BISS със документа за подписване.</param>
        /// <param name="cancellationToken">Токен за отказване.</param>
        /// <returns>OperationResult</returns>
        [HttpPut]
        [Route("completeBissSignProcess")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CompleteBissSignProcess([FromBody]AssembleSignatureWithDocumentRequest assembleSignatureWithDocumentRequest, CancellationToken cancellationToken)
        {
            var res = await _btrustProcessorService.CompleteBissSignProcessAsync(assembleSignatureWithDocumentRequest.ProcessID.Value
                , assembleSignatureWithDocumentRequest.Base64SigningCert
                , assembleSignatureWithDocumentRequest.Base64DocSignature
                , assembleSignatureWithDocumentRequest.HashTime
                , assembleSignatureWithDocumentRequest.SignerID
                , cancellationToken);

            return OperationResult(res);
        }

        #region Test Signing

        /// <summary>
        /// Операция за създаване на заявка за тестово подписване към BISS.
        /// </summary>
        /// <param name="testBissSignRequest">Заявка за тестово подписване чрез Biss</param>
        /// <returns>JSON обект на заявка към BISS.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("createBissTestSignRequest")]
        [ProducesResponseType(typeof(BissSignRequestExtended), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTestBissSignRequest([FromBody]TestBissSignRequest testBissSignRequest)
        {
            var res = await _btrustProcessorService.CreateTestBissSignRequest(testBissSignRequest.UserCertBase64);

            return OperationResult(res);
        }

        /// <summary>
        /// Операция за приключване на тестово подписване с BISS.
        /// </summary>
        /// <param name="completeBissRequest">Данни за сглобяване на подписа от BISS със документа за подписване.</param>
        /// <returns>Флаг указващ дали тестовото подписване е успешно.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("CompleteTestBissSignProcess")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> CompleteTestBissSignProcess([FromBody]CompleteBissSignRequest completeBissRequest)
        {
            CNSys.OperationResult res = await _btrustProcessorService.CompleteTestBissSignProcess(
                completeBissRequest.Base64SigningCert
                , completeBissRequest.Base64DocSignature
                , completeBissRequest.HashTime);

            if (res.IsSuccessfullyCompleted)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest(res);
            }
        }

        #endregion

        /// <summary>
        /// Операция за създаване на заявка за отдалечено подписване.
        /// </summary>
        /// <param name="request">Данни за заявката.</param>
        /// <param name="cancellationToken">Токен за отказванер.</param>
        /// <returns>Данни за заявката в системата на Btrust.</returns>
        [HttpPost]
        [Route("createRemoteSignRequest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateRemoteSignRequest([FromBody]BtrustRemoteSignRequest request, CancellationToken cancellationToken)
        {
            var res = await _btrustProcessorService.CreateRemoteSignRequestAsync(request.ProcessID.Value, request.SignerID.Value, request.UserData, cancellationToken);

            return OperationResult(res);
        }

        /// <summary>
        /// Операция за приключване на отдалечено подписване.
        /// </summary>
        /// <param name="request">Данни за процеса по подписване.</param>
        /// <param name="cancellationToken">Токен за отказване.</param>
        /// <returns>true - при успешно подписване и ApiError при неуспех.</returns>
        [HttpPut]
        [Route("completeRemoteSigning")]
        [ProducesResponseType(typeof(BtrustPullingResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CompleteRemoteSigning([FromBody]BtrustCompleteRemoteSigning request, CancellationToken cancellationToken)
        {
            var res = await _btrustProcessorService.TryCompleteRemoteSigning(request.ProcessID.Value, request.SignerID.Value, cancellationToken);

            return OperationResult(res);
        }
    }
}
