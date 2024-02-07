using AutoMapper;
using CNSys;
using CNSys.Data;
using EAU.Payments.Obligations.Models;
using EAU.Payments.Obligations.Repositories;
using EAU.Payments.PaymentRequests.Epay.Models;
using EAU.Payments.PaymentRequests.Epay.Security;
using EAU.Payments.PaymentRequests.Models;
using EAU.Payments.RegistrationsData.Models;
using EAU.Payments.RegistrationsData.Repositories;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments.PaymentRequests.Epay
{
    /// <summary>
    /// Реализация на интерфейс за обратна връзка при работа с канали за плащане за Epay.
    /// </summary>
    public class EPayPaymentCallbackService : IEPayPaymentCallbackService
    {
        private readonly IRegistrationDataRepository _registrationDataRepository;
        private readonly IPaymentRequestService _paymentRequestService;
        private readonly IStringLocalizer _localizer;
        private readonly ILogger<EPayPaymentCallbackService> _logger;
        private readonly IDbContextOperationExecutor _dbContextOperationExecutor;
        private readonly IObligationRepository _obligationRepository;

        public EPayPaymentCallbackService(IRegistrationDataRepository registrationDataRepository,
            IPaymentRequestService paymentRequestService,
            IStringLocalizer localizer,
            ILogger<EPayPaymentCallbackService> logger,
            IDbContextOperationExecutor dbContextOperationExecutor,
            IObligationRepository obligationRepository)
        {
            _registrationDataRepository = registrationDataRepository;
            _paymentRequestService = paymentRequestService;
            _localizer = localizer;
            _logger = logger;
            _dbContextOperationExecutor = dbContextOperationExecutor;
            _obligationRepository = obligationRepository;
        }

        public async Task<OperationResult<Stream>> ProcessMessageAsync(string registrationDataCin, PaymentsEpayCallbackRequest message, CancellationToken cancellationToken)
        {
            string messageID = Guid.NewGuid().ToString();
            string response = "";
            try
            {
                var registrationData = (await _registrationDataRepository.SearchAsync(new RegistrationDataSearchCriteria() { Cin = registrationDataCin })).Single();

                NotificationMessageRequest msgRequest = LoadNotificationMessageRequest(registrationData, message, messageID);

                NotificationMessageResponse responseMsg = await ProcessNotification(msgRequest, cancellationToken);

                //Create response
                //the response must not be encoded!
                response = responseMsg.ToMessageString();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                response = string.Format("ERR={0}", ex.Message);
            }
            finally
            {
                _logger.LogInformation(string.Format("Message Send. MessageID={0}, RegistrationDataCin={1}, MessageResponse: {2}", messageID, registrationDataCin, response));
            }

            return new OperationResult<Stream>(OperationResultTypes.SuccessfullyCompleted) { Result = new MemoryStream(Encoding.ASCII.GetBytes(response)) };
        }

        #region IEPayPaymentCallbackService Helpers

        private NotificationMessageRequest LoadNotificationMessageRequest(RegistrationData registrationData, PaymentsEpayCallbackRequest requestMessage, string messageID)
        {
            string encoded = requestMessage.Encoded;
            string checksum = requestMessage.Checksum;

            _logger.LogInformation(string.Format("Message arrived. MessageID={0}, RegistrationDataCin={1}, MessageRequest.Encoded: {2}, MessageRequest.Checksum: {2}", messageID, registrationData.Cin, requestMessage.Encoded, requestMessage.Checksum));
            NotificationMessageRequest msgRequest = null;

            if (SecretKey.IsCheckSumValid(checksum, encoded, registrationData.SecretWord))
            {
                msgRequest = new NotificationMessageRequest();

                msgRequest.LoadMessageObject(encoded, true);
            }
            else
            {
                throw new NotSupportedException($"Invalid checksum! encoded: {encoded} checksum: {checksum}");
            }

            return msgRequest;
        }

        private async Task<NotificationMessageResponse> ProcessNotification(NotificationMessageRequest notification, CancellationToken cancellationToken)
        {
            NotificationMessageResponse response = new NotificationMessageResponse();

            foreach (var row in notification.Rows)
            {
                response.Rows.Add(await this.ProcessingMessage(row, cancellationToken));
            }

            return response;
        }

        private async Task<NotificationMessageResponseRow> ProcessingMessage(NotificationMessaRequestRow row, CancellationToken cancellationToken)
        {
            var result = await _dbContextOperationExecutor.ExecuteAsync((Func<IDbContext, CancellationToken, Task<OperationResult<NotificationMessageResponseRow>>>)(async (dbContext, innerToken) =>
            {
                NotificationMessageResponseRow res = null;

                try
                {
                    long paymentRequestID = Convert.ToInt64(row.Invoice);

                    var paymentRequestForLock = (await _paymentRequestService.SearchAsync(new PaymentRequestSearchCriteria() { PaymentRequestIDs = new List<long>() { paymentRequestID } }, cancellationToken)).SingleOrDefault();
                    if (paymentRequestForLock == null)
                    {
                        throw new NotSupportedException(string.Format("No payment found for invoice №{0}", row.Invoice));
                    }
                    
                    var lockedObligaions = (await _obligationRepository.SearchAsync(new ObligationRepositorySearchCriteria()
                    {
                        ObligationIDs = new List<long>() { paymentRequestForLock.ObligationID.Value },
                        WithLock = true,
                        LoadOption = new ObligationLoadOption() { LoadPaymentRequests = true }
                    }, cancellationToken)).ToList();

                    var paymentRequest = lockedObligaions.Single().PaymentRequests.SingleOrDefault(t => t.PaymentRequestID == paymentRequestID);
                    if (paymentRequest == null)
                    {
                        throw new NotSupportedException(string.Format("No payment found for invoice №{0}", row.Invoice));
                    }

                    PaymentRequestStatuses? status = this.GetPaymentStatus(row.Status);
                    string strReason = this.GetPaymentReason(row.Status);

                    /*
                     * ako statusa po plastaneto e platen ili otkazan 
                     * to e obraboteno i ne se obrabotva powtorno.
                     */
                    if (paymentRequest.Status != PaymentRequestStatuses.Paid
                        && paymentRequest.Status != PaymentRequestStatuses.Cancelled
                        && paymentRequest.Status != PaymentRequestStatuses.Expired)
                    {
                        if (status == PaymentRequestStatuses.Paid)
                        {
                            if (paymentRequest.AdditionalData == null)
                            {
                                paymentRequest.AdditionalData = new Utilities.AdditionalData();
                            }
                            paymentRequest.AdditionalData.Add("reason", strReason);
                            paymentRequest.AdditionalData.Add("boricaCode", row.BoricaCode);
                            paymentRequest.AdditionalData.Add("transactionNumber", row.TransactionNumber);
                            paymentRequest.PayDate = row.PaymentTime;

                            var paymentRes = await _paymentRequestService.PaymentPaid(paymentRequest, cancellationToken);
                            if (!paymentRes.IsSuccessfullyCompleted)
                            {
                                throw new NotSupportedException("PaymentPaid not successful");
                            }
                        }
                        else if (status == PaymentRequestStatuses.Cancelled)
                        {
                            if (paymentRequest.AdditionalData == null)
                            {
                                paymentRequest.AdditionalData = new Utilities.AdditionalData();
                            }
                            paymentRequest.AdditionalData.Add("reason", strReason);

                            //Редактираме плащането в базта на портала със статус отказано
                            var paymentRes = await _paymentRequestService.PaymentCancelled(paymentRequest, cancellationToken);
                        }
                        else if (status == PaymentRequestStatuses.Expired)
                        {
                            if (paymentRequest.AdditionalData == null)
                            {
                                paymentRequest.AdditionalData = new Utilities.AdditionalData();
                            }
                            paymentRequest.AdditionalData.Add("reason", strReason);

                            //Редактираме плащането в базта на портала със статус отказано
                            var paymentRes = await _paymentRequestService.PaymentExpired(paymentRequest, cancellationToken);
                        }
                        else
                            throw new NotSupportedException($"Incorrect status from Epay: {row.Status}. Current paymentRequest: {paymentRequest.Status}");
                    }
                    else if (paymentRequest.Status != status)
                    {
                        _logger.LogWarning($"Inconsistency between status from Epay and paymentRequest status. Epay status: {row.Status}. Current paymentRequest status: {paymentRequest.Status}");
                    }

                    res = new NotificationMessageResponseRow()
                    {
                        Invoice = row.Invoice,
                        Status = NotificationMessageResponseStatus.OK
                    };

                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    res = new NotificationMessageResponseRow()
                    {
                        Invoice = row.Invoice,
                        Status = NotificationMessageResponseStatus.ERR
                    };
                }

                return new OperationResult<NotificationMessageResponseRow>(OperationResultTypes.SuccessfullyCompleted) { Result = res };
            }), cancellationToken);

            return result.Result;
        }

        private PaymentRequestStatuses? GetPaymentStatus(string status)
        {
            switch (status)
            {
                case "DENIED":
                    return PaymentRequestStatuses.Cancelled;
                case "EXPIRED":
                    return PaymentRequestStatuses.Expired;
                case "PAID":
                    return PaymentRequestStatuses.Paid;
                default:
                    return null;
            }
        }

        private string GetPaymentReason(string status)
        {
            switch (status)
            {
                case "EXPIRED":
                    return _localizer["GL_PAYMENT_EXPIRED_E"].Value; //"Изтекъл срок за плащане";
                case "DENIED":
                    return _localizer["GL_PAYMENT_DENIED_E"].Value; //"Отказано плащане";
                default:
                    return null;
            }
        }

        #endregion
    }
}