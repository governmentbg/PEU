using CNSys;
using CNSys.Data;
using CNSys.Xml;
using EAU.Common;
using EAU.Documents;
using EAU.DocumentTemplates;
using EAU.DocumentTemplates.Models;
using EAU.Nomenclatures;
using EAU.Security;
using EAU.Services.DocumentProcesses.Models;
using EAU.Services.DocumentProcesses.Repositories;
using EAU.Signing;
using EAU.Signing.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Services.DocumentProcesses
{
    internal class DocumentProcessSigningService : IDocumentProcessSigningService
    {
        private readonly IDocumentProcessRepository DocumentProcessRepository;
        private readonly IDbContextOperationExecutor DBContextOperationExecutor;
        private readonly IDocumentProcessFormService DocumentProcessFormService;
        private readonly IDocumentProcessService DocumentProcessService;
        private readonly IAttachedDocumentRepository AttachedDocumentRepository;
        private readonly IDocumentFormServiceProvider DocumentServicesProvider;
        private readonly IEAUUserAccessor UserAccessor;
        private readonly IDocumentTypes DocumentTypes;
        private readonly ISigningService SigningService;
        private readonly GlobalOptions Options;
        private readonly IDocumentTemplatesServiceClient DocumentTemplatesServiceClient;

        public DocumentProcessSigningService(
            IDocumentProcessRepository docProcessRepository,
            IEAUUserAccessor userAccessor,
            IDocumentProcessFormService documentProcessFormService,
            IDbContextOperationExecutor dbContextOperationExecutor,
            IDocumentProcessService documentProcessService,
            IAttachedDocumentRepository attachedDocumentRepository,
            IDocumentTypes documentTypes,
            IDocumentFormServiceProvider documentServicesProvider,
            ISigningService signingService,
            IOptionsMonitor<GlobalOptions> globalOptions,
            IDocumentTemplatesServiceClient documentTemplatesServiceClient)
        {
            DocumentProcessRepository = docProcessRepository;
            UserAccessor = userAccessor;
            DBContextOperationExecutor = dbContextOperationExecutor;
            DocumentProcessFormService = documentProcessFormService;
            DocumentProcessService = documentProcessService;
            AttachedDocumentRepository = attachedDocumentRepository;
            DocumentTypes = documentTypes;
            DocumentServicesProvider = documentServicesProvider;
            SigningService = signingService;
            Options = globalOptions.CurrentValue;
            DocumentTemplatesServiceClient = documentTemplatesServiceClient;
        }

        public async Task<OperationResult<Guid>> StartSigningAsync(long processID, CancellationToken cancellationToken)
        {
            DocumentProcess process = null;
            Stream xmlFormStream = new MemoryStream();

            process = (await DocumentProcessService.SearchAsync(new DocumentProcessSearchCriteria()
            {
                DocumentProcessID = processID,
                ApplicantCIN = UserAccessor.User.CIN,
                LoadOption = new DocumentProcessLoadOption()
                {
                    LoadAttachedDocument = true,
                    LoadAllContents = true
                }
            }, cancellationToken)).SingleOrDefault();

            EnsureExistingProcessAndCheckAccess(processID, process);

            try
            {
                if (process.Status == ProcessStatuses.Signing)
                {
                    return new OperationResult<Guid>(OperationResultTypes.SuccessfullyCompleted)
                    {
                        Result = process.SigningGuid.Value
                    };
                }

                if (process.Status != ProcessStatuses.InProcess)
                {
                    throw new InvalidOperationException(string.Format("Can't start sing process {0} in status {1}.", process.DocumentProcessID, process.Status.ToString()));
                }

                if (process.Mode != DocumentProcessModes.Sign && process.Mode != DocumentProcessModes.WriteAndSign)
                {
                    throw new InvalidOperationException(string.Format("Can't start sing process {0} for process in mode {1}.", process.DocumentProcessID, process.Mode.ToString()));
                }

                if (process.Mode == DocumentProcessModes.WriteAndSign)
                {
                    //Метода GenerateFormXmlContentAsync Прави въшни извиквания и не трябва да се вика в транзакция
                    var generationResult = await DocumentProcessFormService.GenerateFormXmlContentAsync(process, true, cancellationToken);

                    if (!generationResult.IsSuccessfullyCompleted)
                    {
                        return new OperationResult<Guid>(generationResult.Errors);
                    }

                    xmlFormStream = XmlHelpers.GetStreamFromXmlDocument(generationResult.Result.TextContent);
                }
                else
                {
                    var xmlContent = process.ProcessContents.Single(c => c.Type == DocumentProcessContentTypes.FromXML);

                    xmlFormStream = XmlHelpers.GetStreamFromXmlDocument(xmlContent.TextContent);
                }

                return await DBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
                {
                    #region Създаваме запис в приложението за подписване

                    var signingRequest = CreateDocumentSigningRequest(process, xmlFormStream);

                    var signResult = await SigningService.CreateSigningProcessAsync(signingRequest, token);

                    if (!signResult.IsSuccessfullyCompleted)
                    {
                        return new OperationResult<Guid>(signResult.Errors);
                    }

                    var signingGiud = signResult.Result;

                    #endregion

                    #region Подменяме статуса на заявлението на Signing

                    process.Status = ProcessStatuses.Signing;
                    process.SigningGuid = signingGiud;

                    process.AdditionalData["userSessionID"] = UserAccessor.UserSessionID.ToString();
                    process.AdditionalData["loginSessionID"] = UserAccessor.User?.LoginSessionID.ToString();
                    process.AdditionalData["ipAddress"] = UserAccessor.RemoteIpAddress.ToString();
                    process.AdditionalData["userCIN"] = UserAccessor.User?.CIN.ToString();

                    await DocumentProcessRepository.UpdateAsync(process, token);

                    #endregion

                    return new OperationResult<Guid>(OperationResultTypes.SuccessfullyCompleted)
                    {
                        Result = signingGiud
                    };
                }, cancellationToken);
            }
            finally
            {
                if (xmlFormStream != null)
                {
                    xmlFormStream.Dispose();
                }

                if (process != null && process.ProcessContents != null)
                {
                    foreach (var prContent in process.ProcessContents)
                    {
                        if (prContent.Content != null)
                        {
                            prContent.Content.Dispose();
                        }
                    }
                }

                if (process != null && process.AttachedDocuments != null)
                {
                    foreach (var attDoc in process.AttachedDocuments)
                    {
                        if (attDoc.Content != null && attDoc.Content.Content != null)
                        {
                            attDoc.Content.Content.Dispose();
                        }
                    }
                }
            }
        }

        public async Task<OperationResult<Guid>> StartSigningAttachedDocumentAsync(long processID, long docID, CancellationToken cancellationToken)
        {
            var process = (await DocumentProcessRepository.SearchAsync(new DocumentProcessSearchCriteria()
            {
                DocumentProcessID = processID,
                ApplicantCIN = UserAccessor.User.CIN
            }, cancellationToken)).SingleOrDefault();

            EnsureExistingProcessAndCheckAccess(processID, process);

            var currentDoc = (await AttachedDocumentRepository.SearchAsync(new AttachedDocumentSearchCriteria()
            {
                AttachedDocumentID = docID,
                DocumentProcessID = processID
            })).Single();

            var currentDocDocType = DocumentTypes[currentDoc.DocumentTypeID.Value];
            string fileName = string.Format("{0}.pdf", currentDocDocType.Name);
            if (fileName.Length > 250)
            {
                //Ограничаваме името на файла до 250 символа.
                fileName = string.Format("{0}-{1}", currentDocDocType.Name.Substring(0, 124), currentDocDocType.Name.Substring(currentDocDocType.Name.Length - 125));
            }

            var createDocReq = new CreateDocumentRequest()
            {
                FileName = fileName,
                HtmlTemplateContent = currentDoc.HtmlContent
            };

            var pdfDoc = await DocumentTemplatesServiceClient.CreateDocumentAsync(createDocReq);

            Guid signingGiud;

            #region Създаваме запис в приложението за подписване

            var signingRequest = new SigningRequest()
            {
                Content = pdfDoc.Content,
                Format = SigningFormats.PAdES,
                FileName = pdfDoc.FileName,
                ContentType = pdfDoc.ContentType,
                CompletedCallbackUrl = (Options.GL_EAU_PRIVATE_API + "Services/DocumentProcesses/SigningAttachedDocumentTemplateCompleted"),
                RejectedCallbackUrl = (Options.GL_EAU_PRIVATE_API + "Services/DocumentProcesses/SigningAttachedDocumentTemplatRejected"),
                SignerRequests = new List<SignerRequest>() { new SignerRequest() { Order = 1 } }
            };

            var signResult = await SigningService.CreateSigningProcessAsync(signingRequest, cancellationToken);

            if (!signResult.IsSuccessfullyCompleted)
            {
                return new OperationResult<Guid>(signResult.Errors);
            }

            signingGiud = signResult.Result;

            #endregion

            currentDoc.SigningGuid = signingGiud;

            return await DBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                await AttachedDocumentRepository.UpdateAsync(currentDoc);

                return new OperationResult<Guid>(OperationResultTypes.SuccessfullyCompleted) { Result = signingGiud };
            }, cancellationToken);
        }

        #region Helpers

        private SigningRequest CreateDocumentSigningRequest(DocumentProcess process, Stream xmlFormStream)
        {
            var docType = DocumentTypes[process.DocumentTypeID.Value];
            var docFormService = DocumentServicesProvider.GetDocumentFormService(docType.Uri);
            var signatureXpath = ((process.Type == DocumentProcessTypes.BackOffice)
                && (!String.IsNullOrEmpty(process.AdditionalData["SignatureXPath"]))) ? process.AdditionalData["SignatureXPath"] : docFormService.SignatureXpath;

            Dictionary<string, string> signatureXPathNamespaces = ((process.Type == DocumentProcessTypes.BackOffice)
                && (!String.IsNullOrEmpty(process.AdditionalData["SignatureXPathNamespaces"]))) ? JsonConvert.DeserializeObject<Dictionary<string, string>>(process.AdditionalData["SignatureXPathNamespaces"]) : docFormService.SignatureXPathNamespaces;

            return new SigningRequest()
            {
                Content = xmlFormStream,
                Format = SigningFormats.XAdES,
                ContentType = "application/xml",
                FileName = docType.Name + ".xml",
                SignatureXpath = signatureXpath,
                SignatureXPathNamespaces = signatureXPathNamespaces,
                CompletedCallbackUrl = (Options.GL_EAU_PRIVATE_API + "Services/DocumentProcesses/SigningCompleted"),
                RejectedCallbackUrl = (Options.GL_EAU_PRIVATE_API + "Services/DocumentProcesses/SigningRejected"),
                SignerRequests = new List<SignerRequest>() { new SignerRequest() { Order = 1 } }
            };
        }

        private void EnsureExistingProcessAndCheckAccess(long processID, DocumentProcess process)
        {
            if (process == null)
                throw new NoDataFoundException(processID.ToString(), "DocumentProcess");

            if (process.ApplicantID != UserAccessor.User.LocalClientID)
                throw new AccessDeniedException(processID.ToString(), "DocumentProcess", UserAccessor.User.LocalClientID);
        }

        #endregion
    }
}
