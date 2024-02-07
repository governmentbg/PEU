using CNSys;
using CNSys.Data;
using EAU.Nomenclatures.Models;
using EAU.Nomenclatures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Services
{
    public interface IDeclarationService
    {
        Task<OperationResult> CreateAsync(Declaration obj, CancellationToken cancellationToken);
        Task<OperationResult> UpdateAsync(Declaration obj, CancellationToken cancellationToken);
        Task<OperationResult> DeleteAsync(int declarationID, CancellationToken cancellationToken);
    }

    internal class DeclarationService : IDeclarationService
    {
        private readonly IDeclarationRepository _declarationRepository;
        private readonly IServiceDeclarationRepository _serviceDeclarationRepository;

        public DeclarationService(
            IDeclarationRepository declarationRepository,
            IServiceDeclarationRepository serviceDeclarationRepository)
        {
            _declarationRepository = declarationRepository;
            _serviceDeclarationRepository = serviceDeclarationRepository;
        }

        public async Task<OperationResult> CreateAsync(Declaration obj, CancellationToken cancellationToken)
        {
            var result = Validate(obj, true);

            if (result.IsSuccessfullyCompleted)
            {
                await _declarationRepository.CreateAsync(obj, cancellationToken);
            }

            return result;
        }

        public async Task<OperationResult> UpdateAsync(Declaration obj, CancellationToken cancellationToken)
        {
            var result = Validate(obj, false);

            if (result.IsSuccessfullyCompleted)
            {
                await _declarationRepository.UpdateAsync(obj, cancellationToken);
            }

            return result;
        }

        public async Task<OperationResult> DeleteAsync(int declarationID, CancellationToken cancellationToken)
        {
            var serviceDeclarations = await _serviceDeclarationRepository.SearchAsync(
                 new ServiceDeclarationSearchCriteria()
                 {
                     DeclarationID = declarationID
                 }, cancellationToken);

            var result = new OperationResult(OperationResultTypes.SuccessfullyCompleted);

            if (serviceDeclarations.Any())
            {
                //Декларативно обстоятелство/ политика участва в заявление по услуга
                result.AddError(new TextError("GL_SERVICE_DECLARATION", "GL_SERVICE_DECLARATION"), true);
            }

            if (result.IsSuccessfullyCompleted)
                {
                    await _declarationRepository.DeleteAsync(
                        new Declaration()
                        {
                            DeclarationID = declarationID
                        }, cancellationToken);
                }

            return result;
        }

        private static OperationResult Validate(Declaration obj, bool validateCode)
        {
            var result = new OperationResult(OperationResultTypes.SuccessfullyCompleted);

            if (validateCode && String.IsNullOrWhiteSpace(obj.Code))
                //"Въвеждането на код е задължително"
                result.AddError(new TextError("DECLARATION_CODE", "DECLARATION_CODE"), true);

            if (String.IsNullOrWhiteSpace(obj.Description))
                //"Въвеждането на описание е задължително"
                result.AddError(new TextError("DECLARATION_DESCRIPTION", "DECLARATION_DESCRIPTION"), true);

            if (String.IsNullOrWhiteSpace(obj.Content))
                //"Въвеждането на съдържание е задължително"
                result.AddError(new TextError("DECLARATION_CONTENT", "DECLARATION_CONTENT"), true);

            return result;
        }
    }
}
