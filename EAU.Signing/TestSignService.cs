using CNSys;
using EAU.Signing.Configuration;
using EAU.Signing.Models;
using EAU.Signing.Models.SearchCriteria;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Signing
{
    public interface ITestSignService
    {
        Task<OperationResult> TestSignAsync(Guid processID, CancellationToken cancellationToken);
    }

    internal class TestSignService : ITestSignService
    {
        #region Members

        private readonly ISigningProcessesService _signingProcessesService;
        private readonly IDocumentSigningtUtilityService _docSigningUtilityService;
        private readonly IOptionsMonitor<SignModuleGlobalOptions> _signModuleOptions;
        private readonly ILogger _logger;

        #endregion

        #region Constructor

        public TestSignService(
            ISigningProcessesService signingProcessesService
            , IDocumentSigningtUtilityService docSigningUtilityService
            , IOptionsMonitor<SignModuleGlobalOptions> signModuleOptions
            , ILogger<TestSignService> logger)
        {
            _signingProcessesService = signingProcessesService;
            _docSigningUtilityService = docSigningUtilityService;
            _signModuleOptions = signModuleOptions;
            _logger = logger;
        }

        #endregion

        public async Task<OperationResult> TestSignAsync(Guid processID, CancellationToken cancellationToken)
        {
            if (_signModuleOptions.CurrentValue.SIGN_ALLOW_TEST_SIGN == 0)
                throw new NotSupportedException("Method not allowed.");

            SigningProcess process = null;

            try
            {
                process = (await _signingProcessesService.SearchAsync(new SigningProcessesSearchCriteria()
                {
                    ProcessID = processID,
                    LoadSigners = true,
                    LoadContent = true
                }, cancellationToken)).SingleOrDefault();

                if (process == null || process.Content == null || process.Signers == null)
                {
                    return new OperationResult("GL_NO_DATA_FOUND_L", "GL_NO_DATA_FOUND_L");
                }

                if (process.Signers.Count > 1)
                {
                    return new OperationResult("Тестовото подписване работи само с един подписващ", "Тестовото подписване работи само с един подписващ");
                }

                using (MemoryStream bufferingContent = new MemoryStream())
                {
                    using (process.Content)
                    {
                        process.Content.CopyTo(bufferingContent);
                    }

                    bufferingContent.Position = 0;

                    using (Stream signedContent = await _docSigningUtilityService.SignAsync(
                            process.ContentType
                            , process.FileName
                            , bufferingContent
                            , process.DigestMethod.ToString()
                            , process.Format.Value
                            , process.Type.Value.ToString()
                            , process.Level.Value
                            , _docSigningUtilityService.GeServerCert()
                            , process.AdditionalData.SignatureXpath
                            , process.AdditionalData.SignatureXPathNamespaces
                            , process.Format == SigningFormats.PAdES))
                    {
                        OperationResult signResult = await _signingProcessesService.SignerSignedLocalAsync(processID, process.Signers[0].SignerID.Value, signedContent, cancellationToken);

                        return signResult;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                return new OperationResult("GL_SIGN_FAIL_E", "GL_SIGN_FAIL_E");
            }
            finally
            {
                if (process != null && process.Content != null)
                {
                    process.Content.Dispose();
                }
            }
        }
    }
}
