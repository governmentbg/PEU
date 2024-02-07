using EAU.Web.Mvc;
using EAU.Signing;
using EAU.Signing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Api.Controllers
{
    /// <summary>
    /// This is Evrotrust Vendor callback server API
    /// </summary>
    public class EvrotrustCallbackController : BaseApiController
    {
        #region Private members

        private IEvrotrustProcessorService _evrotrustProcessorService = null;

        #endregion

        #region Constructor

        public EvrotrustCallbackController(IEvrotrustProcessorService evrotrustProcessorService)
        {
            _evrotrustProcessorService = evrotrustProcessorService;
        }

        #endregion

        /// <summary>
        /// Document ready
        /// </summary>
        /// <param name="dataDocumentReady">Document info</param>
        /// <param name="cancellationToken">Токен за отказване</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Route("document/ready")]
        [HttpPost]
        public async Task<IActionResult> DocumentReady([FromBody]DataDocumentReady dataDocumentReady, CancellationToken cancellationToken)
        {
            await _evrotrustProcessorService.AcceptRemoteCallbackNotificationAsync(dataDocumentReady.TransactionID, dataDocumentReady.Status, cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Document group ready
        /// </summary>
        /// <param name="dataDocumentGroupReady">Document info</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Route("document/group/ready")]
        [HttpPost]
        public IActionResult DocumentGroupReady([FromBody]DataDocumentGroupReady dataDocumentGroupReady)
        {
            throw new NotImplementedException();
        }

    }
}
