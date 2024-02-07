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
    /// Критерии за търсене за работа с шаблон за документ
    /// </summary>
    public class DocumentTemplateSearchCriteria : BasePagedSearchCriteria
    {
        public int? DocumentTypeID { get; set; }
    }

    /// <summary>
    /// Интерфeйс за поддържане и съхранение на обекти от тип DocumentTemplate.
    /// </summary>
    public interface IDocumentTemplateRepository :
        IRepositoryAsync<DocumentTemplate, int?, DocumentTemplateSearchCriteria>,
        ISearchCollectionInfo2<DocumentTemplate, DocumentTemplateSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IDocumentTemplateRepository за поддържане и съхранение на обекти от тип DocumentTemplate.
    /// </summary>
    internal class DocumentTemplateRepository : EAURepositoryBase<DocumentTemplate, int?, DocumentTemplateSearchCriteria, DocumentTemplateDataContext>, IDocumentTemplateRepository
    {
        #region Constructors

        public DocumentTemplateRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region CRUD

        protected override async Task CreateInternalAsync(DocumentTemplateDataContext context, DocumentTemplate item, CancellationToken cancellationToken)
        {
            item.DocTemplateID = await context.CreateAsync(
                item.DocumentTypeID,
                item.Content,
                item.IsSubmittedAccordingToTemplate,
                cancellationToken);
        }

        protected override async Task UpdateInternalAsync(DocumentTemplateDataContext context, DocumentTemplate item, CancellationToken cancellationToken)
        {
            await context.UpdateAsync(
                item.DocTemplateID,
                item.DocumentTypeID,
                item.Content,
                item.IsSubmittedAccordingToTemplate,                
                cancellationToken);
        }

        protected override async Task DeleteInternalAsync(DocumentTemplateDataContext context, int? docTemplateID, CancellationToken cancellationToken)
        {
            await DeleteAsync(new DocumentTemplate() { DocTemplateID = docTemplateID });
        }

        protected override async Task DeleteInternalAsync(DocumentTemplateDataContext context, DocumentTemplate item, CancellationToken cancellationToken)
        {
            await context.DeleteAsync(item.DocTemplateID, cancellationToken);
        }

        #endregion

        #region IDocumentTemplateRepository

        public Task<CollectionInfo<DocumentTemplate>> SearchInfoAsync(DocumentTemplateSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<DocumentTemplate>> SearchInfoAsync(PagedDataState state, DocumentTemplateSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    state.StartIndex,
                    state.PageSize,
                    searchCriteria.DocumentTypeID,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<DocumentTemplate>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }

        #endregion
    }
}
