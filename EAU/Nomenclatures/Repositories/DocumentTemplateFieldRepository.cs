using CNSys.Data;
using EAU.Common.Models;
using EAU.Data;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Repositories
{

    /// <summary>
    /// Критерии за търсене за работа с полетата в шаблон за документ
    /// </summary>
    public class DocumentTemplateFieldSearchCriteria : BasePagedSearchCriteria
    {
    }

    /// <summary>
    /// Интерфeйс за поддържане и съхранение на обекти от тип DocumentTemplateField.
    /// </summary>
    public interface IDocumentTemplateFieldRepository :
        ISearchCollectionInfo2<DocumentTemplateField, DocumentTemplateFieldSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IDocumentTemplateFieldRepository за поддържане и съхранение на обекти от тип DocumentTemplateField.
    /// </summary>
    internal class DocumentTemplateFieldRepository : EAURepositoryBase<DocumentTemplateField, int?, DocumentTemplateFieldSearchCriteria, DocumentTemplateFieldDataContext>, IDocumentTemplateFieldRepository
    {
        #region Constructors

        public DocumentTemplateFieldRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion


        #region IDocumentTemplateRepository

        public Task<CollectionInfo<DocumentTemplateField>> SearchInfoAsync(DocumentTemplateFieldSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<DocumentTemplateField>> SearchInfoAsync(PagedDataState state, DocumentTemplateFieldSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<DocumentTemplateField>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }

        #endregion
    }
}
