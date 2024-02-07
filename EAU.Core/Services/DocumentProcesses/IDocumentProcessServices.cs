using CNSys;
using EAU.Documents;
using EAU.Services.DocumentProcesses.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using WAIS.Integration.EPortal.Models;

namespace EAU.Services.DocumentProcesses
{
    public class NewProcessRequest
    {
        public int? ServiceID { get; set; }

        public string RequestID { get; set; }

        public string CaseFileURI { get; set; }

        public string DocumentURI { get; set; }

        public string RemovingIrregularitiesInstructionURI { get; set; }

        public bool? WithdrawService { get; set; }

        public string AdditionalApplicationURI { get; set; }

        public Stream DocumentXMLContent { get; set; }
                
        public string DocumentMetadataURL { get; set; }

        public long? DocProcessId { get; set; }

        public string NotAcknowledgedMessageURI { get; set; }
    }

    public class LoadProcessRequest
    {
        public long? ProcessID { get; set; }

        public int? ServiceID { get; set; }

        public string BackofficeGuid { get; set; }
    }

    public interface IDocumentProcessSigningService
    {
        Task<OperationResult<Guid>> StartSigningAsync(long processID, CancellationToken cancellationToken);

        /// <summary>
        /// Започва процес по подписване на документ прикачен към процес.
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>
        /// <param name="docID">Идентификатор на документ.</param>
        /// <param name="cancellationToken"></param>       
        Task<OperationResult<Guid>> StartSigningAttachedDocumentAsync(long processID, long docID, CancellationToken cancellationToken);
    }

    public interface IDocumentProcessSigningCallbackService
    {
        Task SigningCompletedAsync(Guid signingGiud, Stream documentContent, Guid? userSessionID, Guid? loginSessionID, string ipAddress, int? userCIN, CancellationToken cancellationToken);

        Task SigningRejectedAsync(Guid signingGiud, CancellationToken cancellationToken);

        /// <summary>
        /// Отказва процес по подписване на документ прикачен към процес.
        /// </summary>
        /// <param name="signingGiud">Идентификатор на процес по подписване.</param>
        /// <param name="cancellationToken"></param>
        Task SigningAttachedDocumentRejectedAsync(Guid signingGiud, CancellationToken cancellationToken);

        /// <summary>
        /// Приключва процес по подписване на документ прикачен към процес.
        /// </summary>
        /// <param name="signingGiud">Идентификатор на процес по подписване.</param>
        /// <param name="documentContent">Подписан документ.</param>
        /// <param name="userSessionID"></param>
        /// <param name="loginSessionID"></param>
        /// <param name="ipAddress"></param>
        /// <param name="userCIN"></param>
        /// <param name="cancellationToken"></param>     
        Task SigningAttachedDocumentCompletedAsync(Guid signingGiud, Stream documentContent, Guid? userSessionID, Guid? loginSessionID, string ipAddress, int? userCIN, CancellationToken cancellationToken);
    }

    public interface IDocumentProcessService
    {
        Task<IEnumerable<DocumentProcess>> SearchAsync(DocumentProcessSearchCriteria searchCriteria, CancellationToken cancellationToken);

        Task<OperationResult<DocumentProcess>> StartAsync(NewProcessRequest request, CancellationToken cancellationToken);

        Task DeleteAsync(long processID, CancellationToken cancellationToken);

        Task<OperationResult> UpdateFormAsync(long processID, string formContent, CancellationToken cancellationToken);

        Task<OperationResult> ReturnToInProcessStatusAsync(long processID, CancellationToken cancellationToken);

        /// <summary>
        /// Стартира порцеса по изпращане на заявлението.
        /// </summary>
        /// <param name="processID">Идентификатор на процес по заявяване.</param>
        /// <param name="cancellationToken"></param>        
        Task<OperationResult> StartSendingAsync(long processID, CancellationToken cancellationToken);

