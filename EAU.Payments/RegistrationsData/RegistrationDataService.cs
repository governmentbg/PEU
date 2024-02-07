using CNSys;
using CNSys.Data;
using EAU.Payments.PaymentRequests.Repositories;
using EAU.Payments.RegistrationsData.Models;
using EAU.Payments.RegistrationsData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace EAU.Payments.RegistrationsData
{
    /// <summary>
    /// Реализация на интерфейс за работа с регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
    /// </summary>
    internal class RegistrationDataService : IRegistrationDataService
    {
        private readonly IRegistrationDataRepository _registrationDataRepository;
        private readonly IPaymentRequestRepository _paymentRequestRepository;
        private readonly IDbContextOperationExecutor _dBContextOperationExecutor;

        public RegistrationDataService(
            IRegistrationDataRepository registrationDataRepository,
            IPaymentRequestRepository paymentRequestRepository,
            IDbContextOperationExecutor dBContextOperationExecutor)
        {
            _registrationDataRepository = registrationDataRepository;
            _paymentRequestRepository = paymentRequestRepository;
            _dBContextOperationExecutor = dBContextOperationExecutor;
        }

        public async Task<OperationResult> CreateAsync(RegistrationData item, CancellationToken cancellationToken)
        {
            //За Epay може да има само един запис.
            if (item.Type == RegistrationDataTypes.ePay)
            {
                var itemCol = await _registrationDataRepository.SearchInfoAsync(new RegistrationDataSearchCriteria() { Type = item.Type }, cancellationToken);
                if (itemCol != null && itemCol.Data != null && itemCol.Data.Any())
                {
                    throw new NotSupportedException("There may be only one item for epay");
                }
            }

            //За Epay адресът е информативен и се формира на сървъра
            if (item.Type == RegistrationDataTypes.ePay)
            {
                item.NotificationUrl = null;
            }

            await _registrationDataRepository.CreateAsync(item, cancellationToken);

            return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
        }

        public async Task<OperationResult> DeleteAsync(int registrationDataID, CancellationToken cancellationToken)
        {
            var result = new OperationResult(OperationResultTypes.SuccessfullyCompleted);

            var itemCol = await _registrationDataRepository.SearchInfoAsync(new RegistrationDataSearchCriteria() { RegistrationDataIDs = new List<int>() { registrationDataID } }, cancellationToken);
            if (itemCol == null || itemCol.Data.Count() != 1 || itemCol.Data.Single().Type != RegistrationDataTypes.PepOfDaeu)
            {
                throw new NotSupportedException("Omly items for Pep Of Daeu can be deleted");
            }

            var existingPayments = (await _paymentRequestRepository.SearchAsync(new PagedDataState(1, 1, null), 
                new PaymentRequests.Models.PaymentRequestSearchCriteria() { RegistrationDataID = registrationDataID }));

            if (existingPayments != null && existingPayments.Any())
            {
                //Регистрационните данни не могат да бъдат изтрити защото има направено плащане по тях.
                result.AddError(new TextError("GL_CANT_DELETE_RD_EXISTING_PAYMENT_E", "GL_CANT_DELETE_RD_EXISTING_PAYMENT_E"), true);
            }

            await _registrationDataRepository.DeleteAsync(registrationDataID, cancellationToken);

            return result;
        }

        public Task<OperationResult> UpdateAsync(int registrationDataID, string description, string cin, string email, string secretWord,
            TimeSpan? validityPeriod, string portalUrl, string notificationUrl, string serviceUrl, string iban, CancellationToken cancellationToken)
        {
            return _dBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                var itemCol = await _registrationDataRepository.SearchInfoAsync(new RegistrationDataSearchCriteria() { RegistrationDataIDs = new List<int>() { registrationDataID } }, cancellationToken);
                var item = itemCol.Data.Single();
                item.Description = description;
                item.Cin = cin;
                item.Email = email;
                item.SecretWord = secretWord;
                item.ValidityPeriod = validityPeriod;
                item.PortalUrl = portalUrl;
                item.NotificationUrl = item.Type != RegistrationDataTypes.ePay ? notificationUrl : null; //За Epay адресът е информативен и се формира на сървъра
                item.ServiceUrl = serviceUrl;
                item.IBAN= iban;

                await _registrationDataRepository.UpdateAsync(item, cancellationToken);

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }
    }
}
