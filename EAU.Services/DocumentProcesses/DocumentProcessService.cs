using CNSys;
using CNSys.Data;
using CNSys.Xml;
using EAU.Audit;
using EAU.Audit.Models;
using EAU.Common;
using EAU.Documents;
using EAU.Documents.Domain;
using EAU.Nomenclatures;
using EAU.Security;
using EAU.Services.DocumentProcesses.Models;
using EAU.Services.DocumentProcesses.Repositories;
using EAU.Services.MessageHandlers;
using EAU.Services.ServiceInstances;
using EAU.Signing;
using EAU.Utilities;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using WAIS.Integration.EPortal.Clients;
using WAIS.Integration.EPortal.Models;
using WAIS.Integration.MOI.Core.BDS.NRBLD;

namespace EAU.Services.DocumentProcesses
{
    internal class DocumentProcessService : IDocumentProcessService, IDocumentProcessAttachedDocumentService
    {
        private readonly IDocumentProcessRepository DocumentProcessRepository;
        private readonly IAttachedDocumentRepository AttachedDocumentRepository;
        private readonly IDocumentProcessContentRepository DocumentProcessContentRepository;
        private readonly IDbContextOperationExecutor DBContextOperationExecutor;
        private readonly IEAUUserAccessor UserAccessor;
        private readonly IDocumentProcessFormService DocumentProcessFormService;
        private readonly IActionDispatcher ActionDispatcher;
        private readonly IWAISIntegrationServiceClientsFactory WAISIntegrationServiceClientsFactory;
        private readonly GlobalOptions Options;
        private readonly IServiceInstanceService ServiceInstanceService;
        private readonly ISigningService SigningService;
        private readonly ILogger Logger;
        private readonly INRBLDServicesClientFactory _nRBLDServicesClientFactory;
        private readonly IAuditService AuditService;
        private readonly IStringLocalizer StringLocalizer;
        private readonly IDocumentAccessDataLogManager DocumentAccessDataLogManager;

        //Nomenclatures
        private readonly IServices Services;
        private readonly IDeclarations nomDeclarations;
        private readonly IDeliveryChannels nomDeliveryChannels;
        private readonly IEkatte nomEkatte;
        private readonly IGrao nomGrao;
        private readonly IServiceTerms nomServiceTerms;
        private readonly IDocumentTypes DocumentTypes;
        private readonly IDocumentTemplates nomDocumentTemplates;

        public DocumentProcessService(
            IDocumentProcessRepository docProcessRepository,
            IAttachedDocumentRepository attachedDocumentRepository,
            IDocumentProcessContentRepository documentProcessContentRepository,
            IEAUUserAccessor userAccessor,
            IDocumentProcessFormService documentProcessFormService,
            IDbContextOperationExecutor dbContextOperationExecutor,
            IActionDispatcher actionDispatcher,
            IWAISIntegrationServiceClientsFactory wAISIntegrationServiceClientsFactory,
            IServiceInstanceService serviceInstanceService,
            IOptionsMonitor<GlobalOptions> globalOptions,
            ISigningService signingService,
            INRBLDServicesClientFactory iNRBLDServicesClientFactory,
            IAuditService auditService,
            ILogger<DocumentProcessService> logger,
            IStringLocalizer stringLocalizer,

            IServices services,
            IDeclarations declarations,
            IDeliveryChannels deliveryChannels,
            IEkatte ekatte,
            IGrao grao,
            IServiceTerms serviceTerms,
            IDocumentTypes documentTypes,
            IDocumentTemplates documentTemplates,
            IDocumentAccessDataLogManager documentAccessDataLogManager)
        {
            DocumentProcessRepository = docProcessRepository;
            DocumentProcessContentRepository = documentProcessContentRepository;
            UserAccessor = userAccessor;
            DBContextOperationExecutor = dbContextOperationExecutor;
            DocumentProcessFormService = documentProcessFormService;
            AttachedDocumentRepository = attachedDocumentRepository;
            Services = services;
            ActionDispatcher = actionDispatcher;
            WAISIntegrationServiceClientsFactory = wAISIntegrationServiceClientsFactory;
            DocumentTypes = documentTypes;
            ServiceInstanceService = serviceInstanceService;
            Options = globalOptions.CurrentValue;
            SigningService = signingService;
            _nRBLDServicesClientFactory = iNRBLDServicesClientFactory;
            AuditService = auditService;
            Logger = logger;
            StringLocalizer = stringLocalizer;

            nomDeclarations = declarations;
            nomDeliveryChannels = deliveryChannels;
            nomEkatte = ekatte;
            nomGrao = grao;
            nomServiceTerms = serviceTerms;
            nomDocumentTemplates = documentTemplates;
            DocumentAccessDataLogManager = documentAccessDataLogManager;
        }

        #region IDocumentProcessService

        public Task<IEnumerable<DocumentProcess>> SearchAsync(DocumentProcessSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DocumentProcessRepository.SearchAsync(searchCriteria, cancellationToken);
        }

