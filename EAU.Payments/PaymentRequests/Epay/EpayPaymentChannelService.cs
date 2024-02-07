using CNSys;
using EAU.Common;
using EAU.Payments.Obligations.Models;
using EAU.Payments.PaymentRequests.Epay.Models;
using EAU.Payments.RegistrationsData.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments.PaymentRequests.Epay
{
    /// <summary>
    /// Реализация на интерфейс за работа с канали за плащане за Epay.
    /// </summary>
    public class EpayPaymentChannelService : IPaymentChannelService
    {
        private readonly IOptionsMonitor<GlobalOptions> _optionsMonitor;
        private readonly ILogger<EpayPaymentChannelService> _logger;

        public EpayPaymentChannelService(IOptionsMonitor<GlobalOptions> optionsMonitor, ILogger<EpayPaymentChannelService> logger)
        {
            _optionsMonitor = optionsMonitor;
            _logger = logger;
        }

        public async Task<OperationResult> PayAsync(PaymentRequests.Models.PaymentRequest paymentRequest, Obligation obligation, RegistrationData registrationData, CancellationToken cancellationToken)
        {
            BudgetPaymentRequest budgetPaymentRequest = InitBudgetPaymentRequest(paymentRequest, obligation, registrationData);

            try
            {
                string encoded = budgetPaymentRequest.SaveMessageObject(true);
                string checkSum = Security.SecretKey.GetCheckSum(encoded, registrationData.SecretWord);

                paymentRequest.AdditionalData.Add("hmac", checkSum);
                paymentRequest.AdditionalData.Add("data", encoded);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return new OperationResult("GL_EPAY_PAYMENT_UNSUCCESSFUL_E", "GL_EPAY_PAYMENT_UNSUCCESSFUL_E");
            }

            return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
        }
        
        #region IPaymentChannelService Helpers

        private BudgetPaymentRequest InitBudgetPaymentRequest(PaymentRequests.Models.PaymentRequest paymentRequest, Obligation obligation, RegistrationData registrationData)
        {
            var budgetPaymentRequest = new BudgetPaymentRequest();

            budgetPaymentRequest.Invoice = paymentRequest.PaymentRequestID.Value.ToString();

            budgetPaymentRequest.Amount = paymentRequest.Amount.Value;

            budgetPaymentRequest.ObligPersonID = paymentRequest.ObligedPersonIdent;
            budgetPaymentRequest.ObligPersonType = MapIdentType(paymentRequest.ObligedPersonIdentType);
            
            budgetPaymentRequest.ObligPersonName = paymentRequest.ObligedPersonName;

            // 9 = Платежно нареждане/ вносна бележка за плащане от/ към бюджета
            budgetPaymentRequest.DocumentNumber = $"9{paymentRequest.PaymentRequestID}";
            budgetPaymentRequest.TraderNumber = registrationData.Cin;
            budgetPaymentRequest.Email = registrationData.Email;

            budgetPaymentRequest.Description = obligation.PaymentReason;
            budgetPaymentRequest.PaymentDescription = obligation.PaymentReason;

            budgetPaymentRequest.ExpirationTime = obligation.ExpirationDate.Value;

            if (_optionsMonitor.CurrentValue.GL_ADM_STRUCTURE_NAME_SHORT.Length > 25)
                budgetPaymentRequest.Merchant = _optionsMonitor.CurrentValue.GL_ADM_STRUCTURE_NAME_SHORT.Substring(0, 25);
            else
                budgetPaymentRequest.Merchant = _optionsMonitor.CurrentValue.GL_ADM_STRUCTURE_NAME_SHORT;

            budgetPaymentRequest.IBAN = obligation.Iban;
            budgetPaymentRequest.BIC = obligation.Bic;

            return budgetPaymentRequest;
        }

        private ObligPersonTypes? MapIdentType(ObligedPersonIdentTypes? identType)
        {
            if (identType == ObligedPersonIdentTypes.EGN)
            {
                return ObligPersonTypes.EGN;
            }
            else if (identType == ObligedPersonIdentTypes.LNC)
            {
                return ObligPersonTypes.LNC;
            }
            else if (identType == ObligedPersonIdentTypes.BULSTAT)
            {
                return ObligPersonTypes.BULSTAT;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}