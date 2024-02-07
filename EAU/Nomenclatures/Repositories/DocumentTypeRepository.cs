using CNSys.Data;
using EAU.Common.Models;
using EAU.Data;
using EAU.Nomenclatures.Models;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Repositories
{
    /// <summary>
    /// Критерии за търсене за работа с вид документ
    /// </summary>
    public class DocumentTypeSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Идентификатори на записи на вид документ.
        /// </summary>
        public int[] IDs { get; set; }
    }

    /// <summary>
    /// Интерфeйс за поддържане и съхранение на обекти от тип DocumentType.
    /// </summary>
    public interface IDocumentTypeRepository :        
        ISearchCollectionInfo2<DocumentType, DocumentTypeSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IDocumentTypeRepository за поддържане и съхранение на обекти от тип DocumentType.
    /// </summary>
    internal class DocumentTypeRepository : EAURepositoryBase<DocumentType, int?, DocumentTypeSearchCriteria, DocumentTypeDataContext>, IDocumentTypeRepository
    {
        #region Constructors

        public DocumentTypeRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region IDocumentTypeRepository

        public Task<CollectionInfo<DocumentType>> SearchInfoAsync(DocumentTypeSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<DocumentType>> SearchInfoAsync(PagedDataState state, DocumentTypeSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    EnumerableExtensions.ToStringNumberCollection(searchCriteria.IDs),
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<DocumentType>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }

        #endregion
    }
}