        public async Task<OperationResult<DocumentProcess>> StartAsync(NewProcessRequest request, CancellationToken cancellationToken)
        {
            if (request.ServiceID.HasValue || !string.IsNullOrEmpty(request.DocumentMetadataURL))
            {
                var docProcessType = GetDocumentProcessType(request.RemovingIrregularitiesInstructionURI, request.AdditionalApplicationURI, request.CaseFileURI, request.DocumentMetadataURL, request.ServiceID, request.WithdrawService);

                var process = (await DocumentProcessRepository.SearchAsync(new DocumentProcessSearchCriteria()
                {
                    ApplicantCIN = UserAccessor.User?.CIN,
                    RequestID = request.RequestID,
                    ServiceID = request.ServiceID,
                    DocumentProcessType = docProcessType
                }, cancellationToken)).SingleOrDefault();

                if (process != null)
                {
                    if (!string.IsNullOrEmpty(process.RequestID))
                    {
                        await DeleteAsync(process.DocumentProcessID.Value, cancellationToken);
                    }
                    else
                    {
                        return new OperationResult<DocumentProcess>("GL_ProcessExists_L", "GL_ProcessExists_L");
                    }
                }
            }

            var processData = await InitDocumentProcessAsync(request, cancellationToken);

            if (!processData.IsSuccessfullyCompleted)
            {
                return new OperationResult<DocumentProcess>(processData.Errors);
            }

            var formData = await DocumentProcessFormService.InitFormAsync(processData.Result.initMode, processData.Result.Process.ServiceID, processData.Result.Process.DocumentTypeID.Value, processData.Result.Process.AdditionalData, processData.Result.FormXml, cancellationToken);

            if (!formData.IsSuccessfullyCompleted)
            {
                return new OperationResult<DocumentProcess>(formData.Errors);
            }

            if (processData.Result.Process.ServiceID == null)
            {
                //Ако е възможно взимаме идентификатора на услугата и го поставяме в AdditionalData на процеса, за да може UI-а
                //да има достъп до услугата и до допълнителните конфигурации на услугата, от които някой UI-и се нуждаят.
                var srvId = TryGetDocumentServiceID(processData.Result.FormXml);

                if (srvId != null)
                {
                    processData.Result.Process.AdditionalData.Add("ServiceID", srvId.Value.ToString());
                }
            }

            return await DBContextOperationExecutor.ExecuteAsync<DocumentProcess>(async (dbcontext, token) =>
            {
                await DocumentProcessRepository.CreateAsync(processData.Result.Process, token);

                var result = await DocumentProcessFormService.CreateFormAsync(processData.Result.Process, formData.Result, token);

                if (result.IsSuccessfullyCompleted)
                {
                    if (processData.Result.FormXml != null &&
                       (processData.Result.Process.Mode == DocumentProcessModes.Read || processData.Result.Process.Mode == DocumentProcessModes.Sign))
                    {
                        var docXmlContent = new DocumentProcessContent()
                        {
                            DocumentProcessID = processData.Result.Process.DocumentProcessID,
                            Type = DocumentProcessContentTypes.FromXML,
                            TextContent = processData.Result.FormXml.OuterXml
                        };

                        await DocumentProcessContentRepository.CreateAsync(docXmlContent, token);
                    }

                    if (processData.Result.Process.AttachedDocuments != null && processData.Result.Process.AttachedDocuments.Count > 0)
                    {
                        foreach (var attDoc in processData.Result.Process.AttachedDocuments)
                        {
                            attDoc.Content = null;
                        }
                    }

                    string documentInitMode = string.Empty;

                    if (processData.Result.Process.AdditionalData.ContainsKey("documentInitializationMode") && processData.Result.Process.AdditionalData.TryGetValue("documentInitializationMode", out documentInitMode))
                    {
                        DocumentModes documentMode = (DocumentModes)Enum.Parse(typeof(DocumentModes), documentInitMode);

                        if (documentMode == DocumentModes.WithdrawService)
                        {
                            processData.Result.Process.DocumentTypeID = DocumentTypes.GetByDocumentTypeUri(DocumentTypeUris.ApplicationForWithdrawService).DocumentTypeID;
                        }
                    }

                    return new OperationResult<DocumentProcess>(OperationResultTypes.SuccessfullyCompleted) { Result = processData.Result.Process };
                }
                else
                {
                    return new OperationResult<DocumentProcess>(result.Errors);
                }

            }, cancellationToken);
        }

