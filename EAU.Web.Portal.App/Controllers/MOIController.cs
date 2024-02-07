using CNSys;
using EAU.ServiceLimits.AspNetCore.Mvc;
using EAU.Web.Mvc;
using EAU.Web.Portal.App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.BDS.NRBLD.Models;
using WAIS.Integration.MOI.CHOD;
using WAIS.Integration.MOI.CHOD.Models;
using WAIS.Integration.MOI.Core.BDS.NRBLD;
using WAIS.Integration.MOI.KAT.SPRKRTCO;
using WAIS.Integration.MOI.KAT.SPRKRTCO.Models;

namespace EAU.Web.Portal.App.Controllers
{
    //TODO_	MVREAU2020-756

    /// <summary>
    /// Контролер за работа с услуги на МВР
    /// </summary>   
    [Authorize]
    [Route("Api/MOI")]
    [Produces("application/json")]
    public class MOIController : BaseApiController
    {
        private readonly INRBLDServicesClientFactory _nRBLDServicesClientFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly ISPRKRTCOServiceClientFactory miSPRKRTCOServiceClientFactory;
        private readonly ICHODServicesClientFactory _cHODServicesClientFactory;
        private readonly ILogger<MOIController> _logger;

        public MOIController(INRBLDServicesClientFactory iNRBLDServicesClientFactory,
            IAuthorizationService authorizationService,
            ISPRKRTCOServiceClientFactory sPRKRTCOServiceClientFactory,
            ICHODServicesClientFactory cHODServicesClientFactory,
            ILogger<MOIController> logger)
        {
            _nRBLDServicesClientFactory = iNRBLDServicesClientFactory;
            _authorizationService = authorizationService;
            miSPRKRTCOServiceClientFactory = sPRKRTCOServiceClientFactory;
            _cHODServicesClientFactory = cHODServicesClientFactory;
            _logger = logger;
        }

        [ServiceLimiter(ServiceCode = "NAIF_NRBLD_LIMIT")]
        [Route("NRBLD/PersonsInfo/{pid}")]
        [HttpGet]
        [ProducesResponseType(typeof(PersonInfo), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPersonsInfo([FromRoute] string pid, CancellationToken cancellationToken)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, pid, Code.Authorization.PolicyNames.PersonIdentifierDataPolicyName);
            if (!authorizationResult.Succeeded) return new ForbidResult();

            var result = await _nRBLDServicesClientFactory.GetNRBLDServicesClient().GetPersonInfoAsync(pid, false, cancellationToken);

            if (result.IsSuccessfullyCompleted && result.Response.PersonData != null && result.Response.PersonData.PersonIdentification != null)
            {
                var personInfo = new PersonInfo();

                if (result.Response.PersonData.PersonIdentification.PersonIdentificationBG != null)
                {
                    var bgCitizen = result.Response.PersonData.PersonIdentification.PersonIdentificationBG;
                    personInfo.PIN = bgCitizen.PIN;

                    MapPersonDataCyrilicLatinNames(personInfo, bgCitizen.Names);
                }
                else if (result.Response.PersonData.PersonIdentification.PersonIdentificationF != null)
                {
                    var foreigner = result.Response.PersonData.PersonIdentification.PersonIdentificationF;
                    personInfo.PIN = foreigner.PIN;

                    MapPersonDataCyrilicLatinNames(personInfo, foreigner.Names);
                }

                return Ok(personInfo);
            }

            var errors = new ErrorCollection();
            errors.AddRange(result.Errors?.Select(e => (IError)(new TextError(e.Code, e.Message))));

            return OperationResult(new CNSys.OperationResult(errors));
        }

