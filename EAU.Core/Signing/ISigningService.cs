using CNSys;
using EAU.Signing.Models;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Signing
{
    /// <summary>
    /// Интерфейс за работа с процеса по подписване.
    /// </summary>
    public interface ISigningService
    {
        /// <summary>
        /// Създава процес по подписване.
        /// </summary>
        /// <param name="request">Заявка за процес по подписване.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Идентификатор на създадения процес.</returns>
        Task<OperationResult<Guid>> CreateSigningProcessAsync(SigningRequest request, CancellationToken cancellationToken);
          
        /// <summary>
        /// Валидира положените подписи в документ.
        /// </summary>
        /// <param name="signedDocumentContent">Подписаният документ.</param>
        /// <param name="fileName">Име на файла.</param>
        /// <returns></returns>
        Task<SignatursVerificationResponse> ValidateDocumentSignatures(Stream signedDocumentContent, string fileName);

        /// <summary>
        /// Изтрива процеси по подписване.
        /// </summary>
        /// <param name="guids">Списък с идентификатори на процеси по подписване.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task</returns>
        Task<OperationResult> DeleteSigningProcessesAsync(Guid[] guids, CancellationToken cancellationToken);
    }
}