        public Task DeleteAsync(long processID, CancellationToken cancellationToken)
        {
            return DBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                var process = (await SearchAsync(new DocumentProcessSearchCriteria()
                {
                    DocumentProcessID = processID,
                    LoadOption = new DocumentProcessLoadOption()
                    {
                        LoadAttachedDocument = true,
                        LoadAllContents = true
                    }
                }, token)).SingleOrDefault();

                EnsureExistingProcessAndCheckAccess(processID, process);

                if (process.Status == ProcessStatuses.Sending || (process.Status == ProcessStatuses.Accepted && string.IsNullOrEmpty(process.RequestID)))
                {
                    throw new InvalidOperationException(string.Format("Can't delete document process {0} in status {1}.", processID, process.Status));
                }

                if (process.AttachedDocuments != null)
                {
                    foreach (var document in process.AttachedDocuments)
                    {
                        await AttachedDocumentRepository.DeleteAsync(document, token);

                        if (document.DocumentProcessContentID.HasValue)
                        {
                            await DocumentProcessContentRepository.DeleteAsync(document.DocumentProcessContentID);
                        }

                        if (document.SigningGuid.HasValue)
                        {
                            var deleteRes = await SigningService.DeleteSigningProcessesAsync(new Guid[] { document.SigningGuid.Value }, cancellationToken);

                            if (!deleteRes.IsSuccessfullyCompleted)
                            {
                                throw new Exception(deleteRes.Errors.ElementAt(0).Message);
                            }
                        }
                    }
                }

                if (process.ProcessContents != null)
                {
                    foreach (var content in process.ProcessContents)
                    {
                        await DocumentProcessContentRepository.DeleteAsync(content);
                    }
                }

                await DocumentProcessRepository.DeleteAsync(process, token);

                return new OperationResult<object>(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }

        public async Task<OperationResult> UpdateFormAsync(long processID, string formContent, CancellationToken cancellationToken)
        {
            return await DBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                var process = (await SearchAsync(new DocumentProcessSearchCriteria()
                {
                    DocumentProcessID = processID
                }, cancellationToken)).SingleOrDefault();

                EnsureExistingProcessAndCheckAccess(processID, process);

                if (process.Mode != DocumentProcessModes.Write && process.Mode != DocumentProcessModes.WriteAndSign)
                {
                    throw new InvalidOperationException(string.Format("Can't udate form of document process {0} in Mode Write or WriteAndSign.", processID));
                }

                var processFormContent = (await DocumentProcessContentRepository.SearchAsync(new DocumentProcessContentSearchCriteria()
                {
                    DocumentProcessIDs = new List<long>() { processID },
                    Type = DocumentProcessContentTypes.FormJSON
                }, token)).Single();

                processFormContent.TextContent = formContent;
                await DocumentProcessContentRepository.UpdateAsync(processFormContent, token);

                return new OperationResult<object>(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }

        public async Task<OperationResult> ReturnToInProcessStatusAsync(long processID, CancellationToken cancellationToken)
        {
            return await DBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                var process = (await SearchAsync(new DocumentProcessSearchCriteria()
                {
                    DocumentProcessID = processID,
                    LoadOption = new DocumentProcessLoadOption() { LoadFormXmlContent = true }
                }, token)).SingleOrDefault();

                EnsureExistingProcessAndCheckAccess(processID, process);

                if (process.Status != ProcessStatuses.ErrorInAccepting)
                {
                    throw new InvalidOperationException(string.Format("Can't return to InProcess status document process {0} in status {1}.", processID, process.Status));
                }

                process.Status = ProcessStatuses.InProcess;

                if (process.Mode == DocumentProcessModes.Sign && process.Type == DocumentProcessTypes.BackOffice)
                {
                    await DocumentProcessContentRepository.DeleteAsync(process.ProcessContents.Single(c => c.Type == DocumentProcessContentTypes.FromXML));

                    var docXmlStream = await WAISIntegrationServiceClientsFactory.GetEDocViewerServiceClient().GetDocumentContentAsync(process.AdditionalData["DocumentURL"],
                        cancellationToken);
                    var docXml = DocumentProcessFormService.ParseXmlDocument(docXmlStream.FileContentStream.Content);

                    var docXmlContent = new DocumentProcessContent()
                    {
                        DocumentProcessID = process.DocumentProcessID,
                        Type = DocumentProcessContentTypes.FromXML,
                        TextContent = docXml.OuterXml
                    };

                    await DocumentProcessContentRepository.CreateAsync(docXmlContent, token);
                }

                await DocumentProcessRepository.UpdateAsync(process, token);

                return new OperationResult<object>(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }

        public async Task<OperationResult> StartSendingAsync(long processID, CancellationToken cancellationToken)
        {
            DocumentProcess process = null;

            try
            {
                process = (await SearchAsync(new DocumentProcessSearchCriteria()
                {
                    DocumentProcessID = processID,
                    LoadOption = new DocumentProcessLoadOption()
                    {
                        LoadAttachedDocument = true,
                        LoadAllContents = true
                    }
                }, cancellationToken)).SingleOrDefault();

                EnsureExistingProcessAndCheckAccess(processID, process);

                if (process.Mode != DocumentProcessModes.Write)
                {
                    throw new ArgumentException("Only process in Mode: Write can be send whitout signing");
                }

                if (process.Type != DocumentProcessTypes.BackOffice)
                {
                    throw new ArgumentException("Only process of Type: BackOffice can be send whitout signing");
                }

                if (process.Status == ProcessStatuses.InProcess)
                {
                    //Метода GenerateFormXmlContentAsync Прави въшни извиквания и не трябва да се вика в транзакция, метода е идентотентен, така че не е проблем да се извика няколко пъти
                    var generationResult = await DocumentProcessFormService.GenerateFormXmlContentAsync(process, false, cancellationToken);

                    if (!generationResult.IsSuccessfullyCompleted)
                    {
                        return new OperationResult(generationResult.Errors);
                    }

                    await DBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
                    {
                        process.Status = ProcessStatuses.Sending;
                        await DocumentProcessRepository.UpdateAsync(process, token);

                        await ActionDispatcher.SendAsync(new DocumentSendMessage()
                        {
                            DocumentProcessID = process.DocumentProcessID.Value
                        });

                        return new OperationResult<object>(OperationResultTypes.SuccessfullyCompleted);
                    }, cancellationToken);
                }
            }
            finally
            {
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

            return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
        }

        public async Task SendAsync(long processID, CancellationToken cancellationToken)
        {
            DocumentProcess process = (await SearchAsync(new DocumentProcessSearchCriteria()
            {
                DocumentProcessID = processID,
                LoadOption = new DocumentProcessLoadOption()
                {
                    LoadFormXmlContent = true
                }
            }, cancellationToken)).SingleOrDefault();


            if (process.Status == ProcessStatuses.InProcess || process.Status == ProcessStatuses.Signing)
            {
                throw new InvalidOperationException(string.Format("Can't send document process {0} in status {1}.", processID, process.Status));
            }

            if (process.Status == ProcessStatuses.Sending)
            {
                var docType = DocumentTypes[process.DocumentTypeID.Value];
                bool accepted = true;
                Stream xmlFormStream = new MemoryStream();
                xmlFormStream = XmlDocumentHelpers.GetXmlDocumentStreamUTF8Encoding(process.ProcessContents.Single(pc => pc.Type == DocumentProcessContentTypes.FromXML).TextContent);
                try
                {
                    if (process.Type == DocumentProcessTypes.Portal || process.Type == DocumentProcessTypes.PortalAdditionalApp)
                    {
                        await WAISIntegrationServiceClientsFactory.GetDocumentServiceClient().RegisterStructuredDocumentAsync(new WAIS.Integration.EPortal.Models.DocumentRegistrationRequest()
                        {
                            RequestID = string.Format("{0}_{1}", process.DocumentProcessID, process.ProcessContents.Single(pc => pc.Type == DocumentProcessContentTypes.FromXML).DocumentProcessContentID),
                            DocumentFileName = docType.Name + ".xml",
                            DocumentData = xmlFormStream,
                            CallBackURL = (Options.GL_EAU_PRIVATE_API + "Services/DocumentProcesses/RegistrationCompleted"),
                        }, cancellationToken);
                    }
                    else
                    {
                        try
                        {
                            var res = await WAISIntegrationServiceClientsFactory.GetEDocViewerServiceClient().SaveDocumentAsync(new WAIS.Integration.EPortal.Models.DocumentContentUpdateRequest()
                            {
                                Url = process.AdditionalData["DocumentURL"],
                                DocumentFileName = docType.Name + ".xml",
                                DocumentData = xmlFormStream,
                            }, cancellationToken);

                            if (!res.IsSuccessful)
                            {
                                accepted = false;
                                process.ErrorMessage = ((res.ValidationErrors != null) ? String.Join(".", res.ValidationErrors) : "");
                            }
                        }
                        catch (Exception ex)
                        {
                            accepted = false;
                            Logger.LogError($"Error during sending document to WAIS from EDocViewer: {ex.Message}");
                        }
                    }
                }
                finally
                {
                    if (xmlFormStream != null)
                        xmlFormStream.Dispose();
                }


                await DBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
                {
                    if (accepted)
                    {
                        process = (await SearchAsync(new DocumentProcessSearchCriteria()
                        {
                            DocumentProcessID = processID,
                            LoadOption = new DocumentProcessLoadOption()
                            {
                                LoadWithLock = true
                            }
                        }, token)).SingleOrDefault();

                        if (process.Status == ProcessStatuses.Sending)
                        {
                            process.Status = ProcessStatuses.Accepted;
                            await DocumentProcessRepository.UpdateAsync(process, token);
                        }
                    }
                    else
                    {
                        process.Status = ProcessStatuses.ErrorInAccepting;

                        await DocumentProcessRepository.UpdateAsync(process, token);
                    }

                    return new OperationResult<object>(OperationResultTypes.SuccessfullyCompleted);
                }, cancellationToken);
            }
        }

        public async Task<bool> HasChangesInApplicationsNomenclatureAsync(DocumentProcess process)
        {
            List<Task> tasks = new List<Task>()
            {
                Services.EnsureLoadedAsync(CancellationToken.None).AsTask(),
                nomDeclarations.EnsureLoadedAsync(CancellationToken.None).AsTask(),
                nomDeliveryChannels.EnsureLoadedAsync(CancellationToken.None).AsTask(),
                nomEkatte.EnsureLoadedAsync(CancellationToken.None).AsTask(),
                nomGrao.EnsureLoadedAsync(CancellationToken.None).AsTask(),
                nomServiceTerms.EnsureLoadedAsync(CancellationToken.None).AsTask(),
                DocumentTypes.EnsureLoadedAsync(CancellationToken.None).AsTask(),
                nomDocumentTemplates.EnsureLoadedAsync(CancellationToken.None).AsTask()
            };

            await Task.WhenAll(tasks);

            //Взимаме от номенклатурите, чиято промяна би оказала промяна по заявленията в процес, най-скорошната дата на промяна и 
            //я сравняваме с датата на създаване на процеса.

            Services.Search("bg", out DateTime? servicesUpdatedOn);
            nomDeclarations.Search(out DateTime? declarationsUpdatedOn);
            nomDeliveryChannels.Search(out DateTime? deliveryChannelsUpdatedOn);
            nomEkatte.Search(out DateTime? ekatteUpdatedOn);
            nomGrao.Search(out DateTime? graoUpdatedOn);
            nomServiceTerms.Search(out DateTime? serviceTermsUpdatedOn);
            DocumentTypes.Search(out DateTime? docTypesUpdatedOn);
            nomDocumentTemplates.Search(out DateTime? docTemplatesUpdatedOn);

            var nomDates = new List<DateTime?>()
            {
                servicesUpdatedOn,
                declarationsUpdatedOn,
                deliveryChannelsUpdatedOn,
                ekatteUpdatedOn,
                graoUpdatedOn,
                serviceTermsUpdatedOn,
                docTypesUpdatedOn,
                docTemplatesUpdatedOn
            };

            var lastUpdatedNom = nomDates.OrderByDescending(x => x).FirstOrDefault();

            return lastUpdatedNom > process.CreatedOn;
        }

        public DocumentProcessTypes GetDocumentProcessType(string removingIrregularitiesInstructionURI, string additionalApplicationURI, string caseFileURI, string documentMetadataURL, int? serviceID, bool? withdrawService)
        {
            if (serviceID != null
              && string.IsNullOrEmpty(removingIrregularitiesInstructionURI)
              && string.IsNullOrEmpty(additionalApplicationURI)
              && (!withdrawService.HasValue || withdrawService.Value != true))
            {
                return DocumentProcessTypes.Portal;
            }
            else if (!string.IsNullOrEmpty(caseFileURI) && !string.IsNullOrEmpty(removingIrregularitiesInstructionURI))
            {
                return DocumentProcessTypes.Portal;
            }
            else if (!string.IsNullOrEmpty(caseFileURI) && withdrawService.HasValue && withdrawService.Value == true)
            {
                return DocumentProcessTypes.PortalAdditionalApp;
            }
            else if (!string.IsNullOrEmpty(caseFileURI) && !string.IsNullOrEmpty(additionalApplicationURI))
            {
                return DocumentProcessTypes.PortalAdditionalApp;
            }
            else if (!string.IsNullOrEmpty(documentMetadataURL))
            {
                return DocumentProcessTypes.BackOffice;
            }
            else
            {
                //За преглед
                return DocumentProcessTypes.Portal;
            }
        }

        #endregion

        #region IDocumentProcessAttachedDocumentService

        public async Task<IEnumerable<AttachedDocument>> SearchAttachedDocumentsAsync(AttachedDocumentSearchCriteria criteria, CancellationToken cancellationToken)
        {
            if (criteria.DocumentProcessID != null)
            {
                await SearchForExistingProcessAndCheckAccessAsync(criteria.DocumentProcessID.Value, cancellationToken);
            }

            return await AttachedDocumentRepository.SearchAsync(criteria, cancellationToken);
        }

        public async Task<OperationResult<AttachedDocument>> AddAttachedDocumentAsync(long processID, AttachedDocument doc, Stream content, CancellationToken cancellationToken)
        {
            await SearchForExistingProcessAndCheckAccessAsync(processID, cancellationToken);

            var result = await DBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                if (content != null)
                {
                    var docContent = new DocumentProcessContent()
                    {
                        DocumentProcessID = processID,
                        Type = DocumentProcessContentTypes.AttachedDocument,
                        Content = content
                    };

                    await DocumentProcessContentRepository.CreateAsync(docContent);

                    doc.DocumentProcessContentID = docContent.DocumentProcessContentID;
                }

                doc.DocumentProcessID = processID;
                doc.AttachedDocumentGuid = Guid.NewGuid();

                await AttachedDocumentRepository.CreateAsync(doc);

                return new OperationResult<AttachedDocument>(OperationResultTypes.SuccessfullyCompleted)
                {
                    Result = doc
                };
            }, cancellationToken);

            return result;
        }

