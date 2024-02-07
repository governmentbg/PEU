using CNSys;
using EAU.Common;
using EAU.Payments.Obligations.Models;
using EAU.Payments.PaymentRequests.Models;
using EAU.Payments.PaymentRequests.PepDaeu.Models;
using EAU.Payments.PaymentRequests.Repositories;
using EAU.Payments.RegistrationsData.Models;
using EAU.Payments.RegistrationsData.Repositories;
using EAU.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments.PaymentRequests.PepDaeu
{
    public interface IPepDaeoAccessCodeHelper
    {
        Task<OperationResult<AccessCodeUIResponse>> GetAccessCodeRequestAsync(long paymentReqId, string iban, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Реализация на интерфейс за работа с канали за плащане за ПЕП на ДАЕУ.
    /// </summary>
    public class PepDaeoPaymentChannelService : IPaymentChannelService, IPepDaeoAccessCodeHelper
    {
        private readonly string _pepAccessCode = "pepAccessCode";
        private readonly HttpClient _httpClient;
        private readonly IPaymentRequestRepository _paymentRequestRepository;
        private readonly IRegistrationDataRepository _registrationDataRepository;
        private readonly IOptionsMonitor<GlobalOptions> _optionsMonitor;
        private readonly ILogger<PepDaeoPaymentChannelService> _logger;

        public PepDaeoPaymentChannelService(HttpClient httpClient, 
            IPaymentRequestRepository paymentRequestRepository, 
            IRegistrationDataRepository registrationDataRepository, 
            IOptionsMonitor<GlobalOptions> optionsMonitor, 
            ILogger<PepDaeoPaymentChannelService> logger)
        {
            _httpClient = httpClient;
            _paymentRequestRepository = paymentRequestRepository;
            _registrationDataRepository = registrationDataRepository;
            _optionsMonitor = optionsMonitor;
            _logger = logger;
        }

        private bool ValidateRequest(PaymentMessageRequest paymentMessageRequest)
        {
            if (string.IsNullOrEmpty(paymentMessageRequest.ServiceProviderName)
                // TODO || string.IsNullOrEmpty(paymentMessageRequest.ServiceProviderBank)
                || string.IsNullOrEmpty(paymentMessageRequest.ServiceProviderBIC)
                || string.IsNullOrEmpty(paymentMessageRequest.ServiceProviderIBAN)
                || string.IsNullOrEmpty(paymentMessageRequest.Currency)
                || !paymentMessageRequest.PaymentAmount.HasValue
                || string.IsNullOrEmpty(paymentMessageRequest.PaymentReason)
                || !paymentMessageRequest.ApplicantUinTypeId.HasValue
                || string.IsNullOrEmpty(paymentMessageRequest.ApplicantUin)
                || string.IsNullOrEmpty(paymentMessageRequest.ApplicantName)
                || string.IsNullOrEmpty(paymentMessageRequest.PaymentReferenceNumber)
                || !paymentMessageRequest.PaymentReferenceDate.HasValue
                || !paymentMessageRequest.ExpirationDate.HasValue
                )
            {
                return false;
            }

            return true;
        }

        public async Task<OperationResult> PayAsync(PaymentRequests.Models.PaymentRequest paymentRequest, Obligation obligation, RegistrationData registrationData, CancellationToken cancellationToken)
        {
            try
            {
                PaymentMessageRequest paymentMessageRequest = InitPaymentMessageRequest(paymentRequest, obligation, registrationData);

                if (!ValidateRequest(paymentMessageRequest))
                {
                    throw new NotSupportedException("Missing required Properties for PaymentMessageRequest");
                }

                var request = CreatePepRequest(paymentMessageRequest, registrationData);
                var responseDatastring = string.Empty;

                using (var req = new HttpRequestMessage(HttpMethod.Post, new Uri($"{registrationData.ServiceUrl}api/v1/eService/paymentJson", UriKind.Absolute)) { Content = new FormUrlEncodedContent(request) })
                {
                    using (var messageResponse = await _httpClient.SendAsync(req))
                    {
                        if (messageResponse != null
                            && messageResponse.Content != null
                            && _logger.IsEnabled(LogLevel.Information))
                        {
                            var res = await messageResponse.Content.ReadAsStringAsync();
                            _logger.LogInformation($"create payment request in pep - response message: {res}");
                        }

                        if (messageResponse.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            throw new Exception(messageResponse.ReasonPhrase);
                        }

                        responseDatastring = messageResponse.Content == null ? null : await messageResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
                }

                PaymentMessageResponse paymentMessageResponse = EAUJsonSerializer.Deserialize<PaymentMessageResponse>(responseDatastring);

                ProcessPaymentResponse(paymentRequest, paymentMessageResponse, responseDatastring, obligation);

                paymentRequest.AdditionalData.Add("clientId", request["clientId"]);
                paymentRequest.AdditionalData.Add("hmac", request["hmac"]);
                paymentRequest.AdditionalData.Add("data", request["data"]);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return new OperationResult("GL_PEP_PAYMENT_UNSUCCESSFUL_E", "GL_PEP_PAYMENT_UNSUCCESSFUL_E");
            }

            return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
        }

        #region IPepDaeoAccessCodeHelper

        public async Task<OperationResult<AccessCodeUIResponse>> GetAccessCodeRequestAsync(long paymentReqId, string iban, CancellationToken cancellationToken)
        {
            try
            {
                var pepAccessCode = string.Empty;

                var payReq = (await _paymentRequestRepository.SearchAsync(new PaymentRequestSearchCriteria() { PaymentRequestIDs = new List<long>() { paymentReqId } })).SingleOrDefault();

                if (payReq == null)
                {
                    return new OperationResult<AccessCodeUIResponse>("GL_INVALID_INPUT_PARAMS_E", "GL_INVALID_INPUT_PARAMS_E");
                }

                if (payReq.AdditionalData.TryGetValue(_pepAccessCode, out pepAccessCode))
                {
                    return new OperationResult<AccessCodeUIResponse>(OperationResultTypes.SuccessfullyCompleted) { Result = new AccessCodeUIResponse { Code = pepAccessCode } };
                }

                var pepDaeuChannel = (await _registrationDataRepository.SearchAsync(new RegistrationDataSearchCriteria() { IBAN = iban, Type = RegistrationDataTypes.PepOfDaeu })).Single();

                var pepAccessCodeReq = new
                {
                    Id = payReq.ExternalPortalPaymentNumber
                };

                var accessCodeAdditionalData = CreatePepRequest(pepAccessCodeReq, pepDaeuChannel);

                string responseDatastring = null;
                string absUrl = $"{pepDaeuChannel.ServiceUrl.TrimEnd('/')}/api/v1/eService/accessCode";
                Uri absoluteUri = new Uri(absUrl, UriKind.Absolute);

                using (var req = new HttpRequestMessage(HttpMethod.Post, absoluteUri) { Content = new FormUrlEncodedContent(accessCodeAdditionalData) })
                {
                    using (var messageResponse = await _httpClient.SendAsync(req))
                    {
                        if (messageResponse != null
                                && messageResponse.Content != null
                                && _logger.IsEnabled(LogLevel.Information))
                        {
                            var res = await messageResponse.Content.ReadAsStringAsync();
                            _logger.LogInformation($"pep response on get pep access code: {res}");
                        }

                        messageResponse.EnsureSuccessStatusCode2();

                        responseDatastring = messageResponse.Content == null ? null : await messageResponse.Content.ReadAsStringAsync();
                    }
                }

                var accessCodeResponse = EAUJsonSerializer.Deserialize<AccessCodeResponse>(responseDatastring);

                payReq.AdditionalData[_pepAccessCode] = accessCodeResponse.AccessCode;

                await _paymentRequestRepository.UpdateAsync(payReq, cancellationToken);

                return new OperationResult<AccessCodeUIResponse>(OperationResultTypes.SuccessfullyCompleted) { Result = new AccessCodeUIResponse { Code = accessCodeResponse.AccessCode } };
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return new OperationResult<AccessCodeUIResponse>("GL_PEP_PAYMENT_UNSUCCESSFUL_E", "GL_PEP_PAYMENT_UNSUCCESSFUL_E") { Result = new AccessCodeUIResponse() };
            }
            
        }
        #endregion

        #region Helpers

        private PaymentMessageRequest InitPaymentMessageRequest(PaymentRequests.Models.PaymentRequest paymentRequest, Obligation obligation, RegistrationData registrationData)
        {
            var paymentMessageRequest = new PaymentMessageRequest();

            if (_optionsMonitor.CurrentValue.GL_ADM_STRUCTURE_NAME_SHORT.Length > 25)
                paymentMessageRequest.ServiceProviderName = _optionsMonitor.CurrentValue.GL_ADM_STRUCTURE_NAME_SHORT.Substring(0, 25);
            else
                paymentMessageRequest.ServiceProviderName = _optionsMonitor.CurrentValue.GL_ADM_STRUCTURE_NAME_SHORT;

            paymentMessageRequest.ServiceProviderBank = obligation.BankName;
            paymentMessageRequest.ServiceProviderBIC = obligation.Bic;
            paymentMessageRequest.ServiceProviderIBAN = obligation.Iban;
            paymentMessageRequest.Currency = "BGN";

            //paymentAmount(*): сума на задължението (десетичен разделител ".", до 2 символа след десетичния разделител, пр. "2.33");
            paymentMessageRequest.PaymentAmount = decimal.Round(paymentRequest.Amount.Value, 2, MidpointRounding.AwayFromZero);

            var paymentReason = Regex.Replace(obligation.PaymentReason, @"[^a-zA-Zа-яА-Я0-9- ,.]", " ");
            paymentMessageRequest.PaymentReason = paymentReason.Length > 70 ? paymentReason.Substring(0, 69) : paymentReason;

            paymentMessageRequest.ApplicantUin = paymentRequest.ObligedPersonIdent;
            paymentMessageRequest.ApplicantUinTypeId = MapIdentType(paymentRequest.ObligedPersonIdentType);
            paymentMessageRequest.ApplicantName = paymentRequest.ObligedPersonName;
            
            //9 = Платежно нареждане/вносна бележка за плащане от/към бюджета
            paymentMessageRequest.PaymentReferenceType = 9;
            paymentMessageRequest.PaymentReferenceNumber = paymentRequest.PaymentRequestID.HasValue ? paymentRequest.PaymentRequestID.ToString() : "";
            paymentMessageRequest.PaymentReferenceDate = obligation.ObligationDate;
            paymentMessageRequest.ExpirationDate = obligation.ExpirationDate;
            paymentMessageRequest.AdministrativeServiceNotificationURL = $"{registrationData.NotificationUrl}{registrationData.Cin}";

            return paymentMessageRequest;
        }

        private int? MapIdentType(ObligedPersonIdentTypes? identType)
        {
            if (identType == ObligedPersonIdentTypes.EGN)
            {
                return 1;
            }
            else if (identType == ObligedPersonIdentTypes.LNC)
            {
                return 2;
            }
            else if (identType == ObligedPersonIdentTypes.BULSTAT)
            {
                return 3;
            }
            else
            {
                return null;
            }
        }

        private Dictionary<string, string> CreatePepRequest(object paymentMessageRequest, RegistrationData registrationData)
        {
            string jsonMessageString = EAUJsonSerializer.Serialize(paymentMessageRequest);

            //Данните за предаваното съобщение в полето "data" се изчисляват по следния начин:
            //1.Прави се битова репрезентация с енкодинг UTF-8 на JSON стринга на предаваното съобщение описан в поле Data Params на услугата;
            //2.Битовата поредица се конвертира като Base64 стринг, който се подава в полето "data".
            var jsonMessageBytes = System.Text.Encoding.UTF8.GetBytes(jsonMessageString);
            var data = Convert.ToBase64String(jsonMessageBytes);

            //HMAC-SHA256 кода на предаваното съобщение (data) се изчислява по следния начин:
            //1.Прави се битова репрезентация с енкодинг UTF-8 на стринга "таен ключ"(secret) предоставен на клиента
            //2.Прави се битова репрезентация с енкодинг UTF-8 на стринга в полето "data"
            //3.Използвайки битовите репрезентации на "тайният ключ" и на "data" се изчислява бинарния HMAC-SHA256 код, като се използва функция за хеширане SHA - 256
            //4.Битовата поредица генерирана от алгоритъма се конвертира като Base64 стринг, който се подава в полето "hmac"

            var keyBytes = System.Text.Encoding.UTF8.GetBytes(registrationData.SecretWord);
            var dataBytes = System.Text.Encoding.UTF8.GetBytes(data);

            string hmac;
            using (HMACSHA256 hmacsha256 = new HMACSHA256(keyBytes))
            {
                byte[] hashmessage_arr = hmacsha256.ComputeHash(dataBytes);
                hmac = System.Convert.ToBase64String(hashmessage_arr);
            }

            //1) clientId – тук ще бъде подаден уникалният идентификатора на клиента, който е бил подаден при изначалната заявка към https://portalUrl.com/vpos/payment
            //2) hmac – hmac - sha256 код изчислен върху полето data
            //3) data – данни за предаваното съобщение.

            var request = new Dictionary<string, string>();
            request.Add("clientId", registrationData.Cin);
            request.Add("hmac", hmac);
            request.Add("data", data);

            return request;
        }

        private void ProcessPaymentResponse(PaymentRequests.Models.PaymentRequest paymentRequest, PaymentMessageResponse paymentResponse, string messageResponse, Obligation obligation)
        {
            if (paymentResponse != null && paymentResponse.UnacceptedReceiptJson != null
                && paymentResponse.UnacceptedReceiptJson.Errors != null)
            {
                _logger.LogError($"create pep daeu payment request with obligation identifier: {obligation.ObligationIdentifier} and obligation expiration date: {obligation.ExpirationDate} and external payment number: {paymentRequest.ExternalPortalPaymentNumber}");

                throw new Exception(string.Join("; ", paymentResponse.UnacceptedReceiptJson.Errors));
            }
            else if (paymentResponse != null
                && paymentResponse.AcceptedReceiptJson != null
                && !string.IsNullOrEmpty(paymentResponse.AcceptedReceiptJson.Id))
            {
                paymentRequest.ExternalPortalPaymentNumber = paymentResponse.AcceptedReceiptJson.Id;
                paymentRequest.SendDate = paymentResponse.AcceptedReceiptJson.RegistrationTime;
            }
            else
            {
                _logger.LogError($"create pep daeu payment request with obligation identifier: {obligation.ObligationIdentifier} and obligation expiration date: {obligation.ExpirationDate} and external payment number: {paymentRequest.ExternalPortalPaymentNumber}");

                throw new Exception("Error during Pep payment processing. PEP Response: " + messageResponse);
            }
        }

        private VPosRequest InitPepPosRequestObj(PaymentRequests.Models.PaymentRequest paymentRequest, RegistrationData registrationData)
        {
            var pepPosRequest = new VPosRequest()
            {
                Id = paymentRequest.ExternalPortalPaymentNumber.ToString(),
                OkUrl = paymentRequest.AdditionalData.ContainsKey("okCancelUrl") ? paymentRequest.AdditionalData["okCancelUrl"] : null,
                CancelUrl = paymentRequest.AdditionalData.ContainsKey("okCancelUrl") ? paymentRequest.AdditionalData["okCancelUrl"] : null
            };

            return pepPosRequest;
        }

        private void SetPepPosRequestData(PaymentRequests.Models.PaymentRequest paymentRequest, VPosRequest pepPosRequest, RegistrationData registrationData)
        {
            string jsonMessageString = JsonSerializer.Serialize(pepPosRequest);

            //Данните за предаваното съобщение в полето "data" се изчисляват по следния начин:
            //1.Прави се битова репрезентация с енкодинг UTF-8 на JSON стринга на предаваното съобщение описан в поле Data Params на услугата;
            //2.Битовата поредица се конвертира като Base64 стринг, който се подава в полето "data".
            var jsonMessageBytes = System.Text.Encoding.UTF8.GetBytes(jsonMessageString);
            var data = Convert.ToBase64String(jsonMessageBytes);

            //HMAC-SHA256 кода на предаваното съобщение (data) се изчислява по следния начин:
            //1.Прави се битова репрезентация с енкодинг UTF-8 на стринга "таен ключ"(secret) предоставен на клиента
            //2.Прави се битова репрезентация с енкодинг UTF-8 на стринга в полето "data"
            //3.Използвайки битовите репрезентации на "тайният ключ" и на "data" се изчислява бинарния HMAC-SHA256 код, като се използва функция за хеширане SHA - 256
            //4.Битовата поредица генерирана от алгоритъма се конвертира като Base64 стринг, който се подава в полето "hmac"

            var keyBytes = System.Text.Encoding.UTF8.GetBytes(registrationData.SecretWord);
            var dataBytes = System.Text.Encoding.UTF8.GetBytes(data);

            string hmac;
            using (HMACSHA256 hmacsha256 = new HMACSHA256(keyBytes))
            {
                byte[] hashmessage_arr = hmacsha256.ComputeHash(dataBytes);
                hmac = System.Convert.ToBase64String(hashmessage_arr);
            }

            string clientId = registrationData.Cin;

            //1) clientId – тук ще бъде подаден уникалният идентификатора на клиента, който е бил подаден при изначалната заявка към https://portalUrl.com/vpos/payment
            //2) hmac – hmac - sha256 код изчислен върху полето data
            //3) data – данни за предаваното съобщение.

            paymentRequest.AdditionalData.Add("clientId", clientId);
            paymentRequest.AdditionalData.Add("hmac", hmac);
            paymentRequest.AdditionalData.Add("data", data);
        }

        

        #endregion
    }
}
