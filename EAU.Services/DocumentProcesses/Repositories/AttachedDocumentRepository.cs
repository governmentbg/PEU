using CNSys.Data;
using EAU.Data;
using EAU.Services.DocumentProcesses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Services.DocumentProcesses.Repositories
{
    /// <summary>
    /// Интерфейс за поддържане и съхранение на обекти от тип AttachedDocument.
    /// </summary>
    public interface IAttachedDocumentRepository : IRepositoryAsync<AttachedDocument, long?, AttachedDocumentSearchCriteria>
    { }

    /// <summary>
    /// Реализация на интерфейс IAttachedDocumentRepository за поддържане и съхранение на обекти от тип AttachedDocument.
    /// </summary>
    internal class AttachedDocumentRepository : EAURepositoryBase<AttachedDocument, long?, AttachedDocumentSearchCriteria, DocumentProcessDbContext>, IAttachedDocumentRepository
    {
        #region Constructors

        public AttachedDocumentRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region IAttachedDocumentRepository

        protected override async Task CreateInternalAsync(DocumentProcessDbContext context, AttachedDocument item, CancellationToken cancellationToken)
        {
            long? ID = await context.AttachedDocumentCreateAsync(item.AttachedDocumentGuid,
                               item.DocumentProcessID,
                               item.DocumentProcessContentID,
                               item.DocumentTypeID,
                               item.Description,
                               item.MimeType,
                               item.FileName,
                               item.HtmlContent,
                               item.SigningGuid,
                               cancellationToken);

            item.AttachedDocumentID = ID;
        }

        protected override async Task<AttachedDocument> ReadInternalAsync(DocumentProcessDbContext context, long? key, CancellationToken cancellationToken)
        {
            AttachedDocument attachedDocument = (await SearchInternalAsync(context, new AttachedDocumentSearchCriteria() { AttachedDocumentID = key }, cancellationToken)).SingleOrDefault();

            return attachedDocument;
        }

        protected override Task UpdateInternalAsync(DocumentProcessDbContext context, AttachedDocument item, CancellationToken cancellationToken)
        {
            return context.AttachedDocumentUpdateAsync(item.AttachedDocumentID, item.Description, item.MimeType, item.FileName, item.DocumentProcessContentID, item.HtmlContent, item.SigningGuid, cancellationToken);
        }

        protected override Task DeleteInternalAsync(DocumentProcessDbContext context, AttachedDocument item, CancellationToken cancellationToken)
        {
            return DeleteInternalAsync(context, item.AttachedDocumentID, cancellationToken);
        }

        protected override Task DeleteInternalAsync(DocumentProcessDbContext context, long? key, CancellationToken cancellationToken)
        {
            if (key == null)
                throw new ArgumentNullException();

            return context.AttachedDocumentDeleteAsync(key, cancellationToken);
        }

        protected override Task<IEnumerable<AttachedDocument>> SearchInternalAsync(DocumentProcessDbContext context, AttachedDocumentSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        protected override async Task<IEnumerable<AttachedDocument>> SearchInternalAsync(DocumentProcessDbContext context, PagedDataState state, AttachedDocumentSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            using (var reader = await context.AttachedDocumentsSearchAsync(searchCriteria.AttachedDocumentID,
                                                   searchCriteria.DocumentProcessID,
                                                   searchCriteria.LoadOption?.LoadContent,
                                                   searchCriteria.SignGuid,
                                                   state.StartIndex,
                                                   state.PageSize,
                                                   (state.StartIndex == 1),
                                                   cancellationToken))
            {
                List<AttachedDocument> attachedDocuments = (await reader.ReadAsync<AttachedDocument>())?.ToList();

                if (searchCriteria.LoadOption != null && searchCriteria.LoadOption.LoadContent)
                {
                    //Зарежда съдържанието
                    List<DocumentProcessContent> docProcessContents = (await reader.ReadAsync<DocumentProcessContent>())?.ToList();

                    if (docProcessContents != null && docProcessContents.Any())
                    {
                        foreach (AttachedDocument ad in attachedDocuments)
                        {
                            var currAttachedDocContent = docProcessContents.SingleOrDefault(c => c.DocumentProcessContentID == ad.DocumentProcessContentID);

                            if (currAttachedDocContent != null)
                            {
                                currAttachedDocContent.Content = DocumentProcessDbContext.CreateDocumentProcessContentStream(currAttachedDocContent.DocumentProcessContentID.Value, DbContextProvider);
                                ad.Content = currAttachedDocContent;
                            }
                        }
                    }
                }

                state.Count = reader.ReadOutParameter<int?>("@p_count") ?? state.Count;

                return attachedDocuments;
            }
        }

        #endregion
    }
}