        public async Task UpdateAttachedDocumentAsync(long processID, AttachedDocument doc, Stream content, CancellationToken cancellationToken)
        {
            await SearchForExistingProcessAndCheckAccessAsync(processID, cancellationToken);

            await DBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                var currentDoc = (await AttachedDocumentRepository.SearchAsync(new AttachedDocumentSearchCriteria()
                {
                    AttachedDocumentID = doc.AttachedDocumentID,
                    DocumentProcessID = processID
                })).Single();

                currentDoc.Description = doc.Description;
                currentDoc.DocumentTypeID = doc.DocumentTypeID;
                currentDoc.HtmlContent = doc.HtmlContent;
                currentDoc.SigningGuid = doc.SigningGuid;
                currentDoc.FileName = doc.FileName;
                currentDoc.MimeType = doc.MimeType;

                if (content != null)
                {
                    if (currentDoc.DocumentProcessContentID.HasValue)
                    {
                        await DocumentProcessContentRepository.DeleteAsync(currentDoc.DocumentProcessContentID);
                    }

                    var docContent = new DocumentProcessContent()
                    {
                        DocumentProcessID = processID,
                        Type = DocumentProcessContentTypes.AttachedDocument,
                        Content = content
                    };

                    await DocumentProcessContentRepository.CreateAsync(docContent);

                    currentDoc.DocumentProcessContentID = docContent.DocumentProcessContentID;
                }

                await AttachedDocumentRepository.UpdateAsync(currentDoc);

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }

