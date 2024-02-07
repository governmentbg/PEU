using CNSys;
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
    public interface IDocumentTemplateService
    {
        /// <summary>
        /// Операция за създаване на номенклатура на шаблон за документ.
        /// </summary>
        /// <param name="obj">Номенклатура на шаблон за документ</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        /// <returns></returns>
        Task<OperationResult> CreateAsync(DocumentTemplate obj, CancellationToken cancellationToken);
    }

    internal class DocumentTemplateService : IDocumentTemplateService
    {
        private readonly IDocumentTemplateRepository _documentTemplateRepository;
        private readonly IDocumentTemplates _documentTemplates;

        public DocumentTemplateService(
            IDocumentTemplateRepository documentTemplateRepository,
            IDocumentTemplates documentTemplates)
        {
            _documentTemplateRepository = documentTemplateRepository;
            _documentTemplates = documentTemplates;
        }

        public async Task<OperationResult> CreateAsync(DocumentTemplate obj, CancellationToken cancellationToken)
        {
            var result = new OperationResult(OperationResultTypes.SuccessfullyCompleted);

            await _documentTemplates.EnsureLoadedAsync(CancellationToken.None);
            if (_documentTemplates.Search(obj.DocumentTypeID.Value) != null)
            {
                result.AddError(new TextError("GL_DOC_TYPE_ID_E", "GL_DOC_TYPE_ID_E"), true);
            }

            if (result.IsSuccessfullyCompleted)
            {
                await _documentTemplateRepository.CreateAsync(obj, cancellationToken);
            }

            return result;
        }
    }
}
