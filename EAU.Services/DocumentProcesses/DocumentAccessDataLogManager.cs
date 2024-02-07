using EAU.Audit;
using EAU.Audit.Models;
using EAU.KAT.Documents.Domain;
using EAU.Nomenclatures;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.EPortal.Models;

namespace EAU.Services.DocumentProcesses
{
    public interface IDocumentAccessDataLogManager
    {
        /// <summary>
        /// Дали да запише в Одит-а лог с достъпените данни до документ от тип documentTypeId
        /// </summary>
        /// <param name="documentTypeId"></param>
        /// <returns></returns>
        bool ShouldLogDocumentAccessData(int documentTypeId);

        /// <summary>
        /// Записва в Одит-а лог с достъпените данни до документ
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task LogDocumentAccessDataAsync(LogDocumentAccessDataRequest request, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Данни за запис в Одит-а лог с достъпените данни до документ
    /// </summary>
    public class LogDocumentAccessDataRequest
    {   
        /// <summary>
        /// Имена на заявител, подал заявлението към което е преглеждания документ
        /// </summary>
        public string ApplicantNames { get; set; }

        /// <summary>
        /// Идентификатор на заявител, подал заявлението към което е преглеждания документ
        /// </summary>
        public string ApplicantIdentifier { get; set; }

        /// <summary>
        /// Тип на документа
        /// </summary>
        public int DocumentTypeId { get; set; }

        /// <summary>
        /// УРИ на документа
        /// </summary>
        public string DocumentUri { get; set; }

        /// <summary>
        /// Индексирани данни от съдържанието на документа
        /// </summary>
        public List<ServiceInstanceDocumentFieldInfo> DocumentFields { get; set; }

        /// <summary>
        /// Ip address на потребителя достъпил данните
        /// </summary>
        public byte[] IpAddress { get; set; }
    }

    internal class DocumentAccessDataLogManager : IDocumentAccessDataLogManager
    {
        private readonly IAuditService _auditService;
        private readonly IDocumentTypes _documentTypes;
        private readonly ILogger _logger;

        private readonly string[] _enabledDocTypeURIsForLog = new string[] { DocumentTypeUrisKAT.ReportForChangingOwnershipV2 };

        public DocumentAccessDataLogManager(IDocumentTypes documentTypes, ILogger<DocumentAccessDataLogManager> logger, IAuditService auditService)
        {
            _documentTypes = documentTypes;
            _logger = logger;
            _auditService = auditService;
        }

        public bool ShouldLogDocumentAccessData(int documentTypeId)
        {
            var docType = _documentTypes[documentTypeId];
            return _enabledDocTypeURIsForLog.Contains(docType.Uri);
        }

        public async Task LogDocumentAccessDataAsync(LogDocumentAccessDataRequest request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.DocumentUri)) throw new ArgumentNullException(nameof(request.DocumentUri));

            if (request.DocumentFields == null || !request.DocumentFields.Any())
                return;

            IEnumerable<(DocumentAccessedDataTypes dataType, string dataValue)> interestedFields = GetInterestedFieldForDocument(request.DocumentTypeId, request.DocumentFields);

            if (!interestedFields.Any())
            {
                _logger.LogWarning($"No fields to log access data for document {request.DocumentUri}");
                return;
            }

            var accessedData = new DocumentAccessedData
            {
                DocumentTypeId = request.DocumentTypeId,
                DocumentUri = request.DocumentUri,
                ApplicantIdentifier = request.ApplicantIdentifier,
                ApplicantNames = request.ApplicantNames,
                IpAddress = request.IpAddress,
                DataValues = interestedFields.Select(f => new DocumentAccessedDataValue { DataType = f.dataType, DataValue = f.dataValue })
            };

            await _auditService.CreateDocumentAccessedDataAsync(accessedData, cancellationToken);            
        }

        public IEnumerable<(DocumentAccessedDataTypes dataType, string dataValue)> GetInterestedFieldForDocument(int documentTypeId, List<ServiceInstanceDocumentFieldInfo> documentFields)
        {
            var docType = _documentTypes[documentTypeId];
            var docTypeUri = docType.Uri;

            if (docTypeUri == DocumentTypeUrisKAT.ReportForChangingOwnershipV2)
            {
                foreach (var documentField in documentFields)
                {
                    if (documentField.ValueType == IndexedFieldInfoTypes.VehicleRegNumber)
                        yield return (DocumentAccessedDataTypes.VehicleRegNumber, documentField.Value);

                    if (documentField.ValueType == IndexedFieldInfoTypes.VehicleOwnersChange)
                    {
                        // format here is OWNER_TYPE|OWNER_IDENT|OWNER_NAMES
                        var valueItems = documentField.Value.Split('|');

                        yield return (DocumentAccessedDataTypes.PersonIdentifier, valueItems[1]);
                        yield return (DocumentAccessedDataTypes.PersonIdentifierAndNames, $"{valueItems[1]} {valueItems[2]}");
                    }
                }
            }
        }
    }
}
