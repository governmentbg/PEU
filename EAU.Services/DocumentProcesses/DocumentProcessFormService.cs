using CNSys;
using CNSys.Data;
using CNSys.Xml;
using EAU.Common;
using EAU.Documents;
using EAU.Documents.Domain;
using EAU.Nomenclatures;
using EAU.Security;
using EAU.Services.DocumentProcesses.Models;
using EAU.Services.DocumentProcesses.Repositories;
using EAU.Utilities;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace EAU.Services.DocumentProcesses
{
    internal class DocumentProcessFormService : IDocumentProcessFormService
    {
        private readonly IAttachedDocumentRepository AttachedDocumentRepository;
        private readonly IDocumentProcessContentRepository DocumentProcessContentRepository;
        private readonly IDocumentProcessRepository DocumentProcessRepository;
        private readonly IDbContextOperationExecutor DBContextOperationExecutor;
        private readonly IDocumentFormServiceProvider DocumentServicesProvider;
        private readonly IDocumentTypes DocumentTypes;
        private readonly IOptionsMonitor<GlobalOptions> OptionsMonitor;
        private readonly IStringLocalizer StringLocalizer;
        private readonly IEAUUserAccessor UserAccessor;
        private readonly IServices Services;

        private readonly static XPathExpression RegisterIndexExpression;
        private readonly static XPathExpression BatchNumberExpression;

        static DocumentProcessFormService()
        {
            var nm = new XmlNamespaceManager(new NameTable());
            nm.AddNamespace("p", "http://ereg.egov.bg/segment/0009-000022");

            RegisterIndexExpression = XPathExpression.Compile("//p:RegisterIndex", nm);
            BatchNumberExpression = XPathExpression.Compile("//p:BatchNumber", nm);
        }

        public DocumentProcessFormService(
            IAttachedDocumentRepository attachedDocumentRepository,
            IDocumentProcessContentRepository documentProcessContentRepository,
            IDocumentProcessRepository documentProcessRepository,
            IDbContextOperationExecutor dbContextOperationExecutor,
            IDocumentFormServiceProvider documentServicesProvider,
            IDocumentTypes documentTypes,
                        IOptionsMonitor<GlobalOptions> optionsMonitor,
            IStringLocalizer stringLocalizer,
            IEAUUserAccessor userAccessor,
            IServices services
            )
        {
            DocumentProcessContentRepository = documentProcessContentRepository;
            DBContextOperationExecutor = dbContextOperationExecutor;
            AttachedDocumentRepository = attachedDocumentRepository;
            DocumentProcessRepository = documentProcessRepository;
            DocumentServicesProvider = documentServicesProvider;
            DocumentTypes = documentTypes;
            OptionsMonitor = optionsMonitor;
            StringLocalizer = stringLocalizer;
            UserAccessor = userAccessor;
            Services = services;
        }

        #region IFormDocumentProcessService

        public async Task<OperationResult<DocumentFormData>> InitFormAsync(DocumentModes initMode, int? serviceID, int documentTypeID, AdditionalData additionalData, XmlDocument formXml, CancellationToken cancellationToken)
        {
            IDocumentFormService oldApplicationFormService = null;

            var docType = DocumentTypes[documentTypeID];

            var initService = DocumentServicesProvider.GetDocumentFormInitializationService(docType.Uri);
            var formService = DocumentServicesProvider.GetDocumentFormService(docType.Uri);

            DocumentFormData formData = null;
            List<object> signatures = null;

            if (formXml == null)
            {
                formData = new DocumentFormData();
                formData.Form = formService.CreateDocumentForm();
            }
            else if (formXml != null && initMode == DocumentModes.WithdrawService)
            {
                if (additionalData.ContainsKey("originalApplicationDocumentTypeId")
                    && int.TryParse(additionalData["originalApplicationDocumentTypeId"].ToString(), out int oldDocumentTypeID))
                {
                    var oldDocType = DocumentTypes[oldDocumentTypeID];
                    oldApplicationFormService = DocumentServicesProvider.GetDocumentFormService(oldDocType.Uri);

                    var domainForm = oldApplicationFormService.DeserializeDomainForm(formXml);

                    formData = await oldApplicationFormService.BuildWithdrawServiceFormAsync(domainForm, cancellationToken);

                    if (initMode == DocumentModes.ViewDocument)
                    {
                        signatures = await oldApplicationFormService.GetDocumentSignatures(formXml);

                    }
                }
            }
            else
            {
                var domainForm = formService.DeserializeDomainForm(formXml);

                formData = await formService.TransformToDocumentFormAsync(domainForm, cancellationToken);

                if (initMode == DocumentModes.ViewDocument)
                {
                    signatures = await formService.GetDocumentSignatures(formXml);

                }
            }

            var intiFormResult = await initService.InitializeDocumentFormAsync(new DocumentFormInitializationRequest()
            {
                Form = formData.Form,
                AdditionalData = additionalData,
                Mode = initMode,
                ServiceID = serviceID,
                Signatures = signatures,
            }, cancellationToken);

            if (!intiFormResult.IsSuccessfullyCompleted)
            {
                return new OperationResult<DocumentFormData>(intiFormResult.Errors);
            }

            return new OperationResult<DocumentFormData>(OperationResultTypes.SuccessfullyCompleted)
            {
                Result = formData
            };
        }

        public async Task<OperationResult> CreateFormAsync(DocumentProcess process, DocumentFormData formData, CancellationToken cancellationToken)
        {
            var docType = DocumentTypes[process.DocumentTypeID.Value];

            var documentFormService = DocumentServicesProvider.GetDocumentFormService(docType.Uri);

            var content = await documentFormService.SerializeDocumentFormAsStringAsync(formData.Form, cancellationToken);

            var jsonContent = new DocumentProcessContent()
            {
                DocumentProcessID = process.DocumentProcessID,
                Type = DocumentProcessContentTypes.FormJSON,
                TextContent = content
            };

            await DocumentProcessContentRepository.CreateAsync(jsonContent, cancellationToken);

            process.ProcessContents = new List<DocumentProcessContent>();
            process.ProcessContents.Add(jsonContent);


            if (formData.AttachedDocuments != null)
            {
                process.AttachedDocuments = new List<AttachedDocument>();

                foreach (var attDoc in formData.AttachedDocuments)
                {
                    using (var attDocStream = new MemoryStream(attDoc.Content))
                    {
                        var docContent = new DocumentProcessContent()
                        {
                            DocumentProcessID = process.DocumentProcessID,
                            Type = DocumentProcessContentTypes.AttachedDocument,
                            Content = attDocStream
                        };

                        await DocumentProcessContentRepository.CreateAsync(docContent, cancellationToken);
                        process.ProcessContents.Add(docContent);

                        var doc = new AttachedDocument
                        {
                            DocumentProcessID = process.DocumentProcessID,
                            DocumentProcessContentID = docContent.DocumentProcessContentID,
                            AttachedDocumentGuid = attDoc.Guid,
                            Description = attDoc.Description,
                            DocumentTypeID = attDoc.DocumenTypeID,
                            FileName = attDoc.FileName,
                            MimeType = attDoc.MimeType,
                            Content = docContent
                        };

                        await AttachedDocumentRepository.CreateAsync(doc);

                        process.AttachedDocuments.Add(doc);
                    }

                }
            }

            return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
        }

        public Task<OperationResult<DocumentProcessContent>> GenerateFormXmlContentAsync(DocumentProcess process, bool? loadContent, CancellationToken cancellationToken)
        {
            return DBContextOperationExecutor.ExecuteAsync<DocumentProcessContent>(async (dbcontext, token) =>
            {
                IErrorCollection validationResult = null;

                XmlDocument documentXML;

                var docType = DocumentTypes[process.DocumentTypeID.Value];
                var documentFormService = DocumentServicesProvider.GetDocumentFormService(docType.Uri);
                var documentFormValidationService = DocumentServicesProvider.GetDocumentFormValidationService(docType.Uri);

                bool doValidate = true;

                //всеки път се извършва валидация освен когато е в тип приложение за редакция и визуализация и е в режим на редакция.
                if (process.Type == DocumentProcessTypes.BackOffice &&
                    process.Mode == DocumentProcessModes.Write)
                {
                    doValidate = false;
                }

                #region Create Form Data and transform to domain form

                var formData = await CreateDocumentFormDataAsync(documentFormService, process, true, cancellationToken);

                // тази проверка се прави 2 пъти - преди и след сериализацията към xml. Правим я преди сериализацията, защото сериализацията е бавна и тежка операция. 
                // Правим я и след нея, защото размерът на xml e малко по-голям и може да надхвърли лимита, въпреки че предната проверка е минала.
                var attachedDocumentsTotalSize = formData.AttachedDocuments.Select(a => a.Content?.Length).Sum();
                if (attachedDocumentsTotalSize > (OptionsMonitor.CurrentValue.GL_APPLICATION_MAX_SIZE * 1024))
                {
                    var localizedError = StringLocalizer["GL_APPLICATION_MAX_SIZE2_E"].Value.Replace("{MAX_APPLICATION_SIZE}",
                            OptionsMonitor.CurrentValue.GL_APPLICATION_MAX_SIZE.ToString());

                    return new OperationResult<DocumentProcessContent>(new ErrorCollection() { new TextError(localizedError, localizedError) });
                }

                var domainForm = await documentFormService.TransformToDomainFormAsync(formData, token);

                #endregion

                #region Validate Document Form and domain form

                if (doValidate)
                {
                    //Проверка, дали услугата съществува и е активна.
                    if (!Services.Search().Any(x => x.ServiceID == process.ServiceID && x.IsActive == true))
                    {
                        return new OperationResult<DocumentProcessContent>(new ErrorCollection() { new TextError("GL_SERVICE_CANNOT_BE_REQUESTED_E", "GL_SERVICE_CANNOT_BE_REQUESTED_E") });
                    }

                    validationResult = await documentFormValidationService.ValidateAsync(formData, token);

                    if (validationResult.HasErrors)
                    {
                        return new OperationResult<DocumentProcessContent>(validationResult);
                    }

                    validationResult = documentFormValidationService.ValidateDomainForm(domainForm);

                    if (validationResult.HasErrors)
                    {
                        return new OperationResult<DocumentProcessContent>(validationResult);
                    }
                }

                #endregion

                #region Serialize Document 

                documentXML = documentFormService.SerializeDomainForm(domainForm);

                #endregion

                #region Xsd Validation

                ///валидираме спрямо отслабените схеми. 
                validationResult = documentFormValidationService.ValidateByXSD(documentXML, true);

                if (validationResult.HasErrors)
                    return new OperationResult<DocumentProcessContent>(validationResult);

                #endregion

                #region Create Xml Content

                //Delete Old Xml Content if any
                if (process.ProcessContents.Any(c => c.Type == DocumentProcessContentTypes.FromXML))
                {
                    await DocumentProcessContentRepository.DeleteAsync(process.ProcessContents.Single(c => c.Type == DocumentProcessContentTypes.FromXML));
                }

                var result = new OperationResult<DocumentProcessContent>(OperationResultTypes.SuccessfullyCompleted);

                using (var stream = XmlHelpers.GetStreamFromXmlDocument(documentXML.OuterXml))
                {
                    if (stream.Length > (OptionsMonitor.CurrentValue.GL_APPLICATION_MAX_SIZE * 1024))
                    {
                        var localizedError = StringLocalizer["GL_APPLICATION_MAX_SIZE2_E"].Value.Replace("{MAX_APPLICATION_SIZE}",
                                OptionsMonitor.CurrentValue.GL_APPLICATION_MAX_SIZE.ToString());

                        return new OperationResult<DocumentProcessContent>(new ErrorCollection() { new TextError(localizedError, localizedError) });
                    }
                }

                try
                {
                    var content = new DocumentProcessContent()
                    {
                        DocumentProcessID = process.DocumentProcessID,
                        Type = DocumentProcessContentTypes.FromXML,
                        TextContent = documentXML.OuterXml
                    };

                    await DocumentProcessContentRepository.CreateAsync(content, token);

                    result.Result = content;
                }
                finally
                {
                    if (!loadContent.GetValueOrDefault())
                        result.Result.Content = null;
                }

                #endregion

                return result;
            }, cancellationToken);
        }

        public Task<OperationResult<Stream>> GenerateFormHtmlContentAsync(long documentProcessID, string appicationPath, CancellationToken cancellationToken)
        {
            return DBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                var result = new OperationResult<Stream>(OperationResultTypes.SuccessfullyCompleted);

                var process = (await DocumentProcessRepository.SearchAsync(new DocumentProcessSearchCriteria
                {
                    DocumentProcessID = documentProcessID,
                    LoadOption = new DocumentProcessLoadOption()
                    {
                        LoadAllContents = true,
                        LoadAttachedDocument = true
                    }
                })).SingleOrDefault();

                EnsureExistingProcessAndCheckAccess(documentProcessID, process);

                var docType = DocumentTypes[process.DocumentTypeID.Value];
                var documentFormService = DocumentServicesProvider.GetDocumentFormService(docType.Uri);
                var documentFormPrintService = DocumentServicesProvider.GetDocumentFormPrintService(docType.Uri);

                #region Load DomainFormXMl


                XmlDocument domainFormXml = null;

                if (process.Mode == DocumentProcessModes.Write ||
                    process.Mode == DocumentProcessModes.WriteAndSign)
                {
                    var formData = await CreateDocumentFormDataAsync(documentFormService, process, false, cancellationToken);

                    var domainFrom = await documentFormService.TransformToDomainFormAsync(formData, token);

                    domainFormXml = documentFormService.SerializeDomainForm(domainFrom);
                }
                else
                {
                    DocumentProcessContent processContent = null;

                    processContent = process.ProcessContents.Single(pc => pc.Type == DocumentProcessContentTypes.FromXML);

                    domainFormXml = XmlHelpers.CreateXmlDocument(processContent.TextContent);
                }

                #endregion

                #region Do Transformation

                MemoryStream docProcStream = new MemoryStream();
                using (StreamWriter writer = new StreamWriter(docProcStream, Encoding.UTF8, 0x400, true))
                {
                    await documentFormPrintService.GetPrintPreviewHtmlAsync(writer, domainFormXml, appicationPath, cancellationToken);
                }

                docProcStream.Position = 0;

                #endregion

                result.Result = docProcStream;

                return result;
            }, cancellationToken);
        }

        public Task<OperationResult<Stream>> DownloadDocumentContent(long processID, CancellationToken cancellationToken)
        {
            return DBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                var result = new OperationResult<Stream>(OperationResultTypes.SuccessfullyCompleted);
                var process = (await DocumentProcessRepository.SearchAsync(new DocumentProcessSearchCriteria()
                {
                    DocumentProcessID = processID,
                    LoadOption = new DocumentProcessLoadOption()
                    {
                        LoadAllContents = true,
                        LoadAttachedDocument = true
                    }
                })).SingleOrDefault();

                EnsureExistingProcessAndCheckAccess(processID, process);

                var docType = DocumentTypes[process.DocumentTypeID.Value];
                var documentFormService = DocumentServicesProvider.GetDocumentFormService(docType.Uri);

                #region Load DomainFormXMl

                if (process.Mode == DocumentProcessModes.Write || process.Mode == DocumentProcessModes.WriteAndSign)
                {
                    var formData = await CreateDocumentFormDataAsync(documentFormService, process, true, cancellationToken);

                    var domainForm = await documentFormService.TransformToDomainFormAsync(formData, token);

                    var documentXml = documentFormService.SerializeDomainForm(domainForm);

                    result.Result = XmlHelpers.GetStreamFromXmlDocument(documentXml);
                }
                else
                {
                    DocumentProcessContent processContent = null;

                    processContent = process.ProcessContents.Single(pc => pc.Type == DocumentProcessContentTypes.FromXML);

                    result.Result = XmlHelpers.GetStreamFromXmlDocument(processContent.TextContent);
                }

                #endregion

                return result;

            }, cancellationToken);
        }

        public XmlDocument ParseXmlDocument(Stream stream)
        {
            return XmlHelpers.CreateXmlDocument(stream);
        }

        public string GetDocumentTypeURI(XmlDocument xmlContent)
        {
            var rootNamespace = XmlHelpers.GetDocumentRootNamespace(xmlContent);

            if (rootNamespace == "http://ereg.egov.bg/segment/0009-000017")
                return DocumentTypeUris.ReceiptNotAcknowledgeMessageUri;
            else if (rootNamespace == "http://ereg.egov.bg/segment/0009-000019")
                return DocumentTypeUris.ReceiptAcknowledgeMessageUri;

            var navigator = xmlContent.CreateNavigator();

            var registerIndex = navigator.SelectSingleNode(RegisterIndexExpression);
            var batchNumber = navigator.SelectSingleNode(BatchNumberExpression);

            if (registerIndex == null ||
                batchNumber == null)
                throw new NullReferenceException("registerIndex == null || batchNumber == null");

            return string.Format("{0:D4}-{1:D6}", registerIndex.ValueAsInt, batchNumber.ValueAsInt);
        }

        #endregion

        #region Helpers

        private byte[] ReadBytes(Stream stream)
        {
            byte[] content;

            if (stream is MemoryStream)
            {
                content = ((MemoryStream)stream).ToArray();
            }
            else
            {
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);

                    content = ms.ToArray();
                }
            }

            return content;
        }

        private async Task<DocumentFormData> CreateDocumentFormDataAsync(IDocumentFormService documentFormService, DocumentProcess process, bool loadAttachmentDocumentContent, CancellationToken cancellationToken)
        {
            var formData = new DocumentFormData();

            DocumentProcessContent formContent = process.ProcessContents.Single(pc => pc.Type == DocumentProcessContentTypes.FormJSON);

            if (process.AttachedDocuments != null && process.AttachedDocuments.Count > 0)
            {
                foreach (var doc in process.AttachedDocuments)
                {
                    if (doc.Content == null)
                    {
                        formData.AttachedDocuments.Add((null, doc.Description, doc.AttachedDocumentGuid.Value, doc.MimeType, doc.FileName, doc.DocumentTypeID));
                    }
                    else
                    {
                        byte[] content = null;

                        if (loadAttachmentDocumentContent)
                        {
                            using (doc.Content.Content)
                            {
                                content = ReadBytes(doc.Content.Content);
                            }
                        }

                        formData.AttachedDocuments.Add((content, doc.Description, doc.AttachedDocumentGuid.Value, doc.MimeType, doc.FileName, doc.DocumentTypeID));
                    }
                }
            }

            formData.Form = await documentFormService.DeserializeDocumentFormAsync(formContent.TextContent, cancellationToken);

            return formData;
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
