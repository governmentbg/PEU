using CNSys;
using CNSys.Data;
using CNSys.Xml;
using EAU.Common;
using EAU.Nomenclatures;
using EAU.Services.DocumentProcesses.Models;
using EAU.Services.DocumentProcesses.Repositories;
using EAU.Services.MessageHandlers;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Services.DocumentProcesses
{
    internal class DocumentProcessSigningCallbackService : IDocumentProcessSigningCallbackService
    {
        private readonly IDbContextOperationExecutor DBContextOperationExecutor;
        private readonly IDocumentProcessRepository DocumentProcessRepository;
        private readonly IDocumentProcessService DocumentProcessService;
        private readonly IDocumentProcessContentRepository DocumentProcessContentRepository;
        private readonly IActionDispatcher ActionDispatcher;
        private readonly IDocumentTypes DocumentTypes;
        private readonly IAttachedDocumentRepository AttachedDocumentRepository;

        public DocumentProcessSigningCallbackService(
            IDbContextOperationExecutor dBContextOperationExecutor
            , IDocumentProcessRepository documentProcessRepository
            , IDocumentProcessService documentProcessService
            , IDocumentProcessContentRepository documentProcessContentRepository
            , IActionDispatcher actionDispatcher
            , IDocumentTypes documentTypes
            , IAttachedDocumentRepository attachedDocumentRepository)
        {
            DBContextOperationExecutor = dBContextOperationExecutor;
            DocumentProcessRepository = documentProcessRepository;
            DocumentProcessService = documentProcessService;
            DocumentProcessContentRepository = documentProcessContentRepository;
            ActionDispatcher = actionDispatcher;
            DocumentTypes = documentTypes;
            AttachedDocumentRepository = attachedDocumentRepository;
        }

        #region IDocumentProcessSigningCallbackService

        public async Task SigningCompletedAsync(Guid signingGiud, Stream documentContent, Guid? userSessionID, Guid? loginSessionID, string ipAddress, int? userCIN, CancellationToken cancellationToken)
        {
            DocumentProcess process = null;

            await DBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                process = (await DocumentProcessService.SearchAsync(new DocumentProcessSearchCriteria()
                {
                    SigningGiud = signingGiud,
                    LoadOption = new DocumentProcessLoadOption()
                    {
                        LoadFormXmlContent = true
                    }
                }, cancellationToken)).SingleOrDefault();

                if (process == null || process.Status != ProcessStatuses.Signing)
                {
                    return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
                }

                var formXmlContent = process.ProcessContents.Single(pc => pc.Type == DocumentProcessContentTypes.FromXML);
                formXmlContent.TextContent = XmlHelpers.GetXmlString(documentContent);

                await DocumentProcessContentRepository.UpdateAsync(formXmlContent, token);

                process.Status = ProcessStatuses.Sending;
                process.AdditionalData["userSessionID"] = userSessionID?.ToString();
                process.AdditionalData["loginSessionID"] = loginSessionID?.ToString();
                process.AdditionalData["ipAddress"] = ipAddress;
                process.AdditionalData["userCIN"] = userCIN?.ToString();

                await DocumentProcessRepository.UpdateAsync(process, token);

                await ActionDispatcher.SendAsync(new DocumentSendMessage()
                {
                    DocumentProcessID = process.DocumentProcessID.Value
                });

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }

        public Task SigningRejectedAsync(Guid signingGiud, CancellationToken cancellationToken)
        {
            return DBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                var process = (await DocumentProcessService.SearchAsync(new DocumentProcessSearchCriteria()
                {
                    SigningGiud = signingGiud,
                    LoadOption = new DocumentProcessLoadOption()
                    {
                        LoadFormXmlContent = true
                    }
                }, cancellationToken)).SingleOrDefault();

                if (process == null || process.Status != ProcessStatuses.Signing)
                {
                    return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
                }

                if (process.Mode == DocumentProcessModes.WriteAndSign)
                {
                    await DocumentProcessContentRepository.DeleteAsync(process.ProcessContents.Single(c => c.Type == DocumentProcessContentTypes.FromXML));
                }

                process.Status = ProcessStatuses.InProcess;
                process.SigningGuid = null;

                await DocumentProcessRepository.UpdateAsync(process, cancellationToken);

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }

        public async Task SigningAttachedDocumentRejectedAsync(Guid signingGiud, CancellationToken cancellationToken)
        {
            var currentDoc = (await AttachedDocumentRepository.SearchAsync(new AttachedDocumentSearchCriteria()
            {
                SignGuid = signingGiud
            })).Single();

            currentDoc.SigningGuid = null;

            await AttachedDocumentRepository.UpdateAsync(currentDoc);
        }

        public Task SigningAttachedDocumentCompletedAsync(Guid signingGiud, Stream documentContent, Guid? userSessionID, Guid? loginSessionID, string ipAddress, int? userCIN, CancellationToken cancellationToken)
        {
            return DBContextOperationExecutor.ExecuteAsync(async (dbcontext) =>
            {
                var currentDoc = (await AttachedDocumentRepository.SearchAsync(new AttachedDocumentSearchCriteria()
                {
                    SignGuid = signingGiud
                })).Single();

                var currentDocDocType = DocumentTypes[currentDoc.DocumentTypeID.Value];
                string fileName = string.Format("{0}.pdf", currentDocDocType.Name);
                if (fileName.Length > 250)
                {
                    //Ограничаваме името на файла до 250 символа.
                    fileName = string.Format("{0}-{1}", currentDocDocType.Name.Substring(0, 124), currentDocDocType.Name.Substring(currentDocDocType.Name.Length - 125));
                }

                currentDoc.HtmlContent = null;
                currentDoc.SigningGuid = null;
                currentDoc.FileName = fileName;
                currentDoc.MimeType = "application/pdf";
                currentDoc.Content = new DocumentProcessContent()
                {
                    Content = documentContent,
                    Type = DocumentProcessContentTypes.AttachedDocument,
                    DocumentProcessID = currentDoc.DocumentProcessID
                };

                await DocumentProcessContentRepository.CreateAsync(currentDoc.Content);
                currentDoc.DocumentProcessContentID = currentDoc.Content.DocumentProcessContentID;

                await AttachedDocumentRepository.UpdateAsync(currentDoc);

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            });
        }

        #endregion
    }
}
