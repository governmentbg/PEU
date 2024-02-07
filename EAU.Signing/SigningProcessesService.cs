using CNSys;
using CNSys.Data;
using EAU.Common;
using EAU.Security;
using EAU.Signing.BSecureDSSL;
using EAU.Signing.Configuration;
using EAU.Signing.Models;
using EAU.Signing.Models.SearchCriteria;
using EAU.Signing.ReMessageHandlers;
using EAU.Signing.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Signing
{
    /// <summary>
    /// Интерфейс на услуга за работа с процеси по подписване.
    /// </summary>
    public interface ISigningProcessesService
    {
        /// <summary>
        /// Търси процеси за подписване.
        /// </summary>
        /// <param name="criteria">Критерий за търсене</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Списък с процеси по подписване.</returns>
        Task<IEnumerable<SigningProcess>> SearchAsync(SigningProcessesSearchCriteria criteria, CancellationToken cancellationToken);

        /// <summary>
        /// Създава процес по подписване.
        /// </summary>
        /// <param name="request">Заявка за подписване.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Идентификатор на процеса.</returns>
        Task<OperationResult<Guid>> CreateSigningProcessAsync(SigningRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Стартира процес по отказване на заявката за подписване.
        /// </summary>
        /// <param name="processID">Идентификатор на процеса за подписване.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OperationResult> StartRejectingProcessAsync(Guid processID, CancellationToken cancellationToken);

        /// <summary>
        /// Отказва подписването.
        /// </summary>
        /// <param name="processID">Идентификатор на процеса</param>
        /// <param name="cancellationToken"></param>
        /// <returns>обект резултат от операцията.</returns>
        Task RejectSigningProcessAsync(Guid processID, CancellationToken cancellationToken);

        /// <summary>
        /// Приключва процеса по подписване.
        /// </summary>
        /// <param name="processID">Идентификатор на процеса за подписване.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CompleteSigningProcessAsync(Guid processID, CancellationToken cancellationToken);

        /// <summary>
        /// Потребител започва отдалечено подписване.
        /// </summary>
        /// <param name="processID">Идентификатор на процеса за подписване.</param>
        /// <param name="signerID">Идентификатор на подписващ.</param>
        /// <param name="channel">Избран канал за подписване.</param>
        /// <param name="additionalSigningData">Допълнителни данните за успешно направена заявка за отдалечено подписване.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OperationResult> SignerStartRemoteSigningAsync(Guid processID, long signerID, SigningChannels channel, RemoteSignRequestAdditionalData additionalSigningData, CancellationToken cancellationToken);

        /// <summary>
        /// Потребител отказва подписване по определен канал.
        /// </summary>
        /// <param name="processID">Идентификатор на процеса за подписване.</param>
        /// <param name="signerID">Идентификатор на подписващ.</param>
        /// <param name="reson"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OperationResult> SignerRejectRemoteSigningAsync(Guid processID, long signerID, string reson, CancellationToken cancellationToken);

        /// <summary>
        /// Потребител приключва подписването.
        /// </summary>
        /// <param name="processID">Идентификатор на процеса за подписване.</param>
        /// <param name="signerID">Идентификатор на подписващ.</param>
        /// <param name="signedContent"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OperationResult> SignerCompleteRemoteSigningAsync(Guid processID, long signerID, Stream signedContent, CancellationToken cancellationToken);

        /// <summary>
        /// Потребителя приключва локално подписване.
        /// </summary>
        /// <param name="processID">Идентификатор на процеса за подписване.</param>
        /// <param name="signerID">Идентификатор на подписващ.</param>
        /// <param name="signedContent"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OperationResult> SignerSignedLocalAsync(Guid processID, long signerID, Stream signedContent, CancellationToken cancellationToken);

        /// <summary>
        /// Връща процес за подписване по подадени канал и транзакция в системата на доставчика на отдалечено подписване.
        /// </summary>
        /// <param name="signingChannel">Избран канал за подписване.</param>
        /// <param name="TransactionID">Идентификатор на заявка за подписване в системата на доставчик на услуга по отдалечено подписване.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="mustLoadContent"></param>
        /// <param name="withLock"></param>
        /// <returns>SigningProcess</returns>
        Task<SigningProcess> GetSigningProcessByChannelAndTransactionID(SigningChannels signingChannel, string TransactionID, CancellationToken cancellationToken, bool mustLoadContent = false, bool withLock = false);

        /// <summary>
        /// Изтрива процеси по подписване.
        /// </summary>
        /// <param name="guids">Идентификатори на процеси по подписване.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OperationResult> DeleteSigningProcessesAsync(Guid[] guids, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Реализация  на интерфейс ISigningProcessesService за работа с процеси за подписване.
    /// </summary>
    internal class SigningProcessesService : ISigningProcessesService
    {
        #region Private members

        private static readonly List<string> fileNameUnalowedSymbols = new List<string>() { "<", ">", ":", "\"", "\\", "/", "|", "?", "*", "'", "!", "#", "$", "%", "&", "+", "=", "№" };

        private readonly ISigningProcessesRepository _signingProcessRepository;
        private readonly ISignersRepository _signerRepository;
        private readonly IDbContextOperationExecutor _dbContextOperationExecutor;
        private readonly IDocumentSigningtUtilityService _docSigningUtilityService;
        private readonly IActionDispatcher _actionDispatcher;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IEAUUserAccessor _EPZEUUserAccessor;
        private readonly IOptionsMonitor<SignModuleGlobalOptions> _signModuleOptions;
        private readonly ILogger _logger;

        #endregion

        #region Constructor

        public SigningProcessesService(ISigningProcessesRepository signingProcessRepository
            , ISignersRepository signerRepository
            , IDbContextOperationExecutor dbContextOperationExecutor
            , IDocumentSigningtUtilityService docSigningUtilityService
            , IActionDispatcher actionDispatcher
            , IHttpClientFactory clientFactory
            , IEAUUserAccessor ePZEUUserAccessor
            , IOptionsMonitor<SignModuleGlobalOptions> signModuleOptions
            , ILogger<SigningProcessesService> logger)
        {
            _signingProcessRepository = signingProcessRepository;
            _signerRepository = signerRepository;
            _dbContextOperationExecutor = dbContextOperationExecutor;
            _docSigningUtilityService = docSigningUtilityService;
            _actionDispatcher = actionDispatcher;
            _clientFactory = clientFactory;
            _EPZEUUserAccessor = ePZEUUserAccessor;
            _signModuleOptions = signModuleOptions;
            _logger = logger;
        }

        #endregion

        #region ISigningProcessesService

        public Task<IEnumerable<SigningProcess>> SearchAsync(SigningProcessesSearchCriteria criteria, CancellationToken cancellationToken)
        {
            return _signingProcessRepository.SearchAsync(criteria, cancellationToken);
        }

        public Task<OperationResult<Guid>> CreateSigningProcessAsync(SigningRequest request, CancellationToken cancellationToken)
        {
            var process = ConvertToSigningProcess(request);

            return _dbContextOperationExecutor.ExecuteAsync(async (dbContext, token) =>
           {
               await _signingProcessRepository.CreateAsync(process, token);

               foreach (var signer in process.Signers)
               {
                   signer.ProcessID = process.ProcessID;
                   await _signerRepository.CreateAsync(signer, token);
               }

               return new OperationResult<Guid>(OperationResultTypes.SuccessfullyCompleted) { Result = process.ProcessID.Value };
           }, cancellationToken);
        }

        public async Task<OperationResult> StartRejectingProcessAsync(Guid processID, CancellationToken cancellationToken)
        {
            return await _dbContextOperationExecutor.ExecuteAsync(async (dbContext) =>
            {
                var process = (await _signingProcessRepository.SearchAsync(new SigningProcessesSearchCriteria()
                {
                    ProcessID = processID,
                    WithLock = true
                }
                , cancellationToken)).SingleOrDefault();

                if (process == null)
                    return new OperationResult("GL_NO_DATA_FOUND_L", "GL_NO_DATA_FOUND_L");

                if (process.Status != SigningRequestStatuses.InProcess)
                    return new OperationResult("GL_SIGN_FAIL_E", "GL_SIGN_FAIL_E");

                process.Status = SigningRequestStatuses.Rejecting;

                await _signingProcessRepository.UpdateAsync(process, cancellationToken);

                await _actionDispatcher.SendAsync(new SigningProcessRejectMessage() { ProcessID = process.ProcessID });

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            });
        }

        public async Task RejectSigningProcessAsync(Guid processID, CancellationToken cancellationToken)
        {
            await _dbContextOperationExecutor.ExecuteAsync(async (dbContext) =>
            {
                try
                {
                    SigningProcess process = (await _signingProcessRepository.SearchAsync(new SigningProcessesSearchCriteria()
                    {
                        ProcessID = processID,
                        LoadSigners = true,
                        WithLock = true
                    }, cancellationToken)).SingleOrDefault();

                    if (process == null)
                        return new OperationResult(OperationResultTypes.SuccessfullyCompleted);

                    if (process.Status != SigningRequestStatuses.Rejecting)
                        throw new NotSupportedException(string.Format("Процесът с ProcessID: {0} не в статус подходящ за отказ.", processID.ToString()));

                    _logger.LogInformation(string.Format("Rejecting sign process with processId: {0} step:", process.ProcessID.Value.ToString()));

                    using (var client = _clientFactory.CreateClient(process.CallbackHttpClientName))
                    {
                        string relativeRejectUrl = process.RejectedCallbackUrl.Substring(client.BaseAddress.OriginalString.Length);

                        string url = string.Format("{0}?signingGiud={1}&userSessionID={2}&loginSessionID={3}&ipAddress={4}&userCIN={5}",
                            relativeRejectUrl
                            , process.ProcessID.Value
                            , process.AdditionalData.SessionData.UserSessionID
                            , process.AdditionalData.SessionData.LoginSessionID
                            , process.AdditionalData.SessionData.IpAddress
                            , process.AdditionalData.SessionData.UserCIN);

                        var response = await client.PostAsync(url, null);

                        response.EnsureSuccessStatusCode();
                    }

                    _logger.LogInformation("1. Successfull notification.");

                    await _signingProcessRepository.DeleteSigningProcessesAsync(new Guid[] { process.ProcessID.Value }, cancellationToken);

                    _logger.LogInformation("2. proces was deleted successfull.");

                    return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    throw;
                }
            });
        }

        public async Task CompleteSigningProcessAsync(Guid processID, CancellationToken cancellationToken)
        {
            await _dbContextOperationExecutor.ExecuteAsync(async (dbContext) =>
            {
                SigningProcess process = null;

                try
                {
                    process = (await _signingProcessRepository.SearchAsync(new SigningProcessesSearchCriteria()
                    {
                        ProcessID = processID,
                        LoadContent = true,
                        LoadSigners = true,
                        WithLock = true
                    }, cancellationToken)).SingleOrDefault();

                    if (process == null)
                        return new OperationResult(OperationResultTypes.SuccessfullyCompleted);

                    if (process.Status != SigningRequestStatuses.Completing)
                        throw new NotSupportedException(string.Format("Процесът с ProcessID: {0} не в статус подходящ за приключване.", processID.ToString()));

                    _logger.LogInformation(string.Format("Compliting sign process with processId: {0} step:", process.ProcessID.Value.ToString()));

                    using (var client = _clientFactory.CreateClient(process.CallbackHttpClientName))
                    {
                        var formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
                        using (var mfdc = new MultipartFormDataContent(formDataBoundary))
                        using (process.Content)
                        {
                            mfdc.Add(new StreamContent(process.Content), "file", process.FileName);

                            string relativeCompletedUrl = process.CompletedCallbackUrl.Substring(client.BaseAddress.OriginalString.Length);

                            string url = string.Format("{0}?signingGiud={1}&userSessionID={2}&loginSessionID={3}&ipAddress={4}&userCIN={5}",
                                                        relativeCompletedUrl
                                                        , process.ProcessID.Value
                                                        , process.AdditionalData.SessionData.UserSessionID
                                                        , process.AdditionalData.SessionData.LoginSessionID
                                                        , process.AdditionalData.SessionData.IpAddress
                                                        , process.AdditionalData.SessionData.UserCIN);

                            var response = await client.PostAsync(url, mfdc);

                            response.EnsureSuccessStatusCode();
                        }
                    }

                    _logger.LogInformation("1. Successfull notification.");

                    await _signingProcessRepository.DeleteSigningProcessesAsync(new Guid[] { process.ProcessID.Value }, cancellationToken);

                    _logger.LogInformation("2. proces was deleted successfull.");

                    return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    throw;
                }
                finally
                {
                    if (process != null && process.Content != null)
                    {
                        process.Content.Dispose();
                        process.Content = null;
                    }
                }
            });
        }

        public async Task<OperationResult> SignerStartRemoteSigningAsync(Guid processID, long signerID, SigningChannels channel, RemoteSignRequestAdditionalData additionalSigningData, CancellationToken cancellationToken)
        {
            var res = await _dbContextOperationExecutor.ExecuteAsync<object>(async (dbContext, token) =>
            {
                var process = (await _signingProcessRepository.SearchAsync(new SigningProcessesSearchCriteria()
                {
                    ProcessID = processID,
                    LoadSigners = true,
                    WithLock = true
                }, cancellationToken)).SingleOrDefault();

                #region Проверка

                Signer currentSigner = null;
                if (process == null || process.Signers == null || (currentSigner = process.Signers.SingleOrDefault(s => s.SignerID == signerID)) == null)
                    return new OperationResult<object>("GL_NO_DATA_FOUND_L", "GL_NO_DATA_FOUND_L");

                //Потребителят вече е започнал подписването.
                if (currentSigner.Status != SignerSigningStatuses.Waiting)
                    return new OperationResult<object>("GL_SIGN_ONGOING_SIGNING_E", "GL_SIGN_ONGOING_SIGNING_E");

                //Има не завършено подписване започнато преди вас.
                if (process.Signers.Any(s => s.SignerID != signerID && s.Status == SignerSigningStatuses.StartSigning))
                    return new OperationResult<object>("GL_SIGN_ONGOING_SIGNING_E", "GL_SIGN_ONGOING_SIGNING_E");

                #endregion

                process.AdditionalData.SessionData = CreateSessionData();

                await _signingProcessRepository.UpdateAsync(process, cancellationToken);

                currentSigner.Status = SignerSigningStatuses.StartSigning;
                currentSigner.SigningChannel = channel;
                currentSigner.RejectReson = null;
                currentSigner.TransactionID = additionalSigningData.TransactionID;
                currentSigner.SigningAdditionalData = additionalSigningData;

                await _signerRepository.UpdateAsync(currentSigner, token);

                return new OperationResult<object>(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);

            return res;
        }

        public async Task<OperationResult> SignerRejectRemoteSigningAsync(Guid processID, long signerID, string reson, CancellationToken cancellationToken)
        {
            var res = await _dbContextOperationExecutor.ExecuteAsync<object>(async (ctx, token) =>
            {
                var process = (await _signingProcessRepository.SearchAsync(new SigningProcessesSearchCriteria()
                {
                    ProcessID = processID,
                    LoadSigners = true,
                    WithLock = true
                }, token)).SingleOrDefault();
                Signer currentSigner = process.Signers.SingleOrDefault(s => s.SignerID == signerID);

                if (process == null
                || process.Status != SigningRequestStatuses.InProcess
                || currentSigner == null)
                {
                    return new OperationResult<object>("GL_NO_DATA_FOUND_L", "GL_NO_DATA_FOUND_L");
                }

                if (currentSigner.SigningChannel.HasValue
                    && currentSigner.SigningChannel != SigningChannels.BtrustLocal
                    && currentSigner.Status != SignerSigningStatuses.StartSigning)
                    throw new NotSupportedException("Некоректен статус на подписващия.");

                currentSigner.Status = SignerSigningStatuses.Waiting;
                currentSigner.SigningChannel = null;
                currentSigner.SigningAdditionalData = null;
                currentSigner.TransactionID = null;
                currentSigner.RejectReson = reson;

                await _signerRepository.UpdateAsync(currentSigner, token);

                return new OperationResult<object>(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);

            return res;
        }

        public Task<OperationResult> SignerCompleteRemoteSigningAsync(Guid processID, long signerID, Stream signedContent, CancellationToken cancellationToken)
        {
            return SignerSignedAsync(processID, signerID, signedContent, cancellationToken, true);
        }

        public Task<OperationResult> SignerSignedLocalAsync(Guid processID, long signerID, Stream signedContent, CancellationToken cancellationToken)
        {
            return SignerSignedAsync(processID, signerID, signedContent, cancellationToken);
        }

        public async Task<SigningProcess> GetSigningProcessByChannelAndTransactionID(SigningChannels signingChannel, string TransactionID, CancellationToken cancellationToken, bool mustLoadContent = false, bool withLock = false)
        {
            Signer signer = (await _signerRepository.SearchAsync(new SignerSearchCriteria()
            {
                SigningChannel = signingChannel,
                TransactionID = TransactionID
            }, cancellationToken)).SingleOrDefault();

            if (signer == null)
                return null;

            return (await SearchAsync(new SigningProcessesSearchCriteria()
            {
                ProcessID = signer.ProcessID,
                LoadSigners = true,
                LoadContent = mustLoadContent,
                WithLock = withLock
            }, cancellationToken)).SingleOrDefault();
        }

        public async Task<OperationResult> DeleteSigningProcessesAsync(Guid[] guids, CancellationToken cancellationToken)
        {
            await _signingProcessRepository.DeleteSigningProcessesAsync(guids, cancellationToken);

            return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
        }

        #endregion

        #region Helpers

        private SigningProcess ConvertToSigningProcess(SigningRequest request)
        {
            if (request == null
                || request.Content == null
                || request.Format == null
                || string.IsNullOrEmpty(request.FileName)
                || string.IsNullOrEmpty(request.ContentType)
                || string.IsNullOrEmpty(request.RejectedCallbackUrl)
                || string.IsNullOrEmpty(request.CompletedCallbackUrl)
                || request.SignerRequests == null
                || request.SignerRequests.Count == 0)
                throw new ArgumentException();

            string fileName = request.FileName;

            if (fileNameUnalowedSymbols.Any(ch=> fileName.Contains(ch)))
            {
                foreach (var ch in fileNameUnalowedSymbols)
                {
                    fileName = fileName.Replace(ch, "");
                }
            }    

            if (request.FileName.Length > 100)
            {
                //Ограничаваме името на файла до 100 символа.
                fileName = string.Format("{0}{1}", request.FileName.Substring(0, 96), request.FileName.Substring(request.FileName.Length - 4));
            }

            SigningProcess result = new SigningProcess()
            {
                Format = request.Format,
                Content = request.Content,
                ContentType = request.ContentType,
                FileName = fileName,
                Level = GetSigningLevel(),
                DigestMethod = DigestMethods.SHA256,
                RejectedCallbackUrl = request.RejectedCallbackUrl,
                CompletedCallbackUrl = request.CompletedCallbackUrl,
                AdditionalData = new SignProcessAdditionalData() 
                {
                    SessionData = CreateSessionData(),
                    SignatureXpath = request.SignatureXpath,
                    SignatureXPathNamespaces = request.SignatureXPathNamespaces
                },
                Signers = new List<Signer>()
            };

            switch (request.Format.Value)
            {
                case SigningFormats.CAdES:
                    result.Type = SigningPackingTypes.ENVELOPING;
                    break;
                case SigningFormats.PAdES:
                    result.Type = SigningPackingTypes.ENVELOPED;
                    break;
                case SigningFormats.XAdES:
                    result.Type = SigningPackingTypes.ENVELOPED;
                    break;
                default:
                    throw new NotImplementedException();
            }

            request.SignerRequests.ForEach(sr =>
            {
                var tmpSigner = new Signer()
                {
                    Name = sr.Name,
                    Ident = sr.Ident,
                    Order = sr.Order
                };

                result.Signers.Add(tmpSigner);
            });

            return result;
        }

        private SigningLevels? GetSigningLevel()
        {
            SigningLevels? level = null;

            if (string.IsNullOrEmpty(_signModuleOptions.CurrentValue.SIGN_SIGNATURE_LEVEL))
                throw new KeyNotFoundException("Missign configuration for signature level param: SIGN_SIGNATURE_LEVEL");

            foreach (SigningLevels item in SigningLevels.GetValues(typeof(SigningLevels)))
            {
                if (string.Compare(item.ToString(), _signModuleOptions.CurrentValue.SIGN_SIGNATURE_LEVEL, true) == 0)
                {
                    level = item;
                    break;
                }
            }

            return level;
        }

        private Models.SessionData CreateSessionData()
        {
            Models.SessionData data = new Models.SessionData()
            {
                UserSessionID = _EPZEUUserAccessor.UserSessionID,
                LoginSessionID = _EPZEUUserAccessor.User?.LoginSessionID,
                IpAddress = _EPZEUUserAccessor.RemoteIpAddress.ToString(),
                UserCIN = _EPZEUUserAccessor.User?.CIN
            };

            return data; 
        }

        private async Task<OperationResult> StartCompletingProcessAsync(SigningProcess process, CancellationToken cancellationToken)
        {
            return await _dbContextOperationExecutor.ExecuteAsync(async (dbContext) =>
            {
                process.Status = SigningRequestStatuses.Completing;

                await _signingProcessRepository.UpdateAsync(process, cancellationToken);

                await _actionDispatcher.SendAsync(new SigningProcessCompleteMessage() { ProcessID = process.ProcessID });

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            });
        }

        private async Task<OperationResult> SignerSignedAsync(Guid processID, long signerID, Stream signedContent, CancellationToken cancellationToken, bool isRemote = false)
        {
            if (!signedContent.CanSeek)
                throw new NotSupportedException("signedContent must be seekable");

            if (_signModuleOptions.CurrentValue.SIGN_SKIP_VALIDATION_AFTER_SIGN == null || _signModuleOptions.CurrentValue.SIGN_SKIP_VALIDATION_AFTER_SIGN.GetValueOrDefault() < 0)
                throw new KeyNotFoundException("Missing or invalid configuration param SIGN_SKIP_VALIDATION_AFTER_SIGN");

            ValidateDocumentResponseDto validationResult = null;
            SigningProcess process = await _signingProcessRepository.ReadAsync(processID, cancellationToken);

            if (_signModuleOptions.CurrentValue.SIGN_SKIP_VALIDATION_AFTER_SIGN.GetValueOrDefault() == 0)
            {
                validationResult = await _docSigningUtilityService.SignaturesVerificationAsync(signedContent, process.FileName, true);
                bool isValid = string.Compare(validationResult.DocumentStatusValid, "TRUE", true) == 0 && validationResult.Signatures.Count() > 0;

                if (!isValid)
                {
                    _logger.LogWarning("Неуспешна валидация на подпис. Съобщение за грешка: {message}. Брой валидни подписи: {validSignaturesCount}. Дата на валидация: {validationDateTime}. Подписи в документа: {newLine} {signatures}"
                        , validationResult.Message
                        , validationResult.ValidSignaturesCount
                        , validationResult.ValidationDateTime
                        , Environment.NewLine
                        , validationResult.Signatures != null && validationResult.Signatures.Any() ? string.Join(Environment.NewLine, validationResult.Signatures.Select(el => el.ToJson())) : "");
                    return new OperationResult("GL_SIGN_INAVLD_SIGNATURE_E", "GL_SIGN_INAVLD_SIGNATURE_E");
                }
            }

            return await _dbContextOperationExecutor.ExecuteAsync<object>(async (dbContext, token) =>
            {
                var signedContentPosing = signedContent.Position;

                process = (await _signingProcessRepository.SearchAsync(new SigningProcessesSearchCriteria()
                {
                    ProcessID = processID,
                    LoadSigners = true,
                    WithLock = true
                }, token)).SingleOrDefault();

                #region Проверка

                if (process == null)
                    return new OperationResult<object>("GL_NO_DATA_FOUND_L", "GL_NO_DATA_FOUND_L");

                if (process.Status != SigningRequestStatuses.InProcess)
                    return new OperationResult<object>("GL_SIGN_FAIL_E", "GL_SIGN_FAIL_E");

                var currentSigner = process.Signers.SingleOrDefault(s => s.SignerID == signerID);

                if (currentSigner == null)
                    return new OperationResult<object>("GL_NO_DATA_FOUND_L", "GL_NO_DATA_FOUND_L");

                if (currentSigner.SigningChannel.HasValue && currentSigner.Status != SignerSigningStatuses.StartSigning)
                    return new OperationResult<object>("GL_SIGN_NO_DATA_E", "GL_SIGN_NO_DATA_E");

                if (_signModuleOptions.CurrentValue.SIGN_SKIP_VALIDATION_AFTER_SIGN.GetValueOrDefault() == 0)
                {
                    #region Signatur validation

                    //1. Проверка за дублиране на сертификати използвани за подписване.
                    if (validationResult.Signatures.Count() != validationResult.Signatures.Select(s => s.SignerBase64EncodedCertificate).Distinct().Count())
                    {
                        //Няколко души са подписали документа с един и същи цифров сертификат.
                        return new OperationResult<object>("GL_SIGN_CERT_DOUBLE_USE_E", "GL_SIGN_CERT_DOUBLE_USE_E");
                    }

                    //2. Проверка бр. подписи = бр. подписващи.
                    if ((process.Signers.Count == 1 || process.Signers.Where(s => s.SignerID != currentSigner.SignerID).All(s => s.Status == SignerSigningStatuses.Signed))
                       && validationResult.ValidSignaturesCount != process.Signers.Count)
                    {
                        //Броя на положените подписи в документа не съответства на броя на подписващите.
                        new OperationResult<object>("GL_SIGN_INVALID_NUMB_OF_SIGNATURES_E", "GL_SIGN_INVALID_NUMB_OF_SIGNATURES_E");
                    }

                    #endregion
                }

                #endregion

                if (!isRemote)
                {
                    //1. Записваме данни за сесията на потребителя положил подписа.
                    process.AdditionalData.SessionData = CreateSessionData();

                    await _signingProcessRepository.UpdateAsync(process, token);
                }

                //2. Променяме статуса и канала на подписващия.
                currentSigner.Status = SignerSigningStatuses.Signed;
                currentSigner.RejectReson = null;
                currentSigner.SigningChannel = isRemote ? currentSigner.SigningChannel : SigningChannels.BtrustLocal;

                await _signerRepository.UpdateAsync(currentSigner, token);

                //3. Обновяваме съдържанието на процеса с новото съдържащо подписа на подписващия.
                signedContent.Position = signedContentPosing;
                await _signingProcessRepository.UploadContentAsync(signedContent, processID, token);

                //4. Проверяваме дали е подписал последния подписващ и ако е така приклщчваме процеса.
                if (process.Signers.All(s => s.Status == SignerSigningStatuses.Signed))
                {
                    var completingProcessRes = await StartCompletingProcessAsync(process, token);

                    if (completingProcessRes.IsSuccessfullyCompleted)
                        return new OperationResult<object>(OperationResultTypes.SuccessfullyCompleted);
                    else
                        return new OperationResult<object>(completingProcessRes.Errors);
                }

                return new OperationResult<object>(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }

        #endregion
    }
}