        public async Task DeleteAttachedDocumentAsync(long processID, long docID, CancellationToken cancellationToken)
        {
            await SearchForExistingProcessAndCheckAccessAsync(processID, cancellationToken);

            var doc = (await AttachedDocumentRepository.SearchAsync(new AttachedDocumentSearchCriteria()
            {
                AttachedDocumentID = docID,
                DocumentProcessID = processID
            })).Single();

            if (doc.SigningGuid.HasValue)
            {
                //Ако има започнат процес по подписване го изтриваме.
                var deleteRes = await SigningService.DeleteSigningProcessesAsync(new Guid[] { doc.SigningGuid.Value }, cancellationToken);

                if (!deleteRes.IsSuccessfullyCompleted)
                {
                    throw new Exception(deleteRes.Errors.ElementAt(0).Message);
                }
            }

            await DBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                await AttachedDocumentRepository.DeleteAsync(doc);

                if (doc.DocumentProcessContentID.HasValue)
                {
                    await DocumentProcessContentRepository.DeleteAsync(doc.DocumentProcessContentID);
                }

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }

        public async Task<IEnumerable<DocumentProcessContent>> SearchDocumentProcessContentsAsync(DocumentProcessContentSearchCriteria documentProcessContentSearchCriteria, CancellationToken cancellationToken)
        {
            var processContents = await DocumentProcessContentRepository.SearchAsync(documentProcessContentSearchCriteria, cancellationToken);

            return processContents;
        }

        #endregion

        #region Helpers

        private async Task<OperationResult<(DocumentProcess Process, XmlDocument FormXml, DocumentModes initMode)>> InitDocumentProcessAsync(NewProcessRequest request, CancellationToken cancellationToken)
        {
            XmlDocument fromXml = null;
            DocumentModes? initMode = null;
            var process = new DocumentProcess()
            {
                ApplicantID = UserAccessor.User?.LocalClientID,
                ServiceID = request.ServiceID,
                RequestID = request.RequestID,
                Status = ProcessStatuses.InProcess,
                CaseFileURI = request.CaseFileURI,
                AdditionalData = new AdditionalData()
            };

            if (request.ServiceID != null
                && string.IsNullOrEmpty(request.RemovingIrregularitiesInstructionURI)
                && string.IsNullOrEmpty(request.AdditionalApplicationURI)
                && (!request.WithdrawService.HasValue || request.WithdrawService.Value != true))
            {
                initMode = DocumentModes.NewApplication;
                var result = await InitNewApplicationProcessAsync(process, cancellationToken);

                if (!result.IsSuccessfullyCompleted)
                    return new OperationResult<(DocumentProcess Process, XmlDocument FormXml, DocumentModes initMode)>(result.Errors);
            }
            else if (!string.IsNullOrEmpty(request.CaseFileURI) && !string.IsNullOrEmpty(request.RemovingIrregularitiesInstructionURI))
            {
                initMode = DocumentModes.RemovingIrregularitiesApplication;
                fromXml = await InitRemovingIrregularitiesApplicationProcessAsync(process, request.RemovingIrregularitiesInstructionURI, request.CaseFileURI, cancellationToken);
            }
            else if (!string.IsNullOrEmpty(request.CaseFileURI) && request.WithdrawService.HasValue && request.WithdrawService.Value == true)
            {
                initMode = DocumentModes.WithdrawService;
                fromXml = await InitWithdrawServiceApplicationProcessAsync(process, request.CaseFileURI, cancellationToken);
            }
            else if (!string.IsNullOrEmpty(request.CaseFileURI) && !string.IsNullOrEmpty(request.AdditionalApplicationURI))
            {
                initMode = DocumentModes.AdditionalApplication;
                fromXml = await InitAdditionalApplicationProcessAsync(process, request.AdditionalApplicationURI, request.CaseFileURI, cancellationToken);
            }
            else if (request.DocumentXMLContent != null)
            {
                try
                {
                    initMode = DocumentModes.ViewDocument;
                    fromXml = InitPreviewDocumentProcess(process, request.DocumentXMLContent);
                }
                catch (Exception err)
                {
                    Logger.LogInformation($"Error during parce document for preview: {err}");

                    return new OperationResult<(DocumentProcess Process, XmlDocument FormXml, DocumentModes initMode)>("GL_CANNOT_PREVIEW_XML_DOCUMENT_E", "GL_CANNOT_PREVIEW_XML_DOCUMENT_E");
                }
            }
            else if (!string.IsNullOrEmpty(request.CaseFileURI) && !string.IsNullOrEmpty(request.DocumentURI))
            {
                initMode = DocumentModes.ViewDocument;
                var result = await InitPreviewDocumentProcessAsync(process, request.DocumentURI, request.CaseFileURI, cancellationToken);

                if (!result.IsSuccessfullyCompleted)
                {
                    return new OperationResult<(DocumentProcess Process, XmlDocument FormXml, DocumentModes initMode)>(result.Errors);
                }

                fromXml = result.Result;

                var logAction = new LogAction()
                {
                    ObjectType = ObjectTypes.Document,
                    ActionType = ActionTypes.Preview,
                    Functionality = Common.Models.Functionalities.Portal,
                    UserID = process.ApplicantID,
                    Key = request.DocumentURI.ToString(),
                    LoginSessionID = UserAccessor.User.LoginSessionID,
                    IpAddress = UserAccessor.RemoteIpAddress.GetAddressBytes(),
                    AdditionalData = new Utilities.AdditionalData()
                };
                logAction.AdditionalData.Add("DocumentTypeName", DocumentTypes.Search(new List<int>() { process.DocumentTypeID.Value }).First().Name);
                logAction.AdditionalData.Add("CaseFileURI", request.CaseFileURI);

                await AuditService.CreateLogActionAsync(logAction, cancellationToken);

                if (DocumentAccessDataLogManager.ShouldLogDocumentAccessData(process.DocumentTypeID.Value))
                {
                    Logger.LogInformation($"Will log document access data for document {request.DocumentURI}");

                    var client = WAISIntegrationServiceClientsFactory.GetDocumentServiceClient();

                    var serviceInstance = await client.GetServiceInstanceAsync(new URI(request.CaseFileURI), cancellationToken);
                    var documentFields = await client.GetServiceInstanceDocumentFieldsAsync(new URI(request.CaseFileURI), cancellationToken);

                    var applicant = serviceInstance.Applicants.FirstOrDefault();

                    await DocumentAccessDataLogManager.LogDocumentAccessDataAsync(new LogDocumentAccessDataRequest
                    {
                        ApplicantNames = $"{applicant.FirstName} {applicant.SurName} {applicant.FamilyName}",
                        ApplicantIdentifier = applicant.PersonalIDNumber,
                        DocumentTypeId = process.DocumentTypeID.Value,
                        DocumentUri = request.DocumentURI,
                        DocumentFields = documentFields,
                        IpAddress = logAction.IpAddress
                    }, cancellationToken);
                }
            }
            else if (!string.IsNullOrEmpty(request.DocumentMetadataURL))
            {
                var metadata = await WAISIntegrationServiceClientsFactory.GetEDocViewerServiceClient().GetDocumentMetadataAsync(request.DocumentMetadataURL, cancellationToken);
                initMode = MapDocumentViewMode(metadata.DocumentViewMode);

                fromXml = await InitBackOfficeDocumentProcessAsync(process, metadata, cancellationToken);
            }
            else if (!string.IsNullOrWhiteSpace(request.NotAcknowledgedMessageURI))
            {
                initMode = DocumentModes.ViewDocument;
                var result = await InitNotAcknowledgedMessageAsync(process, request.NotAcknowledgedMessageURI, request.DocProcessId, cancellationToken);

                if (!result.IsSuccessfullyCompleted)
                    return new OperationResult<(DocumentProcess Process, XmlDocument FormXml, DocumentModes initMode)>(result.Errors);

                fromXml = result.Result;
            }

            process.AdditionalData["documentInitializationMode"] = ((int)initMode).ToString();

            return new OperationResult<(DocumentProcess Process, XmlDocument FormXml, DocumentModes initMode)>(OperationResultTypes.SuccessfullyCompleted)
            {
                Result = (process, fromXml, initMode.Value)
            };
        }

