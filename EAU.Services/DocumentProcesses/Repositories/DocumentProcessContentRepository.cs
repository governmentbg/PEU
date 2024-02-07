using CNSys.Data;
using EAU.Common;
using EAU.Data;
using EAU.Services.DocumentProcesses.Models;
using Microsoft.Extensions.Options;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Services.DocumentProcesses.Repositories
{
    /// <summary>
    /// Интерфейс за поддържане и съхранение на обекти от тип DocumentProcessContent.
    /// </summary>
    public interface IDocumentProcessContentRepository : IRepositoryAsync<DocumentProcessContent, long?, DocumentProcessContentSearchCriteria>
    {
        Task UploadDocumentProcessContentAsync(Stream content, long documentProcessContentID, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Реализация на интерфейс IDocumentProcessContentRepository за поддържане и съхранение на обекти от тип DocumentProcessContent.
    /// </summary>
    internal class DocumentProcessContentRepository : EAURepositoryBase<DocumentProcessContent, long?, DocumentProcessContentSearchCriteria, DocumentProcessDbContext>, IDocumentProcessContentRepository
    {
        private readonly GlobalOptions _globalOptions;

        #region Constructors

        public DocumentProcessContentRepository(IOptionsMonitor<GlobalOptions> options, IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
            _globalOptions = options.CurrentValue;
        }

        #endregion
        
        public async Task UploadDocumentProcessContentAsync(Stream content, long documentProcessContentID, CancellationToken cancellationToken)
        {
            await ClearContent(documentProcessContentID, cancellationToken);
            await UploadDocumentProcessContentInternalAsync(content, documentProcessContentID, cancellationToken);
        }

        #region IRepositoryAsync

        protected override async Task CreateInternalAsync(DocumentProcessDbContext context, DocumentProcessContent item, CancellationToken cancellationToken)
        {
            long? ID = await context.DocumentProcessContentCreateAsync(item.DocumentProcessID,
                               (short?)item.Type,
                               item.TextContent,
                               cancellationToken);

            item.DocumentProcessContentID = ID;

            if(item.Content != null)
            {
                await UploadDocumentProcessContentInternalAsync(item.Content, item.DocumentProcessContentID.Value, cancellationToken);
            }
        }

        protected override async Task<DocumentProcessContent> ReadInternalAsync(DocumentProcessDbContext context, long? key, CancellationToken cancellationToken)
        {
            if (key == null)
                throw new ArgumentNullException();

            DocumentProcessContent documentProcessContent = (await SearchInternalAsync(context, new DocumentProcessContentSearchCriteria() 
            { 
                DocumentProcessIDs = new List<long>() { key.Value },
                LoadOption = new DocumentProcessContentLoadOption() { LoadContent = true }
            }, cancellationToken)).SingleOrDefault();

            return documentProcessContent;
        }

        protected override Task UpdateInternalAsync(DocumentProcessDbContext context, DocumentProcessContent item, CancellationToken cancellationToken)
        {
            if (item.Content != null)
                throw new InvalidOperationException("Only text content can be updated! To change byte array content use UploadDocumentProcessContentAsync");

            return context.DocumentProcessContentUpdateAsync(item.DocumentProcessContentID,
                               item.TextContent,
                               cancellationToken);
        }

        protected override Task DeleteInternalAsync(DocumentProcessDbContext context, long? key, CancellationToken cancellationToken)
        {
            if (key == null)
                throw new ArgumentNullException();

            return context.DocumentProcessContentDeleteAsync(key, cancellationToken);
        }

        protected override Task DeleteInternalAsync(DocumentProcessDbContext context, DocumentProcessContent item, CancellationToken cancellationToken)
        {
            return DeleteInternalAsync(context, item.DocumentProcessContentID, cancellationToken);
        }

        protected override Task<IEnumerable<DocumentProcessContent>> SearchInternalAsync(DocumentProcessDbContext context, DocumentProcessContentSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        protected override async Task<IEnumerable<DocumentProcessContent>> SearchInternalAsync(DocumentProcessDbContext context, PagedDataState state, DocumentProcessContentSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var res = await context.DocumentProcessContentSearchAsync(searchCriteria.DocumentProcessIDs,
                                                  (short?)searchCriteria.Type,
                                                  state.StartIndex,
                                                  state.PageSize,
                                                  (state.StartIndex == 1),
                                                  cancellationToken);

            state.Count = res.count ?? state.Count;
            List<DocumentProcessContent> docContents = res.docContents.ToList();

            if(docContents != null 
                && docContents.Count > 0 
                && searchCriteria.LoadOption != null
                && searchCriteria.LoadOption.LoadContent)
            {
                //Зарежда съдържанието
                foreach (DocumentProcessContent item in docContents)
                {
                    item.Content = DocumentProcessDbContext.CreateDocumentProcessContentStream(item.DocumentProcessContentID.Value, DbContextProvider);
                }
            }

            return docContents;
        }

        #endregion

        private Task ClearContent(long documentProcessContentID, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (ctx, token) =>
            {
                await ctx.DocumentProcessContentUploadAsync(documentProcessContentID, null, 0, token);
            }, cancellationToken);
        }

        private Task UploadDocumentProcessContentInternalAsync(Stream content, long documentProcessContentID, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (ctx, token) =>
            {
                if (content.Position != 0)
                    throw new ArgumentException("The content stream must be in 0 position in order to be uploaded.");

                int chunkSize = _globalOptions.GL_COPY_BUFFER_SIZE;
                byte[] buffer = null;

                try
                {
                    buffer = ArrayPool<byte>.Shared.Rent(chunkSize);
                    int bytesReaded = 0;

                    while ((bytesReaded = content.Read(buffer, 0, chunkSize)) > 0)
                    {
                        await ctx.DocumentProcessContentUploadAsync(documentProcessContentID, buffer, bytesReaded, token);
                    }
                }
                finally
                {
                    if (buffer != null)
                        ArrayPool<byte>.Shared.Return(buffer);
                }
            }, cancellationToken);
        }
    }
}
