using CNSys;
using CNSys.Data;
using EAU.Payments.Obligations.Models;
using EAU.Payments.Obligations.Repositories;
using EAU.Payments.PaymentRequests.Models;
using EAU.Payments.PaymentRequests.PepDaeu.Models;
using EAU.Payments.RegistrationsData.Models;
using EAU.Payments.RegistrationsData.Repositories;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments.PaymentRequests.PepDaeu
{
    /// <summary>
    /// Реализация на интерфейс за обратна връзка при работа с канали за плащане за PepDaeu.
    /// </summary>
    public class PepDaeuPaymentCallbackService : IPepDaeuPaymentCallbackService
    {
        private readonly IRegistrationDataRepository _registrationDataRepository;
        private readonly IPaymentRequestService _paymentRequestService;
        private readonly IStringLocalizer _localizer;
        private readonly ILogger<PepDaeuPaymentCallbackService> _logger;
        private readonly IDbContextOperationExecutor _dbContextOperationExecutor;
        private readonly IObligationRepository _obligationRepository;

        public PepDaeuPaymentCallbackService(
            IRegistrationDataRepository registrationDataRepository,
            IPaymentRequestService paymentRequestService,
            IStringLocalizer localizer,
            ILogger<PepDaeuPaymentCallbackService> logger,
            IDbContextOperationExecutor dbContextOperationExecutor,
            IObligationRepository obligationRepository)
        {
            _registrationDataRepository = registrationDataRepository;
            _paymentRequestService = paymentRequestService;
            _localizer = localizer;
            _logger = logger;
            _obligationRepository = obligationRepository;
            _dbContextOperationExecutor = dbContextOperationExecutor;
        }

        public async Task<OperationResult<NotificationMessageResponse>> ProcessNotificationMessageAsync(string registrationDataCin, NotificationMessageTransportRequest message, CancellationToken cancellationToken)
        {
            string messageID = Guid.NewGuid().ToString();
            NotificationMessageResponse response = null;

            try
            {
                var registrationData = (await _registrationDataRepository.SearchAsync(new RegistrationDataSearchCriteria() { Cin = registrationDataCin })).Single();
                NotificationMessageRequest notificationMessageRequest = null;

                string data = message.Data;
                string hmac = message.Hmac;
                byte[] encodedDataAsBytes = System.Convert.FromBase64String(data);
                var msg = Encoding.UTF8.GetString(encodedDataAsBytes);

                if (IsResponseCorrect(msg, hmac, registrationData))
                {
                    notificationMessageRequest = EAUJsonSerializer.Deserialize<NotificationMessageRequest>(msg);
                }
                else
                {
                    throw new NotSupportedException($"Invalid checksum! msg: {msg} hmac: {hmac}");
                }

                NotificationMessageResponse notificationMessageResponse = await ProcessMessageAsync(notificationMessageRequest, cancellationToken);
                response = notificationMessageResponse;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                response = new NotificationMessageResponse() { Success = false };
            }
            finally
            {
                _logger.LogInformation(string.Format("Message Send. MessageID={0}, RegistrationDataCin={1}, MessageResponse: {2}", messageID, registrationDataCin, response.ToString()));
            }

            return new OperationResult<NotificationMessageResponse>(OperationResultTypes.SuccessfullyCompleted) { Result = response };
        }

        public async Task<OperationResult> ProcessVPOSResultMessageAsync(VPOSResultMessageTransportRequest message, CancellationToken cancellationToken)
        {
            string messageID = Guid.NewGuid().ToString();
            string response = "";
            try
            {
                var registrationData = (await _registrationDataRepository.SearchAsync(new RegistrationDataSearchCriteria() { Cin = message.ClientID })).Single();
                VPOSResultMessageRequest notificationMessageRequest = null;

                string data = message.Data;
                string hmac = message.Hmac;
                byte[] encodedDataAsBytes = System.Convert.FromBase64String(data);
                var msg = Encoding.UTF8.GetString(encodedDataAsBytes);

                if (IsResponseCorrect(msg, hmac, registrationData))
                {
                    notificationMessageRequest = EAUJsonSerializer.Deserialize<VPOSResultMessageRequest>(msg);
                }
                else
                {
                    throw new NotSupportedException($"Invalid checksum! msg: {msg} hmac: {hmac}");
                }

                NotificationMessageResponse notificationMessageResponse = await ProcessVPOSResultMessageInternalAsync(registrationData, notificationMessageRequest, cancellationToken);
                response = EAUJsonSerializer.Serialize(notificationMessageResponse);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return new OperationResult("GL_PEP_PAYMENT_RESPONSE_UNSUCCESSFUL_E", "GL_PEP_PAYMENT_RESPONSE_UNSUCCESSFUL_E");
            }
            finally
            {
                _logger.LogInformation(string.Format("Message Send. MessageID={0}, RegistrationDataCin={1}, MessageResponse: {2}", messageID, message.ClientID, response));
            }

            return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
        }

        #region Helpers

        private async Task<NotificationMessageResponse> ProcessVPOSResultMessageInternalAsync(RegistrationData registrationData, VPOSResultMessageRequest notificationMessageRequest, CancellationToken cancellationToken)
        {
            var result = await _dbContextOperationExecutor.ExecuteAsync((Func<IDbContext, CancellationToken, Task<OperationResult<NotificationMessageResponse>>>)(async (dbContext, innerToken) =>
            {
                NotificationMessageResponse res = null;

                try
                {
                    var paymentRequestForLock = (await _paymentRequestService.SearchAsync(
                        new PaymentRequestSearchCriteria()
                        {
                            ExternalPortalNumber = notificationMessageRequest.RequestId,
                            RegistrationDataID = registrationData.RegistrationDataID
                        },
                        cancellationToken)).SingleOrDefault();

                    if (paymentRequestForLock == null)
                    {
                        throw new NotSupportedException(string.Format("No payment found for externalPortalNumber №{0}", notificationMessageRequest.RequestId));
                    }

                    var lockedObligaions = (await _obligationRepository.SearchAsync(new ObligationRepositorySearchCriteria()
                    {
                        ObligationIDs = new List<long>() { paymentRequestForLock.ObligationID.Value },
                        WithLock = true,
                        LoadOption = new ObligationLoadOption() { LoadPaymentRequests = true }
                    }, cancellationToken)).ToList();

                    var paymentRequest = lockedObligaions.Single().PaymentRequests.SingleOrDefault(t => t.ExternalPortalPaymentNumber == notificationMessageRequest.RequestId
                        && t.RegistrationDataID == registrationData.RegistrationDataID);

                    if (paymentRequest == null)
                    {
                        throw new NotSupportedException(string.Format("No payment found for externalPortalNumber №{0}", notificationMessageRequest.RequestId));
                    }

                    PaymentRequestStatuses? status = this.GetPaymentStatusPR(notificationMessageRequest.Status);
                    string strReason = this.GetPaymentReasonPR(notificationMessageRequest.Status);

                    if (paymentRequest.Status != PaymentRequestStatuses.Paid
                        && paymentRequest.Status != PaymentRequestStatuses.Cancelled)
                    {
                        if (status == PaymentRequestStatuses.Paid)
                        {
                            if (paymentRequest.AdditionalData == null)
                            {
                                paymentRequest.AdditionalData = new Utilities.AdditionalData();
                            }
                            paymentRequest.AdditionalData.Add("vposResultGid", notificationMessageRequest.VposResultGid);
                            paymentRequest.AdditionalData.Add("reason", strReason);
                            paymentRequest.AdditionalData.Add("resultTime", notificationMessageRequest.ResultTime?.ToString());
                            paymentRequest.PayDate = notificationMessageRequest.ResultTime;

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

                            paymentRequest.AdditionalData.Add("vposResultGid", notificationMessageRequest.VposResultGid);
                            paymentRequest.AdditionalData.Add("errorMessage", notificationMessageRequest.ErrorMessage);
                            paymentRequest.AdditionalData.Add("reason", strReason);
                            paymentRequest.AdditionalData.Add("resultTime", notificationMessageRequest.ResultTime?.ToString());

                            //Редактираме плащането в базта на портала със статус отказано
                            var paymentRes = await _paymentRequestService.PaymentCancelled(paymentRequest, cancellationToken);
                        }
                        else
                        {
                            throw new NotSupportedException($"Incorrect status from Pep Daeu: {notificationMessageRequest.Status}. Current paymentRequest: {paymentRequest.Status}");
                        }
                    }
                    else if (paymentRequest.Status != status)
                    {
                        _logger.LogWarning($"Inconsistency between status from Pep Daeu and paymentRequest status. Pep Daeu status: {notificationMessageRequest.Status}. Current paymentRequest status: {paymentRequest.Status}");
                    }

                    res = new NotificationMessageResponse() { Success = true };

                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    res = new NotificationMessageResponse() { Success = false };
                }

                return new OperationResult<NotificationMessageResponse>(OperationResultTypes.SuccessfullyCompleted) { Result = res };
            }), cancellationToken);


            return result.Result;
        }

        private PaymentRequestStatuses? GetPaymentStatusPR(string status)
        {
            switch (status.ToUpper())
            {
                case "FAILURE":
                case "CANCELEDBYUSER":
                    return PaymentRequestStatuses.Cancelled;
                case "SUCCESS":
                    return PaymentRequestStatuses.Paid;
                default:
                    return null;
            }
        }

        private string GetPaymentReasonPR(string status)
        {
            switch (status)
            {
                case "SUCCESS":
                    return _localizer["GL_PAYMENT_FAILURE_E"].Value; //"Успешно плащане";
                case "FAILURE":
                    return _localizer["GL_PAYMENT_FAILURE_E"].Value; //"Неуспешно плащане";
                case "CANCELEDBYUSER":
                    return _localizer["GL_PAYMENT_DENIED_BY_USER_E"].Value; //"Отказано плащане от потребител";
                default:
                    return null;
            }
        }

        #endregion

        #region IPepDaeuPaymentCallbackService Helpers

        private bool IsResponseCorrect(string msgData, string msgHmac, RegistrationData registrationData)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(msgData);
            var data = Convert.ToBase64String(plainTextBytes);

            var keyBytes = System.Text.Encoding.UTF8.GetBytes(registrationData.SecretWord);
            var dataBytes = System.Text.Encoding.UTF8.GetBytes(data);

            string hmac;

            using (HMACSHA256 hmacsha256 = new HMACSHA256(keyBytes))
            {
                byte[] hashmessage_arr = hmacsha256.ComputeHash(dataBytes);
                hmac = System.Convert.ToBase64String(hashmessage_arr);
            }

            return !string.IsNullOrEmpty(hmac) && !string.IsNullOrEmpty(msgHmac) && hmac == msgHmac;
        }

        private async Task<NotificationMessageResponse> ProcessMessageAsync(NotificationMessageRequest notificationMessageRequest, CancellationToken cancellationToken)
        {
            _logger.LogInformation(string.Format("request on pep callback call deserialized: id: {0} status: {1}", notificationMessageRequest.Id, notificationMessageRequest.Status));

            var result = await _dbContextOperationExecutor.ExecuteAsync((Func<IDbContext, CancellationToken, Task<OperationResult<NotificationMessageResponse>>>)(async (dbContext, innerToken) =>
            {
                NotificationMessageResponse res = null;

                try
                {
                    var paymentRequestForLock = (await _paymentRequestService.SearchAsync(new PaymentRequestSearchCriteria() 
                    { 
                        ExternalPortalNumber = notificationMessageRequest.Id,
                        PaymentChannel = RegistrationDataTypes.PepOfDaeu
                    }, cancellationToken)).SingleOrDefault();
                    
                    if (paymentRequestForLock == null)
                    {
                        throw new NotSupportedException(string.Format("No payment found for externalPortalNumber №{0}", notificationMessageRequest.Id));
                    }

                    var lockedObligaions = (await _obligationRepository.SearchAsync(new ObligationRepositorySearchCriteria()
                    {
                        ObligationIDs = new List<long>() { paymentRequestForLock.ObligationID.Value },
                        WithLock = true
                    }, cancellationToken)).ToList();

                    //Изчиаме вече заключени данни.
                    var paymentRequest = (await _paymentRequestService.SearchAsync(new PaymentRequestSearchCriteria() 
                    { 
                        ExternalPortalNumber = notificationMessageRequest.Id,
                        PaymentChannel = RegistrationDataTypes.PepOfDaeu
                    }, cancellationToken)).SingleOrDefault();
                    
                    if (paymentRequest == null)
                    {
                        throw new NotSupportedException(string.Format("No payment found for externalPortalNumber №{0}", notificationMessageRequest.Id));
                    }

                    PaymentRequestStatuses? status = this.GetPaymentStatus(notificationMessageRequest.Status);
                    string strReason = this.GetPaymentReason(notificationMessageRequest.Status);

                    if (!(paymentRequest.Status == PaymentRequestStatuses.Paid
                        || paymentRequest.Status == PaymentRequestStatuses.Cancelled
                        || paymentRequest.Status == PaymentRequestStatuses.Expired))
                    {
                        if (status == PaymentRequestStatuses.Paid)
                        {
                            if (paymentRequest.AdditionalData == null)
                            {
                                paymentRequest.AdditionalData = new Utilities.AdditionalData();
                            }
                            paymentRequest.AdditionalData.Add("reason", strReason);
                            paymentRequest.AdditionalData.Add("changeTime", notificationMessageRequest.ChangeTime?.ToString());
                            paymentRequest.PayDate = notificationMessageRequest.ChangeTime;

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
                            paymentRequest.AdditionalData.Add("changeTime", notificationMessageRequest.ChangeTime?.ToString());

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
                            paymentRequest.AdditionalData.Add("changeTime", notificationMessageRequest.ChangeTime?.ToString()); 

                            //Редактираме плащането в базта на портала със статус отказано
                            var paymentRes = await _paymentRequestService.PaymentExpired(paymentRequest, cancellationToken);
                        }
                        // Извикват ни с статус PENDING и не правим промени по данни
                        else if(!string.IsNullOrWhiteSpace(notificationMessageRequest.Status) 
                                && notificationMessageRequest.Status.ToLower() == "pending")
                        {
                            res = new NotificationMessageResponse() { Success = true };
                        }
                        else
                        {
                            _logger.LogError($"pep daeu payment callback with request id: {notificationMessageRequest.Id} and status: {notificationMessageRequest.Status} and mapped status is: {status}");

                            throw new NotSupportedException("Incorrect status from Pep Daeu");
                        }
                    }

                    res = new NotificationMessageResponse() { Success = true };
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    res = new NotificationMessageResponse() { Success = false };
                }

                return new OperationResult<NotificationMessageResponse>(OperationResultTypes.SuccessfullyCompleted) { Result = res };
            }), cancellationToken);


            return result.Result;
        }

        private PaymentRequestStatuses? GetPaymentStatus(string status)
        {
            switch (status.ToUpper())
            {
                case "CANCELED":
                case "SUSPENDED":
                    return PaymentRequestStatuses.Cancelled;
                case "EXPIRED":
                    return PaymentRequestStatuses.Expired;
                case "AUTHORIZED":
                case "ORDERED":
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
                case "SUSPENDED":
                    return _localizer["GL_PAYMENT_DENIED_E"].Value; //"Отказано плащане";
                default:
                    return null;
            }
        }

        #endregion
    }
}