        private async Task<OperationResult> InitNewApplicationProcessAsync(DocumentProcess process, CancellationToken cancellationToken)
        {
            var service = Services.Search().Single(s => s.ServiceID == process.ServiceID);
            process.Mode = DocumentProcessModes.WriteAndSign;
            process.Type = DocumentProcessTypes.Portal; 
            process.DocumentTypeID = service.DocumentTypeID;

            if (service.AdditionalConfiguration != null &&
                service.AdditionalConfiguration.ContainsKey("isServiceForIssuingDrivingLicense") &&
                service.AdditionalConfiguration["isServiceForIssuingDrivingLicense"].ToLower() == "true")
            {
                if (UserAccessor.User?.IsUserIdentifiable != true)
                {
                    //REQ_PEAU_0229, MVREAU2020-1305 Подаване на ново заявление за ЕАУ
                    //Неуспешна автентикация! За подаване на заявление трябва да се автентикирате в системата с КЕП или чрез е-Автентикация.
                    return new OperationResult("GL_00011_E", "GL_00011_E");
                }

                var personInfoResponse = await _nRBLDServicesClientFactory.GetNRBLDServicesClient().GetPersonInfoAsync(UserAccessor.User.PersonIdentifier, false, cancellationToken);

                if (personInfoResponse?.Errors?.Count > 0)
                {
                    var errors = new ErrorCollection();
                    errors.AddRange(personInfoResponse.Errors.Select(e => (IError)(new TextError(e.Code, e.Message))));
                    return new OperationResult(errors);
                }

                if (personInfoResponse.Response.PersonData.PersonIdentification.PersonIdentificationBG == null)
                {
                    process.AdditionalData["IsBulgarianCitizen"] = "false";
                    process.AdditionalData["PersonIdentificationForeignStatut"] = personInfoResponse.Response.PersonData.PersonIdentification.PersonIdentificationF.Statut.Value.ToString();

                    if (personInfoResponse.Response.PersonData.PersonIdentification.PersonIdentificationF.Statut.Value != WAIS.Integration.MOI.BDS.NRBLD.Models.StatutName.EUCitizen
                        && personInfoResponse.Response.PersonData.PersonIdentification.PersonIdentificationF.Statut.Value != WAIS.Integration.MOI.BDS.NRBLD.Models.StatutName.ForeignerPermanently
                            && personInfoResponse.Response.PersonData.PersonIdentification.PersonIdentificationF.Statut.Value != WAIS.Integration.MOI.BDS.NRBLD.Models.StatutName.ForeignerTemporarily)
                    {
                        //Не можете да продължите със заявяване на избраната от Вас услуга. В Националния автоматизиран информационен фонд "Национален регистър на българските лични документи" лицето с ЕГН / ЛНЧ / ЛН { pid}
                        //няма разрешено пребиваване в Република България.
                        var localizedError = StringLocalizer["GL_00039_E"].Value.Replace("{pid}", personInfoResponse.Response.PersonData.PersonIdentification.PersonIdentificationF.LNC);

                        return new OperationResult(localizedError, localizedError);
                    }
                }
                else
                {
                    process.AdditionalData["IsBulgarianCitizen"] = "true";
                }
            }

            return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
        }

        private async Task<OperationResult<XmlDocument>> InitNotAcknowledgedMessageAsync(DocumentProcess process, string NotAcknowledgedMessageURI, long? docProcessId, CancellationToken cancellationToken)
        {
            //docProcessId Validation
            XmlDocument docXml = null;

            process.Mode = DocumentProcessModes.Read;
            process.Type = DocumentProcessTypes.Portal;
            process.AdditionalData["NotAcknowledgedMessageURI"] = NotAcknowledgedMessageURI;

            var document = await WAISIntegrationServiceClientsFactory.GetDocumentServiceClient().GetDocumentContentAsync(new WAIS.Integration.EPortal.Models.URI(NotAcknowledgedMessageURI), cancellationToken);

            using (document.FileContentStream.Content)
            {
                docXml = DocumentProcessFormService.ParseXmlDocument(document.FileContentStream.Content);
            }

            var documentTypeURI = DocumentProcessFormService.GetDocumentTypeURI(docXml);
            process.DocumentTypeID = DocumentTypes.GetByDocumentTypeUri(documentTypeURI).DocumentTypeID;

            return new OperationResult<XmlDocument>(OperationResultTypes.SuccessfullyCompleted)
            {
                Result = docXml
            };
        }

