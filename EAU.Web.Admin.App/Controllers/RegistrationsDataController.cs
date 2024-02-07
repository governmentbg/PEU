using EAU.Common.Cache;
using EAU.Payments.RegistrationsData;
using EAU.Payments.RegistrationsData.Models;
using EAU.Payments.RegistrationsData.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Admin.App.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
    /// </summary>
    [Produces("application/json")]
    public class RegistrationsDataController : BaseApiController
    {
        /// <summary>
        /// Операция за изчитане на регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
        /// </summary>
        /// <param name="searchCriteria">Критерии за търсене.</param>
        /// <param name="registrationDataRepository">Интерфейс за работа с регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.</param>
        /// <param name="appParameters">Кеш за параметри на системата</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RegistrationData>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] RegistrationDataSearchCriteria searchCriteria, [FromServices] IRegistrationDataRepository registrationDataRepository, [FromServices] IAppParameters appParameters, CancellationToken cancellationToken)
        {
            var res = await registrationDataRepository.SearchInfoAsync(searchCriteria, cancellationToken);

            var regDataItems = res?.Data?.ToList();
            if (regDataItems != null)
            {
                foreach (var regDataItem in regDataItems.Where(t => t.Type == RegistrationDataTypes.ePay))
                {
                    regDataItem.NotificationUrl = $"{appParameters.GetParameter("GL_EAU_PUBLIC_APP").ValueString}/api/Obligations/PaymentRequests/Epay/Callback/{regDataItem.Cin}".Replace("//", "/");
                }
            }

            return Ok(regDataItems);
        }

        /// <summary>
        /// Операция за създаване на регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
        /// </summary>
        /// <param name="item">Регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.</param>
        /// <param name="registrationDataService">Интерфейс за работа с регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(RegistrationData item, [FromServices] IRegistrationDataService registrationDataService, CancellationToken cancellationToken)
        {
            var result = await registrationDataService.CreateAsync(item, cancellationToken);

            return OperationResult(result);
        }

        /// <summary>
        /// Операция за редакция на регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
        /// </summary>
        /// <param name="registrationDataID">Идентификатор на регистрационните данни.</param>
        /// <param name="item">Регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.</param>
        /// <param name="registrationDataService">Интерфейс за работа с регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        [HttpPut]
        [Route("{registrationDataID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] int registrationDataID, [FromBody] RegistrationData item, [FromServices] IRegistrationDataService registrationDataService, CancellationToken cancellationToken)
        {
            var res = await registrationDataService.UpdateAsync(registrationDataID, item.Description, item.Cin, item.Email, item.SecretWord, item.ValidityPeriod, item.PortalUrl, item.NotificationUrl, item.ServiceUrl, item.IBAN, cancellationToken);

            return OperationResult(res);
        }

        /// <summary>
        /// Операция за изтриване на процес по заявяване.
        /// </summary>
        /// <param name="registrationDataID">Идентификатор на регистрационните данни.</param>      
        /// <param name="registrationDataService">Интерфейс за работа с регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        [Route("{registrationDataID}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int registrationDataID, [FromServices] IRegistrationDataService registrationDataService, CancellationToken cancellationToken)
        {
            var res = await registrationDataService.DeleteAsync(registrationDataID, cancellationToken);

            return OperationResult(res);
        }
    }
}