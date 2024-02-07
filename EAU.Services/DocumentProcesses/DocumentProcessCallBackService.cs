using CNSys;
using CNSys.Data;
using EAU.Audit;
using EAU.Audit.Models;
using EAU.Documents.Domain;
using EAU.Services.DocumentProcesses.Models;
using EAU.Services.DocumentProcesses.Repositories;
using EAU.Services.ServiceInstances;
using EAU.Services.ServiceInstances.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.EPortal.Models;

namespace EAU.Services.DocumentProcesses
{
    internal class DocumentProcessCallBackService : IDocumentProcessCallBackService
    {
        private readonly IDocumentProcessRepository DocumentProcessRepository;
        private readonly IDocumentProcessService DocumentProcessService;
        private readonly IServiceInstanceService ServiceInstanceService;
        private readonly IAuditService AuditService;
        private readonly IDbContextOperationExecutor DBContextOperationExecutor;

        public DocumentProcessCallBackService(
           IDocumentProcessRepository docProcessRepository,
           IDocumentProcessService docProcessService,
           IServiceInstanceService serviceInstanceService,
           IDbContextOperationExecutor dbContextOperationExecutor,
           IAuditService auditService
          )
        {
            DocumentProcessRepository = docProcessRepository;
            DocumentProcessService = docProcessService;
            ServiceInstanceService = serviceInstanceService;
            DBContextOperationExecutor = dbContextOperationExecutor;
            AuditService = auditService;
        }

        public Task<OperationResult> RegistrationCompletedAsync(DocumentRegistrationResponse regResponse, CancellationToken cancellationToken)
        {
            return DBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                var processID = long.Parse(regResponse.RequestID.Split('_')[0]);
                var process = (await DocumentProcessService.SearchAsync(new DocumentProcessSearchCriteria()
                {
                    DocumentProcessID = processID
                }, token)).SingleOrDefault();

                if (process != null &&
                    (process.Status == ProcessStatuses.Sending || process.Status == ProcessStatuses.Accepted))
                {
                    if (string.IsNullOrEmpty(process.CaseFileURI))//Ново заявление
                    {
                        if (regResponse.RegistrationStatus == DocumentRegistrationStatuses.Registered)
                        {
                            process.Status = ProcessStatuses.Registered;
                            process.CaseFileURI = regResponse.CaseFileUri.ToString();

                            await DocumentProcessRepository.UpdateAsync(process, token);

                            await ServiceInstanceService.CreateAsync(new ServiceInstanceCreateRequest()
                            {
                                DocumentProcess = process,
                                ServiceInstance = regResponse.ServiceInstance
                            }, token);
                        }
                        else
                        {
                            process.Status = ProcessStatuses.NotRegistered;
                            process.NotAcknowledgedMessageURI = regResponse.NotAcceptedDocumentUri.ToString();

                            await DocumentProcessRepository.UpdateAsync(process, token);

                        }
                    }
                    else //Нередовности
                    {
                        if (regResponse.RegistrationStatus == DocumentRegistrationStatuses.Registered)
                        {
                            process.Status = ProcessStatuses.Registered;

                            await DocumentProcessRepository.UpdateAsync(process, token);
                        }
                        else
                        {
                            process.Status = ProcessStatuses.NotRegistered;
                            process.NotAcknowledgedMessageURI = regResponse.NotAcceptedDocumentUri.ToString();

                            await DocumentProcessRepository.UpdateAsync(process, token);
                        }
                    }
                }

                if (regResponse.RegistrationStatus == DocumentRegistrationStatuses.Registered)
                {
                    // При подаване на заявление за отстраняване на нередовности URI-то на регистрирания документ не съвпада с ури на преписката.
                    // Затова търсим последния документ от тип "Потвърждение за получаване" и след това взимаме първият документ преди него (това ще е регистрираният документ, както в случая при подаване на заявление за нередовности,
                    // така и при подаване на първоначално завление!
                    var receiptAcknowledgedDocInfo = regResponse.ServiceInstance.CaseFile.Documents
                        .Where(d => d.DocumentTypeURI == DocumentTypeUris.ReceiptAcknowledgeMessageUri)
                        .OrderByDescending(d => d.CreationTime)
                        .FirstOrDefault();

                    DocumentInfo registeredDocInfo = receiptAcknowledgedDocInfo != null ?
                        regResponse.ServiceInstance.CaseFile.Documents
                        .Where(d => d.CreationTime < receiptAcknowledgedDocInfo.CreationTime)
                        .OrderByDescending(d => d.CreationTime)
                        .FirstOrDefault()
                        : null;

                    var userIPAddress = IPAddress.Parse(process.AdditionalData["ipAddress"]);
                    var logAction = new LogAction()
                    {
                        ObjectType = ObjectTypes.Document,
                        ActionType = ActionTypes.Submission,
                        Functionality = Common.Models.Functionalities.Portal,
                        UserID = process.ApplicantID,
                        Key = registeredDocInfo?.DocumentURI.ToString(),
                        LoginSessionID = string.IsNullOrEmpty(process.AdditionalData["loginSessionID"]) ? (Guid?)null : Guid.Parse(process.AdditionalData["loginSessionID"]),
                        IpAddress = userIPAddress.GetAddressBytes(),
                        AdditionalData = new Utilities.AdditionalData()
                    };
                    logAction.AdditionalData.Add("DocumentTypeName", registeredDocInfo.DocumentTypeName);

                    await AuditService.CreateLogActionAsync(logAction, token);
                }

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }
    }
}
