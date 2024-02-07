using CNSys;
using CNSys.Data;
using EAU.Common;
using EAU.Payments;
using EAU.Payments.Obligations.Models;
using EAU.Payments.PaymentRequests.Models;
using EAU.Payments.PaymentRequests.PepDaeu;
using EAU.Payments.PaymentRequests.PepDaeu.Models;
using EAU.Security;
using EAU.ServiceLimits.AspNetCore.Mvc;
using EAU.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.KAT.AND;

namespace EAU.Web.Portal.App.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с моите плащания.
    /// </summary>    
    [Route("Api/Obligations")]
    [Produces("application/json")]
    public class ObligationsController : BaseApiController
    {
        private readonly IObligationService _obligationService;
        private readonly IPaymentRequestService _paymentRequestService;
        private readonly IEAUUserAccessor _eAUUserAccessor;
        private readonly IOptionsMonitor<GlobalOptions> _optionsMonitor;
        private readonly IANDServicesClientFactory _iANDServicesClientFactory;


        public ObligationsController(
            IObligationService obligationService,
            IPaymentRequestService paymentRequestService,
            IEAUUserAccessor eAUUserAccessor,
            IOptionsMonitor<GlobalOptions> optionsMonitor,
            IANDServicesClientFactory iANDServicesClientFactory
            )
        {
            _obligationService = obligationService;
            _paymentRequestService = paymentRequestService;
            _eAUUserAccessor = eAUUserAccessor;
            _optionsMonitor = optionsMonitor;
            _iANDServicesClientFactory = iANDServicesClientFactory;
        }

        /// <summary>
        /// Операция за изчитане на задължения за моите плащания.
        /// </summary>
        /// <param name="criteria">Китерии за търсене.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Задължения.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Obligation>), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] MyPaymentsObligationSearchCriteria criteria, CancellationToken cancellationToken)
        {
            ObligationSearchCriteria serviceCriteria = new ObligationSearchCriteria()
            {
                Mode = ObligationSearchModes.MyPayments,

                ApplicantID = _eAUUserAccessor?.User?.LocalClientID,

                ServiceInstanceID = criteria.ServiceInstanceID
            };

            var state = criteria.ExtractState();
            var result = await _obligationService.SearchAsync(state, serviceCriteria, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
            {
                return OperationResult(result);
            }

            var data = result.Result.Single().Obligations;
            return PagedResult(data, state);
        }

        /// <summary>
        /// Операция за изчитане на задължения към АНД.
        /// </summary>
        /// <param name="criteria">Китерии за търсене.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Задължения.</returns>
        [HttpGet]
        [Route("AND")]
        [ServiceLimiter(ServiceCode = "AND_LIMIT")]
        [ProducesResponseType(typeof(ANDObligationSearchResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAND([FromQuery] ANDObligationSearchCriteria criteria, CancellationToken cancellationToken)
        {
            if (!criteria.Mode.HasValue
                || (criteria.Mode == ANDObligationSearchMode.ObligedPerson && 
                        (string.IsNullOrEmpty(criteria.ObligedPersonIdent) && string.IsNullOrEmpty(criteria.DrivingLicenceNumber) && string.IsNullOrEmpty(criteria.PersonalDocumentNumber) && string.IsNullOrEmpty(criteria.ForeignVehicleNumber) && string.IsNullOrEmpty(criteria.Uic)))                
                )
            {
                return OperationResult(new OperationResult("GL_INVALID_SEARCH_AND_INPUT_PARAMS_E", "GL_INVALID_SEARCH_AND_INPUT_PARAMS_E"));
            }

            var serviceCriteria = new ObligationSearchCriteria
            {
                Mode = ObligationSearchModes.AND,
                ApplicantID = _eAUUserAccessor?.User?.LocalClientID,
                Type = ObligationTypes.AND,                
                ObligedPersonIdent = criteria.ObligedPersonIdent,
                DrivingLicenceNumber = criteria.DrivingLicenceNumber,
                PersonalDocumentNumber = criteria.PersonalDocumentNumber,
                Uic = criteria.Uic,
                ForeignVehicleNumber = criteria.ForeignVehicleNumber,
                DocumentType = criteria.DocumentType,
                DocumentSeries = criteria.DocumentSeries,
                DocumentNumber = criteria.DocumentNumber,
                InitialAmount = criteria.Amount
            };

            var state = PagedDataState.CreateMaxPagedDataState();
            var obligationsResult = await _obligationService.SearchAsync(state, serviceCriteria, cancellationToken);
            
            if (!obligationsResult.IsSuccessfullyCompleted)
            {                
                return OperationResult(obligationsResult);
            }
            else
            {
                var result = new ANDObligationSearchResponse() { ObligationsData = obligationsResult.Result.ToList() };

                return PagedResult(result, state);
            }
        }

        /// <summary>
        /// Операция за изчитане на задължения за инстанции на услуги.
        /// </summary>
        /// <param name="criteria">Китерии за търсене.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Задължения.</returns>
        [HttpGet]
        [Route("ServiceInstances")]
        [ProducesResponseType(typeof(IEnumerable<Obligation>), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetServiceInstances([FromQuery] SIObligationSearchCriteria criteria, CancellationToken cancellationToken)
        {
            ObligationSearchCriteria serviceCriteria = new ObligationSearchCriteria()
            {
                Mode = ObligationSearchModes.ServiceInstances,
                ApplicantID = _eAUUserAccessor?.User?.LocalClientID,
                Type = ObligationTypes.ServiceInstance,
                ServiceInstanceID = criteria.ServiceInstanceID,
                PaymentInstructionURI = criteria.PaymentInstructionURI
            };

            var state = PagedDataState.CreateMaxPagedDataState();
            var result = await _obligationService.SearchAsync(state, serviceCriteria, cancellationToken);            

            if (!result.IsSuccessfullyCompleted)
            {
                return OperationResult(result);
            }

            var data = result.Result.Single().Obligations;

            return PagedResult(data, state);
        }

        /// <summary>
        /// Операция за създаване на задължение.
        /// </summary>
        /// <param name="obligationRequest">Заявка за създаване на задължение.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Създаденото задължение.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Obligation), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(ObligationRequest obligationRequest, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(obligationRequest.ObligedPersonName) && obligationRequest.ObligedPersonName.Length > 26)
            {
                return OperationResult(new OperationResult("GL_OBLIGED_PERSON_NAME_TOO_LONG_E", "GL_OBLIGED_PERSON_NAME_TOO_LONG_E"));
            }

            obligationRequest.ApplicantID = _eAUUserAccessor?.User?.LocalClientID;

            var result = await _obligationService.CreateAsync(obligationRequest, cancellationToken);
            return OperationResult(result);
        }

        /// <summary>
        /// Операция за започване на плащане.
        /// </summary>
        /// <param name="obligationId">Идентификатор на задължение.</param>
        /// <param name="startPaymentRequest">Заявка за започване на плащане.</param>
        /// <param name="cancellationToken">Създаденото задължение.</param>
        /// <returns>създадената заявка за плащане</returns>
        [Route("{obligationId}/StartPayment")]
        [HttpPost]
        [ProducesResponseType(typeof(PaymentRequest), StatusCodes.Status200OK)]
        public async Task<IActionResult> StartPayment([FromRoute] long obligationId, [FromBody] StartPaymentRequest startPaymentRequest, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(startPaymentRequest.ObligedPersonName) && startPaymentRequest.ObligedPersonName.Length > 26)
            {
                return OperationResult(new OperationResult("GL_OBLIGED_PERSON_NAME_TOO_LONG_E", "GL_OBLIGED_PERSON_NAME_TOO_LONG_E"));
            }

            if (!Url.IsLocalUrl(startPaymentRequest.OkCancelUrl))
            {
                return OperationResult(new CNSys.OperationResult("GL_INVALID_OK_CANCEL_URL_E", "GL_INVALID_OK_CANCEL_URL_E"));
            }

            startPaymentRequest.OkCancelUrl = $"{_optionsMonitor.CurrentValue.GL_EAU_PUBLIC_APP.TrimEnd('/')}{startPaymentRequest.OkCancelUrl}";

            var result = await _paymentRequestService.StartPaymentAsync(obligationId, startPaymentRequest, cancellationToken);
            return OperationResult(result);
        }

        /// <summary>
        /// Операция за подготовка на заявка за директен достъп до ПЕП чрез код за достъп.
        /// </summary>
        /// <param name="regId"></param>
        /// <param name="iban"></param>
        /// <param name="pepDaeoAccessCodeHelper"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Route("PreparePepAccessCodePaymentRequest/{regId}/{iban}")]
        [HttpPost]
        [ProducesResponseType(typeof(AccessCodeUIResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> PreparePepAccessCodePaymentRequest([FromRoute] long regId, [FromRoute] string iban, [FromServices] IPepDaeoAccessCodeHelper pepDaeoAccessCodeHelper, CancellationToken cancellationToken)
        {
            var accessCodeReqResult = await pepDaeoAccessCodeHelper.GetAccessCodeRequestAsync(regId, iban, cancellationToken);

            return OperationResult(accessCodeReqResult);
        }

        [HttpGet]
        [Route("requests")]
        [ProducesResponseType(typeof(IEnumerable<PaymentRequest>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchPaymentRequests([FromQuery] PaymentRequestSearchCriteria criteria, CancellationToken cancellationToken)
        {
            var paymentRequests = await _paymentRequestService.SearchAsync(criteria, cancellationToken);

            return Ok(paymentRequests);
        }
    }
}
