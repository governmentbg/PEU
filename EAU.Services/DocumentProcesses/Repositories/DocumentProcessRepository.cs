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
    /// Интерфейс за поддържане и съхранение на обекти от тип DocumentProcess.
    /// </summary>
    public interface IDocumentProcessRepository : IRepositoryAsync<DocumentProcess, long?, DocumentProcessSearchCriteria>
    { }


    /// <summary>
    /// Реализация на интерфейс IDocumentProcessRepository за поддържане и съхранение на обекти от тип DocumentProcess.
    /// </summary>
    internal class DocumentProcessRepository : EAURepositoryBase<DocumentProcess, long?, DocumentProcessSearchCriteria, DocumentProcessDbContext>, IDocumentProcessRepository
    {
        #region Constructors

        public DocumentProcessRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region IDocumentProcessRepository

        protected override async Task CreateInternalAsync(DocumentProcessDbContext context, DocumentProcess item, CancellationToken cancellationToken)
        {
            long? ID = await context.DocumentProcessCreateAsync(item.ApplicantID,
                                item.DocumentTypeID,
                                item.ServiceID,
                                (short?)item.Status,
                                (short?)item.Mode,
                                (short?)item.Type,
                                item.AdditionalData,
                                item.SigningGuid,
                                item.ErrorMessage,
                                item.CaseFileURI,
                                item.NotAcknowledgedMessageURI,
                                item.RequestID,
                                cancellationToken);

            item.DocumentProcessID = ID;
        }

        protected override async Task<DocumentProcess> ReadInternalAsync(DocumentProcessDbContext context, long? key, CancellationToken cancellationToken)
        {
            DocumentProcess documentProcess = (await SearchInternalAsync(context, new DocumentProcessSearchCriteria() { DocumentProcessID = key }, cancellationToken)).SingleOrDefault();

            return documentProcess;
        }

        protected override Task UpdateInternalAsync(DocumentProcessDbContext context, DocumentProcess item, CancellationToken cancellationToken)
        {
            return context.DocumentProcessUpdateAsync(item.DocumentProcessID,
                                (short?)item.Status,
                                item.AdditionalData,
                                item.SigningGuid,
                                item.ErrorMessage,
                                item.CaseFileURI,
                                item.NotAcknowledgedMessageURI,
                                item.RequestID,
                                cancellationToken);
        }

        protected override Task DeleteInternalAsync(DocumentProcessDbContext context, DocumentProcess item, CancellationToken cancellationToken)
        {
            return DeleteInternalAsync(context, item.DocumentProcessID, cancellationToken);
        }

        protected override Task DeleteInternalAsync(DocumentProcessDbContext context, long? key, CancellationToken cancellationToken)
        {
            if (key == null)
                throw new ArgumentNullException();

            return context.DocumentProcessDeleteAsync(key, cancellationToken);
        }

        protected override async Task<IEnumerable<DocumentProcess>> SearchInternalAsync(DocumentProcessDbContext context, PagedDataState state, DocumentProcessSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            using (var reader = await context.DocumentProcessesSearchAsync(searchCriteria.DocumentProcessID,
                                                    searchCriteria.ApplicantCIN,
                                                    searchCriteria.ServiceID,
                                                    searchCriteria.RequestID,
                                                    searchCriteria.SigningGiud,
                                                    (short?)searchCriteria.DocumentProcessType,
                                                    searchCriteria.LoadOption?.LoadWithLock,
                                                    searchCriteria.LoadOption?.LoadAllContents,
                                                    searchCriteria.LoadOption?.LoadAttachedDocument,
                                                    searchCriteria.LoadOption?.LoadFormJsonContent,
                                                    searchCriteria.LoadOption?.LoadFormXmlContent,
                                                    state.StartIndex,
                                                    state.PageSize,
                                                    (state.StartIndex == 1),
                                                    cancellationToken))
            {
                List<DocumentProcess> docProcesses = (await reader.ReadAsync<DocumentProcess>())?.ToList();

                if (searchCriteria.LoadOption != null)
                {
                    if (searchCriteria.LoadOption.LoadAttachedDocument.GetValueOrDefault())
                    {
                        //Load Attached Docs content
                        List<AttachedDocument> attachedDocuments = (await reader.ReadAsync<AttachedDocument>())?.ToList();

                        if (attachedDocuments != null && attachedDocuments.Any())
                        {
                            foreach (DocumentProcess dp in docProcesses)
                            {
                                dp.AttachedDocuments = (attachedDocuments.Where(ad => ad.DocumentProcessID == dp.DocumentProcessID))?.ToList();
                            }
                        }
                    }

                    if (searchCriteria.LoadOption.LoadAllContents.GetValueOrDefault()
                            || searchCriteria.LoadOption.LoadFormJsonContent.GetValueOrDefault()
                            || searchCriteria.LoadOption.LoadFormXmlContent.GetValueOrDefault())
                    {
                        //Load Document Process Contents
                        List<DocumentProcessContent> documentProcessContents = (await reader.ReadAsync<DocumentProcessContent>())?.ToList();

                        if (documentProcessContents != null && documentProcessContents.Any())
                        {
                            foreach (DocumentProcess dp in docProcesses)
                            {
                                List<DocumentProcessContent> currProcessContents = documentProcessContents.Where(el => el.DocumentProcessID == dp.DocumentProcessID)?.ToList();

                                if (currProcessContents != null && currProcessContents.Any())
                                {
                                    if (searchCriteria.LoadOption.LoadAllContents.GetValueOrDefault() || searchCriteria.LoadOption.LoadFormJsonContent.GetValueOrDefault())
                                    {
                                        DocumentProcessContent jsonProcessContent = currProcessContents.SingleOrDefault(c => c.Type == DocumentProcessContentTypes.FormJSON);

                                        if (jsonProcessContent != null)
                                        {
                                            if (jsonProcessContent.TextContent == null)
                                                jsonProcessContent.Content = DocumentProcessDbContext.CreateDocumentProcessContentStream(jsonProcessContent.DocumentProcessContentID.Value, DbContextProvider);

                                            if (dp.ProcessContents == null)
                                                dp.ProcessContents = new List<DocumentProcessContent>();

                                            dp.ProcessContents.Add(jsonProcessContent);
                                        }
                                    }

                                    if (searchCriteria.LoadOption.LoadAllContents.GetValueOrDefault() || searchCriteria.LoadOption.LoadFormXmlContent.GetValueOrDefault())
                                    {
                                        DocumentProcessContent xmlProcessContent = currProcessContents.SingleOrDefault(c => c.Type == DocumentProcessContentTypes.FromXML);

                                        if (xmlProcessContent != null)
                                        {
                                            if (xmlProcessContent.TextContent == null)
                                                xmlProcessContent.Content = DocumentProcessDbContext.CreateDocumentProcessContentStream(xmlProcessContent.DocumentProcessContentID.Value, DbContextProvider);

                                            if (dp.ProcessContents == null)
                                                dp.ProcessContents = new List<DocumentProcessContent>();

                                            dp.ProcessContents.Add(xmlProcessContent);
                                        }
                                    }

                                    if (searchCriteria.LoadOption.LoadAllContents.GetValueOrDefault() && searchCriteria.LoadOption.LoadAttachedDocument.GetValueOrDefault())
                                    {
                                        if (dp.AttachedDocuments != null)
                                        {
                                            foreach (AttachedDocument ad in dp.AttachedDocuments)
                                            {
                                                DocumentProcessContent attachedDocContent = currProcessContents.SingleOrDefault(c => c.DocumentProcessContentID == ad.DocumentProcessContentID);

                                                if (attachedDocContent != null)
                                                {
                                                    attachedDocContent.Content = DocumentProcessDbContext.CreateDocumentProcessContentStream(attachedDocContent.DocumentProcessContentID.Value, DbContextProvider);

                                                    ad.Content = attachedDocContent;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                state.Count = reader.ReadOutParameter<int?>("@p_count") ?? state.Count;

                return docProcesses;
            }
        }

        protected override Task<IEnumerable<DocumentProcess>> SearchInternalAsync(DocumentProcessDbContext context, DocumentProcessSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        #endregion
    }
}