        Task SendAsync(long processID, CancellationToken cancellationToken);

        Task<bool> HasChangesInApplicationsNomenclatureAsync(DocumentProcess process);

        Task<IEnumerable<DocumentProcessContent>> SearchDocumentProcessContentsAsync(DocumentProcessContentSearchCriteria documentProcessContentSearchCriteria, CancellationToken cancellationToken);

        DocumentProcessTypes GetDocumentProcessType(string removingIrregularitiesInstructionURI, string additionalApplicationURI, string caseFileURI, string documentMetadataURL, int? serviceID, bool? withdrawService);
    }

    public interface IDocumentProcessAttachedDocumentService
    {
        /// <summary>
        /// Търси документи прикачени към процес.
        /// </summary>
        /// <param name="criteria">Критерии за търсене.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Документи прикачени към процес</returns>
        Task<IEnumerable<AttachedDocument>> SearchAttachedDocumentsAsync(AttachedDocumentSearchCriteria criteria, CancellationToken cancellationToken);

        /// <summary>
        /// Добавя документ към процес.
        /// </summary>
        /// <param name="processID">Идентификатор на процес.</param>
        /// <param name="doc">Документ.</param>
        /// <param name="content">Съдържание.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Документ.</returns>
        Task<OperationResult<AttachedDocument>> AddAttachedDocumentAsync(long processID, AttachedDocument doc, Stream content, CancellationToken cancellationToken);

        /// <summary>
        /// Обновява документ прикачен към процес.
        /// </summary>
        /// <param name="processID">Идентификатор на процес.</param>
        /// <param name="doc">Документ.</param>
        /// <param name="content">Съдържание.</param>
        /// <param name="cancellationToken"></param>       
        Task UpdateAttachedDocumentAsync(long processID, AttachedDocument doc, Stream content, CancellationToken cancellationToken);

        /// <summary>
        /// Изтрива документ прикачен към процес.
        /// </summary>
        /// <param name="processID">Идентификатор на процес.</param>
        /// <param name="docID">Идентификатор на документ.</param>
        /// <param name="cancellationToken"></param>       
        Task DeleteAttachedDocumentAsync(long processID, long docID, CancellationToken cancellationToken);
    }

    public interface IDocumentProcessCallBackService
    {
        /// <summary>
        /// Обработва регистрацията на зявлението в бекенда
        /// </summary>
        /// <param name="regResponse">Отговор на заявката за регистрация</param>
        /// <param name="cancellationToken">Токен за отказване.</param>
        /// <returns></returns>
        Task<OperationResult> RegistrationCompletedAsync(DocumentRegistrationResponse regResponse, CancellationToken cancellationToken);
    }

    public interface IDocumentProcessFormService
    {
        XmlDocument ParseXmlDocument(Stream stream);

        Task<OperationResult<DocumentFormData>> InitFormAsync(DocumentModes initMode, int? serviceID, int documentTypeID, AdditionalData additionalData, XmlDocument formXml, CancellationToken cancellationToken);

        Task<OperationResult> CreateFormAsync(DocumentProcess process, DocumentFormData formData, CancellationToken cancellationToken);

        Task<OperationResult<DocumentProcessContent>> GenerateFormXmlContentAsync(DocumentProcess process, bool? loadContent, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за генериране на html за принтиране на документ.
        /// </summary>
        /// <param name="documentProcessID">Идентификатор на процес.</param>
        /// <param name="appicationPath">Път на приложението.</param>
        /// <param name="cancellationToken">Токен поотказване.</param>
        /// <returns>Html за принтиране на документ.</returns>
        Task<OperationResult<Stream>> GenerateFormHtmlContentAsync(long documentProcessID, string appicationPath, CancellationToken cancellationToken);

        Task<OperationResult<Stream>> DownloadDocumentContent(long processID, CancellationToken cancellationToken);

        string GetDocumentTypeURI(XmlDocument xmlContent);
    }
}