        [Route("SPRKRTCO/FreeFourDigitsRegNumbers")]
        [HttpGet]
        [ProducesResponseType(typeof(FourDigitSearchResultVM), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFreeFourDigitsRegNumbers([FromQuery] FourDigitSearchCriteriaVM criteria, CancellationToken cancellationToken)
        {
            var domCriteria = Mapper.Map<FourDigitSearchCriteria>(criteria);
            var res = await miSPRKRTCOServiceClientFactory.GetISPRKRTCOServiceClient().FourDigitStatusCheckAsync(domCriteria, cancellationToken);

            if (res.IsSuccessfullyCompleted)
            {
                var resUI = new FourDigitSearchResultVM()
                {
                    ExceedResultLimiteWarnning = res.Errors != null && res.Errors.Count == 1 ? res.Errors[0].Message : null,
                    Result = res.Response
                };

                return Ok(resUI);
            }
            else
            {
                return BadRequest(res.Errors[0].Code, res.Errors[0].Message);
            }
        }

        [Route("SPRKRTCO/FreeSpecialNumbers")]
        [HttpGet]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFreeSpecialNumbers([FromQuery] SpecialNumberSearchCriteria criteria, CancellationToken cancellationToken)
        {
            var res = await miSPRKRTCOServiceClientFactory.GetISPRKRTCOServiceClient().SpecialNumberStatusCheckAsync(criteria, cancellationToken);

            if (res.IsSuccessfullyCompleted)
                return Ok(res.Response);
            else
                return BadRequest(res.Errors[0].Code, res.Errors[0].Message);
        }

        #region CHOD

        /// <summary>
        /// Извличане на стойности от номенклатура "Вид дейност по ЗЧОД" в АИС ЛКЧОД.
        /// </summary>
        /// <param name="cancellationToken">Токен по отказване</param>
        /// <returns></returns>
        [HttpGet]
        [Route("CHOD/securityActivityTypes")]
        [ProducesResponseType(typeof(List<SecurityActivityTypeInfo>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSecurityActivityTypesAsync(
            CancellationToken cancellationToken)
        {
            var res = await _cHODServicesClientFactory.GetCHODServicesClient().GetSecurityActivityTypesAsync(cancellationToken);

            if (res.IsSuccessfullyCompleted)
            {
                return Result(res?.Response?.Data?.ToList());
            }
            else
            {
                var errors = new ErrorCollection();
                errors.AddRange(res.Errors?.Select(e => (IError)(new TextError(e.Code, e.Message))));

                return OperationResult(new CNSys.OperationResult(errors));
            }
        }

        /// <summary>
        /// Извличане на стойности от номенклатура "Район на обекта" в АИС ЛКЧОД.
        /// </summary>
        /// <param name="cancellationToken">Токен по отказване</param>
        /// <returns></returns>
        [HttpGet]
        [Route("CHOD/regions")]
        [ProducesResponseType(typeof(List<RegionInfo>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRegionsAsync(
            CancellationToken cancellationToken)
        {
            var res = await _cHODServicesClientFactory.GetCHODServicesClient().GetRegionsAsync(cancellationToken);

            if (res.IsSuccessfullyCompleted)
            {
                return Result(res?.Response?.Data?.ToList());
            }
            else
            {
                var errors = new ErrorCollection();
                errors.AddRange(res.Errors?.Select(e => (IError)(new TextError(e.Code, e.Message))));

                return OperationResult(new CNSys.OperationResult(errors));
            }
        }

        /// <summary>
        /// Извличане на стойности от номенклатура "Режим" в АИС ЛКЧОД.
        /// </summary>
        /// <param name="cancellationToken">Токен по отказване</param>
        /// <returns></returns>
        [HttpGet]
        [Route("CHOD/securityTypesMode")]
        [ProducesResponseType(typeof(List<SecurityTypeInfo>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSecurityTypesModeAsync(
            CancellationToken cancellationToken)
        {
            var res = await _cHODServicesClientFactory.GetCHODServicesClient().GetSecurityTypesModeAsync(cancellationToken);

            if (res.IsSuccessfullyCompleted)
            {
                return Result(res?.Response?.Data?.ToList());
            }
            else
            {
                var errors = new ErrorCollection();
                errors.AddRange(res.Errors?.Select(e => (IError)(new TextError(e.Code, e.Message))));

                return OperationResult(new CNSys.OperationResult(errors));
            }
        }

        /// <summary>
        /// Извличане на стойности от номенклатура "Контрол" в АИС ЛКЧОД.
        /// </summary>
        /// <param name="cancellationToken">Токен по отказване</param>
        /// <returns></returns>
        [HttpGet]
        [Route("CHOD/securityTypesControl")]
        [ProducesResponseType(typeof(List<SecurityTypeInfo>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSecurityTypesControlAsync(
            CancellationToken cancellationToken)
        {
            var res = await _cHODServicesClientFactory.GetCHODServicesClient().GetSecurityTypesControlAsync(cancellationToken);

            if (res.IsSuccessfullyCompleted)
            {
                return Result(res?.Response?.Data?.ToList());
            }
            else
            {
                var errors = new ErrorCollection();
                errors.AddRange(res.Errors?.Select(e => (IError)(new TextError(e.Code, e.Message))));

                return OperationResult(new CNSys.OperationResult(errors));
            }
        }

        /// <summary>
        /// Търсене на наети охранители в АИС ЛКЧОД.
        /// </summary>
        /// <param name="requestData">Заявка.</param>
        /// <param name="cancellationToken">Токен по отказване</param>
        /// <returns></returns>
        [HttpGet]
        [Route("CHOD/employees")]
        [ProducesResponseType(typeof(List<EmployeeInfo>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeesAsync(
            [FromQuery] GetEmployeesRequestBody requestData,
            CancellationToken cancellationToken)
        {
            var res = await _cHODServicesClientFactory.GetCHODServicesClient().GetEmployeesAsync(requestData, cancellationToken);

            if (res.IsSuccessfullyCompleted)
            {
                return Result(res?.Response?.Data?.ToList());
            }
            else
            {
                if (res?.Errors.Any() == true)
                {
                    string errorsText = string.Join(';', res.Errors.Select(t => t.Message).ToList());
                    _logger.LogError(errorsText);
                }

                return BadRequest("GL_INTEGRATION_ERROR_E", "GL_INTEGRATION_ERROR_E");
            }
        }

        /// <summary>
        /// Подаване на уведомление за прекратяване на трудови договори на охранители в АИС ЛКЧОД.
        /// </summary>
        /// <param name="requestData">Заявка.</param>
        /// <param name="cancellationToken">Токен по отказване</param>
        /// <returns></returns>
        [HttpGet]
        [Route("CHOD/objects")]
        [ProducesResponseType(typeof(List<FindObjectsContractInfo>), StatusCodes.Status200OK)]
        public async Task<IActionResult> FindObjectsAsync(
            [FromQuery] FindObjectsRequestBody requestData,
            CancellationToken cancellationToken)
        {
            var res = await _cHODServicesClientFactory.GetCHODServicesClient().FindObjectsAsync(requestData, cancellationToken);

            if (res.IsSuccessfullyCompleted)
            {
                return Result(res?.Response?.Data?.ToList());
            }
            else
            {
                if (res?.Errors.Any() == true)
                {
                    string errorsText = string.Join(';', res.Errors.Select(t => t.Message).ToList());
                    _logger.LogError(errorsText);
                }

                return BadRequest("GL_INTEGRATION_ERROR_E", "GL_INTEGRATION_ERROR_E");
            }
        }

        #endregion

        #region Helpers

        private void MapPersonDataCyrilicLatinNames(PersonInfo personInfo, NamesType names)
        {
            personInfo.FirstName = new CyrilicLatinName
            {
                Cyrillic = names?.FirstName.Cyrillic,
                Latin = names?.FirstName.Latin
            };
            personInfo.Surname = new CyrilicLatinName
            {
                Cyrillic = names?.Surname?.Cyrillic,
                Latin = names?.Surname?.Latin
            };

            personInfo.Family = new CyrilicLatinName
            {
                Cyrillic = names?.Family?.Cyrillic,
                Latin = names?.Family?.Latin
            };
        }

        #endregion
    }
}