        private async Task<XmlDocument> InitRemovingIrregularitiesApplicationProcessAsync(DocumentProcess process, string removingIrregularitiesInstructionURI, string caseFileURI, CancellationToken cancellationToken)
        {
            XmlDocument docXml = null;

            var serviceInstance = (await ServiceInstanceService.SearchAsync(new ServiceInstances.Models.ServiceInstanceSearchCriteria()
            {
                CaseFileURI = caseFileURI,
                ApplicantID = process.ApplicantID
            }, cancellationToken)).SingleOrDefault();

            if (serviceInstance == null)
            {
                throw new ArgumentException($"Applicant with UserID{process.ApplicantID}, is not same applicant started the service with CaseFileURI {caseFileURI}.");
            }

            process.Mode = DocumentProcessModes.WriteAndSign;
            process.Type = DocumentProcessTypes.Portal;
            process.ServiceID = serviceInstance.ServiceID;

            process.AdditionalData["removingIrregularitiesInstructionURI"] = removingIrregularitiesInstructionURI;
            process.AdditionalData["caseFileURI"] = caseFileURI;

            var caseFile = await WAISIntegrationServiceClientsFactory.GetDocumentServiceClient().GetCaseFileAsync(new WAIS.Integration.EPortal.Models.URI(caseFileURI), cancellationToken);

            if (!caseFile.Documents.Any(d => d.DocumentURI.ToString() == removingIrregularitiesInstructionURI && d.DocumentTypeURI == DocumentTypeUris.RemovingIrregularitiesInstructionsUri))
            {
                throw new ArgumentException($"CaseFile {caseFileURI}, doesn't contain Removing Irregularities Instructions with uri {removingIrregularitiesInstructionURI}.");
            }

            var applicationTypeURI = caseFile.Documents.Single(d => d.DocumentURI.ToString() == caseFileURI).DocumentTypeURI;

            process.DocumentTypeID = DocumentTypes.GetByDocumentTypeUri(applicationTypeURI).DocumentTypeID;

            var lastApplicationURI = caseFile.Documents.Where(d => d.DocumentTypeURI == applicationTypeURI).OrderByDescending(d => d.RegistrationTime).First().DocumentURI;
            //Изичита се заедно със съдържанието на заявлението
            process.AdditionalData["originalApplicationURI"] = lastApplicationURI.ToString();

            var lastApplication = await WAISIntegrationServiceClientsFactory.GetDocumentServiceClient().GetDocumentContentAsync(lastApplicationURI, cancellationToken);

            using (lastApplication.FileContentStream.Content)
            {
                docXml = DocumentProcessFormService.ParseXmlDocument(lastApplication.FileContentStream.Content);
            }

            return docXml;
        }

        private async Task<XmlDocument> InitWithdrawServiceApplicationProcessAsync(DocumentProcess process, string caseFileURI, CancellationToken cancellationToken)
        {
            XmlDocument docXml = null;

            var serviceInstance = (await ServiceInstanceService.SearchAsync(new ServiceInstances.Models.ServiceInstanceSearchCriteria()
            {
                CaseFileURI = caseFileURI,
                ApplicantID = process.ApplicantID
            }, cancellationToken)).SingleOrDefault();

            if (serviceInstance == null)
            {
                throw new ArgumentException($"Applicant with UserID{process.ApplicantID}, is not same applicant started the service with CaseFileURI {caseFileURI}.");
            }

            process.Mode = DocumentProcessModes.WriteAndSign;
            process.Type = DocumentProcessTypes.PortalAdditionalApp;
            process.ServiceID = serviceInstance.ServiceID;

            var caseFile = await WAISIntegrationServiceClientsFactory.GetDocumentServiceClient().GetCaseFileAsync(new WAIS.Integration.EPortal.Models.URI(caseFileURI), cancellationToken);

            var applicationTypeURI = caseFile.Documents.Single(d => d.DocumentURI.ToString() == caseFileURI).DocumentTypeURI;

            process.DocumentTypeID = DocumentTypes.GetByDocumentTypeUri(DocumentTypeUris.ApplicationForWithdrawService).DocumentTypeID;

            var lastApplicationURI = caseFile.Documents.Where(d => d.DocumentTypeURI == applicationTypeURI).OrderByDescending(d => d.RegistrationTime).First().DocumentURI;
            //Изичита се заедно със съдържанието на заявлението
            process.AdditionalData["originalApplicationURI"] = lastApplicationURI.ToString();
            process.AdditionalData["originalApplicationDocumentTypeId"] = DocumentTypes.GetByDocumentTypeUri(applicationTypeURI).DocumentTypeID.ToString();
            process.AdditionalData["caseFileURI"] = caseFileURI;

            var lastApplication = await WAISIntegrationServiceClientsFactory.GetDocumentServiceClient().GetDocumentContentAsync(lastApplicationURI, cancellationToken);

            using (lastApplication.FileContentStream.Content)
            {
                docXml = DocumentProcessFormService.ParseXmlDocument(lastApplication.FileContentStream.Content);
            }

            return docXml;
        }

        private async Task<XmlDocument> InitAdditionalApplicationProcessAsync(DocumentProcess process, string additionalApplicationURI, string caseFileURI, CancellationToken cancellationToken)
        {
            XmlDocument docXml = null;

            var serviceInstance = (await ServiceInstanceService.SearchAsync(new ServiceInstances.Models.ServiceInstanceSearchCriteria()
            {
                CaseFileURI = caseFileURI,
                ApplicantID = process.ApplicantID
            }, cancellationToken)).SingleOrDefault();

            if (serviceInstance == null)
            {
                throw new ArgumentException($"Applicant with UserID{process.ApplicantID}, is not same applicant started the service with CaseFileURI {caseFileURI}.");
            }

            process.Mode = DocumentProcessModes.WriteAndSign;
            process.Type = DocumentProcessTypes.PortalAdditionalApp;
            process.ServiceID = serviceInstance.ServiceID;

            process.AdditionalData["additionalApplicationURI"] = additionalApplicationURI;
            process.AdditionalData["caseFileURI"] = caseFileURI;

            var caseFile = await WAISIntegrationServiceClientsFactory.GetDocumentServiceClient().GetCaseFileAsync(new WAIS.Integration.EPortal.Models.URI(caseFileURI), cancellationToken);

            if (!caseFile.Documents.Any(d => d.DocumentURI.ToString() == additionalApplicationURI))
            {
                throw new ArgumentException($"CaseFile {caseFileURI}, doesn't contain Аdditional ApplicationURI with uri {additionalApplicationURI}.");
            }

            var applicationTypeURI = caseFile.Documents.Single(d => d.DocumentURI.ToString() == caseFileURI).DocumentTypeURI;

            var аdditionalApplication = await WAISIntegrationServiceClientsFactory.GetDocumentServiceClient().GetDocumentContentAsync(new URI(additionalApplicationURI), cancellationToken);

            using (аdditionalApplication.FileContentStream.Content)
            {
                docXml = DocumentProcessFormService.ParseXmlDocument(аdditionalApplication.FileContentStream.Content);
            }

            var documentTypeURI = DocumentProcessFormService.GetDocumentTypeURI(docXml);
            process.DocumentTypeID = DocumentTypes.GetByDocumentTypeUri(documentTypeURI).DocumentTypeID;

            return docXml;
        }

        private XmlDocument InitPreviewDocumentProcess(DocumentProcess process, Stream docXmlStream)
        {
            var docXml = DocumentProcessFormService.ParseXmlDocument(docXmlStream);
            var docTypeURI = DocumentProcessFormService.GetDocumentTypeURI(docXml);
            var docType = DocumentTypes.GetByDocumentTypeUri(docTypeURI);

            process.Mode = DocumentProcessModes.Read;
            process.Type = DocumentProcessTypes.Portal;
            process.DocumentTypeID = docType.DocumentTypeID;

            return docXml;
        }

