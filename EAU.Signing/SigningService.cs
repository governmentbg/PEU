using CNSys;
using EAU.Signing.BSecureDSSL;
using EAU.Signing.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Signing
{
    internal class SigningService : ISigningService
    {
        #region Members

        private readonly ISigningProcessesService _signingProcessesService;
        private readonly IDocumentSigningtUtilityService _documentSigningtUtilityService;

        #endregion

        public SigningService(
            ISigningProcessesService signingProcessesService
            , IDocumentSigningtUtilityService documentSigningtUtilityService
            , ILogger<SigningService> logger)
        {
            _signingProcessesService = signingProcessesService;
            _documentSigningtUtilityService = documentSigningtUtilityService;
        }

        public Task<OperationResult<Guid>> CreateSigningProcessAsync(SigningRequest request, CancellationToken cancellationToken)
        {
            return _signingProcessesService.CreateSigningProcessAsync(request, cancellationToken);
        }

        public async Task<SignatursVerificationResponse> ValidateDocumentSignatures(Stream signedDocumentContent, string fileName)
        {
            ValidateDocumentResponseDto valRes = await _documentSigningtUtilityService.SignaturesVerificationAsync(signedDocumentContent, fileName);

            JObject jObject = JObject.FromObject(valRes);
            SignatursVerificationResponse result = jObject.ToObject<SignatursVerificationResponse>();

            return result;
        }

        public Task<OperationResult> DeleteSigningProcessesAsync(Guid[] guids, CancellationToken cancellationToken)
        {
            return _signingProcessesService.DeleteSigningProcessesAsync(guids, cancellationToken);
        }
    }
}
