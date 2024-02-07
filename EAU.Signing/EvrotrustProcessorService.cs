using CNSys;
using CNSys.Data;
using EAU.Common;
using EAU.Signing.BSecureDSSL;
using EAU.Signing.Configuration;
using EAU.Signing.Evrotrust;
using EAU.Signing.Helpers;
using EAU.Signing.Models;
using EAU.Signing.Models.SearchCriteria;
using EAU.Signing.ReMessageHandlers;
using EAU.Utilities;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Signing
{
    public interface IEvrotrustProcessorService
    {
        Task<OperationResult> CreateRemoteSignRequestAsync(Guid processID, long SignerID, UserIdentifyData userInfo, CancellationToken cancellationToken);

        Task AcceptRemoteCallbackNotificationAsync(string TransactionID, int? status, CancellationToken cancellationToken);

        Task ProcessRemoteCallbackNotificationAsync(string TransactionID, int? status, CancellationToken cancellationToken);

        Task<OperationResult> SignerRejectRemoteSigningAsync(Guid processID, long signerID, string reson, CancellationToken cancellationToken);
    }

    internal class EvrotrustProcessorService : IEvrotrustProcessorService
    {
        #region private members

        private readonly IOptionsMonitor<SignModuleGlobalOptions> _signModuleOptions;
        private readonly ISigningProcessesService _signingProcessesService = null;
        private readonly IEvrotrustClientFactory _evrotrustClientFactory = null;
        private readonly IActionDispatcher _actionDispatcher;
        private readonly ILogger _logger;
        private readonly IDocumentSigningtUtilityService _documentSigningUtilityService;

        #endregion

        #region Constructor

        public EvrotrustProcessorService(ISigningProcessesService signingProcessesService
            , IEvrotrustClientFactory evrotrustClientFactory
            , IActionDispatcher actionDispatcher
            , ILogger<EvrotrustProcessorService> logger
            , IOptionsMonitor<SignModuleGlobalOptions> signModuleOptions
            , IDocumentSigningtUtilityService documentSigningUtilityService)
        {
            _signingProcessesService = signingProcessesService;
            _evrotrustClientFactory = evrotrustClientFactory;
            _actionDispatcher = actionDispatcher;
            _logger = logger;
            _documentSigningUtilityService = documentSigningUtilityService;
            _signModuleOptions = signModuleOptions;
        }

        #endregion

        public async Task<OperationResult> CreateRemoteSignRequestAsync(Guid processID, long SignerID, UserIdentifyData userInfo, CancellationToken cancellationToken)
        {
            try
            {
                IEvrotrustClient evrotrustClient = _evrotrustClientFactory.GetEvrotrustClient();

                //1. Проверка на потребителя
                OperationResult checkUserRes = await CheckUser(evrotrustClient, userInfo);

                if (!checkUserRes.IsSuccessfullyCompleted)
                {
                    return new OperationResult(checkUserRes.Errors);
                }

                //2. Взимаме сертификата на потребителя
                ResultGetUserCertificate certData = await GetUserCert(evrotrustClient, userInfo);

                //3. Създава и изпраща заявка за подписване
                RemoteSignRequestAdditionalData signRequestAdditionalData = await CreateSignRequest(evrotrustClient, processID, userInfo, certData, cancellationToken);

                //4. Вкарваме подписващия в статус "започнало отдалечено подписване", записваме сесиините му данни 
                //и данните за транзакцията в системата доставчик на отдалечено подписване 
                OperationResult signerStartSigningResult = await _signingProcessesService.SignerStartRemoteSigningAsync(processID, SignerID, SigningChannels.EvrotrustRemote, signRequestAdditionalData, cancellationToken);

                return signerStartSigningResult;
            }
            catch (BSecureDSSL.SwaggerException btrustEx)
            {
                _logger.LogException(btrustEx);

                return new OperationResult<object>("GL_SIGN_FAIL_E", "GL_SIGN_FAIL_E");
            }
            catch (Evrotrust.SwaggerException evrotrustEx)
            {
                //Логваме като грешки само кодовете различни от 438, 442 и 450
                if (evrotrustEx.StatusCode != 438 && evrotrustEx.StatusCode != 442 && evrotrustEx.StatusCode != 450)
                {
                    _logger.LogException(evrotrustEx);
                }

                return new OperationResult<object>(ConvertEvrotrustExceptionToIError(evrotrustEx));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                return new OperationResult<object>("GL_SIGN_FAIL_E", "GL_SIGN_FAIL_E");
            }
        }

        public async Task AcceptRemoteCallbackNotificationAsync(string TransactionID, int? status, CancellationToken cancellationToken)
        {
            _logger.LogInformation(string.Format("Evrotrust notification for Transaction: {0}, and status: {1}", TransactionID, status.HasValue ? status.Value.ToString() : ""));

            SigningProcess process = await _signingProcessesService.GetSigningProcessByChannelAndTransactionID(SigningChannels.EvrotrustRemote, TransactionID, cancellationToken, false);

            //Това служи само за бързодействие на кода.
            if (process == null)
            {
                return;
            }

            await _actionDispatcher.SendAsync(new EvrotrustProcessCallbackNotificationMessage() { TransactionID = TransactionID, Status = status });
        }

        public async Task ProcessRemoteCallbackNotificationAsync(string TransactionID, int? status, CancellationToken cancellationToken)
        {
            _logger.LogInformation(string.Format("Evrotrust accepted notification for Transaction: {0}, and status: {1}", TransactionID, status.HasValue ? status.Value.ToString() : ""));

            SigningProcess process = null;
            try
            {
                process = await _signingProcessesService.GetSigningProcessByChannelAndTransactionID(SigningChannels.EvrotrustRemote, TransactionID, cancellationToken, true);
                Signer currSigner = process?.Signers.Single(s => s.TransactionID != null && s.TransactionID == TransactionID);

                /*Това служи само за бързодействие на кода. В следващите методи има заключване и бизнес проверки !*/
                if (process == null || currSigner == null || process.Status == SigningRequestStatuses.Completing || process.Status == SigningRequestStatuses.Rejecting)
                    return;

                //status: 1 - Pending, 2 - Signed, 3 - Rejected, 4 - Expired, 5 - Canceled, 99 - On hold

                if (status == 2)
                {
                    IEvrotrustClient evrotrustClient = _evrotrustClientFactory.GetEvrotrustClient();

                    if (process.Format == SigningFormats.PAdES)
                    {
                        await ProcessSignedPdfDocumentAsync(evrotrustClient, process, currSigner, cancellationToken);

                    }
                    else
                    {
                        await ProcessSignedDocumentByHashAsync(evrotrustClient, process, currSigner, cancellationToken);
                    }
                }
                else if (status == 3 || status == 4 || status == 5)
                {
                    string reson = status == 3 ? "GL_SIGN_REJECTED_E" : status == 4 ? "GL_SIGN_BTRUST_EXPIRED_E" : "Отменено от Evrotrust.";
                    var res = await _signingProcessesService.SignerRejectRemoteSigningAsync(process.ProcessID.Value, currSigner.SignerID.Value, reson, cancellationToken);

                    if (res.IsSuccessfullyCompleted)
                        return;
                    else
                        throw new Exception("Exception in SignerRejectSigning method.");
                }
            }
            finally
            {
                if (process != null && process.Content != null)
                {
                    process.Content.Dispose();
                    process.Content = null;
                }
            }
        }

        public async Task<OperationResult> SignerRejectRemoteSigningAsync(Guid processID, long signerID, string reson, CancellationToken cancellationToken)
        {
            var client = _evrotrustClientFactory.GetEvrotrustClient();

            var process = (await _signingProcessesService.SearchAsync(new SigningProcessesSearchCriteria()
            {
                ProcessID = processID,
                LoadSigners = true
            }, cancellationToken)).SingleOrDefault();

            var signer = process.Signers.Single(s => s.SignerID == signerID);

            if (signer.Status == SignerSigningStatuses.StartSigning)
            {
                var documentWithdraRequest = new DataDocumentWithdraw()
                {
                    ThreadID = signer.SigningAdditionalData.ThreadID,
                    VendorNumber = _signModuleOptions.CurrentValue.SIGN_EVROTRUST_VENDOR_NUM
                };

                string authorization = EvrotrustApiHelper.GetAuthorizationHeader(_signModuleOptions.CurrentValue.SIGN_EVROTRUST_VENDOR_API_KEY, documentWithdraRequest.ToJson());

                try
                {
                    await client.DocumentWithdrawAsync(authorization, documentWithdraRequest, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                }
            }

            return await _signingProcessesService.SignerRejectRemoteSigningAsync(processID, signerID, reson, cancellationToken);
        }

        #region Helper

        private IErrorCollection ConvertEvrotrustExceptionToIError(Evrotrust.SwaggerException ex)
        {
            var errors = new ErrorCollection();

            switch (ex.StatusCode)
            {
                case 438:
                    //Потребителя не е намерен
                    errors.Add(new TextError("GL_SIGN_MISSING_CUSTMR_EVROTRUST_E", "GL_SIGN_MISSING_CUSTMR_EVROTRUST_E"));
                    break;
                case 442:
                    //Сертификатът не е намерен
                    errors.Add(new TextError("GL_SIGN_MISSING_CERT_EVROTRUST_E", "GL_SIGN_MISSING_CERT_EVROTRUST_E"));
                    break;
                case 450:
                    //Превишен е допустимия размер на файл за подписване.
                    errors.Add(new TextError("GL_SIGN_SIZEOFFILE_LIMIT_EXCEEDED_E", "GL_SIGN_SIZEOFFILE_LIMIT_EXCEEDED_E"));
                    break;
                default:
                    errors.Add(new TextError("GL_SIGN_FAIL_E", "GL_SIGN_FAIL_E"));
                    break;
            }

            return errors;
        }

        private async Task<OperationResult> CheckUser(IEvrotrustClient evrotrustClient, UserIdentifyData userInfo)
        {
            try
            {
                DataCheckUserInfo requestCheckUser = new DataCheckUserInfo()
                {
                    User = userInfo,
                    VendorNumber = _signModuleOptions.CurrentValue.SIGN_EVROTRUST_VENDOR_NUM
                };
                string authorization = EvrotrustApiHelper.GetAuthorizationHeader(_signModuleOptions.CurrentValue.SIGN_EVROTRUST_VENDOR_API_KEY, requestCheckUser.ToJson());
                await evrotrustClient.CheckUserVendorAsync(authorization, requestCheckUser);

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }
            catch (Evrotrust.SwaggerException evrotrustEx)
            {
                _logger.LogException(evrotrustEx);

                string errCode = null;
                switch (evrotrustEx.StatusCode)
                {
                    case 400:
                    case 438:
                        errCode = "GL_SIGN_MISSING_CUSTMR_EVROTRUST_E";
                        break;
                    default:
                        errCode = "GL_SIGN_FAIL_E";
                        break;
                }

                return new OperationResult(errCode, errCode);
            }
        }

        private async Task<ResultGetUserCertificate> GetUserCert(IEvrotrustClient evrotrustClient, UserIdentifyData userInfo)
        {
            DataUserCertificate requestData = new DataUserCertificate()
            {
                VendorNumber = _signModuleOptions.CurrentValue.SIGN_EVROTRUST_VENDOR_NUM,
                User = userInfo,
                Certificate = new DataFilterGetCertificate()
                {
                    Coverage = 20000,
                    IsPidIncluded = true,
                    Type = 1
                }
            };
            string authorization = EvrotrustApiHelper.GetAuthorizationHeader(_signModuleOptions.CurrentValue.SIGN_EVROTRUST_VENDOR_API_KEY, requestData.ToJson());

            return await evrotrustClient.UserVendorCertificateAsync(authorization, requestData);
        }

        private async Task<RemoteSignRequestAdditionalData> CreateSignRequest(IEvrotrustClient evrotrustClient, Guid processID, UserIdentifyData userInfo, ResultGetUserCertificate certData, CancellationToken cancellationToken)
        {
            SigningProcess process = null;
            try
            {
                EvrotrustCryptoUtils.RSAKeys.GenerateRSAToPem(2048, out string privateRsaKeyXml, out string publicRsaKey);
                process = (await _signingProcessesService.SearchAsync(new SigningProcessesSearchCriteria()
                {
                    ProcessID = processID,
                    LoadContent = true
                }, cancellationToken)).SingleOrDefault();

                if (process.Format == SigningFormats.PAdES)
                {
                    ResultDocumentTransaction signRequestResult = null;

                    using (process.Content)
                    using (var ms = new MemoryStream())
                    {
                        process.Content.CopyTo(ms);
                        process.Content.Dispose(); //За оптимизация.

                        ms.Position = 0;
                        string docHashInHex = null;

                        #region Изчислява хеш на PDF файла за подписване

                        byte[] docHash = null;

                        using (var crypt = new SHA512Managed())
                        {
                            docHash = crypt.ComputeHash(ms);
                        }

                        docHashInHex = EvrotrustConvUtils.BytesToHex(docHash);

                        #endregion

                        ms.Position = 0;

                        Evrotrust.FileParameter signFileData = new Evrotrust.FileParameter(ms, "document.pdf", process.ContentType);

                        var dataToCalcAuthorization = new
                        {
                            document = new
                            {
                                description = TransliterationHelper.Transliterate(process.FileName),
                                dateExpire = EvrotrustApiHelper.UnixTime(DateTime.Now.AddDays(3)),
                                coverage = 20000,
                                preview = 1,
                                checksumDocument = docHashInHex
                            },
                            signInfo = new
                            {
                                type = "PDF2",//Так го очакват.
                                algorithm = process.DigestMethod.Value.ToString()
                            },
                            urlCallback = _signModuleOptions.CurrentValue.SIGN_EVROTRUST_CALLBACK_BASE_URL,
                            vendorNumber = _signModuleOptions.CurrentValue.SIGN_EVROTRUST_VENDOR_NUM,
                            publicKey = publicRsaKey,
                            users = new List<object>(1) { userInfo }
                        };

                        string jsonToCalcAuthorization = EAUJsonSerializer.Serialize(dataToCalcAuthorization);
                        string authorization = EvrotrustApiHelper.GetAuthorizationHeader(_signModuleOptions.CurrentValue.SIGN_EVROTRUST_VENDOR_API_KEY, jsonToCalcAuthorization);
                        signRequestResult = await evrotrustClient.AddDocumentDocAsync(authorization, signFileData, jsonToCalcAuthorization);
                    }
                    if (signRequestResult == null
                        ||
                        (string.IsNullOrEmpty(signRequestResult.TransactionID)
                            && (signRequestResult.Transactions == null || signRequestResult.Transactions.Count() != 1 || string.IsNullOrEmpty(signRequestResult.Transactions.ElementAt(0).TransactionID))))
                    {
                        throw new NotSupportedException("Missing TransactionID from Evrotrust.");
                    }

                    if (string.IsNullOrEmpty(signRequestResult.TransactionID))
                    {
                        signRequestResult.TransactionID = signRequestResult.Transactions.ElementAt(0).TransactionID;
                    }

                    return new RemoteSignRequestAdditionalData() { TransactionID = signRequestResult.TransactionID, ThreadID = signRequestResult.ThreadID, UserCert = certData.Certificate, RsaKeyForDecryption = privateRsaKeyXml };
                }
                else
                {
                    DigestResponseDto docHashResult = null;
                    using (process.Content)
                    {
                        docHashResult = await _documentSigningUtilityService.CreateDocumentHashAsync(process.ContentType
                                                                                        , process.FileName
                                                                                        , process.Content
                                                                                        , process.DigestMethod.Value.ToString()
                                                                                        , process.Format.Value
                                                                                        , process.Type.Value.ToString()
                                                                                        , process.Level.Value
                                                                                        , certData.Certificate
                                                                                        , process.AdditionalData.SignatureXpath
                                                                                        , process.AdditionalData.SignatureXPathNamespaces
                                                                                        , process.Format == SigningFormats.PAdES);
                    }

                    DataDocument signRequestData = new DataDocument()
                    {
                        Document = new Document()
                        {
                            Hash = EvrotrustCryptoUtils.GetSha256ToB64(Encoding.UTF8.GetString(docHashResult.DigestValue)),
                            Description = TransliterationHelper.Transliterate(process.FileName),
                            DateExpire = (int)EvrotrustApiHelper.UnixTime(DateTime.Now.AddDays(1))
                        },
                        SignInfo = new SignInfoHash()
                        {
                            Algorithm = process.DigestMethod.ToString()
                        },
                        UrlCallback = _signModuleOptions.CurrentValue.SIGN_EVROTRUST_CALLBACK_BASE_URL,
                        PublicKey = publicRsaKey,
                        CertificateSerialNumber = certData.SerialNumber,
                        VendorNumber = _signModuleOptions.CurrentValue.SIGN_EVROTRUST_VENDOR_NUM,
                        User = userInfo
                    };

                    string authorization = EvrotrustApiHelper.GetAuthorizationHeader(_signModuleOptions.CurrentValue.SIGN_EVROTRUST_VENDOR_API_KEY, signRequestData.ToJson());
                    ResultDocument responseToSignRequest = await evrotrustClient.AddDocumentHashAsync(authorization, signRequestData);

                    return new RemoteSignRequestAdditionalData() { TransactionID = responseToSignRequest.TransactionID, ThreadID = responseToSignRequest.ThreadID, UserCert = certData.Certificate, DigestTime = docHashResult.DigestTime, RsaKeyForDecryption = privateRsaKeyXml };
                }
            }
            finally
            {
                if (process != null && process.Content != null)
                {
                    process.Content.Dispose();
                    process.Content = null;
                }
            }
        }

        private async Task ProcessSignedDocumentByHashAsync(IEvrotrustClient evrotrustClient, SigningProcess process, Signer Signer, CancellationToken cancellationToken)
        {
            try
            {
                string hashSignBase64 = Convert.ToBase64String(await DownloadSignedDataFromEvrotrust(evrotrustClient, Signer));
                OperationResult operationResult = null;
                using (Stream assembledDoc = await _documentSigningUtilityService.AssembleDocumentWithSignatureAsync(process.ContentType
                                                                                        , process.FileName
                                                                                        , process.Content
                                                                                        , process.DigestMethod.Value.ToString()
                                                                                        , process.Format.Value
                                                                                        , process.Type.Value.ToString()
                                                                                        , process.Level.Value
                                                                                        , Signer.SigningAdditionalData.UserCert
                                                                                        , hashSignBase64
                                                                                        , Signer.SigningAdditionalData.DigestTime.Value
                                                                                        , process.AdditionalData.SignatureXpath
                                                                                        , process.AdditionalData.SignatureXPathNamespaces
                                                                                        , process.Format == SigningFormats.PAdES))
                {
                    //Това се прави за оптимизация.
                    if (process.Content != null)
                    {
                        process.Content.Dispose();
                        process.Content = null;
                    }

                    operationResult = await _signingProcessesService.SignerCompleteRemoteSigningAsync(process.ProcessID.Value, Signer.SignerID.Value, assembledDoc, cancellationToken);
                }

                if (!operationResult.IsSuccessfullyCompleted)
                {
                    //Това е случай който не би трябвало да се случи.
                    operationResult = await _signingProcessesService.SignerRejectRemoteSigningAsync(process.ProcessID.Value, Signer.SignerID.Value, operationResult.Errors.ElementAt(0).Message, cancellationToken);
                }

                if (!operationResult.IsSuccessfullyCompleted)
                    throw new NotSupportedException(operationResult.Errors.ElementAt(0).Message);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                throw;
            }
        }

        private async Task ProcessSignedPdfDocumentAsync(IEvrotrustClient evrotrustClient, SigningProcess process, Signer Signer, CancellationToken cancellationToken)
        {
            try
            {
                byte[] signDataBytes = await DownloadSignedDataFromEvrotrust(evrotrustClient, Signer, true);
                OperationResult operationResult = null;

                using (MemoryStream ms = new MemoryStream(signDataBytes))
                {
                    operationResult = await _signingProcessesService.SignerCompleteRemoteSigningAsync(process.ProcessID.Value, Signer.SignerID.Value, ms, cancellationToken);
                }

                if (!operationResult.IsSuccessfullyCompleted)
                {
                    //Това е случай който не би трябвало да се случи.
                    operationResult = await _signingProcessesService.SignerRejectRemoteSigningAsync(process.ProcessID.Value, Signer.SignerID.Value, operationResult.Errors.ElementAt(0).Message, cancellationToken);
                }

                if (!operationResult.IsSuccessfullyCompleted)
                    throw new NotSupportedException(operationResult.Errors.ElementAt(0).Message);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                throw;
            }
        }

        private async Task<byte[]> DownloadSignedDataFromEvrotrust(IEvrotrustClient evrotrustClient, Signer Signer, bool isPdf = false)
        {
            byte[] signedData = null;
            byte[] decryptedKey = null;
            byte[] iv = null;
            //SignerAdditionalData signerAdditionalData = EAUJsonSerializer.Deserialize<SignerAdditionalData>(Signer.SigningAdditionalData);
            DataDocumentDownload dataDocumentDownload = new DataDocumentDownload()
            {
                TransactionID = Signer.TransactionID,
                VendorNumber = _signModuleOptions.CurrentValue.SIGN_EVROTRUST_VENDOR_NUM
            };

            string authorization = EvrotrustApiHelper.GetAuthorizationHeader(_signModuleOptions.CurrentValue.SIGN_EVROTRUST_VENDOR_API_KEY, dataDocumentDownload.ToJson());

            using (FileResponse fileResponse = await evrotrustClient.DocumentDownloadAsync(authorization, dataDocumentDownload))
            using (MemoryStream msTmp = new MemoryStream())
            {
                fileResponse.Stream.CopyTo(msTmp);

                using (ZipFile zipFile = new ZipFile(msTmp))
                {
                    foreach (ZipEntry entry in zipFile)
                    {
                        if (entry.IsFile)
                        {
                            if (entry.Name.EndsWith("filename"))
                                continue;

                            using (Stream entryStream = zipFile.GetInputStream(entry))
                            using (MemoryStream ms = new MemoryStream())
                            {
                                entryStream.CopyTo(ms);

                                if (entry.Name.EndsWith("enc"))
                                {
                                    signedData = ms.ToArray();
                                }
                                else if (entry.Name.EndsWith("iv"))
                                {
                                    iv = ms.ToArray();
                                }
                                else if (entry.Name.EndsWith("key"))
                                {
                                    string key = Encoding.UTF8.GetString(ms.ToArray());
                                    byte[] keyBytes = Convert.FromBase64String(key);
                                    decryptedKey = EvrotrustCryptoUtils.Pkcs1Decrypt(keyBytes, Signer.SigningAdditionalData.RsaKeyForDecryption, 2048);
                                }
                            }
                        }
                    }
                }
            }


            byte[] decryptedSignData = null;

            using (MemoryStream msTmp = new MemoryStream())
            {
                int chunckSize = 7296; //Размер на буфера, какъвто Evrotrust са дали в примера си.

                if (signedData.Length >= chunckSize)
                {
                    int offset = 0;
                    byte[] tmpBuffer = null;
                    try
                    {
                        tmpBuffer = ArrayPool<byte>.Shared.Rent(chunckSize);

                        while (offset != signedData.Length)
                        {
                            string base64 = null;
                            int remainingBytes = signedData.Length - offset;

                            if (remainingBytes >= chunckSize)
                            {
                                Buffer.BlockCopy(signedData, offset, tmpBuffer, 0, chunckSize);
                                base64 = Encoding.UTF8.GetString(tmpBuffer, 0, chunckSize);
                                offset += chunckSize;
                            }
                            else
                            {
                                Buffer.BlockCopy(signedData, offset, tmpBuffer, 0, remainingBytes);
                                base64 = Encoding.UTF8.GetString(tmpBuffer, 0, remainingBytes);
                                offset += remainingBytes;
                            }

                            string decodedBase64 = EvrotrustCryptoUtils.RijndaelDecrypt(base64, decryptedKey, iv);
                            byte[] decryptedChunck = Convert.FromBase64String(decodedBase64);

                            msTmp.Write(decryptedChunck, 0, decryptedChunck.Length);
                        }
                    }
                    finally
                    {
                        if (tmpBuffer != null)
                            ArrayPool<byte>.Shared.Return(tmpBuffer);
                    }
                }
                else
                {
                    string base64 = Encoding.UTF8.GetString(signedData);
                    string decodedBase64 = EvrotrustCryptoUtils.RijndaelDecrypt(base64, decryptedKey, iv);
                    byte[] decryptedChunck = Convert.FromBase64String(decodedBase64);

                    msTmp.Write(decryptedChunck, 0, decryptedChunck.Length);
                }

                if (msTmp.Position != 0)
                {
                    msTmp.Position = 0;
                }

                using (ZipFile zipFile = new ZipFile(msTmp))
                {
                    foreach (ZipEntry entry in zipFile)
                    {
                        if (entry.IsFile)
                        {
                            if (!isPdf || (isPdf && entry.Name.EndsWith(".pdf")))
                            {
                                using (Stream entryStream = zipFile.GetInputStream(entry))
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    entryStream.CopyTo(ms);
                                    decryptedSignData = ms.ToArray();
                                }
                                break;
                            }
                        }
                    }
                }
            }

            return decryptedSignData;
        }

        #endregion
    }
}