        private async Task<OperationResult<XmlDocument>> InitPreviewDocumentProcessAsync(DocumentProcess process, string documentURI, string caseFileURI, CancellationToken cancellationToken)
        {
            XmlDocument docXml = null;

            var serviceInstance = (await ServiceInstanceService.SearchAsync(new ServiceInstances.Models.ServiceInstanceSearchCriteria()
            {
                CaseFileURI = caseFileURI,
                ApplicantID = process.ApplicantID
            }, cancellationToken)).SingleOrDefault();

            if (serviceInstance == null)
            {
                //Нямате достъп до избраната преписка по услуга. Заявлението за услугата е подадено от друг потребител.
                return new OperationResult<XmlDocument>("GL_00022_E", "GL_00022_E");
            }

            var caseFile = await WAISIntegrationServiceClientsFactory.GetDocumentServiceClient().GetCaseFileAsync(new WAIS.Integration.EPortal.Models.URI(caseFileURI), cancellationToken);

            if (!caseFile.Documents.Any(d => d.DocumentURI.ToString() == documentURI))
            {
                //В преписката няма документ с въведеното УРИ.
                return new OperationResult<XmlDocument>("GL_00023_E", "GL_00023_E");
            }

            var documentTypeURI = caseFile.Documents.Single(d => d.DocumentURI.ToString() == documentURI).DocumentTypeURI;
            var documentType = DocumentTypes.GetByDocumentTypeUri(documentTypeURI);

            process.Mode = DocumentProcessModes.Read;
            process.Type = DocumentProcessTypes.Portal;
            process.DocumentTypeID = documentType.DocumentTypeID;
            process.AdditionalData["documentURI"] = documentURI;
            process.AdditionalData["caseFileURI"] = caseFileURI;
            process.AdditionalData["documentDate"] = documentURI.Split('-')[2];

            var document = await WAISIntegrationServiceClientsFactory.GetDocumentServiceClient().GetDocumentContentAsync(new WAIS.Integration.EPortal.Models.URI(documentURI), cancellationToken);

            using (document.FileContentStream.Content)
            {
                docXml = DocumentProcessFormService.ParseXmlDocument(document.FileContentStream.Content);
            }

            return new OperationResult<XmlDocument>(OperationResultTypes.SuccessfullyCompleted)
            {
                Result = docXml
            };
        }

        private async Task<XmlDocument> InitBackOfficeDocumentProcessAsync(DocumentProcess process, DocumentMetadata metadata, CancellationToken cancellationToken)
        {
            var docXmlStream = await WAISIntegrationServiceClientsFactory.GetEDocViewerServiceClient().GetDocumentContentAsync(metadata.DocumentURL, cancellationToken);
            var docXml = DocumentProcessFormService.ParseXmlDocument(docXmlStream.FileContentStream.Content);
            var docTypeURI = DocumentProcessFormService.GetDocumentTypeURI(docXml);

            process.Type = DocumentProcessTypes.BackOffice;
            process.Mode = MapDocumentProcessMode(metadata.DocumentViewMode);
            process.DocumentTypeID = DocumentTypes.GetByDocumentTypeUri(docTypeURI).DocumentTypeID;

            process.AdditionalData["DocumentURL"] = metadata.DocumentURL;
            process.AdditionalData["SignatureXPath"] = metadata.SignatureXPath;
            process.AdditionalData["NomenclatureURL"] = metadata.NomenclatureURL;
            process.AdditionalData["SignatureXPathNamespaces"] = JsonConvert.SerializeObject(metadata.SignatureXPathNamespaces, Newtonsoft.Json.Formatting.Indented);

            return docXml;
        }

        private DocumentProcessModes MapDocumentProcessMode(DocumentViewModes documentViewMode)
        {
            switch (documentViewMode)
            {
                case DocumentViewModes.Read: return DocumentProcessModes.Read;
                case DocumentViewModes.Write: return DocumentProcessModes.Write;
                case DocumentViewModes.Sign: return DocumentProcessModes.Sign;
                case DocumentViewModes.WriteAndSign: return DocumentProcessModes.WriteAndSign;
                default: throw new InvalidCastException();
            }
        }

        private DocumentModes MapDocumentViewMode(DocumentViewModes documentViewMode)
        {
            switch (documentViewMode)
            {
                case DocumentViewModes.Read: return DocumentModes.ViewDocument;
                case DocumentViewModes.Write: return DocumentModes.EditDocument;
                case DocumentViewModes.Sign: return DocumentModes.SignDocument;
                case DocumentViewModes.WriteAndSign: return DocumentModes.EditAndSignDocument;
                default: throw new InvalidCastException();
            }
        }

        private async Task SearchForExistingProcessAndCheckAccessAsync(long processID, CancellationToken cancellationToken)
        {
            var process = (await SearchAsync(new DocumentProcessSearchCriteria()
            {
                DocumentProcessID = processID,
                ApplicantCIN = UserAccessor.User.CIN
            }, cancellationToken)).SingleOrDefault();

            EnsureExistingProcessAndCheckAccess(processID, process);
        }

        private void EnsureExistingProcessAndCheckAccess(long processID, DocumentProcess process)
        {
            if (process == null)
                throw new NoDataFoundException(processID.ToString(), "DocumentProcess");

            if (process.ApplicantID != UserAccessor.User.LocalClientID)
                throw new AccessDeniedException(processID.ToString(), "DocumentProcess", UserAccessor.User.LocalClientID);
        }

        /// <summary>
        /// Ако е възможно извлича идентификатора на услугата по SUNAUServiceURI от тага в xml документа ако е заявление иначе връща null.
        /// </summary>
        /// <param name="docXml"></param>
        /// <returns></returns>
        private int? TryGetDocumentServiceID(XmlDocument docXml)
        {
            var docRootNamespace = XmlHelpers.GetDocumentRootNamespace(docXml);
            var nm = new XmlNamespaceManager(new NameTable());
            nm.AddNamespace("sunau", "http://ereg.egov.bg/segment/0009-000152");

            var sunauServicUriExpresion = XPathExpression.Compile("//sunau:SUNAUServiceURI", nm);
            var navigator = docXml.CreateNavigator();
            var sunauUri = navigator.SelectSingleNode(sunauServicUriExpresion);

            if (sunauUri != null)
            {
                var service = Services.Search().Single(s => s.SunauServiceUri == sunauUri.Value);

                return service.ServiceID;
            }
            
            return null;
        }

        #endregion
    }
}
